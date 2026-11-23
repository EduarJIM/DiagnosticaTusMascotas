# Diagrama de secuencia: análisis de síntomas

Flujo completo desde que el usuario marca síntomas hasta que se guarda la consulta y se muestra
el resultado.

```mermaid
sequenceDiagram
    autonumber
    actor U as Usuario
    participant P as FrmPrincipal
    participant S as FrmSintomas
    participant A as FrmAsignarConsulta
    participant M as MotorTriaje
    participant D as Almacenamiento
    participant R as FrmResultado

    U->>P: Clic en "Análisis"
    P->>S: IrA(Analisis) / mostrar página
    S->>S: Carga MotorTriaje.Categorias
    U->>S: Marca síntomas y pulsa Continuar
    S-->>P: event Continuar(List~string~ síntomas)
    P->>A: Crear con síntomas
    U->>A: Elige mascota, escribe notas y confirma
    A-->>P: event Generar(Consulta)
    P->>M: Evaluar(síntomas)
    M-->>P: ResultadoTriaje
    P->>P: Asigna Severidad, Recomendacion e Implicaciones
    P->>D: AgregarConsulta(consulta)
    D-->>P: Id asignado
    P->>R: Mostrar resultado y consejos
    R-->>U: Nivel, implicaciones, acciones y banner
```

## Guardado en SQLite

`Almacenamiento.AgregarConsulta` ejecuta tres pasos lógicos:

1. `INSERT INTO Consultas (...)` con parámetros.
2. `INSERT INTO ConsultaSintomas (ConsultaId, Orden, Sintoma)` por cada síntoma, respetando el orden
   de selección.
3. Actualiza la lista en memoria para que el historial refleje el cambio al instante.

## Alarmas

```mermaid
sequenceDiagram
    participant T as Timer de FrmPrincipal
    participant C as ServicioAlarmas
    participant D as Almacenamiento
    participant F as FrmAlarma

    loop Cada intervalo
        T->>D: Leer Recordatorios
        T->>C: Pendientes(recordatorios, ahora, avisados)
        C-->>T: Recordatorios que coinciden con el minuto
        alt Hay pendientes
            T->>F: Mostrar aviso con sonido
            T->>T: Añadir Id a yaAvisados
        end
    end
```

Volver al [índice de documentación](../INDICE.md#diagramas).
