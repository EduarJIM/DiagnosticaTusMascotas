using System.ComponentModel;
using DiagnosticaTuMascota.Estilos;

namespace DiagnosticaTuMascota.Formularios.Base
{
    /// <summary>
    /// Base de las páginas que se muestran dentro de la ventana principal, a la
    /// derecha de la barra lateral (como las rutas de la versión web).
    /// </summary>
    public class PaginaBase : Form
    {
        public PaginaBase()
        {
            FormBorderStyle = FormBorderStyle.None;
            base.BackColor = Tema.Fondo;
            base.ForeColor = Tema.Texto;
            Tema.Cambiado += Tema_Cambiado;
        }

        /// <summary>
        /// Si es true, la ventana principal hace aparecer la página con un fundido.
        /// Las páginas cuyos controles ya tienen su propia animación devuelven false.
        /// </summary>
        [Browsable(false)]
        public virtual bool TransicionSuave => true;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color BackColor
        {
            get => base.BackColor;
            set => base.BackColor = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color ForeColor
        {
            get => base.ForeColor;
            set => base.ForeColor = value;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AplicarTema();
        }

        /// <summary>
        /// Vuelve a colorear la página con el tema actual.
        /// </summary>
        protected virtual void AplicarTema()
        {
            base.BackColor = Tema.Fondo;
            base.ForeColor = Tema.Texto;
            Tema.Aplicar(this);
            Tema.BarrasOscuras(this);
            Invalidate(true);
        }

        private void Tema_Cambiado(object sender, EventArgs e) => AplicarTema();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Tema.Cambiado -= Tema_Cambiado;
            }
            base.Dispose(disposing);
        }
    }
}
