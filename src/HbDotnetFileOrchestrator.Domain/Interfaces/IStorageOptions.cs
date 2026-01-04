namespace HbDotnetFileOrchestrator.Domain.Interfaces;

public interface IStorageOptions
{
    public string Id { get; set; }
    public string Rule { get; set; }
    public string Destination { get; set; }

    public string Type { get; }
}