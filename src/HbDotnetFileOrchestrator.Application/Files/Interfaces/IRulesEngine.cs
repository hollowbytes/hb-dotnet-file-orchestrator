using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IRulesEngine
{
    Task<IConnectorOptions[]> RunAsync(Metadata metadata, CancellationToken cancellationToken = default);
}