using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
namespace AG.Pipe.Analyst3DModel
{
    /// <summary>
    /// 管线模型生成类
    /// </summary>
    public class PipeUtil
    {
        #region 圆管线
        /// <summary>
        /// 生成圆管线
        /// </summary>
        /// <param name="FromPnt">起点</param>
        /// <param name="ToPnt">终点</param>
        /// <param name="R">半径</param>
        /// <param name="colse">是否空心 false 空心</param>
        /// <param name="sr">空间信息</param>
        /// <returns></returns>
        public static IGeometry CreateCirclePipe(IPoint FromPnt, IPoint ToPnt, double R, bool colse = false, ISpatialReference sr = null)
        {
            //计算两点之间长度
            double length = GetTwoPntsDistance3D(FromPnt, ToPnt);
            //获取Z轴 向量
            IVector3D VectorZ = ConstructVector3D(0, 0, 1);
            //获取管线的向量
            IVector3D VectorPipe = ConstructVector3D(ToPnt.X - FromPnt.X, ToPnt.Y - FromPnt.Y, ToPnt.Z - FromPnt.Z);
            //获得Z轴向量与管线向量的夹角，用作旋转使用
            double RotateAngle = GetTwoVectorsAngleInclination(VectorZ, VectorPipe);//获得Z轴向量与真实世界中管线向量的夹角，用作旋转使用
            //获取Z轴向量 与管线向量 叉积  旋转轴向量
            IVector3D VectorAxis = VectorZ.CrossProduct(VectorPipe) as IVector3D;
            IGeometry geometry = ExtrudeCircle2(R, length, colse, sr);
            ITransform3D transforms3D = geometry as ITransform3D;
            //使用叉积向量作为旋转轴 进行 旋转
            transforms3D.RotateVector3D(VectorAxis, RotateAngle);
            //移动到 起始点
            transforms3D.Move3D(FromPnt.X, FromPnt.Y, FromPnt.Z);
            return geometry;
        }
        /// <summary>
        /// 根据3D管线符号生成管线模型
        /// </summary>
        /// <param name="FromPnt">起点</param>
        /// <param name="ToPnt">终点</param>
        /// <param name="R">管半径</param>
        /// <param name="pLine3DSymbol">绘制样式</param>
        /// <param name="colse"></param>
        /// <param name="sr"></param>
        /// <returns></returns>
        public static IGeometry CreateCirclePipe(IPoint FromPnt, IPoint ToPnt, double R, IMarker3DSymbol pLine3DSymbol,
            bool colse = false, ISpatialReference sr = null)
        {
            //计算两点之间长度
            double length = GetTwoPntsDistance3D(FromPnt, ToPnt);
            //中心点
            IPoint CenterPnt = ConstructPnt3D(0, 0, 0);
            //获取Z轴 向量
            IVector3D VectorZ = ConstructVector3D(0, 0, 1);
            //获取管线的向量
            IVector3D VectorPipe = ConstructVector3D(ToPnt.X - FromPnt.X, ToPnt.Y - FromPnt.Y, ToPnt.Z - FromPnt.Z);
            //获得Z轴向量与管线向量的夹角，用作旋转使用
            double RotateAngle = GetTwoVectorsAngleInclination(VectorZ, VectorPipe);//获得Z轴向量与真实世界中管线向量的夹角，用作旋转使用
            //获取Z轴向量 与管线向量 叉积  旋转轴向量
            IVector3D VectorAxis = VectorZ.CrossProduct(VectorPipe) as IVector3D;
            IGeometry geometry = null;
            IMarker3DPlacement pMarker3DPlacement = pLine3DSymbol as IMarker3DPlacement;
            pMarker3DPlacement.Width = 2 * R;
            pMarker3DPlacement.Depth = 2 * R;
            pMarker3DPlacement.Size = length;
            pMarker3DPlacement.Angle = 0;
            pMarker3DPlacement.MaintainAspectRatio = false;//生成管线模型要取消纵横比
            pLine3DSymbol.UseMaterialDraping = false;
            pMarker3DPlacement.ApplyToPoint(CenterPnt, out geometry);

            ITransform3D transforms3D = geometry as ITransform3D;
            //使用叉积向量作为旋转轴 进行 旋转
            transforms3D.RotateVector3D(VectorAxis, RotateAngle);
            //移动到 起始点
            transforms3D.Move3D(FromPnt.X, FromPnt.Y, FromPnt.Z);

            return geometry;

        }
        /// <summary>
        /// 生成圆支管线，根据3D样式符号配置读取圆支管线模型
        /// </summary>
        /// <param name="CirType"></param>
        /// <param name="dic3DSymbol"></param>
        /// <param name="FromPnt"></param>
        /// <param name="ToPnt"></param>
        /// <param name="R"></param>
        /// <param name="diameter"></param>
        /// <param name="colse"></param>
        /// <param name="sr"></param>
        /// <returns></returns>
        public static IGeometry CreateCirclePipe(string CirType, Dictionary<string, IMarker3DSymbol> dic3DSymbol, IPoint FromPnt, IPoint ToPnt,
            double R, double diameter, bool colse = false, ISpatialReference sr = null)
        {
            //计算两点之间长度
            double length = GetTwoPntsDistance3D(FromPnt, ToPnt);
            IPoint CenterPnt = ConstructPnt3D(0, 0, 0);
            IVector3D VectorZ = ConstructVector3D(0, 0, 1); //获取Z轴 向量
            //获取管线的向量
            IVector3D VectorPipe = ConstructVector3D(ToPnt.X - FromPnt.X, ToPnt.Y - FromPnt.Y, ToPnt.Z - FromPnt.Z);
            //获得Z轴向量与管线向量的夹角，用作旋转使用
            double RotateAngle = GetTwoVectorsAngleInclination(VectorZ, VectorPipe);//获得Z轴向量与真实世界中管线向量的夹角，用作旋转使用
            //获取Z轴向量 与管线向量 叉积  旋转轴向量
            IVector3D VectorAxis = VectorZ.CrossProduct(VectorPipe) as IVector3D;
            IGeometry geometry = null;
            //判断样式符号文件中是否有这个大类支管名称的样式符号，若有则读取相应样式符号，否则代码生成圆支管模型
            if (dic3DSymbol.ContainsKey(CirType))
            {
                IMarker3DSymbol pLine3DSymbol = dic3DSymbol[CirType];
                IMarker3DPlacement pMarker3DPlacement = pLine3DSymbol as IMarker3DPlacement;
                pMarker3DPlacement.Width = 2 * R;
                pMarker3DPlacement.Depth = 2 * R;
                pMarker3DPlacement.Size = length;
                pMarker3DPlacement.Angle = 0;
                pLine3DSymbol.UseMaterialDraping = false;
                pMarker3DPlacement.ApplyToPoint(CenterPnt, out geometry);
            }
            else
            {
                geometry = ExtrudeCircle2(R, diameter, length, colse, sr);
            }
            ITransform3D transforms3D = geometry as ITransform3D;
            //使用叉积向量作为旋转轴 进行 旋转
            transforms3D.RotateVector3D(VectorAxis, RotateAngle);
            //移动到 起始点
            transforms3D.Move3D(FromPnt.X, FromPnt.Y, FromPnt.Z);
            return geometry;
        }
        /// <summary>
        /// 只连接一条管线生成预留口，读取样式配置符号
        /// </summary>
        /// <param name="branchPoint"></param>
        /// <param name="PointBigType"></param>
        /// <param name="dic3DSymbol"></param>
        /// <param name="sr"></param>
        /// <returns></returns>
        public static IGeometry CreateKeep(BranchPoint branchPoint, string PointBigType,
            Dictionary<string, IMarker3DSymbol> dic3DSymbol, ISpatialReference sr = null)
        {
            double H = branchPoint.H / 2;
            H = H + H / 20;
            double elbowlen = H;
            double headsize = elbowlen / 3;
            double plen1 = GetTwoPntsDistance3D(branchPoint.FromPnt, branchPoint.ToPnt);
            //当弯头长度 大于管线距离时，取 弯头 一半 
            if (elbowlen > plen1)
            {
                elbowlen = elbowlen / 2;
            }
            if (elbowlen > plen1)
            {
                elbowlen = elbowlen / 2;
            }
            IGeometry geometry = null;
            //根据所连管线是圆管或是方管，在3D符号配置中查找对应大类支管的符号
            if (branchPoint.DType == Line3DType.Square)
            {
                string branchType = $"{PointBigType}_方管";
                geometry = CreateCubeBranchPipe(branchType, dic3DSymbol, branchPoint.FromPnt, branchPoint.ToPnt, branchPoint.W / 2, branchPoint.H / 2, elbowlen, 0, 0, sr, true);
            }
            else
            {
                string branchType = $"{PointBigType}_圆管";
                geometry = CreateCircleBranchPipe(branchType, dic3DSymbol, branchPoint.FromPnt, branchPoint.ToPnt, H, elbowlen, 0, 0, sr, true);
            }
            return geometry;
        }
        /// <summary>
        /// 创建分支管线，读取3D符号样式文件用里面的模型
        /// </summary>
        /// <param name="branchType"></param>
        /// <param name="dic3DSymbol"></param>
        /// <param name="FromPnt"></param>
        /// <param name="ToPnt"></param>
        /// <param name="R"></param>
        /// <param name="elbowlen"></param>
        /// <param name="position"></param>
        /// <param name="headposition"></param>
        /// <param name="sr"></param>
        /// <param name="close"></param>
        /// <returns></returns>
        public static IGeometry CreateCircleBranchPipe(string branchType, Dictionary<string, IMarker3DSymbol> dic3DSymbol, IPoint FromPnt, IPoint ToPnt,
            double R, double elbowlen, int position, int headposition, ISpatialReference sr = null, bool close = false)
        {

            R = R + R / 20;
            //弯头长=半径 *2
            double headsize = R / 4;
            IPolyline polyline = new PolylineClass();
            polyline.FromPoint = FromPnt;
            polyline.ToPoint = ToPnt;
            double plen1 = GetTwoPntsDistance3D(FromPnt, ToPnt);
            //当弯头长度 大于管线距离时，取 弯头 一半 
            if (elbowlen > plen1)
            {
                elbowlen = elbowlen / 2;
            }
            if (elbowlen > plen1)
            {
                elbowlen = elbowlen / 2;
            }
            if (position == 0)
            {
                //获取分割点
                IPoint pSpilt = SplitAtDistance(FromPnt, ToPnt, elbowlen, true);
                if (pSpilt == null)
                {
                    return null;
                }
                IPoint pEnd;
                IGeometry geometryE;
                if (headposition == 0)
                {
                    pEnd = SplitAtDistance(pSpilt, FromPnt, headsize, false);
                    geometryE = CreateCirclePipe(branchType, dic3DSymbol, pEnd, FromPnt, R + R / 10, R / 10, close, sr);
                    //geometryE = CreateCirclePipeNew(pEnd, FromPnt, R + R / 10, R / 10, close, sr);
                }
                else
                {
                    pEnd = SplitAtDistance(pSpilt, FromPnt, headsize, true);
                    geometryE = CreateCirclePipe(branchType, dic3DSymbol, pEnd, pSpilt, R + R / 10, R / 10, close, sr);
                    //geometryE = CreateCirclePipeNew(pEnd, FromPnt, R + R / 10, R / 10, close, sr);
                }

                IGeometry geometry1 = CreateCirclePipe(branchType, dic3DSymbol, FromPnt, pSpilt, R, R / 10, close, sr);
                //IGeometry geometry1 = CreateCirclePipeNew(FromPnt, pSpilt, R, R / 10, close, sr);
                IGeometry geometry = UnionTwoGeometries(geometry1, geometryE);
                return geometry;
            }
            else
            {

                //获取分割点
                IPoint pSpilt = SplitAtDistance(FromPnt, ToPnt, elbowlen, false);
                IPoint pEnd;
                IGeometry geometryE;
                if (headposition == 0)
                {
                    pEnd = SplitAtDistance(pSpilt, ToPnt, headsize, true);
                    geometryE = CreateCirclePipe(branchType, dic3DSymbol, pEnd, pSpilt, R + R / 10, R / 10, close, sr);
                    //geometryE = CreateCirclePipeNew(pEnd, FromPnt, R + R / 10, R / 10, close, sr);
                }
                else
                {
                    pEnd = SplitAtDistance(pSpilt, ToPnt, headsize, false);
                    geometryE = CreateCirclePipe(branchType, dic3DSymbol, pEnd, ToPnt, R + R / 10, R / 10, close, sr);
                    //geometryE = CreateCirclePipeNew(pEnd, FromPnt, R + R / 10, R / 10, close, sr);
                }

                IGeometry geometry1 = CreateCirclePipe(branchType, dic3DSymbol, pSpilt, ToPnt, R, R / 10, close, sr);
                //IGeometry geometry1 = CreateCirclePipeNew(FromPnt, pSpilt, R, R / 10, close, sr);
                IGeometry geometry = UnionTwoGeometries(geometry1, geometryE);
                return geometry;
            }
        }
        #endregion

        #region 方管线
        /// <summary>
        /// 生成方管线
        /// </summary>
        /// <param name="FromPnt">起点</param>
        /// <param name="ToPnt">终点</param>
        /// <param name="W">宽</param>
        /// <param name="H">高</param>
        /// <param name="colse">是否封闭 false 不封闭</param>
        /// <param name="sr">空间信息</param>
        /// <returns></returns>
        public static IGeometry CreateCubePipe(IPoint FromPnt, IPoint ToPnt, double W, double H, bool colse = false, ISpatialReference sr = null)
        {
            //计算两点之间长度
            double length = GetTwoPntsDistance3D(FromPnt, ToPnt);
            //获取Z轴 向量
            IVector3D VectorZ = ConstructVector3D(0, 0, 1);
            //获取管线的向量
            IVector3D VectorPipe = ConstructVector3D(ToPnt.X - FromPnt.X, ToPnt.Y - FromPnt.Y, ToPnt.Z - FromPnt.Z);
            //获得Z轴向量与管线向量的夹角，用作旋转使用
            double RotateAngle = GetTwoVectorsAngleInclination(VectorZ, VectorPipe);
            //获取Z轴向量 与管线向量 叉积  旋转轴向量
            IVector3D VectorAxisZ = VectorZ.CrossProduct(VectorPipe) as IVector3D;//旋转轴向量
            IGeometry geometry = ExtrudeCube2(W, H, length, colse, sr);
            ITransform3D transforms3D = geometry as ITransform3D;
            //使用叉积向量作为旋转轴 进行 旋转
            transforms3D.RotateVector3D(VectorAxisZ, RotateAngle);
            double rightangle = GetRadians(90);
            //使用管线向量作为旋转轴 进行 旋转 保持水平
            transforms3D.RotateVector3D(VectorPipe, rightangle - VectorPipe.Azimuth);
            //移动到 起始点
            transforms3D.Move3D(FromPnt.X, FromPnt.Y, FromPnt.Z);
            return geometry;
        }
        /// <summary>
        /// 根据管线模型 生成方管管线
        /// </summary>
        /// <param name="FromPnt"></param>
        /// <param name="ToPnt"></param>
        /// <param name="W"></param>
        /// <param name="H"></param>
        /// <param name="pLine3DSymbol"></param>
        /// <param name="colse"></param>
        /// <param name="sr"></param>
        /// <returns></returns>
        public static IGeometry CreateCubePipe(IPoint FromPnt, IPoint ToPnt, double W, double H, IMarker3DSymbol pLine3DSymbol, bool colse = false, ISpatialReference sr = null)
        {
            //计算两点之间长度
            double length = GetTwoPntsDistance3D(FromPnt, ToPnt);
            IPoint CenterPnt = ConstructPnt3D(0, 0, 0);
            //获取Z轴 向量
            IVector3D VectorZ = ConstructVector3D(0, 0, 1);
            //获取管线的向量
            IVector3D VectorPipe = ConstructVector3D(ToPnt.X - FromPnt.X, ToPnt.Y - FromPnt.Y, ToPnt.Z - FromPnt.Z);
            //获得Z轴向量与管线向量的夹角，用作旋转使用
            double RotateAngle = GetTwoVectorsAngleInclination(VectorZ, VectorPipe);
            //获取Z轴向量 与管线向量 叉积  旋转轴向量
            IVector3D VectorAxisZ = VectorZ.CrossProduct(VectorPipe) as IVector3D;//旋转轴向量
            IGeometry geometry = null;
            IMarker3DPlacement pMarker3DPlacement = pLine3DSymbol as IMarker3DPlacement;
            pMarker3DPlacement.Width = H;
            pMarker3DPlacement.Depth = W;
            pMarker3DPlacement.Size = length;
            pMarker3DPlacement.Angle = 0;
            pMarker3DPlacement.MaintainAspectRatio = false;
            pMarker3DPlacement.ApplyToPoint(CenterPnt, out geometry);
            ITransform3D transforms3D = geometry as ITransform3D;
            //使用叉积向量作为旋转轴 进行 旋转
            transforms3D.RotateVector3D(VectorAxisZ, RotateAngle);
            double rightangle = GetRadians(90);
            //使用管线向量作为旋转轴 进行 旋转 保持水平
            transforms3D.RotateVector3D(VectorPipe, rightangle - VectorPipe.Azimuth);
            //移动到 起始点
            transforms3D.Move3D(FromPnt.X, FromPnt.Y, FromPnt.Z);
            return geometry;
        }
        /// <summary>
        /// 生成方管线
        /// </summary>
        /// <param name="FromPnt">起点</param>
        /// <param name="ToPnt">终点</param>
        /// <param name="W">宽</param>
        /// <param name="H">高</param>
        /// <param name="Diameter">管壁厚度</param>
        /// <param name="colse">是否封闭 false 不封闭</param>
        /// <param name="sr">空间信息</param>
        /// <returns></returns>
        public static IGeometry CreateCubePipe(string CubeType, Dictionary<string, IMarker3DSymbol> dic3DSymbol, IPoint FromPnt, IPoint ToPnt, double W, double H, bool colse = false, ISpatialReference sr = null)
        {
            //计算两点之间长度
            double length = GetTwoPntsDistance3D(FromPnt, ToPnt);

            IPoint CenterPnt = ConstructPnt3D(0, 0, 0);
            //获取Z轴 向量
            IVector3D VectorZ = ConstructVector3D(0, 0, 1);
            //获取管线的向量
            IVector3D VectorPipe = ConstructVector3D(ToPnt.X - FromPnt.X, ToPnt.Y - FromPnt.Y, ToPnt.Z - FromPnt.Z);
            //获得Z轴向量与管线向量的夹角，用作旋转使用
            double RotateAngle = GetTwoVectorsAngleInclination(VectorZ, VectorPipe);
            //获取Z轴向量 与管线向量 叉积  旋转轴向量
            IVector3D VectorAxisZ = VectorZ.CrossProduct(VectorPipe) as IVector3D;//旋转轴向量

            IGeometry geometry = null;
            //判断样式符号文件中是否有这个大类支管名称的样式符号，若有则读取相应样式符号，否则代码生成圆支管模型
            if (dic3DSymbol.ContainsKey(CubeType))
            {
                IMarker3DSymbol pLine3DSymbol = dic3DSymbol[CubeType];
                IMarker3DPlacement pMarker3DPlacement = pLine3DSymbol as IMarker3DPlacement;
                pMarker3DPlacement.Width = 2 * H;
                pMarker3DPlacement.Depth = 2 * W;
                pMarker3DPlacement.Size = length;
                pMarker3DPlacement.Angle = 0;
                pLine3DSymbol.UseMaterialDraping = false;
                pMarker3DPlacement.ApplyToPoint(CenterPnt, out geometry);
            }
            else
            {
                geometry = ExtrudeCube2(W, H, length, false, sr);
            }

            ITransform3D transforms3D = geometry as ITransform3D;
            //使用叉积向量作为旋转轴 进行 旋转
            transforms3D.RotateVector3D(VectorAxisZ, RotateAngle);
            double rightangle = GetRadians(90);
            //使用管线向量作为旋转轴 进行 旋转 保持水平
            transforms3D.RotateVector3D(VectorPipe, rightangle - VectorPipe.Azimuth);
            //移动到 起始点
            transforms3D.Move3D(FromPnt.X, FromPnt.Y, FromPnt.Z);
            return geometry;
        }
        /// <summary>
        /// 生成弯头类特征点，读取3D样式符号文件
        /// </summary>
        /// <param name="CenterPnt"></param>
        /// <param name="branchPoints"></param>
        /// <param name="BigType"></param>
        /// <param name="dic3DSymbol"></param>
        /// <param name="W"></param>
        /// <param name="H"></param>
        /// <param name="sr"></param>
        /// <returns></returns>
        public static IGeometry CreateElbowPipe(IPoint CenterPnt, List<BranchPoint> branchPoints, string BigType,
            Dictionary<string, IMarker3DSymbol> dic3DSymbol, double W, double H, ISpatialReference sr = null)
        {
            string name = sr.Name;

            if (branchPoints.Count != 2) return null;
            List<IGeometry> geometries = new List<IGeometry>();
            W = W + W / 20;
            H = H + H / 20;
            //遍历管点处的每条连接管线,每条管线连接处生成一根支管
            foreach (BranchPoint item in branchPoints)
            {
                IGeometry geometry1 = null;

                if (item.DType == Line3DType.Square)
                {
                    string branchType = $"{BigType}_方管";
                    //geometry1 = CreateCubeBranchPipe(item.FromPnt, item.ToPnt, item.W / 2, item.H / 2, item.H * 2, 0, 1, sr, false);
                    geometry1 = CreateCubeBranchPipe(branchType, dic3DSymbol, item.FromPnt, item.ToPnt, item.W / 2, item.H / 2, item.W, 0, 1, sr);
                }
                else
                {
                    if (item.FromPnt == null)
                    {
                        string ss = "";
                    }
                    string branchType = $"{BigType}_圆管";
                    geometry1 = CreateCircleBranchPipe(branchType, dic3DSymbol, item.FromPnt, item.ToPnt, item.R, item.R * 2, 0, 1, sr, false);
                }

                geometries.Add(geometry1);
            }
            IGeometry geometry3 = null;
            if (branchPoints[0].DType == Line3DType.Square)
            {
                #region 之前是代码里面生成的中间的圆柱体，现改为读取符号配置里面的相应大类的圆管模型
                string str3DSymbol = $"{BigType}_圆管";
                if (dic3DSymbol.ContainsKey(str3DSymbol))
                {
                    IMarker3DSymbol marker3DSymbol = dic3DSymbol[str3DSymbol];
                    IPoint ToPoint = new PointClass();
                    ToPoint.PutCoords(CenterPnt.X, CenterPnt.Y);
                    ToPoint.SpatialReference = CenterPnt.SpatialReference;
                    ToPoint.Z = CenterPnt.Z + H / 2;
                    CenterPnt.Z -= H / 2;
                    geometry3 = CreateCirclePipe(CenterPnt, ToPoint, W / 2, marker3DSymbol, true, sr);
                }

                #endregion
                else
                {
                    geometry3 = ExtrudeSector1(CenterPnt, W / 2, H / 2, sr);
                }
            }
            else
            {
                string sphereType = $"{BigType}_球体";
                //根据大类在3D符号配置里寻找球体符号
                geometry3 = CreateSphere(CenterPnt, H, sphereType, dic3DSymbol);
            }
            IGeometry geometry = null;
            foreach (IGeometry item in geometries)
            {
                if (geometry == null)
                {
                    geometry = UnionTwoGeometries(geometry3, item);
                }
                else
                {
                    geometry = UnionTwoGeometries(geometry, item);
                }

            }
            return geometry;
        }
        /// <summary>
        /// 生成多支管，读取3D样式符号文件
        /// </summary>
        /// <param name="CenterPnt"></param>
        /// <param name="branchPoints"></param>
        /// <param name="BigType"></param>
        /// <param name="dic3DSymbol"></param>
        /// <param name="sr"></param>
        /// <returns></returns>
        public static IGeometry CreateMultiPipe(IPoint CenterPnt, List<BranchPoint> branchPoints, string BigType,
            Dictionary<string, IMarker3DSymbol> dic3DSymbol, ISpatialReference sr)
        {
            List<IGeometry> geometries = new List<IGeometry>();
            double maxH = 0.0;
            double maxW = 0.0;
            //double maxR = 0.0;
            foreach (BranchPoint item in branchPoints)
            {
                if (item.H > maxH) maxH = item.H / 2;
                if (item.W > maxW) maxW = item.W / 2;
                //if (item.R > maxR) maxR = item.R;
            }
            double minZ = 0;
            double maxZ = 0;
            foreach (BranchPoint item in branchPoints)
            {
                if(item.FromPnt==null || item.ToPnt==null)
                {
                    continue;
                }

                if (minZ == 0)
                    minZ = item.FromPnt.Z;
                if (maxZ == 0)
                    maxZ = item.FromPnt.Z;
                minZ = Math.Min(item.FromPnt.Z, minZ);
                maxZ = Math.Max(item.FromPnt.Z, maxZ);
                IGeometry geometry1 = null;
                if (item.DType == Line3DType.Square)
                {
                    string branchType = $"{BigType}_方管";
                    IPoint ift = item.FromPnt;
                    IPoint itt = item.ToPnt;
                    geometry1 = CreateCubeBranchPipe(branchType, dic3DSymbol, ift, itt, item.W / 2, item.H / 2, item.W, 0, 1, sr);
                }
                else
                {
                    string branchType = $"{BigType}_圆管";
                    IPoint ift = item.FromPnt;
                    IPoint itt = item.ToPnt;
                    geometry1 = CreateCircleBranchPipe(branchType, dic3DSymbol, ift, itt, item.R, maxH * 2, 0, 1, sr);
                }

                geometries.Add(geometry1);

            }
            double deep = maxZ - minZ;
            IPoint FromPnt = ConstructPnt3D(CenterPnt.X, CenterPnt.Y, CenterPnt.Z - maxH - maxH / 10);
            IPoint ToPnt = ConstructPnt3D(CenterPnt.X, CenterPnt.Y, CenterPnt.Z + deep + maxH + maxH / 10);
            IGeometry geometryS = null;
            if (branchPoints[0].DType == Line3DType.Square)
            {
                string branchType = $"{BigType}_方管";
                geometryS = CreateCubePipe(branchType, dic3DSymbol, FromPnt, ToPnt, maxW, maxH + maxH / 5, true, sr);
            }
            else
            {
                string branchType = $"{BigType}_圆管";
                geometryS = CreateCirclePipe(branchType, dic3DSymbol, FromPnt, ToPnt, maxH + maxH / 5, maxH / 10, true, sr);
            }

            IGeometry geometry = null;
            foreach (IGeometry item in geometries)
            {
                if (geometry == null)
                {
                    geometry = UnionTwoGeometries(geometryS, item);
                }
                else
                {
                    geometry = UnionTwoGeometries(geometry, item);
                }

            }
            return geometry;
        }
        /// <summary>
        /// 创建方形分支管线
        /// </summary>
        /// <param name="branchType"></param>
        /// <param name="dic3DSymbol"></param>
        /// <param name="FromPnt"></param>
        /// <param name="ToPnt"></param>
        /// <param name="W"></param>
        /// <param name="H"></param>
        /// <param name="elbowlen"></param>
        /// <param name="position"></param>
        /// <param name="headposition"></param>
        /// <param name="sr"></param>
        /// <param name="close"></param>
        /// <returns></returns>
        public static IGeometry CreateCubeBranchPipe(string branchType, Dictionary<string, IMarker3DSymbol> dic3DSymbol, IPoint FromPnt, IPoint ToPnt,
            double W, double H, double elbowlen, int position, int headposition, ISpatialReference sr = null, bool close = false)
        {
            W = W + W / 20;
            H = H + H / 20;
            double dW = W / 10;
            double dH = H / 10;
            double headsize = elbowlen / 10;
            double plen1 = GetTwoPntsDistance3D(FromPnt, ToPnt);
            //当弯头长度 大于管线距离时，取 弯头 一半 
            if (elbowlen > plen1)
            {
                elbowlen = elbowlen / 2;
            }
            if (elbowlen > plen1)
            {
                elbowlen = elbowlen / 2;
            }
            if (position == 0)
            {
                //获取分割点
                IPoint pSpilt = SplitAtDistance(FromPnt, ToPnt, elbowlen, true);
                IPoint pEnd;
                IGeometry geometryE;
                if (headposition == 0)
                {
                    pEnd = SplitAtDistance(pSpilt, FromPnt, headsize, false);
                    geometryE = CreateCubePipe(branchType, dic3DSymbol, pEnd, FromPnt, W + dW, H + dH, close, sr);
                }
                else
                {
                    pEnd = SplitAtDistance(pSpilt, FromPnt, headsize, true);
                    geometryE = CreateCubePipe(branchType, dic3DSymbol, pEnd, pSpilt, W + dW, H + dH, close, sr);
                }

                IGeometry geometry1 = CreateCubePipe(branchType, dic3DSymbol, FromPnt, pSpilt, W, H, false, sr);
                IGeometry geometry = UnionTwoGeometries(geometry1, geometryE);
                return geometry;
            }
            else
            {

                //获取分割点
                IPoint pSpilt = SplitAtDistance(FromPnt, ToPnt, elbowlen, false);
                IPoint pEnd;
                IGeometry geometryE;
                if (headposition == 0)
                {
                    pEnd = SplitAtDistance(pSpilt, ToPnt, headsize, true);
                    geometryE = CreateCubePipe(branchType, dic3DSymbol, pEnd, pSpilt, W + dW, H + dH, close, sr);
                }
                else
                {
                    pEnd = SplitAtDistance(pSpilt, ToPnt, headsize, false);
                    geometryE = CreateCubePipe(branchType, dic3DSymbol, pEnd, ToPnt, W + dW, H + dH, close, sr);
                }

                IGeometry geometry1 = CreateCubePipe(branchType, dic3DSymbol, pSpilt, ToPnt, W, H, false, sr);
                IGeometry geometry = UnionTwoGeometries(geometry1, geometryE);
                return geometry;
            }
        }
        #endregion

        #region 常用公共方法
        /// <summary>
        /// 计算三维维中两点之间距离
        /// </summary>
        /// <param name="FromPnt"></param>
        /// <param name="ToPnt"></param>
        /// <returns></returns>
        public static double GetTwoPntsDistance3D(IPoint FromPnt, IPoint ToPnt)
        {
            double fX = FromPnt.X;
            double fY = FromPnt.Y;
            double fZ = FromPnt.Z;
            double tZ = ToPnt.Z;
            double tX = ToPnt.X;
            double tY = ToPnt.Y;
            return Math.Sqrt(Math.Pow(fX - tX, 2) + Math.Pow(fY - tY, 2) + Math.Pow(fZ - tZ, 2));
        }
        /// <summary>
        /// 获取向量
        /// </summary>
        /// <param name="xComponent"></param>
        /// <param name="yComponent"></param>
        /// <param name="zComponent"></param>
        /// <returns></returns>
        public static IVector3D ConstructVector3D(double xComponent, double yComponent, double zComponent)
        {
            IVector3D vector3D = new Vector3DClass();
            vector3D.SetComponents(xComponent, yComponent, zComponent);
            return vector3D;
        }
        /// <summary>
        /// 根据角度计算弧度
        /// </summary>
        /// <param name="decimalDegrees"></param>
        /// <returns></returns>
        public static double GetRadians(double decimalDegrees)
        {
            return decimalDegrees * (Math.PI / 180);
        }
        /// <summary>
        /// 获得Z轴向量与真实世界中管线向量的夹角
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double GetTwoVectorsAngleInclination(IVector3D vec1, IVector3D vec2)
        {
            double vec3 = vec1.Inclination - vec2.Inclination;
            return vec3;
        }
        /// <summary>
        /// 启用Z
        /// </summary>
        /// <param name="geometry"></param>
        public static void MakeZAware(IGeometry geometry)
        {
            IZAware zAware = geometry as IZAware;
            zAware.ZAware = true;
        }
        /// <summary>
        /// 三维管点
        /// </summary>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="z">Z坐标</param>
        /// <param name="sr">参考空间</param>
        /// <returns></returns>
        public static IPoint ConstructPnt3D(double x, double y, double z, ISpatialReference sr=null)
        {
            IPoint point = ConstructPnt2D(x, y, sr);
            point.Z = z;
            MakeZAware(point as IGeometry);
            return point;
        }
        /// <summary>
        /// 二维管点
        /// </summary>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="sr">参考空间</param>
        /// <returns></returns>
        public static IPoint ConstructPnt2D(double x, double y, ISpatialReference sr = null)
        {
            IPoint point = new PointClass();
            point.X = x;
            point.Y = y;
            point.SpatialReference = sr;
            return point;
        }
        public static IPoint SplitAtDistance(IPoint FromPnt, IPoint ToPnt, double distance, bool beginfrom = false)
        {
            try
            {
                #region
                //double len = GetTwoPntsDistance3D(FromPnt, ToPnt);
                ////判断截取的长度是管线长度的一半以上
                //double ss = (len - distance)/2;
                //if (distance > ss)
                //{
                //    distance = len / 2;
                //}
                //if (!beginfrom)
                //{
                //    distance = len - distance;
                //}
                #endregion

                IPolyline polyline = new PolylineClass();
                polyline.FromPoint = FromPnt;
                polyline.ToPoint = ToPnt;
                IPolyline[] Lines = new IPolyline[2];
                bool isSplit;
                int splitIndex, segIndex;
                object o = Type.Missing;
                double len = polyline.Length;
                if (distance > len)
                {
                    distance = len / 2;
                }
                if (!beginfrom)
                {
                    distance = len - distance;
                }
                if (polyline.Length <= distance)
                {
                    string ss1 = "";
                }
                polyline.SplitAtDistance(distance, false, false, out isSplit, out splitIndex, out segIndex);
                IPolyline newLine = new PolylineClass();
                ISegmentCollection lineSegCol = (ISegmentCollection)polyline;
                ISegmentCollection newSegCol = (ISegmentCollection)newLine;
                for (int j = segIndex; j < lineSegCol.SegmentCount; j++)
                {
                    newSegCol.AddSegment(lineSegCol.get_Segment(j), ref o, ref o);
                }
                //重新构建两条线
                lineSegCol.RemoveSegments(segIndex, lineSegCol.SegmentCount - segIndex, true);
                lineSegCol.SegmentsChanged();
                newSegCol.SegmentsChanged();
                if (beginfrom)
                {
                    IPolyline oldLine = lineSegCol as IPolyline;
                    Lines[0] = oldLine;
                    return oldLine.ToPoint;
                }
                else
                {
                    newLine = newSegCol as IPolyline;
                    return newLine.FromPoint;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
        public static double GetLineLength(IFeature feature)
        {
            if (null != (feature.Shape as ILine))
            {
                return (feature.Shape as ILine).Length;
            }
            else if (null != (feature.Shape as IPolyline))
            {
                return (feature.Shape as IPolyline).Length;
            }
            return 0.0;
        }
        public static double GetLineAngle(IFeature feature)
        {
            if (null != (feature.Shape as ILine))
            {
                return (feature.Shape as ILine).Angle;
            }
            else if (null != (feature.Shape as IPolyline))
            {
                ILine ln = new LineClass();
                ln.PutCoords((feature.Shape as IPolyline).FromPoint, (feature.Shape as IPolyline).ToPoint);
                return ln.Angle;
            }
            return Math.PI / 2.0;
        }
        #endregion

        #region 私有方法
        private static object _missing = Type.Missing;
        public static IGeometry ExtrudeCircle2(double R, double Length, bool colse = false, ISpatialReference sr = null)
        {
            return ExtrudeCircle2(R, R / 10, Length, colse, sr);
        }
        /// <summary>
        /// 圆管管线
        /// </summary>
        /// <param name="outR">外半径</param>
        /// <param name="diameter">管壁厚度</param>
        /// <param name="Length">长度</param>
        /// <param name="sr">空间</param>
        /// <returns></returns>
        public static IGeometry ExtrudeCircle2(double outR, double diameter, double Length, bool colse = false, ISpatialReference sr = null)
        {
            double CircleDegrees = 360.0;
            int CircleDivisions = 28;
            double VectorComponentOffset = 0.0000001;
            //外 半径
            double OuterBuildingExteriorRingRadius = outR;
            //内 半径
            double OuterBuildingInteriorRingRadius = outR - diameter;
            //长度
            double BaseZ = 0.0;
            double OuterBuildingZ = Length;
            //Composite: Tall Building Protruding Through Outer Ring-Shaped Building
            IMultiPatch multiPatch = new MultiPatchClass();
            IGeometryCollection multiPatchGeometryCollection = multiPatch as IGeometryCollection;
            IPoint originPoint = ConstructPnt3D(0, 0, 0, sr);
            //上轴 3d矢量
            IVector3D upperAxisVector3D = ConstructVector3D(0, 0, 10);
            //下轴3d矢量
            IVector3D lowerAxisVector3D = ConstructVector3D(0, 0, -10);
            lowerAxisVector3D.XComponent += VectorComponentOffset;
            IVector3D normalVector3D = upperAxisVector3D.CrossProduct(lowerAxisVector3D) as IVector3D;
            double rotationAngleInRadians = GetRadians(CircleDegrees / CircleDivisions);
            //Outer Building
            IGeometry BaseGeometry = new PolygonClass();
            IGeometryCollection BaseGeometryCollection = BaseGeometry as IGeometryCollection;
            //外环
            IPointCollection ExteriorRingPointCollection = new RingClass();
            //内环
            IPointCollection InteriorRingPointCollection = new RingClass();

            for (int i = 0; i < CircleDivisions; i++)
            {
                normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D);
                normalVector3D.Magnitude = OuterBuildingExteriorRingRadius;
                IPoint outerBuildingBaseExteriorRingVertexPoint = ConstructPnt2D(originPoint.X + normalVector3D.XComponent,
                                                                                 originPoint.Y + normalVector3D.YComponent,
                                                                                 sr);
                ExteriorRingPointCollection.AddPoint(outerBuildingBaseExteriorRingVertexPoint, ref _missing, ref _missing);
                normalVector3D.Magnitude = OuterBuildingInteriorRingRadius;
                IPoint outerBuildingBaseInteriorRingVertexPoint = ConstructPnt2D(originPoint.X + normalVector3D.XComponent,
                                                                                 originPoint.Y + normalVector3D.YComponent,
                                                                                 sr);
                InteriorRingPointCollection.AddPoint(outerBuildingBaseInteriorRingVertexPoint, ref _missing, ref _missing);
            }
            IRing ExteriorRing = ExteriorRingPointCollection as IRing;
            ExteriorRing.Close();


            IRing InteriorRing = InteriorRingPointCollection as IRing;
            InteriorRing.Close();
            InteriorRing.ReverseOrientation();

            BaseGeometryCollection.AddGeometry(ExteriorRing as IGeometry, ref _missing, ref _missing);
            if (!colse)
                BaseGeometryCollection.AddGeometry(InteriorRing as IGeometry, ref _missing, ref _missing);

            ITopologicalOperator topologicalOperator = BaseGeometry as ITopologicalOperator;
            topologicalOperator.Simplify();


            IConstructMultiPatch ConstructMultiPatch = new MultiPatchClass();
            ConstructMultiPatch.ConstructExtrudeFromTo(BaseZ, OuterBuildingZ, BaseGeometry);

            IMultiPatch outerBuildingMultiPatch = ConstructMultiPatch as IMultiPatch;
            IGeometryCollection MultiPatchGeometryCollection = ConstructMultiPatch as IGeometryCollection;
            for (int i = 0; i < MultiPatchGeometryCollection.GeometryCount; i++)
            {
                IGeometry PatchGeometry = MultiPatchGeometryCollection.get_Geometry(i);
                multiPatchGeometryCollection.AddGeometry(PatchGeometry, ref _missing, ref _missing);
                if (PatchGeometry.GeometryType == esriGeometryType.esriGeometryRing)
                {
                    bool isBeginningRing = false;
                    esriMultiPatchRingType multiPatchRingType = outerBuildingMultiPatch.GetRingType(PatchGeometry as IRing, ref isBeginningRing);
                    multiPatch.PutRingType(PatchGeometry as IRing, multiPatchRingType);
                }
            }

            return multiPatchGeometryCollection as IGeometry;
        }
        /// <summary>
        /// 生成方管 带管壁
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="Length"></param>
        /// <param name="sr"></param>
        /// <returns></returns>
        private static IGeometry ExtrudeCube2(double Width, double Height, double Length, bool colse = false, ISpatialReference sr = null)
        {
            //长度
            double FromZ = 0;
            double ToZ = Length;
            //外高
            double oHeight = Height;
            //外宽
            double oWidth = Width;
            //内高
            double iHeight = Height - Height / 10;
            //内宽
            double iWidth = Width - Width / 10;
            IPolygon polygon = new PolygonClass();
            IGeometryCollection geometryCollection = polygon as IGeometryCollection;
            //外面
            IPointCollection exteriorRing1PointCollection = new RingClass();
            exteriorRing1PointCollection.AddPoint(ConstructPnt2D(-oHeight, oWidth, sr), ref _missing, ref _missing);
            exteriorRing1PointCollection.AddPoint(ConstructPnt2D(oHeight, oWidth, sr), ref _missing, ref _missing);
            exteriorRing1PointCollection.AddPoint(ConstructPnt2D(oHeight, -oWidth, sr), ref _missing, ref _missing);
            exteriorRing1PointCollection.AddPoint(ConstructPnt2D(-oHeight, -oWidth, sr), ref _missing, ref _missing);
            IRing exteriorRing1 = exteriorRing1PointCollection as IRing;
            exteriorRing1.Close();
            geometryCollection.AddGeometry(exteriorRing1 as IGeometry, ref _missing, ref _missing);
            //内面
            IPointCollection interiorRing1PointCollection = new RingClass();
            interiorRing1PointCollection.AddPoint(ConstructPnt2D(-iHeight, iWidth, sr), ref _missing, ref _missing);
            interiorRing1PointCollection.AddPoint(ConstructPnt2D(iHeight, iWidth, sr), ref _missing, ref _missing);
            interiorRing1PointCollection.AddPoint(ConstructPnt2D(iHeight, -iWidth, sr), ref _missing, ref _missing);
            interiorRing1PointCollection.AddPoint(ConstructPnt2D(-iHeight, -iWidth, sr), ref _missing, ref _missing);
            IRing interiorRing1 = interiorRing1PointCollection as IRing;
            interiorRing1.Close();
            if (!colse)
                geometryCollection.AddGeometry(interiorRing1 as IGeometry, ref _missing, ref _missing);
            IGeometry polygonGeometry = polygon as IGeometry;
            //相交
            ITopologicalOperator topologicalOperator = polygonGeometry as ITopologicalOperator;
            topologicalOperator.Simplify();

            IConstructMultiPatch constructMultiPatch = new MultiPatchClass();
            constructMultiPatch.ConstructExtrudeFromTo(FromZ, ToZ, polygonGeometry);
            return constructMultiPatch as IGeometry;
        }
        /// <summary>
        /// 生成球体，读取3D样式配置里面的球体符号
        /// </summary>
        /// <param name="originPoint"></param>
        /// <param name="R"></param>
        /// <param name="sType"></param>
        /// <param name="dic3DSymbol"></param>
        /// <returns></returns>
        public static IGeometry CreateSphere(IPoint originPoint, double R, string sType,
            Dictionary<string, IMarker3DSymbol> dic3DSymbol)
        {
            IGeometry geometry = null;
            if (dic3DSymbol.ContainsKey(sType))
            {
                double X = originPoint.X;
                double Y = originPoint.Y;
                double Z = originPoint.Z;
                IPoint Point1 = ConstructPnt3D(X, Y, Z - R / 2);

                IMarker3DSymbol p3DSymbol = dic3DSymbol[sType];
                IMarker3DPlacement pMarker3DPlacement = p3DSymbol as IMarker3DPlacement;
                pMarker3DPlacement.Size = R;
                //pMarker3DPlacement.Width = R;
                //pMarker3DPlacement.Depth = R;
                pMarker3DPlacement.Angle = 0;
                pMarker3DPlacement.ApplyToPoint(Point1, out geometry);
            }
            else
            {
                IMarkerSymbol pMarkerSymbol = new SimpleMarker3DSymbolClass();
                ((ISimpleMarker3DSymbol)pMarkerSymbol).Style = esriSimple3DMarkerStyle.esriS3DMSSphere;
                ((ISimpleMarker3DSymbol)pMarkerSymbol).ResolutionQuality = 1.0;
                pMarkerSymbol.Size = R;
                //pMarkerSymbol.Color = pColor;

                ((IMarker3DPlacement)pMarkerSymbol).ApplyToPoint(originPoint, out geometry);
            }
            return geometry;
        }
        /// <summary>
        /// 根据符号 生成球体
        /// </summary>
        /// <param name="originPoint"></param>
        /// <param name="R"></param>
        /// <param name="Divisions"></param>
        /// <returns></returns>
        public static IGeometry ExtrudeSector1(IPoint CenterPnt, double R, double H, ISpatialReference sr = null)
        {
            double CircleDegrees = 360.0;
            int CircleDivisions = 28;
            double VectorComponentOffset = 0.0000001;
            double InnerBuildingRadius = R;
            double BaseZ = CenterPnt.Z - H;
            double InnerBuildingZ = CenterPnt.Z + H - H / 100;
            IMultiPatch multiPatch = new MultiPatchClass();
            IGeometryCollection multiPatchGeometryCollection = multiPatch as IGeometryCollection;
            IVector3D upperAxisVector3D = ConstructVector3D(0, 0, 10);
            IVector3D lowerAxisVector3D = ConstructVector3D(0, 0, -10);
            lowerAxisVector3D.XComponent += VectorComponentOffset;
            IVector3D normalVector3D = upperAxisVector3D.CrossProduct(lowerAxisVector3D) as IVector3D;
            double rotationAngleInRadians = GetRadians(CircleDegrees / CircleDivisions);
            IGeometry innerBuildingBaseGeometry = new PolygonClass();
            IPointCollection innerBuildingBasePointCollection = innerBuildingBaseGeometry as IPointCollection;
            for (int i = 0; i < CircleDivisions; i++)
            {
                normalVector3D.Rotate(-1 * rotationAngleInRadians, upperAxisVector3D);
                //Inner Building
                normalVector3D.Magnitude = InnerBuildingRadius;

                IPoint innerBuildingBaseVertexPoint = ConstructPnt3D(CenterPnt.X + normalVector3D.XComponent,
                                                                             CenterPnt.Y + normalVector3D.YComponent,
                                                                             CenterPnt.Z + normalVector3D.ZComponent,
                                                                             sr);
                innerBuildingBasePointCollection.AddPoint(innerBuildingBaseVertexPoint, ref _missing, ref _missing);
            }
            IPolygon innerBuildingBasePolygon = innerBuildingBaseGeometry as IPolygon;
            innerBuildingBasePolygon.Close();
            IConstructMultiPatch innerBuildingConstructMultiPatch = new MultiPatchClass();
            innerBuildingConstructMultiPatch.ConstructExtrudeFromTo(BaseZ, InnerBuildingZ, innerBuildingBaseGeometry);
            IGeometryCollection innerBuildingMultiPatchGeometryCollection = innerBuildingConstructMultiPatch as IGeometryCollection;
            for (int i = 0; i < innerBuildingMultiPatchGeometryCollection.GeometryCount; i++)
            {
                multiPatchGeometryCollection.AddGeometry(innerBuildingMultiPatchGeometryCollection.get_Geometry(i), ref _missing, ref _missing);
            }
            return multiPatchGeometryCollection as IGeometry;
        }
        /// <summary>
        /// 合并两个图形
        /// </summary>
        /// <param name="GeometryA"></param>
        /// <param name="GeometryB"></param>
        /// <returns></returns>
        private static IGeometry UnionTwoGeometries(IGeometry GeometryA, IGeometry GeometryB)
        {
            ITopologicalOperator pTopologicalOperator = GeometryA as ITopologicalOperator;
            if (GeometryB == null)
            {
                return null;
            }
            IGeometry UnionGeometry = pTopologicalOperator.Union(GeometryB);
            return UnionGeometry;
        }
        /// <summary>
        /// 绘制圆柱体
        /// </summary>
        /// <returns></returns>
        public static IGeometry DrawCylinder(IVector3D VectorPipe, double R, double ToZ)
        {
            const double CircleDegrees = 360.0;
            const int CircleDivisions = 30;
            const double VectorComponentOffset = 0.0000001;
            double InnerBuildingRadius = R;
            const double BaseZ = 0.0;
            double InnerBuildingZ = ToZ;

            IMultiPatch multiPatch = new MultiPatchClass();//多面体

            IGeometryCollection multiPatchGeometryCollection = multiPatch as IGeometryCollection;
            IPoint originPoint = ConstructPnt3D(0, 0, 0);

            IVector3D upperAxisVector3D = ConstructVector3D(0, 0, 10);

            IVector3D lowerAxisVector3D = ConstructVector3D(0, 0, -10);

            lowerAxisVector3D.XComponent += VectorComponentOffset;

            IVector3D normalVector3D = new Vector3DClass();
            normalVector3D = upperAxisVector3D.CrossProduct(lowerAxisVector3D) as IVector3D;//向量叉积

            double rotationAngleInRadians = GetRadians(CircleDegrees / CircleDivisions);

            IGeometry innerBuildingBaseGeometry = new PolygonClass();

            IPointCollection innerBuildingBasePointCollection = innerBuildingBaseGeometry as IPointCollection;
            for (int i = 0; i < CircleDivisions; i++)
            {
                normalVector3D.Rotate(rotationAngleInRadians, VectorPipe);
                //Inner Building
                //normalVector3D.Magnitude = InnerBuildingRadius;
                normalVector3D.Magnitude = InnerBuildingRadius;
                IPoint innerBuildingBaseVertexPoint = ConstructPnt2D(originPoint.X + normalVector3D.XComponent,
                originPoint.Y + normalVector3D.YComponent);
                innerBuildingBasePointCollection.AddPoint(innerBuildingBaseVertexPoint, ref _missing, ref _missing);
            }
            //IPolygon innerBuildingBasePolygon = innerBuildingBaseGeometry as IPolygon;
            //innerBuildingBasePolygon.Close();
            IConstructMultiPatch innerBuildingConstructMultiPatch = new MultiPatchClass();
            //innerBuildingConstructMultiPatch.ConstructExtrudeFromTo(BaseZ, InnerBuildingZ, innerBuildingBaseGeometry);
            innerBuildingConstructMultiPatch.ConstructExtrudeFromTo(BaseZ, ToZ, innerBuildingBaseGeometry);
            IGeometryCollection innerBuildingMultiPatchGeometryCollection = innerBuildingConstructMultiPatch as IGeometryCollection;
            for (int i = 0; i < innerBuildingMultiPatchGeometryCollection.GeometryCount; i++)
            {
                multiPatchGeometryCollection.AddGeometry(innerBuildingMultiPatchGeometryCollection.get_Geometry(i), ref _missing, ref _missing);
            }
            return multiPatchGeometryCollection as IGeometry;

        }
        #endregion

    }
}
