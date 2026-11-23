# Dependencias y requisitos

## Requisitos de ejecución

| Componente | Versión | Observaciones |
|---|---|---|
| Sistema operativo | Windows 10 / 11 | Windows Forms solo funciona en Windows |
| .NET Runtime (Desktop) | 10.0 | Incluido con el SDK; `Microsoft.WindowsDesktop.App` |
| Arquitectura | x64 / x86 / ARM64 | El `.sln` compila con `Any CPU` |
| Espacio en disco | ~50 MB | Aproximadamente, con SDK y paquetes restaurados |

## Requisitos de compilación

| Componente | Versión |
|---|---|
| .NET SDK | 10.0.402 o superior 10.0.x (ver [`global.json`](../global.json)) |
| Carga de trabajo | `Desktop development with .NET` (en Visual Studio) |
| IDE | Visual Studio Community 2026 (recomendado) o `dotnet CLI` |
| Lenguaje | C# con `ImplicitUsings` activado, `Nullable` desactivado |

Si compilas desde la CLI, el SDK ya incluye los *targeting packs* de Windows Forms, así que no
necesitas instalar la carga de trabajo por separado.

## Paquetes NuGet

### Directo

| Paquete | Versión | Uso |
|---|---|---|
| [`Microsoft.Data.Sqlite`](https://www.nuget.org/packages/Microsoft.Data.Sqlite) | `10.0.15` | Conexión, comandos y parámetros SQLite; creación del archivo `.db` |

Declarado en `DiagnosticaTuMascota/DiagnosticaTuMascota.csproj`:

```xml
<ItemGroup>
  <PackageReference Include="Microsoft.Data.Sqlite" Version="10.0.15" />
</ItemGroup>
```

### Transitivos habituales

`Microsoft.Data.Sqlite` arrastra, entre otros:

- `Microsoft.Data.Sqlite.Core`
- `SQLitePCLRaw.bundle_e_sqlite3` (motor SQLite nativo embebido, sin instalación externa)
- `SQLitePCLRaw.core`, `SQLitePCLRaw.provider.e_sqlite3`
- `Microsoft.Extensions.DependencyInjection.Abstractions` y paquetes de logging

Las versiones exactas se resuelven en el `packages.lock.json` que genera NuGet; no están fijadas a
mano porque son dependencias del paquete principal.

### Dependencias del BCL (sin paquete)

| Espacio de nombres | Uso |
|---|---|
| `System.Windows.Forms` | Ventanas, controles, `Application`, `Timer` |
| `System.Drawing` / GDI+ | `Graphics`, `Bitmap`, `Color`, `Font`, `GraphicsPath` |
| `System.Text.Json` | Importación del antiguo `datos.json` |
| `System.Globalization` | Formato de fechas |
| `System.Linq`, `System.Collections.Generic` | Consultas y colecciones en memoria |

## Lo que NO se usa

- **No hay framework de UI externo** (MahApps, MaterialDesign, Avalonia, etc.). Todo se dibuja con
  GDI+ en `Estilos/` y `Controles/`.
- **No hay servidor de base de datos**. SQLite es un archivo local.
- **No hay sistema de autenticación** ni servicios en la nube.
- **No hay React, Vite ni Tailwind**: la lógica se tomó del proyecto web original, pero la
  interfaz es nativa de Windows.

## Gestión y auditoría

```powershell
dotnet restore DiagnosticaTuMascota.sln
dotnet list DiagnosticaTuMascota/DiagnosticaTuMascota.csproj package
dotnet list DiagnosticaTuMascota/DiagnosticaTuMascota.csproj package --vulnerable
dotnet list DiagnosticaTuMascota/DiagnosticaTuMascota.csproj package --outdated
```

El workflow [`.github/workflows/seguridad.yml`](../.github/workflows/seguridad.yml) ejecuta la
comprobación de vulnerabilidades y Dependabot propone actualizaciones del paquete.

## Licencias de terceros

- `Microsoft.Data.Sqlite` y SQLitePCLRaw: licencia MIT.
- SQLite (motor embebido): [dominio público](https://www.sqlite.org/copyright.html).
- .NET y Windows Forms: licencia MIT de .NET; el SDK/IDE de Microsoft tiene sus propias condiciones.

Ver también [decisiones de diseño](DECISIONES_DISENO.md).
