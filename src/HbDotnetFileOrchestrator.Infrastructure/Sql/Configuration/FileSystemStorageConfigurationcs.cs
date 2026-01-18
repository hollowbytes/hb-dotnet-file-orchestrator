using HbDotnetFileOrchestrator.Infrastructure.Sql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HbDotnetFileOrchestrator.Infrastructure.Sql.Configuration;

public class FileSystemStorageConfiguration : IEntityTypeConfiguration<FileSystemStorage>
{
    public void Configure(EntityTypeBuilder<FileSystemStorage> builder)
    {
        builder.ToTable("FileSystemStorage");
    }
}