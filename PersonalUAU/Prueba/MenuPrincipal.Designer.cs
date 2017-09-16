namespace Prueba
{
    partial class MenuPrincipal
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.cboDispositivo = new System.Windows.Forms.ComboBox();
            this.picHuella = new System.Windows.Forms.PictureBox();
            this.btnCapturar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnIniciarRegistros = new System.Windows.Forms.Button();
            this.btnAlmacenar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnIdentify = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picHuella)).BeginInit();
            this.SuspendLayout();
            // 
            // cboDispositivo
            // 
            this.cboDispositivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDispositivo.FormattingEnabled = true;
            this.cboDispositivo.Location = new System.Drawing.Point(93, 84);
            this.cboDispositivo.Name = "cboDispositivo";
            this.cboDispositivo.Size = new System.Drawing.Size(379, 28);
            this.cboDispositivo.TabIndex = 0;
            this.cboDispositivo.SelectedIndexChanged += new System.EventHandler(this.cboDispositivo_SelectedIndexChanged);
            // 
            // picHuella
            // 
            this.picHuella.Location = new System.Drawing.Point(28, 158);
            this.picHuella.Name = "picHuella";
            this.picHuella.Size = new System.Drawing.Size(342, 262);
            this.picHuella.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHuella.TabIndex = 1;
            this.picHuella.TabStop = false;
            // 
            // btnCapturar
            // 
            this.btnCapturar.Location = new System.Drawing.Point(28, 426);
            this.btnCapturar.Name = "btnCapturar";
            this.btnCapturar.Size = new System.Drawing.Size(168, 44);
            this.btnCapturar.TabIndex = 2;
            this.btnCapturar.Text = "Iniciar Capturas";
            this.btnCapturar.UseVisualStyleBackColor = true;
            this.btnCapturar.Click += new System.EventHandler(this.btnCapturar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(192, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Formulario de prueba";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(234, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "Dispositivos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(234, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "Vista Previa";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(202, 426);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(168, 44);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Detener";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnIniciarRegistros
            // 
            this.btnIniciarRegistros.Location = new System.Drawing.Point(376, 158);
            this.btnIniciarRegistros.Name = "btnIniciarRegistros";
            this.btnIniciarRegistros.Size = new System.Drawing.Size(147, 52);
            this.btnIniciarRegistros.TabIndex = 7;
            this.btnIniciarRegistros.Text = "Iniciar Registros";
            this.btnIniciarRegistros.UseVisualStyleBackColor = true;
            this.btnIniciarRegistros.Click += new System.EventHandler(this.btnVentana_Click);
            // 
            // btnAlmacenar
            // 
            this.btnAlmacenar.Location = new System.Drawing.Point(28, 486);
            this.btnAlmacenar.Name = "btnAlmacenar";
            this.btnAlmacenar.Size = new System.Drawing.Size(118, 34);
            this.btnAlmacenar.TabIndex = 8;
            this.btnAlmacenar.Text = "Almacenar Imagen";
            this.btnAlmacenar.UseVisualStyleBackColor = true;
            this.btnAlmacenar.Click += new System.EventHandler(this.btnAlmacenar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(28, 526);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(118, 33);
            this.btnEliminar.TabIndex = 9;
            this.btnEliminar.Text = "Eliminar Imagen";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnIdentify
            // 
            this.btnIdentify.Location = new System.Drawing.Point(376, 216);
            this.btnIdentify.Name = "btnIdentify";
            this.btnIdentify.Size = new System.Drawing.Size(147, 49);
            this.btnIdentify.TabIndex = 10;
            this.btnIdentify.Text = "Iniciar Identificacion";
            this.btnIdentify.UseVisualStyleBackColor = true;
            this.btnIdentify.Click += new System.EventHandler(this.btnIdentify_Click);
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 593);
            this.Controls.Add(this.btnIdentify);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnAlmacenar);
            this.Controls.Add(this.btnIniciarRegistros);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCapturar);
            this.Controls.Add(this.picHuella);
            this.Controls.Add(this.cboDispositivo);
            this.Name = "MenuPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picHuella)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox cboDispositivo;
        public System.Windows.Forms.PictureBox picHuella;
        private System.Windows.Forms.Button btnCapturar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnIniciarRegistros;
        private System.Windows.Forms.Button btnAlmacenar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnIdentify;
    }
}

