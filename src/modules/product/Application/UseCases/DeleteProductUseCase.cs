using MyInventory2026.src.modules.product.Domain.Repositories;
using MyInventory2026.src.modules.product.Domain.valueObject;

namespace MyInventory2026.src.modules.product.Application.UseCases;

public sealed class DeleteProductUseCase
{
    private readonly IProductRepository _productRepository;

    public DeleteProductUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var productId = ProductId.Create(id);
        var existing = await _productRepository.FindByIdAsync(productId, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        return await _productRepository.DeleteByIdAsync(productId, cancellationToken);
    }
}
