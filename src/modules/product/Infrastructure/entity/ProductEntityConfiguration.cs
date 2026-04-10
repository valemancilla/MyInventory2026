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
            .HasColumnType("varchar(10)")
            .IsRequired();

        builder.HasIndex(x => x.CodeInv)
            .IsUnique()
            .HasDatabaseName("IX_product_CodeInv");

        builder.Property(x => x.NameProduct)
            .HasColumnName("nameProduct")
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(x => x.Stock)
            .HasColumnName("stock")
            .HasColumnType("int")
            .IsRequired();
            builder.ToTable(t => t.HasCheckConstraint("CK_Product_Stock", "Stock >= 0"));

        builder.Property(x => x.StockMin)
            .HasColumnName("stock_min")
            .HasColumnType("int")
            .IsRequired();
            builder.ToTable(t => t.HasCheckConstraint("CK_Product_Stock", "Stock >= 0"));

        builder.Property(x => x.StockMax)
            .HasColumnName("stck_max")
            .HasColumnType("int")
            .IsRequired();
            builder.ToTable(t => t.HasCheckConstraint("CK_Product_Stock", "Stock >= 0"));
    }
}

