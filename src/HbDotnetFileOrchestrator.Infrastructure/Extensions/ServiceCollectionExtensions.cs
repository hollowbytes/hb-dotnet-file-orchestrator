using System.IO.Abstractions;
using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Infrastructure.Authentication;
using HbDotnetFileOrchestrator.Infrastructure.Http;
using HbDotnetFileOrchestrator.Infrastructure.Storage;
using HbDotnetFileOrchestrator.Infrastructure.Storage.FileSystem;
using Microsoft.Extensions.DependencyInjection;

namespace HbDotnetFileOrchestrator.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddOptions<AuthenticationOptions>()
            .BindConfiguration(AuthenticationOptions.SECTION)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<StorageOptions>()
            .BindConfiguration(StorageOptions.SECTION)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IFileSystem>(new FileSystem());
        
        services.AddScoped<IMetadataProvider, MetadataProvider>();
        services.AddScoped<IRuleEvaluator, StorageRuleEvaluator>();
        services.AddScoped<IFileWriterFactory, StorageFactory>();
        services.AddScoped<IFileLocationResolver, StorageLocationResolver>();

        services.AddScoped<IFileWriter<FileSystemStorageOptions>, FileSystemFileWriter>();
    }
}