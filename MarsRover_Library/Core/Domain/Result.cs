namespace MarsRover_Library.Core.Domain;

public class Result<T>
{
    public T Value { get; init; }
    public bool Success { get; init; }
    public string? Message { get; init; }

    public Result(T value, bool success)
    {
        Value = value;
        Success = success;
    }

    public Result(T value, bool success, string message)
    {
        Value = value;
        Success = success;
        Message = message;
    }
}