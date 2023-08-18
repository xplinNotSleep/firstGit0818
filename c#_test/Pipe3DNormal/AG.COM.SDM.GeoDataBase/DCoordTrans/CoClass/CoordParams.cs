using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 坐标参数
    /// </summary>
    public class CoordParams
    {
        private double m_a;             //长半轴
        private double m_b;             //短半轴
        private double m_f;             //扁率
        private double m_r1;            //弧长参数1
        private double m_r2;            //弧长参数2
        private double m_r3;            //弧长参数3
        private double m_r4;            //弧长参数4
        private double m_r5;            //弧长参数5
        private double m_r6;            //弧长参数6
        private double m_r7;            //弧长参数7

        /// <summary>
        /// 获取或设置长半轴长
        /// </summary>
        public double a
        {
            get
            {
                return this.m_a;
            }
            set
            {
                this.m_a = value;
            }
        }

        /// <summary>
        /// 获取或设置短半轴长
        /// </summary>
        public double b
        {
            get
            {
                return this.m_b;
            }
            set
            {
                this.m_b = value;
            }
        }

        /// <summary>
        /// 获取或设置扁率
        /// </summary>
        public double f
        {
            get
            {
                return this.m_f;
            }
            set
            {
                this.m_f = value;
            }
        }

        /// <summary>
        /// 获取第一偏心率
        /// </summary>
        public double e1
        {
            get
            {
                return Math.Sqrt(2 * this.m_f - Math.Pow(this.m_f, 2));
            }
        }

        /// <summary>
        /// 获取第二偏心率
        /// </summary>
        public double e2
        {
            get
            {
                double e = this.e1;

                double e2 = e / Math.Sqrt(1 - e * e);

                return e2;
            }
        }

        /// <summary>
        /// 返回弧长参数1
        /// </summary>
        public double r1
        {
            get
            {
                return this.m_r1;
            }
        }

        /// <summary>
        /// 返回弧长参数2
        /// </summary>
        public double r2
        {
            get
            {
                return this.m_r2;
            }
        }

        /// <summary>
        /// 返回弧长参数3
        /// </summary>
        public double r3
        {
            get
            {
                return this.m_r3;
            }
        }

        /// <summary>
        /// 返回弧长参数4
        /// </summary>
        public double r4
        {
            get
            {
                return this.m_r4;
            }
        }

        /// <summary>
        /// 返回弧长参数5
        /// </summary>
        public double r5
        {
            get
            {
                return this.m_r5;
            }
        }

        /// <summary>
        /// 返回弧长参数6
        /// </summary>
        public double r6
        {
            get
            {
                return this.m_r6;
            }
        }

        /// <summary>
        /// 返回弧长参数7
        /// </summary>
        public double r7
        {
            get
            {
                return this.m_r7;
            }
        }

        /// <summary>
        /// 子午弧长参数求解
        /// </summary>  
        public void CalcRLParams()
        {
            //**********************************************
            ////西安80参数
            //double a = 6378140;
            //double f = 1 / 298.257;
            //double e = Math.Sqrt(2 * f - Math.Pow(f, 2));
            //************************************************

            double a = this.m_a;
            double f = this.m_f;
            double e = e1;

            this.m_r1 = 1 + 0.75 * Math.Pow(e, 2) + 45.0 / 64 * Math.Pow(e, 4) + 175.0 / 256 * Math.Pow(e, 6) + 11025.0 / 16384 * Math.Pow(e, 8) + 43659.0 / 65536 * Math.Pow(e, 10) + 693693.0 / 1048576 * Math.Pow(e, 12);

            this.m_r2 = 3.0 / 8 * Math.Pow(e, 2) + 15.0 / 32 * Math.Pow(e, 4) + 525.0 / 1024 * Math.Pow(e, 6) + 2205.0 / 4096 * Math.Pow(e, 8) + 72765.0 / 131072 * Math.Pow(e, 10) + 297297.0 / 524288 * Math.Pow(e, 12);

            this.m_r3 = 15.0 / 256 * Math.Pow(e, 4) + 105.0 / 1024 * Math.Pow(e, 6) + 2205.0 / 16384 * Math.Pow(e, 8) + 10395.0 / 65536 * Math.Pow(e, 10) + 1486485.0 / 8388608 * Math.Pow(e, 12);

            this.m_r4 = 35.0 / 3072 * Math.Pow(e, 6) + 105.0 / 4096 * Math.Pow(e, 8) + 10395.0 / 262144 * Math.Pow(e, 10) + 55055.0 / 1048576 * Math.Pow(e, 12);

            this.m_r5 = 315.0 / 131072 * Math.Pow(e, 8) + 3465.0 / 524288 * Math.Pow(e, 10) + 99099.0 / 8388608 * Math.Pow(e, 12);

            this.m_r6 = 693.0 / 1310720 * Math.Pow(e, 10) + 9009.0 / 5242880 * Math.Pow(e, 12);

            this.m_r7 = 1001.0 / 8388608 * Math.Pow(e, 12);
        }

        /// <summary>
        /// 子午弧长参数求解
        /// </summary>
        /// <param name="a">长半轴</param>
        /// <param name="f">扁率</param>
        /// <param name="a1">参数1</param>
        /// <param name="b1">参数2</param>
        /// <param name="c1">参数3</param>
        /// <param name="d1">参数4</param>
        /// <param name="e1">参数5</param>
        /// <param name="f1">参数6</param>
        /// <param name="g1">参数7</param> 
        public void CalcRLParams(double a, double f, out double a1, out double b1, out double c1, out double d1, out double e1, out double f1, out double g1)
        {
            //**********************************************
            //西安80参数
            //double a = 6378140;
            //double f = 1 / 298.257;
            //double e = Math.Sqrt(2 * f - Math.Pow(f, 2));
            //************************************************ 

            double e = Math.Sqrt(2 * f - Math.Pow(f, 2));

            a1 = 1 + 0.75 * Math.Pow(e, 2) + 45.0 / 64 * Math.Pow(e, 4) + 175.0 / 256 * Math.Pow(e, 6) + 11025.0 / 16384 * Math.Pow(e, 8) + 43659.0 / 65536 * Math.Pow(e, 10) + 693693.0 / 1048576 * Math.Pow(e, 12);

            b1 = 3.0 / 8 * Math.Pow(e, 2) + 15.0 / 32 * Math.Pow(e, 4) + 525.0 / 1024 * Math.Pow(e, 6) + 2205.0 / 4096 * Math.Pow(e, 8) + 72765.0 / 131072 * Math.Pow(e, 10) + 297297.0 / 524288 * Math.Pow(e, 12);

            c1 = 15.0 / 256 * Math.Pow(e, 4) + 105.0 / 1024 * Math.Pow(e, 6) + 2205.0 / 16384 * Math.Pow(e, 8) + 10395.0 / 65536 * Math.Pow(e, 10) + 1486485.0 / 8388608 * Math.Pow(e, 12);

            d1 = 35.0 / 3072 * Math.Pow(e, 6) + 105.0 / 4096 * Math.Pow(e, 8) + 10395.0 / 262144 * Math.Pow(e, 10) + 55055.0 / 1048576 * Math.Pow(e, 12);

            e1 = 315.0 / 131072 * Math.Pow(e, 8) + 3465.0 / 524288 * Math.Pow(e, 10) + 99099.0 / 8388608 * Math.Pow(e, 12);

            f1 = 693.0 / 1310720 * Math.Pow(e, 10) + 9009.0 / 5242880 * Math.Pow(e, 12);

            g1 = 1001.0 / 8388608 * Math.Pow(e, 12);
        }
    }
}
