using System.Reflection;
using HbDotnetFileOrchestrator.Infrastructure.Sql.Models;
using Microsoft.EntityFrameworkCore;

namespace HbDotnetFileOrchestrator.Infrastructure.Sql;

public class FileOrchestratorDbContext : DbContext
{
    public const string ConnectionStringKey = "FileOrchestratorDb";

    public virtual DbSet<StorageBase> Storages { get; set; }
    
    public virtual DbSet<StorageRule> StorageRules { get; set; }
    
    public FileOrchestratorDbContext(DbContextOptions<FileOrchestratorDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}