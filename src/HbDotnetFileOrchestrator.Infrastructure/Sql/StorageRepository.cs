using HbDotnetFileOrchestrator.Domain.Interfaces;
using HbDotnetFileOrchestrator.Domain.Models;
using HbDotnetFileOrchestrator.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HbDotnetFileOrchestrator.Infrastructure.Sql;

public class StorageRepository
(
    StorageDbContext context
) : IIFileDestinationRepository
{
    public async Task<IFileDestination[]> GetDestinationsByRuleAsync(Rule rule, CancellationToken cancellationToken = default)
    {
        return await context.Storages
            .Include(x => x.StorageRule)
            .Where(x => x.StorageRule.Name == rule.Name)
            .ToArrayAsync(cancellationToken);
    }
}