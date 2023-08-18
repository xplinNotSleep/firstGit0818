namespace Table2Spatial
{
    partial class FormDataScheme
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDataScheme));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeCheckScheme = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.propNodeAttribute = new System.Windows.Forms.PropertyGrid();
            this.contextMenuScheme = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu_AddLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_AddGroupLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_SetSchemeSource = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuLayer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu_AddRule = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_DeleteLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_SetDataSource = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuLayerSet = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu_DeleteLayerSet = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_SetLayerSetSource = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuGroupLayer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu_AddLayerToGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_AddLayerSetToGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_DeletGroupLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_SetGroupSource = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuScheme.SuspendLayout();
            this.contextMenuLayer.SuspendLayout();
            this.contextMenuLayerSet.SuspendLayout();
            this.contextMenuGroupLayer.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.treeCheckScheme);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.propNodeAttribute);
            this.splitContainer1.Size = new System.Drawing.Size(588, 491);
            this.splitContainer1.SplitterDistance = 195;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeCheckScheme
            // 
            this.treeCheckScheme.CheckBoxes = true;
            this.treeCheckScheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeCheckScheme.ImageIndex = 0;
            this.treeCheckScheme.ImageList = this.imageList1;
            this.treeCheckScheme.ItemHeight = 18;
            this.treeCheckScheme.Location = new System.Drawing.Point(0, 0);
            this.treeCheckScheme.Name = "treeCheckScheme";
            this.treeCheckScheme.SelectedImageIndex = 0;
            this.treeCheckScheme.Size = new System.Drawing.Size(195, 491);
            this.treeCheckScheme.TabIndex = 0;
            this.treeCheckScheme.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeCheckScheme_AfterCheck);
            this.treeCheckScheme.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeCheckScheme_AfterSelect);
            this.treeCheckScheme.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeCheckScheme_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "scheme");
            this.imageList1.Images.SetKeyName(1, "layer");
            this.imageList1.Images.SetKeyName(2, "rule");
            this.imageList1.Images.SetKeyName(3, "dataset.bmp");
            // 
            // propNodeAttribute
            // 
            this.propNodeAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propNodeAttribute.Location = new System.Drawing.Point(0, 0);
            this.propNodeAttribute.Name = "propNodeAttribute";
            this.propNodeAttribute.Size = new System.Drawing.Size(389, 491);
            this.propNodeAttribute.TabIndex = 0;
            this.propNodeAttribute.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propNodeAttribute_PropertyValueChanged);
            // 
            // contextMenuScheme
            // 
            this.contextMenuScheme.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_AddLayer,
            this.menu_AddGroupLayer,
            this.toolStripMenuItem1,
            this.menu_SetSchemeSource});
            this.contextMenuScheme.Name = "contextMenuStrip1";
            this.contextMenuScheme.Size = new System.Drawing.Size(149, 76);
            // 
            // menu_AddLayer
            // 
            this.menu_AddLayer.Name = "menu_AddLayer";
            this.menu_AddLayer.Size = new System.Drawing.Size(148, 22);
            this.menu_AddLayer.Text = "添加图层";
            this.menu_AddLayer.Click += new System.EventHandler(this.menu_AddLayer_Click);
            // 
            // menu_AddGroupLayer
            // 
            this.menu_AddGroupLayer.Name = "menu_AddGroupLayer";
            this.menu_AddGroupLayer.Size = new System.Drawing.Size(148, 22);
            this.menu_AddGroupLayer.Text = "添加管线类型";
            this.menu_AddGroupLayer.Click += new System.EventHandler(this.menu_AddGroupLayer_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(145, 6);
            // 
            // menu_SetSchemeSource
            // 
            this.menu_SetSchemeSource.Name = "menu_SetSchemeSource";
            this.menu_SetSchemeSource.Size = new System.Drawing.Size(148, 22);
            this.menu_SetSchemeSource.Text = "设置数据源";
            this.menu_SetSchemeSource.Click += new System.EventHandler(this.menu_SetSchemeSource_Click);
            // 
            // contextMenuLayer
            // 
            this.contextMenuLayer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_AddRule,
            this.menu_DeleteLayer,
            this.toolStripMenuItem2,
            this.menu_SetDataSource});
            this.contextMenuLayer.Name = "contextMenuRule";
            this.contextMenuLayer.Size = new System.Drawing.Size(161, 76);
            // 
            // menu_AddRule
            // 
            this.menu_AddRule.Name = "menu_AddRule";
            this.menu_AddRule.Size = new System.Drawing.Size(160, 22);
            this.menu_AddRule.Text = "添加图层";
            this.menu_AddRule.Click += new System.EventHandler(this.menu_AddRule_Click);
            // 
            // menu_DeleteLayer
            // 
            this.menu_DeleteLayer.Name = "menu_DeleteLayer";
            this.menu_DeleteLayer.Size = new System.Drawing.Size(160, 22);
            this.menu_DeleteLayer.Text = "删除图层";
            this.menu_DeleteLayer.Click += new System.EventHandler(this.menu_DeleteLayer_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(157, 6);
            // 
            // menu_SetDataSource
            // 
            this.menu_SetDataSource.Name = "menu_SetDataSource";
            this.menu_SetDataSource.Size = new System.Drawing.Size(160, 22);
            this.menu_SetDataSource.Text = "设置图层数据源";
            this.menu_SetDataSource.Click += new System.EventHandler(this.menu_SetDataSource_Click);
            // 
            // contextMenuLayerSet
            // 
            this.contextMenuLayerSet.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_DeleteLayerSet,
            this.menu_SetLayerSetSource});
            this.contextMenuLayerSet.Name = "contextMenuRule";
            this.contextMenuLayerSet.Size = new System.Drawing.Size(149, 48);
            // 
            // menu_DeleteLayerSet
            // 
            this.menu_DeleteLayerSet.Name = "menu_DeleteLayerSet";
            this.menu_DeleteLayerSet.Size = new System.Drawing.Size(148, 22);
            this.menu_DeleteLayerSet.Text = "删除管线小类";
            this.menu_DeleteLayerSet.Click += new System.EventHandler(this.menu_DeleteRule_Click);
            // 
            // menu_SetLayerSetSource
            // 
            this.menu_SetLayerSetSource.Name = "menu_SetLayerSetSource";
            this.menu_SetLayerSetSource.Size = new System.Drawing.Size(148, 22);
            this.menu_SetLayerSetSource.Text = "设置数据源";
            this.menu_SetLayerSetSource.Click += new System.EventHandler(this.menu_SetLayerSetSource_Click);
            // 
            // contextMenuGroupLayer
            // 
            this.contextMenuGroupLayer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_AddLayerToGroup,
            this.menu_AddLayerSetToGroup,
            this.menu_DeletGroupLayer,
            this.toolStripSeparator1,
            this.menu_SetGroupSource});
            this.contextMenuGroupLayer.Name = "contextMenuStrip1";
            this.contextMenuGroupLayer.Size = new System.Drawing.Size(149, 98);
            // 
            // menu_AddLayerToGroup
            // 
            this.menu_AddLayerToGroup.Name = "menu_AddLayerToGroup";
            this.menu_AddLayerToGroup.Size = new System.Drawing.Size(148, 22);
            this.menu_AddLayerToGroup.Text = "添加图层";
            this.menu_AddLayerToGroup.Click += new System.EventHandler(this.menu_AddLayerToGroup_Click);
            // 
            // menu_AddLayerSetToGroup
            // 
            this.menu_AddLayerSetToGroup.Name = "menu_AddLayerSetToGroup";
            this.menu_AddLayerSetToGroup.Size = new System.Drawing.Size(148, 22);
            this.menu_AddLayerSetToGroup.Text = "添加管线小类";
            this.menu_AddLayerSetToGroup.Click += new System.EventHandler(this.menu_AddLayerSetToGroup_Click);
            // 
            // menu_DeletGroupLayer
            // 
            this.menu_DeletGroupLayer.Name = "menu_DeletGroupLayer";
            this.menu_DeletGroupLayer.Size = new System.Drawing.Size(148, 22);
            this.menu_DeletGroupLayer.Text = "删除管线类型";
            this.menu_DeletGroupLayer.Click += new System.EventHandler(this.menu_DeletGroupLayer_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // menu_SetGroupSource
            // 
            this.menu_SetGroupSource.Name = "menu_SetGroupSource";
            this.menu_SetGroupSource.Size = new System.Drawing.Size(148, 22);
            this.menu_SetGroupSource.Text = "设置数据源";
            this.menu_SetGroupSource.Click += new System.EventHandler(this.menu_SetGroupSource_Click);
            // 
            // FormDataScheme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 491);         
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FormDataScheme";
         
            this.TabText = "转换方案";
            this.Text = "转换方案";
            this.Load += new System.EventHandler(this.FormDataScheme_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuScheme.ResumeLayout(false);
            this.contextMenuLayer.ResumeLayout(false);
            this.contextMenuLayerSet.ResumeLayout(false);
            this.contextMenuGroupLayer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeCheckScheme;
        private System.Windows.Forms.ContextMenuStrip contextMenuScheme;
        private System.Windows.Forms.ToolStripMenuItem menu_AddLayer;
        private System.Windows.Forms.PropertyGrid propNodeAttribute;
        private System.Windows.Forms.ContextMenuStrip contextMenuLayer;
        private System.Windows.Forms.ToolStripMenuItem menu_AddRule;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menu_SetDataSource;
        private System.Windows.Forms.ContextMenuStrip contextMenuLayerSet;
        private System.Windows.Forms.ToolStripMenuItem menu_DeleteLayerSet;
        private System.Windows.Forms.ToolStripMenuItem menu_DeleteLayer;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem menu_SetSchemeSource;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem menu_AddGroupLayer;
        private System.Windows.Forms.ContextMenuStrip contextMenuGroupLayer;
        private System.Windows.Forms.ToolStripMenuItem menu_AddLayerToGroup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menu_SetGroupSource;
        private System.Windows.Forms.ToolStripMenuItem menu_DeletGroupLayer;
        private System.Windows.Forms.ToolStripMenuItem menu_AddLayerSetToGroup;
        private System.Windows.Forms.ToolStripMenuItem menu_SetLayerSetSource;

    }
}