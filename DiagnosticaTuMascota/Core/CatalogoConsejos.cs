using System.Text;

namespace DiagnosticaTuMascota.Core
{
    /// <summary>
    /// Consejos de cuidado agrupados por tema para el formulario de consejos.
    /// </summary>
    public static class CatalogoConsejos
    {
        public const string TemaDiccionario = "Diccionario de síntomas";

        /// <summary>
        /// Nombre del tema que corresponde a cada nivel de urgencia.
        /// </summary>
        public static string TemaDeNivel(NivelSeveridad nivel) => $"Caso {MotorTriaje.Etiqueta(nivel)}";

        /// <summary>
        /// Lista de temas en el orden en que se muestran.
        /// </summary>
        public static IEnumerable<string> Temas()
        {
            yield return TemaDeNivel(NivelSeveridad.Leve);
            yield return TemaDeNivel(NivelSeveridad.Moderado);
            yield return TemaDeNivel(NivelSeveridad.Critico);
            foreach (var tema in Generales.Keys)
            {
                yield return tema;
            }
            yield return TemaDiccionario;
        }

        /// <summary>
        /// Devuelve el texto de un tema listo para mostrarse.
        /// </summary>
        public static string Contenido(string tema)
        {
            foreach (NivelSeveridad nivel in Enum.GetValues(typeof(NivelSeveridad)))
            {
                if (tema == TemaDeNivel(nivel))
                {
                    return ContenidoNivel(nivel);
                }
            }

            if (tema == TemaDiccionario)
            {
                return ContenidoDiccionario();
            }

            return Generales.TryGetValue(tema, out var puntos) ? Viñetas(puntos) : "";
        }

        private static string ContenidoNivel(NivelSeveridad nivel)
        {
            var texto = new StringBuilder();
            texto.AppendLine(MotorTriaje.Descripcion(nivel));
            texto.AppendLine();
            texto.AppendLine("Qué hacer:");
            texto.Append(Viñetas(MotorTriaje.Acciones(nivel)));
            texto.AppendLine();
            texto.AppendLine("Síntomas de este nivel:");
            var sintomas = MotorTriaje.Diccionario.Where(s => s.Value.Severidad == nivel).Select(s => s.Key);
            texto.Append(Viñetas(sintomas));
            return texto.ToString();
        }

        private static string ContenidoDiccionario()
        {
            var texto = new StringBuilder();
            foreach (var categoria in MotorTriaje.Categorias)
            {
                texto.AppendLine(categoria.Nombre.ToUpper());
                foreach (var sintoma in categoria.Sintomas)
                {
                    var datos = MotorTriaje.Diccionario[sintoma];
                    texto.AppendLine($"  • {sintoma} [{MotorTriaje.Etiqueta(datos.Severidad)}]");
                    texto.AppendLine($"    {datos.Implicacion}");
                }
                texto.AppendLine();
            }
            return texto.ToString();
        }

        private static string Viñetas(IEnumerable<string> puntos) =>
            string.Concat(puntos.Select(p => $"  • {p}{Environment.NewLine}"));

        private static readonly Dictionary<string, string[]> Generales = new Dictionary<string, string[]>
        {
            ["Alimentación"] = new[]
            {
                "Usa un alimento adecuado a la especie, edad y tamaño de tu mascota.",
                "Haz los cambios de dieta de forma gradual, durante 7 a 10 días.",
                "Evita chocolate, uvas, cebolla, ajo, xilitol y huesos cocidos.",
                "Respeta horarios fijos y controla las porciones para prevenir la obesidad."
            },
            ["Hidratación"] = new[]
            {
                "Agua fresca y limpia siempre disponible; cámbiala al menos una vez al día.",
                "Revisa la hidratación: encías húmedas y piel que vuelve rápido a su sitio.",
                "En días calurosos ofrece sombra y evita el ejercicio en horas de calor.",
                "Si bebe mucho más de lo normal, coméntalo con tu veterinario."
            },
            ["Vacunas y desparasitación"] = new[]
            {
                "Sigue el calendario de vacunas que indique tu veterinario.",
                "Desparasitación interna cada 3 meses en adultos (o según indicación).",
                "Protección contra pulgas y garrapatas de forma regular.",
                "Usa la sección Recordatorios para no olvidar ninguna dosis."
            },
            ["Ejercicio y bienestar"] = new[]
            {
                "Paseos diarios adaptados a la raza y a la edad.",
                "Juego y estimulación mental: juguetes, olfato, rutinas nuevas.",
                "Un cambio repentino de energía o conducta puede indicar dolor.",
                "Los gatos necesitan rascadores y zonas elevadas para sentirse seguros."
            },
            ["Higiene"] = new[]
            {
                "Cepillado frecuente para evitar nudos y detectar bultos o parásitos.",
                "Limpieza dental regular para prevenir sarro y mal aliento.",
                "Revisa y limpia oídos; el mal olor o el rascado constante requieren consulta.",
                "Mantén limpios cama, comederos y arenero."
            },
            ["Botiquín básico"] = new[]
            {
                "Gasas, vendas y cinta adhesiva.",
                "Suero fisiológico para lavar heridas u ojos.",
                "Termómetro digital (temperatura normal: 38 a 39 °C en perros y gatos).",
                "Teléfono del veterinario y de una clínica de urgencias 24 horas.",
                "Nunca uses medicamentos de personas sin indicación veterinaria."
            }
        };
    }
}
