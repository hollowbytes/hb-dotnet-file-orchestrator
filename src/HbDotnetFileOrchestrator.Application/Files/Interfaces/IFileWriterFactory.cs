using HbDotnetFileOrchestrator.Domain.Interfaces;

namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IFileWriterFactory
{
    IFileWriterStrategy Create(IFileDestination fileDestination);
}