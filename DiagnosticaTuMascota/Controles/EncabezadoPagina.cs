using System.ComponentModel;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Título grande de cada página con icono verde y subtítulo gris
    /// (h1 text-4xl font-bold + p text-lg text-muted-foreground).
    /// </summary>
    public class EncabezadoPagina : ControlPintado
    {
        private string _titulo = "Título";
        private string _subtitulo = "";
        private string _icono = "";

        public EncabezadoPagina()
        {
            Size = new Size(700, 86);
        }

        [Category("Apariencia"), DefaultValue("Título")]
        public string Titulo
        {
            get => _titulo;
            set { _titulo = value; Invalidate(); }
        }

        [Category("Apariencia"), DefaultValue("")]
        public string Subtitulo
        {
            get => _subtitulo;
            set { _subtitulo = value; Invalidate(); }
        }

        [Category("Apariencia"), DefaultValue("")]
        public string Icono
        {
            get => _icono;
            set { _icono = value ?? ""; Invalidate(); }
        }

        [Category("Apariencia"), DefaultValue(typeof(Color), "16, 185, 129")]
        public Color ColorIcono { get; set; } = Tema.Primario;

        /// <summary>
        /// Usa Segoe UI Black (font-black) en lugar de negrita normal.
        /// </summary>
        [Category("Apariencia"), DefaultValue(false)]
        public bool TituloNegro { get; set; }

        protected override void Pintar(Graphics g)
        {
            var fuenteTitulo = TituloNegro ? Tema.FuenteNegra(24f) : Tema.Fuente(24f, FontStyle.Bold);
            float x = 0;
            if (!string.IsNullOrEmpty(_icono))
            {
                Iconos.Dibujar(g, _icono, new RectangleF(0, 2, 40, 44), ColorIcono, 32);
                x = 50;
            }
            Dibujo.Texto(g, _titulo, fuenteTitulo, Tema.Texto, new RectangleF(x - 4, 0, Width - x, 48),
                StringAlignment.Near, StringAlignment.Center, true);
            Dibujo.Texto(g, _subtitulo, Tema.Fuente(12f), Tema.TextoApagado, new RectangleF(0, 52, Width, Height - 52));
        }
    }
}
