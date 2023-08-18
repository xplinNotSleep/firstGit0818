using System;
using System.Windows.Forms;
using AG.COM.SDM.StyleManager;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
    /// <summary>
    /// 标记文本背景
    /// </summary>
	public partial class CtrlMarkerTextBackground : CtrlPropertyBase
	{
		/// <summary>
		/// 当前符号
		/// </summary>
		private IMarkerTextBackground m_pSymbol = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
		public CtrlMarkerTextBackground()
		{
			InitializeComponent();
		}

		private void CtrlMarkerTextBackground_Load(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol != null)
			{
				m_pSymbol = this.m_pCtrlSymbol as IMarkerTextBackground;
				// 根据符号属性设置当前窗体控件值
				if (null != m_pSymbol)
				{
					this.colorPickerSymbol.Value = StyleCommon.GetColor(m_pSymbol.Symbol.Color);
					this.numericUpDownSize.Value = (decimal)m_pSymbol.Symbol.Size;
					this.checkBoxScaleMarker.Checked = m_pSymbol.ScaleToFit;
				}
			}
			m_bInitComplete = true;
		}

		#region 用户更改属性值
		/// <summary>
		/// 缩放标记适应文本
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBoxScaleMarker_CheckedChanged(object sender, EventArgs e)
		{
			this.numericUpDownSize.Enabled = !this.checkBoxScaleMarker.Checked;
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.ScaleToFit = this.checkBoxScaleMarker.Checked;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// 符号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSymbolSel_Click(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			frmSymbolSelector frm = new frmSymbolSelector();
			frm.SymbolType = SymbolType.stMarkerSymbol;
			frm.InitialSymbol = m_pSymbol.Symbol as ISymbol;
			if (frm.ShowDialog() != DialogResult.OK) return;
			m_pSymbol.Symbol = frm.SelectedSymbol as IMarkerSymbol;
			m_bInitComplete = false;
			this.numericUpDownSize.Value = (decimal)m_pSymbol.Symbol.Size;
			m_bInitComplete = true;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// 颜色
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void colorPickerSymbol_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.Symbol.Color = StyleCommon.GetRgbColor(this.colorPickerSymbol.Value);
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// 大小
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownSize_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			IMarkerSymbol pMarkerSymbol = m_pSymbol.Symbol;
			pMarkerSymbol.Size = (double)this.numericUpDownSize.Value;
			m_pSymbol.Symbol = pMarkerSymbol;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		#endregion
	}
}
