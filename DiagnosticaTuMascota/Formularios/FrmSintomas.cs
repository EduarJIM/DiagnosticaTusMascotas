using DiagnosticaTuMascota.Controles;
using DiagnosticaTuMascota.Core;
using DiagnosticaTuMascota.Estilos;
using DiagnosticaTuMascota.Formularios.Base;

namespace DiagnosticaTuMascota.Formularios
{
    /// <summary>
    /// Paso 1 del diagnóstico: categorías a la izquierda y síntomas de la
    /// categoría activa a la derecha. La selección se conserva entre categorías.
    /// </summary>
    public partial class FrmSintomas : PaginaBase
    {
        private static readonly string[] IconosCategoria = { Iconos.Actividad, Iconos.Hoja, Iconos.Comida, Iconos.Rayo };

        private static readonly (Color Inicio, Color Fin)[] ColoresCategoria =
        {
            (Tema.Primario, Tema.Secundario),
            (Tema.Azul, Tema.Cian),
            (Tema.Naranja, Tema.Ambar),
            (Tema.Morado, Tema.Indigo)
        };

        private readonly List<string> _seleccionados;
        private readonly List<BotonCategoria> _botonesCategoria = new List<BotonCategoria>();

        /// <summary>
        /// Se lanza al pulsar "Continuar" con la lista de síntomas marcados.
        /// </summary>
        public event EventHandler<List<string>> Continuar;

        public event EventHandler Volver;

        public FrmSintomas() : this(new List<string>())
        {
        }

        public FrmSintomas(IEnumerable<string> sintomasPrevios)
        {
            InitializeComponent();
            _seleccionados = new List<string>(sintomasPrevios ?? Enumerable.Empty<string>());
        }

        private void FrmSintomas_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < MotorTriaje.Categorias.Length; i++)
            {
                var categoria = MotorTriaje.Categorias[i];
                var boton = new BotonCategoria
                {
                    Text = categoria.Nombre,
                    Icono = IconosCategoria[i],
                    ColorInicio = ColoresCategoria[i].Inicio,
                    ColorFin = ColoresCategoria[i].Fin,
                    Size = new Size(270, 64),
                    Margin = new Padding(0, 0, 0, 6),
                    Tag = categoria
                };
                boton.Click += (s, a) => MostrarCategoria((CategoriaSintomas)((Control)s).Tag);
                _botonesCategoria.Add(boton);
                flpCategorias.Controls.Add(boton);
            }

            MostrarCategoria(MotorTriaje.Categorias[0]);
            ActualizarContador();
        }

        private void MostrarCategoria(CategoriaSintomas categoria)
        {
            foreach (var boton in _botonesCategoria)
            {
                boton.Activo = ReferenceEquals(boton.Tag, categoria);
            }

            tituloCategoria.Text = categoria.Nombre;
            flpSintomas.SuspendLayout();
            foreach (Control control in flpSintomas.Controls.Cast<Control>().ToList())
            {
                control.Dispose();
            }

            foreach (var sintoma in categoria.Sintomas)
            {
                var opcion = new BotonOpcion
                {
                    Text = sintoma,
                    Seleccionado = _seleccionados.Contains(sintoma),
                    Margin = new Padding(0, 0, 12, 12)
                };
                opcion.SeleccionCambiada += Opcion_SeleccionCambiada;
                flpSintomas.Controls.Add(opcion);
            }
            flpSintomas.ResumeLayout();
            flpSintomas_Resize(this, EventArgs.Empty);
        }

        private void Opcion_SeleccionCambiada(object sender, EventArgs e)
        {
            var opcion = (BotonOpcion)sender;
            if (opcion.Seleccionado && !_seleccionados.Contains(opcion.Text))
            {
                _seleccionados.Add(opcion.Text);
            }
            else if (!opcion.Seleccionado)
            {
                _seleccionados.Remove(opcion.Text);
            }
            ActualizarContador();
        }

        private void flpSintomas_Resize(object sender, EventArgs e)
        {
            int columnas = flpSintomas.ClientSize.Width > 520 ? 2 : 1;
            int ancho = (flpSintomas.ClientSize.Width - 12 * columnas - 4) / columnas;
            foreach (Control control in flpSintomas.Controls)
            {
                control.Width = Math.Max(200, ancho);
            }
        }

        private void btnContinuar_Click(object sender, EventArgs e) => Continuar?.Invoke(this, new List<string>(_seleccionados));

        private void btnVolver_Click(object sender, EventArgs e) => Volver?.Invoke(this, EventArgs.Empty);

        private void ActualizarContador()
        {
            lblCantidad.Text = _seleccionados.Count == 1 ? "1 síntoma" : $"{_seleccionados.Count} síntomas";
            lblCantidad.Left = lblSeleccionados.Right;
            btnContinuar.Enabled = _seleccionados.Count > 0;
        }

        protected override void AplicarTema()
        {
            base.AplicarTema();
            pnlLinea.BackColor = Tema.BordeSobre(Tema.Fondo);
        }
    }
}
