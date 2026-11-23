# Diagrama de clases del dominio

Modelos y servicios de la capa `Core`. Es el corazón de la aplicación y es independiente de la
interfaz.

```mermaid
classDiagram
    class NivelSeveridad {
        <<enumeration>>
        Leve = 1
        Moderado = 2
        Critico = 3
    }

    class Mascota {
        +long Id
        +string Nombre
        +string Edad
        +string Raza
        +string Sexo
        +string Vacunas
        +string Antecedentes
        +string InfoClinica
        +string Veterinario
        +ToString() string
    }

    class Consulta {
        +long Id
        +DateTime Fecha
        +string Mascota
        +List~string~ Sintomas
        +string Notas
        +NivelSeveridad Severidad
        +string Recomendacion
        +FechaTexto string
        +SeveridadTexto string
        +SintomasTexto string
    }

    class Recordatorio {
        +long Id
        +string Titulo
        +string Mascota
        +string Mensaje
        +string Prioridad
        +DateTime FechaHora
        +FechaTexto string
        +HoraTexto string
    }

    class ResultadoTriaje {
        +NivelSeveridad Severidad
        +List~string~ Implicaciones
        +int Puntuacion
    }

    class DatosSintoma {
        <<record>>
        +NivelSeveridad Severidad
        +string Implicacion
        +int Peso
    }

    class CategoriaSintomas {
        <<record>>
        +string Nombre
        +string[] Sintomas
        +ToString() string
    }

    class MotorTriaje {
        <<static>>
        +CategoriaSintomas[] Categorias
        +IReadOnlyDictionary~string,DatosSintoma~ Diccionario
        +Evaluar(IEnumerable~string~) ResultadoTriaje
        +Etiqueta(NivelSeveridad) string
        +Recomendacion(NivelSeveridad) string
        +Descripcion(NivelSeveridad) string
        +Acciones(NivelSeveridad) string[]
        +ColorNivel(NivelSeveridad) Color
    }

    class CatalogoConsejos {
        <<static>>
        +TemaDeNivel(NivelSeveridad) string
        +Temas() IEnumerable~string~
        +Contenido(string) string
    }

    class ServicioAlarmas {
        <<static>>
        +DebeSonar(Recordatorio, DateTime) bool
        +Pendientes(IEnumerable~Recordatorio~, DateTime, ISet~long~) List~Recordatorio~
    }

    class Almacenamiento {
        +string RutaBaseDatos
        +List~Mascota~ Mascotas
        +List~Consulta~ Consultas
        +List~Recordatorio~ Recordatorios
        +Cargar() void
        +AgregarMascota(Mascota) void
        +ActualizarMascota(Mascota) void
        +EliminarMascota(long) void
        +AgregarConsulta(Consulta) void
        +EliminarConsulta(long) void
        +BuscarConsultas(string, NivelSeveridad?) List~Consulta~
        +AgregarRecordatorio(Recordatorio) void
        +EliminarRecordatorio(long) void
        +LeerPreferencia(string, string) string
        +GuardarPreferencia(string, string) void
    }

    MotorTriaje --> DatosSintoma : Diccionario
    MotorTriaje --> CategoriaSintomas : Categorias
    MotorTriaje ..> ResultadoTriaje : Evaluar crea
    MotorTriaje ..> NivelSeveridad : usa
    CatalogoConsejos ..> MotorTriaje : Etiqueta
    Consulta --> NivelSeveridad : Severidad
    ResultadoTriaje --> NivelSeveridad : Severidad
    DatosSintoma --> NivelSeveridad : Severidad
    ServicioAlarmas ..> Recordatorio : evalúa
    Almacenamiento o-- Mascota
    Almacenamiento o-- Consulta
    Almacenamiento o-- Recordatorio
```

## Notas

- `DatosSintoma` y `CategoriaSintomas` son `record` (inmutables por defecto).
- `Consulta.Mascota` guarda el **nombre** como texto, no una clave foránea: el historial sobrevive
  aunque se borre la ficha de la mascota.
- `ResultadoTriaje.Puntuacion` es la suma de pesos; `Implicaciones` contiene una explicación por
  síntoma reconocido.

Volver al [índice de documentación](../INDICE.md#diagramas).
