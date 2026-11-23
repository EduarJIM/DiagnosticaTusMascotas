using System.ComponentModel;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Tarjeta con título y lista de viñetas de color (implicaciones clínicas y
    /// primeros auxilios del resultado). Ajusta su alto al contenido.
    /// </summary>
    public class TarjetaLista : ControlPintado
    {
        private List<string> _elementos = new List<string>();

        public TarjetaLista()
        {
            Size = new Size(430, 300);
        }

        [Category("Apariencia"), DefaultValue("")]
        public string Titulo { get; set; } = "";

        [Category("Apariencia"), DefaultValue("")]
        public string Icono { get; set; } = "";

        [Category("Apariencia"), DefaultValue(typeof(Color), "16, 185, 129")]
        public Color ColorAcento { get; set; } = Tema.Primario;

        /// <summary>
        /// Texto opcional que aparece al final separado por una línea (notas del propietario).
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Nota { get; set; } = "";

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<string> Elementos
        {
            get => _elementos;
            set { _elementos = value ?? new List<string>(); AjustarAlto(); Invalidate(); }
        }

        /// <summary>
        /// Altura mínima, para igualar dos tarjetas puestas lado a lado.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int AltoMinimo { get; set; }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AjustarAlto();
        }

        /// <summary>
        /// Calcula el alto necesario para mostrar todo el contenido.
        /// </summary>
        public int AltoNecesario()
        {
            using (var g = CreateGraphics())
            {
                float ancho = Width - 32 - 26 - 20;
                float y = 32 + 40;
                foreach (var elemento in _elementos)
                {
                    y += Dibujo.Medir(g, elemento, Tema.Fuente(10.5f), ancho).Height + 14;
                }
                if (!string.IsNullOrWhiteSpace(Nota))
                {
                    y += 30 + 24 + Dibujo.Medir(g, $"\"{Nota}\"", Tema.Fuente(10.5f, FontStyle.Italic), Width - 64).Height;
                }
                return (int)y + 24;
            }
        }

        private void AjustarAlto()
        {
            if (!IsHandleCreated || Width <= 100)
            {
                return;
            }
            int alto = Math.Max(AltoMinimo, AltoNecesario());
            if (Height != alto)
            {
                Height = alto;
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            AjustarAlto();
        }

        protected override void Pintar(Graphics g)
        {
            var r = new RectangleF(1, 1, Width - 3, Height - 4);
            Dibujo.Sombra(g, r, 24, Color.FromArgb(Tema.Oscuro ? 50 : 20, 0, 0, 0), 4, 2);
            Dibujo.RellenarRedondeado(g, Tema.Tarjeta, r, 24);
            Dibujo.BordeRedondeado(g, Tema.Borde, r, 24);

            Iconos.Dibujar(g, Icono, new RectangleF(32, 30, 28, 32), ColorAcento, 22);
            Dibujo.Texto(g, Titulo, Tema.Fuente(14f, FontStyle.Bold), Tema.Texto, new RectangleF(68, 30, Width - 100, 32),
                StringAlignment.Near, StringAlignment.Center, true);

            float y = 32 + 40;
            float ancho = Width - 32 - 26 - 20;
            var colorTexto = Dibujo.Alfa(Tema.Texto, 0.9);
            using (var punto = new SolidBrush(ColorAcento))
            {
                foreach (var elemento in _elementos)
                {
                    var tamaño = Dibujo.Medir(g, elemento, Tema.Fuente(10.5f), ancho);
                    g.FillEllipse(punto, 34, y + 8, 8, 8);
                    Dibujo.Texto(g, elemento, Tema.Fuente(10.5f), colorTexto, new RectangleF(58, y, ancho, tamaño.Height + 4));
                    y += tamaño.Height + 14;
                }
            }

            if (!string.IsNullOrWhiteSpace(Nota))
            {
                y += 6;
                using (var linea = new Pen(Tema.Borde))
                {
                    g.DrawLine(linea, 32, y, Width - 32, y);
                }
                y += 22;
                Dibujo.Texto(g, "Notas adicionales del propietario:", Tema.Fuente(9.5f, FontStyle.Bold), Tema.TextoApagado, new RectangleF(32, y, Width - 64, 22));
                y += 24;
                Dibujo.Texto(g, $"\"{Nota}\"", Tema.Fuente(10.5f, FontStyle.Italic), Dibujo.Alfa(Tema.Texto, 0.8), new RectangleF(32, y, Width - 64, Height - y));
            }
        }
    }
}
