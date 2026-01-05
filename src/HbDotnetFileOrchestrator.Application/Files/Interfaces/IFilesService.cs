using CSharpFunctionalExtensions;
using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IFilesService
{
    public Task<Result> SaveFileAsync(ReceivedFile receivedFile, CancellationToken cancellationToken = default);
}