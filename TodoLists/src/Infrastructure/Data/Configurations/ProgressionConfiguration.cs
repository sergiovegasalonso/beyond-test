using TodoLists.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoLists.Infrastructure.Data.Configurations;

public class ProgressionConfiguration : IEntityTypeConfiguration<Progression>
{
    public void Configure(EntityTypeBuilder<Progression> builder)
    {
        builder.Property(t => t.TodoItemId)
            .IsRequired();

        builder.Property(t => t.Date)
            .IsRequired();

        builder.Property(t => t.Percent)
            .HasPrecision(3, 2)
            .IsRequired();
    }
}
