using MyInventory2026.src.modules.provider.Domain.aggregate;

namespace MyInventory2026.src.modules.provider.Application.Interfaces;

public interface IProviderService
{
    Task<Provider> CreateAsync(string id, string name, CancellationToken cancellationToken = default);
    Task<Provider?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Provider>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Provider> UpdateAsync(string id, string name, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default);
}