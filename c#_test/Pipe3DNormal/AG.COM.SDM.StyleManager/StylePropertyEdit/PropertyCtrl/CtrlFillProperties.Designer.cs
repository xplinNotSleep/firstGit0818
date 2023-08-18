namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	partial class CtrlFillProperties
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
			this.numericUpDownYOffset = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.numericUpDownXOffset = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.numericUpDownYSeparation = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDownXSeparation = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownYOffset)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownXOffset)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownYSeparation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownXSeparation)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.numericUpDownYOffset);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.numericUpDownXOffset);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Location = new System.Drawing.Point(22, 25);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(160, 120);
			this.groupBox1.TabIndex = 14;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "偏移量";
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
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.numericUpDownYSeparation);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.numericUpDownXSeparation);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Location = new System.Drawing.Point(22, 162);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(160, 120);
			this.groupBox2.TabIndex = 15;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "分割间距";
			// 
			// numericUpDownYSeparation
			// 
			this.numericUpDownYSeparation.DecimalPlaces = 4;
			this.numericUpDownYSeparation.Location = new System.Drawing.Point(42, 71);
			this.numericUpDownYSeparation.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
			this.numericUpDownYSeparation.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            -2147483648});
			this.numericUpDownYSeparation.Name = "numericUpDownYSeparation";
			this.numericUpDownYSeparation.Size = new System.Drawing.Size(110, 21);
			this.numericUpDownYSeparation.TabIndex = 16;
			this.numericUpDownYSeparation.ValueChanged += new System.EventHandler(this.numericUpDownYSeparation_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(23, 12);
			this.label1.TabIndex = 14;
			this.label1.Text = "X：";
			// 
			// numericUpDownXSeparation
			// 
			this.numericUpDownXSeparation.DecimalPlaces = 4;
			this.numericUpDownXSeparation.Location = new System.Drawing.Point(42, 28);
			this.numericUpDownXSeparation.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
			this.numericUpDownXSeparation.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            -2147483648});
			this.numericUpDownXSeparation.Name = "numericUpDownXSeparation";
			this.numericUpDownXSeparation.Size = new System.Drawing.Size(110, 21);
			this.numericUpDownXSeparation.TabIndex = 17;
			this.numericUpDownXSeparation.ValueChanged += new System.EventHandler(this.numericUpDownXSeparation_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 73);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(23, 12);
			this.label2.TabIndex = 15;
			this.label2.Text = "Y：";
			// 
			// CtrlFillProperties
			// 
			this.AccessibleName = "填充属性";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "CtrlFillProperties";
			this.Size = new System.Drawing.Size(486, 346);
			this.Load += new System.EventHandler(this.CtrlFillProperties_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownYOffset)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownXOffset)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownYSeparation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownXSeparation)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.NumericUpDown numericUpDownYOffset;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown numericUpDownXOffset;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.NumericUpDown numericUpDownYSeparation;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericUpDownXSeparation;
		private System.Windows.Forms.Label label2;
	}
}
