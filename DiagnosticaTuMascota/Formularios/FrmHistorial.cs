using DiagnosticaTuMascota.Controles;
using DiagnosticaTuMascota.Core;
using DiagnosticaTuMascota.Datos;
using DiagnosticaTuMascota.Formularios.Base;

namespace DiagnosticaTuMascota.Formularios
{
    /// <summary>
    /// Historial de consultas con filtros por nivel (pastillas) y por mascota.
    /// Las consultas se buscan en SQLite con <see cref="Almacenamiento.BuscarConsultas"/>.
    /// </summary>
    public partial class FrmHistorial : PaginaBase
    {
        private const string TodasLasMascotas = "Todas las mascotas";

        private readonly Almacenamiento _datos;
        private NivelSeveridad? _nivel;
        private bool _cargando;

        /// <summary>
        /// Se lanza al pulsar una consulta para ver su análisis completo.
        /// </summary>
        public event EventHandler<Consulta> ConsultaAbierta;

        public event EventHandler NuevoDiagnostico;

        public FrmHistorial() : this(new Almacenamiento(Almacenamiento.RutaPorDefecto))
        {
        }

        public FrmHistorial(Almacenamiento datos)
        {
            InitializeComponent();
            _datos = datos;
        }

        private void FrmHistorial_Load(object sender, EventArgs e)
        {
            _cargando = true;
            cmbMascota.Items.Add(TodasLasMascotas);
            foreach (var nombre in _datos.Consultas.Select(c => c.Mascota).Distinct().OrderBy(n => n))
            {
                cmbMascota.Items.Add(nombre);
            }
            cmbMascota.SelectedIndex = 0;
            _cargando = false;
            CargarConsultas();
        }

        private void Filtro_Click(object sender, EventArgs e)
        {
            btnTodas.Activo = sender == btnTodas;
            btnLeve.Activo = sender == btnLeve;
            btnModerado.Activo = sender == btnModerado;
            btnCritico.Activo = sender == btnCritico;

            if (sender == btnLeve) _nivel = NivelSeveridad.Leve;
            else if (sender == btnModerado) _nivel = NivelSeveridad.Moderado;
            else if (sender == btnCritico) _nivel = NivelSeveridad.Critico;
            else _nivel = null;

            CargarConsultas();
        }

        private void cmbMascota_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_cargando)
            {
                CargarConsultas();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e) => NuevoDiagnostico?.Invoke(this, EventArgs.Empty);

        private void CargarConsultas()
        {
            var mascota = cmbMascota.SelectedItem as string;
            if (mascota == TodasLasMascotas)
            {
                mascota = null;
            }

            var consultas = _datos.BuscarConsultas(mascota, _nivel);

            flpConsultas.SuspendLayout();
            foreach (Control control in flpConsultas.Controls.Cast<Control>().ToList())
            {
                control.Dispose();
            }
            foreach (var consulta in consultas)
            {
                var tarjeta = new TarjetaConsulta(consulta);
                tarjeta.Abrir += (s, e) => ConsultaAbierta?.Invoke(this, ((TarjetaConsulta)s).Consulta);
                tarjeta.Eliminar += Tarjeta_Eliminar;
                flpConsultas.Controls.Add(tarjeta);
            }
            flpConsultas.ResumeLayout();

            vacio.Visible = consultas.Count == 0;
            flpConsultas_Resize(this, EventArgs.Empty);
        }

        private void Tarjeta_Eliminar(object sender, EventArgs e)
        {
            var consulta = ((TarjetaConsulta)sender).Consulta;
            if (Dialogos.Confirmar(this, "Eliminar consulta",
                $"¿Eliminar la consulta de {consulta.Mascota} del {consulta.FechaTexto}? Esta acción no se puede deshacer."))
            {
                _datos.EliminarConsulta(consulta.Id);
                Notificaciones.Exito("Consulta eliminada");
                CargarConsultas();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (cmbMascota != null)
            {
                cmbMascota.Left = Math.Max(btnCritico.Right + 12, ClientSize.Width - 44 - cmbMascota.Width);
            }
        }

        private void flpConsultas_Resize(object sender, EventArgs e)
        {
            int ancho = flpConsultas.ClientSize.Width - 6;
            foreach (Control control in flpConsultas.Controls)
            {
                control.Width = ancho;
            }
        }
    }
}
