namespace HbDotnetFileOrchestrator.Infrastructure.Sql.Models;

public class StorageAuditDbo
{
    public int Id { get; set; }
    
    public virtual Dictionary<string, object> Properties { get; set; } = null!;
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    public byte[] RowVersion { get; set; } = [];
}