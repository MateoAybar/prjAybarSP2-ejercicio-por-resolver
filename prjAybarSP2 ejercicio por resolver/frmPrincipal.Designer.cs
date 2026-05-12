namespace prjAybarSP2_ejercicio_por_resolver
{
    partial class frmPrincipal
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
            this.lblInfo = new System.Windows.Forms.Label();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.btnMigra = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(12, 94);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(68, 13);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "Informaciòn :";
            this.lblInfo.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtInfo
            // 
            this.txtInfo.BackColor = System.Drawing.SystemColors.Info;
            this.txtInfo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtInfo.Location = new System.Drawing.Point(12, 110);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(234, 282);
            this.txtInfo.TabIndex = 1;
            this.txtInfo.TextChanged += new System.EventHandler(this.txtInfo_TextChanged);
            // 
            // btnMigra
            // 
            this.btnMigra.BackColor = System.Drawing.Color.IndianRed;
            this.btnMigra.Location = new System.Drawing.Point(56, 31);
            this.btnMigra.Name = "btnMigra";
            this.btnMigra.Size = new System.Drawing.Size(143, 36);
            this.btnMigra.TabIndex = 2;
            this.btnMigra.Text = "Inciar Migraciòn";
            this.btnMigra.UseVisualStyleBackColor = false;
            this.btnMigra.Click += new System.EventHandler(this.btnMigra_Click_1);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 404);
            this.Controls.Add(this.btnMigra);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.lblInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Migraciòn";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.Button btnMigra;
    }
}

