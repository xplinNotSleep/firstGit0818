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
    /// 地理数据操作帮助类
    /// </summary>
    public class GeoDBHelper
    {
        /// <summary>
        /// 创建要素类
        /// </summary>
        /// <param name="pObject">IWorkspace或者IFeatureDataset对象</param>
        /// <param name="pName">要素类名称</param>
        /// <param name="pFeatureType">要素类型</param>
        /// <param name="pFields">字段集</param>
        /// <param name="pUidClsId">CLSID值</param>
        /// <param name="pUidClsExt">EXTCLSID值</param>
        /// <param name="pConfigWord">配置信息关键词</param>
        /// <returns>返回IFeatureClass</returns>
        public static IFeatureClass CreateFeatureClass(object pObject, string pName, esriFeatureType pFeatureType,
            IFields pFields, UID pUidClsId, UID pUidClsExt, string pConfigWord)
        {          
            #region 错误检测
            if (pObject == null)
            {
                throw (new Exception("[pObject] 不能为空!"));
            }
            if (!((pObject is IFeatureWorkspace) || (pObject is IFeatureDataset)))
            {
                throw (new Exception("[pObject] 必须为IFeatureWorkspace 或者 IFeatureDataset"));
            }
            if (pName.Length == 0)
            {
                throw (new Exception("[pName] 不能为空!"));
            }
            if (pFields == null || pFields.FieldCount == 0)
            {
                throw (new Exception("[pFields] 不能为空!"));
            }

            //几何对象字段名称
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
                throw (new Exception("字段集中找不到几何对象定义"));
            }

            #endregion

            #region pUidClsID字段为空时
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

            #region pUidClsExt字段为空时
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
                //创建独立的FeatureClass
                IWorkspace tWorkspace = pObject as IWorkspace;

                //查询引用接口
                IFeatureWorkspace tFeatureWorkspace = tWorkspace as IFeatureWorkspace;
                tFeatureClass = tFeatureWorkspace.CreateFeatureClass(pName, pFields, pUidClsId, pUidClsExt, pFeatureType, strShapeFieldName, pConfigWord);
            }
            else if (pObject is IFeatureDataset)
            {
                //在要素集中创建FeatureClass
                IFeatureDataset tFeatureDataset = (IFeatureDataset)pObject;
                tFeatureClass = tFeatureDataset.CreateFeatureClass(pName, pFields, pUidClsId, pUidClsExt, pFeatureType, strShapeFieldName, pConfigWord);
            }

            return tFeatureClass;
        }
        public static IFeatureClass CreateFeatureClass(object pObject, string pName, esriFeatureType pFeatureType,
        IFields pFields, UID pUidClsId, UID pUidClsExt, string pConfigWord, ISpatialReference spatialReference)
        {
            #region 错误检测
            if (pObject == null)
            {
                throw (new Exception("[pObject] 不能为空!"));
            }
            if (!((pObject is IFeatureWorkspace) || (pObject is IFeatureDataset)))
            {
                throw (new Exception("[pObject] 必须为IFeatureWorkspace 或者 IFeatureDataset"));
            }
            if (pName.Length == 0)
            {
                throw (new Exception("[pName] 不能为空!"));
            }
            if (pFields == null || pFields.FieldCount == 0)
            {
                throw (new Exception("[pFields] 不能为空!"));
            }

            //几何对象字段名称
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
                throw (new Exception("字段集中找不到几何对象定义"));
            }

            #endregion

            #region pUidClsID字段为空时
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

            #region pUidClsExt字段为空时
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
                //创建独立的FeatureClass
                IWorkspace tWorkspace = pObject as IWorkspace;

                //查询引用接口
                IFeatureWorkspace tFeatureWorkspace = tWorkspace as IFeatureWorkspace;
                try
                {
                    //如果在数据库的根节点下直接创建带有坐标系的要素类会导致要素类创建失败，所以，这里将坐标系单独拿出来设置
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
                    MessageBox.Show($"要素类创建失败：{pName} ;" + e.Message, "数据库创建");
                }
            }
            else if (pObject is IFeatureDataset)
            {
                //在要素集中创建FeatureClass
                IFeatureDataset tFeatureDataset = (IFeatureDataset)pObject;
                try
                {
                    pFields = VerifyFields(pFields, tFeatureDataset.Workspace);
                    tFeatureClass = tFeatureDataset.CreateFeatureClass(pName, pFields, pUidClsId, pUidClsExt, pFeatureType, strShapeFieldName, pConfigWord);
                }
                catch (Exception e)
                {
                    MessageBox.Show($"要素类创建失败：{pName} ;" + e.Message, "数据库创建");
                }
            }

            return tFeatureClass;
        }
        /// <summary>
        /// 对字段进行验证，返回验证成功的字段集合
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
        /// 创建要素类
        /// </summary>
        /// <param name="pObject">IWorkspace或者IFeatureDataset对象</param>
        /// <param name="pName">要素类名称</param>
        /// <param name="pFeatureType">要素类型</param>
        /// <param name="pFields">字段集</param>
        /// <param name="pUidClsId">CLSID值</param>
        /// <param name="pUidClsExt">EXTCLSID值</param>
        /// <param name="pConfigWord">配置信息关键词</param>
        /// <returns>返回IFeatureClass</returns>
        public static IFeatureClass CreateFeatureClass(IFeatureClass tFeatureClassSource, object pObject, string tPathTarget, string tFileNameWithoutExt, esriFeatureType pFeatureType, UID pUidClsId, UID pUidClsExt, string pConfigWord, ref Dictionary<int, int> tFieldMatch)
        {
            //新旧字段索引匹配，key为新字段索引，value为旧字段索引
            tFieldMatch = new Dictionary<int, int>();
            //新字段名称与旧字段Index匹配
            Dictionary<string, int> tNewNameWithOldIdx = new Dictionary<string, int>();

            #region 错误检测
            if (pObject == null)
            {
                throw (new Exception("[pObject] 不能为空!"));
            }
            if (!((pObject is IFeatureWorkspace) || (pObject is IFeatureDataset)))
            {
                throw (new Exception("[pObject] 必须为IFeatureWorkspace 或者 IFeatureDataset"));
            }
            if (tFileNameWithoutExt.Length == 0)
            {
                throw (new Exception("[tFileNameWithoutExt] 不能为空!"));
            }
            if (tFeatureClassSource.Fields == null || tFeatureClassSource.Fields.FieldCount == 0)
            {
                throw (new Exception("[pFields] 不能为空!"));
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

            //过滤掉Shapefile不支持的字段类型
            IFieldsEdit tFieldsEditNew = new FieldsClass();
            for (int i = 0; i < tFieldsValid.FieldCount; i++)
            {
                IField tFieldValid = tFieldsValid.get_Field(i);              

                if (tFieldValid.Type != esriFieldType.esriFieldTypeBlob && tFieldValid.Type != esriFieldType.esriFieldTypeRaster &&
                    tFieldValid.Type != esriFieldType.esriFieldTypeXML &&
                    tFieldValid.Type != esriFieldType.esriFieldTypeGUID)
                {
                    tFieldsEditNew.AddField(tFieldValid);
                    //写入新字段名称与就字段Index匹配
                    tNewNameWithOldIdx.Add(tFieldValid.Name, i);
                }
            }

            #endregion

            #region pUidClsID字段为空时
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

            #region pUidClsExt字段为空时
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

            //防止文件重名
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
                //创建独立的FeatureClass
                IWorkspace tWorkspace = pObject as IWorkspace;

                //查询引用接口
                IFeatureWorkspace tFeatureWorkspace = tWorkspace as IFeatureWorkspace;
                tFeatureClassTarget = tFeatureWorkspace.CreateFeatureClass(System.IO.Path.GetFileNameWithoutExtension(tFileName), tFieldsEditNew, pUidClsId, pUidClsExt, pFeatureType, tFeatureClassSource.ShapeFieldName, pConfigWord);
            }
            else if (pObject is IFeatureDataset)
            {
                //在要素集中创建FeatureClass
                IFeatureDataset tFeatureDataset = (IFeatureDataset)pObject;
                tFeatureClassTarget = tFeatureDataset.CreateFeatureClass(System.IO.Path.GetFileNameWithoutExtension(tFileName), tFieldsEditNew, pUidClsId, pUidClsExt, pFeatureType, tFeatureClassSource.ShapeFieldName, pConfigWord);
            }

            //生成新旧字段Index匹配，因此创建FeatureClass时可能会新增字段，因此创建FeatureClass后根据新字段名称与
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
        /// 创建属性表
        /// </summary>
        /// <param name="pFeatWorkspace">IFeatureWorkspace对象</param>
        /// <param name="pName">表名称</param>
        /// <param name="pFields">字段集合</param>
        /// <param name="pUidClsId">CLSID值</param>
        /// <param name="pUidClsExt">EXTCLSID值</param>
        /// <param name="pConfigWord">配置信息关键词</param>
        /// <returns>返回ITable</returns>
        public static ITable CreateTable(IFeatureWorkspace pFeatWorkspace, string pName, IFields pFields, UID pUidClsId, UID pUidClsExt, string pConfigWord)
        {
            #region 错误检测
            if (pFeatWorkspace == null)
            {
                throw (new Exception("[pFeatWorkspace] 不能为空"));
            }

            if (pName.Length == 0)
            {
                throw (new Exception("[pName] 不能为空"));
            }

            if (pUidClsId == null)
            {
                pUidClsId = new UIDClass();
                pUidClsId.Value = "esriGeoDatabase.Object";
            }
            #endregion

            if (pFields == null)
            {
                //实倒化字段集合对象
                pFields = new FieldsClass();
                IFieldsEdit tFieldsEdit = (IFieldsEdit)pFields;

                //创建OID字段
                IField fieldOID = new FieldClass();
                IFieldEdit fieldEditOID = fieldOID as IFieldEdit;
                fieldEditOID.Name_2 = "OBJECTID";
                fieldEditOID.AliasName_2 = "OBJECTID";
                fieldEditOID.IsNullable_2 = false;
                fieldEditOID.Type_2 = esriFieldType.esriFieldTypeOID;
                tFieldsEdit.AddField(fieldOID);
            }

            //创建属性表
            ITable tTable = pFeatWorkspace.CreateTable(pName, pFields, pUidClsId, pUidClsExt, pConfigWord);

            return tTable;
        }

        /// <summary>
        /// 创建要素集
        /// </summary>
        /// <param name="pFeatWorkspace">IFeatureWorkspace对象</param>
        /// <param name="pName">要素集名称</param>
        /// <param name="pSpatialReference">空间参考关系</param>
        /// <returns>返回IFeatureDataset</returns>
        public static IFeatureDataset CreateFeatureDataset(IFeatureWorkspace pFeatWorkspace, string pName, ISpatialReference pSpatialReference)
        {
            #region 错误检测
            if (pFeatWorkspace == null)
            {
                throw (new Exception("[pFeatWorkspace]不能为空"));
            }
            if (pName.Trim().Length == 0)
            {
                throw (new Exception("[pName] 不能为空"));
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
        /// 创建注记要素类
        /// </summary>
        /// <param name="pFeatWorkspace">SDE工作空间</param>
        /// <param name="pName">名称</param>
        /// <param name="pFields">字段集</param>
        /// <param name="pConfigWord">关键字</param>
        /// <param name="pDstFeatureDataset">目标要素集</param>
        /// <param name="pSrcFeatureClass">源要素类</param>
        /// <param name="pAnnoProperties">注记属性集</param>
        /// <param name="pReferenceScale">参考比例</param>
        /// <param name="pSymbolCollection">样式集合</param>
        /// <param name="pAutoCreate">是否自动创建</param>
        /// <returns>返回创建好的注记要素类</returns>
        public static IFeatureClass CreateFeatureAnnoClass(IFeatureWorkspace pFeatWorkspace, string pName, IFields pFields, string pConfigWord,
                                                           IFeatureDataset pDstFeatureDataset, IFeatureClass pSrcFeatureClass, object pAnnoProperties,
                                                           object pReferenceScale, object pSymbolCollection, bool pAutoCreate)
        {
            #region 错误检测
            if (pFeatWorkspace == null)
            {
                throw (new Exception("[pFeatWorkspace] 不能为空!"));
            }

            //几何对象字段名称
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
                throw (new Exception("字段集中找不到几何对象定义"));
            }
            #endregion

            UID tUidClsId = new UIDClass();
            tUidClsId.Value = "esriCarto.AnnotationFeature";

            UID tUidClsExt = new UIDClass();
            tUidClsExt.Value = "esriCarto.AnnotationFeatureClassExtension";

            //查询IFeatureWorkspaceAnno接口
            IFeatureWorkspaceAnno tFeatWorkspaceAnno = pFeatWorkspace as IFeatureWorkspaceAnno;
            //创建注记类
            IFeatureClass tFeatureClass=tFeatWorkspaceAnno.CreateAnnotationClass(pName, pFields, tUidClsId, tUidClsExt, strShapeFieldName, "", 
                                pDstFeatureDataset, pSrcFeatureClass, pAnnoProperties, pReferenceScale, pSymbolCollection, pAutoCreate);

            return tFeatureClass;           
        }

        /// <summary>
        /// 在特定的SDE工作空间中删除指定数据集类型和名称的数据集
        /// </summary>
        /// <param name="pDatasetName">数据集名称</param>
        /// <param name="pFeatWorkspace">SDE工作空间</param>
        /// <param name="pDatasetType">数据集类型</param>
        public  static void  DeleteDatasetByName(string pDatasetName, IFeatureWorkspace pFeatWorkspace, esriDatasetType pDatasetType)
        {
            try
            {
                //查询引用接口
                IWorkspace tWorkspace = pFeatWorkspace as IWorkspace;
                //获取指定类型的数据集集合
                IEnumDataset tEnumDataset = tWorkspace.get_Datasets(pDatasetType);

                //遍历查询与指定名称相同的数据集
                for (IDataset tDateset = tEnumDataset.Next(); tDateset != null; tDateset = tEnumDataset.Next())
                {
                    //比较名称是否相同
                    if (String.Compare(tDateset.Name, pDatasetName, true) == 0)
                    {
                        //能否删除
                        if (tDateset.CanDelete())
                        {
                            tDateset.Delete();
                        }

                        //中断操作
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
        /// 导入源数据到目标要素类
        /// </summary>
        /// <param name="pFromFeatureClass">源要素类</param>
        /// <param name="pToFeatureClass">目标要素类</param>
        /// <param name="pDictFields">匹配规则(key为目标要素类字段索引值 value为源要素类字段索引值)</param>
        /// <param name="pQueryFilter">源要素类过滤条件</param>
        public static string ImportDataToFeatureClass(IFeatureClass pFeatureClassInput, IFeatureClass pFeatureClassTarget, Dictionary<int, int> pDictFields,IQueryFilter pQueryFilter)
        {
            //得到当前工作空间
            IWorkspace tWorkspaceTarget = (pFeatureClassTarget as IDataset).Workspace;
            //得到当前编辑空间
            IWorkspaceEdit tWorkspaceEditTarget = AG.COM.SDM.Utility.Editor.LibEditor.GetNewEditableWorkspace(tWorkspaceTarget);
           
            try
            {
                tWorkspaceEditTarget.StartEditing(false);
                tWorkspaceEditTarget.StartEditOperation();
                //设置游标状态为插入
                IFeatureCursor tFeatureCursorTarget = pFeatureClassTarget.Insert(true);
                //获取源文件要素类的的记录集游标
                IFeatureCursor tFeatureCursorInput = pFeatureClassInput.Search(pQueryFilter, false);
                //返回匹配规则枚举数
                IDictionaryEnumerator tDictEnumerator = pDictFields.GetEnumerator();
                IFeature tFeatureInput = tFeatureCursorInput.NextFeature();
                while (tFeatureInput!=null)
                {  
                    //设置要素缓存
                    IFeatureBuffer tFeatureBufferTarget = pFeatureClassTarget.CreateFeatureBuffer();
                    //设置初始位置
                    tDictEnumerator.Reset(); 
                    while (tDictEnumerator.MoveNext() == true)
                    {
                        //获取当前要素字段值
                        object objValue=   tFeatureInput.get_Value((int)tDictEnumerator.Value);

                        if (objValue.GetType() == typeof(System.DBNull))
                        {
                            #region 防止出现null值
                            IField tField = tFeatureBufferTarget.Fields.get_Field((int)tDictEnumerator.Key);
                            if (tField.Type == esriFieldType.esriFieldTypeDouble ||
                                tField.Type == esriFieldType.esriFieldTypeInteger ||
                                tField.Type == esriFieldType.esriFieldTypeSingle ||
                                tField.Type == esriFieldType.esriFieldTypeSmallInteger)
                            {
                                //设置值
                                tFeatureBufferTarget.set_Value((int)tDictEnumerator.Key, 0);
                            }
                            else if (tField.Type == esriFieldType.esriFieldTypeString)
                            {
                                //设置值
                                tFeatureBufferTarget.set_Value((int)tDictEnumerator.Key, "");
                            }
                            else if (tField.Type == esriFieldType.esriFieldTypeDate)
                            {
                                //设置值
                                tFeatureBufferTarget.set_Value((int)tDictEnumerator.Key, DateTime.MinValue);
                            }
                            #endregion
                        }
                        else
                        {
                            IField tField = tFeatureBufferTarget.Fields.get_Field((int)tDictEnumerator.Key);
                            if (tField.Editable)
                            {
                                //设置值
                                tFeatureBufferTarget.set_Value((int)tDictEnumerator.Key, objValue);
                            }
                        }
                    }
                    //设置几何对象
                    //坐标变换
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
                    //插入记录
                    tFeatureCursorTarget.InsertFeature(tFeatureBufferTarget);
                    tFeatureCursorTarget.Flush();                    
                }

                //释放非托管资源
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursorTarget);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursorInput);

                tWorkspaceEditTarget.StopEditOperation();
                tWorkspaceEditTarget.StopEditing(true);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //忽略编辑操作
                //tWorkspaceEditTarget.AbortEditOperation();
                tWorkspaceEditTarget.StopEditOperation();
                tWorkspaceEditTarget.StopEditing(false);
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                //throw (ex);
                return ex.Message;
            } 
        }

        /// <summary>
        /// 导入源数据到目标要素类
        /// </summary>
        /// <param name="pFromFeatureClass">源要素类</param>
        /// <param name="pToFeatureClass">目标要素类</param>
        /// <param name="pDictFields">匹配规则(key为目标要素类字段索引值 value为源要素类字段索引值)</param>
        /// <param name="pQueryFilter">源要素类过滤条件</param>
        public static string ImportDataToFeatureClass(IFeatureClass pFeatureClassInput, IFeatureClass pFeatureClassTarget, Dictionary<int, int> pDictFields, IQueryFilter pQueryFilter, Utility.Common.TrackProgressDialog progressBar, string strMsg)
        {
            //得到当前工作空间
            IWorkspace tWorkspaceTarget = (pFeatureClassTarget as IDataset).Workspace;
            //得到当前编辑空间
            IWorkspaceEdit tWorkspaceEditTarget = AG.COM.SDM.Utility.Editor.LibEditor.GetNewEditableWorkspace(tWorkspaceTarget);

            try
            {
                tWorkspaceEditTarget.StartEditing(false);
                tWorkspaceEditTarget.StartEditOperation();
                //设置游标状态为插入
                IFeatureCursor tFeatureCursorTarget = pFeatureClassTarget.Insert(true);
                //获取源文件要素类的的记录集游标
                IFeatureCursor tFeatureCursorInput = pFeatureClassInput.Search(pQueryFilter, false);
                //返回匹配规则枚举数
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
                    

                    //设置要素缓存
                    IFeatureBuffer tFeatureBufferTarget = pFeatureClassTarget.CreateFeatureBuffer();
                    //设置初始位置
                    tDictEnumerator.Reset();
                    while (tDictEnumerator.MoveNext() == true)
                    {
                        //获取当前要素字段值
                        object objValue = tFeatureInput.get_Value((int)tDictEnumerator.Value);

                        if (objValue.GetType() == typeof(System.DBNull))
                        {
                            #region 防止出现null值
                            IField tField = tFeatureBufferTarget.Fields.get_Field((int)tDictEnumerator.Key);
                            if (tField.Editable == false) continue;

                            if (tField.Type == esriFieldType.esriFieldTypeDouble ||
                                tField.Type == esriFieldType.esriFieldTypeInteger ||
                                tField.Type == esriFieldType.esriFieldTypeSingle ||
                                tField.Type == esriFieldType.esriFieldTypeSmallInteger)
                            {
                                //设置值
                                tFeatureBufferTarget.set_Value((int)tDictEnumerator.Key, 0);
                            }
                            else if (tField.Type == esriFieldType.esriFieldTypeString)
                            {
                                //设置值
                                tFeatureBufferTarget.set_Value((int)tDictEnumerator.Key, "");
                            }
                            else if (tField.Type == esriFieldType.esriFieldTypeDate)
                            {
                                //设置值
                                tFeatureBufferTarget.set_Value((int)tDictEnumerator.Key, DateTime.MinValue);
                            }
                            #endregion
                        }
                        else
                        {
                            IField tField = tFeatureBufferTarget.Fields.get_Field((int)tDictEnumerator.Key);
                            if (tField.Editable == false) continue;

                            //设置值
                            tFeatureBufferTarget.set_Value((int)tDictEnumerator.Key, objValue);
                        }
                    }
                    //设置几何对象
                    //坐标变换
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
                    //插入记录
                    tFeatureCursorTarget.InsertFeature(tFeatureBufferTarget);
                    tFeatureCursorTarget.Flush();
                }

                //释放非托管资源
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursorTarget);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursorInput);

                tWorkspaceEditTarget.StopEditOperation();
                tWorkspaceEditTarget.StopEditing(true);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //忽略编辑操作
                //tWorkspaceEditTarget.AbortEditOperation();
                tWorkspaceEditTarget.StopEditOperation();
                tWorkspaceEditTarget.StopEditing(false);
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                //throw (ex);
                return ex.Message;
            }
        }

        /// <summary>
        /// 导入源数据到目标要素类
        /// </summary>
        /// <param name="pFromFeatureClass">源属性表</param>
        /// <param name="pToFeatureClass">目标属性表</param>
        /// <param name="pDictFields">匹配规则(key为目标要素类字段索引值 value为源要素类字段索引值)</param>
        /// <param name="pQueryFilter">源要素表过滤条件</param>
        public static void ImportDataToTable(ITable pFromTable, ITable pToTable, Dictionary<int, int> pDictFields, IQueryFilter pQueryFilter)
        {
            //得到当前工作空间
            IWorkspace tWorkspace = (pToTable as IDataset).Workspace;
            //得到当前编辑空间
            IWorkspaceEdit tWorkspaceEdit = AG.COM.SDM.Utility.Editor.LibEditor.GetNewEditableWorkspace(tWorkspace);

            try
            {
                tWorkspaceEdit.StartEditing(false);
                tWorkspaceEdit.StartEditOperation();

                //设置游标状态为插入
                ICursor  tCursor1 = pToTable.Insert(true);  
                //获取源文件要素类的的记录集游标
                ICursor tCursor2 = pFromTable.Search(pQueryFilter, false); 
                //返回匹配规则枚举数
                IDictionaryEnumerator tDictEnumerator = pDictFields.GetEnumerator();

                for (IRow tRow = tCursor2.NextRow(); tRow != null; tRow = tCursor2.NextRow())
                {
                    //设置要素缓存
                    IRowBuffer tRowBuffer = pToTable.CreateRowBuffer();

                    //设置初始位置
                    tDictEnumerator.Reset();

                    while (tDictEnumerator.MoveNext() == true)
                    {
                        //获取当前要素字段值
                        object objValue = tRow.get_Value((int)tDictEnumerator.Value);

                        if (objValue == null)
                        {
                            #region 防止出现null值
                            IField tField = tRowBuffer.Fields.get_Field((int)tDictEnumerator.Key);
                            if (tField.Type == esriFieldType.esriFieldTypeDouble ||
                                tField.Type == esriFieldType.esriFieldTypeInteger ||
                                tField.Type == esriFieldType.esriFieldTypeSingle ||
                                tField.Type == esriFieldType.esriFieldTypeSmallInteger)
                            {
                                //设置值
                                tRowBuffer.set_Value((int)tDictEnumerator.Key, 0);
                            }
                            else if (tField.Type == esriFieldType.esriFieldTypeString)
                            {
                                //设置值
                                tRowBuffer.set_Value((int)tDictEnumerator.Key, "");
                            }
                            else if (tField.Type == esriFieldType.esriFieldTypeDate)
                            {
                                //设置值
                                tRowBuffer.set_Value((int)tDictEnumerator.Key, DateTime.MinValue);
                            }
                            #endregion
                        }
                        else
                        {
                            //设置值
                            tRowBuffer.set_Value((int)tDictEnumerator.Key, tRow.get_Value((int)tDictEnumerator.Value));
                        }           
                    }           
                    //插入记录
                    tCursor1.InsertRow(tRowBuffer);
                }

                //一次性写入
                tCursor1.Flush();

                //释放非托管资源
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tCursor1);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tCursor2);

                tWorkspaceEdit.StopEditOperation();
                tWorkspaceEdit.StopEditing(true);
            }
            catch (Exception ex)
            {
                //忽略编辑操作
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
        /// 返回是否已删除工作空间中指定名称的要素类
        /// </summary>
        /// <param name="pWorkspace">工作空间</param>
        /// <param name="pFeatName">要素类名称</param>
        /// <returns>如果不存在指定名称的要素类或已删除则返回true,否则返回 false</returns>
        public static bool HasDeleteFile(IWorkspace pWorkspace, string pFeatName)
        {
            bool hasDelFile = true;
  
            //查询接口引用 
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
                //提示信息
                string strMessage = string.Format("输出空间已存在相同名称[{0}]的要素类,是否删除重建?", pFeatName);

                if (MessageBox.Show(strMessage, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //查询接口引用      
                    IDataset tDataset = tFeatureClass as IDataset;
                    //删除要素类
                    tDataset.Delete();
                    //置换标识状态
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
