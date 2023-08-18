namespace AG.COM.SDM.Utility.Controls
{
    partial class LonLatTextBox
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.nud3 = new System.Windows.Forms.NumericUpDown();
            this.nud2 = new System.Windows.Forms.NumericUpDown();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.nud1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nud3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud1)).BeginInit();
            this.SuspendLayout();
            // 
            // nud3
            // 
            this.nud3.DecimalPlaces = 6;
            this.nud3.Location = new System.Drawing.Point(172, 5);
            this.nud3.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nud3.Name = "nud3";
            this.nud3.Size = new System.Drawing.Size(63, 21);
            this.nud3.TabIndex = 13;
            // 
            // nud2
            // 
            this.nud2.Location = new System.Drawing.Point(88, 5);
            this.nud2.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nud2.Name = "nud2";
            this.nud2.Size = new System.Drawing.Size(63, 21);
            this.nud2.TabIndex = 12;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(237, 7);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(17, 12);
            this.label31.TabIndex = 11;
            this.label31.Text = "″";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(154, 7);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(17, 12);
            this.label32.TabIndex = 10;
            this.label32.Text = "′";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(71, 7);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(17, 12);
            this.label33.TabIndex = 9;
            this.label33.Text = "°";
            // 
            // nud1
            // 
            this.nud1.Location = new System.Drawing.Point(5, 5);
            this.nud1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nud1.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nud1.Name = "nud1";
            this.nud1.Size = new System.Drawing.Size(63, 21);
            this.nud1.TabIndex = 8;
            // 
            // LonLatTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nud3);
            this.Controls.Add(this.nud2);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.nud1);
            this.Name = "LonLatTextBox";
            this.Size = new System.Drawing.Size(249, 30);
            ((System.ComponentModel.ISupportInitialize)(this.nud3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nud3;
        private System.Windows.Forms.NumericUpDown nud2;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.NumericUpDown nud1;
    }
}
