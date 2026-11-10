using Microsoft.AspNetCore.Identity;

namespace Edfun.Api.Modules.Users;

public sealed class User : IdentityUser<Guid>
{
    public string DisplayName { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

    public static User Create(string userName, string email, string? displayName) => new()
    {
        Id = Guid.CreateVersion7(),
        UserName = userName,
        Email = email,
        DisplayName = string.IsNullOrWhiteSpace(displayName) ? userName : displayName.Trim(),
    };
}
