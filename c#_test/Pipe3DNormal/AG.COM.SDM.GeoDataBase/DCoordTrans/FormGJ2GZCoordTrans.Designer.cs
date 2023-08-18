namespace AG.COM.SDM.GeoDataBase
{
    partial class FormGJ2GZCoordTrans
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGJ2GZCoordTrans));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.combTransType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listSrcFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.chkOnSrcFile = new System.Windows.Forms.CheckBox();
            this.btnOutWorkspace = new System.Windows.Forms.Button();
            this.btnOpenSrcFile = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOutWorkspace = new System.Windows.Forms.TextBox();
            this.txtSrcFile = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.combTransType);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.listSrcFiles);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.chkOnSrcFile);
            this.groupBox1.Controls.Add(this.btnOutWorkspace);
            this.groupBox1.Controls.Add(this.btnOpenSrcFile);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtOutWorkspace);
            this.groupBox1.Controls.Add(this.txtSrcFile);
            this.groupBox1.Location = new System.Drawing.Point(4, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(370, 338);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // combTransType
            // 
            this.combTransType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combTransType.FormattingEnabled = true;
            this.combTransType.Location = new System.Drawing.Point(19, 37);
            this.combTransType.Name = "combTransType";
            this.combTransType.Size = new System.Drawing.Size(312, 20);
            this.combTransType.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 34;
            this.label1.Text = "选择转换类型";
            // 
            // listSrcFiles
            // 
            this.listSrcFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listSrcFiles.GridLines = true;
            this.listSrcFiles.Location = new System.Drawing.Point(19, 114);
            this.listSrcFiles.Name = "listSrcFiles";
            this.listSrcFiles.Size = new System.Drawing.Size(312, 132);
            this.listSrcFiles.TabIndex = 33;
            this.listSrcFiles.UseCompatibleStateImageBehavior = false;
            this.listSrcFiles.View = System.Windows.Forms.View.Details;
            this.listSrcFiles.SelectedIndexChanged += new System.EventHandler(this.listSrcFiles_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "文件路径";
            this.columnHeader1.Width = 306;
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(337, 115);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(24, 24);
            this.btnDelete.TabIndex = 32;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.Enabled = false;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(337, 145);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(24, 24);
            this.btnClear.TabIndex = 31;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // chkOnSrcFile
            // 
            this.chkOnSrcFile.AutoSize = true;
            this.chkOnSrcFile.Location = new System.Drawing.Point(19, 304);
            this.chkOnSrcFile.Name = "chkOnSrcFile";
            this.chkOnSrcFile.Size = new System.Drawing.Size(156, 16);
            this.chkOnSrcFile.TabIndex = 30;
            this.chkOnSrcFile.Text = "在源文件基础上进行转换";
            this.chkOnSrcFile.UseVisualStyleBackColor = true;
            this.chkOnSrcFile.CheckedChanged += new System.EventHandler(this.chkOnSrcFile_CheckedChanged);
            // 
            // btnOutWorkspace
            // 
            //this.btnOutWorkspace.Image = global::AG.COM.SDM.GeoDataBase.Properties.Resources.fileopen;
            this.btnOutWorkspace.Location = new System.Drawing.Point(337, 276);
            this.btnOutWorkspace.Name = "btnOutWorkspace";
            this.btnOutWorkspace.Size = new System.Drawing.Size(24, 24);
            this.btnOutWorkspace.TabIndex = 29;
            this.btnOutWorkspace.UseVisualStyleBackColor = true;
            this.btnOutWorkspace.Click += new System.EventHandler(this.btnOutWorkspace_Click);
            // 
            // btnOpenSrcFile
            // 
            this.btnOpenSrcFile.Image = global::AG.COM.SDM.GeoDataBase.Properties.Resources.fileopen;
            this.btnOpenSrcFile.Location = new System.Drawing.Point(337, 85);
            this.btnOpenSrcFile.Name = "btnOpenSrcFile";
            this.btnOpenSrcFile.Size = new System.Drawing.Size(24, 24);
            this.btnOpenSrcFile.TabIndex = 28;
            this.btnOpenSrcFile.UseVisualStyleBackColor = true;
            this.btnOpenSrcFile.Click += new System.EventHandler(this.btnOpenSrcFile_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 259);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 26;
            this.label4.Text = "选择输出位置";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 12);
            this.label3.TabIndex = 27;
            this.label3.Text = "选择要转换的数据文件";
            // 
            // txtOutWorkspace
            // 
            this.txtOutWorkspace.Location = new System.Drawing.Point(19, 278);
            this.txtOutWorkspace.Name = "txtOutWorkspace";
            this.txtOutWorkspace.Size = new System.Drawing.Size(312, 21);
            this.txtOutWorkspace.TabIndex = 24;
            // 
            // txtSrcFile
            // 
            this.txtSrcFile.Location = new System.Drawing.Point(19, 87);
            this.txtSrcFile.Name = "txtSrcFile";
            this.txtSrcFile.Size = new System.Drawing.Size(312, 21);
            this.txtSrcFile.TabIndex = 25;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(304, 345);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 24);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(228, 345);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 24);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FormGJ2GZCoordTrans
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 374);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGJ2GZCoordTrans";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "国家/广州坐标转换";
            this.Load += new System.EventHandler(this.FormGJ2GZCoordTrans_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox combTransType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listSrcFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox chkOnSrcFile;
        private System.Windows.Forms.Button btnOutWorkspace;
        private System.Windows.Forms.Button btnOpenSrcFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOutWorkspace;
        private System.Windows.Forms.TextBox txtSrcFile;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}