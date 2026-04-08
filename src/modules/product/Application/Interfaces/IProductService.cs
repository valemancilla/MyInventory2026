using MyInventory2026.src.modules.product.Domain.aggregate;

namespace MyInventory2026.src.modules.product.Application.Interfaces;

public interface IProductService
{
    Task<Product> CreateAsync(
        string codeInv,
        string nameProduct,
        int stock,
        int stockMin,
        int stockMax,
        CancellationToken cancellationToken = default);

    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Product>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Product> UpdateAsync(
        int id,
        string codeInv,
        string nameProduct,
        int stock,
        int stockMin,
        int stockMax,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
