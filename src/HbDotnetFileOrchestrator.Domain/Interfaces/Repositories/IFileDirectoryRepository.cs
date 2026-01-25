using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Domain.Interfaces.Repositories;

public interface IFileDirectoryRepository
{
    public Task<IFileDirectory[]> GetDestinationsByRuleAsync(Rule rule, CancellationToken cancellationToken = default);

    public Task<IFileDirectory> GetDestinationByRuleAsync(string rule, string destinationName,
        CancellationToken cancellationToken = default);
}