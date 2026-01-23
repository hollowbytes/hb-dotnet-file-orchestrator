using System.Reflection;
using HbDotnetFileOrchestrator.Infrastructure.Sql.Models;
using Microsoft.EntityFrameworkCore;

namespace HbDotnetFileOrchestrator.Infrastructure.Sql;

public class StorageDbContext(DbContextOptions<StorageDbContext> options) : DbContext(options)
{
    public const string ConnectionStringKey = "FileOrchestratorDb";

    public virtual DbSet<StorageBaseDbo> Storages { get; set; }
    
    public virtual DbSet<StorageRuleDbo> StorageRules { get; set; }

    public virtual DbSet<StorageAuditDbo> StorageAudits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}