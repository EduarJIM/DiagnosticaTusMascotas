# Especificación del motor de triaje

`MotorTriaje.Evaluar(IEnumerable<string>)` convierte una lista de síntomas en un
`ResultadoTriaje` con tres campos: `Severidad`, `Puntuacion` (suma de pesos) e `Implicaciones`
(una explicación por síntoma reconocido). La lógica procede del proyecto
[Diagnostical-Pets](https://github.com/EduarJIM/Diagnostical-Pets).

> ⚠️ Herramienta orientativa. No sustituye la valoración de un veterinario.

## Niveles

| Enumeración | Valor | Etiqueta | Color | Recomendación |
|---|---:|---|---|---|
| `NivelSeveridad.Leve` | 1 | `LEVE` | 🟢 `#10B981` | Monitoreo en casa recomendado. |
| `NivelSeveridad.Moderado` | 2 | `MODERADO` | 🟡 `#F59E0B` | Se sugiere revisión veterinaria pronta. |
| `NivelSeveridad.Critico` | 3 | `CRÍTICO` | 🔴 `#EF4444` | Atención veterinaria inmediata. Riesgo vital. |

## Matriz de decisión

Se evalúan las reglas en este orden (la primera que se cumple gana):

1. `pesoMaximo == 3` → **CRÍTICO**.
2. `Vómitos` **y** `Diarrea` presentes → se fuerza `pesoMaximo = 3` → **CRÍTICO**.
3. `pesoMaximo == 2` **o** `Puntuacion >= 4` → **MODERADO**.
4. En cualquier otro caso → **LEVE**.

Si la lista está vacía o todos los síntomas son desconocidos, el resultado es **LEVE** con
puntuación 0. Los síntomas que no están en el diccionario se ignoran y no generan implicación.

### Casos límite

| Síntomas | Puntuación | Nivel | Por qué |
|---|---:|---|---|
| `Estornudos` | 1 | LEVE | Un solo peso 1 |
| `Estornudos + Secreción nasal + Pérdida de apetito` | 3 | LEVE | Suma < 4 y ningún peso 2 |
| `Estornudos + Pérdida de apetito + Fiebre` | 4 | MODERADO | Hay un peso 2 y además suma 4 |
| `Vómitos` | 2 | MODERADO | Peso 2 |
| `Vómitos + Diarrea` | 4 | CRÍTICO | Regla especial de deshidratación |
| `Dificultad para respirar` | 3 | CRÍTICO | Peso 3 |
| `Convulsiones + Estornudos` | 4 | CRÍTICO | Hay un peso 3 |

## Catálogo de síntomas (20)

### Síntomas generales

| Síntoma | Peso | Nivel | Implicación clínica |
|---|---:|---|---|
| Fiebre | 2 | MODERADO | Proceso infeccioso o inflamatorio activo. |
| Letargo / Debilidad | 2 | MODERADO | Signo inespecífico de enfermedad sistémica, dolor o fiebre. |
| Pérdida de peso | 2 | MODERADO | Posible problema metabólico, parasitario o crónico. |
| Aumento de sed | 2 | MODERADO | Puede indicar problemas renales, diabetes o piómetra. |
| Temblores | 3 | CRÍTICO | Posible dolor agudo, fiebre muy alta, intoxicación o problema neurológico. |

### Respiratorios

| Síntoma | Peso | Nivel | Implicación clínica |
|---|---:|---|---|
| Tos persistente | 2 | MODERADO | Problemas cardíacos, infecciones respiratorias o colapso traqueal. |
| Estornudos | 1 | LEVE | Irritantes ambientales o infección respiratoria alta leve. |
| Dificultad para respirar | 3 | CRÍTICO | Emergencia: fallo cardíaco, asma felino agudo o líquido en pulmones. |
| Secreción nasal | 1 | LEVE | Infección del tracto respiratorio superior o cuerpo extraño. |
| Respiración ruidosa | 2 | MODERADO | Obstrucción parcial de las vías respiratorias superiores. |

### Digestivos

| Síntoma | Peso | Nivel | Implicación clínica |
|---|---:|---|---|
| Vómitos | 2 | MODERADO | Irritación gastrointestinal, dieta, cuerpo extraño o fallo orgánico. |
| Diarrea | 2 | MODERADO | Gastroenteritis, parásitos o infecciones virales (ej. parvovirus). |
| Pérdida de apetito | 1 | LEVE | Primer signo de incomodidad o enfermedad leve; requiere monitoreo. |
| Dificultad al tragar | 3 | CRÍTICO | Posible cuerpo extraño en esófago, reacción alérgica o problema neurológico. |
| Abdomen hinchado | 3 | CRÍTICO | Emergencia: torsión gástrica, hemorragia interna o peritonitis. |

### Neurológicos

| Síntoma | Peso | Nivel | Implicación clínica |
|---|---:|---|---|
| Convulsiones | 3 | CRÍTICO | Actividad cerebral anormal por epilepsia, intoxicación o hipoglucemia. |
| Desorientación | 3 | CRÍTICO | Afectación del SNC, intoxicación o evento vascular. |
| Incoordinación al caminar | 3 | CRÍTICO | Síndrome vestibular, trauma espinal, hernia discal o toxinas. |
| Parálisis | 3 | CRÍTICO | Lesión medular severa, garrapatas paralizantes o tromboembolismo. |
| Cambio de conducta | 2 | MODERADO | Dolor silencioso, senilidad o problemas neurológicos tempranos. |

## Acciones sugeridas por nivel

`MotorTriaje.Acciones(nivel)` devuelve una lista de primeros auxilios:

- **CRÍTICO**: contactar al veterinario de inmediato, minimizar estrés y movimiento, no
  administrar medicamentos humanos y ventilar el área si hay dificultad respiratoria.
- **MODERADO**: programar cita para hoy o mañana, ofrecer agua sin forzar, mantener tranquila a
  la mascota.
- **LEVE**: observar durante horas, asegurar agua fresca y evitar cambios bruscos de dieta.

## Consejos relacionados

`CatalogoConsejos` expone los temas `Caso LEVE`, `Caso MODERADO`, `Caso CRÍTICO`,
`Alimentación`, `Hidratación`, `Vacunas y desparasitación`, `Ejercicio y bienestar`, `Higiene`,
`Botiquín básico` y `Diccionario de síntomas`. El contenido de cada caso se genera a partir de
`Descripcion`, `Acciones` y el propio diccionario de síntomas.

Ver también [referencia de API](REFERENCIA_API.md) y
[diagrama de secuencia](diagramas/03-secuencia-triaje.md).
