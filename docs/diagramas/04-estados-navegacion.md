# Diagrama de estados: navegación

Las páginas se muestran dentro de `FrmPrincipal`. Cada transición la decide el formulario principal
en respuesta a eventos de la página activa.

```mermaid
stateDiagram-v2
    [*] --> Inicio

    Inicio --> Mascotas : Mis Mascotas
    Inicio --> Sintomas : Análisis
    Inicio --> Historial : Historial
    Inicio --> Recordatorios : Alertas
    Inicio --> Consejos : Consejos

    Mascotas --> EditarMascota : Nueva / Editar (modal)
    EditarMascota --> Mascotas : Guardar / Cancelar

    Sintomas --> AsignarConsulta : Continuar(síntomas)
    AsignarConsulta --> Sintomas : Volver
    AsignarConsulta --> Resultado : Generar(consulta)
    Resultado --> Consejos : Ver consejos
    Resultado --> Historial : Cerrar

    Historial --> Resultado : Abrir consulta guardada
    Recordatorios --> EditarRecordatorio : Crear / Editar (modal)
    EditarRecordatorio --> Recordatorios : Guardar / Cancelar

    Inicio --> Inicio : Cambiar tema
    Consejos --> Inicio : Inicio

    note right of Resultado
        Muestra severidad, implicaciones,
        acciones y recomendación.
    end note
```

## Secciones de la barra lateral

| Sección | Enum | Formulario principal |
|---|---|---|
| Inicio | `Seccion.Inicio` | `FrmInicio` |
| Mis Mascotas | `Seccion.Mascotas` | `FrmMascotas` |
| Análisis | `Seccion.Analisis` | `FrmSintomas` |
| Historial | `Seccion.Historial` | `FrmHistorial` |
| Alertas | `Seccion.Alertas` | `FrmRecordatorios` |
| Consejos | `Seccion.Consejos` | `FrmConsejos` |

`FrmPrincipal.IrA(Seccion)` centraliza el cambio de página y aplica la animación de entrada.
Los formularios modales (`FrmEditarMascota`, `FrmEditarRecordatorio`, `FrmAlarma`,
`FrmConfirmar`) se abren con `MostrarSobre(this)` sobre el control que los invoca.

Volver al [índice de documentación](../INDICE.md#diagramas).
