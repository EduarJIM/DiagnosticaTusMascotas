namespace DiagnosticaTuMascota.Formularios
{
    partial class FrmPrincipal
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
            components = new System.ComponentModel.Container();
            pnlLateral = new Controles.PanelLateral();
            navSalir = new Controles.ElementoNav();
            navConsejos = new Controles.ElementoNav();
            navRecordatorios = new Controles.ElementoNav();
            navHistorial = new Controles.ElementoNav();
            navDiagnostico = new Controles.ElementoNav();
            navMascotas = new Controles.ElementoNav();
            navInicio = new Controles.ElementoNav();
            interruptorTema = new Controles.InterruptorTema();
            lblMarcaSub = new Label();
            lblMarca = new Label();
            logo = new Controles.LogoAnimado();
            pnlContenido = new Panel();
            tmrAlarmas = new System.Windows.Forms.Timer(components);
            pnlLateral.SuspendLayout();
            SuspendLayout();
            //
            // pnlLateral
            //
            pnlLateral.Controls.Add(navSalir);
            pnlLateral.Controls.Add(navConsejos);
            pnlLateral.Controls.Add(navRecordatorios);
            pnlLateral.Controls.Add(navHistorial);
            pnlLateral.Controls.Add(navDiagnostico);
            pnlLateral.Controls.Add(navMascotas);
            pnlLateral.Controls.Add(navInicio);
            pnlLateral.Controls.Add(interruptorTema);
            pnlLateral.Controls.Add(lblMarcaSub);
            pnlLateral.Controls.Add(lblMarca);
            pnlLateral.Controls.Add(logo);
            pnlLateral.Dock = DockStyle.Left;
            pnlLateral.Location = new Point(0, 0);
            pnlLateral.Name = "pnlLateral";
            pnlLateral.SeparadorInferior = 84;
            pnlLateral.Size = new Size(256, 800);
            pnlLateral.TabIndex = 0;
            //
            // logo
            //
            logo.Location = new Point(18, 22);
            logo.Name = "logo";
            logo.Size = new Size(52, 52);
            logo.TabIndex = 0;
            //
            // lblMarca
            //
            lblMarca.AutoSize = true;
            lblMarca.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblMarca.Location = new Point(70, 29);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(92, 19);
            lblMarca.TabIndex = 1;
            lblMarca.Text = "Diagnostica";
            //
            // lblMarcaSub
            //
            lblMarcaSub.AutoSize = true;
            lblMarcaSub.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblMarcaSub.Location = new Point(71, 49);
            lblMarcaSub.Name = "lblMarcaSub";
            lblMarcaSub.Size = new Size(62, 13);
            lblMarcaSub.TabIndex = 2;
            lblMarcaSub.Tag = "apagado";
            lblMarcaSub.Text = "tu Mascota";
            //
            // interruptorTema
            //
            interruptorTema.Location = new Point(180, 33);
            interruptorTema.Name = "interruptorTema";
            interruptorTema.Size = new Size(58, 30);
            interruptorTema.TabIndex = 3;
            //
            // navInicio
            //
            navInicio.Icono = "\uE80F";
            navInicio.Location = new Point(16, 112);
            navInicio.Name = "navInicio";
            navInicio.Size = new Size(224, 50);
            navInicio.TabIndex = 4;
            navInicio.Text = "Inicio";
            navInicio.Click += navInicio_Click;
            //
            // navMascotas
            //
            navMascotas.Icono = "huella";
            navMascotas.Location = new Point(16, 164);
            navMascotas.Name = "navMascotas";
            navMascotas.Size = new Size(224, 50);
            navMascotas.TabIndex = 5;
            navMascotas.Text = "Mis Mascotas";
            navMascotas.Click += navMascotas_Click;
            //
            // navDiagnostico
            //
            navDiagnostico.Icono = "\uE95E";
            navDiagnostico.Location = new Point(16, 216);
            navDiagnostico.Name = "navDiagnostico";
            navDiagnostico.Size = new Size(224, 50);
            navDiagnostico.TabIndex = 6;
            navDiagnostico.Text = "Análisis de Síntomas";
            navDiagnostico.Click += navDiagnostico_Click;
            //
            // navHistorial
            //
            navHistorial.Icono = "\uE81C";
            navHistorial.Location = new Point(16, 268);
            navHistorial.Name = "navHistorial";
            navHistorial.Size = new Size(224, 50);
            navHistorial.TabIndex = 7;
            navHistorial.Text = "Historial";
            navHistorial.Click += navHistorial_Click;
            //
            // navRecordatorios
            //
            navRecordatorios.Icono = "\uEA8F";
            navRecordatorios.Location = new Point(16, 320);
            navRecordatorios.Name = "navRecordatorios";
            navRecordatorios.Size = new Size(224, 50);
            navRecordatorios.TabIndex = 8;
            navRecordatorios.Text = "Alertas";
            navRecordatorios.Click += navRecordatorios_Click;
            //
            // navConsejos
            //
            navConsejos.Icono = "\uEA80";
            navConsejos.Location = new Point(16, 372);
            navConsejos.Name = "navConsejos";
            navConsejos.Size = new Size(224, 50);
            navConsejos.TabIndex = 9;
            navConsejos.Text = "Consejos";
            navConsejos.Click += navConsejos_Click;
            //
            // navSalir
            //
            navSalir.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            navSalir.Icono = "\uE7E8";
            navSalir.Location = new Point(16, 732);
            navSalir.Name = "navSalir";
            navSalir.Peligro = true;
            navSalir.Size = new Size(224, 50);
            navSalir.TabIndex = 10;
            navSalir.Text = "Salir";
            navSalir.Click += navSalir_Click;
            //
            // pnlContenido
            //
            pnlContenido.Dock = DockStyle.Fill;
            pnlContenido.Location = new Point(256, 0);
            pnlContenido.Name = "pnlContenido";
            pnlContenido.Size = new Size(1024, 800);
            pnlContenido.TabIndex = 1;
            //
            // tmrAlarmas
            //
            tmrAlarmas.Interval = 15000;
            tmrAlarmas.Tick += tmrAlarmas_Tick;
            //
            // FrmPrincipal
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1280, 800);
            Controls.Add(pnlContenido);
            Controls.Add(pnlLateral);
            MinimumSize = new Size(980, 680);
            Name = "FrmPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Diagnostica tu Mascota";
            Load += FrmPrincipal_Load;
            pnlLateral.ResumeLayout(false);
            pnlLateral.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Controles.PanelLateral pnlLateral;
        private Controles.LogoAnimado logo;
        private Label lblMarca;
        private Label lblMarcaSub;
        private Controles.InterruptorTema interruptorTema;
        private Controles.ElementoNav navInicio;
        private Controles.ElementoNav navMascotas;
        private Controles.ElementoNav navDiagnostico;
        private Controles.ElementoNav navHistorial;
        private Controles.ElementoNav navRecordatorios;
        private Controles.ElementoNav navConsejos;
        private Controles.ElementoNav navSalir;
        private Panel pnlContenido;
        private System.Windows.Forms.Timer tmrAlarmas;
    }
}
