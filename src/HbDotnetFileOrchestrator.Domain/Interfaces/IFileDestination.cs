namespace HbDotnetFileOrchestrator.Domain.Interfaces;

public interface IFileDestination
{
    public string Name { get; set; }
    
    public string Destination { get; set; }
    
    public string Type { get; }
}