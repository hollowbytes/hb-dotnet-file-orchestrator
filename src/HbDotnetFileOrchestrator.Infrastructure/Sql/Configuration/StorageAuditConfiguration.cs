using System.Text.Json;
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

        builder.Property(x => x.Properties)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<Dictionary<string, object>>(v, (JsonSerializerOptions?)null)!)
            .HasColumnType("json");
        //.HasColumnType("json");

        // builder.OwnsOne(x => x.Properties, o =>
        // {
        //     o.ToJson();
        // });

    }
}