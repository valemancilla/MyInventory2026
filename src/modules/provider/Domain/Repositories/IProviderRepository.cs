using MyInventory2026.src.modules.provider.Domain.aggregate;
using MyInventory2026.src.modules.provider.Domain.valueObject;

namespace MyInventory2026.src.modules.provider.Domain.Repositories;

public interface IProviderRepository
{
    Task AddAsync(Provider provider, CancellationToken cancellationToken = default);
    Task<Provider?> FindByIdAsync(ProviderId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Provider>> FindAllAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(Provider provider, CancellationToken cancellationToken = default);
    Task<bool> DeleteByIdAsync(ProviderId id, CancellationToken cancellationToken = default);
}