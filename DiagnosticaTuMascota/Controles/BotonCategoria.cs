using System.ComponentModel;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Botón de categoría con un icono sobre un degradado de color
    /// (barra de categorías del análisis de síntomas y de consejos).
    /// </summary>
    [DefaultEvent("Click")]
    public class BotonCategoria : ControlPintado
    {
        private readonly ValorAnimado _hover;
        private readonly ValorAnimado _activo;
        private string _icono = "";

        public BotonCategoria()
        {
            _hover = new ValorAnimado(this, 250);
            _activo = new ValorAnimado(this, 300);
            Cursor = Cursors.Hand;
            Size = new Size(260, 62);
            Font = Tema.Fuente(10f, FontStyle.Bold);
        }

        [Category("Apariencia"), DefaultValue("")]
        public string Icono
        {
            get => _icono;
            set { _icono = value ?? ""; Invalidate(); }
        }

        [Category("Apariencia"), DefaultValue(typeof(Color), "16, 185, 129")]
        public Color ColorInicio { get; set; } = Tema.Primario;

        [Category("Apariencia"), DefaultValue(typeof(Color), "20, 184, 166")]
        public Color ColorFin { get; set; } = Tema.Secundario;

        [Category("Apariencia"), DefaultValue(false)]
        public bool Activo
        {
            get => _activo.Valor > 0.5;
            set => _activo.IrA(value ? 1 : 0);
        }

        protected override void OnMouseEnter(EventArgs e) { base.OnMouseEnter(e); _hover.IrA(1); }
        protected override void OnMouseLeave(EventArgs e) { base.OnMouseLeave(e); _hover.IrA(0); }
        protected override void OnTextChanged(EventArgs e) { base.OnTextChanged(e); Invalidate(); }

        protected override void Pintar(Graphics g)
        {
            double a = _activo.Valor;
            double h = _hover.Valor * (1 - a);
            var r = new RectangleF(2, 2, Width - 5, Height - 5);
            const float radio = 16;

            if (a > 0.01)
            {
                Dibujo.Sombra(g, r, radio, Dibujo.Alfa(Tema.Primario, 0.10 * a), 6, 4);
            }
            Dibujo.RellenarRedondeado(g, Dibujo.Alfa(Tema.Apagado, h), r, radio);
            Dibujo.RellenarRedondeado(g, Dibujo.Alfa(Tema.Primario, 0.10 * a), r, radio);
            Dibujo.BordeRedondeado(g, Dibujo.Alfa(Tema.Primario, 0.30 * a), r, radio);

            var caja = new RectangleF(r.X + 16, r.Y + (r.Height - 34) / 2, 34, 34);
            Dibujo.DegradadoRedondeado(g, Dibujo.Alfa(ColorInicio, 0.22), Dibujo.Alfa(ColorFin, 0.22), caja, 9);
            Iconos.Dibujar(g, _icono, caja, Dibujo.Mezclar(Tema.TextoApagado, Tema.Primario, a), 18);

            var texto = Dibujo.Mezclar(Dibujo.Mezclar(Tema.TextoApagado, Tema.Texto, h), Tema.Texto, a);
            Dibujo.Texto(g, Text, Font, texto, new RectangleF(caja.Right + 12, r.Y, r.Right - caja.Right - 16, r.Height),
                StringAlignment.Near, StringAlignment.Center, true);
        }
    }
}
