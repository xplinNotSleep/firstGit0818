using System;
using System.Windows.Forms;
using AG.COM.SDM.StyleManager;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// 渐变填充
	/// </summary>
	public partial class CtrlGradientFill : CtrlPropertyBase
	{
		/// <summary>
		/// 当前符号
		/// </summary>
		private IGradientFillSymbol m_pSymbol = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
		public CtrlGradientFill()
		{
			InitializeComponent();
		}

		private void CtrlGradientFill_Load(object sender, EventArgs e)
		{
			// 从ESRI.ServerStyle中读取颜色带
			this.comboBoxCRStyle.Items.Clear();
			StyleCommon.InitialColorCombox(this.comboBoxCRStyle);

			if (this.m_pCtrlSymbol != null)
			{
				m_pSymbol = m_pCtrlSymbol as IGradientFillSymbol;
				// 根据符号属性设置当前窗体控件值
				if (null != m_pSymbol)
				{
					this.numericUpDownAngle.Value = (decimal)m_pSymbol.GradientAngle;
					this.numericUpDownPercentage.Value = (decimal)(m_pSymbol.GradientPercentage * 100.0);
					this.numericUpDownIntervals.Value = (decimal)m_pSymbol.IntervalCount;
					this.comboBoxStyle.SelectedIndex = (int)m_pSymbol.Style;
					// 添加当前颜色带到颜色带下拉框
					IStyleGalleryItem pSGItem = new ServerStyleGalleryItemClass();
					pSGItem.Item = m_pSymbol.ColorRamp;
					this.comboBoxCRStyle.Items.Insert(0, pSGItem);
					this.comboBoxCRStyle.SelectedIndex = 0;
				}
			}
			m_bInitComplete = true;
		}

		#region 用户更改属性值
		/// <summary>
		/// 间隔
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownIntervals_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.IntervalCount = (int)this.numericUpDownIntervals.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		
        /// <summary>
		/// 百分比
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownPercentage_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.GradientPercentage = (double)this.numericUpDownPercentage.Value / 100.0;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		
        /// <summary>
		/// 角度
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownAngle_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.GradientAngle = (double)this.numericUpDownAngle.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		
        /// <summary>
		/// 样式
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBoxStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.Style = (esriGradientFillStyle)this.comboBoxStyle.SelectedIndex;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol,this.m_iLayerIndex);
		}
		
        /// <summary>
		/// 颜色样式
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBoxCRStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			IStyleGalleryItem pStyleGalleryItem = this.comboBoxCRStyle.SelectedItem as IStyleGalleryItem;
			if(null != pStyleGalleryItem)
			{
				IColorRamp pColorRamp = pStyleGalleryItem.Item as ESRI.ArcGIS.Display.IColorRamp;
				if (null != pColorRamp)
				{
					m_pSymbol.ColorRamp = pColorRamp;
					this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
				}
			}
		}
		
        /// <summary>
		/// 轮廓线
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelOutline_Click(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			frmSymbolSelector frm = new frmSymbolSelector();
			frm.SymbolType = SymbolType.stLineSymbol;
			frm.InitialSymbol = m_pSymbol.Outline as ISymbol;
			if (frm.ShowDialog() != DialogResult.OK) return;
			m_pSymbol.Outline = frm.SelectedSymbol as ILineSymbol;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		#endregion
	}
}
