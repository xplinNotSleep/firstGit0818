namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	partial class CtrlMarkerLine
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
			this.btnSelMarkerSymbol = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnSelMarkerSymbol
			// 
			this.btnSelMarkerSymbol.Location = new System.Drawing.Point(28, 29);
			this.btnSelMarkerSymbol.Name = "btnSelMarkerSymbol";
			this.btnSelMarkerSymbol.Size = new System.Drawing.Size(94, 23);
			this.btnSelMarkerSymbol.TabIndex = 36;
			this.btnSelMarkerSymbol.Text = "符号…";
			this.btnSelMarkerSymbol.UseVisualStyleBackColor = true;
			this.btnSelMarkerSymbol.Click += new System.EventHandler(this.btnSelMarkerSymbol_Click);
			// 
			// CtrlMarkerLine
			// 
			this.AccessibleName = "标记线";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnSelMarkerSymbol);
			this.Name = "CtrlMarkerLine";
			this.Size = new System.Drawing.Size(486, 346);
			this.Load += new System.EventHandler(this.CtrlMarkerLine_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnSelMarkerSymbol;
	}
}
