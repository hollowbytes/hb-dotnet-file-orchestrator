using System.Text.Json;
using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Application.Files.Models;
using HbDotnetFileOrchestrator.Application.Files.Models.Commands;
using HbDotnetFileOrchestrator.Domain.Interfaces;
using HbDotnetFileOrchestrator.Domain.Interfaces.Repositories;
using HbDotnetFileOrchestrator.Domain.Interfaces.Services;
using HbDotnetFileOrchestrator.Domain.Models;
using Microsoft.Extensions.Logging;

namespace HbDotnetFileOrchestrator.Application.Files;

public class FileWriterService(
    ILogger<FileWriterService> logger,
    IMetadataProvider metadataProvider,
    IRuleRepository ruleRepository,
    IRuleEvaluator ruleEvaluator,
    IFileDirectoryRepository fileDirectoryRepository,
    IDirectoryResolver directoryResolver,
    IFileWriterFactory fileWriterFactory,
    IAuditRepository auditRepository
) : IFileWriterService
{
    public async Task<SavedFileResult[]> SaveFileAsync(ReceivedFile receivedFile, CancellationToken cancellationToken = default)
    {
        var metadata = await metadataProvider.GetMetadataAsync(cancellationToken);
        var rules = await ruleRepository.GetAllAsync(cancellationToken);
        var ruleResults = await ruleEvaluator.RunAsync(rules, metadata, cancellationToken);
        
        var results = new List<SavedFileResult>();
        
        foreach (var result in ruleResults)
        {
            if (result.IsFailure)
            {
                continue;
            }
            
            var rule = result.Value;
            var destinations = await fileDirectoryRepository.GetDestinationsByRuleAsync(rule, cancellationToken);
            
            logger.LogInformation("Running '{Rule}' with '{Count}' destinations", rule.Name, destinations.Length);
            
            foreach (var destination in destinations)
            {
                var audit = await new Audit()
                    .AddProperty("ConversationId", receivedFile.ConversationId.ToString())
                    .AddProperty("FileName", receivedFile.Name)
                    .AddProperty("FileSize", receivedFile.Size.ToString())
                    .AddProperty("Metadata", metadata)
                    .AddProperty("Rule", rule.Name)
                    .AddProperty("DestinationName", destination.Name)
                    .AddProperty("DestinationType", destination.Type)
                    .AddProperty("Destination", destination.Expression)
                    .AuditActionAsync(async saveAudit =>
                    {
                        var response = await SaveFileAsync(receivedFile, metadata, rule, destination, cancellationToken);
                        saveAudit.AddProperty("Error", response.Error ?? string.Empty);
                        saveAudit.AddProperty("FileLocation", response.FileLocation ?? string.Empty);
                        
                        results.Add(response);
                    });
                
                await auditRepository.AuditAsync(audit, cancellationToken);
            }
        }
        return results.ToArray();
    }

    private async Task<SavedFileResult> SaveFileAsync(ReceivedFile receivedFile, Metadata metadata, Rule rule, IFileDirectory directory, CancellationToken cancellationToken = default)
    {
        var response = new SavedFileResult(rule.Name, directory.Name, directory.Type, receivedFile.Name);  
                
        var directoryResult = await directoryResolver
            .ResolveAsync(metadata, directory, cancellationToken);

        if (directoryResult.IsFailure)
        {
            return response with { Error = directoryResult.Error };
        }
        
        var fileWriter = fileWriterFactory.Create(directory);

        var saveFileCommand = new FileWriterCommand(receivedFile, directoryResult.Value);
        var saveResult = await fileWriter.SaveAsync(saveFileCommand, cancellationToken);

        if (saveResult.IsFailure)
        {
            return response with { FileLocation = directoryResult.Value, Error = saveResult.Error };
        }

        return response with { FileLocation = directoryResult.Value };
    }
}