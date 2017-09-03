namespace Fase1_version_1._0
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
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
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.TextoArchivo = new System.Windows.Forms.TextBox();
            this.textoerror = new System.Windows.Forms.TextBox();
            this.btanalizar = new System.Windows.Forms.Button();
            this.btsalir = new System.Windows.Forms.Button();
            this.btExaminar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextoArchivo
            // 
            this.TextoArchivo.Location = new System.Drawing.Point(12, 108);
            this.TextoArchivo.Multiline = true;
            this.TextoArchivo.Name = "TextoArchivo";
            this.TextoArchivo.ReadOnly = true;
            this.TextoArchivo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextoArchivo.Size = new System.Drawing.Size(399, 370);
            this.TextoArchivo.TabIndex = 1;
            // 
            // textoerror
            // 
            this.textoerror.Location = new System.Drawing.Point(527, 108);
            this.textoerror.Multiline = true;
            this.textoerror.Name = "textoerror";
            this.textoerror.ReadOnly = true;
            this.textoerror.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textoerror.Size = new System.Drawing.Size(399, 370);
            this.textoerror.TabIndex = 2;
            // 
            // btanalizar
            // 
            this.btanalizar.AutoSize = true;
            this.btanalizar.Enabled = false;
            this.btanalizar.Location = new System.Drawing.Point(125, 13);
            this.btanalizar.Name = "btanalizar";
            this.btanalizar.Size = new System.Drawing.Size(75, 30);
            this.btanalizar.TabIndex = 3;
            this.btanalizar.Text = "Analizar";
            this.btanalizar.UseVisualStyleBackColor = true;
            this.btanalizar.Click += new System.EventHandler(this.btanalizar_Click);
            // 
            // btsalir
            // 
            this.btsalir.AutoSize = true;
            this.btsalir.Location = new System.Drawing.Point(908, 13);
            this.btsalir.Name = "btsalir";
            this.btsalir.Size = new System.Drawing.Size(75, 30);
            this.btsalir.TabIndex = 4;
            this.btsalir.Text = "X";
            this.btsalir.UseVisualStyleBackColor = true;
            this.btsalir.Click += new System.EventHandler(this.btsalir_Click);
            // 
            // btExaminar
            // 
            this.btExaminar.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btExaminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btExaminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btExaminar.Image = ((System.Drawing.Image)(resources.GetObject("btExaminar.Image")));
            this.btExaminar.Location = new System.Drawing.Point(12, 13);
            this.btExaminar.Name = "btExaminar";
            this.btExaminar.Size = new System.Drawing.Size(78, 74);
            this.btExaminar.TabIndex = 0;
            this.btExaminar.UseVisualStyleBackColor = true;
            this.btExaminar.Click += new System.EventHandler(this.btExaminar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(995, 511);
            this.Controls.Add(this.btsalir);
            this.Controls.Add(this.btanalizar);
            this.Controls.Add(this.textoerror);
            this.Controls.Add(this.TextoArchivo);
            this.Controls.Add(this.btExaminar);
            this.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compiladores";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TextoArchivo;
        private System.Windows.Forms.TextBox textoerror;
        private System.Windows.Forms.Button btanalizar;
        private System.Windows.Forms.Button btsalir;
        private System.Windows.Forms.Button btExaminar;
    }
}

