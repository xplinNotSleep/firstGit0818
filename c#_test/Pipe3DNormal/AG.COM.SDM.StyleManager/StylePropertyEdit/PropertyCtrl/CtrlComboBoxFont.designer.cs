namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	partial class CtrlComboBoxFont
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
			this.comboBoxFont = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// comboBoxFont
			// 
			this.comboBoxFont.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFont.FormattingEnabled = true;
			this.comboBoxFont.Location = new System.Drawing.Point(0, 0);
			this.comboBoxFont.Name = "comboBoxFont";
			this.comboBoxFont.Size = new System.Drawing.Size(163, 20);
			this.comboBoxFont.TabIndex = 0;
			// 
			// CtrlComboBoxFont
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.comboBoxFont);
			this.MaximumSize = new System.Drawing.Size(1024, 20);
			this.MinimumSize = new System.Drawing.Size(40, 20);
			this.Name = "CtrlComboBoxFont";
			this.Size = new System.Drawing.Size(163, 20);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBoxFont;
	}
}
