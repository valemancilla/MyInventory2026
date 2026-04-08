# MyInventory2026

Documentación alineada con el **estado real del repositorio en disco** (código fuente, migraciones, solución, salida de compilación y metadatos de proyecto). La sección **Estructura del sistema** incluye el inventario **completo** de rutas relevantes para evaluación académica.

---

## Descripción del proyecto

**MyInventory2026** es una **aplicación de consola** escrita en **C#** que gestiona un inventario persistido en **MySQL**. El dominio incluye **proveedores**, **productos** y una relación **muchos a muchos** entre ambos mediante la tabla intermedia **`provider_products`**.

La solución sigue una organización **modular** (`provider`, `product`) con capas **Domain**, **Application** (interfaces, servicios y casos de uso), **Infrastructure** (entidades EF Core y repositorios) y **UI** (menús de consola). El núcleo compartido (`shared`) concentra el **DbContext**, la **unidad de trabajo**, la entidad puente, helpers de conexión y el contrato de menús modulares.

---

## Características destacadas

- **Menú principal modular** (`IModuleUI`): módulos **Provider** (tecla `1`) y **Product** (tecla `2`).
- **CRUD completo** por módulo vía **servicios de aplicación** (`IProviderService`, `IProductService`) y **cinco casos de uso** por entidad (crear, listar todos, obtener por id, actualizar, eliminar).
- **Entity Framework Core 9** con proveedor **Pomelo** para MySQL; **migraciones** versionan tablas `providers`, `products` y `provider_products`.
- **Relación N:N explícita**: entidad `ProviderProductEntity` con clave compuesta y navegaciones en `ProviderEntity` y `ProductEntity`.
- **Configuración**: `appsettings.json` (`ConnectionStrings:MySqlDB`) y variable de entorno opcional **`MYSQL_CONNECTION`** (prioridad en `DbContextFactory`).
- **Detección de versión de MySQL** al crear el contexto en tiempo de ejecución (`MySqlVersionResolver`).
- **Soporte para herramientas EF** en diseño: `AppDbContextDesignTimeFactory` para `dotnet ef migrations`.

---

## Objetivo

Ofrecer una base de código **clara y extensible** para administrar inventario (proveedores, productos y vínculos) sobre MySQL, usando patrones habituales en .NET (repositorios, unidad de trabajo, casos de uso) y permitiendo **evolucionar el esquema** con migraciones.

---

## Tecnologías utilizadas

| Tecnología | Uso en el proyecto |
|------------|---------------------|
| **C#** | Lenguaje principal. |
| **.NET 10** (`net10.0`) | Runtime y SDK de la aplicación consola. |
| **Entity Framework Core 9** | ORM, `DbContext`, configuraciones Fluent API, migraciones. |
| **Microsoft.EntityFrameworkCore.Design** | Herramientas de línea de comandos (`dotnet ef`). |
| **Pomelo.EntityFrameworkCore.MySql** | Proveedor MySQL para EF Core. |
| **MySqlConnector** (transitivo vía Pomelo) | Conexión y lectura de versión del servidor en `MySqlVersionResolver`. |
| **Microsoft.Extensions.Configuration** (+ JSON y variables de entorno) | Carga de `appsettings.json` y `MYSQL_CONNECTION`. |
| **MySQL** | Motor de base de datos (según cadena de conexión). |

---

## Estructura del sistema

### Cómo leer esta sección

- **`src/`**: árbol **completo** — cada carpeta y cada archivo **.cs** existente.
- **`Migrations/`** y **raíz**: listado exhaustivo de archivos del proyecto.
- **`MyInventory2026.csproj`**: relación de entradas `<Folder Include="..."/>` (carpetas mostradas en el IDE).
- **`bin/`** y **`obj/`**: salida de **compilación** (Debug). No se versionan como código fuente, pero se documentan **tal como genera MSBuild** tras `dotnet build` para que la estructura del entregable quede explícita.
- **`.git/`**: repositorio Git; se indica su presencia; el **contenido interno** (objetos, refs, hooks) no se lista línea a línea por tamaño y porque no es código del sistema.

---

### 1. Código fuente — `src/` (árbol completo)

```
src/
├── modules/
│   ├── product/
│   │   ├── Application/
│   │   │   ├── Interfaces/
│   │   │   │   └── IProductService.cs
│   │   │   ├── Services/
│   │   │   │   └── ProductService.cs
│   │   │   └── UseCases/
│   │   │       ├── CreateProductUseCase.cs
│   │   │       ├── DeleteProductUseCase.cs
│   │   │       ├── GetAllProductsUseCase.cs
│   │   │       ├── GetProductByIdUseCase.cs
│   │   │       └── UpdateProductUseCase.cs
│   │   ├── Domain/
│   │   │   ├── aggregate/
│   │   │   │   └── Product.cs
│   │   │   ├── Repositories/
│   │   │   │   └── IProductRepository.cs
│   │   │   └── valueObject/
│   │   │       ├── ProductCodeInv.cs
│   │   │       ├── ProductId.cs
│   │   │       └── ProductName.cs
│   │   ├── Infrastructure/
│   │   │   ├── entity/
│   │   │   │   ├── ProductEntity.cs
│   │   │   │   └── ProductEntityConfiguration.cs
│   │   │   └── repository/
│   │   │       └── ProductRepository.cs
│   │   └── UI/
│   │       └── ProductConsoleUI.cs
│   └── provider/
│       ├── Application/
│       │   ├── Interfaces/
│       │   │   └── IProviderService.cs
│       │   ├── Services/
│       │   │   └── ProviderService.cs
│       │   └── UseCases/
│       │       ├── CreateProviderUseCase.cs
│       │       ├── DeleteProviderUseCase.cs
│       │       ├── GetAllProvidersUseCase.cs
│       │       ├── GetProviderByIdUseCase.cs
│       │       └── UpdateProviderUseCase.cs
│       ├── Domain/
│       │   ├── aggregate/
│       │   │   └── Provider.cs
│       │   ├── Repositories/
│       │   │   └── IProviderRepository.cs
│       │   └── valueObject/
│       │       ├── ProviderId.cs
│       │       └── ProviderName.cs
│       ├── Infrastructure/
│       │   ├── entity/
│       │   │   ├── ProviderEntity.cs
│       │   │   └── ProviderEntityConfiguration.cs
│       │   └── repository/
│       │       └── ProviderRepository.cs
│       └── UI/
│           └── ProviderConsoleUI.cs
└── shared/
    ├── configurations/                 ← carpeta vacía (sin archivos)
    ├── context/
    │   ├── AppDbcontext.cs
    │   ├── AppDbContextDesignTimeFactory.cs
    │   ├── ProviderProductEntity.cs
    │   ├── ProviderProductEntityConfiguration.cs
    │   └── UnitOWork.cs
    ├── contracts/
    │   └── IUnitOfWork.cs
    ├── helpers/
    │   ├── DbContextFactory.cs
    │   └── MySqlVersionResolver.cs
    └── ui/
        └── IModuleUI.cs
```

**Total archivos fuente C# bajo `src/`:** 40 archivos **.cs** (incluye `shared/context/UnitOWork.cs`).

**Nota:** En `MyInventory2026.csproj` existen etiquetas `<Folder Include="..."/>` para guiar el Explorador de soluciones; **no todas** las carpetas físicas aparecen como `<Folder>` (por ejemplo, `provider/Domain/valueObject` y `product/Domain/valueObject` no tienen línea `<Folder>` dedicada). **Todos los `.cs` bajo `src/` se compilan** porque el SDK incluye comodín implícito (`**/*.cs`).

---

### 2. Migraciones EF Core — `Migrations/` (archivos completos)

```
Migrations/
├── 20260408133505_miinventory2026.cs
├── 20260408133505_miinventory2026.Designer.cs
├── 20260408222602_ErdProvidersProducts.cs
├── 20260408222602_ErdProvidersProducts.Designer.cs
└── AppDbContextModelSnapshot.cs
```

---

### 3. Raíz del repositorio (archivos en la carpeta del proyecto)

| Archivo |
|---------|
| `appsettings.json` |
| `MyInventory2026.csproj` |
| `MyInventory2026.sln` |
| `Program.cs` |
| `README.md` |

**Carpeta adicional en la raíz:** `.git/` (control de versiones).

---

### 4. Carpetas declaradas en `MyInventory2026.csproj` (`<Folder Include="..."/>`)

Estas entradas **no crean** carpetas por sí solas; sirven para mostrar la estructura en Visual Studio / IDE:

- `src\modules\provider\Application\`
- `src\modules\provider\Application\Interfaces\`
- `src\modules\provider\Application\Services\`
- `src\modules\provider\Application\UseCases\`
- `src\modules\provider\Domain\aggregate\`
- `src\modules\provider\Domain\Repositories\`
- `src\modules\provider\Infrastructure\`
- `src\modules\provider\Infrastructure\entity\`
- `src\modules\provider\Infrastructure\repository\`
- `src\modules\provider\UI\`
- `src\modules\product\Application\`
- `src\modules\product\Application\Interfaces\`
- `src\modules\product\Application\Services\`
- `src\modules\product\Application\UseCases\`
- `src\modules\product\Domain\aggregate\`
- `src\modules\product\Domain\Repositories\`
- `src\modules\product\Domain\valueObject\`
- `src\modules\product\Infrastructure\`
- `src\modules\product\Infrastructure\entity\`
- `src\modules\product\Infrastructure\repository\`
- `src\modules\product\UI\`
- `src\shared\`
- `src\shared\configurations\`
- `src\shared\context\`
- `src\shared\contracts\`
- `src\shared\helpers\`
- `src\shared\ui\`

**Observación:** Falta una entrada `<Folder>` explícita para `src\modules\provider\Domain\valueObject\` en el `.csproj`, aunque la carpeta **sí existe** en disco con `ProviderId.cs` y `ProviderName.cs`.

---

### 5. Salida de compilación — `bin/` (estructura completa, configuración Debug)

```
bin/
└── Debug/
    └── net10.0/
        ├── appsettings.json
        ├── Humanizer.dll
        ├── Microsoft.Bcl.AsyncInterfaces.dll
        ├── Microsoft.Build.Locator.dll
        ├── Microsoft.CodeAnalysis.CSharp.dll
        ├── Microsoft.CodeAnalysis.CSharp.Workspaces.dll
        ├── Microsoft.CodeAnalysis.dll
        ├── Microsoft.CodeAnalysis.Workspaces.dll
        ├── Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.dll
        ├── Microsoft.CodeAnalysis.Workspaces.MSBuild.dll
        ├── Microsoft.EntityFrameworkCore.Abstractions.dll
        ├── Microsoft.EntityFrameworkCore.Design.dll
        ├── Microsoft.EntityFrameworkCore.dll
        ├── Microsoft.EntityFrameworkCore.Relational.dll
        ├── Microsoft.Extensions.Caching.Abstractions.dll
        ├── Microsoft.Extensions.Caching.Memory.dll
        ├── Microsoft.Extensions.Configuration.Abstractions.dll
        ├── Microsoft.Extensions.Configuration.dll
        ├── Microsoft.Extensions.Configuration.EnvironmentVariables.dll
        ├── Microsoft.Extensions.Configuration.FileExtensions.dll
        ├── Microsoft.Extensions.Configuration.Json.dll
        ├── Microsoft.Extensions.DependencyInjection.Abstractions.dll
        ├── Microsoft.Extensions.DependencyInjection.dll
        ├── Microsoft.Extensions.DependencyModel.dll
        ├── Microsoft.Extensions.FileProviders.Abstractions.dll
        ├── Microsoft.Extensions.FileProviders.Physical.dll
        ├── Microsoft.Extensions.FileSystemGlobbing.dll
        ├── Microsoft.Extensions.Logging.Abstractions.dll
        ├── Microsoft.Extensions.Logging.dll
        ├── Microsoft.Extensions.Options.dll
        ├── Microsoft.Extensions.Primitives.dll
        ├── Mono.TextTemplating.dll
        ├── MyInventory2026.deps.json
        ├── MyInventory2026.dll
        ├── MyInventory2026.exe
        ├── MyInventory2026.pdb
        ├── MyInventory2026.runtimeconfig.json
        ├── MySqlConnector.dll
        ├── Pomelo.EntityFrameworkCore.MySql.dll
        ├── System.CodeDom.dll
        ├── System.Composition.AttributedModel.dll
        ├── System.Composition.Convention.dll
        ├── System.Composition.Hosting.dll
        ├── System.Composition.Runtime.dll
        ├── System.Composition.TypedParts.dll
        ├── cs\          (recursos satélite, ver abajo)
        ├── de\
        ├── es\
        ├── fr\
        ├── it\
        ├── ja\
        ├── ko\
        ├── pl\
        ├── pt-BR\
        ├── ru\
        ├── tr\
        ├── zh-Hans\
        └── zh-Hant\
```

**Contenido de cada subcarpeta de idioma** (`cs`, `de`, `es`, `fr`, `it`, `ja`, `ko`, `pl`, `pt-BR`, `ru`, `tr`, `zh-Hans`, `zh-Hant`) — **los mismos 5 archivos** en todas:

- `Microsoft.CodeAnalysis.CSharp.resources.dll`
- `Microsoft.CodeAnalysis.CSharp.Workspaces.resources.dll`
- `Microsoft.CodeAnalysis.resources.dll`
- `Microsoft.CodeAnalysis.Workspaces.MSBuild.BuildHost.resources.dll`
- `Microsoft.CodeAnalysis.Workspaces.resources.dll`

*(Provienen de **Microsoft.CodeAnalysis** / herramientas de diseño de EF; no son código del alumno.)*

---

### 6. Archivos intermedios — `obj/` (lista completa tras compilación)

```
obj/
├── MyInventory2026.csproj.EntityFrameworkCore.targets
├── MyInventory2026.csproj.nuget.dgspec.json
├── MyInventory2026.csproj.nuget.g.props
├── MyInventory2026.csproj.nuget.g.targets
├── project.assets.json
├── project.nuget.cache
└── Debug/
    └── net10.0/
        ├── .NETCoreApp,Version=v10.0.AssemblyAttributes.cs
        ├── apphost.exe
        ├── MyInvent.5111993D.Up2Date
        ├── MyInventory2026.AssemblyInfo.cs
        ├── MyInventory2026.AssemblyInfoInputs.cache
        ├── MyInventory2026.assets.cache
        ├── MyInventory2026.csproj.AssemblyReference.cache
        ├── MyInventory2026.csproj.CoreCompileInputs.cache
        ├── MyInventory2026.csproj.FileListAbsolute.txt
        ├── MyInventory2026.dll
        ├── MyInventory2026.GeneratedMSBuildEditorConfig.editorconfig
        ├── MyInventory2026.genruntimeconfig.cache
        ├── MyInventory2026.GlobalUsings.g.cs
        ├── MyInventory2026.pdb
        ├── MyInventory2026.sourcelink.json
        ├── ref/
        │   └── MyInventory2026.dll
        └── refint/
            └── MyInventory2026.dll
```

**Nota:** El nombre `MyInvent.5111993D.Up2Date` es un marcador interno de MSBuild; puede variar entre compilaciones.

---

### 7. Control de versiones — `.git/`

Contiene el historial y metadatos de Git (**no** se enumeran aquí los miles de objetos internos). Para el alcance del proyecto académico, basta indicar que el código se gestiona con **Git**.

---

### Esquema de base de datos (modelo principal)

| Tabla | Rol |
|-------|-----|
| `providers` | Proveedores (`id` string, `nameProvider`). |
| `products` | Productos (`id` entero identidad, `codeInv` único, nombre, stocks). |
| `provider_products` | Tabla intermedia N:N (`provider_id`, `product_id`, PK compuesta, FKs en cascada). |

---

## Qué hace cada archivo

### Raíz del repositorio

| Archivo / carpeta | Qué hace |
|-------------------|----------|
| `Program.cs` | Punto de entrada: crea `AppDbContext` con `DbContextFactory`, instancia repositorios (`ProviderRepository`, `ProductRepository`), `UnitOfWork`, servicios (`ProviderService`, `ProductService`) y los `IModuleUI` (`ProviderConsoleUI`, `ProductConsoleUI`). Si la BD responde, muestra el menú principal. |
| `appsettings.json` | Define `ConnectionStrings:MySqlDB` para MySQL. Se copia a `bin/Debug/net10.0/`. |
| `MyInventory2026.csproj` | Proyecto SDK, paquetes NuGet, `<Folder>` para IDE, copia de `appsettings.json`. |
| `MyInventory2026.sln` | Solución con el proyecto `MyInventory2026`. |
| `README.md` | Esta documentación. |
| `.git/` | Repositorio Git. |
| `bin/` | Salida de compilación: ejecutable, DLL del proyecto, dependencias NuGet, `appsettings.json` copiado. Ver sección **Estructura del sistema §5**. |
| `obj/` | Cachés MSBuild, archivos generados, metadatos NuGet y EF. Ver **§6**. |

### `bin/Debug/net10.0/` — referencia rápida de dependencias (raíz)

| Grupo | Archivos (idea) |
|-------|-------------------|
| **Propio** | `MyInventory2026.exe`, `MyInventory2026.dll`, `MyInventory2026.pdb`, `MyInventory2026.deps.json`, `MyInventory2026.runtimeconfig.json`, `appsettings.json` |
| **EF Core / Pomelo / MySQL** | `Microsoft.EntityFrameworkCore*.dll`, `Pomelo.EntityFrameworkCore.MySql.dll`, `MySqlConnector.dll` |
| **Configuration / DI / Logging** | `Microsoft.Extensions.*.dll` |
| **Herramientas diseño (EF / Roslyn)** | `Microsoft.CodeAnalysis*.dll`, `Microsoft.Build.Locator.dll`, `Mono.TextTemplating.dll`, `System.CodeDom.dll`, `System.Composition*.dll`, `Humanizer.dll` |
| **Satélites por idioma** | Subcarpetas `cs` … `zh-Hant` con `.resources.dll` de CodeAnalysis |

### `Migrations/`

| Archivo | Qué hace |
|---------|----------|
| `20260408133505_miinventory2026.cs` | Migración EF; `Up`/`Down` según historial (puede estar vacía). |
| `20260408133505_miinventory2026.Designer.cs` | Diseñador de esa migración. |
| `20260408222602_ErdProvidersProducts.cs` | Ajuste de `providers`, creación de `products` y `provider_products`. |
| `20260408222602_ErdProvidersProducts.Designer.cs` | Diseñador de esa migración. |
| `AppDbContextModelSnapshot.cs` | Snapshot del modelo para la siguiente migración. |

### `src/modules/provider/`

| Archivo | Qué hace |
|---------|----------|
| `Domain/aggregate/Provider.cs` | Agregado `Provider` y factory `Create`. |
| `Domain/valueObject/ProviderId.cs` | Id del proveedor (validación). |
| `Domain/valueObject/ProviderName.cs` | Nombre del proveedor (validación). |
| `Domain/Repositories/IProviderRepository.cs` | Contrato de persistencia. |
| `Application/Interfaces/IProviderService.cs` | Contrato del servicio para la UI. |
| `Application/Services/ProviderService.cs` | Servicio + `IUnitOfWork`. |
| `Application/UseCases/CreateProviderUseCase.cs` | Crear si el id no existe. |
| `Application/UseCases/GetAllProvidersUseCase.cs` | Listar todos. |
| `Application/UseCases/GetProviderByIdUseCase.cs` | Obtener por id. |
| `Application/UseCases/UpdateProviderUseCase.cs` | Actualizar. |
| `Application/UseCases/DeleteProviderUseCase.cs` | Eliminar por id. |
| `Infrastructure/entity/ProviderEntity.cs` | Entidad EF `providers` + navegación a puente. |
| `Infrastructure/entity/ProviderEntityConfiguration.cs` | Fluent API del proveedor. |
| `Infrastructure/repository/ProviderRepository.cs` | Implementación del repositorio. |
| `UI/ProviderConsoleUI.cs` | Menú consola (`Key` **1**). |

### `src/modules/product/`

| Archivo | Qué hace |
|---------|----------|
| `Domain/aggregate/Product.cs` | Agregado; `CreateNew` / `Create`; validación de stocks. |
| `Domain/valueObject/ProductId.cs` | Id numérico (`> 0`). |
| `Domain/valueObject/ProductCodeInv.cs` | Código inventario (máx. 10). |
| `Domain/valueObject/ProductName.cs` | Nombre (3–50 caracteres). |
| `Domain/Repositories/IProductRepository.cs` | Contrato (incluye `FindByCodeInv`). |
| `Application/Interfaces/IProductService.cs` | Contrato del servicio. |
| `Application/Services/ProductService.cs` | Servicio + UoW; tras crear, recarga por código para devolver id generado. |
| `Application/UseCases/CreateProductUseCase.cs` | Crear si el código no existe. |
| `Application/UseCases/GetAllProductsUseCase.cs` | Listar todos. |
| `Application/UseCases/GetProductByIdUseCase.cs` | Obtener por id. |
| `Application/UseCases/UpdateProductUseCase.cs` | Actualizar; unicidad de `codeInv`. |
| `Application/UseCases/DeleteProductUseCase.cs` | Eliminar por id. |
| `Infrastructure/entity/ProductEntity.cs` | Entidad EF `products` + navegación. |
| `Infrastructure/entity/ProductEntityConfiguration.cs` | Fluent API + índice único `codeInv`. |
| `Infrastructure/repository/ProductRepository.cs` | Implementación del repositorio. |
| `UI/ProductConsoleUI.cs` | Menú consola (`Key` **2**). |

### `src/shared/`

| Archivo | Qué hace |
|---------|----------|
| `context/AppDbcontext.cs` | `AppDbContext`, `DbSet` proveedores/productos, `ApplyConfigurationsFromAssembly`. |
| `context/AppDbContextDesignTimeFactory.cs` | Factory para `dotnet ef`. |
| `context/ProviderProductEntity.cs` | Entidad tabla `provider_products`. |
| `context/ProviderProductEntityConfiguration.cs` | Mapeo N:N. |
| `context/UnitOWork.cs` | Implementación de `IUnitOfWork` (nombre de archivo con typo **UnitOWork**). |
| `contracts/IUnitOfWork.cs` | Contrato `SaveChangesAsync`. |
| `helpers/DbContextFactory.cs` | Fábrica en tiempo de ejecución. |
| `helpers/MySqlVersionResolver.cs` | Lee versión del servidor MySQL. |
| `ui/IModuleUI.cs` | Contrato de módulos del menú. |
| `configurations/` | Carpeta vacía reservada en el `.csproj`. |

---

## Autor

**Valentina Mancilla**


