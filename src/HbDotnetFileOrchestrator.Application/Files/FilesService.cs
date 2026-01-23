using System.Runtime.CompilerServices;
using CSharpFunctionalExtensions;
using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Application.Files.Models;
using HbDotnetFileOrchestrator.Domain.Interfaces;
using HbDotnetFileOrchestrator.Domain.Interfaces.Repositories;
using HbDotnetFileOrchestrator.Domain.Interfaces.Services;
using HbDotnetFileOrchestrator.Domain.Models;
using Microsoft.Extensions.Logging;

namespace HbDotnetFileOrchestrator.Application.Files;

public class FilesService(
    ILogger<FilesService> logger,
    IMetadataProvider metadataProvider,
    IRuleRepository ruleRepository,
    IRuleEvaluator ruleEvaluator,
    IIFileDestinationRepository fileDestinationRepository,
    IFileLocationResolver fileLocationResolver,
    IFileWriterFactory fileWriterFactory,
    IAuditRepository auditRepository
) : IFilesService
{
    public async Task<SavedFileResult[]> SaveFileAsync(ReceivedFile receivedFile, CancellationToken cancellationToken = default)
    {
        var metadata = await metadataProvider.GetMetadataAsync(cancellationToken);
        
        var rules = await ruleRepository.GetAllAsync(cancellationToken);
        var evaluatedRules = await ruleEvaluator.RunAsync(rules, metadata, cancellationToken);
        
        var results = new List<SavedFileResult>();
        var audit = new Audit()
            .AddProperty("ConversationId", receivedFile.ConversationId)
            .AddProperty("FileName", receivedFile.Name)
            .AddProperty("FileSize", receivedFile.Size);
        
        logger.LogInformation("Running {Count} rules...", rules.Length);
        
        foreach (var rule in evaluatedRules)
        {
            var ruleAudit = audit.CreateScope()
                .AddProperty("Rule", rule.Name);
            
            var destinations = await fileDestinationRepository.GetDestinationsByRuleAsync(rule, cancellationToken);

            logger.LogInformation("Storing in {destinations} locations...", destinations.Length);

            foreach (var destination in destinations)
            {
                var destinationAudit = await ruleAudit.CreateScope()
                    .AddProperty("DestinationName", destination.Name)
                    .AddProperty("DestinationType", destination.Type)
                    .AddProperty("Destination", destination.Destination)
                    .AuditActionAsync(async saveAudit =>
                    {
                        var response = await SaveFileAsync(receivedFile, metadata, rule, destination, cancellationToken);
                        saveAudit.AddProperty("Error", response.Error ?? string.Empty);
                        
                        results.Add(response);
                    });
                
                await auditRepository.AuditAsync(destinationAudit, cancellationToken);
            }
        }

        return results.ToArray();
    }

    private async Task<SavedFileResult> SaveFileAsync(ReceivedFile receivedFile, Metadata metadata, Rule rule, IFileDestination destination, CancellationToken cancellationToken = default)
    {
        var response = new SavedFileResult(rule.Name, destination.Name, destination.Type, receivedFile.Name);  
                
        var fileLocationResult = await fileLocationResolver.ResolveAsync(metadata, destination, cancellationToken);

        if (fileLocationResult.IsFailure)
        {
            return response with { Error = fileLocationResult.Error };
        }

        var fileWriter = fileWriterFactory.Create(destination);
        var saveResult = await fileWriter.SaveAsync(receivedFile, fileLocationResult.Value, cancellationToken);

        if (saveResult.IsFailure)
        {
            return response with { Error = saveResult.Error };
        }
        return response with { FileLocation = fileLocationResult.Value };
    }
}