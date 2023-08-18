using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.Catalog;
using AG.COM.SDM.Catalog.DataItems;
using AG.COM.SDM.Catalog.Filters;
using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Microsoft.Win32;
using Table2Spatial.CoClass;

namespace Table2Spatial
{
    /// <summary>
    /// 数据核查方案窗体类
    /// </summary>
    [Serializable]
    public partial class FormDataScheme :DockDocument
    {
        private string m_GDBpath = "";

        public string GDBpath
        {
            get
            {
               return m_GDBpath;
            }
        }

        private string m_MDBpath = "";

        public string MDBpath
        {
            get { return m_MDBpath; }
            set { m_MDBpath = value; }
        }

        /// <summary>
        /// 初始化实例对象
        /// </summary>
        public FormDataScheme()
        {
            InitializeComponent();
        }

        public FormDataScheme(bool withoutFormShow)
        {
            InitializeComponent();
            if (withoutFormShow)
            {
                FormDataScheme_Load(null, null);
            }
        }

        private void FormDataScheme_Load(object sender, EventArgs e)
        {
            try
            {
                string strScheme = SysConfig.GetInstance().DataSchemeFile;
                if (File.Exists(strScheme) == false)
                {
                    //初始化数据核查方案属性实例
                    DataCheckScheme tDataCheckScheme = new DataCheckScheme();
                    tDataCheckScheme.ItemPropertyValueChanged += new ItemPropertyValueChangedEventHandler(ItemPropertyValueChanged);

                    TreeNode rootNode = new TreeNode();
                    rootNode.Text = tDataCheckScheme.ItemName;
                    rootNode.Tag = tDataCheckScheme;
                    rootNode.SelectedImageIndex = 0;
                    rootNode.StateImageIndex = 0;

                    this.treeCheckScheme.Nodes.Add(rootNode);
                    this.treeCheckScheme.SelectedNode = rootNode;
                }
                else
                {
                    //外部已经调用了初始化树的方法，这里的屏蔽

                    ////打开默认的数据核查方案
                    //OpenDataCheckScheme(strScheme);

                    ////根节点
                    //TreeNode rootNode = this.treeCheckScheme.Nodes[0];
                    //rootNode.Expand();
                    //this.treeCheckScheme.SelectedNode = rootNode;                    
                }              
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
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

        private void treeCheckScheme_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (this.treeCheckScheme.SelectedNode != e.Node)
                this.treeCheckScheme.SelectedNode = e.Node;

            if (e.Node.Parent == null)
            {
                this.treeCheckScheme.ContextMenuStrip = this.contextMenuScheme;
            }
            else if (e.Node.Tag is DataCheckLayer)
            {
                this.treeCheckScheme.ContextMenuStrip = this.contextMenuLayer;              
            }
            else if (e.Node.Tag is DataCheckGroupLayer)
            {
                
                this.treeCheckScheme.ContextMenuStrip = this.contextMenuGroupLayer;
            }
            else
            {
                this.treeCheckScheme.ContextMenuStrip = this.contextMenuLayerSet;
            }
        }

        private void menu_AddLayer_Click(object sender, EventArgs e)
        {
            //初始化数据核查图层实例
            DataCheckLayer tRuleLayer = new DataCheckLayer();
            tRuleLayer.ItemPropertyValueChanged += new ItemPropertyValueChangedEventHandler(ItemPropertyValueChanged);

            TreeNode ttreeNode = new TreeNode();
            ttreeNode.Text = tRuleLayer.ItemName;
            ttreeNode.Tag = tRuleLayer;
            ttreeNode.ImageIndex = 1;
            ttreeNode.SelectedImageIndex  = 1;
            this.treeCheckScheme.SelectedNode.Nodes.Add(ttreeNode);
            ttreeNode.Parent.Expand();
            this.treeCheckScheme.SelectedNode = ttreeNode;
            //this.treeCheckScheme.Nodes[0].Nodes.Add(ttreeNode);
            //ttreeNode.Parent.Expand();
            //this.treeCheckScheme.SelectedNode = ttreeNode;
        }
        private void menu_AddGroupLayer_Click(object sender, EventArgs e)
        {
            DataCheckGroupLayer tDataCheckGroupLayer = new DataCheckGroupLayer();
            tDataCheckGroupLayer.ItemPropertyValueChanged += new ItemPropertyValueChangedEventHandler(ItemPropertyValueChanged);

            TreeNode rootNode = new TreeNode();
            rootNode.Text = tDataCheckGroupLayer.ItemName;
            rootNode.Tag = tDataCheckGroupLayer;
            rootNode.SelectedImageIndex = 3;
            rootNode.ImageIndex = 3;
            //rootNode.StateImageIndex = 0;

            this.treeCheckScheme.Nodes[0].Nodes.Add(rootNode);
            this.treeCheckScheme.SelectedNode = rootNode;
        }
        private void menu_DeleteLayer_Click(object sender, EventArgs e)
        {
            TreeNode tSelNode = this.treeCheckScheme.SelectedNode;
            tSelNode.Remove();
        }

        private void menu_AddRule_Click(object sender, EventArgs e)
        {
            //FormCheckRule tFrmCheckRule = new FormCheckRule();
            //if (tFrmCheckRule.ShowDialog() == DialogResult.OK)
            //{
            //    TreeNode parentNode = this.treeCheckScheme.SelectedNode;

            //    DataCheckLayer tDataCheckLayer = parentNode.Tag as DataCheckLayer;

            //    IList<IDataCheckRule> tListRule = tFrmCheckRule.ListCheckRule;
            //    foreach (IDataCheckRule tDataCheckRule in tListRule)
            //    {
            //        tDataCheckRule.DataCheckRuleLayer = tDataCheckLayer;

            //        TreeNode ttreeNode = new TreeNode();
            //        ttreeNode.Text = tDataCheckRule.RuleName;
            //        ttreeNode.Tag = tDataCheckRule;
            //        ttreeNode.SelectedImageIndex = 2;
            //        ttreeNode.ImageIndex = 2;

            //        parentNode.Nodes.Add(ttreeNode);

            //        this.treeCheckScheme.SelectedNode = ttreeNode;
            //    }

            //    parentNode.Expand();               
            //}
        }

        private void menu_SetDataSource_Click(object sender, EventArgs e)
        {
            AG.COM.SDM.Catalog.IDataBrowser frm = new FormDataBrowser();
            frm.MultiSelect = false;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                TreeNode parentNode = this.treeCheckScheme.SelectedNode;
                DataCheckLayer tDataCheckLayer = parentNode.Tag as DataCheckLayer;

                IList<DataItem> items = frm.SelectedItems;
                //IDataset ds = items[0].GetGeoObject() as IDataset;
                //IFeatureClass tFeatureClass = items[0].GetGeoObject() as IFeatureClass;
                ITable tFeatureClass = items[0].GetGeoObject() as ITable;

                if ((tFeatureClass != null) && (tDataCheckLayer != null))
                {
                    //设置源数据图层
                    tDataCheckLayer.SourceFeatClass = tFeatureClass;
                    //属性刷新
                    this.propNodeAttribute.Refresh();
                }
                foreach (TreeNode tChildNode in this.treeCheckScheme.SelectedNode.Nodes)
                {
                    object o = tChildNode.Tag;
                    DataCheckLayer tDataCheckLayer1 = tChildNode.Tag as DataCheckLayer;
                    //IDataCheckRule tDataCheckRule = tChildNode.Tag as IDataCheckRule;
                    if (null != tDataCheckLayer1)
                    {
                        tDataCheckLayer1 = tDataCheckLayer;
                    }
                }

                //if ((ds != null) && (tDataCheckLayer != null))
                //{
                //    //设置源数据图层
                //    tDataCheckLayer.SourceFeatClass = ds as IFeatureClass;
                //    //属性刷新
                //    this.propNodeAttribute.Refresh();
                //}
            }
        }

        private void menu_SetSchemeSource_Click(object sender, EventArgs e)
        {
            TreeNode rootNode = this.treeCheckScheme.Nodes[0];
            if (rootNode.Nodes.Count == 0) return;

            //初始化设置数据源实例对象
            FormSetDataSource tFrmSource = new FormSetDataSource();
            tFrmSource.DicDataCheckLayer = this.GetDictDataCheckLayer();
            tFrmSource.DictFeatClass = this.GetDictDataCheckLayerClass();
            tFrmSource.ShowInTaskbar = false;
            if (tFrmSource.DicDataCheckLayer.Count <= 0) return;
            tFrmSource.ShowDialog();
        }
        private void menu_AddLayerToGroup_Click(object sender, EventArgs e)
        { 
            //初始化数据核查图层实例
            DataCheckLayer tRuleLayer = new DataCheckLayer();
            tRuleLayer.ItemPropertyValueChanged += new ItemPropertyValueChangedEventHandler(ItemPropertyValueChanged);

            TreeNode ttreeNode = new TreeNode();
            ttreeNode.Text = tRuleLayer.ItemName;
            ttreeNode.Tag = tRuleLayer;
            ttreeNode.ImageIndex = 1;
            ttreeNode.SelectedImageIndex = 1;
            this.treeCheckScheme.SelectedNode.Nodes.Add(ttreeNode);
            ttreeNode.Parent.Expand();
            this.treeCheckScheme.SelectedNode = ttreeNode;

        }

        private void menu_SetGroupSource_Click(object sender, EventArgs e)
        {
            TreeNode rootNode = this.treeCheckScheme.SelectedNode;
            if (rootNode.Nodes.Count == 0) return;

            //初始化设置数据源实例对象
            FormSetDataSource tFrmSource = new FormSetDataSource();
            tFrmSource.DicDataCheckLayer = this.GetDictDataCheckLayer();
            tFrmSource.ShowInTaskbar = false;
            tFrmSource.ShowDialog();
        }
        private void menu_DeletGroupLayer_Click(object sender, EventArgs e)
        {
            TreeNode tSelNode = this.treeCheckScheme.SelectedNode;
            tSelNode.Remove();
        }

        private void menu_DeleteRule_Click(object sender, EventArgs e)
        {
            TreeNode tSelNode = this.treeCheckScheme.SelectedNode;
            tSelNode.Remove();
        }

        private void treeCheckScheme_AfterSelect(object sender, TreeViewEventArgs e)
        {
            EnumDataNodeItems dataNodeItem = EnumDataNodeItems.None;
            TreeNode treeNode = e.Node;
            if (treeNode != null)
            {
                ItemProperty tItemProperty = treeNode.Tag as ItemProperty;
                dataNodeItem = tItemProperty.DataNodeItem;

                this.propNodeAttribute.SelectedObject = new ItemTypeDescriptor(tItemProperty);
                //this.propNodeAttribute.SelectedObject = tItemProperty;
            }
        }

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

        private void ItemPropertyValueChanged(object sender, ItemPropertyEventArgs e)
        {
            ItemProperty itemProperty = e.ItemProperty;
            TreeNode selTreeNode = this.treeCheckScheme.SelectedNode;
            selTreeNode.Text = itemProperty.ItemName; // string.Format("{0}:{1}({2})", GetDataNodeDescripble(itemProperty.DataNodeItem), itemProperty.ItemName, itemProperty.ItemAliasName);
            #region  设置结点的图片索引值
            //GeometryFieldItemProperty geoFieldItemProperty = itemProperty as GeometryFieldItemProperty;
            //if (geoFieldItemProperty != null)
            //{
            //    if (geoFieldItemProperty.GeometryType == EnumGeometryItems.点 || geoFieldItemProperty.GeometryType == EnumGeometryItems.多点)
            //    {
            //        selTreeNode.Parent.ImageIndex = 3;
            //    }
            //    else if (geoFieldItemProperty.GeometryType == EnumGeometryItems.线)
            //    {
            //        selTreeNode.Parent.ImageIndex = 4;
            //    }
            //    else
            //    {
            //        selTreeNode.Parent.ImageIndex = 5;
            //    }
            //}
            #endregion

            //刷新属性视图
            //this.propNodeAttribute.Refresh();
        }
        
        #region 公开的对外处理方法
        /// <summary>
        /// 加载数据
        /// </summary>
        public void AddData()
        {
            //实例化数据加载对话框
            AG.COM.SDM.Catalog.IDataBrowser tDataBrowser = new FormDataBrowser();
            tDataBrowser.AddFilter(new TableFilter());
            if (tDataBrowser.ShowDialog() == DialogResult.OK)
            {
                //获取选择项
                IList<DataItem> tListDataItem = tDataBrowser.SelectedItems;

                //获取根节点
                TreeNode rootNode = this.treeCheckScheme.Nodes[0];

                for (int i = 0; i < tListDataItem.Count; i++)
                {
                    IDataset tDataset = tListDataItem[i].GetGeoObject() as IDataset;
                    if (tDataset != null)
                    {
                        //实例化数据核查图层
                        DataCheckLayer tDataCheckLayer = new DataCheckLayer();

                        tDataCheckLayer.SourceFeatClass = tDataset as ITable;
                        tDataCheckLayer.LayerName = tDataset.BrowseName;
                        tDataCheckLayer.ItemName = tDataset.BrowseName;
                        tDataCheckLayer.StandardName = tDataset.BrowseName;

                        tDataCheckLayer.ItemPropertyValueChanged += new ItemPropertyValueChangedEventHandler(ItemPropertyValueChanged);

                        //实例化图层节点
                        TreeNode layerNode = new TreeNode();
                        layerNode.Text = tDataCheckLayer.ItemName;
                        layerNode.Tag = tDataCheckLayer;
                        layerNode.ImageIndex = 1;
                        layerNode.SelectedImageIndex = 1;
                        rootNode.Nodes.Add(layerNode);
                    }
                }

                //展开树节点
                rootNode.Expand();
            }
        }

        /// <summary>
        /// 设置源数据图层
        /// </summary>
        public void SetDataSource()
        {
            this.menu_SetSchemeSource_Click(null, null);
        }

        /// <summary>
        /// 把图层树和导入的数据图层结合起来
        /// </summary>
        /// <param name="tSourseLayers"></param>
        public void SetDataSourceByMdi(Dictionary<string, ITable> sourseLayers)
        {
            IDictionary<string, DataCheckLayer> dicLayers = GetAllDictDataCheckLayer();

            IDictionaryEnumerator de = dicLayers.GetEnumerator() as IDictionaryEnumerator;
            while (de.MoveNext())
            {
                if (de.Value is DataCheckLayer)
                {
                    DataCheckLayer dataCheckLayer = de.Value as DataCheckLayer;
                    if (sourseLayers.ContainsKey(dataCheckLayer.StandardName))
                    {
                        dataCheckLayer.SourceFeatClass = sourseLayers[dataCheckLayer.StandardName];
                    }
                }
                //else if (de.Value is DataCheckLayerSet)
                //{
                //    DataCheckLayerSet tDataCheckLayer = de.Value as DataCheckLayerSet;
                //    if (tSourseLayers.ContainsKey(tDataCheckLayer.PointStandardName))
                //    {
                //        tDataCheckLayer.PointSourceFeatClass = tSourseLayers[tDataCheckLayer.PointStandardName];
                //    }
                //    if (tSourseLayers.ContainsKey(tDataCheckLayer.LineStandardName))
                //    {
                //        tDataCheckLayer.LineSourceFeatClass = tSourseLayers[tDataCheckLayer.LineStandardName];
                //    }
                //}

            }
            //this.treeCheckScheme.Nodes[0].Checked = false; ;
            SetChildNodeCheckedBySourseClass(this.treeCheckScheme.Nodes[0]);

        }

        /// <summary>
        /// 把没有导入数据的节点的checked设为false
        /// </summary>
        /// <param name="node"></param>
        private void SetChildNodeCheckedBySourseClass(TreeNode node)
        {
            foreach (TreeNode nodeChild in node.Nodes)
            {

                if (nodeChild.Tag is DataCheckLayerSet)
                {
                    if((nodeChild.Tag as DataCheckLayerSet).LineSourceFeatClass==null || (nodeChild.Tag as DataCheckLayerSet).PointSourceFeatClass==null)
                    {
                        nodeChild.Checked = false;
                    }
                }
                
                //递归设置子节点选中状态
                SetChildNodeCheckedBySourseClass(nodeChild);
            }
        }

        /// <summary>
        /// 新建数据核查方案
        /// </summary>  
        public void NewDataCheckScheme()
        {
            this.treeCheckScheme.Nodes.Clear();

            //初始化数据核查方案属性实例
            DataCheckScheme tDataCheckScheme = new DataCheckScheme();
            tDataCheckScheme.ItemPropertyValueChanged += new ItemPropertyValueChangedEventHandler(ItemPropertyValueChanged);

            TreeNode rootNode = new TreeNode();
            rootNode.Text = tDataCheckScheme.ItemName;
            rootNode.Tag = tDataCheckScheme;

            this.treeCheckScheme.Nodes.Add(rootNode);  
        }

        /// <summary>
        /// 保存数据核查方案到指定的文件路径
        /// </summary>
        /// <param name="pSchemeFile">数据核查方案文件</param>
        public void SaveDataCheckScheme(string pSchemeFile)
        {
            System.IO.FileStream fs = new System.IO.FileStream(pSchemeFile, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();

            TreeNode rootNode = this.treeCheckScheme.Nodes[0];

            //注销事件
            DestroyItemPropertyEventHandler(rootNode);

            try
            {
                formatter.Serialize(fs, rootNode);

                MessageBox.Show("转换方案文件保存成功","提示",MessageBoxButtons.OK ,MessageBoxIcon.Information);
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
        /// 打开指定路径的数据核查方案
        /// </summary>
        /// <param name="schemeFile"></param>
        public void OpenDataCheckScheme(string schemeFile)
        {
            if (File.Exists(schemeFile) == false) return;
          
            // Open the file containing the data that you want to deserialize.
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
                SysConfig.log.Info("打开数据转换方案序列化出错,错误:"+ex1);
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
        /// 开始执行检查
        /// </summary>
        /// <returns></returns>
        //public IList<ErrRecord> StartRunCheck()
        //{            
        //    IDictionary<TreeNode, DataCheckLayer> dataCheckLayers = GetDataCheckLayers();
        //    TreeNode rootNode = this.treeCheckScheme.Nodes[0];

        //    DataCheckScheme dataCheckScheme = rootNode.Tag as DataCheckScheme;
        //    IList<ErrRecord> errRecords = new List<ErrRecord>();

        //    int i = 0;
        //    if (dataCheckScheme.WorkSpace == null)
        //    {
        //        MessageBox.Show("请设置转换方案保存路径！.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return null;
        //    }

        //    //获取进度栏实例
        //    //ITrackProgress trackProgress = SysConfig.GetInstance().TrackProgress;
        //    ITrackProgress trackProgress = new TrackProgressDialog();
        //    trackProgress.DisplayTotal = false;
        //    trackProgress.SubMax = dataCheckLayers.Count;
        //    trackProgress.AutoFinishClose = true;
        //    //tTrackProgress.Owner = this;
        //    trackProgress.Show();

        //    foreach (KeyValuePair<TreeNode, DataCheckLayer> keyPair in dataCheckLayers)
        //    {
        //        i++;
        //        DataCheckLayer dataCheckLayer = keyPair.Value;

        //        if (dataCheckLayer == null) continue;
        //        if (dataCheckLayer.SourceFeatClass == null)
        //        {
        //            if (MessageBox.Show(dataCheckLayer.ItemName + "图层没有设置数据源,是否跳过生成下一个图层.", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
        //                continue;
        //            else
        //                break;
        //        }

        //        trackProgress.SubMessage = "正在生成图层:" + dataCheckLayer.ItemName;
        //        trackProgress.SubValue = i;
        //        TreeNode layerNode = keyPair.Key;

        //        if (trackProgress.IsContinue == false) break;

        //        Application.DoEvents();
        //        try
        //        {
        //            GeoDBHelper.StartConvert(dataCheckLayer, dataCheckScheme);
        //            ErrRecord errRecord = new ErrRecord();
        //            errRecord.TargetPath = dataCheckScheme.WorkSpace.PathName;
        //            errRecord.SourceLayerName = (dataCheckLayer.SourceFeatClass as IDataset).Workspace.PathName + "\\" + (dataCheckLayer.SourceFeatClass as IDataset).BrowseName;
        //            errRecord.LayerName = dataCheckLayer.ItemName;
        //            errRecord.IsSuceed = true;
        //            errRecords.Add(errRecord);
        //        }
        //        catch(Exception ex)
        //        {
        //            ErrRecord errRecord = new ErrRecord();
        //            errRecord.TargetPath = dataCheckScheme.WorkSpace.PathName;
        //            errRecord.SourceLayerName = (dataCheckLayer.SourceFeatClass as IDataset).Workspace.PathName + "\\" + (dataCheckLayer.SourceFeatClass as IDataset).BrowseName;
        //            errRecord.LayerName = dataCheckLayer.ItemName;
        //            errRecord.IsSuceed = false;
        //            errRecord.Detail = ex.Message;
        //            errRecords.Add(errRecord);

        //            continue;
        //        }

        //        if (trackProgress.IsContinue == false) break;
        //    }

        //    //标识完成状态
        //    trackProgress.SetFinish();
        //    trackProgress = null;
        //    //System.Runtime.InteropServices.Marshal.ReleaseComObject(tDataCheckScheme.WorkSpace);
        //    return errRecords;
        //}

        /// <summary>
        /// 设置保存GDB的路径
        /// </summary>
        /// <returns></returns>
        public IList<ErrRecord> StartRunCheckEx()
        {
            TreeNode rootNode = this.treeCheckScheme.Nodes[0];
            DataCheckScheme dataCheckScheme = rootNode.Tag as DataCheckScheme;
            DatabaseScheme databaseScheme = CommanScheme.SearchDatabaseSchemeByName(dataCheckScheme.DatabseScheme);
            dataCheckScheme.DataBaseScheme = databaseScheme;
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "选择保存的路径";
            string WorkspacePath = string.Empty;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                WorkspacePath = folderBrowserDialog.SelectedPath;
            }
            if (string.IsNullOrWhiteSpace(WorkspacePath))
            {
                if (MessageBox.Show("文件保存路径为空，是否默认保存到桌面？。", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    RegistryKey folders;
                    folders = OpenRegistryPath(Registry.CurrentUser, @"\software\microsoft\windows\currentversion\explorer\shell folders");
                    IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
                    IWorkspaceName workspaceName;
                    if (m_MDBpath == "")
                    {
                        workspaceName = workspaceFactory.Create(folders.GetValue("Desktop").ToString(), "GDB" + DateTime.Now.ToString("yyyyMMddHHmmss"), null, 0);
                    }
                    else
                    {
                        FileInfo tFile = new FileInfo(m_MDBpath);
                        workspaceName = workspaceFactory.Create(folders.GetValue("Desktop").ToString(), tFile.Name.Replace(".mdb", "") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss"), null, 0);
                    }
                    IName name = (IName)workspaceName;
                    IWorkspace workspace = (IWorkspace)name.Open();
                    dataCheckScheme.WorkSpace = workspace;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                dataCheckScheme.WorkspacePath = WorkspacePath;
                IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
                IWorkspaceName workspaceName;
                if (m_MDBpath == "")
                {
                    workspaceName = workspaceFactory.Create(dataCheckScheme.WorkspacePath, "GDB" + DateTime.Now.ToString("yyyyMMddHHmmss"), null, 0);
                }
                else
                {
                    FileInfo tFile = new FileInfo(m_MDBpath);
                    workspaceName = workspaceFactory.Create(dataCheckScheme.WorkspacePath, tFile.Name.Replace(".mdb", "") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss"), null, 0);
                }
                IName name = (IName)workspaceName;
                IWorkspace workspace = (IWorkspace)name.Open();
                dataCheckScheme.WorkSpace = workspace;
            }
            m_GDBpath = dataCheckScheme.WorkSpace.PathName;
            IList<ErrRecord> errRecords = new List<ErrRecord>();

            int checkLayerCount = GetCheckLayerCount();
            ITrackProgress trackProgress = new TrackProgressDialog();
            trackProgress.DisplayTotal = false;
            trackProgress.SubMax = checkLayerCount;
            trackProgress.SubValue = 0;
            trackProgress.AutoFinishClose = true;
            //trackProgress.Owner = this;
            trackProgress.Show();
            Application.DoEvents();

            foreach (TreeNode layerNode in rootNode.Nodes)
            {
                if (layerNode.Checked == false) continue;
                //遍历管线转换方案下的子节点类型
                //若为质检图层类(管线/管点)
                if (layerNode.Tag is DataCheckLayer)
                {
                    
                    DataCheckLayer tDataCheckLayer = layerNode.Tag as DataCheckLayer;

                    trackProgress.SubMessage = "正在生成图层:" + tDataCheckLayer.ItemName;
                    trackProgress.SubValue++;
                    Application.DoEvents();

                    if (tDataCheckLayer == null) continue;
                    if (tDataCheckLayer.SourceFeatClass == null)
                    {
                            continue;

                    }
                    
                    errRecords.Add(tDataCheckLayer.StartCheck(dataCheckScheme));
                }
                //若为质检图层组(管线大类)
                else if (layerNode.Tag is DataCheckGroupLayer)
                {
                    //DataCheckGroupLayer tDataCheckLayer=layerNode.Tag as DataCheckGroupLayer;
                    foreach (TreeNode tlayerNode in layerNode.Nodes)
                    {
                        if (tlayerNode.Tag is DataCheckLayer)
                        {
                            if (tlayerNode.Checked == false) continue;
                            DataCheckLayer tDataCheckLayer = tlayerNode.Tag as DataCheckLayer;

                            trackProgress.SubMessage = "正在生成图层:" + tDataCheckLayer.ItemName;
                            trackProgress.SubValue++;
                            Application.DoEvents();

                            if (tDataCheckLayer == null) continue;
                            if (tDataCheckLayer.SourceFeatClass == null)
                            {

                                continue;

                            }
                         
                            errRecords.Add(tDataCheckLayer.StartCheck(dataCheckScheme));
                        }
                        else if (tlayerNode.Tag is DataCheckLayerSet)
                        {
                            if (tlayerNode.Checked == false) continue;
                            DataCheckLayerSet tDataCheckLayer = tlayerNode.Tag as DataCheckLayerSet;

                            trackProgress.SubMessage = "正在生成图层:" + tDataCheckLayer.ItemName;
                            trackProgress.SubValue++;
                            Application.DoEvents();

                            if (tDataCheckLayer == null) continue;
                            if (tDataCheckLayer.m_PointDataCheckLayer.SourceFeatClass == null || tDataCheckLayer.m_LineDataCheckLayer.SourceFeatClass == null)
                            {
                                continue;

                            }
                            List<ErrRecord> records = tDataCheckLayer.StartCheck(dataCheckScheme);
                            errRecords.Add(records[0]);
                            errRecords.Add(records[1]);
                        }
                    }
                }
                //若为质检图层集(管线小类，包含管线管点图层)
                else if (layerNode.Tag is DataCheckLayerSet)
                {
                    DataCheckLayerSet tDataCheckLayer = layerNode.Tag as DataCheckLayerSet;

                    trackProgress.SubMessage = "正在生成图层:" + tDataCheckLayer.ItemName;
                    trackProgress.SubValue++;
                    Application.DoEvents();

                    if (tDataCheckLayer == null) continue;
                    if (tDataCheckLayer.m_PointDataCheckLayer.SourceFeatClass == null || tDataCheckLayer.m_LineDataCheckLayer.SourceFeatClass ==null)
                    {
                            continue;
                    }
                    #region 
                    //if (dataCheckScheme.SpatialReference == null || dataCheckScheme.SpatialReference.Name == "Unknown")
                    //{
                    //    IFeatureClass pFeatureClass1 = (AG.COM.SDM.Utility.CommonVariables.DatabaseConfig.Workspace as IFeatureWorkspace).OpenFeatureClass(tDataCheckLayer.LineStandardName);
                    //    IGeoDataset pGeodataSet = pFeatureClass1 as IGeoDataset;
                    //    ISpatialReference pSpatialReference = pGeodataSet.SpatialReference;
                    //    dataCheckScheme.SpatialReference = pSpatialReference;
                    //}
                    //errRecords.Add(tDataCheckLayer.StartCheck(dataCheckScheme));
                    #endregion
                    List<ErrRecord> records = tDataCheckLayer.StartCheck(dataCheckScheme);
                    errRecords.Add(records[0]);
                    errRecords.Add(records[1]);

                }

            }
            trackProgress.SetFinish();
            trackProgress = null;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(dataCheckScheme.WorkSpace);
            dataCheckScheme.WorkSpace = null;
            return errRecords;
        }

        //直接保存发GDB到系统tmp文件夹
        public IList<ErrRecord> StartRunCheckEx(bool withoutPrompt)
        {
            TreeNode rootNode = this.treeCheckScheme.Nodes[0];
            DataCheckScheme dataCheckScheme = rootNode.Tag as DataCheckScheme;
            if (dataCheckScheme.WorkSpace == null)
            {
                if (withoutPrompt)
                {                  
                    IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
                    IWorkspaceName workspaceName;
                    if (m_MDBpath == "")
                    {
                        workspaceName = workspaceFactory.Create(System.IO.Path.GetTempPath(), "GDB" + DateTime.Now.ToString("yyyyMMddHHmmss"), null, 0);
                    }
                    else
                    {
                        FileInfo tFile = new FileInfo(m_MDBpath);
                        workspaceName = workspaceFactory.Create(System.IO.Path.GetTempPath(), tFile.Name.Replace(".mdb", "") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss"), null, 0);
                    }
                    IName name = (IName)workspaceName;
                    IWorkspace workspace = (IWorkspace)name.Open();
                    dataCheckScheme.WorkSpace = workspace;
                }
                else
                {
                    return null;
                }
            }
            m_GDBpath = dataCheckScheme.WorkSpace.PathName;
            IList<ErrRecord> errRecords = new List<ErrRecord>();

            int checkLayerCount = GetCheckLayerCount();
            ITrackProgress trackProgress = new TrackProgressDialog();
            trackProgress.DisplayTotal = false;
            trackProgress.SubMax = checkLayerCount;
            trackProgress.SubValue = 0;
            trackProgress.AutoFinishClose = true;
            //tTrackProgress.Owner = this;
            trackProgress.Show();
            Application.DoEvents();

            foreach (TreeNode layerNode in rootNode.Nodes)
            {
                if (layerNode.Checked == false) continue;
                //引用类型转换
                if (layerNode.Tag is DataCheckLayer)
                {

                    DataCheckLayer tDataCheckLayer = layerNode.Tag as DataCheckLayer;

                    trackProgress.SubMessage = "正在生成图层:" + tDataCheckLayer.ItemName;
                    trackProgress.SubValue++;
                    Application.DoEvents();

                    if (tDataCheckLayer == null) continue;
                    if (tDataCheckLayer.SourceFeatClass == null)
                    {
                        continue;

                    }
                    errRecords.Add(tDataCheckLayer.StartCheck(dataCheckScheme));
                }
                else if (layerNode.Tag is DataCheckGroupLayer)
                {
                    //DataCheckGroupLayer tDataCheckLayer=layerNode.Tag as DataCheckGroupLayer;
                    foreach (TreeNode tlayerNode in layerNode.Nodes)
                    {
                        if (tlayerNode.Tag is DataCheckLayer)
                        {
                            if (tlayerNode.Checked == false) continue;
                            DataCheckLayer tDataCheckLayer = tlayerNode.Tag as DataCheckLayer;

                            trackProgress.SubMessage = "正在生成图层:" + tDataCheckLayer.ItemName;
                            trackProgress.SubValue++;
                            Application.DoEvents();

                            if (tDataCheckLayer == null) continue;
                            if (tDataCheckLayer.SourceFeatClass == null)
                            {

                                continue;

                            }
                            errRecords.Add(tDataCheckLayer.StartCheck(dataCheckScheme));
                        }
                        else if (tlayerNode.Tag is DataCheckLayerSet)
                        {
                            if (tlayerNode.Checked == false) continue;
                            DataCheckLayerSet tDataCheckLayer = tlayerNode.Tag as DataCheckLayerSet;

                            trackProgress.SubMessage = "正在生成图层:" + tDataCheckLayer.ItemName;
                            trackProgress.SubValue++;
                            Application.DoEvents();

                            if (tDataCheckLayer == null) continue;
                            if (tDataCheckLayer.m_PointDataCheckLayer.SourceFeatClass == null || tDataCheckLayer.m_LineDataCheckLayer.SourceFeatClass == null)
                            {
                                continue;

                            }
                            //errRecords.Add(tDataCheckLayer.StartCheck(dataCheckScheme));
                            List<ErrRecord> records = tDataCheckLayer.StartCheck(dataCheckScheme);
                            errRecords.Add(records[0]);
                            errRecords.Add(records[1]);
                        }
                    }
                }
                else if (layerNode.Tag is DataCheckLayerSet)
                {
                    DataCheckLayerSet tDataCheckLayer = layerNode.Tag as DataCheckLayerSet;

                    trackProgress.SubMessage = "正在生成图层:" + tDataCheckLayer.ItemName;
                    trackProgress.SubValue++;
                    Application.DoEvents();

                    if (tDataCheckLayer == null) continue;
                    if (tDataCheckLayer.m_PointDataCheckLayer.SourceFeatClass == null || tDataCheckLayer.m_LineDataCheckLayer.SourceFeatClass == null)
                    {
                        continue;
                    }
                    //errRecords.Add(tDataCheckLayer.StartCheck(dataCheckScheme));
                    List<ErrRecord> records = tDataCheckLayer.StartCheck(dataCheckScheme);
                    errRecords.Add(records[0]);
                    errRecords.Add(records[1]);
                }

            }
            trackProgress.SetFinish();
            trackProgress = null;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(dataCheckScheme.WorkSpace);
            dataCheckScheme.WorkSpace = null;
            return errRecords;
        }

        public void SetDefualtValue()
        {
            //这里写死，请注意
            TreeNode tRootNode = this.treeCheckScheme.Nodes[0];
            foreach (TreeNode tChildNode in tRootNode.Nodes)
            {
                if (tChildNode.Tag is DataCheckGroupLayer)
                {
                    SetDefualtValueToChildNodes(tChildNode);
                }
                else if (tChildNode.Tag is DataCheckLayerSet)
                {
                    DataCheckLayerSet tDataCheckLayerSet = tChildNode.Tag as DataCheckLayerSet;
                    string LineStandardName = tDataCheckLayerSet.LineStandardName;
                    string PointStandardName = tDataCheckLayerSet.PointStandardName;
                    tDataCheckLayerSet.Init();
                    List<PointInfo> tPointList = new List<PointInfo>();
                    PointInfo tPointInfo = new PointInfo();
                    tPointInfo.x = "y";
                    tPointInfo.y = "x";
                    tPointInfo.z = "h";
                    tPointList.Add(tPointInfo);
                    tDataCheckLayerSet.Points = tPointList;
                    tDataCheckLayerSet.ExpNoFieldName = "p_no";
                    tDataCheckLayerSet.SPointFieldName = "s_point";
                    tDataCheckLayerSet.EPointFieldName = "e_point";
                    tDataCheckLayerSet.HighFieldName = "h";
                    tDataCheckLayerSet.EHighFieldName = "e_h";
                    tDataCheckLayerSet.SHighFieldName = "s_h";
                    tDataCheckLayerSet.LineStandardName = LineStandardName;
                    tDataCheckLayerSet.PointStandardName = PointStandardName;
                }
            }            
        }

        private void SetDefualtValueToChildNodes(TreeNode tRootNode)
        {
            foreach (TreeNode tChildNode in tRootNode.Nodes)
            {
                if (tChildNode.Tag is DataCheckGroupLayer)
                {
                    SetDefualtValueToChildNodes(tChildNode);
                }
                else if (tChildNode.Tag is DataCheckLayerSet)
                {
                    DataCheckLayerSet tDataCheckLayerSet = tChildNode.Tag as DataCheckLayerSet;
                    string LineStandardName = tDataCheckLayerSet.LineStandardName;
                    string PointStandardName = tDataCheckLayerSet.PointStandardName;
                    tDataCheckLayerSet.Init();
                    List<PointInfo> tPointList = new List<PointInfo>();
                    PointInfo tPointInfo = new PointInfo();
                    tPointInfo.x = "y";
                    tPointInfo.y = "x";
                    tPointInfo.z = "h";
                    tPointList.Add(tPointInfo);
                    tDataCheckLayerSet.Points = tPointList;
                    tDataCheckLayerSet.ExpNoFieldName = "p_no";
                    tDataCheckLayerSet.SPointFieldName = "s_point";
                    tDataCheckLayerSet.EPointFieldName = "e_point";
                    tDataCheckLayerSet.HighFieldName = "h";
                    tDataCheckLayerSet.EHighFieldName = "e_h";
                    tDataCheckLayerSet.SHighFieldName = "s_h";
                    tDataCheckLayerSet.LineStandardName = LineStandardName;
                    tDataCheckLayerSet.PointStandardName = PointStandardName;
                }
            }
        }
        #endregion

        #region 内部私有处理方法
        /// <summary>
        /// 获取或设置核层图层集合
        /// </summary>
        /// <returns>返回核查图层集合</returns>
        private IDictionary<string, DataCheckLayer> GetDictDataCheckLayer()
        {
            //TreeNode parentNode = this.treeCheckScheme.Nodes[0];
            TreeNode parentNode;
            if (this.treeCheckScheme.SelectedNode == null)
            {
                parentNode = this.treeCheckScheme.Nodes[0];
            }
            else
            {
                parentNode = this.treeCheckScheme.SelectedNode;
            }
            
            IDictionary<string, DataCheckLayer> dictDataCheckLayer = new Dictionary<string, DataCheckLayer>();
            if (parentNode.Nodes.Count <= 0)
            {
                MessageBox.Show("当前图层类没有子节点");
                return dictDataCheckLayer;
            }
            foreach (TreeNode layerNode in parentNode.Nodes)
            {
                //引用类型转换
                if (layerNode.Tag is DataCheckLayerSet)
                {
                    DataCheckLayerSet tDataCheckLayer = layerNode.Tag as DataCheckLayerSet;
                    string LineName = tDataCheckLayer.LineItemName;
                    string PointName = tDataCheckLayer.PointItemName;
                    if (!dictDataCheckLayer.ContainsKey(LineName))
                        dictDataCheckLayer.Add(LineName, tDataCheckLayer.m_LineDataCheckLayer);
                    if (!dictDataCheckLayer.ContainsKey(PointName))
                        dictDataCheckLayer.Add(PointName, tDataCheckLayer.m_PointDataCheckLayer);
                }
                else if (layerNode.Tag is DataCheckLayer)
                {
                    DataCheckLayer tDataCheckLayer = layerNode.Tag as DataCheckLayer;
                    if (!dictDataCheckLayer.ContainsKey(layerNode.Text.ToString()))
                        dictDataCheckLayer.Add(layerNode.Text.ToString(), tDataCheckLayer);
                }
                else if (layerNode.Tag is DataCheckGroupLayer)
                {
                    //DataCheckGroupLayer tDataCheckLayer=layerNode.Tag as DataCheckGroupLayer;
                    foreach (TreeNode tlayerNode in layerNode.Nodes)
                    {
                        if (tlayerNode.Tag is DataCheckLayerSet)
                        {
                            DataCheckLayerSet tDataCheckLayer = tlayerNode.Tag as DataCheckLayerSet;
                            string LineName = tDataCheckLayer.LineItemName;
                            string PointName = tDataCheckLayer.PointItemName;
                            if (!dictDataCheckLayer.ContainsKey(LineName))
                                dictDataCheckLayer.Add(LineName, tDataCheckLayer.m_LineDataCheckLayer);
                            if (!dictDataCheckLayer.ContainsKey(PointName))
                                dictDataCheckLayer.Add(PointName, tDataCheckLayer.m_PointDataCheckLayer);
                        }
                        else if (tlayerNode.Tag is DataCheckLayer)
                        {
                            DataCheckLayer tDataCheckLayer = tlayerNode.Tag as DataCheckLayer;
                            if (!dictDataCheckLayer.ContainsKey(tlayerNode.Text.ToString()))

                                dictDataCheckLayer.Add(tlayerNode.Text.ToString(), tDataCheckLayer);
                        }
                    }
                }

            }

            return dictDataCheckLayer;
        }

        /// <summary>
        /// 获取或设置核层图层集合
        /// </summary>
        /// <returns>返回核查图层集合</returns>
        private IDictionary<string, DataCheckLayer> GetAllDictDataCheckLayer()
        {
            TreeNode parentNode = this.treeCheckScheme.Nodes[0];
            //TreeNode parentNode = this.treeCheckScheme.SelectedNode;

            IDictionary<string, DataCheckLayer> dictDataCheckLayer = new Dictionary<string, DataCheckLayer>();

            foreach (TreeNode layerNode in parentNode.Nodes)
            {
                //引用类型转换
                if (layerNode.Tag is DataCheckLayerSet)
                {
                    DataCheckLayerSet tDataCheckLayer = layerNode.Tag as DataCheckLayerSet;
                    string LineName = tDataCheckLayer.LineItemName;
                    string PointName = tDataCheckLayer.PointItemName;
                    if (!dictDataCheckLayer.ContainsKey(LineName))
                        dictDataCheckLayer.Add(LineName, tDataCheckLayer.m_LineDataCheckLayer);
                    if (!dictDataCheckLayer.ContainsKey(PointName))
                        dictDataCheckLayer.Add(PointName, tDataCheckLayer.m_PointDataCheckLayer);
                }
                else if (layerNode.Tag is DataCheckLayer)
                {
                    DataCheckLayer tDataCheckLayer = layerNode.Tag as DataCheckLayer;
                    if (!dictDataCheckLayer.ContainsKey(layerNode.Text.ToString()))
                        dictDataCheckLayer.Add(layerNode.Text.ToString(), tDataCheckLayer);
                }
                else if (layerNode.Tag is DataCheckGroupLayer)
                {
                    //DataCheckGroupLayer tDataCheckLayer=layerNode.Tag as DataCheckGroupLayer;
                    foreach (TreeNode tlayerNode in layerNode.Nodes)
                    {
                        if (tlayerNode.Tag is DataCheckLayerSet)
                        {
                            DataCheckLayerSet tDataCheckLayer = tlayerNode.Tag as DataCheckLayerSet;
                            string LineName = tDataCheckLayer.LineItemName;
                            string PointName = tDataCheckLayer.PointItemName;
                            if (!dictDataCheckLayer.ContainsKey(LineName))
                                dictDataCheckLayer.Add(LineName, tDataCheckLayer.m_LineDataCheckLayer);
                            if (!dictDataCheckLayer.ContainsKey(PointName))
                                dictDataCheckLayer.Add(PointName, tDataCheckLayer.m_PointDataCheckLayer);
                        }
                        else if (tlayerNode.Tag is DataCheckLayer)
                        {
                            DataCheckLayer tDataCheckLayer = tlayerNode.Tag as DataCheckLayer;
                            if (!dictDataCheckLayer.ContainsKey(tlayerNode.Text.ToString()))

                                dictDataCheckLayer.Add(tlayerNode.Text.ToString(), tDataCheckLayer);
                        }
                    }
                }

            }

            return dictDataCheckLayer;
        }
        /// <summary>
        /// 获取或设置核层图层集合
        /// </summary>
        /// <returns>返回核查图层集合</returns>
        private IDictionary<string, ITable> GetDictDataCheckLayerClass()
        {
            //TreeNode parentNode = this.treeCheckScheme.Nodes[0];
            TreeNode parentNode;
            if (this.treeCheckScheme.SelectedNode == null)
            {
                parentNode = this.treeCheckScheme.Nodes[0];
            }
            else
            {
                parentNode = this.treeCheckScheme.SelectedNode;
            }

            IDictionary<string, ITable> dictDataCheckLayerClass = new Dictionary<string, ITable>();

            foreach (TreeNode layerNode in parentNode.Nodes)
            {
                //引用类型转换
                if (layerNode.Tag is DataCheckLayerSet)
                {
                    DataCheckLayerSet tDataCheckLayer = layerNode.Tag as DataCheckLayerSet;
                    string LineName = tDataCheckLayer.m_LineDataCheckLayer.StandardName;
                    string PointName = tDataCheckLayer.m_PointDataCheckLayer.StandardName;
                    if (!dictDataCheckLayerClass.ContainsKey(LineName))
                        dictDataCheckLayerClass.Add(LineName, tDataCheckLayer.m_LineDataCheckLayer.SourceFeatClass);
                    if (!dictDataCheckLayerClass.ContainsKey(PointName))
                        dictDataCheckLayerClass.Add(PointName, tDataCheckLayer.m_PointDataCheckLayer.SourceFeatClass);
                }
                else if (layerNode.Tag is DataCheckLayer)
                {
                    DataCheckLayer tDataCheckLayer = layerNode.Tag as DataCheckLayer;
                    if (!dictDataCheckLayerClass.ContainsKey(layerNode.Text.ToString()))
                        dictDataCheckLayerClass.Add(tDataCheckLayer.StandardName, tDataCheckLayer.SourceFeatClass);
                }
                else if (layerNode.Tag is DataCheckGroupLayer)
                {
                    //DataCheckGroupLayer tDataCheckLayer=layerNode.Tag as DataCheckGroupLayer;
                    foreach (TreeNode tlayerNode in layerNode.Nodes)
                    {
                        if (tlayerNode.Tag is DataCheckLayerSet)
                        {
                            DataCheckLayerSet tDataCheckLayer = tlayerNode.Tag as DataCheckLayerSet;
                            string LineName = tDataCheckLayer.m_LineDataCheckLayer.StandardName;
                            string PointName = tDataCheckLayer.m_PointDataCheckLayer.StandardName;
                            if (!dictDataCheckLayerClass.ContainsKey(LineName))
                                dictDataCheckLayerClass.Add(LineName, tDataCheckLayer.m_LineDataCheckLayer.SourceFeatClass);
                            if (!dictDataCheckLayerClass.ContainsKey(PointName))
                                dictDataCheckLayerClass.Add(PointName, tDataCheckLayer.m_PointDataCheckLayer.SourceFeatClass);
                        }
                        else if (tlayerNode.Tag is DataCheckLayer)
                        {
                            DataCheckLayer tDataCheckLayer = tlayerNode.Tag as DataCheckLayer;
                            if (!dictDataCheckLayerClass.ContainsKey(tlayerNode.Text.ToString()))

                                dictDataCheckLayerClass.Add(tDataCheckLayer.StandardName.ToUpper(), tDataCheckLayer.SourceFeatClass);
                        }
                    }
                }

            }

            return dictDataCheckLayerClass;
        }

        private IDictionary<TreeNode, DataCheckLayer> GetDataCheckLayers()
        { 
            TreeNode parentNode = this.treeCheckScheme.Nodes[0];
            IDictionary<TreeNode, DataCheckLayer> dictDataCheckLayer=new  Dictionary<TreeNode, DataCheckLayer>();

            foreach (TreeNode layerNode in parentNode.Nodes)
            {
                if (layerNode.Checked == false) continue;
                //引用类型转换
                if (layerNode.Tag is DataCheckLayer)
                {
                    DataCheckLayer tDataCheckLayer = layerNode.Tag as DataCheckLayer;
                    dictDataCheckLayer.Add(layerNode,tDataCheckLayer);
                }
                else if (layerNode.Tag is DataCheckGroupLayer)
                {
                    //DataCheckGroupLayer tDataCheckLayer=layerNode.Tag as DataCheckGroupLayer;
                    foreach (TreeNode tlayerNode in layerNode.Nodes)
                    {
                        if (tlayerNode.Checked == false) continue;
                        DataCheckLayer tDataCheckLayer = tlayerNode.Tag as DataCheckLayer;
                        dictDataCheckLayer.Add(tlayerNode,tDataCheckLayer);
                    }
                }

            }

            return dictDataCheckLayer;

        }

        private int GetCheckLayerCount()
        {
            int count = 0;
            TreeNode parentNode = this.treeCheckScheme.Nodes[0];

            foreach (TreeNode layerNode in parentNode.Nodes)
            {
                if (layerNode.Checked == false) continue;
                //引用类型转换
                if (layerNode.Tag is DataCheckLayerSet || layerNode.Tag is DataCheckLayer)
                {
                    count++;
                }
                else if (layerNode.Tag is DataCheckGroupLayer)
                {
                    //DataCheckGroupLayer tDataCheckLayer=layerNode.Tag as DataCheckGroupLayer;
                    foreach (TreeNode tlayerNode in layerNode.Nodes)
                    {
                        if (tlayerNode.Checked == false) continue;
                        if (tlayerNode.Tag is DataCheckLayerSet || tlayerNode.Tag is DataCheckLayer)
                        {
                            count++;
                        }
                    }
                }

            }

            return count;
        }

        /// <summary>
        /// 注销ItemProperty事件（递归调用）
        /// </summary>
        /// <param name="rootNode">根节点</param>
        private void DestroyItemPropertyEventHandler(TreeNode rootNode)
        {
            //引用类型转换
            ItemProperty tRootItemProperty = rootNode.Tag as ItemProperty;
            //注销事件
            tRootItemProperty.ItemPropertyValueChanged -= new ItemPropertyValueChangedEventHandler(ItemPropertyValueChanged);

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
            ItemProperty tRootItemProperty = rootNode.Tag as ItemProperty;
            if (tRootItemProperty == null) return;
            //注册事件
            tRootItemProperty.ItemPropertyValueChanged += new ItemPropertyValueChangedEventHandler(ItemPropertyValueChanged);
            if (rootNode.Tag is DataCheckLayer)
            {
                rootNode.SelectedImageIndex = 1;
                rootNode.ImageIndex = 1;
            }
            else if (rootNode.Tag is DataCheckGroupLayer)
            {
                rootNode.SelectedImageIndex = 3;
                rootNode.ImageIndex = 3;
            }
            else if (rootNode.Tag is DataCheckLayerSet)
            {
                rootNode.SelectedImageIndex = 2;
                rootNode.ImageIndex = 2;
            }
            //else if (rootNode.Tag is IDataCheckRule)
            //{
            //    rootNode.SelectedImageIndex = 2;
            //    rootNode.ImageIndex = 2;
            //}
            //rootNode.SelectedImageIndex = rootNode.Level;
            //rootNode.ImageIndex = rootNode.Level;

            foreach (TreeNode childNode in rootNode.Nodes)
            {
                //注册事件
                RegisterItemPropertyEventHandler(childNode);                
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
        #endregion

        private void menu_AddLayerSetToGroup_Click(object sender, EventArgs e)
        {
            //初始化数据核查图层实例
            DataCheckLayerSet tLayerSet = new DataCheckLayerSet();
            tLayerSet.ItemPropertyValueChanged += new ItemPropertyValueChangedEventHandler(ItemPropertyValueChanged);

            TreeNode ttreeNode = new TreeNode();
            ttreeNode.Text = tLayerSet.ItemName;
            ttreeNode.Tag = tLayerSet;
            ttreeNode.ImageIndex = 2;
            ttreeNode.SelectedImageIndex = 2;
            this.treeCheckScheme.SelectedNode.Nodes.Add(ttreeNode);
            ttreeNode.Parent.Expand();
            this.treeCheckScheme.SelectedNode = ttreeNode;
        }

        private void menu_SetLayerSetSource_Click(object sender, EventArgs e)
        {
            TreeNode rootNode = this.treeCheckScheme.SelectedNode;
            //if (rootNode.Tag i) return;

            IDictionary<string, DataCheckLayer> dictDataCheckLayer = new Dictionary<string, DataCheckLayer>();
            if (rootNode.Tag is DataCheckLayerSet)
            {
                DataCheckLayerSet tDataCheckLayer = rootNode.Tag as DataCheckLayerSet;
                string LineName = tDataCheckLayer.m_LineDataCheckLayer.ItemName;
                string PointName = tDataCheckLayer.m_PointDataCheckLayer.ItemName;
                if (!dictDataCheckLayer.ContainsKey(LineName))
                    dictDataCheckLayer.Add(LineName, tDataCheckLayer.m_LineDataCheckLayer);
                if (!dictDataCheckLayer.ContainsKey(PointName))
                    dictDataCheckLayer.Add(PointName, tDataCheckLayer.m_PointDataCheckLayer);
            }

            //初始化设置数据源实例对象
            FormSetDataSource tFrmSource = new FormSetDataSource();
            tFrmSource.DicDataCheckLayer = dictDataCheckLayer;
            tFrmSource.ShowInTaskbar = false;
            tFrmSource.ShowDialog();
            //刷新
            this.propNodeAttribute.Refresh();
        }

        private void propNodeAttribute_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            propNodeAttribute.Refresh();
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

    }
}