namespace DiagnosticaTuMascota.Formularios
{
    partial class FrmMascotas
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
            btnAgregar = new Controles.BotonRedondeado();
            flpMascotas = new FlowLayoutPanel();
            vacio = new Controles.EstadoVacio();
            SuspendLayout();
            //
            // encabezado
            //
            encabezado.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            encabezado.Icono = "huella";
            encabezado.Location = new Point(48, 44);
            encabezado.Name = "encabezado";
            encabezado.Size = new Size(680, 100);
            encabezado.Subtitulo = "Gestiona el perfil de tus mascotas y su información clínica.";
            encabezado.TabIndex = 0;
            encabezado.Titulo = "Mis Mascotas";
            //
            // btnAgregar
            //
            btnAgregar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAgregar.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnAgregar.Icono = "\uE710";
            btnAgregar.Location = new Point(768, 58);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(212, 54);
            btnAgregar.TabIndex = 1;
            btnAgregar.Text = "Agregar Mascota";
            btnAgregar.Click += btnAgregar_Click;
            //
            // flpMascotas
            //
            flpMascotas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flpMascotas.AutoScroll = true;
            flpMascotas.Location = new Point(48, 160);
            flpMascotas.Name = "flpMascotas";
            flpMascotas.Size = new Size(940, 620);
            flpMascotas.TabIndex = 2;
            flpMascotas.Resize += flpMascotas_Resize;
            //
            // vacio
            //
            vacio.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            vacio.Location = new Point(48, 220);
            vacio.Name = "vacio";
            vacio.Size = new Size(928, 140);
            vacio.TabIndex = 3;
            vacio.Text = "Aún no has registrado ninguna mascota.";
            vacio.Visible = false;
            //
            // FrmMascotas
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1024, 800);
            Controls.Add(vacio);
            Controls.Add(flpMascotas);
            Controls.Add(btnAgregar);
            Controls.Add(encabezado);
            Name = "FrmMascotas";
            Text = "Mis Mascotas";
            Load += FrmMascotas_Load;
            ResumeLayout(false);
        }

        #endregion

        private Controles.EncabezadoPagina encabezado;
        private Controles.BotonRedondeado btnAgregar;
        private FlowLayoutPanel flpMascotas;
        private Controles.EstadoVacio vacio;
    }
}
