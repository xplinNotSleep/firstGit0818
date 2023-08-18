using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 高斯投影正反算公式
    /// </summary>
    public class GaussTransform
    {
        private double m_a = 6378140.0;                 //长半轴长
        private double m_f = 1 / 298.25722101;          //扁率
        private int m_zoneWide = 3;                       //带宽    

        public bool BL2XY_New(double B, double L, out double X, out double Y)
        {
            double ProjNo = (int)(L / this.m_zoneWide) + 1;                             //带号
            double centerL0 = ProjNo * this.m_zoneWide - this.m_zoneWide / 2;           //中央经线

            //double dL = L - centerL0;

            double e = Math.Sqrt(2 * this.m_f - Math.Pow(this.m_f, 2));                //第一偏心率
            double e2 = e / Math.Sqrt(1 - e * e);                                      //第二偏心率

            B = AngleTransform.Degree2Radian(B, true);
            L = AngleTransform.Degree2Radian(L, true);

            double n2 = Math.Pow(e2, 2) * Math.Pow(Math.Cos(B), 2);
            double t = Math.Tan(B);

            double W = Math.Sqrt(1 - Math.Pow(e * Math.Sin(B), 2));
            double M = this.m_a * (1 - Math.Pow(e, 2)) / Math.Pow(W, 3);                    //子午圈曲率半径
            double N = this.m_a / W;                                                        //卯酉圈曲率半径      

            double dL = L - AngleTransform.Degree2Radian(centerL0, false);                  //经度差(弧度值)
            double l2 = Math.Pow(dL, 2);                                                    //经差平方值           

            //计算子午弧长
            double Rx = this.m_a * (1 - Math.Pow(e, 2)) * (1.00505250559297 * B - 0.00253155620900066 * Math.Sin(2 * B) + 2.65690155540381E-06 * Math.Sin(4 * B) - 3.47007559905787E-09 * Math.Sin(6 * B) + 4.91654216666515E-12 * Math.Sin(8 * B) - 7.26313725279022E-15 * Math.Sin(10 * B) + 1.07400991193683E-17 * Math.Sin(12 * B));

            Rx = Rx = this.m_a * (1 - Math.Pow(e, 2)) * (1.00505250559297 * B - 0.00253155620900066 * Math.Sin(2 * B) + 2.65690155540381E-06 * Math.Sin(4 * B));

            X = Rx + N * t * Math.Pow(Math.Cos(B), 2) * l2 * (0.5 + (5 - t * t + 9 * n2 + 4 * Math.Pow(n2, 2)) * Math.Pow(Math.Cos(B), 2) * l2 / 24 + (61 - 58 * t * t + Math.Pow(t, 4)) / 720 * Math.Pow(Math.Cos(B), 4) * Math.Pow(l2, 2));

            X = Rx + N * Math.Sin(B) * Math.Cos(B) * l2 * (0.5 + (5 - t * t + 9 * n2 + 4 * Math.Pow(n2, 2)) * Math.Pow(Math.Cos(B), 2) * l2 / 24 + (61 - 58 * t * t + Math.Pow(t, 4)) / 720 * Math.Pow(Math.Cos(B), 4) * Math.Pow(l2, 2));

            Y = N * Math.Cos(B) * dL * (1 + (1 - t * t + n2) * Math.Pow(Math.Cos(B), 2) * l2 / 6.0 + (5 - 18 * t * t + Math.Pow(t, 4) + 14 * n2 - 58 * n2 * t * t) * Math.Pow(Math.Cos(B), 4) * Math.Pow(l2, 2) / 120.0);

            Y = Y + 500000 + ProjNo * 1000000;

            return true;
        }

        /// <summary>
        /// 平面坐标转大地坐标 
        /// 注意：返回的经纬度格式为百分制形式
        /// </summary>
        /// <param name="X">平面X坐标(纬度)</param>
        /// <param name="Y">平面Y坐标(经度)</param>
        /// <param name="B">大地纬度坐标</param>
        /// <param name="L">大地经度坐标</param>
        /// <param name="L0">中央经度</param>
        /// <param name="SphType">坐标系</param>
        public static void XY2BL(double X, double Y, out double B, out double L, int L0, SpheroidType SphType)
        {
            CoordParams coordParams = GetCoordParams(SphType);                      //获取坐标参数            
            coordParams.CalcRLParams();                                             //计算子午线弧长

            double a = coordParams.a;                                               //长半轴
            double f = coordParams.f;                                               //短半轴
            double e = coordParams.e1;                                              //第一偏心率
            double e2 = coordParams.e2;                                             //第二偏心率  

            //double A1 = a * (1 - Math.Pow(e, 2)) * 1.00505250559297 * Math.PI / 180;                    //乘弧长参数1
            //double A2 = a * (1 - Math.Pow(e, 2)) * 0.00253155620900066;                                 //乘弧长参数2
            //double A3 = a * (1 - Math.Pow(e, 2)) * 2.65690155540381E-06;                                //乘弧长参数3
            //double A4 = a * (1 - Math.Pow(e, 2)) * 3.47007559905787E-09;                                //乘弧长参数4                    

            double A1 = a * (1 - Math.Pow(e, 2)) * coordParams.r1 * Math.PI / 180;                      //乘弧长参数1
            double A2 = a * (1 - Math.Pow(e, 2)) * coordParams.r2;                                      //乘弧长参数2
            double A3 = a * (1 - Math.Pow(e, 2)) * coordParams.r3;                                      //乘弧长参数3
            double A4 = a * (1 - Math.Pow(e, 2)) * coordParams.r4;                                      //乘弧长参数4                    

            double B0 = X / A1;
            double preB0 = 0;

            /**************************************
             * 底点纬度迭代算法
             * 求B
             * **************************************/
            do
            {
                preB0 = B0;
                B0 = B0 * Math.PI / 180;
                B0 = (X - (-A2 * Math.Sin(2 * B0) + A3 * Math.Sin(4 * B0) - A4 * Math.Sin(6 * B0))) / A1;
                if (Math.Abs(B0 - preB0) < 0.0000001) break;

            } while (true);

            B0 = B0 * Math.PI / 180;

            double n2 = Math.Pow(e2, 2) * Math.Pow(Math.Cos(B0), 2);
            double t = Math.Tan(B0);

            double W = Math.Sqrt(1 - Math.Pow(e * Math.Sin(B0), 2));                  //第一辅助参数
            double M = a * (1 - Math.Pow(e, 2)) / Math.Pow(W, 3);                     //子午圈曲率半径
            double N = a / W;                                                         //卯酉圈曲率半径  

            Y = Y % 1000000 - 500000;                                                //减去带号--减去偏移值
            double ly = Y / N;

            double centerL0 = AngleTransform.Degree2Radian(L0, false);                //度转换弧度

            double tempL = centerL0 + (ly - (1 + 2 * t * t + n2) * Math.Pow(ly, 3) / 6 + (5 + 28 * t * t + 24 * Math.Pow(t, 4) + 6 * n2 + 8 * n2 * Math.Pow(t, 2)) * Math.Pow(ly, 5) / 120) / Math.Cos(B0);
            //以百分制形式返回经度
            L = Math.Round(AngleTransform.Radian2Degree(tempL, false), 8);

            double tempB = B0 - t / (2 * M) * Y * ly * (1 - (5 + 3 * t * t + n2 - 9 * n2 * Math.Pow(t, 2)) * Math.Pow(ly, 2) / 12 + (61 + 90 * t * t + 45 * Math.Pow(t, 4)) * Math.Pow(ly, 4) / 360);
            //以百分制形式返回纬度
            B = Math.Round(AngleTransform.Radian2Degree(tempB, false), 8);


        }

        /// <summary>
        /// 大地坐标转换为平面坐标
        /// 注意：传递的经纬度为百分制形式
        /// </summary>
        /// <param name="latValue">大地坐标纬度值（非度分秒格式）</param>
        /// <param name="lontValue">大地坐标经度值（非度分秒格式）</param>
        /// <param name="X">返回平面X坐标</param>
        /// <param name="Y">返回平面Y坐标</param>
        /// <param name="ZoneSize">分带（三度或六度）</param>
        /// <param name="SpheroidType">坐标系</param>       
        public static void BL2XY(double latValue, double longValue, out double X, out double Y, ProjectZoneSize ZoneSize, SpheroidType SphType)
        {
            CoordParams coordParams = GetCoordParams(SphType);                      //获取坐标参数
            coordParams.CalcRLParams();                                             //计算子午线弧长

            //几何参数 
            double a = coordParams.a;                                               //长半轴
            double f = coordParams.f;                                               //短半轴
            double e = coordParams.e1;                                              //第一偏心率
            double e2 = coordParams.e2;                                             //第二偏心率

            int ProjNo = GetZone(longValue, ZoneSize);                              //带号           
            int centerL0 = CenterLongitude(longValue, ZoneSize);                    //中央经线

            double B = AngleTransform.Degree2Radian(latValue, false);
            double L = AngleTransform.Degree2Radian(longValue, false);

            double n2 = Math.Pow(e2, 2) * Math.Pow(Math.Cos(B), 2);
            double t = Math.Tan(B);

            double W = Math.Sqrt(1 - Math.Pow(e * Math.Sin(B), 2));                  //第一辅助参数
            double M = a * (1 - Math.Pow(e, 2)) / Math.Pow(W, 3);                    //子午圈曲率半径
            double N = a / W;                                                        //卯酉圈曲率半径      

            double dL = L - AngleTransform.Degree2Radian(centerL0, false);           //经度差(弧度值)
            double l2 = Math.Cos(B) * dL;                                            //经差平方值   

            //计算子午弧长
            //double Rx = a * (1 - Math.Pow(e, 2)) * (1.00505250559297 * B - 0.00253155620900066 * Math.Sin(2 * B) + 2.65690155540381E-06 * Math.Sin(4 * B) - 3.47007559905787E-09 * Math.Sin(6 * B) + 4.91654216666515E-12 * Math.Sin(8 * B) - 7.26313725279022E-15 * Math.Sin(10 * B) + 1.07400991193683E-17 * Math.Sin(12 * B));
            double Rx = a * (1 - Math.Pow(e, 2)) * (coordParams.r1 * B - coordParams.r2 * Math.Sin(2 * B) + coordParams.r3 * Math.Sin(4 * B) - coordParams.r4 * Math.Sin(6 * B) + coordParams.r5 * Math.Sin(8 * B) - coordParams.r6 * Math.Sin(10 * B) + coordParams.r7 * Math.Sin(12 * B));

            X = Rx + N * t * Math.Pow(l2, 2) * (0.5 + (5 - t * t + 9 * n2 + 4 * Math.Pow(n2, 2)) * Math.Pow(l2, 2) / 24 + (61 - 58 * t * t + Math.Pow(t, 4)) / 720 * Math.Pow(l2, 4));

            Y = N * l2 * (1 + (1 - t * t + n2) * Math.Pow(l2, 2) / 6.0 + (5 - 18 * t * t + Math.Pow(t, 4) + 14 * n2 - 58 * n2 * t * t) * Math.Pow(l2, 4) / 120.0);


            Y = Y + 500000 + ProjNo * 1000000;               //+++带号+++偏移量
        }

        /// <summary>
        /// 取得中央经线的位置
        /// </summary>
        /// <param name="SrcLongValue">所在经线</param>
        /// <param name="ZoneSize">几度分带</param>
        /// <returns>返回所在中央经线</returns>
        public static int CenterLongitude(double SrcLongValue, ProjectZoneSize ZoneSize)
        {
            //获取带号
            int zoneID = GetZone(SrcLongValue, ZoneSize);

            //获取中央经线
            int centerLo = CenterLongitude(zoneID, ZoneSize);

            return centerLo;
        }

        /// <summary>
        /// 获取中央经线
        /// </summary>
        /// <param name="ZoneID">带号</param>
        /// <param name="ZoneSize">分带类型</param>
        /// <returns>返回中央经线</returns>
        public static int CenterLongitude(int ZoneID, ProjectZoneSize ZoneSize)
        {
            int tempNum = 0;

            if (ZoneSize == ProjectZoneSize.size6)
            {
                tempNum = ZoneID * 6 - 3;
            }
            else
            {
                tempNum = ZoneID * 3;
                if (tempNum == 360) tempNum = 0;
            }

            return tempNum;
        }

        /// <summary>
        /// 取得所在带号
        /// </summary>
        /// <param name="SrcLontValue">所在经线</param>
        /// <param name="ZoneSize">几度分带</param>
        /// <returns>返回带号</returns>
        private static int GetZone(double SrcLongValue, ProjectZoneSize ZoneSize)
        {
            int tmpNum;
            if (ZoneSize == ProjectZoneSize.size3)
            {
                //按3度分带取得带号
                double tempLontValue = (SrcLongValue - 1.5 + 360) % 360;
                tmpNum = (int)(tempLontValue / 3);

                if (Math.Round(tempLontValue % 3, 6) > 0)
                {
                    tmpNum = tmpNum + 1;
                }
            }
            else
            {
                //按6度分带取得带号
                tmpNum = (int)(SrcLongValue / 6);
                if (Math.Round(SrcLongValue % 6, 6) > 0)
                {
                    tmpNum = tmpNum + 1;
                }
            }

            return tmpNum;
        }

        /// <summary>
        /// 根据坐标系统获取坐标参数
        /// </summary>
        /// <param name="sphType">坐标系统</param>
        /// <returns>返回坐标参数</returns>
        private static CoordParams GetCoordParams(SpheroidType sphType)
        {
            CoordParams tCoordParams = new CoordParams();
            if (sphType == SpheroidType.spheroid80)
            {
                tCoordParams.a = 6378140.0;
                tCoordParams.f = 1 / 298.25722101;
            }
            else if (sphType == SpheroidType.spheroid54)
            {
                tCoordParams.a = 6378245.0;
                tCoordParams.f = 1 / 298.3;
            }

            return tCoordParams;
        }

        /* 另一种算法 
         * 
        public static void BL2XY(double longitude, double latitude, out double X, out double Y)
        {
            int ProjNo = 0;     //带号
            int ZoneWide = 6;       //带宽

            double longitude1, latitude1, longitude0, latitude0, X0, Y0;
            double a, f, e2, ee, NN, T, C, A, M, iPI;

            iPI = Math.PI / 180;

            a = 6378140.0;//长半轴
            f = 1 / 298.257;

            ProjNo = (int)(longitude / ZoneWide);

            longitude0 = ProjNo * ZoneWide + ZoneWide / 2;      //中央经线
            longitude0 = longitude0 * iPI;

            longitude1 = longitude * iPI;                   //经度转换为弧度
            latitude1 = latitude * iPI;                     //纬度转换为弧度

            e2 = 2 * f - f * f;
            ee = e2 * (1.0 - e2);

            NN = a / Math.Sqrt(1.0 - e2 * Math.Sin(latitude1) * Math.Sin(latitude1));
            T = Math.Tan(latitude1) * Math.Tan(latitude1);

            C = ee * Math.Cos(latitude1) * Math.Cos(latitude1);
            A = (longitude1 - longitude0) * Math.Cos(latitude1);

            M = a * ((1 - e2 / 4 - 3 * e2 * e2 / 64 - 5 * e2 * e2 * e2 / 256) * latitude1 - (3 * e2 / 8 + 3 * e2 * e2 / 32 + 45 * e2 * e2 * e2 / 1024) * Math.Sin(2 * latitude1) + (15 * e2 * e2 / 256 + 45 * e2 * e2 * e2 / 1024) * Math.Sin(4 * latitude1) - (35 * e2 * e2 * e2 / 3072) * Math.Sin(6 * latitude1));

            X = NN * (A + (1 - T + C) * A * A * A / 6 + (5 - 18 * T + T * T + 72 * C - 58 * ee) * A * A * A * A * A / 120);
            Y = M + NN * Math.Tan(latitude1) * (A * A / 2 + (5 - T + 9 * C + 4 * C * C) * A * A * A * A / 24 + (61 - 58 * T + T * T + 600 * C - 330 * ee) * A * A * A * A * A * A / 720);

            X = X + (ProjNo + 1) * 1000000 + 500000;
        }        
         * */
    }
}
