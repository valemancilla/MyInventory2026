using MyInventory2026.src.modules.provider.Domain.aggregate;
using MyInventory2026.src.modules.provider.Domain.Repositories;
using MyInventory2026.src.modules.provider.Domain.valueObject;

namespace MyInventory2026.src.modules.provider.Application.UseCases;

public sealed class GetProviderByIdUseCase
{
    private readonly IProviderRepository _providerRepository;

    public GetProviderByIdUseCase(IProviderRepository providerRepository)
    {
        _providerRepository = providerRepository;
    }

    public Task<Provider?> ExecuteAsync(string id, CancellationToken cancellationToken = default)
    {
        return _providerRepository.FindByIdAsync(ProviderId.Create(id), cancellationToken);
    }
}