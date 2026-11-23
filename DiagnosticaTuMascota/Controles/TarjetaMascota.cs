using DiagnosticaTuMascota.Core;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Tarjeta de una mascota: icono de huella, nombre, raza y edad, datos
    /// clínicos y botones de editar, eliminar y ver perfil.
    /// </summary>
    public class TarjetaMascota : ControlPintado
    {
        private readonly ValorAnimado _hover;
        private readonly BotonRedondeado _btnEditar;
        private readonly BotonRedondeado _btnEliminar;
        private readonly BotonRedondeado _btnPerfil;

        public event EventHandler Editar;
        public event EventHandler Eliminar;

        public Mascota Mascota { get; }

        public TarjetaMascota(Mascota mascota)
        {
            Mascota = mascota;
            _hover = new ValorAnimado(this, 250);
            Size = new Size(320, 330);
            Margin = new Padding(0, 0, 20, 20);

            _btnEditar = CrearBotonIcono(Iconos.Editar, Tema.Primario);
            _btnEliminar = CrearBotonIcono(Iconos.Eliminar, Tema.Destructivo);
            _btnPerfil = new BotonRedondeado
            {
                Estilo = EstiloBoton.Suave,
                Text = "Ver perfil completo",
                Icono = Iconos.Siguiente,
                IconoDerecha = true,
                Font = Tema.Fuente(9.5f, FontStyle.Bold)
            };

            _btnEditar.Click += (s, e) => Editar?.Invoke(this, EventArgs.Empty);
            _btnPerfil.Click += (s, e) => Editar?.Invoke(this, EventArgs.Empty);
            _btnEliminar.Click += (s, e) => Eliminar?.Invoke(this, EventArgs.Empty);

            _btnEliminar.Location = new Point(Width - 24 - 40, 20);
            _btnEditar.Location = new Point(Width - 24 - 84, 20);
            _btnPerfil.Bounds = new Rectangle(20, Height - 72, Width - 40, 50);
            _btnEliminar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnEditar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnPerfil.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            Controls.Add(_btnEditar);
            Controls.Add(_btnEliminar);
            Controls.Add(_btnPerfil);
            AlCambiarTema();
        }

        private static BotonRedondeado CrearBotonIcono(string icono, Color acento) => new BotonRedondeado
        {
            Estilo = EstiloBoton.Icono,
            Icono = icono,
            ColorAcento = acento,
            Size = new Size(40, 40),
            Radio = 9,
            Font = Tema.Fuente(8.5f)
        };

        protected override void AlCambiarTema()
        {
            BackColor = Tema.Tarjeta;
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

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(FondoPadre);
        }

        protected override void Pintar(Graphics g)
        {
            double h = _hover.Valor;
            var r = new RectangleF(1, 1, Width - 3, Height - 4);
            Dibujo.Sombra(g, r, 24, h > 0.01 ? Dibujo.Alfa(Tema.Primario, 0.15 * h) : Color.FromArgb(Tema.Oscuro ? 50 : 20, 0, 0, 0), 6, 4);
            Dibujo.RellenarRedondeado(g, Tema.Tarjeta, r, 24);
            Dibujo.BordeRedondeado(g, Dibujo.Mezclar(Tema.Borde, Dibujo.Alfa(Tema.Primario, 0.4), h), r, 24);

            var caja = new RectangleF(24, 24, 56, 56);
            Dibujo.RellenarRedondeado(g, Dibujo.Alfa(Tema.Primario, 0.10), caja, 16);
            Iconos.Dibujar(g, Iconos.Huella, caja, Tema.Primario, 28);

            float anchoTexto = Width - 96 - 100;
            Dibujo.Texto(g, Mascota.Nombre, Tema.Fuente(17f, FontStyle.Bold), Tema.Texto, new RectangleF(94, 22, anchoTexto, 34), unaLinea: true);
            var detalle = string.Join(" • ", new[] { Mascota.Raza, Mascota.Edad }.Where(t => !string.IsNullOrWhiteSpace(t)));
            Dibujo.Texto(g, detalle, Tema.Fuente(9.5f, FontStyle.Bold), Tema.TextoApagado, new RectangleF(94, 56, Width - 110, 22), unaLinea: true);

            float y = 108;
            y = DibujarDato(g, Iconos.CheckCirculo, "Vacunas: ", Mascota.Vacunas, y);
            y = DibujarDato(g, Iconos.Actividad, "Antecedentes: ", Mascota.InfoClinica, y);
            DibujarDato(g, Iconos.Salud, "Veterinario: ", Mascota.Veterinario, y);

            using (var linea = new Pen(Tema.Borde))
            {
                g.DrawLine(linea, 24, Height - 88, Width - 24, Height - 88);
            }
        }

        private float DibujarDato(Graphics g, string icono, string etiqueta, string valor, float y)
        {
            Iconos.Dibujar(g, icono, new RectangleF(24, y, 18, 20), Tema.Primario, 14);
            var fuente = Tema.Fuente(9.5f);
            var negrita = Tema.Fuente(9.5f, FontStyle.Bold);
            float anchoEtiqueta = g.MeasureString(etiqueta, negrita).Width;
            Dibujo.Texto(g, etiqueta, negrita, Tema.Texto, new RectangleF(50, y, anchoEtiqueta + 4, 20), unaLinea: true);
            var area = new RectangleF(50 + anchoEtiqueta, y, Width - 74 - anchoEtiqueta, 40);
            Dibujo.Texto(g, string.IsNullOrWhiteSpace(valor) ? "—" : valor, fuente, Tema.TextoApagado, area);
            float alto = Math.Min(40, Dibujo.Medir(g, valor, fuente, area.Width).Height);
            return y + Math.Max(22, alto) + 10;
        }
    }
}
