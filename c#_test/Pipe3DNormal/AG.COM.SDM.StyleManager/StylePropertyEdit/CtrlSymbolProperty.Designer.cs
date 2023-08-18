namespace AG.COM.SDM.StylePropertyEdit
{
	partial class CtrlSymbolProperty
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

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxSymbolUnit = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxSymbolType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControlProperty = new System.Windows.Forms.TabControl();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxSymbolUnit);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.comboBoxSymbolType);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.tabControlProperty);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(611, 420);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "属性";
            // 
            // comboBoxSymbolUnit
            // 
            this.comboBoxSymbolUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSymbolUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSymbolUnit.FormattingEnabled = true;
            this.comboBoxSymbolUnit.Items.AddRange(new object[] {
            "点",
            "英寸",
            "厘米",
            "毫米"});
            this.comboBoxSymbolUnit.Location = new System.Drawing.Point(508, 17);
            this.comboBoxSymbolUnit.Name = "comboBoxSymbolUnit";
            this.comboBoxSymbolUnit.Size = new System.Drawing.Size(97, 20);
            this.comboBoxSymbolUnit.TabIndex = 3;
            this.comboBoxSymbolUnit.SelectedIndexChanged += new System.EventHandler(this.comboBoxSymbolUnit_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(460, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "单位：";
            // 
            // comboBoxSymbolType
            // 
            this.comboBoxSymbolType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSymbolType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSymbolType.FormattingEnabled = true;
            this.comboBoxSymbolType.Location = new System.Drawing.Point(53, 17);
            this.comboBoxSymbolType.Name = "comboBoxSymbolType";
            this.comboBoxSymbolType.Size = new System.Drawing.Size(200, 20);
            this.comboBoxSymbolType.TabIndex = 1;
            this.comboBoxSymbolType.SelectedIndexChanged += new System.EventHandler(this.comboBoxSymbolType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "类型：";
            // 
            // tabControlProperty
            // 
            this.tabControlProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlProperty.Location = new System.Drawing.Point(8, 43);
            this.tabControlProperty.Name = "tabControlProperty";
            this.tabControlProperty.SelectedIndex = 0;
            this.tabControlProperty.Size = new System.Drawing.Size(593, 371);
            this.tabControlProperty.TabIndex = 4;
            // 
            // CtrlSymbolProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Name = "CtrlSymbolProperty";
            this.Size = new System.Drawing.Size(611, 420);
            this.Load += new System.EventHandler(this.CtrlSymbolProperty_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox comboBoxSymbolUnit;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxSymbolType;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabControl tabControlProperty;
    }
}
