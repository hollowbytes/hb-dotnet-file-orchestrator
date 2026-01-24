using CSharpFunctionalExtensions;
using HbDotnetFileOrchestrator.Domain.Interfaces;
using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IFileReader
{
    Task<Result> ReadFileAsync(string location, CancellationToken cancellationToken = default);
}

public interface IFileReader<in TOptions> : IFileReader where TOptions : class, IFileDestination;