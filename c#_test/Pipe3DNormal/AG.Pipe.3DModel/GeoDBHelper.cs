using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using System.Data.OleDb;
using System.Data;
using ESRI.ArcGIS.Carto;

using System.Linq;
using AG.COM.SDM.Utility.Common;

namespace AG.Pipe.Analyst3DModel
{
    public class GeoDBHelper
    {
        //public static Hashtable m_BelongMapping = new Hashtable();//权属单位
        //public static Hashtable M_BTYPEMapping = new Hashtable();//埋设方式
        //public static Hashtable M_LTCODEapping = new Hashtable(); //线型

        public static bool m_IsMzPipe = false;
        public static string ConvertRecord = "";
        public static string ConvertLineRecord = "";//转换的线要素记录
        public static string ConvertPointRecord = "";//转换的点要素记录

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
        public static IFeatureClass CreateFeatureClass(object pObject, string pName, esriFeatureType pFeatureType, IFields pFields, UID pUidClsId, UID pUidClsExt, string pConfigWord)
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
                        //if (pGeometryType == esriGeometryType.esriGeometryLine)
                        //    pGeometryType = esriGeometryType.esriGeometryPolyline;

                        pUidClsId.Value = "{52353152-891A-11D0-BEC6-00805F7C4268}";
                        //pUidClsId.Value = "esriGeoDatabase.Feature";
                        break;
                    case (esriFeatureType.esriFTSimpleJunction):
                        //pGeometryType = esriGeometryType.esriGeometryPoint;
                        pUidClsId.Value = "{CEE8D6B8-55FE-11D1-AE55-0000F80372B4}";
                        break;
                    case (esriFeatureType.esriFTComplexJunction):
                        pUidClsId.Value = "{DF9D71F4-DA32-11D1-AEBA-0000F80372B4}";
                        break;
                    case (esriFeatureType.esriFTSimpleEdge):
                        //pGeometryType = esriGeometryType.esriGeometryPolyline;
                        pUidClsId.Value = "{E7031C90-55FE-11D1-AE55-0000F80372B4}";
                        break;
                    case (esriFeatureType.esriFTComplexEdge):
                        //pGeometryType = esriGeometryType.esriGeometryPolyline;
                        pUidClsId.Value = "{A30E8A2A-C50B-11D1-AEA9-0000F80372B4}";
                        break;
                    case (esriFeatureType.esriFTAnnotation):
                        //pGeometryType = esriGeometryType.esriGeometryPolygon;
                        pUidClsId.Value = "{E3676993-C682-11D2-8A2A-006097AFF44E}";
                        break;
                    case (esriFeatureType.esriFTDimension):
                        //pGeometryType = esriGeometryType.esriGeometryPolygon;
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

            #region 字段集合为空时
            /*
            if (pFields == null)
            {
                //实倒化字段集合对象
                pFields = new FieldsClass();
                IFieldsEdit tFieldsEdit = (IFieldsEdit)pFields;

                //创建几何对象字段定义
                IGeometryDef tGeometryDef = new GeometryDefClass();
                IGeometryDefEdit tGeometryDefEdit = tGeometryDef as IGeometryDefEdit;

                //指定几何对象字段属性值
                tGeometryDefEdit.GeometryType_2 = pGeometryType;
                tGeometryDefEdit.GridCount_2 = 1;
                tGeometryDefEdit.set_GridSize(0, 1000);
                if (pObject is IWorkspace)
                {
                    tGeometryDefEdit.SpatialReference_2 = new UnknownCoordinateSystemClass();
                }

                //创建OID字段
                IField fieldOID = new FieldClass();
                IFieldEdit fieldEditOID = fieldOID as IFieldEdit;
                fieldEditOID.Name_2 = "OBJECTID";
                fieldEditOID.AliasName_2 = "OBJECTID";
                fieldEditOID.Type_2 = esriFieldType.esriFieldTypeOID;
                tFieldsEdit.AddField(fieldOID);

                //创建几何字段
                IField fieldShape = new FieldClass();
                IFieldEdit fieldEditShape = fieldShape as IFieldEdit;
                fieldEditShape.Name_2 = "SHAPE";
                fieldEditShape.AliasName_2 = "SHAPE";
                fieldEditShape.Type_2 = esriFieldType.esriFieldTypeGeometry;
                fieldEditShape.GeometryDef_2 = tGeometryDef;
                tFieldsEdit.AddField(fieldShape);
            } */
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

        /// <summary>
        /// 创建字段
        /// </summary>
        /// <param name="pTable"></param>
        /// <param name="pFieldMappingTable"></param>
        /// <param name="pGeometryType"></param>
        /// <returns></returns>
        public static IFields CreateFields(ITable pTable, esriGeometryType pGeometryType, ISpatialReference pSpatialReference)
        {
            IClone tClone = pTable.Fields as IClone;
            bool HasOID = false;
            bool HasShape = false;
            IFields pFields = tClone.Clone() as IFields;
            IFieldsEdit pFieldsEdit = pFields as IFieldsEdit;
            for (int i = 0; i < pFieldsEdit.FieldCount; i++)
            {
                IField pFieldz = pFieldsEdit.get_Field(i);
                if (pFieldz.Type == esriFieldType.esriFieldTypeOID)
                {
                    HasOID = true;
                }
                if (pFieldz.Name.ToUpper() == "OBJECTID")
                {
                    HasOID = true;
                }
                if (pFieldz.Type == esriFieldType.esriFieldTypeGeometry)
                {
                    HasShape = true;

                }

                IFieldEdit pFieldEditz = pFieldz as IFieldEdit;
                //去arcgis 主要字段  arcgis 内字段 会带有_
                if (pFieldz.Name.Contains("_"))
                {
                    pFieldEditz.Name_2 = pFieldz.Name.TrimEnd('_');
                }
            }
            if (HasOID == false)
            {
                //创建OID字段
                IField fieldOID = new FieldClass();
                IFieldEdit fieldEditOID = fieldOID as IFieldEdit;
                fieldEditOID.Name_2 = "OBJECTID";
                fieldEditOID.AliasName_2 = "OBJECTID";
                fieldEditOID.IsNullable_2 = false;
                fieldEditOID.Type_2 = esriFieldType.esriFieldTypeOID;
                pFieldsEdit.AddField(fieldOID);
            }

            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = pField as IFieldEdit;
            if (!(pGeometryType == esriGeometryType.esriGeometryAny) && HasShape == false)
            {
                pFieldEdit.Name_2 = "shape";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                ISpatialReference pSR = null;
                if (pSpatialReference == null)
                {
                    pSR = new UnknownCoordinateSystemClass();
                    pSR.SetDomain(-450359962737.05, 450359962737.05, -450359962737.05, 450359962737.05);
                    ISpatialReferenceTolerance pTorlerance;
                    pTorlerance = (ISpatialReferenceTolerance)pSR;
                    pTorlerance.SetDefaultXYTolerance();
                    pTorlerance.SetDefaultZTolerance();
                }
                else
                {
                    pSR = pSpatialReference;
                    //pSR.SetDomain(-450359962737.05, 450359962737.05, -450359962737.05, 450359962737.05);
                    //pSpatialReference.GetDomain()
                }
                IGeometryDef pGeomDef = new GeometryDefClass();
                IGeometryDefEdit pGeomDefEdit = pGeomDef as IGeometryDefEdit;

                pGeomDefEdit.GeometryType_2 = pGeometryType;

                pGeomDefEdit.SpatialReference_2 = pSR;
                pGeomDefEdit.GridCount_2 = 1;

                pGeomDefEdit.set_GridSize(0, 0);
                pGeomDefEdit.HasM_2 = false;
                pGeomDefEdit.HasZ_2 = false;

                pFieldEdit.GeometryDef_2 = pGeomDef;
                pFieldsEdit.AddField(pField);
            }

            return pFields;


        }

        /// <summary>
        /// 根据mdb表中的xy坐标生成点要素
        /// </summary>
        /// <param name="pFeatureClass">生成的点要素类</param>
        /// <param name="pDataTable">源数据表</param>
        /// <param name="strX">X坐标字段</param>
        /// <param name="strY">Y坐标字段</param>
        public static void CreatePointFeature(IFeatureClass pFeatureClass, ITable pDataTable, string strX, string strY, string strZ, string pNo,
            ITrackProgress trackProgress)
        {
            //得到当前工作空间
            IWorkspace tWorkspace = (pFeatureClass as IDataset).Workspace;
            //得到当前编辑空间
            IWorkspaceEdit tWorkspaceEdit = (IWorkspaceEdit)tWorkspace;
            //设置游标状态为插入
            IFeatureCursor tFeatureCursor1 = pFeatureClass.Insert(true);

            tWorkspaceEdit.StartEditing(false);
            tWorkspaceEdit.StartEditOperation();

            //验证下目标要素类的字段是否已导入
            List<string> Names = new List<string>();
            List<string> AliasNames = new List<string>();
            for (int k = 0; k < pFeatureClass.Fields.FieldCount; k++)
            {
                IField field = pFeatureClass.Fields.Field[k];
                string name = field.Name;
                string aliasName = field.AliasName;
                Names.Add(name);
                AliasNames.Add(aliasName);
            }

            IGeometry Geometry;
            IQueryFilter t = new QueryFilterClass();
            t.SubFields = "";
            t.WhereClause = "";
            ICursor tCursor = null;
            tCursor = pDataTable.Search(null, true);
            int rowCount = pDataTable.RowCount(t);
            trackProgress.TotalMessage = "正在生成管点图层:" + pFeatureClass.AliasName;
            trackProgress.SubMax = rowCount;
            trackProgress.SubMin = 0;
            
            Application.DoEvents();

            IRow tRow = tCursor.NextRow();
            int DoneCount = 0;
            string ErrorRecord = "";
            int ipNo = pDataTable.Fields.FindField(pNo);
            while (tRow != null)
            {
                try
                {
                    trackProgress.SubValue++;
                    trackProgress.SubMessage = $"正在生成管点:{DoneCount+1}/{rowCount}";
                    Application.DoEvents();

                    IFeatureBuffer featureBuffer = pFeatureClass.CreateFeatureBuffer();
                    IPoint pPoint = new PointClass();
                    if (strX != string.Empty && strY != string.Empty)
                    {
                        //int iX = tRow.Fields.FindFieldByAliasName(strX);
                        //int iY = tRow.Fields.FindFieldByAliasName(strY);
                        int iX = tRow.Fields.FindField(strX);
                        int iY = tRow.Fields.FindField(strY);

                        if (iX == -1 || iY == -1) continue;
                        try
                        {
                            //廊坊项目 中X、Y 坐标是相反的
                            pPoint.X = Convert.ToDouble(tRow.get_Value(iX));
                            pPoint.Y = Convert.ToDouble(tRow.get_Value(iY));
                        }
                        catch
                        {
                            tRow = tCursor.NextRow();
                            continue;
                        }
                    }

                    //管点高程
                    if (String.IsNullOrEmpty(strZ))
                    {
                        int iZ = tRow.Fields.FindField(strZ);
                        if (iZ != -1)
                        {
                            string zValue = tRow.get_Value(iZ).ToString();
                            double z = 0.0;
                            if (!string.IsNullOrEmpty(zValue)&&Double.TryParse(zValue,out z))
                            {
                                pPoint.Z = Convert.ToDouble(tRow.get_Value(iZ));
                            }
                        }

                    }
                    IGeometry pGeo = pPoint as IGeometry;
                    #region
                    //pGeo.Envelope
                    //if(pGeo.SpatialReference==null)
                    //{
                    //    IFeatureClass pFeatureClass1 = (AG.COM.SDM.Utility.CommonVariables.DatabaseConfig.Workspace as IFeatureWorkspace).OpenFeatureClass((pFeatureClass as IDataset).Name);
                    //    IGeoDataset pGeodataSet = pFeatureClass1 as IGeoDataset;
                    //    ISpatialReference pSpatialReference = pGeodataSet.SpatialReference;
                    //    pSpatialReference.SetDomain(-450359962737.05, 450359962737.05, -450359962737.05, 450359962737.05);
                    //    pGeo.SpatialReference = pSpatialReference;

                    //}
                    #endregion
                    featureBuffer.Shape = pGeo;

                    for (int j = 0; j < tRow.Fields.FieldCount; j++)
                    {
                        IField tField = new FieldClass();
                        IField trField = tRow.Fields.Field[j];
                        //判断目标要素类是否存在导入数据表的字段名或字段别名
                        int index = featureBuffer.Fields.FindField(trField.Name);
                        int index1 = 0;
                        if (index < 0)
                        {
                            index1 = featureBuffer.Fields.FindField(trField.AliasName);
                            if (index1 < 0) continue;
                            tField = featureBuffer.Fields.get_Field(index1);
                        }
                        else
                        {
                            tField = featureBuffer.Fields.get_Field(index);
                        }
                        //IField tField = featureBuffer.Fields.get_Field(index);
                        if (tField.Editable)
                        {
                            #region 针对廊坊CS项目的
                            //if (tField.Name.ToUpper() == "BELONG" && m_BelongMapping.Count != 0)
                            //{
                            //    if (m_BelongMapping.ContainsKey(tRow.get_Value(j)))
                            //    {
                            //        featureBuffer.set_Value(index, m_BelongMapping[tRow.get_Value(j)]);

                            //    }
                            //    else
                            //    {
                            //        featureBuffer.set_Value(index, tRow.get_Value(j));
                            //    }
                            //}
                            //else if (tField.Name.ToUpper() == "ROTATION")
                            //{
                            //    try
                            //    {
                            //        double tRotation = Convert.ToDouble(tRow.get_Value(j));
                            //        featureBuffer.set_Value(index, tRotation * 180 / 3.1415926 + 90);
                            //    }
                            //    catch
                            //    { }
                            //}
                            //else if (tField.Name.ToUpper() == "FGUID")
                            //{
                            //    featureBuffer.set_Value(index, tRow.get_Value(ipNo));
                            //}
                            #endregion
                            featureBuffer.set_Value(index, tRow.get_Value(j));

                        }
                    }


                    tFeatureCursor1.InsertFeature(featureBuffer);
                    DoneCount++;
                }
                catch (Exception ex)
                {
                    ErrorRecord += string.Format("编号为"+tRow.get_Value(0).ToString()+"转换报错,"+ex.Message);
                    DoneCount++;
                }
                finally
                {
                    tRow = tCursor.NextRow();
                }
            }
            //一次性写入
            tFeatureCursor1.Flush();
            if (ErrorRecord == "")
            {
                ConvertPointRecord = string.Format("{0},已转换记录数:{1}, , ,", pFeatureClass.AliasName, DoneCount.ToString() + "/" + pDataTable.RowCount(null).ToString());
            }
            else
            {
                ConvertPointRecord = string.Format("{0},已转换记录数:{1};未转换记录:{2}, , ,", pFeatureClass.AliasName, DoneCount.ToString() + "/" + pDataTable.RowCount(null).ToString());
            }
            //释放非托管资源

            tWorkspaceEdit.StopEditOperation();
            tWorkspaceEdit.StopEditing(true);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursor1);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(tWorkspace);

        }

        /// <summary>
        /// 对选中方案的小类图层集进行图形转换
        /// </summary>
        /// <param name="pLayerSet"></param>
        /// <param name="pConvertManager"></param>
        public static void StartConvert(ConvertLayerSet pLayerSet, PipeConvertManager pConvertManager,
            ITrackProgress trackProgress, IWorkspace tWorkspace)
        {
            if (pLayerSet.LineSource == null || pLayerSet.PointSource == null)
            {
                MessageBox.Show("未配置图层的数据源，无法生成图层数据。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            if (pLayerSet.PointLayerType == null || pLayerSet.LineLayerType == null)
            {
                MessageBox.Show("请设置线或点图层类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (String.IsNullOrEmpty(pLayerSet.PointX) || String.IsNullOrEmpty(pLayerSet.PointY))
            {
                MessageBox.Show("未配置图层的空间信息，无法生成图层数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //if (pLayerSet.Points.Count != 1)
            //{
            //    MessageBox.Show(pLayerSet.PointItemName + "为点图层，需配置仅一个点坐标信息！");
            //    return;
            //}
            //点数据图形化
            bool isCreatPt = CreatPointLayer(tWorkspace, pLayerSet, pConvertManager, trackProgress);

            if (!isCreatPt) return;

            IDataset tDataset = pLayerSet.LineSource as IDataset;
            string mdbPath = tDataset.Workspace.PathName;
            string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + mdbPath;
            OleDbConnection odcConnection = new OleDbConnection(strConn);

            try
            {
                IFields tFields = GeoDBHelper.CreateFields(pLayerSet.LineSource,
                    esriGeometryType.esriGeometryPolyline, pConvertManager.SpatialReference);

                string pathName = tWorkspace.PathName;

                List<string> fieldNames = new List<string>();
                for (int i = 0; i < tFields.FieldCount; i++)
                {
                    string name = tFields.Field[i].Name;
                    fieldNames.Add(name);
                }

                IFeatureClass tFeatureClass = GeoDBHelper.CreateFeatureClass(tWorkspace,
                    pLayerSet.LineLayerName.ToUpper(), esriFeatureType.esriFTSimple, tFields, null, null, "");

                GeoDBHelper.CreatePolylineFeature(tFeatureClass, pLayerSet, odcConnection, trackProgress);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show(e.Message);
            }
            finally
            {
                odcConnection.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pLayerSet"></param>
        /// <param name="pConvertManager"></param>
        /// <returns></returns>
        public static bool CreatPointLayer(IWorkspace tWorkspace,ConvertLayerSet pLayerSet, PipeConvertManager pConvertManager,
            ITrackProgress trackProgress)
        {
            try
            {
                //从转换方案中引用的管点数据源获取字段集合
                IFields tFields = GeoDBHelper.CreateFields(pLayerSet.PointSource, esriGeometryType.esriGeometryPoint,
                    pConvertManager.SpatialReference);

                string pathName = tWorkspace.PathName;

                List<string> fieldNames = new List<string>();
                for (int i = 0; i < tFields.FieldCount; i++)
                {
                    string name = tFields.Field[i].Name;
                    fieldNames.Add(name);
                }

                IFeatureClass tFeatureClass = GeoDBHelper.CreateFeatureClass(tWorkspace, pLayerSet.PointLayerName.ToUpper(),
                    esriFeatureType.esriFTSimple, tFields, null, null, "");
                GeoDBHelper.CreatePointFeature(tFeatureClass, pLayerSet.PointSource, pLayerSet.PointX,
                     pLayerSet.PointY, pLayerSet.HighFieldName, pLayerSet.ExpNoFieldName,
                     trackProgress);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        /// <summary>
        /// 根据mdb中的表数据生成线要素,根据表中管点的坐标
        /// </summary>
        /// <param name="pFeatureClass"></param>
        /// <param name="pLineDataTable"></param>
        /// <param name="m_Points"></param>
        private static void CreatePolylineFeature(IFeatureClass pFeatureClass, ConvertLayerSet pLayerSet, 
            OleDbConnection odcConnection, ITrackProgress trackProgress)
        {
            //得到当前工作空间
            IWorkspace tWorkspace = (pFeatureClass as IDataset).Workspace;
            //得到当前编辑空间
            IWorkspaceEdit tWorkspaceEdit = (IWorkspaceEdit)tWorkspace;

            tWorkspaceEdit.StartEditing(false);
            tWorkspaceEdit.StartEditOperation();

            List<string> fieldNames = new List<string>();
            for (int i = 0; i < pFeatureClass.Fields.FieldCount; i++)
            {
                string fName = pFeatureClass.Fields.Field[i].Name;
                fieldNames.Add(fName);
            }

            IGeometry Geometry;
            //设置游标状态为插入
            IFeatureCursor tFeatureCursor1 = pFeatureClass.Insert(true);
            IQueryFilter t = new QueryFilterClass();
            t.SubFields = "";
            t.WhereClause = "";
            ICursor tCursor = null;
            tCursor = pLayerSet.LineSource.Search(null, true);
            int RowCount = pLayerSet.LineSource.RowCount(null);

            trackProgress.TotalMessage = "正在生成管线图层:" + pFeatureClass.AliasName;
            trackProgress.SubMax = RowCount;
            trackProgress.SubMin = 0;
            Application.DoEvents();

            IRow tRow = tCursor.NextRow();
            int DoneCount = 0;
            while (tRow != null)
            {
                IFeatureBuffer featureBuffer = pFeatureClass.CreateFeatureBuffer();
                IPointCollection tPointCollection = new PolylineClass();
                try
                {
                    if (tRow != null)
                    {
                        trackProgress.SubValue++;
                        trackProgress.SubMessage = $"正在生成管线:{DoneCount + 1}/{RowCount}";
                        Application.DoEvents();

                        string S_Exp = tRow.get_Value(tRow.Fields.FindField(pLayerSet.SPointFieldName)).ToString();
                        string E_Exp = tRow.get_Value(tRow.Fields.FindField(pLayerSet.EPointFieldName)).ToString();
                        if (S_Exp == "" || E_Exp == "")
                        {
                            tRow = tCursor.NextRow();
                            continue;
                        }

                        object o = Type.Missing;
                        IPoint S_point = GetLinePoint(S_Exp, pLayerSet, odcConnection);
                        IPoint E_point = GetLinePoint(E_Exp, pLayerSet, odcConnection);

                        if (S_point == null || E_point == null)
                        {
                            tRow = tCursor.NextRow();
                            continue;
                        }
                        tPointCollection.AddPoint(S_point, ref o, ref o);
                        tPointCollection.AddPoint(E_point, ref o, ref o);
                    }
                    IGeometry pGeo = tPointCollection as IGeometry;
                    featureBuffer.Shape = pGeo;

                    for (int j = 0; j < tRow.Fields.FieldCount; j++)
                    {
                        IField trField = tRow.Fields.get_Field(j);
                        int index = featureBuffer.Fields.FindField(trField.Name);
                        if (index < 0) continue;
                        IField tField = featureBuffer.Fields.get_Field(index);
                        if (tField.Editable)
                        {
                            #region 这里先按照权属代码导出，后面再重新编制配置字典
                            //if (tField.Name.ToUpper() == "BELONG" && m_BelongMapping.Count != 0)
                            //{
                            //    if (m_BelongMapping.ContainsKey(tRow.get_Value(j)))
                            //    {
                            //        featureBuffer.set_Value(index, m_BelongMapping[tRow.get_Value(j)]);
                            //    }
                            //    else
                            //    {
                            //        featureBuffer.set_Value(index, tRow.get_Value(j));
                            //    }
                            //}
                            #endregion
                            featureBuffer.set_Value(index, tRow.get_Value(j));

                        }
                    }

                    tFeatureCursor1.InsertFeature(featureBuffer);
                    DoneCount++;
                    tRow = tCursor.NextRow();
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"记录{DoneCount+1}生成有错:错误为{ex.Message}");
                    DoneCount++;
                    tRow = tCursor.NextRow();
                }
            }
            //一次性写入
            tFeatureCursor1.Flush();

            ConvertLineRecord = string.Format("{0},已转换记录数:{1}", pFeatureClass.AliasName,
                DoneCount.ToString() + "/" + pLayerSet.LineSource.RowCount(null).ToString());

            //释放非托管资源

            tWorkspaceEdit.StopEditOperation();
            tWorkspaceEdit.StopEditing(true);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursor1);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(tWorkspace);
        }

        /// <summary>
        /// 获取线两端点的坐标
        /// </summary>
        /// <param name="LineExp"></param>
        /// <param name="dataCheckLayerSet"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private static IPoint GetLinePoint(string Exp, ConvertLayerSet pLayerSet, OleDbConnection odcConnection)
        {
            IPoint tPoint = new PointClass();
            string X = pLayerSet.PointX;
            string Y = pLayerSet.PointY;
            string S_SQL = string.Format("select {3},{4} from {0} where {1} ='{2}'", pLayerSet.PointLayerName, pLayerSet.ExpNoFieldName, Exp, X, Y);
            odcConnection.Open();
            OleDbCommand odCommand = odcConnection.CreateCommand();
            odCommand.CommandText = S_SQL;
            OleDbDataReader odrReader = odCommand.ExecuteReader();
            if (odrReader.Read())
            {
                tPoint.X = Convert.ToDouble(odrReader[X]);
                tPoint.Y = Convert.ToDouble(odrReader[Y]);
            }
            else
            {
                string message = string.Format("未找到{0}表中{1}字段值为{2}的记录",
                    pLayerSet.PointLayerName, pLayerSet.ExpNoFieldName, Exp);
                //MessageBox.Show(message);
                tPoint = null;
            }
            odcConnection.Close();
            return tPoint;
        }


    }
}
