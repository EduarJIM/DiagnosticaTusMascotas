# Changelog

Todos los cambios notables de este proyecto se documentan aquí.

El formato se basa en [Keep a Changelog](https://keepachangelog.com/es-ES/1.1.0/) y el proyecto
sigue [Versionado Semántico](https://semver.org/lang/es/) a partir de la primera versión publicada.

## [No publicado]

### Añadido

- Documentación completa: arquitectura, especificación de triaje, base de datos, dependencias,
  decisiones de diseño, referencia de API, guía de usuario, FAQ, contribución y código de conducta.
- Cinco diagramas Mermaid: contexto C4, clases, secuencia, estados de navegación y entidad-relación.
- Índice central de documentación en `docs/INDICE.md`.
- Workflows de GitHub Actions para compilación, auditoría de dependencias y sincronización de
  etiquetas.
- Plantillas de issues, plantilla de PR, `CODEOWNERS`, `Dependabot` y definición de etiquetas.
- Script `scripts/verificar.ps1` para restaurar, compilar y probar localmente.
- Proyecto de pruebas unitarias para el motor de triaje.
- Licencia MIT.

### Cambiado

- `README.md` reescrito con badges, inicio rápido, tablas de triaje y base de datos, y enlaces a
  toda la documentación.
- `global.json` fija el SDK 10.0.402 con `rollForward: latestFeature`.

### Eliminado

- Nada.

## [0.1.0] - 2026-11-23

### Añadido

- Importación inicial de la solución `DiagnosticaTuMascota.sln` con 73 archivos fuente.
- Motor de triaje con 20 síntomas, 4 categorías y 3 niveles de urgencia.
- Persistencia SQLite de mascotas, consultas, síntomas, recordatorios y preferencias.
- Interfaz Windows Forms con tema claro/oscuro, controles propios y animaciones GDI+.
- Datos de ejemplo e importación del antiguo `datos.json`.

[No publicado]: https://github.com/EduarJIM/DiagnosticaTusMascotas/compare/main...HEAD
[0.1.0]: https://github.com/EduarJIM/DiagnosticaTusMascotas/releases/tag/v0.1.0
