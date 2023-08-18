namespace AG.COM.SDM.StyleManager.Renderer
{
    partial class frmDotDensityRendererExclusion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDotDensityRendererExclusion));
            this.tbxClause = new System.Windows.Forms.TextBox();
            this.butOk = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbxClause
            // 
            this.tbxClause.Location = new System.Drawing.Point(10, 12);
            this.tbxClause.Multiline = true;
            this.tbxClause.Name = "tbxClause";
            this.tbxClause.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxClause.Size = new System.Drawing.Size(284, 127);
            this.tbxClause.TabIndex = 3;
            // 
            // butOk
            // 
            this.butOk.Location = new System.Drawing.Point(191, 147);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(44, 23);
            this.butOk.TabIndex = 6;
            this.butOk.Text = "确定";
            this.butOk.UseVisualStyleBackColor = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(250, 147);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(44, 23);
            this.butCancel.TabIndex = 6;
            this.butCancel.Text = "取消";
            this.butCancel.UseVisualStyleBackColor = true;
            // 
            // frmDotDensityRendererExclusion
            // 
            this.AcceptButton = this.butOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butCancel;
            this.ClientSize = new System.Drawing.Size(304, 177);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butOk);
            this.Controls.Add(this.tbxClause);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDotDensityRendererExclusion";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "属性过滤";
            this.Load += new System.EventHandler(this.frmDotDensityRendererExclusion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxClause;
        private System.Windows.Forms.Button butOk;
        private System.Windows.Forms.Button butCancel;
    }
}