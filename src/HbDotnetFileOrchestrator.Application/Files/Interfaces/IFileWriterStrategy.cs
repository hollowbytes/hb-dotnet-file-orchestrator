using HbDotnetFileOrchestrator.Application.Files.Models.Commands;
using HbDotnetFileOrchestrator.Domain.Interfaces;
using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IFileWriterStrategy
{
    Task<Result> SaveAsync(FileWriterCommand command, CancellationToken cancellationToken = default);
}

public interface IFileWriterStrategy<in TOptions> : IFileWriterStrategy where TOptions : class, IFileDirectory;