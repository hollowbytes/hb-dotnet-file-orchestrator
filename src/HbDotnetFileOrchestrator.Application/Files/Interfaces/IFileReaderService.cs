using HbDotnetFileOrchestrator.Application.Files.Models;
using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IFileReaderService
{
    public Task ReadFileAsync(Guid conversationId, CancellationToken cancellationToken = default);
}