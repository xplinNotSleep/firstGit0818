using System.Threading;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.Utility.Display
{
    /// <summary>
    /// 2Dͼ����ʾ��
    /// </summary>
    public class Display2D
    {
        private IScreenDisplay m_ScreenDisplay;
       
        private int m_FlashInterval = 300;
        /// <summary>
        /// ��˸Ƶ�ʣ���λ�����룩
        /// </summary>
        public int FlashInterval
        {
            get { return m_FlashInterval; }
            set { m_FlashInterval = value; }
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="pDisplay">��Ļ��ʾ����</param>
        public Display2D(IScreenDisplay pDisplay)
        {
            this.m_ScreenDisplay = pDisplay;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="pDisplay">��Ļ��ʾ����</param>
        /// <param name="FlashInterval">��˸Ƶ�ʣ���λ�����룩</param>
        /// <param name="FlashEffect">��˸����</param>
        public Display2D(IScreenDisplay pDisplay, int FlashInterval, short FlashEffect)
        {
            this.m_ScreenDisplay = pDisplay;
            m_FlashInterval = FlashInterval;
            m_FlashEffect = FlashEffect;
        }

        private short m_FlashEffect = 4;
        /// <summary>
        /// ��ȡ��������˸����
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
        /// ��˸ָ����Ҫ��
        /// </summary>
        /// <param name="pFeature">Ҫ�ؼ�¼</param>
        public void FlashFeature(IFeature pFeature)
        {
            FlashGeometry(pFeature.Shape);
        }

        //public void FlashGeometry(IGeometry pGeometry)
        //{

        //}

        /// <summary>
        /// ��˸���ζ���
        /// </summary>
        /// <param name="pGeometry">���ζ���</param>
        public void FlashGeometry(IGeometry pGeometry)
        {
            m_ScreenDisplay.StartDrawing(0, -1);

            switch (pGeometry.GeometryType)
            {
                case esriGeometryType.esriGeometryPoint:
                case esriGeometryType.esriGeometryMultipoint:
                    //��˸��
                    FlashPoint(pGeometry);
                    break;      
                case esriGeometryType.esriGeometryLine:
                case esriGeometryType.esriGeometryPolyline:
                    //��˸��
                    FlashLine(pGeometry);
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    //��˸��
                    FlashPolygon(pGeometry);
                    break;
            }

            m_ScreenDisplay.FinishDrawing();  
        }

        /// <summary>
        /// ��˸�����
        /// </summary>
        /// <param name="pGeometry">����߶�����</param>
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
        /// ��˸�߶���
        /// </summary>
        /// <param name="pGeometry">�߻��߶��߶���</param>
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
        /// ��ȡ��Ҫ����˸��ʽ
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
        /// ��ȡ��Ҫ����˸��ʽ
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
        /// ��ȡ��Ҫ����˸��ʽ
        /// </summary>
        /// <returns></returns>
        public static ISimpleFillSymbol GetPolygonFlashSymbol()
        {
            ISimpleFillSymbol tSimpleSymbol = new SimpleFillSymbolClass();
            tSimpleSymbol.Outline = GetLineFlashSymbol();

            return tSimpleSymbol;
        }

        /// <summary>
        /// ��˸�����
        /// </summary>
        /// <param name="pGeometry">�����</param>
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
