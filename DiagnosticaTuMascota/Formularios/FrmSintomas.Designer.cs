namespace DiagnosticaTuMascota.Formularios
{
    partial class FrmSintomas
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
            encabezado = new Controles.EncabezadoPagina();
            flpCategorias = new FlowLayoutPanel();
            tarjetaSintomas = new Controles.PanelTarjeta();
            flpSintomas = new FlowLayoutPanel();
            tituloCategoria = new Controles.TituloSeccion();
            pnlLinea = new Panel();
            lblSeleccionados = new Label();
            lblCantidad = new Label();
            btnContinuar = new Controles.BotonRedondeado();
            tarjetaSintomas.SuspendLayout();
            SuspendLayout();
            //
            // btnVolver
            //
            btnVolver.Estilo = Controles.EstiloBoton.Enlace;
            btnVolver.Font = new Font("Segoe UI", 10.5F);
            btnVolver.Icono = "\uE72B";
            btnVolver.Location = new Point(40, 26);
            btnVolver.Name = "btnVolver";
            btnVolver.Size = new Size(200, 44);
            btnVolver.TabIndex = 0;
            btnVolver.Text = "Volver al inicio";
            btnVolver.Click += btnVolver_Click;
            //
            // barraPaso
            //
            barraPaso.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            barraPaso.Location = new Point(742, 44);
            barraPaso.Name = "barraPaso";
            barraPaso.Size = new Size(128, 8);
            barraPaso.TabIndex = 1;
            barraPaso.Valor = 50;
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
            lblPaso.Text = "PASO 1 DE 2";
            //
            // encabezado
            //
            encabezado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            encabezado.Location = new Point(48, 86);
            encabezado.Name = "encabezado";
            encabezado.Size = new Size(928, 90);
            encabezado.Subtitulo = "Selecciona todos los síntomas que has observado en tu mascota para un análisis preliminar.";
            encabezado.TabIndex = 3;
            encabezado.Titulo = "Análisis de Síntomas";
            //
            // flpCategorias
            //
            flpCategorias.FlowDirection = FlowDirection.TopDown;
            flpCategorias.Location = new Point(44, 192);
            flpCategorias.Name = "flpCategorias";
            flpCategorias.Size = new Size(276, 300);
            flpCategorias.TabIndex = 4;
            flpCategorias.WrapContents = false;
            //
            // tarjetaSintomas
            //
            tarjetaSintomas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tarjetaSintomas.Controls.Add(flpSintomas);
            tarjetaSintomas.Controls.Add(tituloCategoria);
            tarjetaSintomas.Location = new Point(336, 192);
            tarjetaSintomas.Name = "tarjetaSintomas";
            tarjetaSintomas.Size = new Size(640, 474);
            tarjetaSintomas.TabIndex = 5;
            //
            // tituloCategoria
            //
            tituloCategoria.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tituloCategoria.Icono = "destello";
            tituloCategoria.Location = new Point(32, 28);
            tituloCategoria.Name = "tituloCategoria";
            tituloCategoria.Size = new Size(576, 34);
            tituloCategoria.TabIndex = 0;
            tituloCategoria.Text = "Síntomas generales";
            //
            // flpSintomas
            //
            flpSintomas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flpSintomas.AutoScroll = true;
            flpSintomas.Location = new Point(30, 80);
            flpSintomas.Name = "flpSintomas";
            flpSintomas.Size = new Size(586, 370);
            flpSintomas.TabIndex = 1;
            flpSintomas.Resize += flpSintomas_Resize;
            //
            // pnlLinea
            //
            pnlLinea.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlLinea.Location = new Point(48, 690);
            pnlLinea.Name = "pnlLinea";
            pnlLinea.Size = new Size(928, 1);
            pnlLinea.TabIndex = 6;
            //
            // lblSeleccionados
            //
            lblSeleccionados.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblSeleccionados.AutoSize = true;
            lblSeleccionados.Font = new Font("Segoe UI", 10.5F);
            lblSeleccionados.Location = new Point(48, 722);
            lblSeleccionados.Name = "lblSeleccionados";
            lblSeleccionados.Size = new Size(104, 19);
            lblSeleccionados.TabIndex = 7;
            lblSeleccionados.Tag = "apagado";
            lblSeleccionados.Text = "Seleccionados:";
            //
            // lblCantidad
            //
            lblCantidad.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblCantidad.AutoSize = true;
            lblCantidad.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblCantidad.Location = new Point(152, 722);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(80, 19);
            lblCantidad.TabIndex = 8;
            lblCantidad.Tag = "primario";
            lblCantidad.Text = "0 síntomas";
            //
            // btnContinuar
            //
            btnContinuar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnContinuar.Enabled = false;
            btnContinuar.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnContinuar.Icono = "\uE76C";
            btnContinuar.IconoDerecha = true;
            btnContinuar.Location = new Point(752, 704);
            btnContinuar.Name = "btnContinuar";
            btnContinuar.Size = new Size(224, 56);
            btnContinuar.TabIndex = 9;
            btnContinuar.Text = "Continuar Análisis";
            btnContinuar.Click += btnContinuar_Click;
            //
            // FrmSintomas
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1024, 800);
            Controls.Add(btnContinuar);
            Controls.Add(lblCantidad);
            Controls.Add(lblSeleccionados);
            Controls.Add(pnlLinea);
            Controls.Add(tarjetaSintomas);
            Controls.Add(flpCategorias);
            Controls.Add(encabezado);
            Controls.Add(lblPaso);
            Controls.Add(barraPaso);
            Controls.Add(btnVolver);
            Name = "FrmSintomas";
            Text = "Análisis de Síntomas";
            Load += FrmSintomas_Load;
            tarjetaSintomas.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controles.BotonRedondeado btnVolver;
        private Controles.BarraProgreso barraPaso;
        private Label lblPaso;
        private Controles.EncabezadoPagina encabezado;
        private FlowLayoutPanel flpCategorias;
        private Controles.PanelTarjeta tarjetaSintomas;
        private Controles.TituloSeccion tituloCategoria;
        private FlowLayoutPanel flpSintomas;
        private Panel pnlLinea;
        private Label lblSeleccionados;
        private Label lblCantidad;
        private Controles.BotonRedondeado btnContinuar;
    }
}
