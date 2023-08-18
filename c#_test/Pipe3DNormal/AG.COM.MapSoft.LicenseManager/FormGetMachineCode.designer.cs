namespace AG.COM.MapSoft.LicenseManager
{
    partial class FormGetMachineCode
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtMechineCode = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtMechineCode
            // 
            this.txtMechineCode.Location = new System.Drawing.Point(29, 21);
            this.txtMechineCode.Name = "txtMechineCode";
            this.txtMechineCode.Size = new System.Drawing.Size(132, 21);
            this.txtMechineCode.TabIndex = 0;
            // 
            // FormGetMachineCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(190, 63);
            this.Controls.Add(this.txtMechineCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGetMachineCode";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "机器码";
            this.Load += new System.EventHandler(this.FormGetMachineCode_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMechineCode;
    }
}