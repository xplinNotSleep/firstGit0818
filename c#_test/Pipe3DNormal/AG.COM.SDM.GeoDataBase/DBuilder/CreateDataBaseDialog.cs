using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AG.COM.SDM.Catalog;
using AG.COM.SDM.Catalog.DataItems;
using AG.COM.SDM.Catalog.Filters;
using AG.COM.SDM.GeoDataBase.Properties;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// �������ݿ� ������
    /// </summary>
    public partial class CreateDataBaseDialog : Form
    {
        private string m_DbInfoFileName;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        /// <param name="treeView"></param>
        public CreateDataBaseDialog(TreeView treeView)
        {
            //��ʼ���������
            InitializeComponent();
            if (treeView != null)
            {
                foreach (TreeNode treeNode in treeView.Nodes)
                {
                    TreeNode node = treeNode.Clone() as TreeNode;
                    treeView1.Nodes.Add(node);
                }
            }
        }

        /// <summary>
        /// ���ع��캯��
        /// </summary>
        /// <param name="pDbInfoFile">������Ϣ�ļ�</param>
        public CreateDataBaseDialog(string pDbInfoFile)
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
            //if (this.m_DbInfoFileName.Trim().Length > 0 && System.IO.File.Exists(this.m_DbInfoFileName))
            //{
            //    //��ʼ��DbInfoReadWriteʵ��
            //    DbInfoReadWrite dbInfoConfig = new DbInfoReadWrite(this.treeView1, this.propertyGrid1);
            //    //��ȡ���ݿ�������Ϣ��������
            //    dbInfoConfig.ReadDBInfoToTreeView(this.m_DbInfoFileName);
            //}
        }

        private void btnLocation_Click(object sender, EventArgs e)
        {
            //����ֱ��ѡ���ļ���
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.Description = "ѡ�����ݿⱣ���·��:";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtWorkspace.Text = folderBrowserDialog.SelectedPath;
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
            if (!Directory.Exists(txtWorkspace.Text))
            {
                MessageBox.Show("����·�������ڣ�", "�������ݿ�", MessageBoxButtons.OK);
                return;
            }

            DBInfoHandler dbInfoHandler = new DBInfoHandler(this.treeView1, null);
            //��������������Ϣ�ļ�������ṹ
            dbInfoHandler.CreateDataBaseByDBInfo(txtWorkspace.Text, strSufficName, checkBox1.Checked, checkBox2.Checked,checkBox3.Checked);

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