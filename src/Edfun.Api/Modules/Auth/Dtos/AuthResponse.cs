using Edfun.Api.Modules.Users.Dtos;

namespace Edfun.Api.Modules.Auth.Dtos;

public sealed record AuthResponse(string AccessToken, DateTimeOffset ExpiresAt, UserProfile User);
