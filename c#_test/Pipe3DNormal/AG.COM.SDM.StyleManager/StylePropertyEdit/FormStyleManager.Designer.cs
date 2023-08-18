namespace AG.COM.SDM.StylePropertyEdit
{
    partial class FormStyleManager
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStyleManager));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewStyle = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnSytle = new System.Windows.Forms.Button();
            this.listViewSymbol = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbDel = new System.Windows.Forms.ToolStripButton();
            this.tsbProperty = new System.Windows.Forms.ToolStripButton();
            this.tsbSetNameAndCategory = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLarge = new System.Windows.Forms.ToolStripButton();
            this.tsbSmall = new System.Windows.Forms.ToolStripButton();
            this.tsbDetail = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewStyle);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnSytle);
            this.splitContainer1.Panel2.Controls.Add(this.listViewSymbol);
            this.splitContainer1.Panel2.Controls.Add(this.comboBoxCategory);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(722, 474);
            this.splitContainer1.SplitterDistance = 227;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeViewStyle
            // 
            this.treeViewStyle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewStyle.HideSelection = false;
            this.treeViewStyle.ImageIndex = 0;
            this.treeViewStyle.ImageList = this.imageList1;
            this.treeViewStyle.LabelEdit = true;
            this.treeViewStyle.Location = new System.Drawing.Point(0, 0);
            this.treeViewStyle.Name = "treeViewStyle";
            this.treeViewStyle.SelectedImageIndex = 3;
            this.treeViewStyle.Size = new System.Drawing.Size(227, 474);
            this.treeViewStyle.TabIndex = 0;
            this.treeViewStyle.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewStyle_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "001.png");
            this.imageList1.Images.SetKeyName(1, "002.png");
            this.imageList1.Images.SetKeyName(2, "003.png");
            this.imageList1.Images.SetKeyName(3, "004.png");
            // 
            // btnSytle
            // 
            this.btnSytle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSytle.Location = new System.Drawing.Point(388, 29);
            this.btnSytle.Name = "btnSytle";
            this.btnSytle.Size = new System.Drawing.Size(100, 23);
            this.btnSytle.TabIndex = 4;
            this.btnSytle.Text = "符号库";
            this.btnSytle.UseVisualStyleBackColor = true;
            this.btnSytle.Click += new System.EventHandler(this.btnSytle_Click);
            // 
            // listViewSymbol
            // 
            this.listViewSymbol.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewSymbol.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewSymbol.FullRowSelect = true;
            this.listViewSymbol.HideSelection = false;
            this.listViewSymbol.Location = new System.Drawing.Point(0, 57);
            this.listViewSymbol.Name = "listViewSymbol";
            this.listViewSymbol.Size = new System.Drawing.Size(491, 417);
            this.listViewSymbol.TabIndex = 3;
            this.listViewSymbol.UseCompatibleStateImageBehavior = false;
            this.listViewSymbol.SelectedIndexChanged += new System.EventHandler(this.listViewSymbol_SelectedIndexChanged);
            this.listViewSymbol.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewSymbol_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "名称";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "编号";
            this.columnHeader2.Width = 50;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "类别";
            this.columnHeader3.Width = 200;
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(50, 31);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(332, 20);
            this.comboBoxCategory.TabIndex = 2;
            this.comboBoxCategory.SelectedIndexChanged += new System.EventHandler(this.comboBoxCategory_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "类别：";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.tsbDel,
            this.tsbProperty,
            this.tsbSetNameAndCategory,
            this.toolStripSeparator1,
            this.tsbLarge,
            this.tsbSmall,
            this.tsbDetail});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(491, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAdd
            // 
            this.tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbAdd.Image")));
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.White;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(23, 22);
            this.tsbAdd.Text = "添加";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // tsbDel
            // 
            this.tsbDel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDel.Image = ((System.Drawing.Image)(resources.GetObject("tsbDel.Image")));
            this.tsbDel.ImageTransparentColor = System.Drawing.Color.White;
            this.tsbDel.Name = "tsbDel";
            this.tsbDel.Size = new System.Drawing.Size(23, 22);
            this.tsbDel.Text = "删除";
            this.tsbDel.Click += new System.EventHandler(this.tsbDel_Click);
            // 
            // tsbProperty
            // 
            this.tsbProperty.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbProperty.Image = ((System.Drawing.Image)(resources.GetObject("tsbProperty.Image")));
            this.tsbProperty.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbProperty.Name = "tsbProperty";
            this.tsbProperty.Size = new System.Drawing.Size(23, 22);
            this.tsbProperty.Text = "属性";
            this.tsbProperty.Click += new System.EventHandler(this.tsbProperty_Click);
            // 
            // tsbSetNameAndCategory
            // 
            this.tsbSetNameAndCategory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSetNameAndCategory.Image = ((System.Drawing.Image)(resources.GetObject("tsbSetNameAndCategory.Image")));
            this.tsbSetNameAndCategory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSetNameAndCategory.Name = "tsbSetNameAndCategory";
            this.tsbSetNameAndCategory.Size = new System.Drawing.Size(23, 22);
            this.tsbSetNameAndCategory.Text = "设置分组和名称";
            this.tsbSetNameAndCategory.Click += new System.EventHandler(this.tsbSetNameAndGroup_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbLarge
            // 
            this.tsbLarge.Checked = true;
            this.tsbLarge.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbLarge.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLarge.Image = ((System.Drawing.Image)(resources.GetObject("tsbLarge.Image")));
            this.tsbLarge.ImageTransparentColor = System.Drawing.Color.White;
            this.tsbLarge.Name = "tsbLarge";
            this.tsbLarge.Size = new System.Drawing.Size(23, 22);
            this.tsbLarge.Text = "大图标";
            this.tsbLarge.Click += new System.EventHandler(this.tsbView_Click);
            // 
            // tsbSmall
            // 
            this.tsbSmall.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSmall.Image = ((System.Drawing.Image)(resources.GetObject("tsbSmall.Image")));
            this.tsbSmall.ImageTransparentColor = System.Drawing.Color.White;
            this.tsbSmall.Name = "tsbSmall";
            this.tsbSmall.Size = new System.Drawing.Size(23, 22);
            this.tsbSmall.Text = "小图标";
            this.tsbSmall.Click += new System.EventHandler(this.tsbView_Click);
            // 
            // tsbDetail
            // 
            this.tsbDetail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDetail.Image = ((System.Drawing.Image)(resources.GetObject("tsbDetail.Image")));
            this.tsbDetail.ImageTransparentColor = System.Drawing.Color.White;
            this.tsbDetail.Name = "tsbDetail";
            this.tsbDetail.Size = new System.Drawing.Size(23, 22);
            this.tsbDetail.Text = "详细信息";
            this.tsbDetail.Click += new System.EventHandler(this.tsbView_Click);
            // 
            // FormStyleManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 474);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormStyleManager";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "符号管理器";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormStyleManager_FormClosed);
            this.Load += new System.EventHandler(this.FormStyleManager_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewStyle;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripButton tsbDel;
        private System.Windows.Forms.ToolStripButton tsbProperty;
        private System.Windows.Forms.ListView listViewSymbol;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbSmall;
        private System.Windows.Forms.ToolStripButton tsbLarge;
        private System.Windows.Forms.ToolStripButton tsbDetail;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnSytle;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ToolStripButton tsbSetNameAndCategory;
    }
}