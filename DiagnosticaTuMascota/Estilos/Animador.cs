using System.Diagnostics;

namespace DiagnosticaTuMascota.Estilos
{
    /// <summary>
    /// Curvas de suavizado para las animaciones (equivalentes a ease-out, ease-in-out, spring...).
    /// </summary>
    public static class Suavizado
    {
        public static double Lineal(double t) => t;

        public static double SalidaCubica(double t) => 1 - Math.Pow(1 - t, 3);

        public static double EntradaSalidaSeno(double t) => -(Math.Cos(Math.PI * t) - 1) / 2;

        /// <summary>
        /// Se pasa un poco del destino y vuelve, como un resorte.
        /// </summary>
        public static double Resorte(double t)
        {
            const double c1 = 1.70158, c3 = c1 + 1;
            return 1 + c3 * Math.Pow(t - 1, 3) + c1 * Math.Pow(t - 1, 2);
        }

        /// <summary>
        /// Onda entre 0 y 1 que se repite cada "periodo" segundos (para pulsos y flotación).
        /// </summary>
        public static double Onda(double segundos, double periodo) =>
            (1 - Math.Cos(2 * Math.PI * segundos / periodo)) / 2;
    }

    /// <summary>
    /// Animación en curso. Se puede detener en cualquier momento.
    /// </summary>
    public sealed class Animacion
    {
        internal Control Dueño;
        internal double Inicio;
        internal double Retraso;
        internal double Duracion;
        internal Func<double, double> Curva;
        internal Action<double> Paso;
        internal Action Fin;
        internal bool EsBucle;

        public bool Activa { get; internal set; } = true;

        public void Detener() => Activa = false;
    }

    /// <summary>
    /// Motor de animaciones basado en un único Timer a ~60 fps. Sustituye a la
    /// librería "motion" de la web: interpola valores en el tiempo y avisa a los
    /// controles para que se redibujen.
    /// </summary>
    public static class Animador
    {
        private static readonly System.Windows.Forms.Timer Temporizador = new System.Windows.Forms.Timer { Interval = 15 };
        private static readonly List<Animacion> Activas = new List<Animacion>();
        private static readonly Stopwatch Reloj = Stopwatch.StartNew();

        static Animador()
        {
            Temporizador.Tick += (s, e) => Avanzar();
        }

        /// <summary>
        /// Segundos transcurridos desde que arrancó la aplicación.
        /// </summary>
        public static double Ahora => Reloj.Elapsed.TotalSeconds;

        /// <summary>
        /// Anima un valor de 0 a 1 durante "milisegundos".
        /// </summary>
        /// <param name="dueño">Control al que pertenece; si se destruye, la animación se cancela.</param>
        /// <param name="paso">Recibe el progreso ya suavizado (0 a 1).</param>
        public static Animacion Animar(Control dueño, int milisegundos, Func<double, double> curva,
            Action<double> paso, Action fin = null, int retrasoMs = 0)
        {
            var animacion = new Animacion
            {
                Dueño = dueño,
                Inicio = Ahora,
                Retraso = retrasoMs / 1000.0,
                Duracion = Math.Max(1, milisegundos) / 1000.0,
                Curva = curva ?? Suavizado.SalidaCubica,
                Paso = paso,
                Fin = fin
            };
            Registrar(animacion);
            return animacion;
        }

        /// <summary>
        /// Animación infinita. "paso" recibe los segundos desde que empezó.
        /// Solo avanza mientras el control está visible.
        /// </summary>
        public static Animacion Bucle(Control dueño, Action<double> paso)
        {
            var animacion = new Animacion { Dueño = dueño, Inicio = Ahora, Paso = paso, EsBucle = true };
            Registrar(animacion);
            return animacion;
        }

        private static void Registrar(Animacion animacion)
        {
            Activas.Add(animacion);
            if (!Temporizador.Enabled)
            {
                Temporizador.Start();
            }
        }

        private static void Avanzar()
        {
            double ahora = Ahora;
            foreach (var a in Activas.ToArray())
            {
                if (!a.Activa || a.Dueño == null || a.Dueño.IsDisposed)
                {
                    a.Activa = false;
                    Activas.Remove(a);
                    continue;
                }

                if (a.EsBucle)
                {
                    if (a.Dueño.Visible)
                    {
                        a.Paso(ahora - a.Inicio);
                    }
                    continue;
                }

                double t = (ahora - a.Inicio - a.Retraso) / a.Duracion;
                if (t < 0)
                {
                    continue;
                }
                if (t >= 1)
                {
                    a.Paso(a.Curva(1));
                    a.Activa = false;
                    Activas.Remove(a);
                    a.Fin?.Invoke();
                    continue;
                }
                a.Paso(a.Curva(t));
            }

            if (Activas.Count == 0)
            {
                Temporizador.Stop();
            }
        }
    }

    /// <summary>
    /// Número que se mueve suavemente hacia un destino y redibuja su control.
    /// Se usa para los efectos de hover, foco y selección.
    /// </summary>
    public sealed class ValorAnimado
    {
        private readonly Control _dueño;
        private readonly int _milisegundos;
        private Animacion _animacion;

        public double Valor { get; private set; }

        public ValorAnimado(Control dueño, int milisegundos = 200, double inicial = 0)
        {
            _dueño = dueño;
            _milisegundos = milisegundos;
            Valor = inicial;
        }

        public void IrA(double destino, Func<double, double> curva = null)
        {
            _animacion?.Detener();
            double origen = Valor;
            if (Math.Abs(origen - destino) < 0.001)
            {
                return;
            }
            _animacion = Animador.Animar(_dueño, _milisegundos, curva ?? Suavizado.SalidaCubica, t =>
            {
                Valor = origen + (destino - origen) * t;
                _dueño.Invalidate();
            });
        }

        public void Fijar(double valor)
        {
            _animacion?.Detener();
            Valor = valor;
            _dueño.Invalidate();
        }
    }
}
