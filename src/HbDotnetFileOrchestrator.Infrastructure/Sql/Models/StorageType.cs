namespace HbDotnetFileOrchestrator.Infrastructure.Sql.Models;

public class StorageType
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;

    public byte[] RowVersion { get; set; } = [];

    public ICollection<StorageRule> StorageRules { get; set; } = [];
}