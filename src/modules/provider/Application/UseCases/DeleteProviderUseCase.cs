using MyInventory2026.src.modules.provider.Domain.Repositories;
using MyInventory2026.src.modules.provider.Domain.valueObject;

namespace MyInventory2026.src.modules.provider.Application.UseCases;

public sealed class DeleteProviderUseCase
{
    private readonly IProviderRepository _providerRepository;

    public DeleteProviderUseCase(IProviderRepository providerRepository)
    {
        _providerRepository = providerRepository;
    }

    public async Task<bool> ExecuteAsync(string id, CancellationToken cancellationToken = default)
    {
        var providerId = ProviderId.Create(id);
        var existingProvider = await _providerRepository.FindByIdAsync(providerId, cancellationToken);

        if (existingProvider is null)
        {
            return false;
        }

        return await _providerRepository.DeleteByIdAsync(providerId, cancellationToken);
    }
}