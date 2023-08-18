using System;
using System.Drawing;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM
{
	/// <summary>
	/// ����ͼƬ�ؼ�
	/// </summary>
	public class SymbolPictureBox : System.Windows.Forms.PictureBox
	{
		#region �ֶ�/����
		/// <summary>
		/// ��ǰ�༭�ķ���
		/// </summary>
		private object m_pSymbol = null;
		
        /// <summary>
		/// �Ƿ���ʾʮ����
		/// </summary>
		private bool m_bShowReticle = false;
		
        /// <summary>
		/// ���ű�����
		/// </summary>
		private double m_dScale = 1.0;
		
        /// <summary>
		/// �Ƿ�Ϊֱ��
		/// </summary>
		private bool m_bBeeline = true;

		/// <summary>
		/// ��ȡ�����õ�ǰ�༭�ķ���
		/// </summary>
		public object Symbol
		{
			set { m_pSymbol = value; this.Invalidate(); }
			get { return m_pSymbol; }
		}

		/// <summary>
        /// ��ȡ�������Ƿ���ʾʮ����
		/// </summary>
		public bool ShowReticle
		{
			set { m_bShowReticle = value;}
			get { return m_bShowReticle; }
		}
		/// <summary>
        /// ��ȡ���������ű�����
		/// </summary>
		public double Scale
		{
			set { m_dScale = value; }
			get { return m_dScale; }
		}
		/// <summary>
        /// ��ȡ�������Ƿ�Ϊֱ��
		/// </summary>
		public bool Beeline
		{
			set { m_bBeeline = value; }
			get { return m_bBeeline; }
		}
		#endregion

		#region �������
		/// <summary>
		/// ��д�����¼�
		/// </summary>
		/// <param name="pe"></param>
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe)
		{
			// ���Ʒ���
			if (m_pSymbol is ISymbol)
			{
				// ����ʮ����
				if (m_bShowReticle)
				{
					Pen penReticle = new Pen(Color.FromArgb(100,0,100), 1);
					penReticle.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
					pe.Graphics.DrawLine(penReticle, 0, this.Bounds.Height / 2-1, this.Bounds.Width, this.Bounds.Height / 2-1);
					pe.Graphics.DrawLine(penReticle, this.Bounds.Width / 2-1, 0, this.Bounds.Width / 2-1, this.Bounds.Height);
				}
				// ���Ʒ���
				DrawSymbol(pe.Graphics, this.Bounds, m_pSymbol as ISymbol);
				// �����ı������м��
				if (m_pSymbol is ITextSymbol)
				{
					SolidBrush blackBrush = new SolidBrush(Color.Black);
					pe.Graphics.FillEllipse(blackBrush, this.Bounds.Width / 2.0f, this.Bounds.Height / 2.0f, 7.0f, 7.0f);
				}
			}
			// ������װ��
			else if (m_pSymbol is ILineDecoration)
			{
				DrawLineDecoration(pe.Graphics, this.Bounds, m_pSymbol as ILineDecoration);
			}
			// �ı�����
			else if(m_pSymbol is ITextBackground )
			{
				DrawTextBackground(pe.Graphics, this.Bounds, m_pSymbol as ITextBackground);
			}
		}
		
        /// <summary>
		/// ���Ʒ���
		/// </summary>
		/// <param name="g">ͼ�ζ���</param>
		/// <param name="r">��Χ</param>
		/// <param name="symbol">����</param>
		public void DrawSymbol(Graphics g, Rectangle r, ISymbol symbol)
		{
			//��ȡ��ͼ��Χ
			int w = r.Width;
			int h = r.Height;
			IEnvelope pEnvelope = new EnvelopeClass();
			pEnvelope.PutCoords(0, 0, w, h);

			//��������ת��
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
			// �����
			if (symbol is IFillSymbol)
			{
				IEnvelope env = pEnvelope;
				env.XMin = 6;
				env.XMax = r.Width - 6;
				env.YMin = 6;
				env.YMax = r.Height - 6;
				pGeometry = pEnvelope as IGeometry;
			}
			// �߷���
			else if (symbol is ILineSymbol)
			{
				// ��ʾΪֱ��
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
				// ��ʾΪ����
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
			// �ı�����
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
		/// ������װ��
		/// </summary>
		/// <param name="g">ͼ�ζ���</param>
		/// <param name="r">��Χ</param>
		/// <param name="pLineDecoration">��װ��</param>
		public void DrawLineDecoration(Graphics g, Rectangle r, ILineDecoration pLineDecoration)
		{
			//��ȡ��ͼ��Χ
			int w = r.Width;
			int h = r.Height;
			IEnvelope pEnvelope = new EnvelopeClass();
			pEnvelope.PutCoords(0, 0, w, h);

			//��������ת��
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
			// ��ʾΪֱ��
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
			// ��ʾΪ����
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
		/// �����ı�����
		/// </summary>
		/// <param name="g">ͼ�ζ���</param>
		/// <param name="r">��Χ</param>
		/// <param name="pTextBackground">�ı�����</param>
		public void DrawTextBackground(Graphics g, Rectangle r, ITextBackground pTextBackground)
		{
			IFormattedTextSymbol pSymbol = new TextSymbolClass();
			pSymbol.Background = pTextBackground;
			DrawSymbol(g, r, pSymbol as ISymbol);
		}
		#endregion
	}
}
