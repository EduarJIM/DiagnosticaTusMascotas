namespace DiagnosticaTuMascota.Formularios
{
    partial class FrmConfirmar
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
            icono = new Controles.TituloSeccion();
            lblTitulo = new Label();
            lblMensaje = new Label();
            btnCancelar = new Controles.BotonRedondeado();
            btnAceptar = new Controles.BotonRedondeado();
            SuspendLayout();
            //
            // icono
            //
            icono.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            icono.Icono = "\uE7BA";
            icono.Location = new Point(36, 36);
            icono.Name = "icono";
            icono.Size = new Size(44, 52);
            icono.TabIndex = 0;
            //
            // lblTitulo
            //
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTitulo.Location = new Point(88, 42);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Título";
            //
            // lblMensaje
            //
            lblMensaje.Font = new Font("Segoe UI", 10.5F);
            lblMensaje.Location = new Point(40, 104);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(440, 68);
            lblMensaje.TabIndex = 2;
            lblMensaje.Tag = "apagado";
            lblMensaje.Text = "Mensaje";
            //
            // btnCancelar
            //
            btnCancelar.Estilo = Controles.EstiloBoton.Fantasma;
            btnCancelar.Font = new Font("Segoe UI", 10F);
            btnCancelar.Location = new Point(196, 188);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(130, 52);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            //
            // btnAceptar
            //
            btnAceptar.ColorAcento = Color.FromArgb(239, 68, 68);
            btnAceptar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAceptar.Location = new Point(332, 188);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(152, 52);
            btnAceptar.TabIndex = 3;
            btnAceptar.Text = "Aceptar";
            btnAceptar.Click += btnAceptar_Click;
            //
            // FrmConfirmar
            //
            AcceptButton = btnAceptar;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(520, 268);
            Controls.Add(btnAceptar);
            Controls.Add(btnCancelar);
            Controls.Add(lblMensaje);
            Controls.Add(lblTitulo);
            Controls.Add(icono);
            Name = "FrmConfirmar";
            Text = "Confirmar";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controles.TituloSeccion icono;
        private Label lblTitulo;
        private Label lblMensaje;
        private Controles.BotonRedondeado btnCancelar;
        private Controles.BotonRedondeado btnAceptar;
    }
}
