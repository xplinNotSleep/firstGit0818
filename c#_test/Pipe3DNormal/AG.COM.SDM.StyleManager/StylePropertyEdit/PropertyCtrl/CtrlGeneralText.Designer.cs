namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	partial class CtrlGeneralText
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrlGeneralText));
			this.comboBoxSize = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.colorPickerSymbol = new AG.COM.SDM.Utility.Controls.ColorPicker();
			this.label4 = new System.Windows.Forms.Label();
			this.numericUpDownYOffset = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownXOffset = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButtonVBottom = new System.Windows.Forms.RadioButton();
			this.radioButtonVBaseline = new System.Windows.Forms.RadioButton();
			this.radioButtonVCenter = new System.Windows.Forms.RadioButton();
			this.radioButtonVTop = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.radioButtonHFull = new System.Windows.Forms.RadioButton();
			this.radioButtonHCenter = new System.Windows.Forms.RadioButton();
			this.radioButtonHRight = new System.Windows.Forms.RadioButton();
			this.radioButtonHLeft = new System.Windows.Forms.RadioButton();
			this.checkBoxRToL = new System.Windows.Forms.CheckBox();
			this.numericUpDownAngle = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.checkBoxBold = new System.Windows.Forms.CheckBox();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.checkBoxItalic = new System.Windows.Forms.CheckBox();
			this.checkBoxUnderline = new System.Windows.Forms.CheckBox();
			this.checkBoxStrikeout = new System.Windows.Forms.CheckBox();
			this.comboBoxFont = new CtrlComboBoxFont();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownYOffset)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownXOffset)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngle)).BeginInit();
			this.SuspendLayout();
			// 
			// comboBoxSize
			// 
			this.comboBoxSize.FormattingEnabled = true;
			this.comboBoxSize.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28",
            "36",
            "48",
            "72"});
			this.comboBoxSize.Location = new System.Drawing.Point(345, 19);
			this.comboBoxSize.Name = "comboBoxSize";
			this.comboBoxSize.Size = new System.Drawing.Size(86, 20);
			this.comboBoxSize.TabIndex = 1;
			this.comboBoxSize.TextChanged += new System.EventHandler(this.comboBoxSize_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(17, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "字体：";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(292, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "大小：";
			// 
			// colorPickerSymbol
			// 
			this.colorPickerSymbol.BackColor = System.Drawing.SystemColors.Window;
			this.colorPickerSymbol.Context = null;
			this.colorPickerSymbol.ForeColor = System.Drawing.SystemColors.WindowText;
			this.colorPickerSymbol.Location = new System.Drawing.Point(64, 83);
			this.colorPickerSymbol.Name = "colorPickerSymbol";
			this.colorPickerSymbol.ReadOnly = false;
			this.colorPickerSymbol.Size = new System.Drawing.Size(215, 21);
			this.colorPickerSymbol.TabIndex = 6;
			this.colorPickerSymbol.Text = "White";
			this.colorPickerSymbol.Value = System.Drawing.Color.White;
			this.colorPickerSymbol.ValueChanged += new System.EventHandler(this.colorPickerSymbol_ValueChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(17, 89);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 12);
			this.label4.TabIndex = 5;
			this.label4.Text = "颜色：";
			// 
			// numericUpDownYOffset
			// 
			this.numericUpDownYOffset.Location = new System.Drawing.Point(345, 123);
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
			this.numericUpDownYOffset.Size = new System.Drawing.Size(89, 21);
			this.numericUpDownYOffset.TabIndex = 16;
			this.numericUpDownYOffset.ValueChanged += new System.EventHandler(this.numericUpDownYOffset_ValueChanged);
			// 
			// numericUpDownXOffset
			// 
			this.numericUpDownXOffset.Location = new System.Drawing.Point(345, 85);
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
			this.numericUpDownXOffset.Size = new System.Drawing.Size(89, 21);
			this.numericUpDownXOffset.TabIndex = 17;
			this.numericUpDownXOffset.ValueChanged += new System.EventHandler(this.numericUpDownXOffset_ValueChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(292, 127);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(47, 12);
			this.label5.TabIndex = 15;
			this.label5.Text = "Y偏移：";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(292, 89);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(47, 12);
			this.label6.TabIndex = 14;
			this.label6.Text = "X偏移：";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButtonVBottom);
			this.groupBox1.Controls.Add(this.radioButtonVBaseline);
			this.groupBox1.Controls.Add(this.radioButtonVCenter);
			this.groupBox1.Controls.Add(this.radioButtonVTop);
			this.groupBox1.Location = new System.Drawing.Point(19, 155);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 113);
			this.groupBox1.TabIndex = 19;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "垂直对齐";
			// 
			// radioButtonVBottom
			// 
			this.radioButtonVBottom.AutoSize = true;
			this.radioButtonVBottom.Location = new System.Drawing.Point(16, 84);
			this.radioButtonVBottom.Name = "radioButtonVBottom";
			this.radioButtonVBottom.Size = new System.Drawing.Size(35, 16);
			this.radioButtonVBottom.TabIndex = 3;
			this.radioButtonVBottom.TabStop = true;
			this.radioButtonVBottom.Text = "下";
			this.radioButtonVBottom.UseVisualStyleBackColor = true;
			this.radioButtonVBottom.CheckedChanged += new System.EventHandler(this.VAlign_CheckedChanged);
			// 
			// radioButtonVBaseline
			// 
			this.radioButtonVBaseline.AutoSize = true;
			this.radioButtonVBaseline.Location = new System.Drawing.Point(16, 64);
			this.radioButtonVBaseline.Name = "radioButtonVBaseline";
			this.radioButtonVBaseline.Size = new System.Drawing.Size(59, 16);
			this.radioButtonVBaseline.TabIndex = 2;
			this.radioButtonVBaseline.TabStop = true;
			this.radioButtonVBaseline.Text = "基准线";
			this.radioButtonVBaseline.UseVisualStyleBackColor = true;
			this.radioButtonVBaseline.CheckedChanged += new System.EventHandler(this.VAlign_CheckedChanged);
			// 
			// radioButtonVCenter
			// 
			this.radioButtonVCenter.AutoSize = true;
			this.radioButtonVCenter.Location = new System.Drawing.Point(16, 42);
			this.radioButtonVCenter.Name = "radioButtonVCenter";
			this.radioButtonVCenter.Size = new System.Drawing.Size(47, 16);
			this.radioButtonVCenter.TabIndex = 1;
			this.radioButtonVCenter.TabStop = true;
			this.radioButtonVCenter.Text = "居中";
			this.radioButtonVCenter.UseVisualStyleBackColor = true;
			this.radioButtonVCenter.CheckedChanged += new System.EventHandler(this.VAlign_CheckedChanged);
			// 
			// radioButtonVTop
			// 
			this.radioButtonVTop.AutoSize = true;
			this.radioButtonVTop.Location = new System.Drawing.Point(16, 20);
			this.radioButtonVTop.Name = "radioButtonVTop";
			this.radioButtonVTop.Size = new System.Drawing.Size(35, 16);
			this.radioButtonVTop.TabIndex = 0;
			this.radioButtonVTop.TabStop = true;
			this.radioButtonVTop.Text = "上";
			this.radioButtonVTop.UseVisualStyleBackColor = true;
			this.radioButtonVTop.CheckedChanged += new System.EventHandler(this.VAlign_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.radioButtonHFull);
			this.groupBox2.Controls.Add(this.radioButtonHCenter);
			this.groupBox2.Controls.Add(this.radioButtonHRight);
			this.groupBox2.Controls.Add(this.radioButtonHLeft);
			this.groupBox2.Location = new System.Drawing.Point(234, 155);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(200, 113);
			this.groupBox2.TabIndex = 20;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "水平对齐";
			// 
			// radioButtonHFull
			// 
			this.radioButtonHFull.AutoSize = true;
			this.radioButtonHFull.Location = new System.Drawing.Point(10, 84);
			this.radioButtonHFull.Name = "radioButtonHFull";
			this.radioButtonHFull.Size = new System.Drawing.Size(47, 16);
			this.radioButtonHFull.TabIndex = 7;
			this.radioButtonHFull.TabStop = true;
			this.radioButtonHFull.Text = "全部";
			this.radioButtonHFull.UseVisualStyleBackColor = true;
			this.radioButtonHFull.CheckedChanged += new System.EventHandler(this.HAlign_CheckedChanged);
			// 
			// radioButtonHCenter
			// 
			this.radioButtonHCenter.AutoSize = true;
			this.radioButtonHCenter.Location = new System.Drawing.Point(10, 64);
			this.radioButtonHCenter.Name = "radioButtonHCenter";
			this.radioButtonHCenter.Size = new System.Drawing.Size(47, 16);
			this.radioButtonHCenter.TabIndex = 6;
			this.radioButtonHCenter.TabStop = true;
			this.radioButtonHCenter.Text = "居中";
			this.radioButtonHCenter.UseVisualStyleBackColor = true;
			this.radioButtonHCenter.CheckedChanged += new System.EventHandler(this.HAlign_CheckedChanged);
			// 
			// radioButtonHRight
			// 
			this.radioButtonHRight.AutoSize = true;
			this.radioButtonHRight.Location = new System.Drawing.Point(10, 42);
			this.radioButtonHRight.Name = "radioButtonHRight";
			this.radioButtonHRight.Size = new System.Drawing.Size(35, 16);
			this.radioButtonHRight.TabIndex = 5;
			this.radioButtonHRight.TabStop = true;
			this.radioButtonHRight.Text = "右";
			this.radioButtonHRight.UseVisualStyleBackColor = true;
			this.radioButtonHRight.CheckedChanged += new System.EventHandler(this.HAlign_CheckedChanged);
			// 
			// radioButtonHLeft
			// 
			this.radioButtonHLeft.AutoSize = true;
			this.radioButtonHLeft.Location = new System.Drawing.Point(10, 20);
			this.radioButtonHLeft.Name = "radioButtonHLeft";
			this.radioButtonHLeft.Size = new System.Drawing.Size(35, 16);
			this.radioButtonHLeft.TabIndex = 4;
			this.radioButtonHLeft.TabStop = true;
			this.radioButtonHLeft.Text = "左";
			this.radioButtonHLeft.UseVisualStyleBackColor = true;
			this.radioButtonHLeft.CheckedChanged += new System.EventHandler(this.HAlign_CheckedChanged);
			// 
			// checkBoxRToL
			// 
			this.checkBoxRToL.AutoSize = true;
			this.checkBoxRToL.Location = new System.Drawing.Point(19, 274);
			this.checkBoxRToL.Name = "checkBoxRToL";
			this.checkBoxRToL.Size = new System.Drawing.Size(252, 16);
			this.checkBoxRToL.TabIndex = 21;
			this.checkBoxRToL.Text = "从右至左(仅用于希来伯文和阿拉伯文字体)";
			this.checkBoxRToL.UseVisualStyleBackColor = true;
			this.checkBoxRToL.CheckedChanged += new System.EventHandler(this.checkBoxRToL_CheckedChanged);
			// 
			// numericUpDownAngle
			// 
			this.numericUpDownAngle.Location = new System.Drawing.Point(64, 121);
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
			this.numericUpDownAngle.Size = new System.Drawing.Size(215, 21);
			this.numericUpDownAngle.TabIndex = 35;
			this.numericUpDownAngle.ValueChanged += new System.EventHandler(this.numericUpDownAngle_ValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(17, 123);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 12);
			this.label3.TabIndex = 34;
			this.label3.Text = "角度：";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(17, 55);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(41, 12);
			this.label7.TabIndex = 36;
			this.label7.Text = "样式：";
			// 
			// checkBoxBold
			// 
			this.checkBoxBold.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBoxBold.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.checkBoxBold.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.checkBoxBold.ImageIndex = 0;
			this.checkBoxBold.ImageList = this.imageList1;
			this.checkBoxBold.Location = new System.Drawing.Point(64, 48);
			this.checkBoxBold.Name = "checkBoxBold";
			this.checkBoxBold.Size = new System.Drawing.Size(29, 24);
			this.checkBoxBold.TabIndex = 37;
			this.checkBoxBold.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.checkBoxBold.UseVisualStyleBackColor = true;
			this.checkBoxBold.CheckedChanged += new System.EventHandler(this.FontStyle_CheckedChanged);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "btnBold.Image.png");
			this.imageList1.Images.SetKeyName(1, "btnItalic.Image.png");
			this.imageList1.Images.SetKeyName(2, "btnUnderline.Image.png");
			this.imageList1.Images.SetKeyName(3, "btnStrikeThrough.Image.png");
			// 
			// checkBoxItalic
			// 
			this.checkBoxItalic.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBoxItalic.Font = new System.Drawing.Font("宋体", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.checkBoxItalic.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.checkBoxItalic.ImageIndex = 1;
			this.checkBoxItalic.ImageList = this.imageList1;
			this.checkBoxItalic.Location = new System.Drawing.Point(99, 48);
			this.checkBoxItalic.Name = "checkBoxItalic";
			this.checkBoxItalic.Size = new System.Drawing.Size(29, 24);
			this.checkBoxItalic.TabIndex = 38;
			this.checkBoxItalic.UseVisualStyleBackColor = true;
			this.checkBoxItalic.CheckedChanged += new System.EventHandler(this.FontStyle_CheckedChanged);
			// 
			// checkBoxUnderline
			// 
			this.checkBoxUnderline.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBoxUnderline.Font = new System.Drawing.Font("宋体", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.checkBoxUnderline.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.checkBoxUnderline.ImageIndex = 2;
			this.checkBoxUnderline.ImageList = this.imageList1;
			this.checkBoxUnderline.Location = new System.Drawing.Point(134, 48);
			this.checkBoxUnderline.Name = "checkBoxUnderline";
			this.checkBoxUnderline.Size = new System.Drawing.Size(29, 24);
			this.checkBoxUnderline.TabIndex = 39;
			this.checkBoxUnderline.UseVisualStyleBackColor = true;
			this.checkBoxUnderline.CheckedChanged += new System.EventHandler(this.FontStyle_CheckedChanged);
			// 
			// checkBoxStrikeout
			// 
			this.checkBoxStrikeout.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBoxStrikeout.Font = new System.Drawing.Font("宋体", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Strikeout))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.checkBoxStrikeout.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.checkBoxStrikeout.ImageIndex = 3;
			this.checkBoxStrikeout.ImageList = this.imageList1;
			this.checkBoxStrikeout.Location = new System.Drawing.Point(169, 48);
			this.checkBoxStrikeout.Name = "checkBoxStrikeout";
			this.checkBoxStrikeout.Size = new System.Drawing.Size(29, 24);
			this.checkBoxStrikeout.TabIndex = 40;
			this.checkBoxStrikeout.UseVisualStyleBackColor = true;
			this.checkBoxStrikeout.CheckedChanged += new System.EventHandler(this.FontStyle_CheckedChanged);
			// 
			// comboBoxFont
			// 
			this.comboBoxFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFont.Location = new System.Drawing.Point(64, 19);
			this.comboBoxFont.MaximumSize = new System.Drawing.Size(1024, 20);
			this.comboBoxFont.MinimumSize = new System.Drawing.Size(40, 20);
			this.comboBoxFont.Name = "comboBoxFont";
			this.comboBoxFont.Size = new System.Drawing.Size(215, 20);
			this.comboBoxFont.TabIndex = 41;
			// 
			// CtrlGeneralText
			// 
			this.AccessibleName = "常规";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.comboBoxFont);
			this.Controls.Add(this.checkBoxStrikeout);
			this.Controls.Add(this.checkBoxUnderline);
			this.Controls.Add(this.checkBoxItalic);
			this.Controls.Add(this.checkBoxBold);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.numericUpDownAngle);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.checkBoxRToL);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.numericUpDownYOffset);
			this.Controls.Add(this.numericUpDownXOffset);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.colorPickerSymbol);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBoxSize);
			this.Name = "CtrlGeneralText";
			this.Size = new System.Drawing.Size(486, 346);
			this.Load += new System.EventHandler(this.CtrlGeneralText_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownYOffset)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownXOffset)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngle)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBoxSize;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private AG.COM.SDM.Utility.Controls.ColorPicker colorPickerSymbol;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown numericUpDownYOffset;
		private System.Windows.Forms.NumericUpDown numericUpDownXOffset;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox checkBoxRToL;
		private System.Windows.Forms.RadioButton radioButtonVBottom;
		private System.Windows.Forms.RadioButton radioButtonVBaseline;
		private System.Windows.Forms.RadioButton radioButtonVCenter;
		private System.Windows.Forms.RadioButton radioButtonVTop;
		private System.Windows.Forms.RadioButton radioButtonHFull;
		private System.Windows.Forms.RadioButton radioButtonHCenter;
		private System.Windows.Forms.RadioButton radioButtonHRight;
		private System.Windows.Forms.RadioButton radioButtonHLeft;
		private System.Windows.Forms.NumericUpDown numericUpDownAngle;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.CheckBox checkBoxBold;
		private System.Windows.Forms.CheckBox checkBoxItalic;
		private System.Windows.Forms.CheckBox checkBoxUnderline;
		private System.Windows.Forms.CheckBox checkBoxStrikeout;
		private CtrlComboBoxFont comboBoxFont;
		private System.Windows.Forms.ImageList imageList1;
	}
}
