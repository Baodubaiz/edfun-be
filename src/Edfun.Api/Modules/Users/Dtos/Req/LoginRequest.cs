namespace Edfun.Api.Modules.Users.Dtos.Req;

public sealed record LoginRequest(string UserNameOrEmail, string Password);
