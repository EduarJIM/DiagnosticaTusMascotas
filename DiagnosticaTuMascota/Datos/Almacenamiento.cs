using System.Globalization;
using System.Text.Json;
using DiagnosticaTuMascota.Core;
using Microsoft.Data.Sqlite;

namespace DiagnosticaTuMascota.Datos
{
    /// <summary>
    /// Capa de acceso a datos sobre una base SQLite (%AppData%\DiagnosticaTuMascota\diagnostica.db).
    /// Mantiene una copia en memoria de cada tabla para que los formularios
    /// lean rápido, y cada cambio se escribe en la base al momento.
    /// </summary>
    public class Almacenamiento
    {
        private const string FormatoFecha = "yyyy-MM-dd HH:mm:ss";

        private const string Esquema = @"
            CREATE TABLE IF NOT EXISTS Mascotas (
                Id           INTEGER PRIMARY KEY AUTOINCREMENT,
                Nombre       TEXT NOT NULL,
                Edad         TEXT,
                Raza         TEXT,
                Sexo         TEXT,
                Vacunas      TEXT,
                Antecedentes TEXT,
                InfoClinica  TEXT,
                Veterinario  TEXT
            );

            CREATE TABLE IF NOT EXISTS Consultas (
                Id            INTEGER PRIMARY KEY AUTOINCREMENT,
                Fecha         TEXT NOT NULL,
                Mascota       TEXT NOT NULL,
                Notas         TEXT,
                Severidad     INTEGER NOT NULL,
                Recomendacion TEXT
            );

            CREATE TABLE IF NOT EXISTS ConsultaSintomas (
                ConsultaId INTEGER NOT NULL REFERENCES Consultas(Id) ON DELETE CASCADE,
                Orden      INTEGER NOT NULL,
                Sintoma    TEXT NOT NULL,
                PRIMARY KEY (ConsultaId, Orden)
            );

            CREATE TABLE IF NOT EXISTS Recordatorios (
                Id        INTEGER PRIMARY KEY AUTOINCREMENT,
                Titulo    TEXT NOT NULL,
                Mascota   TEXT,
                Mensaje   TEXT,
                Prioridad TEXT NOT NULL DEFAULT 'Media',
                FechaHora TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Preferencias (
                Clave TEXT PRIMARY KEY,
                Valor TEXT
            );";

        private readonly string _cadenaConexion;

        /// <summary>
        /// Ruta del archivo .db.
        /// </summary>
        public string RutaBaseDatos { get; }

        public List<Mascota> Mascotas { get; } = new List<Mascota>();
        public List<Consulta> Consultas { get; } = new List<Consulta>();
        public List<Recordatorio> Recordatorios { get; } = new List<Recordatorio>();

        public Almacenamiento(string rutaBaseDatos)
        {
            RutaBaseDatos = rutaBaseDatos;
            _cadenaConexion = new SqliteConnectionStringBuilder
            {
                DataSource = rutaBaseDatos,
                ForeignKeys = true
            }.ToString();
        }

        /// <summary>
        /// Carpeta donde se guardan los datos de la aplicación.
        /// </summary>
        public static string CarpetaDatos =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DiagnosticaTuMascota");

        /// <summary>
        /// Ruta por defecto de la base de datos.
        /// </summary>
        public static string RutaPorDefecto => Path.Combine(CarpetaDatos, "diagnostica.db");

        /// <summary>
        /// Crea las tablas si no existen y carga los datos en memoria.
        /// La primera vez importa el antiguo datos.json (si existe) o los datos de ejemplo.
        /// </summary>
        public void Cargar()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(RutaBaseDatos));
            bool esNueva = !File.Exists(RutaBaseDatos);

            using (var conexion = AbrirConexion())
            using (var comando = new SqliteCommand(Esquema, conexion))
            {
                comando.ExecuteNonQuery();
            }

            if (esNueva)
            {
                InsertarDatosIniciales();
            }

            RecargarMascotas();
            RecargarConsultas();
            RecargarRecordatorios();
        }

        #region Mascotas

        public void AgregarMascota(Mascota m)
        {
            const string sql = @"
                INSERT INTO Mascotas (Nombre, Edad, Raza, Sexo, Vacunas, Antecedentes, InfoClinica, Veterinario)
                VALUES ($nombre, $edad, $raza, $sexo, $vacunas, $antecedentes, $infoClinica, $veterinario);
                SELECT last_insert_rowid();";

            using (var conexion = AbrirConexion())
            using (var comando = new SqliteCommand(sql, conexion))
            {
                ParametrosMascota(comando, m);
                m.Id = (long)comando.ExecuteScalar();
            }
            Mascotas.Add(m);
        }

        public void ActualizarMascota(Mascota m)
        {
            const string sql = @"
                UPDATE Mascotas
                SET Nombre = $nombre, Edad = $edad, Raza = $raza, Sexo = $sexo, Vacunas = $vacunas,
                    Antecedentes = $antecedentes, InfoClinica = $infoClinica, Veterinario = $veterinario
                WHERE Id = $id;";

            using (var conexion = AbrirConexion())
            using (var comando = new SqliteCommand(sql, conexion))
            {
                ParametrosMascota(comando, m);
                comando.Parameters.AddWithValue("$id", m.Id);
                comando.ExecuteNonQuery();
            }
        }

        public void EliminarMascota(long id)
        {
            EjecutarPorId("DELETE FROM Mascotas WHERE Id = $id;", id);
            Mascotas.RemoveAll(m => m.Id == id);
        }

        private void RecargarMascotas()
        {
            const string sql = @"
                SELECT Id, Nombre, Edad, Raza, Sexo, Vacunas, Antecedentes, InfoClinica, Veterinario
                FROM Mascotas
                ORDER BY Nombre;";

            Mascotas.Clear();
            using (var conexion = AbrirConexion())
            using (var comando = new SqliteCommand(sql, conexion))
            using (var lector = comando.ExecuteReader())
            {
                while (lector.Read())
                {
                    Mascotas.Add(new Mascota
                    {
                        Id = lector.GetInt64(0),
                        Nombre = Texto(lector, 1),
                        Edad = Texto(lector, 2),
                        Raza = Texto(lector, 3),
                        Sexo = Texto(lector, 4),
                        Vacunas = Texto(lector, 5),
                        Antecedentes = Texto(lector, 6),
                        InfoClinica = Texto(lector, 7),
                        Veterinario = Texto(lector, 8)
                    });
                }
            }
        }

        private static void ParametrosMascota(SqliteCommand comando, Mascota m)
        {
            comando.Parameters.AddWithValue("$nombre", m.Nombre ?? "");
            comando.Parameters.AddWithValue("$edad", m.Edad ?? "");
            comando.Parameters.AddWithValue("$raza", m.Raza ?? "");
            comando.Parameters.AddWithValue("$sexo", m.Sexo ?? "");
            comando.Parameters.AddWithValue("$vacunas", m.Vacunas ?? "");
            comando.Parameters.AddWithValue("$antecedentes", m.Antecedentes ?? "");
            comando.Parameters.AddWithValue("$infoClinica", m.InfoClinica ?? "");
            comando.Parameters.AddWithValue("$veterinario", m.Veterinario ?? "");
        }

        #endregion

        #region Consultas

        /// <summary>
        /// Guarda una consulta y sus síntomas en una sola transacción.
        /// </summary>
        public void AgregarConsulta(Consulta c)
        {
            const string sqlConsulta = @"
                INSERT INTO Consultas (Fecha, Mascota, Notas, Severidad, Recomendacion)
                VALUES ($fecha, $mascota, $notas, $severidad, $recomendacion);
                SELECT last_insert_rowid();";

            const string sqlSintoma = @"
                INSERT INTO ConsultaSintomas (ConsultaId, Orden, Sintoma)
                VALUES ($consultaId, $orden, $sintoma);";

            using (var conexion = AbrirConexion())
            using (var transaccion = conexion.BeginTransaction())
            {
                using (var comando = new SqliteCommand(sqlConsulta, conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("$fecha", c.Fecha.ToString(FormatoFecha, CultureInfo.InvariantCulture));
                    comando.Parameters.AddWithValue("$mascota", c.Mascota ?? "");
                    comando.Parameters.AddWithValue("$notas", c.Notas ?? "");
                    comando.Parameters.AddWithValue("$severidad", (int)c.Severidad);
                    comando.Parameters.AddWithValue("$recomendacion", c.Recomendacion ?? "");
                    c.Id = (long)comando.ExecuteScalar();
                }

                for (int i = 0; i < c.Sintomas.Count; i++)
                {
                    using (var comando = new SqliteCommand(sqlSintoma, conexion, transaccion))
                    {
                        comando.Parameters.AddWithValue("$consultaId", c.Id);
                        comando.Parameters.AddWithValue("$orden", i);
                        comando.Parameters.AddWithValue("$sintoma", c.Sintomas[i]);
                        comando.ExecuteNonQuery();
                    }
                }

                transaccion.Commit();
            }
            Consultas.Insert(0, c);
        }

        /// <summary>
        /// Elimina una consulta. Sus síntomas se borran solos (ON DELETE CASCADE).
        /// </summary>
        public void EliminarConsulta(long id)
        {
            EjecutarPorId("DELETE FROM Consultas WHERE Id = $id;", id);
            Consultas.RemoveAll(c => c.Id == id);
        }

        /// <summary>
        /// Busca consultas en la base con filtros opcionales.
        /// </summary>
        /// <param name="mascota">Nombre exacto de la mascota, o null para todas.</param>
        /// <param name="nivel">Nivel de urgencia, o null para todos.</param>
        public List<Consulta> BuscarConsultas(string mascota, NivelSeveridad? nivel)
        {
            var sql = "SELECT Id, Fecha, Mascota, Notas, Severidad, Recomendacion FROM Consultas WHERE 1 = 1";
            if (mascota != null)
            {
                sql += " AND Mascota = $mascota";
            }
            if (nivel != null)
            {
                sql += " AND Severidad = $severidad";
            }
            sql += " ORDER BY Fecha DESC;";

            using (var conexion = AbrirConexion())
            using (var comando = new SqliteCommand(sql, conexion))
            {
                if (mascota != null)
                {
                    comando.Parameters.AddWithValue("$mascota", mascota);
                }
                if (nivel != null)
                {
                    comando.Parameters.AddWithValue("$severidad", (int)nivel.Value);
                }
                return LeerConsultas(conexion, comando);
            }
        }

        private void RecargarConsultas()
        {
            Consultas.Clear();
            Consultas.AddRange(BuscarConsultas(null, null));
        }

        private static List<Consulta> LeerConsultas(SqliteConnection conexion, SqliteCommand comando)
        {
            var consultas = new List<Consulta>();
            using (var lector = comando.ExecuteReader())
            {
                while (lector.Read())
                {
                    consultas.Add(new Consulta
                    {
                        Id = lector.GetInt64(0),
                        Fecha = LeerFecha(lector, 1),
                        Mascota = Texto(lector, 2),
                        Notas = Texto(lector, 3),
                        Severidad = (NivelSeveridad)lector.GetInt32(4),
                        Recomendacion = Texto(lector, 5)
                    });
                }
            }

            var porId = consultas.ToDictionary(c => c.Id);
            const string sqlSintomas = "SELECT ConsultaId, Sintoma FROM ConsultaSintomas ORDER BY ConsultaId, Orden;";
            using (var comandoSintomas = new SqliteCommand(sqlSintomas, conexion))
            using (var lector = comandoSintomas.ExecuteReader())
            {
                while (lector.Read())
                {
                    if (porId.TryGetValue(lector.GetInt64(0), out var consulta))
                    {
                        consulta.Sintomas.Add(lector.GetString(1));
                    }
                }
            }

            return consultas;
        }

        #endregion

        #region Recordatorios

        public void AgregarRecordatorio(Recordatorio r)
        {
            const string sql = @"
                INSERT INTO Recordatorios (Titulo, Mascota, Mensaje, Prioridad, FechaHora)
                VALUES ($titulo, $mascota, $mensaje, $prioridad, $fechaHora);
                SELECT last_insert_rowid();";

            using (var conexion = AbrirConexion())
            using (var comando = new SqliteCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("$titulo", r.Titulo ?? "");
                comando.Parameters.AddWithValue("$mascota", r.Mascota ?? "");
                comando.Parameters.AddWithValue("$mensaje", r.Mensaje ?? "");
                comando.Parameters.AddWithValue("$prioridad", r.Prioridad ?? "Media");
                comando.Parameters.AddWithValue("$fechaHora", r.FechaHora.ToString(FormatoFecha, CultureInfo.InvariantCulture));
                r.Id = (long)comando.ExecuteScalar();
            }
            Recordatorios.Add(r);
        }

        public void EliminarRecordatorio(long id)
        {
            EjecutarPorId("DELETE FROM Recordatorios WHERE Id = $id;", id);
            Recordatorios.RemoveAll(r => r.Id == id);
        }

        private void RecargarRecordatorios()
        {
            const string sql = @"
                SELECT Id, Titulo, Mascota, Mensaje, Prioridad, FechaHora
                FROM Recordatorios
                ORDER BY FechaHora;";

            Recordatorios.Clear();
            using (var conexion = AbrirConexion())
            using (var comando = new SqliteCommand(sql, conexion))
            using (var lector = comando.ExecuteReader())
            {
                while (lector.Read())
                {
                    Recordatorios.Add(new Recordatorio
                    {
                        Id = lector.GetInt64(0),
                        Titulo = Texto(lector, 1),
                        Mascota = Texto(lector, 2),
                        Mensaje = Texto(lector, 3),
                        Prioridad = Texto(lector, 4),
                        FechaHora = LeerFecha(lector, 5)
                    });
                }
            }
        }

        #endregion

        #region Preferencias

        /// <summary>
        /// Lee una preferencia del usuario (por ejemplo, el modo claro/oscuro).
        /// </summary>
        public string LeerPreferencia(string clave, string porDefecto)
        {
            using (var conexion = AbrirConexion())
            using (var comando = new SqliteCommand("SELECT Valor FROM Preferencias WHERE Clave = $clave;", conexion))
            {
                comando.Parameters.AddWithValue("$clave", clave);
                return comando.ExecuteScalar() as string ?? porDefecto;
            }
        }

        public void GuardarPreferencia(string clave, string valor)
        {
            const string sql = @"
                INSERT INTO Preferencias (Clave, Valor) VALUES ($clave, $valor)
                ON CONFLICT(Clave) DO UPDATE SET Valor = excluded.Valor;";

            using (var conexion = AbrirConexion())
            using (var comando = new SqliteCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("$clave", clave);
                comando.Parameters.AddWithValue("$valor", valor ?? "");
                comando.ExecuteNonQuery();
            }
        }

        #endregion

        #region Utilidades

        private SqliteConnection AbrirConexion()
        {
            var conexion = new SqliteConnection(_cadenaConexion);
            conexion.Open();
            return conexion;
        }

        private void EjecutarPorId(string sql, long id)
        {
            using (var conexion = AbrirConexion())
            using (var comando = new SqliteCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("$id", id);
                comando.ExecuteNonQuery();
            }
        }

        private static string Texto(SqliteDataReader lector, int columna) =>
            lector.IsDBNull(columna) ? "" : lector.GetString(columna);

        private static DateTime LeerFecha(SqliteDataReader lector, int columna) =>
            DateTime.ParseExact(lector.GetString(columna), FormatoFecha, CultureInfo.InvariantCulture);

        /// <summary>
        /// Llena una base recién creada: con el datos.json de la versión anterior
        /// si existe, o con los datos de ejemplo.
        /// </summary>
        private void InsertarDatosIniciales()
        {
            var rutaJson = Path.Combine(CarpetaDatos, "datos.json");
            var anterior = LeerJsonAnterior(rutaJson);

            var mascotas = anterior?.Mascotas ?? DatosIniciales.Mascotas();
            var consultas = anterior?.Consultas ?? DatosIniciales.Consultas();
            var recordatorios = anterior?.Recordatorios ?? DatosIniciales.Recordatorios();

            mascotas.ForEach(AgregarMascota);
            consultas.OrderBy(c => c.Fecha).ToList().ForEach(AgregarConsulta);
            recordatorios.ForEach(AgregarRecordatorio);
        }

        private static ArchivoJsonAnterior LeerJsonAnterior(string ruta)
        {
            if (!File.Exists(ruta))
            {
                return null;
            }

            try
            {
                return JsonSerializer.Deserialize<ArchivoJsonAnterior>(File.ReadAllText(ruta));
            }
            catch (JsonException)
            {
                return null;
            }
        }

        private class ArchivoJsonAnterior
        {
            public List<Mascota> Mascotas { get; set; }
            public List<Consulta> Consultas { get; set; }
            public List<Recordatorio> Recordatorios { get; set; }
        }

        #endregion
    }
}
