using MyInventory2026.src.modules.provider.Domain.aggregate;
using MyInventory2026.src.modules.provider.Domain.Repositories;
using MyInventory2026.src.modules.provider.Domain.valueObject;

namespace MyInventory2026.src.modules.provider.Application.UseCases;

public sealed class CreateProviderUseCase
{
    private readonly IProviderRepository _providerRepository;

    public CreateProviderUseCase(IProviderRepository providerRepository)
    {
        _providerRepository = providerRepository;
    }

    public async Task<Provider> ExecuteAsync(string id, string name, CancellationToken cancellationToken = default)
    {
        var providerId = ProviderId.Create(id);
        var existingProvider = await _providerRepository.FindByIdAsync(providerId, cancellationToken);

        if (existingProvider is not null)
        {
            throw new InvalidOperationException($"Provider with id '{providerId}' already exists.");
        }

        var provider = Provider.Create(id, name);
        await _providerRepository.AddAsync(provider, cancellationToken);
        return provider;
    }
}