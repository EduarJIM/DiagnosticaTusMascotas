using System.ComponentModel;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Controles
{
    /// <summary>
    /// Variantes visuales del botón, equivalentes a las clases de Tailwind de la web.
    /// </summary>
    public enum EstiloBoton
    {
        /// <summary>bg-primary text-white con sombra de color.</summary>
        Primario,
        /// <summary>bg-muted, texto gris.</summary>
        Secundario,
        /// <summary>Sin fondo; fondo gris al pasar el ratón.</summary>
        Fantasma,
        /// <summary>Fondo rojo suave (bg-red-500/10).</summary>
        Peligro,
        /// <summary>bg-primary/10 text-primary; se rellena al pasar el ratón.</summary>
        Suave,
        /// <summary>Enlace de texto con flecha (botón "Volver").</summary>
        Enlace,
        /// <summary>Botón cuadrado solo con icono.</summary>
        Icono,
        /// <summary>Pastilla redonda de filtro (pestañas del historial).</summary>
        Pastilla
    }

    /// <summary>
    /// Botón redondeado con icono, animación de hover y elevación, basado en los
    /// botones de la web (rounded-xl, shadow-lg shadow-primary/20, hover:-translate-y-0.5).
    /// </summary>
    public class BotonRedondeado : Button
    {
        private readonly ValorAnimado _hover;
        private bool _presionado;
        private EstiloBoton _estilo = EstiloBoton.Primario;
        private string _icono = "";
        private bool _activo;
        private Color _colorAcento = Tema.Primario;

        public BotonRedondeado()
        {
            SetStyle(ControlStyles.UserPaint
                     | ControlStyles.AllPaintingInWmPaint
                     | ControlStyles.OptimizedDoubleBuffer
                     | ControlStyles.ResizeRedraw
                     | ControlStyles.SupportsTransparentBackColor, true);
            _hover = new ValorAnimado(this, 180);
            Cursor = Cursors.Hand;
            Font = Tema.Fuente(10f, FontStyle.Bold);
            Size = new Size(160, 44);
            Tema.Cambiado += Tema_Cambiado;
        }

        [Category("Apariencia"), DefaultValue(EstiloBoton.Primario)]
        public EstiloBoton Estilo
        {
            get => _estilo;
            set { _estilo = value; Invalidate(); }
        }

        /// <summary>
        /// Glifo de la clase <see cref="Iconos"/> (por ejemplo Iconos.Agregar).
        /// </summary>
        [Category("Apariencia"), DefaultValue("")]
        public string Icono
        {
            get => _icono;
            set { _icono = value ?? ""; Invalidate(); }
        }

        [Category("Apariencia"), DefaultValue(false)]
        [Description("Dibuja el icono a la derecha del texto.")]
        public bool IconoDerecha { get; set; }

        [Category("Apariencia"), DefaultValue(12)]
        public int Radio { get; set; } = 12;

        /// <summary>
        /// Color principal del botón (verde por defecto; ámbar, azul o rojo para otras acciones).
        /// </summary>
        [Category("Apariencia"), DefaultValue(typeof(Color), "16, 185, 129")]
        public Color ColorAcento
        {
            get => _colorAcento;
            set { _colorAcento = value; Invalidate(); }
        }

        /// <summary>
        /// Solo para el estilo Pastilla: indica si es el filtro seleccionado.
        /// </summary>
        [Category("Apariencia"), DefaultValue(false)]
        public bool Activo
        {
            get => _activo;
            set { _activo = value; Invalidate(); }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _hover.IrA(1);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _hover.IrA(0);
            _presionado = false;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _presionado = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _presionado = false;
            Invalidate();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Cursor = Enabled ? Cursors.Hand : Cursors.Default;
            Invalidate();
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
            OnPaintBackground(e);
            Dibujo.Preparar(g);

            double h = Enabled ? _hover.Valor : 0;
            float elevacion = _estilo == EstiloBoton.Primario ? (float)(h * 1.5) : 0;
            var r = new RectangleF(4, 4 - elevacion, Width - 8, Height - 8);
            if (_presionado)
            {
                r.Inflate(-1.5f, -1.5f);
            }
            float radio = _estilo == EstiloBoton.Pastilla ? r.Height / 2 : Radio;

            Color texto;
            switch (_estilo)
            {
                case EstiloBoton.Primario:
                    if (!Enabled)
                    {
                        Dibujo.RellenarRedondeado(g, Tema.Apagado, r, radio);
                        Dibujo.BordeRedondeado(g, Tema.Borde, r, radio);
                        texto = Tema.TextoApagado;
                        break;
                    }
                    Dibujo.Sombra(g, r, radio, Dibujo.Alfa(_colorAcento, 0.25 + 0.25 * h), 6, 4 + (float)h * 2);
                    Dibujo.RellenarRedondeado(g, Dibujo.Mezclar(_colorAcento, Color.White, 0.08 * h), r, radio);
                    texto = Color.White;
                    break;

                case EstiloBoton.Secundario:
                    Dibujo.RellenarRedondeado(g, Tema.Apagado, r, radio);
                    texto = Dibujo.Mezclar(Tema.TextoApagado, Tema.Texto, h);
                    break;

                case EstiloBoton.Fantasma:
                    Dibujo.RellenarRedondeado(g, Dibujo.Alfa(Tema.Apagado, h), r, radio);
                    texto = Dibujo.Mezclar(Tema.TextoApagado, Tema.Texto, h);
                    break;

                case EstiloBoton.Peligro:
                    Dibujo.RellenarRedondeado(g, Dibujo.Alfa(Tema.Destructivo, 0.10 + 0.10 * h), r, radio);
                    texto = Tema.Destructivo;
                    break;

                case EstiloBoton.Suave:
                    Dibujo.RellenarRedondeado(g, Dibujo.Mezclar(Dibujo.Alfa(_colorAcento, 0.10), _colorAcento, h), r, radio);
                    texto = Dibujo.Mezclar(_colorAcento, Color.White, h);
                    break;

                case EstiloBoton.Icono:
                    Dibujo.RellenarRedondeado(g, Dibujo.Alfa(Tema.Apagado, 0.6 + 0.4 * h), r, radio);
                    texto = Dibujo.Mezclar(Tema.TextoApagado, _colorAcento, h);
                    break;

                case EstiloBoton.Pastilla:
                    if (_activo)
                    {
                        Dibujo.Sombra(g, r, radio, Dibujo.Alfa(_colorAcento, 0.3), 6, 3);
                        Dibujo.RellenarRedondeado(g, _colorAcento, r, radio);
                        texto = Color.White;
                    }
                    else
                    {
                        Dibujo.RellenarRedondeado(g, Dibujo.Mezclar(Tema.Tarjeta, Tema.Apagado, h), r, radio);
                        Dibujo.BordeRedondeado(g, Tema.Borde, r, radio);
                        texto = Tema.TextoApagado;
                    }
                    break;

                default:
                    texto = Dibujo.Mezclar(Tema.TextoApagado, Tema.Texto, h);
                    break;
            }

            DibujarContenido(g, r, texto, h);
        }

        private void DibujarContenido(Graphics g, RectangleF r, Color color, double h)
        {
            bool hayTexto = !string.IsNullOrEmpty(Text) && _estilo != EstiloBoton.Icono;
            bool hayIcono = !string.IsNullOrEmpty(_icono);
            float tamañoIcono = Font.Size * 1.6f;
            float anchoTexto = hayTexto ? g.MeasureString(Text, Font).Width : 0;
            float separacion = hayTexto && hayIcono ? 6 : 0;
            float anchoIcono = hayIcono ? tamañoIcono + 2 : 0;
            float total = anchoTexto + separacion + anchoIcono;

            float x = _estilo == EstiloBoton.Enlace ? r.X : r.X + (r.Width - total) / 2;
            float desplazamientoIcono = 0;
            if (_estilo == EstiloBoton.Enlace)
            {
                desplazamientoIcono = (float)(-3 * h);
            }
            else if (IconoDerecha)
            {
                desplazamientoIcono = (float)(3 * h);
            }

            if (hayIcono && !IconoDerecha)
            {
                Iconos.Dibujar(g, _icono, new RectangleF(x + desplazamientoIcono, r.Y, anchoIcono, r.Height), color, tamañoIcono);
                x += anchoIcono + separacion;
            }
            if (hayTexto)
            {
                Dibujo.Texto(g, Text, Font, color, new RectangleF(x, r.Y, anchoTexto + 4, r.Height),
                    StringAlignment.Near, StringAlignment.Center, true);
                x += anchoTexto + separacion;
            }
            if (hayIcono && IconoDerecha)
            {
                Iconos.Dibujar(g, _icono, new RectangleF(x + desplazamientoIcono, r.Y, anchoIcono, r.Height), color, tamañoIcono);
            }
        }

        private void Tema_Cambiado(object sender, EventArgs e) => Invalidate();

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
