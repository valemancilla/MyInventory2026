using MyInventory2026.src.modules.provider.Application.Interfaces;
using MyInventory2026.src.shared.ui;

namespace MyInventory2026.src.modules.provider.UI;

public sealed class ProviderConsoleUI : IModuleUI
{
    private readonly IProviderService _providerService;

    public ProviderConsoleUI(IProviderService providerService)
    {
        _providerService = providerService;
    }

    public string Key => "1";
    public string Title => "Provider";

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== CRUD PROVIDER ===");
            Console.WriteLine("1. Crear provider");
            Console.WriteLine("2. Listar providers");
            Console.WriteLine("3. Buscar provider por id");
            Console.WriteLine("4. Actualizar provider");
            Console.WriteLine("5. Eliminar provider");
            Console.WriteLine("0. Volver al menu principal");
            Console.Write("Selecciona una opción: ");

            var option = Console.ReadLine()?.Trim();
            Console.WriteLine();

            try
            {
                switch (option)
                {
                    case "1":
                        await CreateProviderAsync(cancellationToken);
                        break;
                    case "2":
                        await ListProvidersAsync(cancellationToken);
                        break;
                    case "3":
                        await GetProviderByIdAsync(cancellationToken);
                        break;
                    case "4":
                        await UpdateProviderAsync(cancellationToken);
                        break;
                    case "5":
                        await DeleteProviderAsync(cancellationToken);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private async Task CreateProviderAsync(CancellationToken cancellationToken)
    {
        Console.Write("Id: ");
        var id = Console.ReadLine() ?? string.Empty;
        Console.Write("Name: ");
        var name = Console.ReadLine() ?? string.Empty;

        var created = await _providerService.CreateAsync(id, name, cancellationToken);
        Console.WriteLine($"Provider creado. Id: {created.Id.Value}, Name: {created.Name.Value}");
    }

    private async Task ListProvidersAsync(CancellationToken cancellationToken)
    {
        var providers = await _providerService.GetAllAsync(cancellationToken);

        if (providers.Count == 0)
        {
            Console.WriteLine("No hay providers registrados.");
            return;
        }

        foreach (var provider in providers)
        {
            Console.WriteLine($"- Id: {provider.Id.Value} | Name: {provider.Name.Value}");
        }
    }

    private async Task GetProviderByIdAsync(CancellationToken cancellationToken)
    {
        Console.Write("Id: ");
        var id = Console.ReadLine() ?? string.Empty;

        var provider = await _providerService.GetByIdAsync(id, cancellationToken);

        if (provider is null)
        {
            Console.WriteLine("Provider no encontrado.");
            return;
        }

        Console.WriteLine($"Id: {provider.Id.Value}");
        Console.WriteLine($"Name: {provider.Name.Value}");
    }

    private async Task UpdateProviderAsync(CancellationToken cancellationToken)
    {
        Console.Write("Id del provider a actualizar: ");
        var id = Console.ReadLine() ?? string.Empty;
        Console.Write("Nuevo name: ");
        var name = Console.ReadLine() ?? string.Empty;

        var updated = await _providerService.UpdateAsync(id, name, cancellationToken);
        Console.WriteLine($"Provider actualizado. Id: {updated.Id.Value}, Name: {updated.Name.Value}");
    }

    private async Task DeleteProviderAsync(CancellationToken cancellationToken)
    {
        Console.Write("Id del provider a eliminar: ");
        var id = Console.ReadLine() ?? string.Empty;

        var deleted = await _providerService.DeleteAsync(id, cancellationToken);
        Console.WriteLine(deleted ? "Provider eliminado." : "Provider no encontrado.");
    }
}