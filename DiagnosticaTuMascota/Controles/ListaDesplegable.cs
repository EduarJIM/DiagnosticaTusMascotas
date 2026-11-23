using System.ComponentModel;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Lista desplegable redondeada. Envuelve un ComboBox, oculta su flecha
    /// original y dibuja una propia; los elementos se pintan con los colores del tema.
    /// </summary>
    [DefaultEvent("SelectedIndexChanged")]
    public class ListaDesplegable : ControlPintado
    {
        private readonly ComboBox _combo = new ComboBox();
        private readonly Panel _recorte = new Panel();
        private readonly ValorAnimado _foco;
        private readonly ValorAnimado _hover;

        public event EventHandler SelectedIndexChanged;

        public ListaDesplegable()
        {
            _foco = new ValorAnimado(this, 200);
            _hover = new ValorAnimado(this, 200);
            _combo.FlatStyle = FlatStyle.Flat;
            _combo.DropDownStyle = ComboBoxStyle.DropDownList;
            _combo.DrawMode = DrawMode.OwnerDrawFixed;
            _combo.Font = Tema.Fuente(10.5f);
            _combo.ItemHeight = 26;
            _combo.DrawItem += Combo_DrawItem;
            _combo.SelectedIndexChanged += (s, e) => SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
            _combo.TextChanged += (s, e) => OnTextChanged(EventArgs.Empty);
            _combo.GotFocus += (s, e) => _foco.IrA(1);
            _combo.LostFocus += (s, e) => _foco.IrA(0);
            _combo.DropDownClosed += (s, e) => Invalidate();
            _recorte.Controls.Add(_combo);
            Controls.Add(_recorte);
            Size = new Size(300, 44);
            Cursor = Cursors.Hand;
            AplicarColores();
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ComboBox.ObjectCollection Items => _combo.Items;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedItem
        {
            get => _combo.SelectedItem;
            set => _combo.SelectedItem = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get => _combo.SelectedIndex;
            set => _combo.SelectedIndex = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get => _combo.Text;
            set => _combo.Text = value;
        }

        /// <summary>
        /// Permite escribir un valor que no está en la lista.
        /// </summary>
        [Category("Comportamiento"), DefaultValue(false)]
        public bool Editable
        {
            get => _combo.DropDownStyle == ComboBoxStyle.DropDown;
            set
            {
                _combo.DropDownStyle = value ? ComboBoxStyle.DropDown : ComboBoxStyle.DropDownList;
                Ubicar();
            }
        }

        protected override void OnMouseEnter(EventArgs e) { base.OnMouseEnter(e); _hover.IrA(1); }
        protected override void OnMouseLeave(EventArgs e) { base.OnMouseLeave(e); _hover.IrA(0); }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            _combo.Focus();
            _combo.DroppedDown = true;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Ubicar();
        }

        protected override void AlCambiarTema() => AplicarColores();

        private void AplicarColores()
        {
            _combo.BackColor = Tema.Entrada;
            _combo.ForeColor = Tema.Texto;
            _recorte.BackColor = Tema.Entrada;
        }

        private void Ubicar()
        {
            _recorte.Location = new Point(12, (Height - _combo.Height) / 2 + 2);
            _recorte.Size = new Size(Math.Max(20, Width - 52), _combo.Height - 4);
            _combo.Location = new Point(-2, -2);
            _combo.Width = _recorte.Width + 26;
        }

        private void Combo_DrawItem(object sender, DrawItemEventArgs e)
        {
            bool enCampo = (e.State & DrawItemState.ComboBoxEdit) != 0;
            bool resaltado = (e.State & DrawItemState.Selected) != 0 && !enCampo;
            var fondo = enCampo ? Tema.Entrada : resaltado ? Dibujo.Sobre(Tema.Emergente, Tema.Primario, 0.25) : Tema.Emergente;
            using (var pincel = new SolidBrush(fondo))
            {
                e.Graphics.FillRectangle(pincel, e.Bounds);
            }
            if (e.Index >= 0)
            {
                var texto = _combo.GetItemText(_combo.Items[e.Index]);
                TextRenderer.DrawText(e.Graphics, texto, _combo.Font, new Rectangle(e.Bounds.X + 6, e.Bounds.Y, e.Bounds.Width - 6, e.Bounds.Height),
                    Tema.Texto, TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis);
            }
        }

        protected override void Pintar(Graphics g)
        {
            double f = Math.Max(_foco.Valor, _combo.DroppedDown ? 1 : 0);
            var r = new RectangleF(1.5f, 1.5f, Width - 4, Height - 4);
            if (f > 0.01)
            {
                Dibujo.BordeRedondeado(g, Dibujo.Alfa(Tema.Primario, 0.30 * f), RectangleF.Inflate(r, 1, 1), 13, 2);
            }
            Dibujo.RellenarRedondeado(g, Tema.Entrada, r, 12);
            Dibujo.BordeRedondeado(g, Dibujo.Mezclar(Tema.BordeSobre(Tema.Entrada), Dibujo.Alfa(Tema.Primario, 0.6), Math.Max(f, _hover.Valor * 0.5)), r, 12);
            var flecha = new RectangleF(Width - 36, 0, 24, Height);
            var estado = g.Save();
            g.TranslateTransform(flecha.X + 12, Height / 2f);
            g.RotateTransform(90);
            Iconos.Dibujar(g, Iconos.Siguiente, new RectangleF(-10, -10, 20, 20), Tema.TextoApagado, 12);
            g.Restore(estado);
        }
    }
}
