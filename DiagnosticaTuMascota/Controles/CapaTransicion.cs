using System.ComponentModel;
using System.Drawing.Imaging;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Capa que cubre el área de contenido mientras cambia de página: muestra una
    /// captura de la página nueva que aparece y sube 20 px, como las páginas web
    /// con initial={{ opacity: 0, y: 20 }}.
    /// </summary>
    [ToolboxItem(false)]
    public class CapaTransicion : Control
    {
        private Bitmap _captura;
        private double _progreso;

        public CapaTransicion()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            Visible = false;
        }

        /// <summary>
        /// Muestra la captura con la animación y se oculta al terminar.
        /// </summary>
        public void Reproducir(Bitmap captura)
        {
            _captura?.Dispose();
            _captura = captura;
            _progreso = 0;
            Visible = true;
            BringToFront();
            Refresh();
            Animador.Animar(this, 380, Suavizado.SalidaCubica, t =>
            {
                _progreso = t;
                Invalidate();
            }, () =>
            {
                Visible = false;
                _captura?.Dispose();
                _captura = null;
            });
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(Tema.Fondo);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_captura == null || _progreso <= 0)
            {
                return;
            }
            var matriz = new ColorMatrix { Matrix33 = (float)_progreso };
            using (var atributos = new ImageAttributes())
            {
                atributos.SetColorMatrix(matriz);
                int y = (int)((1 - _progreso) * 20);
                e.Graphics.DrawImage(_captura, new Rectangle(0, y, _captura.Width, _captura.Height),
                    0, 0, _captura.Width, _captura.Height, GraphicsUnit.Pixel, atributos);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _captura?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
