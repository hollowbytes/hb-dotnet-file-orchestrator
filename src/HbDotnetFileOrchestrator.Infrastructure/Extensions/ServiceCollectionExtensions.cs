using System.Diagnostics.CodeAnalysis;
using System.IO.Abstractions;
using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Infrastructure.Authentication;
using HbDotnetFileOrchestrator.Infrastructure.Http;
using HbDotnetFileOrchestrator.Infrastructure.Sql;
using HbDotnetFileOrchestrator.Infrastructure.Storage;
using HbDotnetFileOrchestrator.Infrastructure.Storage.FileSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HbDotnetFileOrchestrator.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
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

        services.AddDbContext<FileOrchestratorDbContext>((provider, builder) =>
        {
            var config = provider.GetRequiredService<IConfiguration>();
            var connectionString = config.GetConnectionString(FileOrchestratorDbContext.ConnectionStringKey);
            builder.UseSqlServer(connectionString);
        });

        services.AddSingleton<IFileSystem>(new FileSystem());
        
        services.AddScoped<IMetadataProvider, MetadataProvider>();
        services.AddScoped<IRuleEvaluator, StorageRuleEvaluator>();
        services.AddScoped<IFileWriterFactory, StorageFactory>();
        services.AddScoped<IFileLocationResolver, StorageLocationResolver>();

        services.AddScoped<IFileWriter<FileSystemStorageOptions>, FileSystemFileWriter>();
    }
}