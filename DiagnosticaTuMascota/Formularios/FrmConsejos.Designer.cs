namespace DiagnosticaTuMascota.Formularios
{
    partial class FrmConsejos
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
            flpTemas = new FlowLayoutPanel();
            tarjeta = new Controles.PanelTarjeta();
            txtContenido = new TextBox();
            tituloTema = new Controles.TituloSeccion();
            tarjeta.SuspendLayout();
            SuspendLayout();
            //
            // encabezado
            //
            encabezado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            encabezado.Icono = "\uEA80";
            encabezado.Location = new Point(48, 44);
            encabezado.Name = "encabezado";
            encabezado.Size = new Size(928, 90);
            encabezado.Subtitulo = "Primeros auxilios según el nivel de urgencia y cuidados del día a día.";
            encabezado.TabIndex = 0;
            encabezado.Titulo = "Consejos de Cuidado";
            //
            // flpTemas
            //
            flpTemas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            flpTemas.AutoScroll = true;
            flpTemas.FlowDirection = FlowDirection.TopDown;
            flpTemas.Location = new Point(44, 156);
            flpTemas.Name = "flpTemas";
            flpTemas.Size = new Size(286, 620);
            flpTemas.TabIndex = 1;
            flpTemas.WrapContents = false;
            //
            // tarjeta
            //
            tarjeta.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tarjeta.Controls.Add(txtContenido);
            tarjeta.Controls.Add(tituloTema);
            tarjeta.Location = new Point(344, 156);
            tarjeta.Name = "tarjeta";
            tarjeta.Size = new Size(632, 610);
            tarjeta.TabIndex = 2;
            //
            // tituloTema
            //
            tituloTema.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tituloTema.Icono = "destello";
            tituloTema.Location = new Point(32, 28);
            tituloTema.Name = "tituloTema";
            tituloTema.Size = new Size(568, 36);
            tituloTema.TabIndex = 0;
            tituloTema.Text = "Tema";
            //
            // txtContenido
            //
            txtContenido.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtContenido.BorderStyle = BorderStyle.None;
            txtContenido.Font = new Font("Segoe UI", 11F);
            txtContenido.Location = new Point(34, 84);
            txtContenido.Multiline = true;
            txtContenido.Name = "txtContenido";
            txtContenido.ReadOnly = true;
            txtContenido.ScrollBars = ScrollBars.Vertical;
            txtContenido.Size = new Size(570, 498);
            txtContenido.TabIndex = 1;
            txtContenido.TabStop = false;
            //
            // FrmConsejos
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1024, 800);
            Controls.Add(tarjeta);
            Controls.Add(flpTemas);
            Controls.Add(encabezado);
            Name = "FrmConsejos";
            Text = "Consejos";
            Load += FrmConsejos_Load;
            tarjeta.ResumeLayout(false);
            tarjeta.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Controles.EncabezadoPagina encabezado;
        private FlowLayoutPanel flpTemas;
        private Controles.PanelTarjeta tarjeta;
        private Controles.TituloSeccion tituloTema;
        private TextBox txtContenido;
    }
}
