using AccessManager.Domain.RefreshTokens;
using AccessManager.Domain.Users;
using AccessManager.Infrastructure.Repositories;

public static class RepositoryExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        
        return services;
    }
}
