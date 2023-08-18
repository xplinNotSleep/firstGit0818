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
    /// ���ݽ��ⷽ�����������
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
        /// ���ݿⱣ����ļ���
        /// </summary>
        //private static string databasePath ="\\Database";

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public DBInfoDialog()
        {
            //��ʼ���������
            InitializeComponent();
            m_TreeView = this.treeDataView;
        }

        public DBInfoDialog(EnumDbInfoStates pdataNodeState)
        {
            //��ʼ���������
            InitializeComponent();
            m_TreeView = this.treeDataView;
            this.m_DbInfoState = pdataNodeState;
        }

        /// <summary>
        /// ��ȡ������ö������ͼ״̬
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
        /// ��ȡ�����ý��ⷽ���ļ�·��
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
        /// ��ȡ�����ļ���Ϣ,������νṹ��ʾ����
        /// </summary>
        /// <param name="strFilePath">�ļ�·��</param>
        public void ReadDBInfoToTreeView(string strFilePath)
        {
            //�������ļ�·��
            this.m_FilePath = strFilePath;
            ////��ʼ��DbInfoReadWriteʵ��
            DbInfoReadWrite dbInfoConfig = new DbInfoReadWrite(this.treeDataView, this.propNodeAttribute);
            //��ȡ���ݿ�������Ϣ��������
            dbInfoConfig.ReadDBInfoToTreeView(strFilePath);

        }

        /// <summary>
        /// ��ȡ���е�������
        /// </summary>
        /// <returns>���������򼯺�</returns>
        public static IList<DomainItemProperty> GetAllDomains()
        {
            DomainItemProperty tDomainItem = new DomainItemProperty();
            tDomainItem.ItemName = "(��)";
            tDomainItem.ItemAliasName = "(��)";

            //��ʼ��������
            IList<DomainItemProperty> tDomainItems = new List<DomainItemProperty>();
            //��ӿյ�������
            tDomainItems.Add(tDomainItem);

            if (m_TreeView.Nodes.Count > 0)
            {
                //��ȡ���ڵ�
                TreeNode tdataBaseNode = m_TreeView.Nodes[0];
                TreeNode selectedNode = m_TreeView.SelectedNode;
                //���ݵ�ǰѡ��Ľڵ��ȡ���ڵ�
                while (selectedNode.Parent != null)
                {
                    selectedNode = selectedNode.Parent;
                }

                tdataBaseNode = selectedNode;

                //��������ڵ�ֻ��λ��(�ռ����ݿ�/ר��������)�ڵ��£�����...
                //����������Ҫ�ݹ����ÿ���ڵ����������
                foreach (TreeNode treeNode in tdataBaseNode.Nodes)
                {
                    ItemProperty itemProperty = treeNode.Tag as ItemProperty;

                    //�жϽڵ������Ƿ�Ϊ����������
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


        #region �ؼ������¼�
        private void menu_NewDataBase_Click(object sender, EventArgs e)
        {
            //ʵ����DBInfoHandler������
            DBInfoHandler dbinfoHandler = new DBInfoHandler(this.treeDataView, this.propNodeAttribute);
            //�������ݽ�����ʹ��������
            TreeNode treeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.DataBaseItem);
            treeNode.ImageIndex = 0;
            treeNode.SelectedImageIndex = 0;
            //ˢ������ͼ
            RefreshTreeView(null, treeNode);

            //���ݽ�����ʹ��������
            TreeNode subDomainNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.SubjectDomainItem);
            subDomainNode.ImageIndex = 1;
            subDomainNode.SelectedImageIndex = 1;
            //ˢ������ͼ
            RefreshTreeView(treeNode, subDomainNode);

            subDomainNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.SubjectChildItem);
            subDomainNode.ImageIndex = 1;
            subDomainNode.SelectedImageIndex = 1;
            //ˢ������ͼ
            RefreshTreeView(treeNode, subDomainNode);

        }

        private void menu_NewSubjectDB_Click(object sender, EventArgs e)
        {
            TreeNode tSelTreeNode = this.treeDataView.SelectedNode;
            //ʵ����DBInfoHandler������
            DBInfoHandler dbinfoHandler = new DBInfoHandler(this.treeDataView, this.propNodeAttribute);
            //���ݽ�����ʹ��������
            TreeNode treeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.SubjectChildItem);
            treeNode.ImageIndex = 1;
            treeNode.SelectedImageIndex = 1;
            //ˢ������ͼ
            RefreshTreeView(tSelTreeNode, treeNode);
        }

        private void menu_NewDomains_Click(object sender, EventArgs e)
        {
            TreeNode tSelTreeNode = this.treeDataView.SelectedNode;
            //ʵ����DBInfoHandler������
            DBInfoHandler dbinfoHandler = new DBInfoHandler(this.treeDataView, this.propNodeAttribute);
            //���ݽ�����ʹ��������
            TreeNode treeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.DomainItem);
            treeNode.ImageIndex = 6;
            treeNode.SelectedImageIndex = 6;
            //ˢ������ͼ
            RefreshTreeView(tSelTreeNode, treeNode);
        }

        private void menu_NewDataSet_Click(object sender, EventArgs e)
        {
            TreeNode tSelTreeNode = this.treeDataView.SelectedNode;
            //ʵ����DBInfoHandler������
            DBInfoHandler dbinfoHandler = new DBInfoHandler(this.treeDataView, this.propNodeAttribute);
            //���ݽ�����ʹ��������
            TreeNode treeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.DataSetItem);
            treeNode.ImageIndex = 2;
            treeNode.SelectedImageIndex = 2;
            //ˢ������ͼ
            RefreshTreeView(tSelTreeNode, treeNode);
        }

        private void menu_NewFeatureClass_Click(object sender, EventArgs e)
        {
            TreeNode tSelTreeNode = this.treeDataView.SelectedNode;
            //ʵ����DBInfoHandler������
            DBInfoHandler dbinfoHandler = new DBInfoHandler(this.treeDataView, this.propNodeAttribute);
            //���ݽ�����ʹ��������
            TreeNode treeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.FeatureClassItem);
            treeNode.ImageIndex = 3;
            treeNode.SelectedImageIndex = 3;
            //ˢ������ͼ
            RefreshTreeView(tSelTreeNode, treeNode);

            //tSelTreeNode = this.treeDataView.SelectedNode;
            //����Object�ֶ�������
            TreeNode objectTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.ObjectFieldItem);
            objectTreeNode.ImageIndex = 7;
            objectTreeNode.SelectedImageIndex = 7;
            //ˢ������ͼ
            RefreshTreeView(treeNode, objectTreeNode);

            //tSelTreeNode = this.treeDataView.SelectedNode;
            //����Geometry�ֶ�������
            TreeNode geoTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.GeometryFieldItem);
            geoTreeNode.ImageIndex = 7;
            geoTreeNode.SelectedImageIndex = 7;
            //ˢ������ͼ
            RefreshTreeView(treeNode, geoTreeNode);

            this.treeDataView.SelectedNode = treeNode;
        }

        /// <summary>
        /// ����ע��ͼ�㣨����ע��ͼ�����ͨͼ���ֶ������𣬹ʴ�����������
        /// </summary>
        private void menu_NewAnnotion_Click(object sender, EventArgs e)
        {
            TreeNode tSelTreeNode = this.treeDataView.SelectedNode;
            //ʵ����DBInfoHandler������
            DBInfoHandler dbinfoHandler = new DBInfoHandler(this.treeDataView, this.propNodeAttribute);
            //���ݽ�����ʹ��������
            TreeNode treeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.FeatureClassItem);
            treeNode.ImageIndex = 9;
            treeNode.SelectedImageIndex = 9;
            //ˢ������ͼ
            RefreshTreeView(tSelTreeNode, treeNode);

            //tSelTreeNode = this.treeDataView.SelectedNode;
            //����Object�ֶ�������
            TreeNode objectTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.ObjectFieldItem);
            //ˢ������ͼ
            RefreshTreeView(treeNode, objectTreeNode);

            //tSelTreeNode = this.treeDataView.SelectedNode;
            //����Geometry�ֶ�������
            TreeNode geoTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.GeometryFieldItem);
            //ˢ������ͼ
            RefreshTreeView(treeNode, geoTreeNode);

            #region ����ע��ͼ�������ֶ�
            //�½�FeatureID�ֶ�
            TreeNode FeatureIDTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.FeatureID);
            //ˢ������ͼ
            RefreshTreeView(treeNode, FeatureIDTreeNode);

            //�½�ZOrder�ֶ�
            TreeNode ZOrderTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.ZOrder);
            //ˢ������ͼ
            RefreshTreeView(treeNode, ZOrderTreeNode);
            //�½�AnnotationClassID�ֶ�
            TreeNode AnnotationClassIDTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.AnnotationClassID);
            //ˢ������ͼ
            RefreshTreeView(treeNode, AnnotationClassIDTreeNode);

            //�½�Element�ֶ�
            TreeNode ElementTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.Element);
            //ˢ������ͼ
            RefreshTreeView(treeNode, ElementTreeNode);
            //�½�SymbolID�ֶ�
            TreeNode SymbolIDTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.SymbolID);
            //ˢ������ͼ
            RefreshTreeView(treeNode, SymbolIDTreeNode);
            //�½�Status�ֶ�
            TreeNode StatusTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.Status);
            //ˢ������ͼ
            RefreshTreeView(treeNode, StatusTreeNode);
            //�½�TextString�ֶ�
            TreeNode TextStringTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.TextString);
            //ˢ������ͼ
            RefreshTreeView(treeNode, TextStringTreeNode);
            //�½�FontName�ֶ�
            TreeNode FontNameTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.FontName);
            //ˢ������ͼ
            RefreshTreeView(treeNode, FontNameTreeNode);
            //�½�FontSize�ֶ�
            TreeNode FontSizeTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.FontSize);
            //ˢ������ͼ
            RefreshTreeView(treeNode, FontSizeTreeNode);
            //�½�Bold�ֶ�
            TreeNode BoldTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.Bold);
            //ˢ������ͼ
            RefreshTreeView(treeNode, BoldTreeNode);
            //�½�Italic�ֶ�
            TreeNode ItalicTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.Italic);
            //ˢ������ͼ
            RefreshTreeView(treeNode, ItalicTreeNode);
            //�½�Underline�ֶ�
            TreeNode UnderlineTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.Underline);
            //ˢ������ͼ
            RefreshTreeView(treeNode, UnderlineTreeNode);
            //�½�VerticalAlignment�ֶ�
            TreeNode VerticalAlignmentTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.VerticalAlignment);
            //ˢ������ͼ
            RefreshTreeView(treeNode, VerticalAlignmentTreeNode);
            //�½�HorizontalAlignment�ֶ�
            TreeNode HorizontalAlignmentNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.HorizontalAlignment);
            //ˢ������ͼ
            RefreshTreeView(treeNode, HorizontalAlignmentNode);
            //�½�XOffset�ֶ�
            TreeNode XOffsetTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.XOffset);
            //ˢ������ͼ
            RefreshTreeView(treeNode, XOffsetTreeNode);
            //�½�YOffset�ֶ�
            TreeNode YOffsetTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.YOffset);
            //ˢ������ͼ
            RefreshTreeView(treeNode, YOffsetTreeNode);
            //�½�Angle�ֶ�
            TreeNode AngleTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.Angle);
            //ˢ������ͼ
            RefreshTreeView(treeNode, AngleTreeNode);
            //�½�FontLeading�ֶ�
            TreeNode FontLeadingTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.FontLeading);
            //ˢ������ͼ
            RefreshTreeView(treeNode, FontLeadingTreeNode);
            //�½�WordSpacing�ֶ�
            TreeNode WordSpacingTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.WordSpacing);
            //ˢ������ͼ
            RefreshTreeView(treeNode, WordSpacingTreeNode);
            //�½�CharacterWidth�ֶ�
            TreeNode CharacterWidthTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.CharacterWidth);
            //ˢ������ͼ
            RefreshTreeView(treeNode, CharacterWidthTreeNode);
            //�½�CharacterSpacing�ֶ�
            TreeNode CharacterSpacingTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.CharacterSpacing);
            //ˢ������ͼ
            RefreshTreeView(treeNode, CharacterSpacingTreeNode);
            //�½�FlipAngle�ֶ�
            TreeNode FlipAngleTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.FlipAngle);
            //ˢ������ͼ
            RefreshTreeView(treeNode, FlipAngleTreeNode);

            //�½�Override�ֶ�
            TreeNode OverrideTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.Override);
            //ˢ������ͼ
            RefreshTreeView(treeNode, OverrideTreeNode);

            #endregion

            this.treeDataView.SelectedNode = treeNode;
        }

        private void menu_NewTable_Click(object sender, EventArgs e)
        {
            TreeNode tSelTreeNode = this.treeDataView.SelectedNode;
            //ʵ����DBInfoHandler������
            DBInfoHandler dbinfoHandler = new DBInfoHandler(this.treeDataView, this.propNodeAttribute);
            //���ݽ�����ʹ��������
            TreeNode treeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.CustomTableItem);
            treeNode.ImageIndex = 9;
            treeNode.SelectedImageIndex = 9;
            //ˢ������ͼ
            RefreshTreeView(tSelTreeNode, treeNode);

            //����Object�ֶ�������
            TreeNode objectTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.ObjectFieldItem);
            objectTreeNode.ImageIndex = 7;
            objectTreeNode.SelectedImageIndex = 7;
            //ˢ������ͼ
            RefreshTreeView(treeNode, objectTreeNode);

            this.treeDataView.SelectedNode = treeNode;
        }

        private void menu_NewField_Click(object sender, EventArgs e)
        {
            TreeNode tSelTreeNode = this.treeDataView.SelectedNode;
            //ʵ����DBInfoHandler������
            DBInfoHandler dbinfoHandler = new DBInfoHandler(this.treeDataView, this.propNodeAttribute);
            //���ݽ�����ʹ��������
            TreeNode treeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.CustomFieldItem);
            treeNode.ImageIndex = 7;
            treeNode.SelectedImageIndex = 7;
            //ˢ������ͼ
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
            //ճ������������ճ����
            //����ڵ�����������ôճ���Ķ���Ҳ������������ڵ���ܣ�����ͨһ���ڵ㲻������ճ��
            ItemProperty itemProperty = (ItemProperty)selTreeNode.Tag;
            if (itemProperty.DataNodeItem == EnumDataNodeItems.SubjectDomainItem)
            {
                itemProperty = (ItemProperty)this.m_CopyTreeNode.Tag;
                if (itemProperty.DataNodeItem == EnumDataNodeItems.SubjectDomainItem)
                {
                    //ֻ�������ſ���ճ�������򲻿���
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
                    //ֻ�������ſ���ճ�������򲻿���
                    foreach (TreeNode treeNode in m_CopyTreeNode.Nodes)
                    {
                        selTreeNode.Nodes.Add(treeNode.Clone() as TreeNode);
                    }
                }
                selTreeNode.Expand();
            }
            else if (itemProperty.DataNodeItem == EnumDataNodeItems.DataBaseItem)
            {
                //ֱ����Ӹ��ڵ���ȥ
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

            //�����Ĳ˵�����
            SetContextMenu(dataNodeItem);
        }

        private void treeDataView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && ((this.m_DbInfoState == EnumDbInfoStates.New) || (this.m_DbInfoState == EnumDbInfoStates.Edit)))
            {
                if (this.treeDataView.Nodes.Count == 0)
                {
                    //�����Ĳ˵�����
                    SetContextMenu(EnumDataNodeItems.None);
                }
                if (treeDataView.HitTest(e.X, e.Y).Node != null)
                {
                    this.MenuDataBase.Show(treeDataView, e.Location);
                }
                else
                {
                    //���Ϊ�յĻ����Ҽ��˵�Ϊ�½����ݿ�
                    SetContextMenu(EnumDataNodeItems.DataBaseItem);
                    this.MenuDataBase.Show(treeDataView, e.Location);
                }
            }
        }

        private void treeDataView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
            {
                //�ݹ����ø��ڵ�ѡ��״̬
                SetParentNodeChecked(e.Node);
                //�ݹ������ӽڵ�ѡ��״̬
                SetChildNodeChecked(e.Node);
            }
        }
        #endregion

        #region ˽�д�����
        /// <summary>
        /// �����Ĳ˵�����
        /// </summary>
        /// <param name="dataNodeItem">�ڵ�����</param>
        private void SetContextMenu(EnumDataNodeItems dataNodeItem)
        {
            #region ���ø�����ճ���˵��Ŀ���״̬
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

            #region ���������˵��Ŀ��ò˵�
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
        /// ˢ������ͼ
        /// </summary>
        /// <param name="pSelTreeNode">ѡ������ڵ�</param>
        /// <param name="pTreeNode">���������ڵ�</param>
        private void RefreshTreeView(TreeNode pSelTreeNode, TreeNode pTreeNode)
        {
            if (pSelTreeNode != null)
            {
                //������ڵ�
                pSelTreeNode.Nodes.Add(pTreeNode);
                //չ�����ڵ�
                pSelTreeNode.Expand();
            }
            else
            {
                this.treeDataView.Nodes.Add(pTreeNode);
                //չ���������ڵ�
                //this.treeDataView.ExpandAll();
            }
            this.treeDataView.SelectedNode = pTreeNode;
        }

        /// <summary>
        /// �ݹ����ø��ڵ�ѡ��״̬
        /// </summary>
        /// <param name="ptreeNode">��ǰ�ڵ�</param>
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
        /// �ݹ������ӽڵ�ѡ��״̬
        /// </summary>
        /// <param name="ptreeNode">��ǰ�ڵ�</param>
        private void SetChildNodeChecked(TreeNode ptreeNode)
        {
            foreach (TreeNode treeNode in ptreeNode.Nodes)
            {
                treeNode.Checked = ptreeNode.Checked;
                //�ݹ������ӽڵ�ѡ��״̬
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
            tOpenFileDialog.Filter = "���ݱ�׼(*.dml)|*.dml";
            tOpenFileDialog.Title = "ѡ�����ݱ�׼�ļ�";
            tOpenFileDialog.InitialDirectory = strInitDirectory;
            tOpenFileDialog.RestoreDirectory = true;

            if (tOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                //��ȡ���ݱ�׼��Ϣ�ļ�,Ȼ�󽫽ڵ���ӵ����ͬʱѡ��ýڵ�
                this.ReadDBInfoToTreeView(tOpenFileDialog.FileName);
            }
        }

        private ErrorForm errorForm;
        private string qz = "���ݿ�:";
        private void toolBtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                //ʵ��������������Ϣ�ļ�������
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
                            MessageBox.Show("�����ظ������ݿ����ƣ��޷����棡" + Environment.NewLine + $"���ݿ�����Ϊ�� {itemProperty.ItemName}", "����ģ�屣��");
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
                        MessageBox.Show("����ɹ���", "����ģ�屣��");
                    }
                    else
                    {
                        MessageBox.Show("����ʧ�ܣ�", "����ģ�屣��");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "���ݿⱣ��ʧ��");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        //Ŀǰ���ݿ�Ľṹ����
        // ���ݿ⣺ר��������ר���ӿ�
        //ר��������������
        //ר���ӿ⣺���ݼ���Ҫ���࣬���Ա�
        //���ݼ���Ҫ����
        //Ҫ���ࣺ�ֶ�
        //���Ա��ֶ�

        //ֱ�ӷ���һ�����ڵ�ɣ���������������

        /// <summary>
        /// �������ݿ�����ظ��Ľ��
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        private TreeNode GetRepeatNode(TreeNode treeNode)
        {
            //���ݿ�һ���������ӽڵ�
            TreeNode resultNode = new TreeNode(treeNode.Text);
            //ר��������
            TreeNode domaiNode = treeNode.Nodes[0];
            //���ר�����������Ƿ�����ظ���������
            List<string> repeatDomainList = GetRepeatString(domaiNode);
            if (repeatDomainList.Count > 0)
            {
                //��ʾ�����������ظ�
                TreeNode tempNode = resultNode.Nodes.Add("�������ظ���");
                foreach (string s in repeatDomainList)
                {
                    tempNode.Nodes.Add(s);
                }
            }
            TreeNode dataNode = treeNode.Nodes[1];
            //�г������ظ������ݼ�
            //�г������ظ���Ҫ�������ƻ������Ա�����
            //�г�Ҫ��������ظ����ֶ����ƺ�Ҫ��������
            //���Ȼ�ȡ���ݼ��ļ��ϣ�Ҫ����������Ա�ļ���
            List<string> repeatDataset = GetRepateList(dataNode, "���ݼ�");
            if (repeatDataset.Count > 0)
            {
                TreeNode tempNode = resultNode.Nodes.Add("���ݼ��ظ�");
                foreach (string s in repeatDataset)
                {
                    tempNode.Nodes.Add(s);
                }
            }
            List<string> repeatFeatureClass = GetRepateList(dataNode, "Ҫ����");
            if (repeatFeatureClass.Count > 0)
            {
                TreeNode tempNode = resultNode.Nodes.Add("Ҫ��������Ա��ظ�");
                foreach (string s in repeatFeatureClass)
                {
                    tempNode.Nodes.Add(s);
                }
            }

            List<string> repeatFieldList = GetRepateList(dataNode, "�ֶ�");
            repeatFieldList.Sort();
            if (repeatFieldList.Count > 0)
            {
                TreeNode tempNode = resultNode.Nodes.Add("�ֶδ����ظ�");
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
                case "���ݼ�":
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
                case "Ҫ����":
                    //���Ա��Ҫ�����Ϊһ�������
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
                            //��������ݼ��������Ӽ�
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
                case "�ֶ�":
                    foreach (TreeNode childNode in node.Nodes)
                    {
                        if (childNode.Tag is CustomTableItemProperty || childNode.Tag is FeatureClassItemProperty)
                        {
                            //�����Ӽ�,�ж��ֶ��Ƿ��ظ�
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
                            //��������ݼ��������Ӽ�
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
        /// �����ӽڵ�����ظ���Ҫ����
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
        /// ���ͬһ�������Ƿ�����ظ������������ظ������ô��ʾ��������ʽ���г���
        /// </summary>
        /// <returns></returns>
        private List<string> CheckOk(out List<string> successList)
        {
            List<string> errorList = new List<string>();
            successList = new List<string>();//û����������ݿ����ƣ�����ǰ׺
            //�����ж��Ƿ�������������ݿ�
            List<string> repeatDatabaseList = GetRepeatDatabase();//�ظ����ݿ�����
            List<string> saveFalseList = new List<string>();//����ʧ�ܵ����ݿ�
            saveFalseList.AddRange(repeatDatabaseList);
            foreach (TreeNode treeNode in treeDataView.Nodes)
            {
                if (repeatDatabaseList.Contains(treeNode.Text)) continue;//���˵��ظ������ݿ�
                //���ר���������ר���ӿ������
                int errCount = 0;//������ݿ��Ƿ��������
                foreach (TreeNode node in treeNode.Nodes)
                {
                    ItemProperty itemProperty = (ItemProperty)node.Tag;
                    if (itemProperty.DataNodeItem == EnumDataNodeItems.SubjectChildItem)
                    {
                        //ר���ӿ�
                        List<string> repeatFeatureClass = GetRepeateFeatureClass(node);
                        if (repeatFeatureClass.Count > 0)
                            errCount++;
                        List<TreeNode> nodes = new List<TreeNode>();
                        GetNodes(node, nodes);
                        foreach (TreeNode treeNode1 in nodes)
                        {
                            if (repeatFeatureClass.Contains(treeNode1.Text)) continue;//ȥ�����ظ���Ҫ����
                            itemProperty = (ItemProperty)treeNode1.Tag;
                            if (itemProperty.DataNodeItem == EnumDataNodeItems.DataSetItem) continue;
                            List<string> repeatFieldsList = GetRepeatFields(treeNode1);
                            if (repeatFieldsList.Count > 0)
                                errCount++;
                        }
                    }
                    else if (itemProperty.DataNodeItem == EnumDataNodeItems.SubjectDomainItem)
                    {
                        //ר��������
                        List<string> repeatDomains = GetRepeateFeatureClass(node);
                        if (repeatDomains.Count > 0)
                            errCount++;

                    }
                }

                if (errCount > 0)
                {
                    //��ʾ������ݿ��ǵ���ʧ�ܵ�
                    saveFalseList.Add(treeNode.Text);
                }
            }

            string saveError = "����ʧ�ܵ����ݿ�:";
            if (saveFalseList.Count > 0)
            {
                saveError += string.Join(",", saveFalseList);
                errorList.Insert(0, saveError);
            }
            return errorList;
        }
        /// <summary>
        /// ��ȡ�ظ����ֶμ���
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
        /// ����ר���ӿ����ظ���Ҫ���࣬���ݼ������Ա�
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
        /// ��ȡ�ظ������ݿ�����
        /// </summary>
        /// <returns></returns>
        private List<string> GetRepeatDatabase()
        {
            List<string> repeatDatabaseList = new List<string>();//�ظ����ݿ�����
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
        /// �ݹ��ȡ�ýڵ��µ����нڵ㣬�����ֶνڵ�
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
            //ʵ��������������Ϣ�ļ�������
            DBInfoHandler dbinfoHandler = new DBInfoHandler(this.treeDataView, this.propNodeAttribute);
            //���Ϊ�������÷������ļ�
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

            //    //�������Ϊ�ļ������¼�
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
            this.Text = "���ⷽ������";
            //���ݿ�ģ���ļ�
            //�жϸ����ݿ�ģ���ļ��Ƿ���ڣ��������ֱ�Ӷ�ȡ�򿪣���������ڣ��򴴽�һ���յ�ģ�壬Ĭ�ϴ���һ���յ����ݿ�ģ�����
            //DBFilePath = DBFilePath + "\\DataBaseScheme.dml";
            if (!File.Exists(DBFilePath))
            {
                //����һ�����пձ���ģ���ļ�����
                menu_NewDataBase_Click(sender, e);
            }
            else
            {
                //ReadDBInfoToTreeView(DBFilePath);
            }
            if (treeDataView.Nodes.Count > 0)
            {
                //�����ɺ�Ĭ��ѡ���һ�����ݿ�
                treeDataView.SelectedNode = treeDataView.Nodes[0];
            }
        }


        /// <summary>
        /// ��ʼ���ĵ��Ľṹ
        /// </summary>
        private void InitDatabaseScheme(string fileName)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlDeclaration declaration = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDocument.AppendChild(declaration);
            XmlElement rootElement = xmlDocument.CreateElement("DataBaseConfig");//�������ڵ�
            xmlDocument.AppendChild(rootElement);
            XmlElement databseElement = xmlDocument.CreateElement("");
        }
        //<���ݿ� ItemName="XM_PsDataBase" ItemAliasName="������ˮ�������ݿ��׼" DataNodeItem="1" DBVersion="1.0.0" DBOwner="Augruit_Echo">
        //�������ݿ�ڵ�

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