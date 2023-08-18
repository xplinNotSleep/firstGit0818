namespace AG.COM.SDM.StylePropertyEdit
{
	partial class CtrlSymbolPreview
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrlSymbolPreview));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.pictureBoxPreview = new SymbolPictureBox();
			this.radioButtonFoldline = new System.Windows.Forms.RadioButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.radioButtonBeeline = new System.Windows.Forms.RadioButton();
			this.checkBoxReticle = new System.Windows.Forms.CheckBox();
			this.comboBoxScale = new System.Windows.Forms.ComboBox();
			this.btnZoomTo11 = new System.Windows.Forms.Button();
			this.btnZoomOut = new System.Windows.Forms.Button();
			this.btnZoomIn = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.pictureBoxPreview);
			this.groupBox1.Controls.Add(this.radioButtonFoldline);
			this.groupBox1.Controls.Add(this.radioButtonBeeline);
			this.groupBox1.Controls.Add(this.checkBoxReticle);
			this.groupBox1.Controls.Add(this.comboBoxScale);
			this.groupBox1.Controls.Add(this.btnZoomTo11);
			this.groupBox1.Controls.Add(this.btnZoomOut);
			this.groupBox1.Controls.Add(this.btnZoomIn);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(190, 205);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "预览";
			// 
			// pictureBoxPreview
			// 
			this.pictureBoxPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBoxPreview.Location = new System.Drawing.Point(6, 16);
			this.pictureBoxPreview.Name = "pictureBoxPreview";
			this.pictureBoxPreview.Size = new System.Drawing.Size(178, 129);
			this.pictureBoxPreview.TabIndex = 6;
			this.pictureBoxPreview.TabStop = false;
			// 
			// radioButtonFoldline
			// 
			this.radioButtonFoldline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.radioButtonFoldline.AutoSize = true;
			this.radioButtonFoldline.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
			this.radioButtonFoldline.ImageIndex = 0;
			this.radioButtonFoldline.ImageList = this.imageList1;
			this.radioButtonFoldline.Location = new System.Drawing.Point(59, 150);
			this.radioButtonFoldline.Name = "radioButtonFoldline";
			this.radioButtonFoldline.Size = new System.Drawing.Size(30, 16);
			this.radioButtonFoldline.TabIndex = 5;
			this.radioButtonFoldline.UseVisualStyleBackColor = true;
			this.radioButtonFoldline.Visible = false;
			this.radioButtonFoldline.CheckedChanged += new System.EventHandler(this.radioButtonBeeline_CheckedChanged);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "Foldline.png");
			this.imageList1.Images.SetKeyName(1, "ZoomIn.png");
			this.imageList1.Images.SetKeyName(2, "ZoomOut.png");
			this.imageList1.Images.SetKeyName(3, "11.png");
			this.imageList1.Images.SetKeyName(4, "Beeline.png");
			// 
			// radioButtonBeeline
			// 
			this.radioButtonBeeline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.radioButtonBeeline.AutoSize = true;
			this.radioButtonBeeline.Checked = true;
			this.radioButtonBeeline.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.radioButtonBeeline.ImageIndex = 4;
			this.radioButtonBeeline.ImageList = this.imageList1;
			this.radioButtonBeeline.Location = new System.Drawing.Point(6, 150);
			this.radioButtonBeeline.Name = "radioButtonBeeline";
			this.radioButtonBeeline.Size = new System.Drawing.Size(30, 16);
			this.radioButtonBeeline.TabIndex = 4;
			this.radioButtonBeeline.TabStop = true;
			this.radioButtonBeeline.UseVisualStyleBackColor = true;
			this.radioButtonBeeline.Visible = false;
			this.radioButtonBeeline.CheckedChanged += new System.EventHandler(this.radioButtonBeeline_CheckedChanged);
			// 
			// checkBoxReticle
			// 
			this.checkBoxReticle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkBoxReticle.AutoSize = true;
			this.checkBoxReticle.Checked = true;
			this.checkBoxReticle.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxReticle.Location = new System.Drawing.Point(6, 151);
			this.checkBoxReticle.Name = "checkBoxReticle";
			this.checkBoxReticle.Size = new System.Drawing.Size(60, 16);
			this.checkBoxReticle.TabIndex = 3;
			this.checkBoxReticle.Text = "十字线";
			this.checkBoxReticle.UseVisualStyleBackColor = true;
			this.checkBoxReticle.Visible = false;
			this.checkBoxReticle.CheckedChanged += new System.EventHandler(this.checkBoxReticle_CheckedChanged);
			// 
			// comboBoxScale
			// 
			this.comboBoxScale.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxScale.FormattingEnabled = true;
			this.comboBoxScale.Items.AddRange(new object[] {
            "400%",
            "200%",
            "100%",
            "75%",
            "50%"});
			this.comboBoxScale.Location = new System.Drawing.Point(96, 176);
			this.comboBoxScale.Name = "comboBoxScale";
			this.comboBoxScale.Size = new System.Drawing.Size(88, 20);
			this.comboBoxScale.TabIndex = 2;
			this.comboBoxScale.SelectedIndexChanged += new System.EventHandler(this.comboBoxScale_SelectedIndexChanged);
			// 
			// btnZoomTo11
			// 
			this.btnZoomTo11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnZoomTo11.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
			this.btnZoomTo11.ImageIndex = 3;
			this.btnZoomTo11.ImageList = this.imageList1;
			this.btnZoomTo11.Location = new System.Drawing.Point(66, 173);
			this.btnZoomTo11.Name = "btnZoomTo11";
			this.btnZoomTo11.Size = new System.Drawing.Size(24, 24);
			this.btnZoomTo11.TabIndex = 1;
			this.btnZoomTo11.UseVisualStyleBackColor = true;
			this.btnZoomTo11.Click += new System.EventHandler(this.btnZoomTo11_Click);
			// 
			// btnZoomOut
			// 
			this.btnZoomOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnZoomOut.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
			this.btnZoomOut.ImageIndex = 2;
			this.btnZoomOut.ImageList = this.imageList1;
			this.btnZoomOut.Location = new System.Drawing.Point(36, 173);
			this.btnZoomOut.Name = "btnZoomOut";
			this.btnZoomOut.Size = new System.Drawing.Size(24, 24);
			this.btnZoomOut.TabIndex = 1;
			this.btnZoomOut.UseVisualStyleBackColor = true;
			this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
			// 
			// btnZoomIn
			// 
			this.btnZoomIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnZoomIn.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
			this.btnZoomIn.ImageIndex = 1;
			this.btnZoomIn.ImageList = this.imageList1;
			this.btnZoomIn.Location = new System.Drawing.Point(6, 173);
			this.btnZoomIn.Name = "btnZoomIn";
			this.btnZoomIn.Size = new System.Drawing.Size(24, 24);
			this.btnZoomIn.TabIndex = 1;
			this.btnZoomIn.UseVisualStyleBackColor = true;
			this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
			// 
			// CtrlSymbolPreview
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Name = "CtrlSymbolPreview";
			this.Size = new System.Drawing.Size(190, 205);
			this.Load += new System.EventHandler(this.CtrlSymbolPreview_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxPreview)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnZoomIn;
		private System.Windows.Forms.ComboBox comboBoxScale;
		private System.Windows.Forms.Button btnZoomTo11;
		private System.Windows.Forms.Button btnZoomOut;
		private System.Windows.Forms.CheckBox checkBoxReticle;
		private System.Windows.Forms.RadioButton radioButtonFoldline;
		private System.Windows.Forms.RadioButton radioButtonBeeline;
		private System.Windows.Forms.ImageList imageList1;
		private SymbolPictureBox pictureBoxPreview;
	}
}
