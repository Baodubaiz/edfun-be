using Edfun.Api.Modules.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Edfun.Api.Infrastructure.Persistence;

/// <summary>
/// The single DbContext for the whole app.
/// Identity tables (users, roles, ...) come from IdentityDbContext.
/// Add a DbSet here for each new table, e.g. <c>public DbSet&lt;Game&gt; Games =&gt; Set&lt;Game&gt;();</c>
/// </summary>
public sealed class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>(user =>
        {
            user.Property(u => u.DisplayName).HasMaxLength(64).IsRequired();
        });
    }
}
