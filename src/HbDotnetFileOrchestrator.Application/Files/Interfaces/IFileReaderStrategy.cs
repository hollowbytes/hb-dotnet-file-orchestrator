using CSharpFunctionalExtensions;
using HbDotnetFileOrchestrator.Domain.Interfaces;
using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IFileReaderStrategy
{
    Task<Result> ReadFileAsync(string location, CancellationToken cancellationToken = default);
}

public interface IFileReaderStrategy<in TOptions> : IFileReaderStrategy where TOptions : class, IFileDestination;