using System.ComponentModel;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Título de sección con icono a la izquierda (h3 con icono).
    /// </summary>
    public class TituloSeccion : ControlPintado
    {
        private string _icono = "";

        public TituloSeccion()
        {
            Size = new Size(300, 32);
            Font = Tema.Fuente(14f, FontStyle.Bold);
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
        /// Si es true, el texto también usa el color del icono.
        /// </summary>
        [Category("Apariencia"), DefaultValue(false)]
        public bool TextoDeColor { get; set; }

        protected override void OnTextChanged(EventArgs e) { base.OnTextChanged(e); Invalidate(); }

        protected override void Pintar(Graphics g)
        {
            float x = 0;
            if (!string.IsNullOrEmpty(_icono))
            {
                float tamaño = Font.Size * 1.5f;
                Iconos.Dibujar(g, _icono, new RectangleF(0, 0, tamaño + 4, Height), ColorIcono, tamaño);
                x = tamaño + 10;
            }
            Dibujo.Texto(g, Text, Font, TextoDeColor ? ColorIcono : Tema.Texto, new RectangleF(x, 0, Width - x, Height),
                StringAlignment.Near, StringAlignment.Center, true);
        }
    }
}
