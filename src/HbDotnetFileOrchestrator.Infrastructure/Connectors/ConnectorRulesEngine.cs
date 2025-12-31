using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RulesEngine.Models;

namespace HbDotnetFileOrchestrator.Infrastructure.Connectors;

public class ConnectorRulesEngine(
    ILogger<ConnectorRulesEngine> logger,
    IOptions<ConnectorOptions> connectorOptions
) : IRulesEngine
{
    public async Task<IConnectorOptions[]> RunAsync(Metadata metadata, CancellationToken cancellationToken = default)
    {
        var workflow = ToWorkflow(connectorOptions.Value);

        var re = new RulesEngine.RulesEngine([workflow]);
        var result = await re.ExecuteAllRulesAsync(workflow.WorkflowName, new RuleParameter("metadata", metadata));

        return result.Where(x => x.IsSuccess)
            .Select(x => x.Rule as ValueRule)
            .Select(x => x!.Options)
            .ToArray();
    }

    private static Workflow ToWorkflow(ConnectorOptions options)
    {
        return new Workflow
        {
            WorkflowName = "default",
            Rules = options.All.Select(x => new ValueRule
            {
                Options = x,
                RuleName = x.Id,
                Expression = x.Rule,
                ErrorMessage = $"Failed to evaluate ({x.Type}) {x.Id}: '{x.Rule}'"
            }).ToArray()
        };
    }

    private class ValueRule : Rule
    {
        public required IConnectorOptions Options { get; init; }
    }
}