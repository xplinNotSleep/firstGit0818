namespace AG.COM.SDM.StylePropertyEdit
{
	partial class FormSymbolPropertyEdit
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

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelStylePreview = new System.Windows.Forms.Panel();
            this.ctrlSymbolPreview1 = new AG.COM.SDM.StylePropertyEdit.CtrlSymbolPreview();
            this.panelStyleLayers = new System.Windows.Forms.Panel();
            this.ctrlSymbolLayers1 = new AG.COM.SDM.StylePropertyEdit.CtrlSymbolLayers();
            this.panelStyleProperty = new System.Windows.Forms.Panel();
            this.ctrlSymbolProperty1 = new AG.COM.SDM.StylePropertyEdit.CtrlSymbolProperty();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelStylePreview.SuspendLayout();
            this.panelStyleLayers.SuspendLayout();
            this.panelStyleProperty.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(747, 446);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(666, 446);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelStylePreview, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelStyleLayers, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelStyleProperty, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 206F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(810, 428);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panelStylePreview
            // 
            this.panelStylePreview.Controls.Add(this.ctrlSymbolPreview1);
            this.panelStylePreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStylePreview.Location = new System.Drawing.Point(3, 3);
            this.panelStylePreview.Name = "panelStylePreview";
            this.panelStylePreview.Size = new System.Drawing.Size(194, 200);
            this.panelStylePreview.TabIndex = 0;
            // 
            // ctrlSymbolPreview1
            // 
            this.ctrlSymbolPreview1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlSymbolPreview1.Location = new System.Drawing.Point(0, 0);
            this.ctrlSymbolPreview1.Name = "ctrlSymbolPreview1";
            this.ctrlSymbolPreview1.Size = new System.Drawing.Size(194, 200);
            this.ctrlSymbolPreview1.SymbolType = AG.COM.SDM.StylePropertyEdit.EnumSymbolType.SymbolTypeUnknow;
            this.ctrlSymbolPreview1.TabIndex = 0;
            // 
            // panelStyleLayers
            // 
            this.panelStyleLayers.Controls.Add(this.ctrlSymbolLayers1);
            this.panelStyleLayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStyleLayers.Location = new System.Drawing.Point(3, 209);
            this.panelStyleLayers.Name = "panelStyleLayers";
            this.panelStyleLayers.Size = new System.Drawing.Size(194, 216);
            this.panelStyleLayers.TabIndex = 1;
            // 
            // ctrlSymbolLayers1
            // 
            this.ctrlSymbolLayers1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlSymbolLayers1.Location = new System.Drawing.Point(0, 0);
            this.ctrlSymbolLayers1.Name = "ctrlSymbolLayers1";
            this.ctrlSymbolLayers1.Size = new System.Drawing.Size(194, 216);
            this.ctrlSymbolLayers1.Symbol = null;
            this.ctrlSymbolLayers1.TabIndex = 0;
            // 
            // panelStyleProperty
            // 
            this.panelStyleProperty.Controls.Add(this.ctrlSymbolProperty1);
            this.panelStyleProperty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStyleProperty.Location = new System.Drawing.Point(203, 3);
            this.panelStyleProperty.Name = "panelStyleProperty";
            this.tableLayoutPanel1.SetRowSpan(this.panelStyleProperty, 2);
            this.panelStyleProperty.Size = new System.Drawing.Size(604, 422);
            this.panelStyleProperty.TabIndex = 2;
            // 
            // ctrlSymbolProperty1
            // 
            this.ctrlSymbolProperty1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlSymbolProperty1.Location = new System.Drawing.Point(0, 0);
            this.ctrlSymbolProperty1.Name = "ctrlSymbolProperty1";
            this.ctrlSymbolProperty1.Size = new System.Drawing.Size(604, 422);
            this.ctrlSymbolProperty1.Symbol = null;
            this.ctrlSymbolProperty1.SymbolType = AG.COM.SDM.StylePropertyEdit.EnumSymbolType.SymbolTypeUnknow;
            this.ctrlSymbolProperty1.TabIndex = 0;
            // 
            // FormSymbolPropertyEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(834, 481);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(746, 508);
            this.Name = "FormSymbolPropertyEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "符号属性编辑器";
            this.Load += new System.EventHandler(this.FormSymbolPropertyEdit_Load);
            this.Resize += new System.EventHandler(this.FormSymbolPropertyEdit_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelStylePreview.ResumeLayout(false);
            this.panelStyleLayers.ResumeLayout(false);
            this.panelStyleProperty.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel panelStylePreview;
		private System.Windows.Forms.Panel panelStyleLayers;
		private System.Windows.Forms.Panel panelStyleProperty;
		private CtrlSymbolPreview ctrlSymbolPreview1;
		private CtrlSymbolLayers ctrlSymbolLayers1;
		private CtrlSymbolProperty ctrlSymbolProperty1;
	}
}