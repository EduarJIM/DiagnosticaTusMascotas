using System.ComponentModel;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Mensaje centrado con icono tenue para listas vacías.
    /// </summary>
    public class EstadoVacio : ControlPintado
    {
        public EstadoVacio()
        {
            Size = new Size(500, 140);
        }

        [Category("Apariencia"), DefaultValue(Iconos.Huella)]
        public string Icono { get; set; } = Iconos.Huella;

        protected override void OnTextChanged(EventArgs e) { base.OnTextChanged(e); Invalidate(); }

        protected override void Pintar(Graphics g)
        {
            Iconos.Dibujar(g, Icono, new RectangleF(0, 16, Width, 56), Dibujo.Alfa(Tema.TextoApagado, 0.25), 48);
            Dibujo.Texto(g, Text, Tema.Fuente(10.5f), Tema.TextoApagado, new RectangleF(0, 82, Width, 40), StringAlignment.Center);
        }
    }
}
