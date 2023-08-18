namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	partial class CtrlPicture
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
			this.btnSelPicture = new System.Windows.Forms.Button();
			this.textBoxPicture = new System.Windows.Forms.TextBox();
			this.numericUpDownAngleAndWidth = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.numericUpDownYOffset = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownXOffset = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.numericUpDownSize = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.colorPickerForeground = new AG.COM.SDM.Utility.Controls.ColorPicker();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.colorPickerBackground = new AG.COM.SDM.Utility.Controls.ColorPicker();
			this.label6 = new System.Windows.Forms.Label();
			this.colorPickerTransparent = new AG.COM.SDM.Utility.Controls.ColorPicker();
			this.checkBoxSwapColor = new System.Windows.Forms.CheckBox();
			this.btnSelOutline = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngleAndWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownYOffset)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownXOffset)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSize)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnSelPicture
			// 
			this.btnSelPicture.Location = new System.Drawing.Point(15, 12);
			this.btnSelPicture.Name = "btnSelPicture";
			this.btnSelPicture.Size = new System.Drawing.Size(75, 23);
			this.btnSelPicture.TabIndex = 0;
			this.btnSelPicture.Text = "图片…";
			this.btnSelPicture.UseVisualStyleBackColor = true;
			this.btnSelPicture.Click += new System.EventHandler(this.btnSelPicture_Click);
			// 
			// textBoxPicture
			// 
			this.textBoxPicture.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBoxPicture.Location = new System.Drawing.Point(96, 14);
			this.textBoxPicture.Name = "textBoxPicture";
			this.textBoxPicture.ReadOnly = true;
			this.textBoxPicture.Size = new System.Drawing.Size(376, 14);
			this.textBoxPicture.TabIndex = 1;
			// 
			// numericUpDownAngleAndWidth
			// 
			this.numericUpDownAngleAndWidth.DecimalPlaces = 4;
			this.numericUpDownAngleAndWidth.Location = new System.Drawing.Point(50, 8);
			this.numericUpDownAngleAndWidth.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
			this.numericUpDownAngleAndWidth.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
			this.numericUpDownAngleAndWidth.Name = "numericUpDownAngleAndWidth";
			this.numericUpDownAngleAndWidth.Size = new System.Drawing.Size(100, 21);
			this.numericUpDownAngleAndWidth.TabIndex = 24;
			this.numericUpDownAngleAndWidth.ValueChanged += new System.EventHandler(this.numericUpDownAngleAndWidth_ValueChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(3, 10);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(41, 12);
			this.label7.TabIndex = 23;
			this.label7.Text = "角度：";
			// 
			// numericUpDownYOffset
			// 
			this.numericUpDownYOffset.DecimalPlaces = 4;
			this.numericUpDownYOffset.Location = new System.Drawing.Point(50, 80);
			this.numericUpDownYOffset.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDownYOffset.Name = "numericUpDownYOffset";
			this.numericUpDownYOffset.Size = new System.Drawing.Size(100, 21);
			this.numericUpDownYOffset.TabIndex = 25;
			this.numericUpDownYOffset.ValueChanged += new System.EventHandler(this.numericUpDownYOffset_ValueChanged);
			// 
			// numericUpDownXOffset
			// 
			this.numericUpDownXOffset.DecimalPlaces = 4;
			this.numericUpDownXOffset.Location = new System.Drawing.Point(50, 44);
			this.numericUpDownXOffset.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDownXOffset.Name = "numericUpDownXOffset";
			this.numericUpDownXOffset.Size = new System.Drawing.Size(100, 21);
			this.numericUpDownXOffset.TabIndex = 26;
			this.numericUpDownXOffset.ValueChanged += new System.EventHandler(this.numericUpDownXOffset_ValueChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 82);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(47, 12);
			this.label5.TabIndex = 22;
			this.label5.Text = "Y比例：";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 46);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(47, 12);
			this.label4.TabIndex = 21;
			this.label4.Text = "X比例：";
			// 
			// numericUpDownSize
			// 
			this.numericUpDownSize.DecimalPlaces = 4;
			this.numericUpDownSize.Location = new System.Drawing.Point(50, 11);
			this.numericUpDownSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDownSize.Name = "numericUpDownSize";
			this.numericUpDownSize.Size = new System.Drawing.Size(100, 21);
			this.numericUpDownSize.TabIndex = 28;
			this.numericUpDownSize.ValueChanged += new System.EventHandler(this.numericUpDownSize_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 27;
			this.label1.Text = "大小：";
			// 
			// colorPickerForeground
			// 
			this.colorPickerForeground.BackColor = System.Drawing.SystemColors.Window;
			this.colorPickerForeground.Context = null;
			this.colorPickerForeground.Enabled = false;
			this.colorPickerForeground.ForeColor = System.Drawing.SystemColors.WindowText;
			this.colorPickerForeground.Location = new System.Drawing.Point(212, 8);
			this.colorPickerForeground.Name = "colorPickerForeground";
			this.colorPickerForeground.ReadOnly = false;
			this.colorPickerForeground.Size = new System.Drawing.Size(145, 21);
			this.colorPickerForeground.TabIndex = 30;
			this.colorPickerForeground.Text = "White";
			this.colorPickerForeground.Value = System.Drawing.Color.White;
			this.colorPickerForeground.ValueChanged += new System.EventHandler(this.colorPickerForeground_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Enabled = false;
			this.label2.Location = new System.Drawing.Point(160, 11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 12);
			this.label2.TabIndex = 29;
			this.label2.Text = "前景色：";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(160, 47);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 12);
			this.label3.TabIndex = 29;
			this.label3.Text = "背景色：";
			// 
			// colorPickerBackground
			// 
			this.colorPickerBackground.BackColor = System.Drawing.SystemColors.Window;
			this.colorPickerBackground.Context = null;
			this.colorPickerBackground.ForeColor = System.Drawing.SystemColors.WindowText;
			this.colorPickerBackground.Location = new System.Drawing.Point(212, 44);
			this.colorPickerBackground.Name = "colorPickerBackground";
			this.colorPickerBackground.ReadOnly = false;
			this.colorPickerBackground.Size = new System.Drawing.Size(145, 21);
			this.colorPickerBackground.TabIndex = 30;
			this.colorPickerBackground.Text = "White";
			this.colorPickerBackground.Value = System.Drawing.Color.White;
			this.colorPickerBackground.ValueChanged += new System.EventHandler(this.colorPickerBackground_ValueChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(160, 83);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(53, 12);
			this.label6.TabIndex = 29;
			this.label6.Text = "透明色：";
			// 
			// colorPickerTransparent
			// 
			this.colorPickerTransparent.BackColor = System.Drawing.SystemColors.Window;
			this.colorPickerTransparent.Context = null;
			this.colorPickerTransparent.ForeColor = System.Drawing.SystemColors.WindowText;
			this.colorPickerTransparent.Location = new System.Drawing.Point(212, 80);
			this.colorPickerTransparent.Name = "colorPickerTransparent";
			this.colorPickerTransparent.ReadOnly = false;
			this.colorPickerTransparent.Size = new System.Drawing.Size(145, 21);
			this.colorPickerTransparent.TabIndex = 30;
			this.colorPickerTransparent.Text = "White";
			this.colorPickerTransparent.Value = System.Drawing.Color.White;
			this.colorPickerTransparent.ValueChanged += new System.EventHandler(this.colorPickerTransparent_ValueChanged);
			// 
			// checkBoxSwapColor
			// 
			this.checkBoxSwapColor.AutoSize = true;
			this.checkBoxSwapColor.Enabled = false;
			this.checkBoxSwapColor.Location = new System.Drawing.Point(5, 118);
			this.checkBoxSwapColor.Name = "checkBoxSwapColor";
			this.checkBoxSwapColor.Size = new System.Drawing.Size(132, 16);
			this.checkBoxSwapColor.TabIndex = 31;
			this.checkBoxSwapColor.Text = "交换前景色和背景色";
			this.checkBoxSwapColor.UseVisualStyleBackColor = true;
			this.checkBoxSwapColor.CheckedChanged += new System.EventHandler(this.checkBoxSwapColor_CheckedChanged);
			// 
			// btnSelOutline
			// 
			this.btnSelOutline.Location = new System.Drawing.Point(363, 80);
			this.btnSelOutline.Name = "btnSelOutline";
			this.btnSelOutline.Size = new System.Drawing.Size(75, 23);
			this.btnSelOutline.TabIndex = 32;
			this.btnSelOutline.Text = "轮廓线…";
			this.btnSelOutline.UseVisualStyleBackColor = true;
			this.btnSelOutline.Click += new System.EventHandler(this.btnSelOutline_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 41);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(457, 268);
			this.tableLayoutPanel1.TabIndex = 33;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.numericUpDownSize);
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(451, 32);
			this.panel1.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.label7);
			this.panel2.Controls.Add(this.label4);
			this.panel2.Controls.Add(this.btnSelOutline);
			this.panel2.Controls.Add(this.label5);
			this.panel2.Controls.Add(this.checkBoxSwapColor);
			this.panel2.Controls.Add(this.numericUpDownXOffset);
			this.panel2.Controls.Add(this.colorPickerTransparent);
			this.panel2.Controls.Add(this.numericUpDownYOffset);
			this.panel2.Controls.Add(this.label6);
			this.panel2.Controls.Add(this.numericUpDownAngleAndWidth);
			this.panel2.Controls.Add(this.colorPickerBackground);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.label3);
			this.panel2.Controls.Add(this.colorPickerForeground);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(3, 41);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(451, 224);
			this.panel2.TabIndex = 1;
			// 
			// CtrlPicture
			// 
			this.AccessibleName = "图片";
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.textBoxPicture);
			this.Controls.Add(this.btnSelPicture);
			this.Name = "CtrlPicture";
			this.Size = new System.Drawing.Size(486, 346);
			this.Load += new System.EventHandler(this.CtrlPicture_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngleAndWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownYOffset)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownXOffset)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSize)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnSelPicture;
		private System.Windows.Forms.TextBox textBoxPicture;
		private System.Windows.Forms.NumericUpDown numericUpDownAngleAndWidth;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown numericUpDownYOffset;
		private System.Windows.Forms.NumericUpDown numericUpDownXOffset;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown numericUpDownSize;
		private System.Windows.Forms.Label label1;
		private AG.COM.SDM.Utility.Controls.ColorPicker colorPickerForeground;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private AG.COM.SDM.Utility.Controls.ColorPicker colorPickerBackground;
		private System.Windows.Forms.Label label6;
		private AG.COM.SDM.Utility.Controls.ColorPicker colorPickerTransparent;
		private System.Windows.Forms.CheckBox checkBoxSwapColor;
		private System.Windows.Forms.Button btnSelOutline;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
	}
}
