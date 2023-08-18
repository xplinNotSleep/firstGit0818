using System;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// 填充属性
	/// </summary>
	public partial class CtrlFillProperties : CtrlPropertyBase
	{
		/// <summary>
		/// 当前符号
		/// </summary>
		private IFillProperties m_pSymbol = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
		public CtrlFillProperties()
		{
			InitializeComponent();
		}

		private void CtrlFillProperties_Load(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol != null)
			{
				m_pSymbol = m_pCtrlSymbol as IFillProperties;
				// 根据符号属性设置当前窗体控件值
				if (null != m_pSymbol) 
				{
					this.numericUpDownXOffset.Value = (decimal)m_pSymbol.XOffset;
					this.numericUpDownYOffset.Value = (decimal)m_pSymbol.YOffset;
					this.numericUpDownXSeparation.Value = (decimal)m_pSymbol.XSeparation;
					this.numericUpDownYSeparation.Value = (decimal)m_pSymbol.YSeparation;
				}
			}
			m_bInitComplete = true;
		}

		#region 用户更改属性值
		/// <summary>
		/// 偏移X
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownXOffset_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.XOffset = (double)this.numericUpDownXOffset.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		
        /// <summary>
		/// 偏移Y
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownYOffset_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.YOffset = (double)this.numericUpDownYOffset.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		
        /// <summary>
		/// 分割间距X
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownXSeparation_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.XSeparation = (double)this.numericUpDownXSeparation.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		
        /// <summary>
		/// 分割间距Y
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownYSeparation_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.YSeparation = (double)this.numericUpDownYSeparation.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		#endregion
	}
}
