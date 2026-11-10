namespace Edfun.Api.Modules.Users.Dtos.Req;

public sealed record RegisterRequest(string UserName, string Email, string Password, string? DisplayName);
