using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AG.COM.SDM.GeoDataBase
{
    public class HelmertTransform
    {
        private double m_dx, m_dy, m_dz;
        private double m_rx, m_ry, m_rz;
        private double m_dm;

        /// <summary>
        /// 获取或设置X方向偏移值
        /// </summary>
        public double DX
        {
            get
            {
                return this.m_dx;
            }
            set
            {
                this.m_dx = value;
            }
        }

        /// <summary>
        /// 获取或设置Y方向偏移值
        /// </summary>
        public double DY
        {
            get
            {
                return this.m_dy;
            }
            set
            {
                this.m_dy = value;
            }
        }

        /// <summary>
        /// 获取或设置Z方向偏移值
        /// </summary>
        public double DZ
        {
            get
            {
                return this.m_dz;
            }
            set
            {
                this.m_dz = value;
            }
        }

        /// <summary>
        /// 获取或设置X方向旋转角度
        /// </summary>
        public double RX
        {
            get
            {
                return this.m_rx;
            }
            set
            {
                this.m_rx = value;
            }
        }

        /// <summary>
        /// 获取或设置Y方向旋转角度
        /// </summary>
        public double RY
        {
            get
            {
                return this.m_ry;
            }
            set
            {
                this.m_ry = value;
            }
        }

        /// <summary>
        /// 获取或设置Z方向旋转角度
        /// </summary>
        public double RZ
        {
            get
            {
                return this.m_rz;
            }
            set
            {
                this.m_rz = value;
            }
        }

        /// <summary>
        /// 获取或设置缩放比例
        /// </summary>
        public double DM
        {
            get
            {
                return this.m_dm;
            }
            set
            {
                this.m_dm = value;
            }
        }

        /// <summary>
        /// 根据控制点设定转换参数
        /// </summary>
        /// <param name="fromPoints">源坐标点对</param>
        /// <param name="toPoints">目标坐标点对</param>
        public void DefineFromControlPoints(PointD[] fromPoints, PointD[] toPoints)
        {

        }

        /// <summary>
        /// 七参数变换
        /// </summary>
        /// <param name="fromPoint">要转换的点对象</param>
        /// <returns>返回转换后的对象</returns>
        public PointD TransformPoint(PointD fromPoint)
        {
            //********************************************
            //说明：采用布尔莎-沃尔夫变换模型（七参数变换模型）
            // X1 = ( 1 + M ) * X0 + Rz * Y0 - Ry * Z0 + Dx
            // Y1 = ( 1 + M ) * Y0 - Rz * X0 + Rx * Z0 + Dy
            // Z1 = ( 1 + M ) * Z1 + Ry * X0 - Rx * Y0 + Dz
            //*********************************************  
            PointD tPoint = new PointD();

            tPoint.X = (1 + m_dm) * fromPoint.X + m_rz * fromPoint.Y - m_ry * fromPoint.Z + m_dx;
            tPoint.Y = (1 + m_dm) * fromPoint.Y - m_rz * fromPoint.X + m_rx * fromPoint.Z + m_dy;
            tPoint.Z = (1 + m_dm) * fromPoint.Z + m_ry * fromPoint.X - m_rx * fromPoint.Y + m_dz;

            return tPoint;
        }
    }
}
