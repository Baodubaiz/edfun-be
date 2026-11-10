namespace Edfun.Api.Common.Modules;

/// <summary>
/// A feature module (like a NestJS @Module). Registers its own services.
/// Controllers are discovered automatically.
/// </summary>
public interface IModule
{
    void Register(IServiceCollection services, IConfiguration configuration);
}

public static class ModuleExtensions
{
    public static IServiceCollection AddModules(
        this IServiceCollection services, IConfiguration configuration, params IModule[] modules)
    {
        foreach (var module in modules)
            module.Register(services, configuration);

        return services;
    }
}
