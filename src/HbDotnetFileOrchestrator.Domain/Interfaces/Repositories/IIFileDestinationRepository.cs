using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Domain.Interfaces.Repositories;

public interface IIFileDestinationRepository
{
    public Task<IFileDestination[]> GetDestinationsByRuleAsync(Rule rule, CancellationToken cancellationToken = default);
}