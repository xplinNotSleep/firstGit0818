namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	partial class CtrlMask
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
			this.radioButtonHalo = new System.Windows.Forms.RadioButton();
			this.radioButtonNone = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDownSize = new System.Windows.Forms.NumericUpDown();
			this.btnSymbolSel = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSize)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButtonHalo);
			this.groupBox1.Controls.Add(this.radioButtonNone);
			this.groupBox1.Location = new System.Drawing.Point(12, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(133, 76);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "样式";
			// 
			// radioButtonHalo
			// 
			this.radioButtonHalo.AutoSize = true;
			this.radioButtonHalo.Location = new System.Drawing.Point(6, 42);
			this.radioButtonHalo.Name = "radioButtonHalo";
			this.radioButtonHalo.Size = new System.Drawing.Size(47, 16);
			this.radioButtonHalo.TabIndex = 1;
			this.radioButtonHalo.TabStop = true;
			this.radioButtonHalo.Text = "光环";
			this.radioButtonHalo.UseVisualStyleBackColor = true;
			this.radioButtonHalo.CheckedChanged += new System.EventHandler(this.radioButtonNone_CheckedChanged);
			// 
			// radioButtonNone
			// 
			this.radioButtonNone.AutoSize = true;
			this.radioButtonNone.Location = new System.Drawing.Point(6, 20);
			this.radioButtonNone.Name = "radioButtonNone";
			this.radioButtonNone.Size = new System.Drawing.Size(35, 16);
			this.radioButtonNone.TabIndex = 0;
			this.radioButtonNone.TabStop = true;
			this.radioButtonNone.Text = "无";
			this.radioButtonNone.UseVisualStyleBackColor = true;
			this.radioButtonNone.CheckedChanged += new System.EventHandler(this.radioButtonNone_CheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 98);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "大小：";
			// 
			// numericUpDownSize
			// 
			this.numericUpDownSize.DecimalPlaces = 4;
			this.numericUpDownSize.Location = new System.Drawing.Point(63, 96);
			this.numericUpDownSize.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDownSize.Name = "numericUpDownSize";
			this.numericUpDownSize.Size = new System.Drawing.Size(82, 21);
			this.numericUpDownSize.TabIndex = 2;
			this.numericUpDownSize.ValueChanged += new System.EventHandler(this.numericUpDownSize_ValueChanged);
			// 
			// btnSymbolSel
			// 
			this.btnSymbolSel.Location = new System.Drawing.Point(165, 94);
			this.btnSymbolSel.Name = "btnSymbolSel";
			this.btnSymbolSel.Size = new System.Drawing.Size(95, 23);
			this.btnSymbolSel.TabIndex = 3;
			this.btnSymbolSel.Text = "选择符号…";
			this.btnSymbolSel.UseVisualStyleBackColor = true;
			this.btnSymbolSel.Click += new System.EventHandler(this.btnSymbolSel_Click);
			// 
			// CtrlMask
			// 
			this.AccessibleName = "掩膜";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnSymbolSel);
			this.Controls.Add(this.numericUpDownSize);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox1);
			this.Name = "CtrlMask";
			this.Size = new System.Drawing.Size(486, 346);
			this.Load += new System.EventHandler(this.CtrlMask_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSize)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioButtonHalo;
		private System.Windows.Forms.RadioButton radioButtonNone;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericUpDownSize;
		private System.Windows.Forms.Button btnSymbolSel;
	}
}
