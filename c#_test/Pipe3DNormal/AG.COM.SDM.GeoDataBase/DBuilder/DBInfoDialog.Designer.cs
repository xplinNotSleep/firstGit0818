namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    partial class DBInfoDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBInfoDialog));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeDataView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.propNodeAttribute = new System.Windows.Forms.PropertyGrid();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolBtnNew = new System.Windows.Forms.ToolStripButton();
            this.toolBtnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolBtnSave = new System.Windows.Forms.ToolStripButton();
            this.toolBtnSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtnCreateDataBase = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolClose = new System.Windows.Forms.ToolStripButton();
            this.MenuDataBase = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu_NewDataBase = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_NewSubjectDB = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_NewDomains = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_NewDataSet = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_NewFeatureClass = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_NewAnnotion = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_NewTable = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_NewField = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.MenuDataBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.panel1.Controls.Add(this.lblInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(699, 53);
            this.panel1.TabIndex = 1;
            this.panel1.Visible = false;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInfo.Location = new System.Drawing.Point(30, 18);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(70, 14);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "信息说明:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.splitContainer1);
            this.panel3.Controls.Add(this.toolStrip1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 53);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(699, 561);
            this.panel3.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeDataView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.propNodeAttribute);
            this.splitContainer1.Size = new System.Drawing.Size(699, 536);
            this.splitContainer1.SplitterDistance = 283;
            this.splitContainer1.TabIndex = 2;
            // 
            // treeDataView
            // 
            this.treeDataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeDataView.ImageIndex = 8;
            this.treeDataView.ImageList = this.imageList1;
            this.treeDataView.ItemHeight = 18;
            this.treeDataView.Location = new System.Drawing.Point(0, 0);
            this.treeDataView.Name = "treeDataView";
            this.treeDataView.SelectedImageIndex = 7;
            this.treeDataView.ShowLines = false;
            this.treeDataView.Size = new System.Drawing.Size(281, 534);
            this.treeDataView.TabIndex = 0;
            this.treeDataView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeDataView_AfterCheck);
            this.treeDataView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeDataView_AfterSelect);
            this.treeDataView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeDataView_MouseDown);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Geodatabase.png");
            this.imageList1.Images.SetKeyName(1, "subject.jpg");
            this.imageList1.Images.SetKeyName(2, "dataset.png");
            this.imageList1.Images.SetKeyName(3, "point.png");
            this.imageList1.Images.SetKeyName(4, "polyline.png");
            this.imageList1.Images.SetKeyName(5, "polygon.png");
            this.imageList1.Images.SetKeyName(6, "GeodatabaseTable_C_16.png");
            this.imageList1.Images.SetKeyName(7, "field.gif");
            this.imageList1.Images.SetKeyName(8, "anno.png");
            this.imageList1.Images.SetKeyName(9, "attrtable.bmp");
            // 
            // propNodeAttribute
            // 
            this.propNodeAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propNodeAttribute.HelpVisible = false;
            this.propNodeAttribute.Location = new System.Drawing.Point(0, 0);
            this.propNodeAttribute.Name = "propNodeAttribute";
            this.propNodeAttribute.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propNodeAttribute.Size = new System.Drawing.Size(410, 534);
            this.propNodeAttribute.TabIndex = 0;
            this.propNodeAttribute.ToolbarVisible = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnNew,
            this.toolBtnOpen,
            this.toolBtnSave,
            this.toolBtnSaveAs,
            this.toolStripSeparator1,
            this.toolBtnCreateDataBase,
            this.toolStripSeparator2,
            this.toolClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(699, 25);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip2";
            // 
            // toolBtnNew
            // 
            this.toolBtnNew.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnNew.Image")));
            this.toolBtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnNew.Name = "toolBtnNew";
            this.toolBtnNew.Size = new System.Drawing.Size(52, 22);
            this.toolBtnNew.Text = "新建";
            this.toolBtnNew.Click += new System.EventHandler(this.toolBtnNew_Click);
            // 
            // toolBtnOpen
            // 
            this.toolBtnOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnOpen.Image")));
            this.toolBtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnOpen.Name = "toolBtnOpen";
            this.toolBtnOpen.Size = new System.Drawing.Size(52, 22);
            this.toolBtnOpen.Text = "打开";
            this.toolBtnOpen.Click += new System.EventHandler(this.toolBtnOpen_Click);
            // 
            // toolBtnSave
            // 
            this.toolBtnSave.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnSave.Image")));
            this.toolBtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnSave.Name = "toolBtnSave";
            this.toolBtnSave.Size = new System.Drawing.Size(52, 22);
            this.toolBtnSave.Text = "保存";
            this.toolBtnSave.Click += new System.EventHandler(this.toolBtnSave_Click);
            // 
            // toolBtnSaveAs
            // 
            this.toolBtnSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnSaveAs.Image")));
            this.toolBtnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnSaveAs.Name = "toolBtnSaveAs";
            this.toolBtnSaveAs.Size = new System.Drawing.Size(64, 22);
            this.toolBtnSaveAs.Text = "另存为";
            this.toolBtnSaveAs.Click += new System.EventHandler(this.toolBtnSaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolBtnCreateDataBase
            // 
            this.toolBtnCreateDataBase.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnCreateDataBase.Image")));
            this.toolBtnCreateDataBase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnCreateDataBase.Name = "toolBtnCreateDataBase";
            this.toolBtnCreateDataBase.Size = new System.Drawing.Size(139, 22);
            this.toolBtnCreateDataBase.Text = "创建空白mdb数据库";
            this.toolBtnCreateDataBase.Click += new System.EventHandler(this.toolBtnCreateDataBase_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolClose
            // 
            this.toolClose.BackColor = System.Drawing.SystemColors.Control;
            this.toolClose.Image = ((System.Drawing.Image)(resources.GetObject("toolClose.Image")));
            this.toolClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolClose.Name = "toolClose";
            this.toolClose.Size = new System.Drawing.Size(52, 22);
            this.toolClose.Text = "关闭";
            this.toolClose.Click += new System.EventHandler(this.toolClose_Click);
            // 
            // MenuDataBase
            // 
            this.MenuDataBase.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_NewDataBase,
            this.menu_NewSubjectDB,
            this.menu_NewDomains,
            this.menu_NewDataSet,
            this.menu_NewFeatureClass,
            this.menu_NewAnnotion,
            this.menu_NewTable,
            this.menu_NewField,
            this.toolStripMenuItem3,
            this.menu_Copy,
            this.menu_Paste,
            this.toolStripMenuItem2,
            this.menu_Delete});
            this.MenuDataBase.Name = "contextMenuStrip1";
            this.MenuDataBase.Size = new System.Drawing.Size(137, 258);
            // 
            // menu_NewDataBase
            // 
            this.menu_NewDataBase.Name = "menu_NewDataBase";
            this.menu_NewDataBase.Size = new System.Drawing.Size(136, 22);
            this.menu_NewDataBase.Text = "新建数据库";
            this.menu_NewDataBase.Click += new System.EventHandler(this.menu_NewDataBase_Click);
            // 
            // menu_NewSubjectDB
            // 
            this.menu_NewSubjectDB.Name = "menu_NewSubjectDB";
            this.menu_NewSubjectDB.Size = new System.Drawing.Size(136, 22);
            this.menu_NewSubjectDB.Text = "新建专题库";
            this.menu_NewSubjectDB.Visible = false;
            this.menu_NewSubjectDB.Click += new System.EventHandler(this.menu_NewSubjectDB_Click);
            // 
            // menu_NewDomains
            // 
            this.menu_NewDomains.Name = "menu_NewDomains";
            this.menu_NewDomains.Size = new System.Drawing.Size(136, 22);
            this.menu_NewDomains.Text = "新建属性域";
            this.menu_NewDomains.Visible = false;
            this.menu_NewDomains.Click += new System.EventHandler(this.menu_NewDomains_Click);
            // 
            // menu_NewDataSet
            // 
            this.menu_NewDataSet.Name = "menu_NewDataSet";
            this.menu_NewDataSet.Size = new System.Drawing.Size(136, 22);
            this.menu_NewDataSet.Text = "新建数据集";
            this.menu_NewDataSet.Visible = false;
            this.menu_NewDataSet.Click += new System.EventHandler(this.menu_NewDataSet_Click);
            // 
            // menu_NewFeatureClass
            // 
            this.menu_NewFeatureClass.Name = "menu_NewFeatureClass";
            this.menu_NewFeatureClass.Size = new System.Drawing.Size(136, 22);
            this.menu_NewFeatureClass.Text = "新建要素类";
            this.menu_NewFeatureClass.Visible = false;
            this.menu_NewFeatureClass.Click += new System.EventHandler(this.menu_NewFeatureClass_Click);
            // 
            // menu_NewAnnotion
            // 
            this.menu_NewAnnotion.Enabled = false;
            this.menu_NewAnnotion.Name = "menu_NewAnnotion";
            this.menu_NewAnnotion.Size = new System.Drawing.Size(136, 22);
            this.menu_NewAnnotion.Text = "新建注记";
            this.menu_NewAnnotion.Click += new System.EventHandler(this.menu_NewAnnotion_Click);
            // 
            // menu_NewTable
            // 
            this.menu_NewTable.Name = "menu_NewTable";
            this.menu_NewTable.Size = new System.Drawing.Size(136, 22);
            this.menu_NewTable.Text = "新建属性表";
            this.menu_NewTable.Visible = false;
            this.menu_NewTable.Click += new System.EventHandler(this.menu_NewTable_Click);
            // 
            // menu_NewField
            // 
            this.menu_NewField.Name = "menu_NewField";
            this.menu_NewField.Size = new System.Drawing.Size(136, 22);
            this.menu_NewField.Text = "新增字段";
            this.menu_NewField.Visible = false;
            this.menu_NewField.Click += new System.EventHandler(this.menu_NewField_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(133, 6);
            // 
            // menu_Copy
            // 
            this.menu_Copy.Name = "menu_Copy";
            this.menu_Copy.Size = new System.Drawing.Size(136, 22);
            this.menu_Copy.Text = "复制";
            this.menu_Copy.Click += new System.EventHandler(this.menu_Copy_Click);
            // 
            // menu_Paste
            // 
            this.menu_Paste.Name = "menu_Paste";
            this.menu_Paste.Size = new System.Drawing.Size(136, 22);
            this.menu_Paste.Text = "粘贴";
            this.menu_Paste.Click += new System.EventHandler(this.menu_Paste_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(133, 6);
            // 
            // menu_Delete
            // 
            this.menu_Delete.Name = "menu_Delete";
            this.menu_Delete.Size = new System.Drawing.Size(136, 22);
            this.menu_Delete.Text = "删除";
            this.menu_Delete.Click += new System.EventHandler(this.menu_Delete_Click);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "database.gif");
            this.imageList2.Images.SetKeyName(1, "subject.jpg");
            this.imageList2.Images.SetKeyName(2, "dataset.png");
            this.imageList2.Images.SetKeyName(3, "point.png");
            this.imageList2.Images.SetKeyName(4, "polyline.png");
            this.imageList2.Images.SetKeyName(5, "polygon.png");
            this.imageList2.Images.SetKeyName(6, "anno.png");
            this.imageList2.Images.SetKeyName(7, "field.gif");
            this.imageList2.Images.SetKeyName(8, "attrtable.bmp");
            // 
            // DBInfoDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "DBInfoDialog";
            this.Size = new System.Drawing.Size(699, 614);
            this.Text = "建库方案管理";
            this.Load += new System.EventHandler(this.DBInfoDialog_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.MenuDataBase.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeDataView;
        private System.Windows.Forms.PropertyGrid propNodeAttribute;
        private System.Windows.Forms.ContextMenuStrip MenuDataBase;
        private System.Windows.Forms.ToolStripMenuItem menu_NewDataBase;
        private System.Windows.Forms.ToolStripMenuItem menu_NewSubjectDB;
        private System.Windows.Forms.ToolStripMenuItem menu_NewDataSet;
        private System.Windows.Forms.ToolStripMenuItem menu_NewFeatureClass;
        private System.Windows.Forms.ToolStripMenuItem menu_NewTable;
        private System.Windows.Forms.ToolStripMenuItem menu_NewField;
        private System.Windows.Forms.ToolStripMenuItem menu_Delete;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem menu_Copy;
        private System.Windows.Forms.ToolStripMenuItem menu_Paste;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menu_NewDomains;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolBtnNew;
        private System.Windows.Forms.ToolStripButton toolBtnOpen;
        private System.Windows.Forms.ToolStripButton toolBtnSave;
        private System.Windows.Forms.ToolStripButton toolBtnSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolBtnCreateDataBase;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolClose;
        private System.Windows.Forms.ToolStripMenuItem menu_NewAnnotion;
        private System.Windows.Forms.ImageList imageList2;
    }
}