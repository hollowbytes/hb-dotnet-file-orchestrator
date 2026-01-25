using HbDotnetFileOrchestrator.Domain.Interfaces;

namespace HbDotnetFileOrchestrator.Infrastructure.Sql.Models;

public abstract class StorageBaseDbo : IFileDirectory
{
    public int Id { get; set; }
    
    public int RuleId { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string Expression  { get; set; } = string.Empty;
    
    public byte[] RowVersion { get; set; } = [];
    
    public virtual StorageRuleDbo StorageRuleDbo { get; set; } = new();

    public abstract string Type { get; }
}