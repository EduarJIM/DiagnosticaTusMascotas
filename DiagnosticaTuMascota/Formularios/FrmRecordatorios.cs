using DiagnosticaTuMascota.Controles;
using DiagnosticaTuMascota.Datos;
using DiagnosticaTuMascota.Formularios.Base;

namespace DiagnosticaTuMascota.Formularios
{
    /// <summary>
    /// Lista de alertas y recordatorios. Los próximos aparecen primero; los
    /// pasados se muestran atenuados al final.
    /// </summary>
    public partial class FrmRecordatorios : PaginaBase
    {
        private readonly Almacenamiento _datos;

        public FrmRecordatorios() : this(new Almacenamiento(Almacenamiento.RutaPorDefecto))
        {
        }

        public FrmRecordatorios(Almacenamiento datos)
        {
            InitializeComponent();
            _datos = datos;
        }

        private void FrmRecordatorios_Load(object sender, EventArgs e)
        {
            CargarTarjetas();
        }

        private void btnNueva_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmEditarRecordatorio(_datos.Mascotas.Select(m => m.Nombre)))
            {
                if (frm.MostrarSobre(this) == DialogResult.OK)
                {
                    _datos.AgregarRecordatorio(frm.Recordatorio);
                    Notificaciones.Exito("Alerta programada");
                    CargarTarjetas();
                }
            }
        }

        private void Tarjeta_Eliminar(object sender, EventArgs e)
        {
            var recordatorio = ((TarjetaRecordatorio)sender).Recordatorio;
            if (Dialogos.Confirmar(this, "Eliminar alerta", $"¿Eliminar la alerta \"{recordatorio.Titulo}\"?"))
            {
                _datos.EliminarRecordatorio(recordatorio.Id);
                CargarTarjetas();
            }
        }

        private void CargarTarjetas()
        {
            var ahora = DateTime.Now;
            var ordenados = _datos.Recordatorios
                .OrderBy(r => r.FechaHora < ahora)
                .ThenBy(r => r.FechaHora < ahora ? -r.FechaHora.Ticks : r.FechaHora.Ticks)
                .ToList();

            flpRecordatorios.SuspendLayout();
            foreach (Control control in flpRecordatorios.Controls.Cast<Control>().ToList())
            {
                control.Dispose();
            }
            foreach (var recordatorio in ordenados)
            {
                var tarjeta = new TarjetaRecordatorio(recordatorio);
                tarjeta.Eliminar += Tarjeta_Eliminar;
                flpRecordatorios.Controls.Add(tarjeta);
            }
            flpRecordatorios.ResumeLayout();

            encabezado.Titulo = $"Alertas Activas ({ordenados.Count(r => r.FechaHora >= ahora)})";
            vacio.Visible = ordenados.Count == 0;
            flpRecordatorios_Resize(this, EventArgs.Empty);
        }

        private void flpRecordatorios_Resize(object sender, EventArgs e)
        {
            int ancho = flpRecordatorios.ClientSize.Width - 6;
            foreach (Control control in flpRecordatorios.Controls)
            {
                control.Width = ancho;
            }
        }
    }
}
