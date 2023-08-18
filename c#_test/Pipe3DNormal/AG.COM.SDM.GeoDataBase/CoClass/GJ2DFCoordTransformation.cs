using System;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 国家到地方坐标转换类(四参数)
    /// </summary>
    public class GJ2DFCoordTransformation
    {
        private string m_Name;
        private double m_DX, m_DY;
        private double m_RotationAngle;
        private double m_Scale;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public GJ2DFCoordTransformation()
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
        public GJ2DFCoordTransformation(string pTransName, double pDx, double pDy, double pScale, double pRotationAngle)
        {
            this.m_Name = pTransName;
            this.m_DX = pDx;
            this.m_DY = pDy;
            this.m_Scale = pScale;
            this.m_RotationAngle = pRotationAngle;
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
                return this.m_DX;
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
                return this.m_DY;
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
                return this.m_Scale;
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
                return this.m_RotationAngle;
            }
            set
            {
                this.m_RotationAngle = value;
            }
        }


        /// <summary>
        /// 重载override方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.m_Name;
        }

        /// <summary>
        /// 几何对象坐标转换
        /// </summary>
        /// <param name="pGeometry">几何对象</param>  
        /// <returns>返回转换后的几何对象</returns>
        public IGeometry GeometryTransform(IGeometry pGeometry)
        {
            IGeometry tGeometry = null;

            if (pGeometry.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                tGeometry = TransformPoint(pGeometry as IPoint);
            }
            else if (pGeometry.GeometryType == esriGeometryType.esriGeometryLine)
            {
                tGeometry = TransformLine(pGeometry as ILine);
            }
            else
            {
                tGeometry = TransformPointCollection(pGeometry);
            }

            bool tIsZ = IsHasZValue(pGeometry);
            if (tIsZ == true)
            {
                SetZValue(tGeometry);
            }

            return tGeometry;
        }

        /// <summary>
        /// 判断是否含z值
        /// </summary>
        /// <param name="tGeometry"></param>
        /// <returns></returns>
        private bool IsHasZValue(IGeometry tGeometry)
        {
            IZAware tZAware = tGeometry as IZAware;
            if (tZAware != null)
            {
                return tZAware.ZAware;
            }

            return false;
        }

        /// <summary>
        /// 设Z值
        /// </summary>
        /// <param name="geometry"></param>
        private void SetZValue(IGeometry geometry)
        {
            IZAware pZAware = (IZAware)geometry;
            pZAware.ZAware = true;
            if (geometry.GeometryType == esriGeometryType.esriGeometryPolygon
                || geometry.GeometryType == esriGeometryType.esriGeometryPolyline)
            {
                IZ iz1 = geometry as IZ;
                iz1.SetConstantZ(0);  //将Z值设置为0
            }
            else if (geometry.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                IPoint point = geometry as IPoint;
                point.Z = 0;
            }
            else
            {
                //完全重构图形，根据X,Y坐标

            }
        }

        /// <summary>
        /// 转换空间点
        /// </summary>
        /// <param name="pPoint">点</param> 
        /// <returns>返回转换后的点</returns>
        private IGeometry TransformPoint(IPoint pPoint)
        {
            //********************************************
            //说明：采用相似变换模型（四参数变换模型）
            // X= ax - by + c
            // Y= bx + ay + d
            //*********************************************
            double A = this.m_Scale * Math.Cos(this.m_RotationAngle);
            double B = this.m_Scale * Math.Sin(this.m_RotationAngle);

            IPoint tPoint = new PointClass();

            tPoint.X = A * pPoint.X - B * pPoint.Y + this.m_DX;
            tPoint.Y = B * pPoint.X + A * pPoint.Y + this.m_DY;

            return tPoint;
        }

        /// <summary>
        /// 转换空间线
        /// </summary>
        /// <param name="pLine">线</param>
        /// <returns>返回转换后的线</returns>
        private IGeometry TransformLine(ILine pLine)
        {
            IPoint tFromPoint = TransformPoint(pLine.FromPoint) as IPoint;
            IPoint tToPoint = TransformPoint(pLine.ToPoint) as IPoint;

            ILine tNewLine = new LineClass();
            tNewLine.PutCoords(tFromPoint, tToPoint);

            return tNewLine;
        }

        /// <summary>
        /// 转换除点、线外的其它空间对象
        /// </summary>
        /// <param name="pGeometry">几何对象</param>
        /// <param name="pTransformation">坐标转换类</param>
        /// <returns>返回转换后的几何对象</returns>
        private IGeometry TransformPointCollection(IGeometry pGeometry)
        {
            //查询接口引用
            IPointCollection4 tPointCollection = pGeometry as IPointCollection4;

            IPointCollection tempNewPoints = null;
            if (pGeometry.GeometryType == esriGeometryType.esriGeometryMultipoint)
            {
                tempNewPoints = new MultipointClass();
            }
            else if (pGeometry.GeometryType == esriGeometryType.esriGeometryPolyline)
            {
                tempNewPoints = new PolylineClass();
            }
            else if (pGeometry.GeometryType == esriGeometryType.esriGeometryPolygon)
            {
                tempNewPoints = new PolygonClass();
            }

            object missing = Type.Missing;

            //循环赋值
            for (int i = 0; i < tPointCollection.PointCount; i++)
            {
                IPoint tNewPt = TransformPoint(tPointCollection.get_Point(i)) as IPoint;
                tempNewPoints.AddPoint(tNewPt, ref missing, ref missing);
            }

            return tempNewPoints as IGeometry;   
        } 
    }
}
