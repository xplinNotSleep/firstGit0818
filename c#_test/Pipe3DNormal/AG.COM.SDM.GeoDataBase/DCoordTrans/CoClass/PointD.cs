using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 双精度点对象
    /// </summary>
    public struct PointD
    {
        private double m_X;             //X坐标值
        private double m_Y;             //Y坐标值
        private double m_Z;             //Z坐标值


        /// <summary>
        /// 初始化实例对象
        /// </summary>
        /// <param name="X">X坐标</param>
        /// <param name="Y">Y坐标</param>
        public PointD(double X, double Y)
        {
            this.m_X = X;
            this.m_Y = Y;
            this.m_Z = 0;
        }

        /// <summary>
        /// 初始化实例对象
        /// </summary>
        /// <param name="X">X坐标</param>
        /// <param name="Y">Y坐标</param>
        /// <param name="Z">Z坐标</param>
        public PointD(double X, double Y, double Z)
        {
            this.m_X = X;
            this.m_Y = Y;
            this.m_Z = Z;
        }

        /// <summary>
        /// 获取或设置坐标的中X值
        /// </summary>
        public double X
        {
            get
            {
                return this.m_X;
            }
            set
            {
                this.m_X = value;
            }
        }

        /// <summary>
        /// 获取或设置坐标中的Y值
        /// </summary>
        public double Y
        {
            get
            {
                return this.m_Y;
            }
            set
            {
                this.m_Y = value;
            }
        }

        /// <summary>
        /// 获取或设置坐标中的Z值
        /// </summary>
        public double Z
        {
            get
            {
                return this.m_Z;
            }
            set
            {
                this.m_Z = value;
            }
        }

        /// <summary>
        /// 重载ToString()方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("X坐标:{0}\tY坐标:{1}", X, Y);
        }
    }
}
