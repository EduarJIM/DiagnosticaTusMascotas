using System.Runtime.InteropServices;

namespace DiagnosticaTuMascota.Estilos
{
    /// <summary>
    /// Paleta de colores y fuentes de la aplicación, tomada de theme.css de la
    /// versión web. Tiene modo oscuro (por defecto) y claro.
    /// </summary>
    public static class Tema
    {
        /// <summary>
        /// Se lanza cuando el usuario cambia entre modo claro y oscuro.
        /// </summary>
        public static event EventHandler Cambiado;

        public static bool Oscuro { get; private set; } = true;

        // Colores de marca (iguales en ambos modos).
        public static readonly Color Primario = Color.FromArgb(16, 185, 129);     // emerald-500
        public static readonly Color Secundario = Color.FromArgb(20, 184, 166);   // teal-500
        public static readonly Color Azul = Color.FromArgb(59, 130, 246);
        public static readonly Color Cian = Color.FromArgb(6, 182, 212);
        public static readonly Color Ambar = Color.FromArgb(245, 158, 11);
        public static readonly Color Naranja = Color.FromArgb(249, 115, 22);
        public static readonly Color Rojo = Color.FromArgb(239, 68, 68);
        public static readonly Color Morado = Color.FromArgb(168, 85, 247);
        public static readonly Color Indigo = Color.FromArgb(99, 102, 241);
        public static readonly Color Blanco = Color.White;

        // Colores que cambian con el modo.
        public static Color Fondo => Oscuro ? Color.FromArgb(6, 9, 16) : Color.FromArgb(241, 245, 249);
        public static Color Tarjeta => Oscuro ? Color.FromArgb(15, 18, 25) : Color.White;
        public static Color Emergente => Oscuro ? Color.FromArgb(26, 31, 46) : Color.White;
        public static Color Lateral => Oscuro ? Color.FromArgb(10, 14, 20) : Color.White;
        public static Color LateralAcento => Oscuro ? Color.FromArgb(26, 31, 46) : Color.FromArgb(241, 245, 249);
        public static Color Apagado => Oscuro ? Color.FromArgb(30, 37, 54) : Color.FromArgb(226, 232, 240);
        public static Color TextoApagado => Oscuro ? Color.FromArgb(148, 163, 184) : Color.FromArgb(51, 65, 85);
        public static Color Texto => Oscuro ? Color.FromArgb(248, 250, 252) : Color.FromArgb(2, 6, 23);
        public static Color Destructivo => Oscuro ? Color.FromArgb(255, 107, 107) : Color.FromArgb(239, 68, 68);
        public static Color Entrada => Fondo;

        /// <summary>
        /// Color de los bordes: blanco al 8 % en oscuro y negro al 12 % en claro.
        /// </summary>
        public static Color Borde => Oscuro ? Color.FromArgb(20, 255, 255, 255) : Color.FromArgb(31, 0, 0, 0);

        /// <summary>
        /// Borde ya mezclado con un fondo (para controles que no admiten transparencia).
        /// </summary>
        public static Color BordeSobre(Color fondo) => Dibujo.Mezclar(fondo, Borde, Borde.A / 255.0);

        public static void CambiarModo(bool oscuro)
        {
            if (Oscuro == oscuro)
            {
                return;
            }
            Oscuro = oscuro;
            Cambiado?.Invoke(null, EventArgs.Empty);
        }

        #region Fuentes

        private static readonly Dictionary<string, Font> _fuentes = new Dictionary<string, Font>();

        /// <summary>
        /// Devuelve una fuente Segoe UI en caché (no hace falta liberarla).
        /// </summary>
        public static Font Fuente(float tamaño, FontStyle estilo = FontStyle.Regular) => FuenteDe("Segoe UI", tamaño, estilo);

        public static Font FuenteSemibold(float tamaño) => FuenteDe("Segoe UI Semibold", tamaño, FontStyle.Regular);

        public static Font FuenteNegra(float tamaño) => FuenteDe("Segoe UI Black", tamaño, FontStyle.Regular);

        public static Font FuenteIconos(float tamaño) => FuenteDe(Iconos.Familia, tamaño, FontStyle.Regular);

        private static Font FuenteDe(string familia, float tamaño, FontStyle estilo)
        {
            var clave = $"{familia}|{tamaño}|{estilo}";
            if (!_fuentes.TryGetValue(clave, out var fuente))
            {
                fuente = new Font(familia, tamaño, estilo);
                _fuentes[clave] = fuente;
            }
            return fuente;
        }

        #endregion

        #region Aplicar a controles estándar

        /// <summary>
        /// Colorea los controles estándar según su Tag: "apagado" (texto gris),
        /// "primario" (texto verde) o "destructivo". Los controles propios se
        /// colorean solos al escuchar <see cref="Cambiado"/>.
        /// </summary>
        public static void Aplicar(Control raiz)
        {
            foreach (Control control in raiz.Controls)
            {
                switch (control.Tag as string)
                {
                    case "apagado":
                        control.ForeColor = TextoApagado;
                        break;
                    case "primario":
                        control.ForeColor = Primario;
                        break;
                    case "destructivo":
                        control.ForeColor = Destructivo;
                        break;
                }

                if (control is ScrollableControl || control is TextBoxBase)
                {
                    BarrasOscuras(control);
                }

                Aplicar(control);
            }
        }

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hwnd, string appName, string idList);

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int atributo, ref int valor, int tamaño);

        /// <summary>
        /// Pone las barras de desplazamiento en oscuro o claro (Windows 10/11).
        /// </summary>
        public static void BarrasOscuras(Control control)
        {
            if (!control.IsHandleCreated)
            {
                control.HandleCreated += (s, e) => BarrasOscuras((Control)s);
                return;
            }
            SetWindowTheme(control.Handle, Oscuro ? "DarkMode_Explorer" : "Explorer", null);
        }

        /// <summary>
        /// Barra de título oscura/clara con el color de la barra lateral (Windows 11).
        /// </summary>
        public static void BarraTitulo(Form formulario)
        {
            if (!formulario.IsHandleCreated)
            {
                return;
            }
            int oscuro = Oscuro ? 1 : 0;
            DwmSetWindowAttribute(formulario.Handle, 20, ref oscuro, sizeof(int));
            int color = ColorTranslator.ToWin32(Lateral);
            DwmSetWindowAttribute(formulario.Handle, 35, ref color, sizeof(int));
        }

        /// <summary>
        /// Esquinas redondeadas de Windows 11 para ventanas sin borde.
        /// </summary>
        public static void EsquinasRedondeadas(Form formulario)
        {
            int redondo = 2;
            DwmSetWindowAttribute(formulario.Handle, 33, ref redondo, sizeof(int));
        }

        #endregion
    }
}
