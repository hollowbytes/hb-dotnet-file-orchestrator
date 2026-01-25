using CSharpFunctionalExtensions;
using HbDotnetFileOrchestrator.Domain.Interfaces;
using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IFileWriterStrategy
{
    Task<Result> SaveAsync(ReceivedFile file, string location, CancellationToken cancellationToken = default);
}

public interface IFileWriterStrategy<in TOptions> : IFileWriterStrategy where TOptions : class, IFileDestination;