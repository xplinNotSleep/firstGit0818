using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.Utility.Display
{
    /// <summary>
    /// 2D图形绘制类
    /// </summary>
    public class Drawing2D
    {
        private IActiveView m_ActiveView;        

        /// <summary>
        /// 设置视图对象
        /// </summary>
        public IActiveView ActiveView
        {
            set
            {
                this.m_ActiveView = value;
            }
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Drawing2D()
        {

        }

        /// <summary>
        /// 重载构造函数
        /// </summary>
        /// <param name="pActiveView">当前视图对象</param>
        public Drawing2D(IActiveView pActiveView)
        {
            this.m_ActiveView = pActiveView;            
        }

        /// <summary>
        /// 绘制图元
        /// </summary>
        /// <param name="pGeometry">几何对象</param>
        /// <param name="pSymbol">样式,为null时采用默认样式设置</param>
        public void DrawElement(IGeometry pGeometry,ISymbol pSymbol)
        {
            if (pGeometry.GeometryType == esriGeometryType.esriGeometryMultipoint ||
                pGeometry.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                DrawMarkerElement(pGeometry, pSymbol);
            }
            else if (pGeometry.GeometryType == esriGeometryType.esriGeometryLine ||
                pGeometry.GeometryType==esriGeometryType.esriGeometryPolyline)
            {
                DrawLineElement(pGeometry, pSymbol);
            }
            else
            {
                DrawPolyElement(pGeometry, pSymbol);
            }
        }

        /// <summary>
        /// 绘制点元素
        /// </summary>
        /// <param name="pGeometry">几何对象</param>
        /// <param name="pSymbol">样式,为null时采用默认样式设置</param>
        private void DrawMarkerElement(IGeometry pGeometry,ISymbol pSymbol)
        {
            IMarkerElement tMarkerElement = new MarkerElementClass();
            IMarkerSymbol tMarkerSymbol=pSymbol as IMarkerSymbol;

            if (tMarkerSymbol == null)
            {
                //进行样式设置

                //初始化颜色
                IRgbColor tRgbColor = new RgbColorClass();
                tRgbColor.Red = 255;

                tMarkerSymbol = new SimpleMarkerSymbolClass();
                tMarkerSymbol.Color = tRgbColor;
                (tMarkerSymbol as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSSquare;
            }

            tMarkerElement.Symbol = tMarkerSymbol;            

            IElement tElement = tMarkerElement as IElement;
            tElement.Geometry = pGeometry;

            this.m_ActiveView.GraphicsContainer.AddElement(tElement, 0);
        }

        /// <summary>
        /// 绘制线元素
        /// </summary>
        /// <param name="pGeometry">几何对象</param>
        /// <param name="pSymbol"></param>
        private void DrawLineElement(IGeometry pGeometry,ISymbol pSymbol)
        {
            //实例化线元素
            ILineElement tLineElement = new LineElementClass();
            ISimpleLineSymbol tLineSymbol = pSymbol as ISimpleLineSymbol;

            if (tLineSymbol == null)
            {
                //设置颜色为红色
                IRgbColor tRgbColor = new RgbColorClass();
                tRgbColor.Red = 0;
                tRgbColor.Green = 255;
                tRgbColor.Blue = 197;

                //初始化为简单线性样式
                tLineSymbol = new SimpleLineSymbolClass();
                tLineSymbol.Color = tRgbColor;
                tLineSymbol.Width = 2;
                tLineSymbol.Style = esriSimpleLineStyle.esriSLSDot;                
            }

            tLineElement.Symbol = tLineSymbol;           

            IElement tElement = tLineElement as IElement;
            tElement.Geometry = pGeometry;

            this.m_ActiveView.GraphicsContainer.AddElement(tElement, 0);  
        }

        /// <summary>
        /// 绘制面元素
        /// </summary>
        /// <param name="pGeometry">几何对象</param>
        /// <param name="pSymbol">样式,为null时采用默认样式设置</param>
        private void DrawPolyElement(IGeometry pGeometry, ISymbol pSymbol)
        {
            IFillShapeElement tFillElement = new PolygonElementClass();
            ISimpleFillSymbol tSimpleFillSymbol = pSymbol as ISimpleFillSymbol;

            if (pSymbol == null)
            {
                //设置颜色为红色
                IRgbColor tRgbColor = new RgbColorClass();
                tRgbColor.Red = 255;

                ILineSymbol tLineSymbol = new SimpleLineSymbolClass();
                tLineSymbol.Width = 2;
                tLineSymbol.Color = tRgbColor;

                tSimpleFillSymbol = new SimpleFillSymbolClass();
                tSimpleFillSymbol.Outline = tLineSymbol;
                tSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSCross;
                tSimpleFillSymbol.Color = tRgbColor;
            }

            tFillElement.Symbol = tSimpleFillSymbol;

            IElement tElement = tFillElement as IElement;
            tElement.Geometry = pGeometry;

            this.m_ActiveView.GraphicsContainer.AddElement(tElement, 0);           
        }
    }
}
