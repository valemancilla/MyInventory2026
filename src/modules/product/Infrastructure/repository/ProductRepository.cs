using Microsoft.EntityFrameworkCore;
using MyInventory2026.src.modules.product.Domain.aggregate;
using MyInventory2026.src.modules.product.Domain.Repositories;
using MyInventory2026.src.modules.product.Domain.valueObject;
using MyInventory2026.src.modules.product.Infrastructure.entity;
using MyInventory2026.src.shared.context;

namespace MyInventory2026.src.modules.product.Infrastructure.repository;

public sealed class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        var entity = new ProductEntity
        {
            CodeInv = product.CodeInv.Value,
            NameProduct = product.NameProduct.Value,
            Stock = product.Stock,
            StockMin = product.StockMin,
            StockMax = product.StockMax
        };

        await _dbContext.Products.AddAsync(entity, cancellationToken);
    }

    public async Task<Product?> FindByIdAsync(ProductId id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);

        return entity is null ? null : MapToDomain(entity);
    }

    public async Task<Product?> FindByCodeInvAsync(string codeInv, CancellationToken cancellationToken = default)
    {
        var code = ProductCodeInv.Create(codeInv).Value;
        var entity = await _dbContext.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.CodeInv == code, cancellationToken);

        return entity is null ? null : MapToDomain(entity);
    }

    public async Task<IReadOnlyCollection<Product>> FindAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _dbContext.Products
            .AsNoTracking()
            .OrderBy(x => x.NameProduct)
            .ToListAsync(cancellationToken);

        return entities.Select(MapToDomain).ToList();
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == product.Id, cancellationToken);

        if (entity is null)
        {
            throw new KeyNotFoundException($"Product with id '{product.Id}' was not found.");
        }

        entity.CodeInv = product.CodeInv.Value;
        entity.NameProduct = product.NameProduct.Value;
        entity.Stock = product.Stock;
        entity.StockMin = product.StockMin;
        entity.StockMax = product.StockMax;
    }

    public async Task<bool> DeleteByIdAsync(ProductId id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);

        if (entity is null)
        {
            return false;
        }

        _dbContext.Products.Remove(entity);
        return true;
    }

    private static Product MapToDomain(ProductEntity entity)
    {
        return Product.Create(
            entity.Id,
            entity.CodeInv,
            entity.NameProduct,
            entity.Stock,
            entity.StockMin,
            entity.StockMax);
    }
}
