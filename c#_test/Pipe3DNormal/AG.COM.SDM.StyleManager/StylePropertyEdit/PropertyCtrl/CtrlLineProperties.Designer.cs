namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	partial class CtrlLineProperties
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
			this.numericUpDownOffset = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnProperties = new System.Windows.Forms.Button();
			this.radioButtonBothArrow = new System.Windows.Forms.RadioButton();
			this.radioButtonRightArrow = new System.Windows.Forms.RadioButton();
			this.radioButtonLeftArrow = new System.Windows.Forms.RadioButton();
			this.radioButtonNone = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownOffset)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// numericUpDownOffset
			// 
			this.numericUpDownOffset.DecimalPlaces = 4;
			this.numericUpDownOffset.Location = new System.Drawing.Point(66, 18);
			this.numericUpDownOffset.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDownOffset.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
			this.numericUpDownOffset.Name = "numericUpDownOffset";
			this.numericUpDownOffset.Size = new System.Drawing.Size(93, 21);
			this.numericUpDownOffset.TabIndex = 32;
			this.numericUpDownOffset.ValueChanged += new System.EventHandler(this.numericUpDownOffset_ValueChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(23, 23);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 12);
			this.label4.TabIndex = 31;
			this.label4.Text = "偏移：";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnProperties);
			this.groupBox1.Controls.Add(this.radioButtonBothArrow);
			this.groupBox1.Controls.Add(this.radioButtonRightArrow);
			this.groupBox1.Controls.Add(this.radioButtonLeftArrow);
			this.groupBox1.Controls.Add(this.radioButtonNone);
			this.groupBox1.Location = new System.Drawing.Point(23, 54);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(134, 150);
			this.groupBox1.TabIndex = 33;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "线装饰";
			// 
			// btnProperties
			// 
			this.btnProperties.Location = new System.Drawing.Point(18, 108);
			this.btnProperties.Name = "btnProperties";
			this.btnProperties.Size = new System.Drawing.Size(83, 23);
			this.btnProperties.TabIndex = 4;
			this.btnProperties.Text = "属性…";
			this.btnProperties.UseVisualStyleBackColor = true;
			this.btnProperties.Click += new System.EventHandler(this.btnProperties_Click);
			// 
			// radioButtonBothArrow
			// 
			this.radioButtonBothArrow.AutoSize = true;
			this.radioButtonBothArrow.Location = new System.Drawing.Point(18, 86);
			this.radioButtonBothArrow.Name = "radioButtonBothArrow";
			this.radioButtonBothArrow.Size = new System.Drawing.Size(83, 16);
			this.radioButtonBothArrow.TabIndex = 3;
			this.radioButtonBothArrow.Text = "◀▬▬▬▶";
			this.radioButtonBothArrow.UseVisualStyleBackColor = true;
			this.radioButtonBothArrow.CheckedChanged += new System.EventHandler(this.radioButtonNone_CheckedChanged);
			// 
			// radioButtonRightArrow
			// 
			this.radioButtonRightArrow.AutoSize = true;
			this.radioButtonRightArrow.Location = new System.Drawing.Point(18, 64);
			this.radioButtonRightArrow.Name = "radioButtonRightArrow";
			this.radioButtonRightArrow.Size = new System.Drawing.Size(83, 16);
			this.radioButtonRightArrow.TabIndex = 2;
			this.radioButtonRightArrow.Text = "▬▬▬▬▶";
			this.radioButtonRightArrow.UseVisualStyleBackColor = true;
			this.radioButtonRightArrow.CheckedChanged += new System.EventHandler(this.radioButtonNone_CheckedChanged);
			// 
			// radioButtonLeftArrow
			// 
			this.radioButtonLeftArrow.AutoSize = true;
			this.radioButtonLeftArrow.Location = new System.Drawing.Point(18, 42);
			this.radioButtonLeftArrow.Name = "radioButtonLeftArrow";
			this.radioButtonLeftArrow.Size = new System.Drawing.Size(83, 16);
			this.radioButtonLeftArrow.TabIndex = 1;
			this.radioButtonLeftArrow.Text = "◀▬▬▬▬";
			this.radioButtonLeftArrow.UseVisualStyleBackColor = true;
			this.radioButtonLeftArrow.CheckedChanged += new System.EventHandler(this.radioButtonNone_CheckedChanged);
			// 
			// radioButtonNone
			// 
			this.radioButtonNone.AccessibleName = "线属性";
			this.radioButtonNone.AutoSize = true;
			this.radioButtonNone.Location = new System.Drawing.Point(18, 20);
			this.radioButtonNone.Name = "radioButtonNone";
			this.radioButtonNone.Size = new System.Drawing.Size(35, 16);
			this.radioButtonNone.TabIndex = 0;
			this.radioButtonNone.Text = "无";
			this.radioButtonNone.UseVisualStyleBackColor = true;
			this.radioButtonNone.CheckedChanged += new System.EventHandler(this.radioButtonNone_CheckedChanged);
			// 
			// CtrlLineProperties
			// 
			this.AccessibleName = "线属性";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.numericUpDownOffset);
			this.Controls.Add(this.label4);
			this.Name = "CtrlLineProperties";
			this.Size = new System.Drawing.Size(486, 346);
			this.Load += new System.EventHandler(this.CtrlLineProperties_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownOffset)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown numericUpDownOffset;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnProperties;
		private System.Windows.Forms.RadioButton radioButtonBothArrow;
		private System.Windows.Forms.RadioButton radioButtonRightArrow;
		private System.Windows.Forms.RadioButton radioButtonLeftArrow;
		private System.Windows.Forms.RadioButton radioButtonNone;
	}
}
