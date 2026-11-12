using Edfun.Api.Common.Results;
using Edfun.Api.Modules.Auth.Dtos;
using Edfun.Api.Modules.Users.Dtos;
using Edfun.Api.Modules.Users.Entities;
using Microsoft.AspNetCore.Identity;

namespace Edfun.Api.Modules.Auth;

public sealed class AuthService(UserManager<User> userManager, TokenService tokens)
{
    // Error codes keep the "Users." prefix: they are part of the existing API contract.
    private static readonly Error InvalidCredentials =
        new("Users.InvalidCredentials", "Invalid username/email or password.", ErrorType.Unauthorized);

    private static readonly Error LockedOut =
        new("Users.LockedOut", "Too many failed attempts. Try again later.", ErrorType.Locked);

    public async Task<Result<AuthResponse>> RegisterAsync(RegisterRequest request)
    {
        var user = User.Create(request.UserName.Trim(), request.Email.Trim(), request.DisplayName);

        var created = await userManager.CreateAsync(user, request.Password);
        if (!created.Succeeded)
            return new Error(
                "Users.RegistrationFailed",
                string.Join(" ", created.Errors.Select(e => e.Description)));

        return CreateAuthResponse(user);
    }

    public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request)
    {
        var login = request.UserNameOrEmail.Trim();
        var user = login.Contains('@')
            ? await userManager.FindByEmailAsync(login)
            : await userManager.FindByNameAsync(login);

        if (user is null)
            return InvalidCredentials;

        if (await userManager.IsLockedOutAsync(user))
            return LockedOut;

        if (!await userManager.CheckPasswordAsync(user, request.Password))
        {
            await userManager.AccessFailedAsync(user);
            return InvalidCredentials;
        }

        await userManager.ResetAccessFailedCountAsync(user);
        return CreateAuthResponse(user);
    }

    private AuthResponse CreateAuthResponse(User user)
    {
        var (token, expiresAt) = tokens.CreateAccessToken(user);
        return new AuthResponse(token, expiresAt, UserProfile.From(user));
    }
}
