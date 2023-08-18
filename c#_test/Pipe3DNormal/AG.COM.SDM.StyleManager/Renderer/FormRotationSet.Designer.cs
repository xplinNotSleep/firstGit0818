namespace AG.COM.SDM.StyleManager.Renderer
{
    partial class FormRotationSet
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
            this.cboUVFieldFirst = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rbtnGeo = new System.Windows.Forms.RadioButton();
            this.rbtnMath = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboUVFieldFirst
            // 
            this.cboUVFieldFirst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUVFieldFirst.FormattingEnabled = true;
            this.cboUVFieldFirst.Location = new System.Drawing.Point(12, 21);
            this.cboUVFieldFirst.Name = "cboUVFieldFirst";
            this.cboUVFieldFirst.Size = new System.Drawing.Size(216, 20);
            this.cboUVFieldFirst.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "旋转点依据的字段：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "旋转样式：";
            // 
            // rbtnGeo
            // 
            this.rbtnGeo.AutoSize = true;
            this.rbtnGeo.Checked = true;
            this.rbtnGeo.Location = new System.Drawing.Point(14, 68);
            this.rbtnGeo.Name = "rbtnGeo";
            this.rbtnGeo.Size = new System.Drawing.Size(95, 16);
            this.rbtnGeo.TabIndex = 4;
            this.rbtnGeo.TabStop = true;
            this.rbtnGeo.Text = "地理坐标旋转";
            this.rbtnGeo.UseVisualStyleBackColor = true;
            // 
            // rbtnMath
            // 
            this.rbtnMath.AutoSize = true;
            this.rbtnMath.Location = new System.Drawing.Point(133, 68);
            this.rbtnMath.Name = "rbtnMath";
            this.rbtnMath.Size = new System.Drawing.Size(95, 16);
            this.rbtnMath.TabIndex = 4;
            this.rbtnMath.Text = "平面坐标旋转";
            this.rbtnMath.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(20, 102);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancle.Location = new System.Drawing.Point(139, 102);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 5;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // FormRotationSet
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancle;
            this.ClientSize = new System.Drawing.Size(235, 136);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.rbtnMath);
            this.Controls.Add(this.rbtnGeo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboUVFieldFirst);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRotationSet";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "旋转设置";
            this.Load += new System.EventHandler(this.FormRotationSet_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboUVFieldFirst;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbtnGeo;
        private System.Windows.Forms.RadioButton rbtnMath;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancle;
    }
}