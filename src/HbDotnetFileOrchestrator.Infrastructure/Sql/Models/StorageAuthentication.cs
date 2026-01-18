namespace HbDotnetFileOrchestrator.Infrastructure.Sql.Models;

public class StorageAuthentication
{
    public int StorageTypeId { get; set; }
    
    public int? StorageAuthId { get; set; }
    
    public byte[] RowVersion { get; set; }

    public StorageType StorageType { get; set; } = new();
}