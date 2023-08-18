using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System.IO;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// �������ݲ���������
    /// </summary>
    public class GeoDBHelper
    {
        /// <summary>
        /// ����Ҫ����
        /// </summary>
        /// <param name="pObject">IWorkspace����IFeatureDataset����</param>
        /// <param name="pName">Ҫ��������</param>
        /// <param name="pFeatureType">Ҫ������</param>
        /// <param name="pFields">�ֶμ�</param>
        /// <param name="pUidClsId">CLSIDֵ</param>
        /// <param name="pUidClsExt">EXTCLSIDֵ</param>
        /// <param name="pConfigWord">������Ϣ�ؼ���</param>
        /// <returns>����IFeatureClass</returns>
        public static IFeatureClass CreateFeatureClass(object pObject, string pName, esriFeatureType pFeatureType,
            IFields pFields, UID pUidClsId, UID pUidClsExt, string pConfigWord)
        {          
            #region ������
            if (pObject == null)
            {
                throw (new Exception("[pObject] ����Ϊ��!"));
            }
            if (!((pObject is IFeatureWorkspace) || (pObject is IFeatureDataset)))
            {
                throw (new Exception("[pObject] ����ΪIFeatureWorkspace ���� IFeatureDataset"));
            }
            if (pName.Length == 0)
            {
                throw (new Exception("[pName] ����Ϊ��!"));
            }
            if (pFields == null || pFields.FieldCount == 0)
            {
                throw (new Exception("[pFields] ����Ϊ��!"));
            }

            //���ζ����ֶ�����
            string strShapeFieldName = "";
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                if (pFields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                {
                    strShapeFieldName = pFields.get_Field(i).Name;
                    break;
                }
            }

            if (strShapeFieldName.Length == 0)
            {
                throw (new Exception("�ֶμ����Ҳ������ζ�����"));
            }

            #endregion

            #region pUidClsID�ֶ�Ϊ��ʱ
            if (pUidClsId == null)
            {
                pUidClsId = new UIDClass();
                switch (pFeatureType)
                {
                    case (esriFeatureType.esriFTSimple):
                        pUidClsId.Value = "{52353152-891A-11D0-BEC6-00805F7C4268}";
                        break;
                    case (esriFeatureType.esriFTSimpleJunction):
                        pUidClsId.Value = "{CEE8D6B8-55FE-11D1-AE55-0000F80372B4}";
                        break;
                    case (esriFeatureType.esriFTComplexJunction):
                        pUidClsId.Value = "{DF9D71F4-DA32-11D1-AEBA-0000F80372B4}";
                        break;
                    case (esriFeatureType.esriFTSimpleEdge):
                        pUidClsId.Value = "{E7031C90-55FE-11D1-AE55-0000F80372B4}";
                        break;
                    case (esriFeatureType.esriFTComplexEdge):
                        pUidClsId.Value = "{A30E8A2A-C50B-11D1-AEA9-0000F80372B4}";
                        break;
                    case (esriFeatureType.esriFTAnnotation):
                        pUidClsId.Value = "{E3676993-C682-11D2-8A2A-006097AFF44E}";
                        break;
                    case (esriFeatureType.esriFTDimension):
                        pUidClsId.Value = "{496764FC-E0C9-11D3-80CE-00C04F601565}";
                        break;
                }
            }
            #endregion

            #region pUidClsExt�ֶ�Ϊ��ʱ
            if (pUidClsExt == null)
            {
                switch (pFeatureType)
                {
                    case esriFeatureType.esriFTAnnotation:
                        pUidClsExt = new UIDClass();
                        pUidClsExt.Value = "{24429589-D711-11D2-9F41-00C04F6BC6A5}";
                        break;
                    case esriFeatureType.esriFTDimension:
                        pUidClsExt = new UIDClass();
                        pUidClsExt.Value = "{48F935E2-DA66-11D3-80CE-00C04F601565}";
                        break;
                }
            }

            #endregion

            IFeatureClass tFeatureClass = null;
            if (pObject is IWorkspace)
            {
                //����������FeatureClass
                IWorkspace tWorkspace = pObject as IWorkspace;

                //��ѯ���ýӿ�
                IFeatureWorkspace tFeatureWorkspace = tWorkspace as IFeatureWorkspace;
                tFeatureClass = tFeatureWorkspace.CreateFeatureClass(pName, pFields, pUidClsId, pUidClsExt, pFeatureType, strShapeFieldName, pConfigWord);
            }
            else if (pObject is IFeatureDataset)
            {
                //��Ҫ�ؼ��д���FeatureClass
                IFeatureDataset tFeatureDataset = (IFeatureDataset)pObject;
                tFeatureClass = tFeatureDataset.CreateFeatureClass(pName, pFields, pUidClsId, pUidClsExt, pFeatureType, strShapeFieldName, pConfigWord);
            }

            return tFeatureClass;
        }
        public static IFeatureClass CreateFeatureClass(object pObject, string pName, esriFeatureType pFeatureType,
        IFields pFields, UID pUidClsId, UID pUidClsExt, string pConfigWord, ISpatialReference spatialReference)
        {
            #region ������
            if (pObject == null)
            {
                throw (new Exception("[pObject] ����Ϊ��!"));
            }
            if (!((pObject is IFeatureWorkspace) || (pObject is IFeatureDataset)))
            {
                throw (new Exception("[pObject] ����ΪIFeatureWorkspace ���� IFeatureDataset"));
            }
            if (pName.Length == 0)
            {
                throw (new Exception("[pName] ����Ϊ��!"));
            }
            if (pFields == null || pFields.FieldCount == 0)
            {
                throw (new Exception("[pFields] ����Ϊ��!"));
            }

            //���ζ����ֶ�����
            string strShapeFieldName = "";
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                if (pFields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                {
                    strShapeFieldName = pFields.get_Field(i).Name;
                    break;
                }
            }

            if (strShapeFieldName.Length == 0)
            {
                throw (new Exception("�ֶμ����Ҳ������ζ�����"));
            }

            #endregion

            #region pUidClsID�ֶ�Ϊ��ʱ
            if (pUidClsId == null)
            {
                pUidClsId = new UIDClass();
                switch (pFeatureType)
                {
                    case (esriFeatureType.esriFTSimple):
                        pUidClsId.Value = "{52353152-891A-11D0-BEC6-00805F7C4268}";
                        break;
                    case (esriFeatureType.esriFTSimpleJunction):
                        pUidClsId.Value = "{CEE8D6B8-55FE-11D1-AE55-0000F80372B4}";
                        break;
                    case (esriFeatureType.esriFTComplexJunction):
                        pUidClsId.Value = "{DF9D71F4-DA32-11D1-AEBA-0000F80372B4}";
                        break;
                    case (esriFeatureType.esriFTSimpleEdge):
                        pUidClsId.Value = "{E7031C90-55FE-11D1-AE55-0000F80372B4}";
                        break;
                    case (esriFeatureType.esriFTComplexEdge):
                        pUidClsId.Value = "{A30E8A2A-C50B-11D1-AEA9-0000F80372B4}";
                        break;
                    case (esriFeatureType.esriFTAnnotation):
                        pUidClsId.Value = "{E3676993-C682-11D2-8A2A-006097AFF44E}";
                        break;
                    case (esriFeatureType.esriFTDimension):
                        pUidClsId.Value = "{496764FC-E0C9-11D3-80CE-00C04F601565}";
                        break;
                }
            }
            #endregion

            #region pUidClsExt�ֶ�Ϊ��ʱ
            if (pUidClsExt == null)
            {
                switch (pFeatureType)
                {
                    case esriFeatureType.esriFTAnnotation:
                        pUidClsExt = new UIDClass();
                        pUidClsExt.Value = "{24429589-D711-11D2-9F41-00C04F6BC6A5}";
                        break;
                    case esriFeatureType.esriFTDimension:
                        pUidClsExt = new UIDClass();
                        pUidClsExt.Value = "{48F935E2-DA66-11D3-80CE-00C04F601565}";
                        break;
                }
            }

            #endregion

            IFeatureClass tFeatureClass = null;
            if (pObject is IWorkspace)
            {
                //����������FeatureClass
                IWorkspace tWorkspace = pObject as IWorkspace;

                //��ѯ���ýӿ�
                IFeatureWorkspace tFeatureWorkspace = tWorkspace as IFeatureWorkspace;
                try
                {
                    //��������ݿ�ĸ��ڵ���ֱ�Ӵ�����������ϵ��Ҫ����ᵼ��Ҫ���ഴ��ʧ�ܣ����ԣ����ｫ����ϵ�����ó�������
                    pFields = VerifyFields(pFields, tWorkspace);
                    tFeatureClass = tFeatureWorkspace.CreateFeatureClass(pName, pFields, new UIDClass(), new UIDClass(), esriFeatureType.esriFTSimple, strShapeFieldName, pConfigWord);
                    IGeoDataset geoDataset = (IGeoDataset)tFeatureClass;
                    IGeoDatasetSchemaEdit geoDatasetSchemaEdit = (IGeoDatasetSchemaEdit)geoDataset;
                    if (geoDatasetSchemaEdit.CanAlterSpatialReference)
                    {
                        geoDatasetSchemaEdit.AlterSpatialReference(spatialReference);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Ҫ���ഴ��ʧ�ܣ�{pName} ;" + e.Message, "���ݿⴴ��");
                }
            }
            else if (pObject is IFeatureDataset)
            {
                //��Ҫ�ؼ��д���FeatureClass
                IFeatureDataset tFeatureDataset = (IFeatureDataset)pObject;
                try
                {
                    pFields = VerifyFields(pFields, tFeatureDataset.Workspace);
                    tFeatureClass = tFeatureDataset.CreateFeatureClass(pName, pFields, pUidClsId, pUidClsExt, pFeatureType, strShapeFieldName, pConfigWord);
                }
                catch (Exception e)
                {
                    MessageBox.Show($"Ҫ���ഴ��ʧ�ܣ�{pName} ;" + e.Message, "���ݿⴴ��");
                }
            }

            return tFeatureClass;
        }
        /// <summary>
        /// ���ֶν�����֤��������֤�ɹ����ֶμ���
        /// </summary>
        /// <param name="pFields"></param>
        /// <returns></returns>
        private static IFields VerifyFields(IFields pFields, IWorkspace pWorkspace)
        {
            IFieldChecker fieldChecker = new FieldCheckerClass();
            fieldChecker.InputWorkspace = pWorkspace;
            IFields fixedFields;
            IEnumFieldError error;
            fieldChecker.Validate(pFields, out error, out fixedFields);
            if (error != null)
            {
                IFieldError fieldError = error.Next();
                List<string> tempList = new List<string>();
                while (fieldError != null)
                {
                    tempList.Add(fieldError.FieldError.ToString());
                    fieldError = error.Next();
                }
            }
            return fixedFields;
        }
        /// <summary>
        /// ����Ҫ����
        /// </summary>
        /// <param name="pObject">IWorkspace����IFeatureDataset����</param>
        /// <param name="pName">Ҫ��������</param>
        /// <param name="pFeatureType">Ҫ������</param>
        /// <param name="pFields">�ֶμ�</param>
        /// <param name="pUidClsId">CLSIDֵ</param>
        /// <param name="pUidClsExt">EXTCLSIDֵ</param>
        /// <param name="pConfigWord">������Ϣ�ؼ���</param>
        /// <returns>����IFeatureClass</returns>
        public static IFeatureClass CreateFeatureClass(IFeatureClass tFeatureClassSource, object pObject, string tPathTarget, string tFileNameWithoutExt, esriFeatureType pFeatureType, UID pUidClsId, UID pUidClsExt, string pConfigWord, ref Dictionary<int, int> tFieldMatch)
        {
            //�¾��ֶ�����ƥ�䣬keyΪ���ֶ�������valueΪ���ֶ�����
            tFieldMatch = new Dictionary<int, int>();
            //���ֶ���������ֶ�Indexƥ��
            Dictionary<string, int> tNewNameWithOldIdx = new Dictionary<string, int>();

            #region ������
            if (pObject == null)
            {
                throw (new Exception("[pObject] ����Ϊ��!"));
            }
            if (!((pObject is IFeatureWorkspace) || (pObject is IFeatureDataset)))
            {
                throw (new Exception("[pObject] ����ΪIFeatureWorkspace ���� IFeatureDataset"));
            }
            if (tFileNameWithoutExt.Length == 0)
            {
                throw (new Exception("[tFileNameWithoutExt] ����Ϊ��!"));
            }
            if (tFeatureClassSource.Fields == null || tFeatureClassSource.Fields.FieldCount == 0)
            {
                throw (new Exception("[pFields] ����Ϊ��!"));
            }

            IWorkspace tWorkspaceTarget = null;
            if (pObject is IWorkspace)
            {
                tWorkspaceTarget = pObject as IWorkspace;
            }
            else if (pObject is IFeatureDataset)
            {
                IFeatureDataset tFeatureDataset = (IFeatureDataset)pObject;
                tWorkspaceTarget = tFeatureDataset.Workspace;
            }

            IFieldChecker tFieldChecker = new FieldCheckerClass();
            tFieldChecker.InputWorkspace = (tFeatureClassSource as IDataset).Workspace;
            tFieldChecker.ValidateWorkspace = tWorkspaceTarget as IWorkspace;

            IFields tFieldsValid = null;
            IEnumFieldError tEnumFieldError = null;
            tFieldChecker.Validate(tFeatureClassSource.Fields, out tEnumFieldError, out tFieldsValid);

            //���˵�Shapefile��֧�ֵ��ֶ�����
            IFieldsEdit tFieldsEditNew = new FieldsClass();
            for (int i = 0; i < tFieldsValid.FieldCount; i++)
            {
                IField tFieldValid = tFieldsValid.get_Field(i);              

                if (tFieldValid.Type != esriFieldType.esriFieldTypeBlob && tFieldValid.Type != esriFieldType.esriFieldTypeRaster &&
                    tFieldValid.Type != esriFieldType.esriFieldTypeXML &&
                    tFieldValid.Type != esriFieldType.esriFieldTypeGUID)
                {
                    tFieldsEditNew.AddField(tFieldValid);
                    //д�����ֶ���������ֶ�Indexƥ��
                    tNewNameWithOldIdx.Add(tFieldValid.Name, i);
                }
            }

            #endregion

            #region pUidClsID�ֶ�Ϊ��ʱ
            if (pUidClsId == null)
            {
                pUidClsId = new UIDClass();
                switch (pFeatureType)
                {
                    case (esriFeatureType.esriFTSimple):
                        pUidClsId.Value = "{52353152-891A-11D0-BEC6-00805F7C4268}";
                        break;
                    case (esriFeatureType.esriFTSimpleJunction):
                        pUidClsId.Value = "{CEE8D6B8-55FE-11D1-AE55-0000F80372B4}";
                        break;
                    case (esriFeatureType.esriFTComplexJunction):
                        pUidClsId.Value = "{DF9D71F4-DA32-11D1-AEBA-0000F80372B4}";
                        break;
                    case (esriFeatureType.esriFTSimpleEdge):
                        pUidClsId.Value = "{E7031C90-55FE-11D1-AE55-0000F80372B4}";
                        break;
                    case (esriFeatureType.esriFTComplexEdge):
                        pUidClsId.Value = "{A30E8A2A-C50B-11D1-AEA9-0000F80372B4}";
                        break;
                    case (esriFeatureType.esriFTAnnotation):
                        pUidClsId.Value = "{E3676993-C682-11D2-8A2A-006097AFF44E}";
                        break;
                    case (esriFeatureType.esriFTDimension):
                        pUidClsId.Value = "{496764FC-E0C9-11D3-80CE-00C04F601565}";
                        break;
                }
            }
            #endregion

            #region pUidClsExt�ֶ�Ϊ��ʱ
            if (pUidClsExt == null)
            {
                switch (pFeatureType)
                {
                    case esriFeatureType.esriFTAnnotation:
                        pUidClsExt = new UIDClass();
                        pUidClsExt.Value = "{24429589-D711-11D2-9F41-00C04F6BC6A5}";
                        break;
                    case esriFeatureType.esriFTDimension:
                        pUidClsExt = new UIDClass();
                        pUidClsExt.Value = "{48F935E2-DA66-11D3-80CE-00C04F601565}";
                        break;
                }
            }

            #endregion

            //��ֹ�ļ�����
            string tFileName = System.IO.Path.Combine(tPathTarget, tFileNameWithoutExt + ".shp");
            int k = 1;
            while (File.Exists(tFileName) == true)
            {
                tFileName = System.IO.Path.Combine(tPathTarget, tFileNameWithoutExt + "_" + k + ".shp");
                k++;
            }

            IFeatureClass tFeatureClassTarget = null;
            if (pObject is IWorkspace)
            {
                //����������FeatureClass
                IWorkspace tWorkspace = pObject as IWorkspace;

                //��ѯ���ýӿ�
                IFeatureWorkspace tFeatureWorkspace = tWorkspace as IFeatureWorkspace;
                tFeatureClassTarget = tFeatureWorkspace.CreateFeatureClass(System.IO.Path.GetFileNameWithoutExtension(tFileName), tFieldsEditNew, pUidClsId, pUidClsExt, pFeatureType, tFeatureClassSource.ShapeFieldName, pConfigWord);
            }
            else if (pObject is IFeatureDataset)
            {
                //��Ҫ�ؼ��д���FeatureClass
                IFeatureDataset tFeatureDataset = (IFeatureDataset)pObject;
                tFeatureClassTarget = tFeatureDataset.CreateFeatureClass(System.IO.Path.GetFileNameWithoutExtension(tFileName), tFieldsEditNew, pUidClsId, pUidClsExt, pFeatureType, tFeatureClassSource.ShapeFieldName, pConfigWord);
            }

            //�����¾��ֶ�Indexƥ�䣬��˴���FeatureClassʱ���ܻ������ֶΣ���˴���FeatureClass��������ֶ�������
            foreach (KeyValuePair<string, int> kvp in tNewNameWithOldIdx)
            {
                int tNewIdx = tFeatureClassTarget.Fields.FindField(kvp.Key);
                if (tNewIdx >= 0)
                {
                    tFieldMatch.Add(tNewIdx, kvp.Value);
                }
            }

            return tFeatureClassTarget;
        }

        /// <summary>
        /// �������Ա�
        /// </summary>
        /// <param name="pFeatWorkspace">IFeatureWorkspace����</param>
        /// <param name="pName">������</param>
        /// <param name="pFields">�ֶμ���</param>
        /// <param name="pUidClsId">CLSIDֵ</param>
        /// <param name="pUidClsExt">EXTCLSIDֵ</param>
        /// <param name="pConfigWord">������Ϣ�ؼ���</param>
        /// <returns>����ITable</returns>
        public static ITable CreateTable(IFeatureWorkspace pFeatWorkspace, string pName, IFields pFields, UID pUidClsId, UID pUidClsExt, string pConfigWord)
        {
            #region ������
            if (pFeatWorkspace == null)
            {
                throw (new Exception("[pFeatWorkspace] ����Ϊ��"));
            }

            if (pName.Length == 0)
            {
                throw (new Exception("[pName] ����Ϊ��"));
            }

            if (pUidClsId == null)
            {
                pUidClsId = new UIDClass();
                pUidClsId.Value = "esriGeoDatabase.Object";
            }
            #endregion

            if (pFields == null)
            {
                //ʵ�����ֶμ��϶���
                pFields = new FieldsClass();
                IFieldsEdit tFieldsEdit = (IFieldsEdit)pFields;

                //����OID�ֶ�
                IField fieldOID = new FieldClass();
                IFieldEdit fieldEditOID = fieldOID as IFieldEdit;
                fieldEditOID.Name_2 = "OBJECTID";
                fieldEditOID.AliasName_2 = "OBJECTID";
                fieldEditOID.IsNullable_2 = false;
                fieldEditOID.Type_2 = esriFieldType.esriFieldTypeOID;
                tFieldsEdit.AddField(fieldOID);
            }

            //�������Ա�
            ITable tTable = pFeatWorkspace.CreateTable(pName, pFields, pUidClsId, pUidClsExt, pConfigWord);

            return tTable;
        }

        /// <summary>
        /// ����Ҫ�ؼ�
        /// </summary>
        /// <param name="pFeatWorkspace">IFeatureWorkspace����</param>
        /// <param name="pName">Ҫ�ؼ�����</param>
        /// <param name="pSpatialReference">�ռ�ο���ϵ</param>
        /// <returns>����IFeatureDataset</returns>
        public static IFeatureDataset CreateFeatureDataset(IFeatureWorkspace pFeatWorkspace, string pName, ISpatialReference pSpatialReference)
        {
            #region ������
            if (pFeatWorkspace == null)
            {
                throw (new Exception("[pFeatWorkspace]����Ϊ��"));
            }
            if (pName.Trim().Length == 0)
            {
                throw (new Exception("[pName] ����Ϊ��"));
            }
            #endregion

            if (pSpatialReference == null)
            {
                pSpatialReference = new UnknownCoordinateSystemClass();
            }
            IFeatureDataset tFeatDataset = pFeatWorkspace.CreateFeatureDataset(pName, pSpatialReference);
            return tFeatDataset;
        }

        /// <summary>
        /// ����ע��Ҫ����
        /// </summary>
        /// <param name="pFeatWorkspace">SDE�����ռ�</param>
        /// <param name="pName">����</param>
        /// <param name="pFields">�ֶμ�</param>
        /// <param name="pConfigWord">�ؼ���</param>
        /// <param name="pDstFeatureDataset">Ŀ��Ҫ�ؼ�</param>
        /// <param name="pSrcFeatureClass">ԴҪ����</param>
        /// <param name="pAnnoProperties">ע�����Լ�</param>
        /// <param name="pReferenceScale">�ο�����</param>
        /// <param name="pSymbolCollection">��ʽ����</param>
        /// <param name="pAutoCreate">�Ƿ��Զ�����</param>
        /// <returns>���ش����õ�ע��Ҫ����</returns>
        public static IFeatureClass CreateFeatureAnnoClass(IFeatureWorkspace pFeatWorkspace, string pName, IFields pFields, string pConfigWord,
                                                           IFeatureDataset pDstFeatureDataset, IFeatureClass pSrcFeatureClass, object pAnnoProperties,
                                                           object pReferenceScale, object pSymbolCollection, bool pAutoCreate)
        {
            #region ������
            if (pFeatWorkspace == null)
            {
                throw (new Exception("[pFeatWorkspace] ����Ϊ��!"));
            }

            //���ζ����ֶ�����
            string strShapeFieldName = "";
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                if (pFields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                {
                    strShapeFieldName = pFields.get_Field(i).Name;
                    break;
                }
            }

            if (strShapeFieldName.Length == 0)
            {
                throw (new Exception("�ֶμ����Ҳ������ζ�����"));
            }
            #endregion

            UID tUidClsId = new UIDClass();
            tUidClsId.Value = "esriCarto.AnnotationFeature";

            UID tUidClsExt = new UIDClass();
            tUidClsExt.Value = "esriCarto.AnnotationFeatureClassExtension";

            //��ѯIFeatureWorkspaceAnno�ӿ�
            IFeatureWorkspaceAnno tFeatWorkspaceAnno = pFeatWorkspace as IFeatureWorkspaceAnno;
            //����ע����
            IFeatureClass tFeatureClass=tFeatWorkspaceAnno.CreateAnnotationClass(pName, pFields, tUidClsId, tUidClsExt, strShapeFieldName, "", 
                                pDstFeatureDataset, pSrcFeatureClass, pAnnoProperties, pReferenceScale, pSymbolCollection, pAutoCreate);

            return tFeatureClass;           
        }

        /// <summary>
        /// ���ض���SDE�����ռ���ɾ��ָ�����ݼ����ͺ����Ƶ����ݼ�
        /// </summary>
        /// <param name="pDatasetName">���ݼ�����</param>
        /// <param name="pFeatWorkspace">SDE�����ռ�</param>
        /// <param name="pDatasetType">���ݼ�����</param>
        public  static void  DeleteDatasetByName(string pDatasetName, IFeatureWorkspace pFeatWorkspace, esriDatasetType pDatasetType)
        {
            try
            {
                //��ѯ���ýӿ�
                IWorkspace tWorkspace = pFeatWorkspace as IWorkspace;
                //��ȡָ�����͵����ݼ�����
                IEnumDataset tEnumDataset = tWorkspace.get_Datasets(pDatasetType);

                //������ѯ��ָ��������ͬ�����ݼ�
                for (IDataset tDateset = tEnumDataset.Next(); tDateset != null; tDateset = tEnumDataset.Next())
                {
                    //�Ƚ������Ƿ���ͬ
                    if (String.Compare(tDateset.Name, pDatasetName, true) == 0)
                    {
                        //�ܷ�ɾ��
                        if (tDateset.CanDelete())
                        {
                            tDateset.Delete();
                        }

                        //�жϲ���
                        break;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// ����Դ���ݵ�Ŀ��Ҫ����
        /// </summary>
        /// <param name="pFromFeatureClass">ԴҪ����</param>
        /// <param name="pToFeatureClass">Ŀ��Ҫ����</param>
        /// <param name="pDictFields">ƥ�����(keyΪĿ��Ҫ�����ֶ�����ֵ valueΪԴҪ�����ֶ�����ֵ)</param>
        /// <param name="pQueryFilter">ԴҪ�����������</param>
        public static string ImportDataToFeatureClass(IFeatureClass pFeatureClassInput, IFeatureClass pFeatureClassTarget, Dictionary<int, int> pDictFields,IQueryFilter pQueryFilter)
        {
            //�õ���ǰ�����ռ�
            IWorkspace tWorkspaceTarget = (pFeatureClassTarget as IDataset).Workspace;
            //�õ���ǰ�༭�ռ�
            IWorkspaceEdit tWorkspaceEditTarget = AG.COM.SDM.Utility.Editor.LibEditor.GetNewEditableWorkspace(tWorkspaceTarget);
           
            try
            {
                tWorkspaceEditTarget.StartEditing(false);
                tWorkspaceEditTarget.StartEditOperation();
                //�����α�״̬Ϊ����
                IFeatureCursor tFeatureCursorTarget = pFeatureClassTarget.Insert(true);
                //��ȡԴ�ļ�Ҫ����ĵļ�¼���α�
                IFeatureCursor tFeatureCursorInput = pFeatureClassInput.Search(pQueryFilter, false);
                //����ƥ�����ö����
                IDictionaryEnumerator tDictEnumerator = pDictFields.GetEnumerator();
                IFeature tFeatureInput = tFeatureCursorInput.NextFeature();
                while (tFeatureInput!=null)
                {  
                    //����Ҫ�ػ���
                    IFeatureBuffer tFeatureBufferTarget = pFeatureClassTarget.CreateFeatureBuffer();
                    //���ó�ʼλ��
                    tDictEnumerator.Reset(); 
                    while (tDictEnumerator.MoveNext() == true)
                    {
                        //��ȡ��ǰҪ���ֶ�ֵ
                        object objValue=   tFeatureInput.get_Value((int)tDictEnumerator.Value);

                        if (objValue.GetType() == typeof(System.DBNull))
                        {
                            #region ��ֹ����nullֵ
                            IField tField = tFeatureBufferTarget.Fields.get_Field((int)tDictEnumerator.Key);
                            if (tField.Type == esriFieldType.esriFieldTypeDouble ||
                                tField.Type == esriFieldType.esriFieldTypeInteger ||
                                tField.Type == esriFieldType.esriFieldTypeSingle ||
                                tField.Type == esriFieldType.esriFieldTypeSmallInteger)
                            {
                                //����ֵ
                                tFeatureBufferTarget.set_Value((int)tDictEnumerator.Key, 0);
                            }
                            else if (tField.Type == esriFieldType.esriFieldTypeString)
                            {
                                //����ֵ
                                tFeatureBufferTarget.set_Value((int)tDictEnumerator.Key, "");
                            }
                            else if (tField.Type == esriFieldType.esriFieldTypeDate)
                            {
                                //����ֵ
                                tFeatureBufferTarget.set_Value((int)tDictEnumerator.Key, DateTime.MinValue);
                            }
                            #endregion
                        }
                        else
                        {
                            IField tField = tFeatureBufferTarget.Fields.get_Field((int)tDictEnumerator.Key);
                            if (tField.Editable)
                            {
                                //����ֵ
                                tFeatureBufferTarget.set_Value((int)tDictEnumerator.Key, objValue);
                            }
                        }
                    }
                    //���ü��ζ���
                    //����任
                    IGeometry tGeometry = tFeatureInput.ShapeCopy;

                    tGeometry.SnapToSpatialReference();
                    ITopologicalOperator2 tTopo = tGeometry as ITopologicalOperator2;
                    if (tTopo != null)
                    {
                        tTopo.IsKnownSimple_2 = false;
                        tTopo.Simplify();
                    }

                    tFeatureBufferTarget.Shape = tGeometry;
                    //tFeatureBuffer.Shape = tFeature.ShapeCopy;
                    tFeatureInput = tFeatureCursorInput.NextFeature();
                    //�����¼
                    tFeatureCursorTarget.InsertFeature(tFeatureBufferTarget);
                    tFeatureCursorTarget.Flush();                    
                }

                //�ͷŷ��й���Դ
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursorTarget);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursorInput);

                tWorkspaceEditTarget.StopEditOperation();
                tWorkspaceEditTarget.StopEditing(true);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //���Ա༭����
                //tWorkspaceEditTarget.AbortEditOperation();
                tWorkspaceEditTarget.StopEditOperation();
                tWorkspaceEditTarget.StopEditing(false);
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                //throw (ex);
                return ex.Message;
            } 
        }

        /// <summary>
        /// ����Դ���ݵ�Ŀ��Ҫ����
        /// </summary>
        /// <param name="pFromFeatureClass">ԴҪ����</param>
        /// <param name="pToFeatureClass">Ŀ��Ҫ����</param>
        /// <param name="pDictFields">ƥ�����(keyΪĿ��Ҫ�����ֶ�����ֵ valueΪԴҪ�����ֶ�����ֵ)</param>
        /// <param name="pQueryFilter">ԴҪ�����������</param>
        public static string ImportDataToFeatureClass(IFeatureClass pFeatureClassInput, IFeatureClass pFeatureClassTarget, Dictionary<int, int> pDictFields, IQueryFilter pQueryFilter, Utility.Common.TrackProgressDialog progressBar, string strMsg)
        {
            //�õ���ǰ�����ռ�
            IWorkspace tWorkspaceTarget = (pFeatureClassTarget as IDataset).Workspace;
            //�õ���ǰ�༭�ռ�
            IWorkspaceEdit tWorkspaceEditTarget = AG.COM.SDM.Utility.Editor.LibEditor.GetNewEditableWorkspace(tWorkspaceTarget);

            try
            {
                tWorkspaceEditTarget.StartEditing(false);
                tWorkspaceEditTarget.StartEditOperation();
                //�����α�״̬Ϊ����
                IFeatureCursor tFeatureCursorTarget = pFeatureClassTarget.Insert(true);
                //��ȡԴ�ļ�Ҫ����ĵļ�¼���α�
                IFeatureCursor tFeatureCursorInput = pFeatureClassInput.Search(pQueryFilter, false);
                //����ƥ�����ö����
                IDictionaryEnumerator tDictEnumerator = pDictFields.GetEnumerator();
                IFeature tFeatureInput = tFeatureCursorInput.NextFeature();
                int nCount = pFeatureClassInput.FeatureCount(pQueryFilter);
                int nPos = 0;
                while (tFeatureInput != null)
                {
                    if(progressBar != null)
                    {
                        progressBar.SubMessage = string.Format("{0} ({1}/{2})", strMsg, nPos++, nCount);
                        System.Windows.Forms.Application.DoEvents();
                    }
                    

                    //����Ҫ�ػ���
                    IFeatureBuffer tFeatureBufferTarget = pFeatureClassTarget.CreateFeatureBuffer();
                    //���ó�ʼλ��
                    tDictEnumerator.Reset();
                    while (tDictEnumerator.MoveNext() == true)
                    {
                        //��ȡ��ǰҪ���ֶ�ֵ
                        object objValue = tFeatureInput.get_Value((int)tDictEnumerator.Value);

                        if (objValue.GetType() == typeof(System.DBNull))
                        {
                            #region ��ֹ����nullֵ
                            IField tField = tFeatureBufferTarget.Fields.get_Field((int)tDictEnumerator.Key);
                            if (tField.Editable == false) continue;

                            if (tField.Type == esriFieldType.esriFieldTypeDouble ||
                                tField.Type == esriFieldType.esriFieldTypeInteger ||
                                tField.Type == esriFieldType.esriFieldTypeSingle ||
                                tField.Type == esriFieldType.esriFieldTypeSmallInteger)
                            {
                                //����ֵ
                                tFeatureBufferTarget.set_Value((int)tDictEnumerator.Key, 0);
                            }
                            else if (tField.Type == esriFieldType.esriFieldTypeString)
                            {
                                //����ֵ
                                tFeatureBufferTarget.set_Value((int)tDictEnumerator.Key, "");
                            }
                            else if (tField.Type == esriFieldType.esriFieldTypeDate)
                            {
                                //����ֵ
                                tFeatureBufferTarget.set_Value((int)tDictEnumerator.Key, DateTime.MinValue);
                            }
                            #endregion
                        }
                        else
                        {
                            IField tField = tFeatureBufferTarget.Fields.get_Field((int)tDictEnumerator.Key);
                            if (tField.Editable == false) continue;

                            //����ֵ
                            tFeatureBufferTarget.set_Value((int)tDictEnumerator.Key, objValue);
                        }
                    }
                    //���ü��ζ���
                    //����任
                    IGeometry tGeometry = tFeatureInput.ShapeCopy;

                    tGeometry.SnapToSpatialReference();
                    ITopologicalOperator2 tTopo = tGeometry as ITopologicalOperator2;
                    if (tTopo != null)
                    {
                        tTopo.IsKnownSimple_2 = false;
                        tTopo.Simplify();
                    }

                    tFeatureBufferTarget.Shape = tGeometry;
                    //tFeatureBuffer.Shape = tFeature.ShapeCopy;
                    tFeatureInput = tFeatureCursorInput.NextFeature();
                    //�����¼
                    tFeatureCursorTarget.InsertFeature(tFeatureBufferTarget);
                    tFeatureCursorTarget.Flush();
                }

                //�ͷŷ��й���Դ
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursorTarget);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursorInput);

                tWorkspaceEditTarget.StopEditOperation();
                tWorkspaceEditTarget.StopEditing(true);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //���Ա༭����
                //tWorkspaceEditTarget.AbortEditOperation();
                tWorkspaceEditTarget.StopEditOperation();
                tWorkspaceEditTarget.StopEditing(false);
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                //throw (ex);
                return ex.Message;
            }
        }

        /// <summary>
        /// ����Դ���ݵ�Ŀ��Ҫ����
        /// </summary>
        /// <param name="pFromFeatureClass">Դ���Ա�</param>
        /// <param name="pToFeatureClass">Ŀ�����Ա�</param>
        /// <param name="pDictFields">ƥ�����(keyΪĿ��Ҫ�����ֶ�����ֵ valueΪԴҪ�����ֶ�����ֵ)</param>
        /// <param name="pQueryFilter">ԴҪ�ر��������</param>
        public static void ImportDataToTable(ITable pFromTable, ITable pToTable, Dictionary<int, int> pDictFields, IQueryFilter pQueryFilter)
        {
            //�õ���ǰ�����ռ�
            IWorkspace tWorkspace = (pToTable as IDataset).Workspace;
            //�õ���ǰ�༭�ռ�
            IWorkspaceEdit tWorkspaceEdit = AG.COM.SDM.Utility.Editor.LibEditor.GetNewEditableWorkspace(tWorkspace);

            try
            {
                tWorkspaceEdit.StartEditing(false);
                tWorkspaceEdit.StartEditOperation();

                //�����α�״̬Ϊ����
                ICursor  tCursor1 = pToTable.Insert(true);  
                //��ȡԴ�ļ�Ҫ����ĵļ�¼���α�
                ICursor tCursor2 = pFromTable.Search(pQueryFilter, false); 
                //����ƥ�����ö����
                IDictionaryEnumerator tDictEnumerator = pDictFields.GetEnumerator();

                for (IRow tRow = tCursor2.NextRow(); tRow != null; tRow = tCursor2.NextRow())
                {
                    //����Ҫ�ػ���
                    IRowBuffer tRowBuffer = pToTable.CreateRowBuffer();

                    //���ó�ʼλ��
                    tDictEnumerator.Reset();

                    while (tDictEnumerator.MoveNext() == true)
                    {
                        //��ȡ��ǰҪ���ֶ�ֵ
                        object objValue = tRow.get_Value((int)tDictEnumerator.Value);

                        if (objValue == null)
                        {
                            #region ��ֹ����nullֵ
                            IField tField = tRowBuffer.Fields.get_Field((int)tDictEnumerator.Key);
                            if (tField.Type == esriFieldType.esriFieldTypeDouble ||
                                tField.Type == esriFieldType.esriFieldTypeInteger ||
                                tField.Type == esriFieldType.esriFieldTypeSingle ||
                                tField.Type == esriFieldType.esriFieldTypeSmallInteger)
                            {
                                //����ֵ
                                tRowBuffer.set_Value((int)tDictEnumerator.Key, 0);
                            }
                            else if (tField.Type == esriFieldType.esriFieldTypeString)
                            {
                                //����ֵ
                                tRowBuffer.set_Value((int)tDictEnumerator.Key, "");
                            }
                            else if (tField.Type == esriFieldType.esriFieldTypeDate)
                            {
                                //����ֵ
                                tRowBuffer.set_Value((int)tDictEnumerator.Key, DateTime.MinValue);
                            }
                            #endregion
                        }
                        else
                        {
                            //����ֵ
                            tRowBuffer.set_Value((int)tDictEnumerator.Key, tRow.get_Value((int)tDictEnumerator.Value));
                        }           
                    }           
                    //�����¼
                    tCursor1.InsertRow(tRowBuffer);
                }

                //һ����д��
                tCursor1.Flush();

                //�ͷŷ��й���Դ
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tCursor1);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tCursor2);

                tWorkspaceEdit.StopEditOperation();
                tWorkspaceEdit.StopEditing(true);
            }
            catch (Exception ex)
            {
                //���Ա༭����
                tWorkspaceEdit.AbortEditOperation();
                tWorkspaceEdit.StopEditOperation();
                tWorkspaceEdit.StopEditing(false);

                throw (ex);
            }
            finally
            {
            }
        }

        /// <summary>
        /// �����Ƿ���ɾ�������ռ���ָ�����Ƶ�Ҫ����
        /// </summary>
        /// <param name="pWorkspace">�����ռ�</param>
        /// <param name="pFeatName">Ҫ��������</param>
        /// <returns>���������ָ�����Ƶ�Ҫ�������ɾ���򷵻�true,���򷵻� false</returns>
        public static bool HasDeleteFile(IWorkspace pWorkspace, string pFeatName)
        {
            bool hasDelFile = true;
  
            //��ѯ�ӿ����� 
            IFeatureWorkspace tFeatureWorkspace = pWorkspace as IFeatureWorkspace;
            IFeatureClass tFeatureClass = null;

            if (pWorkspace.IsDirectory() == false)
            {
                IWorkspace2 tWorkspace = pWorkspace as IWorkspace2;
                bool IsExist = tWorkspace.get_NameExists(esriDatasetType.esriDTFeatureClass, pFeatName);
                if (IsExist == true)
                {
                    tFeatureClass = tFeatureWorkspace.OpenFeatureClass(pFeatName);
                }
            }
            else
            {
                if (System.IO.File.Exists(string.Format("{0}\\{1}.shp", pWorkspace.PathName, pFeatName)))
                    tFeatureClass = tFeatureWorkspace.OpenFeatureClass(pFeatName);
            }            

            if (tFeatureClass != null)
            {
                //��ʾ��Ϣ
                string strMessage = string.Format("����ռ��Ѵ�����ͬ����[{0}]��Ҫ����,�Ƿ�ɾ���ؽ�?", pFeatName);

                if (MessageBox.Show(strMessage, "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //��ѯ�ӿ�����      
                    IDataset tDataset = tFeatureClass as IDataset;
                    //ɾ��Ҫ����
                    tDataset.Delete();
                    //�û���ʶ״̬
                    hasDelFile = true;
                }
                else
                {
                    hasDelFile = false;
                }
            }

            return hasDelFile;
        } 
    }
}
