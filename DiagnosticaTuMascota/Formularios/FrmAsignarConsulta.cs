using DiagnosticaTuMascota.Core;
using DiagnosticaTuMascota.Estilos;
using DiagnosticaTuMascota.Formularios.Base;

namespace DiagnosticaTuMascota.Formularios
{
    /// <summary>
    /// Paso 2 del diagnóstico: elegir la mascota y añadir notas. Si se elige
    /// "Otra mascota", el campo de nombre aparece desplegándose.
    /// </summary>
    public partial class FrmAsignarConsulta : PaginaBase
    {
        private const string OpcionOtraMascota = "Otra Mascota (No Registrada)";
        private const int AltoManual = 90;

        private readonly List<string> _sintomas;
        private readonly List<Mascota> _mascotas;
        private Animacion _animacionManual;

        public event EventHandler Volver;
        public event EventHandler Generar;

        /// <summary>
        /// Nombre de la mascota elegida o escrita por el usuario.
        /// </summary>
        public string NombreMascota { get; private set; } = "";

        public string Notas => txtNotas.Text.Trim();

        public FrmAsignarConsulta() : this(new List<string>(), new List<Mascota>())
        {
        }

        public FrmAsignarConsulta(List<string> sintomas, List<Mascota> mascotas)
        {
            InitializeComponent();
            _sintomas = sintomas;
            _mascotas = mascotas;
        }

        private void FrmAsignarConsulta_Load(object sender, EventArgs e)
        {
            lblSintomas.Text = "Síntomas: " + string.Join(", ", _sintomas);
            foreach (var mascota in _mascotas)
            {
                cmbMascota.Items.Add(mascota);
            }
            cmbMascota.Items.Add(OpcionOtraMascota);

            pnlManual.Height = _mascotas.Count == 0 ? AltoManual : 0;
            cmbMascota.SelectedIndex = 0;
            Reubicar();
        }

        private void cmbMascota_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool esManual = Equals(cmbMascota.SelectedItem, OpcionOtraMascota);
            int origen = pnlManual.Height;
            int destino = esManual ? AltoManual : 0;
            _animacionManual?.Detener();
            _animacionManual = Animador.Animar(this, 300, Suavizado.SalidaCubica, t =>
            {
                pnlManual.Height = (int)(origen + (destino - origen) * t);
                Reubicar();
            }, () =>
            {
                if (esManual)
                {
                    txtNombreManual.Focus();
                }
            });
        }

        /// <summary>
        /// Coloca las notas y el botón debajo del campo de nombre (que cambia de alto).
        /// </summary>
        private void Reubicar()
        {
            int y = pnlManual.Top + pnlManual.Height + 12;
            lblNotas.Top = y;
            txtNotas.Top = y + 30;
            btnGenerar.Top = txtNotas.Bottom + 28;
            tarjeta.Height = btnGenerar.Bottom + 36;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            if (cmbMascota.SelectedItem is Mascota mascota)
            {
                NombreMascota = mascota.Nombre;
            }
            else
            {
                var nombre = txtNombreManual.Text.Trim();
                NombreMascota = nombre.Length > 0 ? nombre : "Mascota No Registrada";
            }
            Generar?.Invoke(this, EventArgs.Empty);
        }

        private void btnVolver_Click(object sender, EventArgs e) => Volver?.Invoke(this, EventArgs.Empty);
    }
}
