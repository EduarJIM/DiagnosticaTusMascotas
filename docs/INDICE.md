# Índice de documentación

Toda la documentación del repositorio en un solo lugar.

## Inicio

| Documento | Contenido |
|---|---|
| [README principal](../README.md) | Descripción, características, instalación y navegación |
| [LICENSE](../LICENSE) | Licencia MIT |
| [CHANGELOG](CHANGELOG.md) | Historial de cambios |
| [Guía de contribución](CONTRIBUCION.md) | Cómo preparar el entorno, estilo y PR |
| [Código de conducta](CODIGO_CONDUCTA.md) | Normas de convivencia |

## Especificación y diseño

| Documento | Contenido |
|---|---|
| [Arquitectura](ARQUITECTURA.md) | Capas, responsabilidades y flujo entre formularios |
| [Reglas de triaje](TRIAJE.md) | Niveles, matriz de decisión y catálogo de 20 síntomas |
| [Base de datos](diagramas/05-entidad-relacion.md) | Modelo ER, DDL SQLite y consultas habituales |
| [Dependencias](DEPENDENCIAS.md) | SDK, NuGet, BCL y lo que deliberadamente no se usa |
| [Decisiones de diseño](DECISIONES_DISENO.md) | ADR-001 a ADR-010 con motivos y consecuencias |
| [Referencia de API](REFERENCIA_API.md) | Funciones y miembros públicos por módulo |

## Uso

| Documento | Contenido |
|---|---|
| [Guía de usuario](GUIA_USUARIO.md) | Instalación, recorrido por las 6 secciones y respaldo |
| [FAQ y solución de problemas](FAQ.md) | Alarmas, antivirus, rutas de datos y errores de compilación |
| [Pruebas](PRUEBAS.md) | Cómo ejecutar y ampliar la suite xUnit |

## Diagramas

Todos están en `docs/diagramas/` y usan **Mermaid**, que GitHub renderiza de forma nativa.

| Diagrama | Archivo |
|---|---|
| Contexto y contenedores C4 | [01-contexto.md](diagramas/01-contexto.md) |
| Clases del dominio | [02-clases.md](diagramas/02-clases.md) |
| Secuencia de triaje y alarmas | [03-secuencia-triaje.md](diagramas/03-secuencia-triaje.md) |
| Estados de navegación | [04-estados-navegacion.md](diagramas/04-estados-navegacion.md) |
| Entidad-relación y DDL | [05-entidad-relacion.md](diagramas/05-entidad-relacion.md) |

## Automatización

| Archivo | Contenido |
|---|---|
| [CI](../.github/workflows/ci.yml) | Restaurar, compilar en Release y subir artefacto |
| [Seguridad](../.github/workflows/seguridad.yml) | Auditoría NuGet y revisión de dependencias en PR |
| [Etiquetas](../.github/workflows/labels.yml) | Sincronización desde `.github/labels.yml` |
| [Dependabot](../.github/dependabot.yml) | Actualizaciones semanales de NuGet y GitHub Actions |
| [Verificación local](../scripts/verificar.ps1) | Restore + build + test en un solo comando |

## Convención de nombres

- Los archivos de documentación van en **MAYÚSCULAS** con guiones bajos, salvo los diagramas, que
  usan número y guion para conservar el orden.
- Los ejemplos de código y comandos usan PowerShell porque el proyecto es de Windows.
- El contenido técnico está en español; los identificadores de código se dejan en su forma original.
