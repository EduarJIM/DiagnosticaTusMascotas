using DiagnosticaTuMascota.Core;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Tarjeta de alerta: fondo rojo o ámbar según la prioridad, insignia,
    /// mascota, fecha y hora, mensaje y botón para eliminar.
    /// </summary>
    public class TarjetaRecordatorio : ControlPintado
    {
        private readonly ValorAnimado _hover;
        private readonly BotonRedondeado _btnEliminar;

        public event EventHandler Eliminar;

        public Recordatorio Recordatorio { get; }

        public TarjetaRecordatorio(Recordatorio recordatorio)
        {
            Recordatorio = recordatorio;
            _hover = new ValorAnimado(this, 220);
            Size = new Size(800, 130);
            Margin = new Padding(0, 0, 0, 14);

            _btnEliminar = new BotonRedondeado
            {
                Estilo = EstiloBoton.Fantasma,
                Icono = Iconos.Cerrar,
                Size = new Size(40, 40),
                Radio = 10,
                Font = Tema.Fuente(9f)
            };
            _btnEliminar.Location = new Point(Width - 60, 16);
            _btnEliminar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnEliminar.Click += (s, e) => Eliminar?.Invoke(this, EventArgs.Empty);
            Controls.Add(_btnEliminar);
            AlCambiarTema();
        }

        private Color Acento => Recordatorio.Prioridad switch
        {
            "Alta" => Tema.Rojo,
            "Baja" => Tema.Azul,
            _ => Tema.Ambar
        };

        private bool Pasado => Recordatorio.FechaHora < DateTime.Now;

        protected override void AlCambiarTema() => BackColor = Dibujo.Sobre(Tema.Fondo, Acento, 0.05);

        protected override void OnMouseEnter(EventArgs e) { base.OnMouseEnter(e); _hover.IrA(1); }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (!ClientRectangle.Contains(PointToClient(Cursor.Position)))
            {
                _hover.IrA(0);
            }
        }

        protected override void Pintar(Graphics g)
        {
            double h = _hover.Valor;
            double opacidad = Pasado ? 0.55 : 1;
            var r = new RectangleF(1, 1, Width - 3, Height - 4);
            Dibujo.RellenarRedondeado(g, BackColor, r, 18);
            Dibujo.BordeRedondeado(g, Dibujo.Alfa(Acento, 0.20 + 0.30 * h), r, 18);

            var circulo = new RectangleF(24, 24, 48, 48);
            using (var pincel = new SolidBrush(Dibujo.Alfa(Acento, 0.20 * opacidad)))
            {
                g.FillEllipse(pincel, circulo);
            }
            Iconos.Dibujar(g, Pasado ? Iconos.CheckCirculo : Iconos.Campana, circulo, Dibujo.Alfa(Acento, opacidad), 22);

            float x = 92;
            var fuenteTitulo = Tema.Fuente(13f, FontStyle.Bold);
            float anchoTitulo = Math.Min(g.MeasureString(Recordatorio.Titulo, fuenteTitulo).Width, Width - 320);
            Dibujo.Texto(g, Recordatorio.Titulo, fuenteTitulo, Dibujo.Alfa(Tema.Texto, opacidad), new RectangleF(x, 20, anchoTitulo + 6, 28), StringAlignment.Near, StringAlignment.Center, true);

            var textoInsignia = Pasado ? "COMPLETADO" : $"PRIORIDAD {Recordatorio.Prioridad.ToUpper()}";
            var fuenteInsignia = Tema.Fuente(7.5f, FontStyle.Bold);
            float anchoInsignia = g.MeasureString(textoInsignia, fuenteInsignia).Width + 16;
            var insignia = new RectangleF(x + anchoTitulo + 12, 25, anchoInsignia, 19);
            Dibujo.RellenarRedondeado(g, Pasado ? Tema.Apagado : Acento, insignia, 9.5f);
            Dibujo.Texto(g, textoInsignia, fuenteInsignia, Pasado ? Tema.TextoApagado : Color.White, insignia, StringAlignment.Center, StringAlignment.Center);

            Dibujo.Texto(g, $"Mascota: {(string.IsNullOrWhiteSpace(Recordatorio.Mascota) ? "—" : Recordatorio.Mascota)}",
                Tema.Fuente(10f, FontStyle.Bold), Dibujo.Alfa(Tema.Texto, opacidad), new RectangleF(x, 50, Width - x - 80, 22), unaLinea: true);

            Iconos.Dibujar(g, Iconos.Reloj, new RectangleF(x, 72, 18, 22), Dibujo.Alfa(Tema.Ambar, opacidad), 14);
            Dibujo.Texto(g, $"{Recordatorio.FechaTexto} a las {Recordatorio.HoraTexto}", Tema.Fuente(9.5f, FontStyle.Bold),
                Dibujo.Alfa(Tema.Ambar, opacidad), new RectangleF(x + 22, 72, 300, 22), StringAlignment.Near, StringAlignment.Center, true);

            Dibujo.Texto(g, Recordatorio.Mensaje, Tema.Fuente(9.5f), Tema.TextoApagado, new RectangleF(x, 96, Width - x - 80, 24), unaLinea: true);
        }
    }
}
