using Edfun.Api.Modules.Users.Entities;

namespace Edfun.Api.Modules.Users.Dtos;

public sealed record UserProfile(Guid Id, string UserName, string Email, string DisplayName, DateTimeOffset CreatedAt)
{
    public static UserProfile From(User user) =>
        new(user.Id, user.UserName!, user.Email!, user.DisplayName, user.CreatedAt);
}
