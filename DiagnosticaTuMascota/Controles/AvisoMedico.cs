using System.ComponentModel;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Variantes del aviso.
    /// </summary>
    public enum VarianteAviso
    {
        /// <summary>Rojo con icono de alerta (DisclaimerBanner).</summary>
        Peligro,
        /// <summary>Caja gris con texto centrado (aviso legal del resultado).</summary>
        Neutra,
        /// <summary>Ámbar con icono (advertencias).</summary>
        Advertencia
    }

    /// <summary>
    /// Aviso médico importante: recuerda que la app no sustituye al veterinario.
    /// </summary>
    public class AvisoMedico : ControlPintado
    {
        public AvisoMedico()
        {
            Size = new Size(500, 130);
        }

        [Category("Apariencia"), DefaultValue(VarianteAviso.Peligro)]
        public VarianteAviso Variante { get; set; } = VarianteAviso.Peligro;

        [Category("Apariencia"), DefaultValue("Aviso Médico Importante")]
        public string Titulo { get; set; } = "Aviso Médico Importante";

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
        public override string Text
        {
            get => base.Text;
            set { base.Text = value; Invalidate(); }
        }

        protected override void Pintar(Graphics g)
        {
            var r = new RectangleF(1, 1, Width - 3, Height - 3);
            const float radio = 18;

            if (Variante == VarianteAviso.Neutra)
            {
                Dibujo.RellenarRedondeado(g, Dibujo.Alfa(Tema.Apagado, 0.5), r, radio);
                Dibujo.BordeRedondeado(g, Tema.Borde, r, radio);
                var interior = RectangleF.Inflate(r, -24, -8);
                using (var formato = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                using (var pincel = new SolidBrush(Tema.TextoApagado))
                {
                    g.DrawString($"{Titulo}: {Text}", Tema.Fuente(9.5f), pincel, interior, formato);
                }
                return;
            }

            Color acento = Variante == VarianteAviso.Advertencia ? Tema.Ambar : Tema.Destructivo;
            Dibujo.RellenarRedondeado(g, Dibujo.Alfa(acento, 0.05), r, radio);
            var estado = g.Save();
            using (var ruta = Dibujo.Redondeado(r, radio))
            {
                g.SetClip(ruta);
                Dibujo.Brillo(g, new PointF(r.Right - 40, r.Y + 30), 90, Dibujo.Alfa(Color.FromArgb(255, 82, 82), 0.12));
            }
            g.Restore(estado);
            Dibujo.BordeRedondeado(g, Dibujo.Alfa(acento, 0.20), r, radio);

            var caja = new RectangleF(r.X + 22, r.Y + 22, 40, 40);
            Dibujo.RellenarRedondeado(g, Dibujo.Alfa(acento, 0.10), caja, 10);
            Iconos.Dibujar(g, Iconos.Alerta, caja, acento, 20);

            float x = caja.Right + 16;
            Dibujo.Texto(g, Titulo, Tema.Fuente(11f, FontStyle.Bold), Tema.Texto, new RectangleF(x, r.Y + 20, r.Right - x - 20, 24), unaLinea: true);
            Dibujo.Texto(g, Text, Tema.Fuente(9.5f), Tema.TextoApagado, new RectangleF(x, r.Y + 48, r.Right - x - 20, r.Height - 56));
        }
    }
}
