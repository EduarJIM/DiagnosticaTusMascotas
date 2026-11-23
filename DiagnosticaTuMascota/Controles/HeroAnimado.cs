using System.Drawing.Drawing2D;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Ilustración animada del inicio: círculo con degradado que flota, anillos
    /// que se expanden, un destello que orbita y manchas de luz que laten.
    /// Reproduce la columna derecha del Dashboard web.
    /// </summary>
    public class HeroAnimado : ControlPintado
    {
        private double _segundos;
        private double _aparicion;

        public HeroAnimado()
        {
            Size = new Size(500, 600);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (DesignMode)
            {
                _aparicion = 1;
                return;
            }
            Animador.Animar(this, 800, Suavizado.SalidaCubica, t => _aparicion = t, null, 300);
            Animador.Bucle(this, s =>
            {
                _segundos = s;
                Invalidate();
            });
        }

        protected override void Pintar(Graphics g)
        {
            var area = new RectangleF(0, 0, Width, Height);
            using (var degradado = new LinearGradientBrush(area, Color.FromArgb(0, Tema.Primario), Dibujo.Alfa(Tema.Primario, 0.06), LinearGradientMode.ForwardDiagonal))
            {
                g.FillRectangle(degradado, area);
            }
            Dibujo.Brillo(g, new PointF(Width / 2f, Height / 2f), Math.Max(Width, Height) * 0.6f, Dibujo.Alfa(Tema.Primario, 0.06));

            using (var linea = new Pen(Tema.Borde))
            {
                g.DrawLine(linea, 0, 0, 0, Height);
            }

            double pulso1 = 1 - 0.5 * Suavizado.Onda(_segundos, 2);
            double pulso2 = 1 - 0.5 * Suavizado.Onda(_segundos + 1, 2);
            Dibujo.Brillo(g, new PointF(80 + 64, 80 + 64), 90, Dibujo.Alfa(Tema.Primario, 0.14 * pulso1));
            Dibujo.Brillo(g, new PointF(Width - 80 - 80, Height - 80 - 80), 110, Dibujo.Alfa(Tema.Secundario, 0.14 * pulso2));

            float escala = Math.Min(1f, Math.Min(Width, Height) / 440f) * (0.9f + 0.1f * (float)_aparicion);
            float cx = Width / 2f, cy = Height / 2f;
            double opacidad = _aparicion;
            if (opacidad <= 0.01)
            {
                return;
            }

            var estado = g.Save();
            g.TranslateTransform(cx, cy);
            g.ScaleTransform(escala, escala);

            DibujarAnillos(g, opacidad);
            DibujarCirculo(g, opacidad);

            g.Restore(estado);
        }

        private void DibujarAnillos(Graphics g, double opacidad)
        {
            double ping = (_segundos % 3) / 3;
            double crecimiento = 1 - Math.Pow(1 - ping, 3);
            float radioPing = 160 * (1 + (float)crecimiento);
            using (var lapiz = new Pen(Dibujo.Alfa(Tema.Primario, 0.45 * (1 - ping) * opacidad), 2))
            {
                g.DrawEllipse(lapiz, -radioPing, -radioPing, radioPing * 2, radioPing * 2);
            }

            double pulso = 1 - 0.5 * Suavizado.Onda(_segundos, 4);
            using (var lapiz = new Pen(Dibujo.Alfa(Tema.Secundario, 0.30 * pulso * opacidad), 1.2f))
            {
                g.DrawEllipse(lapiz, -128, -128, 256, 256);
            }
        }

        private void DibujarCirculo(Graphics g, double opacidad)
        {
            double fase = _segundos / 6 * 2 * Math.PI;
            float flotar = -20 * (float)Suavizado.Onda(_segundos, 6);
            float giro = 2 * (float)Math.Sin(fase);

            var estado = g.Save();
            g.TranslateTransform(0, flotar);
            g.RotateTransform(giro);

            Dibujo.Brillo(g, new PointF(0, 26), 165, Dibujo.Alfa(Tema.Primario, 0.30 * opacidad));

            var circulo = new RectangleF(-128, -128, 256, 256);
            using (var ruta = new GraphicsPath())
            {
                ruta.AddEllipse(circulo);
                using (var pincel = new LinearGradientBrush(circulo, Tema.Primario, Tema.Azul, LinearGradientMode.ForwardDiagonal))
                {
                    pincel.InterpolationColors = new ColorBlend
                    {
                        Colors = new[] { Dibujo.Alfa(Tema.Primario, opacidad), Dibujo.Alfa(Tema.Secundario, opacidad), Dibujo.Alfa(Tema.Azul, opacidad) },
                        Positions = new[] { 0f, 0.5f, 1f }
                    };
                    g.FillPath(pincel, ruta);
                }
            }
            using (var borde = new Pen(Dibujo.Alfa(Color.White, 0.12 * opacidad), 4))
            {
                g.DrawEllipse(borde, circulo);
            }

            using (var interior = new SolidBrush(Dibujo.Alfa(Color.White, 0.08 * opacidad)))
            {
                g.FillEllipse(interior, -112, -112, 224, 224);
            }

            Iconos.Dibujar(g, Iconos.Salud, new RectangleF(-60, -56, 120, 120), Dibujo.Alfa(Color.Black, 0.18 * opacidad), 90);
            Iconos.Dibujar(g, Iconos.Salud, new RectangleF(-60, -60, 120, 120), Dibujo.Alfa(Color.White, opacidad), 90);

            float orbita = (float)(_segundos / 20 * 360 % 360);
            g.RotateTransform(orbita);
            var tarjeta = new RectangleF(-20, -128 - 16, 40, 40);
            g.TranslateTransform(0, tarjeta.Y + 20);
            g.RotateTransform(-45);
            var local = new RectangleF(-20, -20, 40, 40);
            Dibujo.Sombra(g, local, 12, Color.FromArgb((int)(60 * opacidad), 0, 0, 0), 4, 3);
            Dibujo.RellenarRedondeado(g, Dibujo.Alfa(Tema.Tarjeta, opacidad), local, 12);
            Dibujo.BordeRedondeado(g, Tema.Borde, local, 12);
            Iconos.Dibujar(g, Iconos.Destello, local, Dibujo.Alfa(Tema.Primario, opacidad), 18);

            g.Restore(estado);
        }
    }
}
