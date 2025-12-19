namespace HbDotnetFileOrchestrator.Infrastructure.Connectors;

public interface IConnectorOptions
{
    public string Id { get; set; }

    public string Type { get; }
}