# Diagnostica tu Mascota

[![CI](https://github.com/EduarJIM/DiagnosticaTusMascotas/actions/workflows/ci.yml/badge.svg)](https://github.com/EduarJIM/DiagnosticaTusMascotas/actions/workflows/ci.yml)
[![Seguridad de dependencias](https://github.com/EduarJIM/DiagnosticaTusMascotas/actions/workflows/seguridad.yml/badge.svg)](https://github.com/EduarJIM/DiagnosticaTusMascotas/actions/workflows/seguridad.yml)
[![Licencia MIT](https://img.shields.io/badge/licencia-MIT-blue.svg)](LICENSE)
[![.NET 10](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Windows](https://img.shields.io/badge/Windows-10%2F11-0078D6?logo=windows)](https://www.microsoft.com/windows)
[![SQLite](https://img.shields.io/badge/SQLite-3-003B57?logo=sqlite)](https://www.sqlite.org/)

Aplicación de escritorio en **C# / Windows Forms** que orienta al dueño de una mascota sobre la
urgencia de unos síntomas mediante un motor de triaje veterinario. Guarda mascotas, consultas y
recordatorios en una base **SQLite local**, sin servidores ni cuentas de usuario.

> ⚠️ **Aviso médico:** es una herramienta orientativa. **No sustituye el diagnóstico de un
> veterinario**. Ante una emergencia, acude de inmediato a una clínica veterinaria.

Basada en la lógica de triaje del proyecto
[Diagnostical-Pets](https://github.com/EduarJIM/Diagnostical-Pets), adaptada a escritorio sin la
parte web (React, Vite, Tailwind).

---

## ✨ Características

- **Triaje en 3 niveles**: leve, moderado y crítico, con 20 síntomas repartidos en 4 categorías.
- **Reglas clínicas**: un síntoma crítico, vómitos + diarrea, o una puntuación ≥ 4 escalan el nivel.
- **Explicaciones y consejos**: implicación de cada síntoma, primeros auxilios y recomendaciones.
- **Historial persistente**: consultas con síntomas, notas, severidad y filtros por mascota/nivel.
- **Perfiles de mascotas**: datos clínicos, vacunas, antecedentes y veterinario.
- **Alertas**: recordatorios con fecha/hora, prioridad y aviso sonoro mientras la app está abierta.
- **Tema claro/oscuro**: interruptor sol/luna con animación, guardado en SQLite.
- **Interfaz propia**: controles dibujados con GDI+ (esquinas redondeadas, degradados, iconos,
  animaciones, modales y notificaciones tipo *toast*), sin librerías de UI externas.

## 📸 Capturas

> Las capturas se añadirán en la carpeta [`docs/capturas/`](docs/) cuando se publiquen las
> primeras versiones. Mientras tanto, puedes ver los planos de pantalla en la
> [guía de usuario](docs/GUIA_USUARIO.md) y los flujos en los
> [diagramas](docs/INDICE.md#diagramas).

## 🧰 Requisitos

| Requisito | Versión |
|---|---|
| Sistema operativo | Windows 10 / 11 |
| SDK de .NET | 10.0 (fijado en [`global.json`](global.json)) |
| Carga de trabajo | Desarrollo de escritorio de .NET (Windows Forms) |
| IDE recomendado | Visual Studio Community 2026 |
| Base de datos | SQLite (incluida vía NuGet, sin instalación adicional) |

## 🚀 Inicio rápido

### Con Visual Studio

1. Clona el repositorio:
   ```powershell
   git clone https://github.com/EduarJIM/DiagnosticaTusMascotas.git
   cd DiagnosticaTusMascotas
   ```
2. Abre `DiagnosticaTuMascota.sln`.
3. Pulsa **F5**.

### Desde la terminal

```powershell
dotnet restore DiagnosticaTuMascota.sln
dotnet build DiagnosticaTuMascota.sln -c Release
dotnet run --project DiagnosticaTuMascota/DiagnosticaTuMascota.csproj
```

También puedes ejecutar `./scripts/verificar.ps1` para restaurar, compilar y probar en un solo paso.

La base se crea automáticamente en:

```
%AppData%\DiagnosticaTuMascota\diagnostica.db
```

La primera vez se importa un `datos.json` antiguo si existe; si no, se cargan datos de ejemplo.

## 🧭 Navegación

```
FrmPrincipal (barra lateral)
├── Inicio          → FrmInicio
├── Mis Mascotas    → FrmMascotas → FrmEditarMascota (modal)
├── Análisis        → FrmSintomas ⇄ FrmAsignarConsulta → FrmResultado → FrmConsejos
├── Historial       → FrmHistorial → FrmResultado
├── Alertas         → FrmRecordatorios → FrmEditarRecordatorio (modal)
└── Consejos        → FrmConsejos
```

## 🏗️ Arquitectura

```
DiagnosticaTusMascota/
├── Program.cs          Punto de entrada ([STAThread])
├── Core/               Lógica de negocio, sin referencias a Windows Forms
│   ├── Modelos.cs
│   ├── MotorTriaje.cs
│   ├── CatalogoConsejos.cs
│   └── ServicioAlarmas.cs
├── Datos/              Acceso a SQLite y datos iniciales
├── Estilos/            Tema, dibujo GDI+, iconos y animaciones
├── Controles/          Controles propios (botones, tarjetas, campos...)
└── Formularios/        Páginas, modales y diálogos
```

Las páginas son formularios `TopLevel = false` que viven dentro de `FrmPrincipal`; se comunican
mediante eventos (`Continuar`, `Volver`, `Generar`...). Los modales se muestran con `MostrarSobre`.

Consulta la [arquitectura completa](docs/ARQUITECTURA.md), la
[referencia de funciones](docs/REFERENCIA_API.md) y los
[diagramas Mermaid](docs/INDICE.md#diagramas).

## 📊 Reglas de triaje

| Condición | Resultado |
|---|---|
| Algún síntoma con peso 3 | **CRÍTICO** |
| Vómitos + Diarrea juntos | **CRÍTICO** (riesgo de deshidratación) |
| Algún síntoma con peso 2, o suma de pesos ≥ 4 | **MODERADO** |
| Resto | **LEVE** |

Detalle completo en [TRIAJE.md](docs/TRIAJE.md).

## 🗄️ Base de datos

| Tabla | Contenido |
|---|---|
| `Mascotas` | Perfil y datos clínicos |
| `Consultas` | Fecha, mascota, notas, severidad (1-3) y recomendación |
| `ConsultaSintomas` | Síntomas por consulta (`ON DELETE CASCADE`) |
| `Recordatorios` | Título, mascota, mensaje, prioridad y fecha/hora |
| `Preferencias` | Clave/valor (por ejemplo, el tema elegido) |

Esquema y DDL en [BASE_DATOS.md](docs/diagramas/05-entidad-relacion.md).

## 📚 Documentación

Toda la documentación está indexada en [docs/INDICE.md](docs/INDICE.md): especificaciones,
arquitectura, diagramas, base de datos, triaje, API interna, guía de usuario, dependencias,
decisiones de diseño, FAQ y changelog.

## 🧪 Pruebas

```powershell
dotnet test DiagnosticaTuMascota.sln -c Release
```

La suite cubre el motor de triaje. Más información en [docs/PRUEBAS.md](docs/PRUEBAS.md).

## 🤝 Contribuir

¿Quieres ayudar? Lee [docs/CONTRIBUCION.md](docs/CONTRIBUCION.md) y
[docs/CODIGO_CONDUCTA.md](docs/CODIGO_CONDUCTA.md). Los issues y PR son bienvenidos.

## 📄 Licencia

Distribuido bajo licencia [MIT](LICENSE).

## 🙏 Créditos

- Lógica de triaje original: [EduarJIM/Diagnostical-Pets](https://github.com/EduarJIM/Diagnostical-Pets).
- Interfaz: Windows Forms + GDI+ estándar de .NET.
- Base de datos: [Microsoft.Data.Sqlite](https://www.nuget.org/packages/Microsoft.Data.Sqlite).
