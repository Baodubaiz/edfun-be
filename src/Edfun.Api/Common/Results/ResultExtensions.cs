using Microsoft.AspNetCore.Mvc;

namespace Edfun.Api.Common.Results;

public static class ResultExtensions
{
    /// <summary>200 OK with the value, or an RFC 7807 problem response for the error.</summary>
    public static ActionResult<T> ToActionResult<T>(this Result<T> result, ControllerBase controller) =>
        result.IsSuccess
            ? controller.Ok(result.Value)
            : controller.Problem(
                statusCode: result.Error!.StatusCode,
                title: result.Error.Code,
                detail: result.Error.Message);
}
