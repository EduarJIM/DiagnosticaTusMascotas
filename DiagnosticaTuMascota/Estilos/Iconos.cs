using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace DiagnosticaTuMascota.Estilos
{
    /// <summary>
    /// Iconos de la aplicación. Se usan los glifos de la fuente de iconos de
    /// Windows (equivalentes a los de lucide-react de la web). La huella y el
    /// destello se dibujan a mano porque la fuente no los incluye.
    /// </summary>
    public static class Iconos
    {
        public const string Inicio = "\uE80F";
        public const string Ajustes = "\uE713";
        public const string Historial = "\uE81C";
        public const string Agregar = "\uE710";
        public const string Editar = "\uE70F";
        public const string Eliminar = "\uE74D";
        public const string Cerrar = "\uE711";
        public const string Siguiente = "\uE76C";
        public const string Atras = "\uE72B";
        public const string Reloj = "\uE823";
        public const string Alerta = "\uE7BA";
        public const string Check = "\uE73E";
        public const string CheckCirculo = "\uE930";
        public const string Salud = "\uE95E";
        public const string Campana = "\uEA8F";
        public const string Calendario = "\uE787";
        public const string Sol = "\uE706";
        public const string Luna = "\uE708";
        public const string Documento = "\uE8A5";
        public const string Info = "\uE946";
        public const string Bombilla = "\uEA80";
        public const string Salir = "\uE7E8";
        public const string Actividad = "\uE9D9";
        public const string Termometro = "\uE9CA";
        public const string Corazon = "\uEB51";
        public const string Rayo = "\uE945";
        public const string Comida = "\uED56";
        public const string Hoja = "\uEC0A";
        public const string Gota = "\uEB42";
        public const string Libro = "\uE82D";
        public const string Lista = "\uE9D5";

        /// <summary>
        /// Valores especiales de la propiedad Icono que se dibujan a mano.
        /// </summary>
        public const string Huella = "huella";
        public const string Destello = "destello";

        private static string _familia;

        /// <summary>
        /// "Segoe Fluent Icons" en Windows 11 o "Segoe MDL2 Assets" en Windows 10.
        /// </summary>
        public static string Familia
        {
            get
            {
                if (_familia == null)
                {
                    using (var fuentes = new InstalledFontCollection())
                    {
                        _familia = fuentes.Families.Any(f => f.Name == "Segoe Fluent Icons")
                            ? "Segoe Fluent Icons"
                            : "Segoe MDL2 Assets";
                    }
                }
                return _familia;
            }
        }

        /// <summary>
        /// Dibuja un icono centrado en el rectángulo.
        /// </summary>
        public static void Dibujar(Graphics g, string icono, RectangleF r, Color color, float tamaño)
        {
            if (string.IsNullOrEmpty(icono))
            {
                return;
            }

            if (icono == Huella)
            {
                DibujarHuella(g, r, color, tamaño);
                return;
            }
            if (icono == Destello)
            {
                DibujarDestello(g, r, color, tamaño);
                return;
            }

            var estado = g.TextRenderingHint;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            using (var formato = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            using (var pincel = new SolidBrush(color))
            {
                g.DrawString(icono, Tema.FuenteIconos(tamaño * 0.75f), pincel, r, formato);
            }
            g.TextRenderingHint = estado;
        }

        /// <summary>
        /// Huella de mascota (PawPrint): una almohadilla y cuatro dedos.
        /// </summary>
        public static void DibujarHuella(Graphics g, RectangleF r, Color color, float tamaño)
        {
            float s = tamaño / 24f;
            float x = r.X + (r.Width - tamaño) / 2;
            float y = r.Y + (r.Height - tamaño) / 2;
            using (var lapiz = new Pen(color, Math.Max(1.5f, 2f * s)))
            {
                lapiz.LineJoin = LineJoin.Round;
                g.DrawEllipse(lapiz, x + 2.5f * s, y + 8.5f * s, 4f * s, 5f * s);
                g.DrawEllipse(lapiz, x + 6.5f * s, y + 3f * s, 4f * s, 5f * s);
                g.DrawEllipse(lapiz, x + 13.5f * s, y + 3f * s, 4f * s, 5f * s);
                g.DrawEllipse(lapiz, x + 17.5f * s, y + 8.5f * s, 4f * s, 5f * s);
                using (var ruta = new GraphicsPath())
                {
                    ruta.AddBezier(x + 12f * s, y + 11f * s, x + 7f * s, y + 11f * s, x + 5f * s, y + 18f * s, x + 7.5f * s, y + 20.5f * s);
                    ruta.AddBezier(x + 7.5f * s, y + 20.5f * s, x + 9.5f * s, y + 22.5f * s, x + 14.5f * s, y + 22.5f * s, x + 16.5f * s, y + 20.5f * s);
                    ruta.AddBezier(x + 16.5f * s, y + 20.5f * s, x + 19f * s, y + 18f * s, x + 17f * s, y + 11f * s, x + 12f * s, y + 11f * s);
                    g.DrawPath(lapiz, ruta);
                }
            }
        }

        /// <summary>
        /// Destello de cuatro puntas (Sparkles).
        /// </summary>
        public static void DibujarDestello(Graphics g, RectangleF r, Color color, float tamaño)
        {
            float cx = r.X + r.Width / 2, cy = r.Y + r.Height / 2;
            DibujarEstrella(g, cx - tamaño * 0.08f, cy + tamaño * 0.05f, tamaño * 0.42f, color);
            DibujarEstrella(g, cx + tamaño * 0.32f, cy - tamaño * 0.3f, tamaño * 0.15f, color);
        }

        private static void DibujarEstrella(Graphics g, float cx, float cy, float radio, Color color)
        {
            float c = radio * 0.22f;
            var puntos = new[]
            {
                new PointF(cx, cy - radio), new PointF(cx + c, cy - c),
                new PointF(cx + radio, cy), new PointF(cx + c, cy + c),
                new PointF(cx, cy + radio), new PointF(cx - c, cy + c),
                new PointF(cx - radio, cy), new PointF(cx - c, cy - c)
            };
            using (var pincel = new SolidBrush(color))
            {
                g.FillPolygon(pincel, puntos);
            }
        }
    }
}
