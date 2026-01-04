using CSharpFunctionalExtensions;

namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IFilesService
{
    public Task<Result> SaveFileAsync(CancellationToken cancellationToken = default);
}