using HbDotnetFileOrchestrator.Domain.Interfaces;
using HbDotnetFileOrchestrator.Domain.Interfaces.Repositories;
using HbDotnetFileOrchestrator.Domain.Models;
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
            .Include(x => x.StorageRuleDbo)
            .Where(x => x.StorageRuleDbo.Name == rule.Name)
            .ToArrayAsync(cancellationToken);
    }
    
    public async Task<IFileDestination> GetDestinationByRuleAsync(string rule, string destinationName, CancellationToken cancellationToken = default)
    {
        return await context.Storages
            .Include(x => x.StorageRuleDbo)
            .SingleAsync(x =>
                x.Name == destinationName && x.StorageRuleDbo.Name == rule, 
                cancellationToken
            );
    }
}