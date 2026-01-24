using HbDotnetFileOrchestrator.Domain.Interfaces;

namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IFileReaderFactory
{
    IFileReader Create(IFileDestination fileDestination);
}