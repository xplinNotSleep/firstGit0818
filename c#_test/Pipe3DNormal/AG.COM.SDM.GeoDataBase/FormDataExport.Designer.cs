namespace AG.COM.SDM.GeoDataBase
{
    partial class FormDataExport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDataExport));
            this.panel1 = new System.Windows.Forms.Panel();
            this.SelectorInputFeatClass = new AG.COM.SDM.GeoDataBase.FeatureLayerSelector();
            this.label6 = new System.Windows.Forms.Label();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnDelField = new System.Windows.Forms.Button();
            this.btnAddField = new System.Windows.Forms.Button();
            this.btnQueryFilter = new System.Windows.Forms.Button();
            this.btnOutFolder = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.listFields = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.txtQueryFilter = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOutFileName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOutFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SelectorInputFeatClass);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.btnMoveDown);
            this.panel1.Controls.Add(this.btnMoveUp);
            this.panel1.Controls.Add(this.btnDelField);
            this.panel1.Controls.Add(this.btnAddField);
            this.panel1.Controls.Add(this.btnQueryFilter);
            this.panel1.Controls.Add(this.btnOutFolder);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.listFields);
            this.panel1.Controls.Add(this.txtQueryFilter);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtOutFileName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtOutFolder);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(379, 405);
            this.panel1.TabIndex = 0;
            // 
            // SelectorInputFeatClass
            // 
            this.SelectorInputFeatClass.FeatureClass = null;
            this.SelectorInputFeatClass.Filter = AG.COM.SDM.GeoDataBase.FeatureLayerFilterType.lyFilterNone;
            this.SelectorInputFeatClass.Location = new System.Drawing.Point(28, 27);
            this.SelectorInputFeatClass.Name = "SelectorInputFeatClass";
            this.SelectorInputFeatClass.Size = new System.Drawing.Size(348, 25);
            this.SelectorInputFeatClass.TabIndex = 53;
            this.SelectorInputFeatClass.LayerChanged += new AG.COM.SDM.GeoDataBase.LayerChangedDelegate(this.SelectorInputFeatClass_LayerChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(83, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 52;
            this.label6.Text = "(可选)";
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveDown.Image")));
            this.btnMoveDown.Location = new System.Drawing.Point(346, 299);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(24, 21);
            this.btnMoveDown.TabIndex = 10;
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveUp.Image")));
            this.btnMoveUp.Location = new System.Drawing.Point(346, 272);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(24, 21);
            this.btnMoveUp.TabIndex = 9;
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnDelField
            // 
            this.btnDelField.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDelField.Location = new System.Drawing.Point(346, 233);
            this.btnDelField.Name = "btnDelField";
            this.btnDelField.Size = new System.Drawing.Size(24, 21);
            this.btnDelField.TabIndex = 8;
            this.btnDelField.Text = "-";
            this.btnDelField.UseVisualStyleBackColor = true;
            this.btnDelField.Click += new System.EventHandler(this.btnDelField_Click);
            // 
            // btnAddField
            // 
            this.btnAddField.Font = new System.Drawing.Font("SimSun", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddField.Location = new System.Drawing.Point(346, 206);
            this.btnAddField.Name = "btnAddField";
            this.btnAddField.Size = new System.Drawing.Size(24, 21);
            this.btnAddField.TabIndex = 7;
            this.btnAddField.Text = "+";
            this.btnAddField.UseVisualStyleBackColor = true;
            this.btnAddField.Click += new System.EventHandler(this.btnAddField_Click);
            // 
            // btnQueryFilter
            // 
            this.btnQueryFilter.Enabled = false;
            this.btnQueryFilter.Image = ((System.Drawing.Image)(resources.GetObject("btnQueryFilter.Image")));
            this.btnQueryFilter.Location = new System.Drawing.Point(346, 154);
            this.btnQueryFilter.Name = "btnQueryFilter";
            this.btnQueryFilter.Size = new System.Drawing.Size(24, 21);
            this.btnQueryFilter.TabIndex = 6;
            this.btnQueryFilter.UseVisualStyleBackColor = true;
            this.btnQueryFilter.Click += new System.EventHandler(this.btnQueryFilter_Click);
            // 
            // btnOutFolder
            // 
            this.btnOutFolder.Image = global::AG.COM.SDM.GeoDataBase.Properties.Resources.fileopen;
            this.btnOutFolder.Location = new System.Drawing.Point(346, 70);
            this.btnOutFolder.Name = "btnOutFolder";
            this.btnOutFolder.Size = new System.Drawing.Size(24, 21);
            this.btnOutFolder.TabIndex = 3;
            this.btnOutFolder.UseVisualStyleBackColor = true;
            this.btnOutFolder.Click += new System.EventHandler(this.btnOutFolder_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 188);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 44;
            this.label5.Text = "输出字段集合";
            // 
            // listFields
            // 
            this.listFields.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listFields.Enabled = false;
            this.listFields.FullRowSelect = true;
            this.listFields.GridLines = true;
            this.listFields.Location = new System.Drawing.Point(28, 206);
            this.listFields.Name = "listFields";
            this.listFields.Size = new System.Drawing.Size(312, 195);
            this.listFields.TabIndex = 43;
            this.listFields.UseCompatibleStateImageBehavior = false;
            this.listFields.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "字段别名";
            this.columnHeader1.Width = 86;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "字段名称";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "字段类型";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 117;
            // 
            // txtQueryFilter
            // 
            this.txtQueryFilter.BackColor = System.Drawing.SystemColors.Window;
            this.txtQueryFilter.Location = new System.Drawing.Point(28, 154);
            this.txtQueryFilter.Name = "txtQueryFilter";
            this.txtQueryFilter.ReadOnly = true;
            this.txtQueryFilter.Size = new System.Drawing.Size(312, 21);
            this.txtQueryFilter.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 36;
            this.label4.Text = "过滤条件";
            // 
            // txtOutFileName
            // 
            this.txtOutFileName.Location = new System.Drawing.Point(28, 112);
            this.txtOutFileName.Name = "txtOutFileName";
            this.txtOutFileName.Size = new System.Drawing.Size(312, 21);
            this.txtOutFileName.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 35;
            this.label3.Text = "输出名称";
            // 
            // txtOutFolder
            // 
            this.txtOutFolder.Enabled = false;
            this.txtOutFolder.Location = new System.Drawing.Point(28, 70);
            this.txtOutFolder.Name = "txtOutFolder";
            this.txtOutFolder.Size = new System.Drawing.Size(312, 21);
            this.txtOutFolder.TabIndex = 2;
            this.txtOutFolder.TextChanged += new System.EventHandler(this.txtOutFolder_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 38;
            this.label2.Text = "输出位置";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 37;
            this.label1.Text = "源要素类";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 405);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(379, 28);
            this.panel2.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(270, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(194, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 24);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FormDataExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 433);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDataExport";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据传导(单个)";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnDelField;
        private System.Windows.Forms.Button btnAddField;
        private System.Windows.Forms.Button btnQueryFilter;
        private System.Windows.Forms.Button btnOutFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView listFields;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TextBox txtQueryFilter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOutFileName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOutFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private FeatureLayerSelector SelectorInputFeatClass;
    }
}