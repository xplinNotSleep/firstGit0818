using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Exception = System.Exception;
using Path = System.IO.Path;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// 数据信息文件处理类
    /// </summary>
    public class DBInfoHandler
    {
        private TreeView m_Treeview;                                            //树形控件
        private PropertyGrid m_Propertygrid;                                    //属性控件
        private IList<TreeNode> m_CheckedTreeNodes = new List<TreeNode>();        //被选中的树节点
        private string m_SufficName = "";                                       //后缀名     

        /// <summary>
        /// 默认构造函数 
        /// </summary>
        /// <param name="ptreeview">树对象</param>
        /// <param name="propertygrid">属性框对象</param>
        public DBInfoHandler(TreeView ptreeview, PropertyGrid propertygrid)
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
        public bool SaveDBInfoToFile(string filepath)
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
                return true;
            }
            catch (Exception ex)
            {
                return false;
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
        private void GetCheckedTreeNode(TreeNode pTreeNode, EnumDataNodeItems pEnumDataNodeItem, ref IList<TreeNode> pCheckTreeNodes)
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
        /// <param name="pTreeNodes"></param>
        /// <returns></returns>
        private IFields GetFeatureClassFields(TreeNodeCollection pTreeNodes, out ISpatialReference spatialReference)
        {
            IFeatureClassDescription fcDescription = new FeatureClassDescriptionClass();
            IObjectClassDescription pObjectDescription = (IObjectClassDescription)fcDescription;
            IFields pFields = new FieldsClass();
            IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;

            IFields fields = pObjectDescription.RequiredFields;
            //IClone pClone = (IClone)fields;
            //IFields pFields = (IFields)pClone.Clone();
            //IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;
            pFieldsEdit.AddField(fields.Field[0]);
          
            esriGeometryType geometryType = esriGeometryType.esriGeometryPoint;
            spatialReference = null;
            foreach (TreeNode treeNode in pTreeNodes)
            {
                if (treeNode.Tag is ItemProperty)
                {
                    ItemProperty itemProperty = (ItemProperty)treeNode.Tag;
                    if (itemProperty.DataNodeItem == EnumDataNodeItems.GeometryFieldItem)
                    {
                        GeometryFieldItemProperty tGeoItemProperty = itemProperty as GeometryFieldItemProperty;
                        geometryType = (esriGeometryType)tGeoItemProperty.GeometryType;
                        spatialReference = tGeoItemProperty.GeoReference;
                    }
                    else if (itemProperty.DataNodeItem == EnumDataNodeItems.CustomFieldItem)
                    {
                        FieldItemProperty tFieldItemProperty = itemProperty as FieldItemProperty;
                        IField pField = new FieldClass();
                        IFieldEdit pFieldEdit = (IFieldEdit)pField;
                        pFieldEdit.Name_2 = itemProperty.ItemName;
                        pFieldEdit.AliasName_2 = itemProperty.ItemAliasName;
                        pFieldEdit.Type_2 = (esriFieldType)tFieldItemProperty.FieldType;
                        pFieldEdit.Length_2 = tFieldItemProperty.Length;
                        pFieldEdit.Precision_2 = tFieldItemProperty.Precision;
                        pFieldEdit.Scale_2 = tFieldItemProperty.Scale;
                        pFieldEdit.IsNullable_2 = tFieldItemProperty.IsNull;
                        pFieldEdit.DefaultValue_2 = tFieldItemProperty.DefaultValue;
                        if (tFieldItemProperty.DomainItemProperty != null)
                        {
                            pFieldEdit.Domain_2 = tFieldItemProperty.DomainItemProperty.Domain;
                        }
                        pFieldsEdit.AddField(pFieldEdit);
                    }
                }
            }
            pFieldsEdit.AddField(fields.Field[1]);
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                IField pField = pFields.Field[i];
                if (pField.Type == esriFieldType.esriFieldTypeGeometry)
                {
                    IGeometryDef pGeomDef = pField.GeometryDef;
                    IGeometryDefEdit pGeomDefEdit = pGeomDef as IGeometryDefEdit;
                    pGeomDefEdit.GeometryType_2 = geometryType;
                    IFieldEdit pFieldEdit = (IFieldEdit)pField;
                    pFieldEdit.GeometryDef_2 = pGeomDef;
                }
            }
            return pFieldsEdit;
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
                tFieldEdit.AliasName_2 = itemProperty.ItemAliasName;

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
                    tGeometryDef.GeometryType_2 = (esriGeometryType)tGeoItemProperty.GeometryType;
                    //设置空间参考
                    //tGeometryDef.SpatialReference_2 = tGeoItemProperty.GeoReference;

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
        private void CreateDatasetByDBInfo(TreeNode rootNode, IFeatureWorkspace pFeatureWorkspace, ITrackProgress pTrackProgress)
        {
            IList<TreeNode> tCheckTreeNodes = new List<TreeNode>();

            #region 获取所有选中的节点
            foreach (TreeNode treeNode in rootNode.Nodes)
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
        private void CreateCustomTableByDBInfo(TreeNode rootNode, IFeatureWorkspace pFeatureWorkspace, ITrackProgress pTrackProgress)
        {
            IList<TreeNode> tCheckTreeNodes = new List<TreeNode>();

            #region 获取所有选中的节点
            foreach (TreeNode treeNode in rootNode.Nodes)
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
        private void CreateFeatureClassByDBInfo(TreeNode rootNode, IFeatureWorkspace pFeatureWorkspace, ITrackProgress pTrackProgress)
        {
            IList<TreeNode> tCheckTreeNodes = new List<TreeNode>();

            #region 获取所有选中的节点
            foreach (TreeNode treeNode in rootNode.Nodes)
            {
                if (treeNode.Checked == true)
                {
                    //递归获取所有被选中的节点
                    GetCheckedTreeNode(treeNode, EnumDataNodeItems.FeatureClassItem, ref tCheckTreeNodes);
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
                IFeatureDataset tFeatDataset = null;
                if (tDSItemProperty != null)
                {
                    tFeatDataset = pFeatureWorkspace.OpenFeatureDataset(tDSItemProperty.ItemName + this.m_SufficName);
                }

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
                        if (tFeatDataset != null)
                        {
                            tFeatureClass = GeoDBHelper.CreateFeatureClass(tFeatDataset, strFeatClassName, (esriFeatureType)tFeatItemProperty.FeatureType, tFields, null, null, "");
                        }
                        else
                        {
                            tFeatureClass = GeoDBHelper.CreateFeatureClass(pFeatureWorkspace, strFeatClassName, (esriFeatureType)tFeatItemProperty.FeatureType, tFields, null, null, "");
                        }
                    }
                    else
                    {
                        //实例化对象类
                        IGraphicsLayerScale graphicLayerScale = new GraphicsLayerScaleClass();
                        //设置单位
                        if (tDSItemProperty != null && tDSItemProperty.GeoReference is IGeographicCoordinateSystem)
                            graphicLayerScale.Units = esriUnits.esriDecimalDegrees;
                        else
                            graphicLayerScale.Units = esriUnits.esriMeters;

                        //设置参考比例
                        graphicLayerScale.ReferenceScale = tFeatItemProperty.RefrenceScale;

                        //创建注记类
                        tFeatureClass = GeoDBHelper.CreateFeatureAnnoClass(pFeatureWorkspace, strFeatClassName, tFields, "", tFeatDataset, null, null, graphicLayerScale, null, true);
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
        private void CreateWorkspaceDomains(TreeNode rootNoe, IFeatureWorkspace pFeatureWorkspace, ITrackProgress pTrackProgress)
        {
            IList<TreeNode> tCheckTreeNodes = new List<TreeNode>();

            #region 获取所有选中的节点
            foreach (TreeNode treeNode in rootNoe.Nodes)
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
        private string qz = "数据库:";


        public void CreateDataBaseByDBInfo(string savePath, string strSufficName, bool isCover)
        {
            //目标文件夹已经存在的重名文件
            List<string> coverdList = new List<string>();
            if (!isCover)
            {
                //找出目标路径存在的重复文件，保存起来，等最后的时候弹出提示，这些数据库因为存在重名数据库所以创建失败
                foreach (TreeNode treeNode in m_Treeview.Nodes)
                {
                    string name = treeNode.Text.Replace(qz, "");
                    if (File.Exists(savePath + "\\" + name + ".mdb"))
                    {
                        coverdList.Add(name);
                    }
                }
            }
            else
            {
                //否则覆盖掉数据库，直接删除，如果删除失败提示错误
                List<string> errorList = new List<string>();
                foreach (TreeNode treeNode in m_Treeview.Nodes)
                {
                    string name = treeNode.Text.Replace(qz, "");
                    if (File.Exists(savePath + "\\" + name + ".mdb"))
                    {
                        try
                        {
                            File.Delete(savePath + "\\" + name + ".mdb");
                        }
                        catch (Exception e)
                        {
                            errorList.Add(name);
                        }
                    }
                }

                this.m_SufficName = strSufficName;

                ITrackProgress tTrackProgress = new TrackProgressDialog();
                tTrackProgress.DisplayTotal = true;
                tTrackProgress.TotalMax = 4;
                (tTrackProgress as Form).TopMost = true;
                tTrackProgress.Show();

                try
                {

                    //循环遍历创建数据库，在errorList里面的不再创建
                    foreach (TreeNode treeNode in m_Treeview.Nodes)
                    {
                        if (treeNode.Checked)
                        {
                            //查询IFeatureWorkspace引用
                            IWorkspace pWorkspace = CreateWorkspace(savePath + "\\" + treeNode.Text.Replace(qz, "") + ".mdb");
                            if (pWorkspace == null)
                            {
                                //如果创建失败记录下来
                                errorList.Add(treeNode.Text.Replace(qz, ""));
                            }
                            IFeatureWorkspace tFeatWorkspace = pWorkspace as IFeatureWorkspace;

                            if (tFeatWorkspace != null)
                            {
                                //创建属性域
                                CreateWorkspaceDomains(treeNode, tFeatWorkspace, tTrackProgress);
                                //创建数据集
                                CreateDatasetByDBInfo(treeNode, tFeatWorkspace, tTrackProgress);
                                //创建要素类
                                CreateFeatureClassByDBInfo(treeNode, tFeatWorkspace, tTrackProgress);
                                //创建属性表
                                CreateCustomTableByDBInfo(treeNode, tFeatWorkspace, tTrackProgress);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //设置该窗体不为顶层窗体
                    (tTrackProgress as Form).TopMost = false;
                    MessageBox.Show(ex.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    tTrackProgress.AutoFinishClose = true;
                    tTrackProgress.SetFinish();
                }
            }
            if (coverdList.Count > 0)
            {
                string temp = string.Join(",", coverdList);
                MessageBox.Show($"目标路径已经存在数据库[{temp}]", "数据库创建失败", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// 创建mdb数据库
        /// </summary>
        /// <returns></returns>
        private IWorkspace CreateWorkspace(string fileName)
        {
            try
            {
                IWorkspaceFactory factory = new AccessWorkspaceFactoryClass();
                IWorkspaceName workspaceName = factory.Create(Path.GetDirectoryName(fileName), Path.GetFileNameWithoutExtension(fileName), null, 0);
                IName pName = (IName)workspaceName;
                return (IWorkspace)pName.Open();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public void CreateDataBaseByDBInfo(string savePath, string strSufficName, bool isCover, bool isAppend,bool isDataSet)
        {
            //目标文件夹已经存在的重名文件
            List<string> coverdList = new List<string>();
            List<string> errorList = new List<string>();
            if (!isCover)
            {
                //找出目标路径存在的重复文件，保存起来，等最后的时候弹出提示，这些数据库因为存在重名数据库所以创建失败
                foreach (TreeNode treeNode in m_Treeview.Nodes)
                {
                    string name = treeNode.Text.Replace(qz, "");
                    if (File.Exists(savePath + "\\" + name + ".mdb"))
                    {
                        coverdList.Add(name);
                    }
                }
            }
            else
            {
                //否则覆盖掉数据库，直接删除，如果删除失败提示错误

                foreach (TreeNode treeNode in m_Treeview.Nodes)
                {
                    if (!treeNode.Checked) continue;
                    string name = treeNode.Text.Replace(qz, "");
                    if (File.Exists(savePath + "\\" + name + ".mdb"))
                    {
                        try
                        {
                            File.Delete(savePath + "\\" + name + ".mdb");
                        }
                        catch (Exception e)
                        {
                            errorList.Add(name);
                        }
                    }
                }


            }

            if (errorList.Count > 0)
            {
                MessageBox.Show("数据库覆盖失败！" + string.Join(",", errorList));
            }
            this.m_SufficName = strSufficName;
            ITrackProgress tTrackProgress = new TrackProgressDialog();
            tTrackProgress.DisplayTotal = true;
            tTrackProgress.TotalMax = 4;
            (tTrackProgress as Form).TopMost = true;
            tTrackProgress.Show();
            try
            {

                //循环遍历创建数据库，在errorList里面的不再创建
                foreach (TreeNode treeNode in m_Treeview.Nodes)
                {
                    if (treeNode.Checked)
                    {
                        //查询IFeatureWorkspace引用
                        IWorkspace pWorkspace = CreateWorkspace(savePath + "\\" + treeNode.Text.Replace(qz, "") + ".mdb");
                        if (pWorkspace == null)
                        {
                            //如果创建失败记录下来
                            errorList.Add(treeNode.Text.Replace(qz, ""));
                            continue;
                        }
                        IFeatureWorkspace tFeatWorkspace = pWorkspace as IFeatureWorkspace;

                        if (tFeatWorkspace != null)
                        {
                            //创建属性域
                            CreateWorkspaceDomains(treeNode, tFeatWorkspace, tTrackProgress);
                            //创建数据集
                            if(isDataSet)
                            {
                                CreateDatasetByDBInfo(treeNode, tFeatWorkspace, tTrackProgress);
                            }

                            //创建要素类
                            CreateFeatureClassByDBInfo(treeNode, tFeatWorkspace, tTrackProgress, isAppend, isDataSet);
                            //创建属性表
                            CreateCustomTableByDBInfo(treeNode, tFeatWorkspace, tTrackProgress);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //设置该窗体不为顶层窗体
                (tTrackProgress as Form).TopMost = false;
                MessageBox.Show(ex.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                tTrackProgress.AutoFinishClose = true;
                tTrackProgress.SetFinish();
            }
            if (coverdList.Count > 0)
            {
                string temp = string.Join(",", coverdList);
                MessageBox.Show($"目标路径已经存在数据库[{temp}]，跳过创建！", "数据库创建", MessageBoxButtons.OK);
            }
        }

        private void CreateFeatureClassByDBInfo(TreeNode rootNode, IFeatureWorkspace pFeatureWorkspace, ITrackProgress pTrackProgress, bool isAppend,bool isDataSet)
        {
            IList<TreeNode> tCheckTreeNodes = new List<TreeNode>();

            #region 获取所有选中的节点
            foreach (TreeNode treeNode in rootNode.Nodes)
            {
                if (treeNode.Checked == true)
                {
                    //递归获取所有被选中的节点
                    GetCheckedTreeNode(treeNode, EnumDataNodeItems.FeatureClassItem, ref tCheckTreeNodes);
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
                if (tFeatItemProperty.ItemAliasName == "新建要素类")
                    Console.WriteLine("");
                //获取字段集合
                ISpatialReference spatialReference = null;
                IFields tFields = GetFeatureClassFields(treeNode.Nodes,out spatialReference);
                InitFieldDomian(tFields, isAppend);
                DataSetItemProperty tDSItemProperty = treeNode.Parent.Tag as DataSetItemProperty;
                IFeatureDataset tFeatDataset = null;
                if (isDataSet)
                {
                    if (tDSItemProperty != null)
                    {
                        tFeatDataset = pFeatureWorkspace.OpenFeatureDataset(tDSItemProperty.ItemName + this.m_SufficName);
                    }
                }
                    

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
                        if (tFeatDataset != null)
                        {
                            tFeatureClass = GeoDBHelper.CreateFeatureClass(tFeatDataset, strFeatClassName, (esriFeatureType)tFeatItemProperty.FeatureType, tFields, null, null, "");
                        }
                        else
                        {
                            tFeatureClass = GeoDBHelper.CreateFeatureClass(pFeatureWorkspace, strFeatClassName, (esriFeatureType)tFeatItemProperty.FeatureType, tFields, null, null, "", spatialReference);
                        }
                    }
                    else
                    {
                        //实例化对象类
                        IGraphicsLayerScale graphicLayerScale = new GraphicsLayerScaleClass();
                        //设置单位
                        if (tDSItemProperty != null && tDSItemProperty.GeoReference is IGeographicCoordinateSystem)
                            graphicLayerScale.Units = esriUnits.esriDecimalDegrees;
                        else
                            graphicLayerScale.Units = esriUnits.esriMeters;

                        //设置参考比例
                        graphicLayerScale.ReferenceScale = tFeatItemProperty.RefrenceScale;

                        //创建注记类
                        tFeatureClass = GeoDBHelper.CreateFeatureAnnoClass(pFeatureWorkspace, strFeatClassName, tFields, "", tFeatDataset, null, null, graphicLayerScale, null, true);
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
        /// 初始化字段的属性域
        /// </summary>
        /// <param name="pFields"></param>
        /// <param name="isAppend"></param>
        private void InitFieldDomian(IFields pFields, bool isAppend)
        {
            if (!isAppend)
            {
                //取消所有字段的属性域
                int count = pFields.FieldCount;
                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        IField pField = pFields.Field[i];
                        IFieldEdit pFieldEdit = (IFieldEdit)pField;
                        pFieldEdit.Domain_2 = null;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }
    }
}
