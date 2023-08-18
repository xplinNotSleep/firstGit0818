using System;
using System.Collections;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;

namespace AG.COM.SDM.Utility.Display
{
    /// <summary>
    /// 新建线状对象类
    /// </summary>
    public class NewPolylineFeedBackEx : INewGeometryFeedBack, IDisposable,IOperation
    {
        private ISymbol m_LineSymbol = null;

        public NewPolylineFeedBackEx()
        {
            m_LineSymbol = new SimpleLineSymbolClass();
            m_LineSymbol.ROP2 = esriRasterOpCode.esriROPCopyPen;
            IRgbColor color = new RgbColorClass();
            color.Blue = 0;
            color.Green = 0;
            color.Red = 0;
            (m_LineSymbol as ILineSymbol).Color = color as IColor;

            m_LastPoint = null;
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

        private IPolyline m_Polyline = null;
        public void Start(ESRI.ArcGIS.Geometry.IPoint point)
        {
            m_Polyline = new PolylineClass();
            IPath path = new PathClass();
            object a = System.Reflection.Missing.Value;
            (path as IPointCollection).AddPoint(point, ref a, ref a);
            (m_Polyline as IGeometryCollection).AddGeometry(path, ref a, ref a);

            m_UndoList.Add(point);
            m_RedoList.Clear();

        }

  
        public void AddPoint(ESRI.ArcGIS.Geometry.IPoint point)
        {
            if (m_Polyline == null)
                return;
            IGeometryCollection pGeoCol = m_Polyline as IGeometryCollection;
            IPath path = pGeoCol.get_Geometry(pGeoCol.GeometryCount - 1) as IPath;
            object a = System.Reflection.Missing.Value;
            (path as IPointCollection).AddPoint(point, ref a, ref a); 

            MoveTo(point);

            //undolist
            m_UndoList.Add(point);
            m_RedoList.Clear();
        }

        private IPoint m_LastPoint = null;
        public void MoveTo(ESRI.ArcGIS.Geometry.IPoint point)
        {
            if (m_Polyline == null)
                return;

            object a = System.Reflection.Missing.Value;
            IGeometryCollection pGeoCol = m_Polyline as IGeometryCollection;
 
            for (int i = 0; i <= pGeoCol.GeometryCount - 1; i++)
            {
                IGeometry polyline = new PolylineClass();
                (polyline as IGeometryCollection).AddGeometry(Clone(pGeoCol.get_Geometry(i)) as IGeometry, ref a, ref a);
                m_LineSymbol.Draw(polyline);
            }

            DrawMovedPoints(pGeoCol.get_Geometry(pGeoCol.GeometryCount - 1) as IPointCollection, point);

            m_LastPoint = new PointClass();
            m_LastPoint.PutCoords(point.X, point.Y);
        }

        private IPolyline GetPolyline(IPoint point1, IPoint point2)
        {
            object a = System.Reflection.Missing.Value;
            IPolyline line = new PolylineClass();
            IPath path = new PathClass();
            (path as IPointCollection).AddPoint(point1, ref a, ref a);
            (path as IPointCollection).AddPoint(point2, ref a, ref a);

            (line as IGeometryCollection).AddGeometry(path as IGeometry, ref a, ref a);
            return line;
        }

        private void DrawMovedPoints(IPointCollection points, IPoint point)
        {
            if (points.PointCount == 0)
                return;
            
            IPolyline line;
            m_LineSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;             
            m_LineSymbol.SetupDC(m_HDC, m_Display.DisplayTransformation);

            //活动部份            

            if (points.PointCount > 0)
            {
                if (m_LastPoint != null)
                {
                    line = GetPolyline(points.get_Point(points.PointCount - 1), m_LastPoint);
                    m_LineSymbol.Draw(line as IGeometry);
                }
                line = GetPolyline(points.get_Point(points.PointCount - 1), point);
                m_LineSymbol.Draw(line as IGeometry);
            }

            m_LineSymbol.ROP2 = esriRasterOpCode.esriROPCopyPen;                       
            m_LineSymbol.SetupDC(m_HDC, m_Display.DisplayTransformation);
        }

        public void FinishPart()
        {
            if (m_Polyline == null)
                return;
            IGeometryCollection pGeoCol = m_Polyline as IGeometryCollection;
            
            IGeometry pGeometry = new PathClass();
            object a = System.Reflection.Missing.Value;
            pGeoCol.AddGeometry(pGeometry, ref a, ref a);

            //undolist
            for (int i = m_UndoList.Count - 1; i >= 0; i--)
            {
                if (m_UndoList[i] is IPoint)
                {
                    m_UndoList.RemoveAt(i);
                }
                else
                    break;
            }
            m_UndoList.Add(pGeoCol as IGeometry);
        }

        private object Clone(object obj)
        {
            if (obj is ESRI.ArcGIS.esriSystem.IClone)
            {
                return (obj as ESRI.ArcGIS.esriSystem.IClone).Clone();
            }
            else
                return null;
        }

        public ESRI.ArcGIS.Geometry.IGeometry Stop()
        {
            if (m_Polyline == null)
                return null;
            
            IGeometry pGeometry = (m_Polyline as ESRI.ArcGIS.esriSystem.IClone).Clone() as IGeometry;
            m_Polyline = null;
            m_LastPoint = null;

            m_UndoList.Clear();
            m_RedoList.Clear();

            return pGeometry;
        }

        public void Refresh()
        { 
            object a = System.Reflection.Missing.Value;
            IGeometryCollection pGeoCol = m_Polyline as IGeometryCollection;

            for (int i = 0; i <= pGeoCol.GeometryCount - 1; i++)
            {
                IGeometry polyline = new PolylineClass();
                (polyline as IGeometryCollection).AddGeometry(pGeoCol.get_Geometry(i), ref a, ref a);
                m_LineSymbol.Draw(polyline);
            }

            m_LastPoint = null; 
        }

        public IGeometry Geometry
        {
            get { return m_Polyline; }
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

        #region IOperation 成员

        private ArrayList m_UndoList = new ArrayList();
        private ArrayList m_RedoList = new ArrayList();
    
        public bool CanRedo
        {
            get
            {
                return m_RedoList.Count > 0;
            }
        }

        public bool CanUndo
        {
            get
            {
                return m_UndoList.Count > 0;
            }
        }

        public void Do()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public string MenuString
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public void Redo()
        {
            if (m_RedoList.Count == 0)
                return;
            object obj = m_RedoList[m_RedoList.Count - 1];
            m_RedoList.RemoveAt(m_RedoList.Count - 1);
            object a = System.Reflection.Missing.Value;
            IGeometryCollection pGeoCol = m_Polyline as IGeometryCollection;
            if (obj is IPoint)
            {
                IPointCollection pts = pGeoCol.get_Geometry(pGeoCol.GeometryCount - 1) as IPointCollection;
                pts.AddPoint(obj as IPoint, ref a, ref a);

            }
            else if (obj is IPointCollection)
            {
                pGeoCol.AddGeometry(obj as IGeometry, ref a, ref a);
            }


            m_UndoList.Add(obj);
        }

        public void Undo()
        {
            if (m_UndoList.Count == 0)
                return;
            IGeometryCollection pGeoCol = m_Polyline as IGeometryCollection;
            object obj = m_UndoList[m_UndoList.Count - 1];
            m_UndoList.RemoveAt(m_UndoList.Count - 1);

            IPointCollection pts = pGeoCol.get_Geometry(pGeoCol.GeometryCount - 1) as IPointCollection;

            if (obj is IPoint)
            {
                pts.RemovePoints(pts.PointCount - 1, 1);

                m_RedoList.Add(obj);
            }
            else if (obj is IPointCollection)
            {
                if (pts.PointCount == 0)
                {
                    if (pGeoCol.GeometryCount > 2)
                        pGeoCol.RemoveGeometries(pGeoCol.GeometryCount - 2, 2);
                    else
                        pGeoCol.RemoveGeometries(pGeoCol.GeometryCount - 1, 1);

                    pts = new PathClass();
                    object a = System.Reflection.Missing.Value;
                    pGeoCol.AddGeometry(pts as IGeometry, ref a, ref a);

                }
                else
                    pGeoCol.RemoveGeometries(pGeoCol.GeometryCount - 1, 1);

                m_RedoList.Add(obj);
            }
        }
        #endregion
    }
}
