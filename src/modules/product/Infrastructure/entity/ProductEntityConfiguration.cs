using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyInventory2026.src.modules.product.Infrastructure.entity;

public sealed class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.ToTable("products");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.CodeInv)
            .HasColumnName("codeInv")
            .HasMaxLength(10)
            .IsRequired();

        builder.HasIndex(x => x.CodeInv)
            .IsUnique();

        builder.Property(x => x.NameProduct)
            .HasColumnName("nameProduct")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Stock)
            .HasColumnName("stock")
            .IsRequired();

        builder.Property(x => x.StockMin)
            .HasColumnName("stock_min")
            .IsRequired();

        builder.Property(x => x.StockMax)
            .HasColumnName("stck_max")
            .IsRequired();
    }
}

