namespace HbDotnetFileOrchestrator.Infrastructure.Connectors.Sftp;

public class SftpConnectorOptions : IConnectorOptions
{
    public string Id { get; set; } = string.Empty;

    public string Type => "Sftp";
}