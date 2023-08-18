using System.Threading;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.Utility.Display
{
    /// <summary>
    /// 2D图形显示类
    /// </summary>
    public class Display2D
    {
        private IScreenDisplay m_ScreenDisplay;
       
        private int m_FlashInterval = 300;
        /// <summary>
        /// 闪烁频率（单位：毫秒）
        /// </summary>
        public int FlashInterval
        {
            get { return m_FlashInterval; }
            set { m_FlashInterval = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pDisplay">屏幕显示对象</param>
        public Display2D(IScreenDisplay pDisplay)
        {
            this.m_ScreenDisplay = pDisplay;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pDisplay">屏幕显示对象</param>
        /// <param name="FlashInterval">闪烁频率（单位：毫秒）</param>
        /// <param name="FlashEffect">闪烁次数</param>
        public Display2D(IScreenDisplay pDisplay, int FlashInterval, short FlashEffect)
        {
            this.m_ScreenDisplay = pDisplay;
            m_FlashInterval = FlashInterval;
            m_FlashEffect = FlashEffect;
        }

        private short m_FlashEffect = 4;
        /// <summary>
        /// 获取或设置闪烁次数
        /// </summary>
        public short FlashEffect
        {
            get
            {
                return m_FlashEffect;
            }
            set
            {
                m_FlashEffect = value;
            }
        }

        /// <summary>
        /// 闪烁指定的要素
        /// </summary>
        /// <param name="pFeature">要素记录</param>
        public void FlashFeature(IFeature pFeature)
        {
            FlashGeometry(pFeature.Shape);
        }

        //public void FlashGeometry(IGeometry pGeometry)
        //{

        //}

        /// <summary>
        /// 闪烁几何对象
        /// </summary>
        /// <param name="pGeometry">几何对象</param>
        public void FlashGeometry(IGeometry pGeometry)
        {
            m_ScreenDisplay.StartDrawing(0, -1);

            switch (pGeometry.GeometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                case esriGeometryType.esriGeometryMultipoint:
                    //闪烁点
                    FlashPoint(pGeometry);
                    break;      
                case esriGeometryType.esriGeometryLine:
                case esriGeometryType.esriGeometryPolyline:
                    //闪烁线
                    FlashLine(pGeometry);
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    //闪烁面
                    FlashPolygon(pGeometry);
                    break;
            }

            m_ScreenDisplay.FinishDrawing();  
        }

        /// <summary>
        /// 闪烁点对象
        /// </summary>
        /// <param name="pGeometry">点或者多点对象</param>
        private void FlashPoint(IGeometry pGeometry)
        {          
            ISimpleMarkerSymbol tMarkSymbol = GetPointFlashSymbol();

            m_ScreenDisplay.SetSymbol(tMarkSymbol as ISymbol); 
     
            if (pGeometry.GeometryType == esriGeometryType.esriGeometryPoint)
            {
                for (short i = 0; i < m_FlashEffect; i++)
                {
                    m_ScreenDisplay.DrawPoint(pGeometry);
                    Thread.Sleep(m_FlashInterval);
                }
            }
            else
            {
                for (short i = 0; i < m_FlashEffect; i++)
                {
                    m_ScreenDisplay.DrawMultipoint(pGeometry);
                    Thread.Sleep(m_FlashInterval);
                }
            }        
        }

        /// <summary>
        /// 闪烁线对象
        /// </summary>
        /// <param name="pGeometry">线或者多线对象</param>
        private void FlashLine(IGeometry pGeometry)
        {
            ISimpleLineSymbol tSimpleSymbol = GetLineFlashSymbol();
          
            ISymbol tSymbol = tSimpleSymbol as ISymbol;
            m_ScreenDisplay.SetSymbol(tSymbol);

            for (short i = 0; i < m_FlashEffect; i++)
            {
                m_ScreenDisplay.DrawPolyline(pGeometry);
                Thread.Sleep(m_FlashInterval);
            }
        }

        /// <summary>
        /// 获取点要素闪烁样式
        /// </summary>
        /// <returns></returns>
        public static ISimpleMarkerSymbol GetPointFlashSymbol()
        {
            IRgbColor tRgbColor = new RgbColorClass();
            tRgbColor.Green = 255;
            tRgbColor.Blue = 255;

            ISimpleMarkerSymbol tMarkSymbol = new SimpleMarkerSymbolClass();
            tMarkSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
            tMarkSymbol.Color = tRgbColor;

            ISymbol tSymbol = tMarkSymbol as ISymbol;
            tSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;

            return tMarkSymbol;
        }

        /// <summary>
        /// 获取线要素闪烁样式
        /// </summary>
        /// <returns></returns>
        public static ISimpleLineSymbol GetLineFlashSymbol()
        {
            ISimpleLineSymbol tSimpleSymbol = new SimpleLineSymbolClass();
            tSimpleSymbol.Width = 4;
            (tSimpleSymbol as ISymbol).ROP2 = esriRasterOpCode.esriROPNot;         

            IRgbColor tRgbColor = new RgbColorClass();
            tRgbColor.Green = 255;
            tRgbColor.Blue = 255;
            tSimpleSymbol.Color = tRgbColor as IColor;

            return tSimpleSymbol;
        }

        /// <summary>
        /// 获取面要素闪烁样式
        /// </summary>
        /// <returns></returns>
        public static ISimpleFillSymbol GetPolygonFlashSymbol()
        {
            ISimpleFillSymbol tSimpleSymbol = new SimpleFillSymbolClass();
            tSimpleSymbol.Outline = GetLineFlashSymbol();

            return tSimpleSymbol;
        }

        /// <summary>
        /// 闪烁面对象
        /// </summary>
        /// <param name="pGeometry">面对象</param>
        private void FlashPolygon(IGeometry pGeometry)
        {
            ISimpleFillSymbol tSimpleSymbol = GetPolygonFlashSymbol();

            m_ScreenDisplay.SetSymbol(tSimpleSymbol as ISymbol);

            for (short i = 0; i < m_FlashEffect; i++)
            {
                m_ScreenDisplay.DrawPolygon(pGeometry);
                Thread.Sleep(m_FlashInterval);
            }           
        }     
    }
}
