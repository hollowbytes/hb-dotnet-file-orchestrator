using HbDotnetFileOrchestrator.Domain.Interfaces.Services;
using HbDotnetFileOrchestrator.Domain.Models;
using Microsoft.Extensions.Logging;
using RulesEngine.Models;
using Rule = HbDotnetFileOrchestrator.Domain.Models.Rule;

namespace HbDotnetFileOrchestrator.Infrastructure.Storage;

public class RuleEvaluator
(
    ILogger<RuleEvaluator> logger
) : IRuleEvaluator
{
    public async Task<Result<Rule>[]> RunAsync(Rule[] rules, Metadata metadata, CancellationToken cancellationToken = default)
    {
        var workflow = ToWorkflow(rules);

        var re = new RulesEngine.RulesEngine([workflow]);
        var result = await re.ExecuteAllRulesAsync(workflow.WorkflowName, new RuleParameter("metadata", metadata));

        return result
            .Select(x =>
            {
                var rule = new Rule(x.Rule.RuleName, x.Rule.Expression);
                return x.IsSuccess ? rule : Result.Failure(rule, x.Rule.ErrorMessage);
            })
            .ToArray();
    }

    private static Workflow ToWorkflow(Rule[] rules) => new()
    {
        WorkflowName = "default",
        Rules = rules.Select(ToValueRule).ToArray()
    };

    private static RulesEngine.Models.Rule ToValueRule(Rule option) => new()
    {
        RuleName = option.Name,
        Expression = option.Expression,
        ErrorMessage = $"Error - Failed to evaluate {option.Name}: '{option.Expression}'"
    };
}