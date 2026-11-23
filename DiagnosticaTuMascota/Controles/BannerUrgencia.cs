using System.ComponentModel;
using DiagnosticaTuMascota.Core;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Banner principal del resultado: círculo con icono, nivel de urgencia,
    /// recomendación y una mancha de luz que late, todo en el color del nivel.
    /// </summary>
    public class BannerUrgencia : ControlPintado
    {
        private NivelSeveridad _nivel = NivelSeveridad.Leve;
        private double _segundos;

        public BannerUrgencia()
        {
            Size = new Size(880, 230);
        }

        [Category("Apariencia"), DefaultValue(NivelSeveridad.Leve)]
        public NivelSeveridad Nivel
        {
            get => _nivel;
            set { _nivel = value; Invalidate(); }
        }

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

        private string Icono => _nivel switch
        {
            NivelSeveridad.Critico => Iconos.Alerta,
            NivelSeveridad.Moderado => Iconos.Termometro,
            _ => Iconos.CheckCirculo
        };

        private string Titulo => _nivel switch
        {
            NivelSeveridad.Critico => "Atención Veterinaria Inmediata Requerida",
            NivelSeveridad.Moderado => "Se Sugiere Revisión Veterinaria Pronta",
            _ => "Monitoreo en Casa Recomendado"
        };

        protected override void Pintar(Graphics g)
        {
            var color = MotorTriaje.ColorNivel(_nivel);
            var r = new RectangleF(2, 2, Width - 5, Height - 5);
            const float radio = 32;

            Dibujo.Sombra(g, r, radio, Dibujo.Alfa(color, 0.15), 8, 6);
            Dibujo.RellenarRedondeado(g, FondoPadre, r, radio);
            Dibujo.RellenarRedondeado(g, Dibujo.Alfa(color, 0.10), r, radio);

            var estado = g.Save();
            using (var ruta = Dibujo.Redondeado(r, radio))
            {
                g.SetClip(ruta);
                double ciclo = Suavizado.Onda(_segundos, 4);
                float escala = 1 + 0.05f * (float)ciclo;
                Dibujo.Brillo(g, new PointF(r.Right - 48, r.Y + 48), 150 * escala, Dibujo.Alfa(color, 0.20 + 0.15 * ciclo));
            }
            g.Restore(estado);
            Dibujo.BordeRedondeado(g, Dibujo.Alfa(color, 0.25), r, radio, 2);

            var circulo = new RectangleF(r.X + 40, r.Y + (r.Height - 128) / 2, 128, 128);
            Dibujo.Brillo(g, new PointF(circulo.X + 64, circulo.Y + 76), 90, Dibujo.Alfa(color, 0.35));
            using (var pincel = new SolidBrush(color))
            {
                g.FillEllipse(pincel, circulo);
            }
            Iconos.Dibujar(g, Icono, circulo, Color.White, 60);

            float x = circulo.Right + 36;
            float ancho = r.Right - x - 36;
            var textoNivel = $"NIVEL DE URGENCIA: {MotorTriaje.Etiqueta(_nivel)}";
            var fuenteNivel = Tema.Fuente(9f, FontStyle.Bold);
            float anchoNivel = g.MeasureString(textoNivel, fuenteNivel).Width + 28;
            var pastilla = new RectangleF(x, r.Y + 40, anchoNivel, 30);
            Dibujo.RellenarRedondeado(g, color, pastilla, 15);
            Dibujo.Texto(g, textoNivel, fuenteNivel, Color.White, pastilla, StringAlignment.Center, StringAlignment.Center);

            float tamaño = 18f;
            while (tamaño > 13f && g.MeasureString(Titulo, Tema.Fuente(tamaño, FontStyle.Bold)).Width > ancho)
            {
                tamaño -= 0.5f;
            }
            var fuenteTitulo = Tema.Fuente(tamaño, FontStyle.Bold);
            float altoTitulo = Math.Min(64, Dibujo.Medir(g, Titulo, fuenteTitulo, ancho).Height);
            Dibujo.Texto(g, Titulo, fuenteTitulo, Tema.Texto, new RectangleF(x, pastilla.Bottom + 12, ancho, altoTitulo + 2));
            float yDescripcion = pastilla.Bottom + 18 + altoTitulo;
            Dibujo.Texto(g, MotorTriaje.Descripcion(_nivel), Tema.Fuente(11f), Dibujo.Alfa(Tema.Texto, 0.8),
                new RectangleF(x, yDescripcion, ancho, r.Bottom - yDescripcion - 12));
        }
    }
}
