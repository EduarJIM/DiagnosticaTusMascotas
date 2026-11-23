# Preguntas frecuentes y solución de problemas

## ¿La aplicación sustituye a un veterinario?

No. Es una herramienta **orientativa**. Ante un resultado `CRÍTICO` o `MODERADO`, acude a un
veterinario. No administres medicamentos humanos a tu mascota.

## ¿Dónde se guardan mis datos?

En `%AppData%\DiagnosticaTuMascota\diagnostica.db`. Puedes pegar esa ruta en el explorador de
archivos de Windows. No se envía nada a Internet.

## ¿Cómo hago una copia de seguridad?

Cierra la app y copia `diagnostica.db` a otro lugar. Para restaurar, pega el archivo en la misma
ruta (sobrescribiendo el actual) y vuelve a abrir la aplicación.

## ¿Puedo usarlo en macOS o Linux?

No. Windows Forms solo funciona en Windows. La lógica de `Core` sí es portable, pero la interfaz no.

## El antivirus elimina el archivo `.db`, ¿qué hago?

Añade la carpeta `%AppData%\DiagnosticaTuMascota` a las exclusiones de tu antivirus. Algunos
antivirus confunden los archivos SQLite recién creados con amenazas. La app volverá a crear la base
al iniciarse, pero se perderán los datos si no tienes copia.

## ¿Por qué no suena la alarma?

- La app debe estar **abierta**: las alarmas no funcionan en segundo plano cerrado.
- Comprueba que la fecha y hora del recordatorio sean correctas; `ServicioAlarmas.DebeSonar`
  compara año/mes/día/hora/**minuto** exactos.
- Cada recordatorio avisa una sola vez por minuto (controlado por `yaAvisados`).
- Revisa el volumen del sistema y el de `MessageBox`.

## ¿Puedo borrar una mascota sin perder su historial?

Sí. Las consultas guardan el **nombre** de la mascota como texto, no una clave foránea, así que el
historial permanece en `Historial`. Puedes filtrar por ese nombre.

## ¿Qué hago si un síntoma no aparece en la lista?

El catálogo tiene 20 síntomas fijos. Puedes usar las **notas** de `FrmAsignarConsulta` para describir
cualquier otro signo y comentarlo con tu veterinario. Añadir síntomas requiere modificar
`Core/MotorTriaje.cs`.

## ¿Cómo reinicio la aplicación a los datos de ejemplo?

Cierra la app, renombra `diagnostica.db` a `diagnostica.db.bak` y vuelve a abrirla. Se creará una
base nueva con los datos iniciales. El archivo `.bak` queda como respaldo.

## `dotnet build` falla con "SDK not found"

Instala el SDK 10.0 o superior y comprueba:

```powershell
dotnet --version
```

El repositorio incluye `global.json` pidiendo `10.0.402` con `rollForward: latestFeature`, así que
cualquier 10.0.x reciente sirve. En Visual Studio, instala la carga de trabajo
**Desarrollo de escritorio de .NET**.

## ¿Cómo ejecuto las pruebas?

```powershell
dotnet test DiagnosticaTuMascota.sln -c Release
```

Detalles en [PRUEBAS.md](PRUEBAS.md).

## ¿Los temas claro/oscuro se guardan?

Sí. `Tema.CambiarModo` escribe la preferencia `tema` en la tabla `Preferencias` y se restaura al
abrir la app.

## ¿Puedo cambiar el color de la interfaz?

Sí, edita las constantes de `Estilos/Tema.cs` (equivalente a `theme.css` en la versión web) y
recompila. Ten cuidado con el contraste de texto sobre los fondos.

## ¿Hay una versión web?

La lógica original está en [Diagnostical-Pets](https://github.com/EduarJIM/Diagnostical-Pets)
(React + Vite + Tailwind). Este repositorio es la adaptación de escritorio y no incluye la parte web.
