using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Domain.Interfaces.Services;

public interface IRuleEvaluator
{
    Task<RuleResult[]> RunAsync(Rule[] rules, Metadata metadata, CancellationToken cancellationToken = default);
}