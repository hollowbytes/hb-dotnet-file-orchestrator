using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Domain.Interfaces.Services;

public interface IAuditRepository
{
    public Task AuditAsync(Audit audit, CancellationToken cancellationToken = default);
    
    public Task<Audit> GetAuditByConversationIdAsync(Guid conversationId, CancellationToken cancellationToken);
}