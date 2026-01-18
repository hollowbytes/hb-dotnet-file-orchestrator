using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Domain.Interfaces.Services;

public interface IRuleEvaluator
{
    Task<Rule[]> RunAsync(Rule[] rules, Metadata metadata, CancellationToken cancellationToken = default);
}