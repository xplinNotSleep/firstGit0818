using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AG.COM.SDM.Catalog;
using AG.COM.SDM.Catalog.DataItems;
using AG.COM.SDM.Catalog.Filters;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// �������ݿ� ������
    /// </summary>
    public partial class CreateDataBaseDialog1 : Form
    {
        private string m_DbInfoFileName;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        /// <param name="pDBInfoHandler">������Ϣ�ļ�������</param>
        public CreateDataBaseDialog1()
        {
            //��ʼ���������
            InitializeComponent();
        }

        /// <summary>
        /// ���ع��캯��
        /// </summary>
        /// <param name="pDbInfoFile">������Ϣ�ļ�</param>
        public CreateDataBaseDialog1(string pDbInfoFile)
        {
            //��ʼ���������
            InitializeComponent();

            this.m_DbInfoFileName = pDbInfoFile;
        }

        /// <summary>
        /// ����Ҫ���������ݱ�׼�ļ�
        /// </summary>
        public string DBInfoFileName
        {
            set
            {
                this.m_DbInfoFileName = value;
            }
        }

        private void CreateDataBaseDialog_Load(object sender, EventArgs e)
        {
            if (this.m_DbInfoFileName.Trim().Length > 0 && System.IO.File.Exists(this.m_DbInfoFileName))
            {
                //��ʼ��DbInfoReadWriteʵ��
                DbInfoReadWrite dbInfoConfig = new DbInfoReadWrite(this.treeView1, this.propertyGrid1);
                //��ȡ���ݿ�������Ϣ��������
                dbInfoConfig.ReadDBInfoToTreeView(this.m_DbInfoFileName);
            }
        }

        private void btnLocation_Click(object sender, EventArgs e)
        {
            try
            {

                IWorkspace tWorkspace = null;

                //ʵ���������������ʵ��
                AG.COM.SDM.Catalog.IDataBrowser frm = new FormDataBrowser();
                //frm.AddFilter(new WorkspaceFilter());
                frm.AddFilter(new PersonalGeoDatabaseFilter());
                frm.AddFilter(new FileGeoDatabaseFilter());
                frm.AddFilter(new SDEWorkspaceFilter());
            
                frm.MultiSelect = false;

                //��ȡ�����ռ�
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    IList<DataItem> items = frm.SelectedItems;
                    if (items.Count == 0) return;

                    object obj = items[0].GetGeoObject();
                    tWorkspace = obj as IWorkspace;
                    if (tWorkspace != null)
                    {
                        if (tWorkspace.Type == esriWorkspaceType.esriFileSystemWorkspace)
                        {
                            MessageBox.Show("����ѡ���ļ�Ŀ¼��");
                            return;
                        }

                        this.txtWorkspace.Text = items[0].Name;
                        this.txtWorkspace.Tag = tWorkspace;
                       
                        SetBtnOkEabled(null, null);             //����ȷ����ť�Ŀ�����
                    }
                    else
                        this.btnOK.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetBtnOkEabled(object sender, EventArgs e)
        {  
            string strSuffic = txtSufficName.Text.Trim();

            if (strSuffic.Length == 0)
            {
                this.btnOK.Enabled = true;
                return;
            }

            if (strSuffic.Length > 0 && Regex.IsMatch(strSuffic, @"^_[A-Za-z0-9]+$"))
            {
                this.btnOK.Enabled = true;
                return;
            }

            this.btnOK.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //��׺��
            string strSufficName = this.txtSufficName.Text.Trim();

            IWorkspace tWorkspace = this.txtWorkspace.Tag as IWorkspace;

            DBInfoHandler dbInfoHandler = new DBInfoHandler(this.treeView1, this.propertyGrid1);
            //��������������Ϣ�ļ�������ṹ
            dbInfoHandler.CreateDataBaseByDBInfo(strSufficName, tWorkspace); 

            //�رմ���
            btnCancel_Click(null, null);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {          
            //�رմ���
            this.Close();
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
            {
                //�ݹ����ø��ڵ�ѡ��״̬
                SetParentNodeChecked(e.Node);
                //�ݹ������ӽڵ�ѡ��״̬
                SetChildNodeChecked(e.Node);
            } 
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
    }
}