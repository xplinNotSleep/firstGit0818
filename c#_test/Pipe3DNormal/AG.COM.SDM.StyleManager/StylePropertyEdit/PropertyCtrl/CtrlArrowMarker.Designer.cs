namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	partial class CtrlArrowMarker
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
			this.numericUpDownAngle = new System.Windows.Forms.NumericUpDown();
			this.colorPickerSymbol = new AG.COM.SDM.Utility.Controls.ColorPicker();
			this.label7 = new System.Windows.Forms.Label();
			this.numericUpDownYOffset = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownXOffset = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDownLength = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownYOffset)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownXOffset)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownLength)).BeginInit();
			this.SuspendLayout();
			// 
			// numericUpDownAngle
			// 
			this.numericUpDownAngle.DecimalPlaces = 4;
			this.numericUpDownAngle.Location = new System.Drawing.Point(238, 167);
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
			this.numericUpDownAngle.Size = new System.Drawing.Size(96, 21);
			this.numericUpDownAngle.TabIndex = 18;
			this.numericUpDownAngle.ValueChanged += new System.EventHandler(this.numericUpDownAngle_ValueChanged);
			// 
			// colorPickerSymbol
			// 
			this.colorPickerSymbol.BackColor = System.Drawing.SystemColors.Window;
			this.colorPickerSymbol.Context = null;
			this.colorPickerSymbol.ForeColor = System.Drawing.SystemColors.WindowText;
			this.colorPickerSymbol.Location = new System.Drawing.Point(74, 16);
			this.colorPickerSymbol.Name = "colorPickerSymbol";
			this.colorPickerSymbol.ReadOnly = false;
			this.colorPickerSymbol.Size = new System.Drawing.Size(145, 21);
			this.colorPickerSymbol.TabIndex = 22;
			this.colorPickerSymbol.Text = "White";
			this.colorPickerSymbol.Value = System.Drawing.Color.White;
			this.colorPickerSymbol.ValueChanged += new System.EventHandler(this.colorPickerSymbol_ValueChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(191, 170);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(41, 12);
			this.label7.TabIndex = 16;
			this.label7.Text = "角度：";
			// 
			// numericUpDownYOffset
			// 
			this.numericUpDownYOffset.DecimalPlaces = 4;
			this.numericUpDownYOffset.Location = new System.Drawing.Point(74, 167);
			this.numericUpDownYOffset.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
			this.numericUpDownYOffset.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            -2147483648});
			this.numericUpDownYOffset.Name = "numericUpDownYOffset";
			this.numericUpDownYOffset.Size = new System.Drawing.Size(84, 21);
			this.numericUpDownYOffset.TabIndex = 19;
			this.numericUpDownYOffset.ValueChanged += new System.EventHandler(this.numericUpDownYOffset_ValueChanged);
			// 
			// numericUpDownXOffset
			// 
			this.numericUpDownXOffset.DecimalPlaces = 4;
			this.numericUpDownXOffset.Location = new System.Drawing.Point(74, 124);
			this.numericUpDownXOffset.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
			this.numericUpDownXOffset.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            -2147483648});
			this.numericUpDownXOffset.Name = "numericUpDownXOffset";
			this.numericUpDownXOffset.Size = new System.Drawing.Size(84, 21);
			this.numericUpDownXOffset.TabIndex = 20;
			this.numericUpDownXOffset.ValueChanged += new System.EventHandler(this.numericUpDownXOffset_ValueChanged);
			// 
			// numericUpDownWidth
			// 
			this.numericUpDownWidth.DecimalPlaces = 4;
			this.numericUpDownWidth.Location = new System.Drawing.Point(74, 88);
			this.numericUpDownWidth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDownWidth.Name = "numericUpDownWidth";
			this.numericUpDownWidth.Size = new System.Drawing.Size(84, 21);
			this.numericUpDownWidth.TabIndex = 21;
			this.numericUpDownWidth.ValueChanged += new System.EventHandler(this.numericUpDownWidth_ValueChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 170);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(47, 12);
			this.label5.TabIndex = 15;
			this.label5.Text = "Y偏移：";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 126);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(47, 12);
			this.label4.TabIndex = 14;
			this.label4.Text = "X偏移：";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 12);
			this.label3.TabIndex = 13;
			this.label3.Text = "宽度：";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 50);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 12);
			this.label2.TabIndex = 12;
			this.label2.Text = "长度：";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 11;
			this.label1.Text = "颜色：";
			// 
			// numericUpDownLength
			// 
			this.numericUpDownLength.DecimalPlaces = 4;
			this.numericUpDownLength.Location = new System.Drawing.Point(74, 48);
			this.numericUpDownLength.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDownLength.Name = "numericUpDownLength";
			this.numericUpDownLength.Size = new System.Drawing.Size(84, 21);
			this.numericUpDownLength.TabIndex = 21;
			this.numericUpDownLength.ValueChanged += new System.EventHandler(this.numericUpDownLength_ValueChanged);
			// 
			// CtrlArrowMarker
			// 
			this.AccessibleName = "箭头标记";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.numericUpDownAngle);
			this.Controls.Add(this.colorPickerSymbol);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.numericUpDownYOffset);
			this.Controls.Add(this.numericUpDownXOffset);
			this.Controls.Add(this.numericUpDownLength);
			this.Controls.Add(this.numericUpDownWidth);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "CtrlArrowMarker";
			this.Size = new System.Drawing.Size(486, 346);
			this.Load += new System.EventHandler(this.CtrlArrowMarker_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownYOffset)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownXOffset)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownLength)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown numericUpDownAngle;
		private AG.COM.SDM.Utility.Controls.ColorPicker colorPickerSymbol;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown numericUpDownYOffset;
		private System.Windows.Forms.NumericUpDown numericUpDownXOffset;
		private System.Windows.Forms.NumericUpDown numericUpDownWidth;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericUpDownLength;
	}
}
