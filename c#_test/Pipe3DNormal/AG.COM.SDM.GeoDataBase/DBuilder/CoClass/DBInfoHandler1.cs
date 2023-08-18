using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// 数据信息文件处理类
    /// </summary>
    public class DBInfoHandler1
    {
        private TreeView m_Treeview;                                            //树形控件
        private PropertyGrid m_Propertygrid;                                    //属性控件
        private IList<TreeNode> m_CheckedTreeNodes=new List<TreeNode>();        //被选中的树节点
        private string m_SufficName = "";                                       //后缀名     

        /// <summary>
        /// 默认构造函数 
        /// </summary>
        /// <param name="ptreeview">树对象</param>
        /// <param name="propertygrid">属性框对象</param>
        public DBInfoHandler1(TreeView ptreeview, PropertyGrid propertygrid)
        {
            this.m_Treeview = ptreeview;
            this.m_Propertygrid = propertygrid;
        }

        /// <summary>
        /// 获取属性结点类型的描述
        /// </summary>
        /// <param name="dataNodeItem">属性结点类型<see cref="EnumDataNodeItems"/></param>
        /// <returns>返回描述信息</returns>
        public static string GetDataNodeDescripble(EnumDataNodeItems dataNodeItem)
        {
            switch (dataNodeItem)
            {
                case EnumDataNodeItems.DataBaseItem:
                    return "数据库";
                case EnumDataNodeItems.SubjectChildItem:
                    return "专题子库";
                case EnumDataNodeItems.DataSetItem:
                    return "要素集";
                case EnumDataNodeItems.FeatureClassItem:
                    return "要素类";
                case EnumDataNodeItems.CustomTableItem:
                    return "属性表";
                case EnumDataNodeItems.GeometryFieldItem:
                    return "几何字段";
                case EnumDataNodeItems.SubjectDomainItem:
                    return "专题属性域";
                case EnumDataNodeItems.DomainItem:
                    return "属性域";
                default:
                    return "字段";
            }
        }

        /// <summary>
        /// 根据数据结点类型创建结点
        /// </summary>
        /// <param name="dataNodeItem">数据结点类型</param>
        /// <returns>返回TreeNode</returns>
        public TreeNode CreateTreeNode(EnumDataNodeItems dataNodeItem)
        {
            //初始化属性创建工厂类实例
            ItemPropertyFactory itemPropertyFactory = new ItemPropertyFactory();
            //创建属性项
            ItemProperty itemProperty = itemPropertyFactory.Create(dataNodeItem);
            //添加属性项改变事件
            itemProperty.ItemPropertyValueChanged += new ItemPropertyValueChangedEventHandler(ItemPropertyValueChanged);
            //实例化树节点
            TreeNode treeNode = new TreeNode();
            treeNode.Text = string.Format("{0}:{1}({2})", GetDataNodeDescripble(dataNodeItem), itemProperty.ItemName, itemProperty.ItemAliasName);
            treeNode.Tag = itemProperty;
            return treeNode;
        }

        /// <summary>
        /// 保存数据配置方案到文件
        /// </summary>
        /// <param name="filepath">文件路径</param>
        public void SaveDBInfoToFile(ref string filepath)
        {
            try
            {
                if (filepath.Trim().Length == 0)
                {
                    //另存为数据配置方案到文件
                    SaveAsDBInfoToFile(ref filepath);
                }
                else
                {
                    if (System.IO.File.Exists(filepath) == true)                    
                        System.IO.File.Delete(filepath);
                    
                    //实例化 数据配置信息读写类 对象
                    DbInfoReadWrite dbInfoConfig = new DbInfoReadWrite(this.m_Treeview, this.m_Propertygrid);
                    //写入数据库配置信息到文件
                    dbInfoConfig.WriteDBInfoToFile(filepath);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 另存为数据配置方案到文件
        /// </summary>
        /// <param name="filepath">文件路径</param>
        public void SaveAsDBInfoToFile(ref string filepath)
        {
            try
            {
                SaveFileDialog tSaveFileDlg = new SaveFileDialog();
                tSaveFileDlg.Filter = "数据库标准文件(*.dml)|*.dml";
                tSaveFileDlg.Title = "指定保存路径";

                if (tSaveFileDlg.ShowDialog() == DialogResult.OK)
                {
                    filepath = tSaveFileDlg.FileName;
                    //实例化 数据配置信息读写类 对象
                    DbInfoReadWrite dbInfoConfig = new DbInfoReadWrite(this.m_Treeview, this.m_Propertygrid);
                    //写入数据库配置信息到文件
                    dbInfoConfig.WriteDBInfoToFile(filepath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 根据数据配置信息创建数据表
        /// </summary>
        /// <param name="pSufficName">后缀名</param>
        /// <param name="pWorkspace">工作空间</param>
        public void CreateDataBaseByDBInfo(string pSufficName,IWorkspace pWorkspace)
        {                 
            if (pWorkspace == null) return;

            this.m_SufficName = pSufficName;

            ITrackProgress tTrackProgress = new TrackProgressDialog();           
            tTrackProgress.DisplayTotal = true;
            tTrackProgress.TotalMax = 4;
            (tTrackProgress as Form).TopMost = true;
            tTrackProgress.Show();

            try
            {
                //查询IFeatureWorkspace引用
                IFeatureWorkspace tFeatWorkspace = pWorkspace as IFeatureWorkspace;

                if (tFeatWorkspace != null)
                {
                    //创建属性域
                    CreateWorkspaceDomains(tFeatWorkspace, tTrackProgress);
                    //创建数据集
                    CreateDatasetByDBInfo(tFeatWorkspace, tTrackProgress);
                    //创建要素类
                    CreateFeatureClassByDBInfo(tFeatWorkspace, tTrackProgress);
                    //创建属性表
                    CreateCustomTableByDBInfo(tFeatWorkspace, tTrackProgress);
                }
            }
            catch (Exception ex)
            {
                //设置该窗体不为顶层窗体
                //(tTrackProgress as Form).TopMost = false;
                MessageBox.Show(ex.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                tTrackProgress.AutoFinishClose = true;
                tTrackProgress.SetFinish();
            }
        }

        /// <summary>
        /// ItemProperty将调用此方法来通知DBTreeControl对象其属性值已发生改变
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">ItemPropertyEventArgs事件参数</param>
        private void ItemPropertyValueChanged(object sender, ItemPropertyEventArgs e)
        {
            ItemProperty itemProperty = e.ItemProperty;
            TreeNode selTreeNode = this.m_Treeview.SelectedNode;
            selTreeNode.Text = string.Format("{0}:{1}({2})", GetDataNodeDescripble(itemProperty.DataNodeItem), itemProperty.ItemName, itemProperty.ItemAliasName);

            #region  设置结点的图片索引值
            GeometryFieldItemProperty geoFieldItemProperty = itemProperty as GeometryFieldItemProperty;
            if (geoFieldItemProperty != null)
            {
                if (geoFieldItemProperty.GeometryType == EnumGeometryItems.点 || geoFieldItemProperty.GeometryType == EnumGeometryItems.多点)
                {
                    selTreeNode.Parent.ImageIndex = 3;
                    selTreeNode.Parent.SelectedImageIndex = 3;
                    
                }
                else if (geoFieldItemProperty.GeometryType == EnumGeometryItems.线)
                {
                    selTreeNode.Parent.ImageIndex = 4;
                    selTreeNode.Parent.SelectedImageIndex = 4;
                }
                else
                {
                    selTreeNode.Parent.ImageIndex = 5;
                    selTreeNode.Parent.SelectedImageIndex = 5;
                }
            }
            #endregion

            //刷新属性视图
            this.m_Propertygrid.Refresh();
        }

        /// <summary>
        /// 递归获取所有被选中的节点
        /// </summary>
        /// <param name="pTreeNode">当前树节点</param>
        /// <param name="pEnumDataNodeItem">节点类型</param>
        /// <param name="pCheckTreeNodes">引用 选中节点集合</param>
        private void GetCheckedTreeNode(TreeNode pTreeNode,EnumDataNodeItems pEnumDataNodeItem,ref IList<TreeNode> pCheckTreeNodes)
        {
            foreach (TreeNode treeNode in pTreeNode.Nodes)
            {
                if (treeNode.Checked == true)
                {
                    ItemProperty itemProperty = treeNode.Tag as ItemProperty;
                    if (itemProperty.DataNodeItem == pEnumDataNodeItem)
                    {
                        pCheckTreeNodes.Add(treeNode);
                    }
                }

                //递归获取所有被选中的节点
                GetCheckedTreeNode(treeNode, pEnumDataNodeItem, ref pCheckTreeNodes);
            }
        }

        /// <summary>
        /// 得到字段集合
        /// </summary>
        /// <param name="pTreeNodes">字段树节点集合</param>
        /// <returns>返回IFields</returns>
        private IFields GetFields(TreeNodeCollection pTreeNodes)
        {
            IFieldsEdit tFieldsEdit = new FieldsClass();

            foreach (TreeNode treeNode in pTreeNodes)
            {
                ItemProperty itemProperty = treeNode.Tag as ItemProperty;
                IFieldEdit tFieldEdit = new FieldClass();
                tFieldEdit.Name_2 = itemProperty.ItemName;
                tFieldEdit.AliasName_2  = itemProperty.ItemAliasName;

                if (itemProperty.DataNodeItem == EnumDataNodeItems.ObjectFieldItem)
                {
                    tFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
                }
                else if (itemProperty.DataNodeItem == EnumDataNodeItems.GeometryFieldItem)
                {
                    GeometryFieldItemProperty tGeoItemProperty = itemProperty as GeometryFieldItemProperty;           
                    //实例化空间对象字段
                    IGeometryDefEdit tGeometryDef = new GeometryDefClass();   
                    //设置空间对象类型
                    tGeometryDef.GeometryType_2 =(esriGeometryType)tGeoItemProperty.GeometryType;
                    //设置空间参考
                    tGeometryDef.SpatialReference_2 = tGeoItemProperty.GeoReference;

                    //设置字段类型
                    tFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                    //设置空间对象字段
                    tFieldEdit.GeometryDef_2 = tGeometryDef as IGeometryDef;
                }
                else if (itemProperty.DataNodeItem == EnumDataNodeItems.CustomFieldItem)
                {
                    FieldItemProperty tFieldItemProperty = itemProperty as FieldItemProperty;
                    tFieldEdit.Type_2 = (esriFieldType)tFieldItemProperty.FieldType;
                    tFieldEdit.Length_2 = tFieldItemProperty.Length;
                    tFieldEdit.Precision_2 = tFieldItemProperty.Precision;
                    tFieldEdit.Scale_2 = tFieldItemProperty.Scale;
                    tFieldEdit.IsNullable_2 = tFieldItemProperty.IsNull;
                    tFieldEdit.DefaultValue_2 = tFieldItemProperty.DefaultValue;
                    if (tFieldItemProperty.DomainItemProperty != null)
                    {
                        tFieldEdit.Domain_2 = tFieldItemProperty.DomainItemProperty.Domain;
                        tFieldEdit.DomainFixed_2 = true;
                    }                    
                }

                tFieldsEdit.AddField(tFieldEdit as IField);
            }

            return tFieldsEdit as IFields;
        }

        /// <summary>
        /// 根据配置信息在指定的工作空间中创建要素集
        /// </summary>
        /// <param name="pFeatureWorkspace">GeoDataBase工作空间</param>
        /// <param name="pTrackProgress">进度条对话框</param>
        private void CreateDatasetByDBInfo(IFeatureWorkspace pFeatureWorkspace, ITrackProgress pTrackProgress)
        {
            IList<TreeNode> tCheckTreeNodes = new List<TreeNode>();

            #region 获取所有选中的节点
            foreach (TreeNode treeNode in this.m_Treeview.Nodes)
            {
                if (treeNode.Checked == true)
                {
                    //递归获取所有被选中的节点
                    GetCheckedTreeNode(treeNode, EnumDataNodeItems.DataSetItem, ref tCheckTreeNodes);
                }
            }
            #endregion

            //设置父进度条状态
            pTrackProgress.TotalValue += 1;
            pTrackProgress.TotalMessage = "正在创建要素集……";
            pTrackProgress.SubMax = tCheckTreeNodes.Count;

            int i = 0;  //临时变量

            #region 循环创建要素集
            foreach (TreeNode treeNode in tCheckTreeNodes)
            {
                //类型转换
                DataSetItemProperty tDSItemProperty = treeNode.Tag as DataSetItemProperty;

                //设置子进度条状态
                pTrackProgress.SubValue = (i++);
                pTrackProgress.SubMessage = string.Format("正在创建[{0}]要素集……", tDSItemProperty.ItemAliasName);
                Application.DoEvents();

                //数据集名称
                string strDatasetName = string.Format("{0}{1}", tDSItemProperty.ItemName, this.m_SufficName);

                //判断数据集是否存在
                bool IsExist = (pFeatureWorkspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureDataset, strDatasetName);

                #region 已存在要素集的情况 注释掉
                //if (IsExist == true)
                //{
                //    DialogResult tDlgResult = MessageBox.Show(string.Format("[{0}] 要素集已经存在,是否确认删除重建？", itemProperty.ItemName), "提示",
                //                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                //    //确认删除后再重建
                //    if (tDlgResult == DialogResult.Yes)
                //    {         
                //        //删除已经存在的要素集
                //        GeoDBHelper.DeleteDatasetByName(tDSItemProperty.ItemName, pFeatureWorkspace, esriDatasetType.esriDTFeatureDataset);
                //    }
                //}
                #endregion

                if (IsExist == false)
                    //如果要素集不存在则创建          
                    GeoDBHelper.CreateFeatureDataset(pFeatureWorkspace, strDatasetName, tDSItemProperty.GeoReference);
            }
            #endregion
        }

        /// <summary>
        /// 根据配置信息在指定的工作空间中创建属性表
        /// </summary>
        /// <param name="pFeatureWorkspace">GeoDataBase工作空间</param>
        /// <param name="pTrackProgress">进度条对话框</param>
        private void CreateCustomTableByDBInfo(IFeatureWorkspace pFeatureWorkspace, ITrackProgress pTrackProgress)
        {
            IList<TreeNode> tCheckTreeNodes = new List<TreeNode>();

            #region 获取所有选中的节点
            foreach (TreeNode treeNode in this.m_Treeview.Nodes)
            {
                if (treeNode.Checked == true)
                {
                    //递归获取所有被选中的节点
                    GetCheckedTreeNode(treeNode, EnumDataNodeItems.CustomTableItem, ref tCheckTreeNodes);
                }
            }
            #endregion

            //设置父进度条状态
            pTrackProgress.TotalValue += 1;
            pTrackProgress.TotalMessage = "正在创建属性表……";
            pTrackProgress.SubMax = tCheckTreeNodes.Count;

            int i = 0;  //临时变量

            #region 循环创建属性表
            foreach (TreeNode treeNode in tCheckTreeNodes)
            {
                //属性表项
                ItemProperty itemProperty = treeNode.Tag as ItemProperty;
                
                //设置子进度条状态
                pTrackProgress.SubValue = (i++);
                pTrackProgress.SubMessage = string.Format("正在创建[{0}]属性表……", itemProperty.ItemAliasName);
                Application.DoEvents();

                //获取字段集合
                IFields tFields = GetFields(treeNode.Nodes);

                string strCustomTableName = string.Format("{0}{1}", itemProperty.ItemName, this.m_SufficName);
                //判断要素类是否已经存在
                bool IsExist = (pFeatureWorkspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTTable, strCustomTableName);

                #region 已存在属性表的情况 注释掉
                //if (IsExist == true)
                //{
                //    DialogResult tDlgResult=MessageBox.Show(string.Format("[{0}] 属性表已经存在,是否确认删除重建？", itemProperty.ItemName), "提示", 
                //        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);                    
                //    //确认删除后再重建
                //    if (tDlgResult == DialogResult.Yes)
                //    {
                //        //删除已经存在的属性表
                //        GeoDBHelper.DeleteDatasetByName(itemProperty.ItemName, pFeatureWorkspace, esriDatasetType.esriDTTable);
                //    }                      
                //}
                #endregion

                if (IsExist == false)
                {
                    //创建属性表
                    GeoDBHelper.CreateTable(pFeatureWorkspace, strCustomTableName, tFields, null, null, "");
                }
            }
            #endregion
        }

        /// <summary>
        /// 根据配置信息在指定的工作空间中创建要素类
        /// </summary>
        /// <param name="pFeatureWorkspace">GeoDataBase工作空间</param>
        /// <param name="pTrackProgress">进度条对话框</param>
        private void CreateFeatureClassByDBInfo(IFeatureWorkspace pFeatureWorkspace, ITrackProgress pTrackProgress)
        {
            IList<TreeNode> tCheckTreeNodes = new List<TreeNode>();

            #region 获取所有选中的节点
            foreach (TreeNode treeNode in this.m_Treeview.Nodes)
            {
                if (treeNode.Checked == true)
                {
                    //递归获取所有被选中的节点
                    GetCheckedTreeNode(treeNode,EnumDataNodeItems.FeatureClassItem, ref tCheckTreeNodes);
                }
            }
            #endregion
           
            //设置父进度条状态
            pTrackProgress.TotalValue += 1;
            pTrackProgress.TotalMessage = "正在创建要素类……";
            pTrackProgress.SubMax = tCheckTreeNodes.Count;

            int i = 0;  //临时变量

            #region 循环创建要素类
            foreach (TreeNode treeNode in tCheckTreeNodes)
            {
                //要素类属性项
                FeatureClassItemProperty tFeatItemProperty = treeNode.Tag as FeatureClassItemProperty;

                //设置子进度条状态
                pTrackProgress.SubValue = (i++);
                pTrackProgress.SubMessage = string.Format("正在创建[{0}]要素类……", tFeatItemProperty.ItemAliasName);
                Application.DoEvents();

                //获取字段集合
                IFields tFields = GetFields(treeNode.Nodes);

                DataSetItemProperty tDSItemProperty = treeNode.Parent.Tag as DataSetItemProperty;
                IFeatureDataset tFeatDataset = pFeatureWorkspace.OpenFeatureDataset(tDSItemProperty.ItemName+this.m_SufficName);

                string strFeatClassName = string.Format("{0}{1}", tFeatItemProperty.ItemName, this.m_SufficName);

                //判断要素类是否已经存在
                bool IsExist = (pFeatureWorkspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureClass, strFeatClassName);

                if (IsExist == false)
                {
                    //创建要素类
                    IFeatureClass tFeatureClass = null;
                    if (tFeatItemProperty.FeatureType != EnumFeatureType.注记要素类)
                    {
                        //创建要素类
                        tFeatureClass = GeoDBHelper.CreateFeatureClass(tFeatDataset, strFeatClassName, (esriFeatureType)tFeatItemProperty.FeatureType, tFields, null, null, "");
                    }
                    else
                    {
                        //实例化对象类
                        IGraphicsLayerScale graphicLayerScale = new GraphicsLayerScaleClass();
                        //设置单位
                        if (tDSItemProperty.GeoReference is IGeographicCoordinateSystem)
                            graphicLayerScale.Units = esriUnits.esriDecimalDegrees;
                        else
                            graphicLayerScale.Units = esriUnits.esriMeters;

                        //设置参考比例
                        graphicLayerScale.ReferenceScale = tFeatItemProperty.RefrenceScale;

                        //创建注记类
                        tFeatureClass = GeoDBHelper.CreateFeatureAnnoClass(pFeatureWorkspace, strFeatClassName, tFields, "", tFeatDataset, null, null,graphicLayerScale, null, true);
                    }

                    //判断要素类与别名是否为空
                    if (tFeatureClass != null && tFeatItemProperty.ItemAliasName.Trim().Length > 0)
                    {
                        //查询IClassSchemaEdit接口
                        IClassSchemaEdit tClassSchemaEdit = tFeatureClass as IClassSchemaEdit;
                        //修改别名
                        tClassSchemaEdit.AlterAliasName(tFeatItemProperty.ItemAliasName);
                    }
                }                
            }
            #endregion
        }

        /// <summary>
        /// 在指定的SDE工作空间中创建属性域
        /// </summary>
        /// <param name="pFeatureWorkspace">指定的SDE工作空间</param>
        /// <param name="pTrackProgress">进度条对话框</param>
        private void CreateWorkspaceDomains(IFeatureWorkspace pFeatureWorkspace,ITrackProgress pTrackProgress)
        {
            IList<TreeNode> tCheckTreeNodes = new List<TreeNode>();

            #region 获取所有选中的节点
            foreach (TreeNode treeNode in this.m_Treeview.Nodes)
            {
                if (treeNode.Checked == true)
                {
                    //递归获取所有被选中的节点
                    GetCheckedTreeNode(treeNode, EnumDataNodeItems.DomainItem, ref tCheckTreeNodes);
                }
            }
            #endregion

            //设置父进度条状态
            pTrackProgress.TotalValue += 1;
            pTrackProgress.TotalMessage = "正在创建属性域……";
            pTrackProgress.SubMax = tCheckTreeNodes.Count; 

            int i = 0;  //临时变量

            #region 循环创建要素类
            foreach (TreeNode treeNode in tCheckTreeNodes)
            {              
                //类型转换
                DomainItemProperty itemProperty = treeNode.Tag as DomainItemProperty;
                //查询该接口的引用
                IWorkspaceDomains tWorkspaceDomains = pFeatureWorkspace as IWorkspaceDomains;
                //根据属性域名获取属性域
                IDomain tDomain = tWorkspaceDomains.get_DomainByName(itemProperty.ItemName);

                //设置子进度条状态
                pTrackProgress.SubValue = (i++);
                pTrackProgress.SubMessage = string.Format("正在创建[{0}]属性域……", itemProperty.ItemAliasName);
                Application.DoEvents();

                #region 已存在属性域的情况 注释掉
                ////如果该属性域存在
                //if (tDomain != null)
                //{
                //    //获取对话结果状态
                //    DialogResult tDlgResult=MessageBox.Show(string.Format("{0} 域已经存在,是否删除重建？", itemProperty.ItemName), "提示！", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                //    //如果确认删除重建并且其有这样的权限，则删除该属性域
                //    if (tDlgResult == DialogResult.Yes && tWorkspaceDomains.get_CanDeleteDomain(itemProperty.ItemName))
                //    {
                //        tWorkspaceDomains.DeleteDomain(itemProperty.ItemName);
                //    }
                //}
                #endregion

                if (tDomain == null)
                {
                    //添加属性域
                    tWorkspaceDomains.AddDomain(itemProperty.Domain);
                }
            }
            #endregion
        }   
    }
}
