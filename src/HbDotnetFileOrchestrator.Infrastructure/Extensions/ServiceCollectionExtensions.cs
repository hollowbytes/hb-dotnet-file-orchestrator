using System.Diagnostics.CodeAnalysis;
using System.IO.Abstractions;
using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Domain.Interfaces.Repositories;
using HbDotnetFileOrchestrator.Domain.Interfaces.Services;
using HbDotnetFileOrchestrator.Infrastructure.Http;
using HbDotnetFileOrchestrator.Infrastructure.Sql;
using HbDotnetFileOrchestrator.Infrastructure.Sql.Models;
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
        services.AddDbContext<StorageDbContext>((provider, builder) =>
        {
            var config = provider.GetRequiredService<IConfiguration>();
            var connectionString = config.GetConnectionString(StorageDbContext.ConnectionStringKey);
            builder.UseSqlServer(connectionString);
        });

        services.AddSingleton<IFileSystem>(new FileSystem());
        
        services.AddScoped<IMetadataProvider, MetadataProvider>();
        services.AddScoped<IRuleEvaluator, StorageRuleEvaluator>();
        services.AddScoped<IFileWriterFactory, StorageFactory>();
        services.AddScoped<IFileLocationResolver, StorageLocationResolver>();
        
        services.AddScoped<IRuleRepository, StorageRuleRepository>();
        services.AddScoped<IIFileDestinationRepository, StorageRepository>();

        services.AddScoped<IFileWriter<FileSystemStorage>, FileSystemFileWriter>();
    }
}