using HbDotnetFileOrchestrator.Application.Files.Models;
using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IFileWriterService
{
    public Task<SavedFileResult[]> SaveFileAsync(ReceivedFile receivedFile, CancellationToken cancellationToken = default);
}