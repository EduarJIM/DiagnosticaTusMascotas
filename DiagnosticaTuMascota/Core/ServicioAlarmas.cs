namespace DiagnosticaTuMascota.Core
{
    /// <summary>
    /// Decide qué recordatorios deben avisar en un momento dado.
    /// </summary>
    public static class ServicioAlarmas
    {
        /// <summary>
        /// Indica si el recordatorio coincide con el minuto indicado.
        /// </summary>
        public static bool DebeSonar(Recordatorio recordatorio, DateTime ahora)
        {
            if (recordatorio == null)
            {
                return false;
            }

            var r = recordatorio.FechaHora;
            return r.Date == ahora.Date && r.Hour == ahora.Hour && r.Minute == ahora.Minute;
        }

        /// <summary>
        /// Devuelve los recordatorios pendientes de avisar que coinciden con este minuto.
        /// </summary>
        public static List<Recordatorio> Pendientes(IEnumerable<Recordatorio> recordatorios, DateTime ahora, ISet<long> yaAvisados)
        {
            return recordatorios
                .Where(r => DebeSonar(r, ahora) && !yaAvisados.Contains(r.Id))
                .ToList();
        }
    }
}
