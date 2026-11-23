# Estrategia de pruebas

La suite vive en `tests/DiagnosticaTuMascota.Tests` y usa **xUnit**. Se ejecuta en local y en el
workflow de CI.

## Ejecutar

```powershell
# Solo pruebas
dotnet test DiagnosticaTuMascota.sln -c Release

# Con cobertura (XPlat)
dotnet test DiagnosticaTuMascota.sln -c Release --collect:"XPlat Code Coverage"

# Todo en un paso: restore + build + test
./scripts/verificar.ps1
```

Los resultados de cobertura se generan en `tests/**/TestResults/**/coverage.cobertura.xml` y CI los
sube como artefacto.

## Estado actual

| Métrica | Valor |
|---|---:|
| Proyectos de prueba | 1 |
| Pruebas | 30 |
| Fallos | 0 |
| frameworks | `net10.0-windows` |

## Cobertura funcional

### `MotorTriajeTests`

| Caso | Resultado esperado |
|---|---|
| Lista vacía / lista nula | `LEVE`, puntuación 0, sin implicaciones |
| Un síntoma leve (Estornudos, Secreción nasal, Pérdida de apetito) | `LEVE`, puntuación 1 |
| Tres síntomas leves (suma 3) | `LEVE` (la suma no llega a 4) |
| Un síntoma moderado (Fiebre, Tos persistente, Cambio de conducta) | `MODERADO`, puntuación 2 |
| Fiebre + Estornudos + Secreción nasal (suma 4) | `MODERADO` |
| Vómitos + Diarrea | `CRÍTICO` por la regla de deshidratación, con implicación específica |
| Síntoma crítico (Dificultad para respirar, Temblores, Convulsiones, Parálisis) | `CRÍTICO`, puntuación 3 |
| Crítico + leve | `CRÍTICO` (un peso 3 escala siempre) |
| Síntomas desconocidos | Se ignoran; no suman ni generan implicaciones |
| Mezcla de conocidos y desconocidos | Solo cuentan los conocidos |
| `Etiqueta`, `Recomendacion`, `Descripcion`, `Acciones` y `ColorNivel` | Textos y colores por nivel |
| Integridad del catálogo | 20 síntomas, 4 categorías, todos presentes en el diccionario |

### `ServicioAlarmasTests`

| Caso | Resultado esperado |
|---|---|
| Mismo minuto (aunque los segundos difieran) | `DebeSonar == true` |
| Otro minuto, otro día u otro mes | `DebeSonar == false` |
| Recordatorio nulo | `DebeSonar == false` |
| Mezcla de alarmas con `yaAvisados` | `Pendientes` devuelve solo la no avisada que coincide con el minuto |

## Dependencias del proyecto de pruebas

| Paquete | Versión instalada | Uso |
|---|---|---|
| `Microsoft.NET.Test.Sdk` | 18.12.0 | Host de pruebas |
| `xunit` | 2.9.3 | Framework y aserciones |
| `xunit.runner.visualstudio` | 4.0.1 | Descubrimiento en `dotnet test` |
| `coverlet.collector` | 10.1.1 | Recolección de cobertura |

Las versiones se resolvieron con `dotnet add package` y están fijadas en el `.csproj`.

## Cómo añadir pruebas

1. Crea un archivo `AlgoTests.cs` en `tests/DiagnosticaTuMascota.Tests/`.
2. Usa `[Fact]` para un caso y `[Theory]` + `[InlineData]` para tablas de casos.
3. Nombra el método con el patrón `Metodo_Escenario_ResultadoEsperado`.
4. Si tocas `MotorTriaje`, actualiza también [TRIAJE.md](TRIAJE.md) con el nuevo caso límite.
5. Ejecuta `dotnet test` antes de abrir el PR.

## Lo que todavía no se prueba

- Interacción de Windows Forms (requeriría tests de UI y una sesión interactiva).
- Escritura real en SQLite (se necesitaría una base temporal por prueba).
- Formato y pintura GDI+ de los controles.

Son mejoras candidatas para futuras versiones; consulta [CONTRIBUCION.md](CONTRIBUCION.md) si quieres
abordarlas.

## Integración en CI

`.github/workflows/ci.yml` ejecuta `dotnet test ... --no-build --collect:"XPlat Code Coverage"`
después de compilar en Release, y sube `coverage.cobertura.xml` como artefacto. Un fallo en cualquier
prueba hace fallar el workflow.

Volver al [índice de documentación](INDICE.md).
