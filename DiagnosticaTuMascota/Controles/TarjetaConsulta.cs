using DiagnosticaTuMascota.Core;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Fila del historial: icono de documento, mascota, insignia de severidad,
    /// fecha y estado. Toda la fila se puede pulsar para abrir el análisis.
    /// </summary>
    public class TarjetaConsulta : ControlPintado
    {
        private readonly ValorAnimado _hover;
        private readonly BotonRedondeado _btnEliminar;

        public event EventHandler Abrir;
        public event EventHandler Eliminar;

        public Consulta Consulta { get; }

        public TarjetaConsulta(Consulta consulta)
        {
            Consulta = consulta;
            _hover = new ValorAnimado(this, 220);
            Cursor = Cursors.Hand;
            Size = new Size(800, 100);
            Margin = new Padding(0, 0, 0, 14);

            _btnEliminar = new BotonRedondeado
            {
                Estilo = EstiloBoton.Fantasma,
                Icono = Iconos.Eliminar,
                Size = new Size(44, 44),
                Radio = 10,
                Font = Tema.Fuente(9f)
            };
            _btnEliminar.Location = new Point(Width - 110, (Height - 44) / 2);
            _btnEliminar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnEliminar.MouseEnter += (s, e) => _hover.IrA(1);
            _btnEliminar.Click += (s, e) => Eliminar?.Invoke(this, EventArgs.Empty);
            Controls.Add(_btnEliminar);
            AlCambiarTema();
        }

        protected override void AlCambiarTema() => BackColor = Tema.Tarjeta;

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Abrir?.Invoke(this, EventArgs.Empty);
        }

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
            var r = new RectangleF(1, 1, Width - 3, Height - 4);
            Dibujo.Sombra(g, r, 18, Color.FromArgb(Tema.Oscuro ? 40 : 15, 0, 0, 0), 3, 2);
            Dibujo.RellenarRedondeado(g, Tema.Tarjeta, r, 18);
            Dibujo.BordeRedondeado(g, Dibujo.Mezclar(Tema.Borde, Dibujo.Alfa(Tema.Primario, 0.5), h), r, 18);

            float lado = 56 * (1 + 0.1f * (float)h);
            var caja = new RectangleF(24 + (56 - lado) / 2, (Height - lado) / 2, lado, lado);
            Dibujo.RellenarRedondeado(g, Dibujo.Alfa(Tema.Primario, 0.10), caja, 16);
            Iconos.Dibujar(g, Iconos.Documento, caja, Tema.Primario, 26 * (1 + 0.1f * (float)h));

            float x = 100;
            var fuenteNombre = Tema.Fuente(15f, FontStyle.Bold);
            float anchoNombre = Math.Min(g.MeasureString(Consulta.Mascota, fuenteNombre).Width, Width - 400);
            Dibujo.Texto(g, Consulta.Mascota, fuenteNombre, Tema.Texto, new RectangleF(x, 18, anchoNombre + 6, 32), StringAlignment.Near, StringAlignment.Center, true);

            var color = MotorTriaje.ColorNivel(Consulta.Severidad);
            var fuenteInsignia = Tema.Fuente(7.5f, FontStyle.Bold);
            var textoInsignia = Consulta.SeveridadTexto;
            float anchoInsignia = g.MeasureString(textoInsignia, fuenteInsignia).Width + 16;
            var insignia = new RectangleF(x + anchoNombre + 12, 24, anchoInsignia, 20);
            Dibujo.RellenarRedondeado(g, Dibujo.Alfa(color, 0.12), insignia, 10);
            Dibujo.Texto(g, textoInsignia, fuenteInsignia, color, insignia, StringAlignment.Center, StringAlignment.Center);

            var fuente = Tema.Fuente(9.5f);
            Iconos.Dibujar(g, Iconos.Reloj, new RectangleF(x, 54, 16, 22), Tema.TextoApagado, 13);
            Dibujo.Texto(g, Consulta.FechaTexto, fuente, Tema.TextoApagado, new RectangleF(x + 20, 54, 140, 22), StringAlignment.Near, StringAlignment.Center, true);
            Iconos.Dibujar(g, Iconos.CheckCirculo, new RectangleF(x + 160, 54, 16, 22), Tema.Primario, 13);
            Dibujo.Texto(g, "Completado", Tema.Fuente(9.5f, FontStyle.Bold), Tema.Primario, new RectangleF(x + 180, 54, 100, 22), StringAlignment.Near, StringAlignment.Center, true);
            Dibujo.Texto(g, Consulta.SintomasTexto, fuente, Dibujo.Alfa(Tema.TextoApagado, 0.8), new RectangleF(x + 290, 54, Width - x - 420, 22), StringAlignment.Near, StringAlignment.Center, true);

            Iconos.Dibujar(g, Iconos.Siguiente, new RectangleF(Width - 56 + 3 * (float)h, 0, 30, Height), Dibujo.Mezclar(Tema.TextoApagado, Tema.Primario, h), 18);
        }
    }
}
