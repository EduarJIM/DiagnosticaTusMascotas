namespace DiagnosticaTuMascota.Formularios
{
    partial class FrmHistorial
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
            encabezado = new Controles.EncabezadoPagina();
            btnNuevo = new Controles.BotonRedondeado();
            btnTodas = new Controles.BotonRedondeado();
            btnLeve = new Controles.BotonRedondeado();
            btnModerado = new Controles.BotonRedondeado();
            btnCritico = new Controles.BotonRedondeado();
            cmbMascota = new Controles.ListaDesplegable();
            flpConsultas = new FlowLayoutPanel();
            vacio = new Controles.EstadoVacio();
            SuspendLayout();
            //
            // encabezado
            //
            encabezado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            encabezado.Icono = "\uE81C";
            encabezado.Location = new Point(48, 44);
            encabezado.Name = "encabezado";
            encabezado.Size = new Size(680, 100);
            encabezado.Subtitulo = "Revisa los diagnósticos guardados, filtra por nivel y abre cualquier análisis.";
            encabezado.TabIndex = 0;
            encabezado.Titulo = "Historial de Consultas";
            //
            // btnNuevo
            //
            btnNuevo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNuevo.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnNuevo.Icono = "\uE710";
            btnNuevo.Location = new Point(768, 58);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(212, 54);
            btnNuevo.TabIndex = 1;
            btnNuevo.Text = "Nuevo Diagnóstico";
            btnNuevo.Click += btnNuevo_Click;
            //
            // btnTodas
            //
            btnTodas.Activo = true;
            btnTodas.Estilo = Controles.EstiloBoton.Pastilla;
            btnTodas.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnTodas.Icono = "\uE9D5";
            btnTodas.Location = new Point(44, 156);
            btnTodas.Name = "btnTodas";
            btnTodas.Size = new Size(112, 54);
            btnTodas.TabIndex = 2;
            btnTodas.Text = "Todas";
            btnTodas.Click += Filtro_Click;
            //
            // btnLeve
            //
            btnLeve.ColorAcento = Color.FromArgb(16, 185, 129);
            btnLeve.Estilo = Controles.EstiloBoton.Pastilla;
            btnLeve.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLeve.Icono = "\uE930";
            btnLeve.Location = new Point(158, 156);
            btnLeve.Name = "btnLeve";
            btnLeve.Size = new Size(104, 54);
            btnLeve.TabIndex = 3;
            btnLeve.Text = "Leve";
            btnLeve.Click += Filtro_Click;
            //
            // btnModerado
            //
            btnModerado.ColorAcento = Color.FromArgb(245, 158, 11);
            btnModerado.Estilo = Controles.EstiloBoton.Pastilla;
            btnModerado.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnModerado.Icono = "\uE9CA";
            btnModerado.Location = new Point(264, 156);
            btnModerado.Name = "btnModerado";
            btnModerado.Size = new Size(140, 54);
            btnModerado.TabIndex = 4;
            btnModerado.Text = "Moderado";
            btnModerado.Click += Filtro_Click;
            //
            // btnCritico
            //
            btnCritico.ColorAcento = Color.FromArgb(239, 68, 68);
            btnCritico.Estilo = Controles.EstiloBoton.Pastilla;
            btnCritico.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCritico.Icono = "\uE7BA";
            btnCritico.Location = new Point(406, 156);
            btnCritico.Name = "btnCritico";
            btnCritico.Size = new Size(120, 54);
            btnCritico.TabIndex = 5;
            btnCritico.Text = "Crítico";
            btnCritico.Click += Filtro_Click;
            //
            // cmbMascota
            //
            cmbMascota.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbMascota.Location = new Point(780, 158);
            cmbMascota.Name = "cmbMascota";
            cmbMascota.Size = new Size(200, 50);
            cmbMascota.TabIndex = 6;
            cmbMascota.SelectedIndexChanged += cmbMascota_SelectedIndexChanged;
            //
            // flpConsultas
            //
            flpConsultas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flpConsultas.AutoScroll = true;
            flpConsultas.FlowDirection = FlowDirection.TopDown;
            flpConsultas.Location = new Point(48, 232);
            flpConsultas.Name = "flpConsultas";
            flpConsultas.Size = new Size(936, 548);
            flpConsultas.TabIndex = 7;
            flpConsultas.WrapContents = false;
            flpConsultas.Resize += flpConsultas_Resize;
            //
            // vacio
            //
            vacio.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            vacio.Icono = "\uE8A5";
            vacio.Location = new Point(48, 280);
            vacio.Name = "vacio";
            vacio.Size = new Size(928, 140);
            vacio.TabIndex = 8;
            vacio.Text = "No hay consultas registradas.";
            vacio.Visible = false;
            //
            // FrmHistorial
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1024, 800);
            Controls.Add(vacio);
            Controls.Add(flpConsultas);
            Controls.Add(cmbMascota);
            Controls.Add(btnCritico);
            Controls.Add(btnModerado);
            Controls.Add(btnLeve);
            Controls.Add(btnTodas);
            Controls.Add(btnNuevo);
            Controls.Add(encabezado);
            Name = "FrmHistorial";
            Text = "Historial";
            Load += FrmHistorial_Load;
            ResumeLayout(false);
        }

        #endregion

        private Controles.EncabezadoPagina encabezado;
        private Controles.BotonRedondeado btnNuevo;
        private Controles.BotonRedondeado btnTodas;
        private Controles.BotonRedondeado btnLeve;
        private Controles.BotonRedondeado btnModerado;
        private Controles.BotonRedondeado btnCritico;
        private Controles.ListaDesplegable cmbMascota;
        private FlowLayoutPanel flpConsultas;
        private Controles.EstadoVacio vacio;
    }
}
