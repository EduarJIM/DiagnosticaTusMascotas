using DiagnosticaTuMascota.Controles;
using DiagnosticaTuMascota.Core;
using DiagnosticaTuMascota.Estilos;
using DiagnosticaTuMascota.Formularios.Base;

namespace DiagnosticaTuMascota.Formularios
{
    /// <summary>
    /// Consejos de cuidado: temas a la izquierda y contenido en una tarjeta.
    /// </summary>
    public partial class FrmConsejos : PaginaBase
    {
        private readonly string _temaInicial;
        private readonly List<BotonCategoria> _botones = new List<BotonCategoria>();

        public FrmConsejos() : this(null)
        {
        }

        /// <param name="temaInicial">Tema que se abre al entrar, o null para el primero.</param>
        public FrmConsejos(string temaInicial)
        {
            InitializeComponent();
            _temaInicial = temaInicial;
        }

        private void FrmConsejos_Load(object sender, EventArgs e)
        {
            foreach (var tema in CatalogoConsejos.Temas())
            {
                var (icono, inicio, fin) = EstiloDeTema(tema);
                var boton = new BotonCategoria
                {
                    Text = tema,
                    Icono = icono,
                    ColorInicio = inicio,
                    ColorFin = fin,
                    Size = new Size(270, 58),
                    Margin = new Padding(0, 0, 0, 4)
                };
                boton.Click += (s, a) => MostrarTema(((Control)s).Text);
                _botones.Add(boton);
                flpTemas.Controls.Add(boton);
            }

            var inicial = _temaInicial != null && _botones.Any(b => b.Text == _temaInicial) ? _temaInicial : _botones[0].Text;
            MostrarTema(inicial);
        }

        private void MostrarTema(string tema)
        {
            foreach (var boton in _botones)
            {
                boton.Activo = boton.Text == tema;
            }
            var (icono, color, _) = EstiloDeTema(tema);
            tituloTema.Text = tema;
            tituloTema.Icono = icono;
            tituloTema.ColorIcono = color;
            tituloTema.Invalidate();
            txtContenido.Text = CatalogoConsejos.Contenido(tema).Replace("\n", Environment.NewLine).Replace("\r\r", "\r");
            txtContenido.SelectionStart = 0;
        }

        /// <summary>
        /// Icono y colores de cada tema.
        /// </summary>
        private static (string Icono, Color Inicio, Color Fin) EstiloDeTema(string tema)
        {
            if (tema == CatalogoConsejos.TemaDeNivel(NivelSeveridad.Leve)) return (Iconos.CheckCirculo, Tema.Primario, Tema.Secundario);
            if (tema == CatalogoConsejos.TemaDeNivel(NivelSeveridad.Moderado)) return (Iconos.Termometro, Tema.Ambar, Tema.Naranja);
            if (tema == CatalogoConsejos.TemaDeNivel(NivelSeveridad.Critico)) return (Iconos.Alerta, Tema.Rojo, Tema.Naranja);
            if (tema == CatalogoConsejos.TemaDiccionario) return (Iconos.Libro, Tema.Indigo, Tema.Morado);

            return tema switch
            {
                "Alimentación" => (Iconos.Comida, Tema.Naranja, Tema.Ambar),
                "Hidratación" => (Iconos.Gota, Tema.Azul, Tema.Cian),
                "Vacunas y desparasitación" => (Iconos.Calendario, Tema.Primario, Tema.Azul),
                "Ejercicio y bienestar" => (Iconos.Corazon, Tema.Rojo, Tema.Morado),
                "Higiene" => (Iconos.Hoja, Tema.Secundario, Tema.Primario),
                _ => (Iconos.Salud, Tema.Primario, Tema.Secundario)
            };
        }

        protected override void AplicarTema()
        {
            base.AplicarTema();
            txtContenido.BackColor = Tema.Tarjeta;
            txtContenido.ForeColor = Tema.Texto;
        }
    }
}
