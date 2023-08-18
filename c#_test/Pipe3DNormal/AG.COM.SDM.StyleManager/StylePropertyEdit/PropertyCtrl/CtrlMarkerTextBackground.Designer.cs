namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	partial class CtrlMarkerTextBackground
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
			this.checkBoxScaleMarker = new System.Windows.Forms.CheckBox();
			this.colorPickerSymbol = new AG.COM.SDM.Utility.Controls.ColorPicker();
			this.label1 = new System.Windows.Forms.Label();
			this.btnSymbolSel = new System.Windows.Forms.Button();
			this.numericUpDownSize = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSize)).BeginInit();
			this.SuspendLayout();
			// 
			// checkBoxScaleMarker
			// 
			this.checkBoxScaleMarker.AutoSize = true;
			this.checkBoxScaleMarker.Location = new System.Drawing.Point(21, 22);
			this.checkBoxScaleMarker.Name = "checkBoxScaleMarker";
			this.checkBoxScaleMarker.Size = new System.Drawing.Size(120, 16);
			this.checkBoxScaleMarker.TabIndex = 0;
			this.checkBoxScaleMarker.Text = "缩放标记适应文本";
			this.checkBoxScaleMarker.UseVisualStyleBackColor = true;
			this.checkBoxScaleMarker.CheckedChanged += new System.EventHandler(this.checkBoxScaleMarker_CheckedChanged);
			// 
			// colorPickerSymbol
			// 
			this.colorPickerSymbol.BackColor = System.Drawing.SystemColors.Window;
			this.colorPickerSymbol.Context = null;
			this.colorPickerSymbol.ForeColor = System.Drawing.SystemColors.WindowText;
			this.colorPickerSymbol.Location = new System.Drawing.Point(66, 53);
			this.colorPickerSymbol.Name = "colorPickerSymbol";
			this.colorPickerSymbol.ReadOnly = false;
			this.colorPickerSymbol.Size = new System.Drawing.Size(145, 21);
			this.colorPickerSymbol.TabIndex = 24;
			this.colorPickerSymbol.Text = "White";
			this.colorPickerSymbol.Value = System.Drawing.Color.White;
			this.colorPickerSymbol.ValueChanged += new System.EventHandler(this.colorPickerSymbol_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(19, 59);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 23;
			this.label1.Text = "颜色：";
			// 
			// btnSymbolSel
			// 
			this.btnSymbolSel.Location = new System.Drawing.Point(147, 18);
			this.btnSymbolSel.Name = "btnSymbolSel";
			this.btnSymbolSel.Size = new System.Drawing.Size(64, 23);
			this.btnSymbolSel.TabIndex = 27;
			this.btnSymbolSel.Text = "符号…";
			this.btnSymbolSel.UseVisualStyleBackColor = true;
			this.btnSymbolSel.Click += new System.EventHandler(this.btnSymbolSel_Click);
			// 
			// numericUpDownSize
			// 
			this.numericUpDownSize.DecimalPlaces = 4;
			this.numericUpDownSize.Location = new System.Drawing.Point(66, 96);
			this.numericUpDownSize.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDownSize.Name = "numericUpDownSize";
			this.numericUpDownSize.Size = new System.Drawing.Size(145, 21);
			this.numericUpDownSize.TabIndex = 26;
			this.numericUpDownSize.ValueChanged += new System.EventHandler(this.numericUpDownSize_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(19, 98);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 12);
			this.label2.TabIndex = 25;
			this.label2.Text = "大小：";
			// 
			// CtrlMarkerTextBackground
			// 
			this.AccessibleName = "标记文本背景";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnSymbolSel);
			this.Controls.Add(this.numericUpDownSize);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.colorPickerSymbol);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkBoxScaleMarker);
			this.Name = "CtrlMarkerTextBackground";
			this.Size = new System.Drawing.Size(486, 346);
			this.Load += new System.EventHandler(this.CtrlMarkerTextBackground_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSize)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkBoxScaleMarker;
		private AG.COM.SDM.Utility.Controls.ColorPicker colorPickerSymbol;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnSymbolSel;
		private System.Windows.Forms.NumericUpDown numericUpDownSize;
		private System.Windows.Forms.Label label2;
	}
}
