using HbDotnetFileOrchestrator.Domain.Interfaces.Services;
using HbDotnetFileOrchestrator.Domain.Models;
using HbDotnetFileOrchestrator.Infrastructure.Sql.Models;

namespace HbDotnetFileOrchestrator.Infrastructure.Sql;

public class StorageAuditRepository
(
    StorageDbContext context
) : IAuditRepository
{
    public Task AuditAsync(Audit audit, CancellationToken cancellationToken)
    {
        var value = new StorageAuditDbo
        {
            Properties = audit.GetProperties()
        };
        
        context.StorageAudits.Add(value);
        return context.SaveChangesAsync(cancellationToken);
    }
}