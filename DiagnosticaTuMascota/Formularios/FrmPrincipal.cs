using System.Media;
using DiagnosticaTuMascota.Controles;
using DiagnosticaTuMascota.Core;
using DiagnosticaTuMascota.Datos;
using DiagnosticaTuMascota.Estilos;
using DiagnosticaTuMascota.Formularios.Base;

namespace DiagnosticaTuMascota.Formularios
{
    /// <summary>
    /// Ventana principal: barra lateral de navegación y área de contenido donde
    /// se muestran las páginas (equivale a App.tsx + Sidebar.tsx de la web).
    /// También vigila los recordatorios y muestra las notificaciones.
    /// </summary>
    public partial class FrmPrincipal : Form
    {
        private const string PreferenciaTema = "tema";

        private readonly Almacenamiento _datos = new Almacenamiento(Almacenamiento.RutaPorDefecto);
        private readonly HashSet<long> _recordatoriosAvisados = new HashSet<long>();
        private readonly CapaTransicion _capa = new CapaTransicion { Dock = DockStyle.Fill };
        private PaginaBase _pagina;
        private List<string> _sintomasEnCurso = new List<string>();

        public FrmPrincipal()
        {
            InitializeComponent();
            pnlContenido.Controls.Add(_capa);
            Tema.Cambiado += Tema_Cambiado;
            Notificaciones.Solicitada += MostrarNotificacion;
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            var area = Screen.FromControl(this).WorkingArea;
            if (area.Width < Width + 40 || area.Height < Height + 40)
            {
                WindowState = FormWindowState.Maximized;
            }

            _datos.Cargar();
            Tema.CambiarModo(_datos.LeerPreferencia(PreferenciaTema, "oscuro") == "oscuro");
            AplicarTema();
            IrA(Seccion.Inicio);
            tmrAlarmas.Start();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Tema.BarraTitulo(this);
        }

        #region Navegación

        private void navInicio_Click(object sender, EventArgs e) => IrA(Seccion.Inicio);
        private void navMascotas_Click(object sender, EventArgs e) => IrA(Seccion.Mascotas);
        private void navDiagnostico_Click(object sender, EventArgs e) => IrA(Seccion.Diagnostico);
        private void navHistorial_Click(object sender, EventArgs e) => IrA(Seccion.Historial);
        private void navRecordatorios_Click(object sender, EventArgs e) => IrA(Seccion.Recordatorios);
        private void navConsejos_Click(object sender, EventArgs e) => IrA(Seccion.Consejos);
        private void navSalir_Click(object sender, EventArgs e) => Close();

        /// <summary>
        /// Abre una sección de la aplicación.
        /// </summary>
        public void IrA(Seccion seccion)
        {
            switch (seccion)
            {
                case Seccion.Inicio:
                    var inicio = new FrmInicio();
                    inicio.SeccionSolicitada += (s, destino) => IrA(destino);
                    Navegar(inicio, navInicio);
                    break;

                case Seccion.Mascotas:
                    Navegar(new FrmMascotas(_datos), navMascotas);
                    break;

                case Seccion.Diagnostico:
                    _sintomasEnCurso = new List<string>();
                    MostrarSintomas();
                    break;

                case Seccion.Historial:
                    var historial = new FrmHistorial(_datos);
                    historial.ConsultaAbierta += (s, consulta) => MostrarResultado(consulta, true);
                    historial.NuevoDiagnostico += (s, e) => IrA(Seccion.Diagnostico);
                    Navegar(historial, navHistorial);
                    break;

                case Seccion.Recordatorios:
                    Navegar(new FrmRecordatorios(_datos), navRecordatorios);
                    break;

                case Seccion.Consejos:
                    Navegar(new FrmConsejos(), navConsejos);
                    break;
            }
        }

        private void MostrarSintomas()
        {
            var sintomas = new FrmSintomas(_sintomasEnCurso);
            sintomas.Volver += (s, e) => IrA(Seccion.Inicio);
            sintomas.Continuar += (s, lista) =>
            {
                _sintomasEnCurso = lista;
                MostrarAsignacion();
            };
            Navegar(sintomas, navDiagnostico);
        }

        private void MostrarAsignacion()
        {
            var asignar = new FrmAsignarConsulta(_sintomasEnCurso, _datos.Mascotas);
            asignar.Volver += (s, e) => MostrarSintomas();
            asignar.Generar += (s, e) =>
            {
                var consulta = CrearConsulta(asignar.NombreMascota, _sintomasEnCurso, asignar.Notas);
                _datos.AgregarConsulta(consulta);
                Notificaciones.Exito("Diagnóstico guardado en el historial");
                MostrarResultado(consulta, false);
            };
            Navegar(asignar, navDiagnostico);
        }

        private void MostrarResultado(Consulta consulta, bool desdeHistorial)
        {
            var resultado = new FrmResultado(consulta, desdeHistorial);
            resultado.Volver += (s, e) => IrA(desdeHistorial ? Seccion.Historial : Seccion.Inicio);
            resultado.ConsejosSolicitados += (s, tema) => Navegar(new FrmConsejos(tema), navConsejos);
            Navegar(resultado, desdeHistorial ? navHistorial : navDiagnostico);
        }

        /// <summary>
        /// Coloca una página en el área de contenido y reproduce la transición.
        /// </summary>
        private void Navegar(PaginaBase nueva, ElementoNav opcionActiva)
        {
            foreach (var opcion in new[] { navInicio, navMascotas, navDiagnostico, navHistorial, navRecordatorios, navConsejos })
            {
                opcion.Activo = opcion == opcionActiva;
            }

            var anterior = _pagina;
            _pagina = nueva;

            if (nueva.TransicionSuave)
            {
                _capa.Visible = true;
                _capa.BringToFront();
                _capa.Refresh();
            }

            nueva.TopLevel = false;
            nueva.Dock = DockStyle.Fill;
            pnlContenido.Controls.Add(nueva);
            nueva.Show();
            _capa.BringToFront();

            if (anterior != null)
            {
                pnlContenido.Controls.Remove(anterior);
                anterior.Dispose();
            }

            if (nueva.TransicionSuave)
            {
                var captura = new Bitmap(Math.Max(1, pnlContenido.ClientSize.Width), Math.Max(1, pnlContenido.ClientSize.Height));
                nueva.DrawToBitmap(captura, new Rectangle(Point.Empty, captura.Size));
                _capa.Reproducir(captura);
            }
            else
            {
                _capa.Visible = false;
            }
        }

        #endregion

        #region Recordatorios y notificaciones

        private void tmrAlarmas_Tick(object sender, EventArgs e)
        {
            var pendientes = ServicioAlarmas.Pendientes(_datos.Recordatorios, DateTime.Now, _recordatoriosAvisados);
            foreach (var recordatorio in pendientes)
            {
                _recordatoriosAvisados.Add(recordatorio.Id);
                SystemSounds.Exclamation.Play();
                using (var alarma = new FrmAlarma(recordatorio))
                {
                    alarma.MostrarSobre(this);
                }
            }
        }

        private void MostrarNotificacion(string mensaje, bool esError)
        {
            int visibles = Controls.OfType<Notificacion>().Count();
            var aviso = new Notificacion(mensaje, esError)
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            aviso.Location = new Point(ClientSize.Width - aviso.Width - 20, 18 + visibles * 70);
            Controls.Add(aviso);
            aviso.BringToFront();
            aviso.Mostrar();
        }

        #endregion

        private static Consulta CrearConsulta(string mascota, List<string> sintomas, string notas)
        {
            var triaje = MotorTriaje.Evaluar(sintomas);
            return new Consulta
            {
                Fecha = DateTime.Now,
                Mascota = mascota,
                Sintomas = new List<string>(sintomas),
                Notas = notas,
                Severidad = triaje.Severidad,
                Recomendacion = MotorTriaje.Recomendacion(triaje.Severidad)
            };
        }

        private void Tema_Cambiado(object sender, EventArgs e)
        {
            AplicarTema();
            _datos.GuardarPreferencia(PreferenciaTema, Tema.Oscuro ? "oscuro" : "claro");
        }

        private void AplicarTema()
        {
            BackColor = Tema.Fondo;
            ForeColor = Tema.Texto;
            pnlContenido.BackColor = Tema.Fondo;
            Tema.Aplicar(this);
            Tema.BarraTitulo(this);
            Invalidate(true);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Tema.Cambiado -= Tema_Cambiado;
            Notificaciones.Solicitada -= MostrarNotificacion;
            base.OnFormClosed(e);
        }
    }
}
