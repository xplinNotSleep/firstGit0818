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
    /// ���ݱ��� ����
    /// </summary>
    public partial class DataBaseBackupDialog : Form
    {
        /// <summary>
        /// ע������
        /// </summary>
        private static string STR_CANCEL_DESP = "(��ע��)";

        private IList<string> m_ListFeatureClassName=new List<string>();

        /// <summary>
        /// ��ʼ��ʵ������
        /// </summary>
        public DataBaseBackupDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��ȡ��Ҫ���ݵ�ͼ���б���
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
        /// ��ȡ��ѡ���featureclass������
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
        /// ��ѡ���ͼ����ӵ�һ��list
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
            using (StreamWriter tStreamWriter = new StreamWriter(strBatfile, false, Encoding.GetEncoding(936)))    //936  ��������
            {
                tStreamWriter.WriteLine("@echo off");
                tStreamWriter.WriteLine("rem ��Ҫ���õĲ���˵��:");
                tStreamWriter.WriteLine("rem dump_Dir:��Ҫ���ݵ������ļ�Ŀ¼��");

                tStreamWriter.WriteLine("at 1 /delete/yes");
              
                if (this.combFrequency.SelectedIndex == 1)
                {
                    tStreamWriter.WriteLine(string.Format("at {0}:{1} /every:M,T,W,Th,F cmd /c {2}", numHour.Value, numMinute.Value, CommonConstString.STR_ConfigPath + "\\databackup.bat"));
                }
                else if(this.combFrequency.SelectedIndex==2)
                {
                    //����ָ��������
                    string strWeek = this.GetWeekChar();
                    tStreamWriter.WriteLine(string.Format("at {0}:{1} /every:{2} cmd /c {3}", numHour.Value, numMinute.Value,strWeek, CommonConstString.STR_ConfigPath + "\\databackup.bat"));
                }

            }
      
            //д��������������
            this.WriteDataBackupBat();

            //������ɾ������ƻ�
            System.Diagnostics.Process.Start(strBatfile);

            MessageBox.Show("���ݲ����ѳɹ�ִ��");
            this.Close();
        }

        /// <summary>
        /// д��������������
        /// </summary>
        private void WriteDataBackupBat()
        {
            //��ȡ��ѡ��featureclass������
            this.m_ListFeatureClassName = GetSelectFeatureClassName(tvwData.Nodes);

            string strBatfile = CommonConstString.STR_ConfigPath + "\\databackup.bat";
            using (StreamWriter tStreamWriter = new StreamWriter(strBatfile, false,Encoding.Unicode))
            {
                tStreamWriter.WriteLine("@echo off");
                //tStreamWriter.WriteLine("rem call DataBackup.bat %Dump_Dir%");
                tStreamWriter.WriteLine("rem =============================================");
                //tStreamWriter.WriteLine("rem ��������ʵ��֧��SDE���ݱ���,����Ϊ:�������ݵ�dump_dir�ļ�Ŀ¼��,Ȼ");
                //tStreamWriter.WriteLine("rem ���ļ���rar��ʽ���뵱�챸������������Ŀ¼��,�����Ժ������ڻָ�");
                //tStreamWriter.WriteLine("rem");
                tStreamWriter.WriteLine("rem ��Ҫ���õĲ���˵��:");
                //tStreamWriter.WriteLine("Bat_Home:�������������ڵ�Ŀ¼��");
                tStreamWriter.WriteLine("rem BKDIR���ǵ��ñ�������ʱ�������Ĳ��� BKFILE��Ҫ���ݵ��������ļ�Ŀ¼��");
                tStreamWriter.WriteLine("rem Author:����");
                tStreamWriter.WriteLine("rem ==================================================");

                tStreamWriter.WriteLine("echo ���ݿ�ʼ...");
                tStreamWriter.WriteLine("echo ��ǰ��ʱ���ǣ� %DATE% %time%");
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

                tStreamWriter.WriteLine("if not exist %BKDIR%\\%BKFILE%_LOGIC ( md %BKDIR%\\%BKFILE%_LOGIC ) else ( echo Ŀ¼ %BKDIR%\\%BKFILE%_LOGIC �Ѿ�����)");

                foreach (string strName in this.m_ListFeatureClassName)
                {
                    tStreamWriter.WriteLine(string.Format("sdeexport -o create -l {0},shape -f %BKDIR%\\%BKFILE%_LOGIC\\{0}.xml -i %Instance% -s %Server%  -D %DB% -u %User% -p %Pw% ", strName));                   
                }

                /*
                tStreamWriter.WriteLine("echo ��ʼѹ���ļ���");
                tStreamWriter.WriteLine("rar a %BKDIR%\\%BKFILE%_LOGIC\\%BKFILE%%HHMMSS%_LOGIC_FULL.rar %BKDIR%\\*.dmp");
                tStreamWriter.WriteLine("echo ѹ���ļ� %BKFILE%%HHMMSS%_LOGIC_FULL.rar ���");

                tStreamWriter.WriteLine("echo ��ʼ�ƶ��ļ���");
                tStreamWriter.WriteLine("move %BKDIR%\\*.dmp %BKDIR%\\%BKFILE%_LOGIC\\");
                tStreamWriter.WriteLine("move %BKDIR%\\*.log %BKDIR%\\%BKFILE%_LOGIC\\");
                tStreamWriter.WriteLine("echo �ƶ��ļ����");

                tStreamWriter.WriteLine("echo ��ʼɾ��dmp�ļ�...");
                tStreamWriter.WriteLine("del /f /s /q %BKDIR%\\%BKFILE%_LOGIC\\*.dmp");
                tStreamWriter.WriteLine("echo ɾ��dmp�ļ����");
                 */

                tStreamWriter.WriteLine("net send %userdomain%  ���ݿ��߼���������:%DATE% %time% ��ɣ�");
                tStreamWriter.WriteLine("echo .");
                tStreamWriter.WriteLine("echo �������ڣ�%DATE% %time% ��ɣ�");
                tStreamWriter.WriteLine("echo .");

                tStreamWriter.WriteLine("rem �ƶ����������־�ļ�������Ŀ¼����");
                tStreamWriter.WriteLine("xcopy %BAT_HOME%\\LogicBackup_%BKFILE%*.log %BKDIR%\\%BKFILE%_LOGIC\\");
                tStreamWriter.WriteLine("echo �����ѳɹ�����");
            }
        }

        /// <summary>
        /// ����ָ��������
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

        #region �������ڵ�

        #region �������ڵ�

        /// <summary>
        /// �������ڵ�
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
                    #region ���Ҷ�ڵ�
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
                ///Ĭ��ѡ���һ�����ݼ��Ľڵ�
                if (tr.Nodes.Count > 0)
                {
                    this.tvwData.SelectedNode = tr.Nodes[0];

                    break;
                }
            }
        }

        /// <summary>
        /// ͨ��ָ������������Ҷ�ڵ�
        /// </summary>
        /// <param name="pDataItem">����Ŀ¼</param>
        /// <returns>����Ҷ�ڵ�</returns>      
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
        /// ͨ��ָ���������������ڵ�
        /// </summary>
        /// <param name="pDataItem">����Ŀ¼</param>
        /// <returns>�������ڵ�</returns>
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
        /// ͨ��ָ�������������ӽڵ�
        /// </summary>
        /// <param name="pDataItem">����Ŀ¼</param>
        /// <returns>�����ӽڵ�</returns>      
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
                case "��":
                    return 4;
                case "��":
                    return 3;
                case "��":
                    return 2;
                default:
                    return 3;
            }
        }

        /// <summary>
        /// �ж��Ƿ���Է�ע��
        /// </summary>
        /// <param name="value">״ֵ̬</param>
        /// <returns></returns>
        private bool CanReCancel(string value)
        {
            return value == "0";
        }

        #endregion                                  

        #region �ڵ㼶��ѡ��

        private void tvwData_AfterCheck(object sender, TreeViewEventArgs e)
        {
            ControlHelper.TreeViewRelateSelect(e, TreeViewRelateSelectDirection.All);

            //if (e.Action == TreeViewAction.ByMouse || e.Action == TreeViewAction.ByKeyboard)//
            //{
            //    //�ݹ������ӽڵ�ѡ��״̬
            //    this.SetChildNodeChecked(e.Node);
            //    //�ݹ����ø��ڵ�ѡ��״̬
            //    this.SetParentNodeChecked(e.Node);
            //}
        }

        /// <summary>
        /// �ݹ����ø��ڵ�ѡ��״̬
        /// </summary>
        /// <param name="ptreeNode">��ǰ�ڵ�</param>
        public void SetParentNodeChecked(TreeNode ptreeNode)
        {
            if (ptreeNode.Checked == true)
            {
                if (ptreeNode.Parent != null)
                {
                    ptreeNode.Parent.Checked = ptreeNode.Checked;
                    //�ݹ����ø��ڵ�ѡ��״̬
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
        /// �ӽڵ��Ƿ���ѡ��
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
        /// �ݹ������ӽڵ�ѡ��״̬
        /// </summary>
        /// <param name="ptreeNode">��ǰ�ڵ�</param>
        public void SetChildNodeChecked(TreeNode ptreeNode)
        {
            foreach (TreeNode treeNode in ptreeNode.Nodes)
            {
                treeNode.Checked = ptreeNode.Checked;
                //�ݹ������ӽڵ�ѡ��״̬
                SetChildNodeChecked(treeNode);
            }
        }

        #endregion

        #endregion
    }
}