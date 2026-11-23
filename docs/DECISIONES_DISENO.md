# Decisiones de diseño (ADR)

Registro breve de las decisiones arquitectónicas y su motivo.

## ADR-001 — Aplicación local de un solo usuario

- **Estado**: aceptada.
- **Contexto**: el proyecto web original tenía cuentas e inicio de sesión.
- **Decisión**: eliminar autenticación; no hay usuarios ni contraseñas.
- **Motivo**: es una herramienta doméstica; simplifica el despliegue y evita almacenar datos
  sensibles de acceso. Consecuencia: no hay sincronización entre dispositivos.

## ADR-002 — SQLite en un archivo

- **Estado**: aceptada.
- **Decisión**: usar `Microsoft.Data.Sqlite` con la base en `%AppData%\DiagnosticaTuMascota\diagnostica.db`.
- **Motivo**: reemplaza `localStorage` del navegador; no requiere servidor, admite SQL parametrizado
  y el respaldo es copiar un archivo.
- **Alternativa descartada**: SQL Server / PostgreSQL (innecesarios para un usuario).

## ADR-003 — Windows Forms + GDI+ sin librerías de UI

- **Estado**: aceptada.
- **Decisión**: replicar `theme.css` y las animaciones con controles propios dibujados a mano.
- **Motivo**: control total del aspecto (esquinas redondeadas, degradados, sombras, iconos) sin
  añadir dependencias de terceros.
- **Coste**: más código de pintura; los formularios `*.Designer.cs` son más pequeños porque gran
  parte del diseño se hace en `OnPaint`.

## ADR-004 — Páginas embebidas en `FrmPrincipal`

- **Estado**: aceptada.
- **Decisión**: los formularios de página son `TopLevel = false` y se añaden a un panel de
  `FrmPrincipal`; los modales usan `MostrarSobre(this)`.
- **Motivo**: barra lateral siempre visible, una sola ventana principal y transiciones controladas
  por `CapaTransicion`.

## ADR-005 — `Core` sin Windows Forms

- **Estado**: aceptada.
- **Decisión**: la lógica de triaje, consejos y alarmas no depende de controles (salvo el `Color`
  devuelto por `MotorTriaje.ColorNivel`).
- **Motivo**: permite probar las reglas con xUnit sin abrir ventanas y hace explícita la frontera
  entre dominio y presentación.

## ADR-006 — Nombres de mascota como texto en consultas y alertas

- **Estado**: aceptada.
- **Decisión**: `Consultas.Mascota` y `Recordatorios.Mascota` son `TEXT`, no claves foráneas.
- **Motivo**: una consulta puede pertenecer a una mascota no registrada y el historial se conserva
  al borrar una ficha.
- **Consecuencia**: renombrar una mascota no reescribe consultas antiguas; es un compromiso
  deliberado a favor de la integridad del historial.

## ADR-007 — Triaje determinista y explicable

- **Estado**: aceptada.
- **Decisión**: reglas fijas con pesos 1/2/3 y una regla especial para vómitos + diarrea; se
  guardan las implicaciones por síntoma.
- **Motivo**: el usuario debe entender *por qué* aparece un nivel; evita modelos opacos.
- **Salvedad**: es orientativo y nunca sustituye al veterinario (ADR-008 del aviso médico).

## ADR-008 — Aviso médico visible

- **Estado**: aceptada.
- **Decisión**: mensaje de advertencia en Inicio y en los resultados.
- **Motivo**: responsabilidad legal y ética; el triaje puede dar un falso negativo.

## ADR-009 — Fijar el SDK con `global.json`

- **Estado**: aceptada.
- **Decisión**: `10.0.402` con `rollForward: latestFeature`.
- **Motivo**: reproducibilidad local y en CI sin bloquear parches posteriores de .NET 10.

## ADR-010 — Alarmas solo con la app abierta

- **Estado**: aceptada.
- **Decisión**: un `Timer` de `FrmPrincipal` comprueba los recordatorios; se usa `MessageBox` con
  sonido.
- **Motivo**: sustituye las notificaciones del navegador sin servicios del sistema ni permisos de Windows.
- **Limitación**: si la aplicación está cerrada, no avisa. Posible mejora futura: tarea programada
  o notificación del Centro de actividad.

## Cómo proponer un cambio

Añade un ADR nuevo en este archivo con contexto, decisión, motivo y consecuencias. No reescribas
los antiguos: el historial de decisiones también es documentación.
