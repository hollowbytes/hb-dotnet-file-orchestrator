using HbDotnetFileOrchestrator.Infrastructure.Sql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HbDotnetFileOrchestrator.Infrastructure.Sql.Configuration;

public class StorageRuleConfiguration : IEntityTypeConfiguration<StorageRule>
{
    public void Configure(EntityTypeBuilder<StorageRule> builder)
    {
        builder.ToTable("StorageRule");
        
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.RowVersion).IsRowVersion();
        
        builder.HasMany(x => x.Storages)
            .WithOne(x => x.StorageRule)
            .HasForeignKey(x => x.RuleId);
    }
}