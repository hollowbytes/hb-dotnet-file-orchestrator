using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IConnectorProvider
{
    Task<string[]> GetProvidersAsync(Metadata metadata, CancellationToken cancellationToken = default);
}