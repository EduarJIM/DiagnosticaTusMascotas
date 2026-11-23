namespace DiagnosticaTuMascota.Formularios
{
    partial class FrmAlarma
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
            contenido = new Controles.ContenidoAlarma();
            btnHecho = new Controles.BotonRedondeado();
            SuspendLayout();
            //
            // contenido
            //
            contenido.Location = new Point(40, 44);
            contenido.Name = "contenido";
            contenido.Size = new Size(520, 400);
            contenido.TabIndex = 0;
            //
            // btnHecho
            //
            btnHecho.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnHecho.Icono = "\uE930";
            btnHecho.Location = new Point(46, 464);
            btnHecho.Name = "btnHecho";
            btnHecho.Radio = 18;
            btnHecho.Size = new Size(508, 72);
            btnHecho.TabIndex = 1;
            btnHecho.Text = "Marcar como Hecho";
            btnHecho.Click += btnHecho_Click;
            //
            // FrmAlarma
            //
            AcceptButton = btnHecho;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(600, 572);
            Controls.Add(btnHecho);
            Controls.Add(contenido);
            Name = "FrmAlarma";
            Text = "Alarma";
            ResumeLayout(false);
        }

        #endregion

        private Controles.ContenidoAlarma contenido;
        private Controles.BotonRedondeado btnHecho;
    }
}
