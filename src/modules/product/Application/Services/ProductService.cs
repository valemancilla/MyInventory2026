using MyInventory2026.src.modules.product.Application.Interfaces;
using MyInventory2026.src.modules.product.Domain.aggregate;
using MyInventory2026.src.modules.product.Domain.Repositories;
using MyInventory2026.src.modules.product.Domain.valueObject;
using MyInventory2026.src.shared.contracts;

namespace MyInventory2026.src.modules.product.Application.Services;

public sealed class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Product> CreateAsync(
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
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var persisted = await _productRepository.FindByCodeInvAsync(normalizedCode, cancellationToken);
        return persisted
               ?? throw new InvalidOperationException("Product was not persisted correctly.");
    }

    public Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _productRepository.FindByIdAsync(ProductId.Create(id), cancellationToken);
    }

    public Task<IReadOnlyCollection<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _productRepository.FindAllAsync(cancellationToken);
    }

    public async Task<Product> UpdateAsync(
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
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return updated;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var productId = ProductId.Create(id);
        var wasDeleted = await _productRepository.DeleteByIdAsync(productId, cancellationToken);
        if (!wasDeleted)
        {
            return false;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
