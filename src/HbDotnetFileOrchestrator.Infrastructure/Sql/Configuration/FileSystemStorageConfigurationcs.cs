using HbDotnetFileOrchestrator.Infrastructure.Sql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HbDotnetFileOrchestrator.Infrastructure.Sql.Configuration;

public class FileSystemStorageConfiguration : IEntityTypeConfiguration<FileSystemStorageDbo>
{
    public void Configure(EntityTypeBuilder<FileSystemStorageDbo> builder)
    {
        builder.ToTable("FileSystemStorage");
    }
}