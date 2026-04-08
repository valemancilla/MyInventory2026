# MyInventory2026

## Descripción del proyecto

**MyInventory2026** es una aplicación de consola en **C# (.NET)** que gestiona inventario mediante una base de datos **MySQL**. El sistema permite administrar **proveedores** y **productos** con un modelo relacional que incluye una tabla intermedia **muchos a muchos** entre proveedores y productos. La arquitectura está organizada por **módulos** (Domain, Application, Infrastructure, UI) y un núcleo **shared** para contexto de base de datos, contratos y utilidades.

## Características destacadas

- **Arquitectura por capas y módulos**: separación entre dominio, casos de uso, servicios de aplicación, persistencia y consola.
- **CRUD de proveedores** y **CRUD de productos** desde menús de consola, con **cinco casos de uso** por entidad (crear, listar, obtener por id, actualizar, eliminar).
- **Entity Framework Core** con **Pomelo** para MySQL y **migraciones** para versionar el esquema.
- **Relación muchos a muchos** modelada con la tabla intermedia `provider_products` (clave compuesta y claves foráneas).
- **Cadena de conexión** en `appsettings.json` y soporte opcional de variable de entorno `MYSQL_CONNECTION`.
- **Factory de diseño** (`IDesignTimeDbContextFactory`) para ejecutar `dotnet ef` sin depender de la ejecución manual de la consola.

## Objetivo

Centralizar la gestión de **proveedores**, **productos** y sus **asociaciones** en un único proyecto mantenible, con un esquema de base de datos alineado al modelo de negocio y herramientas estándar de .NET para persistencia y evolución del esquema.

## Tecnologías utilizadas

| Tecnología | Uso |
|------------|-----|
| **C#** | Lenguaje de implementación. |
| **.NET 10** (`net10.0`) | Plataforma de ejecución (consola). |
| **Entity Framework Core 9** | ORM y migraciones. |
| **Pomelo.EntityFrameworkCore.MySql** | Proveedor MySQL para EF Core. |
| **Microsoft.Extensions.Configuration** | Lectura de `appsettings.json` y variables de entorno. |
| **MySQL** | Motor de base de datos (según cadena de conexión). |

## Estructura del sistema

```
MyInventory2026/
├── Program.cs                    # Punto de entrada, composición manual y menú principal
├── appsettings.json              # Cadena de conexión y configuración
├── MyInventory2026.csproj        # Proyecto y paquetes NuGet
├── MyInventory2026.sln           # Solución
├── Migrations/                   # Migraciones EF Core y snapshot del modelo
└── src/
    ├── modules/
    │   ├── provider/             # Módulo proveedor
    │   │   ├── Application/      # Interfaces, servicios, casos de uso
    │   │   ├── Domain/           # Agregado, value objects, repositorio (contrato)
    │   │   ├── Infrastructure/   # Entidades EF, repositorio concreto
    │   │   └── UI/               # Menú consola del módulo
    │   └── product/              # Módulo producto (misma división)
    └── shared/
        ├── context/              # DbContext, entidad puente, Unit of Work, factory diseño
        ├── contracts/            # Contratos transversales (p. ej. IUnitOfWork)
        ├── helpers/              # Fábrica de DbContext en tiempo de ejecución, resolución MySQL
        └── ui/                   # Contrato IModuleUI para menús modulares
```

## Qué hace cada archivo

### Raíz del proyecto

| Archivo | Qué hace |
|---------|----------|
| `Program.cs` | Crea `AppDbContext`, repositorios, `UnitOfWork`, servicios y módulos de UI; verifica conexión a BD y ejecuta el menú principal. |
| `appsettings.json` | Define `ConnectionStrings:MySqlDB` para MySQL. |
| `MyInventory2026.csproj` | Define el SDK, `net10.0`, paquetes EF Core y copia de `appsettings.json` al directorio de salida. |
| `MyInventory2026.sln` | Agrupa el proyecto en la solución. |

### `Migrations/`

| Archivo | Qué hace |
|---------|----------|
| `*_NombreMigracion*.cs` / `*_miinventory2026*.cs` / `*_ErdProvidersProducts*.cs` | Historial de migraciones aplicadas al esquema (tablas, columnas, índices). |
| `AppDbContextModelSnapshot.cs` | Snapshot del modelo EF para generar migraciones incrementales. |

### Módulo `provider`

| Archivo | Qué hace |
|---------|----------|
| `Domain/aggregate/Provider.cs` | Agregado de dominio del proveedor. |
| `Domain/valueObject/ProviderId.cs` | Identificador del proveedor (validación). |
| `Domain/valueObject/ProviderName.cs` | Nombre del proveedor (validación). |
| `Domain/Repositories/IProviderRepository.cs` | Contrato de persistencia del proveedor. |
| `Application/Interfaces/IProviderService.cs` | Contrato del servicio de aplicación de proveedor. |
| `Application/Services/ProviderService.cs` | Orquesta repositorio y `IUnitOfWork` para operaciones CRUD. |
| `Application/UseCases/*ProviderUseCase.cs` | Casos de uso: crear, listar, obtener por id, actualizar, eliminar. |
| `Infrastructure/entity/ProviderEntity.cs` | Entidad EF mapeada a la tabla `providers`. |
| `Infrastructure/entity/ProviderEntityConfiguration.cs` | Configuración Fluent API de `ProviderEntity`. |
| `Infrastructure/repository/ProviderRepository.cs` | Implementación de `IProviderRepository` con EF Core. |
| `UI/ProviderConsoleUI.cs` | Menú de consola del CRUD de proveedores (`IModuleUI`). |

### Módulo `product`

| Archivo | Qué hace |
|---------|----------|
| `Domain/aggregate/Product.cs` | Agregado de dominio del producto. |
| `Domain/valueObject/ProductId.cs` | Identificador numérico del producto. |
| `Domain/valueObject/ProductCodeInv.cs` | Código de inventario (validación y longitud). |
| `Domain/valueObject/ProductName.cs` | Nombre del producto (validación). |
| `Domain/Repositories/IProductRepository.cs` | Contrato de persistencia del producto. |
| `Application/Interfaces/IProductService.cs` | Contrato del servicio de aplicación de producto. |
| `Application/Services/ProductService.cs` | Orquesta repositorio y `IUnitOfWork`; tras crear, recarga el producto para devolver el id generado. |
| `Application/UseCases/*ProductUseCase.cs` | Casos de uso: crear, listar, obtener por id, actualizar, eliminar. |
| `Infrastructure/entity/ProductEntity.cs` | Entidad EF mapeada a la tabla `products`. |
| `Infrastructure/entity/ProductEntityConfiguration.cs` | Configuración Fluent API de `ProductEntity` (incluye índice único en `codeInv`). |
| `Infrastructure/repository/ProductRepository.cs` | Implementación de `IProductRepository` con EF Core. |
| `UI/ProductConsoleUI.cs` | Menú de consola del CRUD de productos (`IModuleUI`). |

### `shared`

| Archivo | Qué hace |
|---------|----------|
| `context/AppDbcontext.cs` | `DbContext` principal: `DbSet` de proveedores y productos; aplica configuraciones por ensamblado. |
| `context/AppDbContextDesignTimeFactory.cs` | Crea el `DbContext` para herramientas `dotnet ef` (migraciones). |
| `context/ProviderProductEntity.cs` | Entidad de la tabla intermedia `provider_products` (muchos a muchos). |
| `context/ProviderProductEntityConfiguration.cs` | Configuración de la tabla puente, clave compuesta y relaciones. |
| `context/UnitOWork.cs` | Implementación de `IUnitOfWork` delegando en `SaveChangesAsync` del `DbContext`. |
| `contracts/IUnitOfWork.cs` | Contrato para persistir cambios en una unidad de trabajo. |
| `helpers/DbContextFactory.cs` | Construye `AppDbContext` en tiempo de ejecución leyendo configuración y `MYSQL_CONNECTION`. |
| `helpers/MySqlVersionResolver.cs` | Ayuda a determinar la versión del servidor MySQL para configurar el proveedor. |
| `ui/IModuleUI.cs` | Define `Key`, `Title` y `RunAsync` para cada módulo de menú. |

### Carpetas generadas al compilar (referencia)

| Ubicación | Qué hace |
|-----------|----------|
| `bin/`, `obj/` | Salida de compilación y artefactos intermedios; no suelen editarse a mano. |

## Autor

**Valentina Mancilla**

*(Nombre obtenido de la configuración local de Git del repositorio; si no corresponde, actualiza esta sección.)*
