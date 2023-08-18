using System;
using System.Collections;
using System.Drawing;
using AG.COM.SDM.Utility;
using AG.COM.SDM.Utility.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.StylePropertyEdit
{
    /// <summary>
    /// ���ſ⹫����
    /// </summary>
    public class StyleCommon
	{
		#region ���ŷ���
		/// <summary>
		/// ���ŷ���������Ӣ�Ķ��ձ�
		/// </summary>
		private static Hashtable m_htStyleGallery = new Hashtable();
		
        /// <summary>
		/// ���ŷ���������Ӣ�Ķ��ձ�
		/// </summary>
		public static Hashtable StyleGallery
		{
			get { return m_htStyleGallery; }
		}
		// 
		static StyleCommon()
		{
			// �����Ҫ����ķ��ŷ��࣬�������������ͷ��ŵı༭ȥ��ע�ͼ���
            m_htStyleGallery.Add("Reference Systems", "�ο�ϵͳ");
			//m_htStyleGallery.Add("Maplex Labels","Maplex��ע");
			//m_htStyleGallery.Add( "Shadows","��Ӱ");
			//m_htStyleGallery.Add( "Area Patches","��������");
			//m_htStyleGallery.Add("Line Patches","������");
			//m_htStyleGallery.Add("Labels", "��ע");
			//m_htStyleGallery.Add("Representation Markers","��ͼ�������");
			//m_htStyleGallery.Add("North Arrows","ָ����");
			//m_htStyleGallery.Add("Scale Bars","������");
			//m_htStyleGallery.Add("Legend Items","ͼ����");
			//m_htStyleGallery.Add("Scale Texts","����������");
			//m_htStyleGallery.Add("Color Ramps","ɫ��");
			//m_htStyleGallery.Add("Borders","�߽�");
			//m_htStyleGallery.Add( "Backgrounds","����");
			//m_htStyleGallery.Add("Colors","��ɫ");
			//m_htStyleGallery.Add("Vectorization Settings", "ʸ��������");
			m_htStyleGallery.Add("Fill Symbols", "������");
			m_htStyleGallery.Add("Line Symbols", "�߷���");
			m_htStyleGallery.Add("Marker Symbols", "�����");
			m_htStyleGallery.Add("Text Symbols", "�ı�����");
			//m_htStyleGallery.Add("Representation Rules","��ͼ������");
			//m_htStyleGallery.Add("Hatches", "Hatches");
		}
		
        /// <summary>
		/// ��ȡ�������������
		/// </summary>
		/// <param name="strValue"></param>
		/// <returns></returns>
		public static string GetStyleGalleryName(string strValue)
		{
			foreach ( DictionaryEntry de in m_htStyleGallery )
			{
				if (de.Value.ToString() == strValue) return de.Key.ToString();
			}
			return "δ֪����";
		}
		#endregion
		/// <summary>
		/// ������������ͼ
		/// </summary>
		/// <param name="pSymbol"></param>
		/// <param name="iwidth"></param>
		/// <param name="iheight"></param>
		/// <returns></returns>
		public static System.Drawing.Bitmap SymbolToBitmp(ESRI.ArcGIS.Display.ISymbol pSymbol, int iwidth, int iheight)

		{

			//���ݸ߿���ͼ��

			Bitmap bmp = new Bitmap(iwidth, iheight);

			Graphics gImage = Graphics.FromImage(bmp);

			gImage.Clear(Color.White);

			double dpi = gImage.DpiX;

			IEnvelope pEnvelope = new EnvelopeClass();

			pEnvelope.PutCoords(0, 0, (double)bmp.Width, (double)bmp.Height);

			tagRECT deviceRect;

			deviceRect.left = 0;

			deviceRect.right = bmp.Width;

			deviceRect.top = 0;

			deviceRect.bottom = bmp.Height;

			IDisplayTransformation pDisplayTransformation = new DisplayTransformationClass();

			pDisplayTransformation.VisibleBounds = pEnvelope;

			pDisplayTransformation.Bounds = pEnvelope;

			pDisplayTransformation.set_DeviceFrame(ref deviceRect);

			pDisplayTransformation.Resolution = dpi;



			IGeometry pGeo = CreateSymShape(pSymbol, pEnvelope);

			System.IntPtr hdc = new IntPtr();

			hdc = gImage.GetHdc();

			//�����ŵ���״���Ƶ�ͼ����

			pSymbol.SetupDC((int)hdc, pDisplayTransformation);

			pSymbol.Draw(pGeo);

			pSymbol.ResetDC();

			gImage.ReleaseHdc(hdc);

			gImage.Dispose();

			return bmp;



		}

		public static ESRI.ArcGIS.Geometry.IGeometry CreateSymShape(ISymbol pSymbol, IEnvelope pEnvelope)

		{// ���ݴ���ķ����Լ�����������򷵻ض�Ӧ�ļ��οռ�ʵ�壨�㣬�ߡ��棩

			//�ж��Ƿ�Ϊ���㡱����

			ESRI.ArcGIS.Display.IMarkerSymbol IMarkerSym;

			IMarkerSym = pSymbol as IMarkerSymbol;

			if (IMarkerSym != null)

			{
				// Ϊ���㡱�����򷵻�IEnvelope�����ĵ�

				IArea pArea;

				pArea = pEnvelope as IArea;

				return pArea.Centroid as IGeometry;

			}

			else

			{
				//�ж��Ƿ�Ϊ���ߡ�����

				ESRI.ArcGIS.Display.ILineSymbol IlineSym;

				ESRI.ArcGIS.Display.ITextSymbol ITextSym;

				IlineSym = pSymbol as ILineSymbol;

				ITextSym = pSymbol as ITextSymbol;

				if (IlineSym != null || ITextSym != null)

				{
					//����45�ȵĶԽ���

					ESRI.ArcGIS.Geometry.IPolyline IpLine;

					IpLine = new PolylineClass();

					IpLine.FromPoint = pEnvelope.LowerLeft;

					IpLine.ToPoint = pEnvelope.UpperRight;

					return IpLine as IGeometry;

				}

				else

				{
					//ֱ�ӷ���һ��IEnvelope��������

					return pEnvelope as IGeometry;

				}

			}

		}
		#region ��ȡ��ɫ
		/// <summary>
		/// ��ȡRGB��ɫ
		/// </summary>
		/// <param name="clrPicker">�ؼ���ȡ����ɫ</param>
		/// <returns>RGB��ɫ</returns>
		public static IColor GetRgbColor(Color clrPicker)
		{
			RgbColor clr = new RgbColorClass();
			clr.Red = clrPicker.R;
			clr.Green = clrPicker.G;
			clr.Blue = clrPicker.B;
			clr.Transparency = clrPicker.A;
			return clr;
		}
		
        /// <summary>
		/// ����IColor��ȡColor��ɫ
		/// </summary>
		/// <param name="pColor"></param>
		/// <returns></returns>
		public static Color GetColor(IColor pColor)
		{
			IRgbColor clr = pColor as IRgbColor;
            if (null != clr) return Color.FromArgb(clr.Transparency, clr.Red, clr.Green, clr.Blue);
			return Color.Empty;
		}
		#endregion

		#region ����
		/// <summary>
		/// ��ʼ����ɫCombox�ؼ�
		/// </summary>
		/// <param name="pStyleCombox"></param>
		public static void InitialColorCombox(StyleComboBox pStyleCombox)
		{
			pStyleCombox.Items.Clear();
			//��ɫ���ļ�
			String strStyleFile;
            strStyleFile = CommonConstString.STR_StylePath + @"\ESRI.ServerStyle";
			string strStyleGalleryClass;
			strStyleGalleryClass = "Color Ramps";
			IStyleGallery pStyleGallery = new ServerStyleGalleryClass();
			IStyleGalleryStorage pStyleGalleryStorage;
			pStyleGalleryStorage = pStyleGallery as IStyleGalleryStorage;
			pStyleGalleryStorage.AddFile(strStyleFile);

			//��ȡStyleGalleryClass
			IStyleGalleryClass pStyleGalleryClass;
			pStyleGalleryClass = null;
			for (int i = 0; i < pStyleGallery.ClassCount; i++)
			{
				pStyleGalleryClass = pStyleGallery.get_Class(i);
				if (pStyleGalleryClass.Name == strStyleGalleryClass)
					break;
			}

			pStyleCombox.StyleGalleryClass = pStyleGalleryClass;

			//�����ɫ��
			IEnumStyleGalleryItem pEnumStyleGalleryItem = new EnumServerStyleGalleryItemClass();
			pEnumStyleGalleryItem = pStyleGallery.get_Items(strStyleGalleryClass, strStyleFile, "");
			pEnumStyleGalleryItem.Reset();
			IStyleGalleryItem pEnumItem;
			pEnumItem = pEnumStyleGalleryItem.Next();
			while (pEnumItem != null)
			{
				pStyleCombox.Items.Add(pEnumItem);
				pEnumItem = pEnumStyleGalleryItem.Next();
			}

			//�ͷŷ��йܱ���
			System.Runtime.InteropServices.Marshal.ReleaseComObject(pEnumStyleGalleryItem);
		}
	
        /// <summary>
		/// ��ȡ��ͼ������е�ĳͼ�����
		/// </summary>
		/// <param name="pMultiLayerSymbol">��ͼ�����</param>
		/// <param name="iIndex">����</param>
		/// <returns></returns>
		public static object GetLayerSymbol(object pMultiLayerSymbol,int iIndex)
		{
			if (pMultiLayerSymbol is ITextBackground) return pMultiLayerSymbol;
			if (iIndex < 0) return null;
			if (pMultiLayerSymbol is IMultiLayerMarkerSymbol)
			{
				if ((pMultiLayerSymbol as IMultiLayerMarkerSymbol).LayerCount > iIndex) return (pMultiLayerSymbol as IMultiLayerMarkerSymbol).get_Layer(iIndex);
			}
			else if (pMultiLayerSymbol is IMultiLayerLineSymbol)
			{
				if ((pMultiLayerSymbol as IMultiLayerLineSymbol).LayerCount > iIndex) return (pMultiLayerSymbol as IMultiLayerLineSymbol).get_Layer(iIndex);
			}
			else if (pMultiLayerSymbol is IMultiLayerFillSymbol)
			{
				if ((pMultiLayerSymbol as IMultiLayerFillSymbol).LayerCount > iIndex) return (pMultiLayerSymbol as IMultiLayerFillSymbol).get_Layer(iIndex);
			}
			else if (pMultiLayerSymbol is ILineDecoration)
			{
				if ((pMultiLayerSymbol as ILineDecoration).ElementCount > iIndex) return (pMultiLayerSymbol as ILineDecoration).get_Element(iIndex);
			}
			return null;
		}
	
        /// <summary>
		/// ��ȡ����ͼ�����
		/// </summary>
		/// <param name="pMultiLayerSymbol">��ͼ�����</param>
		/// <returns>����ͼ�����</returns>
		public static int GetLayerCount(object pMultiLayerSymbol)
		{
			if (pMultiLayerSymbol is IMultiLayerMarkerSymbol) return (pMultiLayerSymbol as IMultiLayerMarkerSymbol).LayerCount;
			else if (pMultiLayerSymbol is IMultiLayerLineSymbol) return (pMultiLayerSymbol as IMultiLayerLineSymbol).LayerCount;
			else if (pMultiLayerSymbol is IMultiLayerFillSymbol) return (pMultiLayerSymbol as IMultiLayerFillSymbol).LayerCount;
			else if (pMultiLayerSymbol is ILineDecoration) return (pMultiLayerSymbol as ILineDecoration).ElementCount;
			return 0;
		}

		#endregion

		#region ͨ�����ſ��е�IStyleGalleryItem �� IStyleGalleryClass�������ͼƬԤ��
		/// <summary>
		///  ͨ�����ſ��е�IStyleGalleryItem �� IStyleGalleryClass�������ͼƬԤ��
		/// </summary>
		/// <param name="iWidth">���</param>
		/// <param name="iHeight">�߶�</param>
		/// <param name="mStyleGlyCs">IStyleGalleryClass</param>
		/// <param name="mStyleGlyItem">IStyleGalleryItem</param>
		/// <returns></returns>
		public static System.Drawing.Bitmap StyleGalleryItemToBmp(int iWidth,int iHeight,IStyleGalleryClass mStyleGlyCs,IStyleGalleryItem mStyleGlyItem)
		{
			Bitmap bmp = new Bitmap(iWidth, iHeight);
			Graphics gImage = Graphics.FromImage(bmp);

			tagRECT rect = new tagRECT();
			rect.right = bmp.Width;
			rect.bottom = bmp.Height;
			//����Ԥ��
			System.IntPtr hdc = new IntPtr();
			hdc = gImage.GetHdc();
			mStyleGlyCs.Preview(mStyleGlyItem.Item, hdc.ToInt32(), ref rect);
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
		public static System.Drawing.Bitmap StyleGalleryItemToBmp(int iHeight,IStyleGalleryClass mStyleGlyCs,IStyleGalleryItem mStyleGlyItem)
		{
			int iWidth;
			iWidth = (int)(mStyleGlyCs.PreviewRatio * iHeight);
			return StyleGalleryItemToBmp(iWidth, iHeight, mStyleGlyCs, mStyleGlyItem);
		}
		#endregion

		#region ���ݷ�������ͼƬ
		/// <summary>
		/// ���ݷ�������ͼƬ
		/// </summary>
		/// <param name="iWidth">���</param>
		/// <param name="iHeight">�߶�</param>
		/// <param name="pSymbol">����</param>
		/// <returns></returns>
		public static System.Drawing.Bitmap SymbolToBmp(int iWidth,int iHeight,ISymbol pSymbol)
		{
			if (null == pSymbol) return null;
			Bitmap bmp = new Bitmap(iWidth, iHeight);
			Graphics gImage = Graphics.FromImage(bmp);
			gImage.Dispose();
			return bmp;
		}

		/// <summary>
		/// �ӷ����д���ָ����С��ͼƬ
		/// </summary>
		public static Image CreatePictureFromSymbol(object pSymbol, double dblWidth, double dblHeight, double dblGap)
		{
			if (pSymbol is ISymbol) return CreatePictureFromSymbol(pSymbol as ISymbol, dblWidth, dblHeight, dblGap);
			else if (pSymbol is ILineDecorationElement) return CreatePictureFromSymbol(pSymbol as ILineDecorationElement, (int)dblWidth, (int)dblHeight);
			return null;
		}
	
        /// <summary>
		/// �ӷ����д���ָ����С��ͼƬ
		/// </summary>
		private static Image CreatePictureFromSymbol(ILineDecorationElement pLineDecorationElement, int w, int h)
		{
			Bitmap bitmap = new Bitmap(w, h);
			Graphics gImage = Graphics.FromImage(bitmap);

			IDisplayTransformation pDisplayTransformation = GetDisplayTransformation(gImage, w, h);
			IGeometry pGeometry = GetGeometry(w, h);

			IntPtr hdc = new IntPtr();
			hdc = gImage.GetHdc();
			pLineDecorationElement.Draw(hdc.ToInt32(), pDisplayTransformation, pGeometry);
			gImage.ReleaseHdc(hdc);
			gImage.Dispose();
			return bitmap;

		}
	
        /// <summary>
		/// ��ȡͼ��
		/// </summary>
		/// <param name="w"></param>
		/// <param name="h"></param>
		/// <returns></returns>
		static IGeometry GetGeometry(int w, int h)
		{
			IEnvelope pEnvelope = new EnvelopeClass();
			pEnvelope.PutCoords(0, 0, w, h);
			IPoint pFromPoint = new PointClass();
			IPoint pToPoint = new PointClass();
			pFromPoint.X = pEnvelope.XMin + 6;
			pFromPoint.Y = pEnvelope.YMax / 2;
			pToPoint.X = pEnvelope.XMax - 6;
			pToPoint.Y = pEnvelope.YMax / 2;
			IPolyline pPolyline = new PolylineClass();
			pPolyline.FromPoint = pFromPoint;
			pPolyline.ToPoint = pToPoint;
			return pPolyline as IGeometry;
		}
		
        /// <summary>
		/// ��ȡ����ת����Ϣ
		/// </summary>
		/// <param name="gImage"></param>
		/// <param name="w"></param>
		/// <param name="h"></param>
		/// <returns></returns>
		static IDisplayTransformation GetDisplayTransformation(Graphics gImage,int w, int h)
		{
			IEnvelope pEnvelope = new EnvelopeClass();
			pEnvelope.PutCoords(0, 0, w, h);
			tagRECT DeviceRect;
			DeviceRect.left = 0;
			DeviceRect.right = w;
			DeviceRect.top = 0;
			DeviceRect.bottom = h;

			// ����ת��
			IDisplayTransformation pDisplayTransformation = new DisplayTransformationClass();
			pDisplayTransformation.VisibleBounds = pEnvelope;
			pDisplayTransformation.Bounds = pEnvelope;
			pDisplayTransformation.set_DeviceFrame(ref DeviceRect);
			pDisplayTransformation.Resolution = gImage.DpiX;

			return pDisplayTransformation;
		}
	
        /// <summary>
		/// �ӷ����д���ָ����С��ͼƬ
		/// </summary>
		private static Image CreatePictureFromSymbol(ISymbol pSymbol, double dblWidth, double dblHeight, double dblGap)
		{
			Bitmap bitmap = new Bitmap((int)dblWidth, (int)dblHeight);
			Graphics gImage = Graphics.FromImage(bitmap);
			IntPtr hdc = new IntPtr();
			hdc = gImage.GetHdc();
			DrawToDC(hdc.ToInt32(), dblWidth, dblHeight, dblGap, pSymbol, false);

			gImage.ReleaseHdc(hdc);
			gImage.Dispose();
			return bitmap;
		}
	
        /// <summary>
		/// �ӷ����д���ָ����С��ͼƬ
		/// </summary>
		public static Image CreatePictureFromSymbol(ISymbol pSymbol, double dblWidth, double dblHeight, double dblGap, bool blnLine)
		{
			Bitmap bitmap = new Bitmap((int)dblWidth, (int)dblHeight);
			Graphics gImage = Graphics.FromImage(bitmap);
			IntPtr hdc = new IntPtr();
			hdc = gImage.GetHdc();
			DrawToDC(hdc.ToInt32(), dblWidth, dblHeight, dblGap, pSymbol, blnLine);

			gImage.ReleaseHdc(hdc);
			gImage.Dispose();
			return bitmap;
		}

		#endregion

		#region ���Ʒ���
		private static void DrawToDC(int hDC, double dblWidth, double dblHeight, double dblGap, ISymbol pSymbol, bool blnLine)
		{
			IEnvelope pEnvelope = new EnvelopeClass();
			pEnvelope.PutCoords(dblGap, dblGap, dblWidth - dblGap, dblHeight - dblGap);
			ITransformation pTransformation = CreateTransFromDC(hDC, dblWidth, dblHeight);

			IGeometry pGeom = CreateSymShape(pSymbol, pEnvelope, blnLine);

			pSymbol.SetupDC(hDC, pTransformation);
			bool bSkipDraw = false;
			if (pSymbol is IPictureFillSymbol)
			{
				if ((pSymbol as IPictureFillSymbol).Picture == null) bSkipDraw = true;
			}
			else if(pSymbol is IPictureLineSymbol)
			{
				if ((pSymbol as IPictureLineSymbol).Picture == null) bSkipDraw = true;
			}
			else if (pSymbol is IPictureMarkerSymbol)
			{
				if ((pSymbol as IPictureMarkerSymbol).Picture == null) bSkipDraw = true;
			}
			if (!bSkipDraw) pSymbol.Draw(pGeom);
			pSymbol.ResetDC();
		}
	
        private static ITransformation CreateTransFromDC(int hDC, double dblWidth, double dblHeight)
		{
			IEnvelope pBoundsEnvelope = new EnvelopeClass();
			pBoundsEnvelope.PutCoords(0, 0, dblWidth, dblHeight);

			tagRECT deviceRect = new tagRECT();
			deviceRect.left = 0;
			deviceRect.top = 0;
			deviceRect.right = (int)dblWidth;
			deviceRect.bottom = (int)dblHeight;

			IDisplayTransformation pDisplayTransformation = new DisplayTransformationClass();
			pDisplayTransformation.VisibleBounds = pBoundsEnvelope;
			pDisplayTransformation.Bounds = pBoundsEnvelope;
			pDisplayTransformation.set_DeviceFrame(ref deviceRect);
			pDisplayTransformation.Resolution = 96;

			return pDisplayTransformation as ITransformation;
		}
	
        private static IGeometry CreateSymShape(ISymbol pSymbol, IEnvelope pEnvelope, bool blnLine)
		{
			if (pSymbol is IMarkerSymbol)
			{
				IArea pArea = pEnvelope as IArea;
				return pArea.Centroid;
			}
			else if (pSymbol is ILineSymbol || pSymbol is ITextSymbol)
			{
				if (blnLine)
				{
					IPointCollection pPC = new PolylineClass();
					IPoint pPoint = new PointClass();
					pPoint.PutCoords(pEnvelope.XMin, pEnvelope.YMax);
					object obj = Type.Missing;
					pPC.AddPoint(pPoint, ref obj, ref obj);
					pPoint = new PointClass();
					pPoint.PutCoords(pEnvelope.XMin + pEnvelope.Width / 3, pEnvelope.YMin);
					obj = Type.Missing;
					pPC.AddPoint(pPoint, ref obj, ref obj);
					pPoint = new PointClass();
					pPoint.PutCoords(pEnvelope.XMax - pEnvelope.Width / 3, pEnvelope.YMax);
					obj = Type.Missing;
					pPC.AddPoint(pPoint, ref obj, ref obj);
					pPoint = new PointClass();
					pPoint.PutCoords(pEnvelope.XMax, pEnvelope.YMin);
					obj = Type.Missing;
					pPC.AddPoint(pPoint, ref obj, ref obj);

					return pPC as IPolyline;
				}
				else
				{
					IPolyline pPolyline = new PolylineClass();
					IPoint pFromPoint = new PointClass();
					pFromPoint.PutCoords(pEnvelope.XMin, pEnvelope.YMin + pEnvelope.Height / 2);
					IPoint pToPoint = new PointClass();
					pToPoint.PutCoords(pEnvelope.XMax, pEnvelope.YMin + pEnvelope.Height / 2);
					pPolyline.FromPoint = pFromPoint;
					pPolyline.ToPoint = pToPoint;
					return pPolyline;
				}
			}
			else
				return pEnvelope;
		}
		#endregion
	}
}
