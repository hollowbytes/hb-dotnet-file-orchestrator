using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Domain.Interfaces.Services;

public interface IRuleEvaluator
{
    Task<Result<Rule>[]> RunAsync(Rule[] rules, Metadata metadata, CancellationToken cancellationToken = default);
}