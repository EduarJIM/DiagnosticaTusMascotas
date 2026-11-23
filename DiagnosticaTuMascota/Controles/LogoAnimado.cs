using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Logotipo de la barra lateral: cuadro verde con el icono de salud que
    /// "late" cada 3 segundos (escala 1 → 1.1 y brillo).
    /// </summary>
    public class LogoAnimado : ControlPintado
    {
        private double _onda;

        public LogoAnimado()
        {
            Size = new Size(52, 52);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!DesignMode)
            {
                Animador.Bucle(this, s =>
                {
                    _onda = Suavizado.Onda(s, 3);
                    Invalidate();
                });
            }
        }

        protected override void Pintar(Graphics g)
        {
            float centro = Width / 2f;
            float lado = 36 * (1 + 0.1f * (float)_onda);
            Dibujo.Brillo(g, new PointF(centro, centro), lado / 2 + 4 + 8 * (float)_onda, Dibujo.Alfa(Tema.Primario, 0.55 * _onda));

            var caja = new RectangleF(centro - lado / 2, centro - lado / 2, lado, lado);
            Dibujo.Sombra(g, caja, 12, Dibujo.Alfa(Tema.Primario, 0.25), 4, 3);
            Dibujo.RellenarRedondeado(g, Tema.Primario, caja, 12);
            Iconos.Dibujar(g, Iconos.Salud, caja, Color.White, 20 * (1 + 0.1f * (float)_onda));
        }
    }
}
