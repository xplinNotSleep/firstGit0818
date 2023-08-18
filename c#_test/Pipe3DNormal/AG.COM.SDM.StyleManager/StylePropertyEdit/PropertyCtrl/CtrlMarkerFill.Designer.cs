namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	partial class CtrlMarkerFill
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
			this.btnSelOutline = new System.Windows.Forms.Button();
			this.btnSelMarkerSymbol = new System.Windows.Forms.Button();
			this.colorPickerSymbol = new AG.COM.SDM.Utility.Controls.ColorPicker();
			this.label1 = new System.Windows.Forms.Label();
			this.radioButtonGrid = new System.Windows.Forms.RadioButton();
			this.radioButtonRandom = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// btnSelOutline
			// 
			this.btnSelOutline.Location = new System.Drawing.Point(20, 92);
			this.btnSelOutline.Name = "btnSelOutline";
			this.btnSelOutline.Size = new System.Drawing.Size(94, 23);
			this.btnSelOutline.TabIndex = 36;
			this.btnSelOutline.Text = "轮廓线…";
			this.btnSelOutline.UseVisualStyleBackColor = true;
			this.btnSelOutline.Click += new System.EventHandler(this.btnSelOutline_Click);
			// 
			// btnSelMarkerSymbol
			// 
			this.btnSelMarkerSymbol.Location = new System.Drawing.Point(20, 53);
			this.btnSelMarkerSymbol.Name = "btnSelMarkerSymbol";
			this.btnSelMarkerSymbol.Size = new System.Drawing.Size(94, 23);
			this.btnSelMarkerSymbol.TabIndex = 35;
			this.btnSelMarkerSymbol.Text = "标记…";
			this.btnSelMarkerSymbol.UseVisualStyleBackColor = true;
			this.btnSelMarkerSymbol.Click += new System.EventHandler(this.btnSelMarkerSymbol_Click);
			// 
			// colorPickerSymbol
			// 
			this.colorPickerSymbol.BackColor = System.Drawing.SystemColors.Window;
			this.colorPickerSymbol.Context = null;
			this.colorPickerSymbol.ForeColor = System.Drawing.SystemColors.WindowText;
			this.colorPickerSymbol.Location = new System.Drawing.Point(61, 15);
			this.colorPickerSymbol.Name = "colorPickerSymbol";
			this.colorPickerSymbol.ReadOnly = false;
			this.colorPickerSymbol.Size = new System.Drawing.Size(145, 21);
			this.colorPickerSymbol.TabIndex = 34;
			this.colorPickerSymbol.Text = "White";
			this.colorPickerSymbol.Value = System.Drawing.Color.White;
			this.colorPickerSymbol.ValueChanged += new System.EventHandler(this.colorPickerSymbol_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(18, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 33;
			this.label1.Text = "颜色：";
			// 
			// radioButtonGrid
			// 
			this.radioButtonGrid.AutoSize = true;
			this.radioButtonGrid.Checked = true;
			this.radioButtonGrid.Location = new System.Drawing.Point(20, 135);
			this.radioButtonGrid.Name = "radioButtonGrid";
			this.radioButtonGrid.Size = new System.Drawing.Size(47, 16);
			this.radioButtonGrid.TabIndex = 37;
			this.radioButtonGrid.TabStop = true;
			this.radioButtonGrid.Text = "网格";
			this.radioButtonGrid.UseVisualStyleBackColor = true;
			this.radioButtonGrid.CheckedChanged += new System.EventHandler(this.radioButtonGrid_CheckedChanged);
			// 
			// radioButtonRandom
			// 
			this.radioButtonRandom.AutoSize = true;
			this.radioButtonRandom.Location = new System.Drawing.Point(73, 135);
			this.radioButtonRandom.Name = "radioButtonRandom";
			this.radioButtonRandom.Size = new System.Drawing.Size(59, 16);
			this.radioButtonRandom.TabIndex = 38;
			this.radioButtonRandom.Text = "随机的";
			this.radioButtonRandom.UseVisualStyleBackColor = true;
			this.radioButtonRandom.CheckedChanged += new System.EventHandler(this.radioButtonGrid_CheckedChanged);
			// 
			// CtrlMarkerFill
			// 
			this.AccessibleName = "标记填充";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.radioButtonRandom);
			this.Controls.Add(this.radioButtonGrid);
			this.Controls.Add(this.btnSelOutline);
			this.Controls.Add(this.btnSelMarkerSymbol);
			this.Controls.Add(this.colorPickerSymbol);
			this.Controls.Add(this.label1);
			this.Name = "CtrlMarkerFill";
			this.Size = new System.Drawing.Size(486, 346);
			this.Load += new System.EventHandler(this.CtrlMarkerFill_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnSelOutline;
		private System.Windows.Forms.Button btnSelMarkerSymbol;
		private AG.COM.SDM.Utility.Controls.ColorPicker colorPickerSymbol;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton radioButtonGrid;
		private System.Windows.Forms.RadioButton radioButtonRandom;
	}
}
