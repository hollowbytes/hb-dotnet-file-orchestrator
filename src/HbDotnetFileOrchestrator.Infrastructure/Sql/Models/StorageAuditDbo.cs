namespace HbDotnetFileOrchestrator.Infrastructure.Sql.Models;

public class StorageAuditDbo
{
    public int Id { get; set; } = -1;
    
    public Dictionary<string, object> Properties { get; set; } = new();
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    public byte[] RowVersion { get; set; } = [];
}