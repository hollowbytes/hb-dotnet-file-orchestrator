using System.Text.Json;
using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Domain.Interfaces.Repositories;
using HbDotnetFileOrchestrator.Domain.Interfaces.Services;
using HbDotnetFileOrchestrator.Domain.Models;
using Microsoft.Extensions.Logging;

namespace HbDotnetFileOrchestrator.Application.Files;

public class FileReaderService(
    ILogger<FileReaderService> logger,
    IFileDirectoryRepository fileDirectoryRepository,
    IDirectoryResolver directoryResolver,
    IFileReaderFactory fileReaderFactory,
    IAuditRepository auditRepository
) : IFileReaderService
{
    public async Task ReadFileAsync(Guid conversationId, CancellationToken cancellationToken = default)
    {
        var audit = await auditRepository.GetAuditByConversationIdAsync(conversationId, cancellationToken);

        var ruleName = audit["Rule"].ToString() ?? string.Empty;
        var destinationName = audit["DestinationName"].ToString() ?? string.Empty;
        var auditMetadata = audit["Metadata"].ToString() ?? string.Empty;
        
        var metadata = JsonSerializer.Deserialize<Metadata>(auditMetadata)!;

        var destination = await fileDirectoryRepository.GetDestinationByRuleAsync(ruleName, destinationName, cancellationToken);
        var fileReader = fileReaderFactory.Create(destination);
        
        var fileLocationResult = await directoryResolver.ResolveAsync(metadata, destination, cancellationToken);

        var path = Path.Combine(fileLocationResult.Value, metadata.Files[0].FileName);
        await fileReader.ReadFileAsync(path, cancellationToken);
    }
}