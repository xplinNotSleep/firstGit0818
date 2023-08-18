namespace AG.COM.SDM.GeoDataBase
{
    partial class FormCoordTrans
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCoordTrans));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.btnOpenSrcFile = new System.Windows.Forms.Button();
            this.btnCoordPrj = new System.Windows.Forms.Button();
            this.btnOutPutLocation = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtSrcFile = new System.Windows.Forms.TextBox();
            this.txtSpatialReference = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listFiles);
            this.groupBox1.Controls.Add(this.btnOpenSrcFile);
            this.groupBox1.Controls.Add(this.btnCoordPrj);
            this.groupBox1.Controls.Add(this.btnOutPutLocation);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.txtSrcFile);
            this.groupBox1.Controls.Add(this.txtSpatialReference);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtFolderPath);
            this.groupBox1.Location = new System.Drawing.Point(12, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(341, 386);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // listFiles
            // 
            this.listFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listFiles.GridLines = true;
            this.listFiles.Location = new System.Drawing.Point(18, 67);
            this.listFiles.Name = "listFiles";
            this.listFiles.Size = new System.Drawing.Size(280, 195);
            this.listFiles.TabIndex = 6;
            this.listFiles.UseCompatibleStateImageBehavior = false;
            this.listFiles.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "源文件路径";
            this.columnHeader1.Width = 188;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "能否转换";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 87;
            // 
            // btnOpenSrcFile
            // 
            this.btnOpenSrcFile.Image = global::AG.COM.SDM.GeoDataBase.Properties.Resources.fileopen;
            this.btnOpenSrcFile.Location = new System.Drawing.Point(304, 40);
            this.btnOpenSrcFile.Name = "btnOpenSrcFile";
            this.btnOpenSrcFile.Size = new System.Drawing.Size(24, 24);
            this.btnOpenSrcFile.TabIndex = 5;
            this.btnOpenSrcFile.UseVisualStyleBackColor = true;
            this.btnOpenSrcFile.Click += new System.EventHandler(this.btnOpenSrcFile_Click);
            // 
            // btnCoordPrj
            // 
            this.btnCoordPrj.Image = global::AG.COM.SDM.GeoDataBase.Properties.Resources.fileopen;
            this.btnCoordPrj.Location = new System.Drawing.Point(304, 343);
            this.btnCoordPrj.Name = "btnCoordPrj";
            this.btnCoordPrj.Size = new System.Drawing.Size(24, 24);
            this.btnCoordPrj.TabIndex = 4;
            this.btnCoordPrj.UseVisualStyleBackColor = true;
            this.btnCoordPrj.Click += new System.EventHandler(this.btnCoordPrj_Click);
            // 
            // btnOutPutLocation
            // 
            this.btnOutPutLocation.Image = global::AG.COM.SDM.GeoDataBase.Properties.Resources.fileopen;
            this.btnOutPutLocation.Location = new System.Drawing.Point(304, 296);
            this.btnOutPutLocation.Name = "btnOutPutLocation";
            this.btnOutPutLocation.Size = new System.Drawing.Size(24, 24);
            this.btnOutPutLocation.TabIndex = 4;
            this.btnOutPutLocation.UseVisualStyleBackColor = true;
            this.btnOutPutLocation.Click += new System.EventHandler(this.btnOutPutLocation_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(304, 70);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(24, 24);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.Enabled = false;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(304, 102);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(24, 24);
            this.btnClear.TabIndex = 3;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtSrcFile
            // 
            this.txtSrcFile.Location = new System.Drawing.Point(18, 40);
            this.txtSrcFile.Name = "txtSrcFile";
            this.txtSrcFile.Size = new System.Drawing.Size(280, 21);
            this.txtSrcFile.TabIndex = 2;
            // 
            // txtSpatialReference
            // 
            this.txtSpatialReference.Location = new System.Drawing.Point(18, 343);
            this.txtSpatialReference.Name = "txtSpatialReference";
            this.txtSpatialReference.Size = new System.Drawing.Size(280, 21);
            this.txtSpatialReference.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "选择源文件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 328);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "输出坐标系";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 278);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "输出目录";
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Location = new System.Drawing.Point(18, 296);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.Size = new System.Drawing.Size(280, 21);
            this.txtFolderPath.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(207, 394);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 24);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(283, 394);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 24);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormCoordTrans
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 425);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCoordTrans";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "通用坐标转换";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtFolderPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnOpenSrcFile;
        private System.Windows.Forms.Button btnCoordPrj;
        private System.Windows.Forms.Button btnOutPutLocation;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtSrcFile;
        private System.Windows.Forms.TextBox txtSpatialReference;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListView listFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}