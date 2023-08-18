using System;
using System.Collections.Generic;
using System.Drawing;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// ��ģ��
	/// </summary>
	public class PictureBoxTemplateLine : System.Windows.Forms.PictureBox
	{
		#region ����/�ֶ�
		/// <summary>
		/// ��ģ���boolֵ��ʾ���Ƿ�Ϊ��ǣ�
		/// </summary>
		private IList<bool> m_listPatternElement = new List<bool>();
		
        /// <summary>
		/// �߿��ϲ�����
		/// </summary>
		private readonly int m_iOutlineTop = 12;
		
        /// <summary>
		/// �ƶ������
		/// </summary>
		private Point m_bPointMoveableRect;
		
        /// <summary>
		/// ��갴��
		/// </summary>
		private bool m_bMouseDown = false;
		
        /// <summary>
		/// ��ȡ����
		/// </summary>
		public int ElementLength
		{
			get { return m_listPatternElement.Count; }
		}
		
        /// <summary>
		/// ��ȡ��ģ�������
		/// </summary>
		public int PatternElementCount
		{
			get { return 0; }
		}
		#endregion
	
        /// <summary>
		/// ģ����ı�
		/// </summary>
		public event EventHandler ElementChange;

		#region ģ����
		public void ClearPatternElements()
		{
			m_listPatternElement.Clear();
			this.Invalidate();
		}
		
        /// <summary>
		/// �����ģ����
		/// </summary>
		public void AddPatternElement(double dMark, double dGap)
		{
			for (int i = 0; i < (int)dMark; i++)
			{
				m_listPatternElement.Add(true);
			}
			for (int i = 0; i < (int)dGap; i++)
			{
				m_listPatternElement.Add(false);
			}
		}
		
        /// <summary>
		/// ��ȡ��ģ����
		/// </summary>
		/// <returns></returns>
		public IList<PatternElement> GetPatternElement()
		{
			IList<PatternElement> listPatternElement = new List<PatternElement>();
			IList<SegmentInfo> listSegmentInfo = GetSegmentInfo();
			for (int i = 0; i < listSegmentInfo.Count;i++ )
			{
				PatternElement pe = null;
				if(listSegmentInfo[i].Black)
				{
					if(i+1<listSegmentInfo.Count)
					{
						pe = new PatternElement(listSegmentInfo[i].Count, listSegmentInfo[i+1].Count);
						i++;
					}
					else
					{
						pe = new PatternElement(listSegmentInfo[i].Count, 0);
					}
				}
				else
				{
					pe = new PatternElement(0, listSegmentInfo[i].Count);
				}
				listPatternElement.Add(pe);
			}

			return listPatternElement;
		}
		
        /// <summary>
		/// ��ȡƬ����Ϣ
		/// </summary>
		/// <returns></returns>
		private IList<SegmentInfo> GetSegmentInfo()
		{
			IList<SegmentInfo> listSegmentInfo = new List<SegmentInfo>();
			SegmentInfo sInfo = null;
			for (int i = 0; i < m_listPatternElement.Count; i++)
			{
				if (m_listPatternElement[i])
				{
					if (null == sInfo) sInfo = new SegmentInfo(true, 1);
					else
					{
						if (sInfo.Black) sInfo.Count++;
						else
						{
							listSegmentInfo.Add(sInfo);
							sInfo = null;
							i--;
						}
					}
				}
				else
				{
					if (null == sInfo) sInfo = new SegmentInfo(false, 1);
					else
					{
						if (!sInfo.Black) sInfo.Count++;
						else
						{
							listSegmentInfo.Add(sInfo);
							sInfo = null;
							i--;
						}
					}
				}
				if(i == m_listPatternElement.Count-1)
				{
					if(null != sInfo) listSegmentInfo.Add(sInfo);
				}
			}
			return listSegmentInfo;
		}
		#endregion

		#region �������
		/// <summary>
		/// ��д�����¼�
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			int iWidth = this.Bounds.Width - this.Bounds.Width % 50;
			// ���ƿ̶�
			Pen penScale = new Pen(Color.FromArgb(100, 100, 100), 1);
			penScale.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
			int iHeight = 0;
			for (int i = 0; i <= iWidth/10; i++)
			{
				if (i % 5 == 0) iHeight = 0;
				else iHeight = 5;
				e.Graphics.DrawLine(penScale, i * 10, iHeight, i * 10, 10);
			}
			// �������
			Pen penOutline = new Pen(Color.FromArgb(128, 128, 128), 1);
			penOutline.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
			e.Graphics.DrawLine(penOutline, 0, m_iOutlineTop, iWidth, m_iOutlineTop);
			e.Graphics.DrawLine(penOutline, 0, m_iOutlineTop, 0, m_iOutlineTop+10);
			e.Graphics.DrawLine(penOutline, 0, m_iOutlineTop + 10, iWidth, m_iOutlineTop + 10);
			e.Graphics.DrawLine(penOutline, iWidth, m_iOutlineTop, iWidth, m_iOutlineTop + 10);

			// ���Ʒ���
			DrawSquare(e);
			// �������һ�����϶��ķ���
			e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, 100, 100)), GetFillRectangle(m_listPatternElement.Count));

		}
		
        /// <summary>
		/// ���Ʒ���
		/// </summary>
		/// <param name="e"></param>
		private void DrawSquare(System.Windows.Forms.PaintEventArgs e)
		{
			for (int i = 0; i < m_listPatternElement.Count;i++ )
			{
				if (m_listPatternElement[i]) e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 0)), GetFillRectangle(i));
				else e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255)), GetFillRectangle(i));
			}
		}
		
        /// <summary>
		/// ��ȡ������
		/// </summary>
		/// <param name="iIndex">����</param>
		/// <returns>������</returns>
		private Rectangle GetFillRectangle(int iIndex)
		{
			Rectangle rect = new Rectangle(iIndex * 10 + 1, m_iOutlineTop + 1, 9, 9);
			return rect;
		}
		#endregion

		#region ����¼�
		/// <param name="e">�����¼����ݵ� <see cref="T:System.Windows.Forms.MouseEventArgs"/>��</param>
		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button != System.Windows.Forms.MouseButtons.Left) return;
			int iIndex = GetIndexOfMousePos(e);
			if (iIndex < 0) return;
			if (iIndex == m_listPatternElement.Count)
			{
				m_bPointMoveableRect = new Point(e.X, e.Y);
				m_bMouseDown = true;
				return;
			}
			m_listPatternElement[iIndex] = !m_listPatternElement[iIndex];
			ElementChange(this, null);
			this.Invalidate();
		}

		/// <param name="e">�����¼����ݵ� <see cref="T:System.Windows.Forms.MouseEventArgs"/>��</param>
		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
			if (!m_bMouseDown) return;
			if (e.X > this.Bounds.Width||e.X<0) return;
			// ����
			if (e.X - m_bPointMoveableRect.X > 10)
			{
				for (int i = 0; i < (e.X - m_bPointMoveableRect.X) / 10; i++)
				{
					m_listPatternElement.Add(false);
				}
			}
			// ����
			else if (e.X - m_bPointMoveableRect.X < -10)
			{
				for (int i = 0; i < (m_bPointMoveableRect.X - e.X) / 10; i++)
				{
					m_listPatternElement.RemoveAt(m_listPatternElement.Count - 1);
				}
			}
			else return;
			// ���»�ȡ���ƶ�����λ�ã�ˢ����ʾ
			Rectangle rect = GetFillRectangle(m_listPatternElement.Count);
			int iX = m_bPointMoveableRect.X;
			int iY = m_bPointMoveableRect.Y;
			m_bPointMoveableRect.X = (rect.Right + rect.Left) / 2;
			m_bPointMoveableRect.Y = (rect.Bottom + rect.Top) / 2;
			ElementChange(this, null);
			this.Invalidate();
		}

		/// <param name="e">�����¼����ݵ� <see cref="T:System.Windows.Forms.MouseEventArgs"/>��</param>
		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{
			m_bMouseDown = false;
		}
		
        /// <summary>
		/// ��ȡ��ǰ�������λ�ö�Ӧ��ģ��������
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		private int GetIndexOfMousePos(System.Windows.Forms.MouseEventArgs e)
		{
			for (int i = 0; i < m_listPatternElement.Count + 1;i++ )
			{
				if(GetFillRectangle(i).Contains(e.X,e.Y)) return i;
			}
			return -1;
		}
		#endregion
	}

	/// <summary>
	/// Ƭ����Ϣ
	/// </summary>
	public class SegmentInfo
	{
		/// <summary>
		/// �Ƿ�Ϊ��ɫ
		/// </summary>
		private bool m_bBlack = false;
		
        /// <summary>
		/// ����
		/// </summary>
		private int m_iCount = 0;

		public SegmentInfo(bool bBlack,int iCount)
		{
			m_bBlack = bBlack;
			m_iCount = iCount;
		}

		/// <summary>
		/// ��ȡ�������Ƿ�Ϊ��ɫ
		/// </summary>
		public bool Black
		{
			get { return m_bBlack; }
			set { m_bBlack = value; }
		}
		/// <summary>
        /// ��ȡ�����ø���
		/// </summary>
		public int Count
		{
			get { return m_iCount; }
			set { m_iCount = value; }
		}
	}

	/// <summary>
	/// ��ģ����
	/// </summary>
	public class PatternElement
	{
		/// <summary>
		/// ��ǵ����
		/// </summary>
		private double m_dMark = -1.0;
		
        /// <summary>
		/// �������
		/// </summary>
		private double m_dGap = 0.0;

		public PatternElement()
		{
		}
		
        public PatternElement(double dMark, double dGap)
		{
			m_dMark = dMark;
			m_dGap = dGap;
		}

		/// <summary>
        /// ��ȡ�����ñ�ǵ����
		/// </summary>
		public double Mark
		{
			get { return m_dMark; }
			set { m_dMark = value; }
		}
		/// <summary>
        /// ��ȡ�����ü������
		/// </summary>
		public double Gap
		{
			get { return m_dGap; }
			set { m_dGap = value; }
		}
		/// <summary>
        /// ��ȡ��Ǻͼ������
		/// </summary>
		public int Length
		{
			get
			{
				if (m_dMark < 0.0) return 0;
				return (int)m_dMark + (int)m_dGap;
			}
		}
	}
}
