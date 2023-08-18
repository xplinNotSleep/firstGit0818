namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	partial class CtrlAdvancedText
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
			this.checkBoxTextFillPattern = new System.Windows.Forms.CheckBox();
			this.checkBoxTextBackground = new System.Windows.Forms.CheckBox();
			this.btnSelFillSymbol = new System.Windows.Forms.Button();
			this.btnBackground = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.numericUpDownYOffset = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownXOffset = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.colorPickerSymbol = new AG.COM.SDM.Utility.Controls.ColorPicker();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownYOffset)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownXOffset)).BeginInit();
			this.SuspendLayout();
			// 
			// checkBoxTextFillPattern
			// 
			this.checkBoxTextFillPattern.AutoSize = true;
			this.checkBoxTextFillPattern.Location = new System.Drawing.Point(19, 17);
			this.checkBoxTextFillPattern.Name = "checkBoxTextFillPattern";
			this.checkBoxTextFillPattern.Size = new System.Drawing.Size(96, 16);
			this.checkBoxTextFillPattern.TabIndex = 0;
			this.checkBoxTextFillPattern.Text = "文本填充样式";
			this.checkBoxTextFillPattern.UseVisualStyleBackColor = true;
			this.checkBoxTextFillPattern.CheckedChanged += new System.EventHandler(this.checkBoxTextFillPattern_CheckedChanged);
			// 
			// checkBoxTextBackground
			// 
			this.checkBoxTextBackground.AutoSize = true;
			this.checkBoxTextBackground.Location = new System.Drawing.Point(19, 68);
			this.checkBoxTextBackground.Name = "checkBoxTextBackground";
			this.checkBoxTextBackground.Size = new System.Drawing.Size(72, 16);
			this.checkBoxTextBackground.TabIndex = 1;
			this.checkBoxTextBackground.Text = "文本背景";
			this.checkBoxTextBackground.UseVisualStyleBackColor = true;
			this.checkBoxTextBackground.CheckedChanged += new System.EventHandler(this.checkBoxTextBackground_CheckedChanged);
			// 
			// btnSelFillSymbol
			// 
			this.btnSelFillSymbol.Enabled = false;
			this.btnSelFillSymbol.Location = new System.Drawing.Point(34, 39);
			this.btnSelFillSymbol.Name = "btnSelFillSymbol";
			this.btnSelFillSymbol.Size = new System.Drawing.Size(75, 23);
			this.btnSelFillSymbol.TabIndex = 2;
			this.btnSelFillSymbol.Text = "属性";
			this.btnSelFillSymbol.UseVisualStyleBackColor = true;
			this.btnSelFillSymbol.Click += new System.EventHandler(this.btnSelFillSymbol_Click);
			// 
			// btnBackground
			// 
			this.btnBackground.Enabled = false;
			this.btnBackground.Location = new System.Drawing.Point(34, 90);
			this.btnBackground.Name = "btnBackground";
			this.btnBackground.Size = new System.Drawing.Size(75, 23);
			this.btnBackground.TabIndex = 3;
			this.btnBackground.Text = "属性";
			this.btnBackground.UseVisualStyleBackColor = true;
			this.btnBackground.Click += new System.EventHandler(this.btnBackground_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.numericUpDownYOffset);
			this.groupBox1.Controls.Add(this.numericUpDownXOffset);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.colorPickerSymbol);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(19, 131);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(247, 159);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "阴影";
			// 
			// numericUpDownYOffset
			// 
			this.numericUpDownYOffset.DecimalPlaces = 4;
			this.numericUpDownYOffset.Location = new System.Drawing.Point(68, 93);
			this.numericUpDownYOffset.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDownYOffset.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
			this.numericUpDownYOffset.Name = "numericUpDownYOffset";
			this.numericUpDownYOffset.Size = new System.Drawing.Size(173, 21);
			this.numericUpDownYOffset.TabIndex = 12;
			this.numericUpDownYOffset.ValueChanged += new System.EventHandler(this.numericUpDownYOffset_ValueChanged);
			// 
			// numericUpDownXOffset
			// 
			this.numericUpDownXOffset.DecimalPlaces = 4;
			this.numericUpDownXOffset.Location = new System.Drawing.Point(68, 55);
			this.numericUpDownXOffset.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDownXOffset.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
			this.numericUpDownXOffset.Name = "numericUpDownXOffset";
			this.numericUpDownXOffset.Size = new System.Drawing.Size(173, 21);
			this.numericUpDownXOffset.TabIndex = 13;
			this.numericUpDownXOffset.ValueChanged += new System.EventHandler(this.numericUpDownXOffset_ValueChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(15, 97);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(47, 12);
			this.label5.TabIndex = 11;
			this.label5.Text = "Y偏移：";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(15, 59);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(47, 12);
			this.label4.TabIndex = 10;
			this.label4.Text = "X偏移：";
			// 
			// colorPickerSymbol
			// 
			this.colorPickerSymbol.BackColor = System.Drawing.SystemColors.Window;
			this.colorPickerSymbol.Context = null;
			this.colorPickerSymbol.ForeColor = System.Drawing.SystemColors.WindowText;
			this.colorPickerSymbol.Location = new System.Drawing.Point(68, 24);
			this.colorPickerSymbol.Name = "colorPickerSymbol";
			this.colorPickerSymbol.ReadOnly = false;
			this.colorPickerSymbol.Size = new System.Drawing.Size(173, 21);
			this.colorPickerSymbol.TabIndex = 1;
			this.colorPickerSymbol.Text = "White";
			this.colorPickerSymbol.Value = System.Drawing.Color.White;
			this.colorPickerSymbol.ValueChanged += new System.EventHandler(this.colorPickerSymbol_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "颜色：";
			// 
			// CtrlAdvancedText
			// 
			this.AccessibleName = "高级文本";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnBackground);
			this.Controls.Add(this.btnSelFillSymbol);
			this.Controls.Add(this.checkBoxTextBackground);
			this.Controls.Add(this.checkBoxTextFillPattern);
			this.Name = "CtrlAdvancedText";
			this.Size = new System.Drawing.Size(486, 346);
			this.Load += new System.EventHandler(this.CtrlAdvancedText_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownYOffset)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownXOffset)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkBoxTextFillPattern;
		private System.Windows.Forms.CheckBox checkBoxTextBackground;
		private System.Windows.Forms.Button btnSelFillSymbol;
		private System.Windows.Forms.Button btnBackground;
		private System.Windows.Forms.GroupBox groupBox1;
		private AG.COM.SDM.Utility.Controls.ColorPicker colorPickerSymbol;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericUpDownYOffset;
		private System.Windows.Forms.NumericUpDown numericUpDownXOffset;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
	}
}
