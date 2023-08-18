using System;
using System.Drawing;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.StyleManager
{
	/// <summary>
	/// SymbolTransBitmp ��ժҪ˵����
	/// </summary>
	public class StyleGalleryItemView
	{
		public StyleGalleryItemView()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		///  ͨ�����ſ��е�IStyleGalleryItem �� IStyleGalleryClass�������ͼƬԤ��
		/// </summary>
		/// <param name="iWidth">���</param>
		/// <param name="iHeight">�߶�</param>
		/// <param name="mStyleGlyCs">IStyleGalleryClass</param>
		/// <param name="mStyleGlyItem">IStyleGalleryItem</param>
		/// <returns></returns>
		public static System.Drawing.Bitmap StyleGalleryItemToBmp(
			int iWidth,
			int iHeight,
			ESRI.ArcGIS.Display.IStyleGalleryClass mStyleGlyCs,
			ESRI.ArcGIS.Display.IStyleGalleryItem  mStyleGlyItem)
		{
			Bitmap bmp = new Bitmap(iWidth,iHeight);
			Graphics gImage = Graphics.FromImage(bmp);

			tagRECT rect = new tagRECT();			
			rect.right  = bmp.Width;
			rect.bottom = bmp.Height;
			//����Ԥ��
			System.IntPtr hdc = new IntPtr();
			hdc = gImage.GetHdc();			
			mStyleGlyCs.Preview(mStyleGlyItem.Item,hdc.ToInt32(),ref rect);
			gImage.ReleaseHdc(hdc);
			gImage.Dispose();
			
			return bmp;
			
		}
		
        /// <summary>
		/// ͨ�����ſ��е�IStyleGalleryItem �� IStyleGalleryClass�������ͼƬԤ������ָ����ȣ�
		/// </summary>
		/// <param name="iHeight">�߶� (����Զ�����)</param>
		/// <param name="mStyleGlyCs">IStyleGalleryClass</param>
		/// <param name="mStyleGlyItem">IStyleGalleryItem</param>
		/// <returns></returns>
		public static System.Drawing.Bitmap StyleGalleryItemToBmp(
			int iHeight,
			ESRI.ArcGIS.Display.IStyleGalleryClass mStyleGlyCs,
			ESRI.ArcGIS.Display.IStyleGalleryItem  mStyleGlyItem)
		{
			int iWidth;
			iWidth =(int)(mStyleGlyCs.PreviewRatio * iHeight);
			return StyleGalleryItemToBmp(iWidth,iHeight,mStyleGlyCs,mStyleGlyItem);
		}

        /// <summary>
        /// ������ת��ΪBmpͼƬ
        /// </summary>
        /// <param name="pSymbol"></param>����
        /// <param name="pWidth"></param>
        /// <param name="pHeight"></param>
        /// <param name="pDimention"></param>
        /// <returns></returns>
        public static Bitmap CreateBmp(ISymbol pSymbol, int pWidth, int pHeight, int pDimention)
        {
            Bitmap Bmp = new Bitmap(pWidth, pHeight);
            Graphics GImage = Graphics.FromImage(Bmp);
            GImage.Clear(Color.White);

            Double Dpi = GImage.DpiX;

            IGeometry pGeo;
            IEnvelope pEnvelope = new EnvelopeClass();
            pEnvelope.PutCoords(0, 0, (double)Bmp.Width, (double)Bmp.Height);
            switch (pDimention)
            {
                case 0:             //�����
                    IArea pArea;
                    pArea = pEnvelope as IArea;
                    pGeo = pArea.Centroid as IGeometry;
                    break;
                case 1:             //�߷���
                    IPoint pPoint = null;
                    pPoint = new ESRI.ArcGIS.Geometry.Point();
                    IPolyline pPolyline = new PolylineClass();
                    pPoint.X = pEnvelope.XMin;
                    pPoint.Y = (pEnvelope.YMin + pEnvelope.YMax) / 2;
                    pPolyline.FromPoint = pPoint;
                    pPoint = new ESRI.ArcGIS.Geometry.Point();
                    pPoint.X = pEnvelope.XMax;
                    pPoint.Y = (pEnvelope.YMin + pEnvelope.YMax) / 2;
                    pPolyline.ToPoint = pPoint;
                    pGeo = pPolyline as IGeometry;
                    break;
                default:            //����Ż�����
                    pGeo = pEnvelope as IGeometry;
                    break;
            }

            tagRECT DeviceRect;
            DeviceRect.left = 0;
            DeviceRect.right = Bmp.Width;
            DeviceRect.top = 0;
            DeviceRect.bottom = Bmp.Height;

            //���û�ͼ�豸ת��
            IDisplayTransformation pDisplayTransformation = new DisplayTransformationClass();
            pDisplayTransformation.VisibleBounds = pEnvelope;
            pDisplayTransformation.Bounds = pEnvelope;
            pDisplayTransformation.set_DeviceFrame(ref DeviceRect);
            pDisplayTransformation.Resolution = Dpi;

            System.IntPtr hdc = new IntPtr();
            hdc = GImage.GetHdc();

            pSymbol.SetupDC((int)hdc, pDisplayTransformation);
            pSymbol.Draw(pGeo);
            pSymbol.ResetDC();
            GImage.ReleaseHdc(hdc);
            GImage.Dispose();
            return Bmp;
        }
	}
}
