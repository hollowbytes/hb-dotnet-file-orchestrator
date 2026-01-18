using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace HbDotnetFileOrchestrator.Infrastructure.Sql;

public class FileOrchestratorDbContext : DbContext
{
    public const string ConnectionStringKey = "FileOrchestratorDbContext";

    public FileOrchestratorDbContext(DbContextOptions<FileOrchestratorDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}