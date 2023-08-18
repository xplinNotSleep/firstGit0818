namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	partial class CtrlSimpleLineCallout
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
			this.numericUpDownLeader = new System.Windows.Forms.NumericUpDown();
			this.checkBoxAutoSnap = new System.Windows.Forms.CheckBox();
			this.btnSymbolSel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeader)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(24, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "引导容限：";
			// 
			// numericUpDownLeader
			// 
			this.numericUpDownLeader.Location = new System.Drawing.Point(95, 24);
			this.numericUpDownLeader.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.numericUpDownLeader.Name = "numericUpDownLeader";
			this.numericUpDownLeader.Size = new System.Drawing.Size(130, 21);
			this.numericUpDownLeader.TabIndex = 1;
			this.numericUpDownLeader.ValueChanged += new System.EventHandler(this.numericUpDownLeader_ValueChanged);
			// 
			// checkBoxAutoSnap
			// 
			this.checkBoxAutoSnap.AutoSize = true;
			this.checkBoxAutoSnap.Location = new System.Drawing.Point(26, 99);
			this.checkBoxAutoSnap.Name = "checkBoxAutoSnap";
			this.checkBoxAutoSnap.Size = new System.Drawing.Size(144, 16);
			this.checkBoxAutoSnap.TabIndex = 2;
			this.checkBoxAutoSnap.Text = "自动捕捉引导线到文本";
			this.checkBoxAutoSnap.UseVisualStyleBackColor = true;
			this.checkBoxAutoSnap.CheckedChanged += new System.EventHandler(this.checkBoxAutoSnap_CheckedChanged);
			// 
			// btnSymbolSel
			// 
			this.btnSymbolSel.Location = new System.Drawing.Point(26, 58);
			this.btnSymbolSel.Name = "btnSymbolSel";
			this.btnSymbolSel.Size = new System.Drawing.Size(63, 23);
			this.btnSymbolSel.TabIndex = 28;
			this.btnSymbolSel.Text = "符号…";
			this.btnSymbolSel.UseVisualStyleBackColor = true;
			this.btnSymbolSel.Click += new System.EventHandler(this.btnSymbolSel_Click);
			// 
			// CtrlSimpleLineCallout
			// 
			this.AccessibleName = "简单线形提示框";
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnSymbolSel);
			this.Controls.Add(this.checkBoxAutoSnap);
			this.Controls.Add(this.numericUpDownLeader);
			this.Controls.Add(this.label1);
			this.Name = "CtrlSimpleLineCallout";
			this.Size = new System.Drawing.Size(486, 346);
			this.Load += new System.EventHandler(this.CtrlSimpleLineCallout_Load);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeader)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericUpDownLeader;
		private System.Windows.Forms.CheckBox checkBoxAutoSnap;
		private System.Windows.Forms.Button btnSymbolSel;
	}
}
