namespace HbDotnetFileOrchestrator.Infrastructure.Authentication.BlobStorage;

public class BlobStorageAuthenticationOptions : IAuthenticationOptions
{
    public string ConnectionString { get; set; } = string.Empty;

    public string Type => "BlobStorage";
}