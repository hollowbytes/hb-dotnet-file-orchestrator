namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IConnectorProvider
{
    IConnectorStrategy CreateConnector(IConnectorOptions options);
}