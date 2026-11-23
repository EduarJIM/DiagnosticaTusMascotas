using System.ComponentModel;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Variantes de color de una tarjeta.
    /// </summary>
    public enum VarianteTarjeta
    {
        /// <summary>bg-card border-border.</summary>
        Normal,
        /// <summary>bg-muted/50 (cajas de información).</summary>
        Apagada,
        /// <summary>bg-primary/5 border-primary/20.</summary>
        Primaria,
        /// <summary>bg-red-500/5 border-red-500/20.</summary>
        Peligro
    }

    /// <summary>
    /// Contenedor con esquinas redondeadas, borde fino y sombra suave
    /// (bg-card border border-border rounded-3xl shadow-sm).
    /// </summary>
    public class PanelTarjeta : Panel
    {
        private VarianteTarjeta _variante = VarianteTarjeta.Normal;
        private int _radio = 24;

        public PanelTarjeta()
        {
            SetStyle(ControlStyles.UserPaint
                     | ControlStyles.AllPaintingInWmPaint
                     | ControlStyles.OptimizedDoubleBuffer
                     | ControlStyles.ResizeRedraw, true);
            Padding = new Padding(24);
            ActualizarColor();
            Tema.Cambiado += Tema_Cambiado;
        }

        [Category("Apariencia"), DefaultValue(VarianteTarjeta.Normal)]
        public VarianteTarjeta Variante
        {
            get => _variante;
            set { _variante = value; ActualizarColor(); Invalidate(); }
        }

        [Category("Apariencia"), DefaultValue(24)]
        public int Radio
        {
            get => _radio;
            set { _radio = value; Invalidate(); }
        }

        [Category("Apariencia"), DefaultValue(true)]
        public bool Sombra { get; set; } = true;

        /// <summary>
        /// El color de fondo lo decide el tema; no se guarda en el diseñador.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color BackColor
        {
            get => base.BackColor;
            set => base.BackColor = value;
        }

        private Color ColorBorde => _variante switch
        {
            VarianteTarjeta.Primaria => Dibujo.Alfa(Tema.Primario, 0.20),
            VarianteTarjeta.Peligro => Dibujo.Alfa(Tema.Rojo, 0.20),
            _ => Tema.Borde
        };

        private void ActualizarColor()
        {
            base.BackColor = _variante switch
            {
                VarianteTarjeta.Apagada => Dibujo.Sobre(Tema.Tarjeta, Tema.Apagado, 0.5),
                VarianteTarjeta.Primaria => Dibujo.Sobre(Tema.Tarjeta, Tema.Primario, 0.05),
                VarianteTarjeta.Peligro => Dibujo.Sobre(Tema.Tarjeta, Tema.Rojo, 0.05),
                _ => Tema.Tarjeta
            };
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            var padre = Parent;
            while (padre != null && padre.BackColor.A < 255)
            {
                padre = padre.Parent;
            }
            e.Graphics.Clear(padre?.BackColor ?? Tema.Fondo);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            Dibujo.Preparar(g);
            var r = new RectangleF(1, 1, Width - 3, Height - 4);
            if (Sombra)
            {
                Dibujo.Sombra(g, r, _radio, Color.FromArgb(Tema.Oscuro ? 60 : 25, 0, 0, 0), 4, 2);
            }
            Dibujo.RellenarRedondeado(g, BackColor, r, _radio);
            Dibujo.BordeRedondeado(g, ColorBorde, r, _radio);
        }

        private void Tema_Cambiado(object sender, EventArgs e)
        {
            ActualizarColor();
            Invalidate(true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Tema.Cambiado -= Tema_Cambiado;
            }
            base.Dispose(disposing);
        }
    }
}
