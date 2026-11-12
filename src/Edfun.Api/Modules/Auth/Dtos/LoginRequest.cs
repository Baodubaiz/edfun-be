namespace Edfun.Api.Modules.Auth.Dtos;

public sealed record LoginRequest(string UserNameOrEmail, string Password);
