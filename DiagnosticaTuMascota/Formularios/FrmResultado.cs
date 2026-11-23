using DiagnosticaTuMascota.Core;
using DiagnosticaTuMascota.Formularios.Base;

namespace DiagnosticaTuMascota.Formularios
{
    /// <summary>
    /// Resultado de una consulta: banner de urgencia, implicaciones clínicas,
    /// primeros auxilios y aviso legal. Sirve para consultas nuevas y del historial.
    /// </summary>
    public partial class FrmResultado : PaginaBase
    {
        private readonly Consulta _consulta;
        private readonly bool _desdeHistorial;
        private NivelSeveridad _nivel;

        public event EventHandler Volver;

        /// <summary>
        /// Se lanza con el nombre del tema de consejos correspondiente al nivel.
        /// </summary>
        public event EventHandler<string> ConsejosSolicitados;

        public FrmResultado() : this(new Consulta { Fecha = DateTime.Now, Mascota = "Tu Mascota" }, false)
        {
        }

        public FrmResultado(Consulta consulta, bool desdeHistorial)
        {
            InitializeComponent();
            _consulta = consulta;
            _desdeHistorial = desdeHistorial;
        }

        /// <summary>
        /// Los bloques tienen su propia entrada escalonada.
        /// </summary>
        public override bool TransicionSuave => false;

        private void FrmResultado_Load(object sender, EventArgs e)
        {
            var triaje = MotorTriaje.Evaluar(_consulta.Sintomas);
            _nivel = triaje.Severidad;

            btnVolver.Text = _desdeHistorial ? "Volver al historial" : "Volver al inicio";
            btnFinalizar.Text = _desdeHistorial ? "Volver al Historial" : "Finalizar y Volver al Inicio";
            encabezado.Titulo = $"Análisis Clínico de {_consulta.Mascota}";
            encabezado.Subtitulo = $"{_consulta.FechaTexto}  ·  Evaluación preliminar basada en protocolos de triaje veterinario.";

            banner.Nivel = _nivel;
            var color = MotorTriaje.ColorNivel(_nivel);

            tarjetaImplicaciones.ColorAcento = color;
            tarjetaImplicaciones.Nota = _consulta.Notas;
            tarjetaImplicaciones.Elementos = triaje.Implicaciones;

            tarjetaAcciones.ColorAcento = color;
            tarjetaAcciones.Elementos = MotorTriaje.Acciones(_nivel).ToList();

            Reubicar();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (IsHandleCreated && pnlCentro != null && tarjetaImplicaciones.IsHandleCreated)
            {
                Reubicar();
            }
        }

        /// <summary>
        /// Ajusta el contenido al ancho disponible: dos columnas de tarjetas si
        /// caben (iguales en altura) o una debajo de otra en pantallas pequeñas.
        /// </summary>
        private void Reubicar()
        {
            int ancho = Math.Min(912, Math.Max(600, ClientSize.Width - 48));
            pnlCentro.Width = ancho;
            encabezado.Width = ancho - 16;
            banner.Width = ancho - 8;
            avisoLegal.Width = ancho - 16;

            bool dosColumnas = ancho >= 820;
            int anchoTarjeta = dosColumnas ? (ancho - 24) / 2 : ancho - 8;
            tarjetaImplicaciones.AltoMinimo = 0;
            tarjetaAcciones.AltoMinimo = 0;
            tarjetaImplicaciones.Width = anchoTarjeta;
            tarjetaAcciones.Width = anchoTarjeta;

            int altoImplicaciones = tarjetaImplicaciones.AltoNecesario();
            int altoAcciones = tarjetaAcciones.AltoNecesario();
            if (dosColumnas)
            {
                int alto = Math.Max(altoImplicaciones, altoAcciones);
                tarjetaImplicaciones.AltoMinimo = alto;
                tarjetaAcciones.AltoMinimo = alto;
                tarjetaImplicaciones.Height = alto;
                tarjetaAcciones.Height = alto;
                tarjetaAcciones.Location = new Point(tarjetaImplicaciones.Right + 16, tarjetaImplicaciones.Top);
            }
            else
            {
                tarjetaImplicaciones.Height = altoImplicaciones;
                tarjetaAcciones.Height = altoAcciones;
                tarjetaAcciones.Location = new Point(tarjetaImplicaciones.Left, tarjetaImplicaciones.Bottom + 16);
            }

            avisoLegal.Top = Math.Max(tarjetaImplicaciones.Bottom, tarjetaAcciones.Bottom) + 20;
            int total = btnConsejos.Width + 16 + btnFinalizar.Width;
            btnConsejos.Location = new Point((ancho - total) / 2, avisoLegal.Bottom + 24);
            btnFinalizar.Location = new Point(btnConsejos.Right + 16, btnConsejos.Top);
            pnlCentro.Height = btnFinalizar.Bottom + 40;
            pnlCentro.Left = Math.Max(0, (ClientSize.Width - ancho) / 2);
        }

        private void btnVolver_Click(object sender, EventArgs e) => Volver?.Invoke(this, EventArgs.Empty);

        private void btnConsejos_Click(object sender, EventArgs e) =>
            ConsejosSolicitados?.Invoke(this, CatalogoConsejos.TemaDeNivel(_nivel));
    }
}
