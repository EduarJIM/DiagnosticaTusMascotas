namespace DiagnosticaTuMascota.Core
{
    /// <summary>
    /// Nivel de urgencia de un diagnóstico. El valor numérico es el peso del síntoma.
    /// </summary>
    public enum NivelSeveridad
    {
        Leve = 1,
        Moderado = 2,
        Critico = 3
    }

    /// <summary>
    /// Mascota registrada por el usuario.
    /// </summary>
    public class Mascota
    {
        public long Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Edad { get; set; } = "";
        public string Raza { get; set; } = "";
        public string Sexo { get; set; } = "";
        public string Vacunas { get; set; } = "";
        public string Antecedentes { get; set; } = "";
        public string InfoClinica { get; set; } = "";
        public string Veterinario { get; set; } = "";

        public override string ToString() =>
            string.IsNullOrWhiteSpace(Raza) ? Nombre : $"{Nombre} ({Raza})";
    }

    /// <summary>
    /// Consulta de diagnóstico guardada en el historial.
    /// </summary>
    public class Consulta
    {
        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Mascota { get; set; } = "";
        public List<string> Sintomas { get; set; } = new List<string>();
        public string Notas { get; set; } = "";
        public NivelSeveridad Severidad { get; set; }
        public string Recomendacion { get; set; } = "";

        public string FechaTexto => Fecha.ToString("dd/MM/yyyy HH:mm");
        public string SeveridadTexto => MotorTriaje.Etiqueta(Severidad);
        public string SintomasTexto => string.Join(", ", Sintomas);

        public override string ToString() => $"{FechaTexto}  ·  {Mascota}  ·  {SeveridadTexto}";
    }

    /// <summary>
    /// Recordatorio con fecha y hora (vacunas, medicación, citas...).
    /// </summary>
    public class Recordatorio
    {
        public long Id { get; set; }
        public string Titulo { get; set; } = "";
        public string Mascota { get; set; } = "";
        public string Mensaje { get; set; } = "";
        public string Prioridad { get; set; } = "Media";
        public DateTime FechaHora { get; set; }

        public string FechaTexto => FechaHora.ToString("dd/MM/yyyy");
        public string HoraTexto => FechaHora.ToString("HH:mm");
    }

    /// <summary>
    /// Resultado de evaluar una lista de síntomas.
    /// </summary>
    public class ResultadoTriaje
    {
        public NivelSeveridad Severidad { get; set; } = NivelSeveridad.Leve;
        public List<string> Implicaciones { get; set; } = new List<string>();
        public int Puntuacion { get; set; }
    }
}
