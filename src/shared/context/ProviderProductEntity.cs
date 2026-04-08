using MyInventory2026.src.modules.product.Infrastructure.entity;
using MyInventory2026.src.modules.provider.Infrastructure.entity;

namespace MyInventory2026.src.shared.context;

public sealed class ProviderProductEntity
{
    public string ProviderId { get; set; } = string.Empty;
    public int ProductId { get; set; }

    public ProviderEntity Provider { get; set; } = null!;
    public ProductEntity Product { get; set; } = null!;
}

