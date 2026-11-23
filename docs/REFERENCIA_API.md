# Referencia de funciones

Inventario de los miembros públicos más relevantes, agrupados por archivo. Es una guía rápida
para navegar el código; la firma exacta está siempre en el archivo fuente.

## `Core/MotorTriaje.cs`

| Miembro | Tipo | Descripción |
|---|---|---|
| `Categorias` | `CategoriaSintomas[]` | 4 categorías en el orden de la interfaz: generales, respiratorios, digestivos y neurológicos. |
| `Diccionario` | `IReadOnlyDictionary<string, DatosSintoma>` | Los 20 síntomas con severidad, implicación y peso. |
| `Evaluar(IEnumerable<string>)` | `ResultadoTriaje` | Aplica las reglas de triaje y devuelve nivel, puntuación e implicaciones. |
| `Etiqueta(NivelSeveridad)` | `string` | `LEVE`, `MODERADO` o `CRÍTICO`. |
| `Recomendacion(NivelSeveridad)` | `string` | Texto breve guardado en el historial. |
| `Descripcion(NivelSeveridad)` | `string` | Explicación ampliada para la pantalla de resultado. |
| `Acciones(NivelSeveridad)` | `string[]` | Primeros auxilios sugeridos. |
| `ColorNivel(NivelSeveridad)` | `Color` | Verde, ámbar o rojo (ARGB). |

Registros relacionados: `DatosSintoma(Severidad, Implicacion, Peso)` y
`CategoriaSintomas(Nombre, Sintomas)`.

## `Core/ServicioAlarmas.cs`

| Miembro | Descripción |
|---|---|
| `DebeSonar(Recordatorio, DateTime)` | `true` cuando fecha, hora y **minuto** coinciden exactamente. |
| `Pendientes(recordatorios, ahora, yaAvisados)` | Filtra los recordatorios que deben sonar y aún no se han avisado. |

## `Core/CatalogoConsejos.cs`

| Miembro | Descripción |
|---|---|
| `TemaDiccionario` | Constante `"Diccionario de síntomas"`. |
| `TemaDeNivel(NivelSeveridad)` | Devuelve `Caso LEVE`, `Caso MODERADO` o `Caso CRÍTICO`. |
| `Temas()` | Enumera los 3 casos, los temas generales y el diccionario. |
| `Contenido(string tema)` | Texto formateado con viñetas para el tema solicitado; `""` si no existe. |

## `Core/Modelos.cs`

| Tipo | Miembros destacados |
|---|---|
| `NivelSeveridad` | `Leve = 1`, `Moderado = 2`, `Critico = 3`. |
| `Mascota` | `Id`, `Nombre`, `Edad`, `Raza`, `Sexo`, `Vacunas`, `Antecedentes`, `InfoClinica`, `Veterinario`, `ToString()`. |
| `Consulta` | `Id`, `Fecha`, `Mascota`, `Sintomas`, `Notas`, `Severidad`, `Recomendacion`, `FechaTexto`, `SeveridadTexto`, `SintomasTexto`. |
| `Recordatorio` | `Id`, `Titulo`, `Mascota`, `Mensaje`, `Prioridad`, `FechaHora`, `FechaTexto`, `HoraTexto`. |
| `ResultadoTriaje` | `Severidad`, `Implicaciones`, `Puntuacion`. |

## `Datos/Almacenamiento.cs`

| Miembro | Descripción |
|---|---|
| `CarpetaDatos` (static) | `%AppData%\DiagnosticaTuMascota`. |
| `RutaPorDefecto` (static) | `<CarpetaDatos>\diagnostica.db`. |
| `RutaBaseDatos` | Ruta efectiva de la instancia. |
| `Mascotas`, `Consultas`, `Recordatorios` | Copias en memoria de las tablas. |
| `Cargar()` | Crea el esquema, importa datos iniciales si la base es nueva y recarga las listas. |
| `AgregarMascota(m)` / `ActualizarMascota(m)` | INSERT / UPDATE con parámetros; asigna `m.Id`. |
| `EliminarMascota(id)` | DELETE y eliminación de la lista en memoria. |
| `AgregarConsulta(c)` | INSERT de la consulta y de sus síntomas en orden. |
| `EliminarConsulta(id)` | DELETE; los síntomas se borran por `ON DELETE CASCADE`. |
| `BuscarConsultas(mascota, nivel)` | Filtro en base por texto y severidad, ordenado por fecha descendente. |
| `AgregarRecordatorio(r)` / `EliminarRecordatorio(id)` | Alta y baja de alertas. |
| `LeerPreferencia(clave, porDefecto)` / `GuardarPreferencia(clave, valor)` | Persistencia de preferencias (tema claro/oscuro). |

`DatosIniciales` expone `Mascotas()`, `Consultas()` y `Recordatorios()` con los ejemplos de la
primera ejecución.

## `Estilos/Tema.cs`

| Miembro | Descripción |
|---|---|
| `CambiarModo(bool oscuro)` | Alterna la paleta y guarda la preferencia. |
| `BordeSobre(Color)` | Calcula el color de borde según el fondo. |
| `Fuente`, `FuenteSemibold`, `FuenteNegra`, `FuenteIconos` | Fábricas de `Font` (Segoe UI / fuente de iconos). |
| `Aplicar(Control raiz)` | Aplica colores y fuentes a todo el árbol de controles. |
| `BarrasOscuras(Control)` / `BarraTitulo(Form)` / `EsquinasRedondeadas(Form)` | Ajustes visuales del contenedor. |

## `Estilos/Dibujo.cs`

`Preparar(Graphics)` activa antialiasing; `Redondeado`, `RellenarRedondeado`, `BordeRedondeado`,
`DegradadoRedondeado`, `Sombra` y `Brillo` dibujan las formas; `Mezclar`, `Alfa`, `Sobre` y `Lerp`
operan colores; `Texto` y `Medir` gestionan la tipografía.

## `Estilos/Iconos.cs`

`Dibujar(Graphics, icono, rect, color, tamaño)` pinta un glifo de la fuente de iconos de Windows;
`DibujarHuella` y `DibujarDestello` son ilustraciones vectoriales propias.

## `Estilos/Animador.cs`

| Miembro | Descripción |
|---|---|
| `Lineal`, `SalidaCubica`, `EntradaSalidaSeno`, `Resorte`, `Onda` | Curvas de interpolación. |
| `Animar(Control, ms, curva, paso)` | Animación de duración definida. |
| `Bucle(Control, paso)` | Animación infinita (logo, anillos, destello). |
| `Animacion.Detener()` / `Activa` | Control del ciclo de vida. |
| `ValorAnimado.IrA(destino, curva)` / `Fijar(valor)` | Transiciones suaves de un valor (barras, tarjetas). |

## `Formularios` y `Controles`

| Miembro | Descripción |
|---|---|
| `FrmPrincipal.IrA(Seccion)` | Cambia la página activa y aplica la transición. |
| `PaginaBase` | Base de las páginas embebidas (`TopLevel = false`). |
| `ModalBase.MostrarSobre(Control)` / `Ocultar()` | Apertura sobre fondo oscurecido y cierre. |
| `Dialogos.Confirmar(...)` / `Dialogos.Avisar(...)` | Confirmaciones y avisos con estilo propio. |
| `Notificacion.Exito(mensaje)` / `Notificacion.Error(mensaje)` | Disparan un *toast* arriba a la derecha. |
| `CapaTransicion.Reproducir(Bitmap)` | Animación de entrada de página. |
| `TarjetaLista.AltoNecesario()` | Calcula la altura de una tarjeta según su contenido. |

## Convenciones

- Los métodos públicos llevan documentación XML `/// <summary>` en el código fuente.
- `Nullable` está desactivado en el `.csproj`; los textos se inicializan a `""` para evitar nulos.
- `Core` no debe añadir referencias a `System.Windows.Forms`.

Ver también [arquitectura](ARQUITECTURA.md) y [reglas de triaje](TRIAJE.md).
