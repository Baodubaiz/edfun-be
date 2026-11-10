namespace Gamora.Api.Common.Results;

public enum ErrorType
{
    Validation,
    Unauthorized,
    NotFound,
    Conflict,
    Locked,
}

public sealed record Error(string Code, string Message, ErrorType Type = ErrorType.Validation)
{
    public int StatusCode => Type switch
    {
        ErrorType.Validation => StatusCodes.Status400BadRequest,
        ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
        ErrorType.NotFound => StatusCodes.Status404NotFound,
        ErrorType.Conflict => StatusCodes.Status409Conflict,
        ErrorType.Locked => StatusCodes.Status423Locked,
        _ => StatusCodes.Status400BadRequest,
    };
}

/// <summary>Success-or-error return value, so services don't throw for expected failures.</summary>
public sealed class Result<T>
{
    private readonly T? _value;

    private Result(T? value, Error? error)
    {
        _value = value;
        Error = error;
    }

    public Error? Error { get; }
    public bool IsSuccess => Error is null;

    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Cannot access the value of a failed result.");

    public static Result<T> Success(T value) => new(value, null);
    public static Result<T> Failure(Error error) => new(default, error);

    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(Error error) => Failure(error);
}
