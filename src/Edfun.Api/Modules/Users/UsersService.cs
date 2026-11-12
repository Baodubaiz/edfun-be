using Edfun.Api.Common.Results;
using Edfun.Api.Modules.Users.Dtos;
using Edfun.Api.Modules.Users.Entities;
using Microsoft.AspNetCore.Identity;

namespace Edfun.Api.Modules.Users;

public sealed class UsersService(UserManager<User> userManager)
{
    private static readonly Error NotFound =
        new("Users.NotFound", "User not found.", ErrorType.NotFound);

    public async Task<Result<UserProfile>> GetProfileAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        return user is null ? NotFound : UserProfile.From(user);
    }
}
