using MyInventory2026.src.shared.context;

namespace MyInventory2026.src.modules.provider.Infrastructure.entity;

public class ProviderEntity
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public ICollection<ProviderProductEntity> ProviderProducts { get; set; } = new List<ProviderProductEntity>();
}