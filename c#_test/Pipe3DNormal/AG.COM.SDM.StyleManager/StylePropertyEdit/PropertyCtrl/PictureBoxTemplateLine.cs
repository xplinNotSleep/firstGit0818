using System;
using System.Collections.Generic;
using System.Drawing;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// 线模板
	/// </summary>
	public class PictureBoxTemplateLine : System.Windows.Forms.PictureBox
	{
		#region 属性/字段
		/// <summary>
		/// 线模板项（bool值表示：是否为标记）
		/// </summary>
		private IList<bool> m_listPatternElement = new List<bool>();
		
        /// <summary>
		/// 边框上部坐标
		/// </summary>
		private readonly int m_iOutlineTop = 12;
		
        /// <summary>
		/// 移动方格点
		/// </summary>
		private Point m_bPointMoveableRect;
		
        /// <summary>
		/// 鼠标按下
		/// </summary>
		private bool m_bMouseDown = false;
		
        /// <summary>
		/// 获取长度
		/// </summary>
		public int ElementLength
		{
			get { return m_listPatternElement.Count; }
		}
		
        /// <summary>
		/// 获取线模板项个数
		/// </summary>
		public int PatternElementCount
		{
			get { return 0; }
		}
		#endregion
	
        /// <summary>
		/// 模板项改变
		/// </summary>
		public event EventHandler ElementChange;

		#region 模板项
		public void ClearPatternElements()
		{
			m_listPatternElement.Clear();
			this.Invalidate();
		}
		
        /// <summary>
		/// 添加线模板项
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
		/// 获取线模板项
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
		/// 获取片段信息
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

		#region 绘制相关
		/// <summary>
		/// 重写绘制事件
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			int iWidth = this.Bounds.Width - this.Bounds.Width % 50;
			// 绘制刻度
			Pen penScale = new Pen(Color.FromArgb(100, 100, 100), 1);
			penScale.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
			int iHeight = 0;
			for (int i = 0; i <= iWidth/10; i++)
			{
				if (i % 5 == 0) iHeight = 0;
				else iHeight = 5;
				e.Graphics.DrawLine(penScale, i * 10, iHeight, i * 10, 10);
			}
			// 绘制外框
			Pen penOutline = new Pen(Color.FromArgb(128, 128, 128), 1);
			penOutline.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
			e.Graphics.DrawLine(penOutline, 0, m_iOutlineTop, iWidth, m_iOutlineTop);
			e.Graphics.DrawLine(penOutline, 0, m_iOutlineTop, 0, m_iOutlineTop+10);
			e.Graphics.DrawLine(penOutline, 0, m_iOutlineTop + 10, iWidth, m_iOutlineTop + 10);
			e.Graphics.DrawLine(penOutline, iWidth, m_iOutlineTop, iWidth, m_iOutlineTop + 10);

			// 绘制方格
			DrawSquare(e);
			// 绘制最后一个可拖动的方格
			e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, 100, 100)), GetFillRectangle(m_listPatternElement.Count));

		}
		
        /// <summary>
		/// 绘制方格
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
		/// 获取填充矩形
		/// </summary>
		/// <param name="iIndex">索引</param>
		/// <returns>填充矩形</returns>
		private Rectangle GetFillRectangle(int iIndex)
		{
			Rectangle rect = new Rectangle(iIndex * 10 + 1, m_iOutlineTop + 1, 9, 9);
			return rect;
		}
		#endregion

		#region 鼠标事件
		/// <param name="e">包含事件数据的 <see cref="T:System.Windows.Forms.MouseEventArgs"/>。</param>
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

		/// <param name="e">包含事件数据的 <see cref="T:System.Windows.Forms.MouseEventArgs"/>。</param>
		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
			if (!m_bMouseDown) return;
			if (e.X > this.Bounds.Width||e.X<0) return;
			// 右移
			if (e.X - m_bPointMoveableRect.X > 10)
			{
				for (int i = 0; i < (e.X - m_bPointMoveableRect.X) / 10; i++)
				{
					m_listPatternElement.Add(false);
				}
			}
			// 左移
			else if (e.X - m_bPointMoveableRect.X < -10)
			{
				for (int i = 0; i < (m_bPointMoveableRect.X - e.X) / 10; i++)
				{
					m_listPatternElement.RemoveAt(m_listPatternElement.Count - 1);
				}
			}
			else return;
			// 重新获取可移动方格位置，刷新显示
			Rectangle rect = GetFillRectangle(m_listPatternElement.Count);
			int iX = m_bPointMoveableRect.X;
			int iY = m_bPointMoveableRect.Y;
			m_bPointMoveableRect.X = (rect.Right + rect.Left) / 2;
			m_bPointMoveableRect.Y = (rect.Bottom + rect.Top) / 2;
			ElementChange(this, null);
			this.Invalidate();
		}

		/// <param name="e">包含事件数据的 <see cref="T:System.Windows.Forms.MouseEventArgs"/>。</param>
		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{
			m_bMouseDown = false;
		}
		
        /// <summary>
		/// 获取当前鼠标所处位置对应的模板项索引
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
	/// 片段信息
	/// </summary>
	public class SegmentInfo
	{
		/// <summary>
		/// 是否为黑色
		/// </summary>
		private bool m_bBlack = false;
		
        /// <summary>
		/// 个数
		/// </summary>
		private int m_iCount = 0;

		public SegmentInfo(bool bBlack,int iCount)
		{
			m_bBlack = bBlack;
			m_iCount = iCount;
		}

		/// <summary>
		/// 获取或设置是否为黑色
		/// </summary>
		public bool Black
		{
			get { return m_bBlack; }
			set { m_bBlack = value; }
		}
		/// <summary>
        /// 获取或设置个数
		/// </summary>
		public int Count
		{
			get { return m_iCount; }
			set { m_iCount = value; }
		}
	}

	/// <summary>
	/// 线模板项
	/// </summary>
	public class PatternElement
	{
		/// <summary>
		/// 标记点个数
		/// </summary>
		private double m_dMark = -1.0;
		
        /// <summary>
		/// 间隔个数
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
        /// 获取或设置标记点个数
		/// </summary>
		public double Mark
		{
			get { return m_dMark; }
			set { m_dMark = value; }
		}
		/// <summary>
        /// 获取或设置间隔个数
		/// </summary>
		public double Gap
		{
			get { return m_dGap; }
			set { m_dGap = value; }
		}
		/// <summary>
        /// 获取标记和间隔长度
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
