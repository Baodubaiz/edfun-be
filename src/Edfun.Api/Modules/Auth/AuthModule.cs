using Edfun.Api.Common.Modules;

namespace Edfun.Api.Modules.Auth;

/// <summary>Registers JWT authentication (validation + TokenService) and the Auth services.</summary>
public sealed class AuthModule : IModule
{
    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddJwtAuthentication(configuration);
        services.AddScoped<AuthService>();
    }
}
