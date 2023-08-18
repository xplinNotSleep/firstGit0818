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
    /// 符号库公共类
    /// </summary>
    public class StyleCommon
	{
		#region 符号分类
		/// <summary>
		/// 符号分类名称中英文对照表
		/// </summary>
		private static Hashtable m_htStyleGallery = new Hashtable();
		
        /// <summary>
		/// 符号分类名称中英文对照表
		/// </summary>
		public static Hashtable StyleGallery
		{
			get { return m_htStyleGallery; }
		}
		// 
		static StyleCommon()
		{
			// 添加需要处理的符号分类，如果添加其他类型符号的编辑去掉注释即可
            m_htStyleGallery.Add("Reference Systems", "参考系统");
			//m_htStyleGallery.Add("Maplex Labels","Maplex标注");
			//m_htStyleGallery.Add( "Shadows","阴影");
			//m_htStyleGallery.Add( "Area Patches","区域区块");
			//m_htStyleGallery.Add("Line Patches","线区块");
			//m_htStyleGallery.Add("Labels", "标注");
			//m_htStyleGallery.Add("Representation Markers","制图表达点符号");
			//m_htStyleGallery.Add("North Arrows","指北针");
			//m_htStyleGallery.Add("Scale Bars","比例尺");
			//m_htStyleGallery.Add("Legend Items","图例项");
			//m_htStyleGallery.Add("Scale Texts","比例尺文字");
			//m_htStyleGallery.Add("Color Ramps","色阶");
			//m_htStyleGallery.Add("Borders","边界");
			//m_htStyleGallery.Add( "Backgrounds","背景");
			//m_htStyleGallery.Add("Colors","颜色");
			//m_htStyleGallery.Add("Vectorization Settings", "矢量化设置");
			m_htStyleGallery.Add("Fill Symbols", "填充符号");
			m_htStyleGallery.Add("Line Symbols", "线符号");
			m_htStyleGallery.Add("Marker Symbols", "点符号");
			m_htStyleGallery.Add("Text Symbols", "文本符号");
			//m_htStyleGallery.Add("Representation Rules","制图表达规则");
			//m_htStyleGallery.Add("Hatches", "Hatches");
		}
		
        /// <summary>
		/// 获取分类的中文名称
		/// </summary>
		/// <param name="strValue"></param>
		/// <returns></returns>
		public static string GetStyleGalleryName(string strValue)
		{
			foreach ( DictionaryEntry de in m_htStyleGallery )
			{
				if (de.Value.ToString() == strValue) return de.Key.ToString();
			}
			return "未知分类";
		}
		#endregion
		/// <summary>
		/// 符号生成缩略图
		/// </summary>
		/// <param name="pSymbol"></param>
		/// <param name="iwidth"></param>
		/// <param name="iheight"></param>
		/// <returns></returns>
		public static System.Drawing.Bitmap SymbolToBitmp(ESRI.ArcGIS.Display.ISymbol pSymbol, int iwidth, int iheight)

		{

			//根据高宽创建图象

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

			//将符号的形状绘制到图象中

			pSymbol.SetupDC((int)hdc, pDisplayTransformation);

			pSymbol.Draw(pGeo);

			pSymbol.ResetDC();

			gImage.ReleaseHdc(hdc);

			gImage.Dispose();

			return bmp;



		}

		public static ESRI.ArcGIS.Geometry.IGeometry CreateSymShape(ISymbol pSymbol, IEnvelope pEnvelope)

		{// 根据传入的符号以及外包矩形区域返回对应的几何空间实体（点，线、面）

			//判断是否为“点”符号

			ESRI.ArcGIS.Display.IMarkerSymbol IMarkerSym;

			IMarkerSym = pSymbol as IMarkerSymbol;

			if (IMarkerSym != null)

			{
				// 为“点”符号则返回IEnvelope的中心点

				IArea pArea;

				pArea = pEnvelope as IArea;

				return pArea.Centroid as IGeometry;

			}

			else

			{
				//判断是否为“线”符号

				ESRI.ArcGIS.Display.ILineSymbol IlineSym;

				ESRI.ArcGIS.Display.ITextSymbol ITextSym;

				IlineSym = pSymbol as ILineSymbol;

				ITextSym = pSymbol as ITextSymbol;

				if (IlineSym != null || ITextSym != null)

				{
					//返回45度的对角线

					ESRI.ArcGIS.Geometry.IPolyline IpLine;

					IpLine = new PolylineClass();

					IpLine.FromPoint = pEnvelope.LowerLeft;

					IpLine.ToPoint = pEnvelope.UpperRight;

					return IpLine as IGeometry;

				}

				else

				{
					//直接返回一个IEnvelope矩形区域

					return pEnvelope as IGeometry;

				}

			}

		}
		#region 获取颜色
		/// <summary>
		/// 获取RGB颜色
		/// </summary>
		/// <param name="clrPicker">控件获取的颜色</param>
		/// <returns>RGB颜色</returns>
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
		/// 根据IColor获取Color颜色
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

		#region 其他
		/// <summary>
		/// 初始化颜色Combox控件
		/// </summary>
		/// <param name="pStyleCombox"></param>
		public static void InitialColorCombox(StyleComboBox pStyleCombox)
		{
			pStyleCombox.Items.Clear();
			//颜色带文件
			String strStyleFile;
            strStyleFile = CommonConstString.STR_StylePath + @"\ESRI.ServerStyle";
			string strStyleGalleryClass;
			strStyleGalleryClass = "Color Ramps";
			IStyleGallery pStyleGallery = new ServerStyleGalleryClass();
			IStyleGalleryStorage pStyleGalleryStorage;
			pStyleGalleryStorage = pStyleGallery as IStyleGalleryStorage;
			pStyleGalleryStorage.AddFile(strStyleFile);

			//获取StyleGalleryClass
			IStyleGalleryClass pStyleGalleryClass;
			pStyleGalleryClass = null;
			for (int i = 0; i < pStyleGallery.ClassCount; i++)
			{
				pStyleGalleryClass = pStyleGallery.get_Class(i);
				if (pStyleGalleryClass.Name == strStyleGalleryClass)
					break;
			}

			pStyleCombox.StyleGalleryClass = pStyleGalleryClass;

			//添加颜色带
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

			//释放非托管变量
			System.Runtime.InteropServices.Marshal.ReleaseComObject(pEnumStyleGalleryItem);
		}
	
        /// <summary>
		/// 获取多图层符号中的某图层符号
		/// </summary>
		/// <param name="pMultiLayerSymbol">多图层符号</param>
		/// <param name="iIndex">索引</param>
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
		/// 获取符号图层个数
		/// </summary>
		/// <param name="pMultiLayerSymbol">多图层符号</param>
		/// <returns>符号图层个数</returns>
		public static int GetLayerCount(object pMultiLayerSymbol)
		{
			if (pMultiLayerSymbol is IMultiLayerMarkerSymbol) return (pMultiLayerSymbol as IMultiLayerMarkerSymbol).LayerCount;
			else if (pMultiLayerSymbol is IMultiLayerLineSymbol) return (pMultiLayerSymbol as IMultiLayerLineSymbol).LayerCount;
			else if (pMultiLayerSymbol is IMultiLayerFillSymbol) return (pMultiLayerSymbol as IMultiLayerFillSymbol).LayerCount;
			else if (pMultiLayerSymbol is ILineDecoration) return (pMultiLayerSymbol as ILineDecoration).ElementCount;
			return 0;
		}

		#endregion

		#region 通过符号库中的IStyleGalleryItem 和 IStyleGalleryClass类别生成图片预览
		/// <summary>
		///  通过符号库中的IStyleGalleryItem 和 IStyleGalleryClass类别生成图片预览
		/// </summary>
		/// <param name="iWidth">宽度</param>
		/// <param name="iHeight">高度</param>
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
			//生成预览
			System.IntPtr hdc = new IntPtr();
			hdc = gImage.GetHdc();
			mStyleGlyCs.Preview(mStyleGlyItem.Item, hdc.ToInt32(), ref rect);
			gImage.ReleaseHdc(hdc);
			gImage.Dispose();
			return bmp;
		}
	
        /// <summary>
		/// 通过符号库中的IStyleGalleryItem 和 IStyleGalleryClass类别生成图片预览（不指定宽度）
		/// </summary>
		/// <param name="iHeight">高度 (宽度自动调整)</param>
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

		#region 根据符号生成图片
		/// <summary>
		/// 根据符号生成图片
		/// </summary>
		/// <param name="iWidth">宽度</param>
		/// <param name="iHeight">高度</param>
		/// <param name="pSymbol">符号</param>
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
		/// 从符号中创建指定大小的图片
		/// </summary>
		public static Image CreatePictureFromSymbol(object pSymbol, double dblWidth, double dblHeight, double dblGap)
		{
			if (pSymbol is ISymbol) return CreatePictureFromSymbol(pSymbol as ISymbol, dblWidth, dblHeight, dblGap);
			else if (pSymbol is ILineDecorationElement) return CreatePictureFromSymbol(pSymbol as ILineDecorationElement, (int)dblWidth, (int)dblHeight);
			return null;
		}
	
        /// <summary>
		/// 从符号中创建指定大小的图片
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
		/// 获取图形
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
		/// 获取坐标转换信息
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

			// 坐标转换
			IDisplayTransformation pDisplayTransformation = new DisplayTransformationClass();
			pDisplayTransformation.VisibleBounds = pEnvelope;
			pDisplayTransformation.Bounds = pEnvelope;
			pDisplayTransformation.set_DeviceFrame(ref DeviceRect);
			pDisplayTransformation.Resolution = gImage.DpiX;

			return pDisplayTransformation;
		}
	
        /// <summary>
		/// 从符号中创建指定大小的图片
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
		/// 从符号中创建指定大小的图片
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

		#region 绘制符号
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
