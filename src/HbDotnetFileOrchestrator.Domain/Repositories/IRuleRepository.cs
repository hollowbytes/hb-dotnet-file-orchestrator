using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Domain.Repositories;

public interface IRuleRepository
{
    public Task<Rule[]> GetAllAsync(CancellationToken cancellationToken = default);
}