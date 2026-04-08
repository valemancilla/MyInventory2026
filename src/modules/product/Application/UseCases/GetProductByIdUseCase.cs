using MyInventory2026.src.modules.product.Domain.aggregate;
using MyInventory2026.src.modules.product.Domain.Repositories;
using MyInventory2026.src.modules.product.Domain.valueObject;

namespace MyInventory2026.src.modules.product.Application.UseCases;

public sealed class GetProductByIdUseCase
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<Product?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        return _productRepository.FindByIdAsync(ProductId.Create(id), cancellationToken);
    }
}
