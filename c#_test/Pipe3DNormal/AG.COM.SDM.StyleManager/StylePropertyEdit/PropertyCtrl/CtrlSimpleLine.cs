using System;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// 简单线
	/// </summary>
	public partial class CtrlSimpleLine : CtrlPropertyBase
	{
		/// <summary>
		/// 当前符号
		/// </summary>
		private ISimpleLineSymbol m_pSymbol = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
		public CtrlSimpleLine()
		{
			InitializeComponent();
		}

		private void CtrlSimpleLine_Load(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol != null)
			{
				m_pSymbol = m_pCtrlSymbol as ISimpleLineSymbol;
				// 根据符号属性设置当前窗体控件值
				if (null != m_pSymbol)
				{
					this.colorPickerSymbol.Value = StyleCommon.GetColor(m_pSymbol.Color);
					this.numericUpDownWidth.Value = (decimal)m_pSymbol.Width;
					this.comboBoxSMStyle.SelectedIndex = (int)m_pSymbol.Style;
					if (this.comboBoxSMStyle.SelectedIndex > 0)
					{
						this.numericUpDownWidth.Enabled = false;
						this.numericUpDownWidth.Value = 1.0M;
					}
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
			if (this.comboBoxSMStyle.SelectedIndex != -1) m_pSymbol.Style = (esriSimpleLineStyle)this.comboBoxSMStyle.SelectedIndex;
			if (this.comboBoxSMStyle.SelectedIndex > 0)
			{
				this.numericUpDownWidth.Enabled = false;
				this.numericUpDownWidth.Value = 1.0M;
			}
			else this.numericUpDownWidth.Enabled = true;
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
		#endregion
	}
}
