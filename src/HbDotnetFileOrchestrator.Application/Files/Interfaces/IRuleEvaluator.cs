using HbDotnetFileOrchestrator.Domain.Interfaces;
using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IRuleEvaluator
{
    Task<Rule[]> RunAsync(Rule[] rules, Metadata metadata, CancellationToken cancellationToken = default);
}