using HbDotnetFileOrchestrator.Domain.Models;
using HbDotnetFileOrchestrator.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HbDotnetFileOrchestrator.Infrastructure.Sql;

public class StorageRuleRepository
(
    StorageDbContext context
) : IRuleRepository
{
    public Task<Rule[]> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return context.StorageRules
            .Select(x => new Rule
            (
                Name: x.Name, 
                Expression: x.Expression
            ))
            .ToArrayAsync(cancellationToken);
    }
}