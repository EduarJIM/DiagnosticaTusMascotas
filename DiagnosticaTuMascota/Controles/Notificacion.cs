using System.ComponentModel;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Mensajes breves tipo "toast" (sonner en la web). Cualquier formulario
    /// puede llamar a <see cref="Notificaciones.Exito"/> y la ventana principal lo muestra.
    /// </summary>
    public static class Notificaciones
    {
        public static event Action<string, bool> Solicitada;

        public static void Exito(string mensaje) => Solicitada?.Invoke(mensaje, false);

        public static void Error(string mensaje) => Solicitada?.Invoke(mensaje, true);
    }

    /// <summary>
    /// Aviso que entra deslizándose desde la derecha y se va solo a los 3 segundos.
    /// </summary>
    [ToolboxItem(false)]
    public class Notificacion : ControlPintado
    {
        private readonly bool _esError;
        private double _desliz;

        public Notificacion(string mensaje, bool esError)
        {
            Text = mensaje;
            _esError = esError;
            Size = new Size(340, 64);
        }

        /// <summary>
        /// Anima la entrada, espera y anima la salida; luego se destruye.
        /// </summary>
        public void Mostrar()
        {
            Animador.Animar(this, 350, Suavizado.SalidaCubica, t => { _desliz = t; Invalidate(); }, () =>
                Animador.Animar(this, 300, Suavizado.EntradaSalidaSeno, t => { _desliz = 1 - t; Invalidate(); }, Dispose, 3000));
        }

        protected override void Pintar(Graphics g)
        {
            if (_desliz <= 0)
            {
                return;
            }
            float x = (float)((1 - _desliz) * 60);
            var r = new RectangleF(6 + x, 4, Width - 14, Height - 12);
            var acento = _esError ? Tema.Destructivo : Tema.Primario;
            Dibujo.Sombra(g, r, 14, Color.FromArgb((int)(80 * _desliz), 0, 0, 0), 5, 3);
            Dibujo.RellenarRedondeado(g, Dibujo.Alfa(Tema.Emergente, _desliz), r, 14);
            Dibujo.RellenarRedondeado(g, Dibujo.Alfa(acento, 0.12 * _desliz), r, 14);
            Dibujo.BordeRedondeado(g, Dibujo.Alfa(acento, 0.35 * _desliz), r, 14);
            Iconos.Dibujar(g, _esError ? Iconos.Alerta : Iconos.CheckCirculo, new RectangleF(r.X + 14, r.Y, 26, r.Height), Dibujo.Alfa(acento, _desliz), 18);
            Dibujo.Texto(g, Text, Tema.Fuente(10f, FontStyle.Bold), Dibujo.Alfa(Tema.Texto, _desliz),
                new RectangleF(r.X + 50, r.Y, r.Width - 60, r.Height), StringAlignment.Near, StringAlignment.Center, true);
        }
    }
}
