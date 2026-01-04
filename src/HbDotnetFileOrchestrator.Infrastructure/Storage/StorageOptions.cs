using HbDotnetFileOrchestrator.Domain.Interfaces;
using HbDotnetFileOrchestrator.Infrastructure.Storage.Blob;
using HbDotnetFileOrchestrator.Infrastructure.Storage.FileSystem;
using HbDotnetFileOrchestrator.Infrastructure.Storage.Sftp;

namespace HbDotnetFileOrchestrator.Infrastructure.Storage;

public class StorageOptions
{
    public const string SECTION = "Storage";

    public BlobStorageOptions[] BlobStorage { get; set; } = [];

    public SftpStorageOptions[] Sftp { get; set; } = [];

    public FileSystemStorageOptions[] FileSystem { get; set; } = [];

    public IStorageOptions[] All => typeof(StorageOptions).GetProperties()
        .Where(prop => prop.PropertyType.IsArray)
        .Where(prop => !prop.PropertyType.GetElementType()!.IsInterface)
        .Where(prop => typeof(IStorageOptions[]).IsAssignableFrom(prop.PropertyType))
        .SelectMany(prop => prop.GetValue(this) as IStorageOptions[] ?? [])
        .ToArray();
}