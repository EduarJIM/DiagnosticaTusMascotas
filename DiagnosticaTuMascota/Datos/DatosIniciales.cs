using DiagnosticaTuMascota.Core;

namespace DiagnosticaTuMascota.Datos
{
    /// <summary>
    /// Datos de ejemplo que se cargan la primera vez que se abre la aplicación.
    /// </summary>
    public static class DatosIniciales
    {
        public static List<Mascota> Mascotas() => new List<Mascota>
        {
            new Mascota
            {
                Id = 1,
                Nombre = "Max",
                Edad = "3 años",
                Raza = "Golden Retriever",
                Sexo = "Macho",
                Vacunas = "Rabia, Parvovirus, Moquillo (al día)",
                Antecedentes = "Ninguna enfermedad grave previa.",
                InfoClinica = "Alergia leve al pollo.",
                Veterinario = "Dr. Ramírez (Clínica VetSalud)"
            },
            new Mascota
            {
                Id = 2,
                Nombre = "Luna",
                Edad = "1 año",
                Raza = "Gato Siamés",
                Sexo = "Hembra",
                Vacunas = "Triple felina (al día)",
                Antecedentes = "Infección urinaria leve hace 6 meses.",
                InfoClinica = "Saludable.",
                Veterinario = "Dra. Silva (Centro Felino)"
            }
        };

        public static List<Consulta> Consultas()
        {
            var sintomas = new List<string> { "Letargo / Debilidad", "Pérdida de apetito" };
            var triaje = MotorTriaje.Evaluar(sintomas);
            return new List<Consulta>
            {
                new Consulta
                {
                    Id = 1,
                    Fecha = DateTime.Now.AddDays(-6),
                    Mascota = "Max",
                    Sintomas = sintomas,
                    Notas = "Se notó decaído después del paseo.",
                    Severidad = triaje.Severidad,
                    Recomendacion = MotorTriaje.Recomendacion(triaje.Severidad)
                }
            };
        }

        public static List<Recordatorio> Recordatorios() => new List<Recordatorio>
        {
            new Recordatorio
            {
                Id = 1,
                Titulo = "Dosis de desparasitante",
                Mascota = "Max",
                Mensaje = "Hoy corresponde la dosis mensual de desparasitante oral.",
                Prioridad = "Alta",
                FechaHora = DateTime.Today.AddHours(12)
            }
        };
    }
}
