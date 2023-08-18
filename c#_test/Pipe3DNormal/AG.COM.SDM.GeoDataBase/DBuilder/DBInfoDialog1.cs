using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// ���ݽ��ⷽ�����������
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
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public DBInfoDialog1()
        {
            //��ʼ���������
            InitializeComponent();

            m_TreeView = this.treeDataView;
            this.FormClosed += new EventHandler(DBInfoDialog_FormClosed);
        }   

        public DBInfoDialog1(EnumDbInfoStates pdataNodeState)
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
            //��ʼ��DbInfoReadWriteʵ��
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

        private void CreateDataBaseDialog_Load(object sender, EventArgs e)
        {
            if (this.m_DbInfoState == EnumDbInfoStates.New)
            {
                this.TabText = "�½����ݱ�׼";
                this.lblInfo.Text = "��Ϣ˵��:�½����ݱ�׼";
                //this.btnSave.Text = "����(&S)";
            }
            else if (this.m_DbInfoState == EnumDbInfoStates.Edit)
            {
                this.TabText = "���ⷽ������";
                this.lblInfo.Text = "��Ϣ˵��:�༭���ݱ�׼";
                //this.btnSave.Text = "����(&S)";
            }
            else if (this.m_DbInfoState == EnumDbInfoStates.Browse)
            {
                this.TabText = "�������";
                this.lblInfo.Text = "��Ϣ˵��:�������";
                //this.panelBottom.Visible = false;
                this.propNodeAttribute.Enabled = false;
            }
            else if (this.m_DbInfoState == EnumDbInfoStates.CreateDataBase)
            {
                this.TabText = "�������ݿ��";
                this.lblInfo.Text = "��Ϣ˵��:�������ݿ��";
                this.treeDataView.CheckBoxes = true;
                this.propNodeAttribute.Enabled = false;
            }
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
            //ˢ������ͼ
            RefreshTreeView(treeNode, objectTreeNode);

            //tSelTreeNode = this.treeDataView.SelectedNode;
            //����Geometry�ֶ�������
            TreeNode geoTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.GeometryFieldItem);
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
            treeNode.ImageIndex = 7;
            treeNode.SelectedImageIndex = 7;
            //ˢ������ͼ
            RefreshTreeView(tSelTreeNode, treeNode);

            //����Object�ֶ�������
            TreeNode objectTreeNode = dbinfoHandler.CreateTreeNode(EnumDataNodeItems.ObjectFieldItem);            
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

            //�����Ĳ˵�����
            SetContextMenu(dataNodeItem);
        }

        private void treeDataView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && ((this.m_DbInfoState == EnumDbInfoStates.New) || (this.m_DbInfoState==EnumDbInfoStates.Edit)))
            {
                if (this.treeDataView.Nodes.Count == 0)
                {
                    //�����Ĳ˵�����
                    SetContextMenu(EnumDataNodeItems.None);
                }

                this.MenuDataBase.Show(treeDataView, e.Location);                
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
                this.treeDataView.ExpandAll();
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
            //ɾ���������ڵ�
            this.treeDataView.Nodes.Clear();
            this.m_FilePath = "";
            
            //�����Ĳ˵�����
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
            tOpenFileDialog.Filter = "���ݱ�׼(*.dml)|*.dml";
            tOpenFileDialog.Title = "ѡ�����ݱ�׼�ļ�";
            tOpenFileDialog.InitialDirectory = strInitDirectory;
            tOpenFileDialog.RestoreDirectory = true;

            if (tOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                //�Ӽ�����ɾ�����нڵ�
                this.treeDataView.Nodes.Clear();
                //��ȡ���ݱ�׼��Ϣ�ļ�
                this.ReadDBInfoToTreeView(tOpenFileDialog.FileName);               
            }    
        }

        private void toolBtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor; 
                //ʵ��������������Ϣ�ļ�������
                DBInfoHandler dbinfoHandler = new DBInfoHandler(this.treeDataView, this.propNodeAttribute);
                //��������������Ϣ���ļ�
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

            if (this.m_FilePath.Length == 0)
            {
                this.m_FilePath = string.Format("{0}\\DataBase\\temp.dml", CommonConstString.STR_TemplatePath);
                if (File.Exists(this.m_FilePath) == true)
                {
                    File.Delete(this.m_FilePath);
                }

                //�������Ϊ�ļ������¼�
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