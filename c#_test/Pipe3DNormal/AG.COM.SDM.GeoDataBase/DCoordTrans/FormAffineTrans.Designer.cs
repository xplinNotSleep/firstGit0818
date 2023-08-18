namespace AG.COM.SDM.GeoDataBase
{
    partial class FormAffineTrans
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAffineTrans));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listSrcFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.chkOnSrcFile = new System.Windows.Forms.CheckBox();
            this.btnOutWorkspace = new System.Windows.Forms.Button();
            this.btnOpenSrcFile = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOutWorkspace = new System.Windows.Forms.TextBox();
            this.txtSrcFile = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnXYFile = new System.Windows.Forms.Button();
            this.txtXYFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgXY = new System.Windows.Forms.DataGridView();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgXY)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listSrcFiles);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.chkOnSrcFile);
            this.groupBox1.Controls.Add(this.btnOutWorkspace);
            this.groupBox1.Controls.Add(this.btnOpenSrcFile);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtOutWorkspace);
            this.groupBox1.Controls.Add(this.txtSrcFile);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnXYFile);
            this.groupBox1.Controls.Add(this.txtXYFile);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtgXY);
            this.groupBox1.Location = new System.Drawing.Point(7, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(409, 469);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // listSrcFiles
            // 
            this.listSrcFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listSrcFiles.GridLines = true;
            this.listSrcFiles.Location = new System.Drawing.Point(18, 283);
            this.listSrcFiles.Name = "listSrcFiles";
            this.listSrcFiles.Size = new System.Drawing.Size(346, 97);
            this.listSrcFiles.TabIndex = 23;
            this.listSrcFiles.UseCompatibleStateImageBehavior = false;
            this.listSrcFiles.View = System.Windows.Forms.View.Details;
            this.listSrcFiles.SelectedIndexChanged += new System.EventHandler(this.listSrcFiles_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "文件路径";
            this.columnHeader1.Width = 332;
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(370, 286);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(24, 24);
            this.btnDelete.TabIndex = 22;
            this.toolTip1.SetToolTip(this.btnDelete, "移除当前选择项");
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.Enabled = false;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(370, 316);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(24, 24);
            this.btnClear.TabIndex = 21;
            this.toolTip1.SetToolTip(this.btnClear, "移除所有数据文件");
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // chkOnSrcFile
            // 
            this.chkOnSrcFile.AutoSize = true;
            this.chkOnSrcFile.Location = new System.Drawing.Point(18, 440);
            this.chkOnSrcFile.Name = "chkOnSrcFile";
            this.chkOnSrcFile.Size = new System.Drawing.Size(156, 16);
            this.chkOnSrcFile.TabIndex = 20;
            this.chkOnSrcFile.Text = "在源文件基础上进行转换";
            this.chkOnSrcFile.UseVisualStyleBackColor = true;
            this.chkOnSrcFile.CheckedChanged += new System.EventHandler(this.chkOnSrcFile_CheckedChanged);
            // 
            // btnOutWorkspace
            // 
            this.btnOutWorkspace.Image = global::AG.COM.SDM.GeoDataBase.Properties.Resources.fileopen;
            this.btnOutWorkspace.Location = new System.Drawing.Point(370, 407);
            this.btnOutWorkspace.Name = "btnOutWorkspace";
            this.btnOutWorkspace.Size = new System.Drawing.Size(24, 24);
            this.btnOutWorkspace.TabIndex = 18;
            this.btnOutWorkspace.UseVisualStyleBackColor = true;
            this.btnOutWorkspace.Click += new System.EventHandler(this.btnOutWorkspace_Click);
            // 
            // btnOpenSrcFile
            // 
            this.btnOpenSrcFile.Image = global::AG.COM.SDM.GeoDataBase.Properties.Resources.fileopen;
            this.btnOpenSrcFile.Location = new System.Drawing.Point(370, 256);
            this.btnOpenSrcFile.Name = "btnOpenSrcFile";
            this.btnOpenSrcFile.Size = new System.Drawing.Size(24, 24);
            this.btnOpenSrcFile.TabIndex = 18;
            this.btnOpenSrcFile.UseVisualStyleBackColor = true;
            this.btnOpenSrcFile.Click += new System.EventHandler(this.btnOpenSrcFile_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 392);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "选择输出目录";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 241);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "选择要转换的数据文件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "坐标对列表";
            // 
            // txtOutWorkspace
            // 
            this.txtOutWorkspace.Location = new System.Drawing.Point(18, 407);
            this.txtOutWorkspace.Name = "txtOutWorkspace";
            this.txtOutWorkspace.Size = new System.Drawing.Size(346, 21);
            this.txtOutWorkspace.TabIndex = 15;
            // 
            // txtSrcFile
            // 
            this.txtSrcFile.Location = new System.Drawing.Point(18, 256);
            this.txtSrcFile.Name = "txtSrcFile";
            this.txtSrcFile.Size = new System.Drawing.Size(346, 21);
            this.txtSrcFile.TabIndex = 15;
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(370, 80);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(24, 24);
            this.btnSave.TabIndex = 14;
            this.toolTip1.SetToolTip(this.btnSave, "保存坐标数据到文件");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnXYFile
            // 
            this.btnXYFile.Image = global::AG.COM.SDM.GeoDataBase.Properties.Resources.fileopen;
            this.btnXYFile.Location = new System.Drawing.Point(370, 33);
            this.btnXYFile.Name = "btnXYFile";
            this.btnXYFile.Size = new System.Drawing.Size(24, 24);
            this.btnXYFile.TabIndex = 14;
            this.btnXYFile.UseVisualStyleBackColor = true;
            this.btnXYFile.Click += new System.EventHandler(this.btnXYFile_Click);
            // 
            // txtXYFile
            // 
            this.txtXYFile.Location = new System.Drawing.Point(18, 33);
            this.txtXYFile.Name = "txtXYFile";
            this.txtXYFile.Size = new System.Drawing.Size(346, 21);
            this.txtXYFile.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "导入源/目标坐标控制点文件";
            // 
            // dtgXY
            // 
            this.dtgXY.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgXY.Location = new System.Drawing.Point(18, 80);
            this.dtgXY.Name = "dtgXY";
            this.dtgXY.RowTemplate.Height = 23;
            this.dtgXY.Size = new System.Drawing.Size(346, 148);
            this.dtgXY.TabIndex = 11;
            this.dtgXY.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dtgXY_RowsAdded);
            this.dtgXY.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dtgXY_RowPostPaint);
            this.dtgXY.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dtgXY_DataError);
            this.dtgXY.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dtgXY_RowsRemoved);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(270, 475);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 24);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(346, 475);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 24);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormAffineTrans
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 504);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAffineTrans";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "坐标仿射变换";
            this.Load += new System.EventHandler(this.FormAffineTrans_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgXY)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOpenSrcFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOutWorkspace;
        private System.Windows.Forms.TextBox txtSrcFile;
        private System.Windows.Forms.Button btnXYFile;
        private System.Windows.Forms.TextBox txtXYFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtgXY;
        private System.Windows.Forms.CheckBox chkOnSrcFile;
        private System.Windows.Forms.Button btnOutWorkspace;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListView listSrcFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;

    }
}