using HbDotnetFileOrchestrator.Infrastructure.Sql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HbDotnetFileOrchestrator.Infrastructure.Sql.Configuration;

public class StorageTypeConfiguration : IEntityTypeConfiguration<StorageType>
{
    public void Configure(EntityTypeBuilder<StorageType> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.RowVersion).IsRowVersion();
        //
        // builder.HasMany(x => x.StorageRules)
        //     .WithOne(x => x.StorageType)
        //     .HasForeignKey(x => x.StorageTypeId);
    }
}