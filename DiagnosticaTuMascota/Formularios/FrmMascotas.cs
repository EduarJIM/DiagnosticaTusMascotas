using DiagnosticaTuMascota.Controles;
using DiagnosticaTuMascota.Core;
using DiagnosticaTuMascota.Datos;
using DiagnosticaTuMascota.Formularios.Base;

namespace DiagnosticaTuMascota.Formularios
{
    /// <summary>
    /// Cuadrícula de tarjetas con las mascotas registradas.
    /// </summary>
    public partial class FrmMascotas : PaginaBase
    {
        private readonly Almacenamiento _datos;

        public FrmMascotas() : this(new Almacenamiento(Almacenamiento.RutaPorDefecto))
        {
        }

        public FrmMascotas(Almacenamiento datos)
        {
            InitializeComponent();
            _datos = datos;
        }

        private void FrmMascotas_Load(object sender, EventArgs e)
        {
            CargarTarjetas();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmEditarMascota(null, Enumerable.Empty<Consulta>()))
            {
                if (frm.MostrarSobre(this) == DialogResult.OK)
                {
                    _datos.AgregarMascota(frm.Mascota);
                    Notificaciones.Exito($"{frm.Mascota.Nombre} se registró correctamente");
                    CargarTarjetas();
                }
            }
        }

        private void Tarjeta_Editar(object sender, EventArgs e)
        {
            var mascota = ((TarjetaMascota)sender).Mascota;
            var historial = _datos.Consultas.Where(c => c.Mascota == mascota.Nombre);
            using (var frm = new FrmEditarMascota(mascota, historial))
            {
                if (frm.MostrarSobre(this) == DialogResult.OK)
                {
                    _datos.ActualizarMascota(mascota);
                    Notificaciones.Exito("Perfil actualizado");
                    CargarTarjetas();
                }
            }
        }

        private void Tarjeta_Eliminar(object sender, EventArgs e)
        {
            var mascota = ((TarjetaMascota)sender).Mascota;
            if (Dialogos.Confirmar(this, "Eliminar mascota",
                $"¿Estás seguro de eliminar a {mascota.Nombre}? Su historial de consultas se conservará."))
            {
                _datos.EliminarMascota(mascota.Id);
                Notificaciones.Exito($"{mascota.Nombre} fue eliminada");
                CargarTarjetas();
            }
        }

        private void CargarTarjetas()
        {
            flpMascotas.SuspendLayout();
            foreach (Control control in flpMascotas.Controls.Cast<Control>().ToList())
            {
                control.Dispose();
            }

            foreach (var mascota in _datos.Mascotas.OrderBy(m => m.Nombre))
            {
                var tarjeta = new TarjetaMascota(mascota);
                tarjeta.Editar += Tarjeta_Editar;
                tarjeta.Eliminar += Tarjeta_Eliminar;
                flpMascotas.Controls.Add(tarjeta);
            }

            flpMascotas.ResumeLayout();
            vacio.Visible = _datos.Mascotas.Count == 0;
            flpMascotas_Resize(this, EventArgs.Empty);
        }

        /// <summary>
        /// Tres columnas si caben (grid-cols-3), si no, dos.
        /// </summary>
        private void flpMascotas_Resize(object sender, EventArgs e)
        {
            int disponible = flpMascotas.ClientSize.Width - 4;
            int columnas = Math.Max(1, Math.Min(3, disponible / 300));
            int ancho = disponible / columnas - 20;
            foreach (Control control in flpMascotas.Controls)
            {
                control.Width = ancho;
            }
        }
    }
}
