using HbDotnetFileOrchestrator.Domain.Interfaces;
using HbDotnetFileOrchestrator.Domain.Interfaces.Repositories;
using HbDotnetFileOrchestrator.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HbDotnetFileOrchestrator.Infrastructure.Sql;

public class StorageRepository
(
    StorageDbContext context
) : IFileDirectoryRepository
{
    public async Task<IFileDirectory[]> GetDestinationsByRuleAsync(Rule rule, CancellationToken cancellationToken = default)
    {
        return await context.Storages
            .AsNoTracking()
            .Include(x => x.StorageRuleDbo)
            .Where(x => x.StorageRuleDbo.Name == rule.Name)
            .ToArrayAsync(cancellationToken);
    }
    
    public async Task<IFileDirectory> GetDestinationByRuleAsync(string rule, string destinationName, CancellationToken cancellationToken = default)
    {
        return await context.Storages
            .AsNoTracking()
            .Include(x => x.StorageRuleDbo)
            .SingleAsync(x =>
                x.Name == destinationName && x.StorageRuleDbo.Name == rule, 
                cancellationToken
            );
    }
}