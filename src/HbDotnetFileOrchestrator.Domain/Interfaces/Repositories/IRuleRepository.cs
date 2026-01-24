using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Domain.Interfaces.Repositories;

public interface IRuleRepository
{
    public Task<Rule[]> GetAllAsync(CancellationToken cancellationToken = default);
    
    public Task<Rule> FindAsync(string name, CancellationToken cancellationToken = default);
}