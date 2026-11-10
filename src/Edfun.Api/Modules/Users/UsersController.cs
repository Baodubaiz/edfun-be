using Edfun.Api.Modules.Users.Dtos.Req;
using Edfun.Api.Modules.Users.Dtos.Res;
using System.Security.Claims;
using Edfun.Api.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edfun.Api.Modules.Users;

[ApiController]
[Route("api/users")]
public sealed class UsersController(UsersService users) : ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request) =>
        (await users.RegisterAsync(request)).ToActionResult(this);

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request) =>
        (await users.LoginAsync(request)).ToActionResult(this);

    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<UserProfile>> Me()
    {
        if (!Guid.TryParse(User.FindFirstValue("sub"), out var userId))
            return Unauthorized();

        return (await users.GetProfileAsync(userId)).ToActionResult(this);
    }
}
