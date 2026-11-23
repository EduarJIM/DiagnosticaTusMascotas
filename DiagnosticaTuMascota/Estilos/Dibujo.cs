using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace DiagnosticaTuMascota.Estilos
{
    /// <summary>
    /// Utilidades de dibujo: rectángulos redondeados, mezclas de color y brillos
    /// suaves que imitan las sombras y desenfoques de la versión web.
    /// </summary>
    public static class Dibujo
    {
        /// <summary>
        /// Prepara un Graphics para dibujar con bordes suaves y texto nítido.
        /// </summary>
        public static void Preparar(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        }

        public static GraphicsPath Redondeado(RectangleF r, float radio)
        {
            var ruta = new GraphicsPath();
            float d = Math.Min(radio * 2, Math.Min(r.Width, r.Height));
            if (d <= 0.5f)
            {
                ruta.AddRectangle(r);
                return ruta;
            }
            ruta.AddArc(r.X, r.Y, d, d, 180, 90);
            ruta.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            ruta.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            ruta.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            ruta.CloseFigure();
            return ruta;
        }

        public static void RellenarRedondeado(Graphics g, Color color, RectangleF r, float radio)
        {
            using (var ruta = Redondeado(r, radio))
            using (var pincel = new SolidBrush(color))
            {
                g.FillPath(pincel, ruta);
            }
        }

        public static void BordeRedondeado(Graphics g, Color color, RectangleF r, float radio, float grosor = 1f)
        {
            using (var ruta = Redondeado(r, radio))
            using (var lapiz = new Pen(color, grosor))
            {
                g.DrawPath(lapiz, ruta);
            }
        }

        /// <summary>
        /// Rellena con un degradado diagonal (como bg-gradient-to-br).
        /// </summary>
        public static void DegradadoRedondeado(Graphics g, Color inicio, Color fin, RectangleF r, float radio)
        {
            if (r.Width < 1 || r.Height < 1)
            {
                return;
            }
            using (var ruta = Redondeado(r, radio))
            using (var pincel = new LinearGradientBrush(r, inicio, fin, LinearGradientMode.ForwardDiagonal))
            {
                g.FillPath(pincel, ruta);
            }
        }

        /// <summary>
        /// Sombra difusa bajo un rectángulo redondeado (shadow-lg / shadow-xl).
        /// </summary>
        public static void Sombra(Graphics g, RectangleF r, float radio, Color color, int capas = 8, float desplazamientoY = 4)
        {
            for (int i = capas; i >= 1; i--)
            {
                int alfa = (int)(color.A * 0.6 / capas * (capas - i + 1) / capas);
                var rect = RectangleF.Inflate(r, i * 1.5f, i * 1.5f);
                rect.Offset(0, desplazamientoY);
                RellenarRedondeado(g, Color.FromArgb(Math.Max(0, Math.Min(255, alfa)), color), rect, radio + i * 1.5f);
            }
        }

        /// <summary>
        /// Mancha de luz circular difuminada (blur-3xl).
        /// </summary>
        public static void Brillo(Graphics g, PointF centro, float radio, Color color)
        {
            if (radio < 1)
            {
                return;
            }
            using (var ruta = new GraphicsPath())
            {
                ruta.AddEllipse(centro.X - radio, centro.Y - radio, radio * 2, radio * 2);
                using (var pincel = new PathGradientBrush(ruta))
                {
                    pincel.CenterColor = color;
                    pincel.SurroundColors = new[] { Color.FromArgb(0, color) };
                    g.FillPath(pincel, ruta);
                }
            }
        }

        /// <summary>
        /// Mezcla dos colores: t=0 devuelve a, t=1 devuelve b.
        /// </summary>
        public static Color Mezclar(Color a, Color b, double t)
        {
            t = Math.Max(0, Math.Min(1, t));
            return Color.FromArgb(
                (int)(a.A + (b.A - a.A) * t),
                (int)(a.R + (b.R - a.R) * t),
                (int)(a.G + (b.G - a.G) * t),
                (int)(a.B + (b.B - a.B) * t));
        }

        /// <summary>
        /// Color con opacidad (como primary/10 en Tailwind: Alfa(Primario, 0.10)).
        /// </summary>
        public static Color Alfa(Color color, double opacidad) =>
            Color.FromArgb((int)Math.Max(0, Math.Min(255, 255 * opacidad * (color.A / 255.0))), color.R, color.G, color.B);

        /// <summary>
        /// Color sólido equivalente a poner "color" con cierta opacidad sobre "fondo".
        /// </summary>
        public static Color Sobre(Color fondo, Color color, double opacidad) =>
            Mezclar(Color.FromArgb(255, fondo), Color.FromArgb(255, color), opacidad);

        public static float Lerp(float a, float b, double t) => (float)(a + (b - a) * t);

        /// <summary>
        /// Dibuja texto con ajuste de línea dentro de un rectángulo.
        /// </summary>
        public static void Texto(Graphics g, string texto, Font fuente, Color color, RectangleF r,
            StringAlignment horizontal = StringAlignment.Near, StringAlignment vertical = StringAlignment.Near, bool unaLinea = false)
        {
            using (var formato = new StringFormat())
            using (var pincel = new SolidBrush(color))
            {
                formato.Alignment = horizontal;
                formato.LineAlignment = vertical;
                if (unaLinea)
                {
                    formato.FormatFlags = StringFormatFlags.NoWrap;
                    formato.Trimming = StringTrimming.EllipsisCharacter;
                }
                g.DrawString(texto ?? "", fuente, pincel, r, formato);
            }
        }

        public static SizeF Medir(Graphics g, string texto, Font fuente, float ancho) =>
            g.MeasureString(string.IsNullOrEmpty(texto) ? " " : texto, fuente, (int)Math.Max(1, ancho));
    }
}
