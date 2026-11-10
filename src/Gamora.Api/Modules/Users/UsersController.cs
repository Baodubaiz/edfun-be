using Gamora.Api.Modules.Users.Dtos.Req;
using Gamora.Api.Modules.Users.Dtos.Res;
using System.Security.Claims;
using Gamora.Api.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamora.Api.Modules.Users;

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
