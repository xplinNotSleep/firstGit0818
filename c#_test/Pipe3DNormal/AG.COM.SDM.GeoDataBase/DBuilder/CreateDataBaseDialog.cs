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
    /// 创建数据库 窗体类
    /// </summary>
    public partial class CreateDataBaseDialog : Form
    {
        private string m_DbInfoFileName;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="treeView"></param>
        public CreateDataBaseDialog(TreeView treeView)
        {
            //初始化界面组件
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
        /// 重载构造函数
        /// </summary>
        /// <param name="pDbInfoFile">数据信息文件</param>
        public CreateDataBaseDialog(string pDbInfoFile)
        {
            //初始化界面组件
            InitializeComponent();

            this.m_DbInfoFileName = pDbInfoFile;
        }

        /// <summary>
        /// 设置要创建的数据标准文件
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
            //    //初始化DbInfoReadWrite实例
            //    DbInfoReadWrite dbInfoConfig = new DbInfoReadWrite(this.treeView1, this.propertyGrid1);
            //    //读取数据库配置信息到树对象
            //    dbInfoConfig.ReadDBInfoToTreeView(this.m_DbInfoFileName);
            //}
        }

        private void btnLocation_Click(object sender, EventArgs e)
        {
            //这里直接选择文件夹
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.Description = "选择数据库保存的路径:";
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
            //后缀名
            string strSufficName = this.txtSufficName.Text.Trim();
            if (!Directory.Exists(txtWorkspace.Text))
            {
                MessageBox.Show("保存路径不存在！", "创建数据库", MessageBoxButtons.OK);
                return;
            }

            DBInfoHandler dbInfoHandler = new DBInfoHandler(this.treeView1, null);
            //根据数据配置信息文件创建表结构
            dbInfoHandler.CreateDataBaseByDBInfo(txtWorkspace.Text, strSufficName, checkBox1.Checked, checkBox2.Checked,checkBox3.Checked);

            //关闭窗体
            btnCancel_Click(null, null);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //关闭窗体
            this.Close();
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
            {
                //递归设置父节点选中状态
                SetParentNodeChecked(e.Node);
                //递归设置子节点选中状态
                SetChildNodeChecked(e.Node);
            }
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
    }
}