using MyInventory2026.src.modules.product.Domain.aggregate;
using MyInventory2026.src.modules.product.Domain.Repositories;
using MyInventory2026.src.modules.product.Domain.valueObject;

namespace MyInventory2026.src.modules.product.Application.UseCases;

public sealed class CreateProductUseCase
{
    private readonly IProductRepository _productRepository;

    public CreateProductUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> ExecuteAsync(
        string codeInv,
        string nameProduct,
        int stock,
        int stockMin,
        int stockMax,
        CancellationToken cancellationToken = default)
    {
        var normalizedCode = ProductCodeInv.Create(codeInv).Value;
        var existingByCode = await _productRepository.FindByCodeInvAsync(normalizedCode, cancellationToken);
        if (existingByCode is not null)
        {
            throw new InvalidOperationException($"Product with code '{normalizedCode}' already exists.");
        }

        var product = Product.CreateNew(codeInv, nameProduct, stock, stockMin, stockMax);
        await _productRepository.AddAsync(product, cancellationToken);
        return product;
    }
}
