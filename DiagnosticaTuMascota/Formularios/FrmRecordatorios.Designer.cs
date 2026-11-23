namespace DiagnosticaTuMascota.Formularios
{
    partial class FrmRecordatorios
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
            btnNueva = new Controles.BotonRedondeado();
            flpRecordatorios = new FlowLayoutPanel();
            vacio = new Controles.EstadoVacio();
            SuspendLayout();
            //
            // encabezado
            //
            encabezado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            encabezado.ColorIcono = Color.FromArgb(245, 158, 11);
            encabezado.Icono = "\uEA8F";
            encabezado.Location = new Point(48, 44);
            encabezado.Name = "encabezado";
            encabezado.Size = new Size(680, 100);
            encabezado.Subtitulo = "Programa vacunas, medicinas y citas. Te avisaremos a la hora indicada.";
            encabezado.TabIndex = 0;
            encabezado.Titulo = "Alertas Activas";
            //
            // btnNueva
            //
            btnNueva.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNueva.ColorAcento = Color.FromArgb(245, 158, 11);
            btnNueva.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnNueva.Icono = "\uE710";
            btnNueva.Location = new Point(788, 58);
            btnNueva.Name = "btnNueva";
            btnNueva.Size = new Size(192, 54);
            btnNueva.TabIndex = 1;
            btnNueva.Text = "Nueva Alerta";
            btnNueva.Click += btnNueva_Click;
            //
            // flpRecordatorios
            //
            flpRecordatorios.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flpRecordatorios.AutoScroll = true;
            flpRecordatorios.FlowDirection = FlowDirection.TopDown;
            flpRecordatorios.Location = new Point(48, 160);
            flpRecordatorios.Name = "flpRecordatorios";
            flpRecordatorios.Size = new Size(936, 620);
            flpRecordatorios.TabIndex = 2;
            flpRecordatorios.WrapContents = false;
            flpRecordatorios.Resize += flpRecordatorios_Resize;
            //
            // vacio
            //
            vacio.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            vacio.Icono = "\uEA8F";
            vacio.Location = new Point(48, 220);
            vacio.Name = "vacio";
            vacio.Size = new Size(928, 140);
            vacio.TabIndex = 3;
            vacio.Text = "No hay alertas activas.";
            vacio.Visible = false;
            //
            // FrmRecordatorios
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1024, 800);
            Controls.Add(vacio);
            Controls.Add(flpRecordatorios);
            Controls.Add(btnNueva);
            Controls.Add(encabezado);
            Name = "FrmRecordatorios";
            Text = "Alertas";
            Load += FrmRecordatorios_Load;
            ResumeLayout(false);
        }

        #endregion

        private Controles.EncabezadoPagina encabezado;
        private Controles.BotonRedondeado btnNueva;
        private FlowLayoutPanel flpRecordatorios;
        private Controles.EstadoVacio vacio;
    }
}
