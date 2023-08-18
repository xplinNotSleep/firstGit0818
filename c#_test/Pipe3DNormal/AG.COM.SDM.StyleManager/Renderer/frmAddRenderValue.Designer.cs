namespace AG.COM.SDM.StyleManager.Renderer
{
    partial class frmAddRenderValue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddRenderValue));
            this.lbxRenderValue = new System.Windows.Forms.ListBox();
            this.butOK = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.butAdd = new System.Windows.Forms.Button();
            this.tbxNewValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbxRenderValue
            // 
            this.lbxRenderValue.FormattingEnabled = true;
            this.lbxRenderValue.ItemHeight = 12;
            this.lbxRenderValue.Location = new System.Drawing.Point(12, 29);
            this.lbxRenderValue.Name = "lbxRenderValue";
            this.lbxRenderValue.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbxRenderValue.Size = new System.Drawing.Size(120, 124);
            this.lbxRenderValue.TabIndex = 0;
            this.lbxRenderValue.SelectedIndexChanged += new System.EventHandler(this.lbxRenderValue_SelectedIndexChanged);
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(196, 30);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(58, 23);
            this.butOK.TabIndex = 1;
            this.butOK.Text = "确定";
            this.butOK.UseVisualStyleBackColor = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(196, 63);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(58, 23);
            this.butCancel.TabIndex = 2;
            this.butCancel.Text = "取消";
            this.butCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "选择要添加的值：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.butAdd);
            this.groupBox1.Controls.Add(this.tbxNewValue);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 162);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 75);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "新值";
            // 
            // butAdd
            // 
            this.butAdd.Location = new System.Drawing.Point(172, 39);
            this.butAdd.Name = "butAdd";
            this.butAdd.Size = new System.Drawing.Size(52, 23);
            this.butAdd.TabIndex = 2;
            this.butAdd.Text = "添加";
            this.butAdd.UseVisualStyleBackColor = true;
            this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
            // 
            // tbxNewValue
            // 
            this.tbxNewValue.Location = new System.Drawing.Point(16, 39);
            this.tbxNewValue.Name = "tbxNewValue";
            this.tbxNewValue.Size = new System.Drawing.Size(150, 21);
            this.tbxNewValue.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "创建新值到列表中：";
            // 
            // frmAddRenderValue
            // 
            this.AcceptButton = this.butOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.butCancel;
            this.ClientSize = new System.Drawing.Size(266, 249);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.lbxRenderValue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddRenderValue";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加渲染值";
            this.Load += new System.EventHandler(this.frmAddRenderValue_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbxRenderValue;
        private System.Windows.Forms.Button butOK;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button butAdd;
        private System.Windows.Forms.TextBox tbxNewValue;
        private System.Windows.Forms.Label label2;
    }
}