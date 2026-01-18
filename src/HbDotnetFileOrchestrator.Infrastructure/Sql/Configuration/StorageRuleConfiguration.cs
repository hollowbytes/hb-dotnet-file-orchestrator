using HbDotnetFileOrchestrator.Infrastructure.Sql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HbDotnetFileOrchestrator.Infrastructure.Sql.Configuration;

public class StorageRuleConfiguration : IEntityTypeConfiguration<StorageRule>
{
    public void Configure(EntityTypeBuilder<StorageRule> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.RowVersion).IsRowVersion();
        
        builder.HasOne(x => x.StorageType)
            .WithMany(x => x.StorageRules)
            .HasForeignKey(x => x.StorageTypeId);
    }
}