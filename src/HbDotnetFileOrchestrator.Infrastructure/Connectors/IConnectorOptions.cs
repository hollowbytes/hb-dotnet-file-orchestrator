namespace HbDotnetFileOrchestrator.Infrastructure.Connectors;

public interface IConnectorOptions
{
    public string Id { get; set; }
    public string Rule { get; set; }

    public string Type { get; }
}