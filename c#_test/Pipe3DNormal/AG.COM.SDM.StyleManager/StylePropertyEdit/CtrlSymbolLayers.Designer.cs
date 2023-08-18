namespace AG.COM.SDM.StylePropertyEdit
{
	partial class CtrlSymbolLayers
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrlSymbolLayers));
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.listViewSymbolLayer = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.btnDown = new System.Windows.Forms.Button();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.btnUp = new System.Windows.Forms.Button();
			this.btnDel = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.listViewSymbolLayer);
			this.groupBox3.Controls.Add(this.btnDown);
			this.groupBox3.Controls.Add(this.btnUp);
			this.groupBox3.Controls.Add(this.btnDel);
			this.groupBox3.Controls.Add(this.btnAdd);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox3.Location = new System.Drawing.Point(0, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(190, 216);
			this.groupBox3.TabIndex = 1;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "图层";
			// 
			// listViewSymbolLayer
			// 
			this.listViewSymbolLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.listViewSymbolLayer.CheckBoxes = true;
			this.listViewSymbolLayer.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.listViewSymbolLayer.FullRowSelect = true;
			this.listViewSymbolLayer.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listViewSymbolLayer.HideSelection = false;
			this.listViewSymbolLayer.Location = new System.Drawing.Point(6, 20);
			this.listViewSymbolLayer.MultiSelect = false;
			this.listViewSymbolLayer.Name = "listViewSymbolLayer";
			this.listViewSymbolLayer.Size = new System.Drawing.Size(178, 160);
			this.listViewSymbolLayer.TabIndex = 5;
			this.listViewSymbolLayer.UseCompatibleStateImageBehavior = false;
			this.listViewSymbolLayer.View = System.Windows.Forms.View.Details;
			this.listViewSymbolLayer.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewSymbolLayer_ItemChecked);
			this.listViewSymbolLayer.SelectedIndexChanged += new System.EventHandler(this.listViewSymbolLayer_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 168;
			// 
			// btnDown
			// 
			this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDown.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
			this.btnDown.ImageIndex = 3;
			this.btnDown.ImageList = this.imageList1;
			this.btnDown.Location = new System.Drawing.Point(128, 186);
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(24, 24);
			this.btnDown.TabIndex = 4;
			this.btnDown.UseVisualStyleBackColor = true;
			this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "Add.png");
			this.imageList1.Images.SetKeyName(1, "Del.png");
			this.imageList1.Images.SetKeyName(2, "Up.png");
			this.imageList1.Images.SetKeyName(3, "Down.png");
			// 
			// btnUp
			// 
			this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnUp.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
			this.btnUp.ImageIndex = 2;
			this.btnUp.ImageList = this.imageList1;
			this.btnUp.Location = new System.Drawing.Point(98, 186);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(24, 24);
			this.btnUp.TabIndex = 3;
			this.btnUp.UseVisualStyleBackColor = true;
			this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
			// 
			// btnDel
			// 
			this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.btnDel.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.btnDel.ImageIndex = 1;
			this.btnDel.ImageList = this.imageList1;
			this.btnDel.Location = new System.Drawing.Point(68, 186);
			this.btnDel.Name = "btnDel";
			this.btnDel.Size = new System.Drawing.Size(24, 24);
			this.btnDel.TabIndex = 2;
			this.btnDel.UseVisualStyleBackColor = true;
			this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
			this.btnAdd.ImageIndex = 0;
			this.btnAdd.ImageList = this.imageList1;
			this.btnAdd.Location = new System.Drawing.Point(38, 186);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(24, 24);
			this.btnAdd.TabIndex = 1;
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// CtrlSymbolLayers
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox3);
			this.Name = "CtrlSymbolLayers";
			this.Size = new System.Drawing.Size(190, 216);
			this.Load += new System.EventHandler(this.CtrlSymbolLayers_Load);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.Button btnDel;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ListView listViewSymbolLayer;
		private System.Windows.Forms.ColumnHeader columnHeader1;
	}
}
