using System.ComponentModel;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Campo de fecha redondeado. Al hacer clic abre un calendario desplegable.
    /// </summary>
    [DefaultEvent("ValueChanged")]
    public class SelectorFecha : ControlPintado
    {
        private readonly ValorAnimado _hover;
        private DateTime _valor = DateTime.Today;

        public event EventHandler ValueChanged;

        public SelectorFecha()
        {
            _hover = new ValorAnimado(this, 200);
            Cursor = Cursors.Hand;
            Size = new Size(200, 44);
            Font = Tema.Fuente(10.5f);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime Value
        {
            get => _valor;
            set
            {
                _valor = value.Date;
                Invalidate();
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        protected override void OnMouseEnter(EventArgs e) { base.OnMouseEnter(e); _hover.IrA(1); }
        protected override void OnMouseLeave(EventArgs e) { base.OnMouseLeave(e); _hover.IrA(0); }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            var calendario = new MonthCalendar { MaxSelectionCount = 1, SelectionStart = _valor };
            var anfitrion = new ToolStripControlHost(calendario) { Margin = Padding.Empty, Padding = Padding.Empty };
            var desplegable = new ToolStripDropDown { Padding = Padding.Empty };
            desplegable.Items.Add(anfitrion);
            calendario.DateSelected += (s, a) =>
            {
                Value = a.Start;
                desplegable.Close();
            };
            desplegable.Closed += (s, a) => desplegable.Dispose();
            desplegable.Show(this, new Point(0, Height + 2));
        }

        protected override void Pintar(Graphics g)
        {
            var r = new RectangleF(1.5f, 1.5f, Width - 4, Height - 4);
            Dibujo.RellenarRedondeado(g, Tema.Entrada, r, 12);
            Dibujo.BordeRedondeado(g, Dibujo.Mezclar(Tema.BordeSobre(Tema.Entrada), Dibujo.Alfa(Tema.Primario, 0.6), _hover.Valor * 0.6), r, 12);
            Dibujo.Texto(g, _valor.ToString("dd/MM/yyyy"), Font, Tema.Texto, new RectangleF(14, 0, Width - 50, Height),
                StringAlignment.Near, StringAlignment.Center, true);
            Iconos.Dibujar(g, Iconos.Calendario, new RectangleF(Width - 40, 0, 26, Height), Tema.TextoApagado, 16);
        }
    }
}
