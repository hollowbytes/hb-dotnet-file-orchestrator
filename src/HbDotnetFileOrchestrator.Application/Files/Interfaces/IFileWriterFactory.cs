using HbDotnetFileOrchestrator.Domain.Interfaces;

namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IFileWriterFactory
{
    IFileWriter Create(IStorageOptions options);
}