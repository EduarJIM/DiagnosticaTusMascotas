using DiagnosticaTuMascota.Core;
using DiagnosticaTuMascota.Estilos;
using DiagnosticaTuMascota.Formularios.Base;

namespace DiagnosticaTuMascota.Formularios
{
    /// <summary>
    /// Aviso que aparece cuando llega la hora de un recordatorio.
    /// </summary>
    public partial class FrmAlarma : ModalBase
    {
        public FrmAlarma() : this(new Recordatorio { Titulo = "Recordatorio", Mascota = "Max" })
        {
        }

        public FrmAlarma(Recordatorio recordatorio)
        {
            InitializeComponent();
            contenido.Titulo = recordatorio.Titulo;
            contenido.Mascota = string.IsNullOrWhiteSpace(recordatorio.Mascota) ? "tu mascota" : recordatorio.Mascota;
            contenido.Mensaje = recordatorio.Mensaje;
            btnCerrarModal.Visible = false;
        }

        private void btnHecho_Click(object sender, EventArgs e) => DialogResult = DialogResult.OK;

        /// <summary>
        /// Borde verde de 2 px (border-2 border-primary/50).
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            using (var lapiz = new Pen(Dibujo.Sobre(Tema.Tarjeta, Tema.Primario, 0.5), 3))
            {
                e.Graphics.DrawRectangle(lapiz, 1, 1, Width - 3, Height - 3);
            }
        }
    }
}
