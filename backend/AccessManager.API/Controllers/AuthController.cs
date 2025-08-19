using AccessManager.Application.Auth.Commands;
using Microsoft.AspNetCore.Mvc;

namespace AccessManager.API.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly LoginHandler _loginHandler;
    private readonly LogoutHandler _logoutHandler;
    private readonly RefreshHandler _refreshHandler;

    public AuthController(
        LoginHandler loginHandler,
        LogoutHandler logoutHandler,
        RefreshHandler refreshHandler
    )
    {
        _loginHandler = loginHandler;
        _logoutHandler = logoutHandler;
        _refreshHandler = refreshHandler;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _loginHandler.Handle(command);
        if (result.AccessToken == null || result.RefreshToken == null)
            return Unauthorized();

        Response.Cookies.Append("access_token", result.AccessToken.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = result.AccessToken.ExpiresAtUtc,
            SameSite = SameSiteMode.Strict
        });

        Response.Cookies.Append("refresh_token", result.RefreshToken.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = result.RefreshToken.ExpiresAtUtc,
            SameSite = SameSiteMode.Strict
        });

        return Ok();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var refreshToken = Request.Cookies["refresh_token"];
        if (string.IsNullOrEmpty(refreshToken))
        {
            return BadRequest("No refresh token found.");
        }

        await _logoutHandler.Handle(new LogoutCommand { RefreshToken = refreshToken });

        Response.Cookies.Delete("access_token");
        Response.Cookies.Delete("refresh_token");

        return NoContent();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refresh_token"];
        if (string.IsNullOrEmpty(refreshToken))
        {
            return BadRequest("No refresh token found.");
        }

        var result = await _refreshHandler.Handle(new RefreshCommand { RefreshToken = refreshToken });

        if (result.AccessToken == null || result.RefreshToken == null)
        {
            return Unauthorized();
        }

        Response.Cookies.Append("access_token", result.AccessToken.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = result.AccessToken.ExpiresAtUtc,
            SameSite = SameSiteMode.Strict
        });

        Response.Cookies.Append("refresh_token", result.RefreshToken.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = result.RefreshToken.ExpiresAtUtc,
            SameSite = SameSiteMode.Strict
        });

        return NoContent();
    }
}
