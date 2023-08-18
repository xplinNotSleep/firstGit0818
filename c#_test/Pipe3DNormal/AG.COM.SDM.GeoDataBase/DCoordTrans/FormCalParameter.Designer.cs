namespace AG.COM.SDM.GeoDataBase
{
    partial class FormCalParameter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCalParameter));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnXYFile = new System.Windows.Forms.Button();
            this.txtXYFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgXY = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtScale = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtYTranslation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRotation = new System.Windows.Forms.TextBox();
            this.txtXTranslation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCalParameter = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtX0 = new System.Windows.Forms.TextBox();
            this.txtY0 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtX1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtY1 = new System.Windows.Forms.TextBox();
            this.btn1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgXY)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnXYFile);
            this.groupBox1.Controls.Add(this.txtXYFile);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtgXY);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(405, 252);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(369, 83);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(24, 24);
            this.btnSave.TabIndex = 18;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnXYFile
            // 
            this.btnXYFile.Image = global::AG.COM.SDM.GeoDataBase.Properties.Resources.fileopen;
            this.btnXYFile.Location = new System.Drawing.Point(369, 36);
            this.btnXYFile.Name = "btnXYFile";
            this.btnXYFile.Size = new System.Drawing.Size(24, 24);
            this.btnXYFile.TabIndex = 19;
            this.btnXYFile.UseVisualStyleBackColor = true;
            this.btnXYFile.Click += new System.EventHandler(this.btnXYFile_Click);
            // 
            // txtXYFile
            // 
            this.txtXYFile.Location = new System.Drawing.Point(17, 36);
            this.txtXYFile.Name = "txtXYFile";
            this.txtXYFile.Size = new System.Drawing.Size(346, 21);
            this.txtXYFile.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "导入源/目标坐标控制点文件";
            // 
            // dtgXY
            // 
            this.dtgXY.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgXY.Location = new System.Drawing.Point(17, 83);
            this.dtgXY.Name = "dtgXY";
            this.dtgXY.RowTemplate.Height = 23;
            this.dtgXY.Size = new System.Drawing.Size(346, 148);
            this.dtgXY.TabIndex = 15;
            this.dtgXY.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dtgXY_RowsAdded);
            this.dtgXY.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dtgXY_RowPostPaint);
            this.dtgXY.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dtgXY_DataError);
            this.dtgXY.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dtgXY_RowsRemoved);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtScale);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtYTranslation);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtRotation);
            this.groupBox2.Controls.Add(this.txtXTranslation);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(10, 282);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(405, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "转换参数";
            // 
            // txtScale
            // 
            this.txtScale.Location = new System.Drawing.Point(263, 59);
            this.txtScale.Name = "txtScale";
            this.txtScale.Size = new System.Drawing.Size(130, 21);
            this.txtScale.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(205, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "缩放尺度:";
            // 
            // txtYTranslation
            // 
            this.txtYTranslation.Location = new System.Drawing.Point(263, 19);
            this.txtYTranslation.Name = "txtYTranslation";
            this.txtYTranslation.Size = new System.Drawing.Size(130, 21);
            this.txtYTranslation.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(205, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "Y偏移量:";
            // 
            // txtRotation
            // 
            this.txtRotation.Location = new System.Drawing.Point(70, 59);
            this.txtRotation.Name = "txtRotation";
            this.txtRotation.Size = new System.Drawing.Size(129, 21);
            this.txtRotation.TabIndex = 1;
            // 
            // txtXTranslation
            // 
            this.txtXTranslation.Location = new System.Drawing.Point(70, 19);
            this.txtXTranslation.Name = "txtXTranslation";
            this.txtXTranslation.Size = new System.Drawing.Size(129, 21);
            this.txtXTranslation.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "旋转角度:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "X偏移量:";
            // 
            // btnCalParameter
            // 
            this.btnCalParameter.Location = new System.Drawing.Point(10, 397);
            this.btnCalParameter.Name = "btnCalParameter";
            this.btnCalParameter.Size = new System.Drawing.Size(75, 23);
            this.btnCalParameter.TabIndex = 2;
            this.btnCalParameter.Text = "计算参数";
            this.btnCalParameter.UseVisualStyleBackColor = true;
            this.btnCalParameter.Click += new System.EventHandler(this.btnCalParameter_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(92, 397);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "导出参数";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(330, 397);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 448);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "X0:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 488);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "Y0:";
            // 
            // txtX0
            // 
            this.txtX0.Location = new System.Drawing.Point(38, 445);
            this.txtX0.Name = "txtX0";
            this.txtX0.Size = new System.Drawing.Size(129, 21);
            this.txtX0.TabIndex = 1;
            // 
            // txtY0
            // 
            this.txtY0.Location = new System.Drawing.Point(38, 485);
            this.txtY0.Name = "txtY0";
            this.txtY0.Size = new System.Drawing.Size(129, 21);
            this.txtY0.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(244, 449);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "X1:";
            // 
            // txtX1
            // 
            this.txtX1.Location = new System.Drawing.Point(273, 445);
            this.txtX1.Name = "txtX1";
            this.txtX1.Size = new System.Drawing.Size(130, 21);
            this.txtX1.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(244, 488);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "Y1:";
            // 
            // txtY1
            // 
            this.txtY1.Location = new System.Drawing.Point(273, 485);
            this.txtY1.Name = "txtY1";
            this.txtY1.Size = new System.Drawing.Size(130, 21);
            this.txtY1.TabIndex = 1;
            // 
            // btn1
            // 
            this.btn1.Location = new System.Drawing.Point(184, 461);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(43, 23);
            this.btn1.TabIndex = 3;
            this.btn1.Text = "转换";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.btn1_Click);
            // 
            // FormCalParameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 514);
            this.Controls.Add(this.txtY1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtX1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.txtY0);
            this.Controls.Add(this.btnCalParameter);
            this.Controls.Add(this.txtX0);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormCalParameter";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "坐标参数(四参数)计算";
            this.Load += new System.EventHandler(this.FormCalParameter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgXY)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnXYFile;
        private System.Windows.Forms.TextBox txtXYFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtgXY;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtScale;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtYTranslation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRotation;
        private System.Windows.Forms.TextBox txtXTranslation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCalParameter;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtX0;
        private System.Windows.Forms.TextBox txtY0;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtX1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtY1;
        private System.Windows.Forms.Button btn1;
    }
}