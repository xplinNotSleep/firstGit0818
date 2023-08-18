using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// 数据建库方案窗体对象类
    /// </summary>
    public partial class DBInfoDialog1 : DockDocument
    {
        private EnumDbInfoStates m_DbInfoState = EnumDbInfoStates.Edit;
        private ContextMenu m_DataBaseMenu=new ContextMenu();
        private ContextMenu m_SubjectDBMenu = new ContextMenu();
        private ContextMenu m_DatasetMenu = new ContextMenu();
        private ContextMenu m_CustomTableMenu = new ContextMenu();
        private ContextMenu m_FieldMenu = new ContextMenu();
        private TreeNode m_CopyTreeNode = null;
        private string m_FilePath="";
        private static TreeView m_TreeView;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DBInfoDialog1()
        {
            //初始化界面组件
            InitializeComponent();

            m_TreeView = this.treeDataView;
            this.FormClosed += new EventHandler(DBInfoDialog_FormClosed);
        }   

        public DBInfoDialog1(EnumDbInfoStates pdataNodeState)
        {
            //初始化界面组件
            InitializeComponent();

            m_TreeView = this.treeDataView;
            this.m_DbInfoState = pdataNodeState;
        }

        /// <summary>
        /// 获取或设置枚举树视图状态
        /// </summary>
        public EnumDbInfoStates DbInfoState
        {
            get
            {
                return this.m_DbInfoState;
            }                             
            set
            {
                this.m_DbInfoState = value;
            }
        }

        /// <summary>
        /// 获取或设置建库方案文件路径
        /// </summary>
        public string DBFilePath
        {
            get
            {
                return m_FilePath;
            }
            set
            {
                m_FilePath = value;
            }
        }

        /// <summary>
        /// 读取数据文件信息,以树层次结构显示出来
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        public void ReadDBInfoToTreeView(string strFilePath)
        {
            //设置其文件路径
            this.m_FilePath = strFilePath;
            //初始化DbInfoReadWrite实例
            DbInfoReadWrite dbInfoConfig = new DbInfoReadWrite(this.treeDataView, this.propNodeAttribute);
            //读取数据库配置信息到树对象
            dbInfoConfig.ReadDBInfoToTreeView(strFilePath);          
        }

        /// <summary>
        /// 获取所有的属性域集
        /// </summary>
        /// <returns>返回属性域集合</returns>
        public static IList<DomainItemProperty> GetAllDomains()
        {      
            DomainItemProperty tDomainItem = new DomainItemProperty();
            tDomainItem.ItemName = "(无)";
            tDomainItem.ItemAliasName = "(无)";

            //初始化属性域集
            IList<DomainItemProperty> tDomainItems = new List<DomainItemProperty>();
            //添加空的属性域
            tDomainItems.Add(tDomainItem); 

            if (m_TreeView.Nodes.Count > 0)
            {
                //获取根节点
                TreeNode tdataBaseNode = m_TreeView.Nodes[0];

                //因属性域节点只能位于(空间数据库/专题属性域)节点下，所以...
                //否则我们需要递归遍历每个节点的数据类型
                foreach (TreeNode treeNode in tdataBaseNode.Nodes)
                {
                    ItemProperty itemProperty = treeNode.Tag as ItemProperty;

                    //判断节点类型是否为属性域类型
                    if (itemProperty.DataNodeItem == EnumDataNodeItems.SubjectDomainItem)
                    {
                        foreach (TreeNode domainNode in treeNode.Nodes)
                        {
                            tDomainItems.Add(domainNode.Tag as DomainItemProperty);
                        }

                        break;
                    }
                }                                                                    
            } 

            return tDomainItems;
     
        }

        private void CreateDataBaseDialog_Load(object sender, EventArgs e)
        {
            if (this.m_DbInfoState == EnumDbInfoStates.New)
            {
                this.TabText = "新建数据标准";
                this.lblInfo.Text = "信息说明:新建数据标准";
                //this.btnSave.Text = "保存(&S)";
            }
            else if (this.m_DbInfoState == EnumDbInfoStates.Edit)
            {
                this.TabText = "建库方案管理";
                this.lblInfo.Text = "信息说明:编辑数据标准";
                //this.btnSave.Text = "保存(&S)";
            }
            else if (this.m_DbInfoState == EnumDbInfoStates.Browse)
            {
                this.TabText = "数据浏览";
                this.lblInfo.Text = "信息说明:数据浏览";
                //this.panelBottom.Visible = false;
                this.propNodeAttribute.Enabled = false;
            }
            else if (this.m_DbInfoState == EnumDbInfoStates.CreateDataBase)
            {
                this.TabText = "创建数据库表";
                this.lblInfo.Text = "信息说明:创建数据库表";
                this.treeDataView.CheckBoxes = true;
                this.propNodeAttribute.Enabled = false;
            }
        }        

        #region 控件处理事件
        private void menu_NewDataBase_Click(object sender, EventArgs e)
        {
            //实例化DBInfoHandler处理类
            DBInfoHandler dbinfoHandler = new DBInfoHandler(this.treeDataView, this.propNodeAttribute);
            //根据数据结点类型创建树结点
            TreeNode treeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.DataBaseItem);
            treeNode.ImageIndex = 0;
            treeNode.SelectedImageIndex = 0;
            //刷新树视图
            RefreshTreeView(null, treeNode);        
                        
            //根据结点类型创建树结点
            TreeNode subDomainNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.SubjectDomainItem);
            subDomainNode.ImageIndex = 1;
            subDomainNode.SelectedImageIndex = 1;
            //刷新树视图
            RefreshTreeView(treeNode, subDomainNode);
        }

        private void menu_NewSubjectDB_Click(object sender, EventArgs e)
        {
            TreeNode tSelTreeNode = this.treeDataView.SelectedNode;
            //实例化DBInfoHandler处理类
            DBInfoHandler dbinfoHandler = new DBInfoHandler(this.treeDataView, this.propNodeAttribute);
            //根据结点类型创建树结点
            TreeNode treeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.SubjectChildItem);
            treeNode.ImageIndex = 1;
            treeNode.SelectedImageIndex = 1;
            //刷新树视图
            RefreshTreeView(tSelTreeNode, treeNode);
        } 

        private void menu_NewDomains_Click(object sender, EventArgs e)
        {
            TreeNode tSelTreeNode = this.treeDataView.SelectedNode;
            //实例化DBInfoHandler处理类
            DBInfoHandler dbinfoHandler = new DBInfoHandler(this.treeDataView, this.propNodeAttribute);
            //根据结点类型创建树结点
            TreeNode treeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.DomainItem);              
            treeNode.ImageIndex = 6;
            treeNode.SelectedImageIndex = 6;
            //刷新树视图
            RefreshTreeView(tSelTreeNode, treeNode);
        }

        private void menu_NewDataSet_Click(object sender, EventArgs e)
        {
            TreeNode tSelTreeNode = this.treeDataView.SelectedNode;
            //实例化DBInfoHandler处理类
            DBInfoHandler dbinfoHandler = new DBInfoHandler(this.treeDataView, this.propNodeAttribute);
            //根据结点类型创建树结点
            TreeNode treeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.DataSetItem);
            treeNode.ImageIndex = 2;
            treeNode.SelectedImageIndex = 2;
            //刷新树视图
            RefreshTreeView(tSelTreeNode, treeNode);
        }

        private void menu_NewFeatureClass_Click(object sender, EventArgs e)
        {
            TreeNode tSelTreeNode = this.treeDataView.SelectedNode;
            //实例化DBInfoHandler处理类
            DBInfoHandler dbinfoHandler = new DBInfoHandler(this.treeDataView, this.propNodeAttribute);
            //根据结点类型创建树结点
            TreeNode treeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.FeatureClassItem);
            treeNode.ImageIndex = 3;
            treeNode.SelectedImageIndex = 3;
            //刷新树视图
            RefreshTreeView(tSelTreeNode, treeNode);

            //tSelTreeNode = this.treeDataView.SelectedNode;
            //增加Object字段属性项
            TreeNode objectTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.ObjectFieldItem);
            //刷新树视图
            RefreshTreeView(treeNode, objectTreeNode);

            //tSelTreeNode = this.treeDataView.SelectedNode;
            //增加Geometry字段属性项
            TreeNode geoTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.GeometryFieldItem);
            //刷新树视图
            RefreshTreeView(treeNode, geoTreeNode);

            this.treeDataView.SelectedNode = treeNode;
        }

        /// <summary>
        /// 创建注记图层（由于注记图层和普通图层字段有区别，故创建区别开来）
        /// </summary>
        private void menu_NewAnnotion_Click(object sender, EventArgs e)
        {
            TreeNode tSelTreeNode = this.treeDataView.SelectedNode;
            //实例化DBInfoHandler处理类
            DBInfoHandler dbinfoHandler = new DBInfoHandler(this.treeDataView, this.propNodeAttribute);
            //根据结点类型创建树结点
            TreeNode treeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.FeatureClassItem);
            treeNode.ImageIndex = 9;
            treeNode.SelectedImageIndex = 9;
            //刷新树视图
            RefreshTreeView(tSelTreeNode, treeNode);

            //tSelTreeNode = this.treeDataView.SelectedNode;
            //增加Object字段属性项
            TreeNode objectTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.ObjectFieldItem);
            //刷新树视图
            RefreshTreeView(treeNode, objectTreeNode);

            //tSelTreeNode = this.treeDataView.SelectedNode;
            //增加Geometry字段属性项
            TreeNode geoTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.GeometryFieldItem);
            //刷新树视图
            RefreshTreeView(treeNode, geoTreeNode);

            #region 创建注记图层特有字段
            //新建FeatureID字段
            TreeNode FeatureIDTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.FeatureID);
            //刷新树视图
            RefreshTreeView(treeNode, FeatureIDTreeNode);

            //新建ZOrder字段
            TreeNode ZOrderTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.ZOrder);
            //刷新树视图
            RefreshTreeView(treeNode, ZOrderTreeNode);
            //新建AnnotationClassID字段
            TreeNode AnnotationClassIDTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.AnnotationClassID);
            //刷新树视图
            RefreshTreeView(treeNode, AnnotationClassIDTreeNode);

            //新建Element字段
            TreeNode ElementTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.Element);
            //刷新树视图
            RefreshTreeView(treeNode, ElementTreeNode);
            //新建SymbolID字段
            TreeNode SymbolIDTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.SymbolID);
            //刷新树视图
            RefreshTreeView(treeNode, SymbolIDTreeNode);
            //新建Status字段
            TreeNode StatusTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.Status);
            //刷新树视图
            RefreshTreeView(treeNode, StatusTreeNode);
            //新建TextString字段
            TreeNode TextStringTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.TextString);
            //刷新树视图
            RefreshTreeView(treeNode, TextStringTreeNode);
            //新建FontName字段
            TreeNode FontNameTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.FontName);
            //刷新树视图
            RefreshTreeView(treeNode, FontNameTreeNode);
            //新建FontSize字段
            TreeNode FontSizeTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.FontSize);
            //刷新树视图
            RefreshTreeView(treeNode, FontSizeTreeNode);
            //新建Bold字段
            TreeNode BoldTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.Bold);
            //刷新树视图
            RefreshTreeView(treeNode, BoldTreeNode);
            //新建Italic字段
            TreeNode ItalicTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.Italic);
            //刷新树视图
            RefreshTreeView(treeNode, ItalicTreeNode);
            //新建Underline字段
            TreeNode UnderlineTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.Underline);
            //刷新树视图
            RefreshTreeView(treeNode, UnderlineTreeNode);
            //新建VerticalAlignment字段
            TreeNode VerticalAlignmentTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.VerticalAlignment);
            //刷新树视图
            RefreshTreeView(treeNode, VerticalAlignmentTreeNode);
            //新建HorizontalAlignment字段
            TreeNode HorizontalAlignmentNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.HorizontalAlignment);
            //刷新树视图
            RefreshTreeView(treeNode, HorizontalAlignmentNode);
            //新建XOffset字段
            TreeNode XOffsetTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.XOffset);
            //刷新树视图
            RefreshTreeView(treeNode, XOffsetTreeNode);
            //新建YOffset字段
            TreeNode YOffsetTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.YOffset);
            //刷新树视图
            RefreshTreeView(treeNode, YOffsetTreeNode);
            //新建Angle字段
            TreeNode AngleTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.Angle);
            //刷新树视图
            RefreshTreeView(treeNode, AngleTreeNode);
            //新建FontLeading字段
            TreeNode FontLeadingTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.FontLeading);
            //刷新树视图
            RefreshTreeView(treeNode, FontLeadingTreeNode);
            //新建WordSpacing字段
            TreeNode WordSpacingTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.WordSpacing);
            //刷新树视图
            RefreshTreeView(treeNode, WordSpacingTreeNode);
            //新建CharacterWidth字段
            TreeNode CharacterWidthTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.CharacterWidth);
            //刷新树视图
            RefreshTreeView(treeNode, CharacterWidthTreeNode);
            //新建CharacterSpacing字段
            TreeNode CharacterSpacingTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.CharacterSpacing);
            //刷新树视图
            RefreshTreeView(treeNode, CharacterSpacingTreeNode);
            //新建FlipAngle字段
            TreeNode FlipAngleTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.FlipAngle);
            //刷新树视图
            RefreshTreeView(treeNode, FlipAngleTreeNode);

            //新建Override字段
            TreeNode OverrideTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.Override);
            //刷新树视图
            RefreshTreeView(treeNode, OverrideTreeNode);

            #endregion

            this.treeDataView.SelectedNode = treeNode;
        }

        private void menu_NewTable_Click(object sender, EventArgs e)
        {
            TreeNode tSelTreeNode = this.treeDataView.SelectedNode;
            //实例化DBInfoHandler处理类
            DBInfoHandler dbinfoHandler = new DBInfoHandler(this.treeDataView, this.propNodeAttribute);
            //根据结点类型创建树结点
            TreeNode treeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.CustomTableItem);
            treeNode.ImageIndex = 7;
            treeNode.SelectedImageIndex = 7;
            //刷新树视图
            RefreshTreeView(tSelTreeNode, treeNode);

            //增加Object字段属性项
            TreeNode objectTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.ObjectFieldItem);            
            //刷新树视图
            RefreshTreeView(treeNode, objectTreeNode);

            this.treeDataView.SelectedNode = treeNode;
        }

        private void menu_NewField_Click(object sender, EventArgs e)
        {
            TreeNode tSelTreeNode = this.treeDataView.SelectedNode;
            //实例化DBInfoHandler处理类
            DBInfoHandler dbinfoHandler = new DBInfoHandler(this.treeDataView, this.propNodeAttribute);
            //根据结点类型创建树结点
            TreeNode treeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.CustomFieldItem);
            //刷新树视图
            RefreshTreeView(tSelTreeNode, treeNode);
        }

        private void menu_Delete_Click(object sender, EventArgs e)
        {
            TreeNode selTreeNode = this.treeDataView.SelectedNode;
            if (selTreeNode != null)
            {
                this.treeDataView.Nodes.Remove(selTreeNode); 
            }
        }

        private void menu_Copy_Click(object sender, EventArgs e)
        {
            if (this.treeDataView.SelectedNode != null)
            {
                this.m_CopyTreeNode = this.treeDataView.SelectedNode.Clone() as TreeNode;
            }
        }

        private void menu_Paste_Click(object sender, EventArgs e)
        {
            TreeNode selTreeNode = this.treeDataView.SelectedNode;
            selTreeNode.Parent.Nodes.Insert(selTreeNode.Index + 1, this.m_CopyTreeNode);
            this.m_CopyTreeNode = null;
        }

        private void treeDataView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            EnumDataNodeItems dataNodeItem = EnumDataNodeItems.None;
            TreeNode treeNode = e.Node;
            if (treeNode != null)
            {
                ItemProperty tItemProperty = treeNode.Tag as ItemProperty;
                dataNodeItem = tItemProperty.DataNodeItem;

                this.propNodeAttribute.SelectedObject = new ItemTypeDescriptor(tItemProperty);
            }

            //上下文菜单设置
            SetContextMenu(dataNodeItem);
        }

        private void treeDataView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && ((this.m_DbInfoState == EnumDbInfoStates.New) || (this.m_DbInfoState==EnumDbInfoStates.Edit)))
            {
                if (this.treeDataView.Nodes.Count == 0)
                {
                    //上下文菜单设置
                    SetContextMenu(EnumDataNodeItems.None);
                }

                this.MenuDataBase.Show(treeDataView, e.Location);                
            }
        }

        private void treeDataView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
            {
                //递归设置父节点选中状态
                SetParentNodeChecked(e.Node);
                //递归设置子节点选中状态
                SetChildNodeChecked(e.Node);
            }        
        }
        #endregion

        #region 私有处理方法
        /// <summary>
        /// 上下文菜单设置
        /// </summary>
        /// <param name="dataNodeItem">节点类型</param>
        private void SetContextMenu(EnumDataNodeItems dataNodeItem)
        { 
            #region 设置复制与粘贴菜单的可用状态
            if ( dataNodeItem == EnumDataNodeItems.ObjectFieldItem ||
                dataNodeItem == EnumDataNodeItems.GeometryFieldItem)
            {
                this.menu_Copy.Enabled = false;               
            }
            else
            {
                this.menu_Copy.Enabled = true;

                if (this.m_CopyTreeNode != null)
                {
                    ItemProperty oldItemProperty = this.m_CopyTreeNode.Tag as ItemProperty;
                    if (oldItemProperty.DataNodeItem == dataNodeItem)
                        this.menu_Paste.Enabled = true;
                    else
                        this.menu_Paste.Enabled = false;
                }
                else
                    this.menu_Paste.Enabled = false;
            }
            #endregion

            #region 设置其它菜单的可用菜单
            switch (dataNodeItem)
            {
                case EnumDataNodeItems.None:
                    this.menu_NewDataBase.Visible = true;
                    this.menu_NewSubjectDB.Visible = false;
                    this.menu_NewDomains.Visible = false;
                    this.menu_NewDataSet.Visible = false;
                    this.menu_NewFeatureClass.Visible = false;
                    this.menu_NewAnnotion.Visible = false;
                    this.menu_NewTable.Visible = false;
                    this.menu_NewField.Visible = false;
                    this.menu_Delete.Enabled = true;
                    break;
                case EnumDataNodeItems.DataBaseItem:
                    this.menu_NewDataBase.Visible = false;
                    this.menu_NewSubjectDB.Visible = true;
                    this.menu_NewDomains.Visible = false;
                    this.menu_NewDataSet.Visible = false;
                    this.menu_NewFeatureClass.Visible = false;
                    this.menu_NewAnnotion.Visible = false;
                    this.menu_NewTable.Visible = false;
                    this.menu_NewField.Visible = false;
                    this.menu_Delete.Enabled = true;
                    break;
                case EnumDataNodeItems.SubjectChildItem:
                    this.menu_NewDataBase.Visible = false;
                    this.menu_NewSubjectDB.Visible = false;
                    this.menu_NewDomains.Visible = false;
                    this.menu_NewDataSet.Visible = true;
                    this.menu_NewFeatureClass.Visible = false;
                    this.menu_NewAnnotion.Visible = false;
                    this.menu_NewTable.Visible = true;
                    this.menu_NewField.Visible = false;
                    this.menu_Delete.Enabled = true;
                    break;
                case EnumDataNodeItems.SubjectDomainItem:
                    this.menu_NewDataBase.Visible = false;
                    this.menu_NewSubjectDB.Visible = false;
                    this.menu_NewDomains.Visible = true;
                    this.menu_NewDataSet.Visible = false;
                    this.menu_NewFeatureClass.Visible = false;
                    this.menu_NewAnnotion.Visible = false;
                    this.menu_NewTable.Visible = false;
                    this.menu_NewField.Visible = false;
                    this.menu_Delete.Enabled = false;
                    break;
                case EnumDataNodeItems.DataSetItem:
                    this.menu_NewDataBase.Visible = false;
                    this.menu_NewSubjectDB.Visible = false;
                    this.menu_NewDomains.Visible = false;
                    this.menu_NewDataSet.Visible = false;
                    this.menu_NewFeatureClass.Visible = true;
                    this.menu_NewAnnotion.Visible = true;
                    this.menu_NewTable.Visible = false;
                    this.menu_NewField.Visible = false;
                    this.menu_Delete.Enabled = true;
                    break;
                case EnumDataNodeItems.FeatureClassItem:
                case EnumDataNodeItems.CustomTableItem:
                    this.menu_NewDataBase.Visible = false;
                    this.menu_NewSubjectDB.Visible = false;
                    this.menu_NewDomains.Visible = false;
                    this.menu_NewDataSet.Visible = false;
                    this.menu_NewFeatureClass.Visible = false;
                    this.menu_NewAnnotion.Visible = false;
                    this.menu_NewTable.Visible = false;
                    this.menu_NewField.Visible = true;
                    this.menu_NewField.Enabled = true;
                    this.menu_Delete.Enabled = true;
                    break;
                case EnumDataNodeItems.ObjectFieldItem:
                case EnumDataNodeItems.GeometryFieldItem:
                    this.menu_NewDataBase.Visible = false;
                    this.menu_NewSubjectDB.Visible = false;
                    this.menu_NewDomains.Visible = false;
                    this.menu_NewDataSet.Visible = false;
                    this.menu_NewFeatureClass.Visible = false;
                    this.menu_NewAnnotion.Visible = false;
                    this.menu_NewTable.Visible = false;
                    this.menu_NewField.Visible = true;
                    this.menu_NewField.Enabled = false;
                    this.menu_Delete.Enabled = false;
                    break;
                default:
                    this.menu_NewDataBase.Visible = false;
                    this.menu_NewSubjectDB.Visible = false;
                    this.menu_NewDomains.Visible = false;
                    this.menu_NewDataSet.Visible = false;
                    this.menu_NewFeatureClass.Visible = false;
                    this.menu_NewAnnotion.Visible = false;
                    this.menu_NewTable.Visible = false;
                    this.menu_NewField.Visible = true;
                    this.menu_NewField.Enabled = false;                
                    this.menu_Delete.Enabled = true;
                    break;
            }
            #endregion
        }

        /// <summary>
        /// 刷新树视图
        /// </summary>
        /// <param name="pSelTreeNode">选择的树节点</param>
        /// <param name="pTreeNode">新增的树节点</param>
        private void RefreshTreeView(TreeNode pSelTreeNode, TreeNode pTreeNode)
        {
            if (pSelTreeNode != null)
            {
                //添加树节点
                pSelTreeNode.Nodes.Add(pTreeNode);
                //展开树节点
                pSelTreeNode.Expand();
            }
            else
            {
                this.treeDataView.Nodes.Add(pTreeNode);
                //展开所有树节点
                this.treeDataView.ExpandAll();
            }
            this.treeDataView.SelectedNode = pTreeNode;
        }  

        /// <summary>
        /// 递归设置父节点选中状态
        /// </summary>
        /// <param name="ptreeNode">当前节点</param>
        private void SetParentNodeChecked(TreeNode ptreeNode)
        {
            if (ptreeNode.Checked == true)
            {
                if (ptreeNode.Parent != null)
                {
                    ptreeNode.Parent.Checked = ptreeNode.Checked;

                    SetParentNodeChecked(ptreeNode.Parent);
                }
            }
        }

        /// <summary>
        /// 递归设置子节点选中状态
        /// </summary>
        /// <param name="ptreeNode">当前节点</param>
        private void SetChildNodeChecked(TreeNode ptreeNode)
        {
            foreach (TreeNode treeNode in ptreeNode.Nodes)
            {
                treeNode.Checked = ptreeNode.Checked;
                //递归设置子节点选中状态
                SetChildNodeChecked(treeNode);
            }
        }
        #endregion       

        private void toolBtnNew_Click(object sender, EventArgs e)
        {
            //删除所有树节点
            this.treeDataView.Nodes.Clear();
            this.m_FilePath = "";
            
            //上下文菜单设置
            SetContextMenu(EnumDataNodeItems.None);
        }

        private void toolBtnOpen_Click(object sender, EventArgs e)
        {
            string strInitDirectory = string.Format("{0}\\DataBase",CommonConstString.STR_TemplatePath);

            if (System.IO.Directory.Exists(strInitDirectory) == false)
            {
                System.IO.Directory.CreateDirectory(strInitDirectory);
            }

            OpenFileDialog tOpenFileDialog = new OpenFileDialog();
            tOpenFileDialog.Filter = "数据标准(*.dml)|*.dml";
            tOpenFileDialog.Title = "选择数据标准文件";
            tOpenFileDialog.InitialDirectory = strInitDirectory;
            tOpenFileDialog.RestoreDirectory = true;

            if (tOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                //从集合中删除所有节点
                this.treeDataView.Nodes.Clear();
                //读取数据标准信息文件
                this.ReadDBInfoToTreeView(tOpenFileDialog.FileName);               
            }    
        }

        private void toolBtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor; 
                //实例化数据配置信息文件处理类
                DBInfoHandler dbinfoHandler = new DBInfoHandler(this.treeDataView, this.propNodeAttribute);
                //保存数据配置信息到文件
                dbinfoHandler.SaveDBInfoToFile(ref this.m_FilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void toolBtnSaveAs_Click(object sender, EventArgs e)
        {
            //实例化数据配置信息文件处理类
            DBInfoHandler dbinfoHandler = new DBInfoHandler(this.treeDataView, this.propNodeAttribute);
            //另存为数据配置方案到文件
            dbinfoHandler.SaveAsDBInfoToFile(ref this.m_FilePath); 
        }

        private void toolClose_Click(object sender, EventArgs e)
        {
            this.Close();
        } 

        private void toolBtnCreateDataBase_Click(object sender, EventArgs e)
        {
            if (this.treeDataView.Nodes.Count == 0) return;

            if (this.m_FilePath.Length == 0)
            {
                this.m_FilePath = string.Format("{0}\\DataBase\\temp.dml", CommonConstString.STR_TemplatePath);
                if (File.Exists(this.m_FilePath) == true)
                {
                    File.Delete(this.m_FilePath);
                }

                //调用另存为文件处理事件
                this.toolBtnSave_Click(null, null);                
            }

            try
            {
                CreateDataBaseDialog1 tCreateDbDlg = new CreateDataBaseDialog1(this.m_FilePath);
                tCreateDbDlg.ShowInTaskbar = false;
                tCreateDbDlg.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DBInfoDialog_FormClosed(object sender, EventArgs e)
        {
            m_TreeView = null;

            this.m_CustomTableMenu = null;
            this.m_SubjectDBMenu = null;
            this.m_DataBaseMenu = null;
            this.m_DatasetMenu = null;
            this.m_FieldMenu = null;
            this.treeDataView = null;
        }
    }
}