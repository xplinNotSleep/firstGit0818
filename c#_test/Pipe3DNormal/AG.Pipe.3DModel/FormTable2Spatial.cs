using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using AG.COM.SDM.Utility;
using AG.COM.SDM.Utility.Common;
using AG.Pipe.Analyst3DModel.Editor;
using DevExpress.XtraBars.Docking;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using Microsoft.Win32;

namespace AG.Pipe.Analyst3DModel
{
    /// <summary>
    /// 数据图形化主窗口
    /// </summary>
    public partial class FormTable2Spatial : SkinForm
    {
        private FormMap m_FrmMap;
        private string m_SchemeFilePath = "";
        private string m_MDBpath = "";
        private Dictionary<string, ITable> m_SourseLayers = null;
        private string m_GDBpath = "";
        private IWorkspaceFactory Fact = null;
        private IFeatureWorkspace pFeatureWorkspace = null;
        private IEnumDataset pEnumDataset = null;
        PipeConvertManager convertManager = new PipeConvertManager();

        public string GDBpath
        {
            get { return m_GDBpath; }
            set { m_GDBpath = value; }
        }

        public string MDBpath
        {
            get { return m_MDBpath; }
            set { m_MDBpath = value; }
        }

        /// <summary>
        /// 初始化数字图形化窗体，传入方案配置文件
        /// </summary>
        /// <param name="ProjectFeaturePath"></param>
        public FormTable2Spatial(string ConvertSchemeFile)
        {
            InitializeComponent();

            m_SchemeFilePath = ConvertSchemeFile;

            this.FormClosed += Form_FormClosed;
            this.FormClosing += Form_FormClosing;

        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_SourseLayers != null)
            {
                foreach (ITable item in m_SourseLayers.Values)
                {
                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(item);
                }
            }
        }

        private void FormTable2Spatial_Load(object sender, EventArgs e)
        {
            try
            {
                //打开配置路径下的方案文件
                SysConfig.GetInstance().DataSchemeFile = m_SchemeFilePath;

                if (m_SchemeFilePath != "")
                {
                    //初始化图层树
                    OpenConvertScheme(m_SchemeFilePath);

                }

            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        /// <summary>
        /// 保存转换方案到默认配置的路径下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_SaveScheme_Click(object sender, EventArgs e)
        {
            string strSchemeFile = SysConfig.GetInstance().DataSchemeFile;

            if (strSchemeFile.Length == 0 || File.Exists(strSchemeFile) == false)
            {
                menu_SavaAsScheme_Click(null, null);
            }
            else
            {
                //保存数据核查方案到指定的文件路径
                SaveConvertScheme(strSchemeFile);
            }
        }

        /// <summary>
        /// 打开某路径下的数据转换方案并将默认路径设为此路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_OpenScheme_Click(object sender, EventArgs e)
        {
            OpenFileDialog tOpenFileDlg = new OpenFileDialog();
            tOpenFileDlg.Filter = "数据转换方案(*.xml)|*.xml";
            tOpenFileDlg.Title = "选择数据转换方案";

            if (tOpenFileDlg.ShowDialog() == DialogResult.OK)
            {
                OpenConvertScheme(tOpenFileDlg.FileName);
                SysConfig.GetInstance().DataSchemeFile = tOpenFileDlg.FileName;
            }
        }

        /// <summary>
        /// 保存当前转换方案到指定路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_SavaAsScheme_Click(object sender, EventArgs e)
        {
            SaveFileDialog tSaveFileDialog = new SaveFileDialog();
            tSaveFileDialog.DefaultExt = "xml";
            tSaveFileDialog.Filter = "数据转换方案(*.xml)|*.xml";
            tSaveFileDialog.Title = "指定数据转换方案保存路径";

            if (tSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveConvertScheme(tSaveFileDialog.FileName);

                //SysConfig tSysConfig = SysConfig.GetInstance();
                //tSysConfig.DataSchemeFile = tSaveFileDialog.FileName;
            }
        }

        /// <summary>
        /// 设置数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_SetDataSource_Click(object sender, EventArgs e)
        {
            //规则核查图层
            List<string> DicConvertSetLayers = new List<string>();
            //添加勾选的到待转换数据标准名称列表中
            foreach (TreeNode ManagerNode in this.treeCheckScheme.Nodes)
            {
                foreach (TreeNode groupNode in ManagerNode.Nodes)
                {
                    foreach (TreeNode setNode in groupNode.Nodes)
                    {
                        if (setNode.Checked && setNode.Tag is ConvertLayerSet)
                        {
                            ConvertLayerSet layerSet = setNode.Tag as ConvertLayerSet;

                            if (string.IsNullOrEmpty(layerSet.LineStandardName) ||
                                string.IsNullOrEmpty(layerSet.PointStandardName)) continue;

                            if (!DicConvertSetLayers.Exists(m => m == layerSet.LineStandardName))
                                DicConvertSetLayers.Add(layerSet.LineStandardName);
                            if (!DicConvertSetLayers.Exists(m => m == layerSet.PointStandardName))
                                DicConvertSetLayers.Add(layerSet.PointStandardName);

                        }
                    }
                }
            }
            FormSetDataSource formSetDataSource = new FormSetDataSource();
            formSetDataSource.DicDataCheckLayer = DicConvertSetLayers;
            DialogResult dr = formSetDataSource.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Dictionary<string, string> DicFeatName = formSetDataSource.DicDataName;
                Dictionary<string, ITable> DicTable = formSetDataSource.DicTable;
                //Dictionary<string, IFeatureClass> DictFeatClass = formSetDataSource.DictFeatClass;
                SetDataSource(DicTable, DicFeatName);

            }
        }


        private bool canGoOn = true;
        /// <summary>
        /// 是否可以继续下一步,如果转换存在错误，需要提示用户
        /// </summary>
        public bool CanGoOn
        {
            get { return canGoOn; }
        }

        /// <summary>
        /// 点击运行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_RunCheck_Click(object sender, EventArgs e)
        {
            IList<InputRecord> inputRecords;

            try
            {
                string tMDBpath = m_MDBpath;
                if (tMDBpath == "")
                {
                    MessageBox.Show("未设置数据源，请重新设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                inputRecords = StartRunCheckEx();//设置保存GDB的路径并导出GDB
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");

                return;
            }
            if (inputRecords != null)
            {
                if (MessageBox.Show("数据转换已经完成,是否显示转换过程中的记录信息?", "提示信息", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    this.m_FrmMap = new FormMap();
                    m_FrmMap.Show();

                    if (Directory.Exists(GDBpath))
                    {
                        this.m_FrmMap.LoadGdb(GDBpath);
                    }

                    //绑定错误信息到列表
                    this.m_FrmMap.BindListErr(inputRecords);
                    
                }
            }
        }


        private void menu_NewScheme_Click(object sender, EventArgs e)
        {
            //获取系统配置信息
            SysConfig tSysConfig = SysConfig.GetInstance();
            tSysConfig.DataSchemeFile = "";

            //if (this.m_FrmErrTable != null)
            //{
            //    //清除错误记录列表
            //    this.m_FrmErrTable.ClearListItems();
            //}

            //新建数据核查方案
            NewDataCheckScheme();
        }


        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(pEnumDataset);
            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(pFeatureWorkspace);
            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(Fact);
            if (m_SourseLayers != null)
            {
                foreach (KeyValuePair<string, ITable> item in m_SourseLayers)
                {
                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(item.Value);
                }
            }
            GC.Collect();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            menu_SetDataSource_Click(sender, e);
        }


        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (this.m_FrmMap == null)
            {
                MessageBox.Show("转换日志为空，请先进行转换！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                this.m_FrmMap.ExportErrRecord();
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存错误记录文件过程中出错.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void 恢复默认检查方案ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string m_tempSchemeFilePath = CommonConstString.STR_ConfigPath + "\\PipeConvert.cml";
            OpenConvertScheme(m_tempSchemeFilePath);
        }

        /// <summary>
        /// 点击树视图中某一列表项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeCheckScheme_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode treeNode = e.Node;
            if (treeNode != null)
            {
                if (treeNode.Tag != null)
                    this.propNodeAttribute.SelectedObject = new SchemeTypeDescriptor(treeNode.Tag);

            }
        }

        /// <summary>
        /// 勾选树视图中的某一列表项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeCheckScheme_AfterCheck(object sender, TreeViewEventArgs e)
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
        /// 将数据源界面设置好的数据源引用到转换方案的配置中
        /// </summary>
        /// <param name="DictTable"></param>
        /// <param name="DictFeatName"></param>
        private void SetDataSource(Dictionary<string, ITable> DictTable, Dictionary<string, string> DictFeatName)
        {
            foreach (TreeNode managerNode in this.treeCheckScheme.Nodes)
            {
                foreach (TreeNode groupNode in managerNode.Nodes)
                {
                    foreach (TreeNode setNode in groupNode.Nodes) //图层方案
                    {
                        if (setNode.Checked)
                        {
                            if (setNode.Tag is ConvertLayerSet)
                            {
                                ConvertLayerSet layerSet = setNode.Tag as ConvertLayerSet;
                                string lineTableName = "";
                                string ptTableName = "";
                                if (DictFeatName.ContainsKey(layerSet.LineStandardName))
                                {
                                    lineTableName = DictFeatName[layerSet.LineStandardName];
                                }
                                if (DictFeatName.ContainsKey(layerSet.PointStandardName))
                                {
                                    ptTableName = DictFeatName[layerSet.PointStandardName];
                                }
                                if (DictTable.ContainsKey(lineTableName))
                                {
                                    layerSet.LineSource = DictTable[lineTableName];
                                    layerSet.LineLayerName = (layerSet.LineSource as IDataset).Name;
                                    m_MDBpath = (layerSet.LineSource as IDataset).Workspace.PathName;
                                }
                                if (DictTable.ContainsKey(ptTableName))
                                {
                                    layerSet.PointSource = DictTable[ptTableName];
                                    layerSet.PointLayerName = (layerSet.PointSource as IDataset).Name;
                                    m_MDBpath = (layerSet.PointSource as IDataset).Workspace.PathName;
                                }
                            }
                        }

                    }

                }
            }
        }

        /// <summary>
        /// 加载数据图形化转换方案
        /// </summary>
        public void OpenConvertScheme(string m_tempSchemeFilePath)
        {
            try
            {
                //读配置路径下的图形转换方案xml
                if (!File.Exists(m_tempSchemeFilePath))
                {
                    return;
                }

                XmlSerializer tXmlSerializer = new XmlSerializer(typeof(PipeConvertManager));
                StreamReader tStreamReaderScheme = new StreamReader(m_tempSchemeFilePath);
                convertManager = tXmlSerializer.Deserialize(tStreamReaderScheme) as PipeConvertManager;
                convertManager.SchemePropertyValueChanged += ItemPropertyValueChanged;
                tStreamReaderScheme.Close();
                this.treeCheckScheme.Nodes.Clear();
                TreeNode rootNode = new TreeNode();
                rootNode.Text = convertManager.Name;
                rootNode.Tag = convertManager;
                //遍历方案管理下的图层大类组
                for (int i = 0; i < convertManager.PipeConvertGroups.Count; i++)
                {
                    PipeConvertGroup pipeConvertGroup = convertManager.PipeConvertGroups[i];
                    AddTreeItem(rootNode, pipeConvertGroup);
                }
                this.treeCheckScheme.Nodes.Add(rootNode);
            }
            catch (Exception ex)
            {
                string eeee = ex.Message;
                MessageBox.Show(eeee);
            }
        }

        /// <summary>
        /// 在管线方案配置文件里面把管线管点方案加载到树状列表中
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="tScheme"></param>
        public void AddTreeItem(TreeNode parentNode, ConvertLayerSet convertLayerSet)
        {
            //先清空数据源名称配置
            convertLayerSet.LineLayerName = "";
            convertLayerSet.PointLayerName = "";
            //convertLayerSet.Points.Clear();
            //convertLayerSet.LinePoints.Clear();
            convertLayerSet.SchemePropertyValueChanged += ItemPropertyValueChanged;

            TreeNode rootNode = new TreeNode();
            rootNode.Text = convertLayerSet.Name;
            rootNode.Tag = convertLayerSet;
            parentNode.Nodes.Add(rootNode);

        }


        public void AddTreeItem(TreeNode parentNode, PipeConvertGroup tGroup)
        {
            tGroup.SchemePropertyValueChanged += ItemPropertyValueChanged;
            TreeNode rootNode = new TreeNode();
            rootNode.Text = tGroup.Name;
            rootNode.Tag = tGroup;
            for (int i = 0; i < tGroup.ConvertLayerSets.Count; i++)
            {
                ConvertLayerSet convertLayerSet = tGroup.ConvertLayerSets[i];
                AddTreeItem(rootNode, convertLayerSet);
            }
            parentNode.Nodes.Add(rootNode);
        }

        /// <summary>
        /// 打开指定路径的数据转化方案(读取方案文件中的配置内容将其反序列化)
        /// </summary>
        /// <param name="schemeFile"></param>
        public void OpenConvertScheme_Old(string schemeFile)
        {
            //如果指定路径的数据核查方案路径不存在
            if (File.Exists(schemeFile) == false) return;

            System.IO.FileStream fs = new System.IO.FileStream(schemeFile, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                //反序列化
                TreeNode rootNode = (TreeNode)formatter.Deserialize(fs);

                //先清空
                this.treeCheckScheme.Nodes.Clear();

                //添加根节点
                this.treeCheckScheme.Nodes.Add(rootNode);

                //注册刷新事件
                this.RegisterItemPropertyEventHandler(rootNode);

                //展开节点
                this.treeCheckScheme.Nodes[0].ExpandAll();
                //MessageBox.Show("OK");

                //自动选中所有树节点
                CheckAllNode(treeCheckScheme.Nodes);
            }
            catch (SerializationException ex1)
            {
                MessageBox.Show("打开数据转换方案序列化出错,消息:" + ex1.Message);
                SysConfig.log.Info("打开数据转换方案序列化出错,错误:" + ex1);
            }
            catch (Exception ex2)
            {
                MessageBox.Show("打开数据转换方案出错,错误:" + ex2);
                SysConfig.log.Info("打开数据转换方案序列化出错,错误:" + ex2);
            }
            finally
            {
                fs.Close();
            }
        }

        /// <summary>
        /// 保存数据核查方案到指定的文件路径(读取方案文件中的配置内容将其序列化)
        /// </summary>
        /// <param name="pSchemeFile">数据核查方案文件</param>
        public void SaveDataCheckScheme_Old(string pSchemeFile)
        {
            System.IO.FileStream fs = new System.IO.FileStream(pSchemeFile, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();

            TreeNode rootNode = this.treeCheckScheme.Nodes[0];

            //注销事件
            DestroyItemPropertyEventHandler(rootNode);

            try
            {
                formatter.Serialize(fs, rootNode);

                MessageBox.Show("转换方案文件保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SerializationException e)
            {
                SysConfig.log.Info("序列化失败. Reason: ", e);
                MessageBox.Show(e.Message);
            }
            finally
            {
                fs.Close();
            }

            //重新注册事件
            RegisterItemPropertyEventHandler(rootNode);
        }

        /// <summary>
        /// 保存数据核查方案到指定文件路径
        /// </summary>
        /// <param name="pSchemeFile"></param>
        public void SaveConvertScheme(string pSchemeFile)
        {
            try
            {
                if (!File.Exists(pSchemeFile))
                {
                    return;
                }

                XmlSerializer tXmlSerializer = new XmlSerializer(typeof(PipeConvertManager));
                StreamWriter tStreamWriter = new StreamWriter(pSchemeFile, false);
                tXmlSerializer.Serialize(tStreamWriter, convertManager);
                tStreamWriter.Close();
                MessageBox.Show("保存成功!", "提示");
            }
            catch (Exception ex)
            {
                string eeee = ex.Message;
            }
        }

        /// <summary>
        /// 新建数据核查方案
        /// </summary>  
        public void NewDataCheckScheme()
        {
            this.treeCheckScheme.Nodes.Clear();

            //初始化数据核查方案属性实例
            PipeConvertManager tConvertManager = new PipeConvertManager();
            tConvertManager.SchemePropertyValueChanged += new SchemeValueChangedEventHandler
                (ItemPropertyValueChanged);

            TreeNode rootNode = new TreeNode();
            rootNode.Text = tConvertManager.Name;
            rootNode.Tag = tConvertManager;

            this.treeCheckScheme.Nodes.Add(rootNode);
        }

        private void ItemPropertyValueChanged(object sender, SchemePropertyEventArgs e)
        {
            ISchemeName itemProperty = e.ItemProperty as ISchemeName;
            TreeNode selTreeNode = this.treeCheckScheme.SelectedNode;
            selTreeNode.Text = itemProperty.Name;
        }

        /// <summary>
        /// 注销ItemProperty事件（递归调用）
        /// </summary>
        /// <param name="rootNode">根节点</param>
        private void DestroyItemPropertyEventHandler(TreeNode rootNode)
        {
            //引用类型转换
            ISchemeValueChanged tRootItemProperty = rootNode.Tag as ISchemeValueChanged;
            //注销事件
            tRootItemProperty.SchemePropertyValueChanged -= new SchemeValueChangedEventHandler(ItemPropertyValueChanged);

            foreach (TreeNode childNode in rootNode.Nodes)
            {
                //注销ItemProperty事件（递归调用）
                DestroyItemPropertyEventHandler(childNode);
            }
        }

        /// <summary>
        /// 注册ItemProperty事件（递归调用）
        /// </summary>
        /// <param name="rootNode">根节点</param>
        private void RegisterItemPropertyEventHandler(TreeNode rootNode)
        {

            //引用类型转换
            ISchemeValueChanged tRootItemProperty = rootNode.Tag as ISchemeValueChanged;
            if (tRootItemProperty == null) return;
            //注册事件
            tRootItemProperty.SchemePropertyValueChanged += new SchemeValueChangedEventHandler(ItemPropertyValueChanged);

            #region
            //if (rootNode.Tag is PipeConvertManager)
            //{
            //    rootNode.SelectedImageIndex = 1;
            //    rootNode.ImageIndex = 1;
            //}
            //else if (rootNode.Tag is DataCheckGroupLayer)
            //{
            //    rootNode.SelectedImageIndex = 3;
            //    rootNode.ImageIndex = 3;
            //}
            //else if (rootNode.Tag is DataCheckLayerSet)
            //{
            //    rootNode.SelectedImageIndex = 2;
            //    rootNode.ImageIndex = 2;
            //}
            #endregion

            foreach (TreeNode childNode in rootNode.Nodes)
            {
                //注册事件
                RegisterItemPropertyEventHandler(childNode);
            }
        }

        /// <summary>
        /// 设置保存GDB的路径
        /// </summary>
        /// <returns></returns>
        public IList<InputRecord> StartRunCheckEx()
        {
            TreeNode rootNode = this.treeCheckScheme.Nodes[0];
            PipeConvertManager convertManager = rootNode.Tag as PipeConvertManager;
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "选择保存的路径";
            string WorkspacePath = string.Empty;
            IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
            IWorkspaceName workspaceName;
            WorkspacePath = convertManager.SaveDic;
            #region 不需要在转换前还在弹框中设置导出路径
            //if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            //{
            //    WorkspacePath = folderBrowserDialog.SelectedPath;
            //}
#endregion
            if (string.IsNullOrWhiteSpace(WorkspacePath))
            {
                if (MessageBox.Show("文件保存路径为空，是否默认保存到桌面？。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    RegistryKey folders;
                    folders = OpenRegistryPath(Registry.CurrentUser, @"\software\microsoft\windows\currentversion\explorer\shell folders");
                    if (m_MDBpath == "")
                    {
                        workspaceName = workspaceFactory.Create(folders.GetValue("Desktop").ToString(), "GDB" + DateTime.Now.ToString("yyyyMMddHHmmss"), null, 0);
                    }
                    else
                    {
                        FileInfo tFile = new FileInfo(m_MDBpath);
                        workspaceName = workspaceFactory.Create(folders.GetValue("Desktop").ToString(), tFile.Name.Replace(".mdb", "") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss"), null, 0);
                    }
                    WorkspacePath = folders.GetValue("Desktop").ToString();
                    convertManager.SaveDic = folders.GetValue("Desktop").ToString();
                    
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if(!Directory.Exists(WorkspacePath))
                {
                    WorkspacePath="D://";
                }

                //convertManager.SaveDic = WorkspacePath;
                
                if (m_MDBpath == "")
                {
                    workspaceName = workspaceFactory.Create(WorkspacePath, "GDB" + DateTime.Now.ToString("yyyyMMddHHmmss"), null, 0);
                }
                else
                {
                    FileInfo tFile = new FileInfo(m_MDBpath);
                    workspaceName = workspaceFactory.Create(WorkspacePath, tFile.Name.Replace(".mdb", "") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss"), null, 0);
                }
                
            }
            IName name = (IName)workspaceName;
            IWorkspace workspace = (IWorkspace)name.Open();
            //convertManager.WorkSpace = workspace;
            convertManager.SaveDic = WorkspacePath;
            m_GDBpath = workspace.PathName;

            IList<InputRecord> inputRecords = new List<InputRecord>();

            int checkLayerCount = GetCheckLayerCount();
            ITrackProgress trackProgress = new TrackProgressDialog();
            trackProgress.DisplayTotal = true;
            trackProgress.SubMax = checkLayerCount;
            trackProgress.SubValue = 0;
            trackProgress.AutoFinishClose = true;
            trackProgress.Show();
            Application.DoEvents();

            foreach (TreeNode layerNode in rootNode.Nodes)
            {
                if (layerNode.Checked == false) continue;
                //遍历管线转换方案下的子节点类型
                //若为图层组(管线大类)
                if (layerNode.Tag is PipeConvertGroup)
                {
                    foreach (TreeNode tlayerNode in layerNode.Nodes)
                    {
                        if (tlayerNode.Tag is ConvertLayerSet)
                        {
                            if (tlayerNode.Checked == false) continue;
                            ConvertLayerSet tLayerSet = tlayerNode.Tag as ConvertLayerSet;

                            trackProgress.SubMessage = "正在生成管线图层集:" + tLayerSet.Name;
                            trackProgress.SubValue++;
                            Application.DoEvents();

                            if (tLayerSet == null) continue;
                            //检查数据源是否为空
                            if (tLayerSet.LineSource == null || tLayerSet.PointSource == null)
                            {
                                continue;

                            }
                            List<InputRecord> records = tLayerSet.StartConvert(workspace, convertManager, trackProgress);
                            inputRecords.Add(records[0]);
                            inputRecords.Add(records[1]);
                        }
                    }
                }
                //若为质检图层集(管线小类，包含管线管点图层)
                else if (layerNode.Tag is ConvertLayerSet)
                {
                    if (layerNode.Checked == false) continue;
                    ConvertLayerSet tLayerSet = layerNode.Tag as ConvertLayerSet;

                    trackProgress.SubMessage = "正在生成管线图层集:" + tLayerSet.Name;
                    trackProgress.SubValue++;
                    Application.DoEvents();

                    if (tLayerSet == null) continue;
                    //检查数据源是否为空
                    if (tLayerSet.LineSource == null || tLayerSet.PointSource == null)
                    {
                        continue;

                    }
                    List<InputRecord> records = tLayerSet.StartConvert(workspace, convertManager, trackProgress);
                    inputRecords.Add(records[0]);
                    inputRecords.Add(records[1]);
                }

            }
            trackProgress.SetFinish();
            trackProgress = null;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workspace);
            workspace = null;
            return inputRecords;
        }

        /// <summary>
        /// 选中所有子级节点
        /// </summary>
        /// <param name="nodes"></param>
        private void CheckAllNode(TreeNodeCollection nodes)
        {
            foreach (TreeNode nodeChild in nodes)
            {
                nodeChild.Checked = true;

                CheckAllNode(nodeChild.Nodes);
            }
        }

        private RegistryKey OpenRegistryPath(RegistryKey root, string s)
        {
            s = s.Remove(0, 1) + @"\";
            while (s.IndexOf(@"\") != -1)
            {
                root = root.OpenSubKey(s.Substring(0, s.IndexOf(@"\")));
                s = s.Remove(0, s.IndexOf(@"\") + 1);
            }
            return root;
        }

        /// <summary>
        /// 获取勾选的管线小类数目
        /// </summary>
        /// <returns></returns>
        private int GetCheckLayerCount()
        {
            int count = 0;
            TreeNode parentNode = this.treeCheckScheme.Nodes[0];

            foreach (TreeNode layerNode in parentNode.Nodes)
            {
                if (layerNode.Checked == false) continue;
                //引用类型转换
                if (layerNode.Tag is ConvertLayerSet)
                {
                    count++;
                }
                else if (layerNode.Tag is PipeConvertGroup)
                {
                    foreach (TreeNode tlayerNode in layerNode.Nodes)
                    {
                        if (tlayerNode.Checked == false) continue;
                        if (tlayerNode.Tag is ConvertLayerSet)
                        {
                            count++;
                        }
                    }
                }

            }

            return count;
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