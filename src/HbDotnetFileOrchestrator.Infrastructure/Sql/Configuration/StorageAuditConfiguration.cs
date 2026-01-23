using HbDotnetFileOrchestrator.Infrastructure.Sql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HbDotnetFileOrchestrator.Infrastructure.Sql.Configuration;

public class StorageAuditConfiguration : IEntityTypeConfiguration<StorageAuditDbo>
{
    public void Configure(EntityTypeBuilder<StorageAuditDbo> builder)
    {
        builder.ToTable("StorageAudit");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.RowVersion).IsRowVersion();
        
        builder.OwnsOne(p => p.Properties, ownedNavigationBuilder =>
        {
            ownedNavigationBuilder.ToJson();
        });
    }
}