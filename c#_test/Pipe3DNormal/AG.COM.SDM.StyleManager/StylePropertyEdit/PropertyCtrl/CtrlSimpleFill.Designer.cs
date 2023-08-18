namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	partial class CtrlSimpleFill
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
			this.numericUpDownOutlineSize = new System.Windows.Forms.NumericUpDown();
			this.colorPickerOutline = new AG.COM.SDM.Utility.Controls.ColorPicker();
			this.label6 = new System.Windows.Forms.Label();
			this.colorPickerSymbol = new AG.COM.SDM.Utility.Controls.ColorPicker();
			this.label7 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnSelOutline = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownOutlineSize)).BeginInit();
			this.SuspendLayout();
			// 
			// numericUpDownOutlineSize
			// 
			this.numericUpDownOutlineSize.DecimalPlaces = 4;
			this.numericUpDownOutlineSize.Location = new System.Drawing.Point(97, 83);
			this.numericUpDownOutlineSize.Name = "numericUpDownOutlineSize";
			this.numericUpDownOutlineSize.Size = new System.Drawing.Size(75, 21);
			this.numericUpDownOutlineSize.TabIndex = 15;
			this.numericUpDownOutlineSize.ValueChanged += new System.EventHandler(this.numericUpDownOutlineSize_ValueChanged);
			// 
			// colorPickerOutline
			// 
			this.colorPickerOutline.BackColor = System.Drawing.SystemColors.Window;
			this.colorPickerOutline.Context = null;
			this.colorPickerOutline.ForeColor = System.Drawing.SystemColors.WindowText;
			this.colorPickerOutline.Location = new System.Drawing.Point(97, 45);
			this.colorPickerOutline.Name = "colorPickerOutline";
			this.colorPickerOutline.ReadOnly = false;
			this.colorPickerOutline.Size = new System.Drawing.Size(145, 21);
			this.colorPickerOutline.TabIndex = 17;
			this.colorPickerOutline.Text = "White";
			this.colorPickerOutline.Value = System.Drawing.Color.White;
			this.colorPickerOutline.ValueChanged += new System.EventHandler(this.colorPickerOutline_ValueChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(17, 49);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(77, 12);
			this.label6.TabIndex = 13;
			this.label6.Text = "轮廓线颜色：";
			// 
			// colorPickerSymbol
			// 
			this.colorPickerSymbol.BackColor = System.Drawing.SystemColors.Window;
			this.colorPickerSymbol.Context = null;
			this.colorPickerSymbol.ForeColor = System.Drawing.SystemColors.WindowText;
			this.colorPickerSymbol.Location = new System.Drawing.Point(97, 15);
			this.colorPickerSymbol.Name = "colorPickerSymbol";
			this.colorPickerSymbol.ReadOnly = false;
			this.colorPickerSymbol.Size = new System.Drawing.Size(145, 21);
			this.colorPickerSymbol.TabIndex = 16;
			this.colorPickerSymbol.Text = "White";
			this.colorPickerSymbol.Value = System.Drawing.Color.White;
			this.colorPickerSymbol.ValueChanged += new System.EventHandler(this.colorPickerSymbol_ValueChanged);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(17, 87);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(77, 12);
			this.label7.TabIndex = 14;
			this.label7.Text = "轮廓线大小：";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(17, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 12;
			this.label1.Text = "颜色：";
			// 
			// btnSelOutline
			// 
			this.btnSelOutline.Location = new System.Drawing.Point(97, 110);
			this.btnSelOutline.Name = "btnSelOutline";
			this.btnSelOutline.Size = new System.Drawing.Size(75, 23);
			this.btnSelOutline.TabIndex = 33;
			this.btnSelOutline.Text = "轮廓线…";
			this.btnSelOutline.UseVisualStyleBackColor = true;
			this.btnSelOutline.Click += new System.EventHandler(this.btnSelOutline_Click);
			// 
			// CtrlSimpleFill
			// 
			this.AccessibleName = "简单填充";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnSelOutline);
			this.Controls.Add(this.numericUpDownOutlineSize);
			this.Controls.Add(this.colorPickerOutline);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.colorPickerSymbol);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label1);
			this.Name = "CtrlSimpleFill";
			this.Size = new System.Drawing.Size(486, 346);
			this.Load += new System.EventHandler(this.CtrlSimpleFill_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownOutlineSize)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown numericUpDownOutlineSize;
		private AG.COM.SDM.Utility.Controls.ColorPicker colorPickerOutline;
		private System.Windows.Forms.Label label6;
		private AG.COM.SDM.Utility.Controls.ColorPicker colorPickerSymbol;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnSelOutline;
	}
}
