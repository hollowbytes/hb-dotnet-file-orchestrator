using HbDotnetFileOrchestrator.Domain.Interfaces;

namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IFileReaderFactory
{
    IFileReaderStrategy Create(IFileDestination fileDestination);
}