using System.ComponentModel;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Opción de la barra lateral (NavItem). La opción activa se pinta en verde
    /// con brillo; las demás se iluminan al pasar el ratón.
    /// </summary>
    [DefaultEvent("Click")]
    public class ElementoNav : ControlPintado
    {
        private readonly ValorAnimado _hover;
        private readonly ValorAnimado _activo;
        private string _icono = "";

        public ElementoNav()
        {
            _hover = new ValorAnimado(this, 200);
            _activo = new ValorAnimado(this, 250);
            Cursor = Cursors.Hand;
            Size = new Size(224, 46);
            Font = Tema.Fuente(10.5f);
        }

        [Category("Apariencia"), DefaultValue("")]
        public string Icono
        {
            get => _icono;
            set { _icono = value ?? ""; Invalidate(); }
        }

        [Category("Apariencia"), DefaultValue(false)]
        public bool Activo
        {
            get => _activo.Valor > 0.5;
            set => _activo.IrA(value ? 1 : 0);
        }

        /// <summary>
        /// Estilo rojo para "Salir" (text-destructive/70).
        /// </summary>
        [Category("Apariencia"), DefaultValue(false)]
        public bool Peligro { get; set; }

        protected override void OnMouseEnter(EventArgs e) { base.OnMouseEnter(e); _hover.IrA(1); }
        protected override void OnMouseLeave(EventArgs e) { base.OnMouseLeave(e); _hover.IrA(0); }
        protected override void OnTextChanged(EventArgs e) { base.OnTextChanged(e); Invalidate(); }

        protected override void Pintar(Graphics g)
        {
            double h = _hover.Valor;
            double a = _activo.Valor;
            var r = new RectangleF(2, 3, Width - 4, Height - 8);
            const float radio = 10;

            Color texto;
            float desplazamiento = 0;
            if (Peligro)
            {
                Dibujo.RellenarRedondeado(g, Dibujo.Alfa(Tema.Destructivo, 0.10 * h), r, radio);
                texto = Dibujo.Mezclar(Dibujo.Alfa(Tema.Destructivo, 0.7), Tema.Destructivo, h);
                desplazamiento = (float)(-4 * h);
            }
            else
            {
                Color acentoTexto = Tema.Oscuro ? Tema.Texto : Tema.Primario;
                Dibujo.RellenarRedondeado(g, Dibujo.Alfa(Tema.LateralAcento, h * (1 - a)), r, radio);
                if (a > 0.01)
                {
                    Dibujo.Sombra(g, r, radio, Dibujo.Alfa(Tema.Primario, 0.25 * a), 6, 4);
                    Dibujo.RellenarRedondeado(g, Dibujo.Alfa(Tema.Primario, a), r, radio);
                }
                texto = Dibujo.Mezclar(Dibujo.Mezclar(Tema.TextoApagado, acentoTexto, h), Color.White, a);
            }

            Iconos.Dibujar(g, _icono, new RectangleF(r.X + 14 + desplazamiento, r.Y, 22, r.Height), texto, 18);
            Dibujo.Texto(g, Text, Font, texto, new RectangleF(r.X + 48, r.Y, r.Width - 52, r.Height),
                StringAlignment.Near, StringAlignment.Center, true);
        }
    }
}
