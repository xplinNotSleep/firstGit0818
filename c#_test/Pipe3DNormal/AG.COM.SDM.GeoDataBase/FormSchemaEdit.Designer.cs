namespace AG.COM.SDM.GeoDataBase
{
    partial class FormSchemaEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSchemaEdit));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTableAlias = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDelField = new System.Windows.Forms.Button();
            this.btnAddField = new System.Windows.Forms.Button();
            this.listFields = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pnlAtrr = new System.Windows.Forms.Panel();
            this.txtScale = new System.Windows.Forms.TextBox();
            this.lblScale = new System.Windows.Forms.Label();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.lblLength = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.combDomains = new System.Windows.Forms.ComboBox();
            this.pnlGeoAttr = new System.Windows.Forms.Panel();
            this.txtGeometryType = new System.Windows.Forms.TextBox();
            this.lblGeoType = new System.Windows.Forms.Label();
            this.pnlCommon = new System.Windows.Forms.Panel();
            this.txtFieldName = new System.Windows.Forms.TextBox();
            this.combFieldType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.chkIsNull = new System.Windows.Forms.CheckBox();
            this.txtDefault = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFieldAlias = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnApply = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.pnlAtrr.SuspendLayout();
            this.pnlGeoAttr.SuspendLayout();
            this.pnlCommon.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(241, 378);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(312, 378);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "取消";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTableAlias);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtTableName);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Location = new System.Drawing.Point(12, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(441, 89);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            // 
            // txtTableAlias
            // 
            this.txtTableAlias.Location = new System.Drawing.Point(86, 52);
            this.txtTableAlias.Name = "txtTableAlias";
            this.txtTableAlias.Size = new System.Drawing.Size(327, 21);
            this.txtTableAlias.TabIndex = 20;
            this.txtTableAlias.TextChanged += new System.EventHandler(this.txtTableAlias_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 56);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 12);
            this.label12.TabIndex = 17;
            this.label12.Text = "表别名:";
            // 
            // txtTableName
            // 
            this.txtTableName.Enabled = false;
            this.txtTableName.Location = new System.Drawing.Point(86, 20);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(327, 21);
            this.txtTableName.TabIndex = 19;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 24);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 12);
            this.label13.TabIndex = 18;
            this.label13.Text = "表名称:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDelField);
            this.groupBox2.Controls.Add(this.btnAddField);
            this.groupBox2.Controls.Add(this.listFields);
            this.groupBox2.Location = new System.Drawing.Point(12, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(175, 273);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            // 
            // btnDelField
            // 
            this.btnDelField.Image = ((System.Drawing.Image)(resources.GetObject("btnDelField.Image")));
            this.btnDelField.Location = new System.Drawing.Point(145, 47);
            this.btnDelField.Name = "btnDelField";
            this.btnDelField.Size = new System.Drawing.Size(24, 24);
            this.btnDelField.TabIndex = 10;
            this.btnDelField.UseVisualStyleBackColor = true;
            this.btnDelField.Click += new System.EventHandler(this.btnDelField_Click);
            // 
            // btnAddField
            // 
            this.btnAddField.Image = ((System.Drawing.Image)(resources.GetObject("btnAddField.Image")));
            this.btnAddField.Location = new System.Drawing.Point(145, 17);
            this.btnAddField.Name = "btnAddField";
            this.btnAddField.Size = new System.Drawing.Size(24, 24);
            this.btnAddField.TabIndex = 9;
            this.btnAddField.UseVisualStyleBackColor = true;
            this.btnAddField.Click += new System.EventHandler(this.btnAddField_Click);
            // 
            // listFields
            // 
            this.listFields.FormattingEnabled = true;
            this.listFields.ItemHeight = 12;
            this.listFields.Location = new System.Drawing.Point(6, 17);
            this.listFields.Name = "listFields";
            this.listFields.Size = new System.Drawing.Size(133, 244);
            this.listFields.TabIndex = 8;
            this.listFields.SelectedValueChanged += new System.EventHandler(this.listFields_SelectedValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pnlAtrr);
            this.groupBox3.Controls.Add(this.pnlGeoAttr);
            this.groupBox3.Controls.Add(this.pnlCommon);
            this.groupBox3.Location = new System.Drawing.Point(193, 97);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(260, 273);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            // 
            // pnlAtrr
            // 
            this.pnlAtrr.Controls.Add(this.txtScale);
            this.pnlAtrr.Controls.Add(this.lblScale);
            this.pnlAtrr.Controls.Add(this.txtLength);
            this.pnlAtrr.Controls.Add(this.lblLength);
            this.pnlAtrr.Controls.Add(this.label6);
            this.pnlAtrr.Controls.Add(this.combDomains);
            this.pnlAtrr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAtrr.Location = new System.Drawing.Point(3, 145);
            this.pnlAtrr.Name = "pnlAtrr";
            this.pnlAtrr.Size = new System.Drawing.Size(254, 125);
            this.pnlAtrr.TabIndex = 11;
            // 
            // txtScale
            // 
            this.txtScale.Enabled = false;
            this.txtScale.Location = new System.Drawing.Point(76, 57);
            this.txtScale.Name = "txtScale";
            this.txtScale.Size = new System.Drawing.Size(153, 21);
            this.txtScale.TabIndex = 11;
            // 
            // lblScale
            // 
            this.lblScale.AutoSize = true;
            this.lblScale.Location = new System.Drawing.Point(3, 61);
            this.lblScale.Name = "lblScale";
            this.lblScale.Size = new System.Drawing.Size(59, 12);
            this.lblScale.TabIndex = 10;
            this.lblScale.Text = "小数位数:";
            // 
            // txtLength
            // 
            this.txtLength.Enabled = false;
            this.txtLength.Location = new System.Drawing.Point(76, 31);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(153, 21);
            this.txtLength.TabIndex = 12;
            this.txtLength.TextChanged += new System.EventHandler(this.txtLength_TextChanged);
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(3, 35);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(59, 12);
            this.lblLength.TabIndex = 9;
            this.lblLength.Text = "字段长度:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "属性域:";
            // 
            // combDomains
            // 
            this.combDomains.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combDomains.FormattingEnabled = true;
            this.combDomains.Location = new System.Drawing.Point(76, 3);
            this.combDomains.Name = "combDomains";
            this.combDomains.Size = new System.Drawing.Size(153, 20);
            this.combDomains.TabIndex = 7;
            // 
            // pnlGeoAttr
            // 
            this.pnlGeoAttr.Controls.Add(this.txtGeometryType);
            this.pnlGeoAttr.Controls.Add(this.lblGeoType);
            this.pnlGeoAttr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGeoAttr.Location = new System.Drawing.Point(3, 145);
            this.pnlGeoAttr.Name = "pnlGeoAttr";
            this.pnlGeoAttr.Size = new System.Drawing.Size(254, 125);
            this.pnlGeoAttr.TabIndex = 14;
            // 
            // txtGeometryType
            // 
            this.txtGeometryType.Enabled = false;
            this.txtGeometryType.Location = new System.Drawing.Point(76, 3);
            this.txtGeometryType.Name = "txtGeometryType";
            this.txtGeometryType.Size = new System.Drawing.Size(153, 21);
            this.txtGeometryType.TabIndex = 18;
            // 
            // lblGeoType
            // 
            this.lblGeoType.AutoSize = true;
            this.lblGeoType.Location = new System.Drawing.Point(3, 7);
            this.lblGeoType.Name = "lblGeoType";
            this.lblGeoType.Size = new System.Drawing.Size(59, 12);
            this.lblGeoType.TabIndex = 13;
            this.lblGeoType.Text = "几何类型:";
            // 
            // pnlCommon
            // 
            this.pnlCommon.Controls.Add(this.txtFieldName);
            this.pnlCommon.Controls.Add(this.combFieldType);
            this.pnlCommon.Controls.Add(this.label11);
            this.pnlCommon.Controls.Add(this.chkIsNull);
            this.pnlCommon.Controls.Add(this.txtDefault);
            this.pnlCommon.Controls.Add(this.label5);
            this.pnlCommon.Controls.Add(this.txtFieldAlias);
            this.pnlCommon.Controls.Add(this.label4);
            this.pnlCommon.Controls.Add(this.label3);
            this.pnlCommon.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCommon.Location = new System.Drawing.Point(3, 17);
            this.pnlCommon.Name = "pnlCommon";
            this.pnlCommon.Size = new System.Drawing.Size(254, 128);
            this.pnlCommon.TabIndex = 9;
            // 
            // txtFieldName
            // 
            this.txtFieldName.Location = new System.Drawing.Point(76, 3);
            this.txtFieldName.Name = "txtFieldName";
            this.txtFieldName.Size = new System.Drawing.Size(153, 21);
            this.txtFieldName.TabIndex = 16;
            this.txtFieldName.Leave += new System.EventHandler(this.txtFieldName_Leave);
            // 
            // combFieldType
            // 
            this.combFieldType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combFieldType.FormattingEnabled = true;
            this.combFieldType.Location = new System.Drawing.Point(76, 57);
            this.combFieldType.Name = "combFieldType";
            this.combFieldType.Size = new System.Drawing.Size(153, 20);
            this.combFieldType.TabIndex = 15;
            this.combFieldType.SelectedIndexChanged += new System.EventHandler(this.combFieldType_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 89);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 12);
            this.label11.TabIndex = 14;
            this.label11.Text = "默认值:";
            // 
            // chkIsNull
            // 
            this.chkIsNull.AutoSize = true;
            this.chkIsNull.Enabled = false;
            this.chkIsNull.Location = new System.Drawing.Point(76, 110);
            this.chkIsNull.Name = "chkIsNull";
            this.chkIsNull.Size = new System.Drawing.Size(72, 16);
            this.chkIsNull.TabIndex = 13;
            this.chkIsNull.Text = "允许为空";
            this.chkIsNull.UseVisualStyleBackColor = true;
            // 
            // txtDefault
            // 
            this.txtDefault.Location = new System.Drawing.Point(76, 83);
            this.txtDefault.Name = "txtDefault";
            this.txtDefault.Size = new System.Drawing.Size(153, 21);
            this.txtDefault.TabIndex = 11;
            this.txtDefault.Leave += new System.EventHandler(this.txtDefault_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "字段类型:";
            // 
            // txtFieldAlias
            // 
            this.txtFieldAlias.Location = new System.Drawing.Point(76, 30);
            this.txtFieldAlias.Name = "txtFieldAlias";
            this.txtFieldAlias.Size = new System.Drawing.Size(153, 21);
            this.txtFieldAlias.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "字段别名:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "字段名称:";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(383, 378);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(70, 23);
            this.btnApply.TabIndex = 20;
            this.btnApply.Text = "应用";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // FormSchemaEdit
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(461, 406);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSchemaEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "表结构修改";
            this.Load += new System.EventHandler(this.FormSchemaEdit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.pnlAtrr.ResumeLayout(false);
            this.pnlAtrr.PerformLayout();
            this.pnlGeoAttr.ResumeLayout(false);
            this.pnlGeoAttr.PerformLayout();
            this.pnlCommon.ResumeLayout(false);
            this.pnlCommon.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtTableAlias;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listFields;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel pnlAtrr;
        private System.Windows.Forms.TextBox txtScale;
        private System.Windows.Forms.Label lblScale;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox combDomains;
        private System.Windows.Forms.Panel pnlCommon;
        private System.Windows.Forms.ComboBox combFieldType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkIsNull;
        private System.Windows.Forms.TextBox txtDefault;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFieldAlias;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDelField;
        private System.Windows.Forms.Button btnAddField;
        private System.Windows.Forms.TextBox txtFieldName;
        private System.Windows.Forms.Panel pnlGeoAttr;
        private System.Windows.Forms.TextBox txtGeometryType;
        private System.Windows.Forms.Label lblGeoType;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnApply;
    }
}