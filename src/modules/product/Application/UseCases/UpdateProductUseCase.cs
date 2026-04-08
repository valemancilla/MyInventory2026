using MyInventory2026.src.modules.product.Domain.aggregate;
using MyInventory2026.src.modules.product.Domain.Repositories;
using MyInventory2026.src.modules.product.Domain.valueObject;

namespace MyInventory2026.src.modules.product.Application.UseCases;

public sealed class UpdateProductUseCase
{
    private readonly IProductRepository _productRepository;

    public UpdateProductUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> ExecuteAsync(
        int id,
        string codeInv,
        string nameProduct,
        int stock,
        int stockMin,
        int stockMax,
        CancellationToken cancellationToken = default)
    {
        var productId = ProductId.Create(id);
        var existing = await _productRepository.FindByIdAsync(productId, cancellationToken);
        if (existing is null)
        {
            throw new KeyNotFoundException($"Product with id '{id}' was not found.");
        }

        var normalizedCode = ProductCodeInv.Create(codeInv).Value;
        var otherWithCode = await _productRepository.FindByCodeInvAsync(normalizedCode, cancellationToken);
        if (otherWithCode is not null && otherWithCode.Id != id)
        {
            throw new InvalidOperationException($"Another product already uses code '{normalizedCode}'.");
        }

        var updated = Product.Create(id, codeInv, nameProduct, stock, stockMin, stockMax);
        await _productRepository.UpdateAsync(updated, cancellationToken);
        return updated;
    }
}
