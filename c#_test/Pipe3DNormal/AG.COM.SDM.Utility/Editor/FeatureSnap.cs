using System;
using System.Collections.Generic;
using AG.COM.SDM.Utility.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.Utility.Editor
{
    /// <summary>
    /// Ҫ��ҧ����
    /// </summary>
    public class FeatureSnap
    {
        private IMapControl2 m_MapControl = null;

        /// <summary>
        /// ���õ�ͼ����
        /// </summary>
        /// <param name="mapControl">��ͼ�ؼ�</param>
        public void ResetMap(IMapControl2 mapControl)
        {
            m_MapControl = mapControl;
            (mapControl as IMapControlEvents2_Event).OnMouseMove -= new IMapControlEvents2_OnMouseMoveEventHandler(FeatureSnap_OnMouseMove);
            (mapControl as IMapControlEvents2_Event).OnMouseMove += new IMapControlEvents2_OnMouseMoveEventHandler(FeatureSnap_OnMouseMove);
        }

        private void FeatureSnap_OnMouseMove(int button, int shift, int X, int Y, double mapX, double mapY)
        {
            if (m_SnapEnabled == false)
            {
                m_SnappedSucccess = false;
                return;
            }

            //ʵ������
            IPoint pt = new PointClass();
            pt.PutCoords(mapX, mapY);

            double tolerance = DisplayHelper.PixelsToMapUnits(m_MapControl.ActiveView, m_Tolerance);
            m_SnappedSucccess = Snap(pt, tolerance, ref m_SnappedPoint);    //��׽ҧ�ϵ�
            if (m_SnappedSucccess == false)
                ShowAnchorPointOut();               
            else
                ShowSnapAnchorPoint();
        }

        private IAnchorPoint m_AnchorPoint = new AnchorPointClass();

        /// <summary>
        /// ��ʾҧ�ϵ���ʽ
        /// </summary>
        private void ShowSnapAnchorPoint()
        {
            if (m_MapControl == null) return;

            m_AnchorPoint.Symbol = SnappedPointSymbol as ISymbol;
            m_AnchorPoint.MoveTo(m_SnappedPoint, m_MapControl.ActiveView.ScreenDisplay);
        }

        /// <summary>
        /// ����ʾ��SnapPoint�Ƶ���ʾ��Χ����
        /// </summary>
        private void ShowAnchorPointOut()
        {
            if (m_MapControl == null) return;

            IEnvelope tExtent = m_MapControl.ActiveView.Extent;

            IPoint tPoint = new PointClass();
            tPoint.PutCoords(tExtent.XMin - 10, tExtent.YMin - 10);

            m_AnchorPoint.Symbol = SnappedPointSymbol as ISymbol;
            m_AnchorPoint.MoveTo(tPoint, m_MapControl.ActiveView.ScreenDisplay);
        }

        private double m_Tolerance = 5;//����
        /// <summary>
        /// ��ȡ������ҧ���ݲ�
        /// </summary>
        public double Tolerance
        {
            get { return m_Tolerance; }
            set { m_Tolerance = value; }
        }

        private bool m_SnappedSucccess = false;
        /// <summary>
        /// ��ȡҧ��״̬
        /// ���ҧ�ϳɹ��򷵻�true,���򷵻�false
        /// </summary>
        public bool SnappedSucccess
        {
            get
            {
                return m_SnappedSucccess;
            }
        }

        private IPoint m_SnappedPoint = new PointClass();
        /// <summary>
        /// ��ȡҧ�ϵ�
        /// </summary>
        public IPoint SnappedPoint
        {
            get { return m_SnappedPoint; }
        }

        private bool m_SnapEnabled = false;
        /// <summary>
        /// �Ƿ�����ҧ��
        /// </summary>
        public bool SnapEnabled
        {
            get { return m_SnapEnabled; }
            set { m_SnapEnabled = value; }
        }

        private IList<SnapInfo> m_SnapList = new List<SnapInfo>();
        public IList<SnapInfo> SnapList
        {
            get { return m_SnapList; }
        }

        public SnapInfo FindSnapInfo(IFeatureClass fcls)
        {
            for (int i=0;i<=m_SnapList.Count-1;i++)
            {
                if (m_SnapList[i].FeatureClass == fcls)
                    return m_SnapList[i];
            }
            return null;
        }

        /// <summary>
        /// ��׽ҧ�ϵ�
        /// </summary>
        /// <param name="mousePoint">������ڵĵ�ͼλ��</param>
        /// <param name="tolerance">��׽���ݲ�ֵ����ͼ��λ</param>
        /// <param name="snappedPoint">ҧ�Ϻ�ĵ�</param>
        /// <returns>���ҧ�ϳɹ��򷵻�true,���򷵻�false</returns>
        public bool Snap(IPoint mousePoint, double tolerance, ref IPoint snappedPoint)
        {
            if (m_SnapEnabled == false) return false;
            if (m_SnapList.Count == 0) return false;
                       
            for (int i = 0; i <= m_SnapList.Count - 1; i++)
            {
                if (TrySnapLayer(m_SnapList[i].FeatureClass, mousePoint, tolerance, ref snappedPoint))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// ��һ��ͼ���Ͻ��в�׽����
        /// </summary>
        /// <param name="fcls"></param>
        /// <param name="mousePoint"></param>
        /// <param name="tolerance"></param>
        /// <param name="snappedPoint"></param>
        /// <returns></returns>
        public bool TrySnapLayer(IFeatureClass fcls, IPoint mousePoint,double tolerance,ref IPoint snappedPoint)
        {
            SnapInfo snapInfo = FindSnapInfo(fcls);
            if (snapInfo == null)   return false;
            if (snapInfo.SnapType == FeatureSnapType.stNone) return false;
               
            IFeatureCache pCache = new FeatureCacheClass();
            pCache.Initialize(mousePoint, tolerance);
            pCache.AddFeatures(fcls);

            IGeometry pGeometry;
            for (int i = 0; i <= pCache.Count - 1; i++)
            {
                pGeometry = pCache.get_Feature(i).Shape;
                if (TrySnapGeometry(pGeometry,mousePoint,tolerance,snapInfo.SnapType,ref snappedPoint))
                {
                    return true;                            
                }
            }
            return false;
        }

        private IMarkerSymbol m_MarkerSymbol = null;
        public IMarkerSymbol SnappedPointSymbol
        {
            get { return m_MarkerSymbol;}
        }

        /// <summary>
        /// ��һ��ͼ�ν��в�׽����
        /// </summary>
        /// <param name="pGeometry"></param>
        /// <param name="mousePoint"></param>
        /// <param name="tolerance"></param>
        /// <param name="snapType"></param>
        /// <param name="snappedPoint"></param>
        /// <returns></returns>
        public bool TrySnapGeometry(IGeometry pGeometry, IPoint mousePoint,double tolerance, FeatureSnapType snapType, ref IPoint snappedPoint)
        {

            double dblHitDist = 0;
            int partIndex = 0;
            int segmentIndex = 0;
            bool bRightSide = true;
            IPoint hitPoint = new PointClass();
            double dblOffsetDist = tolerance;// *3; //3�����޲�

            IHitTest pHitTest = pGeometry as IHitTest;
            //�˵�
            if ((snapType & FeatureSnapType.stEndPoint) == FeatureSnapType.stEndPoint)
            {
                if (pGeometry.GeometryType == esriGeometryType.esriGeometryPolyline)
                {
                    if (pHitTest.HitTest(mousePoint, tolerance, esriGeometryHitPartType.esriGeometryPartEndpoint,
                                            hitPoint, ref dblHitDist, ref partIndex, ref segmentIndex, ref bRightSide)) //������и�Ҫ��
                    {
                        if (dblOffsetDist > dblHitDist)
                        {
                            dblOffsetDist = dblHitDist;
                            snappedPoint.PutCoords(hitPoint.X, hitPoint.Y);
                            m_MarkerSymbol = CommonVariables.SnapSymbol_Endpoint;
                            return true;
                        }

                    }
                }
            }

            //����
            if ((snapType & FeatureSnapType.stVertex) == FeatureSnapType.stVertex)
            {

                if (pHitTest.HitTest(mousePoint, tolerance, esriGeometryHitPartType.esriGeometryPartVertex,
                    hitPoint, ref dblHitDist, ref partIndex, ref segmentIndex, ref bRightSide)) //������и�Ҫ��
                {
                    if (dblOffsetDist > dblHitDist)
                    {
                        dblOffsetDist = dblHitDist;
                        snappedPoint.PutCoords(hitPoint.X, hitPoint.Y);
                        m_MarkerSymbol = CommonVariables.SnapSymbol_Endpoint;
                        return true;
                    }

                }

            } 

            //����
            if ((snapType & FeatureSnapType.stEdge) == FeatureSnapType.stEdge)
            {
                if (pGeometry.GeometryType == esriGeometryType.esriGeometryPolyline
                    || pGeometry.GeometryType == esriGeometryType.esriGeometryPolygon)
                {
                    if (pHitTest.HitTest(mousePoint, tolerance, esriGeometryHitPartType.esriGeometryPartBoundary,
                        hitPoint, ref dblHitDist, ref partIndex, ref segmentIndex, ref bRightSide)) //������и�Ҫ��
                    {
                        if (dblOffsetDist > dblHitDist)
                        {
                            dblOffsetDist = dblHitDist;
                            snappedPoint.PutCoords(hitPoint.X, hitPoint.Y);
                            m_MarkerSymbol = CommonVariables.SnapSymbol_Endpoint;
                            return true;
                        }
                    }
                }
            }

            //����
            if ((snapType & FeatureSnapType.stCentroid) == FeatureSnapType.stCentroid)
            {
                if (pGeometry.GeometryType == esriGeometryType.esriGeometryPolygon)
                {
                    if (pHitTest.HitTest(mousePoint, tolerance, esriGeometryHitPartType.esriGeometryPartCentroid,
                        hitPoint, ref dblHitDist, ref partIndex, ref segmentIndex, ref bRightSide)) //������и�Ҫ��
                    {
                        if (dblOffsetDist > dblHitDist)
                        {
                            dblOffsetDist = dblHitDist;
                            snappedPoint.PutCoords(hitPoint.X, hitPoint.Y);
                            m_MarkerSymbol = CommonVariables.SnapSymbol_Endpoint;
                            return true;
                        }
                    }
                }
            }

            //����
            if ((snapType & FeatureSnapType.stCentroid) == FeatureSnapType.stCentroid)
            {
                //�ж��Ƿ����ڻ���
            }

            return false;
        }
    }

    public class SnapInfo
    {
        public IFeatureClass FeatureClass = null;
        public FeatureSnapType SnapType = FeatureSnapType.stNone;
    }

    /// <summary>
    /// Ҫ��ҧ������
    /// </summary>
    [Flags]
    public enum FeatureSnapType
    {
        stNone = 0,

        /// <summary>
        /// ���ĵ�
        /// </summary>
        stCentroid = 1,

        /// <summary>
        /// �ڵ�
        /// </summary>
        stVertex = 2,

        /// <summary>
        /// �˵�
        /// </summary>
        stEndPoint = 4,

        /// <summary>
        /// ����
        /// </summary>
        stPendicularFoot = 8,

        /// <summary>
        /// ��
        /// </summary>
        stEdge = 16
    }
}
