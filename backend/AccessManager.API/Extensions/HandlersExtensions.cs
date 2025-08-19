using AccessManager.Application.Auth.Commands;
using AccessManager.Application.Users.Commands;
using AccessManager.Application.Users.Queries;

public static class HandlersExtension
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<CreateUserHandler>();
        services.AddScoped<GetUserHandler>();
        services.AddScoped<GetAllUsersHandler>();
        services.AddScoped<DeleteUserHandler>();
        services.AddScoped<EditUserHandler>();
        services.AddScoped<LoginHandler>();
        services.AddScoped<LogoutHandler>();
        services.AddScoped<RefreshHandler>();

        return services;
    }
}
