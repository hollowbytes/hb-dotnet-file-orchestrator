using HbDotnetFileOrchestrator.Domain.Interfaces;
using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IFileLocationResolver
{
    ValueTask<string> ResolveAsync(Metadata metadata, IStorageOptions options,
        CancellationToken cancellationToken = default);
}