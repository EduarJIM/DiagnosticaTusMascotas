namespace DiagnosticaTuMascota.Formularios.Base
{
    /// <summary>
    /// Atajos para mostrar diálogos con el estilo de la aplicación en lugar de MessageBox.
    /// </summary>
    public static class Dialogos
    {
        /// <summary>
        /// Pregunta de confirmación. Devuelve true si el usuario acepta.
        /// </summary>
        public static bool Confirmar(Control origen, string titulo, string mensaje, string textoAccion = "Eliminar")
        {
            using (var frm = new FrmConfirmar(titulo, mensaje, textoAccion, true))
            {
                return frm.MostrarSobre(origen) == DialogResult.OK;
            }
        }

        /// <summary>
        /// Mensaje informativo con un solo botón.
        /// </summary>
        public static void Avisar(Control origen, string titulo, string mensaje)
        {
            using (var frm = new FrmConfirmar(titulo, mensaje, "Entendido", false))
            {
                frm.MostrarSobre(origen);
            }
        }
    }
}
