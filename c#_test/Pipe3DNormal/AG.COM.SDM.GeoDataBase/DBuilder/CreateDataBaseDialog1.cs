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
    /// 创建数据库 窗体类
    /// </summary>
    public partial class CreateDataBaseDialog1 : Form
    {
        private string m_DbInfoFileName;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="pDBInfoHandler">数据信息文件处理类</param>
        public CreateDataBaseDialog1()
        {
            //初始化界面组件
            InitializeComponent();
        }

        /// <summary>
        /// 重载构造函数
        /// </summary>
        /// <param name="pDbInfoFile">数据信息文件</param>
        public CreateDataBaseDialog1(string pDbInfoFile)
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
            if (this.m_DbInfoFileName.Trim().Length > 0 && System.IO.File.Exists(this.m_DbInfoFileName))
            {
                //初始化DbInfoReadWrite实例
                DbInfoReadWrite dbInfoConfig = new DbInfoReadWrite(this.treeView1, this.propertyGrid1);
                //读取数据库配置信息到树对象
                dbInfoConfig.ReadDBInfoToTreeView(this.m_DbInfoFileName);
            }
        }

        private void btnLocation_Click(object sender, EventArgs e)
        {
            try
            {

                IWorkspace tWorkspace = null;

                //实例化数据浏览窗体实例
                AG.COM.SDM.Catalog.IDataBrowser frm = new FormDataBrowser();
                //frm.AddFilter(new WorkspaceFilter());
                frm.AddFilter(new PersonalGeoDatabaseFilter());
                frm.AddFilter(new FileGeoDatabaseFilter());
                frm.AddFilter(new SDEWorkspaceFilter());
            
                frm.MultiSelect = false;

                //获取工作空间
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
                            MessageBox.Show("不能选择文件目录。");
                            return;
                        }

                        this.txtWorkspace.Text = items[0].Name;
                        this.txtWorkspace.Tag = tWorkspace;
                       
                        SetBtnOkEabled(null, null);             //设置确定按钮的可用性
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
            //后缀名
            string strSufficName = this.txtSufficName.Text.Trim();

            IWorkspace tWorkspace = this.txtWorkspace.Tag as IWorkspace;

            DBInfoHandler dbInfoHandler = new DBInfoHandler(this.treeView1, this.propertyGrid1);
            //根据数据配置信息文件创建表结构
            dbInfoHandler.CreateDataBaseByDBInfo(strSufficName, tWorkspace); 

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