using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyInventory2026.src.modules.provider.Infrastructure.entity;

public sealed class ProviderEntityConfiguration : IEntityTypeConfiguration<ProviderEntity>
{
    public void Configure(EntityTypeBuilder<ProviderEntity> builder)
    {
        builder.ToTable("providers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("nameProvider")
            .HasMaxLength(50)
            .IsRequired();
    }
}