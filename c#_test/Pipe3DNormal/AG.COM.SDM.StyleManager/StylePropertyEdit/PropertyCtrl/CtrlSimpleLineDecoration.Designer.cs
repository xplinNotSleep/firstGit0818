namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	partial class CtrlSimpleLineDecoration
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
			this.numericUpDownPositions = new System.Windows.Forms.NumericUpDown();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkBoxFlipFirst = new System.Windows.Forms.CheckBox();
			this.checkBoxFlipAll = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.radioButtonPositionAsRatio = new System.Windows.Forms.RadioButton();
			this.radioButtonRotate = new System.Windows.Forms.RadioButton();
			this.btnSelMarkerSymbol = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownPositions)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(23, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "位置数：";
			// 
			// numericUpDownPositions
			// 
			this.numericUpDownPositions.Location = new System.Drawing.Point(82, 25);
			this.numericUpDownPositions.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDownPositions.Name = "numericUpDownPositions";
			this.numericUpDownPositions.Size = new System.Drawing.Size(100, 21);
			this.numericUpDownPositions.TabIndex = 1;
			this.numericUpDownPositions.ValueChanged += new System.EventHandler(this.numericUpDownPositions_ValueChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkBoxFlipFirst);
			this.groupBox1.Controls.Add(this.checkBoxFlipAll);
			this.groupBox1.Location = new System.Drawing.Point(25, 68);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(157, 76);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "翻转";
			// 
			// checkBoxFlipFirst
			// 
			this.checkBoxFlipFirst.AutoSize = true;
			this.checkBoxFlipFirst.Location = new System.Drawing.Point(10, 42);
			this.checkBoxFlipFirst.Name = "checkBoxFlipFirst";
			this.checkBoxFlipFirst.Size = new System.Drawing.Size(84, 16);
			this.checkBoxFlipFirst.TabIndex = 1;
			this.checkBoxFlipFirst.Text = "第一个反向";
			this.checkBoxFlipFirst.UseVisualStyleBackColor = true;
			this.checkBoxFlipFirst.CheckedChanged += new System.EventHandler(this.checkBoxFlipFirst_CheckedChanged);
			// 
			// checkBoxFlipAll
			// 
			this.checkBoxFlipAll.AutoSize = true;
			this.checkBoxFlipAll.Location = new System.Drawing.Point(10, 20);
			this.checkBoxFlipAll.Name = "checkBoxFlipAll";
			this.checkBoxFlipAll.Size = new System.Drawing.Size(72, 16);
			this.checkBoxFlipAll.TabIndex = 0;
			this.checkBoxFlipAll.Text = "全部反向";
			this.checkBoxFlipAll.UseVisualStyleBackColor = true;
			this.checkBoxFlipAll.CheckedChanged += new System.EventHandler(this.checkBoxFlipAll_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.radioButtonPositionAsRatio);
			this.groupBox2.Controls.Add(this.radioButtonRotate);
			this.groupBox2.Location = new System.Drawing.Point(188, 68);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(251, 76);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "旋转";
			// 
			// radioButtonPositionAsRatio
			// 
			this.radioButtonPositionAsRatio.AutoSize = true;
			this.radioButtonPositionAsRatio.Location = new System.Drawing.Point(9, 42);
			this.radioButtonPositionAsRatio.Name = "radioButtonPositionAsRatio";
			this.radioButtonPositionAsRatio.Size = new System.Drawing.Size(191, 16);
			this.radioButtonPositionAsRatio.TabIndex = 1;
			this.radioButtonPositionAsRatio.TabStop = true;
			this.radioButtonPositionAsRatio.Text = "保持符号的角度相对于页面固定";
			this.radioButtonPositionAsRatio.UseVisualStyleBackColor = true;
			this.radioButtonPositionAsRatio.CheckedChanged += new System.EventHandler(this.radioButtonRotate_CheckedChanged);
			// 
			// radioButtonRotate
			// 
			this.radioButtonRotate.AutoSize = true;
			this.radioButtonRotate.Location = new System.Drawing.Point(9, 20);
			this.radioButtonRotate.Name = "radioButtonRotate";
			this.radioButtonRotate.Size = new System.Drawing.Size(131, 16);
			this.radioButtonRotate.TabIndex = 0;
			this.radioButtonRotate.TabStop = true;
			this.radioButtonRotate.Text = "沿着线角度旋转符号";
			this.radioButtonRotate.UseVisualStyleBackColor = true;
			this.radioButtonRotate.CheckedChanged += new System.EventHandler(this.radioButtonRotate_CheckedChanged);
			// 
			// btnSelMarkerSymbol
			// 
			this.btnSelMarkerSymbol.Location = new System.Drawing.Point(188, 25);
			this.btnSelMarkerSymbol.Name = "btnSelMarkerSymbol";
			this.btnSelMarkerSymbol.Size = new System.Drawing.Size(94, 23);
			this.btnSelMarkerSymbol.TabIndex = 36;
			this.btnSelMarkerSymbol.Text = "标记…";
			this.btnSelMarkerSymbol.UseVisualStyleBackColor = true;
			this.btnSelMarkerSymbol.Click += new System.EventHandler(this.btnSelMarkerSymbol_Click);
			// 
			// CtrlSimpleLineDecoration
			// 
			this.AccessibleName = "线装饰";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnSelMarkerSymbol);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.numericUpDownPositions);
			this.Controls.Add(this.label1);
			this.Name = "CtrlSimpleLineDecoration";
			this.Size = new System.Drawing.Size(486, 346);
			this.Load += new System.EventHandler(this.CtrlSimpleLineDecoration_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownPositions)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericUpDownPositions;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBoxFlipFirst;
		private System.Windows.Forms.CheckBox checkBoxFlipAll;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton radioButtonPositionAsRatio;
		private System.Windows.Forms.RadioButton radioButtonRotate;
		private System.Windows.Forms.Button btnSelMarkerSymbol;
	}
}
