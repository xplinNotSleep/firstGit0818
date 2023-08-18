namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	partial class CtrlTemplate
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
			this.btnClear = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDownInterval = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.pictureBoxTemplateLine1 = new PictureBoxTemplateLine();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxTemplateLine1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(392, 72);
			this.label1.TabIndex = 0;
			this.label1.Text = "模板指定一个重复标记/间隙的线形式样.\r\n\r\n单击并拖动灰色‘方块’设置模板的长度。单击白色‘方块’指\r\n定点或线符号，利用间隔设置模板‘方块’的长度。\r\n";
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(14, 126);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(75, 23);
			this.btnClear.TabIndex = 2;
			this.btnClear.Text = "清除";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(141, 131);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "间距：";
			// 
			// numericUpDownInterval
			// 
			this.numericUpDownInterval.DecimalPlaces = 4;
			this.numericUpDownInterval.Location = new System.Drawing.Point(188, 129);
			this.numericUpDownInterval.Name = "numericUpDownInterval";
			this.numericUpDownInterval.Size = new System.Drawing.Size(120, 21);
			this.numericUpDownInterval.TabIndex = 4;
			this.numericUpDownInterval.ValueChanged += new System.EventHandler(this.numericUpDownInterval_ValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 170);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(161, 12);
			this.label3.TabIndex = 5;
			this.label3.Text = "间隔和线模式长度用点计算。";
			// 
			// pictureBoxTemplateLine1
			// 
			this.pictureBoxTemplateLine1.Location = new System.Drawing.Point(14, 85);
			this.pictureBoxTemplateLine1.Name = "pictureBoxTemplateLine1";
			this.pictureBoxTemplateLine1.Size = new System.Drawing.Size(459, 35);
			this.pictureBoxTemplateLine1.TabIndex = 6;
			this.pictureBoxTemplateLine1.TabStop = false;
			this.pictureBoxTemplateLine1.ElementChange += new System.EventHandler(this.pictureBoxTemplateLine1_ElementChange);
			// 
			// CtrlTemplate
			// 
			this.AccessibleName = "模板";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pictureBoxTemplateLine1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.numericUpDownInterval);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.label1);
			this.Name = "CtrlTemplate";
			this.Size = new System.Drawing.Size(486, 346);
			this.Load += new System.EventHandler(this.CtrlTemplate_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxTemplateLine1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDownInterval;
		private System.Windows.Forms.Label label3;
		private PictureBoxTemplateLine pictureBoxTemplateLine1;
	}
}
