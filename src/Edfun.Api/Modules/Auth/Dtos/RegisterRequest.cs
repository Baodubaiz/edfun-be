namespace Edfun.Api.Modules.Auth.Dtos;

public sealed record RegisterRequest(string UserName, string Email, string Password, string? DisplayName);
