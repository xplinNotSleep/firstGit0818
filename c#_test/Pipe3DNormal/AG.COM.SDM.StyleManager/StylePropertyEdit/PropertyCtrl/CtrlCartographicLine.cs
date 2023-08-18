using System;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// 制图线
	/// </summary>
	public partial class CtrlCartographicLine : CtrlPropertyBase
	{
		/// <summary>
		/// 当前符号
		/// </summary>
		private ICartographicLineSymbol m_pSymbol = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
		public CtrlCartographicLine()
		{
			InitializeComponent();
		}

		private void CtrlCartographicLine_Load(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol != null)
			{
				m_pSymbol = m_pCtrlSymbol as ICartographicLineSymbol;
				// 根据符号属性设置当前窗体控件值
				if (null != m_pSymbol)
				{
					this.colorPickerSymbol.Value = StyleCommon.GetColor(m_pSymbol.Color);
					this.numericUpDownWidth.Value = (decimal)m_pSymbol.Width;

					// 线头
					this.radioButtonButt.Checked = m_pSymbol.Cap == esriLineCapStyle.esriLCSButt;
					this.radioButtonRound.Checked = m_pSymbol.Cap == esriLineCapStyle.esriLCSRound;
					this.radioButtonSquare.Checked = m_pSymbol.Cap == esriLineCapStyle.esriLCSSquare;
					// 线连接
					this.radioButtonBevel.Checked = m_pSymbol.Join == esriLineJoinStyle.esriLJSBevel;
					this.radioButtonJRound.Checked = m_pSymbol.Join == esriLineJoinStyle.esriLJSRound;
					this.radioButtonMiter.Checked = m_pSymbol.Join == esriLineJoinStyle.esriLJSMitre;
				}
			}
			m_bInitComplete = true;
		}

		#region 用户更改属性值
		/// <summary>
		/// 颜色
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void colorPickerSymbol_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.Color = StyleCommon.GetRgbColor(this.colorPickerSymbol.Value);
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		
        /// <summary>
		/// 宽度
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if (this.numericUpDownWidth.Value > 0) m_pSymbol.Width = (double)this.numericUpDownWidth.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		
        /// <summary>
		/// 线头
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonButt_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if (this.radioButtonButt.Checked) m_pSymbol.Cap = esriLineCapStyle.esriLCSButt;
			if (this.radioButtonRound.Checked) m_pSymbol.Cap = esriLineCapStyle.esriLCSRound;
			if (this.radioButtonSquare.Checked) m_pSymbol.Cap = esriLineCapStyle.esriLCSSquare;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		
        /// <summary>
		/// 线连接
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonMiter_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if (this.radioButtonBevel.Checked) m_pSymbol.Join = esriLineJoinStyle.esriLJSBevel;
			if (this.radioButtonMiter.Checked) m_pSymbol.Join = esriLineJoinStyle.esriLJSMitre;
			if (this.radioButtonJRound.Checked) m_pSymbol.Join = esriLineJoinStyle.esriLJSRound;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		#endregion
	}
}
