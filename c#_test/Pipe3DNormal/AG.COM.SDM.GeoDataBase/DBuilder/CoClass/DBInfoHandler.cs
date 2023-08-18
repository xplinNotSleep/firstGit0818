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
    /// ������Ϣ�ļ�������
    /// </summary>
    public class DBInfoHandler
    {
        private TreeView m_Treeview;                                            //���οؼ�
        private PropertyGrid m_Propertygrid;                                    //���Կؼ�
        private IList<TreeNode> m_CheckedTreeNodes = new List<TreeNode>();        //��ѡ�е����ڵ�
        private string m_SufficName = "";                                       //��׺��     

        /// <summary>
        /// Ĭ�Ϲ��캯�� 
        /// </summary>
        /// <param name="ptreeview">������</param>
        /// <param name="propertygrid">���Կ����</param>
        public DBInfoHandler(TreeView ptreeview, PropertyGrid propertygrid)
        {
            this.m_Treeview = ptreeview;
            this.m_Propertygrid = propertygrid;
        }

        /// <summary>
        /// ��ȡ���Խ�����͵�����
        /// </summary>
        /// <param name="dataNodeItem">���Խ������<see cref="EnumDataNodeItems"/></param>
        /// <returns>����������Ϣ</returns>
        public static string GetDataNodeDescripble(EnumDataNodeItems dataNodeItem)
        {
            switch (dataNodeItem)
            {
                case EnumDataNodeItems.DataBaseItem:
                    return "���ݿ�";
                case EnumDataNodeItems.SubjectChildItem:
                    return "ר���ӿ�";
                case EnumDataNodeItems.DataSetItem:
                    return "Ҫ�ؼ�";
                case EnumDataNodeItems.FeatureClassItem:
                    return "Ҫ����";
                case EnumDataNodeItems.CustomTableItem:
                    return "���Ա�";
                case EnumDataNodeItems.GeometryFieldItem:
                    return "�����ֶ�";
                case EnumDataNodeItems.SubjectDomainItem:
                    return "ר��������";
                case EnumDataNodeItems.DomainItem:
                    return "������";
                default:
                    return "�ֶ�";
            }
        }

        /// <summary>
        /// �������ݽ�����ʹ������
        /// </summary>
        /// <param name="dataNodeItem">���ݽ������</param>
        /// <returns>����TreeNode</returns>
        public TreeNode CreateTreeNode(EnumDataNodeItems dataNodeItem)
        {
            //��ʼ�����Դ���������ʵ��
            ItemPropertyFactory itemPropertyFactory = new ItemPropertyFactory();
            //����������
            ItemProperty itemProperty = itemPropertyFactory.Create(dataNodeItem);
            //���������ı��¼�
            itemProperty.ItemPropertyValueChanged += new ItemPropertyValueChangedEventHandler(ItemPropertyValueChanged);
            //ʵ�������ڵ�
            TreeNode treeNode = new TreeNode();
            treeNode.Text = string.Format("{0}:{1}({2})", GetDataNodeDescripble(dataNodeItem), itemProperty.ItemName, itemProperty.ItemAliasName);
            treeNode.Tag = itemProperty;
            return treeNode;
        }

        /// <summary>
        /// �����������÷������ļ�
        /// </summary>
        /// <param name="filepath">�ļ�·��</param>
        public bool SaveDBInfoToFile(string filepath)
        {
            try
            {
                if (filepath.Trim().Length == 0)
                {
                    //���Ϊ�������÷������ļ�
                    SaveAsDBInfoToFile(ref filepath);
                }
                else
                {
                    if (System.IO.File.Exists(filepath) == true)
                        System.IO.File.Delete(filepath);

                    //ʵ���� ����������Ϣ��д�� ����
                    DbInfoReadWrite dbInfoConfig = new DbInfoReadWrite(this.m_Treeview, this.m_Propertygrid);
                    //д�����ݿ�������Ϣ���ļ�
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
        /// ���Ϊ�������÷������ļ�
        /// </summary>
        /// <param name="filepath">�ļ�·��</param>
        public void SaveAsDBInfoToFile(ref string filepath)
        {
            try
            {
                SaveFileDialog tSaveFileDlg = new SaveFileDialog();
                tSaveFileDlg.Filter = "���ݿ��׼�ļ�(*.dml)|*.dml";
                tSaveFileDlg.Title = "ָ������·��";

                if (tSaveFileDlg.ShowDialog() == DialogResult.OK)
                {
                    filepath = tSaveFileDlg.FileName;
                    //ʵ���� ����������Ϣ��д�� ����
                    DbInfoReadWrite dbInfoConfig = new DbInfoReadWrite(this.m_Treeview, this.m_Propertygrid);
                    //д�����ݿ�������Ϣ���ļ�
                    dbInfoConfig.WriteDBInfoToFile(filepath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// ItemProperty�����ô˷�����֪ͨDBTreeControl����������ֵ�ѷ����ı�
        /// </summary>
        /// <param name="sender">����</param>
        /// <param name="e">ItemPropertyEventArgs�¼�����</param>
        private void ItemPropertyValueChanged(object sender, ItemPropertyEventArgs e)
        {
            ItemProperty itemProperty = e.ItemProperty;
            TreeNode selTreeNode = this.m_Treeview.SelectedNode;
            selTreeNode.Text = string.Format("{0}:{1}({2})", GetDataNodeDescripble(itemProperty.DataNodeItem), itemProperty.ItemName, itemProperty.ItemAliasName);

            #region  ���ý���ͼƬ����ֵ
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

            //ˢ��������ͼ
            this.m_Propertygrid.Refresh();
        }

        /// <summary>
        /// �ݹ��ȡ���б�ѡ�еĽڵ�
        /// </summary>
        /// <param name="pTreeNode">��ǰ���ڵ�</param>
        /// <param name="pEnumDataNodeItem">�ڵ�����</param>
        /// <param name="pCheckTreeNodes">���� ѡ�нڵ㼯��</param>
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

                //�ݹ��ȡ���б�ѡ�еĽڵ�
                GetCheckedTreeNode(treeNode, pEnumDataNodeItem, ref pCheckTreeNodes);
            }
        }

        /// <summary>
        /// �õ��ֶμ���
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
        /// �õ��ֶμ���
        /// </summary>
        /// <param name="pTreeNodes">�ֶ����ڵ㼯��</param>
        /// <returns>����IFields</returns>
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
                    //ʵ�����ռ�����ֶ�
                    IGeometryDefEdit tGeometryDef = new GeometryDefClass();
                    //���ÿռ��������
                    tGeometryDef.GeometryType_2 = (esriGeometryType)tGeoItemProperty.GeometryType;
                    //���ÿռ�ο�
                    //tGeometryDef.SpatialReference_2 = tGeoItemProperty.GeoReference;

                    //�����ֶ�����
                    tFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                    //���ÿռ�����ֶ�
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
        /// ����������Ϣ��ָ���Ĺ����ռ��д���Ҫ�ؼ�
        /// </summary>
        /// <param name="pFeatureWorkspace">GeoDataBase�����ռ�</param>
        /// <param name="pTrackProgress">�������Ի���</param>
        private void CreateDatasetByDBInfo(TreeNode rootNode, IFeatureWorkspace pFeatureWorkspace, ITrackProgress pTrackProgress)
        {
            IList<TreeNode> tCheckTreeNodes = new List<TreeNode>();

            #region ��ȡ����ѡ�еĽڵ�
            foreach (TreeNode treeNode in rootNode.Nodes)
            {
                if (treeNode.Checked == true)
                {
                    //�ݹ��ȡ���б�ѡ�еĽڵ�
                    GetCheckedTreeNode(treeNode, EnumDataNodeItems.DataSetItem, ref tCheckTreeNodes);
                }
            }
            #endregion

            //���ø�������״̬
            pTrackProgress.TotalValue += 1;
            pTrackProgress.TotalMessage = "���ڴ���Ҫ�ؼ�����";
            pTrackProgress.SubMax = tCheckTreeNodes.Count;

            int i = 0;  //��ʱ����

            #region ѭ������Ҫ�ؼ�
            foreach (TreeNode treeNode in tCheckTreeNodes)
            {
                //����ת��
                DataSetItemProperty tDSItemProperty = treeNode.Tag as DataSetItemProperty;

                //�����ӽ�����״̬
                pTrackProgress.SubValue = (i++);
                pTrackProgress.SubMessage = string.Format("���ڴ���[{0}]Ҫ�ؼ�����", tDSItemProperty.ItemAliasName);
                Application.DoEvents();

                //���ݼ�����
                string strDatasetName = string.Format("{0}{1}", tDSItemProperty.ItemName, this.m_SufficName);

                //�ж����ݼ��Ƿ����
                bool IsExist = (pFeatureWorkspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureDataset, strDatasetName);

                #region �Ѵ���Ҫ�ؼ������ ע�͵�
                //if (IsExist == true)
                //{
                //    DialogResult tDlgResult = MessageBox.Show(string.Format("[{0}] Ҫ�ؼ��Ѿ�����,�Ƿ�ȷ��ɾ���ؽ���", itemProperty.ItemName), "��ʾ",
                //                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                //    //ȷ��ɾ�������ؽ�
                //    if (tDlgResult == DialogResult.Yes)
                //    {         
                //        //ɾ���Ѿ����ڵ�Ҫ�ؼ�
                //        GeoDBHelper.DeleteDatasetByName(tDSItemProperty.ItemName, pFeatureWorkspace, esriDatasetType.esriDTFeatureDataset);
                //    }
                //}
                #endregion

                if (IsExist == false)
                    //���Ҫ�ؼ��������򴴽�          
                    GeoDBHelper.CreateFeatureDataset(pFeatureWorkspace, strDatasetName, tDSItemProperty.GeoReference);
            }
            #endregion
        }

        /// <summary>
        /// ����������Ϣ��ָ���Ĺ����ռ��д������Ա�
        /// </summary>
        /// <param name="pFeatureWorkspace">GeoDataBase�����ռ�</param>
        /// <param name="pTrackProgress">�������Ի���</param>
        private void CreateCustomTableByDBInfo(TreeNode rootNode, IFeatureWorkspace pFeatureWorkspace, ITrackProgress pTrackProgress)
        {
            IList<TreeNode> tCheckTreeNodes = new List<TreeNode>();

            #region ��ȡ����ѡ�еĽڵ�
            foreach (TreeNode treeNode in rootNode.Nodes)
            {
                if (treeNode.Checked == true)
                {
                    //�ݹ��ȡ���б�ѡ�еĽڵ�
                    GetCheckedTreeNode(treeNode, EnumDataNodeItems.CustomTableItem, ref tCheckTreeNodes);
                }
            }
            #endregion

            //���ø�������״̬
            pTrackProgress.TotalValue += 1;
            pTrackProgress.TotalMessage = "���ڴ������Ա���";
            pTrackProgress.SubMax = tCheckTreeNodes.Count;

            int i = 0;  //��ʱ����

            #region ѭ���������Ա�
            foreach (TreeNode treeNode in tCheckTreeNodes)
            {
                //���Ա���
                ItemProperty itemProperty = treeNode.Tag as ItemProperty;

                //�����ӽ�����״̬
                pTrackProgress.SubValue = (i++);
                pTrackProgress.SubMessage = string.Format("���ڴ���[{0}]���Ա���", itemProperty.ItemAliasName);
                Application.DoEvents();

                //��ȡ�ֶμ���
                IFields tFields = GetFields(treeNode.Nodes);

                string strCustomTableName = string.Format("{0}{1}", itemProperty.ItemName, this.m_SufficName);
                //�ж�Ҫ�����Ƿ��Ѿ�����
                bool IsExist = (pFeatureWorkspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTTable, strCustomTableName);

                #region �Ѵ������Ա����� ע�͵�
                //if (IsExist == true)
                //{
                //    DialogResult tDlgResult=MessageBox.Show(string.Format("[{0}] ���Ա��Ѿ�����,�Ƿ�ȷ��ɾ���ؽ���", itemProperty.ItemName), "��ʾ", 
                //        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);                    
                //    //ȷ��ɾ�������ؽ�
                //    if (tDlgResult == DialogResult.Yes)
                //    {
                //        //ɾ���Ѿ����ڵ����Ա�
                //        GeoDBHelper.DeleteDatasetByName(itemProperty.ItemName, pFeatureWorkspace, esriDatasetType.esriDTTable);
                //    }                      
                //}
                #endregion

                if (IsExist == false)
                {
                    //�������Ա�
                    GeoDBHelper.CreateTable(pFeatureWorkspace, strCustomTableName, tFields, null, null, "");
                }
            }
            #endregion
        }

        /// <summary>
        /// ����������Ϣ��ָ���Ĺ����ռ��д���Ҫ����
        /// </summary>
        /// <param name="pFeatureWorkspace">GeoDataBase�����ռ�</param>
        /// <param name="pTrackProgress">�������Ի���</param>
        private void CreateFeatureClassByDBInfo(TreeNode rootNode, IFeatureWorkspace pFeatureWorkspace, ITrackProgress pTrackProgress)
        {
            IList<TreeNode> tCheckTreeNodes = new List<TreeNode>();

            #region ��ȡ����ѡ�еĽڵ�
            foreach (TreeNode treeNode in rootNode.Nodes)
            {
                if (treeNode.Checked == true)
                {
                    //�ݹ��ȡ���б�ѡ�еĽڵ�
                    GetCheckedTreeNode(treeNode, EnumDataNodeItems.FeatureClassItem, ref tCheckTreeNodes);
                }
            }
            #endregion

            //���ø�������״̬
            pTrackProgress.TotalValue += 1;
            pTrackProgress.TotalMessage = "���ڴ���Ҫ���࡭��";
            pTrackProgress.SubMax = tCheckTreeNodes.Count;

            int i = 0;  //��ʱ����

            #region ѭ������Ҫ����
            foreach (TreeNode treeNode in tCheckTreeNodes)
            {
                //Ҫ����������
                FeatureClassItemProperty tFeatItemProperty = treeNode.Tag as FeatureClassItemProperty;

                //�����ӽ�����״̬
                pTrackProgress.SubValue = (i++);
                pTrackProgress.SubMessage = string.Format("���ڴ���[{0}]Ҫ���࡭��", tFeatItemProperty.ItemAliasName);
                Application.DoEvents();

                //��ȡ�ֶμ���
                IFields tFields = GetFields(treeNode.Nodes);
                DataSetItemProperty tDSItemProperty = treeNode.Parent.Tag as DataSetItemProperty;
                IFeatureDataset tFeatDataset = null;
                if (tDSItemProperty != null)
                {
                    tFeatDataset = pFeatureWorkspace.OpenFeatureDataset(tDSItemProperty.ItemName + this.m_SufficName);
                }

                string strFeatClassName = string.Format("{0}{1}", tFeatItemProperty.ItemName, this.m_SufficName);

                //�ж�Ҫ�����Ƿ��Ѿ�����
                bool IsExist = (pFeatureWorkspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureClass, strFeatClassName);

                if (IsExist == false)
                {
                    //����Ҫ����
                    IFeatureClass tFeatureClass = null;
                    if (tFeatItemProperty.FeatureType != EnumFeatureType.ע��Ҫ����)
                    {
                        //����Ҫ����
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
                        //ʵ����������
                        IGraphicsLayerScale graphicLayerScale = new GraphicsLayerScaleClass();
                        //���õ�λ
                        if (tDSItemProperty != null && tDSItemProperty.GeoReference is IGeographicCoordinateSystem)
                            graphicLayerScale.Units = esriUnits.esriDecimalDegrees;
                        else
                            graphicLayerScale.Units = esriUnits.esriMeters;

                        //���òο�����
                        graphicLayerScale.ReferenceScale = tFeatItemProperty.RefrenceScale;

                        //����ע����
                        tFeatureClass = GeoDBHelper.CreateFeatureAnnoClass(pFeatureWorkspace, strFeatClassName, tFields, "", tFeatDataset, null, null, graphicLayerScale, null, true);
                    }

                    //�ж�Ҫ����������Ƿ�Ϊ��
                    if (tFeatureClass != null && tFeatItemProperty.ItemAliasName.Trim().Length > 0)
                    {
                        //��ѯIClassSchemaEdit�ӿ�
                        IClassSchemaEdit tClassSchemaEdit = tFeatureClass as IClassSchemaEdit;
                        //�޸ı���
                        tClassSchemaEdit.AlterAliasName(tFeatItemProperty.ItemAliasName);
                    }
                }

            }
            #endregion
        }

        /// <summary>
        /// ��ָ����SDE�����ռ��д���������
        /// </summary>
        /// <param name="pFeatureWorkspace">ָ����SDE�����ռ�</param>
        /// <param name="pTrackProgress">�������Ի���</param>
        private void CreateWorkspaceDomains(TreeNode rootNoe, IFeatureWorkspace pFeatureWorkspace, ITrackProgress pTrackProgress)
        {
            IList<TreeNode> tCheckTreeNodes = new List<TreeNode>();

            #region ��ȡ����ѡ�еĽڵ�
            foreach (TreeNode treeNode in rootNoe.Nodes)
            {
                if (treeNode.Checked == true)
                {
                    //�ݹ��ȡ���б�ѡ�еĽڵ�
                    GetCheckedTreeNode(treeNode, EnumDataNodeItems.DomainItem, ref tCheckTreeNodes);
                }
            }
            #endregion

            //���ø�������״̬
            pTrackProgress.TotalValue += 1;
            pTrackProgress.TotalMessage = "���ڴ��������򡭡�";
            pTrackProgress.SubMax = tCheckTreeNodes.Count;

            int i = 0;  //��ʱ����

            #region ѭ������Ҫ����
            foreach (TreeNode treeNode in tCheckTreeNodes)
            {
                //����ת��
                DomainItemProperty itemProperty = treeNode.Tag as DomainItemProperty;
                //��ѯ�ýӿڵ�����
                IWorkspaceDomains tWorkspaceDomains = pFeatureWorkspace as IWorkspaceDomains;
                //��������������ȡ������
                IDomain tDomain = tWorkspaceDomains.get_DomainByName(itemProperty.ItemName);

                //�����ӽ�����״̬
                pTrackProgress.SubValue = (i++);
                pTrackProgress.SubMessage = string.Format("���ڴ���[{0}]�����򡭡�", itemProperty.ItemAliasName);
                Application.DoEvents();

                #region �Ѵ������������� ע�͵�
                ////��������������
                //if (tDomain != null)
                //{
                //    //��ȡ�Ի����״̬
                //    DialogResult tDlgResult=MessageBox.Show(string.Format("{0} ���Ѿ�����,�Ƿ�ɾ���ؽ���", itemProperty.ItemName), "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                //    //���ȷ��ɾ���ؽ���������������Ȩ�ޣ���ɾ����������
                //    if (tDlgResult == DialogResult.Yes && tWorkspaceDomains.get_CanDeleteDomain(itemProperty.ItemName))
                //    {
                //        tWorkspaceDomains.DeleteDomain(itemProperty.ItemName);
                //    }
                //}
                #endregion

                if (tDomain == null)
                {
                    //���������
                    tWorkspaceDomains.AddDomain(itemProperty.Domain);
                }
            }
            #endregion
        }
        private string qz = "���ݿ�:";


        public void CreateDataBaseByDBInfo(string savePath, string strSufficName, bool isCover)
        {
            //Ŀ���ļ����Ѿ����ڵ������ļ�
            List<string> coverdList = new List<string>();
            if (!isCover)
            {
                //�ҳ�Ŀ��·�����ڵ��ظ��ļ�������������������ʱ�򵯳���ʾ����Щ���ݿ���Ϊ�����������ݿ����Դ���ʧ��
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
                //���򸲸ǵ����ݿ⣬ֱ��ɾ�������ɾ��ʧ����ʾ����
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

                    //ѭ�������������ݿ⣬��errorList����Ĳ��ٴ���
                    foreach (TreeNode treeNode in m_Treeview.Nodes)
                    {
                        if (treeNode.Checked)
                        {
                            //��ѯIFeatureWorkspace����
                            IWorkspace pWorkspace = CreateWorkspace(savePath + "\\" + treeNode.Text.Replace(qz, "") + ".mdb");
                            if (pWorkspace == null)
                            {
                                //�������ʧ�ܼ�¼����
                                errorList.Add(treeNode.Text.Replace(qz, ""));
                            }
                            IFeatureWorkspace tFeatWorkspace = pWorkspace as IFeatureWorkspace;

                            if (tFeatWorkspace != null)
                            {
                                //����������
                                CreateWorkspaceDomains(treeNode, tFeatWorkspace, tTrackProgress);
                                //�������ݼ�
                                CreateDatasetByDBInfo(treeNode, tFeatWorkspace, tTrackProgress);
                                //����Ҫ����
                                CreateFeatureClassByDBInfo(treeNode, tFeatWorkspace, tTrackProgress);
                                //�������Ա�
                                CreateCustomTableByDBInfo(treeNode, tFeatWorkspace, tTrackProgress);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //���øô��岻Ϊ���㴰��
                    (tTrackProgress as Form).TopMost = false;
                    MessageBox.Show(ex.Message, "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show($"Ŀ��·���Ѿ��������ݿ�[{temp}]", "���ݿⴴ��ʧ��", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// ����mdb���ݿ�
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
            //Ŀ���ļ����Ѿ����ڵ������ļ�
            List<string> coverdList = new List<string>();
            List<string> errorList = new List<string>();
            if (!isCover)
            {
                //�ҳ�Ŀ��·�����ڵ��ظ��ļ�������������������ʱ�򵯳���ʾ����Щ���ݿ���Ϊ�����������ݿ����Դ���ʧ��
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
                //���򸲸ǵ����ݿ⣬ֱ��ɾ�������ɾ��ʧ����ʾ����

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
                MessageBox.Show("���ݿ⸲��ʧ�ܣ�" + string.Join(",", errorList));
            }
            this.m_SufficName = strSufficName;
            ITrackProgress tTrackProgress = new TrackProgressDialog();
            tTrackProgress.DisplayTotal = true;
            tTrackProgress.TotalMax = 4;
            (tTrackProgress as Form).TopMost = true;
            tTrackProgress.Show();
            try
            {

                //ѭ�������������ݿ⣬��errorList����Ĳ��ٴ���
                foreach (TreeNode treeNode in m_Treeview.Nodes)
                {
                    if (treeNode.Checked)
                    {
                        //��ѯIFeatureWorkspace����
                        IWorkspace pWorkspace = CreateWorkspace(savePath + "\\" + treeNode.Text.Replace(qz, "") + ".mdb");
                        if (pWorkspace == null)
                        {
                            //�������ʧ�ܼ�¼����
                            errorList.Add(treeNode.Text.Replace(qz, ""));
                            continue;
                        }
                        IFeatureWorkspace tFeatWorkspace = pWorkspace as IFeatureWorkspace;

                        if (tFeatWorkspace != null)
                        {
                            //����������
                            CreateWorkspaceDomains(treeNode, tFeatWorkspace, tTrackProgress);
                            //�������ݼ�
                            if(isDataSet)
                            {
                                CreateDatasetByDBInfo(treeNode, tFeatWorkspace, tTrackProgress);
                            }

                            //����Ҫ����
                            CreateFeatureClassByDBInfo(treeNode, tFeatWorkspace, tTrackProgress, isAppend, isDataSet);
                            //�������Ա�
                            CreateCustomTableByDBInfo(treeNode, tFeatWorkspace, tTrackProgress);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //���øô��岻Ϊ���㴰��
                (tTrackProgress as Form).TopMost = false;
                MessageBox.Show(ex.Message, "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                tTrackProgress.AutoFinishClose = true;
                tTrackProgress.SetFinish();
            }
            if (coverdList.Count > 0)
            {
                string temp = string.Join(",", coverdList);
                MessageBox.Show($"Ŀ��·���Ѿ��������ݿ�[{temp}]������������", "���ݿⴴ��", MessageBoxButtons.OK);
            }
        }

        private void CreateFeatureClassByDBInfo(TreeNode rootNode, IFeatureWorkspace pFeatureWorkspace, ITrackProgress pTrackProgress, bool isAppend,bool isDataSet)
        {
            IList<TreeNode> tCheckTreeNodes = new List<TreeNode>();

            #region ��ȡ����ѡ�еĽڵ�
            foreach (TreeNode treeNode in rootNode.Nodes)
            {
                if (treeNode.Checked == true)
                {
                    //�ݹ��ȡ���б�ѡ�еĽڵ�
                    GetCheckedTreeNode(treeNode, EnumDataNodeItems.FeatureClassItem, ref tCheckTreeNodes);
                }
            }
            #endregion

            //���ø�������״̬
            pTrackProgress.TotalValue += 1;
            pTrackProgress.TotalMessage = "���ڴ���Ҫ���࡭��";
            pTrackProgress.SubMax = tCheckTreeNodes.Count;

            int i = 0;  //��ʱ����

            #region ѭ������Ҫ����
            foreach (TreeNode treeNode in tCheckTreeNodes)
            {
                //Ҫ����������
                FeatureClassItemProperty tFeatItemProperty = treeNode.Tag as FeatureClassItemProperty;

                //�����ӽ�����״̬
                pTrackProgress.SubValue = (i++);
                pTrackProgress.SubMessage = string.Format("���ڴ���[{0}]Ҫ���࡭��", tFeatItemProperty.ItemAliasName);
                Application.DoEvents();
                if (tFeatItemProperty.ItemAliasName == "�½�Ҫ����")
                    Console.WriteLine("");
                //��ȡ�ֶμ���
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

                //�ж�Ҫ�����Ƿ��Ѿ�����
                bool IsExist = (pFeatureWorkspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTFeatureClass, strFeatClassName);

                if (IsExist == false)
                {
                    //����Ҫ����
                    IFeatureClass tFeatureClass = null;
                    if (tFeatItemProperty.FeatureType != EnumFeatureType.ע��Ҫ����)
                    {
                        //����Ҫ����
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
                        //ʵ����������
                        IGraphicsLayerScale graphicLayerScale = new GraphicsLayerScaleClass();
                        //���õ�λ
                        if (tDSItemProperty != null && tDSItemProperty.GeoReference is IGeographicCoordinateSystem)
                            graphicLayerScale.Units = esriUnits.esriDecimalDegrees;
                        else
                            graphicLayerScale.Units = esriUnits.esriMeters;

                        //���òο�����
                        graphicLayerScale.ReferenceScale = tFeatItemProperty.RefrenceScale;

                        //����ע����
                        tFeatureClass = GeoDBHelper.CreateFeatureAnnoClass(pFeatureWorkspace, strFeatClassName, tFields, "", tFeatDataset, null, null, graphicLayerScale, null, true);
                    }

                    //�ж�Ҫ����������Ƿ�Ϊ��
                    if (tFeatureClass != null && tFeatItemProperty.ItemAliasName.Trim().Length > 0)
                    {
                        //��ѯIClassSchemaEdit�ӿ�
                        IClassSchemaEdit tClassSchemaEdit = tFeatureClass as IClassSchemaEdit;
                        //�޸ı���
                        tClassSchemaEdit.AlterAliasName(tFeatItemProperty.ItemAliasName);
                    }
                }

            }
            #endregion
        }

        /// <summary>
        /// ��ʼ���ֶε�������
        /// </summary>
        /// <param name="pFields"></param>
        /// <param name="isAppend"></param>
        private void InitFieldDomian(IFields pFields, bool isAppend)
        {
            if (!isAppend)
            {
                //ȡ�������ֶε�������
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
