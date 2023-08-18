using System;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// ���ҵ��ط�����ת����(�Ĳ���)
    /// </summary>
    public class GJ2DFCoordTransformation
    {
        private string m_Name;
        private double m_DX, m_DY;
        private double m_RotationAngle;
        private double m_Scale;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public GJ2DFCoordTransformation()
        {
            this.m_Name = "δ����";
        }

        /// <summary>
        /// ���ع��캯��
        /// </summary>
        /// <param name="pTransName">ת������</param>
        /// <param name="pDx">X����ƫ�ƾ���</param>
        /// <param name="pDy">Y����ƫ�ƾ���</param>
        /// <param name="pScale">���ų߶�</param>
        /// <param name="pRotationAngle">��ת�Ƕ�</param>
        public GJ2DFCoordTransformation(string pTransName, double pDx, double pDy, double pScale, double pRotationAngle)
        {
            this.m_Name = pTransName;
            this.m_DX = pDx;
            this.m_DY = pDy;
            this.m_Scale = pScale;
            this.m_RotationAngle = pRotationAngle;
        }

        /// <summary>
        /// ��ȡ������ת������
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
        /// ��ȡ������X����ƫ�ƾ���
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
        /// ��ȡ������Y����ƫ�ƾ���
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
        /// ��ȡ�����ó߶�
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
        /// ��ȡ��������ת�Ƕ�
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
        /// ����override����
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.m_Name;
        }

        /// <summary>
        /// ���ζ�������ת��
        /// </summary>
        /// <param name="pGeometry">���ζ���</param>  
        /// <returns>����ת����ļ��ζ���</returns>
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
        /// �ж��Ƿ�zֵ
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
        /// ��Zֵ
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
                iz1.SetConstantZ(0);  //��Zֵ����Ϊ0
            }
            else if (geometry.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                IPoint point = geometry as IPoint;
                point.Z = 0;
            }
            else
            {
                //��ȫ�ع�ͼ�Σ�����X,Y����

            }
        }

        /// <summary>
        /// ת���ռ��
        /// </summary>
        /// <param name="pPoint">��</param> 
        /// <returns>����ת����ĵ�</returns>
        private IGeometry TransformPoint(IPoint pPoint)
        {
            //********************************************
            //˵�����������Ʊ任ģ�ͣ��Ĳ����任ģ�ͣ�
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
        /// ת���ռ���
        /// </summary>
        /// <param name="pLine">��</param>
        /// <returns>����ת�������</returns>
        private IGeometry TransformLine(ILine pLine)
        {
            IPoint tFromPoint = TransformPoint(pLine.FromPoint) as IPoint;
            IPoint tToPoint = TransformPoint(pLine.ToPoint) as IPoint;

            ILine tNewLine = new LineClass();
            tNewLine.PutCoords(tFromPoint, tToPoint);

            return tNewLine;
        }

        /// <summary>
        /// ת�����㡢����������ռ����
        /// </summary>
        /// <param name="pGeometry">���ζ���</param>
        /// <param name="pTransformation">����ת����</param>
        /// <returns>����ת����ļ��ζ���</returns>
        private IGeometry TransformPointCollection(IGeometry pGeometry)
        {
            //��ѯ�ӿ�����
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

            //ѭ����ֵ
            for (int i = 0; i < tPointCollection.PointCount; i++)
            {
                IPoint tNewPt = TransformPoint(tPointCollection.get_Point(i)) as IPoint;
                tempNewPoints.AddPoint(tNewPt, ref missing, ref missing);
            }

            return tempNewPoints as IGeometry;   
        } 
    }
}
