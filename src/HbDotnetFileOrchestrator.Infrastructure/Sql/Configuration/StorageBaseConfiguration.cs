using HbDotnetFileOrchestrator.Infrastructure.Sql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HbDotnetFileOrchestrator.Infrastructure.Sql.Configuration;

public class StorageBaseConfiguration : IEntityTypeConfiguration<FileDestinationBase>
{
    public void Configure(EntityTypeBuilder<FileDestinationBase> builder)
    {
        builder.UseTpcMappingStrategy();
        
        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.RowVersion).IsRowVersion();

        builder.Ignore(x => x.Type);
    }
}