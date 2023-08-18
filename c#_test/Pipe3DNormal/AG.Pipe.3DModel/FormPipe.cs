using AG.COM.SDM.Utility;
using AG.COM.SDM.Utility.Common;
using AG.COM.SDM.Utility.Logger;
using AG.Pipe.Analyst3DModel.Editor;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AG.Pipe.Analyst3DModel
{
    public partial class FormPipe : SkinForm
    {
        public FormPipe()
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
        PipeSchemeManager schemeManager = new PipeSchemeManager();
        public IWorkspace pWorkspace { get; set; }
        private void FormMain_Load(object sender, EventArgs e)
        {
            //加载管线模型转换方案
            LoadPipeScheme();
        }
        private void treeCheckScheme_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (this.treeCheckScheme.SelectedNode != e.Node)
                this.treeCheckScheme.SelectedNode = e.Node;

            if (e.Node.Tag is PipeSchemeManager)
            {
                this.treeCheckScheme.ContextMenuStrip = this.contextMenuStrip1;
            }
            else if (e.Node.Tag is PipeScheme)
            {

                this.treeCheckScheme.ContextMenuStrip = this.contextMenuStrip2;
            }
            else if (e.Node.Tag is PipeSchemeGroup)
            {

                this.treeCheckScheme.ContextMenuStrip = this.contextMenuStrip3;
            }
        }

        /// <summary>
        /// 在管线方案配置文件里面把管线管点方案加载到树状列表中
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="tScheme"></param>
        public void AddTreeItem(TreeNode parentNode, PipeScheme tScheme)
        {
            tScheme.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
            TreeNode rootNode = new TreeNode();
            rootNode.Text = tScheme.Name;
            rootNode.Tag = tScheme;

            //先清空数据源名称配置
            tScheme.LineScheme.LineDataSource = "";
            //tScheme.LineScheme.PointDataSource = "";
            tScheme.PointScheme.LineDataSource = "";
            tScheme.PointScheme.PointDataSource = "";

            TreeNode childNode1 = new TreeNode();
            childNode1.Text = tScheme.LineScheme.Name;
            childNode1.Tag = tScheme.LineScheme;
            rootNode.Nodes.Add(childNode1);
            tScheme.LineScheme.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
            TreeNode childNode2 = new TreeNode();
            childNode2.Text = tScheme.PointScheme.Name;
            childNode2.Tag = tScheme.PointScheme;
            rootNode.Nodes.Add(childNode2);
            tScheme.PointScheme.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
            parentNode.Nodes.Add(rootNode);
        }
        public void AddTreeItem(TreeNode parentNode, PipeSchemeGroup schemeGroup)
        {
            schemeGroup.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
            TreeNode rootNode = new TreeNode();
            rootNode.Text = schemeGroup.Name;
            rootNode.Tag = schemeGroup;
            for (int i = 0; i < schemeGroup.Schemes.Count; i++)
            {
                PipeScheme scheme = schemeGroup.Schemes[i];
                AddTreeItem(rootNode, scheme);
            }
            parentNode.Nodes.Add(rootNode);
        }

        /// <summary>
        /// 加载管线模型转换方案
        /// </summary>
        public void LoadPipeScheme()
        {
            try
            {
                //读配置路径下的管线转换方案xml
                string configxmlPath = CommonConstString.STR_ConfigPath + "\\PipeSchemeManager.xml";
                if (!Directory.Exists(CommonConstString.STR_ConfigPath))
                {
                    Directory.CreateDirectory(CommonConstString.STR_ConfigPath);
                }

                XmlSerializer tXmlSerializer = new XmlSerializer(typeof(PipeSchemeManager));
                StreamReader tStreamReaderScheme = new StreamReader(configxmlPath);
                schemeManager = tXmlSerializer.Deserialize(tStreamReaderScheme) as PipeSchemeManager;
                schemeManager.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
                tStreamReaderScheme.Close();
                this.treeCheckScheme.Nodes.Clear();
                TreeNode rootNode = new TreeNode();
                rootNode.Text = schemeManager.Name;
                rootNode.Tag = schemeManager;
                for (int i = 0; i < schemeManager.Schemes.Count; i++)
                {
                    PipeScheme scheme = schemeManager.Schemes[i];
                    AddTreeItem(rootNode, scheme);
                }
                for (int i = 0; i < schemeManager.SchemeGroups.Count; i++)
                {
                    PipeSchemeGroup schemeGroup = schemeManager.SchemeGroups[i];
                    AddTreeItem(rootNode, schemeGroup);
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
                string configxmlPath = CommonConstString.STR_ConfigPath + "\\PipeSchemeManager.xml";
                if (!Directory.Exists(CommonConstString.STR_ConfigPath))
                {
                    Directory.CreateDirectory(CommonConstString.STR_ConfigPath);
                }

                XmlSerializer tXmlSerializer = new XmlSerializer(typeof(PipeSchemeManager));
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
                        //if (nextCheck(e.Node))
                        //{
                        //    cycleParent(e.Node, true);
                        //}
                        //else
                        //{
                        //    cycleParent(e.Node, false);
                        //}
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
                //if (nextCheck(n))
                //{
                //    n.Parent.Checked = true;
                //}
                //else
                //{
                //    n.Parent.Checked = false;
                //}
                n.Parent.Checked = check;
                cycleParent(n.Parent, check);
            }
        }
        /// <summary>
        /// 判断通级节点是否全选
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private bool nextCheck(TreeNode n)
        {
            foreach (TreeNode node in n.Parent.Nodes)
            {
                if (node.Checked == false)
                {
                    return false;
                }
            }
            return true;
        }
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
        public List<string> Layer3D { get; } = new List<string>(); //生成的3D管线名称集合

        public List<string> Line3DNames { get; } = new List<string>(); //生成的3D管线名称集合

        public List<string> Point3DNames{ get; } = new List<string>(); //生成的3D管线名称集合

        public List<LineScheme> LineLayer3D { get; } = new List<LineScheme>();//生成的3D管线方案集合

        public List<PointScheme> PointLayer3D { get; } = new List<PointScheme>();

        private void tool_RunCheck_Click(object sender, EventArgs e)
        {
            //检查勾选的生成管线类的数据源
            List<string> list = CheckDataSource();
            if (list.Count > 0)
            {
                FormErrorList frm = new FormErrorList(list);
                frm.ShowDialog();
                return;
            }

            bool HasLine = false;
            bool HasPoint = false;

            ITrackProgress trackProgress = new TrackProgressDialog();
            trackProgress.DisplayTotal = true;
            trackProgress.AutoFinishClose = true;

            Application.DoEvents();
            try
            {
                Line3DNames.Clear();
                Point3DNames.Clear();
                Layer3D.Clear();
                LineLayer3D.Clear();
                PointLayer3D.Clear();
                //先指定转换方案要使用的样式符号配置文件
                string stylefile = "";
                if(string.IsNullOrWhiteSpace(schemeManager.StylePath))
                {
                    stylefile = "\\管线3D.ServerStyle";
                    PublicPath.StyleFilePath = stylefile;
                }
                else
                {
                    string stylePath= schemeManager.StylePath;
                    stylefile = Path.GetFileName(stylePath);
                    PublicPath.StyleFilePath = stylePath;
                }

                #region 适配球面坐标系
                if (File.Exists(schemeManager.ProjectPath))
                {
                    PublicPath.ProjectPath = schemeManager.ProjectPath;
                }
                else
                {
                    PublicPath.ProjectPath = CommonConstString.STR_ConfigPath + "\\CGCS2000 3 Degree GK Zone 42.prj";
                }
                #endregion

                //创建一个存储生成三维模型的GDB在指定路径下(指定存在D盘根目录)
                string gdbfile = $"{schemeManager.Name}_{DateTime.Now.ToString("yyyyMMddHHmmss")}";
                IWorkspaceFactory workspaceFactory = new FileGDBWorkspaceFactoryClass();
                IWorkspaceName workspaceName = new WorkspaceNameClass();

                //判断是否存在方案导出路径
                if (!Directory.Exists(schemeManager.OutPath))
                {
                    PublicPath.SavePath = "D:/";
                    if(!Directory.Exists(PublicPath.SavePath))
                    {
                        MessageBox.Show("电脑上没有D盘，请重新设置数据导出路径!");
                        return; 
                    }
                    workspaceName = workspaceFactory.Create("D:/", gdbfile, null, 0);
                }
                else
                {
                    PublicPath.SavePath = schemeManager.OutPath;
                    workspaceName = workspaceFactory.Create(schemeManager.OutPath, gdbfile, null, 0);
                }
                IName pTempName = (IName)workspaceName;
                IWorkspace pTempWorkspace = (IWorkspace)pTempName.Open();

                //添加勾选的管线管点类型
                List<TreeNode> list1 = new List<TreeNode>();
                foreach (TreeNode child in this.treeCheckScheme.Nodes)
                {
                    if (child.Checked)
                    {
                        foreach (TreeNode group in child.Nodes)
                        {
                            if (group.Checked)
                            {
                                foreach (TreeNode nodeScheme in group.Nodes)
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

                    }

                }

                trackProgress.TotalMax = list1.Count;
                trackProgress.TotalMin = 0;
                trackProgress.TotalValue = 0;
                trackProgress.Show();
                trackProgress.TotalMessage = $"正在初始化模型库";
                trackProgress.TotalValue = 1;
                //根据管点附属物获取附属物模型
                Dictionary<string, IMarker3DSymbol> dictMarker3DSymbol = GetMarker3DSymbols
                    (CommonConstString.STR_StylePath + "\\" +stylefile, trackProgress);
                trackProgress.SubMessage = $"";
                trackProgress.SubValue=0;
                Application.DoEvents();
                trackProgress.TotalMax = list1.Count;
                for (int i = 0; i < list1.Count; i++)
                {
                    if (!trackProgress.IsContinue) break;
                    trackProgress.TotalMessage = $"正在生成{list1[i].Text}模型";
                    trackProgress.TotalValue++;
                    Application.DoEvents();
                    //如果是管线
                    if (list1[i].Tag is LineScheme)
                    {
                        HasLine = true;
                        LineScheme line = list1[i].Tag as LineScheme;
                        PipeSchemeGroup schemeGroup = list1[i].Parent.Parent.Tag as PipeSchemeGroup;
                        //绘制管线模型
                        bool IsRunSuc = line.RunTask(pTempWorkspace, schemeGroup.Name, trackProgress, dictMarker3DSymbol);
                        if (!IsRunSuc)
                        {
                            break;
                        }
                        LineLayer3D.Add(line);
                        Line3DNames.AddRange(line.Layer3D);
                    }
                    //如果是管点
                    if (list1[i].Tag is PointScheme)
                    {
                        HasPoint = true;
                        PointScheme point = list1[i].Tag as PointScheme;
                        PipeSchemeGroup schemeGroup = list1[i].Parent.Parent.Tag as PipeSchemeGroup;
                        //绘制管点模型
                        bool IsRunSuc = point.RunTask(pTempWorkspace, schemeGroup.Name, trackProgress, dictMarker3DSymbol);
                        if(!IsRunSuc)
                        {
                            break;
                        }
                        PointLayer3D.Add(point);
                        Point3DNames.AddRange(point.Layer3D);
                    }
                }
                trackProgress.SetFinish();
                pWorkspace = pTempWorkspace;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
                ExceptionLog.LogError(ex.Message, ex);
            } 
            finally
            {
                trackProgress.SetFinish();
            }

            //如果勾选了管线模型
            if(HasLine)
            {
                //如果生成了管线模型
                if(Line3DNames.Count > 0)
                {
                    //同时勾选管点模型
                    if(HasPoint)
                    {
                        //生成了管点模型
                        if(Point3DNames.Count>0)
                        {
                            Layer3D.AddRange(Line3DNames);
                            Layer3D.AddRange(Point3DNames);
                        }
                        else
                        {
                            MessageBox.Show("管点模型生成失败!", "提示", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        Layer3D.AddRange(Line3DNames);
                    }
                }
                else
                {
                    MessageBox.Show("管线模型生成失败!", "提示", MessageBoxButtons.OK);
                }
            }
            else
            {
                //勾选管点模型
                if (HasPoint)
                {
                    //生成了管点模型
                    if (Point3DNames.Count > 0)
                    {
                        Layer3D.AddRange(Point3DNames);
                    }
                    else
                    {
                        MessageBox.Show("管点模型生成失败!", "提示", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("无法生成模型，没有勾选管线和管点项!", "提示", MessageBoxButtons.OK);
                }
            }

            if (Layer3D.Count > 0)
            {
                DialogResult dr = MessageBox.Show("生成成功! 是否显示生成的三维模型", "提示", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.Yes;
                    this.Close();

                }
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
                foreach (TreeNode group in child.Nodes)
                {
                    foreach (TreeNode nodeScheme in group.Nodes)
                    {
                        foreach (TreeNode node in nodeScheme.Nodes)
                        {
                            if (node.Checked)
                            {
                                if (node.Tag is LineScheme)
                                {
                                    LineScheme line = node.Tag as LineScheme;

                                    if (string.IsNullOrEmpty(line.LineLayerName)) continue;

                                    if (!DicDataCheckLayer.Exists(m => m == line.LineLayerName))
                                        DicDataCheckLayer.Add(line.LineLayerName);
                                    //if (!DicDataCheckLayer.Exists(m => m == line.PointLayerName))
                                    //    DicDataCheckLayer.Add(line.PointLayerName);
                                }

                                if (node.Tag is PointScheme)
                                {
                                    PointScheme point = node.Tag as PointScheme;

                                    if (string.IsNullOrEmpty(point.LineLayerName) ||
                                        string.IsNullOrEmpty(point.PointLayerName)) continue;

                                    if (!DicDataCheckLayer.Exists(m => m == point.LineLayerName))
                                        DicDataCheckLayer.Add(point.LineLayerName);
                                    if (!DicDataCheckLayer.Exists(m => m == point.PointLayerName))
                                        DicDataCheckLayer.Add(point.PointLayerName);
                                }
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
                Dictionary<string, string> DicFeatName = formSetDataSource.DicDataName;
                Dictionary<string, IFeatureClass> DictFeatClass = formSetDataSource.DictFeatClass;
                SetDataSource(DictFeatClass, DicFeatName);

            }

        }

        /// <summary>
        /// 根据设置的数据源给图层小类设置好数据源名称
        /// </summary>
        /// <param name="DictFeatClass"></param>
        /// <param name="DictFeatName"></param>
        private void SetDataSource(Dictionary<string, IFeatureClass> DictFeatClass,Dictionary<string,string> DictFeatName)
        {
            foreach (TreeNode child in this.treeCheckScheme.Nodes)
            {
                foreach (TreeNode group in child.Nodes)
                {
                    foreach (TreeNode nodeScheme in group.Nodes) //图层方案
                    {
                        foreach (TreeNode node in nodeScheme.Nodes)
                        {
                            if (node.Checked)
                            {
                                if (node.Tag is LineScheme)
                                {
                                    LineScheme line = node.Tag as LineScheme;
                                    string lineFeatName = "";
                                    string ptFeatName = "";
                                    if(DictFeatName.ContainsKey(line.LineLayerName))
                                    {
                                        lineFeatName = DictFeatName[line.LineLayerName];
                                    }
                                    if (DictFeatClass.ContainsKey(lineFeatName))
                                    {
                                        line.LineFeatureClass = DictFeatClass[lineFeatName];
                                        line.WorkPath = (line.LineFeatureClass as IDataset).Workspace.PathName;
                                        line.LineDataSource = (line.LineFeatureClass as IDataset).Name;
                                    }
                                }

                                if (node.Tag is PointScheme)
                                {
                                    PointScheme point = node.Tag as PointScheme;
                                    string lineFeatName = "";
                                    string ptFeatName = "";
                                    if(DictFeatName.ContainsKey(point.LineLayerName))
                                    {
                                        lineFeatName = DictFeatName[point.LineLayerName];
                                    }
                                    if (DictFeatName.ContainsKey(point.PointLayerName))
                                    {
                                        ptFeatName = DictFeatName[point.PointLayerName];
                                    }
                                    if (DictFeatClass.ContainsKey(lineFeatName))
                                    {
                                        point.LineFeatureClass = DictFeatClass[lineFeatName];
                                        point.LineDataSource = (point.LineFeatureClass as IDataset).Name;
                                    }
                                    if (DictFeatClass.ContainsKey(ptFeatName))
                                    {
                                        point.PointFeatureClass = DictFeatClass[ptFeatName];
                                        point.WorkPath = (point.PointFeatureClass as IDataset).Workspace.PathName;
                                        point.PointDataSource = (point.PointFeatureClass as IDataset).Name;
                                    }

                                }


                            }

                        }
                    }

                }
            }
        }

        /// <summary>
        /// 检查勾选的管线管点类型是否设置数据源
        /// </summary>
        /// <returns></returns>
        private List<string> CheckDataSource()
        {
            List<string> errorMsg = new List<string>();
            foreach (TreeNode child in this.treeCheckScheme.Nodes) //方案
            {
                foreach (TreeNode group in child.Nodes) //图层组(遍历管线大类)
                {
                    foreach (TreeNode nodeScheme in group.Nodes) //图层方案(遍历管线小类)
                    {
                        foreach (TreeNode node in nodeScheme.Nodes)//图层(遍历小类下的管线管点)
                        {
                            if (node.Checked)
                            {
                                if (node.Tag is LineScheme)
                                {
                                    LineScheme line = node.Tag as LineScheme;
                                    if (line.LineFeatureClass == null)
                                    {
                                        errorMsg.Add($"{line.LineLayerName}未设置管线数据源");
                                    }
                                    #region
                                    //if(line.PointFeatureClass==null)
                                    //{
                                    //    errorMsg.Add($"{line.LineLayerName}未设置管点数据源");
                                    //}
                                    #endregion
                                }

                                if (node.Tag is PointScheme)
                                {
                                    PointScheme point = node.Tag as PointScheme;
                                    if (point.LineFeatureClass == null)
                                    {
                                        errorMsg.Add($"{point.PointLayerName}未设置管线数据源");
                                    }
                                    if (point.PointFeatureClass == null)
                                    {
                                        errorMsg.Add($"{point.PointLayerName}未设置管点数据源");
                                    }

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
            PipeScheme tScheme = new PipeScheme();
            tScheme.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
            TreeNode rootNode = new TreeNode();
            rootNode.Text = tScheme.Name;
            rootNode.Tag = tScheme;
            tScheme.LineScheme.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
            tScheme.PointScheme.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
            TreeNode childNode1 = new TreeNode();
            childNode1.Text = tScheme.LineScheme.Name;
            childNode1.Tag = tScheme.LineScheme;
            rootNode.Nodes.Add(childNode1);
            TreeNode childNode2 = new TreeNode();
            childNode2.Text = tScheme.PointScheme.Name;
            childNode2.Tag = tScheme.PointScheme;
            rootNode.Nodes.Add(childNode2);
            schemeManager.Schemes.Add(tScheme);
            parentNode.Nodes.Add(rootNode);
        }
        private void 新建图层组ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode parentNode = this.treeCheckScheme.SelectedNode;
            //初始化数据核查方案属性实例
            PipeSchemeGroup tScheme = new PipeSchemeGroup();
            tScheme.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
            TreeNode rootNode = new TreeNode();
            rootNode.Text = tScheme.Name;
            rootNode.Tag = tScheme;
            schemeManager.SchemeGroups.Add(tScheme);
            parentNode.Nodes.Add(rootNode);
        }


        private void 一键设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PipeScheme scheme = schemeManager.SchemeGroups.FirstOrDefault(m => m.Name == "给水")?.Schemes.FirstOrDefault(m => m.Name == "给水");
            if (scheme == null)
            {
                MessageBox.Show("请配置参考管线类别模型方案", "提示");
                return;
            }
            TreeNode tSelNode = this.treeCheckScheme.SelectedNode;
            PipeScheme pScheme = tSelNode.Tag as PipeScheme;
            if(pScheme!=null)
            {
                pScheme.LineScheme.Set(scheme.LineScheme);
                pScheme.PointScheme.Set(scheme.PointScheme);
            }
           
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否删除此图层模型方案?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                TreeNode tSelNode = this.treeCheckScheme.SelectedNode;
                PipeScheme scheme = tSelNode.Tag as PipeScheme;
                if (scheme != null)
                {
                    PipeSchemeGroup schemeGroup = tSelNode.Parent.Tag as PipeSchemeGroup;
                    if (schemeGroup != null)
                    {
                        tSelNode.Remove();
                        schemeGroup.Schemes.Remove(scheme);
                    }
                    else
                    {
                        PipeSchemeManager schemeManager = tSelNode.Parent.Tag as PipeSchemeManager;
                        if (schemeManager != null)
                        {
                            tSelNode.Remove();
                            schemeManager.Schemes.Remove(scheme);
                        }

                    }
                }
            }
        }

        private void 新建图层模型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode parentNode = this.treeCheckScheme.SelectedNode;
            if (parentNode.Tag is PipeSchemeGroup)
            {
                PipeSchemeGroup schemeGroup = parentNode.Tag as PipeSchemeGroup;
                //初始化数据核查方案属性实例
                PipeScheme tScheme = new PipeScheme();
                tScheme.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
                TreeNode rootNode = new TreeNode();
                rootNode.Text = tScheme.Name;
                rootNode.Tag = tScheme;
                tScheme.LineScheme.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
                tScheme.PointScheme.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
                TreeNode childNode1 = new TreeNode();
                childNode1.Text = tScheme.LineScheme.Name;
                childNode1.Tag = tScheme.LineScheme;
                rootNode.Nodes.Add(childNode1);
                TreeNode childNode2 = new TreeNode();
                childNode2.Text = tScheme.PointScheme.Name;
                childNode2.Tag = tScheme.PointScheme;
                rootNode.Nodes.Add(childNode2);
                schemeGroup.Schemes.Add(tScheme);
                parentNode.Nodes.Add(rootNode);
            }

        }
        PipeScheme tSchemeCopy = null;
        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode parentNode = this.treeCheckScheme.SelectedNode;
            if (parentNode.Tag is PipeScheme)
            {
                tSchemeCopy = TransExpV2<PipeScheme, PipeScheme>.Trans(parentNode.Tag as PipeScheme);

            }
            else
            {
                MessageBox.Show("只能复制图层模型");
            }
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode parentNode = this.treeCheckScheme.SelectedNode;
            if (parentNode.Tag is PipeSchemeGroup)
            {
                tSchemeCopy.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
                TreeNode rootNode = new TreeNode();
                rootNode.Text = tSchemeCopy.Name;
                rootNode.Tag = tSchemeCopy;
                tSchemeCopy.LineScheme.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
                tSchemeCopy.PointScheme.SchemePropertyValueChanged += OnSchemePropertyValueChanged;
                TreeNode childNode1 = new TreeNode();
                childNode1.Text = tSchemeCopy.LineScheme.Name;
                childNode1.Tag = tSchemeCopy.LineScheme;
                rootNode.Nodes.Add(childNode1);
                TreeNode childNode2 = new TreeNode();
                childNode2.Text = tSchemeCopy.PointScheme.Name;
                childNode2.Tag = tSchemeCopy.PointScheme;
                rootNode.Nodes.Add(childNode2);
                (parentNode.Tag as PipeSchemeGroup).Schemes.Add(tSchemeCopy);
                parentNode.Nodes.Add(rootNode);
            }
        }

        private void 删除模型方案ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否删除此图层组模型方案?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                TreeNode schemeGroupNode = this.treeCheckScheme.SelectedNode;
                PipeSchemeGroup schemeGroup = schemeGroupNode.Tag as PipeSchemeGroup;
                if (schemeGroup != null)
                {
                    PipeSchemeManager schemeManager = schemeGroupNode.Parent.Tag as PipeSchemeManager;
                    if (schemeManager != null)
                    {
                        schemeGroupNode.Remove();
                        schemeManager.SchemeGroups.Remove(schemeGroup);
                    }
                }
            }
        }
    }

    public static class TransExpV2<TIn, TOut>
    {

        private static readonly Func<TIn, TOut> cache = GetFunc();
        private static Func<TIn, TOut> GetFunc()
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TIn), "p");
            List<MemberBinding> memberBindingList = new List<MemberBinding>();

            foreach (var item in typeof(TOut).GetProperties())
            {
                if (!item.CanWrite) continue;
                MemberExpression property = Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name));
                MemberBinding memberBinding = Expression.Bind(item, property);
                memberBindingList.Add(memberBinding);
            }

            MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TOut)), memberBindingList.ToArray());
            Expression<Func<TIn, TOut>> lambda = Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, new ParameterExpression[] { parameterExpression });

            return lambda.Compile();
        }

        public static TOut Trans(TIn tIn)
        {
            return cache(tIn);
        }

    }
}
