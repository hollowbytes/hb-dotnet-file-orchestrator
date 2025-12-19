namespace HbDotnetFileOrchestrator.Infrastructure.Authentication.Sftp;

public class SftpAuthenticationOptions : IAuthenticationOptions
{
    public string ConnectionString { get; set; } = string.Empty;

    public string Type => "Sftp";
}