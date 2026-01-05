using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IMetadataProvider
{
    public Task<Metadata> GetMetadataAsync(CancellationToken cancellationToken = default);
}