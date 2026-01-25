namespace HbDotnetFileOrchestrator.Domain.Models;

public record RuleResult(
    string Name,
    string Expression,
    string Error
) : Rule(Name, Expression)
{
    public bool IsFailure => string.IsNullOrEmpty(Error);
    
    public bool IsSuccess => !IsFailure;
}