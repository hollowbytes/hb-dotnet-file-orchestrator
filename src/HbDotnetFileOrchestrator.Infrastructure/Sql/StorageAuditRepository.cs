using HbDotnetFileOrchestrator.Domain.Interfaces.Services;
using HbDotnetFileOrchestrator.Domain.Models;
using HbDotnetFileOrchestrator.Infrastructure.Sql.Models;
using Microsoft.EntityFrameworkCore;

namespace HbDotnetFileOrchestrator.Infrastructure.Sql;

public class StorageAuditRepository(
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

    public async Task<Audit> GetAuditByConversationIdAsync(Guid conversationId, CancellationToken cancellationToken)
    {
        var value = await context.StorageAudits
            .FromSqlInterpolated($"""
                 SELECT * FROM dbo.StorageAudit 
                 WHERE JSON_VALUE(Properties, '$.ConversationId') = {conversationId.ToString()}
            """)
            .FirstAsync(cancellationToken);
        
        return new Audit(value.Properties);
    }
}