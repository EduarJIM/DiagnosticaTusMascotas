using System.ComponentModel;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Campo de texto redondeado (rounded-xl) con anillo verde al recibir el foco.
    /// Envuelve un TextBox sin borde.
    /// </summary>
    [DefaultEvent("TextChanged"), DefaultProperty("Text")]
    public class CajaTexto : ControlPintado
    {
        private readonly TextBox _caja = new TextBox();
        private readonly ValorAnimado _foco;

        public CajaTexto()
        {
            _foco = new ValorAnimado(this, 200);
            _caja.BorderStyle = BorderStyle.None;
            _caja.Font = Tema.Fuente(10.5f);
            _caja.GotFocus += (s, e) => _foco.IrA(1);
            _caja.LostFocus += (s, e) => _foco.IrA(0);
            _caja.TextChanged += (s, e) => OnTextChanged(EventArgs.Empty);
            Controls.Add(_caja);
            Size = new Size(300, 44);
            Cursor = Cursors.IBeam;
            AplicarColores();
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue("")]
        public override string Text
        {
            get => _caja.Text;
            set => _caja.Text = value ?? "";
        }

        [Category("Apariencia"), DefaultValue("")]
        public string PlaceholderText
        {
            get => _caja.PlaceholderText;
            set => _caja.PlaceholderText = value;
        }

        [Category("Comportamiento"), DefaultValue(false)]
        public bool Multiline
        {
            get => _caja.Multiline;
            set
            {
                _caja.Multiline = value;
                _caja.ScrollBars = value ? ScrollBars.Vertical : ScrollBars.None;
                Ubicar();
            }
        }

        [Category("Comportamiento"), DefaultValue(false)]
        public bool ReadOnly
        {
            get => _caja.ReadOnly;
            set { _caja.ReadOnly = value; AplicarColores(); }
        }

        [Category("Comportamiento"), DefaultValue(32767)]
        public int MaxLength
        {
            get => _caja.MaxLength;
            set => _caja.MaxLength = value;
        }

        /// <summary>
        /// TextBox interno, por si se necesita algo más avanzado.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TextBox Caja => _caja;

        public new void Focus() => _caja.Focus();

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            _caja.Focus();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Ubicar();
        }

        protected override void AlCambiarTema() => AplicarColores();

        private void AplicarColores()
        {
            _caja.BackColor = ReadOnly ? Tema.Apagado : Tema.Entrada;
            _caja.ForeColor = ReadOnly ? Tema.TextoApagado : Tema.Texto;
            Tema.BarrasOscuras(_caja);
        }

        private void Ubicar()
        {
            if (_caja.Multiline)
            {
                _caja.Location = new Point(14, 11);
                _caja.Size = new Size(Math.Max(10, Width - 22), Math.Max(10, Height - 22));
            }
            else
            {
                _caja.Location = new Point(14, (Height - _caja.Height) / 2);
                _caja.Width = Math.Max(10, Width - 28);
            }
        }

        protected override void Pintar(Graphics g)
        {
            double f = _foco.Valor;
            var r = new RectangleF(1.5f, 1.5f, Width - 4, Height - 4);
            if (f > 0.01)
            {
                Dibujo.BordeRedondeado(g, Dibujo.Alfa(Tema.Primario, 0.30 * f), RectangleF.Inflate(r, 1, 1), 13, 2);
            }
            Dibujo.RellenarRedondeado(g, _caja.BackColor, r, 12);
            Dibujo.BordeRedondeado(g, Dibujo.Mezclar(Tema.BordeSobre(_caja.BackColor), Dibujo.Alfa(Tema.Primario, 0.6), f), r, 12);
        }
    }
}
