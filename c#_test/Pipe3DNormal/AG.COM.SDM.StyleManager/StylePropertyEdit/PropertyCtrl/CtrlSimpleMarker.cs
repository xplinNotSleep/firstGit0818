using System;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// 简单标记
	/// </summary>
	public partial class CtrlSimpleMarker : CtrlPropertyBase
	{
		/// <summary>
		/// 当前符号
		/// </summary>
		private ISimpleMarkerSymbol m_pSymbol = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
		public CtrlSimpleMarker()
		{
			InitializeComponent();
		}

		private void CtrlSimpleMarker_Load(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol != null)
			{
				m_pSymbol = m_pCtrlSymbol as ISimpleMarkerSymbol;
				// 根据符号属性设置当前窗体控件值
				if (null != m_pSymbol)
				{
					this.colorPickerSymbol.Value = StyleCommon.GetColor(m_pSymbol.Color);
					this.comboBoxSMStyle.SelectedIndex = (int)m_pSymbol.Style;
					this.numericUpDownSMSize.Value = (decimal)m_pSymbol.Size;
					this.numericUpDownXOffset.Value = (decimal)m_pSymbol.XOffset;
					this.numericUpDownYOffset.Value = (decimal)m_pSymbol.YOffset;
					this.checkBoxUseOutline.Checked = m_pSymbol.Outline;
					this.colorPickerOutline.Value = StyleCommon.GetColor(m_pSymbol.OutlineColor);
					this.numericUpDownOutlineSize.Value = (decimal)m_pSymbol.OutlineSize;
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
		/// 样式
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBoxSMStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if (-1 != this.comboBoxSMStyle.SelectedIndex) m_pSymbol.Style = (esriSimpleMarkerStyle)this.comboBoxSMStyle.SelectedIndex;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
	
        /// <summary>
		/// 大小
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownSMSize_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if (this.numericUpDownSMSize.Value > 0) m_pSymbol.Size = (double)this.numericUpDownSMSize.Value;
			else this.numericUpDownSMSize.Value = (decimal)m_pSymbol.Size;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
	
        /// <summary>
		/// X偏移
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
		/// Y偏移
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
		/// 使用轮廓线
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBoxUseOutline_CheckedChanged(object sender, EventArgs e)
		{
			this.colorPickerOutline.Enabled = this.checkBoxUseOutline.Checked;
			this.numericUpDownOutlineSize.Enabled = this.checkBoxUseOutline.Checked;
			// 更新符号预览
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.Outline = this.checkBoxUseOutline.Checked;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
	
        /// <summary>
		/// 轮廓线颜色
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void colorPickerOutline_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.OutlineColor = StyleCommon.GetRgbColor(this.colorPickerOutline.Value);
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
	
        /// <summary>
		/// 轮廓线大小
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownOutlineSize_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.OutlineSize = (double)this.numericUpDownOutlineSize.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		#endregion
	}
}
