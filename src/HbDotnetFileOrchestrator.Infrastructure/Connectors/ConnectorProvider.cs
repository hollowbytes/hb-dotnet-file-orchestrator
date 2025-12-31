using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RulesEngine.Models;

namespace HbDotnetFileOrchestrator.Infrastructure.Connectors;

public class ConnectorProvider(
    ILogger<ConnectorProvider> logger,
    IOptions<ConnectorOptions> connectorOptions
) : IConnectorProvider
{
    public async Task<string[]> GetProvidersAsync(Metadata metadata, CancellationToken cancellationToken = default)
    {
        var all = connectorOptions.Value.All;

        var rules = all.Select(x => new Rule
        {
            RuleName = x.Id,
            Expression = x.Rule,
            ErrorMessage = $"Failed to evaluate ({x.Type}) {x.Id}: '{x.Rule}'"
        }).ToArray();

        var workflow = new Workflow
        {
            WorkflowName = "default",
            Rules = rules
        };

        var re = new RulesEngine.RulesEngine([workflow]);
        var ruleParameter = new RuleParameter("metadata", metadata);
        var result = await re.ExecuteAllRulesAsync(workflow.WorkflowName, ruleParameter);

        var names = result.Where(x => x.IsSuccess).Select(x => x.Rule.RuleName).ToArray();
        return names;
    }
}