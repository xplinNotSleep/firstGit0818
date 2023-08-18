using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 大地坐标转空间直角坐标计算
    /// </summary>
    public class CoordTransform
    {
        private double m_a;     //长半轴
        private double m_f;     //扁率

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="a">长半轴长</param>
        /// <param name="f">扁率</param>
        public CoordTransform(double a, double f)
        {
            this.m_a = a;
            this.m_f = f;
        }

        /// <summary>
        /// 大地坐标转空间直角坐标
        /// </summary>
        /// <param name="B">大地纬度B</param>
        /// <param name="L">大地经度L</param>
        /// <param name="H">大地高H</param>
        /// <param name="X">平面X坐标</param>
        /// <param name="Y">平面Y坐标</param>
        /// <param name="Z">平面Z坐标</param>
        public void BLH2XYZ(double B, double L, double H, out double X, out double Y, out double Z)
        {
            //将角度转换成弧度
            double degB = AngleTransform.Degree2Radian(B, true);
            double degL = AngleTransform.Degree2Radian(L, true);

            //第一偏心率
            double e = Math.Sqrt(2 * this.m_f - 1) / this.m_f;

            //获取第一辅助系数
            double W = Math.Sqrt(1 - e * e * Math.Sin(degB) * Math.Sin(degB));

            //卯酉圈曲率半径
            double N = this.m_a / W;

            X = (N + H) * Math.Cos(degB) * Math.Cos(degL);
            Y = (N + H) * Math.Cos(degB) * Math.Sin(degL);
            Z = (N * (1 - e * e) + H) * Math.Sin(degB);
        }

        /// <summary>
        /// 空间直角坐标转经纬度坐标
        /// </summary>
        /// <param name="X">平面X坐标</param>
        /// <param name="Y">平面Y坐标</param>
        /// <param name="Z">平面Z坐标</param>
        /// <param name="B">大地纬度坐标</param>
        /// <param name="L">大地经度坐标</param>
        /// <param name="H">大地高H</param>
        public void XYZ2BLH(double X, double Y, double Z, out double B, out double L, out double H)
        {
            //第一偏心率
            double e = Math.Sqrt(2 * this.m_f - 1) / this.m_f;

            //第二偏心率
            double e2 = (Math.Pow(this.m_a, 2) - Math.Pow(this.m_a - this.m_a / this.m_f, 2)) / Math.Pow(this.m_a - this.m_a / this.m_f, 2);

            double Q = Math.Atan(Z * this.m_a / (this.m_a - this.m_a / this.m_f) / Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2)));

            if (Math.Abs(X - 0) > 0.000001)
            {
                B = AngleTransform.Radian2Degree(Math.Atan((Z + e2 * (this.m_a - this.m_a / this.m_f) * Math.Pow(Math.Sin(Q), 3)) / (Math.Sqrt(X * X + Y * Y) - Math.Pow(e, 2) * this.m_a * Math.Pow(Math.Cos(Q), 3))), true);
                L = AngleTransform.Radian2Degree(Math.Atan(Y / X), true);
            }
            else
            {
                if (Math.Abs(Y - 0) > 0.000001)
                {
                    B = AngleTransform.Radian2Degree(Math.Atan((Z + e2 * (this.m_a - this.m_a / this.m_f) * Math.Pow(Math.Sin(Q), 3) / (Math.Sqrt(X * X + Y * Y) - Math.Pow(e, 2) * this.m_a * Math.Pow(Math.Cos(Q), 3)))), false);
                    L = 90;
                }
                else
                {
                    B = 0;
                    L = 0;
                }
            }

            if (L < 0) L = 180 + L;

            //度转换为弧度
            double rs = AngleTransform.Degree2Radian(B, true);

            //获取第一辅助系数
            double w = Math.Sqrt(1 - Math.Pow(e * Math.Sin(rs), 2));

            //卯酉圈曲率半径
            double N = this.m_a / w;

            H = Math.Sqrt(X * X + Y * Y) / Math.Cos(rs) - N;
        }
    }
}