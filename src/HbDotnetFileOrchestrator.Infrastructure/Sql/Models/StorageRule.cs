namespace HbDotnetFileOrchestrator.Infrastructure.Sql.Models;

public class StorageRule
{
    public int Id { get; set; }
    
    public int StorageTypeId { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string Rule { get; set; } = string.Empty;

    public string Destination { get; set; } = string.Empty;

    public byte[] RowVersion { get; set; } = [];
    
    public StorageType StorageType { get; set; } = new();
}