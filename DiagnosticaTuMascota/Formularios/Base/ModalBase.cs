using System.ComponentModel;
using System.Runtime.InteropServices;
using DiagnosticaTuMascota.Controles;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Formularios.Base
{
    /// <summary>
    /// Base de las ventanas modales: sin borde, esquinas redondeadas, botón X,
    /// fondo oscurecido detrás y entrada con fundido + desplazamiento
    /// (como los modales con AnimatePresence de la web).
    /// </summary>
    public class ModalBase : Form
    {
        private const int CS_DROPSHADOW = 0x20000;
        private int _destinoY;
        private Rectangle? _areaPrincipal;

        protected readonly BotonRedondeado btnCerrarModal;

        public ModalBase()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            ShowInTaskbar = false;
            KeyPreview = true;
            base.BackColor = Tema.Tarjeta;
            base.ForeColor = Tema.Texto;

            btnCerrarModal = new BotonRedondeado
            {
                Estilo = EstiloBoton.Icono,
                Icono = Iconos.Cerrar,
                ColorAcento = Tema.Texto,
                Radio = 20,
                Size = new Size(48, 48),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                TabStop = false,
                Font = Tema.Fuente(9f)
            };
            btnCerrarModal.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
            Controls.Add(btnCerrarModal);
            Tema.Cambiado += Tema_Cambiado;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color BackColor
        {
            get => base.BackColor;
            set => base.BackColor = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color ForeColor
        {
            get => base.ForeColor;
            set => base.ForeColor = value;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var parametros = base.CreateParams;
                parametros.ClassStyle |= CS_DROPSHADOW;
                return parametros;
            }
        }

        /// <summary>
        /// Muestra el modal centrado sobre la ventana principal con el fondo oscurecido.
        /// </summary>
        public DialogResult MostrarSobre(Control origen)
        {
            var principal = (origen?.TopLevelControl as Form) ?? Form.ActiveForm;
            using (var fondo = new FondoModal(principal))
            {
                fondo.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
                fondo.Show(principal);
                _areaPrincipal = principal?.Bounds;
                var resultado = ShowDialog(fondo);
                fondo.Ocultar();
                return resultado;
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Tema.EsquinasRedondeadas(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AplicarTema();
            if (_areaPrincipal.HasValue)
            {
                var area = _areaPrincipal.Value;
                Height = Math.Min(Height, Screen.FromRectangle(area).WorkingArea.Height - 40);
                Location = new Point(area.X + (area.Width - Width) / 2, area.Y + (area.Height - Height) / 2);
            }
            btnCerrarModal.Location = new Point(ClientSize.Width - 72, 20);
            btnCerrarModal.BringToFront();
            if (!DesignMode)
            {
                Opacity = 0;
                _destinoY = Top;
                Top += 20;
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (DesignMode)
            {
                return;
            }
            int origenY = Top;
            Animador.Animar(this, 280, Suavizado.SalidaCubica, t =>
            {
                Opacity = t;
                Top = (int)(origenY + (_destinoY - origenY) * t);
            });
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var lapiz = new Pen(Tema.BordeSobre(Tema.Tarjeta)))
            {
                e.Graphics.DrawRectangle(lapiz, 0, 0, Width - 1, Height - 1);
            }
        }

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, (IntPtr)2, IntPtr.Zero);
            }
        }

        protected virtual void AplicarTema()
        {
            base.BackColor = Tema.Tarjeta;
            base.ForeColor = Tema.Texto;
            Tema.Aplicar(this);
            Invalidate(true);
        }

        private void Tema_Cambiado(object sender, EventArgs e) => AplicarTema();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Tema.Cambiado -= Tema_Cambiado;
            }
            base.Dispose(disposing);
        }
    }

    /// <summary>
    /// Capa negra semitransparente que oscurece la ventana principal detrás de un modal.
    /// </summary>
    public class FondoModal : Form
    {
        public FondoModal(Form principal)
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            ShowInTaskbar = false;
            BackColor = Color.Black;
            Opacity = 0;
            if (principal != null)
            {
                Bounds = principal.Bounds;
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Animador.Animar(this, 250, Suavizado.SalidaCubica, t => Opacity = 0.55 * t);
        }

        /// <summary>
        /// Desvanece la capa antes de cerrarla.
        /// </summary>
        public void Ocultar()
        {
            double inicial = Opacity;
            for (int i = 1; i <= 8; i++)
            {
                Opacity = inicial * (1 - i / 8.0);
                Application.DoEvents();
                Thread.Sleep(12);
            }
            Close();
        }
    }
}
