namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	partial class CtrlLineFill
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
			this.label7 = new System.Windows.Forms.Label();
			this.numericUpDownAngle = new System.Windows.Forms.NumericUpDown();
			this.colorPickerSymbol = new AG.COM.SDM.Utility.Controls.ColorPicker();
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDownOffset = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.btnSelLineSymbol = new System.Windows.Forms.Button();
			this.btnSelOutline = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDownSeparation = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownOffset)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeparation)).BeginInit();
			this.SuspendLayout();
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(25, 60);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(41, 12);
			this.label7.TabIndex = 25;
			this.label7.Text = "角度：";
			// 
			// numericUpDownAngle
			// 
			this.numericUpDownAngle.DecimalPlaces = 4;
			this.numericUpDownAngle.Location = new System.Drawing.Point(70, 56);
			this.numericUpDownAngle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
			this.numericUpDownAngle.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
			this.numericUpDownAngle.Name = "numericUpDownAngle";
			this.numericUpDownAngle.Size = new System.Drawing.Size(145, 21);
			this.numericUpDownAngle.TabIndex = 26;
			this.numericUpDownAngle.ValueChanged += new System.EventHandler(this.numericUpDownAngle_ValueChanged);
			// 
			// colorPickerSymbol
			// 
			this.colorPickerSymbol.BackColor = System.Drawing.SystemColors.Window;
			this.colorPickerSymbol.Context = null;
			this.colorPickerSymbol.ForeColor = System.Drawing.SystemColors.WindowText;
			this.colorPickerSymbol.Location = new System.Drawing.Point(70, 17);
			this.colorPickerSymbol.Name = "colorPickerSymbol";
			this.colorPickerSymbol.ReadOnly = false;
			this.colorPickerSymbol.Size = new System.Drawing.Size(145, 21);
			this.colorPickerSymbol.TabIndex = 28;
			this.colorPickerSymbol.Text = "White";
			this.colorPickerSymbol.Value = System.Drawing.Color.White;
			this.colorPickerSymbol.ValueChanged += new System.EventHandler(this.colorPickerSymbol_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(25, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 27;
			this.label1.Text = "颜色：";
			// 
			// numericUpDownOffset
			// 
			this.numericUpDownOffset.DecimalPlaces = 4;
			this.numericUpDownOffset.Location = new System.Drawing.Point(70, 95);
			this.numericUpDownOffset.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDownOffset.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
			this.numericUpDownOffset.Name = "numericUpDownOffset";
			this.numericUpDownOffset.Size = new System.Drawing.Size(145, 21);
			this.numericUpDownOffset.TabIndex = 30;
			this.numericUpDownOffset.ValueChanged += new System.EventHandler(this.numericUpDownOffset_ValueChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(25, 100);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 12);
			this.label4.TabIndex = 29;
			this.label4.Text = "偏移：";
			// 
			// btnSelLineSymbol
			// 
			this.btnSelLineSymbol.Location = new System.Drawing.Point(255, 56);
			this.btnSelLineSymbol.Name = "btnSelLineSymbol";
			this.btnSelLineSymbol.Size = new System.Drawing.Size(93, 23);
			this.btnSelLineSymbol.TabIndex = 31;
			this.btnSelLineSymbol.Text = "线符号…";
			this.btnSelLineSymbol.UseVisualStyleBackColor = true;
			this.btnSelLineSymbol.Click += new System.EventHandler(this.btnSelLineSymbol_Click);
			// 
			// btnSelOutline
			// 
			this.btnSelOutline.Location = new System.Drawing.Point(255, 95);
			this.btnSelOutline.Name = "btnSelOutline";
			this.btnSelOutline.Size = new System.Drawing.Size(93, 23);
			this.btnSelOutline.TabIndex = 32;
			this.btnSelOutline.Text = "轮廓线…";
			this.btnSelOutline.UseVisualStyleBackColor = true;
			this.btnSelOutline.Click += new System.EventHandler(this.btnSelOutline_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(25, 136);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 12);
			this.label2.TabIndex = 29;
			this.label2.Text = "间隔：";
			// 
			// numericUpDownSeparation
			// 
			this.numericUpDownSeparation.DecimalPlaces = 4;
			this.numericUpDownSeparation.Location = new System.Drawing.Point(70, 131);
			this.numericUpDownSeparation.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDownSeparation.Name = "numericUpDownSeparation";
			this.numericUpDownSeparation.Size = new System.Drawing.Size(145, 21);
			this.numericUpDownSeparation.TabIndex = 30;
			this.numericUpDownSeparation.ValueChanged += new System.EventHandler(this.numericUpDownSeparation_ValueChanged);
			// 
			// CtrlLineFill
			// 
			this.AccessibleName = "线填充";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnSelOutline);
			this.Controls.Add(this.btnSelLineSymbol);
			this.Controls.Add(this.numericUpDownSeparation);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.numericUpDownOffset);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.colorPickerSymbol);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.numericUpDownAngle);
			this.Name = "CtrlLineFill";
			this.Size = new System.Drawing.Size(486, 346);
			this.Load += new System.EventHandler(this.CtrlLineFill_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownOffset)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSeparation)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown numericUpDownAngle;
		private AG.COM.SDM.Utility.Controls.ColorPicker colorPickerSymbol;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericUpDownOffset;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnSelLineSymbol;
		private System.Windows.Forms.Button btnSelOutline;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDownSeparation;
	}
}
