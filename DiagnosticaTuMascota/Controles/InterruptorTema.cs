using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Interruptor sol/luna para cambiar entre modo claro y oscuro (ThemeToggle).
    /// La bolita se desliza con efecto resorte y gira 180°.
    /// </summary>
    public class InterruptorTema : ControlPintado
    {
        private readonly ValorAnimado _posicion;

        public InterruptorTema()
        {
            _posicion = new ValorAnimado(this, 450, Tema.Oscuro ? 1 : 0);
            Cursor = Cursors.Hand;
            Size = new Size(58, 30);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Tema.CambiarModo(!Tema.Oscuro);
        }

        protected override void AlCambiarTema()
        {
            _posicion.IrA(Tema.Oscuro ? 1 : 0, Suavizado.Resorte);
        }

        protected override void Pintar(Graphics g)
        {
            double p = _posicion.Valor;
            var pista = new RectangleF(1, 1, 56, 28);
            var colorPista = Dibujo.Mezclar(Color.FromArgb(254, 243, 199), Color.FromArgb(30, 41, 59), Math.Max(0, Math.Min(1, p)));
            Dibujo.RellenarRedondeado(g, colorPista, pista, 14);
            Dibujo.BordeRedondeado(g, Tema.Borde, pista, 14);

            var tenue = Dibujo.Alfa(Tema.Oscuro ? Color.FromArgb(148, 163, 184) : Color.FromArgb(245, 158, 11), 0.5);
            Iconos.Dibujar(g, Iconos.Sol, new RectangleF(8, 1, 14, 28), tenue, 10);
            Iconos.Dibujar(g, Iconos.Luna, new RectangleF(36, 1, 14, 28), tenue, 10);

            float x = 5 + (float)(28 * p);
            var bolita = new RectangleF(x, 5, 20, 20);
            var colorBolita = Dibujo.Mezclar(Color.FromArgb(251, 191, 36), Color.FromArgb(226, 232, 240), Math.Max(0, Math.Min(1, p)));
            Dibujo.Sombra(g, bolita, 10, Color.FromArgb(70, 0, 0, 0), 3, 1);
            Dibujo.RellenarRedondeado(g, colorBolita, bolita, 10);

            var estado = g.Save();
            g.TranslateTransform(bolita.X + 10, bolita.Y + 10);
            g.RotateTransform((float)(180 * p));
            var icono = p >= 0.5 ? Iconos.Luna : Iconos.Sol;
            var colorIcono = p >= 0.5 ? Color.FromArgb(15, 23, 42) : Color.FromArgb(120, 53, 15);
            Iconos.Dibujar(g, icono, new RectangleF(-10, -10, 20, 20), colorIcono, 12);
            g.Restore(estado);
        }
    }
}
