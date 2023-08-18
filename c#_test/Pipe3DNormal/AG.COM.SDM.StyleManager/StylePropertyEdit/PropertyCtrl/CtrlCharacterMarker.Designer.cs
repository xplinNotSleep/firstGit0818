namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	partial class CtrlCharacterMarker
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
			this.fontListView1 = new CommonLibrary.Control.FontListView();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxUnicode = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.comboBoxSize = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.colorPickerSymbol = new AG.COM.SDM.Utility.Controls.ColorPicker();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.numericUpDownYOffset = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.numericUpDownXOffset = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.numericUpDownAngle = new System.Windows.Forms.NumericUpDown();
			this.comboBoxFont = new CtrlComboBoxFont();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownYOffset)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownXOffset)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngle)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "字体：";
			// 
			// fontListView1
			// 
			this.fontListView1.AutoScroll = true;
			this.fontListView1.DrawFont = null;
			this.fontListView1.Location = new System.Drawing.Point(17, 43);
			this.fontListView1.Name = "fontListView1";
			this.fontListView1.Size = new System.Drawing.Size(292, 241);
			this.fontListView1.TabIndex = 2;
			this.fontListView1.SelectFontChanged += new CommonLibrary.Control.SelectFontEventHanlder(this.fontListView1_SelectFontChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15, 293);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "Unicode:";
			// 
			// textBoxUnicode
			// 
			this.textBoxUnicode.Location = new System.Drawing.Point(74, 290);
			this.textBoxUnicode.Name = "textBoxUnicode";
			this.textBoxUnicode.Size = new System.Drawing.Size(100, 21);
			this.textBoxUnicode.TabIndex = 4;
			this.textBoxUnicode.TextChanged += new System.EventHandler(this.textBoxUnicode_TextChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(313, 20);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 12);
			this.label3.TabIndex = 5;
			this.label3.Text = "大小：";
			// 
			// comboBoxSize
			// 
			this.comboBoxSize.FormattingEnabled = true;
			this.comboBoxSize.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
			this.comboBoxSize.Location = new System.Drawing.Point(357, 17);
			this.comboBoxSize.Name = "comboBoxSize";
			this.comboBoxSize.Size = new System.Drawing.Size(118, 20);
			this.comboBoxSize.TabIndex = 6;
			this.comboBoxSize.TextChanged += new System.EventHandler(this.comboBoxSize_TextChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(313, 43);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 12);
			this.label4.TabIndex = 7;
			this.label4.Text = "角度：";
			// 
			// colorPickerSymbol
			// 
			this.colorPickerSymbol.BackColor = System.Drawing.SystemColors.Window;
			this.colorPickerSymbol.Context = null;
			this.colorPickerSymbol.ForeColor = System.Drawing.SystemColors.WindowText;
			this.colorPickerSymbol.Location = new System.Drawing.Point(357, 69);
			this.colorPickerSymbol.Name = "colorPickerSymbol";
			this.colorPickerSymbol.ReadOnly = false;
			this.colorPickerSymbol.Size = new System.Drawing.Size(118, 21);
			this.colorPickerSymbol.TabIndex = 12;
			this.colorPickerSymbol.Text = "White";
			this.colorPickerSymbol.Value = System.Drawing.Color.White;
			this.colorPickerSymbol.ValueChanged += new System.EventHandler(this.colorPickerSymbol_ValueChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(313, 71);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 12);
			this.label5.TabIndex = 11;
			this.label5.Text = "颜色：";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.numericUpDownYOffset);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.numericUpDownXOffset);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Location = new System.Drawing.Point(315, 96);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(160, 120);
			this.groupBox2.TabIndex = 13;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "偏移量";
			// 
			// numericUpDownYOffset
			// 
			this.numericUpDownYOffset.DecimalPlaces = 4;
			this.numericUpDownYOffset.Location = new System.Drawing.Point(42, 71);
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
			this.numericUpDownYOffset.Size = new System.Drawing.Size(110, 21);
			this.numericUpDownYOffset.TabIndex = 16;
			this.numericUpDownYOffset.ValueChanged += new System.EventHandler(this.numericUpDownYOffset_ValueChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(13, 30);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(23, 12);
			this.label7.TabIndex = 14;
			this.label7.Text = "X：";
			// 
			// numericUpDownXOffset
			// 
			this.numericUpDownXOffset.DecimalPlaces = 4;
			this.numericUpDownXOffset.Location = new System.Drawing.Point(42, 28);
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
			this.numericUpDownXOffset.Size = new System.Drawing.Size(110, 21);
			this.numericUpDownXOffset.TabIndex = 17;
			this.numericUpDownXOffset.ValueChanged += new System.EventHandler(this.numericUpDownXOffset_ValueChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(13, 73);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(23, 12);
			this.label6.TabIndex = 15;
			this.label6.Text = "Y：";
			// 
			// numericUpDownAngle
			// 
			this.numericUpDownAngle.DecimalPlaces = 4;
			this.numericUpDownAngle.Location = new System.Drawing.Point(357, 41);
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
			this.numericUpDownAngle.Size = new System.Drawing.Size(118, 21);
			this.numericUpDownAngle.TabIndex = 19;
			this.numericUpDownAngle.ValueChanged += new System.EventHandler(this.numericUpDownAngle_ValueChanged);
			// 
			// comboBoxFont
			// 
			this.comboBoxFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFont.Location = new System.Drawing.Point(52, 17);
			this.comboBoxFont.MaximumSize = new System.Drawing.Size(1024, 20);
			this.comboBoxFont.MinimumSize = new System.Drawing.Size(40, 20);
			this.comboBoxFont.Name = "comboBoxFont";
			this.comboBoxFont.Size = new System.Drawing.Size(255, 20);
			this.comboBoxFont.TabIndex = 42;
			// 
			// CtrlCharacterMarker
			// 
			this.AccessibleName = "字符标记";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.fontListView1);
			this.Controls.Add(this.comboBoxFont);
			this.Controls.Add(this.numericUpDownAngle);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.colorPickerSymbol);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.comboBoxSize);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxUnicode);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "CtrlCharacterMarker";
			this.Size = new System.Drawing.Size(486, 346);
			this.Load += new System.EventHandler(this.CtrlCharacterMarker_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownYOffset)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownXOffset)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngle)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxUnicode;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboBoxSize;
		private System.Windows.Forms.Label label4;
		private AG.COM.SDM.Utility.Controls.ColorPicker colorPickerSymbol;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.NumericUpDown numericUpDownYOffset;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown numericUpDownXOffset;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown numericUpDownAngle;
		private CommonLibrary.Control.FontListView fontListView1;
		private CtrlComboBoxFont comboBoxFont;
	}
}
