namespace KyrgyzTest.Application.Extensions.Result;

/// <summary>
/// Base class for result objects.
/// </summary>
public abstract class Result
{

    public Error? Error { get; set; }  // Error associated with the result, if any

    public bool IsSuccess => Error is null; // Indicates whether the result is successful

    public bool IsFailure => Error is not null; // Indicates whether the result is a failure

    public static Result<TValue> Ok<TValue>() => new(); // Creates a successful result with no value

    public static Result<TValue> Ok<TValue>(TValue value) => new(value);  // Creates a successful result with a value

    public static Result<TValue> Fail<TValue>(Error error) => new(error); // Creates a failed result with an error

    public static Result<TValue> Fail<TValue>(string errorCode, string? message)
        => new(new Error(errorCode, message)); // Creates a failed result with an error code and message
}

/// <summary>
/// Represents a result that can hold a value or an error.
/// </summary>
/// <typeparam name="TValue">The type of the value.</typeparam>
public class Result<TValue> : Result
{
    public TValue? Value { get; set; } // The value of the result

    internal Result()
    {
        Value = default;  // Initializes a new instance of the Result<TValue> class
    }
    internal Result(TValue value)
    {
        Value = value;    // Initializes a new instance of the Result<TValue> class with a value
    }
    internal Result(Error error)
    {
        Value = default;   // Initializes a new instance of the Result < TValue > class with an error
        Error = error;
    }

    public static implicit operator Result<TValue>(TValue value) => new(value);  // Implicitly converts a value to a successful result
    public static implicit operator Result<TValue>(Error error) => new(error);  // Implicitly converts an error to a failed result
}