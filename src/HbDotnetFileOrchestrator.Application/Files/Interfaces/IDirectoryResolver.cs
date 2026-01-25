using CSharpFunctionalExtensions;
using HbDotnetFileOrchestrator.Domain.Interfaces;
using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IDirectoryResolver
{
    Task<Result<string>> ResolveAsync(Metadata metadata, IFileDirectory options,
        CancellationToken cancellationToken = default);
}