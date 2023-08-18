namespace AG.COM.SDM.GeoDataBase.VersionManager
{
    partial class FormNewVersion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescption = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rdoPrivate = new System.Windows.Forms.RadioButton();
            this.rdoPublic = new System.Windows.Forms.RadioButton();
            this.rdoProtect = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(59, 13);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(213, 21);
            this.txtName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "描述：";
            // 
            // txtDescption
            // 
            this.txtDescption.Location = new System.Drawing.Point(59, 40);
            this.txtDescption.Multiline = true;
            this.txtDescption.Name = "txtDescption";
            this.txtDescption.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescption.Size = new System.Drawing.Size(213, 111);
            this.txtDescption.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "权限：";
            // 
            // rdoPrivate
            // 
            this.rdoPrivate.AutoSize = true;
            this.rdoPrivate.Checked = true;
            this.rdoPrivate.Location = new System.Drawing.Point(59, 164);
            this.rdoPrivate.Name = "rdoPrivate";
            this.rdoPrivate.Size = new System.Drawing.Size(47, 16);
            this.rdoPrivate.TabIndex = 5;
            this.rdoPrivate.TabStop = true;
            this.rdoPrivate.Text = "私有";
            this.rdoPrivate.UseVisualStyleBackColor = true;
            // 
            // rdoPublic
            // 
            this.rdoPublic.AutoSize = true;
            this.rdoPublic.Location = new System.Drawing.Point(127, 164);
            this.rdoPublic.Name = "rdoPublic";
            this.rdoPublic.Size = new System.Drawing.Size(47, 16);
            this.rdoPublic.TabIndex = 6;
            this.rdoPublic.Text = "公共";
            this.rdoPublic.UseVisualStyleBackColor = true;
            // 
            // rdoProtect
            // 
            this.rdoProtect.AutoSize = true;
            this.rdoProtect.Location = new System.Drawing.Point(197, 164);
            this.rdoProtect.Name = "rdoProtect";
            this.rdoProtect.Size = new System.Drawing.Size(47, 16);
            this.rdoProtect.TabIndex = 7;
            this.rdoProtect.Text = "保护";
            this.rdoProtect.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(116, 194);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(197, 194);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormNewVersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 226);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.rdoProtect);
            this.Controls.Add(this.rdoPublic);
            this.Controls.Add(this.rdoPrivate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDescption);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNewVersion";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新增版本";
            this.Load += new System.EventHandler(this.FormNewVersion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescption;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdoPrivate;
        private System.Windows.Forms.RadioButton rdoPublic;
        private System.Windows.Forms.RadioButton rdoProtect;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}