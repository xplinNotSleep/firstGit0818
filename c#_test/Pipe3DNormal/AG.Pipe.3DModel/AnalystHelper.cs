using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using AG.COM.SDM.Utility.Common;
using AG.COM.SDM.Utility.Display;
using AG.COM.SDM.Utility.Logger;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
namespace AG.Pipe.Analyst3DModel
{
    public static class AnalystHelper
    {
        /// <summary>
        /// 判断是否存在要素
        /// </summary>
        /// <param name="featureClassName"></param>
        /// <param name="workspace"></param>
        /// <returns></returns>
        public static bool IsExistFeatureClass(string featureClassName, IWorkspace workspace)
        {
            bool isExist = false;
            IFeatureClass fc = null;
            try
            {
                fc = (workspace as IFeatureWorkspace).OpenFeatureClass(featureClassName);
                if (fc != null)
                    isExist = true;
            }
            catch
            {
                isExist = false;
            }
            finally
            {
                if (fc != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(fc);
            }

            return isExist;
        }

        /// <summary>
        /// 生成管线模型
        /// </summary>
        /// <param name="lineFeatureClass"></param>
        /// <param name="pToFeatureClass"></param>
        /// <param name="pQueryFilter"></param>
        /// <param name="strS_HIGH"></param>
        /// <param name="strE_HIGH"></param>
        /// <param name="strPSize"></param>
        /// <param name="strS_Point"></param>
        /// <param name="strE_Point"></param>
        /// <param name="pLine3DSymbol"></param>
        /// <param name="type"></param>
        /// <param name="trackProgress"></param>
        public static bool CopyToLine3D(this IFeatureClass lineFeatureClass, IFeatureClass pToFeatureClass,
            IQueryFilter pQueryFilter, string strS_HIGH, string strE_HIGH, string strPSize, string strS_Point,
            string strE_Point, IMarker3DSymbol pLine3DSymbol, CalculationType type, ITrackProgress trackProgress)
        {
            bool IsSuccess = true;

            //得到当前要导入的三维管线模型工作空间
            IWorkspace tWorkspace = (pToFeatureClass as IDataset).Workspace;
            //得到当前编辑空间
            IWorkspaceEdit tWorkspaceEdit = tWorkspace as IWorkspaceEdit;

            tWorkspaceEdit.StartEditing(false);
            tWorkspaceEdit.StartEditOperation();

            try
            {
                //设置游标状态为插入
                IFeatureCursor tFeatureCursor1 = pToFeatureClass.Insert(true);
                //设置要素缓存
                IFeatureBuffer tFeatureBuffer = pToFeatureClass.CreateFeatureBuffer();

                //获取源文件要素类的的记录集游标
                IFeatureCursor tFeatureCursor2 = lineFeatureClass.Search(pQueryFilter, true);
                //复制每个要素
                IFeature tFeature = tFeatureCursor2.NextFeature();
                int number = 0;
                trackProgress.SubValue = 0;
                trackProgress.SubMax = lineFeatureClass.FeatureCount(pQueryFilter);
                trackProgress.SubMin = 0;
                //遍历管线二维数据的每个实体
                while (tFeature != null)
                {
                    try
                    {
                        int oid = tFeature.OID;
                        if (!trackProgress.IsContinue)
                        {
                            IsSuccess = false;
                            break;
                        }

                        trackProgress.SubValue++;
                        trackProgress.SubMessage = $"正在处理要素{trackProgress.SubValue}/{trackProgress.SubMax}" +
                            $"({pQueryFilter.WhereClause})";
                        Application.DoEvents();
                        number++;

                        if (tFeature.Shape == null || tFeature.ShapeCopy == null)
                        {
                            tFeature = tFeatureCursor2.NextFeature();
                            continue;
                        }

                        IGeometry tShapeCopy0 = tFeature.ShapeCopy;
                        IGeometry tShapeCopy = tFeature.ShapeCopy;
                        #region 这里如果是球面坐标系，则转成投影坐标才能导出正确的模型
                        IGeometry pShapeCopy = GCStoPRJ(tShapeCopy);
                        if (pShapeCopy == null)
                        {
                            //MessageBox.Show("投影转换出错!");
                            IsSuccess = false;
                            break;
                        }
                        ISpatialReference spatialReference = pShapeCopy.SpatialReference;
                        string name = spatialReference.Name;
                        #endregion
                        //IGeometry pShapeCopy = tShapeCopy;
                        IPolyline polyline = pShapeCopy as IPolyline;
                        if (polyline == null)
                        {
                            tFeature = tFeatureCursor2.NextFeature();
                            continue;
                        }

                        //将原来管线二维数据的字段值拷贝到三维数据对应字段的值中
                        IFields Flds = lineFeatureClass.Fields;
                        for (int i = 0; i < Flds.FieldCount; i++)
                        {
                            //获取对应字段的索引
                            IField Fld = Flds.get_Field(i);
                            int index = tFeatureCursor2.Fields.FindField(Fld.Name);
                            if (index < 0) continue;
                            if (!Fld.Editable) continue;
                            if (Fld.Type == esriFieldType.esriFieldTypeGeometry) continue;
                            int index1 = pToFeatureClass.FindField(Fld.Name);
                            if (index1 < 0) continue;
                            //字段赋值
                            tFeatureBuffer.set_Value(index1, tFeature.get_Value(i));
                        }

                        string S_POINT = tFeature.GetValue(strS_Point).ToString();//二维管线的起点编号
                        string E_POINT = tFeature.GetValue(strE_Point).ToString();//二维管线的终点编号

                        #region 管线的起止点编号在对应管点中寻找XY坐标字段值
                        //IQueryFilter filter1 = new QueryFilterClass();
                        //filter1.WhereClause = $"{strPNo}= '{S_POINT}' ";
                        //IFeatureCursor ptCursor1 = pointFeatureClass.Search(filter1, true);//在管点中找到起点编号记录
                        //IFeature ptFeature1 = ptCursor1.NextFeature();
                        //string FX = "";
                        //string FY = "";
                        //if (ptFeature1 != null)
                        //{
                        //    FX = ptFeature1.GetValue(strX).ToString();
                        //    FY = ptFeature1.GetValue(strY).ToString();
                        //}
                        //IQueryFilter filter2 = new QueryFilterClass();
                        //filter1.WhereClause = $"{strPNo}= '{E_POINT}' ";
                        //IFeatureCursor ptCursor2 = pointFeatureClass.Search(filter2, true);
                        //IFeature ptFeature2 = ptCursor2.NextFeature();
                        //string TX = "";
                        //string TY = "";
                        //if (ptFeature2 != null)
                        //{
                        //    TX = ptFeature2.GetValue(strX).ToString();
                        //    TY = ptFeature2.GetValue(strY).ToString();
                        //}
                        //if (FX == "" || FY == "" || TX == "" || TY == "")
                        //{
                        //    continue;
                        //}
                        #endregion

                        //若管线的起点高程或终点高程为空，则不会生成此记录的管线模型
                        string strSH = tFeature.get_Value(tFeature.Fields.FindField(strS_HIGH)).ToString();
                        string strEH = tFeature.get_Value(tFeature.Fields.FindField(strE_HIGH)).ToString();

                        if (string.IsNullOrEmpty(strSH) || string.IsNullOrEmpty(strEH))
                        {
                            tFeature = tFeatureCursor2.NextFeature();
                            continue;
                        }

                        double S_HIGH = tFeature.GetDoubleValue(strS_HIGH);
                        double E_HIGH = tFeature.GetDoubleValue(strE_HIGH);

                        //一般情况下管高的值不会刚好为0
                        if (S_HIGH == 0 || E_HIGH == 0)
                        {
                            tFeature = tFeatureCursor2.NextFeature();
                            continue;
                        }

                        double PSizeH = 0.0;//管径高
                        double PSizeW = 0.0;//管径宽
                        Line3DType dType;
                        tFeature.GetPSize(strPSize, out PSizeW, out PSizeH, out dType);

                        if (PSizeH == 0 || PSizeW == 0)
                        {
                            tFeature = tFeatureCursor2.NextFeature();
                            continue;
                        }
                        //这里默认是圆管
                        double fX = (pShapeCopy as IPolyline).FromPoint.X;
                        double fY = (pShapeCopy as IPolyline).FromPoint.Y;
                        double fZ = 0.0;
                        if (type == CalculationType.Type1)
                        {
                            fZ = S_HIGH + PSizeH / 2;
                        }
                        else
                        {
                            fZ = S_HIGH - PSizeH / 2;
                        }
                        //创建起始三维管点
                        IPoint FromPnt = PipeUtil.ConstructPnt3D(fX, fY, fZ, pShapeCopy.SpatialReference);

                        double tX = (pShapeCopy as IPolyline).ToPoint.X;
                        double tY = (pShapeCopy as IPolyline).ToPoint.Y;
                        double tZ = 0.0;
                        if (type == CalculationType.Type1)
                        {
                            tZ = E_HIGH + PSizeH / 2;
                        }
                        else
                        {
                            tZ = E_HIGH - PSizeH / 2;
                        }
                        //创建终点
                        IPoint ToPnt = PipeUtil.ConstructPnt3D(tX, tY, tZ, pShapeCopy.SpatialReference);

                        IGeometry geometry = null;
                        //根据管径类型创建不同形状的管
                        if (dType == Line3DType.Circle)
                        {
                            geometry = PipeUtil.CreateCirclePipe(FromPnt, ToPnt, PSizeH / 2, pLine3DSymbol, false, pShapeCopy.SpatialReference);
                        }
                        else
                        {
                            geometry = PipeUtil.CreateCubePipe(FromPnt, ToPnt, PSizeW, PSizeH, pLine3DSymbol, false, pShapeCopy.SpatialReference);
                        }
                        if (geometry.SpatialReference == null)
                        {
                            geometry.SpatialReference = pShapeCopy.SpatialReference;
                        }
                        //这里如果二维数据是球面坐标系，则还需重新恢复成原来的坐标参考
                        if (tShapeCopy0.SpatialReference is IGeographicCoordinateSystem)
                        {
                            geometry = ProjectTo4490(geometry);
                            double x = geometry.Envelope.XMax;
                        }

                        tFeatureBuffer.Shape = geometry;// as IGeometry;
                                                        //插入要素
                        tFeatureCursor1.InsertFeature(tFeatureBuffer);
                        if (number > 100)
                        {
                            tFeatureCursor1.Flush();
                            number = 0;
                        }
                        tFeature = tFeatureCursor2.NextFeature();
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog.LogError($"实体为{tFeature.OID}的要素出错," + ex.Message, ex);
                        tFeature = tFeatureCursor2.NextFeature();
                    }
                }
                tFeatureCursor1.Flush();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursor1);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursor2);
                GC.Collect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                IsSuccess = false;
            }
            finally
            {
                tWorkspaceEdit.StopEditOperation();
                //结束编辑
                tWorkspaceEdit.StopEditing(true);
            }
            return IsSuccess;

        }

        #region 根据连接管线获取附属物模型绘制需要关联到的管线的属性

        /// <summary>
        /// 获取管点空间位置上关联的管线的最低管线高程和对应管线的管径
        /// </summary>
        /// <param name="pointScheme"></param>
        /// <param name="p_no"></param>
        /// <param name="minPSizeH"></param>
        /// <param name="maxPSizeH"></param>
        public static void GetSubsidByLine(this PointScheme pointScheme, string p_no, out double minHigh,
            out double MaxLSizeW, out double MinLSizeH, out double LineAngle, out bool Isolated,
            ISpatialFilter spatialFilter)
        {
            minHigh = 0.0;
            MinLSizeH = 0.0;
            MaxLSizeW = 0.0;
            Isolated = false;
            double MinLSizeW = 0.0;
            LineAngle = 0.0;

            //这里先查找空间位置上跟管点相连的管线
            IQueryFilter queryFilter = spatialFilter as IQueryFilter;
            IFeatureCursor pCursor;
            IFeature feature;
            int spCount = pointScheme.LineFeatureClass.FeatureCount(queryFilter);
            if(spCount==0)
            {
                Isolated = true;
                return;
            }
            //遍历关联管线中空间上与待建模管点要素相接的要素
            pCursor = pointScheme.LineFeatureClass.Search(queryFilter, false);
            feature = pCursor.NextFeature();
            Line3DType dType = Line3DType.Circle;
            int i = 0;//循环次数
            while (feature != null)
            {
                //获取相连的管线中起点号和终点号，跟管点编号比对是否一致
                string strSNo = feature.GetValue(pointScheme.strS_Point).ToString();
                string strENo = feature.GetValue(pointScheme.strE_Point).ToString();
                //这种情况仍然无法获取到连线对应的高程
                if(p_no !=strSNo && p_no != strENo)
                {
                    feature = pCursor.NextFeature();
                    continue;
                }
                string strLH = "";
                //如果是遍历管线的起点，则获取起点高程，终点则获取终点高程
                if (p_no == strSNo)
                {
                    strLH = feature.GetValue(pointScheme.strS_Hight).ToString();
                }
                if(p_no==strENo)
                {
                    strLH = feature.GetValue(pointScheme.strE_Hight).ToString();
                }
                //如果所连线的起点高程值是否为空,则跳过遍历下一记录
                if (string.IsNullOrEmpty(strLH))
                {
                    feature = pCursor.NextFeature();
                    continue;
                }
                //如果是第一条记录
                if (i == 0)
                {
                    minHigh = Convert.ToDouble(strLH);
                    feature.GetPSize(pointScheme.strPSize, out MinLSizeW, out MinLSizeH, out dType);
                    feature.GetLineMaxSize(pointScheme.strPSize, out MaxLSizeW);
                    LineAngle = PipeUtil.GetLineAngle(feature);
                    LineAngle = LineAngle * 180 / Math.PI;
                    i++;
                }
                else
                {
                    double lineHigh = Convert.ToDouble(strLH); //起点高程
                    if (lineHigh < minHigh)
                    {
                        minHigh = lineHigh;
                        feature.GetPSize(pointScheme.strPSize, out MinLSizeW, out MinLSizeH, out dType);
                    }
                    double LSize = 0.0;
                    feature.GetLineMaxSize(pointScheme.strPSize, out LSize);
                    if (LSize > MaxLSizeW)
                    {
                        MaxLSizeW = LSize;
                    }
                }
                feature = pCursor.NextFeature();
            }

            if (minHigh == 0 && MinLSizeH == 0 && MaxLSizeW == 0)
            {
                Isolated = true;
            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(pCursor);
        }

        #endregion

        #region 根据连接管线获取附属物模型绘制需要关联到的管线的属性以及分支点
        /// <summary>
        /// 获取管点关联管线的最低管线高程和对应管线的管径
        /// </summary>
        /// <param name="pointScheme"></param>
        /// <param name="p_no"></param>
        /// <param name="minPSizeH"></param>
        /// <param name="maxPSizeH"></param>
        public static void GetFeatureByLine(this PointScheme pointScheme, string p_no, out double minHigh,
            out double MinLSizeW, out double MinLSizeH, out bool Isolated, ISpatialFilter spatialFilter)
        {
            minHigh = 0.0;
            MinLSizeH = 0.0;
            MinLSizeW = 0.0;
            Isolated = false;

            //这里先查找空间位置上跟管点相连的管线
            IQueryFilter queryFilter = spatialFilter as IQueryFilter;
            IFeatureCursor pCursor;
            IFeature feature;
            int spCount = pointScheme.LineFeatureClass.FeatureCount(queryFilter);
            if (spCount == 0)
            {
                Isolated = true;
                return;
            }
            pCursor = pointScheme.LineFeatureClass.Search(queryFilter, false);
            feature = pCursor.NextFeature();
            Line3DType dType = Line3DType.Circle;
            int i = 0;//循环次数
            while (feature != null)
            {
                //获取相连的管线中起点号和终点号，跟管点编号比对是否一致
                string strSNo = feature.GetValue(pointScheme.strS_Point).ToString();
                string strENo = feature.GetValue(pointScheme.strE_Point).ToString();
                //这种情况仍然无法获取到连线对应的高程
                if (p_no != strSNo && p_no != strENo)
                {
                    feature = pCursor.NextFeature();
                    continue;
                }
                string strLH = "";
                //如果是遍历管线的起点，则获取起点高程，终点则获取终点高程
                if (p_no == strSNo)
                {
                    strLH = feature.GetValue(pointScheme.strS_Hight).ToString();
                }
                if (p_no == strENo)
                {
                    strLH = feature.GetValue(pointScheme.strE_Hight).ToString();
                }
                if (string.IsNullOrEmpty(strLH))
                {
                    feature = pCursor.NextFeature();
                    continue;
                }
                //如果是第一条记录
                if (i == 0)
                {
                    minHigh = Convert.ToDouble(strLH);
                    feature.GetPSize(pointScheme.strPSize, out MinLSizeW, out MinLSizeH, out dType);
                    i++;
                }
                else
                {
                    double sHigh = Convert.ToDouble(strLH); //起点高程
                    if (sHigh < minHigh)
                    {
                        minHigh = sHigh;
                        feature.GetPSize(pointScheme.strPSize, out MinLSizeW, out MinLSizeH, out dType);
                    }
                }
                feature = pCursor.NextFeature();
            }

            if (minHigh == 0 && MinLSizeH == 0 && MinLSizeW == 0)
            {
                Isolated = true;
            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(pCursor);
        }

        #endregion

        #region
        ///// <summary>
        ///// 获取管点关联管线的最低管线高程和对应管线的管径
        ///// </summary>
        ///// <param name="pointScheme"></param>
        ///// <param name="p_no"></param>
        ///// <param name="minPSizeH"></param>
        ///// <param name="maxPSizeH"></param>
        //public static void GetLineHighPSize(this PointScheme pointScheme, string p_no, out double minHigh, out double maxHigh,
        //    out double PSizeW, out double PSizeH)
        //{
        //    minHigh = 0.0;
        //    maxHigh = 0.0;
        //    PSizeH = 0.0;
        //    PSizeW = 0.0;

        //    IQueryFilter queryFilter = new QueryFilterClass();
        //    IFeatureCursor pCursor;
        //    IFeature feature;
        //    //先遍历关联管线中对应的起点编号等于管点编号的要素
        //    queryFilter.WhereClause = $"{pointScheme.strS_Point}='{p_no}'";
        //    pCursor = pointScheme.LineFeatureClass.Search(queryFilter, false);
        //    feature = pCursor.NextFeature();
        //    Line3DType dType = Line3DType.Circle;
        //    int i = 0;//循环次数
        //    //如果找到有起点编号跟管点编号对应
        //    while (feature != null)
        //    {
        //        //如果是第一次循环
        //        if (i == 0)
        //        {
        //            string strMinH = feature.GetValue(pointScheme.strS_Hight).ToString();
        //            //如果所连线的起点高程值是否为空,则跳过遍历下一记录
        //            if (string.IsNullOrEmpty(strMinH))
        //            {
        //                feature = pCursor.NextFeature();
        //                continue;
        //            }

        //            minHigh = Convert.ToDouble(feature.GetValue(pointScheme.strS_Hight));
        //            maxHigh = Convert.ToDouble(feature.GetValue(pointScheme.strS_Hight));
        //            feature.GetPSize(pointScheme.strPSize, out PSizeW, out PSizeH, out dType);
        //            i++;
        //            feature = pCursor.NextFeature();
        //            continue;
        //        }
        //        string strH = feature.GetValue(pointScheme.strS_Hight).ToString();
        //        if (string.IsNullOrEmpty(strH))
        //        {
        //            feature = pCursor.NextFeature();
        //            continue;
        //        }

        //        double sHigh = Convert.ToDouble(feature.GetValue(pointScheme.strS_Hight)); //起点高程
        //        if (sHigh > maxHigh) maxHigh = sHigh;
        //        if (sHigh < minHigh)
        //        {
        //            minHigh = sHigh;
        //            feature.GetPSize(pointScheme.strPSize, out PSizeW, out PSizeH, out dType);
        //        }
        //        feature = pCursor.NextFeature();
        //    }

        //    //查询管线终点管点 
        //    queryFilter.WhereClause = $"{pointScheme.strE_Point}='{p_no}'";
        //    pCursor = pointScheme.LineFeatureClass.Search(queryFilter, false);
        //    feature = pCursor.NextFeature();
        //    if (minHigh == 0 && maxHigh == 0 && PSizeH == 0 && PSizeW == 0)
        //    {
        //        if (feature != null)
        //        {
        //            string strMinH = feature.GetValue(pointScheme.strE_Hight).ToString();
        //            if (!string.IsNullOrEmpty(strMinH))
        //            {
        //                minHigh = Convert.ToDouble(feature.GetValue(pointScheme.strE_Hight));
        //                maxHigh = Convert.ToDouble(feature.GetValue(pointScheme.strE_Hight));
        //                feature.GetPSize(pointScheme.strPSize, out PSizeW, out PSizeH, out dType);
        //            }

        //        }
        //    }

        //    while (feature != null)
        //    {
        //        string strMinH = feature.GetValue(pointScheme.strE_Hight).ToString();
        //        if (string.IsNullOrEmpty(strMinH))
        //        {
        //            feature = pCursor.NextFeature();
        //            continue;
        //        }
        //        double eHigh = Convert.ToDouble(feature.GetValue(pointScheme.strE_Hight)); //终点高程
        //        if (eHigh > maxHigh) maxHigh = eHigh;
        //        if (eHigh < minHigh)
        //        {
        //            minHigh = eHigh;
        //            feature.GetPSize(pointScheme.strPSize, out PSizeW, out PSizeH, out dType);
        //        }
        //        feature = pCursor.NextFeature();
        //    }
        //    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(pCursor);
        //}

        ///// <summary>
        ///// 获取以管点编号为起点或终点的管线的最大管径
        ///// </summary>
        ///// <param name="pointScheme"></param>
        ///// <param name="p_no"></param>
        ///// <param name="minPSize"></param>
        ///// <param name="maxPSize"></param>
        //public static void GetLinePSize(this PointScheme pointScheme, string p_no,
        //    out double maxPSize)
        //{
        //    IQueryFilter tQueryFilter;
        //    IFeatureCursor pCursor;
        //    IFeature feature;
        //    //查询管线起始管点 
        //    tQueryFilter = new QueryFilterClass();
        //    tQueryFilter.WhereClause = $"{pointScheme.strS_Point}='{p_no}'";
        //    pCursor = pointScheme.LineFeatureClass.Search(tQueryFilter, false);
        //    feature = pCursor.NextFeature();
        //    //minPSize = 0.0;
        //    maxPSize = 0.0;
        //    //获取遍历的第一条连线的管径
        //    if (feature != null)
        //    {
        //        double sPSize = 0.0;
        //        feature.GetLinePSize(pointScheme.strPSize, out sPSize);//获取管径(取大的)
        //        //minPSize = sPSize;
        //        maxPSize = sPSize;
        //    }
        //    while (feature != null)
        //    {
        //        double sPSize = 0.0;
        //        feature.GetLinePSize(pointScheme.strPSize, out sPSize);
        //        if (sPSize > maxPSize)
        //        {
        //            maxPSize = sPSize;
        //        }
        //        //if (sPSize < minPSize)
        //        //{
        //        //    minPSize = sPSize;
        //        //}
        //        feature = pCursor.NextFeature();
        //    }
        //    //查询管线终点管点 
        //    tQueryFilter.WhereClause = $"{pointScheme.strE_Point}='{p_no}'";
        //    pCursor = pointScheme.LineFeatureClass.Search(tQueryFilter, false);
        //    feature = pCursor.NextFeature();
        //    while (feature != null)
        //    {
        //        double ePSize = 0.0;
        //        feature.GetLinePSize(pointScheme.strPSize, out ePSize);
        //        if (ePSize > maxPSize)
        //        {
        //            maxPSize = ePSize;
        //        }
        //        //if (ePSize < minPSize)
        //        //{
        //        //    minPSize = ePSize;
        //        //}
        //        feature = pCursor.NextFeature();
        //    }
        //    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(pCursor);
        //}
        #endregion

        /// <summary>
        /// 获取要素管径
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="strPSize"></param>
        /// <param name="dType"></param>
        /// <param name="dDSize"></param>
        public static void GetLineMaxSize(this IFeature feature, string strPSize, out double dDSize)
        {
            double PSize = 0.0;
            string strValue = feature.GetValue(strPSize).ToString();
            char[] charSeparators = new char[] { 'X', 'x', '×' };
            string[] result = strValue.Split(charSeparators, StringSplitOptions.None);
            if (result.Length > 1)
            {
                double PSize1 = Convert.ToDouble(result[0]) / 1000;
                double PSize2 = Convert.ToDouble(result[1]) / 1000;
                PSize = PSize1 > PSize2 ? PSize1 : PSize2;
                //PSize = Convert.ToDouble(result[1]) / 1000;
            }
            else
            {
                PSize = Convert.ToDouble(result[0]) / 1000;
            }
            dDSize = PSize;
        }

        #region
        /// <summary>
        /// 判断是否是孤立点
        /// </summary>
        /// <param name="pointScheme"></param>
        /// <param name="p_no"></param>
        /// <returns></returns>
        //public static bool IsolatedPoint(this PointScheme pointScheme, string p_no)
        //{
        //    bool Isolated = true;
        //    IQueryFilter tQueryFilter;
        //    IFeatureCursor pCursor;
        //    IFeature feature;
        //    //查询管线起始管点 
        //    tQueryFilter = new QueryFilterClass();
        //    tQueryFilter.WhereClause = $"{pointScheme.strS_Point}='{p_no}'";
        //    pCursor = pointScheme.LineFeatureClass.Search(tQueryFilter, false);
        //    feature = pCursor.NextFeature();
        //    if (feature != null)
        //    {
        //        Isolated = false;
        //        return Isolated;
        //    }
        //    //查询管线终点管点 
        //    tQueryFilter.WhereClause = $"{pointScheme.strE_Point}='{p_no}'";
        //    pCursor = pointScheme.LineFeatureClass.Search(tQueryFilter, false);
        //    feature = pCursor.NextFeature();
        //    if (feature != null)
        //    {
        //        Isolated = false;
        //        return Isolated;
        //    }
        //    return Isolated;

        //}
        ///// <summary>
        ///// 根据管点编号 获取 管径大小
        ///// </summary>
        ///// <param name="pointScheme"></param>
        ///// <param name="p_no"></param>
        ///// <param name="minPSizeW"></param>
        ///// <param name="minPSizeH"></param>
        ///// <param name="maxPSizeW"></param>
        ///// <param name="maxPSizeH"></param>
        //public static void GetLinePSize(this PointScheme pointScheme, string p_no, out double minPSizeW, out double minPSizeH, out double maxPSizeW, out double maxPSizeH)
        //{
        //    IQueryFilter tQueryFilter;
        //    IFeatureCursor pCursor;
        //    IFeature feature;
        //    //查询管线起始管点 
        //    tQueryFilter = new QueryFilterClass();
        //    tQueryFilter.WhereClause = $"{pointScheme.strS_Point}='{p_no}'";
        //    pCursor = pointScheme.LineFeatureClass.Search(tQueryFilter, false);
        //    feature = pCursor.NextFeature();
        //    minPSizeW = 0.0;
        //    maxPSizeW = 0.0;
        //    minPSizeH = 0.0;
        //    maxPSizeH = 0.0;
        //    Line3DType dType = Line3DType.Circle;
        //    if (feature != null)
        //    {
        //        double sPSizeW = 0.0;
        //        double sPSizeH = 0.0;
        //        feature.GetPSize(pointScheme.strPSize, out sPSizeW, out sPSizeH, out dType);
        //        minPSizeW = sPSizeW;
        //        maxPSizeW = sPSizeW;
        //        minPSizeH = sPSizeH;
        //        maxPSizeH = sPSizeH;
        //    }
        //    while (feature != null)
        //    {
        //        double sPSizeW = 0.0;
        //        double sPSizeH = 0.0;
        //        feature.GetPSize(pointScheme.strPSize, out sPSizeW, out sPSizeH, out dType);
        //        if (sPSizeW > maxPSizeW) maxPSizeW = sPSizeW;
        //        if (sPSizeW < minPSizeW) minPSizeW = sPSizeW;

        //        if (sPSizeH > maxPSizeH) maxPSizeH = sPSizeH;
        //        if (sPSizeH < minPSizeH) minPSizeH = sPSizeH;
        //        feature = pCursor.NextFeature();
        //    }
        //    //查询管线终点管点 
        //    tQueryFilter.WhereClause = $"{pointScheme.strE_Point}='{p_no}'";
        //    pCursor = pointScheme.LineFeatureClass.Search(tQueryFilter, false);
        //    feature = pCursor.NextFeature();
        //    while (feature != null)
        //    {
        //        double sPSizeW = 0.0;
        //        double sPSizeH = 0.0;
        //        feature.GetPSize(pointScheme.strPSize, out sPSizeW, out sPSizeH, out dType);
        //        if (sPSizeW > maxPSizeW) maxPSizeW = sPSizeW;
        //        if (sPSizeW < minPSizeW) minPSizeW = sPSizeW;

        //        if (sPSizeH > maxPSizeH) maxPSizeH = sPSizeH;
        //        if (sPSizeH < minPSizeH) minPSizeH = sPSizeH;
        //        feature = pCursor.NextFeature();
        //    }
        //    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(pCursor);
        //}
        #endregion

        /// <summary>
        /// 根据管线要素管径值获取管径
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="strPSize"></param>
        /// <param name="PSizeW"></param>
        /// <param name="PSizeH"></param>
        /// <param name="dType"></param>
        public static void GetPSize(this IFeature feature, string strPSize, out double PSizeW, out double PSizeH, out Line3DType dType)
        {
            PSizeW = 0.0;
            PSizeH = 0.0;
            dType = Line3DType.Circle;
            int indexSize = feature.Fields.FindField(strPSize);
            if (indexSize == -1)
            {
                PSizeW = 0.1;
                PSizeH = PSizeW;
                dType = Line3DType.Circle;
                return;
            }

            string strValue = feature.GetValue(strPSize).ToString();
            if (string.IsNullOrWhiteSpace(strValue) || strValue == "0")
            {
                strValue = "100";//默认100毫米
                //return;
            }
            char[] charSeparators = new char[] { 'X', 'x', '×' };
            string[] result = strValue.Split(charSeparators, StringSplitOptions.None);
            try
            {
                if (result.Length > 1)
                {
                    // 宽 * 高
                    PSizeW = Convert.ToDouble(result[0]) / 1000;
                    PSizeH = Convert.ToDouble(result[1]) / 1000;
                    dType = Line3DType.Square;
                }
                else
                {
                    PSizeW = Convert.ToDouble(result[0]) / 1000;
                    PSizeH = PSizeW;
                    dType = Line3DType.Circle;
                }
            }
            catch
            {
                PSizeW = 0.1;
                PSizeH = PSizeW;
                dType = Line3DType.Circle;
            }

        }

        /// <summary>
        /// 根据附属物井盖尺寸大小及井盖类型设置要引用的模型
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="strPSize"></param>
        /// <param name="PSizeW"></param>
        /// <param name="PSizeH"></param>
        /// <param name="dType"></param>
        public static void GetSubSize(this IFeature feature, string strPSize, string strPType, out double PSizeW, out double PSizeH, out Sub3DType dType)
        {
            PSizeW = 0.0;
            PSizeH = 0.0;
            dType = Sub3DType.Circle;
            //判定附属物井盖尺寸字段和类型字段是否存在
            int indexSize = -1;
            int indexType = -1;
            indexSize = strPSize != null ? feature.Fields.FindField(strPSize) : -1;
            indexType = strPType != null ? feature.Fields.FindField(strPType) : -1;

            if (indexSize == -1 && indexSize == -1)
            {
                PSizeW = 0.1;
                PSizeH = PSizeW;
                dType = Sub3DType.Circle;
                return;
            }
            //如果附属物井盖尺寸字段存在
            if (indexSize >= 0)
            {
                //获取附属物井盖尺寸值
                string strValue = feature.GetValue(strPSize).ToString();
                if (string.IsNullOrWhiteSpace(strValue))
                {
                    strValue = "100";//默认100毫米
                }
                char[] charSeparators = new char[] { 'X', 'x', '×' };
                string[] result = strValue.Split(charSeparators, StringSplitOptions.None);
                try
                {
                    //如果附属物尺寸值为000X000格式
                    if (result.Length > 1)
                    {
                        // 宽 * 高
                        PSizeW = Convert.ToDouble(result[0]) / 1000;
                        PSizeH = Convert.ToDouble(result[1]) / 1000;
                        dType = Sub3DType.Square;
                    }
                    //如果附属物尺寸值为一个数，还要判断井盖类型
                    else
                    {
                        PSizeW = Convert.ToDouble(result[0]) / 1000;
                        PSizeH = PSizeW;
                        //如果井盖类型字段存在
                        if (indexType >= 0)
                        {
                            string type = feature.GetValue(strPType).ToString();
                            if (type.Contains("方"))
                            {
                                dType = Sub3DType.Square;
                            }
                            else if (string.IsNullOrEmpty(type))
                            {
                                dType = Sub3DType.Circle;
                            }
                            else
                            {
                                dType = Sub3DType.Circle;
                            }
                        }
                        else
                        {
                            dType = Sub3DType.Circle;
                        }
                    }
                }
                catch
                {
                    PSizeW = 0.1;
                    PSizeH = PSizeW;
                    dType = Sub3DType.Circle;
                }
            }
            else
            {
                PSizeW = 0.1;
                PSizeH = PSizeW;
                dType = Sub3DType.Circle;
                return;
            }

        }

        #region 关联埋深的调用算法
        //public static void GetLineDeepPSize(this PointScheme pointScheme, string p_no, out double minDeep, out double maxDeep,
        //    out double PSizeW, out double PSizeH)
        //{
        //    minDeep = 0.0;
        //    maxDeep = 0.0;
        //    PSizeH = 0.0;
        //    PSizeW = 0.0;

        //    //查询管线起始管点 
        //    IQueryFilter tQueryFilter;
        //    IFeatureCursor pCursor;
        //    IFeature feature;
        //    tQueryFilter = new QueryFilterClass();
        //    tQueryFilter.WhereClause = $"{pointScheme.strS_Point}='{p_no}'";
        //    pCursor = pointScheme.LineFeatureClass.Search(tQueryFilter, false);
        //    feature = pCursor.NextFeature();
        //    Line3DType dType = Line3DType.Circle;
        //    if (feature != null)
        //    {
        //        minDeep = Convert.ToDouble(feature.GetValue(pointScheme.strS_Deep)); ;
        //        maxDeep = Convert.ToDouble(feature.GetValue(pointScheme.strS_Deep)); ;
        //        feature.GetPSize(pointScheme.strPSize, out PSizeW, out PSizeH, out dType);
        //    }
        //    while (feature != null)
        //    {
        //        double sDeep = Convert.ToDouble(feature.GetValue(pointScheme.strS_Deep));
        //        if (sDeep > maxDeep) maxDeep = sDeep;
        //        if (sDeep < minDeep)
        //        {
        //            minDeep = sDeep;
        //            feature.GetPSize(pointScheme.strPSize, out PSizeW, out PSizeH, out dType);
        //        }
        //        feature = pCursor.NextFeature();
        //    }
        //    //查询管线终点管点 
        //    tQueryFilter.WhereClause = $"{pointScheme.strE_Point}='{p_no}'";
        //    pCursor = pointScheme.LineFeatureClass.Search(tQueryFilter, false);
        //    feature = pCursor.NextFeature();
        //    while (feature != null)
        //    {
        //        double eDeep = Convert.ToDouble(feature.GetValue(pointScheme.strE_Deep));
        //        if (eDeep > maxDeep) maxDeep = eDeep;
        //        if (eDeep < minDeep)
        //        {
        //            minDeep = eDeep;
        //            feature.GetPSize(pointScheme.strPSize, out PSizeW, out PSizeH, out dType);
        //        }
        //        feature = pCursor.NextFeature();
        //    }
        //    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(pCursor);
        //}

        /// <summary>
        /// 通过管线起止点高程获取管点地面高程
        /// </summary>
        /// <param name="pointScheme"></param>
        /// <param name="p_no"></param>
        /// <param name="pointZ"></param>
        //public static void GetPointZByLine(this PointScheme pointScheme, string p_no, out double pointZ)
        //{
        //    pointZ = 0.0;
        //    //double sPointZ = 0.0;
        //    //double ePointZ = 0.0;

        //    IQueryFilter queryFilter = new QueryFilterClass();
        //    IFeatureCursor pCursor;
        //    IFeature feature;
        //    //先遍历关联管线中对应的起点编号等于管点编号的要素
        //    queryFilter.WhereClause = $"{pointScheme.strS_Point}='{p_no}'";
        //    pCursor = pointScheme.LineFeatureClass.Search(queryFilter, false);
        //    feature = pCursor.NextFeature();
        //    Line3DType dType = Line3DType.Circle;
        //    if (feature != null)
        //    {
        //        double sDeep = Convert.ToDouble(feature.GetValue(pointScheme.strS_Deep));
        //        double sHigh = Convert.ToDouble(feature.GetValue(pointScheme.strS_Hight));
        //        pointZ = sHigh + sDeep;
        //    }
        //    //查询管线终点管点 
        //    queryFilter.WhereClause = $"{pointScheme.strE_Point}='{p_no}'";
        //    pCursor = pointScheme.LineFeatureClass.Search(queryFilter, false);
        //    feature = pCursor.NextFeature();
        //    if (pointZ == 0)
        //    {
        //        if (feature != null)
        //        {
        //            double eDeep = Convert.ToDouble(feature.GetValue(pointScheme.strE_Deep));
        //            double eHigh = Convert.ToDouble(feature.GetValue(pointScheme.strE_Hight));
        //            pointZ = eHigh + eDeep;
        //        }
        //    }
        //    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(pCursor);
        //}



        #endregion



        /// <summary>
        /// 获取管点模型连线的方向,根据连线方向改变模型的生成角度
        /// </summary>
        /// <param name="pointScheme"></param>
        /// <param name="p_no"></param>
        /// <param name="LineAngle"></param>
        public static void GetLineAngle(this PointScheme pointScheme, string p_no, out double LineAngle)
        {
            LineAngle = 0.0;
            double maxLineLen = 0.0;
            //bool IsSetAngle = false;

            //查询管线起始管点 
            IQueryFilter tQueryFilter;
            IFeatureCursor pCursor;
            IFeature feature;
            tQueryFilter = new QueryFilterClass();
            tQueryFilter.WhereClause = $"{pointScheme.strS_Point}='{p_no}'";
            pCursor = pointScheme.LineFeatureClass.Search(tQueryFilter, false);
            feature = pCursor.NextFeature();
            Line3DType dType = Line3DType.Circle;
            if (feature != null)
            {
                //maxLineLen = (feature as IPolyline).Length;
                maxLineLen = PipeUtil.GetLineLength(feature);
                LineAngle = PipeUtil.GetLineAngle(feature);
                LineAngle = LineAngle * 180 / Math.PI;

            }
            while (feature != null)
            {
                //double LineLen= (feature as IPolyline).Length;
                double LineLen = PipeUtil.GetLineLength(feature);

                if (LineLen > maxLineLen)
                {
                    maxLineLen = LineLen;
                    LineAngle = PipeUtil.GetLineAngle(feature);
                    LineAngle = LineAngle * 180 / Math.PI;
                }
                feature = pCursor.NextFeature();
            }

            //查询管线终点管点 
            tQueryFilter.WhereClause = $"{pointScheme.strE_Point}='{p_no}'";
            pCursor = pointScheme.LineFeatureClass.Search(tQueryFilter, false);
            feature = pCursor.NextFeature();
            //如果起点没有记录
            if (maxLineLen == 0)
            {
                if (feature != null)
                {
                    //maxLineLen = (feature as IPolyline).Length;
                    maxLineLen = PipeUtil.GetLineLength(feature);
                    LineAngle = PipeUtil.GetLineAngle(feature);
                    LineAngle = LineAngle * 180 / Math.PI;
                }
            }
            while (feature != null)
            {
                //double LineLen = (feature as IPolyline).Length;
                double LineLen = PipeUtil.GetLineLength(feature);

                if (LineLen > maxLineLen)
                {
                    maxLineLen = LineLen;
                    LineAngle = PipeUtil.GetLineAngle(feature);
                    LineAngle = LineAngle * 180 / Math.PI;
                }
                feature = pCursor.NextFeature();
            }


            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(pCursor);
        }

        /// <summary>
        /// 获取要素值
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="strField"></param>
        /// <returns></returns>
        public static object GetValue(this IFeature feature, string strField)
        {
            return feature.get_Value(feature.Fields.FindField(strField));
        }

        public static double GetDoubleValue(this IFeature feature, string strField)
        {
            string strValue = feature.get_Value(feature.Fields.FindField(strField)).ToString();
            if (string.IsNullOrWhiteSpace(strValue)) return 0;
            return Convert.ToDouble(strValue);
        }

        /// <summary>
        /// 多个字段取得唯一值。返回一个hashtable,键为唯一值的value,值为对应的个数
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static Hashtable GetUniqueValues(ITable pTable, List<string> fields)
        {
            IUniqueValueRenderer rend = new UniqueValueRendererClass();
            System.Collections.Hashtable table = new System.Collections.Hashtable();
            if (fields.Count <= 0)
            {
                return table;
            }
            ICursor cursor = pTable.Search(null, true);
            int TotalCount = pTable.RowCount(null);
            string fieldValue = "";
            IList<int> fieldIndexes = new List<int>();
            for (int i = 0; i <= fields.Count - 1; i++)
            {
                fieldIndexes.Add(cursor.Fields.FindField(fields[i]));
            }
            IRow row = cursor.NextRow();
            int j = 0;

            while (row != null)
            {
                //fieldValue = CommonFunction.GetFieldValueString(row.get_Value(-1));
                fieldValue = CommonFunction.GetFieldValueString(row.get_Value(fieldIndexes[0]));
                for (int i = 1; i <= fieldIndexes.Count - 1; i++)
                {
                    if (fieldIndexes[i] >= 0)
                    {
                        fieldValue = fieldValue + rend.FieldDelimiter + CommonFunction.GetFieldValueString(row.get_Value(fieldIndexes[i]));
                    }
                }
                if (fieldIndexes.Count == 1)
                {
                    if (fieldValue == "")
                    {
                        j++;
                        row = cursor.NextRow();
                        continue;
                    }
                }
                if (fieldIndexes.Count == 2)
                {
                    if (fieldValue == ", ")
                    {
                        j++;
                        row = cursor.NextRow();
                        continue;
                    }
                }
                if (fieldIndexes.Count == 3)
                {
                    if (fieldValue == ", , ")
                    {
                        j++;
                        row = cursor.NextRow();
                        continue;
                    }
                }
                if (table.Contains(fieldValue) == false)
                    table.Add(fieldValue, 1);
                else
                {
                    int c = (int)table[fieldValue];
                    table[fieldValue] = c + 1;  //个数加1
                }
                j++;
                row = cursor.NextRow();
            }
            //释放资源
            System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);

            return table;
        }

        /// <summary>
        /// 单个字段取得唯一值。返回一个hashtable,键为唯一值的value,值为对应的个数
        /// </summary>
        /// <param name="pTable"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static Hashtable GetUniqueValue(ITable pTable, string field)
        {
            System.Collections.Hashtable table = new System.Collections.Hashtable();
            if (string.IsNullOrEmpty(field))
            {
                return table;
            }

            int fieldIndex = pTable.FindField(field);
            if (fieldIndex < 0) return table;

            ICursor cursor = pTable.Search(null, true);
            int TotalCount = pTable.RowCount(null);
            string fieldValue = "";

            IRow row = cursor.NextRow();
            int j = 0;

            while (row != null)
            {
                //这里改成如果值为空，那么标记为空
                fieldValue = CommonFunction.GetFieldValueString(row.get_Value(fieldIndex));
                //fieldValue = fieldValue.Trim();

                if (String.IsNullOrEmpty(fieldValue))
                {
                    fieldValue = "空";
                }
                #region
                //if (fieldValue == "")
                //{
                //    j++;
                //    row = cursor.NextRow();
                //    continue;
                //}
                #endregion

                if (table.Contains(fieldValue) == false)
                    table.Add(fieldValue, 1);
                else
                {
                    int c = (int)table[fieldValue];
                    table[fieldValue] = c + 1;  //个数加1
                }
                j++;
                row = cursor.NextRow();
            }
            //释放资源
            System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);

            return table;
        }

        #region 坐标系转换
        /// <summary>
        ///  地理坐标系 转 投影坐标系
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="GCSType">地理坐标系编号</param>
        /// <param name="PRJType">投影坐标系编号</param>
        /// <returns></returns>
        public static IGeometry GCStoPRJ(IGeometry geometry)
        {
            IGeographicCoordinateSystem spatialReference = geometry.SpatialReference as IGeographicCoordinateSystem;
            //如果是球面坐标系,则需要设置投影文件
            if (spatialReference != null)
            {
                try
                {
                    ISpatialReferenceFactory pSpatialReferenceFactory = new SpatialReferenceEnvironment();
                    if (String.IsNullOrEmpty(PublicPath.ProjectPath) || !File.Exists(PublicPath.ProjectPath))
                    {
                        MessageBox.Show("转换的数据为球面坐标系，需进行投影转换方可建模，未设置投影文件路径或者设置的投影文件路径不存在!");
                        return null;
                    }

                    ISpatialReference pSpatialReference = pSpatialReferenceFactory.
                        CreateESRISpatialReferenceFromPRJFile(PublicPath.ProjectPath);

                    geometry.Project(pSpatialReference);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("投影出错，请重新设置投影文件!" + ex.Message);
                    geometry = null;
                }

            }

            return geometry;
        }

        /// <summary>
        /// 空间参考转换
        /// </summary>
        /// <returns></returns>
        public static ISpatialReference GCStoPRJ(ISpatialReference sr)
        {
            IGeographicCoordinateSystem spatialReference = sr as IGeographicCoordinateSystem;

            if (spatialReference != null)
            {
                ISpatialReferenceFactory pSpatialReferenceFactory = new SpatialReferenceEnvironment();
                if (String.IsNullOrEmpty(PublicPath.ProjectPath) || !File.Exists(PublicPath.ProjectPath))
                {
                    return spatialReference;
                }
                ISpatialReference pSpatialReference = pSpatialReferenceFactory.
                    CreateESRISpatialReferenceFromPRJFile(PublicPath.ProjectPath);

                return pSpatialReference;
            }
            return sr;

        }

        public static IGeometry ProjectTo4490(IGeometry geometry)
        {
            ISpatialReferenceFactory pSpatialReferenceFactory = new SpatialReferenceEnvironmentClass();
            ISpatialReference pSpatialReference = pSpatialReferenceFactory.CreateGeographicCoordinateSystem(4490);
            geometry.Project(pSpatialReference);
            return geometry;
        }
        #endregion


        /// <summary>
        /// 根据遍历的点要素记录设置空间查询条件(按照点要素原来的空间参考)
        /// </summary>
        /// <param name="pLocation">指定点</param>
        /// <param name="pFeatureLayer">图层</param>
        /// <returns>返回查询过滤器</returns>
        public static ISpatialFilter GetPointSpatialFilter(IPoint pLocation, double tolerace)
        {
            ISpatialFilter tSpatialFilter = new SpatialFilterClass();
            tSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

            ISpatialReference sp = pLocation.SpatialReference;
            string spName = sp.Name;

            IGeometry tGeometry = (pLocation as ITopologicalOperator).Buffer(tolerace);
            tGeometry.SnapToSpatialReference();
            ITopologicalOperator2 tTopo = tGeometry as ITopologicalOperator2;
            if (tTopo != null)
            {
                tTopo.IsKnownSimple_2 = false;
                tTopo.Simplify();
            }
            tSpatialFilter.Geometry = tGeometry;

            return tSpatialFilter;
        }


    }


}
