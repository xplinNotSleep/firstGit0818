using AG.COM.SDM.Utility;
using AG.COM.SDM.Utility.Common;
using AG.Pipe.Analyst3DModel.Editor;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AG.Pipe.Analyst3DModel
{
    /// <summary>
    /// 地质钻孔模型转换
    /// </summary>
    public partial class FormDzzk : SkinForm
    {
        DzConvertSchemeManager schemeManager = new DzConvertSchemeManager();
        public IWorkspace pWorkspace { get; set; }

        public FormDzzk()
        {
            InitializeComponent();
        }


        private void treeCheckScheme_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode treeNode = e.Node;
            if (treeNode != null)
            {
                if (treeNode.Tag != null)
                    this.propNodeAttribute.SelectedObject = new SchemeTypeDescriptor(treeNode.Tag);
            }
        }

        private void FormDzzk_Load(object sender, EventArgs e)
        {
            //加载地质数据模型转换方案
            LoadDzConvertScheme();
        }

        private void treeCheckScheme_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (this.treeCheckScheme.SelectedNode != e.Node)
                this.treeCheckScheme.SelectedNode = e.Node;

            if (e.Node.Tag is DzConvertSchemeManager)
            {
                this.treeCheckScheme.ContextMenuStrip = this.contextMenuStrip1;
            }
            else if (e.Node.Tag is DzConvertScheme)
            {
                this.treeCheckScheme.ContextMenuStrip = this.contextMenuStrip2;
            }
            else if(e.Node.Tag is DzzkScheme)
            {
                this.treeCheckScheme.ContextMenuStrip = this.contextMenuStrip2;
            }
            else if (e.Node.Tag is ZkytScheme)
            {
                this.treeCheckScheme.ContextMenuStrip = this.contextMenuStrip2;
            }
        }

        public void AddTreeItem(TreeNode parentNode, DzConvertScheme tScheme)
        {
            tScheme.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
            TreeNode rootNode = new TreeNode();
            rootNode.Text = tScheme.Name;
            rootNode.Tag = tScheme;

            tScheme.DzzkScheme.DataSource = "";
            tScheme.ZkytScheme.DzzkDataSource = "";
            tScheme.ZkytScheme.ZkytDataSource = "";

            TreeNode childNode1 = new TreeNode();
            childNode1.Text = tScheme.DzzkScheme.Name;
            childNode1.Tag = tScheme.DzzkScheme;
            rootNode.Nodes.Add(childNode1);
            tScheme.DzzkScheme.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
            TreeNode childNode2 = new TreeNode();
            childNode2.Text = tScheme.ZkytScheme.Name;
            childNode2.Tag = tScheme.ZkytScheme;
            rootNode.Nodes.Add(childNode2);
            tScheme.ZkytScheme.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
            parentNode.Nodes.Add(rootNode);
        }

        /// <summary>
        /// 加载地质数据转换方案
        /// </summary>
        public void LoadDzConvertScheme()
        {
            try
            {
                //读配置路径下的管线转换方案xml
                string configxmlPath = CommonConstString.STR_ConfigPath + "\\DzzkSchemeManager.xml";
                if (!Directory.Exists(CommonConstString.STR_ConfigPath))
                {
                    Directory.CreateDirectory(CommonConstString.STR_ConfigPath);
                }
                if(!File.Exists(configxmlPath))
                {
                    MessageBox.Show("找不到方案配置文件");
                    return;
                }

                XmlSerializer tXmlSerializer = new XmlSerializer(typeof(DzConvertSchemeManager));
                StreamReader tStreamReaderScheme = new StreamReader(configxmlPath);
                schemeManager = tXmlSerializer.Deserialize(tStreamReaderScheme) as DzConvertSchemeManager;
                tStreamReaderScheme.Close();
                this.treeCheckScheme.Nodes.Clear();
                TreeNode rootNode = new TreeNode();
                rootNode.Text = schemeManager.Name;
                rootNode.Tag = schemeManager;
                for (int i = 0; i < schemeManager.Schemes.Count; i++)
                {
                    DzConvertScheme scheme = schemeManager.Schemes[i];
                    AddTreeItem(rootNode, scheme);
                }
                this.treeCheckScheme.Nodes.Add(rootNode);
            }
            catch (Exception ex)
            {

                string eeee = ex.Message;
            }
        }

        private void tool_SaveScheme_Click(object sender, EventArgs e)
        {
            try
            {
                string configxmlPath = CommonConstString.STR_ConfigPath + "\\DzzkSchemeManager.xml";
                if (!Directory.Exists(CommonConstString.STR_ConfigPath))
                {
                    Directory.CreateDirectory(CommonConstString.STR_ConfigPath);
                }
                if (!File.Exists(configxmlPath))
                {
                    MessageBox.Show("找不到方案配置文件");
                    return;
                }

                XmlSerializer tXmlSerializer = new XmlSerializer(typeof(DzConvertSchemeManager));
                StreamWriter tStreamWriter = new StreamWriter(configxmlPath, false);
                tXmlSerializer.Serialize(tStreamWriter, schemeManager);
                tStreamWriter.Close();
                MessageBox.Show("保存成功!", "提示");
            }
            catch (Exception ex)
            {

                string eeee = ex.Message;
            }

        }

        private void treeCheckScheme_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                //如果对应的父节点被选中，对应的子节点全部被选中
                if (e.Node.Checked == true)
                {
                    cycleChild(e.Node, true);

                    if (e.Node.Parent != null)
                    {
                        //如果对应的父节点的其中一个子节点没有被选中父节点不选中，如果对应的子节点全部选中父节点选中      
                        cycleParent(e.Node, true);
                    }
                }
                //如果对应的父节点没被选中，对应的子节点与父节点全部不选中
                if (e.Node.Checked == false)
                {
                    if (!nextChildCheck(e.Node))
                    {
                        cycleParent(e.Node, false);
                    }

                    cycleChild(e.Node, false);
                }
            }

        }

        /// <summary>
        /// 选中父节点所有子节点被选中
        /// </summary>
        /// <param name="n">当前选中节点</param>
        /// <param name="check">是否被选中</param>
        private void cycleChild(TreeNode n, bool check)
        {
            if (n.Nodes.Count != 0)
            {
                foreach (TreeNode child in n.Nodes)
                {
                    child.Checked = check;
                    if (child.Nodes.Count != 0)
                    {
                        cycleChild(child, check);
                    }
                }
            }
        }

        /// <summary>
        /// 遍历父节点如果子节点选中则全部选中，子节点没有选中则父节点不选中
        /// </summary>
        /// <param name="n"></param>
        /// <param name="check"></param>
        private void cycleParent(TreeNode n, bool check)
        {
            if (n.Parent != null)
            {
                n.Parent.Checked = check;
                cycleParent(n.Parent, check);
            }
        }

        /// <summary>
        /// 判断通级节点是否全选
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private bool nextChildCheck(TreeNode n)
        {
            if (n.Parent == null) return false;
            foreach (TreeNode node in n.Parent.Nodes)
            {
                if (node.Checked == true)
                {
                    return true;
                }
            }
            return false;
        }

        public List<string> Layer3D { get; } = new List<string>(); //生成的3D图层名称集合
        public List<DzzkScheme> DzzkLayer3D { get; } = new List<DzzkScheme>();//生成模型的地质钻孔方案集合

        public List<ZkytScheme> ZkytLayer3D { get; } = new List<ZkytScheme>();

        
        /// <summary>
        /// 运行生成模型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tool_RunCheck_Click(object sender, EventArgs e)
        {
            //检查地质数据的数据源
            List<string> list = CheckDataSource();
            if (list.Count > 0)
            {
                FormErrorList frm = new FormErrorList(list);
                frm.ShowDialog();
                return;
            }

            ITrackProgress trackProgress = new TrackProgressDialog();
            trackProgress.DisplayTotal = true;
            trackProgress.AutoFinishClose = true;

            Application.DoEvents();
            try
            {
                Layer3D.Clear();
                DzzkLayer3D.Clear();
                ZkytLayer3D.Clear();
                //创建一个存储生成三维模型的GDB在指定路径下(指定存在D盘根目录)
                string gdbfile = $"{schemeManager.Name}_{DateTime.Now.ToString("yyyyMMddHHmmss")}";
                IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
                IWorkspaceName workspaceName = new WorkspaceNameClass();
                //判断是否存在方案导出路径
                if (!Directory.Exists(schemeManager.OutPath))
                {
                    workspaceName = workspaceFactory.Create("D:/", gdbfile, null, 0);
                }
                else
                {
                    workspaceName = workspaceFactory.Create(schemeManager.OutPath, gdbfile, null, 0);
                }
                IName pTempName = (IName)workspaceName;
                IWorkspace pTempWorkspace = (IWorkspace)pTempName.Open();

                //添加勾选的地质转换方案
                List<TreeNode> list1 = new List<TreeNode>();
                foreach (TreeNode child in this.treeCheckScheme.Nodes)
                {
                    if (child.Checked)
                    {
                        foreach (TreeNode nodeScheme in child.Nodes)
                        {
                            foreach (TreeNode node in nodeScheme.Nodes)
                            {
                                if (node.Checked)
                                {
                                    list1.Add(node);
                                }
                            }
                        }
                    }

                }

                trackProgress.TotalMax = list1.Count;
                trackProgress.TotalMin = 0;
                trackProgress.TotalValue = 0;
                trackProgress.Show();
                trackProgress.TotalMessage = $"正在初始化模型库";
                trackProgress.TotalValue = 1;
                //根据管点附属物获取附属物模型
                Dictionary<string, IMarker3DSymbol> dictMarker3DSymbol = GetMarker3DSymbols(CommonConstString.STR_StylePath + "\\地质3D.ServerStyle", trackProgress);
                trackProgress.SubMessage = $"";
                trackProgress.SubValue = 0;
                Application.DoEvents();
                trackProgress.TotalMax = list1.Count;
                for (int i = 0; i < list1.Count; i++)
                {
                    trackProgress.TotalMessage = $"正在生成{list1[i].Text}模型";
                    trackProgress.TotalValue++;
                    Application.DoEvents();
                    //如果是地质钻孔
                    if (list1[i].Tag is DzzkScheme)
                    {
                        DzzkScheme dzzkScheme = list1[i].Tag as DzzkScheme ;
                        //绘制地质钻孔模型
                        dzzkScheme.RunTask(pTempWorkspace,trackProgress, dictMarker3DSymbol);
                        DzzkLayer3D.Add(dzzkScheme);
                        Layer3D.AddRange(dzzkScheme.DzzkLayer3D);
                    }
                    //如果是钻孔岩土
                    if (list1[i].Tag is ZkytScheme)
                    {
                        ZkytScheme zkytScheme = list1[i].Tag as ZkytScheme;
                        //绘制钻孔岩土模型
                        zkytScheme.RunTask(pTempWorkspace,trackProgress,dictMarker3DSymbol);
                        ZkytLayer3D.Add(zkytScheme);
                        Layer3D.AddRange(zkytScheme.ZkytLayer3D);
                    }
                }
                trackProgress.SetFinish();
                pWorkspace = pTempWorkspace;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                trackProgress.SetFinish();
            }
            if (Layer3D.Count > 0 || DzzkLayer3D.Count > 0 || ZkytLayer3D.Count>0)
            {
                DialogResult dr = MessageBox.Show("生成成功! 是否显示三维地质图层", "提示", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.Yes;
                    this.Close();

                }
                else
                {
                    this.DialogResult = DialogResult.No;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("生成失败!", "提示", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// 根据管点附属物获取附属物模型
        /// </summary>
        /// <param name="path"></param>
        /// <param name="trackProgress"></param>
        /// <returns></returns>
        public Dictionary<string, IMarker3DSymbol> GetMarker3DSymbols(string path, ITrackProgress trackProgress)
        {
            Dictionary<string, IMarker3DSymbol> dicMarker3DSymbols = new Dictionary<string, IMarker3DSymbol>();
            IStyleGalleryItem mStyleItem;
            IStyleGallery pSGallery = new ServerStyleGalleryClass();// new StyleGalleryClass();
            IStyleGalleryStorage pSGStorage = pSGallery as IStyleGalleryStorage;
            string strPath = path;
            pSGStorage.TargetFile = strPath;
            pSGStorage.AddFile(strPath);
            IEnumStyleGalleryItem pEnumGItem;
            pEnumGItem = pSGallery.get_Items("Marker Symbols", strPath, "");
            pEnumGItem.Reset();
            mStyleItem = pEnumGItem.Next();
            trackProgress.SubMax = pSGallery.ClassCount;
            while (mStyleItem != null)
            {
                if (mStyleItem.Item is IMultiLayerMarkerSymbol)
                {
                    IMultiLayerMarkerSymbol pIMultiLayerMarkerSymbol = mStyleItem.Item as IMultiLayerMarkerSymbol;
                    IMarker3DSymbol p3DSymbol = pIMultiLayerMarkerSymbol.Layer[0] as IMarker3DSymbol;
                    if (!dicMarker3DSymbols.ContainsKey(mStyleItem.Name))
                    {
                        dicMarker3DSymbols.Add(mStyleItem.Name, p3DSymbol);
                    }
                    trackProgress.SubMessage = $"正在加载{mStyleItem.Name}模型";
                    trackProgress.SubValue++;
                    Application.DoEvents();
                }
                mStyleItem = pEnumGItem.Next();
            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(pEnumGItem);

            return dicMarker3DSymbols;
        }

        /// <summary>
        /// 设置数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tool_SetDataSource_Click(object sender, EventArgs e)
        {
            //规则核查图层
            List<string> DicDataCheckLayer = new List<string>();
            //添加勾选的图层类型到规则核查列表中
            foreach (TreeNode child in this.treeCheckScheme.Nodes)
            {
                foreach (TreeNode nodeScheme in child.Nodes)//生成方案
                {
                    foreach (TreeNode node in nodeScheme.Nodes)//岩土/钻孔
                    {
                        if (node.Checked)
                        {
                            if (node.Tag is DzzkScheme)
                            {
                                DzzkScheme dzzkScheme = node.Tag as DzzkScheme;
                                if (!DicDataCheckLayer.Exists(m => m == dzzkScheme.LayerName))
                                    DicDataCheckLayer.Add(dzzkScheme.LayerName);
                            }
                            if (node.Tag is ZkytScheme)
                            {
                                ZkytScheme zkytScheme = node.Tag as ZkytScheme;
                                if (!DicDataCheckLayer.Exists(m => m == zkytScheme.ZkytDataName))
                                    DicDataCheckLayer.Add(zkytScheme.ZkytDataName);
                                if (!DicDataCheckLayer.Exists(m => m == zkytScheme.DzzkLayerName))
                                    DicDataCheckLayer.Add(zkytScheme.DzzkLayerName);
                            }
                        }
                    }
                }

            }
            FormSetDataSource formSetDataSource = new FormSetDataSource();
            formSetDataSource.DicDataCheckLayer = DicDataCheckLayer;
            DialogResult dr = formSetDataSource.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Dictionary<string, IFeatureClass> DictFeatClass = formSetDataSource.DictFeatClass;
                Dictionary<string, ITable> DictTable = formSetDataSource.DicTable;
                Dictionary<string, string> DictDataName = formSetDataSource.DicDataName;
                SetDataSource(DictFeatClass,DictTable,DictDataName);
            }

        }

        /// <summary>
        /// 设置勾选图层数据源
        /// </summary>
        /// <param name="DictFeatClass"></param>
        private void SetDataSource(Dictionary<string, IFeatureClass> DictFeatClass,
            Dictionary<string,ITable> DictTable,Dictionary<string,string> DictDataName)
        {
            foreach (TreeNode child in this.treeCheckScheme.Nodes)
            {
                foreach (TreeNode nodeScheme in child.Nodes) //方案
                {
                    foreach (TreeNode node in nodeScheme.Nodes)
                    {
                        if (node.Checked)
                        {
                            //地质钻孔方案
                            if (node.Tag is DzzkScheme)
                            {
                                DzzkScheme dzzkScheme = node.Tag as DzzkScheme;

                                string feaLayerName = "";
                                if(DictDataName.ContainsKey(dzzkScheme.LayerName))
                                {
                                    feaLayerName = DictDataName[dzzkScheme.LayerName];
                                }

                                if (DictFeatClass.ContainsKey(feaLayerName))
                                {
                                    dzzkScheme.DzzkFeatureClass = DictFeatClass[feaLayerName];
                                    dzzkScheme.DataSource = (dzzkScheme.DzzkFeatureClass as IDataset).Name;
                                }
                            }
                            //钻孔岩土方案
                            if (node.Tag is ZkytScheme)
                            {
                                ZkytScheme zkytScheme = node.Tag as ZkytScheme;
                                //if (DictFeatClass.ContainsKey(zkytScheme.ZkytLayerName.ToUpper()))
                                //{
                                //    zkytScheme.ZkytFeatureClass = DictFeatClass[zkytScheme.ZkytLayerName.ToUpper()];
                                //    zkytScheme.ZkytDataSource = (zkytScheme.ZkytFeatureClass as IDataset).Name;
                                //}
                                string tableName = "";
                                string feaLayerName = "";
                                if (DictDataName.ContainsKey(zkytScheme.ZkytDataName))
                                {
                                    tableName = DictDataName[zkytScheme.ZkytDataName];
                                }
                                if (DictDataName.ContainsKey(zkytScheme.DzzkLayerName))
                                {
                                    feaLayerName = DictDataName[zkytScheme.DzzkLayerName];
                                }

                                if (DictTable.ContainsKey(tableName))
                                {
                                    zkytScheme.ZkytDataTable = DictTable[tableName];
                                    zkytScheme.ZkytDataSource = (zkytScheme.ZkytDataTable as IDataset).Name;
                                }

                                if (DictFeatClass.ContainsKey(feaLayerName))
                                {
                                    zkytScheme.DzzkFeatureClass = DictFeatClass[feaLayerName];
                                    zkytScheme.DzzkDataSource = (zkytScheme.DzzkFeatureClass as IDataset).Name;
                                }

                            }


                        }

                    }
                }
            }
        }

        /// <summary>
        /// 检查地质数据是否设置数据源
        /// </summary>
        /// <returns></returns>
        private List<string> CheckDataSource()
        {
            List<string> errorMsg = new List<string>();
            foreach (TreeNode child in this.treeCheckScheme.Nodes) //方案
            {
                foreach (TreeNode nodeScheme in child.Nodes) //地质数据生成方案
                {
                    foreach (TreeNode node in nodeScheme.Nodes)//钻孔/岩土图层
                    {
                        if (node.Checked)
                        {
                            if (node.Tag is DzzkScheme)
                            {
                                DzzkScheme dzzkScheme = node.Tag as DzzkScheme;
                                if (dzzkScheme.DzzkFeatureClass == null)
                                {
                                    errorMsg.Add($"{dzzkScheme.LayerName}未设置数据源");
                                }
                            }
                            if (node.Tag is ZkytScheme)
                            {
                                ZkytScheme zkytScheme = node.Tag as ZkytScheme;
                                if (zkytScheme.ZkytDataTable == null)
                                {
                                    errorMsg.Add($"{zkytScheme.ZkytDataName}未设置岩土图层数据源");
                                }
                                if (zkytScheme.DzzkFeatureClass == null)
                                {
                                    errorMsg.Add($"{zkytScheme.DzzkLayerName}未设置钻孔图层数据源");
                                }

                            }
                        }
                    }

                }
            }

            return errorMsg;
        }
        private void OnSchemePropertyValueChanged(object sender, SchemePropertyEventArgs e)
        {
            ISchemeName itemProperty = e.ItemProperty as ISchemeName;
            TreeNode selTreeNode = this.treeCheckScheme.SelectedNode;
            selTreeNode.Text = itemProperty.Name;
        }
        private void 新建模型方案ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode parentNode = this.treeCheckScheme.SelectedNode;
            //初始化数据核查方案属性实例
            DzConvertScheme tScheme = new DzConvertScheme();
            tScheme.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
            TreeNode rootNode = new TreeNode();
            rootNode.Text = tScheme.Name;
            rootNode.Tag = tScheme;
            tScheme.DzzkScheme.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
            tScheme.ZkytScheme.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
            TreeNode childNode1 = new TreeNode();
            childNode1.Text = tScheme.DzzkScheme.Name;
            childNode1.Tag = tScheme.DzzkScheme;
            rootNode.Nodes.Add(childNode1);
            TreeNode childNode2 = new TreeNode();
            childNode2.Text = tScheme.ZkytScheme.Name;
            childNode2.Tag = tScheme.ZkytScheme;
            rootNode.Nodes.Add(childNode2);
            schemeManager.Schemes.Add(tScheme);
            parentNode.Nodes.Add(rootNode);
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode tSelNode = this.treeCheckScheme.SelectedNode;
            tSelNode.Remove();
            DzConvertScheme scheme = tSelNode.Tag as DzConvertScheme;
            if (scheme != null)
            {
                schemeManager.Schemes.Remove(scheme);
            }
            #region
            //else
            //{
            //    DzzkScheme dzzkScheme = tSelNode.Tag as DzzkScheme;
            //    if(dzzkScheme!=null)
            //    {
            //        TreeNode pNode = tSelNode.Parent;
            //        DzConvertScheme dzConvertScheme = pNode.Tag as DzConvertScheme;
            //        if(dzConvertScheme!=null)
            //        {

            //        }
            //    }
            //}
            #endregion
        }

        DzConvertScheme tSchemeCopy = null;
        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode parentNode = this.treeCheckScheme.SelectedNode;
            if (parentNode.Tag is DzConvertScheme)
            {
                tSchemeCopy = TransExpV2<DzConvertScheme, DzConvertScheme>.Trans(parentNode.Tag as DzConvertScheme);

            }
            else
            {
                MessageBox.Show("只能复制图层模型");
            }
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode parentNode = this.treeCheckScheme.SelectedNode;
            if (parentNode.Tag is DzConvertSchemeManager)
            {
                tSchemeCopy.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
                TreeNode rootNode = new TreeNode();
                rootNode.Text = tSchemeCopy.Name;
                rootNode.Tag = tSchemeCopy;
                tSchemeCopy.DzzkScheme.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
                tSchemeCopy.ZkytScheme.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
                TreeNode childNode1 = new TreeNode();
                childNode1.Text = tSchemeCopy.DzzkScheme.Name;
                childNode1.Tag = tSchemeCopy.ZkytScheme;
                rootNode.Nodes.Add(childNode1);
                TreeNode childNode2 = new TreeNode();
                childNode2.Text = tSchemeCopy.ZkytScheme.Name;
                childNode2.Tag = tSchemeCopy.ZkytScheme;
                rootNode.Nodes.Add(childNode2);
                (parentNode.Tag as DzConvertSchemeManager).Schemes.Add(tSchemeCopy);
                parentNode.Nodes.Add(rootNode);
            }
        }
    }


}
