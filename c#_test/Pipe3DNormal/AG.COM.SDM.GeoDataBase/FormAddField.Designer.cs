namespace AG.COM.SDM.GeoDataBase
{
    partial class FormAddField
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
            this.txtFieldName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFieldAlias = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.combFieldType = new System.Windows.Forms.ComboBox();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.lblLength = new System.Windows.Forms.Label();
            this.lblScale = new System.Windows.Forms.Label();
            this.txtScale = new System.Windows.Forms.TextBox();
            this.chkIsNull = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "字段名称";
            // 
            // txtFieldName
            // 
            this.txtFieldName.Location = new System.Drawing.Point(77, 17);
            this.txtFieldName.Name = "txtFieldName";
            this.txtFieldName.Size = new System.Drawing.Size(181, 21);
            this.txtFieldName.TabIndex = 0;
            this.txtFieldName.Validated += new System.EventHandler(this.txtFieldName_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "字段别名";
            // 
            // txtFieldAlias
            // 
            this.txtFieldAlias.Location = new System.Drawing.Point(77, 48);
            this.txtFieldAlias.Name = "txtFieldAlias";
            this.txtFieldAlias.Size = new System.Drawing.Size(181, 21);
            this.txtFieldAlias.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "字段类型";
            // 
            // combFieldType
            // 
            this.combFieldType.FormattingEnabled = true;
            this.combFieldType.Location = new System.Drawing.Point(77, 79);
            this.combFieldType.Name = "combFieldType";
            this.combFieldType.Size = new System.Drawing.Size(181, 20);
            this.combFieldType.TabIndex = 2;
            this.combFieldType.SelectedIndexChanged += new System.EventHandler(this.combFieldType_SelectedIndexChanged);
            // 
            // txtLength
            // 
            this.txtLength.Location = new System.Drawing.Point(77, 109);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(181, 21);
            this.txtLength.TabIndex = 3;
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(12, 113);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(53, 12);
            this.lblLength.TabIndex = 5;
            this.lblLength.Text = "字段长度";
            // 
            // lblScale
            // 
            this.lblScale.AutoSize = true;
            this.lblScale.Location = new System.Drawing.Point(12, 144);
            this.lblScale.Name = "lblScale";
            this.lblScale.Size = new System.Drawing.Size(53, 12);
            this.lblScale.TabIndex = 6;
            this.lblScale.Text = "小数位数";
            // 
            // txtScale
            // 
            this.txtScale.Location = new System.Drawing.Point(77, 140);
            this.txtScale.Name = "txtScale";
            this.txtScale.Size = new System.Drawing.Size(181, 21);
            this.txtScale.TabIndex = 4;
            // 
            // chkIsNull
            // 
            this.chkIsNull.AutoSize = true;
            this.chkIsNull.Location = new System.Drawing.Point(77, 171);
            this.chkIsNull.Name = "chkIsNull";
            this.chkIsNull.Size = new System.Drawing.Size(72, 16);
            this.chkIsNull.TabIndex = 5;
            this.chkIsNull.Text = "允许为空";
            this.chkIsNull.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(102, 198);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(183, 198);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormAddField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 226);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkIsNull);
            this.Controls.Add(this.lblScale);
            this.Controls.Add(this.lblLength);
            this.Controls.Add(this.txtScale);
            this.Controls.Add(this.txtLength);
            this.Controls.Add(this.combFieldType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFieldAlias);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFieldName);
            this.Controls.Add(this.label1);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAddField";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新增字段";
            this.Load += new System.EventHandler(this.FormAddField_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFieldName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFieldAlias;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox combFieldType;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.Label lblScale;
        private System.Windows.Forms.TextBox txtScale;
        private System.Windows.Forms.CheckBox chkIsNull;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}