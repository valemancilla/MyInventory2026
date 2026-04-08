using MyInventory2026.src.modules.product.Domain.aggregate;
using MyInventory2026.src.modules.product.Domain.Repositories;

namespace MyInventory2026.src.modules.product.Application.UseCases;

public sealed class GetAllProductsUseCase
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<IReadOnlyCollection<Product>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return _productRepository.FindAllAsync(cancellationToken);
    }
}
