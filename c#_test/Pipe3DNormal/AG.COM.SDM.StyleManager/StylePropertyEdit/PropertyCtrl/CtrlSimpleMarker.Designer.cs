namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	partial class CtrlSimpleMarker
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.checkBoxUseOutline = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.comboBoxSMStyle = new System.Windows.Forms.ComboBox();
			this.numericUpDownSMSize = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownXOffset = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownYOffset = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownOutlineSize = new System.Windows.Forms.NumericUpDown();
			this.colorPickerSymbol = new AG.COM.SDM.Utility.Controls.ColorPicker();
			this.colorPickerOutline = new AG.COM.SDM.Utility.Controls.ColorPicker();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSMSize)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownXOffset)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownYOffset)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownOutlineSize)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "颜色：";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 59);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 12);
			this.label2.TabIndex = 1;
			this.label2.Text = "样式：";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 97);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 12);
			this.label3.TabIndex = 2;
			this.label3.Text = "大小：";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(13, 135);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(47, 12);
			this.label4.TabIndex = 3;
			this.label4.Text = "X偏移：";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(13, 173);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(47, 12);
			this.label5.TabIndex = 4;
			this.label5.Text = "Y偏移：";
			// 
			// checkBoxUseOutline
			// 
			this.checkBoxUseOutline.AutoSize = true;
			this.checkBoxUseOutline.Location = new System.Drawing.Point(175, 96);
			this.checkBoxUseOutline.Name = "checkBoxUseOutline";
			this.checkBoxUseOutline.Size = new System.Drawing.Size(84, 16);
			this.checkBoxUseOutline.TabIndex = 5;
			this.checkBoxUseOutline.Text = "使用轮廓线";
			this.checkBoxUseOutline.UseVisualStyleBackColor = true;
			this.checkBoxUseOutline.CheckedChanged += new System.EventHandler(this.checkBoxUseOutline_CheckedChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(173, 135);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(77, 12);
			this.label6.TabIndex = 6;
			this.label6.Text = "轮廓线颜色：";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(173, 173);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(77, 12);
			this.label7.TabIndex = 7;
			this.label7.Text = "轮廓线大小：";
			// 
			// comboBoxSMStyle
			// 
			this.comboBoxSMStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSMStyle.FormattingEnabled = true;
			this.comboBoxSMStyle.Items.AddRange(new object[] {
            "Circle",
            "Square",
            "Cross",
            "X",
            "Diamond"});
			this.comboBoxSMStyle.Location = new System.Drawing.Point(75, 56);
			this.comboBoxSMStyle.Name = "comboBoxSMStyle";
			this.comboBoxSMStyle.Size = new System.Drawing.Size(145, 20);
			this.comboBoxSMStyle.TabIndex = 8;
			this.comboBoxSMStyle.SelectedIndexChanged += new System.EventHandler(this.comboBoxSMStyle_SelectedIndexChanged);
			// 
			// numericUpDownSMSize
			// 
			this.numericUpDownSMSize.DecimalPlaces = 4;
			this.numericUpDownSMSize.Location = new System.Drawing.Point(75, 93);
			this.numericUpDownSMSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDownSMSize.Name = "numericUpDownSMSize";
			this.numericUpDownSMSize.Size = new System.Drawing.Size(84, 21);
			this.numericUpDownSMSize.TabIndex = 9;
			this.numericUpDownSMSize.ValueChanged += new System.EventHandler(this.numericUpDownSMSize_ValueChanged);
			// 
			// numericUpDownXOffset
			// 
			this.numericUpDownXOffset.DecimalPlaces = 4;
			this.numericUpDownXOffset.Location = new System.Drawing.Point(75, 131);
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
			this.numericUpDownXOffset.Size = new System.Drawing.Size(84, 21);
			this.numericUpDownXOffset.TabIndex = 9;
			this.numericUpDownXOffset.ValueChanged += new System.EventHandler(this.numericUpDownXOffset_ValueChanged);
			// 
			// numericUpDownYOffset
			// 
			this.numericUpDownYOffset.DecimalPlaces = 4;
			this.numericUpDownYOffset.Location = new System.Drawing.Point(75, 169);
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
			this.numericUpDownYOffset.Size = new System.Drawing.Size(84, 21);
			this.numericUpDownYOffset.TabIndex = 9;
			this.numericUpDownYOffset.ValueChanged += new System.EventHandler(this.numericUpDownYOffset_ValueChanged);
			// 
			// numericUpDownOutlineSize
			// 
			this.numericUpDownOutlineSize.DecimalPlaces = 4;
			this.numericUpDownOutlineSize.Location = new System.Drawing.Point(249, 169);
			this.numericUpDownOutlineSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDownOutlineSize.Name = "numericUpDownOutlineSize";
			this.numericUpDownOutlineSize.Size = new System.Drawing.Size(145, 21);
			this.numericUpDownOutlineSize.TabIndex = 9;
			this.numericUpDownOutlineSize.ValueChanged += new System.EventHandler(this.numericUpDownOutlineSize_ValueChanged);
			// 
			// colorPickerSymbol
			// 
			this.colorPickerSymbol.BackColor = System.Drawing.SystemColors.Window;
			this.colorPickerSymbol.Context = null;
			this.colorPickerSymbol.ForeColor = System.Drawing.SystemColors.WindowText;
			this.colorPickerSymbol.Location = new System.Drawing.Point(75, 18);
			this.colorPickerSymbol.Name = "colorPickerSymbol";
			this.colorPickerSymbol.ReadOnly = false;
			this.colorPickerSymbol.Size = new System.Drawing.Size(145, 21);
			this.colorPickerSymbol.TabIndex = 10;
			this.colorPickerSymbol.Text = "White";
			this.colorPickerSymbol.Value = System.Drawing.Color.White;
			this.colorPickerSymbol.ValueChanged += new System.EventHandler(this.colorPickerSymbol_ValueChanged);
			// 
			// colorPickerOutline
			// 
			this.colorPickerOutline.BackColor = System.Drawing.SystemColors.Window;
			this.colorPickerOutline.Context = null;
			this.colorPickerOutline.ForeColor = System.Drawing.SystemColors.WindowText;
			this.colorPickerOutline.Location = new System.Drawing.Point(249, 131);
			this.colorPickerOutline.Name = "colorPickerOutline";
			this.colorPickerOutline.ReadOnly = false;
			this.colorPickerOutline.Size = new System.Drawing.Size(145, 21);
			this.colorPickerOutline.TabIndex = 11;
			this.colorPickerOutline.Text = "White";
			this.colorPickerOutline.Value = System.Drawing.Color.White;
			this.colorPickerOutline.ValueChanged += new System.EventHandler(this.colorPickerOutline_ValueChanged);
			// 
			// CtrlSimpleMarker
			// 
			this.AccessibleName = "简单标记";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.numericUpDownOutlineSize);
			this.Controls.Add(this.colorPickerOutline);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.colorPickerSymbol);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.numericUpDownYOffset);
			this.Controls.Add(this.numericUpDownXOffset);
			this.Controls.Add(this.numericUpDownSMSize);
			this.Controls.Add(this.comboBoxSMStyle);
			this.Controls.Add(this.checkBoxUseOutline);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "CtrlSimpleMarker";
			this.Size = new System.Drawing.Size(486, 346);
			this.Load += new System.EventHandler(this.CtrlSimpleMarker_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSMSize)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownXOffset)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownYOffset)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownOutlineSize)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox checkBoxUseOutline;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox comboBoxSMStyle;
		private System.Windows.Forms.NumericUpDown numericUpDownSMSize;
		private System.Windows.Forms.NumericUpDown numericUpDownXOffset;
		private System.Windows.Forms.NumericUpDown numericUpDownYOffset;
		private System.Windows.Forms.NumericUpDown numericUpDownOutlineSize;
		private AG.COM.SDM.Utility.Controls.ColorPicker colorPickerSymbol;
		private AG.COM.SDM.Utility.Controls.ColorPicker colorPickerOutline;
	}
}
