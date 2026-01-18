using System.Reflection;
using HbDotnetFileOrchestrator.Infrastructure.Sql.Models;
using Microsoft.EntityFrameworkCore;

namespace HbDotnetFileOrchestrator.Infrastructure.Sql;

public class StorageDbContext : DbContext
{
    public const string ConnectionStringKey = "FileOrchestratorDb";

    public virtual DbSet<FileDestinationBase> Storages { get; set; }
    
    public virtual DbSet<StorageRule> StorageRules { get; set; }
    
    public StorageDbContext(DbContextOptions<StorageDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}