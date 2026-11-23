# Cómo contribuir

¡Gracias por interesarte! Hay muchas formas de ayudar: corregir documentación, reportar un error,
proponer síntomas o mejorar el motor de triaje.

## Antes de empezar

1. Lee el [código de conducta](CODIGO_CONDUCTA.md) y el [aviso médico](../README.md).
2. Busca si ya existe un issue sobre lo que quieres hacer.
3. Para cambios grandes, abre primero un issue para discutir el enfoque.

## Preparar el entorno

```powershell
git clone https://github.com/EduarJIM/DiagnosticaTusMascotas.git
cd DiagnosticaTusMascotas
dotnet restore DiagnosticaTuMascota.sln
dotnet build DiagnosticaTuMascota.sln -c Release
dotnet test DiagnosticaTuMascota.sln -c Release
```

Necesitas Windows, .NET 10 y (opcionalmente) Visual Studio 2026 con la carga de trabajo de
escritorio .NET.

## Ramas y commits

- Crea una rama desde `main`: `git switch -c feature/nueva-regla-triaje`.
- Nombres sugeridos: `fix/`, `feature/`, `docs/`, `chore/`, `test/`.
- Mensajes de commit en español, con prefijo y en imperativo:
  - `docs: corregir tabla de síntomas respiratorios`
  - `test: cubrir caso vómitos + diarrea`
  - `fix: evitar duplicar avisos de alarma`
- Un commit = un cambio lógico. Los commits vacíos o puramente cosméticos se agrupan.

## Estilo de código

- C# con `ImplicitUsings` y 4 espacios de indentación.
- Los métodos públicos llevan documentación XML `/// <summary>`.
- `Core` **no debe** referenciar `System.Windows.Forms`. Si necesitas un color, usa
  `System.Drawing.Color` como ya hace `MotorTriaje.ColorNivel`.
- Las cadenas nuevas del dominio van en español y se inicializan a `""` en los modelos.
- Todo SQL nuevo debe usar parámetros (`$id`, `$nombre`), nunca concatenación.
- Los textos de interfaz van en `Core` o en el formulario, no dispersos en los controles.

## Pruebas

- Añade pruebas en `tests/DiagnosticaTuMascota.Tests` cuando cambies `MotorTriaje`,
  `ServicioAlarmas` o `CatalogoConsejos`.
- Ejecuta `dotnet test DiagnosticaTuMascota.sln -c Release` antes de abrir el PR.
- Si cambias una regla de triaje, actualiza también [TRIAJE.md](TRIAJE.md) y el
  [diagrama de clases](diagramas/02-clases.md) si procede.

## Documentación

- El índice de documentación es [INDICE.md](INDICE.md); enlaza desde él cualquier archivo nuevo.
- Los diagramas son Mermaid y viven en `docs/diagramas/`. GitHub los renderiza sin herramientas
  adicionales.
- Los cambios visibles para el usuario se registran en [CHANGELOG.md](CHANGELOG.md).

## Pull request

1. Haz push de tu rama y abre un PR contra `main`.
2. Rellena la plantilla: descripción, tipo de cambio, issues relacionados y comprobaciones.
3. Asegúrate de que CI pase (compilación, pruebas y auditoría de dependencias).
4. Un mantenedor revisará el PR. Atiende los comentarios con commits adicionales o *squash*.

## Reportar un error

Usa la plantilla de issue **🐞 Error** e incluye pasos para reproducirlo, versión de Windows/.NET y
capturas si puedes. Si es un problema de salud de una mascota, **no lo reportes como bug**: acude a
un veterinario.

## Licencia

Al contribuir aceptas que tu trabajo se distribuya bajo la [licencia MIT](../LICENSE).
