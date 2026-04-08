using Microsoft.EntityFrameworkCore;
using MyInventory2026.src.modules.provider.Domain.aggregate;
using MyInventory2026.src.modules.provider.Domain.Repositories;
using MyInventory2026.src.modules.provider.Domain.valueObject;
using MyInventory2026.src.modules.provider.Infrastructure.entity;
using MyInventory2026.src.shared.context;

namespace MyInventory2026.src.modules.provider.Infrastructure.repository;

public sealed class ProviderRepository : IProviderRepository
{
    private readonly AppDbContext _dbContext;

    public ProviderRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Provider provider, CancellationToken cancellationToken = default)
    {
        var entity = new ProviderEntity
        {
            Id = provider.Id.Value,
            Name = provider.Name.Value
        };

        await _dbContext.Providers.AddAsync(entity, cancellationToken);
    }

    public async Task<Provider?> FindByIdAsync(ProviderId id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Providers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);

        return entity is null ? null : Provider.Create(entity.Id, entity.Name);
    }

    public async Task<IReadOnlyCollection<Provider>> FindAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _dbContext.Providers
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);

        return entities.Select(x => Provider.Create(x.Id, x.Name)).ToList();
    }

    public async Task UpdateAsync(Provider provider, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Providers
            .FirstOrDefaultAsync(x => x.Id == provider.Id.Value, cancellationToken);

        if (entity is null)
        {
            throw new KeyNotFoundException($"Provider with id '{provider.Id.Value}' was not found.");
        }

        entity.Name = provider.Name.Value;
    }

    public async Task<bool> DeleteByIdAsync(ProviderId id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Providers
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);

        if (entity is null)
        {
            return false;
        }

        _dbContext.Providers.Remove(entity);
        return true;
    }
}