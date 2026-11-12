using System.Security.Claims;
using Edfun.Api.Common.Results;
using Edfun.Api.Modules.Users.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edfun.Api.Modules.Users;

[ApiController]
[Route("api/users")]
public sealed class UsersController(UsersService users) : ControllerBase
{
    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<UserProfile>> Me()
    {
        if (!Guid.TryParse(User.FindFirstValue("sub"), out var userId))
            return Unauthorized();

        return (await users.GetProfileAsync(userId)).ToActionResult(this);
    }
}
