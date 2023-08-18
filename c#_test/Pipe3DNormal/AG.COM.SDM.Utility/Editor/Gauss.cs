using System;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.Utility.Editor
{
    /// <summary>
    /// 西安80坐标系下的高斯投影计算
    /// </summary>
    public class Gauss
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="centerLongitude">中央经线</param>
        public Gauss(double centerLongitude)
        {
            centerL = centerLongitude;
            initParam();
        }

        //80椭球参数
        private const double PI = 3.14159265358979; //圆周率值
        private const double RHO = 206264.8062471;//
        private const double ZERO = 0.000000000001;//
        private const double aRadius = 6378140;//椭球长半轴
        private const double bRadius = 6356755.29;//椭球短半轴
        private const double ParaAF = 1 / 298.257;//椭球扁率
        private const double ParaE1 = 6.69438499958795E-03;//椭球第一偏心率
        private const double ParaE2 = 6.73950181947292E-03;//椭球第2偏心率
        private const double StandartLat = 0;
        private double ParaC = 6399596.65198801;//极点子午圈曲率半径
        //相关常数:
        private const double Parak0 = 1.57048687472752E-07;
        private const double Parak1 = 5.05250559291393E-03;
        private const double Parak2 = 2.98473350966158E-05;
        private const double Parak3 = 2.41627215981336E-07;
        private const double Parak4 = 2.22241909461273E-09;
        //参数变量
        private double e = 0;
        //private double a = 0;
        private double ParamA = 0;
        private double ParamB = 0;
        private double ParamC = 0;
        private double ParamD = 0;
        private double ParamE = 0;
        private double centerL = 120; //中央经线:

        //初始化面积计算参数
        private void initParam()
        {
            e = ParaE2;
            ParaC = aRadius / (1 - ParaAF);

            ParamA = 1 + (3 / 6) * e + (30 / 80) * System.Math.Pow(e, 2) + (35 / 112) * System.Math.Pow(e, 3) + (630 / 2304) * System.Math.Pow(e, 4);
            ParamB = (1 / 6) * e + (15 / 80) * System.Math.Pow(e, 2) + (21 / 112) * System.Math.Pow(e, 3) + (420 / 2304) * System.Math.Pow(e, 4);
            ParamC = (3 / 80) * System.Math.Pow(e, 2) + (7 / 112) * System.Math.Pow(e, 3) + (180 / 2304) * System.Math.Pow(e, 4);
            ParamD = (1 / 112) * System.Math.Pow(e, 3) + (45 / 2304) * System.Math.Pow(e, 4);
            ParamE = (5 / 2304) * System.Math.Pow(e, 4);

        }

        //度转换为弧度
        private double TransDegreeToArc(double degree)
        {
            return degree / 180 * PI;
        }

        //弧度转换为度
        private double TransArcToDegree(double arc)
        {
            double ret = arc * 180 / PI;
            double degree = FormatValue(ret, 100, 100);
            double tmp = (ret - degree) * 60;
            double min = FormatValue(tmp, 100, 100);
            double sec = (tmp - min) * 60;
            sec = System.Math.Round(sec, 6);

            return degree * 3600 + min * 60 + sec;
        }

        //取整
        private double FormatValue(double inputVal, long precsion, long scaleNum)
        {
            return (Convert.ToInt32(inputVal * precsion) - Convert.ToInt32(inputVal * precsion) % scaleNum) / precsion;
        }

        /// <summary>
        /// 高斯坐标反解计算经纬度值 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="center">中央经线</param>
        /// <returns></returns>
        public IPoint ComputeXYGeo(double x, double y)
        {
            double y1 = y - 500000;
            double e = Parak0 * x;
            double se = System.Math.Sin(e);
            double bf = e + System.Math.Cos(e) * (Parak1 * se - Parak2 * System.Math.Pow(se, 3) + Parak3 * System.Math.Pow(se, 5) - Parak4 * System.Math.Pow(se, 7));

            double t = System.Math.Tan(bf);
            double nl = ParaE1 * System.Math.Pow(System.Math.Cos(bf), 2);
            double v = System.Math.Sqrt(1 + nl);
            double N = ParaC / v;
            double yn = y1 / N;
            double vt = System.Math.Pow(v, 2) * t;
            double t2 = System.Math.Pow(t, 2);
            double B = bf - vt * System.Math.Pow(yn, 2) / 2 + (5 + 3 * t2 + nl - 9 * nl * t2) * vt * System.Math.Pow(yn, 4) / 24 - (61 + 90 * t2 + 45 * System.Math.Pow(t2, 2)) * vt * System.Math.Pow(yn, 6) / 720;

            B = TransArcToDegree(B);

            double cbf = 1 / System.Math.Cos(bf);
            double L = cbf * yn - (1 + 2 * t2 + nl) * cbf * System.Math.Pow(yn, 3) / 6 + (5 + 28 * t2 + 24 * System.Math.Pow(t2, 2) + 6 * nl + 8 * nl * t2) * cbf * System.Math.Pow(yn, 5) / 120 + centerL;

            L = TransArcToDegree(L);

            IPoint point = new ESRI.ArcGIS.Geometry.Point();
            point.X = B;
            point.Y = L;

            return point;
        }

        /// <summary>
        /// 高斯坐标反解计算经纬度值 返回值单位为弧度
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public IPoint ComputeXYGeo_Arc(double x, double y)
        {
            IPoint pt = ComputeXYGeo(x, y);
            pt.Y = pt.Y / RHO;
            pt.X = pt.X / RHO;
            return pt;
        }


        /// <summary>
        /// 计算多边形椭球面面积
        /// </summary>
        /// <param name="calcShape">要计算的多边形</param>
        /// <returns></returns>
        public double ComputeArea(IPolygon4 poly)
        {
            //面积初始化
            double areaSum = 0;
            double areaExt = 0;
            double areaInt = 0;

            //处理内岛和飞地                
            IGeometryBag exteriorRings = poly.ExteriorRingBag;
            int exteriorRingCount = poly.ExteriorRingCount;
            IEnumGeometry exteriorRingsEnum = exteriorRings as IEnumGeometry;
            exteriorRingsEnum.Reset();

            IRing currentExteriorRing = exteriorRingsEnum.Next() as IRing;
            while (currentExteriorRing != null)
            {
                IPointCollection extRingPointColl = currentExteriorRing as IPointCollection;
                //计算外围最大多边形面积
                areaExt = areaExt + CalcAreaByPointCol(extRingPointColl);

                IGeometryBag interiorRings = poly.get_InteriorRingBag(currentExteriorRing);

                IEnumGeometry interiorRingsEnum = interiorRings as IEnumGeometry;
                interiorRingsEnum.Reset();

                IRing currentinteriorRing = interiorRingsEnum.Next() as IRing;
                while (currentinteriorRing != null)
                {
                    //currentinteriorRing.
                    IPointCollection inRingPointColl = currentinteriorRing as IPointCollection;
                    //计算内岛多边形面积
                    areaInt = areaInt + CalcAreaByPointCol(inRingPointColl);
                    currentinteriorRing = interiorRingsEnum.Next() as IRing;
                }
                currentExteriorRing = exteriorRingsEnum.Next() as IRing;
            }

            areaSum = areaExt - areaInt;
            return areaSum;
        }

        //由点串计算多边形面积
        private double CalcAreaByPointCol(IPointCollection pointCollection)
        {
            double areaSum = 0;

            for (int i = 0; i < pointCollection.PointCount - 1; i++)
            {
                IPoint point1 = pointCollection.get_Point(i);
                IPoint pointGeo1 = ComputeXYGeo(point1.Y, point1.X);

                IPoint point2 = pointCollection.get_Point(i + 1);
                IPoint pointGeo2 = ComputeXYGeo(point2.Y, point2.X);

                //将经纬度转换为弧度值
                double B = pointGeo1.X / RHO;
                double L = pointGeo1.Y / RHO;
                double B1 = pointGeo2.X / RHO;
                double L1 = pointGeo2.Y / RHO;

                //计算梯形面积
                double bDiference = (B1 - B);
                double bSum = (B1 + B) / 2;
                double lDiference = (L1 + L) / 2;
                double AreaVal = 0;

                double ItemValue0 = ParamA * System.Math.Sin(bDiference / 2) * System.Math.Cos(bSum);
                double ItemValue1 = ParamB * System.Math.Sin(3 * bDiference / 2) * System.Math.Cos(3 * bSum);
                double ItemValue2 = ParamC * System.Math.Sin(5 * bDiference / 2) * System.Math.Cos(5 * bSum);
                double ItemValue3 = ParamD * System.Math.Sin(7 * bDiference / 2) * System.Math.Cos(7 * bSum);
                double ItemValue4 = ParamE * System.Math.Sin(9 * bDiference / 2) * System.Math.Cos(9 * bSum);
                AreaVal = 2 * bRadius * lDiference * bRadius * (ItemValue0 - ItemValue1 + ItemValue2 - ItemValue3 + ItemValue4);

                //计算总面积
                areaSum = areaSum + AreaVal;
            }

            return System.Math.Abs(areaSum);
        }

        /// <summary>
        /// 高斯坐标正解计算地理坐标
        /// </summary>
        /// <param name="longitude">单位：度</param>
        /// <param name="latitude">单位：度</param>
        /// <param name="center">中央经线：度</param>
        /// <returns></returns>
        public IPoint ComputeGeoXY(double longitude, double latitude)
        {
            double longitude1, latitude1, longitude0, X0, Y0, X, Y, xval, yval;
            double e2, ee, NN, T, C, A, M;
            X0 = 0;
            Y0 = 0;
            longitude0 = TransDegreeToArc(centerL);//中央经线转为弧度
            longitude1 = TransDegreeToArc(longitude); //经度转换为弧度
            latitude1 = TransDegreeToArc(latitude); //纬度转换为弧度
            e2 = 2 * ParaAF - ParaAF * ParaAF;
            ee = e2 * (1.0 - e2);
            NN = aRadius / Math.Sqrt(1.0 - e2 * Math.Sin(latitude1) * Math.Sin(latitude1));
            T = Math.Tan(latitude1) * Math.Tan(latitude1);
            C = ee * Math.Cos(latitude1) * Math.Cos(latitude1);
            A = (longitude1 - longitude0) * Math.Cos(latitude1);
            M = aRadius * ((1 - e2 / 4 - 3 * e2 * e2 / 64 - 5 * e2 * e2 * e2 / 256) * latitude1 - (3 * e2 / 8 + 3 * e2 * e2 / 32 + 45 * e2 * e2 * e2 / 1024) * Math.Sin(2 * latitude1) + (15 * e2 * e2 / 256 + 45 * e2 * e2 * e2 / 1024) * Math.Sin(4 * latitude1) - (35 * e2 * e2 * e2 / 3072) * Math.Sin(6 * latitude1));
            xval = NN * (A + (1 - T + C) * A * A * A / 6 + (5 - 18 * T + T * T + 72 * C - 58 * ee) * A * A * A * A * A / 120);
            yval = M + NN * Math.Tan(latitude1) * (A * A / 2 + (5 - T + 9 * C + 4 * C * C) * A * A * A * A / 24 + (61 - 58 * T + T * T + 600 * C - 330 * ee) * A * A * A * A * A * A / 720);
            X0 = X0 + 500000L;
            X = Math.Round((xval + X0) * 1000000) / 1000000.0;
            Y = Math.Round((yval + Y0) * 1000000) / 1000000.0;
            IPoint point = new ESRI.ArcGIS.Geometry.Point();
            point.X = X;
            point.Y = Y;
            return point;
        } 

    }
}
