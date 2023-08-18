namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    partial class DomainPropertyDialog
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.combMergePolicy = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.combSplitPolicy = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.combFieldType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.combDomainType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.codeDataGrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtMaxValue = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMinValue = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.codeDataGrid)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称:";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.SystemColors.Control;
            this.txtName.Location = new System.Drawing.Point(89, 12);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(195, 21);
            this.txtName.TabIndex = 0;
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.SystemColors.Control;
            this.txtDescription.Location = new System.Drawing.Point(89, 45);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(195, 21);
            this.txtDescription.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "描述信息:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 83);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(291, 231);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.combMergePolicy);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.combSplitPolicy);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.combFieldType);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.combDomainType);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(283, 206);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "属性设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // combMergePolicy
            // 
            this.combMergePolicy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combMergePolicy.FormattingEnabled = true;
            this.combMergePolicy.Location = new System.Drawing.Point(73, 139);
            this.combMergePolicy.Name = "combMergePolicy";
            this.combMergePolicy.Size = new System.Drawing.Size(195, 20);
            this.combMergePolicy.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 143);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 23;
            this.label8.Text = "合并策略:";
            // 
            // combSplitPolicy
            // 
            this.combSplitPolicy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combSplitPolicy.FormattingEnabled = true;
            this.combSplitPolicy.Location = new System.Drawing.Point(73, 103);
            this.combSplitPolicy.Name = "combSplitPolicy";
            this.combSplitPolicy.Size = new System.Drawing.Size(195, 20);
            this.combSplitPolicy.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 21;
            this.label7.Text = "分割策略:";
            // 
            // combFieldType
            // 
            this.combFieldType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combFieldType.FormattingEnabled = true;
            this.combFieldType.Location = new System.Drawing.Point(73, 67);
            this.combFieldType.Name = "combFieldType";
            this.combFieldType.Size = new System.Drawing.Size(195, 20);
            this.combFieldType.TabIndex = 1;
            this.combFieldType.SelectedIndexChanged += new System.EventHandler(this.combFieldType_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "域类型:";
            // 
            // combDomainType
            // 
            this.combDomainType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combDomainType.FormattingEnabled = true;
            this.combDomainType.Location = new System.Drawing.Point(73, 31);
            this.combDomainType.Name = "combDomainType";
            this.combDomainType.Size = new System.Drawing.Size(195, 20);
            this.combDomainType.TabIndex = 0;
            this.combDomainType.SelectedIndexChanged += new System.EventHandler(this.combDomainType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "字段类型:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.codeDataGrid);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(283, 206);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "代码值域";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // codeDataGrid
            // 
            this.codeDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.codeDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.codeDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeDataGrid.Location = new System.Drawing.Point(3, 3);
            this.codeDataGrid.Name = "codeDataGrid";
            this.codeDataGrid.RowTemplate.Height = 23;
            this.codeDataGrid.Size = new System.Drawing.Size(277, 200);
            this.codeDataGrid.TabIndex = 6;
            this.codeDataGrid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.codeDataGrid_CellValidating);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "代码值";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "代码描述";
            this.Column2.Name = "Column2";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtMaxValue);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.txtMinValue);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Location = new System.Drawing.Point(4, 21);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(283, 206);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "范围域";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtMaxValue
            // 
            this.txtMaxValue.Location = new System.Drawing.Point(73, 68);
            this.txtMaxValue.Name = "txtMaxValue";
            this.txtMaxValue.Size = new System.Drawing.Size(195, 21);
            this.txtMaxValue.TabIndex = 24;
            this.txtMaxValue.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 23;
            this.label6.Text = "最大值:";
            // 
            // txtMinValue
            // 
            this.txtMinValue.Location = new System.Drawing.Point(73, 35);
            this.txtMinValue.Name = "txtMinValue";
            this.txtMinValue.Size = new System.Drawing.Size(195, 21);
            this.txtMinValue.TabIndex = 22;
            this.txtMinValue.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "最小值:";
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Controls.Add(this.btnOk);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 319);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(311, 30);
            this.panelBottom.TabIndex = 8;
            this.panelBottom.VisibleChanged += new System.EventHandler(this.panelBottom_VisibleChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(233, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(157, 0);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(70, 24);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // DomainPropertyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 349);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DomainPropertyDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "域设置";
            this.Load += new System.EventHandler(this.DomainPropertyDialog_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.codeDataGrid)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox combMergePolicy;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox combSplitPolicy;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox combFieldType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox combDomainType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView codeDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox txtMaxValue;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMinValue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
    }
}