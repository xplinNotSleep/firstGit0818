using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.Utility.Display
{
    /// <summary>
    /// 2Dͼ�λ�����
    /// </summary>
    public class Drawing2D
    {
        private IActiveView m_ActiveView;        

        /// <summary>
        /// ������ͼ����
        /// </summary>
        public IActiveView ActiveView
        {
            set
            {
                this.m_ActiveView = value;
            }
        }

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public Drawing2D()
        {

        }

        /// <summary>
        /// ���ع��캯��
        /// </summary>
        /// <param name="pActiveView">��ǰ��ͼ����</param>
        public Drawing2D(IActiveView pActiveView)
        {
            this.m_ActiveView = pActiveView;            
        }

        /// <summary>
        /// ����ͼԪ
        /// </summary>
        /// <param name="pGeometry">���ζ���</param>
        /// <param name="pSymbol">��ʽ,Ϊnullʱ����Ĭ����ʽ����</param>
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
        /// ���Ƶ�Ԫ��
        /// </summary>
        /// <param name="pGeometry">���ζ���</param>
        /// <param name="pSymbol">��ʽ,Ϊnullʱ����Ĭ����ʽ����</param>
        private void DrawMarkerElement(IGeometry pGeometry,ISymbol pSymbol)
        {
            IMarkerElement tMarkerElement = new MarkerElementClass();
            IMarkerSymbol tMarkerSymbol=pSymbol as IMarkerSymbol;

            if (tMarkerSymbol == null)
            {
                //������ʽ����

                //��ʼ����ɫ
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
        /// ������Ԫ��
        /// </summary>
        /// <param name="pGeometry">���ζ���</param>
        /// <param name="pSymbol"></param>
        private void DrawLineElement(IGeometry pGeometry,ISymbol pSymbol)
        {
            //ʵ������Ԫ��
            ILineElement tLineElement = new LineElementClass();
            ISimpleLineSymbol tLineSymbol = pSymbol as ISimpleLineSymbol;

            if (tLineSymbol == null)
            {
                //������ɫΪ��ɫ
                IRgbColor tRgbColor = new RgbColorClass();
                tRgbColor.Red = 0;
                tRgbColor.Green = 255;
                tRgbColor.Blue = 197;

                //��ʼ��Ϊ��������ʽ
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
        /// ������Ԫ��
        /// </summary>
        /// <param name="pGeometry">���ζ���</param>
        /// <param name="pSymbol">��ʽ,Ϊnullʱ����Ĭ����ʽ����</param>
        private void DrawPolyElement(IGeometry pGeometry, ISymbol pSymbol)
        {
            IFillShapeElement tFillElement = new PolygonElementClass();
            ISimpleFillSymbol tSimpleFillSymbol = pSymbol as ISimpleFillSymbol;

            if (pSymbol == null)
            {
                //������ɫΪ��ɫ
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
