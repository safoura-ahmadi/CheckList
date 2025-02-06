using CheckList.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckList.Infrastructure.EfCore.Configuration;

public class MyTaskConfiguration : IEntityTypeConfiguration<MyTask>
{
    public void Configure(EntityTypeBuilder<MyTask> builder)
    {
        builder.Property(t => t.Title)
             .IsRequired()
             .HasMaxLength(50)
             .HasColumnType("nvarchar");
    }
}
