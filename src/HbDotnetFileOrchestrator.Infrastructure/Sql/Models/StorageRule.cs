namespace HbDotnetFileOrchestrator.Infrastructure.Sql.Models;

public class StorageRule
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string Expression { get; set; } = string.Empty;
    
    public byte[] RowVersion { get; set; } = [];
    
    public virtual ICollection<FileDestinationBase> Storages { get; set; } = [];
}