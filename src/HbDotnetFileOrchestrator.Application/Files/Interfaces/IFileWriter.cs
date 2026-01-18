using CSharpFunctionalExtensions;
using HbDotnetFileOrchestrator.Domain.Interfaces;
using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IFileWriter
{
    Task<Result> SaveAsync(ReceivedFile file, string location, CancellationToken cancellationToken = default);
}

public interface IFileWriter<in TOptions> : IFileWriter where TOptions : class, IFileDestination;