using DiagnosticaTuMascota.Core;
using DiagnosticaTuMascota.Formularios.Base;

namespace DiagnosticaTuMascota.Formularios
{
    /// <summary>
    /// Modal "Crear Alerta": mascota, prioridad, fecha, hora, título y mensaje.
    /// </summary>
    public partial class FrmEditarRecordatorio : ModalBase
    {
        private readonly List<string> _mascotas;

        /// <summary>
        /// Recordatorio creado al pulsar Guardar.
        /// </summary>
        public Recordatorio Recordatorio { get; private set; }

        public FrmEditarRecordatorio() : this(Enumerable.Empty<string>())
        {
        }

        public FrmEditarRecordatorio(IEnumerable<string> nombresMascotas)
        {
            InitializeComponent();
            _mascotas = nombresMascotas.OrderBy(n => n).ToList();
        }

        private void FrmEditarRecordatorio_Load(object sender, EventArgs e)
        {
            foreach (var nombre in _mascotas)
            {
                cmbMascota.Items.Add(nombre);
            }
            if (_mascotas.Count > 0)
            {
                cmbMascota.SelectedIndex = 0;
            }

            cmbPrioridad.Items.AddRange(new object[] { "Alta", "Media", "Baja" });
            cmbPrioridad.SelectedItem = "Media";

            for (int h = 0; h < 24; h++)
            {
                cmbHora.Items.Add(h.ToString("00"));
            }
            for (int m = 0; m < 60; m++)
            {
                cmbMinuto.Items.Add(m.ToString("00"));
            }

            var dentroDeUnaHora = DateTime.Now.AddHours(1);
            selFecha.Value = dentroDeUnaHora.Date;
            cmbHora.SelectedIndex = dentroDeUnaHora.Hour;
            cmbMinuto.SelectedIndex = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                Dialogos.Avisar(this, "Falta información", "Escribe un título para la alerta.");
                return;
            }

            var fechaHora = selFecha.Value.Date
                .AddHours(cmbHora.SelectedIndex)
                .AddMinutes(cmbMinuto.SelectedIndex);

            if (fechaHora < DateTime.Now.AddMinutes(-1) &&
                !Dialogos.Confirmar(this, "Fecha pasada", "La fecha y hora ya pasaron, así que la alarma no sonará. ¿Guardarla igualmente?", "Guardar"))
            {
                return;
            }

            Recordatorio = new Recordatorio
            {
                Titulo = txtTitulo.Text.Trim(),
                Mascota = cmbMascota.Text.Trim(),
                Mensaje = txtMensaje.Text.Trim(),
                Prioridad = cmbPrioridad.SelectedItem?.ToString() ?? "Media",
                FechaHora = fechaHora
            };
            DialogResult = DialogResult.OK;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
