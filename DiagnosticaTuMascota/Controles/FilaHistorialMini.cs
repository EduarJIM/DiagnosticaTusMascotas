using DiagnosticaTuMascota.Core;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Fila compacta de una consulta, usada en el perfil de la mascota
    /// ("Historial de Análisis (Automático)").
    /// </summary>
    public class FilaHistorialMini : ControlPintado
    {
        private readonly Consulta _consulta;

        public FilaHistorialMini(Consulta consulta)
        {
            _consulta = consulta;
            Size = new Size(640, 78);
            Margin = new Padding(0, 0, 0, 10);
        }

        protected override void Pintar(Graphics g)
        {
            var r = new RectangleF(1, 1, Width - 3, Height - 3);
            Dibujo.RellenarRedondeado(g, Dibujo.Alfa(Tema.Apagado, 0.3), r, 14);
            Dibujo.BordeRedondeado(g, Tema.Borde, r, 14);

            var caja = new RectangleF(16, 16, 40, 40);
            Dibujo.RellenarRedondeado(g, Dibujo.Alfa(Tema.Primario, 0.10), caja, 10);
            Iconos.Dibujar(g, Iconos.Documento, caja, Tema.Primario, 20);

            var fuenteFecha = Tema.Fuente(10f, FontStyle.Bold);
            float anchoFecha = g.MeasureString(_consulta.FechaTexto, fuenteFecha).Width;
            Dibujo.Texto(g, _consulta.FechaTexto, fuenteFecha, Tema.Texto, new RectangleF(72, 14, anchoFecha + 4, 22), unaLinea: true);

            var color = MotorTriaje.ColorNivel(_consulta.Severidad);
            var fuenteInsignia = Tema.Fuente(7f, FontStyle.Bold);
            float anchoInsignia = g.MeasureString(_consulta.SeveridadTexto, fuenteInsignia).Width + 12;
            var insignia = new RectangleF(72 + anchoFecha + 8, 16, anchoInsignia, 18);
            Dibujo.RellenarRedondeado(g, Dibujo.Alfa(color, 0.12), insignia, 9);
            Dibujo.Texto(g, _consulta.SeveridadTexto, fuenteInsignia, color, insignia, StringAlignment.Center, StringAlignment.Center);

            Dibujo.Texto(g, "Síntomas: " + _consulta.SintomasTexto, Tema.Fuente(9f), Tema.TextoApagado,
                new RectangleF(72, 40, Width - 90, 34));
        }
    }
}
