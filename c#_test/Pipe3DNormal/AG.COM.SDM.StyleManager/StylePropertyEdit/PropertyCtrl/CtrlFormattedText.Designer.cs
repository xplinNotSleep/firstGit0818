namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	partial class CtrlFormattedText
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButtonSubscript = new System.Windows.Forms.RadioButton();
			this.radioButtonSuperscript = new System.Windows.Forms.RadioButton();
			this.radioButtonPNormal = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.radioButtonCSmallCaps = new System.Windows.Forms.RadioButton();
			this.radioButtonCAllCaps = new System.Windows.Forms.RadioButton();
			this.radioButtonCNormal = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.checkBoxKerning = new System.Windows.Forms.CheckBox();
			this.numericUpDownCharSpacing = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownLeading = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownFlipAngle = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownCharWidth = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownWordSpacing = new System.Windows.Forms.NumericUpDown();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownCharSpacing)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeading)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownFlipAngle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownCharWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWordSpacing)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButtonSubscript);
			this.groupBox1.Controls.Add(this.radioButtonSuperscript);
			this.groupBox1.Controls.Add(this.radioButtonPNormal);
			this.groupBox1.Location = new System.Drawing.Point(22, 17);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(152, 100);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "文本位置";
			// 
			// radioButtonSubscript
			// 
			this.radioButtonSubscript.AutoSize = true;
			this.radioButtonSubscript.Location = new System.Drawing.Point(16, 64);
			this.radioButtonSubscript.Name = "radioButtonSubscript";
			this.radioButtonSubscript.Size = new System.Drawing.Size(47, 16);
			this.radioButtonSubscript.TabIndex = 2;
			this.radioButtonSubscript.Text = "下标";
			this.radioButtonSubscript.UseVisualStyleBackColor = true;
			this.radioButtonSubscript.CheckedChanged += new System.EventHandler(this.radioButtonPNormal_CheckedChanged);
			// 
			// radioButtonSuperscript
			// 
			this.radioButtonSuperscript.AutoSize = true;
			this.radioButtonSuperscript.Location = new System.Drawing.Point(16, 42);
			this.radioButtonSuperscript.Name = "radioButtonSuperscript";
			this.radioButtonSuperscript.Size = new System.Drawing.Size(47, 16);
			this.radioButtonSuperscript.TabIndex = 1;
			this.radioButtonSuperscript.Text = "上标";
			this.radioButtonSuperscript.UseVisualStyleBackColor = true;
			this.radioButtonSuperscript.CheckedChanged += new System.EventHandler(this.radioButtonPNormal_CheckedChanged);
			// 
			// radioButtonPNormal
			// 
			this.radioButtonPNormal.AutoSize = true;
			this.radioButtonPNormal.Checked = true;
			this.radioButtonPNormal.Location = new System.Drawing.Point(16, 20);
			this.radioButtonPNormal.Name = "radioButtonPNormal";
			this.radioButtonPNormal.Size = new System.Drawing.Size(47, 16);
			this.radioButtonPNormal.TabIndex = 0;
			this.radioButtonPNormal.TabStop = true;
			this.radioButtonPNormal.Text = "常规";
			this.radioButtonPNormal.UseVisualStyleBackColor = true;
			this.radioButtonPNormal.CheckedChanged += new System.EventHandler(this.radioButtonPNormal_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.radioButtonCSmallCaps);
			this.groupBox2.Controls.Add(this.radioButtonCAllCaps);
			this.groupBox2.Controls.Add(this.radioButtonCNormal);
			this.groupBox2.Location = new System.Drawing.Point(22, 123);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(152, 100);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "文本大小写";
			// 
			// radioButtonCSmallCaps
			// 
			this.radioButtonCSmallCaps.AutoSize = true;
			this.radioButtonCSmallCaps.Location = new System.Drawing.Point(16, 67);
			this.radioButtonCSmallCaps.Name = "radioButtonCSmallCaps";
			this.radioButtonCSmallCaps.Size = new System.Drawing.Size(71, 16);
			this.radioButtonCSmallCaps.TabIndex = 2;
			this.radioButtonCSmallCaps.Text = "全部小写";
			this.radioButtonCSmallCaps.UseVisualStyleBackColor = true;
			this.radioButtonCSmallCaps.CheckedChanged += new System.EventHandler(this.radioButtonCNormal_CheckedChanged);
			// 
			// radioButtonCAllCaps
			// 
			this.radioButtonCAllCaps.AutoSize = true;
			this.radioButtonCAllCaps.Location = new System.Drawing.Point(16, 45);
			this.radioButtonCAllCaps.Name = "radioButtonCAllCaps";
			this.radioButtonCAllCaps.Size = new System.Drawing.Size(71, 16);
			this.radioButtonCAllCaps.TabIndex = 1;
			this.radioButtonCAllCaps.Text = "全部大写";
			this.radioButtonCAllCaps.UseVisualStyleBackColor = true;
			this.radioButtonCAllCaps.CheckedChanged += new System.EventHandler(this.radioButtonCNormal_CheckedChanged);
			// 
			// radioButtonCNormal
			// 
			this.radioButtonCNormal.AutoSize = true;
			this.radioButtonCNormal.Checked = true;
			this.radioButtonCNormal.Location = new System.Drawing.Point(16, 23);
			this.radioButtonCNormal.Name = "radioButtonCNormal";
			this.radioButtonCNormal.Size = new System.Drawing.Size(47, 16);
			this.radioButtonCNormal.TabIndex = 0;
			this.radioButtonCNormal.TabStop = true;
			this.radioButtonCNormal.Text = "常规";
			this.radioButtonCNormal.UseVisualStyleBackColor = true;
			this.radioButtonCNormal.CheckedChanged += new System.EventHandler(this.radioButtonCNormal_CheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(197, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 12);
			this.label1.TabIndex = 3;
			this.label1.Text = "字符间隔：";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(197, 63);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 12);
			this.label2.TabIndex = 4;
			this.label2.Text = "行距：";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(197, 95);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(65, 12);
			this.label3.TabIndex = 5;
			this.label3.Text = "翻转角度：";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(197, 135);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(65, 12);
			this.label4.TabIndex = 6;
			this.label4.Text = "字符宽度：";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(197, 171);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(65, 12);
			this.label5.TabIndex = 7;
			this.label5.Text = "单词间隔：";
			// 
			// checkBoxKerning
			// 
			this.checkBoxKerning.AutoSize = true;
			this.checkBoxKerning.Location = new System.Drawing.Point(197, 207);
			this.checkBoxKerning.Name = "checkBoxKerning";
			this.checkBoxKerning.Size = new System.Drawing.Size(72, 16);
			this.checkBoxKerning.TabIndex = 8;
			this.checkBoxKerning.Text = "字距调整";
			this.checkBoxKerning.UseVisualStyleBackColor = true;
			this.checkBoxKerning.CheckedChanged += new System.EventHandler(this.checkBoxKerning_CheckedChanged);
			// 
			// numericUpDownCharSpacing
			// 
			this.numericUpDownCharSpacing.DecimalPlaces = 2;
			this.numericUpDownCharSpacing.Location = new System.Drawing.Point(278, 26);
			this.numericUpDownCharSpacing.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDownCharSpacing.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
			this.numericUpDownCharSpacing.Name = "numericUpDownCharSpacing";
			this.numericUpDownCharSpacing.Size = new System.Drawing.Size(103, 21);
			this.numericUpDownCharSpacing.TabIndex = 9;
			this.numericUpDownCharSpacing.ValueChanged += new System.EventHandler(this.numericUpDownCharSpacing_ValueChanged);
			// 
			// numericUpDownLeading
			// 
			this.numericUpDownLeading.DecimalPlaces = 4;
			this.numericUpDownLeading.Location = new System.Drawing.Point(278, 59);
			this.numericUpDownLeading.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDownLeading.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
			this.numericUpDownLeading.Name = "numericUpDownLeading";
			this.numericUpDownLeading.Size = new System.Drawing.Size(103, 21);
			this.numericUpDownLeading.TabIndex = 9;
			this.numericUpDownLeading.ValueChanged += new System.EventHandler(this.numericUpDownLeading_ValueChanged);
			// 
			// numericUpDownFlipAngle
			// 
			this.numericUpDownFlipAngle.DecimalPlaces = 2;
			this.numericUpDownFlipAngle.Location = new System.Drawing.Point(278, 93);
			this.numericUpDownFlipAngle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
			this.numericUpDownFlipAngle.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
			this.numericUpDownFlipAngle.Name = "numericUpDownFlipAngle";
			this.numericUpDownFlipAngle.Size = new System.Drawing.Size(103, 21);
			this.numericUpDownFlipAngle.TabIndex = 9;
			this.numericUpDownFlipAngle.ValueChanged += new System.EventHandler(this.numericUpDownFlipAngle_ValueChanged);
			// 
			// numericUpDownCharWidth
			// 
			this.numericUpDownCharWidth.DecimalPlaces = 2;
			this.numericUpDownCharWidth.Location = new System.Drawing.Point(278, 133);
			this.numericUpDownCharWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDownCharWidth.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
			this.numericUpDownCharWidth.Name = "numericUpDownCharWidth";
			this.numericUpDownCharWidth.Size = new System.Drawing.Size(103, 21);
			this.numericUpDownCharWidth.TabIndex = 9;
			this.numericUpDownCharWidth.ValueChanged += new System.EventHandler(this.numericUpDownCharWidth_ValueChanged);
			// 
			// numericUpDownWordSpacing
			// 
			this.numericUpDownWordSpacing.DecimalPlaces = 2;
			this.numericUpDownWordSpacing.Location = new System.Drawing.Point(278, 168);
			this.numericUpDownWordSpacing.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDownWordSpacing.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
			this.numericUpDownWordSpacing.Name = "numericUpDownWordSpacing";
			this.numericUpDownWordSpacing.Size = new System.Drawing.Size(103, 21);
			this.numericUpDownWordSpacing.TabIndex = 9;
			this.numericUpDownWordSpacing.ValueChanged += new System.EventHandler(this.numericUpDownWordSpacing_ValueChanged);
			// 
			// CtrlFormattedText
			// 
			this.AccessibleName = "格式化文本";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.numericUpDownWordSpacing);
			this.Controls.Add(this.numericUpDownCharWidth);
			this.Controls.Add(this.numericUpDownFlipAngle);
			this.Controls.Add(this.numericUpDownLeading);
			this.Controls.Add(this.numericUpDownCharSpacing);
			this.Controls.Add(this.checkBoxKerning);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "CtrlFormattedText";
			this.Size = new System.Drawing.Size(486, 346);
			this.Load += new System.EventHandler(this.CtrlFormattedText_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownCharSpacing)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeading)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownFlipAngle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownCharWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWordSpacing)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioButtonSubscript;
		private System.Windows.Forms.RadioButton radioButtonSuperscript;
		private System.Windows.Forms.RadioButton radioButtonPNormal;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton radioButtonCSmallCaps;
		private System.Windows.Forms.RadioButton radioButtonCAllCaps;
		private System.Windows.Forms.RadioButton radioButtonCNormal;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox checkBoxKerning;
		private System.Windows.Forms.NumericUpDown numericUpDownCharSpacing;
		private System.Windows.Forms.NumericUpDown numericUpDownLeading;
		private System.Windows.Forms.NumericUpDown numericUpDownFlipAngle;
		private System.Windows.Forms.NumericUpDown numericUpDownCharWidth;
		private System.Windows.Forms.NumericUpDown numericUpDownWordSpacing;
	}
}
