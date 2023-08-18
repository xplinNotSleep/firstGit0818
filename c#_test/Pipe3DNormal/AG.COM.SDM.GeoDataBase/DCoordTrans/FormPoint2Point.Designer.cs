namespace AG.COM.SDM.GeoDataBase.DCoordTrans
{
    partial class FormPoint2Point
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtSrcPrj = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtToPrj = new System.Windows.Forms.TextBox();
            this.btnToPrj = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSrcX = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSrcY = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtToX = new System.Windows.Forms.TextBox();
            this.txtToY = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnFromPrj = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "源坐标系";
            // 
            // txtSrcPrj
            // 
            this.txtSrcPrj.Location = new System.Drawing.Point(18, 32);
            this.txtSrcPrj.Name = "txtSrcPrj";
            this.txtSrcPrj.Size = new System.Drawing.Size(308, 21);
            this.txtSrcPrj.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(175, 66);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(41, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "=>";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "目标坐标系";
            // 
            // txtToPrj
            // 
            this.txtToPrj.Location = new System.Drawing.Point(18, 85);
            this.txtToPrj.Name = "txtToPrj";
            this.txtToPrj.Size = new System.Drawing.Size(308, 21);
            this.txtToPrj.TabIndex = 1;
            // 
            // btnToPrj
            // 
            this.btnToPrj.Image = global::AG.COM.SDM.GeoDataBase.Properties.Resources.fileopen;
            this.btnToPrj.Location = new System.Drawing.Point(332, 85);
            this.btnToPrj.Name = "btnToPrj";
            this.btnToPrj.Size = new System.Drawing.Size(24, 21);
            this.btnToPrj.TabIndex = 2;
            this.btnToPrj.UseVisualStyleBackColor = true;
            this.btnToPrj.Click += new System.EventHandler(this.btnToPrj_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "源坐标点";
            // 
            // txtSrcX
            // 
            this.txtSrcX.Location = new System.Drawing.Point(69, 52);
            this.txtSrcX.Name = "txtSrcX";
            this.txtSrcX.Size = new System.Drawing.Size(100, 21);
            this.txtSrcX.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "X坐标：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "Y坐标：";
            // 
            // txtSrcY
            // 
            this.txtSrcY.Location = new System.Drawing.Point(69, 81);
            this.txtSrcY.Name = "txtSrcY";
            this.txtSrcY.Size = new System.Drawing.Size(100, 21);
            this.txtSrcY.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(226, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "目标坐标点";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(226, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "X坐标：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(226, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "Y坐标：";
            // 
            // txtToX
            // 
            this.txtToX.Location = new System.Drawing.Point(279, 52);
            this.txtToX.Name = "txtToX";
            this.txtToX.Size = new System.Drawing.Size(100, 21);
            this.txtToX.TabIndex = 4;
            // 
            // txtToY
            // 
            this.txtToY.Location = new System.Drawing.Point(279, 81);
            this.txtToY.Name = "txtToY";
            this.txtToY.Size = new System.Drawing.Size(100, 21);
            this.txtToY.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtToY);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Controls.Add(this.txtSrcY);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtToX);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtSrcX);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(12, 145);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(390, 110);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtSrcPrj);
            this.groupBox2.Controls.Add(this.btnFromPrj);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnToPrj);
            this.groupBox2.Controls.Add(this.txtToPrj);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(390, 127);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // btnFromPrj
            // 
            this.btnFromPrj.Image = global::AG.COM.SDM.GeoDataBase.Properties.Resources.fileopen;
            this.btnFromPrj.Location = new System.Drawing.Point(332, 32);
            this.btnFromPrj.Name = "btnFromPrj";
            this.btnFromPrj.Size = new System.Drawing.Size(24, 21);
            this.btnFromPrj.TabIndex = 2;
            this.btnFromPrj.UseVisualStyleBackColor = true;
            this.btnFromPrj.Click += new System.EventHandler(this.btnFromPrj_Click);
            // 
            // FormPoint2Point
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 264);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormPoint2Point";
            this.Text = "点到点坐标转换";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSrcPrj;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtToPrj;
        private System.Windows.Forms.Button btnToPrj;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSrcX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSrcY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtToX;
        private System.Windows.Forms.TextBox txtToY;
        private System.Windows.Forms.Button btnFromPrj;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}