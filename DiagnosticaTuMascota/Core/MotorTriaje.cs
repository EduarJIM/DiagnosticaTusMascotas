namespace DiagnosticaTuMascota.Core
{
    /// <summary>
    /// Información de un síntoma: severidad, explicación clínica y peso.
    /// </summary>
    public record DatosSintoma(NivelSeveridad Severidad, string Implicacion, int Peso);

    /// <summary>
    /// Grupo de síntomas que se muestra junto en la pantalla de análisis.
    /// </summary>
    public record CategoriaSintomas(string Nombre, string[] Sintomas)
    {
        public override string ToString() => Nombre;
    }

    /// <summary>
    /// Motor de triaje veterinario. Convierte una lista de síntomas en un nivel
    /// de urgencia (Leve, Moderado o Crítico) con sus implicaciones clínicas.
    /// Lógica tomada del proyecto original Diagnostical-Pets.
    /// </summary>
    public static class MotorTriaje
    {
        /// <summary>
        /// Categorías de síntomas en el orden en que se presentan al usuario.
        /// </summary>
        public static readonly CategoriaSintomas[] Categorias =
        {
            new CategoriaSintomas("Síntomas generales",
                new[] { "Fiebre", "Letargo / Debilidad", "Pérdida de peso", "Aumento de sed", "Temblores" }),
            new CategoriaSintomas("Respiratorios",
                new[] { "Tos persistente", "Estornudos", "Dificultad para respirar", "Secreción nasal", "Respiración ruidosa" }),
            new CategoriaSintomas("Digestivos",
                new[] { "Vómitos", "Diarrea", "Pérdida de apetito", "Dificultad al tragar", "Abdomen hinchado" }),
            new CategoriaSintomas("Neurológicos",
                new[] { "Convulsiones", "Desorientación", "Incoordinación al caminar", "Parálisis", "Cambio de conducta" })
        };

        /// <summary>
        /// Diccionario de síntomas con su severidad, implicación y peso.
        /// </summary>
        public static readonly IReadOnlyDictionary<string, DatosSintoma> Diccionario =
            new Dictionary<string, DatosSintoma>
            {
                ["Fiebre"] = new(NivelSeveridad.Moderado, "Indica un proceso infeccioso o inflamatorio activo en el cuerpo.", 2),
                ["Letargo / Debilidad"] = new(NivelSeveridad.Moderado, "Signo inespecífico de enfermedad sistémica, dolor o fiebre.", 2),
                ["Pérdida de peso"] = new(NivelSeveridad.Moderado, "Posible problema metabólico, parasitario o crónico.", 2),
                ["Aumento de sed"] = new(NivelSeveridad.Moderado, "Puede indicar problemas renales, diabetes o infecciones uterinas (piómetra).", 2),
                ["Temblores"] = new(NivelSeveridad.Critico, "Posible dolor agudo, fiebre muy alta, intoxicación o problema neurológico.", 3),

                ["Tos persistente"] = new(NivelSeveridad.Moderado, "Podría indicar problemas cardíacos, infecciones respiratorias o colapso traqueal.", 2),
                ["Estornudos"] = new(NivelSeveridad.Leve, "Suele deberse a irritantes ambientales o infecciones respiratorias altas leves.", 1),
                ["Dificultad para respirar"] = new(NivelSeveridad.Critico, "EMERGENCIA: posible fallo cardíaco, asma felino agudo o líquido en los pulmones.", 3),
                ["Secreción nasal"] = new(NivelSeveridad.Leve, "Infección del tracto respiratorio superior o presencia de un cuerpo extraño.", 1),
                ["Respiración ruidosa"] = new(NivelSeveridad.Moderado, "Obstrucción parcial de las vías respiratorias superiores.", 2),

                ["Vómitos"] = new(NivelSeveridad.Moderado, "Irritación gastrointestinal, cambios de dieta, ingestión de cuerpos extraños o fallo orgánico.", 2),
                ["Diarrea"] = new(NivelSeveridad.Moderado, "Gastroenteritis, parásitos intestinales o infecciones virales (ej. parvovirus en cachorros).", 2),
                ["Pérdida de apetito"] = new(NivelSeveridad.Leve, "Primer signo de incomodidad o enfermedad leve; requiere monitoreo.", 1),
                ["Dificultad al tragar"] = new(NivelSeveridad.Critico, "Posible cuerpo extraño en el esófago, reacción alérgica o problema neurológico.", 3),
                ["Abdomen hinchado"] = new(NivelSeveridad.Critico, "EMERGENCIA: posible torsión gástrica, hemorragia interna o peritonitis.", 3),

                ["Convulsiones"] = new(NivelSeveridad.Critico, "EMERGENCIA: actividad cerebral anormal por epilepsia, intoxicación o hipoglucemia severa.", 3),
                ["Desorientación"] = new(NivelSeveridad.Critico, "Afección del sistema nervioso central, intoxicación o evento vascular.", 3),
                ["Incoordinación al caminar"] = new(NivelSeveridad.Critico, "Síndrome vestibular, trauma espinal, hernia discal o toxinas.", 3),
                ["Parálisis"] = new(NivelSeveridad.Critico, "EMERGENCIA: lesión severa de la médula espinal, garrapatas paralizantes o tromboembolismo.", 3),
                ["Cambio de conducta"] = new(NivelSeveridad.Moderado, "A menudo asociado a dolor silencioso, senilidad o problemas neurológicos tempranos.", 2)
            };

        /// <summary>
        /// Evalúa los síntomas seleccionados y devuelve el nivel de urgencia global.
        /// </summary>
        /// <remarks>
        /// Reglas: si algún síntoma es crítico, el caso es crítico. Si hay alguno
        /// moderado o la suma de pesos llega a 4, es moderado. Vómitos + diarrea
        /// juntos se consideran críticos por el riesgo de deshidratación.
        /// </remarks>
        public static ResultadoTriaje Evaluar(IEnumerable<string> sintomas)
        {
            var lista = sintomas?.ToList() ?? new List<string>();
            var resultado = new ResultadoTriaje();
            if (lista.Count == 0)
            {
                return resultado;
            }

            int pesoMaximo = 0;
            foreach (var sintoma in lista)
            {
                if (Diccionario.TryGetValue(sintoma, out var datos))
                {
                    resultado.Puntuacion += datos.Peso;
                    pesoMaximo = Math.Max(pesoMaximo, datos.Peso);
                    resultado.Implicaciones.Add($"{sintoma}: {datos.Implicacion}");
                }
            }

            if (lista.Contains("Vómitos") && lista.Contains("Diarrea"))
            {
                pesoMaximo = 3;
                resultado.Implicaciones.Add("Vómitos + Diarrea: ALTO RIESGO de deshidratación rápida y posible infección grave (ej. parvovirus).");
            }

            if (pesoMaximo == 3)
                resultado.Severidad = NivelSeveridad.Critico;
            else if (pesoMaximo == 2 || resultado.Puntuacion >= 4)
                resultado.Severidad = NivelSeveridad.Moderado;
            else
                resultado.Severidad = NivelSeveridad.Leve;

            return resultado;
        }

        /// <summary>
        /// Texto corto del nivel: LEVE, MODERADO o CRÍTICO.
        /// </summary>
        public static string Etiqueta(NivelSeveridad nivel) => nivel switch
        {
            NivelSeveridad.Critico => "CRÍTICO",
            NivelSeveridad.Moderado => "MODERADO",
            _ => "LEVE"
        };

        /// <summary>
        /// Recomendación de una línea que se guarda en el historial.
        /// </summary>
        public static string Recomendacion(NivelSeveridad nivel) => nivel switch
        {
            NivelSeveridad.Critico => "Atención veterinaria inmediata requerida. Riesgo vital.",
            NivelSeveridad.Moderado => "Se sugiere revisión veterinaria pronta para evitar complicaciones.",
            _ => "Monitoreo en casa recomendado."
        };

        /// <summary>
        /// Explicación ampliada del nivel para la pantalla de resultados.
        /// </summary>
        public static string Descripcion(NivelSeveridad nivel) => nivel switch
        {
            NivelSeveridad.Critico => "Los síntomas representan un riesgo potencial de vida o daño orgánico severo. No intentes tratamientos caseros: acude a la clínica de emergencias más cercana.",
            NivelSeveridad.Moderado => "Tu mascota presenta signos de incomodidad o enfermedad que requieren evaluación médica para evitar complicaciones.",
            _ => "Los síntomas parecen leves o pasajeros. Mantén a tu mascota en observación durante 24-48 horas. Si persisten o empeoran, acude al veterinario."
        };

        /// <summary>
        /// Primeros auxilios y acciones sugeridas según el nivel.
        /// </summary>
        public static string[] Acciones(NivelSeveridad nivel) => nivel switch
        {
            NivelSeveridad.Critico => new[]
            {
                "Mantén la calma y contacta a tu veterinario de inmediato.",
                "Minimiza el estrés y el movimiento de la mascota.",
                "No administres medicamentos humanos bajo ninguna circunstancia.",
                "Si hay dificultad respiratoria, asegúrate de que el área esté bien ventilada."
            },
            NivelSeveridad.Moderado => new[]
            {
                "Programa una cita veterinaria para hoy o mañana.",
                "Ofrécele agua fresca, pero no la fuerces a beber o comer.",
                "Mantén a tu mascota en un lugar tranquilo y cómodo."
            },
            _ => new[]
            {
                "Observa de cerca su comportamiento durante las próximas horas.",
                "Asegúrate de que tenga acceso a agua fresca.",
                "Evita cambios bruscos en su dieta actual."
            }
        };

        /// <summary>
        /// Color asociado a cada nivel (verde, ámbar o rojo).
        /// </summary>
        public static Color ColorNivel(NivelSeveridad nivel) => nivel switch
        {
            NivelSeveridad.Critico => Color.FromArgb(239, 68, 68),
            NivelSeveridad.Moderado => Color.FromArgb(245, 158, 11),
            _ => Color.FromArgb(16, 185, 129)
        };
    }
}
