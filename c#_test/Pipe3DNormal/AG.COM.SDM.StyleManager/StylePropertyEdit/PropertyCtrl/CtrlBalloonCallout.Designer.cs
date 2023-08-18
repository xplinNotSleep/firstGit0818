namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	partial class CtrlBalloonCallout
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrlBalloonCallout));
			this.radioButtonRect = new System.Windows.Forms.RadioButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.radioButtonRoundRect = new System.Windows.Forms.RadioButton();
			this.btnSymbolSel = new System.Windows.Forms.Button();
			this.numericUpDownLeader = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.numericUpDownDown = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownUp = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownRight = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownLeft = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeader)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownUp)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownRight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeft)).BeginInit();
			this.SuspendLayout();
			// 
			// radioButtonRect
			// 
			this.radioButtonRect.AutoSize = true;
			this.radioButtonRect.ImageIndex = 0;
			this.radioButtonRect.ImageList = this.imageList1;
			this.radioButtonRect.Location = new System.Drawing.Point(18, 27);
			this.radioButtonRect.Name = "radioButtonRect";
			this.radioButtonRect.Size = new System.Drawing.Size(58, 29);
			this.radioButtonRect.TabIndex = 0;
			this.radioButtonRect.UseVisualStyleBackColor = true;
			this.radioButtonRect.CheckedChanged += new System.EventHandler(this.radioButtonRect_CheckedChanged);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
			this.imageList1.Images.SetKeyName(0, "01.bmp");
			this.imageList1.Images.SetKeyName(1, "02.bmp");
			// 
			// radioButtonRoundRect
			// 
			this.radioButtonRoundRect.AutoSize = true;
			this.radioButtonRoundRect.ImageIndex = 1;
			this.radioButtonRoundRect.ImageList = this.imageList1;
			this.radioButtonRoundRect.Location = new System.Drawing.Point(18, 58);
			this.radioButtonRoundRect.Name = "radioButtonRoundRect";
			this.radioButtonRoundRect.Size = new System.Drawing.Size(58, 29);
			this.radioButtonRoundRect.TabIndex = 1;
			this.radioButtonRoundRect.UseVisualStyleBackColor = true;
			this.radioButtonRoundRect.CheckedChanged += new System.EventHandler(this.radioButtonRect_CheckedChanged);
			// 
			// btnSymbolSel
			// 
			this.btnSymbolSel.Location = new System.Drawing.Point(231, 64);
			this.btnSymbolSel.Name = "btnSymbolSel";
			this.btnSymbolSel.Size = new System.Drawing.Size(85, 23);
			this.btnSymbolSel.TabIndex = 31;
			this.btnSymbolSel.Text = "符号…";
			this.btnSymbolSel.UseVisualStyleBackColor = true;
			this.btnSymbolSel.Click += new System.EventHandler(this.btnSymbolSel_Click);
			// 
			// numericUpDownLeader
			// 
			this.numericUpDownLeader.Location = new System.Drawing.Point(210, 32);
			this.numericUpDownLeader.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDownLeader.Name = "numericUpDownLeader";
			this.numericUpDownLeader.Size = new System.Drawing.Size(106, 21);
			this.numericUpDownLeader.TabIndex = 30;
			this.numericUpDownLeader.ValueChanged += new System.EventHandler(this.numericUpDownLeader_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(139, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 12);
			this.label1.TabIndex = 29;
			this.label1.Text = "引导容限：";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.numericUpDownDown);
			this.groupBox1.Controls.Add(this.numericUpDownUp);
			this.groupBox1.Controls.Add(this.numericUpDownRight);
			this.groupBox1.Controls.Add(this.numericUpDownLeft);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(18, 93);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(298, 95);
			this.groupBox1.TabIndex = 32;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "页边距";
			// 
			// numericUpDownDown
			// 
			this.numericUpDownDown.Location = new System.Drawing.Point(194, 58);
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
			this.numericUpDownUp.Location = new System.Drawing.Point(194, 21);
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
			this.numericUpDownRight.Location = new System.Drawing.Point(53, 58);
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
			this.numericUpDownLeft.Location = new System.Drawing.Point(53, 21);
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
			this.label5.Location = new System.Drawing.Point(160, 62);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(29, 12);
			this.label5.TabIndex = 3;
			this.label5.Text = "下：";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(160, 26);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(29, 12);
			this.label4.TabIndex = 2;
			this.label4.Text = "上：";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(18, 62);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(29, 12);
			this.label3.TabIndex = 1;
			this.label3.Text = "右：";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(18, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 12);
			this.label2.TabIndex = 0;
			this.label2.Text = "左：";
			// 
			// CtrlBalloonCallout
			// 
			this.AccessibleName = "气球型提示";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnSymbolSel);
			this.Controls.Add(this.numericUpDownLeader);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.radioButtonRoundRect);
			this.Controls.Add(this.radioButtonRect);
			this.Name = "CtrlBalloonCallout";
			this.Size = new System.Drawing.Size(486, 346);
			this.Load += new System.EventHandler(this.CtrlBalloonCallout_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeader)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownUp)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownRight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeft)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RadioButton radioButtonRect;
		private System.Windows.Forms.RadioButton radioButtonRoundRect;
		private System.Windows.Forms.Button btnSymbolSel;
		private System.Windows.Forms.NumericUpDown numericUpDownLeader;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDownDown;
		private System.Windows.Forms.NumericUpDown numericUpDownUp;
		private System.Windows.Forms.NumericUpDown numericUpDownRight;
		private System.Windows.Forms.NumericUpDown numericUpDownLeft;
		private System.Windows.Forms.ImageList imageList1;
	}
}
