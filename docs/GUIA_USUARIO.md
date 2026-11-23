# Guía de usuario

## Instalación

Necesitas **Windows 10/11** y el SDK de **.NET 10**. No hay que instalar SQLite: viene como
paquete NuGet y la base se crea sola.

```powershell
git clone https://github.com/EduarJIM/DiagnosticaTusMascotas.git
cd DiagnosticaTusMascotas
dotnet build DiagnosticaTuMascota.sln -c Release
dotnet run --project DiagnosticaTuMascota/DiagnosticaTuMascota.csproj
```

Con Visual Studio, abre `DiagnosticaTuMascota.sln` y pulsa **F5**.

## Primera ejecución

Se crea `%AppData%\DiagnosticaTuMascota\diagnostica.db` con:

- **Mascotas**, **Consultas** y **Recordatorios** de ejemplo, o los datos importados de un
  `datos.json` de una versión anterior si existe.
- La preferencia de tema en `Preferencias`.

Puedes ver o editar la base con [DB Browser for SQLite](https://sqlitebrowser.org/).

## Recorrido por la interfaz

La barra lateral tiene seis secciones.

### 1. Inicio

Pantalla de bienvenida con accesos rápidos, ilustración animada y el aviso médico. El interruptor
sol/luna cambia entre tema oscuro (por defecto) y claro; la elección se guarda.

### 2. Mis Mascotas

Muestra una tarjeta por mascota. El botón **Nueva** abre `FrmEditarMascota` como modal:

| Campo | Uso |
|---|---|
| Nombre | Obligatorio; se usa como texto en consultas y recordatorios |
| Edad, Raza, Sexo | Datos básicos |
| Vacunas | Calendario o dosis aplicadas |
| Antecedentes | Enfermedades previas, alergias |
| Info clínica | Notas adicionales |
| Veterinario | Nombre y contacto |

El modal también muestra el **historial de consultas** de esa mascota.

### 3. Análisis

Paso 1 — `FrmSintomas`: marca los síntomas en cuatro categorías (generales, respiratorios,
digestivos y neurológicos) y pulsa **Continuar**.

Paso 2 — `FrmAsignarConsulta`: elige la mascota (o deja el nombre libre) y escribe notas.
**Generar** calcula el resultado.

Paso 3 — `FrmResultado`: muestra el nivel (`LEVE`, `MODERADO`, `CRÍTICO`), una barra de progreso,
la implicación de cada síntoma, las acciones sugeridas y la recomendación. Desde aquí puedes abrir
**Consejos**. La consulta ya quedó guardada en el historial.

> Si aparece `CRÍTICO`, acude a una clínica de urgencias. No esperes a ver el resultado de esta app
> para actuar.

### 4. Historial

Lista de consultas guardadas con filtros por **nombre de mascota** y **nivel de severidad**.
Al abrir una consulta se muestra el mismo `FrmResultado`, con los síntomas en el orden original.

### 5. Alertas

Cada recordatorio tiene título, mascota, mensaje, prioridad (Alta/Media/Baja) y fecha/hora.
El modal `FrmEditarRecordatorio` se llama **Crear Alerta**.

Mientras la aplicación esté abierta, un `Timer` comprueba los recordatorios cada intervalo;
cuando llega la hora se muestra `FrmAlarma` con sonido. Cada alerta avisa una sola vez por minuto
(gracias al conjunto `yaAvisados`).

### 6. Consejos

Contenido estático y generado: casos por nivel de urgencia, temas de cuidado (alimentación,
hidratación, vacunas, ejercicio, higiene y botiquín) y un diccionario con los 20 síntomas y su
explicación clínica.

## Cambiar de tema

Clic en el interruptor sol/luna de la barra lateral. El cambio se aplica de inmediato a todas las
páginas y se persiste en la tabla `Preferencias` con la clave `tema`.

## Respaldar tus datos

Cierra la aplicación y copia:

```
%AppData%\DiagnosticaTuMascota\diagnostica.db
```

Para restaurar, pega el archivo en la misma ruta y vuelve a abrir la app.

## Preguntas frecuentes

Consulta [FAQ.md](FAQ.md) si algo no funciona como esperas.
