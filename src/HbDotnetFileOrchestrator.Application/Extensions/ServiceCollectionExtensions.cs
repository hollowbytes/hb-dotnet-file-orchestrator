using HbDotnetFileOrchestrator.Application.Files;
using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HbDotnetFileOrchestrator.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IFilesService, FilesService>();
    }
}