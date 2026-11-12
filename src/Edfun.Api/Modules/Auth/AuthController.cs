using Edfun.Api.Common.Results;
using Edfun.Api.Modules.Auth.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edfun.Api.Modules.Auth;

// Route stays "api/users" so the existing client keeps working.
[ApiController]
[Route("api/users")]
public sealed class AuthController(AuthService auth) : ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request) =>
        (await auth.RegisterAsync(request)).ToActionResult(this);

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request) =>
        (await auth.LoginAsync(request)).ToActionResult(this);
}
