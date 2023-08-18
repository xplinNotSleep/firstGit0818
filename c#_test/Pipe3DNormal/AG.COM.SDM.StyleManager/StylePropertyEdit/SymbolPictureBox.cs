using System;
using System.Drawing;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM
{
	/// <summary>
	/// 符号图片控件
	/// </summary>
	public class SymbolPictureBox : System.Windows.Forms.PictureBox
	{
		#region 字段/属性
		/// <summary>
		/// 当前编辑的符号
		/// </summary>
		private object m_pSymbol = null;
		
        /// <summary>
		/// 是否显示十字线
		/// </summary>
		private bool m_bShowReticle = false;
		
        /// <summary>
		/// 缩放比例尺
		/// </summary>
		private double m_dScale = 1.0;
		
        /// <summary>
		/// 是否为直线
		/// </summary>
		private bool m_bBeeline = true;

		/// <summary>
		/// 获取或设置当前编辑的符号
		/// </summary>
		public object Symbol
		{
			set { m_pSymbol = value; this.Invalidate(); }
			get { return m_pSymbol; }
		}

		/// <summary>
        /// 获取或设置是否显示十字线
		/// </summary>
		public bool ShowReticle
		{
			set { m_bShowReticle = value;}
			get { return m_bShowReticle; }
		}
		/// <summary>
        /// 获取或设置缩放比例尺
		/// </summary>
		public double Scale
		{
			set { m_dScale = value; }
			get { return m_dScale; }
		}
		/// <summary>
        /// 获取或设置是否为直线
		/// </summary>
		public bool Beeline
		{
			set { m_bBeeline = value; }
			get { return m_bBeeline; }
		}
		#endregion

		#region 绘制相关
		/// <summary>
		/// 重写绘制事件
		/// </summary>
		/// <param name="pe"></param>
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe)
		{
			// 绘制符号
			if (m_pSymbol is ISymbol)
			{
				// 绘制十字线
				if (m_bShowReticle)
				{
					Pen penReticle = new Pen(Color.FromArgb(100,0,100), 1);
					penReticle.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
					pe.Graphics.DrawLine(penReticle, 0, this.Bounds.Height / 2-1, this.Bounds.Width, this.Bounds.Height / 2-1);
					pe.Graphics.DrawLine(penReticle, this.Bounds.Width / 2-1, 0, this.Bounds.Width / 2-1, this.Bounds.Height);
				}
				// 绘制符号
				DrawSymbol(pe.Graphics, this.Bounds, m_pSymbol as ISymbol);
				// 绘制文本符号中间点
				if (m_pSymbol is ITextSymbol)
				{
					SolidBrush blackBrush = new SolidBrush(Color.Black);
					pe.Graphics.FillEllipse(blackBrush, this.Bounds.Width / 2.0f, this.Bounds.Height / 2.0f, 7.0f, 7.0f);
				}
			}
			// 绘制线装饰
			else if (m_pSymbol is ILineDecoration)
			{
				DrawLineDecoration(pe.Graphics, this.Bounds, m_pSymbol as ILineDecoration);
			}
			// 文本背景
			else if(m_pSymbol is ITextBackground )
			{
				DrawTextBackground(pe.Graphics, this.Bounds, m_pSymbol as ITextBackground);
			}
		}
		
        /// <summary>
		/// 绘制符号
		/// </summary>
		/// <param name="g">图形对象</param>
		/// <param name="r">范围</param>
		/// <param name="symbol">符号</param>
		public void DrawSymbol(Graphics g, Rectangle r, ISymbol symbol)
		{
			//获取绘图范围
			int w = r.Width;
			int h = r.Height;
			IEnvelope pEnvelope = new EnvelopeClass();
			pEnvelope.PutCoords(0, 0, w, h);

			//声明坐标转换
			tagRECT DeviceRect;
			DeviceRect.left = 0;
			DeviceRect.right = w;
			DeviceRect.top = 0;
			DeviceRect.bottom = h;
			IDisplayTransformation pDisplayTransformation = new DisplayTransformationClass();
			pDisplayTransformation.VisibleBounds = pEnvelope;
			pDisplayTransformation.Bounds = pEnvelope;
			pDisplayTransformation.set_DeviceFrame(ref DeviceRect);
			pDisplayTransformation.Resolution = g.DpiX*m_dScale;

			int hdc = (int)g.GetHdc();
			symbol.SetupDC(hdc, pDisplayTransformation);
			symbol.ROP2 = esriRasterOpCode.esriROPCopyPen;
			IGeometry pGeometry = null;
			// 面符号
			if (symbol is IFillSymbol)
			{
				IEnvelope env = pEnvelope;
				env.XMin = 6;
				env.XMax = r.Width - 6;
				env.YMin = 6;
				env.YMax = r.Height - 6;
				pGeometry = pEnvelope as IGeometry;
			}
			// 线符号
			else if (symbol is ILineSymbol)
			{
				// 显示为直线
				if (m_bBeeline)
				{
					IPoint pFromPoint = new PointClass();
					IPoint pToPoint = new PointClass();
					pFromPoint.X = pEnvelope.XMin + 6;
					pFromPoint.Y = pEnvelope.YMax / 2;
					pToPoint.X = pEnvelope.XMax - 6;
					pToPoint.Y = pEnvelope.YMax / 2;
					IPolyline pPolyline = new PolylineClass();
					pPolyline.FromPoint = pFromPoint;
					pPolyline.ToPoint = pToPoint;
					pGeometry = pPolyline as IGeometry;
				}
				// 显示为折线
				else 
				{
					IPoint pPoint1 = new PointClass();
					IPoint pPoint2 = new PointClass();
					IPoint pPoint3 = new PointClass();
					IPoint pPoint4 = new PointClass();

					pPoint1.X = 6;
					pPoint1.Y = pEnvelope.YMax - 6;

					pPoint2.X = pEnvelope.XMax / 3.0;
					pPoint2.Y = 6;

					pPoint3.X = pEnvelope.XMax * 2.0 / 3.0;
					pPoint3.Y = pEnvelope.YMax - 6;

					pPoint4.X = pEnvelope.XMax - 6;
					pPoint4.Y = 6;

					IPointCollection pPointCollection = new PolylineClass();
					object missing = Type.Missing;
					pPointCollection.AddPoint(pPoint1, ref missing, ref missing);
					pPointCollection.AddPoint(pPoint2, ref missing, ref missing);
					pPointCollection.AddPoint(pPoint3, ref missing, ref missing);
					pPointCollection.AddPoint(pPoint4, ref missing, ref missing);
					pGeometry = pPointCollection as IGeometry;
				}
			}
			// 文本、点
			else
			{
                if (symbol is ITextSymbol)
                {
                    ITextSymbol pTextSymbol = symbol as ITextSymbol;
                    if (string.IsNullOrEmpty(pTextSymbol.Text)) pTextSymbol.Text = "AaBbYyZz";
                }
				IArea pArea = pEnvelope as IArea;
				IPoint pt = pArea.Centroid;
				pGeometry = pt as IGeometry;
			}
			symbol.Draw(pGeometry);
			symbol.ResetDC();
			g.ReleaseHdc();
		}
		
        /// <summary>
		/// 绘制线装饰
		/// </summary>
		/// <param name="g">图形对象</param>
		/// <param name="r">范围</param>
		/// <param name="pLineDecoration">线装饰</param>
		public void DrawLineDecoration(Graphics g, Rectangle r, ILineDecoration pLineDecoration)
		{
			//获取绘图范围
			int w = r.Width;
			int h = r.Height;
			IEnvelope pEnvelope = new EnvelopeClass();
			pEnvelope.PutCoords(0, 0, w, h);

			//声明坐标转换
			tagRECT DeviceRect;
			DeviceRect.left = 0;
			DeviceRect.right = w;
			DeviceRect.top = 0;
			DeviceRect.bottom = h;
			IDisplayTransformation pDisplayTransformation = new DisplayTransformationClass();
			pDisplayTransformation.VisibleBounds = pEnvelope;
			pDisplayTransformation.Bounds = pEnvelope;
			pDisplayTransformation.set_DeviceFrame(ref DeviceRect);
			pDisplayTransformation.Resolution = g.DpiX*m_dScale;

			int hdc = (int)g.GetHdc();
			IGeometry pGeometry = null;
			// 显示为直线
			if (m_bBeeline)
			{
				IPoint pFromPoint = new PointClass();
				IPoint pToPoint = new PointClass();
				pFromPoint.X = pEnvelope.XMin + 6;
				pFromPoint.Y = pEnvelope.YMax / 2;
				pToPoint.X = pEnvelope.XMax - 6;
				pToPoint.Y = pEnvelope.YMax / 2;
				IPolyline pPolyline = new PolylineClass();
				pPolyline.FromPoint = pFromPoint;
				pPolyline.ToPoint = pToPoint;
				pGeometry = pPolyline as IGeometry;
			}
			// 显示为折线
			else 
			{
				IPoint pPoint1 = new PointClass();
				IPoint pPoint2 = new PointClass();
				IPoint pPoint3 = new PointClass();
				IPoint pPoint4 = new PointClass();

				pPoint1.X = 6;
				pPoint1.Y = pEnvelope.YMax - 6;

				pPoint2.X = pEnvelope.XMax / 3.0;
				pPoint2.Y = 6;

				pPoint3.X = pEnvelope.XMax * 2.0 / 3.0;
				pPoint3.Y = pEnvelope.YMax - 6;

				pPoint4.X = pEnvelope.XMax - 6;
				pPoint4.Y = 6;

				IPointCollection pPointCollection = new PolylineClass();
				object missing = Type.Missing;
				pPointCollection.AddPoint(pPoint1, ref missing, ref missing);
				pPointCollection.AddPoint(pPoint2, ref missing, ref missing);
				pPointCollection.AddPoint(pPoint3, ref missing, ref missing);
				pPointCollection.AddPoint(pPoint4, ref missing, ref missing);
				pGeometry = pPointCollection as IGeometry;
			}
			pLineDecoration.Draw(hdc,pDisplayTransformation,pGeometry);
			g.ReleaseHdc();
		}
	
        /// <summary>
		/// 绘制文本背景
		/// </summary>
		/// <param name="g">图形对象</param>
		/// <param name="r">范围</param>
		/// <param name="pTextBackground">文本背景</param>
		public void DrawTextBackground(Graphics g, Rectangle r, ITextBackground pTextBackground)
		{
			IFormattedTextSymbol pSymbol = new TextSymbolClass();
			pSymbol.Background = pTextBackground;
			DrawSymbol(g, r, pSymbol as ISymbol);
		}
		#endregion
	}
}
