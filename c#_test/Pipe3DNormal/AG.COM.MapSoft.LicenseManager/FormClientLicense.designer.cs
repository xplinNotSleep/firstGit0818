namespace AG.COM.MapSoft.LicenseManager
{
    partial class FormClientLicense
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormClientLicense));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btnRegister = new System.Windows.Forms.ToolStripMenuItem();
            this.获得机器码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpLimitDate = new System.Windows.Forms.DateTimePicker();
            this.lblName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRegister,
            this.获得机器码ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(289, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // btnRegister
            // 
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(68, 21);
            this.btnRegister.Text = "注册许可";
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // 获得机器码ToolStripMenuItem
            // 
            this.获得机器码ToolStripMenuItem.Name = "获得机器码ToolStripMenuItem";
            this.获得机器码ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.获得机器码ToolStripMenuItem.Text = "获得机器码";
            this.获得机器码ToolStripMenuItem.Click += new System.EventHandler(this.获得机器码ToolStripMenuItem_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "许可有效期：";
            // 
            // dtpLimitDate
            // 
            this.dtpLimitDate.Enabled = false;
            this.dtpLimitDate.Location = new System.Drawing.Point(104, 62);
            this.dtpLimitDate.Name = "dtpLimitDate";
            this.dtpLimitDate.Size = new System.Drawing.Size(160, 21);
            this.dtpLimitDate.TabIndex = 11;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(102, 39);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 12);
            this.lblName.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "产品名称：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(104, 35);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(160, 21);
            this.txtName.TabIndex = 14;
            this.txtName.Visible = false;
            // 
            // FormClientLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 102);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpLimitDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FormClientLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "许可管理器";
            this.Load += new System.EventHandler(this.FormClientLicense_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnRegister;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpLimitDate;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ToolStripMenuItem 获得机器码ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
    }
}

