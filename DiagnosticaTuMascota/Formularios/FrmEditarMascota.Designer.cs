namespace DiagnosticaTuMascota.Formularios
{
    partial class FrmEditarMascota
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
            pnlDesplazable = new Panel();
            flpHistorial = new FlowLayoutPanel();
            tituloHistorial = new Controles.TituloSeccion();
            btnGuardar = new Controles.BotonRedondeado();
            btnCancelar = new Controles.BotonRedondeado();
            txtVeterinario = new Controles.CajaTexto();
            lblVeterinario = new Label();
            txtClinica = new Controles.CajaTexto();
            lblClinica = new Label();
            txtAntecedentes = new Controles.CajaTexto();
            lblAntecedentes = new Label();
            txtVacunas = new Controles.CajaTexto();
            lblVacunas = new Label();
            cmbSexo = new Controles.ListaDesplegable();
            lblSexo = new Label();
            txtEdad = new Controles.CajaTexto();
            lblEdad = new Label();
            txtRaza = new Controles.CajaTexto();
            lblRaza = new Label();
            txtNombre = new Controles.CajaTexto();
            lblNombre = new Label();
            titulo = new Controles.TituloSeccion();
            pnlDesplazable.SuspendLayout();
            SuspendLayout();
            //
            // pnlDesplazable
            //
            pnlDesplazable.AutoScroll = true;
            pnlDesplazable.Controls.Add(flpHistorial);
            pnlDesplazable.Controls.Add(tituloHistorial);
            pnlDesplazable.Controls.Add(btnGuardar);
            pnlDesplazable.Controls.Add(btnCancelar);
            pnlDesplazable.Controls.Add(txtVeterinario);
            pnlDesplazable.Controls.Add(lblVeterinario);
            pnlDesplazable.Controls.Add(txtClinica);
            pnlDesplazable.Controls.Add(lblClinica);
            pnlDesplazable.Controls.Add(txtAntecedentes);
            pnlDesplazable.Controls.Add(lblAntecedentes);
            pnlDesplazable.Controls.Add(txtVacunas);
            pnlDesplazable.Controls.Add(lblVacunas);
            pnlDesplazable.Controls.Add(cmbSexo);
            pnlDesplazable.Controls.Add(lblSexo);
            pnlDesplazable.Controls.Add(txtEdad);
            pnlDesplazable.Controls.Add(lblEdad);
            pnlDesplazable.Controls.Add(txtRaza);
            pnlDesplazable.Controls.Add(lblRaza);
            pnlDesplazable.Controls.Add(txtNombre);
            pnlDesplazable.Controls.Add(lblNombre);
            pnlDesplazable.Controls.Add(titulo);
            pnlDesplazable.Dock = DockStyle.Fill;
            pnlDesplazable.Location = new Point(0, 0);
            pnlDesplazable.Name = "pnlDesplazable";
            pnlDesplazable.Size = new Size(720, 760);
            pnlDesplazable.TabIndex = 0;
            //
            // titulo
            //
            titulo.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            titulo.Icono = "huella";
            titulo.Location = new Point(36, 32);
            titulo.Name = "titulo";
            titulo.Size = new Size(560, 44);
            titulo.TabIndex = 0;
            titulo.Text = "Registrar Nueva Mascota";
            //
            // lblNombre
            //
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblNombre.Location = new Point(36, 98);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(55, 17);
            lblNombre.TabIndex = 1;
            lblNombre.Tag = "apagado";
            lblNombre.Text = "Nombre";
            //
            // txtNombre
            //
            txtNombre.Location = new Point(36, 120);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(316, 48);
            txtNombre.TabIndex = 2;
            //
            // lblRaza
            //
            lblRaza.AutoSize = true;
            lblRaza.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblRaza.Location = new Point(368, 98);
            lblRaza.Name = "lblRaza";
            lblRaza.Size = new Size(95, 17);
            lblRaza.TabIndex = 3;
            lblRaza.Tag = "apagado";
            lblRaza.Text = "Especie / Raza";
            //
            // txtRaza
            //
            txtRaza.Location = new Point(368, 120);
            txtRaza.Name = "txtRaza";
            txtRaza.Size = new Size(316, 48);
            txtRaza.TabIndex = 3;
            //
            // lblEdad
            //
            lblEdad.AutoSize = true;
            lblEdad.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblEdad.Location = new Point(36, 182);
            lblEdad.Name = "lblEdad";
            lblEdad.Size = new Size(37, 17);
            lblEdad.TabIndex = 5;
            lblEdad.Tag = "apagado";
            lblEdad.Text = "Edad";
            //
            // txtEdad
            //
            txtEdad.Location = new Point(36, 204);
            txtEdad.Name = "txtEdad";
            txtEdad.PlaceholderText = "Ej: 3 años";
            txtEdad.Size = new Size(316, 48);
            txtEdad.TabIndex = 4;
            //
            // lblSexo
            //
            lblSexo.AutoSize = true;
            lblSexo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSexo.Location = new Point(368, 182);
            lblSexo.Name = "lblSexo";
            lblSexo.Size = new Size(36, 17);
            lblSexo.TabIndex = 7;
            lblSexo.Tag = "apagado";
            lblSexo.Text = "Sexo";
            //
            // cmbSexo
            //
            cmbSexo.Location = new Point(368, 204);
            cmbSexo.Name = "cmbSexo";
            cmbSexo.Size = new Size(316, 48);
            cmbSexo.TabIndex = 5;
            //
            // lblVacunas
            //
            lblVacunas.AutoSize = true;
            lblVacunas.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblVacunas.Location = new Point(36, 266);
            lblVacunas.Name = "lblVacunas";
            lblVacunas.Size = new Size(129, 17);
            lblVacunas.TabIndex = 9;
            lblVacunas.Tag = "apagado";
            lblVacunas.Text = "Registro de Vacunas";
            //
            // txtVacunas
            //
            txtVacunas.Location = new Point(36, 288);
            txtVacunas.Name = "txtVacunas";
            txtVacunas.PlaceholderText = "Ej: Rabia, Parvovirus (Al día)";
            txtVacunas.Size = new Size(648, 48);
            txtVacunas.TabIndex = 6;
            //
            // lblAntecedentes
            //
            lblAntecedentes.AutoSize = true;
            lblAntecedentes.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblAntecedentes.Location = new Point(36, 350);
            lblAntecedentes.Name = "lblAntecedentes";
            lblAntecedentes.Size = new Size(229, 17);
            lblAntecedentes.TabIndex = 11;
            lblAntecedentes.Tag = "apagado";
            lblAntecedentes.Text = "Registro de Enfermedades Previas";
            //
            // txtAntecedentes
            //
            txtAntecedentes.Location = new Point(36, 372);
            txtAntecedentes.Multiline = true;
            txtAntecedentes.Name = "txtAntecedentes";
            txtAntecedentes.PlaceholderText = "Describe cualquier enfermedad que haya tenido...";
            txtAntecedentes.Size = new Size(648, 72);
            txtAntecedentes.TabIndex = 7;
            //
            // lblClinica
            //
            lblClinica.AutoSize = true;
            lblClinica.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblClinica.Location = new Point(36, 458);
            lblClinica.Name = "lblClinica";
            lblClinica.Size = new Size(299, 17);
            lblClinica.TabIndex = 13;
            lblClinica.Tag = "apagado";
            lblClinica.Text = "Antecedentes Clínicos (Alergias, Condiciones)";
            //
            // txtClinica
            //
            txtClinica.Location = new Point(36, 480);
            txtClinica.Multiline = true;
            txtClinica.Name = "txtClinica";
            txtClinica.PlaceholderText = "Ej: Alergia al pollo, asma...";
            txtClinica.Size = new Size(648, 72);
            txtClinica.TabIndex = 8;
            //
            // lblVeterinario
            //
            lblVeterinario.AutoSize = true;
            lblVeterinario.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblVeterinario.Location = new Point(36, 566);
            lblVeterinario.Name = "lblVeterinario";
            lblVeterinario.Size = new Size(176, 17);
            lblVeterinario.TabIndex = 15;
            lblVeterinario.Tag = "apagado";
            lblVeterinario.Text = "Veterinario Tratante / Clínica";
            //
            // txtVeterinario
            //
            txtVeterinario.Location = new Point(36, 588);
            txtVeterinario.Name = "txtVeterinario";
            txtVeterinario.PlaceholderText = "Nombre del doctor o centro veterinario";
            txtVeterinario.Size = new Size(648, 48);
            txtVeterinario.TabIndex = 9;
            //
            // btnCancelar
            //
            btnCancelar.Estilo = Controles.EstiloBoton.Fantasma;
            btnCancelar.Font = new Font("Segoe UI", 10F);
            btnCancelar.Location = new Point(392, 664);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(130, 52);
            btnCancelar.TabIndex = 11;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            //
            // btnGuardar
            //
            btnGuardar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnGuardar.Location = new Point(528, 664);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(160, 52);
            btnGuardar.TabIndex = 10;
            btnGuardar.Text = "Guardar Perfil";
            btnGuardar.Click += btnGuardar_Click;
            //
            // tituloHistorial
            //
            tituloHistorial.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            tituloHistorial.Icono = "\uE81C";
            tituloHistorial.Location = new Point(36, 752);
            tituloHistorial.Name = "tituloHistorial";
            tituloHistorial.Size = new Size(560, 34);
            tituloHistorial.TabIndex = 12;
            tituloHistorial.Text = "Historial de Análisis (Automático)";
            //
            // flpHistorial
            //
            flpHistorial.FlowDirection = FlowDirection.TopDown;
            flpHistorial.Location = new Point(36, 798);
            flpHistorial.Name = "flpHistorial";
            flpHistorial.Size = new Size(648, 80);
            flpHistorial.TabIndex = 13;
            flpHistorial.WrapContents = false;
            //
            // FrmEditarMascota
            //
            AcceptButton = btnGuardar;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(720, 760);
            Controls.Add(pnlDesplazable);
            Name = "FrmEditarMascota";
            Text = "Mascota";
            Load += FrmEditarMascota_Load;
            pnlDesplazable.ResumeLayout(false);
            pnlDesplazable.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlDesplazable;
        private Controles.TituloSeccion titulo;
        private Label lblNombre;
        private Controles.CajaTexto txtNombre;
        private Label lblRaza;
        private Controles.CajaTexto txtRaza;
        private Label lblEdad;
        private Controles.CajaTexto txtEdad;
        private Label lblSexo;
        private Controles.ListaDesplegable cmbSexo;
        private Label lblVacunas;
        private Controles.CajaTexto txtVacunas;
        private Label lblAntecedentes;
        private Controles.CajaTexto txtAntecedentes;
        private Label lblClinica;
        private Controles.CajaTexto txtClinica;
        private Label lblVeterinario;
        private Controles.CajaTexto txtVeterinario;
        private Controles.BotonRedondeado btnCancelar;
        private Controles.BotonRedondeado btnGuardar;
        private Controles.TituloSeccion tituloHistorial;
        private FlowLayoutPanel flpHistorial;
    }
}
