namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	partial class CtrlLineCallout
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrlLineCallout));
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDownLeader = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDownGap = new System.Windows.Forms.NumericUpDown();
			this.checkBoxLeader = new System.Windows.Forms.CheckBox();
			this.checkBoxAccentBar = new System.Windows.Forms.CheckBox();
			this.checkBoxBorder = new System.Windows.Forms.CheckBox();
			this.btnSymbolSel = new System.Windows.Forms.Button();
			this.btnSymbolSel2 = new System.Windows.Forms.Button();
			this.btnSymbolSel3 = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButtonStyle7 = new System.Windows.Forms.RadioButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.radioButtonStyle6 = new System.Windows.Forms.RadioButton();
			this.radioButtonStyle5 = new System.Windows.Forms.RadioButton();
			this.radioButtonStyle4 = new System.Windows.Forms.RadioButton();
			this.radioButtonStyle3 = new System.Windows.Forms.RadioButton();
			this.radioButtonStyle2 = new System.Windows.Forms.RadioButton();
			this.radioButtonStyle1 = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.numericUpDownDown = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownUp = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownRight = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownLeft = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeader)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownGap)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownUp)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownRight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeft)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(22, 29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "间隙：";
			// 
			// numericUpDownLeader
			// 
			this.numericUpDownLeader.Location = new System.Drawing.Point(93, 53);
			this.numericUpDownLeader.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDownLeader.Name = "numericUpDownLeader";
			this.numericUpDownLeader.Size = new System.Drawing.Size(130, 21);
			this.numericUpDownLeader.TabIndex = 32;
			this.numericUpDownLeader.ValueChanged += new System.EventHandler(this.numericUpDownLeader_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(22, 57);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 31;
			this.label2.Text = "引导容限：";
			// 
			// numericUpDownGap
			// 
			this.numericUpDownGap.Location = new System.Drawing.Point(93, 26);
			this.numericUpDownGap.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDownGap.Name = "numericUpDownGap";
			this.numericUpDownGap.Size = new System.Drawing.Size(130, 21);
			this.numericUpDownGap.TabIndex = 32;
			this.numericUpDownGap.ValueChanged += new System.EventHandler(this.numericUpDownGap_ValueChanged);
			// 
			// checkBoxLeader
			// 
			this.checkBoxLeader.AutoSize = true;
			this.checkBoxLeader.Location = new System.Drawing.Point(24, 89);
			this.checkBoxLeader.Name = "checkBoxLeader";
			this.checkBoxLeader.Size = new System.Drawing.Size(60, 16);
			this.checkBoxLeader.TabIndex = 33;
			this.checkBoxLeader.Text = "引导线";
			this.checkBoxLeader.UseVisualStyleBackColor = true;
			this.checkBoxLeader.CheckedChanged += new System.EventHandler(this.checkBoxLeader_CheckedChanged);
			// 
			// checkBoxAccentBar
			// 
			this.checkBoxAccentBar.AutoSize = true;
			this.checkBoxAccentBar.Location = new System.Drawing.Point(152, 89);
			this.checkBoxAccentBar.Name = "checkBoxAccentBar";
			this.checkBoxAccentBar.Size = new System.Drawing.Size(60, 16);
			this.checkBoxAccentBar.TabIndex = 34;
			this.checkBoxAccentBar.Text = "强调线";
			this.checkBoxAccentBar.UseVisualStyleBackColor = true;
			this.checkBoxAccentBar.CheckedChanged += new System.EventHandler(this.checkBoxAccentBar_CheckedChanged);
			// 
			// checkBoxBorder
			// 
			this.checkBoxBorder.AutoSize = true;
			this.checkBoxBorder.Location = new System.Drawing.Point(280, 89);
			this.checkBoxBorder.Name = "checkBoxBorder";
			this.checkBoxBorder.Size = new System.Drawing.Size(60, 16);
			this.checkBoxBorder.TabIndex = 35;
			this.checkBoxBorder.Text = "边界线";
			this.checkBoxBorder.UseVisualStyleBackColor = true;
			this.checkBoxBorder.CheckedChanged += new System.EventHandler(this.checkBoxBorder_CheckedChanged);
			// 
			// btnSymbolSel
			// 
			this.btnSymbolSel.Location = new System.Drawing.Point(24, 111);
			this.btnSymbolSel.Name = "btnSymbolSel";
			this.btnSymbolSel.Size = new System.Drawing.Size(85, 23);
			this.btnSymbolSel.TabIndex = 36;
			this.btnSymbolSel.Text = "符号…";
			this.btnSymbolSel.UseVisualStyleBackColor = true;
			this.btnSymbolSel.Click += new System.EventHandler(this.btnSymbolSel_Click);
			// 
			// btnSymbolSel2
			// 
			this.btnSymbolSel2.Location = new System.Drawing.Point(152, 111);
			this.btnSymbolSel2.Name = "btnSymbolSel2";
			this.btnSymbolSel2.Size = new System.Drawing.Size(85, 23);
			this.btnSymbolSel2.TabIndex = 36;
			this.btnSymbolSel2.Text = "符号…";
			this.btnSymbolSel2.UseVisualStyleBackColor = true;
			this.btnSymbolSel2.Click += new System.EventHandler(this.btnSymbolSel2_Click);
			// 
			// btnSymbolSel3
			// 
			this.btnSymbolSel3.Location = new System.Drawing.Point(280, 111);
			this.btnSymbolSel3.Name = "btnSymbolSel3";
			this.btnSymbolSel3.Size = new System.Drawing.Size(85, 23);
			this.btnSymbolSel3.TabIndex = 36;
			this.btnSymbolSel3.Text = "符号…";
			this.btnSymbolSel3.UseVisualStyleBackColor = true;
			this.btnSymbolSel3.Click += new System.EventHandler(this.btnSymbolSel3_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButtonStyle7);
			this.groupBox1.Controls.Add(this.radioButtonStyle6);
			this.groupBox1.Controls.Add(this.radioButtonStyle5);
			this.groupBox1.Controls.Add(this.radioButtonStyle4);
			this.groupBox1.Controls.Add(this.radioButtonStyle3);
			this.groupBox1.Controls.Add(this.radioButtonStyle2);
			this.groupBox1.Controls.Add(this.radioButtonStyle1);
			this.groupBox1.Location = new System.Drawing.Point(24, 150);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(341, 52);
			this.groupBox1.TabIndex = 37;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "样式";
			// 
			// radioButtonStyle7
			// 
			this.radioButtonStyle7.AutoSize = true;
			this.radioButtonStyle7.ImageIndex = 6;
			this.radioButtonStyle7.ImageList = this.imageList1;
			this.radioButtonStyle7.Location = new System.Drawing.Point(288, 20);
			this.radioButtonStyle7.Name = "radioButtonStyle7";
			this.radioButtonStyle7.Size = new System.Drawing.Size(34, 20);
			this.radioButtonStyle7.TabIndex = 0;
			this.radioButtonStyle7.TabStop = true;
			this.radioButtonStyle7.UseVisualStyleBackColor = true;
			this.radioButtonStyle7.CheckedChanged += new System.EventHandler(this.radioButtonStyle1_CheckedChanged);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
			this.imageList1.Images.SetKeyName(0, "001.bmp");
			this.imageList1.Images.SetKeyName(1, "002.bmp");
			this.imageList1.Images.SetKeyName(2, "003.bmp");
			this.imageList1.Images.SetKeyName(3, "004.bmp");
			this.imageList1.Images.SetKeyName(4, "005.bmp");
			this.imageList1.Images.SetKeyName(5, "006.bmp");
			this.imageList1.Images.SetKeyName(6, "007.bmp");
			// 
			// radioButtonStyle6
			// 
			this.radioButtonStyle6.AutoSize = true;
			this.radioButtonStyle6.ImageIndex = 5;
			this.radioButtonStyle6.ImageList = this.imageList1;
			this.radioButtonStyle6.Location = new System.Drawing.Point(241, 20);
			this.radioButtonStyle6.Name = "radioButtonStyle6";
			this.radioButtonStyle6.Size = new System.Drawing.Size(34, 20);
			this.radioButtonStyle6.TabIndex = 0;
			this.radioButtonStyle6.TabStop = true;
			this.radioButtonStyle6.UseVisualStyleBackColor = true;
			this.radioButtonStyle6.CheckedChanged += new System.EventHandler(this.radioButtonStyle1_CheckedChanged);
			// 
			// radioButtonStyle5
			// 
			this.radioButtonStyle5.AutoSize = true;
			this.radioButtonStyle5.ImageIndex = 4;
			this.radioButtonStyle5.ImageList = this.imageList1;
			this.radioButtonStyle5.Location = new System.Drawing.Point(194, 20);
			this.radioButtonStyle5.Name = "radioButtonStyle5";
			this.radioButtonStyle5.Size = new System.Drawing.Size(34, 20);
			this.radioButtonStyle5.TabIndex = 0;
			this.radioButtonStyle5.TabStop = true;
			this.radioButtonStyle5.UseVisualStyleBackColor = true;
			this.radioButtonStyle5.CheckedChanged += new System.EventHandler(this.radioButtonStyle1_CheckedChanged);
			// 
			// radioButtonStyle4
			// 
			this.radioButtonStyle4.AutoSize = true;
			this.radioButtonStyle4.ImageIndex = 3;
			this.radioButtonStyle4.ImageList = this.imageList1;
			this.radioButtonStyle4.Location = new System.Drawing.Point(147, 20);
			this.radioButtonStyle4.Name = "radioButtonStyle4";
			this.radioButtonStyle4.Size = new System.Drawing.Size(34, 20);
			this.radioButtonStyle4.TabIndex = 0;
			this.radioButtonStyle4.TabStop = true;
			this.radioButtonStyle4.UseVisualStyleBackColor = true;
			this.radioButtonStyle4.CheckedChanged += new System.EventHandler(this.radioButtonStyle1_CheckedChanged);
			// 
			// radioButtonStyle3
			// 
			this.radioButtonStyle3.AutoSize = true;
			this.radioButtonStyle3.ImageIndex = 2;
			this.radioButtonStyle3.ImageList = this.imageList1;
			this.radioButtonStyle3.Location = new System.Drawing.Point(100, 20);
			this.radioButtonStyle3.Name = "radioButtonStyle3";
			this.radioButtonStyle3.Size = new System.Drawing.Size(34, 20);
			this.radioButtonStyle3.TabIndex = 0;
			this.radioButtonStyle3.TabStop = true;
			this.radioButtonStyle3.UseVisualStyleBackColor = true;
			this.radioButtonStyle3.CheckedChanged += new System.EventHandler(this.radioButtonStyle1_CheckedChanged);
			// 
			// radioButtonStyle2
			// 
			this.radioButtonStyle2.AutoSize = true;
			this.radioButtonStyle2.ImageIndex = 1;
			this.radioButtonStyle2.ImageList = this.imageList1;
			this.radioButtonStyle2.Location = new System.Drawing.Point(53, 20);
			this.radioButtonStyle2.Name = "radioButtonStyle2";
			this.radioButtonStyle2.Size = new System.Drawing.Size(34, 20);
			this.radioButtonStyle2.TabIndex = 0;
			this.radioButtonStyle2.TabStop = true;
			this.radioButtonStyle2.UseVisualStyleBackColor = true;
			this.radioButtonStyle2.CheckedChanged += new System.EventHandler(this.radioButtonStyle1_CheckedChanged);
			// 
			// radioButtonStyle1
			// 
			this.radioButtonStyle1.AutoSize = true;
			this.radioButtonStyle1.ImageIndex = 0;
			this.radioButtonStyle1.ImageList = this.imageList1;
			this.radioButtonStyle1.Location = new System.Drawing.Point(6, 20);
			this.radioButtonStyle1.Name = "radioButtonStyle1";
			this.radioButtonStyle1.Size = new System.Drawing.Size(34, 20);
			this.radioButtonStyle1.TabIndex = 0;
			this.radioButtonStyle1.TabStop = true;
			this.radioButtonStyle1.UseVisualStyleBackColor = true;
			this.radioButtonStyle1.CheckedChanged += new System.EventHandler(this.radioButtonStyle1_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.numericUpDownDown);
			this.groupBox2.Controls.Add(this.numericUpDownUp);
			this.groupBox2.Controls.Add(this.numericUpDownRight);
			this.groupBox2.Controls.Add(this.numericUpDownLeft);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Location = new System.Drawing.Point(24, 208);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(292, 97);
			this.groupBox2.TabIndex = 38;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "页边距";
			// 
			// numericUpDownDown
			// 
			this.numericUpDownDown.Location = new System.Drawing.Point(186, 58);
			this.numericUpDownDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDownDown.Name = "numericUpDownDown";
			this.numericUpDownDown.Size = new System.Drawing.Size(88, 21);
			this.numericUpDownDown.TabIndex = 4;
			this.numericUpDownDown.ValueChanged += new System.EventHandler(this.numericUpDownDown_ValueChanged);
			// 
			// numericUpDownUp
			// 
			this.numericUpDownUp.Location = new System.Drawing.Point(186, 21);
			this.numericUpDownUp.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDownUp.Name = "numericUpDownUp";
			this.numericUpDownUp.Size = new System.Drawing.Size(88, 21);
			this.numericUpDownUp.TabIndex = 4;
			this.numericUpDownUp.ValueChanged += new System.EventHandler(this.numericUpDownUp_ValueChanged);
			// 
			// numericUpDownRight
			// 
			this.numericUpDownRight.Location = new System.Drawing.Point(45, 58);
			this.numericUpDownRight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDownRight.Name = "numericUpDownRight";
			this.numericUpDownRight.Size = new System.Drawing.Size(88, 21);
			this.numericUpDownRight.TabIndex = 4;
			this.numericUpDownRight.ValueChanged += new System.EventHandler(this.numericUpDownRight_ValueChanged);
			// 
			// numericUpDownLeft
			// 
			this.numericUpDownLeft.Location = new System.Drawing.Point(45, 21);
			this.numericUpDownLeft.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDownLeft.Name = "numericUpDownLeft";
			this.numericUpDownLeft.Size = new System.Drawing.Size(88, 21);
			this.numericUpDownLeft.TabIndex = 4;
			this.numericUpDownLeft.ValueChanged += new System.EventHandler(this.numericUpDownLeft_ValueChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(152, 62);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(29, 12);
			this.label5.TabIndex = 3;
			this.label5.Text = "下：";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(152, 26);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(29, 12);
			this.label4.TabIndex = 2;
			this.label4.Text = "上：";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 62);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(29, 12);
			this.label3.TabIndex = 1;
			this.label3.Text = "右：";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(10, 26);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(29, 12);
			this.label6.TabIndex = 0;
			this.label6.Text = "左：";
			// 
			// CtrlLineCallout
			// 
			this.AccessibleName = "线状提示框";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnSymbolSel3);
			this.Controls.Add(this.btnSymbolSel2);
			this.Controls.Add(this.btnSymbolSel);
			this.Controls.Add(this.checkBoxBorder);
			this.Controls.Add(this.checkBoxAccentBar);
			this.Controls.Add(this.checkBoxLeader);
			this.Controls.Add(this.numericUpDownGap);
			this.Controls.Add(this.numericUpDownLeader);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "CtrlLineCallout";
			this.Size = new System.Drawing.Size(486, 346);
			this.Load += new System.EventHandler(this.CtrlLineCallout_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeader)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownGap)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownUp)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownRight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeft)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericUpDownLeader;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDownGap;
		private System.Windows.Forms.CheckBox checkBoxLeader;
		private System.Windows.Forms.CheckBox checkBoxAccentBar;
		private System.Windows.Forms.CheckBox checkBoxBorder;
		private System.Windows.Forms.Button btnSymbolSel;
		private System.Windows.Forms.Button btnSymbolSel2;
		private System.Windows.Forms.Button btnSymbolSel3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioButtonStyle2;
		private System.Windows.Forms.RadioButton radioButtonStyle1;
		private System.Windows.Forms.RadioButton radioButtonStyle7;
		private System.Windows.Forms.RadioButton radioButtonStyle6;
		private System.Windows.Forms.RadioButton radioButtonStyle5;
		private System.Windows.Forms.RadioButton radioButtonStyle4;
		private System.Windows.Forms.RadioButton radioButtonStyle3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.NumericUpDown numericUpDownDown;
		private System.Windows.Forms.NumericUpDown numericUpDownUp;
		private System.Windows.Forms.NumericUpDown numericUpDownRight;
		private System.Windows.Forms.NumericUpDown numericUpDownLeft;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ImageList imageList1;
	}
}
