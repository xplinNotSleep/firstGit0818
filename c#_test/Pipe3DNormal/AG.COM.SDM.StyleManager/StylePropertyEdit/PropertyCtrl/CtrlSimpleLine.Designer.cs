namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	partial class CtrlSimpleLine
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
			this.colorPickerSymbol = new AG.COM.SDM.Utility.Controls.ColorPicker();
			this.comboBoxSMStyle = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).BeginInit();
			this.SuspendLayout();
			// 
			// colorPickerSymbol
			// 
			this.colorPickerSymbol.BackColor = System.Drawing.SystemColors.Window;
			this.colorPickerSymbol.Context = null;
			this.colorPickerSymbol.ForeColor = System.Drawing.SystemColors.WindowText;
			this.colorPickerSymbol.Location = new System.Drawing.Point(76, 14);
			this.colorPickerSymbol.Name = "colorPickerSymbol";
			this.colorPickerSymbol.ReadOnly = false;
			this.colorPickerSymbol.Size = new System.Drawing.Size(145, 21);
			this.colorPickerSymbol.TabIndex = 16;
			this.colorPickerSymbol.Text = "White";
			this.colorPickerSymbol.Value = System.Drawing.Color.White;
			this.colorPickerSymbol.ValueChanged += new System.EventHandler(this.colorPickerSymbol_ValueChanged);
			// 
			// comboBoxSMStyle
			// 
			this.comboBoxSMStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSMStyle.FormattingEnabled = true;
			this.comboBoxSMStyle.Items.AddRange(new object[] {
            "Solid",
            "Dashed",
            "Dotted",
            "Dash-Dot",
            "Dash-Dot-Dot",
            "Null",
            "InsideFrame"});
			this.comboBoxSMStyle.Location = new System.Drawing.Point(76, 52);
			this.comboBoxSMStyle.Name = "comboBoxSMStyle";
			this.comboBoxSMStyle.Size = new System.Drawing.Size(145, 20);
			this.comboBoxSMStyle.TabIndex = 14;
			this.comboBoxSMStyle.SelectedIndexChanged += new System.EventHandler(this.comboBoxSMStyle_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 55);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 12);
			this.label2.TabIndex = 12;
			this.label2.Text = "样式：";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 11;
			this.label1.Text = "颜色：";
			// 
			// numericUpDownWidth
			// 
			this.numericUpDownWidth.DecimalPlaces = 4;
			this.numericUpDownWidth.Location = new System.Drawing.Point(76, 89);
			this.numericUpDownWidth.Name = "numericUpDownWidth";
			this.numericUpDownWidth.Size = new System.Drawing.Size(84, 21);
			this.numericUpDownWidth.TabIndex = 23;
			this.numericUpDownWidth.ValueChanged += new System.EventHandler(this.numericUpDownWidth_ValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(14, 89);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 12);
			this.label3.TabIndex = 22;
			this.label3.Text = "宽度：";
			// 
			// CtrlSimpleLine
			// 
			this.AccessibleName = "简单线";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.numericUpDownWidth);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.colorPickerSymbol);
			this.Controls.Add(this.comboBoxSMStyle);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "CtrlSimpleLine";
			this.Size = new System.Drawing.Size(486, 346);
			this.Load += new System.EventHandler(this.CtrlSimpleLine_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private AG.COM.SDM.Utility.Controls.ColorPicker colorPickerSymbol;
		private System.Windows.Forms.ComboBox comboBoxSMStyle;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericUpDownWidth;
		private System.Windows.Forms.Label label3;
	}
}
