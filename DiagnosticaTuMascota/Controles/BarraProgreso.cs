using System.ComponentModel;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Barra de progreso del asistente (h-1.5 rounded-full). Al mostrarse se anima
    /// desde <see cref="ValorInicial"/> hasta <see cref="Valor"/>.
    /// </summary>
    public class BarraProgreso : ControlPintado
    {
        private readonly ValorAnimado _relleno;
        private int _valor;

        public BarraProgreso()
        {
            _relleno = new ValorAnimado(this, 700);
            Size = new Size(128, 8);
        }

        [Category("Comportamiento"), DefaultValue(0)]
        public int ValorInicial { get; set; }

        [Category("Comportamiento"), DefaultValue(0)]
        public int Valor
        {
            get => _valor;
            set
            {
                _valor = Math.Max(0, Math.Min(100, value));
                if (IsHandleCreated && !DesignMode)
                {
                    _relleno.IrA(_valor);
                }
                else
                {
                    _relleno.Fijar(_valor);
                }
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!DesignMode)
            {
                _relleno.Fijar(ValorInicial);
                _relleno.IrA(_valor);
            }
        }

        protected override void Pintar(Graphics g)
        {
            var r = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
            float radio = r.Height / 2;
            Dibujo.RellenarRedondeado(g, Tema.Apagado, r, radio);
            float ancho = (float)(r.Width * _relleno.Valor / 100.0);
            if (ancho > 1)
            {
                Dibujo.RellenarRedondeado(g, Tema.Primario, new RectangleF(r.X, r.Y, ancho, r.Height), radio);
            }
            Dibujo.BordeRedondeado(g, Tema.Borde, r, radio);
        }
    }
}
