namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	partial class CtrlGradientFill
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.comboBoxCRStyle = new AG.COM.SDM.Utility.Controls.StyleComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.btnSelOutline = new System.Windows.Forms.Button();
			this.numericUpDownIntervals = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownPercentage = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownAngle = new System.Windows.Forms.NumericUpDown();
			this.comboBoxStyle = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownIntervals)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownPercentage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngle)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "间隔：";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "百分比：";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 87);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 12);
			this.label3.TabIndex = 4;
			this.label3.Text = "角度：";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(224, 22);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 12);
			this.label4.TabIndex = 6;
			this.label4.Text = "样式：";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.comboBoxCRStyle);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Location = new System.Drawing.Point(226, 45);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 59);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "色阶";
			// 
			// comboBoxCRStyle
			// 
			this.comboBoxCRStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.comboBoxCRStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCRStyle.FormattingEnabled = true;
			this.comboBoxCRStyle.Location = new System.Drawing.Point(62, 25);
			this.comboBoxCRStyle.Name = "comboBoxCRStyle";
			this.comboBoxCRStyle.Size = new System.Drawing.Size(121, 22);
			this.comboBoxCRStyle.StyleGalleryClass = null;
			this.comboBoxCRStyle.TabIndex = 10;
			this.comboBoxCRStyle.SelectedIndexChanged += new System.EventHandler(this.comboBoxCRStyle_SelectedIndexChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(15, 28);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 12);
			this.label5.TabIndex = 9;
			this.label5.Text = "样式：";
			// 
			// btnSelOutline
			// 
			this.btnSelOutline.Location = new System.Drawing.Point(243, 121);
			this.btnSelOutline.Name = "btnSelOutline";
			this.btnSelOutline.Size = new System.Drawing.Size(93, 23);
			this.btnSelOutline.TabIndex = 33;
			this.btnSelOutline.Text = "轮廓线…";
			this.btnSelOutline.UseVisualStyleBackColor = true;
			this.btnSelOutline.Click += new System.EventHandler(this.btnSelOutline_Click);
			// 
			// numericUpDownIntervals
			// 
			this.numericUpDownIntervals.Location = new System.Drawing.Point(69, 20);
			this.numericUpDownIntervals.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDownIntervals.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.numericUpDownIntervals.Name = "numericUpDownIntervals";
			this.numericUpDownIntervals.Size = new System.Drawing.Size(120, 21);
			this.numericUpDownIntervals.TabIndex = 34;
			this.numericUpDownIntervals.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.numericUpDownIntervals.ValueChanged += new System.EventHandler(this.numericUpDownIntervals_ValueChanged);
			// 
			// numericUpDownPercentage
			// 
			this.numericUpDownPercentage.Location = new System.Drawing.Point(69, 47);
			this.numericUpDownPercentage.Name = "numericUpDownPercentage";
			this.numericUpDownPercentage.Size = new System.Drawing.Size(120, 21);
			this.numericUpDownPercentage.TabIndex = 34;
			this.numericUpDownPercentage.ValueChanged += new System.EventHandler(this.numericUpDownPercentage_ValueChanged);
			// 
			// numericUpDownAngle
			// 
			this.numericUpDownAngle.Location = new System.Drawing.Point(69, 83);
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
			this.numericUpDownAngle.Size = new System.Drawing.Size(120, 21);
			this.numericUpDownAngle.TabIndex = 34;
			this.numericUpDownAngle.ValueChanged += new System.EventHandler(this.numericUpDownAngle_ValueChanged);
			// 
			// comboBoxStyle
			// 
			this.comboBoxStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxStyle.FormattingEnabled = true;
			this.comboBoxStyle.Items.AddRange(new object[] {
            "Linear",
            "Rectangular",
            "Circular",
            "Buffered"});
			this.comboBoxStyle.Location = new System.Drawing.Point(288, 19);
			this.comboBoxStyle.Name = "comboBoxStyle";
			this.comboBoxStyle.Size = new System.Drawing.Size(121, 20);
			this.comboBoxStyle.TabIndex = 35;
			this.comboBoxStyle.SelectedIndexChanged += new System.EventHandler(this.comboBoxStyle_SelectedIndexChanged);
			// 
			// CtrlGradientFill
			// 
			this.AccessibleName = "渐变填充";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.comboBoxStyle);
			this.Controls.Add(this.numericUpDownAngle);
			this.Controls.Add(this.numericUpDownPercentage);
			this.Controls.Add(this.numericUpDownIntervals);
			this.Controls.Add(this.btnSelOutline);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "CtrlGradientFill";
			this.Size = new System.Drawing.Size(486, 346);
			this.Load += new System.EventHandler(this.CtrlGradientFill_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownIntervals)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownPercentage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngle)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnSelOutline;
		private System.Windows.Forms.NumericUpDown numericUpDownIntervals;
		private System.Windows.Forms.NumericUpDown numericUpDownPercentage;
		private System.Windows.Forms.NumericUpDown numericUpDownAngle;
		private System.Windows.Forms.ComboBox comboBoxStyle;
		private AG.COM.SDM.Utility.Controls.StyleComboBox comboBoxCRStyle;
	}
}
