using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// 数据建库方案窗体对象类
    /// </summary>
    public partial class DBInfoDialog : Form
    {
        private EnumDbInfoStates m_DbInfoState = EnumDbInfoStates.Edit;
        private ContextMenu m_DataBaseMenu = new ContextMenu();
        private ContextMenu m_SubjectDBMenu = new ContextMenu();
        private ContextMenu m_DatasetMenu = new ContextMenu();
        private ContextMenu m_CustomTableMenu = new ContextMenu();
        private ContextMenu m_FieldMenu = new ContextMenu();
        private TreeNode m_CopyTreeNode = null;
        private string m_FilePath = "";
        private static TreeView m_TreeView;

        /// <summary>
        /// 数据库保存的文件夹
        /// </summary>
        //private static string databasePath ="\\Database";

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DBInfoDialog()
        {
            //初始化界面组件
            InitializeComponent();
            m_TreeView = this.treeDataView;
        }

        public DBInfoDialog(EnumDbInfoStates pdataNodeState)
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
            ////初始化DbInfoReadWrite实例
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
                TreeNode selectedNode = m_TreeView.SelectedNode;
                //根据当前选择的节点获取根节点
                while (selectedNode.Parent != null)
                {
                    selectedNode = selectedNode.Parent;
                }

                tdataBaseNode = selectedNode;

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

            subDomainNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.SubjectChildItem);
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
            objectTreeNode.ImageIndex = 7;
            objectTreeNode.SelectedImageIndex = 7;
            //刷新树视图
            RefreshTreeView(treeNode, objectTreeNode);

            //tSelTreeNode = this.treeDataView.SelectedNode;
            //增加Geometry字段属性项
            TreeNode geoTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.GeometryFieldItem);
            geoTreeNode.ImageIndex = 7;
            geoTreeNode.SelectedImageIndex = 7;
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
            treeNode.ImageIndex = 9;
            treeNode.SelectedImageIndex = 9;
            //刷新树视图
            RefreshTreeView(tSelTreeNode, treeNode);

            //增加Object字段属性项
            TreeNode objectTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.ObjectFieldItem);
            objectTreeNode.ImageIndex = 7;
            objectTreeNode.SelectedImageIndex = 7;
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
            treeNode.ImageIndex = 7;
            treeNode.SelectedImageIndex = 7;
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
                string file = Application.StartupPath + "\\treeNode.xml";
                DbInfoReadWrite dbInfoConfig = new DbInfoReadWrite(this.treeDataView, this.propNodeAttribute);
                dbInfoConfig.WriteDBInfoToFile(file, this.treeDataView.SelectedNode);
                this.m_CopyTreeNode = this.treeDataView.SelectedNode.Clone() as TreeNode;
                ItemProperty item = dbInfoConfig.ReadDBInfoToTreeNode(file);
                this.m_CopyTreeNode.Tag = item;
                File.Delete(file);
            }
        }

        private void menu_Paste_Click(object sender, EventArgs e)
        {
            TreeNode selTreeNode = this.treeDataView.SelectedNode;
            //粘贴不是随便就能粘贴的
            //如果节点是属性域，那么粘贴的对象也必须是属性域节点才能，并且通一个节点不能自我粘贴
            ItemProperty itemProperty = (ItemProperty)selTreeNode.Tag;
            if (itemProperty.DataNodeItem == EnumDataNodeItems.SubjectDomainItem)
            {
                itemProperty = (ItemProperty)this.m_CopyTreeNode.Tag;
                if (itemProperty.DataNodeItem == EnumDataNodeItems.SubjectDomainItem)
                {
                    //只有这样才可以粘贴，否则不可以
                    foreach (TreeNode treeNode in m_CopyTreeNode.Nodes)
                    {
                        selTreeNode.Nodes.Add(treeNode.Clone() as TreeNode);
                    }
                }
                selTreeNode.Expand();
            }
            else if (itemProperty.DataNodeItem == EnumDataNodeItems.SubjectChildItem)
            {
                itemProperty = (ItemProperty)this.m_CopyTreeNode.Tag;
                if (itemProperty.DataNodeItem == EnumDataNodeItems.SubjectChildItem)
                {
                    //只有这样才可以粘贴，否则不可以
                    foreach (TreeNode treeNode in m_CopyTreeNode.Nodes)
                    {
                        selTreeNode.Nodes.Add(treeNode.Clone() as TreeNode);
                    }
                }
                selTreeNode.Expand();
            }
            else if (itemProperty.DataNodeItem == EnumDataNodeItems.DataBaseItem)
            {
                //直接添加根节点上去
                int i = treeDataView.Nodes.Add(m_CopyTreeNode);
                treeDataView.Nodes[i].Expand();
            }
            else
            {
                selTreeNode.Parent.Nodes.Insert(selTreeNode.Index + 1, this.m_CopyTreeNode);
            }
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
            if (e.Button == MouseButtons.Right && ((this.m_DbInfoState == EnumDbInfoStates.New) || (this.m_DbInfoState == EnumDbInfoStates.Edit)))
            {
                if (this.treeDataView.Nodes.Count == 0)
                {
                    //上下文菜单设置
                    SetContextMenu(EnumDataNodeItems.None);
                }
                if (treeDataView.HitTest(e.X, e.Y).Node != null)
                {
                    this.MenuDataBase.Show(treeDataView, e.Location);
                }
                else
                {
                    //如果为空的话，右键菜单为新建数据库
                    SetContextMenu(EnumDataNodeItems.DataBaseItem);
                    this.MenuDataBase.Show(treeDataView, e.Location);
                }
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
            if (dataNodeItem == EnumDataNodeItems.ObjectFieldItem ||
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
                    this.menu_NewSubjectDB.Visible = false;
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
                    this.menu_NewFeatureClass.Visible = true;
                    this.menu_NewAnnotion.Visible = false;
                    this.menu_NewTable.Visible = true;
                    this.menu_NewField.Visible = false;
                    this.menu_Delete.Enabled = false;
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
                    this.menu_NewAnnotion.Visible = false;
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
                //this.treeDataView.ExpandAll();
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
            menu_NewDataBase_Click(sender, e);
        }

        private void toolBtnOpen_Click(object sender, EventArgs e)
        {
            string strInitDirectory = string.Format("{0}\\DataBase", CommonConstString.STR_TemplatePath);

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
                //读取数据标准信息文件,然后将节点添加到最后，同时选择该节点
                this.ReadDBInfoToTreeView(tOpenFileDialog.FileName);
            }
        }

        private ErrorForm errorForm;
        private string qz = "数据库:";
        private void toolBtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                //实例化数据配置信息文件处理类
                DBInfoHandler dbinfoHandler = new DBInfoHandler(this.treeDataView, this.propNodeAttribute);
                List<TreeNode> repeatNodes = new List<TreeNode>();
                List<string> tempList = new List<string>();
                foreach (TreeNode treeNode in treeDataView.Nodes)
                {
                    if (treeNode.Tag is ItemProperty)
                    {
                        ItemProperty itemProperty = (ItemProperty)treeNode.Tag;
                        if (tempList.Contains(itemProperty.ItemName))
                        {
                            MessageBox.Show("存在重复的数据库名称，无法保存！" + Environment.NewLine + $"数据库名称为： {itemProperty.ItemName}", "数据模板保存");
                            return;
                        }
                        else
                        {
                            tempList.Add(itemProperty.ItemName);
                        }
                    }
                    TreeNode repeatNode = GetRepeatNode(treeNode);
                    if (repeatNode.Nodes.Count > 0)
                        repeatNodes.Add(repeatNode);
                }

                if (repeatNodes.Count > 0)
                {
                    if (errorForm == null || errorForm.IsDisposed)
                    {
                        errorForm = new ErrorForm(repeatNodes);
                        errorForm.StartPosition = FormStartPosition.CenterScreen;
                        errorForm.Show();
                    }
                    else
                    {
                        errorForm.RefreshTreeview(repeatNodes);
                        errorForm.Activate();
                    }

                }
                else
                {
                    bool success = dbinfoHandler.SaveDBInfoToFile(DBFilePath);
                    if (success)
                    {
                        MessageBox.Show("保存成功！", "数据模板保存");
                    }
                    else
                    {
                        MessageBox.Show("保存失败！", "数据模板保存");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "数据库保存失败");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        //目前数据库的结构如下
        // 数据库：专题属性域，专题子库
        //专题属性域：属性域
        //专题子库：数据集，要素类，属性表
        //数据集：要素类
        //要素类：字段
        //属性表：字段

        //直接返回一个树节点吧，这样更清晰明了

        /// <summary>
        /// 返回数据库存在重复的结果
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        private TreeNode GetRepeatNode(TreeNode treeNode)
        {
            //数据库一定有两个子节点
            TreeNode resultNode = new TreeNode(treeNode.Text);
            //专题属性域
            TreeNode domaiNode = treeNode.Nodes[0];
            //检查专题属性域内是否存在重复的属性域
            List<string> repeatDomainList = GetRepeatString(domaiNode);
            if (repeatDomainList.Count > 0)
            {
                //表示存在属性域重复
                TreeNode tempNode = resultNode.Nodes.Add("属性域重复项");
                foreach (string s in repeatDomainList)
                {
                    tempNode.Nodes.Add(s);
                }
            }
            TreeNode dataNode = treeNode.Nodes[1];
            //列出存在重复的数据集
            //列出存在重复的要素类名称或者属性表名称
            //列出要素类存在重复的字段名称和要素类名称
            //首先获取数据集的集合，要素类或者属性表的集合
            List<string> repeatDataset = GetRepateList(dataNode, "数据集");
            if (repeatDataset.Count > 0)
            {
                TreeNode tempNode = resultNode.Nodes.Add("数据集重复");
                foreach (string s in repeatDataset)
                {
                    tempNode.Nodes.Add(s);
                }
            }
            List<string> repeatFeatureClass = GetRepateList(dataNode, "要素类");
            if (repeatFeatureClass.Count > 0)
            {
                TreeNode tempNode = resultNode.Nodes.Add("要素类或属性表重复");
                foreach (string s in repeatFeatureClass)
                {
                    tempNode.Nodes.Add(s);
                }
            }

            List<string> repeatFieldList = GetRepateList(dataNode, "字段");
            repeatFieldList.Sort();
            if (repeatFieldList.Count > 0)
            {
                TreeNode tempNode = resultNode.Nodes.Add("字段存在重复");
                foreach (string s in repeatFieldList)
                {
                    tempNode.Nodes.Add(s);
                }
            }

            return resultNode;
        }

        private List<string> GetRepateList(TreeNode node, string type)
        {
            List<string> repeatList = new List<string>();
            List<string> tempList = new List<string>();
            switch (type)
            {
                case "数据集":
                    foreach (TreeNode childNode in node.Nodes)
                    {
                        if (childNode.Tag is DataSetItemProperty)
                        {
                            DataSetItemProperty dataSetItemProperty = (DataSetItemProperty)childNode.Tag;
                            if (tempList.Contains(dataSetItemProperty.ItemName))
                            {
                                repeatList.Add(dataSetItemProperty.ItemName);
                            }
                            else
                            {
                                tempList.Add(dataSetItemProperty.ItemName);
                            }
                        }
                    }
                    break;
                case "要素类":
                    //属性表和要素类归为一类来检查
                    foreach (TreeNode childNode in node.Nodes)
                    {
                        if (childNode.Tag is CustomTableItemProperty || childNode.Tag is FeatureClassItemProperty)
                        {
                            ItemProperty itemProperty = (ItemProperty)childNode.Tag;
                            if (tempList.Contains(itemProperty.ItemName))
                            {
                                repeatList.Add(itemProperty.ItemName);
                            }
                            else
                            {
                                tempList.Add(itemProperty.ItemName);
                            }
                        }
                        if (childNode.Tag is DataSetItemProperty)
                        {
                            //如果是数据集，遍历子集
                            foreach (TreeNode nodeNode in childNode.Nodes)
                            {
                                if (nodeNode.Tag is ItemProperty)
                                {
                                    ItemProperty itemProperty = (ItemProperty)nodeNode.Tag;
                                    if (tempList.Contains(itemProperty.ItemName))
                                    {
                                        repeatList.Add(itemProperty.ItemName);
                                    }
                                    else
                                    {
                                        tempList.Add(itemProperty.ItemName);
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "字段":
                    foreach (TreeNode childNode in node.Nodes)
                    {
                        if (childNode.Tag is CustomTableItemProperty || childNode.Tag is FeatureClassItemProperty)
                        {
                            //遍历子集,判断字段是否重复
                            List<string> tempList1 = GetRepeatString(childNode);
                            ItemProperty itemProperty = (ItemProperty)childNode.Tag;
                            for (int i = 0; i < tempList1.Count; i++)
                            {
                                tempList1[i] = itemProperty.ItemName + ": " + tempList1[i];
                            }
                            repeatList.AddRange(tempList1);
                        }
                        if (childNode.Tag is DataSetItemProperty)
                        {
                            //如果是数据集，遍历子集
                            foreach (TreeNode nodeNode in childNode.Nodes)
                            {
                                if (nodeNode.Tag is ItemProperty)
                                {
                                    ItemProperty itemProperty = (ItemProperty)nodeNode.Tag;
                                    List<string> tempList1 = GetRepeatString(nodeNode);
                                    for (int i = 0; i < tempList1.Count; i++)
                                    {
                                        tempList1[i] = itemProperty.ItemName + ": " + tempList1[i];
                                    }
                                    repeatList.AddRange(tempList1);
                                }
                            }
                        }
                    }
                    break;

            }
            return repeatList.Distinct().ToList();
        }


        /// <summary>
        /// 返回子节点存在重复的要素来
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private List<string> GetRepeatString(TreeNode node)
        {
            List<string> tempList = new List<string>();
            List<string> repeatList = new List<string>();
            foreach (TreeNode treeNode in node.Nodes)
            {
                if (treeNode.Tag is ItemProperty)
                {
                    ItemProperty itemProperty = (ItemProperty)treeNode.Tag;
                    if (tempList.Contains(itemProperty.ItemName))
                    {
                        repeatList.Add(itemProperty.ItemName);
                    }
                    else
                    {
                        tempList.Add(itemProperty.ItemName);
                    }
                }
            }
            return repeatList.Distinct().ToList();
        }


        /// <summary>
        /// 检查同一级别中是否存在重复的项，如果存在重复的项，那么提示出来，方式是列出来
        /// </summary>
        /// <returns></returns>
        private List<string> CheckOk(out List<string> successList)
        {
            List<string> errorList = new List<string>();
            successList = new List<string>();//没有问题的数据库名称，包含前缀
            //首先判断是否存在重名的数据库
            List<string> repeatDatabaseList = GetRepeatDatabase();//重复数据库名称
            List<string> saveFalseList = new List<string>();//保存失败的数据库
            saveFalseList.AddRange(repeatDatabaseList);
            foreach (TreeNode treeNode in treeDataView.Nodes)
            {
                if (repeatDatabaseList.Contains(treeNode.Text)) continue;//过滤掉重复的数据库
                //检查专题属性域和专题子库的问题
                int errCount = 0;//标记数据库是否存在问题
                foreach (TreeNode node in treeNode.Nodes)
                {
                    ItemProperty itemProperty = (ItemProperty)node.Tag;
                    if (itemProperty.DataNodeItem == EnumDataNodeItems.SubjectChildItem)
                    {
                        //专题子库
                        List<string> repeatFeatureClass = GetRepeateFeatureClass(node);
                        if (repeatFeatureClass.Count > 0)
                            errCount++;
                        List<TreeNode> nodes = new List<TreeNode>();
                        GetNodes(node, nodes);
                        foreach (TreeNode treeNode1 in nodes)
                        {
                            if (repeatFeatureClass.Contains(treeNode1.Text)) continue;//去除掉重复的要素类
                            itemProperty = (ItemProperty)treeNode1.Tag;
                            if (itemProperty.DataNodeItem == EnumDataNodeItems.DataSetItem) continue;
                            List<string> repeatFieldsList = GetRepeatFields(treeNode1);
                            if (repeatFieldsList.Count > 0)
                                errCount++;
                        }
                    }
                    else if (itemProperty.DataNodeItem == EnumDataNodeItems.SubjectDomainItem)
                    {
                        //专题属性域
                        List<string> repeatDomains = GetRepeateFeatureClass(node);
                        if (repeatDomains.Count > 0)
                            errCount++;

                    }
                }

                if (errCount > 0)
                {
                    //表示这个数据库是导出失败的
                    saveFalseList.Add(treeNode.Text);
                }
            }

            string saveError = "保存失败的数据库:";
            if (saveFalseList.Count > 0)
            {
                saveError += string.Join(",", saveFalseList);
                errorList.Insert(0, saveError);
            }
            return errorList;
        }
        /// <summary>
        /// 获取重复的字段集合
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private List<string> GetRepeatFields(TreeNode node)
        {
            List<string> repeatDatabaseList = new List<string>();
            List<string> tempList = new List<string>();
            foreach (TreeNode nodeNode in node.Nodes)
            {
                if (tempList.Contains(nodeNode.Text))
                {
                    repeatDatabaseList.Add(nodeNode.Text);
                }
                else
                {
                    tempList.Add(nodeNode.Text);
                }
            }
            return repeatDatabaseList;
        }

        /// <summary>
        /// 返回专题子库类重复的要素类，数据集，属性表
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        private List<string> GetRepeateFeatureClass(TreeNode treeNode)
        {
            List<string> repeatDatabaseList = new List<string>();
            List<TreeNode> nodes = new List<TreeNode>();
            GetNodes(treeNode, nodes);
            foreach (TreeNode node in nodes)
            {
                repeatDatabaseList.Add(node.Text);
            }
            return repeatDatabaseList;
        }



        /// <summary>
        /// 获取重复的数据库名称
        /// </summary>
        /// <returns></returns>
        private List<string> GetRepeatDatabase()
        {
            List<string> repeatDatabaseList = new List<string>();//重复数据库名称
            List<string> tempList = new List<string>();
            foreach (TreeNode treeNode in treeDataView.Nodes)
            {
                if (tempList.Contains(treeNode.Text))
                {
                    repeatDatabaseList.Add(treeNode.Text);
                }
                else
                {
                    tempList.Add(treeNode.Text);
                }
            }

            return repeatDatabaseList;
        }


        /// <summary>
        /// 递归获取该节点下的所有节点，除了字段节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="treeNodes"></param>
        private void GetNodes(TreeNode node, List<TreeNode> treeNodes)
        {
            if (node != null)
            {
                ItemProperty itemProperty = (ItemProperty)node.Tag;
                if (itemProperty.DataNodeItem == EnumDataNodeItems.CustomFieldItem ||
                    itemProperty.DataNodeItem == EnumDataNodeItems.GeometryFieldItem ||
                    itemProperty.DataNodeItem == EnumDataNodeItems.ObjectFieldItem)
                    return;
                foreach (TreeNode treeNode in node.Nodes)
                {
                    treeNodes.Add(treeNode);
                    GetNodes(treeNode, treeNodes);
                }
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

            //if (this.m_FilePath.Length == 0)
            //{
            //    this.m_FilePath = string.Format("{0}\\DataBase\\temp.dml", CommonConstString.STR_TemplatePath);
            //    if (File.Exists(this.m_FilePath) == true)
            //    {
            //        File.Delete(this.m_FilePath);
            //    }

            //    //调用另存为文件处理事件
            //    this.toolBtnSave_Click(null, null);
            //}

            try
            {
                CreateDataBaseDialog tCreateDbDlg = new CreateDataBaseDialog(treeDataView);
                tCreateDbDlg.ShowInTaskbar = false;
                tCreateDbDlg.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DBInfoDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_TreeView = null;

            this.m_CustomTableMenu = null;
            this.m_SubjectDBMenu = null;
            this.m_DataBaseMenu = null;
            this.m_DatasetMenu = null;
            this.m_FieldMenu = null;
            this.treeDataView = null;
        }

        private void DBInfoDialog_Load(object sender, EventArgs e)
        {
            this.Text = "建库方案管理";
            //数据库模板文件
            //判断该数据库模板文件是否存在，如果存在直接读取打开，如果不存在，则创建一个空的模板，默认创建一个空的数据库模板出来
            //DBFilePath = DBFilePath + "\\DataBaseScheme.dml";
            if (!File.Exists(DBFilePath))
            {
                //创建一个带有空表表的模板文件出来
                menu_NewDataBase_Click(sender, e);
            }
            else
            {
                //ReadDBInfoToTreeView(DBFilePath);
            }
            if (treeDataView.Nodes.Count > 0)
            {
                //添加完成后默认选择第一个数据库
                treeDataView.SelectedNode = treeDataView.Nodes[0];
            }
        }


        /// <summary>
        /// 初始化文档的结构
        /// </summary>
        private void InitDatabaseScheme(string fileName)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlDeclaration declaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDocument.AppendChild(declaration);
            XmlElement rootElement = xmlDocument.CreateElement("DataBaseConfig");//创建根节点
            xmlDocument.AppendChild(rootElement);
            XmlElement databseElement = xmlDocument.CreateElement("");
        }
        //<数据库 ItemName="XM_PsDataBase" ItemAliasName="厦门排水管网数据库标准" DataNodeItem="1" DBVersion="1.0.0" DBOwner="Augruit_Echo">
        //创建数据库节点

    }
    public static class CloneEx
    {
        public static T Clone<T>(T obj)
        {
            //T ret = default(T);
            //if (obj != null)
            //{
            //    XmlSerializer cloner = new XmlSerializer(typeof(T));
            //    MemoryStream stream = new MemoryStream();
            //    cloner.Serialize(stream, obj);
            //    stream.Seek(0, SeekOrigin.Begin);
            //    ret = (T)cloner.Deserialize(stream);
            //}
            //return ret;
            try
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                MemoryStream memStream = new MemoryStream();
                formatter.Serialize(memStream, obj);
                memStream.Position = 0;
                return (T)(formatter.Deserialize(memStream));
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
    
    
}