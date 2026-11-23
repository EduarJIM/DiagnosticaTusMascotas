using DiagnosticaTuMascota.Controles;
using DiagnosticaTuMascota.Core;
using DiagnosticaTuMascota.Formularios.Base;

namespace DiagnosticaTuMascota.Formularios
{
    /// <summary>
    /// Modal para registrar o editar una mascota. Al editar, muestra debajo el
    /// historial de análisis de esa mascota.
    /// </summary>
    public partial class FrmEditarMascota : ModalBase
    {
        private readonly List<Consulta> _historial;
        private readonly bool _esNueva;

        /// <summary>
        /// Mascota editada (o nueva, con los datos escritos al pulsar Guardar).
        /// </summary>
        public Mascota Mascota { get; }

        public FrmEditarMascota() : this(null, Enumerable.Empty<Consulta>())
        {
        }

        /// <param name="mascota">Mascota a editar, o null para registrar una nueva.</param>
        public FrmEditarMascota(Mascota mascota, IEnumerable<Consulta> historial)
        {
            InitializeComponent();
            _esNueva = mascota == null;
            Mascota = mascota ?? new Mascota();
            _historial = historial.OrderByDescending(c => c.Fecha).ToList();
        }

        private void FrmEditarMascota_Load(object sender, EventArgs e)
        {
            titulo.Text = _esNueva ? "Registrar Nueva Mascota" : "Editar Perfil de Mascota";
            cmbSexo.Items.AddRange(new object[] { "Macho", "Hembra" });
            cmbSexo.SelectedItem = Mascota.Sexo == "Hembra" ? "Hembra" : "Macho";

            txtNombre.Text = Mascota.Nombre;
            txtRaza.Text = Mascota.Raza;
            txtEdad.Text = Mascota.Edad;
            txtVacunas.Text = Mascota.Vacunas;
            txtAntecedentes.Text = Mascota.Antecedentes;
            txtClinica.Text = Mascota.InfoClinica;
            txtVeterinario.Text = Mascota.Veterinario;

            CargarHistorial();
        }

        private void CargarHistorial()
        {
            tituloHistorial.Visible = !_esNueva;
            flpHistorial.Visible = !_esNueva;
            if (_esNueva)
            {
                ClientSize = new Size(ClientSize.Width, btnGuardar.Bottom + 36);
                return;
            }

            if (_historial.Count == 0)
            {
                flpHistorial.Controls.Add(new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Italic),
                    Tag = "apagado",
                    Text = "No hay consultas registradas para esta mascota aún."
                });
            }
            foreach (var consulta in _historial)
            {
                flpHistorial.Controls.Add(new FilaHistorialMini(consulta) { Width = flpHistorial.Width - 4 });
            }

            int alto = _historial.Count == 0 ? 40 : _historial.Count * 88;
            flpHistorial.Height = alto;
            ClientSize = new Size(ClientSize.Width, Math.Min(flpHistorial.Bottom + 40, 1000));
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtRaza.Text))
            {
                Dialogos.Avisar(this, "Falta información", "El nombre y la especie o raza son obligatorios.");
                return;
            }

            Mascota.Nombre = txtNombre.Text.Trim();
            Mascota.Raza = txtRaza.Text.Trim();
            Mascota.Edad = txtEdad.Text.Trim();
            Mascota.Sexo = cmbSexo.SelectedItem?.ToString() ?? "";
            Mascota.Vacunas = txtVacunas.Text.Trim();
            Mascota.Antecedentes = txtAntecedentes.Text.Trim();
            Mascota.InfoClinica = txtClinica.Text.Trim();
            Mascota.Veterinario = txtVeterinario.Text.Trim();

            DialogResult = DialogResult.OK;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
