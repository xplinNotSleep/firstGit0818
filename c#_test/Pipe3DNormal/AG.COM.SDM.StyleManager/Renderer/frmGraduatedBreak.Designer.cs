namespace AG.COM.SDM.StyleManager.Renderer
{
    partial class frmGraduatedBreak
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
            this.butOk = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.tbxBreakValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // butOk
            // 
            this.butOk.Location = new System.Drawing.Point(61, 90);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(52, 23);
            this.butOk.TabIndex = 0;
            this.butOk.Text = "确定";
            this.butOk.UseVisualStyleBackColor = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(123, 90);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(52, 23);
            this.butCancel.TabIndex = 1;
            this.butCancel.Text = "取消";
            this.butCancel.UseVisualStyleBackColor = true;
            // 
            // tbxBreakValue
            // 
            this.tbxBreakValue.Location = new System.Drawing.Point(71, 22);
            this.tbxBreakValue.Name = "tbxBreakValue";
            this.tbxBreakValue.Size = new System.Drawing.Size(100, 21);
            this.tbxBreakValue.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "分界值：";
            // 
            // frmGraduatedBreak
            // 
            this.AcceptButton = this.butOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butCancel;
            this.ClientSize = new System.Drawing.Size(187, 129);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxBreakValue);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGraduatedBreak";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改分界值";
            this.Load += new System.EventHandler(this.frmGraduatedBreak_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butOk;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.TextBox tbxBreakValue;
        private System.Windows.Forms.Label label1;
    }
}