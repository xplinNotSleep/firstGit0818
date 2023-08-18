using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using AG.COM.SDM.Catalog.DataItems;
using AG.COM.SDM.Catalog.Filters;
using ESRI.ArcGIS;

namespace AG.COM.SDM.Catalog
{
    /// <summary>
    /// 数据加载窗体类，主要用于加载数据项，类似于ArcGIS中的数据加载对象。
    /// </summary>
    public class FormDataBrowser : DevExpress.XtraEditors.XtraForm, IDataBrowser
    {
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colAliasName;
        private System.Windows.Forms.ColumnHeader colType;
        private DevExpress.XtraEditors.SimpleButton btOK;
        private DevExpress.XtraEditors.SimpleButton btCancel;
        private System.Windows.Forms.ListView lvwCategories;
        private System.Windows.Forms.ListView lvwItems;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.ComboBoxEdit cboFilters;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private ToolStrip toolStrip1;
        private ToolStripButton btUpLevel;
        private ToolStripButton btLocateTo;
        private ToolStripButton btDelete;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btEdit;
        private ToolStripButton btViewDetail;
        private ToolStripButton btViewList;
        private ToolStripButton btViewIcon;
        private ImageList istToolbar;
        private Panel pnlLeft;
        private Panel panel1;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ListViewItem lviDisk;
        private System.Windows.Forms.ListViewItem lviDatabase;
        private System.Windows.Forms.ListViewItem lviService;

        private static DataItem m_LastLocation = null;

        /// <summary>
        /// 初始化数据加载窗体实例对象
        /// </summary>
        public FormDataBrowser()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            //设置图标
            this.lvwItems.SmallImageList = m_Images.ImageList;
            this.lvwItems.LargeImageList = m_Images.ImageList;
            this.lvwItems.ShowItemToolTips = true;

            //lvwColumnSorter = new ListViewColumnSorter();
            //this.lvwItems.ListViewItemSorter = lvwColumnSorter;
            ListViewItemSorterBinder.BindListView(lvwItems);

            m_InitFlag = true;
            //初始化过滤器         
            cboFilters.Properties.Items.Add(new DefaultFilter());
            this.cboFilters.SelectedIndex = 0;

            m_InitFlag = false;
        }

        /// <summary>
        /// 初始化数据源菜单
        /// </summary>
        private void InitCategories()
        {
            lviDisk = new ListViewItem("磁盘文件", 0);
            lviDatabase = new ListViewItem("数据库", 1);
            lviService = new ListViewItem("地图服务", 2);

            lviDisk.Tag = "disk";
            lviDatabase.Tag = "database";
            lviService.Tag = "service";

            switch (CategoriesType)
            {
                case EnumCategoriesType.Database:
                    lvwCategories.Items.Add(lviDatabase);
                    break;
                case EnumCategoriesType.Disk:
                    lvwCategories.Items.Add(lviDisk);
                    break;
                case EnumCategoriesType.Service:
                    lvwCategories.Items.Add(lviService);
                    break;
                case EnumCategoriesType.DiskAndService:
                    lvwCategories.Items.Add(lviDisk);
                    lvwCategories.Items.Add(lviService);
                    break;
                case EnumCategoriesType.DiskAndDatabase:
                    lvwCategories.Items.Add(lviDisk);
                    lvwCategories.Items.Add(lviDatabase);
                    break;
                case EnumCategoriesType.DatabaseAndService:
                    lvwCategories.Items.Add(lviDatabase);
                    lvwCategories.Items.Add(lviService);
                    break;
                case EnumCategoriesType.Both:
                default:
                    lvwCategories.Items.Add(lviDisk);
                    lvwCategories.Items.Add(lviDatabase);
                    lvwCategories.Items.Add(lviService);
                    break;
            }
        }

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            //释放时保存浏览过历史记录
            HistoryKeeperInstance.Instance.SaveHistories();

            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDataBrowser));
            this.lvwCategories = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.lvwItems = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAliasName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btOK = new DevExpress.XtraEditors.SimpleButton();
            this.btCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.cboFilters = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btUpLevel = new System.Windows.Forms.ToolStripButton();
            this.btLocateTo = new System.Windows.Forms.ToolStripButton();
            this.btDelete = new System.Windows.Forms.ToolStripButton();
            this.btEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btViewDetail = new System.Windows.Forms.ToolStripButton();
            this.btViewList = new System.Windows.Forms.ToolStripButton();
            this.btViewIcon = new System.Windows.Forms.ToolStripButton();
            this.istToolbar = new System.Windows.Forms.ImageList();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFilters.Properties)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvwCategories
            // 
            this.lvwCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwCategories.HideSelection = false;
            this.lvwCategories.LargeImageList = this.imageList1;
            this.lvwCategories.Location = new System.Drawing.Point(0, 0);
            this.lvwCategories.MultiSelect = false;
            this.lvwCategories.Name = "lvwCategories";
            this.lvwCategories.Size = new System.Drawing.Size(90, 459);
            this.lvwCategories.SmallImageList = this.imageList1;
            this.lvwCategories.TabIndex = 0;
            this.lvwCategories.UseCompatibleStateImageBehavior = false;
            this.lvwCategories.SelectedIndexChanged += new System.EventHandler(this.lvwCategories_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder.bmp");
            this.imageList1.Images.SetKeyName(1, "database.bmp");
            this.imageList1.Images.SetKeyName(2, "server.bmp");
            this.imageList1.Images.SetKeyName(3, "his.bmp");
            // 
            // lvwItems
            // 
            this.lvwItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colAliasName,
            this.colType});
            this.lvwItems.HideSelection = false;
            this.lvwItems.Location = new System.Drawing.Point(6, 33);
            this.lvwItems.Name = "lvwItems";
            this.lvwItems.ShowItemToolTips = true;
            this.lvwItems.Size = new System.Drawing.Size(560, 353);
            this.lvwItems.TabIndex = 1;
            this.lvwItems.UseCompatibleStateImageBehavior = false;
            this.lvwItems.View = System.Windows.Forms.View.Details;
            this.lvwItems.SelectedIndexChanged += new System.EventHandler(this.lvwItems_SelectedIndexChanged);
            this.lvwItems.DoubleClick += new System.EventHandler(this.lvwItems_DoubleClick);
            // 
            // colName
            // 
            this.colName.Text = "名称";
            this.colName.Width = 197;
            // 
            // colAliasName
            // 
            this.colAliasName.Text = "别名";
            this.colAliasName.Width = 122;
            // 
            // colType
            // 
            this.colType.Text = "类型";
            this.colType.Width = 78;
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(492, 391);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(62, 24);
            this.btOK.TabIndex = 2;
            this.btOK.Text = "确定";
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(492, 423);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(62, 23);
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "取消";
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(43, 393);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(443, 20);
            this.txtName.TabIndex = 3;
            // 
            // cboFilters
            // 
            this.cboFilters.Location = new System.Drawing.Point(43, 425);
            this.cboFilters.Name = "cboFilters";
            this.cboFilters.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboFilters.Size = new System.Drawing.Size(443, 20);
            this.cboFilters.TabIndex = 4;
            this.cboFilters.SelectedIndexChanged += new System.EventHandler(this.cboFilters_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 426);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "类型:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(-3, 392);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "名称:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btUpLevel,
            this.btLocateTo,
            this.btDelete,
            this.btEdit,
            this.toolStripSeparator1,
            this.btViewDetail,
            this.btViewList,
            this.btViewIcon});
            this.toolStrip1.Location = new System.Drawing.Point(7, 4);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(357, 31);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btUpLevel
            // 
            this.btUpLevel.ForeColor = System.Drawing.Color.Black;
            this.btUpLevel.Image = ((System.Drawing.Image)(resources.GetObject("btUpLevel.Image")));
            this.btUpLevel.ImageTransparentColor = System.Drawing.Color.White;
            this.btUpLevel.Name = "btUpLevel";
            this.btUpLevel.Size = new System.Drawing.Size(72, 28);
            this.btUpLevel.Text = "上一级";
            this.btUpLevel.Click += new System.EventHandler(this.btUpLevel_Click);
            // 
            // btLocateTo
            // 
            this.btLocateTo.ForeColor = System.Drawing.Color.Black;
            this.btLocateTo.Image = ((System.Drawing.Image)(resources.GetObject("btLocateTo.Image")));
            this.btLocateTo.ImageTransparentColor = System.Drawing.Color.White;
            this.btLocateTo.Name = "btLocateTo";
            this.btLocateTo.Size = new System.Drawing.Size(72, 28);
            this.btLocateTo.Text = "定位到";
            this.btLocateTo.Click += new System.EventHandler(this.btLocateTo_Click);
            // 
            // btDelete
            // 
            this.btDelete.Enabled = false;
            this.btDelete.ForeColor = System.Drawing.Color.Black;
            this.btDelete.Image = ((System.Drawing.Image)(resources.GetObject("btDelete.Image")));
            this.btDelete.ImageTransparentColor = System.Drawing.Color.White;
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(60, 28);
            this.btDelete.Text = "删除";
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btEdit
            // 
            this.btEdit.Enabled = false;
            this.btEdit.ForeColor = System.Drawing.Color.Black;
            this.btEdit.Image = ((System.Drawing.Image)(resources.GetObject("btEdit.Image")));
            this.btEdit.ImageTransparentColor = System.Drawing.Color.White;
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(60, 28);
            this.btEdit.Text = "编辑";
            this.btEdit.Click += new System.EventHandler(this.btEdit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // btViewDetail
            // 
            this.btViewDetail.Checked = true;
            this.btViewDetail.CheckOnClick = true;
            this.btViewDetail.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btViewDetail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btViewDetail.Image = ((System.Drawing.Image)(resources.GetObject("btViewDetail.Image")));
            this.btViewDetail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btViewDetail.Name = "btViewDetail";
            this.btViewDetail.Size = new System.Drawing.Size(28, 28);
            this.btViewDetail.Text = "toolStripButton5";
            this.btViewDetail.ToolTipText = "详细查看方式";
            this.btViewDetail.Click += new System.EventHandler(this.btViewDetail_Click);
            // 
            // btViewList
            // 
            this.btViewList.CheckOnClick = true;
            this.btViewList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btViewList.Image = ((System.Drawing.Image)(resources.GetObject("btViewList.Image")));
            this.btViewList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btViewList.Name = "btViewList";
            this.btViewList.Size = new System.Drawing.Size(28, 28);
            this.btViewList.Text = "toolStripButton6";
            this.btViewList.ToolTipText = "列表查看方式";
            this.btViewList.Click += new System.EventHandler(this.btViewList_Click);
            // 
            // btViewIcon
            // 
            this.btViewIcon.CheckOnClick = true;
            this.btViewIcon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btViewIcon.Image = ((System.Drawing.Image)(resources.GetObject("btViewIcon.Image")));
            this.btViewIcon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btViewIcon.Name = "btViewIcon";
            this.btViewIcon.Size = new System.Drawing.Size(28, 28);
            this.btViewIcon.Text = "toolStripButton7";
            this.btViewIcon.ToolTipText = "小图标查看方式";
            this.btViewIcon.Click += new System.EventHandler(this.btViewIcon_Click);
            // 
            // istToolbar
            // 
            this.istToolbar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("istToolbar.ImageStream")));
            this.istToolbar.TransparentColor = System.Drawing.Color.Transparent;
            this.istToolbar.Images.SetKeyName(0, "bmpToHigher.bmp");
            this.istToolbar.Images.SetKeyName(1, "bmpFolder2.bmp");
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.lvwCategories);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(90, 459);
            this.pnlLeft.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.lvwItems);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cboFilters);
            this.panel1.Controls.Add(this.btCancel);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.btOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(90, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(566, 459);
            this.panel1.TabIndex = 8;
            // 
            // FormDataBrowser
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(656, 459);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDataBrowser";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据浏览";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormDataBrowser_FormClosed);
            this.Load += new System.EventHandler(this.FormDataBrowser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboFilters.Properties)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private void FormDataBrowser_Load(object sender, System.EventArgs e)
        {
            RuntimeInfo runtimeInfo = ESRI.ArcGIS.RuntimeManager.ActiveRuntime;
            //由于不同版本的ArcGIS其连接文件保存路径不同，因此初始化时要生成正确的路径
            CommonConst.m_ConnectPropertyPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ESRI\Desktop" + runtimeInfo.Version + "\\ArcCatalog";

            InitCategories();

            //如果没有定义初始位置
            if (m_Location == null)
            {
                //上次浏览位置                 
                if (m_LastLocation != null)
                {
                    NavigateLocation = m_LastLocation;
                }
                else
                {
                    //选种磁盘
                    lvwCategories.Items[0].Selected = true;
                }
            }
        }

        private void AddItem(string name, string aliasName, DataType type, string typeName, DataItem dataItem, List<ListViewItem> lvItems)
        {
            //先检查过滤条件
            bool flag = false;
            if (dataItem is CommandItem)
            {
                flag = true;
            }
            else if (m_Filters.Count == 0)
                flag = true;
            else
            {
                if (cboFilters.SelectedItem is DefaultFilter)
                {
                    for (int i = 0; i <= m_Filters.Count - 1; i++)
                    {
                        if (m_Filters[i].CanPass(dataItem))
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                else
                    flag = (cboFilters.SelectedItem as IDataItemFilter).CanPass(dataItem);
            }

            if (flag == false) return;

            //加入到列表中
            ListViewItem item = new ListViewItem();
            item.Text = name;
            item.SubItems.Add(aliasName);
            item.SubItems.Add(typeName);
            item.Tag = dataItem;

            item.ImageIndex = m_Images.GetImageIndex(type);
            lvItems.Add(item);
            //lvwItems.Items.Add(item);				
        }

        private ImageListWrap m_Images = new ImageListWrap();
        private bool m_InitFlag = false;

        private void lvwCategories_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (m_InitFlag) return;

            if (this.lvwCategories.SelectedItems.Count == 0) return;

            switch (lvwCategories.SelectedItems[0].Tag.ToString())
            {
                case "disk":
                    NavigateLocation = new DiskConnectionRootItem();
                    break;
                case "database":
                    NavigateLocation = new DatabaseRootItem();
                    break;
                case "service":
                    NavigateLocation = new ServiceRootItem();
                    break;
                case "history":
                    NavigateLocation = new HistoryRootItem();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 双击列表框，如果选中的数据合乎调用者的要求，则执行“确定”的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvwItems_DoubleClick(object sender, System.EventArgs e)
        {
            //btOK_Click(sender, e);  
            if (lvwItems.SelectedItems.Count == 0) return;

            bool canCloseDialog = false;
            DataItem item = lvwItems.SelectedItems[0].Tag as DataItem;

            try
            {
                if (item is CommandItem)
                {
                    if ((item as CommandItem).OnClick())
                    {
                        RefreshItems();
                    }
                    return;
                }

                ResetSelectedItemsByNameString();

                if (item.CanLoad == false)
                {
                    NavigateLocation = item;
                }
                else
                {
                    //添加到历史记录
                    string his = "";
                    his = LocationSerializer.SaveToString(NavigateLocation);
                    HistoryKeeperInstance.Instance.AddHistory(his);

                    m_LastLocation = NavigateLocation;

                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private IList<IDataItemFilter> m_Filters = new List<IDataItemFilter>();
        /// <summary>
        /// 添加数据过滤器
        /// </summary>
        /// <param name="filter">数据项过滤器</param>
        public void AddFilter(IDataItemFilter filter)
        {
            if (filter != null)
            {
                m_Filters.Add(filter);
                cboFilters.Properties.Items.Add(filter);
            }
        }

        //private OpenAction m_OpenAction = OpenAction.oaSelecData;
        ///// <summary>
        ///// ShowDialog之前用于设置打开本选择窗口的目的是什么
        ///// </summary>
        //public OpenAction OpenAction
        //{
        //    get { return m_OpenAction; }
        //    set 
        //    { 
        //        m_OpenAction = value;

        //        if ((m_OpenAction & OpenAction.oaSelectWorkspace) == OpenAction.oaSelectWorkspace)
        //        {
        //            this.lvwItems.MultiSelect = false;
        //            AddFilter(new WorkspaceFilter());
        //        }
        //        else if ((m_OpenAction & OpenAction.oaSelectFeatureDataset) == OpenAction.oaSelectFeatureDataset)
        //        {
        //            this.lvwItems.MultiSelect = false;
        //            AddFilter(new FeatureDatasetFilter());
        //        }
        //        else
        //            lvwItems.MultiSelect = true;
        //    }
        //}

        /// <summary>
        /// 用于返回给调用者的接口，返回选择了哪些数据 
        /// </summary>
        public IList<DataItem> SelectedItems
        {
            get
            {
                IList<DataItem> items = new List<DataItem>();
                for (int i = 0; i <= lvwItems.SelectedItems.Count - 1; i++)
                {
                    items.Add(lvwItems.SelectedItems[i].Tag as DataItem);
                }
                return items;
            }
        }

        /// <summary>
        /// 返回选择项的名称
        /// </summary>
        public string NameString
        {
            get { return txtName.Text; }
        }

        /// <summary>
        /// 是否可以多选，在OpenAction之后设置
        /// </summary>
        public bool MultiSelect
        {
            get { return lvwItems.MultiSelect; }
            set { lvwItems.MultiSelect = value; }
        }

        private DataItem m_Location = null;

        /// <summary>
        /// 导航到指定位置
        /// </summary>
        public DataItem NavigateLocation
        {
            get
            {
                return m_Location;
            }
            set
            {
                this.Cursor = Cursors.WaitCursor;
                IList<DataItem> items = null;
                try
                {
                    if (value is HisLocationItem)
                    {
                        DataItem tmpItem = (value as HisLocationItem).GetGeoObject() as DataItem;
                        m_Location = tmpItem;
                    }
                    else
                        m_Location = value;

                    items = m_Location.GetChildren();
                }
                catch (Exception ex)
                {
                    string ext = System.IO.Path.GetExtension(m_Location.Name).ToLower();
                    if (".mdb".Equals(ext))
                        MessageBox.Show("无法打开文件：" + m_Location.Name + Environment.NewLine + "建议在Access中打开另存为“Access 2002-2003数据库”\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("无法定位到该位置:" + m_Location.Name + Environment.NewLine + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }

                lvwItems.Items.Clear();

                if (items != null)
                {
                    List<ListViewItem> tLvItems = new List<ListViewItem>();

                    for (int i = 0; i <= items.Count - 1; i++)
                    {
                        AddItem(items[i].Name, items[i].AliasName, items[i].Type, items[i].TypeName, items[i], tLvItems);
                    }

                    this.lvwItems.Items.AddRange(tLvItems.ToArray());

                    if (lvwItems.Items.Count > 0)
                    {
                        lvwItems.Items[0].Selected = true;
                    }
                }
            }
        }

        /// <summary>
        /// 设置是否可以自由导航，如果为false，则只能在初始位置上选择
        /// </summary>
        public bool FreeNavigation
        {
            set
            {
                if (value == false)
                {
                    this.pnlLeft.Visible = false;
                    btLocateTo.Enabled = false;
                    this.Width = 433;
                }
                else
                {
                    this.pnlLeft.Visible = true;
                    btLocateTo.Enabled = true;
                    this.Width = 529;
                }
            }
        }

        public EnumCategoriesType CategoriesType
        {
            get;
            set;
        }

        private void ResetSelectedItemsByNameString()
        {

        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btOK_Click(object sender, System.EventArgs e)
        {
            if (lvwItems.SelectedItems.Count == 0)
                return;
            bool canCloseDialog = false;
            DataItem item = lvwItems.SelectedItems[0].Tag as DataItem;

            try
            {
                if (item is CommandItem)
                {
                    if ((item as CommandItem).OnClick())
                    {
                        RefreshItems();

                    }
                    return;
                }

                ResetSelectedItemsByNameString();

                if (cboFilters.SelectedItem is DefaultFilter)
                {
                    if (m_Filters.Count == 0)
                    {
                        if (item.CanLoad)
                            canCloseDialog = true;
                    }
                    for (int i = 0; i <= m_Filters.Count - 1; i++)
                    {
                        if (m_Filters[i].Confirm(item))
                        {
                            canCloseDialog = true;
                            break;
                        }
                    }
                }
                else
                    canCloseDialog = (cboFilters.SelectedItem as IDataItemFilter).Confirm(item);


                if (canCloseDialog == false)
                {
                    NavigateLocation = item;
                }
                else
                {
                    //添加到历史记录
                    string his = "";
                    his = LocationSerializer.SaveToString(NavigateLocation);
                    HistoryKeeperInstance.Instance.AddHistory(his);

                    m_LastLocation = NavigateLocation;

                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void lvwItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvwItems.SelectedItems.Count == 0)
                this.txtName.Text = "";
            else
            {
                string s = this.lvwItems.SelectedItems[0].Text;
                for (int i = 1; i <= this.lvwItems.SelectedItems.Count - 1; i++)
                {
                    s = s + "," + this.lvwItems.SelectedItems[i].Text;
                }
                txtName.Text = s;
            }

            //删除和编辑按钮的可用性
            bool editFlag = false;
            bool delFlag = false;
            if (lvwItems.SelectedItems.Count == 1)
            {
                DataItem item = lvwItems.SelectedItems[0].Tag as DataItem;
                if ((item is ImsConnectionItem) ||
                    (item is AgsConnectionItem) ||
                    (item is DatabaseConnectionItem))
                {
                    editFlag = true;
                    delFlag = true;
                }
                else if (item is HisLocationItem)
                {
                    editFlag = false;
                    delFlag = true;
                }

                //获取数据项全路径
                this.Text = "添加数据：" + item.FullPath;
            }

            btEdit.Enabled = editFlag;
            btDelete.Enabled = delFlag;
        }

        /// <summary>
        /// 上一级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btUpLevel_Click(object sender, EventArgs e)
        {
            if (this.NavigateLocation != null)
            {
                if (this.NavigateLocation.Parent != null)
                    NavigateLocation = NavigateLocation.Parent;
            }
        }

        private void cboFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_InitFlag) return;

            RefreshItems();
        }

        private void btLocateTo_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.ShowNewFolderButton = true;
            dlg.Description = "请选择定位到哪个文件夹:";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string folder = dlg.SelectedPath;
                DataItem item = new HisLocationItem(folder);
                NavigateLocation = item;
            }
        }

        private void btViewDetail_Click(object sender, EventArgs e)
        {
            lvwItems.View = View.Details;
            btViewIcon.Checked = false;
            btViewList.Checked = false;
        }

        private void btViewList_Click(object sender, EventArgs e)
        {
            lvwItems.View = View.List;
            btViewDetail.Checked = false;
            btViewIcon.Checked = false;
        }

        private void btViewIcon_Click(object sender, EventArgs e)
        {
            lvwItems.View = View.SmallIcon;
            btViewDetail.Checked = false; ;
            btViewList.Checked = false;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDelete_Click(object sender, EventArgs e)
        {
            if (lvwItems.SelectedItems.Count != 1)
                return;
            DataItem item = lvwItems.SelectedItems[0].Tag as DataItem;
            if (item == null)
                return;
            if (item is IFileNameItem)
            {
                if (DialogResult.OK == MessageBox.Show("确定删除" + item.Name + "?", "删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    System.IO.File.Delete((item as IFileNameItem).FileName);
                    lvwItems.Items.Remove(lvwItems.SelectedItems[0]);
                }
            }
            else if (item is HisLocationItem)
            {
                HistoryKeeperInstance.Instance.RemoveHistory((item as HisLocationItem).Name);
                lvwItems.Items.Remove(lvwItems.SelectedItems[0]);
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btEdit_Click(object sender, EventArgs e)
        {
            if (lvwItems.SelectedItems.Count != 1)
                return;
            DataItem item = lvwItems.SelectedItems[0].Tag as DataItem;
            if (item is ImsConnectionItem)
            {
                UI.FormAddImsService frmIms = new AG.COM.SDM.Catalog.UI.FormAddImsService();
                frmIms.ServiceDescription = (item as ImsConnectionItem).GetGeoObject() as ESRI.ArcGIS.GISClient.IIMSServiceDescription;
                frmIms.ShowDialog();
            }
            else if (item is DatabaseConnectionItem)
            {
                UI.FormAddConnection frmSdeCon = new AG.COM.SDM.Catalog.UI.FormAddConnection();
                frmSdeCon.FileName = (item as IFileNameItem).FileName;
                frmSdeCon.ShowDialog();
            }
            else if (item is AgsConnectionItem)
            {

            }

        }

        private void FormDataBrowser_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void btCancel_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void RefreshItems()
        {
            if (m_Location != null)
                NavigateLocation = m_Location;
        }

    }
}
