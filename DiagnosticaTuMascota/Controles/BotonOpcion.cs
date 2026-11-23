using System.ComponentModel;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Botón de síntoma que se marca y desmarca. Muestra un círculo a la derecha
    /// cuyo punto blanco aparece con efecto resorte (scale 0 → 1).
    /// </summary>
    [DefaultEvent("SeleccionCambiada")]
    public class BotonOpcion : ControlPintado
    {
        private readonly ValorAnimado _hover;
        private readonly ValorAnimado _seleccion;
        private readonly ValorAnimado _punto;
        private bool _seleccionado;

        public event EventHandler SeleccionCambiada;

        public BotonOpcion()
        {
            _hover = new ValorAnimado(this, 200);
            _seleccion = new ValorAnimado(this, 200);
            _punto = new ValorAnimado(this, 350);
            Cursor = Cursors.Hand;
            Size = new Size(300, 58);
            Font = Tema.Fuente(10.5f, FontStyle.Bold);
        }

        [Category("Comportamiento"), DefaultValue(false)]
        public bool Seleccionado
        {
            get => _seleccionado;
            set
            {
                if (_seleccionado == value)
                {
                    return;
                }
                _seleccionado = value;
                _seleccion.IrA(value ? 1 : 0);
                _punto.IrA(value ? 1 : 0, value ? Suavizado.Resorte : Suavizado.SalidaCubica);
                SeleccionCambiada?.Invoke(this, EventArgs.Empty);
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Seleccionado = !Seleccionado;
        }

        protected override void OnMouseEnter(EventArgs e) { base.OnMouseEnter(e); _hover.IrA(1); }
        protected override void OnMouseLeave(EventArgs e) { base.OnMouseLeave(e); _hover.IrA(0); }
        protected override void OnTextChanged(EventArgs e) { base.OnTextChanged(e); Invalidate(); }

        protected override void Pintar(Graphics g)
        {
            double h = _hover.Valor * (1 - _seleccion.Valor);
            double s = _seleccion.Valor;
            var r = new RectangleF(1, 1, Width - 3, Height - 3);
            const float radio = 16;

            var fondoNormal = Dibujo.Mezclar(Tema.Fondo, Tema.Apagado, h);
            var fondoSeleccion = Dibujo.Sobre(FondoPadre, Tema.Primario, 0.10);
            Dibujo.RellenarRedondeado(g, Dibujo.Mezclar(fondoNormal, fondoSeleccion, s), r, radio);

            var bordeNormal = Dibujo.Mezclar(Tema.BordeSobre(FondoPadre), Dibujo.Sobre(FondoPadre, Tema.Primario, 0.3), h);
            var bordeSeleccion = Dibujo.Sobre(FondoPadre, Tema.Primario, 0.5);
            Dibujo.BordeRedondeado(g, Dibujo.Mezclar(bordeNormal, bordeSeleccion, s), r, radio);

            var texto = Dibujo.Mezclar(Dibujo.Mezclar(Tema.TextoApagado, Tema.Texto, h), Tema.Primario, s);
            Dibujo.Texto(g, Text, Font, texto, new RectangleF(r.X + 20, r.Y, r.Width - 64, r.Height),
                StringAlignment.Near, StringAlignment.Center, true);

            var circulo = new RectangleF(r.Right - 40, r.Y + (r.Height - 20) / 2, 20, 20);
            using (var relleno = new SolidBrush(Dibujo.Alfa(Tema.Primario, s)))
            {
                g.FillEllipse(relleno, circulo);
            }
            using (var lapiz = new Pen(Dibujo.Mezclar(Tema.BordeSobre(FondoPadre), Tema.Primario, s), 2))
            {
                g.DrawEllipse(lapiz, circulo);
            }

            float punto = 8 * (float)Math.Max(0, _punto.Valor);
            if (punto > 0.3f)
            {
                using (var blanco = new SolidBrush(Color.White))
                {
                    g.FillEllipse(blanco, circulo.X + 10 - punto / 2, circulo.Y + 10 - punto / 2, punto, punto);
                }
            }
        }
    }
}
