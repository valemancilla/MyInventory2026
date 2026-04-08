using MyInventory2026.src.shared.context;

namespace MyInventory2026.src.modules.product.Infrastructure.entity;

public sealed class ProductEntity
{
    public int Id { get; set; }
    public string CodeInv { get; set; } = string.Empty;
    public string NameProduct { get; set; } = string.Empty;
    public int Stock { get; set; }
    public int StockMin { get; set; }
    public int StockMax { get; set; }

    public ICollection<ProviderProductEntity> ProviderProducts { get; set; } = new List<ProviderProductEntity>();
}

