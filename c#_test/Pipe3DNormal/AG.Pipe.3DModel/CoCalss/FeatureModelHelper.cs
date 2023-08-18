using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using AG.COM.SDM.Utility;
using AG.COM.SDM.Utility.Common;
using AG.COM.SDM.Utility.Logger;
using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.Pipe.Analyst3DModel
{
    public class FeatureModelHelper
    {
        /// <summary>
        /// 根据管点方案中的附属物相关字段值找到3D符号样式，并绘制附属物
        /// </summary>
        /// <param name="pWorkspace"></param>
        /// <param name="LayerName"></param>
        /// <param name="strValue"></param>
        /// <param name="marker3DSymbol"></param>
        /// <param name="pointScheme"></param>
        /// <param name="tQueryFilter"></param>
        /// <param name="trackProgress"></param>
        public static bool CreateFSW(IWorkspace pWorkspace, string Layer3dName, string strValue, PointScheme pointScheme,
            IFeatureClass pToFeatureClass, IQueryFilter tQueryFilter, Dictionary<string, IMarker3DSymbol> dic3DSymbol, ITrackProgress trackProgress)
        {
            bool IsSuccess = true;
            try
            {
                //先查找附属物尺寸字段和地面高程字段是否存在
                int subSizeIndex = -1;
                int pointZIndex = -1;

                subSizeIndex = pointScheme.strSubsidSize != null ?
                    pointScheme.PointFeatureClass.FindField(pointScheme.strSubsidSize) : -1;
                pointZIndex = pointScheme.strZ != null ?
                    pointScheme.PointFeatureClass.FindField(pointScheme.strZ) : -1;//判断管点数据源的地面高程字段是否存在

                //判断管点地面高程字段是否存在，如果不存在则无法建模
                if (pointZIndex == -1)
                {
                    IsSuccess = false;
                    return false;
                }

                double tolerace = 0.0;
                //先判断空间查询所需缓冲半径
                IGeoDataset gds = pointScheme.PointFeatureClass as IGeoDataset;
                if(gds.SpatialReference is IGeographicCoordinateSystem)
                {
                    tolerace = 0.0000002;
                }
                else
                {
                    tolerace = 0.1;
                }

                trackProgress.SubValue = 0;
                trackProgress.SubMessage = $"正在创建{Layer3dName}附属物{strValue}";
                Application.DoEvents();
                

                IFeatureClass pFromFeatureClass = pointScheme.PointFeatureClass;
                //设置游标状态为插入
                IFeatureCursor tFeatureCursor = pToFeatureClass.Insert(true);
                //设置要素缓存
                IFeatureBuffer tFeatureBuffer = pToFeatureClass.CreateFeatureBuffer();
                //获取源文件要素类的的记录集游标
                IFeatureCursor tFeatureCursor2 = pFromFeatureClass.Search(tQueryFilter, true);
                //复制每个要素
                IFeature tFeature = tFeatureCursor2.NextFeature();
                trackProgress.SubValue = 0;
                trackProgress.SubMax = pFromFeatureClass.FeatureCount(tQueryFilter);
                int number = 0;

                int singleNum = 0;
                int noHighNum = 0;
                while (tFeature != null)
                {
                    try
                    {
                        if (!trackProgress.IsContinue) break;
                        number++;
                        trackProgress.SubValue++;
                        trackProgress.SubMessage = $"正在生成{Layer3dName}模型{trackProgress.SubValue}/{trackProgress.SubMax}({strValue})";
                        Application.DoEvents();

                        IGeometry tShapeCopy0 = tFeature.ShapeCopy;
                        //求出管点附属物所在的空间位置坐标(原来的空间参考)
                        IPoint pPoint = tShapeCopy0 as IPoint;
                        if (pPoint == null)
                        {
                            MessageBox.Show($"选择的{Layer3dName}管点数据源其实不是点图层!");
                            IsSuccess = false;
                            break;
                        }
                        //初始化空间查询条件
                        ISpatialFilter spatialFilter = AnalystHelper.GetPointSpatialFilter(pPoint, tolerace);

                        IGeometry tShapeCopy = tFeature.ShapeCopy;
                        #region 如果是球面坐标系，则需要转成投影坐标系再进行空间位置的计算
                        IGeometry pShapeCopy = AnalystHelper.GCStoPRJ(tShapeCopy);
                        if (pShapeCopy == null)
                        {
                            IsSuccess = false;
                            break;
                        }
                        ISpatialReference spatialReference = pShapeCopy.SpatialReference;
                        string name = spatialReference.Name;
                        #endregion

                        //获取管点编号
                        string sPno = tFeature.GetValue(pointScheme.strPNo).ToString();
                        if(string.IsNullOrEmpty(sPno))
                        {
                            tFeature = tFeatureCursor2.NextFeature();
                            continue;
                        }

                        //判断管点所关联管线是否有起止点高程字段，若存在则按照关联管线的端点高程
                        int shIndex = pointScheme.LineFeatureClass.FindField(pointScheme.strS_Hight);
                        int ehIndex = pointScheme.LineFeatureClass.FindField(pointScheme.strE_Hight);
                        if (shIndex < 0 || ehIndex < 0)
                        {
                            IsSuccess = false;
                            break;
                        }

                        
                        double dX = (pShapeCopy as IPoint).X;
                        double dY = (pShapeCopy as IPoint).Y;
                        double dZ = 0.0;

                        #region 根据管点点号从关联管线数据中获取管点模型绘制所需的一些属性
                        bool Isolated = false;
                        double maxPSizeW = 0.0;
                        double lineAngle = 0.0;
                        double minHigh = 0.0;
                        double minLSizeH = 0.0;

                        //需要通过关联管线获取连接的管线的最大管径、最长管线的旋转角、最低高程值、管线的横截面高
                        //之前是通过管点编号在管线中查找关联管线，现在修改成通过空间位置去遍历，这样更准确些
                        pointScheme.GetSubsidByLine(sPno, out minHigh,out maxPSizeW, out minLSizeH, out lineAngle, out Isolated,
                            spatialFilter);

                        //判断孤立节点
                        if (Isolated)
                        {
                            singleNum++;
                            tFeature = tFeatureCursor2.NextFeature();
                            continue;
                        }
                        #endregion
                        
                        double ptHeight = 0.0;//地面高

                        //判断地面高程值是否为空，若为空的话则不再继续进行建模，跳到下一条记录
                        string strZ = Convert.ToString(tFeature.GetValue(pointScheme.strZ));
                        if (string.IsNullOrWhiteSpace(strZ) || strZ.Trim() == "0")
                        {
                            noHighNum++;
                            tFeature = tFeatureCursor2.NextFeature();
                            continue;
                        }
                        else
                        {
                            ptHeight = Convert.ToDouble(tFeature.GetValue(pointScheme.strZ));
                        }

                        #region
                        //double PSizeW = 0.0;
                        //double PSizeH = 0.0;
                        //double minHigh = 0;
                        //double maxHigh = 0;
                        //获取高度最低的连接管线(第一次遍历管线)
                        //pointScheme.GetLineHighPSize(sPno, out minHigh, out maxHigh, out PSizeW, out PSizeH);
                        #endregion
                        //管点高程 = 关联管线端点最低高程 + 1 / 2管径
                        if (pointScheme.CalculationType == CalculationType.Type1)
                        {
                            dZ = minHigh - 0.1;
                        }
                        else
                        {
                            dZ = minHigh - minLSizeH - 0.1;
                        }
                        
                        double SubsidHeight = ptHeight-dZ;
                       
                        #region 这里暂时用不到井深字段
                        //string strDeep = Convert.ToString(tFeature.GetValue(pointScheme.strDeep));
                        //if (string.IsNullOrWhiteSpace(strDeep) || strDeep.Trim() == "0")
                        //{
                        //    tFeature = tFeatureCursor2.NextFeature();
                        //    continue;
                        //}
                        //else
                        //{
                        //    SubsidHeight = Convert.ToDouble(tFeature.GetValue(pointScheme.strDeep));
                        //    dZ = ptHeight - SubsidHeight;
                        //}
                        #endregion
                        IPoint CenterPoint = PipeUtil.ConstructPnt3D(dX, dY, dZ, pShapeCopy.SpatialReference);

                        IGeometry pGeometry = null;
                        //根据井盖尺寸值生成附属物模型是矩形还是圆形
                        bool IsSetSize = false;//是否设置3D符号的高度

                        //根据管点附属物获取附属物模型
                        double sPSizeW = 0.0;
                        double sPSizeH = 0.0;
                        Sub3DType pType = Sub3DType.Circle;//先设附属物横截面默认类型
                        string PSizeValue = "";
                        //如果不存在附属物大小字段或未设置该字段
                        if (subSizeIndex < 0)
                        {
                            //将关联管线的最大管径值再加上10厘米作为附属物大小值
                            sPSizeW = maxPSizeW ;
                            sPSizeH = maxPSizeW ;

                            List<string> subsids = IsAddSizeSubsid();
                            if (subsids.Contains(strValue) || strValue.Contains("井") || strValue.Contains("孔"))
                            {
                                IsSetSize = true;
                            }
                            else
                            {
                                IsSetSize = false;
                            }
                        }
                        //如果存在附属物大小字段
                        if (subSizeIndex >= 0)
                        {
                            PSizeValue = tFeature.GetValue(pointScheme.strSubsidSize).ToString();
                            //如果附属物尺寸值为空
                            if (string.IsNullOrWhiteSpace(PSizeValue))
                            {
                                sPSizeW = maxPSizeW ;
                                sPSizeH = maxPSizeW ;
                                List<string> subsids = IsAddSizeSubsid();
                                if (subsids.Contains(strValue) || strValue.Contains("井") || strValue.Contains("孔"))
                                {
                                    IsSetSize = true;
                                }
                                else
                                {
                                    IsSetSize = false;
                                }

                            }
                            //如果附属物有尺寸值
                            else
                            {
                                IsSetSize = true;
                                tFeature.GetSubSize(pointScheme.strSubsidSize, pointScheme.strSubsidType, out sPSizeW, out sPSizeH, out pType);

                                //这里增加：跟所连管线的最大的管径值相比较，若小于最大管径，则将尺寸设为最大管径还要加上10厘米
                                if (sPSizeH < maxPSizeW)
                                {
                                    sPSizeH = maxPSizeW;

                                }
                                if (sPSizeW < maxPSizeW)
                                {
                                    sPSizeW = maxPSizeW;
                                }

                            }
                        }
                        //如果井盖为圆形
                        if (pType == Sub3DType.Circle)
                        {
                            string strValue1 = strValue + "_圆形";
                            if (dic3DSymbol.ContainsKey(strValue1))
                            {
                                IMarker3DSymbol marker3DSymbol = dic3DSymbol[strValue1];
                                IMarker3DPlacement pMarker3DPlacement = marker3DSymbol as IMarker3DPlacement;

                                if (IsSetSize)
                                {
                                    if (sPSizeW != 0 && sPSizeH != 0)
                                    {
                                        pMarker3DPlacement.Depth = sPSizeH;
                                        pMarker3DPlacement.Width = sPSizeW;
                                        pMarker3DPlacement.Size = SubsidHeight;
                                    }

                                }
                                //if (!IsSetSize && pMarker3DPlacement.Size < PSizeH)
                                //{
                                //    pMarker3DPlacement.Size += 0.5;
                                //}

                                pMarker3DPlacement.MaintainAspectRatio = false; //保持纵横比
                                pMarker3DPlacement.SetRotationAngles(0, 0, lineAngle); //设置模型的水平方向上的旋转角度与连接线的角度一致
                                pMarker3DPlacement.ApplyToPoint(CenterPoint, out pGeometry);

                            }
                            else if (dic3DSymbol.ContainsKey(strValue))
                            {
                                IMarker3DSymbol marker3DSymbol = dic3DSymbol[strValue];
                                IMarker3DPlacement pMarker3DPlacement = marker3DSymbol as IMarker3DPlacement;

                                if (IsSetSize)
                                {
                                    if (sPSizeW != 0 && sPSizeH != 0)
                                    {
                                        pMarker3DPlacement.Depth = sPSizeH;
                                        pMarker3DPlacement.Width = sPSizeW;

                                        List<string> subsids = IsNotSizeSubsid();
                                        //再进行判定，如果附属物为井类，需按样式符号配置里面的设置高度来显示
                                        if (!subsids.Contains(strValue))
                                        {
                                            pMarker3DPlacement.Size = SubsidHeight;
                                            pMarker3DPlacement.MaintainAspectRatio = false;
                                        }
                                    }
                                }
                                //if (!IsSetSize && pMarker3DPlacement.Size < PSizeH)
                                //{
                                //    pMarker3DPlacement.Size += 0.5;
                                //}
                                pMarker3DPlacement.SetRotationAngles(0, 0, lineAngle); //设置模型的水平方向上的旋转角度与连接线的角度一致
                                pMarker3DPlacement.ApplyToPoint(CenterPoint, out pGeometry);
                            }

                        }
                        //如果井盖为矩形
                        if (pType == Sub3DType.Square)
                        {
                            string strValue1 = strValue + "_矩形";
                            if (dic3DSymbol.ContainsKey(strValue1))
                            {
                                IMarker3DSymbol marker3DSymbol = dic3DSymbol[strValue1];
                                IMarker3DPlacement pMarker3DPlacement = marker3DSymbol as IMarker3DPlacement;
                                if (IsSetSize)
                                {
                                    if (sPSizeW != 0 && sPSizeH != 0)
                                    {
                                        pMarker3DPlacement.Depth = sPSizeH;
                                        pMarker3DPlacement.Width = sPSizeW;
                                        pMarker3DPlacement.Size = SubsidHeight;
                                    }
                                }
                                //if (!IsSetSize && pMarker3DPlacement.Size < PSizeH)
                                //{
                                //    pMarker3DPlacement.Size += 0.5;
                                //}

                                pMarker3DPlacement.MaintainAspectRatio = false;
                                pMarker3DPlacement.SetRotationAngles(0, 0, lineAngle);
                                pMarker3DPlacement.ApplyToPoint(CenterPoint, out pGeometry);
                            }
                            else if (dic3DSymbol.ContainsKey(strValue))
                            {
                                IMarker3DSymbol marker3DSymbol = dic3DSymbol[strValue];
                                IMarker3DPlacement pMarker3DPlacement = marker3DSymbol as IMarker3DPlacement;

                                if (IsSetSize)
                                {
                                    if (sPSizeW != 0 && sPSizeH != 0)
                                    {
                                        pMarker3DPlacement.Depth = sPSizeH;
                                        pMarker3DPlacement.Width = sPSizeW;

                                        List<string> subsids = IsNotSizeSubsid();

                                        if (!subsids.Contains(strValue))
                                        {
                                            pMarker3DPlacement.Size = SubsidHeight;
                                            pMarker3DPlacement.MaintainAspectRatio = false;
                                        }
                                    }
                                }
                                //if (!IsSetSize && pMarker3DPlacement.Size < PSizeH)
                                //{
                                //    pMarker3DPlacement.Size += 0.5;
                                //}
                                pMarker3DPlacement.SetRotationAngles(0, 0, lineAngle);
                                pMarker3DPlacement.ApplyToPoint(CenterPoint, out pGeometry);
                            }
                        }

                        //将生成的几何图形以及源数据相应记录属性写入待插入的要素中
                        if (pGeometry != null)
                        {
                            IFields Flds = pFromFeatureClass.Fields;
                            //写入字段属性值
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
                                string str = tFeature.get_Value(i).ToString();
                                //字段赋值
                                tFeatureBuffer.set_Value(index1, tFeature.get_Value(i));
                            }

                            pGeometry.SpatialReference = pShapeCopy.SpatialReference;

                            if (tShapeCopy0.SpatialReference is IGeographicCoordinateSystem)
                            {
                                pGeometry = AnalystHelper.ProjectTo4490(pGeometry);
                                double x = pGeometry.Envelope.XMax;
                            }
                            tFeatureBuffer.Shape = pGeometry as IGeometry;
                            //插入要素
                            tFeatureCursor.InsertFeature(tFeatureBuffer);
                        }

                        if (number > 500)
                        {
                            tFeatureCursor.Flush();
                            number = 0;
                        }
                        tFeature = tFeatureCursor2.NextFeature();
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog.LogError($"实体号为{tFeature.OID}的{strValue}模型生成失败,"
                            + ex.Message, ex);
                        tFeature = tFeatureCursor2.NextFeature();
                    }

                }

                int sN = singleNum;
                int nH = noHighNum;
                tFeatureCursor.Flush();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursor);
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
            }
            return IsSuccess;

        }

        public static bool CreateFeature(IFeatureClass pToFeatureClass,IWorkspace pWorkspace, PointScheme pointScheme,
             string PointBigType, Dictionary<string, IMarker3DSymbol> dic3DSymbol, IQueryFilter tQueryFilter, ITrackProgress trackProgress)
        {
            bool IsSuccess = true;

            int pointZIndex = pointScheme.PointFeatureClass.FindField(pointScheme.strZ);//判断管点数据源的地面高程字段是否存在
            //判断管点地面高程字段是否存在，如果不存在则无法建模
            if (pointZIndex == -1)
            {
                IsSuccess = false;
                return IsSuccess;
            }

            double tolerace = 0.0;
            //先判断空间查询所需缓冲半径
            IGeoDataset gds = pointScheme.PointFeatureClass as IGeoDataset;
            if (gds.SpatialReference is IGeographicCoordinateSystem)
            {
                tolerace = 0.0000002;
            }
            else
            {
                tolerace = 0.1;
            }

            IFeatureClass pFromFeatureClass = pointScheme.PointFeatureClass;
            try
            {
                //设置游标状态为插入
                IFeatureCursor tFeatureCursor = pToFeatureClass.Insert(true);
                //设置要素缓存
                IFeatureBuffer tFeatureBuffer = pToFeatureClass.CreateFeatureBuffer();
                //获取源文件要素类的的记录集游标
                IFeatureCursor tFeatureCursor2 = pFromFeatureClass.Search(tQueryFilter, true);
                //复制每个要素
                IFeature tFeature = tFeatureCursor2.NextFeature();
                trackProgress.SubValue = 0;
                trackProgress.SubMax = pFromFeatureClass.FeatureCount(tQueryFilter);
                int number = 0;
                while (tFeature != null)
                {
                    string sPno = tFeature.GetValue(pointScheme.strPNo).ToString();
                    try
                    {
                        if (!trackProgress.IsContinue) break;
                        number++;
                        trackProgress.SubValue++;
                        trackProgress.SubMessage = $"正在自动生成特征点{trackProgress.SubValue}/{trackProgress.SubMax}";
                        Application.DoEvents();

                        IGeometry tShapeCopy0 = tFeature.ShapeCopy;
                        //求出管点附属物所在的空间位置坐标(原来的空间参考)
                        IPoint pPoint = tShapeCopy0 as IPoint;
                        if (pPoint == null)
                        {
                            MessageBox.Show($"选择的管点数据源其实不是点图层!");
                            IsSuccess = false;
                            break;
                        }
                        //初始化空间查询条件
                        ISpatialFilter spatialFilter = AnalystHelper.GetPointSpatialFilter(pPoint, tolerace);

                        IGeometry tShapeCopy = tFeature.ShapeCopy;
                        #region 如果是球面坐标系，则需要转成投影坐标系再进行空间位置的计算
                        IGeometry pShapeCopy = AnalystHelper.GCStoPRJ(tShapeCopy);
                        if (pShapeCopy == null)
                        {
                            IsSuccess = false;
                            break;
                        }
                        ISpatialReference spatialReference = pShapeCopy.SpatialReference;
                        string name = spatialReference.Name;
                        #endregion

                        if (string.IsNullOrWhiteSpace(sPno))
                        {
                            tFeature = tFeatureCursor2.NextFeature();
                            continue;
                        }

                        //判断管点所关联管线是否有起止点高程字段，若存在则按照关联管线的端点高程
                        int shIndex = pointScheme.LineFeatureClass.FindField(pointScheme.strS_Hight);
                        int ehIndex = pointScheme.LineFeatureClass.FindField(pointScheme.strE_Hight);
                        if (shIndex < 0 || ehIndex < 0)
                        {
                            IsSuccess = false;
                            break;
                        }

                        #region 根据管点点号从关联管线数据中获取特征点模型绘制所需的一些属性
                        bool Isolated = false;
                        double minLSizeW = 0.0;
                        double minLSizeH = 0.0;
                        double minHigh = 0.0;

                        //需要通过关联管线获取连接的管线的最大管径、最长管线的旋转角、最低高程值、管线的横截面高
                        pointScheme.GetFeatureByLine(sPno, out minHigh, out minLSizeW, out minLSizeH, out Isolated,
                            spatialFilter);

                        //判断孤立节点
                        if (Isolated)
                        {
                            tFeature = tFeatureCursor2.NextFeature();
                            continue;
                        }
                        #endregion

                        double dX = (pShapeCopy as IPoint).X;
                        double dY = (pShapeCopy as IPoint).Y;
                        double dZ = 0.0;

                        #region 判断地面高程值是否为空
                        //string strZ = Convert.ToString(tFeature.GetValue(pointScheme.strZ));
                        //if (string.IsNullOrWhiteSpace(strZ) || strZ.Trim() == "0")
                        //{
                        //    tFeature = tFeatureCursor2.NextFeature();
                        //    continue;
                        //}
                        //else
                        //{
                        //    dZ = Convert.ToDouble(tFeature.GetValue(pointScheme.strZ));//先将管点高度设为管点地面高
                        //}
                        #endregion

                        //管点高程=关联管线端点最低高程+1/2管径
                        if (pointScheme.CalculationType == CalculationType.Type1)
                        {
                            dZ = minHigh + minLSizeH / 2;
                        }
                        else
                        {
                            dZ = minHigh - minLSizeH / 2;
                        }

                        IPoint CenterPoint = PipeUtil.ConstructPnt3D(dX, dY, dZ, pShapeCopy.SpatialReference);

                        IGeometry pGeometry = null;
                        //这里利用空间位置遍历相接管线并获取分支点
                        List<BranchPoint> lsPoints = GetBranchPoint3D(pointScheme, sPno, spatialFilter);//跟空间参考有关

                        if (lsPoints.Count > 5)
                        {
                            tFeature = tFeatureCursor2.NextFeature();
                            continue;
                        }

                        if (lsPoints.Count == 1)
                        {
                            pGeometry = PipeUtil.CreateKeep(lsPoints[0], PointBigType, dic3DSymbol, pShapeCopy.SpatialReference);
                        }
                        else if (lsPoints.Count > 2)
                        {
                            pGeometry = PipeUtil.CreateMultiPipe(CenterPoint, lsPoints, PointBigType, dic3DSymbol, pShapeCopy.SpatialReference);
                        }
                        else if (lsPoints.Count == 2)
                        {
                            pGeometry = PipeUtil.CreateElbowPipe(CenterPoint, lsPoints, PointBigType, dic3DSymbol,
                                minLSizeW, minLSizeH, pShapeCopy.SpatialReference);
                        }
                        if (pGeometry != null)
                        {
                            IFields Flds = pFromFeatureClass.Fields;
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

                            pGeometry.SpatialReference = pShapeCopy.SpatialReference;
                            if (tShapeCopy0.SpatialReference is IGeographicCoordinateSystem)
                            {
                                pGeometry = AnalystHelper.ProjectTo4490(pGeometry);
                                double x = pGeometry.Envelope.XMax;
                            }

                            tFeatureBuffer.Shape = pGeometry as IGeometry;
                            //插入要素
                            tFeatureCursor.InsertFeature(tFeatureBuffer);
                        }

                        if (number > 100)
                        {
                            tFeatureCursor.Flush();
                            number = 0;
                        }

                        tFeature = tFeatureCursor2.NextFeature();
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog.LogError($"实体号为{tFeature.OID},点号为{sPno}的特征点生成失败,"
                        + ex.Message, ex);
                        tFeature = tFeatureCursor2.NextFeature();
                    }
                }

                tFeatureCursor.Flush();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursor);
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
                //tWorkspaceEdit.StopEditOperation();
                ////结束编辑
                //tWorkspaceEdit.StopEditing(true);
            }
            return IsSuccess;

        }

        #region 之前还要对管点数据中的特征点进行分类并分批转换，免去这一步
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pWorkspace"></param>
        /// <param name="LayerName"></param>
        /// <param name="strValue"></param>
        /// <param name="pointScheme"></param>
        /// <param name="PointBigType"></param>
        /// <param name="dic3DSymbol"></param>
        /// <param name="tQueryFilter"></param>
        /// <param name="trackProgress"></param>
        //public static bool CreateModel_弯头(IWorkspace pWorkspace, string LayerName, string strValue, PointScheme pointScheme,
        //     string PointBigType, Dictionary<string, IMarker3DSymbol> dic3DSymbol, IQueryFilter tQueryFilter, ITrackProgress trackProgress)
        //{
        //    bool IsSuccess = true;

        //    int pointZIndex = pointScheme.PointFeatureClass.FindField(pointScheme.strZ);//判断管点数据源的地面高程字段是否存在
        //    //判断管点地面高程字段是否存在，如果不存在则需要计算
        //    if (pointZIndex == -1)
        //    {
        //        IsSuccess = false;
        //        return IsSuccess;
        //    }

        //    //原要素字段
        //    IFields pFields = (pointScheme.PointFeatureClass.Fields as IClone).Clone() as IFields;
        //    //3d属性要素
        //    IGeoDataset pGeodataSet = pointScheme.PointFeatureClass as IGeoDataset;
        //    ISpatialReference sr = pGeodataSet.SpatialReference;
        //    string tName = sr.Name;

        //    #region 适配投影坐标
        //    //ISpatialReference sr3D = AnalystHelper.GCStoPRJ(sr);
        //    //string sName = sr3D.Name;
        //    #endregion
        //    ISpatialReference sr3D = sr;

        //    //string codeValue = "弯头";
        //    //创建三维多面体字段
        //    IFields pNewFields = CreateFields3D(pFields, sr3D, esriGeometryType.esriGeometryMultiPatch);

        //    IFeatureClass pToFeatureClass;
        //    if (AnalystHelper.IsExistFeatureClass(LayerName, pWorkspace))
        //    {
        //        pToFeatureClass = (pWorkspace as IFeatureWorkspace).OpenFeatureClass(LayerName);
        //    }
        //    else
        //    {
        //        pToFeatureClass = (pWorkspace as IFeatureWorkspace).CreateFeatureClass(LayerName, pNewFields, null, null, pointScheme.PointFeatureClass.FeatureType, "SHAPE", "");
        //    }
        //    IGeoDataset pGeoSet = pToFeatureClass as IGeoDataset;
        //    ISpatialReference pSr = pGeoSet.SpatialReference;
        //    string pSrName = pSr.Name;

        //    IFeatureClass pFromFeatureClass = pointScheme.PointFeatureClass;
        //    //得到当前编辑空间
        //    IWorkspaceEdit tWorkspaceEdit = pWorkspace as IWorkspaceEdit;
        //    tWorkspaceEdit.StartEditing(false);
        //    tWorkspaceEdit.StartEditOperation();
        //    try
        //    {
        //        //设置游标状态为插入
        //        IFeatureCursor tFeatureCursor = pToFeatureClass.Insert(true);
        //        //设置要素缓存
        //        IFeatureBuffer tFeatureBuffer = pToFeatureClass.CreateFeatureBuffer();
        //        //获取源文件要素类的的记录集游标
        //        IFeatureCursor tFeatureCursor2 = pFromFeatureClass.Search(tQueryFilter, true);

        //        //复制每个要素
        //        IFeature tFeature = tFeatureCursor2.NextFeature();
        //        trackProgress.SubValue = 0;
        //        trackProgress.SubMax = pFromFeatureClass.FeatureCount(tQueryFilter);
        //        int number = 0;
        //        while (tFeature != null)
        //        {
        //            string sPno = tFeature.GetValue(pointScheme.strPNo).ToString();
        //            try
        //            {
        //                if (!trackProgress.IsContinue) break;
        //                number++;
        //                trackProgress.SubValue++;
        //                trackProgress.SubMessage = $"正在生成{strValue}模型{trackProgress.SubValue}/{trackProgress.SubMax}";
        //                Application.DoEvents();

        //                IGeometry tShapeCopy0 = tFeature.ShapeCopy;
        //                IGeometry tShapeCopy = tFeature.ShapeCopy;
        //                #region 如果是球面坐标系，则需要转成投影坐标系再进行空间位置的计算
        //                IGeometry pShapeCopy = AnalystHelper.GCStoPRJ(tShapeCopy);
        //                if (pShapeCopy == null)
        //                {
        //                    IsSuccess = false;
        //                    break;
        //                }
        //                ISpatialReference spatialReference = pShapeCopy.SpatialReference;
        //                string name = spatialReference.Name;
        //                #endregion

        //                if (string.IsNullOrWhiteSpace(sPno))
        //                {
        //                    tFeature = tFeatureCursor2.NextFeature();
        //                    continue;
        //                }

        //                //判断管点所关联管线是否有起止点高程字段，若存在则按照关联管线的端点高程
        //                int shIndex = pointScheme.LineFeatureClass.FindField(pointScheme.strS_Hight);
        //                int ehIndex = pointScheme.LineFeatureClass.FindField(pointScheme.strE_Hight);
        //                if (shIndex < 0 || ehIndex < 0)
        //                {
        //                    IsSuccess = false;
        //                    break;
        //                }

        //                #region 根据管点点号从关联管线数据中获取特征点模型绘制所需的一些属性
        //                bool Isolated = false;
        //                double maxPSizeW = 0.0;
        //                double maxPSizeH = 0.0;
        //                double minHigh = 0.0;

        //                //需要通过关联管线获取连接的管线的最大管径、最长管线的旋转角、最低高程值、管线的横截面高
        //                pointScheme.GetFeatureByLine(sPno, out minHigh,out maxPSizeW, out maxPSizeH, out Isolated);

        //                //判断孤立节点
        //                if (Isolated)
        //                {
        //                    tFeature = tFeatureCursor2.NextFeature();
        //                    continue;
        //                }
        //                #endregion

        //                double dX = (pShapeCopy as IPoint).X;
        //                double dY = (pShapeCopy as IPoint).Y;
        //                double dZ = 0.0;

        //                #region 判断地面高程值是否为空
        //                //string strZ = Convert.ToString(tFeature.GetValue(pointScheme.strZ));
        //                //if (string.IsNullOrWhiteSpace(strZ) || strZ.Trim() == "0")
        //                //{
        //                //    tFeature = tFeatureCursor2.NextFeature();
        //                //    continue;
        //                //}
        //                //else
        //                //{
        //                //    dZ = Convert.ToDouble(tFeature.GetValue(pointScheme.strZ));//先将管点高度设为管点地面高
        //                //}
        //                #endregion

        //                //管点高程=关联管线端点最低高程+1/2管径
        //                if (pointScheme.CalculationType == CalculationType.Type1)
        //                {
        //                    dZ = minHigh + maxPSizeH / 2;
        //                }
        //                else
        //                {
        //                    dZ = minHigh - maxPSizeH / 2;
        //                }

        //                IPoint CenterPoint = PipeUtil.ConstructPnt3D(dX, dY, dZ, pShapeCopy.SpatialReference);

        //                IGeometry pGeometry = null;
        //                List<BranchPoint> lsPoints = GetBranchPoint3D(pointScheme, sPno);//跟空间参考有关

        //                if (lsPoints.Count == 1)
        //                {
        //                    pGeometry = PipeUtil.CreateKeep(lsPoints[0], PointBigType, dic3DSymbol, pShapeCopy.SpatialReference);
        //                }
        //                else if (lsPoints.Count > 2)
        //                {
        //                    pGeometry = PipeUtil.CreateMultiPipe(CenterPoint, lsPoints, PointBigType, dic3DSymbol, pShapeCopy.SpatialReference);
        //                }
        //                else if (lsPoints.Count == 2)
        //                {
        //                    pGeometry = PipeUtil.CreateElbowPipe(CenterPoint, lsPoints, PointBigType, dic3DSymbol,
        //                        maxPSizeW, maxPSizeH, pShapeCopy.SpatialReference);

        //                }
        //                if (pGeometry != null)
        //                {
        //                    IFields Flds = pFromFeatureClass.Fields;
        //                    for (int i = 0; i < Flds.FieldCount; i++)
        //                    {
        //                        //获取对应字段的索引
        //                        IField Fld = Flds.get_Field(i);
        //                        int index = tFeatureCursor2.Fields.FindField(Fld.Name);
        //                        if (index < 0) continue;
        //                        if (!Fld.Editable) continue;
        //                        if (Fld.Type == esriFieldType.esriFieldTypeGeometry) continue;
        //                        int index1 = pToFeatureClass.FindField(Fld.Name);
        //                        if (index1 < 0) continue;
        //                        //字段赋值
        //                        tFeatureBuffer.set_Value(index1, tFeature.get_Value(i));
        //                    }

        //                    pGeometry.SpatialReference = pShapeCopy.SpatialReference;
        //                    if (tShapeCopy0.SpatialReference is IGeographicCoordinateSystem)
        //                    {
        //                        pGeometry = AnalystHelper.ProjectTo4490(pGeometry);
        //                        double x = pGeometry.Envelope.XMax;
        //                    }

        //                    tFeatureBuffer.Shape = pGeometry as IGeometry;
        //                    //插入要素
        //                    tFeatureCursor.InsertFeature(tFeatureBuffer);
        //                }

        //                if (number > 100)
        //                {
        //                    tFeatureCursor.Flush();
        //                    number = 0;
        //                }

        //                tFeature = tFeatureCursor2.NextFeature();
        //            }
        //            catch (Exception ex)
        //            {
        //                ExceptionLog.LogError($"实体号为{tFeature.OID},点号为{sPno}的{strValue}模型生成失败,"
        //                + ex.Message, ex);
        //                tFeature = tFeatureCursor2.NextFeature();
        //            }
        //        }

        //        tFeatureCursor.Flush();
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursor);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursor2);
        //        GC.Collect();

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        IsSuccess = false;
        //    }
        //    finally
        //    {
        //        tWorkspaceEdit.StopEditOperation();
        //        //结束编辑
        //        tWorkspaceEdit.StopEditing(true);
        //    }
        //    return IsSuccess;

        //}

        //public static bool CreateModel_多通头(IWorkspace pWorkspace, string LayerName, string strValue, PointScheme pointScheme,
        //    string PointBigType, Dictionary<string, IMarker3DSymbol> dic3DSymbol, IQueryFilter tQueryFilter, ITrackProgress trackProgress)
        //{
        //    bool IsSuccess = true;

        //    int pointZIndex = pointScheme.PointFeatureClass.FindField(pointScheme.strZ);//判断管点数据源的地面高程字段是否存在
        //    //判断管点地面高程字段是否存在，如果不存在则需要计算
        //    if (pointZIndex == -1)
        //    {
        //        IsSuccess = false;
        //        return IsSuccess;
        //    }

        //    //原要素字段
        //    IFields pFields = (pointScheme.PointFeatureClass.Fields as IClone).Clone() as IFields;
        //    //3d属性要素
        //    IGeoDataset pGeodataSet = pointScheme.PointFeatureClass as IGeoDataset;
        //    ISpatialReference sr = pGeodataSet.SpatialReference;
        //    #region 适配球面坐标
        //    //ISpatialReference sr3D = AnalystHelper.GCStoPRJ(sr);
        //    //string sName = sr3D.Name;
        //    #endregion
        //    ISpatialReference sr3D = sr;
        //    //创建三维多面体字段
        //    IFields pNewFields = CreateFields3D(pFields, sr3D, esriGeometryType.esriGeometryMultiPatch);
        //    IFeatureClass pToFeatureClass;
        //    if (AnalystHelper.IsExistFeatureClass(LayerName, pWorkspace))
        //    {
        //        pToFeatureClass = (pWorkspace as IFeatureWorkspace).OpenFeatureClass(LayerName);
        //    }
        //    else
        //    {
        //        pToFeatureClass = (pWorkspace as IFeatureWorkspace).CreateFeatureClass(LayerName, pNewFields, null, null, pointScheme.PointFeatureClass.FeatureType, "SHAPE", "");
        //    }
        //    IFeatureClass pFromFeatureClass = pointScheme.PointFeatureClass;
        //    //得到当前编辑空间
        //    IWorkspaceEdit tWorkspaceEdit = pWorkspace as IWorkspaceEdit;
        //    tWorkspaceEdit.StartEditing(false);
        //    tWorkspaceEdit.StartEditOperation();
        //    try
        //    {
        //        //设置游标状态为插入
        //        IFeatureCursor tFeatureCursor = pToFeatureClass.Insert(true);
        //        //设置要素缓存
        //        IFeatureBuffer tFeatureBuffer = pToFeatureClass.CreateFeatureBuffer();
        //        //获取源文件要素类的的记录集游标
        //        IFeatureCursor tFeatureCursor2 = pFromFeatureClass.Search(tQueryFilter, true);
        //        trackProgress.SubValue = 0;
        //        trackProgress.SubMax = pFromFeatureClass.FeatureCount(tQueryFilter);
        //        //复制每个要素
        //        IFeature tFeature = tFeatureCursor2.NextFeature();
        //        int number = 0;
        //        while (tFeature != null)
        //        {
        //            string sPno = tFeature.GetValue(pointScheme.strPNo).ToString();
        //            try
        //            {
        //                if (!trackProgress.IsContinue) break;
        //                number++;
        //                trackProgress.SubValue++;
        //                trackProgress.SubMessage = $"正在生成{strValue}模型{trackProgress.SubValue}/{trackProgress.SubMax}";
        //                Application.DoEvents();

        //                IGeometry tShapeCopy0 = tFeature.ShapeCopy;
        //                IGeometry tShapeCopy = tFeature.ShapeCopy;

        //                #region 适配球面坐标

        //                IGeometry pShapeCopy = AnalystHelper.GCStoPRJ(tShapeCopy);
        //                if (pShapeCopy == null)
        //                {
        //                    IsSuccess = false;
        //                    break;
        //                }
        //                ISpatialReference spatialReference = pShapeCopy.SpatialReference;
        //                string name = spatialReference.Name;
        //                #endregion

        //                #region 获取关联管线属性
        //                if (string.IsNullOrWhiteSpace(sPno))
        //                {
        //                    tFeature = tFeatureCursor2.NextFeature();
        //                    continue;
        //                }

        //                //判断管点所关联管线是否有起止点高程字段，若存在则按照关联管线的端点高程
        //                int shIndex = pointScheme.LineFeatureClass.FindField(pointScheme.strS_Hight);
        //                int ehIndex = pointScheme.LineFeatureClass.FindField(pointScheme.strE_Hight);
        //                if (shIndex < 0 || ehIndex < 0)
        //                {
        //                    IsSuccess = false;
        //                    break;
        //                }

        //                #region 根据管点点号从关联管线数据中获取特征点模型绘制所需的一些属性
        //                bool Isolated = false;
        //                double maxPSizeW = 0.0;
        //                double maxPSizeH = 0.0;
        //                double minHigh = 0.0;

        //                //需要通过关联管线获取连接的管线的最大管径、最长管线的旋转角、最低高程值、管线的横截面高
        //                pointScheme.GetFeatureByLine(sPno, out minHigh, out maxPSizeW, out maxPSizeH, out Isolated);

        //                //判断孤立节点
        //                if (Isolated)
        //                {
        //                    tFeature = tFeatureCursor2.NextFeature();
        //                    continue;
        //                }
        //                #endregion

        //                double dX = (pShapeCopy as IPoint).X;
        //                double dY = (pShapeCopy as IPoint).Y;
        //                double dZ = 0.0;

        //                #endregion

        //                //管点高程=关联管线端点最低高程+1/2管径
        //                if (pointScheme.CalculationType == CalculationType.Type1)
        //                {
        //                    dZ = minHigh + maxPSizeH / 2;
        //                }
        //                else
        //                {
        //                    dZ = minHigh - maxPSizeH / 2;
        //                }

        //                IPoint CenterPoint = PipeUtil.ConstructPnt3D(dX, dY, dZ, pShapeCopy.SpatialReference);
        //                IGeometry pGeometry = null;
        //                List<BranchPoint> lsPoints = GetBranchPoint3D(pointScheme, sPno);

        //                if (lsPoints.Count == 0)
        //                {
        //                    pGeometry = null;
        //                }
        //                foreach (BranchPoint lsPoint in lsPoints)
        //                {
        //                    if (lsPoint.FromPnt == null || lsPoint.ToPnt == null)
        //                    {
        //                        Console.WriteLine("Null");
        //                    }
        //                }
        //                //制作通头模型
        //                pGeometry = PipeUtil.CreateMultiPipe(CenterPoint, lsPoints, PointBigType, dic3DSymbol, pShapeCopy.SpatialReference);

        //                if (pGeometry != null)
        //                {
        //                    IFields Flds = pFromFeatureClass.Fields;
        //                    for (int i = 0; i < Flds.FieldCount; i++)
        //                    {
        //                        IField Fld = Flds.get_Field(i);
        //                        int index = tFeatureCursor2.Fields.FindField(Fld.Name);
        //                        if (index < 0) continue;
        //                        if (!Fld.Editable) continue;
        //                        if (Fld.Type == esriFieldType.esriFieldTypeGeometry) continue;
        //                        int index1 = pToFeatureClass.FindField(Fld.Name);
        //                        if (index1 < 0) continue;
        //                        //字段赋值
        //                        tFeatureBuffer.set_Value(index1, tFeature.get_Value(i));
        //                    }

        //                    pGeometry.SpatialReference = pShapeCopy.SpatialReference;
        //                    if (tShapeCopy0.SpatialReference is IGeographicCoordinateSystem)
        //                    {
        //                        pGeometry = AnalystHelper.ProjectTo4490(pGeometry);
        //                    }
        //                    tFeatureBuffer.Shape = pGeometry as IGeometry;
        //                    //插入要素
        //                    tFeatureCursor.InsertFeature(tFeatureBuffer);
        //                }
        //                if (number > 100)
        //                {
        //                    tFeatureCursor.Flush();
        //                    number = 0;
        //                }
        //                tFeature = tFeatureCursor2.NextFeature();
        //            }
        //            catch (Exception ex)
        //            {
        //                ExceptionLog.LogError($"实体号为{tFeature.OID},点号为{sPno}的{strValue}模型生成失败,"
        //                    + ex.Message, ex);
        //                tFeature = tFeatureCursor2.NextFeature();
        //            }
        //        }
        //        tFeatureCursor.Flush();
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursor);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursor2);
        //        GC.Collect();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        IsSuccess = false;
        //    }
        //    finally
        //    {
        //        tWorkspaceEdit.StopEditOperation();
        //        //结束编辑
        //        tWorkspaceEdit.StopEditing(true);
        //    }
        //    return IsSuccess;
        //}

        //public static bool CreateModel_预留口(IWorkspace pWorkspace, string LayerName, string strValue, PointScheme pointScheme,
        //    string PointBigType, Dictionary<string, IMarker3DSymbol> dic3DSymbol, IQueryFilter tQueryFilter, ITrackProgress trackProgress)
        //{
        //    bool IsSuccess = true;

        //    int pointZIndex = pointScheme.PointFeatureClass.FindField(pointScheme.strZ);//判断管点数据源的地面高程字段是否存在
        //    //判断管点地面高程字段是否存在，如果不存在则需要计算
        //    if (pointZIndex == -1)
        //    {
        //        IsSuccess = false;
        //        return IsSuccess;
        //    }
        //    //原要素字段
        //    IFields pFields = (pointScheme.PointFeatureClass.Fields as IClone).Clone() as IFields;
        //    //3d属性要素
        //    IGeoDataset pGeodataSet = pointScheme.PointFeatureClass as IGeoDataset;
        //    ISpatialReference sr = pGeodataSet.SpatialReference;
        //    #region 适配投影坐标
        //    //ISpatialReference sr3D = AnalystHelper.GCStoPRJ(sr);
        //    //string sName = sr3D.Name;
        //    #endregion
        //    ISpatialReference sr3D = sr;

        //    //创建三维多面体字段
        //    IFields pNewFields = CreateFields3D(pFields, sr3D, esriGeometryType.esriGeometryMultiPatch);
        //    IFeatureClass pToFeatureClass;
        //    if (AnalystHelper.IsExistFeatureClass(LayerName, pWorkspace))
        //    {
        //        pToFeatureClass = (pWorkspace as IFeatureWorkspace).OpenFeatureClass(LayerName);
        //    }
        //    else
        //    {
        //        pToFeatureClass = (pWorkspace as IFeatureWorkspace).CreateFeatureClass(LayerName, pNewFields, null, null, pointScheme.PointFeatureClass.FeatureType, "SHAPE", "");
        //    }

        //    IFeatureClass pFromFeatureClass = pointScheme.PointFeatureClass;
        //    //int FeatureCount = pFromFeatureClass.FeatureCount(tQueryFilter);

        //    //得到当前编辑空间
        //    IWorkspaceEdit tWorkspaceEdit = pWorkspace as IWorkspaceEdit;
        //    tWorkspaceEdit.StartEditing(false);
        //    tWorkspaceEdit.StartEditOperation();

        //    try
        //    {
        //        //设置游标状态为插入
        //        IFeatureCursor tFeatureCursor = pToFeatureClass.Insert(true);
        //        //设置要素缓存
        //        IFeatureBuffer tFeatureBuffer = pToFeatureClass.CreateFeatureBuffer();
        //        //获取源文件要素类的的记录集游标
        //        IFeatureCursor tFeatureCursor2 = pFromFeatureClass.Search(tQueryFilter, true);
        //        trackProgress.SubValue = 0;
        //        trackProgress.SubMax = pFromFeatureClass.FeatureCount(tQueryFilter);
        //        //复制每个要素
        //        IFeature tFeature = tFeatureCursor2.NextFeature();
        //        int number = 0;

        //        while (tFeature != null)
        //        {
        //            string sPno = tFeature.GetValue(pointScheme.strPNo).ToString();
        //            try
        //            {
        //                if (!trackProgress.IsContinue) break;
        //                int feaId = tFeature.OID;
        //                number++;
        //                trackProgress.SubValue++;
        //                trackProgress.SubMessage = $"正在生成{strValue}模型{trackProgress.SubValue}/{trackProgress.SubMax}";
        //                Application.DoEvents();

                        

        //                IGeometry tShapeCopy0 = tFeature.ShapeCopy;
        //                IGeometry tShapeCopy = tFeature.ShapeCopy;
        //                #region 适配投影坐标
        //                IGeometry pShapeCopy = AnalystHelper.GCStoPRJ(tShapeCopy);
        //                if (pShapeCopy == null)
        //                {
        //                    IsSuccess = false;
        //                    break;
        //                }
        //                ISpatialReference spatialReference = pShapeCopy.SpatialReference;
        //                string name = spatialReference.Name;
        //                #endregion

        //                #region 获取关联管线属性
        //                if (string.IsNullOrWhiteSpace(sPno))
        //                {
        //                    tFeature = tFeatureCursor2.NextFeature();
        //                    continue;
        //                }

        //                //判断管点所关联管线是否有起止点高程字段，若存在则按照关联管线的端点高程
        //                int shIndex = pointScheme.LineFeatureClass.FindField(pointScheme.strS_Hight);
        //                int ehIndex = pointScheme.LineFeatureClass.FindField(pointScheme.strE_Hight);
        //                if (shIndex < 0 || ehIndex < 0)
        //                {
        //                    IsSuccess = false;
        //                    break;
        //                }

        //                #region 根据管点点号从关联管线数据中获取特征点模型绘制所需的一些属性
        //                bool Isolated = false;
        //                double maxPSizeW = 0.0;
        //                double maxPSizeH = 0.0;
        //                double minHigh = 0.0;

        //                //需要通过关联管线获取连接的管线的最大管径、最长管线的旋转角、最低高程值、管线的横截面高
        //                pointScheme.GetFeatureByLine(sPno, out minHigh, out maxPSizeW, out maxPSizeH, out Isolated);

        //                //判断孤立节点
        //                if (Isolated)
        //                {
        //                    tFeature = tFeatureCursor2.NextFeature();
        //                    continue;
        //                }
        //                #endregion

        //                double dX = (pShapeCopy as IPoint).X;
        //                double dY = (pShapeCopy as IPoint).Y;
        //                double dZ = 0.0;

        //                #endregion

        //                //管点高程=关联管线端点最低高程+1/2管径
        //                if (pointScheme.CalculationType == CalculationType.Type1)
        //                {
        //                    dZ = minHigh + maxPSizeH / 2;
        //                }
        //                else
        //                {
        //                    dZ = minHigh - maxPSizeH / 2;
        //                }
                        

        //                IPoint CenterPoint = PipeUtil.ConstructPnt3D(dX, dY, dZ, pShapeCopy.SpatialReference);

        //                IGeometry pGeometry = null;
        //                List<BranchPoint> lsPoints = GetBranchPoint3D(pointScheme, sPno);

        //                if (lsPoints.Count > 0)
        //                {
        //                    pGeometry = PipeUtil.CreateKeep(lsPoints[0], PointBigType, dic3DSymbol,
        //                    pShapeCopy.SpatialReference);
        //                }
        //                if (pGeometry != null)
        //                {
        //                    IFields Flds = pFromFeatureClass.Fields;
        //                    for (int i = 0; i < Flds.FieldCount; i++)
        //                    {
        //                        IField Fld = Flds.get_Field(i);
        //                        int index = tFeatureCursor2.Fields.FindField(Fld.Name);
        //                        if (index < 0) continue;
        //                        if (!Fld.Editable) continue;
        //                        if (Fld.Type == esriFieldType.esriFieldTypeGeometry) continue;
        //                        int index1 = pToFeatureClass.FindField(Fld.Name);
        //                        if (index1 < 0) continue;
        //                        //字段赋值
        //                        tFeatureBuffer.set_Value(index1, tFeature.get_Value(i));
        //                    }

        //                    pGeometry.SpatialReference = pShapeCopy.SpatialReference;
        //                    if (tShapeCopy0.SpatialReference is IGeographicCoordinateSystem)
        //                    {
        //                        pGeometry = AnalystHelper.ProjectTo4490(pGeometry);
        //                        double x = pGeometry.Envelope.XMax;
        //                    }
        //                    tFeatureBuffer.Shape = pGeometry as IGeometry;
        //                    //插入要素
        //                    tFeatureCursor.InsertFeature(tFeatureBuffer);
        //                }
        //                if (number > 100)
        //                {
        //                    tFeatureCursor.Flush();
        //                    number = 0;
        //                }
        //                tFeature = tFeatureCursor2.NextFeature();
        //            }
        //            catch (Exception ex)
        //            {
        //                ExceptionLog.LogError($"实体号为{tFeature.OID},点号为{sPno}的{strValue}模型生成失败,"
        //                    + ex.Message, ex);
        //                tFeature = tFeatureCursor2.NextFeature();
        //            }
        //        }
        //        tFeatureCursor.Flush();
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursor);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursor2);
        //        GC.Collect();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        IsSuccess = false;
        //    }
        //    finally
        //    {
        //        tWorkspaceEdit.StopEditOperation();
        //        //结束编辑
        //        tWorkspaceEdit.StopEditing(true);
        //    }
        //    return IsSuccess;
        //}

        ///// <summary>
        ///// 没有的模型 统一 为球形 表示
        ///// </summary>
        ///// <param name="pWorkspace"></param>
        ///// <param name="LayerName"></param>
        ///// <param name="strValue"></param>
        ///// <param name="pointScheme"></param>
        ///// <param name="tQueryFilter"></param>
        ///// <param name="trackProgress"></param>
        //public static bool CreateModel_其他模型(IWorkspace pWorkspace, string LayerName, string strValue, PointScheme pointScheme,
        //    string PointBigType, Dictionary<string, IMarker3DSymbol> dic3DSymbol, IQueryFilter tQueryFilter, ITrackProgress trackProgress)
        //{
        //    bool IsSuccess = true;

        //    int pointZIndex = pointScheme.PointFeatureClass.FindField(pointScheme.strZ);//判断管点数据源的地面高程字段是否存在
        //    //判断管点地面高程字段是否存在，如果不存在则需要计算
        //    if (pointZIndex == -1)
        //    {
        //        IsSuccess = false;
        //        return IsSuccess;
        //    }

        //    //原要素字段
        //    IFields pFields = (pointScheme.PointFeatureClass.Fields as IClone).Clone() as IFields;
        //    //3d属性要素
        //    IGeoDataset pGeodataSet = pointScheme.PointFeatureClass as IGeoDataset;
        //    ISpatialReference sr = pGeodataSet.SpatialReference;
        //    #region 适配球面
        //    //ISpatialReference sr3D = AnalystHelper.GCStoPRJ(sr);
        //    //string sName = sr3D.Name;
        //    #endregion
        //    ISpatialReference sr3D = sr;
        //    //创建三维多面体字段
        //    IFields pNewFields = CreateFields3D(pFields, sr3D, esriGeometryType.esriGeometryMultiPatch);
        //    IFeatureClass pToFeatureClass;
        //    if (AnalystHelper.IsExistFeatureClass(LayerName, pWorkspace))
        //    {
        //        pToFeatureClass = (pWorkspace as IFeatureWorkspace).OpenFeatureClass(LayerName);
        //    }
        //    else
        //    {
        //        pToFeatureClass = (pWorkspace as IFeatureWorkspace).CreateFeatureClass(LayerName, pNewFields, null, null, pointScheme.PointFeatureClass.FeatureType, "SHAPE", "");
        //    }

        //    string strValue1 = "";
        //    if (string.IsNullOrWhiteSpace(strValue))
        //    {
        //        strValue1 = "其他";
        //    }
        //    else
        //    {
        //        strValue1 = strValue;
        //    }
        //    IFeatureClass pFromFeatureClass = pointScheme.PointFeatureClass;

        //    //得到当前编辑空间
        //    IWorkspaceEdit tWorkspaceEdit = pWorkspace as IWorkspaceEdit;// GetNewEditableWorkspace(tWorkspace);
        //    tWorkspaceEdit.StartEditing(false);
        //    tWorkspaceEdit.StartEditOperation();

        //    try
        //    {
        //        //设置游标状态为插入
        //        IFeatureCursor tFeatureCursor = pToFeatureClass.Insert(true);
        //        //设置要素缓存
        //        IFeatureBuffer tFeatureBuffer = pToFeatureClass.CreateFeatureBuffer();
        //        //获取源文件要素类的的记录集游标
        //        IFeatureCursor tFeatureCursor2 = pFromFeatureClass.Search(tQueryFilter, true);
        //        trackProgress.SubValue = 0;
        //        trackProgress.SubMax = pFromFeatureClass.FeatureCount(tQueryFilter);
        //        //复制每个要素
        //        IFeature tFeature = tFeatureCursor2.NextFeature();
        //        int number = 0;

        //        while (tFeature != null)
        //        {
        //            string sPno = tFeature.GetValue(pointScheme.strPNo).ToString();
        //            try
        //            {
        //                if (!trackProgress.IsContinue) break;
        //                number++;
        //                trackProgress.SubValue++;
        //                trackProgress.SubMessage = $"正在生成{strValue1}{trackProgress.SubValue}/{trackProgress.SubMax}";
        //                Application.DoEvents();

        //                IGeometry tShapeCopy0 = tFeature.ShapeCopy;
        //                IGeometry tShapeCopy = tFeature.ShapeCopy;
        //                #region 适配球面
        //                IGeometry pShapeCopy = AnalystHelper.GCStoPRJ(tShapeCopy);
        //                if (pShapeCopy == null)
        //                {
        //                    IsSuccess = false;
        //                    break;
        //                }
        //                ISpatialReference spatialReference = pShapeCopy.SpatialReference;
        //                string name = spatialReference.Name;
        //                #endregion

        //                #region 获取关联管线属性
        //                if (string.IsNullOrWhiteSpace(sPno))
        //                {
        //                    tFeature = tFeatureCursor2.NextFeature();
        //                    continue;
        //                }

        //                //判断管点所关联管线是否有起止点高程字段，若存在则按照关联管线的端点高程
        //                int shIndex = pointScheme.LineFeatureClass.FindField(pointScheme.strS_Hight);
        //                int ehIndex = pointScheme.LineFeatureClass.FindField(pointScheme.strE_Hight);
        //                if (shIndex < 0 || ehIndex < 0)
        //                {
        //                    IsSuccess = false;
        //                    break;
        //                }

        //                #region 根据管点点号从关联管线数据中获取特征点模型绘制所需的一些属性
        //                bool Isolated = false;
        //                double maxPSizeW = 0.0;
        //                double maxPSizeH = 0.0;
        //                double minHigh = 0.0;

        //                //需要通过关联管线获取连接的管线的最大管径、最长管线的旋转角、最低高程值、管线的横截面高
        //                pointScheme.GetFeatureByLine(sPno, out minHigh, out maxPSizeW, out maxPSizeH, out Isolated);

        //                //判断孤立节点
        //                if (Isolated)
        //                {
        //                    tFeature = tFeatureCursor2.NextFeature();
        //                    continue;
        //                }
        //                #endregion

        //                double dX = (pShapeCopy as IPoint).X;
        //                double dY = (pShapeCopy as IPoint).Y;
        //                double dZ = 0.0;

        //                #endregion
        //                if (pointScheme.CalculationType == CalculationType.Type1)
        //                {
        //                    dZ = minHigh + maxPSizeH / 2;
        //                }
        //                else
        //                {
        //                    dZ = minHigh - maxPSizeH / 2;
        //                }

        //                IPoint CenterPoint = PipeUtil.ConstructPnt3D(dX, dY, dZ, pShapeCopy.SpatialReference);
        //                List<BranchPoint> lsPoints = GetBranchPoint3D(pointScheme, sPno);
        //                IGeometry pGeometry = null;

        //                if (lsPoints.Count > 0)
        //                {
        //                    pGeometry = PipeUtil.CreateSphere(CenterPoint, maxPSizeH, PointBigType, dic3DSymbol);
        //                    if (pGeometry != null)
        //                    {
        //                        IFields Flds = pFromFeatureClass.Fields;
        //                        for (int i = 0; i < Flds.FieldCount; i++)
        //                        {
        //                            IField Fld = Flds.get_Field(i);
        //                            int index = tFeatureCursor2.Fields.FindField(Fld.Name);
        //                            if (index < 0) continue;
        //                            if (!Fld.Editable) continue;
        //                            if (Fld.Type == esriFieldType.esriFieldTypeGeometry) continue;
        //                            int index1 = pToFeatureClass.FindField(Fld.Name);
        //                            if (index1 < 0) continue;
        //                            //字段赋值
        //                            tFeatureBuffer.set_Value(index1, tFeature.get_Value(i));
        //                        }

        //                        pGeometry.SpatialReference = pShapeCopy.SpatialReference;
        //                        if (tShapeCopy0.SpatialReference is IGeographicCoordinateSystem)
        //                        {
        //                            pGeometry = AnalystHelper.ProjectTo4490(pGeometry);
        //                            double x = pGeometry.Envelope.XMax;
        //                        }
        //                        tFeatureBuffer.Shape = pGeometry as IGeometry;
        //                        //插入要素
        //                        tFeatureCursor.InsertFeature(tFeatureBuffer);
        //                    }
        //                }

        //                if (number > 100)
        //                {
        //                    tFeatureCursor.Flush();
        //                    number = 0;
        //                }

        //                tFeature = tFeatureCursor2.NextFeature();
        //            }
        //            catch (Exception ex)
        //            {
        //                ExceptionLog.LogError($"实体号为{tFeature.OID},点号为{sPno}的{strValue}模型生成失败,"
        //                    + ex.Message, ex);
        //                tFeature = tFeatureCursor2.NextFeature();
        //            }
        //        }
        //        tFeatureCursor.Flush();
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursor);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursor2);
        //        GC.Collect();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        IsSuccess = false;
        //    }
        //    finally
        //    {
        //        tWorkspaceEdit.StopEditOperation();
        //        //结束编辑
        //        tWorkspaceEdit.StopEditing(true);
        //    }
        //    return IsSuccess;
        //}
        #endregion

        /// <summary>
        /// 获取管点所连接的管线信息,这里管线的空间参考也要调整一下
        /// </summary>
        /// <param name="pointScheme"></param>
        /// <param name="sPno"></param>
        /// <returns></returns>
        private static List<BranchPoint> GetBranchPoint3D(PointScheme pointScheme, string sPno,
            ISpatialFilter spatialFilter)
        {
            List<BranchPoint> points = new List<BranchPoint>();
            IFeatureCursor pCursor;
            IFeature feature;
            //遍历与点要素相接的管线
            IQueryFilter queryFilter = spatialFilter as IQueryFilter;
            pCursor = pointScheme.LineFeatureClass.Search(queryFilter, false);
            feature = pCursor.NextFeature();
            while (feature != null)
            {
                IGeometry fGeometry = feature.ShapeCopy;
                #region 如果是球面坐标系，则需要先进行投影转换
                ISpatialReference fsr = fGeometry.SpatialReference;
                string name = fsr.Name;
                IGeometry pGeometry = AnalystHelper.GCStoPRJ(fGeometry);
                if (pGeometry == null)
                {
                    break;
                }
                ISpatialReference psr = pGeometry.SpatialReference;
                string name1 = psr.Name;
                #endregion
                //IGeometry pGeometry = fGeometry;
                IPoint sPoint = (pGeometry as IPolyline).FromPoint;
                IPoint ePoint = (pGeometry as IPolyline).ToPoint;
                PipeUtil.MakeZAware(sPoint);
                PipeUtil.MakeZAware(ePoint);

                string strSH = feature.get_Value(feature.Fields.FindField(pointScheme.strS_Hight)).ToString();
                string strEH = feature.get_Value(feature.Fields.FindField(pointScheme.strE_Hight)).ToString();
                if(string.IsNullOrEmpty(strSH)|| string.IsNullOrEmpty(strEH))
                {
                    feature = pCursor.NextFeature();
                    continue;
                }
                double S_HIGH = Convert.ToDouble(strSH);
                double E_HIGH = Convert.ToDouble(strEH);
                double PSizeW = 0;
                double PSizeH = 0;
                Line3DType dType = Line3DType.Circle;
                feature.GetPSize(pointScheme.strPSize, out PSizeW, out PSizeH, out dType);

                if (pointScheme.CalculationType == CalculationType.Type1)
                {
                    sPoint.Z = S_HIGH + PSizeH / 2;
                    ePoint.Z = E_HIGH + PSizeH / 2;
                }
                else
                {
                    sPoint.Z = S_HIGH - PSizeH / 2;
                    ePoint.Z = E_HIGH - PSizeH / 2;
                }

                string strSNo = feature.GetValue(pointScheme.strS_Point).ToString();
                string strENo = feature.GetValue(pointScheme.strE_Point).ToString();
                
                if (sPno == strSNo)
                {
                    points.Add(new BranchPoint() { FromPnt = sPoint, ToPnt = ePoint, W = PSizeW, H = PSizeH, DType = dType });
                }
                if (sPno == strENo)
                {
                    points.Add(new BranchPoint() { FromPnt = ePoint, ToPnt = sPoint, W = PSizeW, H = PSizeH, DType = dType });
                }

                feature = pCursor.NextFeature();
            }

            return points;
        }

        /// <summary>
        /// 已原有字段集 生成新的3维字段
        /// </summary>
        /// <param name="pFields"></param>
        /// <returns></returns>
        public static IFields CreateFields3D(IFields pFields, ISpatialReference spatialReference, esriGeometryType esriGeometryType)
        {
            IFields newFields = new FieldsClass();
            IFieldsEdit tFieldsEdit = (IFieldsEdit)newFields;
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                IField pField = pFields.Field[i];
                if (pField.Type == esriFieldType.esriFieldTypeGeometry)
                {
                    continue;
                }
                tFieldsEdit.AddField(pField);
            }

            //创建要素类的关键字段shape字段支持类型设置。
            IField field = new FieldClass();
            IFieldEdit fieldEdit = field as IFieldEdit;
            IGeometryDef geoDef = new GeometryDefClass();
            IGeometryDefEdit geoDefEdit = (IGeometryDefEdit)geoDef;
            geoDefEdit.SpatialReference_2 = spatialReference;
            geoDefEdit.AvgNumPoints_2 = 5;
            geoDefEdit.GeometryType_2 = esriGeometryType;
            geoDefEdit.GridCount_2 = 1;
            geoDefEdit.HasM_2 = false;//支持M字段
            geoDefEdit.HasZ_2 = true;//设置为true以支持存储高程
            fieldEdit.Name_2 = "shape";
            fieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
            fieldEdit.GeometryDef_2 = geoDef;
            fieldEdit.IsNullable_2 = true;
            fieldEdit.Required_2 = true;
            tFieldsEdit.AddField(field);

            return newFields;
        }

        /// <summary>
        /// 附属物是否顶部设为地面高程
        /// </summary>
        /// <returns></returns>
        private static List<string> IsAddSizeSubsid()
        {
            List<string> subsids = new List<string>();
            #region
            //subsids.Add("雨篦子");
            //subsids.Add("雨篦");
            //subsids.Add("雨水口");
            //subsids.Add("污水口");
            //subsids.Add("排水口");
            //subsids.Add("进水口");
            //subsids.Add("污篦");
            #endregion
            string configFile = CommonConstString.STR_ConfigPath + "\\AddSizeSubsids.txt";
            if (!File.Exists(configFile)) return subsids;

            StreamReader streamReader = new StreamReader(configFile);
            string strValues = streamReader.ReadLine();
            if (string.IsNullOrEmpty(strValues)) return subsids;
            string[] values = strValues.Split(',');
            if (values.Length <= 0) return subsids;
            foreach (string value in values)
            {
                subsids.Add(value);
            }

            return subsids;
        }

        /// <summary>
        /// 附属物是否顶部不设为地面高程
        /// </summary>
        /// <returns></returns>
        private static List<string> IsNotSizeSubsid()
        {
            List<string> subsids = new List<string>();
            #region
            //subsids.Add("阀门");
            //subsids.Add("消火栓");
            //subsids.Add("压力表");
            //subsids.Add("水表");
            //subsids.Add("排泥阀");
            //subsids.Add("排气阀");
            //subsids.Add("路灯");
            //subsids.Add("灯杆");
            //subsids.Add("信号灯");
            //subsids.Add("交通信号灯");
            //subsids.Add("地灯");
            //subsids.Add("消防栓");
            //subsids.Add("交通信号杆");
            //subsids.Add("交通监控器");
            //subsids.Add("监视器");
            //subsids.Add("电线杆");
            //subsids.Add("监控器");
            #endregion

            string configFile = CommonConstString.STR_ConfigPath + "\\NotSizeSubsids.txt";
            if (!File.Exists(configFile)) return subsids;

            StreamReader streamReader = new StreamReader(configFile);
            string strValues = streamReader.ReadLine();
            if (string.IsNullOrEmpty(strValues)) return subsids;
            string[] values = strValues.Split(',');
            if (values.Length <= 0) return subsids;
            foreach (string value in values)
            {
                subsids.Add(value);
            }

            return subsids;
        }

        public static string[] FeatureDoubleConnect()
        {
            string[] features = new string[]
            { "弯头", "直线点","拐点","转折点","变径","变材","管线点","一般管线点","量测点",
            "探测点","变规格"};

            return features;
        }

        public static string[] FeatureMoreConnect()
        {
            string[] features = new string[]
            { "多通点", "三通点","三通","四通","五通","六通","直通","分支","分支点",
            "多通头","三通头"};

            return features;
        }

        public static string[] FeatureSingleConnect()
        {
            string[] features = new string[]
            { "预留口", "非普查点","非普查","起点","终点","井边点","偏心点","入户点",
                "非探测点","出水口","进水口","上杆","堵口","出地点","入地点","出入地点",
            "管末","起始点","终止点","进出房点","封头","蒙板","非普区"};

            return features;
        }

    }
}
