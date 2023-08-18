using System;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.Utility.Display
{
    /// <summary>
    /// 新建园形对象
    /// </summary>
    public class NewCircleFeedBackEx : INewGeometryFeedBack,IDisposable
    {
        private ISymbol m_LineSymbol = null;
        private IPoint m_CenterPoint = null; 
        private bool m_AsLine = false;

        public NewCircleFeedBackEx(bool asLine)
        {
            m_AsLine = asLine;

            m_LineSymbol = new SimpleLineSymbolClass();
            m_LineSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            IRgbColor color = new RgbColorClass();
            color.Blue = 0;
            color.Green = 0;
            color.Red = 0;
            (m_LineSymbol as ILineSymbol).Color = color as IColor; 
        } 

        #region INewGeometryFeedBack 成员

        private IScreenDisplay m_Display = null;
        private int m_HDC = 0;
        private IActiveView m_ActiveView;
      
        public IActiveView ActiveView
        {
            get
            {
                return m_ActiveView;
            }
            set
            {
                m_ActiveView = value;
                m_Display = m_ActiveView.ScreenDisplay;
                if (m_Display != null)
                {
                    Win32APIs.WinAPI.ReleaseDC(m_Display.hWnd, m_HDC);
                }

                m_HDC = Win32APIs.WinAPI.GetWindowDC(m_Display.hWnd);
                m_LineSymbol.ResetDC();
                m_LineSymbol.SetupDC(m_HDC, m_Display.DisplayTransformation);
            }
        }


        public ESRI.ArcGIS.Display.ISymbol Symbol
        {
            get
            {
                return m_LineSymbol;
            }
        }

        public void Start(ESRI.ArcGIS.Geometry.IPoint point)
        {
            m_CenterPoint = point;

            ISimpleMarkerSymbol symbol = new SimpleMarkerSymbolClass();
            symbol.Style = esriSimpleMarkerStyle.esriSMSDiamond;
            symbol.Size = 5;
            IRgbColor color = new RgbColorClass();
            color.Red = 100;
            color.Green = 255;
            color.Blue = 165;
            symbol.Color = color as IColor;
            (symbol as ISymbol).ROP2 = esriRasterOpCode.esriROPCopyPen;

            m_CenterAnchorPoint = new AnchorPointClass();
            m_CenterAnchorPoint.Symbol = symbol as ISymbol;
            m_CenterAnchorPoint.MoveTo(point,m_ActiveView.ScreenDisplay); 
        }

        public void AddPoint(ESRI.ArcGIS.Geometry.IPoint point)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        private IGeometry ConstructPolyline(IPoint centerPoint,double radius)
        {
            ISegmentCollection pSegCol = new PolylineClass();
            ICircularArc arc = null;
            arc = new CircularArcClass();
            (arc as IConstructCircularArc).ConstructCircle(centerPoint, radius, true);

            object a = System.Reflection.Missing.Value;
            pSegCol.AddSegment(arc as ISegment, ref a, ref a);

            return pSegCol as IGeometry;
        }

        private IPoint m_LastPoint = null;
        public void MoveTo(ESRI.ArcGIS.Geometry.IPoint point)
        {
            if (m_CenterPoint == null)
                return;

            IGeometry pGeometry = null;
            double radius = 0;
            if (m_LastPoint != null)
            {
                radius = (m_LastPoint as IProximityOperator).ReturnDistance(m_CenterPoint);
                pGeometry = ConstructPolyline(m_CenterPoint, radius);
                m_LineSymbol.Draw(pGeometry);
            }

            radius = (point as IProximityOperator).ReturnDistance(m_CenterPoint);
            pGeometry = ConstructPolyline(m_CenterPoint, radius);
            m_LineSymbol.Draw(pGeometry);

            m_LastPoint = point;
        }

        public void FinishPart()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public ESRI.ArcGIS.Geometry.IGeometry Stop()
        {
            if (m_LastPoint == null)
                return null;
            ISegmentCollection pSegCol = null;
            if (m_AsLine)
                pSegCol = new PolylineClass();
            else
                pSegCol = new PolygonClass();

            ICircularArc arc = null;
            arc = new CircularArcClass();
            double radius = (m_LastPoint as IProximityOperator).ReturnDistance(m_CenterPoint);
            (arc as IConstructCircularArc).ConstructCircle(m_CenterPoint, radius, true);

            object a = System.Reflection.Missing.Value;
            pSegCol.AddSegment(arc as ISegment, ref a, ref a);

            m_LastPoint = null;
            m_CenterAnchorPoint = null;

            return pSegCol as IGeometry;
        }

        private IAnchorPoint m_CenterAnchorPoint = null;
        public void Refresh()
        {
            if (m_LastPoint == null)
                return ;
            if (m_CenterPoint == null)
                return;
            if (m_CenterAnchorPoint != null)
            {
                m_CenterAnchorPoint.Symbol.ROP2 = esriRasterOpCode.esriROPCopyPen;
                m_CenterAnchorPoint.Draw(m_ActiveView.ScreenDisplay);
            }
            double radius = (m_LastPoint as IProximityOperator).ReturnDistance(m_CenterPoint);
            IGeometry pGeometry = ConstructPolyline(m_CenterPoint, radius);
            m_LineSymbol.Draw(pGeometry);
        }

        public ESRI.ArcGIS.Geometry.IGeometry Geometry
        {
            get 
            {
                if (m_CenterPoint == null)
                    return null;
                IPoint pt = (m_CenterPoint as ESRI.ArcGIS.esriSystem.IClone).Clone() as IPoint;
                m_CenterPoint = null;
                m_LastPoint = null;
                return pt;
            }
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            if (m_Display != null)
            {
                Win32APIs.WinAPI.ReleaseDC(m_Display.hWnd, m_HDC);
            }
            m_LineSymbol.ResetDC();
        }

        #endregion
    }
}
