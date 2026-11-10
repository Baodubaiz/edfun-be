using Edfun.Api.Common.Data;
using Edfun.Api.Common.Modules;
using Microsoft.AspNetCore.Identity;

namespace Edfun.Api.Modules.Users;

/// <summary>Like users.module.ts: registers the Users module's services for DI.</summary>
public sealed class UsersModule : IModule
{
    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddIdentityCore<User>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddScoped<UsersService>();
    }
}
