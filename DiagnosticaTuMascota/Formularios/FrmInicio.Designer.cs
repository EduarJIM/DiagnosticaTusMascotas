namespace DiagnosticaTuMascota.Formularios
{
    partial class FrmInicio
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
            tlpInicio = new TableLayoutPanel();
            pnlIzquierda = new Panel();
            aviso = new Controles.AvisoMedico();
            tarjetaHistorial = new Controles.TarjetaFuncion();
            tarjetaMascotas = new Controles.TarjetaFuncion();
            tarjetaAnalisis = new Controles.TarjetaFuncion();
            encabezado = new Controles.EncabezadoPagina();
            hero = new Controles.HeroAnimado();
            tlpInicio.SuspendLayout();
            pnlIzquierda.SuspendLayout();
            SuspendLayout();
            //
            // tlpInicio
            //
            tlpInicio.ColumnCount = 2;
            tlpInicio.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 56F));
            tlpInicio.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44F));
            tlpInicio.Controls.Add(pnlIzquierda, 0, 0);
            tlpInicio.Controls.Add(hero, 1, 0);
            tlpInicio.Dock = DockStyle.Fill;
            tlpInicio.Location = new Point(0, 0);
            tlpInicio.Margin = new Padding(0);
            tlpInicio.Name = "tlpInicio";
            tlpInicio.RowCount = 1;
            tlpInicio.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpInicio.Size = new Size(1024, 800);
            tlpInicio.TabIndex = 0;
            //
            // pnlIzquierda
            //
            pnlIzquierda.AutoScroll = true;
            pnlIzquierda.Controls.Add(aviso);
            pnlIzquierda.Controls.Add(tarjetaHistorial);
            pnlIzquierda.Controls.Add(tarjetaMascotas);
            pnlIzquierda.Controls.Add(tarjetaAnalisis);
            pnlIzquierda.Controls.Add(encabezado);
            pnlIzquierda.Dock = DockStyle.Fill;
            pnlIzquierda.Location = new Point(0, 0);
            pnlIzquierda.Margin = new Padding(0);
            pnlIzquierda.Name = "pnlIzquierda";
            pnlIzquierda.Size = new Size(512, 800);
            pnlIzquierda.TabIndex = 0;
            pnlIzquierda.Resize += pnlIzquierda_Resize;
            //
            // encabezado
            //
            encabezado.Icono = "destello";
            encabezado.Location = new Point(44, 44);
            encabezado.Name = "encabezado";
            encabezado.RetrasoEntrada = 0;
            encabezado.Size = new Size(430, 90);
            encabezado.Subtitulo = "Sistema de Orientación de Salud para Mascotas";
            encabezado.TabIndex = 0;
            encabezado.Titulo = "Bienvenido";
            //
            // tarjetaAnalisis
            //
            tarjetaAnalisis.Descripcion = "Evalúa los síntomas de tu mascota y recibe orientación inmediata";
            tarjetaAnalisis.Icono = "\uE95E";
            tarjetaAnalisis.Location = new Point(34, 150);
            tarjetaAnalisis.Name = "tarjetaAnalisis";
            tarjetaAnalisis.RetrasoEntrada = 100;
            tarjetaAnalisis.Size = new Size(450, 176);
            tarjetaAnalisis.TabIndex = 1;
            tarjetaAnalisis.Titulo = "Análisis de Síntomas";
            tarjetaAnalisis.Click += tarjetaAnalisis_Click;
            //
            // tarjetaMascotas
            //
            tarjetaMascotas.ColorFin = Color.FromArgb(59, 130, 246);
            tarjetaMascotas.ColorInicio = Color.FromArgb(20, 184, 166);
            tarjetaMascotas.Descripcion = "Gestiona los perfiles de tus mascotas";
            tarjetaMascotas.Icono = "huella";
            tarjetaMascotas.Location = new Point(34, 326);
            tarjetaMascotas.Name = "tarjetaMascotas";
            tarjetaMascotas.RetrasoEntrada = 150;
            tarjetaMascotas.Size = new Size(225, 196);
            tarjetaMascotas.TabIndex = 2;
            tarjetaMascotas.Titulo = "Mis Mascotas";
            tarjetaMascotas.Click += tarjetaMascotas_Click;
            //
            // tarjetaHistorial
            //
            tarjetaHistorial.ColorFin = Color.FromArgb(99, 102, 241);
            tarjetaHistorial.ColorInicio = Color.FromArgb(168, 85, 247);
            tarjetaHistorial.Descripcion = "Revisa consultas anteriores";
            tarjetaHistorial.Icono = "\uE81C";
            tarjetaHistorial.Location = new Point(259, 326);
            tarjetaHistorial.Name = "tarjetaHistorial";
            tarjetaHistorial.RetrasoEntrada = 150;
            tarjetaHistorial.Size = new Size(225, 196);
            tarjetaHistorial.TabIndex = 3;
            tarjetaHistorial.Titulo = "Historial";
            tarjetaHistorial.Click += tarjetaHistorial_Click;
            //
            // aviso
            //
            aviso.Location = new Point(44, 540);
            aviso.Name = "aviso";
            aviso.RetrasoEntrada = 250;
            aviso.Size = new Size(430, 150);
            aviso.TabIndex = 4;
            aviso.Text = "Esta herramienta proporciona orientación general y NO reemplaza el diagnóstico veterinario profesional. Siempre consulte con un veterinario certificado para obtener un diagnóstico preciso y tratamiento adecuado para su mascota.";
            //
            // hero
            //
            hero.Dock = DockStyle.Fill;
            hero.Location = new Point(512, 0);
            hero.Margin = new Padding(0);
            hero.Name = "hero";
            hero.Size = new Size(512, 800);
            hero.TabIndex = 1;
            //
            // FrmInicio
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1024, 800);
            Controls.Add(tlpInicio);
            Name = "FrmInicio";
            Text = "Inicio";
            tlpInicio.ResumeLayout(false);
            pnlIzquierda.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpInicio;
        private Panel pnlIzquierda;
        private Controles.EncabezadoPagina encabezado;
        private Controles.TarjetaFuncion tarjetaAnalisis;
        private Controles.TarjetaFuncion tarjetaMascotas;
        private Controles.TarjetaFuncion tarjetaHistorial;
        private Controles.AvisoMedico aviso;
        private Controles.HeroAnimado hero;
    }
}
