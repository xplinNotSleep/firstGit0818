using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// 数据配置信息读写类
    /// </summary>
    public class DbInfoReadWrite1
    {
        private TreeView m_dbTreeView;
        private PropertyGrid m_propertyGrid;
        private EnumDbInfoStates m_treeviewState;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="pTreeView">树对象</param>
        /// <param name="propertyGrid">属性控件</param>
        public DbInfoReadWrite1(TreeView pTreeView,PropertyGrid propertyGrid)
        {
            this.m_dbTreeView = pTreeView;
            this.m_propertyGrid = propertyGrid;
        }

        /// <summary>
        /// 根据指定的字段类型,得到代码值
        /// </summary>
        /// <param name="pFieldType">字段类型中文名</param>
        /// <param name="pCellValue">代码值对象</param>
        /// <returns>返回正确的代码值</returns>
        public static object GetCodedValue(esriFieldType pFieldType, object pCellValue)
        {
            try
            {
                switch (pFieldType)
                {
                    case esriFieldType.esriFieldTypeDate:
                        return Convert.ToDateTime(pCellValue);
                    case esriFieldType.esriFieldTypeDouble:
                        return Convert.ToDouble(pCellValue);
                    case esriFieldType.esriFieldTypeSingle:
                        return Convert.ToSingle(pCellValue);
                    case esriFieldType.esriFieldTypeSmallInteger:
                        return Convert.ToByte(pCellValue);
                    case esriFieldType.esriFieldTypeInteger:
                        return Convert.ToInt32(pCellValue);
                    default:
                        return Convert.ToString(pCellValue);
                }
            }
            catch
            {
                throw (new Exception(string.Format("{0} 值与字段类型不符,请重新输入！", pCellValue)));
            }
        }  

        /// <summary>
        /// 树视图状态
        /// </summary>
        public EnumDbInfoStates TreeViewState
        {
            get
            {
                return m_treeviewState;
            }
            set
            {
                this.m_treeviewState = value;
            }
        }

        /// <summary>
        /// 写入数据库配置信息到数据文件
        /// </summary>
        /// <param name="filePath">文件路径</param>  
        public void WriteDBInfoToFile(string filePath)
        {
            //初始化文件流
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                XmlTextWriter tXmlOut = new XmlTextWriter(fs, Encoding.Unicode);
                tXmlOut.Formatting = Formatting.Indented;

                //书写XML声明
                tXmlOut.WriteStartDocument();

                //书写配置文件注释信息
                tXmlOut.WriteComment("标题:数据库标准文件！");
                tXmlOut.WriteComment("版本:");
                tXmlOut.WriteComment("作者:Echo-AG.COM.SDM");

                //开始
                tXmlOut.WriteStartElement("DataBaseConfig");

                for (int i = 0; i < m_dbTreeView.Nodes.Count; i++)
                {
                    //开始
                    tXmlOut.WriteStartElement("数据库");
                    //写入属性信息
                    WriteAttributeString(tXmlOut, m_dbTreeView.Nodes[i]);
                    //递归写入子节点信息
                    WriteChildNode(tXmlOut, m_dbTreeView.Nodes[i]);
                    //结束
                    tXmlOut.WriteFullEndElement();

                }

                //终止一个元素
                tXmlOut.WriteEndElement();
                //关闭任何打开的元素
                tXmlOut.WriteEndDocument();
                //关闭
                tXmlOut.Close();
            }
        }

        /// <summary>
        /// 递归写入子节点信息
        /// </summary>
        /// <param name="pXmlOut">XmlTextWriter对象</param>
        /// <param name="pTreeNode">树结点</param>
        private void WriteChildNode(XmlTextWriter pXmlOut, TreeNode pTreeNode)
        {
            for (int i = 0; i < pTreeNode.Nodes.Count; i++)
            {
                //转换为ItemProperty类型
                ItemProperty itemProperty = pTreeNode.Nodes[i].Tag as ItemProperty;
                //开始
                pXmlOut.WriteStartElement(DBInfoHandler.GetDataNodeDescripble(itemProperty.DataNodeItem));
                //写入属性信息
                WriteAttributeString(pXmlOut, pTreeNode.Nodes[i]);
                //递归写入子节点信息
                WriteChildNode(pXmlOut, pTreeNode.Nodes[i]);
                //结束
                pXmlOut.WriteFullEndElement();
            }
        }

        /// <summary>
        /// 写入属性信息
        /// </summary>
        /// <param name="pXmlOut">XmlTextWriter对象</param>
        /// <param name="pTreeNode">树结点</param>
        private void WriteAttributeString(XmlTextWriter pXmlOut, TreeNode pTreeNode)
        {
            ItemProperty itemProperty = pTreeNode.Tag as ItemProperty;
            pXmlOut.WriteAttributeString("ItemName", itemProperty.ItemName);
            pXmlOut.WriteAttributeString("ItemAliasName", itemProperty.ItemAliasName);
            pXmlOut.WriteAttributeString("DataNodeItem", Convert.ToString((int)itemProperty.DataNodeItem));

            switch (itemProperty.DataNodeItem)
            {
                case EnumDataNodeItems.DataBaseItem:
                    #region 写数据库属性
                    DataBaseItemProperty dbItemProperty = itemProperty as DataBaseItemProperty;
                    pXmlOut.WriteAttributeString("DBVersion", dbItemProperty.DBVersion);
                    pXmlOut.WriteAttributeString("DBOwner", dbItemProperty.DBOwner);
                    break;
                    #endregion
                case EnumDataNodeItems.DataSetItem:
                    #region 写要素集属性
                    DataSetItemProperty dsItemProperty = itemProperty as DataSetItemProperty;
                    pXmlOut.WriteAttributeString("CoordinateSystem", CoordinateSystemToString(dsItemProperty.GeoReference));
                    pXmlOut.WriteAttributeString("XYDomain", XYDomainToString(dsItemProperty.GeoReference));
                    pXmlOut.WriteAttributeString("ZDomain", ZDomainToString(dsItemProperty.GeoReference));
                    pXmlOut.WriteAttributeString("MDomain", MDomainToString(dsItemProperty.GeoReference));
                    break;
                    #endregion
                case EnumDataNodeItems.GeometryFieldItem:
                    #region 写几何字段属性
                    GeometryFieldItemProperty geoFieldItem = itemProperty as GeometryFieldItemProperty;
                    pXmlOut.WriteAttributeString("AveragePoint", geoFieldItem.AveragePoint.ToString());
                    pXmlOut.WriteAttributeString("FieldType", Convert.ToString((int)geoFieldItem.FieldType));
                    pXmlOut.WriteAttributeString("GeometryType", Convert.ToString((int)geoFieldItem.GeometryType));
                    pXmlOut.WriteAttributeString("GeoReference", geoFieldItem.GeoReference.Name);
                    pXmlOut.WriteAttributeString("Grid1", geoFieldItem.Grid1.ToString());
                    pXmlOut.WriteAttributeString("Grid2", geoFieldItem.Grid2.ToString());
                    pXmlOut.WriteAttributeString("Grid3", geoFieldItem.Grid3.ToString());
                    pXmlOut.WriteAttributeString("IsContainM", geoFieldItem.IsContainM.ToString());
                    pXmlOut.WriteAttributeString("IsContainZ", geoFieldItem.IsContainZ.ToString());
                    pXmlOut.WriteAttributeString("IsNull", geoFieldItem.IsNull.ToString());
                    break;
                    #endregion
                case EnumDataNodeItems.CustomFieldItem:
                    #region 写通用字段属性
                    FieldItemProperty fieldItem = itemProperty as FieldItemProperty;
                    pXmlOut.WriteAttributeString("DefaultValue", fieldItem.DefaultValue);
                    pXmlOut.WriteAttributeString("FieldType", Convert.ToString((int)fieldItem.FieldType));
                    pXmlOut.WriteAttributeString("IsNull", fieldItem.IsNull.ToString());
                    pXmlOut.WriteAttributeString("Length", fieldItem.Length.ToString());
                    pXmlOut.WriteAttributeString("Precision", fieldItem.Precision.ToString());
                    pXmlOut.WriteAttributeString("Scale", fieldItem.Scale.ToString());
                    pXmlOut.WriteAttributeString("Domain", (fieldItem.DomainItemProperty == null) ? "" : fieldItem.DomainItemProperty.ItemName);
                    break;
                    #endregion
                case EnumDataNodeItems.FeatureClassItem:
                    #region 写要素类属性
                    FeatureClassItemProperty featClassItem = itemProperty as FeatureClassItemProperty;
                    pXmlOut.WriteAttributeString("FeatureType", Convert.ToString((int)featClassItem.FeatureType));
                    pXmlOut.WriteAttributeString("RefrenceScale", Convert.ToString(featClassItem.RefrenceScale));
                    break;
                    #endregion
                case EnumDataNodeItems.DomainItem:
                    #region 写属性域属性
                    DomainItemProperty domainItem = itemProperty as DomainItemProperty;
                    //写入属性域的所有属性值到相应节点属性
                    WriteDomainAttribute(domainItem, pXmlOut);
                    break;
                    #endregion
                default:
                    break;
            }
        }  

        /// <summary>
        /// 写入属性域的所有属性值到相应节点属性
        /// </summary>
        /// <param name="pDomainItem">属性域对象</param>
        /// <param name="pXmlNode">XmlNode节点</param>
        private void WriteDomainAttribute(DomainItemProperty pDomainItem, XmlTextWriter pXmlWriter)
        {
            //字段类型
            pXmlWriter.WriteAttributeString("FieldType", Convert.ToString((int)pDomainItem.Domain.FieldType));
            //合并策略
            pXmlWriter.WriteAttributeString("MergePolicy", Convert.ToString((int)pDomainItem.Domain.MergePolicy));
            //分割策略
            pXmlWriter.WriteAttributeString("SplitPolicy", Convert.ToString((int)pDomainItem.Domain.SplitPolicy));
            //域类型
            pXmlWriter.WriteAttributeString("DomainType", Convert.ToString((int)pDomainItem.Domain.Type));

            if (pDomainItem.Domain.Type == esriDomainType.esriDTRange)
            {
                //查询IRangeDomain接口
                IRangeDomain tRangDomain = pDomainItem.Domain as IRangeDomain;
                //最大值
                pXmlWriter.WriteAttributeString("MaxValue", Convert.ToString(tRangDomain.MaxValue));
                //最小值
                pXmlWriter.WriteAttributeString("MinValue", Convert.ToString(tRangDomain.MinValue));
            }
            else
            {
                //查询ICodeValueDomain接口
                ICodedValueDomain tCodeDomain = pDomainItem.Domain as ICodedValueDomain;
                //实例化StringBuilder实例
                StringBuilder tStrBuilder = new StringBuilder();
                //遍历写入值
                for (int i = 0; i < tCodeDomain.CodeCount; i++)
                {
                    tStrBuilder.Append(string.Format("({0},{1})", tCodeDomain.get_Value(i), tCodeDomain.get_Name(i)));
                }
                //写入相关的代码值域
                pXmlWriter.WriteAttributeString("CodeValue", tStrBuilder.ToString());
            }
        }

        /// <summary>
        /// 读取数据库配置信息到树对象
        /// </summary>
        /// <param name="filePath">数据库配置文件路径</param>
        public void ReadDBInfoToTreeView(string filePath)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);

                string strPath = "DataBaseConfig/数据库";
                XmlNodeList xmlNodeList = xmlDoc.SelectNodes(strPath);
                foreach (XmlNode tXmlNode in xmlNodeList)
                {
                    //根据节点信息返回属性项
                    ItemProperty itemProperty = GetItemPropertyInfo(tXmlNode,xmlDoc);

                    TreeNode treeNode = new TreeNode();
                    //获取属性节点类型的描述信息
                    string strNodeDes = DBInfoHandler.GetDataNodeDescripble(itemProperty.DataNodeItem);
                    treeNode.Text = string.Format("{0}:{1}({2})", strNodeDes, itemProperty.ItemName, itemProperty.ItemAliasName);
                    treeNode.Tag = itemProperty;
                    treeNode.ImageIndex = 0;
                    treeNode.SelectedImageIndex = 0;
                    treeNode.Expand();

                    //递归读入子节点信息
                    ReadChildXmlNode(tXmlNode, treeNode,xmlDoc);
                    //添加节点到树对象
                    m_dbTreeView.Nodes.Add(treeNode);
                }
                //全部展开
                //m_dbTreeView.ExpandAll(); 
                //设置选择结点
                if (m_dbTreeView.Nodes.Count > 0)
                {
                    this.m_dbTreeView.SelectedNode = this.m_dbTreeView.Nodes[0];
                }
            }
            catch
            {
                throw;
            }
        }  

        /// <summary>
        /// 递归读入子节点信息
        /// </summary>
        /// <param name="pXmlNode">XmlNode节点</param>
        /// <param name="pTreeNode">树节点对象</param>
        /// <param name="pXmlDoc">XmlDocument文档对象</param>
        private void ReadChildXmlNode(XmlNode pXmlNode,TreeNode pTreeNode,XmlDocument pXmlDoc)
        {
            foreach (XmlNode txmlNode in pXmlNode.ChildNodes)
            {
                //根据节点信息返回属性项
                ItemProperty itemProperty = GetItemPropertyInfo(txmlNode,pXmlDoc);
                //实例化树节点
                TreeNode treeNode = new TreeNode();
                //获取属性节点的描述信息
                string strDes = DBInfoHandler.GetDataNodeDescripble(itemProperty.DataNodeItem);
                treeNode.Text = string.Format("{0}:{1}({2})",strDes, itemProperty.ItemName, itemProperty.ItemAliasName); 
                treeNode.Tag = itemProperty;

                #region 设置节点图片索引
                if (itemProperty.DataNodeItem == EnumDataNodeItems.SubjectChildItem || itemProperty.DataNodeItem==EnumDataNodeItems.SubjectDomainItem)
                {
                    treeNode.ImageIndex = 1;
                    treeNode.SelectedImageIndex = 1;
                    treeNode.Expand();
                }
                else if (itemProperty.DataNodeItem == EnumDataNodeItems.DataSetItem)
                {
                    treeNode.ImageIndex = 2;
                    treeNode.SelectedImageIndex = 2;
                    treeNode.Expand();
                }
                else if (itemProperty.DataNodeItem == EnumDataNodeItems.GeometryFieldItem)
                {
                    GeometryFieldItemProperty geoFieldItemProperty = itemProperty as GeometryFieldItemProperty;
                    if (geoFieldItemProperty.GeometryType == EnumGeometryItems.点 || geoFieldItemProperty.GeometryType == EnumGeometryItems.多点)
                    {
                        pTreeNode.ImageIndex = 3;
                        pTreeNode.SelectedImageIndex = 3;
                    }
                    else if (geoFieldItemProperty.GeometryType == EnumGeometryItems.线)
                    {
                        pTreeNode.ImageIndex = 4;
                        pTreeNode.SelectedImageIndex = 4;
                    }
                    else
                    {
                        pTreeNode.ImageIndex = 5;
                        pTreeNode.SelectedImageIndex = 5;
                    }
                }
                else if (itemProperty.DataNodeItem == EnumDataNodeItems.DomainItem)
                {
                    treeNode.ImageIndex = 6;
                    treeNode.SelectedImageIndex = 6;
                }
                else if (itemProperty.DataNodeItem == EnumDataNodeItems.CustomTableItem)
                {
                    treeNode.ImageIndex = 7;
                    treeNode.SelectedImageIndex = 7;
                }
                else if (itemProperty.DataNodeItem == EnumDataNodeItems.FeatureClassItem)
                {
                    FeatureClassItemProperty tItemProperty = itemProperty as FeatureClassItemProperty;
                    if (tItemProperty.FeatureType == EnumFeatureType.注记要素类)
                    {
                        treeNode.ImageIndex = 9;
                        treeNode.SelectedImageIndex = 9;
                    }
                }
                #endregion

                //递归读入子节点信息
                ReadChildXmlNode(txmlNode, treeNode,pXmlDoc);

                pTreeNode.Nodes.Add(treeNode);
            }            
        }

        /// <summary>
        /// 根据节点信息返回属性项
        /// </summary>
        /// <param name="pXmlNode">XmlNode节点</param>
        /// <param name="pXmlDoc">XmlDocument文档对象</param>
        /// <returns>返回属性项</returns>
        private ItemProperty GetItemPropertyInfo(XmlNode pXmlNode,XmlDocument pXmlDoc)
        {
            EnumDataNodeItems dataNodeItem = (EnumDataNodeItems)Convert.ToInt32(pXmlNode.Attributes["DataNodeItem"].InnerText);
            ItemProperty itemProperty=null;

            switch (dataNodeItem)
            {
                case EnumDataNodeItems.DataBaseItem:
                    #region 读数据库项属性
                    DataBaseItemProperty dbItemProperty = new DataBaseItemProperty();
                    dbItemProperty.ItemName = pXmlNode.Attributes["ItemName"].InnerText;
                    dbItemProperty.ItemAliasName = pXmlNode.Attributes["ItemAliasName"].InnerText;
                    dbItemProperty.DataNodeItem = dataNodeItem;
                    dbItemProperty.DBOwner = pXmlNode.Attributes["DBOwner"].InnerText;
                    dbItemProperty.DBVersion = pXmlNode.Attributes["DBVersion"].InnerText;
                    itemProperty = dbItemProperty;                   
                    break;
                    #endregion
                case EnumDataNodeItems.DataSetItem:
                    #region 读要素集项属性
                    DataSetItemProperty dsItemProperty = new DataSetItemProperty();
                    dsItemProperty.ItemName = pXmlNode.Attributes["ItemName"].InnerText;
                    dsItemProperty.ItemAliasName = pXmlNode.Attributes["ItemAliasName"].InnerText;
                    dsItemProperty.DataNodeItem = dataNodeItem;
                    #region 设置空间参考
                    ISpatialReference tSpatialReference;
                    CreateESRISpatialReference(pXmlNode.Attributes["CoordinateSystem"].InnerText, out tSpatialReference);
                    SetXYDomain(pXmlNode.Attributes["XYDomain"].InnerText, ref  tSpatialReference);
                    SetMDomain(pXmlNode.Attributes["MDomain"].InnerText, ref  tSpatialReference);
                    SetZDomain(pXmlNode.Attributes["ZDomain"].InnerText, ref tSpatialReference);
                    dsItemProperty.GeoReference = tSpatialReference;
                    #endregion
                    itemProperty = dsItemProperty;
                    break;
                    #endregion
                case EnumDataNodeItems.FeatureClassItem:
                    #region 读要素类项属性
                    FeatureClassItemProperty featClassItemProperty = new FeatureClassItemProperty();
                    featClassItemProperty.ItemName = pXmlNode.Attributes["ItemName"].InnerText;
                    featClassItemProperty.ItemAliasName = pXmlNode.Attributes["ItemAliasName"].InnerText;
                    featClassItemProperty.DataNodeItem = dataNodeItem;
                    featClassItemProperty.FeatureType = (EnumFeatureType)(Convert.ToInt32(pXmlNode.Attributes["FeatureType"].InnerText));
                    featClassItemProperty.RefrenceScale = Convert.ToInt32(pXmlNode.Attributes["RefrenceScale"].InnerText);
                    itemProperty = featClassItemProperty;
                    break;
                    #endregion
                case EnumDataNodeItems.ObjectFieldItem:
                    #region 读Object字段项属性
                    ObjectFieldItemProperty objItemProperty = new ObjectFieldItemProperty();
                    objItemProperty.ItemName = pXmlNode.Attributes["ItemName"].InnerText;
                    objItemProperty.ItemAliasName = pXmlNode.Attributes["ItemAliasName"].InnerText;                  
                    objItemProperty.DataNodeItem = dataNodeItem;
                    itemProperty = objItemProperty;
                    break;
                    #endregion
                case EnumDataNodeItems.GeometryFieldItem:
                    #region 读几何字段项属性
                    GeometryFieldItemProperty geoItemProperty = new GeometryFieldItemProperty();
                    geoItemProperty.ItemName = pXmlNode.Attributes["ItemName"].InnerText;
                    geoItemProperty.ItemAliasName = pXmlNode.Attributes["ItemAliasName"].InnerText;
                    geoItemProperty.DataNodeItem = dataNodeItem;
                    geoItemProperty.AveragePoint =Convert.ToInt32(pXmlNode.Attributes["AveragePoint"].InnerText);             
                    geoItemProperty.GeometryType =(EnumGeometryItems)Convert.ToInt32( pXmlNode.Attributes["GeometryType"].InnerText);                    
                    //geoItemProperty.GeoReference = pXmlNode.Attributes["GeoReference"].InnerText;
                    geoItemProperty.Grid1 = Convert.ToInt32(pXmlNode.Attributes["Grid1"].InnerText);
                    geoItemProperty.Grid2 = Convert.ToInt32(pXmlNode.Attributes["Grid2"].InnerText);
                    geoItemProperty.Grid3 = Convert.ToInt32(pXmlNode.Attributes["Grid3"].InnerText);
                    geoItemProperty.IsContainM = Boolean.Parse(pXmlNode.Attributes["IsContainM"].InnerText);
                    geoItemProperty.IsContainZ = Boolean.Parse(pXmlNode.Attributes["IsContainZ"].InnerText);
                    geoItemProperty.IsNull = Boolean.Parse(pXmlNode.Attributes["IsNull"].InnerText);
                    itemProperty = geoItemProperty;
                    break;
                    #endregion
                case EnumDataNodeItems.CustomFieldItem:
                    #region 读通用字段属性
                    //FieldItemProperty fieldItemProperty = new FieldItemProperty();
                    //fieldItemProperty.ItemName = pXmlNode.Attributes["ItemName"].InnerText;
                    //fieldItemProperty.ItemAliasName = pXmlNode.Attributes["ItemAliasName"].InnerText;
                    //fieldItemProperty.DataNodeItem = dataNodeItem;
                    //fieldItemProperty.DefaultValue = pXmlNode.Attributes["DefaultValue"].InnerText;
                    //fieldItemProperty.IsNull = Boolean.Parse(pXmlNode.Attributes["IsNull"].InnerText);
                    //fieldItemProperty.FieldType = (EnumFieldItems)Convert.ToInt32(pXmlNode.Attributes["FieldType"].InnerText);
                    //fieldItemProperty.Length = Convert.ToInt32(pXmlNode.Attributes["Length"].InnerText);
                    //fieldItemProperty.Precision = Convert.ToInt32(pXmlNode.Attributes["Precision"].InnerText);
                    //fieldItemProperty.Scale = Convert.ToInt32(pXmlNode.Attributes["Scale"].InnerText);

                    //读取通用字段属性项
                    itemProperty = ReadFieldItemAttribute(pXmlNode, pXmlDoc);
                    break;
                    #endregion
                case EnumDataNodeItems.DomainItem:
                    //读取节点信息到属性域项
                    itemProperty = ReadDomainAttribute(pXmlNode);
                    break;
                default:
                    itemProperty = new ItemProperty();
                    itemProperty.ItemName = pXmlNode.Attributes["ItemName"].InnerText;
                    itemProperty.ItemAliasName = pXmlNode.Attributes["ItemAliasName"].InnerText;
                    itemProperty.DataNodeItem = dataNodeItem;
                    break;
            }

            //添加属性值发生改变事件
            itemProperty.ItemPropertyValueChanged += new ItemPropertyValueChangedEventHandler(ItemPropertyValueChanged);

            return itemProperty;
        }  

        /// <summary>
        /// 读取节点信息到属性域项
        /// </summary>
        /// <param name="pXmlNode">XML节点</param>
        /// <returns>返回属性域项</returns>
        private DomainItemProperty ReadDomainAttribute(XmlNode pXmlNode)
        {
            //实例化属性域对象
            DomainItemProperty tDomainItemProperty = new DomainItemProperty();
            tDomainItemProperty.ItemName = pXmlNode.Attributes["ItemName"].InnerText;
            tDomainItemProperty.ItemAliasName = pXmlNode.Attributes["ItemAliasName"].InnerText;
            IDomain tDomain;

            esriDomainType tDomainType = (esriDomainType)Convert.ToInt32(pXmlNode.Attributes["DomainType"].InnerText);
            if (tDomainType == esriDomainType.esriDTRange)
            {
                tDomain = new RangeDomainClass();
                //字段类型
                tDomain.FieldType = (esriFieldType)Convert.ToInt32(pXmlNode.Attributes["FieldType"].InnerText);
                //查询IRangeDomain接口
                IRangeDomain tRangeDomain = tDomain as IRangeDomain;
                //设置最小值/最大值
                tRangeDomain.MinValue = Convert.ToInt32(pXmlNode.Attributes["MinValue"].InnerText);
                tRangeDomain.MaxValue = Convert.ToInt32(pXmlNode.Attributes["MaxValue"].InnerText);
            }
            else
            {
                tDomain = new CodedValueDomainClass();
                //字段类型
                tDomain.FieldType = (esriFieldType)Convert.ToInt32(pXmlNode.Attributes["FieldType"].InnerText);
                //查询引用接口
                ICodedValueDomain tCodeValue = tDomain as ICodedValueDomain;

                string tempCodeValues = pXmlNode.Attributes["CodeValue"].InnerText;
                string[] strCodeValues1 = tempCodeValues.Split('(', ')');
                for (int i = 0; i < strCodeValues1.Length; i++)
                {
                    if (strCodeValues1[i].Length > 0)
                    {
                        string[] strCodeValues2=strCodeValues1[i].Split(',');
                        if (strCodeValues2.Length == 2)
                        {
                            tCodeValue.AddCode(GetCodedValue(tDomain.FieldType, strCodeValues2[0]), strCodeValues2[1]);
                        }
                    }
                }
            }

            //通用部分赋值
            //名称
            tDomain.Name = tDomainItemProperty.ItemName;  
            //描述信息
            tDomain.Description = tDomainItemProperty.ItemAliasName;    
            //合并策略
            tDomain.MergePolicy = (esriMergePolicyType)Convert.ToInt32(pXmlNode.Attributes["MergePolicy"].InnerText);
            //分割策略
            tDomain.SplitPolicy = (esriSplitPolicyType)Convert.ToInt32(pXmlNode.Attributes["SplitPolicy"].InnerText);

            tDomainItemProperty.Domain = tDomain;
            return tDomainItemProperty;
        }

        /// <summary>
        /// 读取通用字段的属性值
        /// </summary>
        /// <param name="pXmlNode">XmlNode节点</param>
        /// <param name="pXmlDoc">XmlDocument文档对象</param>
        /// <returns>返回FieldItemProperty对象</returns>
        private FieldItemProperty ReadFieldItemAttribute(XmlNode pXmlNode,XmlDocument pXmlDoc)
        {
            //实例字段属性类
            FieldItemProperty fieldItemProperty = new FieldItemProperty();
            //名称
            fieldItemProperty.ItemName = pXmlNode.Attributes["ItemName"].InnerText;   
            //别名
            fieldItemProperty.ItemAliasName = pXmlNode.Attributes["ItemAliasName"].InnerText;        
            //默认值
            fieldItemProperty.DefaultValue = pXmlNode.Attributes["DefaultValue"].InnerText;
            //是否允许为空
            fieldItemProperty.IsNull = Boolean.Parse(pXmlNode.Attributes["IsNull"].InnerText);
            //字段类型
            fieldItemProperty.FieldType = (EnumFieldItems)Convert.ToInt32(pXmlNode.Attributes["FieldType"].InnerText);
            //长度
            fieldItemProperty.Length = Convert.ToInt32(pXmlNode.Attributes["Length"].InnerText);
            //精度
            fieldItemProperty.Precision = Convert.ToInt32(pXmlNode.Attributes["Precision"].InnerText);
            //小数位数
            fieldItemProperty.Scale = Convert.ToInt32(pXmlNode.Attributes["Scale"].InnerText);

            #region 获取属性域
            //属性域名称
            string strDomainName = pXmlNode.Attributes["Domain"].InnerText;
            if (strDomainName.Length > 0)
            {
                string xPath = "DataBaseConfig/数据库/专题属性域/属性域";
                //得到属性域集合节点
                XmlNodeList xmlNodeList = pXmlDoc.SelectNodes(xPath);
                //遍历属性域集合节点
                foreach (XmlNode xmlNode in xmlNodeList)
                {
                    //查询名称相同的属性域
                    if (xmlNode.Attributes["ItemName"].InnerText == strDomainName)
                    {
                        //读取节点信息到属性域
                        fieldItemProperty.DomainItemProperty = ReadDomainAttribute(xmlNode);
                        break;
                    }
                }
            }
            #endregion

            return fieldItemProperty;                
        }

        /// <summary>
        /// ItemProperty将调用此方法来通知DBTreeControl对象其属性值已发生改变
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">ItemPropertyEventArgs事件参数</param>
        private void ItemPropertyValueChanged(object sender, ItemPropertyEventArgs e)
        {
            ItemProperty itemProperty = e.ItemProperty;
            TreeNode selTreeNode = this.m_dbTreeView.SelectedNode;
            selTreeNode.Text = string.Format("{0}:{1}({2})", DBInfoHandler.GetDataNodeDescripble(itemProperty.DataNodeItem), itemProperty.ItemName, itemProperty.ItemAliasName);

            #region 设置结点的图片索引值
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
            //Rewrite xubin 20120203
            else
            {
                FeatureClassItemProperty tItemProperty = itemProperty as FeatureClassItemProperty;
                if (tItemProperty != null)
                {
                    if (tItemProperty.FeatureType == EnumFeatureType.简单要素类)
                    {
                        //修改子节点

                        selTreeNode.ImageIndex = 3;
                        selTreeNode.SelectedImageIndex = 3;
                    }
                    else if (tItemProperty.FeatureType == EnumFeatureType.注记要素类)
                    {
                        selTreeNode.ImageIndex = 9;
                        selTreeNode.SelectedImageIndex = 9;
                    }
                }
            }

            //刷新属性框对象
            this.m_propertyGrid.Refresh();
        }

        #region 修改注记节点属性(Rewrite xubin 20120203)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pNode"></param>
        private void UpdataAnnoNode(TreeNode pNode)
        {
        }
        #endregion

        #region 读写空间参考 私有处理方法
        /// <summary>
        /// 将指定的空间参考坐标系转换为字符串
        /// </summary>
        /// <param name="pSpatialReferenc">指定的空间参考</param>
        private string CoordinateSystemToString(ISpatialReference pSpatialReference)
        {
            string strSR = "";              
            int cBytesWrote;

            //此处一定要引用IESRISpatialReferenceGEN接口,
            //否则引用IESRISpatialReference此接口将产生不可预测错误
            //查询引用接口
            IESRISpatialReferenceGEN tESRISpatialReference = pSpatialReference as IESRISpatialReferenceGEN;
            //输出空间参考信息
            tESRISpatialReference.ExportToESRISpatialReference(out strSR, out cBytesWrote);                   
            
            return strSR;
        }

        /// <summary>
        /// 将指定的空间参考XY域转换为字符串
        /// </summary>
        /// <param name="pSpatialReferenc">指定的空间参考</param>
        private string XYDomainToString(ISpatialReference pSpatialReference)
        {
            if (pSpatialReference.HasXYPrecision() == true)
            {
                double xMin, xMax, yMin, yMax, xyResolution;            
                //得到XY方向的取值范围
                pSpatialReference.GetDomain(out xMin,out xMax, out yMin,out yMax);
                //查询ISpatialReferenceResolution接口
                ISpatialReferenceResolution tSRResolution = pSpatialReference as ISpatialReferenceResolution;
                //得到Z轴方向的分辨率
                xyResolution = tSRResolution.get_XYResolution(false);    

                return string.Format("{0},{1},{2},{3},{4}", xMin, xMax, yMin, yMax, xyResolution);
            }
            else
                return "";            
        }

        /// <summary>
        /// 将指定的空间参考Z域转换为字符串
        /// </summary>
        /// <param name="pSpatialReferenc">指定的空间参考</param>
        private string ZDomainToString(ISpatialReference pSpatialReference)
        {
            if (pSpatialReference.HasZPrecision()==true)
            {
                double zMin, zMax,zResolution;
                //得到Z值范围
                pSpatialReference.GetZDomain(out zMin, out zMax);
                //查询ISpatialReferenceResolution接口
                ISpatialReferenceResolution tSRResolution = pSpatialReference as ISpatialReferenceResolution;
                //得到Z轴方向的分辨率
                zResolution=tSRResolution.get_ZResolution(false);

                return string.Format("{0},{1},{2}", zMin, zMax, zResolution);
            }
            else
                return "";
        }

        /// <summary>
        /// 将指定的空间参考M域转换为字符串
        /// </summary>
        /// <param name="pSpatialReferenc">指定的空间参考</param>
        private string MDomainToString(ISpatialReference pSpatialReference)
        {
            if (pSpatialReference.HasMPrecision()==true)
            {
                double MMin, MMax, MResolution;
                //得取M值范围
                pSpatialReference.GetMDomain(out MMin, out MMax);
                //查询ISpatialReferenceResolution接口
                ISpatialReferenceResolution tSRResolution = pSpatialReference as ISpatialReferenceResolution;
                //得到M方向的分辨率
                MResolution = tSRResolution.MResolution;

                return string.Format("{0},{1},{2}", MMin, MMax, MResolution);
            }
            else
                return "";
        }

        /// <summary>
        /// 从指定坐标信息创建空间参考
        /// </summary>
        /// <param name="pSpaBuffer">坐标信息</param>
        /// <param name="pSpatialReference">空间参考</param>
        private void CreateESRISpatialReference(string pSpaBuffer, out ISpatialReference pSpatialReference)
        {
            if (pSpaBuffer.Trim().Length == 0)
            {
                pSpatialReference = new UnknownCoordinateSystemClass();
            }
            else
            {
                int cBytesRead;
                //实例空间参考环境类
                ISpatialReferenceFactory2 spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                spatialReferenceFactory.CreateESRISpatialReference(pSpaBuffer, out pSpatialReference, out cBytesRead);
            }
        }

        /// <summary>
        /// 从指定XY域信息设置XY域
        /// </summary>
        /// <param name="pXYDomain">XY域信息</param>
        /// <param name="pSpatialReference">空间参考</param>
        private void SetXYDomain(string pXYDomain, ref ISpatialReference pSpatialReference)
        {
            if (pXYDomain.Trim().Length > 0 && (pXYDomain.Split(',').Length ==5 ))
            {
                double xMin, xMax, yMin, yMax, xyResolution;

                string[] strXYDomain = pXYDomain.Split(',');
                xMin = Convert.ToDouble(strXYDomain[0]);
                xMax = Convert.ToDouble(strXYDomain[1]);
                yMin = Convert.ToDouble(strXYDomain[2]);
                yMax = Convert.ToDouble(strXYDomain[3]);
                xyResolution = Convert.ToDouble(strXYDomain[4]);

                pSpatialReference.SetDomain(xMin, xMax, yMin, yMax);
                //查询ISpatialReferenceResolution接口
                ISpatialReferenceResolution tSRResolution = pSpatialReference as ISpatialReferenceResolution;
                //设置分辨率
                tSRResolution.set_XYResolution(false, xyResolution);    
            }
        }

        /// <summary>
        /// 从指定M域信息设置空间参考M域
        /// </summary>
        /// <param name="pMDomain">M域信息</param>
        /// <param name="pSpatialReference">空间参考</param>
        private void SetMDomain(string pMDomain, ref ISpatialReference pSpatialReference)
        {
            if (pMDomain.Trim().Length > 0 && (pMDomain.Split(',').Length == 3))
            {
                double MMin, MMax, MResolution;
                string[] strMDomain = pMDomain.Split(',');
                MMin = Convert.ToDouble(strMDomain[0]);
                MMax = Convert.ToDouble(strMDomain[1]);
                MResolution = Convert.ToDouble(strMDomain[2]);

                //设置M域最大、最小值
                pSpatialReference.SetMDomain(MMin, MMax);
                //查询ISpatialReferenceResolution接口
                ISpatialReferenceResolution tSRResolution = pSpatialReference as ISpatialReferenceResolution;
                //设置分辨率
                tSRResolution.MResolution=MResolution;      
            }
        }

        /// <summary>
        /// 从指定Z域信息设置空间参考Z域
        /// </summary>
        /// <param name="pZDomain">Z域信息</param>
        /// <param name="pSpatialReference">空间参考</param>
        private void SetZDomain(string pZDomain, ref  ISpatialReference pSpatialReference)
        {
            if (pZDomain.Trim().Length > 0 && (pZDomain.Split(',').Length == 3))
            {
                double zMin, zMax, zResolution;

                string[] strZDomain = pZDomain.Split(',');
                zMin = Convert.ToDouble(strZDomain[0]);
                zMax = Convert.ToDouble(strZDomain[1]);
                zResolution = Convert.ToDouble(strZDomain[2]);                 

                pSpatialReference.SetZDomain(zMin, zMax);
                //查询ISpatialReferenceResolution接口
                ISpatialReferenceResolution tSRResolution = pSpatialReference as ISpatialReferenceResolution;
                //设置分辨率
                tSRResolution.set_ZResolution(false,zResolution);               
            }
        }
        #endregion
    }
}
