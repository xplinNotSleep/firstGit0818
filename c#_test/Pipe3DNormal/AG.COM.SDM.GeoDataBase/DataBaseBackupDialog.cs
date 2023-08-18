using AG.COM.SDM.DAL;
using AG.COM.SDM.GeoDataBase.DBuilder;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 数据备份 窗体
    /// </summary>
    public partial class DataBaseBackupDialog : Form
    {
        /// <summary>
        /// 注销描述
        /// </summary>
        private static string STR_CANCEL_DESP = "(已注销)";

        private IList<string> m_ListFeatureClassName=new List<string>();

        /// <summary>
        /// 初始化实例对象
        /// </summary>
        public DataBaseBackupDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取需要备份的图层列表项
        /// </summary>
        public IList<string> ListFeatureClassName
        {
            get
            {
                return this.m_ListFeatureClassName;
            }
        }

        private void DataBaseBackupDialog_Load(object sender, EventArgs e)
        {
            AddTreeNode();

            this.combFrequency.SelectedIndex = 0;
            this.combWeek.SelectedIndex = 0;
        }

        private void gxTreeView1_ObjectChecked(object sender)
        {
            TreeNode tCurrentNode = sender as TreeNode;
            ItemProperty tItemProperty = tCurrentNode.Tag as ItemProperty;

            if (tItemProperty.DataNodeItem == EnumDataNodeItems.FeatureClassItem)
            {
                if (tCurrentNode.Checked == true)
                {
                    if (this.m_ListFeatureClassName.Contains(tItemProperty.ItemName) == false)
                    {
                        this.m_ListFeatureClassName.Add(tItemProperty.ItemName);
                    }
                }
                else
                {
                    if (this.m_ListFeatureClassName.Contains(tItemProperty.ItemName))
                    {
                        this.m_ListFeatureClassName.Remove(tItemProperty.ItemName);
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFolderSel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;

            if (folderDlg.ShowDialog() == DialogResult.OK)
            {
                this.txtFolder.Text = folderDlg.SelectedPath;
            }
        }

        private void combFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combFrequency.SelectedIndex == 0)
            {
                this.numHour.Enabled = false;
                this.numMinute.Enabled = false;
                this.combWeek.Enabled = false;
            }
            else if (combFrequency.SelectedIndex == 1)
            {
                this.numHour.Enabled = true;
                this.numMinute.Enabled = true;
                this.combWeek.Enabled = false;
            }
            else
            {
                this.numHour.Enabled = true;
                this.numMinute.Enabled = true;
                this.combWeek.Enabled = true;
            }
        }

        /// <summary>
        /// 获取被选择的featureclass的名称
        /// </summary>
        /// <param name="tNodeColl"></param>
        /// <returns></returns>
        private IList<string> GetSelectFeatureClassName(TreeNodeCollection tNodeColl)
        {
            IList<string> result = new List<string>();

            AddSelectLayerToList(tNodeColl, result);

            return result;
        }
       
        /// <summary>
        /// 把选择的图层添加到一个list
        /// </summary>
        /// <param name="tNodeColl"></param>
        /// <param name="tLayers"></param>
        private void AddSelectLayerToList(TreeNodeCollection tNodeColl, IList<string> result)
        {
            foreach (TreeNode tNode in tNodeColl)
            {
                if (tNode.Tag is AGSDM_LAYER && tNode.Checked == true)
                {
                    result.Add((tNode.Tag as AGSDM_LAYER).LAYER_NAME);
                }

                AddSelectLayerToList(tNode.Nodes, result);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string strBatfile = CommonConstString.STR_ConfigPath + "\\Schedluer.bat";
            using (StreamWriter tStreamWriter = new StreamWriter(strBatfile, false, Encoding.GetEncoding(936)))    //936  简体中文
            {
                tStreamWriter.WriteLine("@echo off");
                tStreamWriter.WriteLine("rem 需要配置的参数说明:");
                tStreamWriter.WriteLine("rem dump_Dir:即要备份的数据文件目录。");

                tStreamWriter.WriteLine("at 1 /delete/yes");
              
                if (this.combFrequency.SelectedIndex == 1)
                {
                    tStreamWriter.WriteLine(string.Format("at {0}:{1} /every:M,T,W,Th,F cmd /c {2}", numHour.Value, numMinute.Value, CommonConstString.STR_ConfigPath + "\\databackup.bat"));
                }
                else if(this.combFrequency.SelectedIndex==2)
                {
                    //返回指定的星期
                    string strWeek = this.GetWeekChar();
                    tStreamWriter.WriteLine(string.Format("at {0}:{1} /every:{2} cmd /c {3}", numHour.Value, numMinute.Value,strWeek, CommonConstString.STR_ConfigPath + "\\databackup.bat"));
                }

            }
      
            //写导出批处理命令
            this.WriteDataBackupBat();

            //创建或删除任务计划
            System.Diagnostics.Process.Start(strBatfile);

            MessageBox.Show("备份策略已成功执行");
            this.Close();
        }

        /// <summary>
        /// 写导出批处理命令
        /// </summary>
        private void WriteDataBackupBat()
        {
            //获取被选择featureclass的名称
            this.m_ListFeatureClassName = GetSelectFeatureClassName(tvwData.Nodes);

            string strBatfile = CommonConstString.STR_ConfigPath + "\\databackup.bat";
            using (StreamWriter tStreamWriter = new StreamWriter(strBatfile, false,Encoding.Unicode))
            {
                tStreamWriter.WriteLine("@echo off");
                //tStreamWriter.WriteLine("rem call DataBackup.bat %Dump_Dir%");
                tStreamWriter.WriteLine("rem =============================================");
                //tStreamWriter.WriteLine("rem 本批处理实现支持SDE数据备份,流程为:导出数据到dump_dir文件目录下,然");
                //tStreamWriter.WriteLine("rem 后将文件以rar格式放入当天备份日期命名的目录下,便于以后按照日期恢复");
                //tStreamWriter.WriteLine("rem");
                tStreamWriter.WriteLine("rem 需要配置的参数说明:");
                //tStreamWriter.WriteLine("Bat_Home:即本批处理所在的目录；");
                tStreamWriter.WriteLine("rem BKDIR：是调用本批处理时传进来的参数 BKFILE即要备份到的数据文件目录。");
                tStreamWriter.WriteLine("rem Author:王政");
                tStreamWriter.WriteLine("rem ==================================================");

                tStreamWriter.WriteLine("echo 备份开始...");
                tStreamWriter.WriteLine("echo 当前的时间是： %DATE% %time%");
                //tStreamWriter.WriteLine("rem set BAT_HOME=E:\\BAT");
                tStreamWriter.WriteLine("set BKDIR=" + this.txtFolder.Text);
                tStreamWriter.WriteLine("set BKFILE=%Date:0,4%%Date:5,2%%Date:8,2%");
                tStreamWriter.WriteLine("set HHMMSS=%time:~0,2%%time:~3,2%%time:~6,2%");
                tStreamWriter.WriteLine("set Instance=" + CommonVariables.DatabaseConfig.SDE_Instance);
                tStreamWriter.WriteLine("set Server=" + CommonVariables.DatabaseConfig.SDE_Server);
                tStreamWriter.WriteLine("set User=" + CommonVariables.DatabaseConfig.SDE_User);
                tStreamWriter.WriteLine("set Pw=" + CommonVariables.DatabaseConfig.SDE_Password);
                tStreamWriter.WriteLine("set DB=" + CommonVariables.DatabaseConfig.SDE_DataBase);
                tStreamWriter.WriteLine("");

                tStreamWriter.WriteLine("if not exist %BKDIR%\\%BKFILE%_LOGIC ( md %BKDIR%\\%BKFILE%_LOGIC ) else ( echo 目录 %BKDIR%\\%BKFILE%_LOGIC 已经存在)");

                foreach (string strName in this.m_ListFeatureClassName)
                {
                    tStreamWriter.WriteLine(string.Format("sdeexport -o create -l {0},shape -f %BKDIR%\\%BKFILE%_LOGIC\\{0}.xml -i %Instance% -s %Server%  -D %DB% -u %User% -p %Pw% ", strName));                   
                }

                /*
                tStreamWriter.WriteLine("echo 开始压缩文件…");
                tStreamWriter.WriteLine("rar a %BKDIR%\\%BKFILE%_LOGIC\\%BKFILE%%HHMMSS%_LOGIC_FULL.rar %BKDIR%\\*.dmp");
                tStreamWriter.WriteLine("echo 压缩文件 %BKFILE%%HHMMSS%_LOGIC_FULL.rar 完成");

                tStreamWriter.WriteLine("echo 开始移动文件…");
                tStreamWriter.WriteLine("move %BKDIR%\\*.dmp %BKDIR%\\%BKFILE%_LOGIC\\");
                tStreamWriter.WriteLine("move %BKDIR%\\*.log %BKDIR%\\%BKFILE%_LOGIC\\");
                tStreamWriter.WriteLine("echo 移动文件完成");

                tStreamWriter.WriteLine("echo 开始删除dmp文件...");
                tStreamWriter.WriteLine("del /f /s /q %BKDIR%\\%BKFILE%_LOGIC\\*.dmp");
                tStreamWriter.WriteLine("echo 删除dmp文件完成");
                 */

                tStreamWriter.WriteLine("net send %userdomain%  数据库逻辑备份已于:%DATE% %time% 完成！");
                tStreamWriter.WriteLine("echo .");
                tStreamWriter.WriteLine("echo 备份已于：%DATE% %time% 完成！");
                tStreamWriter.WriteLine("echo .");

                tStreamWriter.WriteLine("rem 移动批处理的日志文件到备份目录下面");
                tStreamWriter.WriteLine("xcopy %BAT_HOME%\\LogicBackup_%BKFILE%*.log %BKDIR%\\%BKFILE%_LOGIC\\");
                tStreamWriter.WriteLine("echo 数据已成功导出");
            }
        }

        /// <summary>
        /// 返回指定的星期
        /// </summary>
        /// <returns></returns>
        private string GetWeekChar()
        {
            switch (this.combWeek.SelectedIndex)
            {
                case 0:
                    return "Su";
                case 1:
                    return "M";
                case 2:
                    return "T";
                case 3:
                    return "W";
                case 4:
                    return "Th";
                case 5:
                    return "F";
                default:
                    return "S";
            }
        }

        #region 加载树节点

        #region 加载树节点

        /// <summary>
        /// 加载树节点
        /// </summary>
        private void AddTreeNode()
        {
            IList<TreeNode> tListTreeNode = new List<TreeNode>();
            string strHQL = "from AGSDM_DATASOURCE t";
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            IList tList = tEntityHandler.GetEntities(strHQL);
            for (int i = 0; i < tList.Count; i++)
            {
                AGSDM_DATASOURCE tDataSourse = tList[i] as AGSDM_DATASOURCE;
                TreeNode tRootNode = CreateNodeByDatasourse(tDataSourse);
                strHQL = string.Format("from AGSDM_DATASET as t where t.DATASOURCE_ID={0}", tDataSourse.ID);
                IList tDataSetList = tEntityHandler.GetEntities(strHQL);
                for (int j = 0; j < tDataSetList.Count; j++)
                {
                    AGSDM_DATASET tDataSet = tDataSetList[j] as AGSDM_DATASET;
                    TreeNode tSetTreeNode = CreateTreeNodeByDataSet(tDataSet);
                    #region 添加叶节点
                    strHQL = string.Format("from AGSDM_LAYER as t where t.DATASET_ID={0}", tDataSet.ID);
                    IList tDataLayerList = tEntityHandler.GetEntities(strHQL);
                    for (int k = 0; k < tDataLayerList.Count; k++)
                    {
                        AGSDM_LAYER tDataLayer = tDataLayerList[k] as AGSDM_LAYER;
                        TreeNode tLayerNode = CreateTreeNodeByDataLayer(tDataLayer);
                        tSetTreeNode.Nodes.Add(tLayerNode);
                    }
                    #endregion
                    tRootNode.Nodes.Add(tSetTreeNode);
                }
                this.tvwData.Nodes.Add(tRootNode);
                this.tvwData.ExpandAll();
            }

            foreach (TreeNode tr in this.tvwData.Nodes)
            {
                ///默认选择第一个数据集的节点
                if (tr.Nodes.Count > 0)
                {
                    this.tvwData.SelectedNode = tr.Nodes[0];

                    break;
                }
            }
        }

        /// <summary>
        /// 通过指定的数据生成叶节点
        /// </summary>
        /// <param name="pDataItem">数据目录</param>
        /// <returns>返回叶节点</returns>      
        private TreeNode CreateTreeNodeByDataLayer(AGSDM_LAYER pDataItem)
        {
            TreeNode tNode = new TreeNode();
            tNode.Text = pDataItem.LAYER_NAME;
            tNode.ImageIndex = GetImageIndex(pDataItem.FEATURE_TYPE);
            tNode.SelectedImageIndex = GetImageIndex(pDataItem.FEATURE_TYPE);
            if (CanReCancel(pDataItem.STATE))
            {
                tNode.Text += STR_CANCEL_DESP;
                tNode.ForeColor = Color.DarkGray;
            }
            tNode.Tag = pDataItem;
            return tNode;
        }

        /// <summary>
        /// 通过指定的数据生成树节点
        /// </summary>
        /// <param name="pDataItem">数据目录</param>
        /// <returns>返回树节点</returns>
        private TreeNode CreateNodeByDatasourse(AGSDM_DATASOURCE pDataItem)
        {
            TreeNode tNode = new TreeNode();
            tNode.Text = pDataItem.SOURCE_NAME;
            tNode.ImageIndex = 0;
            tNode.SelectedImageIndex = 0;
            if (CanReCancel(pDataItem.STATE))
            {
                tNode.ForeColor = Color.DarkGray;
                tNode.Text += STR_CANCEL_DESP;
            }
            tNode.Tag = pDataItem;
            return tNode;
        }

        /// <summary>
        /// 通过指定的数据生成子节点
        /// </summary>
        /// <param name="pDataItem">数据目录</param>
        /// <returns>返回子节点</returns>      
        private TreeNode CreateTreeNodeByDataSet(AGSDM_DATASET pDataItem)
        {
            TreeNode tNode = new TreeNode();
            tNode.Text = pDataItem.DATASET_NAME_CN;
            tNode.ImageIndex = 1;
            tNode.SelectedImageIndex = 1;
            if (CanReCancel(pDataItem.STATE))
            {
                tNode.Text += STR_CANCEL_DESP;
                tNode.ForeColor = Color.DarkGray;
            }
            else
            {
                //tNode.ForeColor = Color.White;
            }
            tNode.Tag = pDataItem;
            return tNode;
        }

        private int GetImageIndex(string pLayerType)
        {
            switch (pLayerType)
            {
                case "点":
                    return 4;
                case "线":
                    return 3;
                case "面":
                    return 2;
                default:
                    return 3;
            }
        }

        /// <summary>
        /// 判断是否可以反注销
        /// </summary>
        /// <param name="value">状态值</param>
        /// <returns></returns>
        private bool CanReCancel(string value)
        {
            return value == "0";
        }

        #endregion                                  

        #region 节点级联选择

        private void tvwData_AfterCheck(object sender, TreeViewEventArgs e)
        {
            ControlHelper.TreeViewRelateSelect(e, TreeViewRelateSelectDirection.All);

            //if (e.Action == TreeViewAction.ByMouse || e.Action == TreeViewAction.ByKeyboard)//
            //{
            //    //递归设置子节点选中状态
            //    this.SetChildNodeChecked(e.Node);
            //    //递归设置父节点选中状态
            //    this.SetParentNodeChecked(e.Node);
            //}
        }

        /// <summary>
        /// 递归设置父节点选中状态
        /// </summary>
        /// <param name="ptreeNode">当前节点</param>
        public void SetParentNodeChecked(TreeNode ptreeNode)
        {
            if (ptreeNode.Checked == true)
            {
                if (ptreeNode.Parent != null)
                {
                    ptreeNode.Parent.Checked = ptreeNode.Checked;
                    //递归设置父节点选中状态
                    SetParentNodeChecked(ptreeNode.Parent);
                }
            }
            else
            {
                if (ptreeNode.Parent != null)
                {
                    if (!HasChildChecked(ptreeNode.Parent))
                    {
                        ptreeNode.Parent.Checked = false;
                        SetParentNodeChecked(ptreeNode.Parent);
                    }
                }
            }
        }

        /// <summary>
        /// 子节点是否有选中
        /// </summary>
        /// <param name="ptreeNode"></param>
        /// <returns></returns>
        private bool HasChildChecked(TreeNode ptreeNode)
        {
            foreach (TreeNode subNode in ptreeNode.Nodes)
            {
                if (subNode.Checked) return true;
            }

            return false;
        }

        /// <summary>
        /// 递归设置子节点选中状态
        /// </summary>
        /// <param name="ptreeNode">当前节点</param>
        public void SetChildNodeChecked(TreeNode ptreeNode)
        {
            foreach (TreeNode treeNode in ptreeNode.Nodes)
            {
                treeNode.Checked = ptreeNode.Checked;
                //递归设置子节点选中状态
                SetChildNodeChecked(treeNode);
            }
        }

        #endregion

        #endregion
    }
}