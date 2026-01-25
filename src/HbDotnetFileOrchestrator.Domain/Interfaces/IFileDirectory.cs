namespace HbDotnetFileOrchestrator.Domain.Interfaces;

public interface IFileDirectory
{
    public string Name { get; set; }
    
    public string Expression { get; set; }
    
    public string Type { get; }
}