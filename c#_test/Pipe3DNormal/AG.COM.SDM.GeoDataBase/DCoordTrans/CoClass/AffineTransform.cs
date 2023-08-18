using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 国家到地方坐标转换类(四参数)
    /// </summary>
    public class AffineTransform
    {
        private string m_Name;
        private double m_DX, m_DY;
        private double m_RotationAngle;
        private double m_Scale;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AffineTransform()
        {
            this.m_Name = "未定义";
        }

        /// <summary>
        /// 重载构造函数
        /// </summary>
        /// <param name="pTransName">转换名称</param>
        /// <param name="pDx">X方向偏移距离</param>
        /// <param name="pDy">Y方向偏移距离</param>
        /// <param name="pScale">缩放尺度</param>
        /// <param name="pRotationAngle">旋转角度</param>
        public AffineTransform(string pTransName, double pDx, double pDy, double pScale, double pRotationAngle)
        {
            this.m_Name = pTransName;
            this.m_DX = pDx;
            this.m_DY = pDy;
            this.m_Scale = pScale;
            this.m_RotationAngle = pRotationAngle;
        }

        /// <summary>
        /// 根据控制点设定转换参数
        /// </summary>
        /// <param name="fromPoints">源坐标点对</param>
        /// <param name="toPoints">目标坐标点对</param>
        public void DefineFromControlPoints(PointD[] fromPoints, PointD[] toPoints)
        {
            if (fromPoints.Length != toPoints.Length)
                throw new Exception("源坐标点个数与目标坐标点个数不一致");

            int count = fromPoints.Length - 1;
            double sumDx = 0, sumDy = 0, sumRotation = 0, sumScale = 0;
            double dX, dY, dT, dK;

            for (int i = 0; i < count; i++)
            {
                dT = this.GetRotation(fromPoints[i], toPoints[i], fromPoints[i + 1], toPoints[i + 1]);      //旋转角度
                dK = this.GetScale(fromPoints[i], toPoints[i], fromPoints[i + 1], toPoints[i + 1], dT);     //缩放因子
                dX = this.GetXTranslation(fromPoints[i], toPoints[i], dT, dK);                              //X方向偏移量
                dY = this.GetYTranslation(fromPoints[i], toPoints[i], dT, dK);                              //Y方向偏移量

                sumDx += dX;
                sumDy += dY;
                sumRotation += dT;
                sumScale += dK;
            }

            this.m_DX = sumDx / count;
            this.m_DY = sumDy / count;
            this.m_RotationAngle = sumRotation / count;
            this.m_Scale = sumScale / count;
        }

        /// <summary>
        /// 获取或设置转换名称
        /// </summary>
        public string TransName
        {
            get
            {
                return this.m_Name;
            }
            set
            {
                this.m_Name = value;
            }
        }

        /// <summary>
        /// 获取或设置X方向偏移距离
        /// </summary>
        public double XTranslation
        {
            get
            {
                return Math.Round(this.m_DX, 12);
            }
            set
            {
                this.m_DX = value;
            }
        }

        /// <summary>
        /// 获取或设置Y方向偏移距离
        /// </summary>
        public double YTranslation
        {
            get
            {
                return Math.Round(this.m_DY, 12);
            }
            set
            {
                this.m_DY = value;
            }
        }

        /// <summary>
        /// 获取或设置尺度
        /// </summary>
        public double Scale
        {
            get
            {
                return Math.Round(this.m_Scale, 12);
            }
            set
            {
                this.m_Scale = value;
            }
        }

        /// <summary>
        /// 获取或设置旋转角度
        /// </summary>
        public double RotationAngle
        {
            get
            {
                return Math.Round(this.m_RotationAngle, 12);
            }
            set
            {
                this.m_RotationAngle = value;
            }
        }

        /// <summary>
        /// 四参数变换（原来代码）
        /// </summary>
        /// <param name="fromPoint">要转换的点对象</param>
        /// <returns>返回转换后的对象</returns>
        public PointD TransformPoint(PointD fromPoint)
        {
            //********************************************
            //说明：采用相似变换模型（四参数变换模型）
            // X= ax - by + c
            // Y= bx + ay + d
            //*********************************************
            double A = this.m_Scale * Math.Cos(this.m_RotationAngle);
            double B = this.m_Scale * Math.Sin(this.m_RotationAngle);

            PointD tPoint = new PointD();

            tPoint.X = A * fromPoint.X - B * fromPoint.Y + this.m_DX;
            tPoint.Y = B * fromPoint.X + A * fromPoint.Y + this.m_DY;

            return tPoint;
        }

        /// <summary>
        /// 四参数变换（新代码）
        /// </summary>
        /// <param name="fromPoint">要转换的点对象</param>
        /// <returns>返回转换后的对象</returns>
        public PointD TransformPoint2(PointD fromPoint)
        {
            //********************************************
            //说明：采用相似变换模型（四参数变换模型）
            //x2 = k*(cosA*x1 + sinA*y1) + Dx
            //y2 = k*(-sinA*x1 + cosA*y1) + Dy
            //即
            // X= ax + by + c
            // Y= -bx + ay + d
            //*********************************************
            double A = this.m_Scale * Math.Cos(this.m_RotationAngle);
            double B = this.m_Scale * Math.Sin(this.m_RotationAngle);

            PointD tPoint = new PointD();

            tPoint.X = A * fromPoint.X + B * fromPoint.Y + this.m_DX;
            tPoint.Y = A * fromPoint.Y - B * fromPoint.X + this.m_DY;

            //tPoint.X = this.m_Scale * (Math.Cos(this.m_RotationAngle) * fromPoint.X + Math.Sin(this.m_RotationAngle) * fromPoint.Y) + this.m_DX;
            //tPoint.Y = this.m_Scale * (-Math.Sin(this.m_RotationAngle) * fromPoint.X + Math.Cos(this.m_RotationAngle) * fromPoint.Y) + this.m_DY;

            return tPoint;
        }

        /// <summary>
        /// 重载override方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.m_Name;
        }

        ///<summary>
        ///获取旋转角度
        ///</summary>
        ///<param name="fromCoordPoint1">源点1</param>
        ///<param name="toCoordPoint1">目标点1</param>
        ///<param name="fromCoordPoint2">源点2</param>
        ///<param name="toCoordPoint2">目标点2</param>
        ///<returns>返回旋转角度</returns>
        private double GetRotation(PointD fromPoint1, PointD toPoint1, PointD fromPoint2, PointD toPoint2)
        {
            double a = (toPoint2.Y - toPoint1.Y) * (fromPoint2.X - fromPoint1.X) - (toPoint2.X - toPoint1.X) * (fromPoint2.Y - fromPoint1.Y);
            double b = (toPoint2.X - toPoint1.X) * (fromPoint2.X - fromPoint1.X) + (toPoint2.Y - toPoint1.Y) * (fromPoint2.Y - fromPoint1.Y);

            if (Math.Abs(Math.Round(b, 8)) > 0)
                return Math.Tan(a / b);
            else
                return Math.Tan(0);
        }

        ///<summary>
        ///获取缩放比例因子
        ///</summary>
        ///<param name="fromCoordPoint1">源点1</param>
        ///<param name="toCoordPoint1">目标点1</param>
        ///<param name="fromCoordPoint2">源点2</param>
        ///<param name="toCoordPoint2">目标点2</param>
        ///<param name="rotation">旋转角度</param>
        ///<returns>返回旋转因子</returns>
        private double GetScale(PointD fromPoint1, PointD toPoint1, PointD fromPoint2, PointD toPoint2, double rotation)
        {
            double a = toPoint2.X - toPoint1.X;
            double b = (fromPoint2.X - fromPoint1.X) * Math.Cos(rotation) - (fromPoint2.Y - fromPoint1.Y) * Math.Sin(rotation);

            double scale = Math.Round(b, 8);
            if (Math.Abs(scale) > 0)
                return a / b;
            else
                return 0;
        }

        ///<summary>
        ///得到X方向偏移量
        ///</summary>
        ///<param name="fromCoordPoint1">源点1</param>
        ///<param name="toCoordPoint1">目标点1</param>
        ///<param name="rotation">旋转角度</param>
        ///<param name="scale">缩放因子</param>
        ///<returns>返回X方向偏移量</returns>
        private double GetXTranslation(PointD fromPoint1, PointD toPoint1, double rotation, double scale)
        {
            double dX = (toPoint1.X - scale * (fromPoint1.X * Math.Cos(rotation) - fromPoint1.Y * Math.Sin(rotation)));
            return dX;
        }

        ///<summary>
        ///得到Y方向偏移量
        ///</summary>
        ///<param name="fromCoordPoint1">源点1</param>
        ///<param name="toCoordPoint1">目标点1</param>
        ///<param name="rotation">旋转角度</param>
        ///<param name="scale">缩放因子</param>
        ///<returns>返回Y方向偏移量</returns>
        private double GetYTranslation(PointD fromPoint1, PointD toPoint1, double rotation, double scale)
        {
            double dY = (toPoint1.Y - scale * (fromPoint1.X * Math.Sin(rotation) + fromPoint1.Y * Math.Cos(rotation)));

            return dY;
        }
    }
}
