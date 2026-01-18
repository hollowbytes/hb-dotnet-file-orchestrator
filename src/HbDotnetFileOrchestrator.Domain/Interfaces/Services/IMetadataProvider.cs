using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Domain.Interfaces.Services;

public interface IMetadataProvider
{
    public Task<Metadata> GetMetadataAsync(CancellationToken cancellationToken = default);
}