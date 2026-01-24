using System.Text.Json;
using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Application.Files.Models;
using HbDotnetFileOrchestrator.Domain.Interfaces;
using HbDotnetFileOrchestrator.Domain.Interfaces.Repositories;
using HbDotnetFileOrchestrator.Domain.Interfaces.Services;
using HbDotnetFileOrchestrator.Domain.Models;
using Microsoft.Extensions.Logging;

namespace HbDotnetFileOrchestrator.Application.Files;

public class FileReaderService(
    ILogger<FileReaderService> logger,
    IRuleRepository ruleRepository,
    IIFileDestinationRepository fileDestinationRepository,
    IFileLocationResolver fileLocationResolver,
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
        
        var metadata = JsonSerializer.Deserialize<Metadata>(auditMetadata);

        var rule =  await ruleRepository.FindAsync(ruleName, cancellationToken);
        var destinations = await fileDestinationRepository
            .GetDestinationsByRuleAsync(rule, cancellationToken);

        var fileDestination = destinations.Single(x => x.Name == destinationName);
        var fileReader = fileReaderFactory.Create(fileDestination);
        
        var fileLocationResult = await fileLocationResolver.ResolveAsync(metadata, fileDestination, cancellationToken);

        var path = Path.Combine(fileLocationResult.Value, metadata.Files[0].FileName);
        await fileReader.ReadFileAsync(path, cancellationToken);
    }
}