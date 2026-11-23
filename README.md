# Diagnostica tu Mascota (Windows Forms)

Aplicación de escritorio en C# / Windows Forms basada en el proyecto
[Diagnostical-Pets](https://github.com/EduarJIM/Diagnostical-Pets). Se tomó la lógica
de triaje veterinario y se adaptó a formularios de escritorio, sin la parte web
(React, Vite, Tailwind).

> Herramienta orientativa: no sustituye el diagnóstico de un veterinario.

## Requisitos

- Visual Studio Community 2026 con la carga de trabajo **Desarrollo de escritorio de .NET**
- .NET 10 SDK

## Cómo abrirlo

1. Abre `DiagnosticaTuMascota.sln` en Visual Studio.
2. Pulsa **F5**.

Los formularios se pueden editar en el Diseñador (doble clic sobre cualquier `Frm*.cs`).

## Estructura

```
DiagnosticaTuMascota/
├── Program.cs                 Punto de entrada
├── Core/                      Lógica del negocio (no sabe nada de ventanas)
│   ├── Modelos.cs             Mascota, Consulta, Recordatorio, NivelSeveridad
│   ├── MotorTriaje.cs         Diccionario de síntomas y reglas de urgencia
│   ├── CatalogoConsejos.cs    Consejos por tema y por nivel
│   └── ServicioAlarmas.cs     Cuándo debe sonar un recordatorio
├── Datos/                     Base de datos SQLite
│   ├── Almacenamiento.cs      Tablas, INSERT/UPDATE/DELETE/SELECT y preferencias
│   └── DatosIniciales.cs      Datos de ejemplo de la primera ejecución
├── Estilos/                   El "CSS" de la aplicación
│   ├── Tema.cs                Colores de theme.css (modo oscuro y claro) y fuentes
│   ├── Dibujo.cs              Esquinas redondeadas, degradados, sombras y brillos
│   ├── Iconos.cs              Iconos (fuente de iconos de Windows + huella y destello)
│   └── Animador.cs            Motor de animaciones (sustituye a "motion" de la web)
├── Controles/                 Controles propios dibujados a mano
│   ├── BotonRedondeado        Botones: primario, suave, peligro, pastilla, enlace...
│   ├── PanelTarjeta           Tarjeta redondeada con borde y sombra
│   ├── TarjetaFuncion         Tarjeta del inicio que crece al pasar el ratón
│   ├── ElementoNav            Opción de la barra lateral
│   ├── InterruptorTema        Interruptor sol/luna con efecto resorte
│   ├── HeroAnimado            Ilustración animada del inicio
│   ├── CajaTexto, ListaDesplegable, SelectorFecha   Campos de formulario
│   ├── BannerUrgencia, TarjetaLista                 Bloques del resultado
│   └── TarjetaMascota, TarjetaConsulta, ...         Tarjetas de las listas
└── Formularios/
    ├── Base/
    │   ├── PaginaBase         Base de las páginas que van dentro de la ventana
    │   ├── ModalBase          Base de las ventanas modales (fondo oscurecido)
    │   └── Dialogos           Confirmaciones y avisos con el estilo de la app
    ├── FrmPrincipal           Barra lateral + área de contenido + alarmas
    ├── FrmInicio              Bienvenida, accesos y aviso médico
    ├── FrmSintomas            Diagnóstico, paso 1: elegir síntomas
    ├── FrmAsignarConsulta     Diagnóstico, paso 2: mascota y notas
    ├── FrmResultado           Nivel de urgencia, implicaciones y acciones
    ├── FrmMascotas            Tarjetas de mascotas
    ├── FrmEditarMascota       Modal de alta/edición con historial de la mascota
    ├── FrmHistorial           Consultas guardadas con filtros
    ├── FrmRecordatorios       Alertas y recordatorios
    ├── FrmEditarRecordatorio  Modal "Crear Alerta"
    ├── FrmConsejos            Consejos de cuidado y diccionario de síntomas
    ├── FrmAlarma              Aviso cuando llega la hora de un recordatorio
    └── FrmConfirmar           Diálogo de confirmación
```

## Diseño (basado en la versión web)

El aspecto reproduce el front-end React del repositorio original:

- **Colores**: los de `theme.css`. Por defecto se usa el modo oscuro; el interruptor sol/luna
  de la barra lateral cambia al claro, y la elección se guarda en SQLite.
- **Layout**: barra lateral fija a la izquierda y páginas a la derecha, como las rutas de la web.
- **Animaciones**: las páginas aparecen subiendo y con un fundido, las tarjetas crecen al pasar
  el ratón, la barra de progreso se llena sola, el logotipo late, el círculo del inicio flota con
  anillos y un destello que orbita, la campana de la alarma se sacude y los modales aparecen
  sobre un fondo oscurecido. Hay notificaciones tipo "toast" arriba a la derecha.

Todo está hecho con Windows Forms estándar y GDI+ (`OnPaint`), sin librerías externas de interfaz.

## Navegación

```
FrmPrincipal (barra lateral)
├── Inicio          → FrmInicio
├── Mis Mascotas    → FrmMascotas → FrmEditarMascota (modal)
├── Análisis        → FrmSintomas ⇄ FrmAsignarConsulta → FrmResultado → FrmConsejos
├── Historial       → FrmHistorial → FrmResultado
├── Alertas         → FrmRecordatorios → FrmEditarRecordatorio (modal)
└── Consejos        → FrmConsejos
```

Las páginas son formularios que se muestran **dentro** de la ventana principal
(`TopLevel = false`). Cada página avisa con eventos (`Continuar`, `Volver`, `Generar`...)
y `FrmPrincipal` decide a dónde ir. Los modales se abren con `MostrarSobre(this)`.

## Base de datos (SQLite)

Se usa el paquete NuGet `Microsoft.Data.Sqlite`. No hay que instalar ningún servidor:
la base es un solo archivo, que se crea solo la primera vez que se abre la app:

```
%AppData%\DiagnosticaTuMascota\diagnostica.db
```

| Tabla | Contenido |
|---|---|
| `Mascotas` | Perfil y datos clínicos de cada mascota |
| `Consultas` | Cada diagnóstico: fecha, mascota, notas, severidad (1-3), recomendación |
| `ConsultaSintomas` | Síntomas de cada consulta (relación 1 a muchos, `ON DELETE CASCADE`) |
| `Recordatorios` | Título, mascota, mensaje, prioridad y fecha/hora |

`Consultas.Mascota` guarda el nombre como texto, no una clave foránea. Así una consulta
puede ser de una mascota no registrada, y el historial se conserva si borras la mascota.

Todo el SQL está en `Datos\Almacenamiento.cs`. Las consultas usan parámetros
(`$nombre`, `$id`...) para evitar inyección SQL. El filtro del Historial se hace en la
propia base con `BuscarConsultas(mascota, nivel)`.

La primera vez, si existe el `datos.json` de la versión anterior, sus datos se importan
a la base. Si no existe, se cargan los datos de ejemplo.

Para ver o editar la base a mano puedes usar [DB Browser for SQLite](https://sqlitebrowser.org/).

## Reglas del triaje

Cada síntoma tiene un peso: 1 (leve), 2 (moderado) o 3 (crítico).

| Condición | Resultado |
|---|---|
| Algún síntoma con peso 3 | **CRÍTICO** |
| Vómitos + Diarrea juntos | **CRÍTICO** (riesgo de deshidratación) |
| Algún síntoma con peso 2, o suma de pesos ≥ 4 | **MODERADO** |
| Resto | **LEVE** |

## Diferencias con el original

- Sin inicio de sesión ni cuentas: es una app local de un solo usuario.
- El `localStorage` del navegador se reemplaza por una base de datos SQLite.
- Las alarmas del navegador se reemplazan por un `Timer` y un `MessageBox` con sonido.
- Se añadió el formulario de **Consejos** y filtros en el historial.
