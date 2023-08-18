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
    /// ����������Ϣ��д��
    /// </summary>
    public class DbInfoReadWrite1
    {
        private TreeView m_dbTreeView;
        private PropertyGrid m_propertyGrid;
        private EnumDbInfoStates m_treeviewState;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        /// <param name="pTreeView">������</param>
        /// <param name="propertyGrid">���Կؼ�</param>
        public DbInfoReadWrite1(TreeView pTreeView,PropertyGrid propertyGrid)
        {
            this.m_dbTreeView = pTreeView;
            this.m_propertyGrid = propertyGrid;
        }

        /// <summary>
        /// ����ָ�����ֶ�����,�õ�����ֵ
        /// </summary>
        /// <param name="pFieldType">�ֶ�����������</param>
        /// <param name="pCellValue">����ֵ����</param>
        /// <returns>������ȷ�Ĵ���ֵ</returns>
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
                throw (new Exception(string.Format("{0} ֵ���ֶ����Ͳ���,���������룡", pCellValue)));
            }
        }  

        /// <summary>
        /// ����ͼ״̬
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
        /// д�����ݿ�������Ϣ�������ļ�
        /// </summary>
        /// <param name="filePath">�ļ�·��</param>  
        public void WriteDBInfoToFile(string filePath)
        {
            //��ʼ���ļ���
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                XmlTextWriter tXmlOut = new XmlTextWriter(fs, Encoding.Unicode);
                tXmlOut.Formatting = Formatting.Indented;

                //��дXML����
                tXmlOut.WriteStartDocument();

                //��д�����ļ�ע����Ϣ
                tXmlOut.WriteComment("����:���ݿ��׼�ļ���");
                tXmlOut.WriteComment("�汾:");
                tXmlOut.WriteComment("����:Echo-AG.COM.SDM");

                //��ʼ
                tXmlOut.WriteStartElement("DataBaseConfig");

                for (int i = 0; i < m_dbTreeView.Nodes.Count; i++)
                {
                    //��ʼ
                    tXmlOut.WriteStartElement("���ݿ�");
                    //д��������Ϣ
                    WriteAttributeString(tXmlOut, m_dbTreeView.Nodes[i]);
                    //�ݹ�д���ӽڵ���Ϣ
                    WriteChildNode(tXmlOut, m_dbTreeView.Nodes[i]);
                    //����
                    tXmlOut.WriteFullEndElement();

                }

                //��ֹһ��Ԫ��
                tXmlOut.WriteEndElement();
                //�ر��κδ򿪵�Ԫ��
                tXmlOut.WriteEndDocument();
                //�ر�
                tXmlOut.Close();
            }
        }

        /// <summary>
        /// �ݹ�д���ӽڵ���Ϣ
        /// </summary>
        /// <param name="pXmlOut">XmlTextWriter����</param>
        /// <param name="pTreeNode">�����</param>
        private void WriteChildNode(XmlTextWriter pXmlOut, TreeNode pTreeNode)
        {
            for (int i = 0; i < pTreeNode.Nodes.Count; i++)
            {
                //ת��ΪItemProperty����
                ItemProperty itemProperty = pTreeNode.Nodes[i].Tag as ItemProperty;
                //��ʼ
                pXmlOut.WriteStartElement(DBInfoHandler.GetDataNodeDescripble(itemProperty.DataNodeItem));
                //д��������Ϣ
                WriteAttributeString(pXmlOut, pTreeNode.Nodes[i]);
                //�ݹ�д���ӽڵ���Ϣ
                WriteChildNode(pXmlOut, pTreeNode.Nodes[i]);
                //����
                pXmlOut.WriteFullEndElement();
            }
        }

        /// <summary>
        /// д��������Ϣ
        /// </summary>
        /// <param name="pXmlOut">XmlTextWriter����</param>
        /// <param name="pTreeNode">�����</param>
        private void WriteAttributeString(XmlTextWriter pXmlOut, TreeNode pTreeNode)
        {
            ItemProperty itemProperty = pTreeNode.Tag as ItemProperty;
            pXmlOut.WriteAttributeString("ItemName", itemProperty.ItemName);
            pXmlOut.WriteAttributeString("ItemAliasName", itemProperty.ItemAliasName);
            pXmlOut.WriteAttributeString("DataNodeItem", Convert.ToString((int)itemProperty.DataNodeItem));

            switch (itemProperty.DataNodeItem)
            {
                case EnumDataNodeItems.DataBaseItem:
                    #region д���ݿ�����
                    DataBaseItemProperty dbItemProperty = itemProperty as DataBaseItemProperty;
                    pXmlOut.WriteAttributeString("DBVersion", dbItemProperty.DBVersion);
                    pXmlOut.WriteAttributeString("DBOwner", dbItemProperty.DBOwner);
                    break;
                    #endregion
                case EnumDataNodeItems.DataSetItem:
                    #region дҪ�ؼ�����
                    DataSetItemProperty dsItemProperty = itemProperty as DataSetItemProperty;
                    pXmlOut.WriteAttributeString("CoordinateSystem", CoordinateSystemToString(dsItemProperty.GeoReference));
                    pXmlOut.WriteAttributeString("XYDomain", XYDomainToString(dsItemProperty.GeoReference));
                    pXmlOut.WriteAttributeString("ZDomain", ZDomainToString(dsItemProperty.GeoReference));
                    pXmlOut.WriteAttributeString("MDomain", MDomainToString(dsItemProperty.GeoReference));
                    break;
                    #endregion
                case EnumDataNodeItems.GeometryFieldItem:
                    #region д�����ֶ�����
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
                    #region дͨ���ֶ�����
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
                    #region дҪ��������
                    FeatureClassItemProperty featClassItem = itemProperty as FeatureClassItemProperty;
                    pXmlOut.WriteAttributeString("FeatureType", Convert.ToString((int)featClassItem.FeatureType));
                    pXmlOut.WriteAttributeString("RefrenceScale", Convert.ToString(featClassItem.RefrenceScale));
                    break;
                    #endregion
                case EnumDataNodeItems.DomainItem:
                    #region д����������
                    DomainItemProperty domainItem = itemProperty as DomainItemProperty;
                    //д�����������������ֵ����Ӧ�ڵ�����
                    WriteDomainAttribute(domainItem, pXmlOut);
                    break;
                    #endregion
                default:
                    break;
            }
        }  

        /// <summary>
        /// д�����������������ֵ����Ӧ�ڵ�����
        /// </summary>
        /// <param name="pDomainItem">���������</param>
        /// <param name="pXmlNode">XmlNode�ڵ�</param>
        private void WriteDomainAttribute(DomainItemProperty pDomainItem, XmlTextWriter pXmlWriter)
        {
            //�ֶ�����
            pXmlWriter.WriteAttributeString("FieldType", Convert.ToString((int)pDomainItem.Domain.FieldType));
            //�ϲ�����
            pXmlWriter.WriteAttributeString("MergePolicy", Convert.ToString((int)pDomainItem.Domain.MergePolicy));
            //�ָ����
            pXmlWriter.WriteAttributeString("SplitPolicy", Convert.ToString((int)pDomainItem.Domain.SplitPolicy));
            //������
            pXmlWriter.WriteAttributeString("DomainType", Convert.ToString((int)pDomainItem.Domain.Type));

            if (pDomainItem.Domain.Type == esriDomainType.esriDTRange)
            {
                //��ѯIRangeDomain�ӿ�
                IRangeDomain tRangDomain = pDomainItem.Domain as IRangeDomain;
                //���ֵ
                pXmlWriter.WriteAttributeString("MaxValue", Convert.ToString(tRangDomain.MaxValue));
                //��Сֵ
                pXmlWriter.WriteAttributeString("MinValue", Convert.ToString(tRangDomain.MinValue));
            }
            else
            {
                //��ѯICodeValueDomain�ӿ�
                ICodedValueDomain tCodeDomain = pDomainItem.Domain as ICodedValueDomain;
                //ʵ����StringBuilderʵ��
                StringBuilder tStrBuilder = new StringBuilder();
                //����д��ֵ
                for (int i = 0; i < tCodeDomain.CodeCount; i++)
                {
                    tStrBuilder.Append(string.Format("({0},{1})", tCodeDomain.get_Value(i), tCodeDomain.get_Name(i)));
                }
                //д����صĴ���ֵ��
                pXmlWriter.WriteAttributeString("CodeValue", tStrBuilder.ToString());
            }
        }

        /// <summary>
        /// ��ȡ���ݿ�������Ϣ��������
        /// </summary>
        /// <param name="filePath">���ݿ������ļ�·��</param>
        public void ReadDBInfoToTreeView(string filePath)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);

                string strPath = "DataBaseConfig/���ݿ�";
                XmlNodeList xmlNodeList = xmlDoc.SelectNodes(strPath);
                foreach (XmlNode tXmlNode in xmlNodeList)
                {
                    //���ݽڵ���Ϣ����������
                    ItemProperty itemProperty = GetItemPropertyInfo(tXmlNode,xmlDoc);

                    TreeNode treeNode = new TreeNode();
                    //��ȡ���Խڵ����͵�������Ϣ
                    string strNodeDes = DBInfoHandler.GetDataNodeDescripble(itemProperty.DataNodeItem);
                    treeNode.Text = string.Format("{0}:{1}({2})", strNodeDes, itemProperty.ItemName, itemProperty.ItemAliasName);
                    treeNode.Tag = itemProperty;
                    treeNode.ImageIndex = 0;
                    treeNode.SelectedImageIndex = 0;
                    treeNode.Expand();

                    //�ݹ�����ӽڵ���Ϣ
                    ReadChildXmlNode(tXmlNode, treeNode,xmlDoc);
                    //��ӽڵ㵽������
                    m_dbTreeView.Nodes.Add(treeNode);
                }
                //ȫ��չ��
                //m_dbTreeView.ExpandAll(); 
                //����ѡ����
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
        /// �ݹ�����ӽڵ���Ϣ
        /// </summary>
        /// <param name="pXmlNode">XmlNode�ڵ�</param>
        /// <param name="pTreeNode">���ڵ����</param>
        /// <param name="pXmlDoc">XmlDocument�ĵ�����</param>
        private void ReadChildXmlNode(XmlNode pXmlNode,TreeNode pTreeNode,XmlDocument pXmlDoc)
        {
            foreach (XmlNode txmlNode in pXmlNode.ChildNodes)
            {
                //���ݽڵ���Ϣ����������
                ItemProperty itemProperty = GetItemPropertyInfo(txmlNode,pXmlDoc);
                //ʵ�������ڵ�
                TreeNode treeNode = new TreeNode();
                //��ȡ���Խڵ��������Ϣ
                string strDes = DBInfoHandler.GetDataNodeDescripble(itemProperty.DataNodeItem);
                treeNode.Text = string.Format("{0}:{1}({2})",strDes, itemProperty.ItemName, itemProperty.ItemAliasName); 
                treeNode.Tag = itemProperty;

                #region ���ýڵ�ͼƬ����
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
                    if (geoFieldItemProperty.GeometryType == EnumGeometryItems.�� || geoFieldItemProperty.GeometryType == EnumGeometryItems.���)
                    {
                        pTreeNode.ImageIndex = 3;
                        pTreeNode.SelectedImageIndex = 3;
                    }
                    else if (geoFieldItemProperty.GeometryType == EnumGeometryItems.��)
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
                    if (tItemProperty.FeatureType == EnumFeatureType.ע��Ҫ����)
                    {
                        treeNode.ImageIndex = 9;
                        treeNode.SelectedImageIndex = 9;
                    }
                }
                #endregion

                //�ݹ�����ӽڵ���Ϣ
                ReadChildXmlNode(txmlNode, treeNode,pXmlDoc);

                pTreeNode.Nodes.Add(treeNode);
            }            
        }

        /// <summary>
        /// ���ݽڵ���Ϣ����������
        /// </summary>
        /// <param name="pXmlNode">XmlNode�ڵ�</param>
        /// <param name="pXmlDoc">XmlDocument�ĵ�����</param>
        /// <returns>����������</returns>
        private ItemProperty GetItemPropertyInfo(XmlNode pXmlNode,XmlDocument pXmlDoc)
        {
            EnumDataNodeItems dataNodeItem = (EnumDataNodeItems)Convert.ToInt32(pXmlNode.Attributes["DataNodeItem"].InnerText);
            ItemProperty itemProperty=null;

            switch (dataNodeItem)
            {
                case EnumDataNodeItems.DataBaseItem:
                    #region �����ݿ�������
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
                    #region ��Ҫ�ؼ�������
                    DataSetItemProperty dsItemProperty = new DataSetItemProperty();
                    dsItemProperty.ItemName = pXmlNode.Attributes["ItemName"].InnerText;
                    dsItemProperty.ItemAliasName = pXmlNode.Attributes["ItemAliasName"].InnerText;
                    dsItemProperty.DataNodeItem = dataNodeItem;
                    #region ���ÿռ�ο�
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
                    #region ��Ҫ����������
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
                    #region ��Object�ֶ�������
                    ObjectFieldItemProperty objItemProperty = new ObjectFieldItemProperty();
                    objItemProperty.ItemName = pXmlNode.Attributes["ItemName"].InnerText;
                    objItemProperty.ItemAliasName = pXmlNode.Attributes["ItemAliasName"].InnerText;                  
                    objItemProperty.DataNodeItem = dataNodeItem;
                    itemProperty = objItemProperty;
                    break;
                    #endregion
                case EnumDataNodeItems.GeometryFieldItem:
                    #region �������ֶ�������
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
                    #region ��ͨ���ֶ�����
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

                    //��ȡͨ���ֶ�������
                    itemProperty = ReadFieldItemAttribute(pXmlNode, pXmlDoc);
                    break;
                    #endregion
                case EnumDataNodeItems.DomainItem:
                    //��ȡ�ڵ���Ϣ����������
                    itemProperty = ReadDomainAttribute(pXmlNode);
                    break;
                default:
                    itemProperty = new ItemProperty();
                    itemProperty.ItemName = pXmlNode.Attributes["ItemName"].InnerText;
                    itemProperty.ItemAliasName = pXmlNode.Attributes["ItemAliasName"].InnerText;
                    itemProperty.DataNodeItem = dataNodeItem;
                    break;
            }

            //�������ֵ�����ı��¼�
            itemProperty.ItemPropertyValueChanged += new ItemPropertyValueChangedEventHandler(ItemPropertyValueChanged);

            return itemProperty;
        }  

        /// <summary>
        /// ��ȡ�ڵ���Ϣ����������
        /// </summary>
        /// <param name="pXmlNode">XML�ڵ�</param>
        /// <returns>������������</returns>
        private DomainItemProperty ReadDomainAttribute(XmlNode pXmlNode)
        {
            //ʵ�������������
            DomainItemProperty tDomainItemProperty = new DomainItemProperty();
            tDomainItemProperty.ItemName = pXmlNode.Attributes["ItemName"].InnerText;
            tDomainItemProperty.ItemAliasName = pXmlNode.Attributes["ItemAliasName"].InnerText;
            IDomain tDomain;

            esriDomainType tDomainType = (esriDomainType)Convert.ToInt32(pXmlNode.Attributes["DomainType"].InnerText);
            if (tDomainType == esriDomainType.esriDTRange)
            {
                tDomain = new RangeDomainClass();
                //�ֶ�����
                tDomain.FieldType = (esriFieldType)Convert.ToInt32(pXmlNode.Attributes["FieldType"].InnerText);
                //��ѯIRangeDomain�ӿ�
                IRangeDomain tRangeDomain = tDomain as IRangeDomain;
                //������Сֵ/���ֵ
                tRangeDomain.MinValue = Convert.ToInt32(pXmlNode.Attributes["MinValue"].InnerText);
                tRangeDomain.MaxValue = Convert.ToInt32(pXmlNode.Attributes["MaxValue"].InnerText);
            }
            else
            {
                tDomain = new CodedValueDomainClass();
                //�ֶ�����
                tDomain.FieldType = (esriFieldType)Convert.ToInt32(pXmlNode.Attributes["FieldType"].InnerText);
                //��ѯ���ýӿ�
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

            //ͨ�ò��ָ�ֵ
            //����
            tDomain.Name = tDomainItemProperty.ItemName;  
            //������Ϣ
            tDomain.Description = tDomainItemProperty.ItemAliasName;    
            //�ϲ�����
            tDomain.MergePolicy = (esriMergePolicyType)Convert.ToInt32(pXmlNode.Attributes["MergePolicy"].InnerText);
            //�ָ����
            tDomain.SplitPolicy = (esriSplitPolicyType)Convert.ToInt32(pXmlNode.Attributes["SplitPolicy"].InnerText);

            tDomainItemProperty.Domain = tDomain;
            return tDomainItemProperty;
        }

        /// <summary>
        /// ��ȡͨ���ֶε�����ֵ
        /// </summary>
        /// <param name="pXmlNode">XmlNode�ڵ�</param>
        /// <param name="pXmlDoc">XmlDocument�ĵ�����</param>
        /// <returns>����FieldItemProperty����</returns>
        private FieldItemProperty ReadFieldItemAttribute(XmlNode pXmlNode,XmlDocument pXmlDoc)
        {
            //ʵ���ֶ�������
            FieldItemProperty fieldItemProperty = new FieldItemProperty();
            //����
            fieldItemProperty.ItemName = pXmlNode.Attributes["ItemName"].InnerText;   
            //����
            fieldItemProperty.ItemAliasName = pXmlNode.Attributes["ItemAliasName"].InnerText;        
            //Ĭ��ֵ
            fieldItemProperty.DefaultValue = pXmlNode.Attributes["DefaultValue"].InnerText;
            //�Ƿ�����Ϊ��
            fieldItemProperty.IsNull = Boolean.Parse(pXmlNode.Attributes["IsNull"].InnerText);
            //�ֶ�����
            fieldItemProperty.FieldType = (EnumFieldItems)Convert.ToInt32(pXmlNode.Attributes["FieldType"].InnerText);
            //����
            fieldItemProperty.Length = Convert.ToInt32(pXmlNode.Attributes["Length"].InnerText);
            //����
            fieldItemProperty.Precision = Convert.ToInt32(pXmlNode.Attributes["Precision"].InnerText);
            //С��λ��
            fieldItemProperty.Scale = Convert.ToInt32(pXmlNode.Attributes["Scale"].InnerText);

            #region ��ȡ������
            //����������
            string strDomainName = pXmlNode.Attributes["Domain"].InnerText;
            if (strDomainName.Length > 0)
            {
                string xPath = "DataBaseConfig/���ݿ�/ר��������/������";
                //�õ������򼯺Ͻڵ�
                XmlNodeList xmlNodeList = pXmlDoc.SelectNodes(xPath);
                //���������򼯺Ͻڵ�
                foreach (XmlNode xmlNode in xmlNodeList)
                {
                    //��ѯ������ͬ��������
                    if (xmlNode.Attributes["ItemName"].InnerText == strDomainName)
                    {
                        //��ȡ�ڵ���Ϣ��������
                        fieldItemProperty.DomainItemProperty = ReadDomainAttribute(xmlNode);
                        break;
                    }
                }
            }
            #endregion

            return fieldItemProperty;                
        }

        /// <summary>
        /// ItemProperty�����ô˷�����֪ͨDBTreeControl����������ֵ�ѷ����ı�
        /// </summary>
        /// <param name="sender">����</param>
        /// <param name="e">ItemPropertyEventArgs�¼�����</param>
        private void ItemPropertyValueChanged(object sender, ItemPropertyEventArgs e)
        {
            ItemProperty itemProperty = e.ItemProperty;
            TreeNode selTreeNode = this.m_dbTreeView.SelectedNode;
            selTreeNode.Text = string.Format("{0}:{1}({2})", DBInfoHandler.GetDataNodeDescripble(itemProperty.DataNodeItem), itemProperty.ItemName, itemProperty.ItemAliasName);

            #region ���ý���ͼƬ����ֵ
            GeometryFieldItemProperty geoFieldItemProperty = itemProperty as GeometryFieldItemProperty;
            if (geoFieldItemProperty != null)
            {
                if (geoFieldItemProperty.GeometryType == EnumGeometryItems.�� || geoFieldItemProperty.GeometryType == EnumGeometryItems.���)
                {
                    selTreeNode.Parent.ImageIndex = 3;
                    selTreeNode.Parent.SelectedImageIndex = 3;
                }
                else if (geoFieldItemProperty.GeometryType == EnumGeometryItems.��)
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
                    if (tItemProperty.FeatureType == EnumFeatureType.��Ҫ����)
                    {
                        //�޸��ӽڵ�

                        selTreeNode.ImageIndex = 3;
                        selTreeNode.SelectedImageIndex = 3;
                    }
                    else if (tItemProperty.FeatureType == EnumFeatureType.ע��Ҫ����)
                    {
                        selTreeNode.ImageIndex = 9;
                        selTreeNode.SelectedImageIndex = 9;
                    }
                }
            }

            //ˢ�����Կ����
            this.m_propertyGrid.Refresh();
        }

        #region �޸�ע�ǽڵ�����(Rewrite xubin 20120203)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pNode"></param>
        private void UpdataAnnoNode(TreeNode pNode)
        {
        }
        #endregion

        #region ��д�ռ�ο� ˽�д�����
        /// <summary>
        /// ��ָ���Ŀռ�ο�����ϵת��Ϊ�ַ���
        /// </summary>
        /// <param name="pSpatialReferenc">ָ���Ŀռ�ο�</param>
        private string CoordinateSystemToString(ISpatialReference pSpatialReference)
        {
            string strSR = "";              
            int cBytesWrote;

            //�˴�һ��Ҫ����IESRISpatialReferenceGEN�ӿ�,
            //��������IESRISpatialReference�˽ӿڽ���������Ԥ�����
            //��ѯ���ýӿ�
            IESRISpatialReferenceGEN tESRISpatialReference = pSpatialReference as IESRISpatialReferenceGEN;
            //����ռ�ο���Ϣ
            tESRISpatialReference.ExportToESRISpatialReference(out strSR, out cBytesWrote);                   
            
            return strSR;
        }

        /// <summary>
        /// ��ָ���Ŀռ�ο�XY��ת��Ϊ�ַ���
        /// </summary>
        /// <param name="pSpatialReferenc">ָ���Ŀռ�ο�</param>
        private string XYDomainToString(ISpatialReference pSpatialReference)
        {
            if (pSpatialReference.HasXYPrecision() == true)
            {
                double xMin, xMax, yMin, yMax, xyResolution;            
                //�õ�XY�����ȡֵ��Χ
                pSpatialReference.GetDomain(out xMin,out xMax, out yMin,out yMax);
                //��ѯISpatialReferenceResolution�ӿ�
                ISpatialReferenceResolution tSRResolution = pSpatialReference as ISpatialReferenceResolution;
                //�õ�Z�᷽��ķֱ���
                xyResolution = tSRResolution.get_XYResolution(false);    

                return string.Format("{0},{1},{2},{3},{4}", xMin, xMax, yMin, yMax, xyResolution);
            }
            else
                return "";            
        }

        /// <summary>
        /// ��ָ���Ŀռ�ο�Z��ת��Ϊ�ַ���
        /// </summary>
        /// <param name="pSpatialReferenc">ָ���Ŀռ�ο�</param>
        private string ZDomainToString(ISpatialReference pSpatialReference)
        {
            if (pSpatialReference.HasZPrecision()==true)
            {
                double zMin, zMax,zResolution;
                //�õ�Zֵ��Χ
                pSpatialReference.GetZDomain(out zMin, out zMax);
                //��ѯISpatialReferenceResolution�ӿ�
                ISpatialReferenceResolution tSRResolution = pSpatialReference as ISpatialReferenceResolution;
                //�õ�Z�᷽��ķֱ���
                zResolution=tSRResolution.get_ZResolution(false);

                return string.Format("{0},{1},{2}", zMin, zMax, zResolution);
            }
            else
                return "";
        }

        /// <summary>
        /// ��ָ���Ŀռ�ο�M��ת��Ϊ�ַ���
        /// </summary>
        /// <param name="pSpatialReferenc">ָ���Ŀռ�ο�</param>
        private string MDomainToString(ISpatialReference pSpatialReference)
        {
            if (pSpatialReference.HasMPrecision()==true)
            {
                double MMin, MMax, MResolution;
                //��ȡMֵ��Χ
                pSpatialReference.GetMDomain(out MMin, out MMax);
                //��ѯISpatialReferenceResolution�ӿ�
                ISpatialReferenceResolution tSRResolution = pSpatialReference as ISpatialReferenceResolution;
                //�õ�M����ķֱ���
                MResolution = tSRResolution.MResolution;

                return string.Format("{0},{1},{2}", MMin, MMax, MResolution);
            }
            else
                return "";
        }

        /// <summary>
        /// ��ָ��������Ϣ�����ռ�ο�
        /// </summary>
        /// <param name="pSpaBuffer">������Ϣ</param>
        /// <param name="pSpatialReference">�ռ�ο�</param>
        private void CreateESRISpatialReference(string pSpaBuffer, out ISpatialReference pSpatialReference)
        {
            if (pSpaBuffer.Trim().Length == 0)
            {
                pSpatialReference = new UnknownCoordinateSystemClass();
            }
            else
            {
                int cBytesRead;
                //ʵ���ռ�ο�������
                ISpatialReferenceFactory2 spatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                spatialReferenceFactory.CreateESRISpatialReference(pSpaBuffer, out pSpatialReference, out cBytesRead);
            }
        }

        /// <summary>
        /// ��ָ��XY����Ϣ����XY��
        /// </summary>
        /// <param name="pXYDomain">XY����Ϣ</param>
        /// <param name="pSpatialReference">�ռ�ο�</param>
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
                //��ѯISpatialReferenceResolution�ӿ�
                ISpatialReferenceResolution tSRResolution = pSpatialReference as ISpatialReferenceResolution;
                //���÷ֱ���
                tSRResolution.set_XYResolution(false, xyResolution);    
            }
        }

        /// <summary>
        /// ��ָ��M����Ϣ���ÿռ�ο�M��
        /// </summary>
        /// <param name="pMDomain">M����Ϣ</param>
        /// <param name="pSpatialReference">�ռ�ο�</param>
        private void SetMDomain(string pMDomain, ref ISpatialReference pSpatialReference)
        {
            if (pMDomain.Trim().Length > 0 && (pMDomain.Split(',').Length == 3))
            {
                double MMin, MMax, MResolution;
                string[] strMDomain = pMDomain.Split(',');
                MMin = Convert.ToDouble(strMDomain[0]);
                MMax = Convert.ToDouble(strMDomain[1]);
                MResolution = Convert.ToDouble(strMDomain[2]);

                //����M�������Сֵ
                pSpatialReference.SetMDomain(MMin, MMax);
                //��ѯISpatialReferenceResolution�ӿ�
                ISpatialReferenceResolution tSRResolution = pSpatialReference as ISpatialReferenceResolution;
                //���÷ֱ���
                tSRResolution.MResolution=MResolution;      
            }
        }

        /// <summary>
        /// ��ָ��Z����Ϣ���ÿռ�ο�Z��
        /// </summary>
        /// <param name="pZDomain">Z����Ϣ</param>
        /// <param name="pSpatialReference">�ռ�ο�</param>
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
                //��ѯISpatialReferenceResolution�ӿ�
                ISpatialReferenceResolution tSRResolution = pSpatialReference as ISpatialReferenceResolution;
                //���÷ֱ���
                tSRResolution.set_ZResolution(false,zResolution);               
            }
        }
        #endregion
    }
}
