using System.ComponentModel;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Tarjeta de acceso del inicio (FeatureCard). Al pasar el ratón crece un 2 %,
    /// sube 4 px, el icono se agranda y el título se vuelve verde.
    /// </summary>
    [DefaultEvent("Click")]
    public class TarjetaFuncion : ControlPintado
    {
        private readonly ValorAnimado _hover;
        private bool _presionada;

        public TarjetaFuncion()
        {
            _hover = new ValorAnimado(this, 250);
            Cursor = Cursors.Hand;
            Size = new Size(400, 170);
        }

        [Category("Apariencia"), DefaultValue("")]
        public string Titulo { get; set; } = "";

        [Category("Apariencia"), DefaultValue("")]
        public string Descripcion { get; set; } = "";

        [Category("Apariencia"), DefaultValue("")]
        public string Icono { get; set; } = "";

        [Category("Apariencia"), DefaultValue(typeof(Color), "16, 185, 129")]
        public Color ColorInicio { get; set; } = Tema.Primario;

        [Category("Apariencia"), DefaultValue(typeof(Color), "20, 184, 166")]
        public Color ColorFin { get; set; } = Tema.Secundario;

        protected override void OnMouseEnter(EventArgs e) { base.OnMouseEnter(e); _hover.IrA(1); }
        protected override void OnMouseLeave(EventArgs e) { base.OnMouseLeave(e); _hover.IrA(0); _presionada = false; }
        protected override void OnMouseDown(MouseEventArgs e) { base.OnMouseDown(e); _presionada = true; Invalidate(); }
        protected override void OnMouseUp(MouseEventArgs e) { base.OnMouseUp(e); _presionada = false; Invalidate(); }

        protected override void Pintar(Graphics g)
        {
            double h = _hover.Valor;
            float escala = _presionada ? 0.98f : 1 + 0.02f * (float)h;
            var cx = Width / 2f;
            var cy = Height / 2f;

            var estado = g.Save();
            g.TranslateTransform(cx, cy - 4 * (float)h);
            g.ScaleTransform(escala, escala);
            g.TranslateTransform(-cx, -cy);

            var r = new RectangleF(10, 8, Width - 20, Height - 20);
            const float radio = 24;

            if (h > 0.01)
            {
                Dibujo.Sombra(g, r, radio, Dibujo.Alfa(Tema.Primario, 0.18 * h), 8, 8);
            }
            Dibujo.RellenarRedondeado(g, Tema.Tarjeta, r, radio);
            Dibujo.DegradadoRedondeado(g, Dibujo.Alfa(ColorInicio, 0.10), Dibujo.Alfa(ColorFin, 0.10), r, radio);
            if (h > 0.01)
            {
                Dibujo.DegradadoRedondeado(g, Dibujo.Alfa(Color.White, 0.05 * h), Color.FromArgb(0, Color.White), r, radio);
            }
            Dibujo.BordeRedondeado(g, Dibujo.Alfa(Tema.Borde, 0.5 + 0.5 * h), r, radio);

            float lado = 48 * (1 + 0.1f * (float)h);
            var caja = new RectangleF(r.X + 24 + (48 - lado) / 2, r.Y + 24 + (48 - lado) / 2, lado, lado);
            Dibujo.RellenarRedondeado(g, Tema.Tarjeta, caja, 12);
            Dibujo.BordeRedondeado(g, Tema.Borde, caja, 12);
            Iconos.Dibujar(g, Icono, caja, Tema.Primario, 24 * (1 + 0.1f * (float)h));

            var titulo = new RectangleF(r.X + 24, r.Y + 82, r.Width - 44, 30);
            float tamaño = 15f;
            while (tamaño > 10.5f && g.MeasureString(Titulo, Tema.Fuente(tamaño, FontStyle.Bold)).Width > titulo.Width)
            {
                tamaño -= 0.5f;
            }
            Dibujo.Texto(g, Titulo, Tema.Fuente(tamaño, FontStyle.Bold), Dibujo.Mezclar(Tema.Texto, Tema.Primario, h), titulo,
                StringAlignment.Near, StringAlignment.Center, true);

            var descripcion = new RectangleF(r.X + 24, r.Y + 114, r.Width - 44, r.Bottom - r.Y - 118);
            Dibujo.Texto(g, Descripcion, Tema.Fuente(10f), Tema.TextoApagado, descripcion);

            g.Restore(estado);
        }
    }
}
