using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Domain.Interfaces;
using HbDotnetFileOrchestrator.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RulesEngine.Models;

namespace HbDotnetFileOrchestrator.Infrastructure.Storage;

public class StorageRuleEvaluator(
    ILogger<StorageRuleEvaluator> logger,
    IOptions<StorageOptions> connectorOptions
) : IRuleEvaluator
{
    public async Task<IStorageOptions[]> RunAsync(Metadata metadata, CancellationToken cancellationToken = default)
    {
        var workflow = ToWorkflow(connectorOptions.Value);

        var re = new RulesEngine.RulesEngine([workflow]);
        var result = await re.ExecuteAllRulesAsync(workflow.WorkflowName, new RuleParameter("metadata", metadata));

        return result.Where(x => x.IsSuccess)
            .Select(x => x.Rule as ValueRule)
            .Select(x => x!.Options)
            .ToArray();
    }

    private static Workflow ToWorkflow(StorageOptions options) => new()
    {
        WorkflowName = "default",
        Rules = options.All.Select(ToValueRule).ToArray()
    };

    private static ValueRule ToValueRule(IStorageOptions option) => new()
    {
        Options = option,
        RuleName = option.Id,
        Expression = option.Rule,
        ErrorMessage = $"Error - Failed to evaluate ({option.Type}) {option.Id}: '{option.Rule}'"
    };

    private class ValueRule() : Rule
    {
        public required IStorageOptions Options { get; init; }
    }
}