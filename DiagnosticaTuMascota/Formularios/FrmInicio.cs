using DiagnosticaTuMascota.Formularios.Base;

namespace DiagnosticaTuMascota.Formularios
{
    /// <summary>
    /// Página de inicio (Dashboard): bienvenida, accesos rápidos, aviso médico
    /// e ilustración animada.
    /// </summary>
    public partial class FrmInicio : PaginaBase
    {
        /// <summary>
        /// Se lanza al pulsar una tarjeta de acceso.
        /// </summary>
        public event EventHandler<Seccion> SeccionSolicitada;

        public FrmInicio()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Las tarjetas ya tienen su propia animación de entrada escalonada.
        /// </summary>
        public override bool TransicionSuave => false;

        private void tarjetaAnalisis_Click(object sender, EventArgs e) => SeccionSolicitada?.Invoke(this, Seccion.Diagnostico);

        private void tarjetaMascotas_Click(object sender, EventArgs e) => SeccionSolicitada?.Invoke(this, Seccion.Mascotas);

        private void tarjetaHistorial_Click(object sender, EventArgs e) => SeccionSolicitada?.Invoke(this, Seccion.Historial);

        /// <summary>
        /// Reparte el ancho: la tarjeta grande ocupa todo y las dos pequeñas, la mitad cada una.
        /// </summary>
        private void pnlIzquierda_Resize(object sender, EventArgs e)
        {
            int margen = pnlIzquierda.ClientSize.Width < 520 ? 32 : 44;
            int ancho = Math.Max(300, pnlIzquierda.ClientSize.Width - margen * 2);
            bool compacto = pnlIzquierda.ClientSize.Height < 760;

            encabezado.Bounds = new Rectangle(margen, compacto ? 30 : 44, ancho, 90);
            tarjetaAnalisis.Bounds = new Rectangle(margen - 10, encabezado.Bottom + 8, ancho + 20, compacto ? 178 : 186);

            int mitad = (ancho + 20) / 2;
            int altoMitad = compacto ? 178 : 196;
            tarjetaMascotas.Bounds = new Rectangle(margen - 10, tarjetaAnalisis.Bottom - 2, mitad, altoMitad);
            tarjetaHistorial.Bounds = new Rectangle(tarjetaMascotas.Right, tarjetaMascotas.Top, mitad, altoMitad);

            int espacio = pnlIzquierda.ClientSize.Height - tarjetaMascotas.Bottom - 34;
            aviso.Bounds = new Rectangle(margen, tarjetaMascotas.Bottom + 10, ancho, Math.Max(136, Math.Min(190, espacio)));
        }
    }
}
