namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	partial class CtrlCartographicLine
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
			this.numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.colorPickerSymbol = new AG.COM.SDM.Utility.Controls.ColorPicker();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButtonSquare = new System.Windows.Forms.RadioButton();
			this.radioButtonRound = new System.Windows.Forms.RadioButton();
			this.radioButtonButt = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.radioButtonBevel = new System.Windows.Forms.RadioButton();
			this.radioButtonJRound = new System.Windows.Forms.RadioButton();
			this.radioButtonMiter = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// numericUpDownWidth
			// 
			this.numericUpDownWidth.DecimalPlaces = 4;
			this.numericUpDownWidth.Location = new System.Drawing.Point(66, 52);
			this.numericUpDownWidth.Name = "numericUpDownWidth";
			this.numericUpDownWidth.Size = new System.Drawing.Size(84, 21);
			this.numericUpDownWidth.TabIndex = 27;
			this.numericUpDownWidth.ValueChanged += new System.EventHandler(this.numericUpDownWidth_ValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(19, 54);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 12);
			this.label3.TabIndex = 26;
			this.label3.Text = "宽度：";
			// 
			// colorPickerSymbol
			// 
			this.colorPickerSymbol.BackColor = System.Drawing.SystemColors.Window;
			this.colorPickerSymbol.Context = null;
			this.colorPickerSymbol.ForeColor = System.Drawing.SystemColors.WindowText;
			this.colorPickerSymbol.Location = new System.Drawing.Point(66, 19);
			this.colorPickerSymbol.Name = "colorPickerSymbol";
			this.colorPickerSymbol.ReadOnly = false;
			this.colorPickerSymbol.Size = new System.Drawing.Size(145, 21);
			this.colorPickerSymbol.TabIndex = 25;
			this.colorPickerSymbol.Text = "White";
			this.colorPickerSymbol.Value = System.Drawing.Color.White;
			this.colorPickerSymbol.ValueChanged += new System.EventHandler(this.colorPickerSymbol_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(19, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 24;
			this.label1.Text = "颜色：";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButtonSquare);
			this.groupBox1.Controls.Add(this.radioButtonRound);
			this.groupBox1.Controls.Add(this.radioButtonButt);
			this.groupBox1.Location = new System.Drawing.Point(21, 81);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(129, 97);
			this.groupBox1.TabIndex = 28;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "线头";
			// 
			// radioButtonSquare
			// 
			this.radioButtonSquare.AutoSize = true;
			this.radioButtonSquare.Location = new System.Drawing.Point(18, 64);
			this.radioButtonSquare.Name = "radioButtonSquare";
			this.radioButtonSquare.Size = new System.Drawing.Size(47, 16);
			this.radioButtonSquare.TabIndex = 2;
			this.radioButtonSquare.TabStop = true;
			this.radioButtonSquare.Text = "方头";
			this.radioButtonSquare.UseVisualStyleBackColor = true;
			this.radioButtonSquare.CheckedChanged += new System.EventHandler(this.radioButtonButt_CheckedChanged);
			// 
			// radioButtonRound
			// 
			this.radioButtonRound.AutoSize = true;
			this.radioButtonRound.Location = new System.Drawing.Point(18, 42);
			this.radioButtonRound.Name = "radioButtonRound";
			this.radioButtonRound.Size = new System.Drawing.Size(47, 16);
			this.radioButtonRound.TabIndex = 1;
			this.radioButtonRound.TabStop = true;
			this.radioButtonRound.Text = "圆接";
			this.radioButtonRound.UseVisualStyleBackColor = true;
			this.radioButtonRound.CheckedChanged += new System.EventHandler(this.radioButtonButt_CheckedChanged);
			// 
			// radioButtonButt
			// 
			this.radioButtonButt.AutoSize = true;
			this.radioButtonButt.Location = new System.Drawing.Point(18, 20);
			this.radioButtonButt.Name = "radioButtonButt";
			this.radioButtonButt.Size = new System.Drawing.Size(47, 16);
			this.radioButtonButt.TabIndex = 0;
			this.radioButtonButt.TabStop = true;
			this.radioButtonButt.Text = "平头";
			this.radioButtonButt.UseVisualStyleBackColor = true;
			this.radioButtonButt.CheckedChanged += new System.EventHandler(this.radioButtonButt_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.radioButtonBevel);
			this.groupBox2.Controls.Add(this.radioButtonJRound);
			this.groupBox2.Controls.Add(this.radioButtonMiter);
			this.groupBox2.Location = new System.Drawing.Point(156, 81);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(129, 97);
			this.groupBox2.TabIndex = 29;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "线连接";
			// 
			// radioButtonBevel
			// 
			this.radioButtonBevel.AutoSize = true;
			this.radioButtonBevel.Location = new System.Drawing.Point(18, 64);
			this.radioButtonBevel.Name = "radioButtonBevel";
			this.radioButtonBevel.Size = new System.Drawing.Size(47, 16);
			this.radioButtonBevel.TabIndex = 2;
			this.radioButtonBevel.TabStop = true;
			this.radioButtonBevel.Text = "平切";
			this.radioButtonBevel.UseVisualStyleBackColor = true;
			this.radioButtonBevel.CheckedChanged += new System.EventHandler(this.radioButtonMiter_CheckedChanged);
			// 
			// radioButtonJRound
			// 
			this.radioButtonJRound.AutoSize = true;
			this.radioButtonJRound.Location = new System.Drawing.Point(18, 42);
			this.radioButtonJRound.Name = "radioButtonJRound";
			this.radioButtonJRound.Size = new System.Drawing.Size(47, 16);
			this.radioButtonJRound.TabIndex = 1;
			this.radioButtonJRound.TabStop = true;
			this.radioButtonJRound.Text = "圆接";
			this.radioButtonJRound.UseVisualStyleBackColor = true;
			this.radioButtonJRound.CheckedChanged += new System.EventHandler(this.radioButtonMiter_CheckedChanged);
			// 
			// radioButtonMiter
			// 
			this.radioButtonMiter.AutoSize = true;
			this.radioButtonMiter.Location = new System.Drawing.Point(18, 20);
			this.radioButtonMiter.Name = "radioButtonMiter";
			this.radioButtonMiter.Size = new System.Drawing.Size(47, 16);
			this.radioButtonMiter.TabIndex = 0;
			this.radioButtonMiter.TabStop = true;
			this.radioButtonMiter.Text = "斜接";
			this.radioButtonMiter.UseVisualStyleBackColor = true;
			this.radioButtonMiter.CheckedChanged += new System.EventHandler(this.radioButtonMiter_CheckedChanged);
			// 
			// CtrlCartographicLine
			// 
			this.AccessibleName = "制图线";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.numericUpDownWidth);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.colorPickerSymbol);
			this.Controls.Add(this.label1);
			this.Name = "CtrlCartographicLine";
			this.Size = new System.Drawing.Size(486, 346);
			this.Load += new System.EventHandler(this.CtrlCartographicLine_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown numericUpDownWidth;
		private System.Windows.Forms.Label label3;
		private AG.COM.SDM.Utility.Controls.ColorPicker colorPickerSymbol;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioButtonSquare;
		private System.Windows.Forms.RadioButton radioButtonRound;
		private System.Windows.Forms.RadioButton radioButtonButt;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton radioButtonBevel;
		private System.Windows.Forms.RadioButton radioButtonJRound;
		private System.Windows.Forms.RadioButton radioButtonMiter;
	}
}
