using MyInventory2026.src.modules.provider.Application.Interfaces;
using MyInventory2026.src.modules.provider.Application.Services;
using MyInventory2026.src.modules.provider.Infrastructure.repository;
using MyInventory2026.src.modules.provider.UI;
using MyInventory2026.src.shared.context;
using MyInventory2026.src.shared.helpers;
using MyInventory2026.src.shared.ui;

try
{
    var context = DbContextFactory.Create();
    var providerRepository = new ProviderRepository(context);
    var unitOfWork = new UnitOfWork(context);
    IProviderService providerService = new ProviderService(providerRepository, unitOfWork);
    var modules = new List<IModuleUI>
    {
        new ProviderConsoleUI(providerService)
    };

    if (context.Database.CanConnect())
    {
        Console.WriteLine("Conexión exitosa a la base de datos.");
        await RunMainMenuAsync(modules);
    }
    else
    {
        Console.WriteLine("No se pudo conectar con la base de datos.");
    }
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
    if (ex.InnerException != null)
    {
        Console.Error.WriteLine($"Detalle: {ex.InnerException.Message}");
    }
}

static async Task RunMainMenuAsync(IReadOnlyCollection<IModuleUI> modules)
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("=== MENU PRINCIPAL ===");
        foreach (var menuModule in modules.OrderBy(x => x.Key))
        {
            Console.WriteLine($"{menuModule.Key}. {menuModule.Title}");
        }
        Console.WriteLine("0. Salir");
        Console.Write("Selecciona una opción: ");

        var option = Console.ReadLine()?.Trim();
        Console.WriteLine();

        if (option == "0")
        {
            Console.WriteLine("Saliendo...");
            return;
        }

        var module = modules.FirstOrDefault(x => x.Key == option);
        if (module is null)
        {
            Console.WriteLine("Opción inválida.");
            continue;
        }

        await module.RunAsync();
    }
}