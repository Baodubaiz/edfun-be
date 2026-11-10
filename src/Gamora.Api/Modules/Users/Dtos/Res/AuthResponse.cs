namespace Gamora.Api.Modules.Users.Dtos.Res;

public sealed record AuthResponse(string AccessToken, DateTimeOffset ExpiresAt, UserProfile User);
