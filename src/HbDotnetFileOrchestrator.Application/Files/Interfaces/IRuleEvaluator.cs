using HbDotnetFileOrchestrator.Domain.Interfaces;
using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IRuleEvaluator
{
    Task<IStorageOptions[]> RunAsync(Metadata metadata, CancellationToken cancellationToken = default);
}