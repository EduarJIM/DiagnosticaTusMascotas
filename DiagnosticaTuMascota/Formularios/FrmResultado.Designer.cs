namespace DiagnosticaTuMascota.Formularios
{
    partial class FrmResultado
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            pnlCentro = new Panel();
            btnFinalizar = new Controles.BotonRedondeado();
            btnConsejos = new Controles.BotonRedondeado();
            avisoLegal = new Controles.AvisoMedico();
            tarjetaAcciones = new Controles.TarjetaLista();
            tarjetaImplicaciones = new Controles.TarjetaLista();
            banner = new Controles.BannerUrgencia();
            encabezado = new Controles.EncabezadoPagina();
            btnVolver = new Controles.BotonRedondeado();
            pnlCentro.SuspendLayout();
            SuspendLayout();
            //
            // pnlCentro
            //
            pnlCentro.Anchor = AnchorStyles.Top;
            pnlCentro.Controls.Add(btnFinalizar);
            pnlCentro.Controls.Add(btnConsejos);
            pnlCentro.Controls.Add(avisoLegal);
            pnlCentro.Controls.Add(tarjetaAcciones);
            pnlCentro.Controls.Add(tarjetaImplicaciones);
            pnlCentro.Controls.Add(banner);
            pnlCentro.Controls.Add(encabezado);
            pnlCentro.Controls.Add(btnVolver);
            pnlCentro.Location = new Point(56, 0);
            pnlCentro.Name = "pnlCentro";
            pnlCentro.Size = new Size(912, 900);
            pnlCentro.TabIndex = 0;
            //
            // btnVolver
            //
            btnVolver.Estilo = Controles.EstiloBoton.Enlace;
            btnVolver.Font = new Font("Segoe UI", 10.5F);
            btnVolver.Icono = "\uE72B";
            btnVolver.Location = new Point(0, 26);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(220, 44);
            btnVolver.TabIndex = 0;
            btnVolver.Text = "Volver al inicio";
            btnVolver.Click += btnVolver_Click;
            //
            // encabezado
            //
            encabezado.Location = new Point(8, 78);
            encabezado.Name = "encabezado";
            encabezado.RetrasoEntrada = 0;
            encabezado.Size = new Size(896, 90);
            encabezado.Subtitulo = "Evaluación preliminar basada en protocolos de triaje veterinario.";
            encabezado.TabIndex = 1;
            encabezado.Titulo = "Análisis Clínico";
            encabezado.TituloNegro = true;
            //
            // banner
            //
            banner.Location = new Point(4, 184);
            banner.Name = "banner";
            banner.RetrasoEntrada = 80;
            banner.Size = new Size(904, 236);
            banner.TabIndex = 2;
            //
            // tarjetaImplicaciones
            //
            tarjetaImplicaciones.Icono = "\uE9D9";
            tarjetaImplicaciones.Location = new Point(4, 440);
            tarjetaImplicaciones.Name = "tarjetaImplicaciones";
            tarjetaImplicaciones.RetrasoEntrada = 180;
            tarjetaImplicaciones.Size = new Size(444, 300);
            tarjetaImplicaciones.TabIndex = 3;
            tarjetaImplicaciones.Titulo = "Implicaciones Clínicas";
            //
            // tarjetaAcciones
            //
            tarjetaAcciones.Icono = "\uE946";
            tarjetaAcciones.Location = new Point(464, 440);
            tarjetaAcciones.Name = "tarjetaAcciones";
            tarjetaAcciones.RetrasoEntrada = 260;
            tarjetaAcciones.Size = new Size(444, 300);
            tarjetaAcciones.TabIndex = 4;
            tarjetaAcciones.Titulo = "Primeros Auxilios / Acciones";
            //
            // avisoLegal
            //
            avisoLegal.Location = new Point(8, 760);
            avisoLegal.Name = "avisoLegal";
            avisoLegal.RetrasoEntrada = 320;
            avisoLegal.Size = new Size(896, 72);
            avisoLegal.TabIndex = 5;
            avisoLegal.Text = "Este análisis es una herramienta de orientación basada en síntomas generales y no sustituye el diagnóstico profesional de un médico veterinario cualificado.";
            avisoLegal.Titulo = "Aviso Legal";
            avisoLegal.Variante = Controles.VarianteAviso.Neutra;
            //
            // btnConsejos
            //
            btnConsejos.Estilo = Controles.EstiloBoton.Secundario;
            btnConsejos.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnConsejos.Icono = "\uEA80";
            btnConsejos.Location = new Point(196, 852);
            btnConsejos.Name = "btnConsejos";
            btnConsejos.Radio = 16;
            btnConsejos.Size = new Size(240, 62);
            btnConsejos.TabIndex = 6;
            btnConsejos.Text = "Ver consejos del nivel";
            btnConsejos.Click += btnConsejos_Click;
            //
            // btnFinalizar
            //
            btnFinalizar.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnFinalizar.Icono = "\uE80F";
            btnFinalizar.Location = new Point(452, 852);
            btnFinalizar.Name = "btnFinalizar";
            btnFinalizar.Radio = 16;
            btnFinalizar.Size = new Size(280, 62);
            btnFinalizar.TabIndex = 7;
            btnFinalizar.Text = "Finalizar y Volver al Inicio";
            btnFinalizar.Click += btnVolver_Click;
            //
            // FrmResultado
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            ClientSize = new Size(1024, 800);
            Controls.Add(pnlCentro);
            Name = "FrmResultado";
            Text = "Resultado";
            Load += FrmResultado_Load;
            pnlCentro.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlCentro;
        private Controles.BotonRedondeado btnVolver;
        private Controles.EncabezadoPagina encabezado;
        private Controles.BannerUrgencia banner;
        private Controles.TarjetaLista tarjetaImplicaciones;
        private Controles.TarjetaLista tarjetaAcciones;
        private Controles.AvisoMedico avisoLegal;
        private Controles.BotonRedondeado btnConsejos;
        private Controles.BotonRedondeado btnFinalizar;
    }
}
