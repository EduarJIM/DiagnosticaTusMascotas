using System.ComponentModel;
using System.Drawing.Imaging;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Base de los controles dibujados a mano. Se redibuja al cambiar el tema y
    /// puede aparecer con una animación de entrada (aparece y sube 20 px, igual que
    /// initial={{ opacity: 0, y: 20 }} en la web).
    /// </summary>
    [ToolboxItem(false)]
    public abstract class ControlPintado : Control
    {
        private bool _entradaIniciada;

        protected ControlPintado()
        {
            SetStyle(ControlStyles.UserPaint
                     | ControlStyles.AllPaintingInWmPaint
                     | ControlStyles.OptimizedDoubleBuffer
                     | ControlStyles.ResizeRedraw
                     | ControlStyles.SupportsTransparentBackColor, true);
            Tema.Cambiado += Tema_Cambiado;
        }

        /// <summary>
        /// Milisegundos de espera antes de la animación de entrada. -1 = sin animación.
        /// </summary>
        [Category("Animación"), DefaultValue(-1)]
        [Description("Retraso de la animación de entrada en milisegundos. -1 desactiva la animación.")]
        public int RetrasoEntrada { get; set; } = -1;

        /// <summary>
        /// Progreso de la entrada: 0 = invisible, 1 = en su sitio.
        /// </summary>
        protected double Entrada { get; private set; } = 1;

        /// <summary>
        /// Dibuja el contenido del control. El fondo del padre ya está pintado.
        /// </summary>
        protected abstract void Pintar(Graphics g);

        /// <summary>
        /// Se llama cuando cambia el modo claro/oscuro.
        /// </summary>
        protected virtual void AlCambiarTema()
        {
        }

        /// <summary>
        /// Color de fondo real del contenedor (sube por los padres si son transparentes).
        /// </summary>
        protected Color FondoPadre
        {
            get
            {
                var padre = Parent;
                while (padre != null && padre.BackColor.A < 255)
                {
                    padre = padre.Parent;
                }
                return padre?.BackColor ?? Tema.Fondo;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(FondoPadre);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Entrada >= 1 || DesignMode)
            {
                Dibujo.Preparar(e.Graphics);
                Pintar(e.Graphics);
                return;
            }

            if (Entrada <= 0 || Width <= 0 || Height <= 0)
            {
                return;
            }

            using (var capa = new Bitmap(Width, Height))
            {
                using (var g = Graphics.FromImage(capa))
                {
                    Dibujo.Preparar(g);
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                    Pintar(g);
                }

                var matriz = new ColorMatrix { Matrix33 = (float)Entrada };
                using (var atributos = new ImageAttributes())
                {
                    atributos.SetColorMatrix(matriz);
                    int desplazamiento = (int)((1 - Entrada) * 20);
                    e.Graphics.DrawImage(capa, new Rectangle(0, desplazamiento, Width, Height),
                        0, 0, Width, Height, GraphicsUnit.Pixel, atributos);
                }
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible && RetrasoEntrada >= 0 && !_entradaIniciada && !DesignMode)
            {
                ReiniciarEntrada();
            }
        }

        /// <summary>
        /// Vuelve a reproducir la animación de entrada.
        /// </summary>
        public void ReiniciarEntrada()
        {
            _entradaIniciada = true;
            Entrada = 0;
            Invalidate();
            Animador.Animar(this, 500, Suavizado.SalidaCubica, t =>
            {
                Entrada = t;
                Invalidate();
            }, null, Math.Max(0, RetrasoEntrada));
        }

        private void Tema_Cambiado(object sender, EventArgs e)
        {
            AlCambiarTema();
            Invalidate();
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
