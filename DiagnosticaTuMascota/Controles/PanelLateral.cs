using System.ComponentModel;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Fondo de la barra lateral (bg-sidebar border-r shadow-xl) con la línea que
    /// separa las opciones inferiores.
    /// </summary>
    public class PanelLateral : Panel
    {
        public PanelLateral()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            base.BackColor = Tema.Lateral;
            Tema.Cambiado += Tema_Cambiado;
        }

        /// <summary>
        /// Distancia desde abajo a la que se dibuja la línea separadora (0 = sin línea).
        /// </summary>
        [Category("Apariencia"), DefaultValue(0)]
        public int SeparadorInferior { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color BackColor
        {
            get => base.BackColor;
            set => base.BackColor = value;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var lapiz = new Pen(Tema.BordeSobre(Tema.Lateral)))
            {
                e.Graphics.DrawLine(lapiz, Width - 1, 0, Width - 1, Height);
                if (SeparadorInferior > 0)
                {
                    e.Graphics.DrawLine(lapiz, 0, Height - SeparadorInferior, Width - 1, Height - SeparadorInferior);
                }
            }
        }

        private void Tema_Cambiado(object sender, EventArgs e)
        {
            base.BackColor = Tema.Lateral;
            Invalidate(true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Tema.Cambiado -= Tema_Cambiado;
            }
            base.Dispose(disposing);
        }
    }
}
