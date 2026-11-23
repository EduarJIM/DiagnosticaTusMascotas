using System.ComponentModel;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Contenido del aviso de alarma (ActiveAlarmModal): campana que se sacude,
    /// fondo que late, "¡Es la hora!", título, mascota y mensaje.
    /// </summary>
    public class ContenidoAlarma : ControlPintado
    {
        private double _segundos;

        public ContenidoAlarma()
        {
            Size = new Size(520, 420);
        }

        [Category("Apariencia"), DefaultValue("")]
        public string Titulo { get; set; } = "";

        [Category("Apariencia"), DefaultValue("")]
        public string Mascota { get; set; } = "";

        [Category("Apariencia"), DefaultValue("")]
        public string Mensaje { get; set; } = "";

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!DesignMode)
            {
                Animador.Bucle(this, s =>
                {
                    _segundos = s;
                    Invalidate();
                });
            }
        }

        /// <summary>
        /// Sacudida de 1 s seguida de 1 s de pausa: 0, -10, 10, -10, 10, 0 grados.
        /// </summary>
        private float AnguloCampana()
        {
            double t = _segundos % 2;
            if (t > 1)
            {
                return 0;
            }
            return (float)(-10 * Math.Sin(t * 4 * Math.PI) * (1 - t * 0.2));
        }

        protected override void Pintar(Graphics g)
        {
            double pulso = Suavizado.Onda(_segundos, 2);
            float cx = Width / 2f;
            var circulo = new RectangleF(cx - 48, 8, 96, 96);
            Dibujo.Brillo(g, new PointF(cx, 56), 70 + 14 * (float)pulso, Dibujo.Alfa(Tema.Primario, 0.18 + 0.15 * pulso));
            using (var pincel = new SolidBrush(Dibujo.Alfa(Tema.Primario, 0.12)))
            {
                g.FillEllipse(pincel, circulo);
            }
            var estado = g.Save();
            g.TranslateTransform(cx, 56);
            g.RotateTransform(AnguloCampana());
            Iconos.Dibujar(g, Iconos.Campana, new RectangleF(-32, -32, 64, 64), Tema.Primario, 44);
            g.Restore(estado);

            var fuentePastilla = Tema.Fuente(9f, FontStyle.Bold);
            const string textoPastilla = "¡ES LA HORA!";
            float ancho = g.MeasureString(textoPastilla, fuentePastilla).Width + 48;
            var pastilla = new RectangleF(cx - ancho / 2, 126, ancho, 32);
            Dibujo.RellenarRedondeado(g, Dibujo.Alfa(Tema.Primario, 0.20), pastilla, 16);
            Iconos.Dibujar(g, Iconos.Reloj, new RectangleF(pastilla.X + 10, pastilla.Y, 20, pastilla.Height), Tema.Primario, 14);
            Dibujo.Texto(g, textoPastilla, fuentePastilla, Tema.Primario, new RectangleF(pastilla.X + 28, pastilla.Y, pastilla.Width - 32, pastilla.Height),
                StringAlignment.Center, StringAlignment.Center);

            Dibujo.Texto(g, Titulo, Tema.FuenteNegra(22f), Tema.Texto, new RectangleF(10, 170, Width - 20, 48), StringAlignment.Center, StringAlignment.Center, true);
            Dibujo.Texto(g, $"Para: {Mascota}", Tema.Fuente(15f, FontStyle.Bold), Tema.Primario, new RectangleF(10, 220, Width - 20, 34), StringAlignment.Center, StringAlignment.Center, true);

            var caja = new RectangleF(10, 272, Width - 20, Height - 280);
            Dibujo.RellenarRedondeado(g, Dibujo.Alfa(Tema.Apagado, 0.5), caja, 24);
            Dibujo.BordeRedondeado(g, Tema.Borde, caja, 24);
            Dibujo.Texto(g, string.IsNullOrWhiteSpace(Mensaje) ? "Es momento de atender este recordatorio." : Mensaje,
                Tema.Fuente(12f, FontStyle.Bold), Tema.Texto, RectangleF.Inflate(caja, -20, -12), StringAlignment.Center, StringAlignment.Center);
        }
    }
}
