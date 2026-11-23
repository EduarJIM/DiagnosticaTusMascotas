namespace DiagnosticaTuMascota.Formularios
{
    partial class FrmAsignarConsulta
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
            btnVolver = new Controles.BotonRedondeado();
            barraPaso = new Controles.BarraProgreso();
            lblPaso = new Label();
            tarjeta = new Controles.PanelTarjeta();
            btnGenerar = new Controles.BotonRedondeado();
            txtNotas = new Controles.CajaTexto();
            lblNotas = new Label();
            pnlManual = new Panel();
            txtNombreManual = new Controles.CajaTexto();
            lblNombreManual = new Label();
            cmbMascota = new Controles.ListaDesplegable();
            lblMascota = new Label();
            lblSintomas = new Label();
            lblSubtitulo = new Label();
            lblTitulo = new Label();
            tarjeta.SuspendLayout();
            pnlManual.SuspendLayout();
            SuspendLayout();
            //
            // btnVolver
            //
            btnVolver.Estilo = Controles.EstiloBoton.Enlace;
            btnVolver.Font = new Font("Segoe UI", 10.5F);
            btnVolver.Icono = "\uE72B";
            btnVolver.Location = new Point(40, 26);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(220, 44);
            btnVolver.TabIndex = 0;
            btnVolver.Text = "Volver a Síntomas";
            btnVolver.Click += btnVolver_Click;
            //
            // barraPaso
            //
            barraPaso.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            barraPaso.Location = new Point(742, 44);
            barraPaso.Name = "barraPaso";
            barraPaso.Size = new Size(128, 8);
            barraPaso.TabIndex = 1;
            barraPaso.Valor = 100;
            barraPaso.ValorInicial = 50;
            //
            // lblPaso
            //
            lblPaso.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblPaso.AutoSize = true;
            lblPaso.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblPaso.Location = new Point(884, 39);
            lblPaso.Name = "lblPaso";
            lblPaso.Size = new Size(80, 15);
            lblPaso.TabIndex = 2;
            lblPaso.Tag = "apagado";
            lblPaso.Text = "PASO 2 DE 2";
            //
            // tarjeta
            //
            tarjeta.Anchor = AnchorStyles.Top;
            tarjeta.Controls.Add(btnGenerar);
            tarjeta.Controls.Add(txtNotas);
            tarjeta.Controls.Add(lblNotas);
            tarjeta.Controls.Add(pnlManual);
            tarjeta.Controls.Add(cmbMascota);
            tarjeta.Controls.Add(lblMascota);
            tarjeta.Controls.Add(lblSintomas);
            tarjeta.Controls.Add(lblSubtitulo);
            tarjeta.Controls.Add(lblTitulo);
            tarjeta.Location = new Point(152, 92);
            tarjeta.Name = "tarjeta";
            tarjeta.Radio = 28;
            tarjeta.Size = new Size(720, 660);
            tarjeta.TabIndex = 3;
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitulo.Location = new Point(38, 34);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(240, 37);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Asignar Consulta";
            //
            // lblSubtitulo
            //
            lblSubtitulo.Font = new Font("Segoe UI", 12F);
            lblSubtitulo.Location = new Point(40, 80);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(640, 46);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Tag = "apagado";
            lblSubtitulo.Text = "Selecciona a cuál de tus mascotas le pertenece este análisis de síntomas.";
            //
            // lblSintomas
            //
            lblSintomas.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSintomas.Location = new Point(40, 128);
            lblSintomas.Name = "lblSintomas";
            lblSintomas.Size = new Size(640, 40);
            lblSintomas.TabIndex = 2;
            lblSintomas.Tag = "primario";
            lblSintomas.Text = "Síntomas:";
            //
            // lblMascota
            //
            lblMascota.AutoSize = true;
            lblMascota.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblMascota.Location = new Point(40, 178);
            lblMascota.Name = "lblMascota";
            lblMascota.Size = new Size(150, 20);
            lblMascota.TabIndex = 3;
            lblMascota.Text = "Mascota Registrada";
            //
            // cmbMascota
            //
            cmbMascota.Location = new Point(40, 208);
            cmbMascota.Name = "cmbMascota";
            cmbMascota.Size = new Size(640, 52);
            cmbMascota.TabIndex = 4;
            cmbMascota.SelectedIndexChanged += cmbMascota_SelectedIndexChanged;
            //
            // pnlManual
            //
            pnlManual.Controls.Add(txtNombreManual);
            pnlManual.Controls.Add(lblNombreManual);
            pnlManual.Location = new Point(40, 274);
            pnlManual.Name = "pnlManual";
            pnlManual.Size = new Size(640, 90);
            pnlManual.TabIndex = 5;
            //
            // lblNombreManual
            //
            lblNombreManual.AutoSize = true;
            lblNombreManual.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblNombreManual.Location = new Point(0, 2);
            lblNombreManual.Name = "lblNombreManual";
            lblNombreManual.Size = new Size(170, 20);
            lblNombreManual.TabIndex = 0;
            lblNombreManual.Text = "Nombre de la mascota";
            //
            // txtNombreManual
            //
            txtNombreManual.Location = new Point(0, 32);
            txtNombreManual.Name = "txtNombreManual";
            txtNombreManual.PlaceholderText = "Ej. Max, Luna...";
            txtNombreManual.Size = new Size(640, 52);
            txtNombreManual.TabIndex = 1;
            //
            // lblNotas
            //
            lblNotas.AutoSize = true;
            lblNotas.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblNotas.Location = new Point(40, 376);
            lblNotas.Name = "lblNotas";
            lblNotas.Size = new Size(330, 20);
            lblNotas.TabIndex = 6;
            lblNotas.Text = "Información adicional o contexto importante";
            //
            // txtNotas
            //
            txtNotas.Location = new Point(40, 406);
            txtNotas.Multiline = true;
            txtNotas.Name = "txtNotas";
            txtNotas.PlaceholderText = "Ej. Empezó hace 2 días, comió algo raro...";
            txtNotas.Size = new Size(640, 130);
            txtNotas.TabIndex = 7;
            //
            // btnGenerar
            //
            btnGenerar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnGenerar.Icono = "\uE76C";
            btnGenerar.IconoDerecha = true;
            btnGenerar.Location = new Point(36, 566);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Radio = 16;
            btnGenerar.Size = new Size(648, 66);
            btnGenerar.TabIndex = 8;
            btnGenerar.Text = "Generar Diagnóstico y Guardar";
            btnGenerar.Click += btnGenerar_Click;
            //
            // FrmAsignarConsulta
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            ClientSize = new Size(1024, 800);
            Controls.Add(tarjeta);
            Controls.Add(lblPaso);
            Controls.Add(barraPaso);
            Controls.Add(btnVolver);
            Name = "FrmAsignarConsulta";
            Text = "Asignar Consulta";
            Load += FrmAsignarConsulta_Load;
            tarjeta.ResumeLayout(false);
            tarjeta.PerformLayout();
            pnlManual.ResumeLayout(false);
            pnlManual.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controles.BotonRedondeado btnVolver;
        private Controles.BarraProgreso barraPaso;
        private Label lblPaso;
        private Controles.PanelTarjeta tarjeta;
        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label lblSintomas;
        private Label lblMascota;
        private Controles.ListaDesplegable cmbMascota;
        private Panel pnlManual;
        private Label lblNombreManual;
        private Controles.CajaTexto txtNombreManual;
        private Label lblNotas;
        private Controles.CajaTexto txtNotas;
        private Controles.BotonRedondeado btnGenerar;
    }
}
