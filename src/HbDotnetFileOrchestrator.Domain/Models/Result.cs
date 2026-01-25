namespace HbDotnetFileOrchestrator.Domain.Models;

public record Result(string? Error) 
{
    public bool IsSuccess => string.IsNullOrEmpty(Error);
    
    public bool IsFailure => !IsSuccess;
    
    public static Result Success() => new ((string?) null);
    
    public static Result Failure(string error) => new (error);
    
    public static Result<T> Failure<T>(T value, string error) => new (value, error);
    
    public static Result<T?> Failure<T>(string error) => new (default, error);
} 

public record Result<T>(T Value, string? Error = null) : Result(Error)
{
    public static implicit operator Result<T>(T value) => new (value, null);
}