using DiagnosticaTuMascota.Estilos;
using DiagnosticaTuMascota.Formularios.Base;

namespace DiagnosticaTuMascota.Formularios
{
    /// <summary>
    /// Diálogo de confirmación o aviso con el estilo de la aplicación
    /// (sustituye a window.confirm de la web y a MessageBox).
    /// </summary>
    public partial class FrmConfirmar : ModalBase
    {
        public FrmConfirmar() : this("Confirmar", "", "Aceptar", true)
        {
        }

        /// <param name="esPeligro">true: botón rojo y Cancelar visible; false: aviso con un solo botón verde.</param>
        public FrmConfirmar(string titulo, string mensaje, string textoAccion, bool esPeligro)
        {
            InitializeComponent();
            lblTitulo.Text = titulo;
            lblMensaje.Text = mensaje;
            btnAceptar.Text = textoAccion;
            btnCancelar.Visible = esPeligro;
            btnAceptar.ColorAcento = esPeligro ? Tema.Rojo : Tema.Primario;
            icono.Icono = esPeligro ? Iconos.Alerta : Iconos.Info;
            icono.ColorIcono = esPeligro ? Tema.Rojo : Tema.Primario;
            btnCerrarModal.Visible = false;
        }

        private void btnAceptar_Click(object sender, EventArgs e) => DialogResult = DialogResult.OK;

        private void btnCancelar_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;
    }
}
