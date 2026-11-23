namespace DiagnosticaTuMascota.Formularios
{
    partial class FrmEditarRecordatorio
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
            titulo = new Controles.TituloSeccion();
            lblMascota = new Label();
            cmbMascota = new Controles.ListaDesplegable();
            lblPrioridad = new Label();
            cmbPrioridad = new Controles.ListaDesplegable();
            lblFecha = new Label();
            selFecha = new Controles.SelectorFecha();
            lblHora = new Label();
            cmbHora = new Controles.ListaDesplegable();
            cmbMinuto = new Controles.ListaDesplegable();
            lblTitulo = new Label();
            txtTitulo = new Controles.CajaTexto();
            lblMensaje = new Label();
            txtMensaje = new Controles.CajaTexto();
            btnCancelar = new Controles.BotonRedondeado();
            btnGuardar = new Controles.BotonRedondeado();
            SuspendLayout();
            //
            // titulo
            //
            titulo.ColorIcono = Color.FromArgb(245, 158, 11);
            titulo.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            titulo.Icono = "\uEA8F";
            titulo.Location = new Point(36, 32);
            titulo.Name = "titulo";
            titulo.Size = new Size(460, 44);
            titulo.TabIndex = 0;
            titulo.Text = "Crear Alerta";
            titulo.TextoDeColor = true;
            //
            // lblMascota
            //
            lblMascota.AutoSize = true;
            lblMascota.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblMascota.Location = new Point(36, 98);
            lblMascota.Name = "lblMascota";
            lblMascota.Tag = "apagado";
            lblMascota.Text = "Mascota";
            //
            // cmbMascota
            //
            cmbMascota.Editable = true;
            cmbMascota.Location = new Point(36, 120);
            cmbMascota.Name = "cmbMascota";
            cmbMascota.Size = new Size(296, 48);
            cmbMascota.TabIndex = 1;
            //
            // lblPrioridad
            //
            lblPrioridad.AutoSize = true;
            lblPrioridad.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPrioridad.Location = new Point(348, 98);
            lblPrioridad.Name = "lblPrioridad";
            lblPrioridad.Tag = "apagado";
            lblPrioridad.Text = "Prioridad";
            //
            // cmbPrioridad
            //
            cmbPrioridad.Location = new Point(348, 120);
            cmbPrioridad.Name = "cmbPrioridad";
            cmbPrioridad.Size = new Size(296, 48);
            cmbPrioridad.TabIndex = 2;
            //
            // lblFecha
            //
            lblFecha.AutoSize = true;
            lblFecha.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblFecha.Location = new Point(36, 182);
            lblFecha.Name = "lblFecha";
            lblFecha.Tag = "apagado";
            lblFecha.Text = "Fecha de la Alarma";
            //
            // selFecha
            //
            selFecha.Location = new Point(36, 204);
            selFecha.Name = "selFecha";
            selFecha.Size = new Size(296, 48);
            selFecha.TabIndex = 3;
            //
            // lblHora
            //
            lblHora.AutoSize = true;
            lblHora.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblHora.Location = new Point(348, 182);
            lblHora.Name = "lblHora";
            lblHora.Tag = "apagado";
            lblHora.Text = "Hora de la Alarma";
            //
            // cmbHora
            //
            cmbHora.Location = new Point(348, 204);
            cmbHora.Name = "cmbHora";
            cmbHora.Size = new Size(144, 48);
            cmbHora.TabIndex = 4;
            //
            // cmbMinuto
            //
            cmbMinuto.Location = new Point(500, 204);
            cmbMinuto.Name = "cmbMinuto";
            cmbMinuto.Size = new Size(144, 48);
            cmbMinuto.TabIndex = 5;
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblTitulo.Location = new Point(36, 266);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Tag = "apagado";
            lblTitulo.Text = "Título de Alerta";
            //
            // txtTitulo
            //
            txtTitulo.Location = new Point(36, 288);
            txtTitulo.Name = "txtTitulo";
            txtTitulo.PlaceholderText = "Ej: Dosis de desparasitante";
            txtTitulo.Size = new Size(608, 48);
            txtTitulo.TabIndex = 6;
            //
            // lblMensaje
            //
            lblMensaje.AutoSize = true;
            lblMensaje.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblMensaje.Location = new Point(36, 350);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Tag = "apagado";
            lblMensaje.Text = "Mensaje / Instrucciones";
            //
            // txtMensaje
            //
            txtMensaje.Location = new Point(36, 372);
            txtMensaje.Multiline = true;
            txtMensaje.Name = "txtMensaje";
            txtMensaje.Size = new Size(608, 80);
            txtMensaje.TabIndex = 7;
            //
            // btnCancelar
            //
            btnCancelar.Estilo = Controles.EstiloBoton.Fantasma;
            btnCancelar.Font = new Font("Segoe UI", 10F);
            btnCancelar.Location = new Point(352, 476);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(130, 52);
            btnCancelar.TabIndex = 9;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            //
            // btnGuardar
            //
            btnGuardar.ColorAcento = Color.FromArgb(245, 158, 11);
            btnGuardar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnGuardar.Location = new Point(488, 476);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(160, 52);
            btnGuardar.TabIndex = 8;
            btnGuardar.Text = "Guardar Alerta";
            btnGuardar.Click += btnGuardar_Click;
            //
            // FrmEditarRecordatorio
            //
            AcceptButton = btnGuardar;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(680, 560);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            Controls.Add(txtMensaje);
            Controls.Add(lblMensaje);
            Controls.Add(txtTitulo);
            Controls.Add(lblTitulo);
            Controls.Add(cmbMinuto);
            Controls.Add(cmbHora);
            Controls.Add(lblHora);
            Controls.Add(selFecha);
            Controls.Add(lblFecha);
            Controls.Add(cmbPrioridad);
            Controls.Add(lblPrioridad);
            Controls.Add(cmbMascota);
            Controls.Add(lblMascota);
            Controls.Add(titulo);
            Name = "FrmEditarRecordatorio";
            Text = "Crear Alerta";
            Load += FrmEditarRecordatorio_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controles.TituloSeccion titulo;
        private Label lblMascota;
        private Controles.ListaDesplegable cmbMascota;
        private Label lblPrioridad;
        private Controles.ListaDesplegable cmbPrioridad;
        private Label lblFecha;
        private Controles.SelectorFecha selFecha;
        private Label lblHora;
        private Controles.ListaDesplegable cmbHora;
        private Controles.ListaDesplegable cmbMinuto;
        private Label lblTitulo;
        private Controles.CajaTexto txtTitulo;
        private Label lblMensaje;
        private Controles.CajaTexto txtMensaje;
        private Controles.BotonRedondeado btnCancelar;
        private Controles.BotonRedondeado btnGuardar;
    }
}
