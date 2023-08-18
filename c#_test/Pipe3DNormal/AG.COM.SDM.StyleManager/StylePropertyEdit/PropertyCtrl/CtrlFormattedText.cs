using System;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// 格式化文本属性
	/// </summary>
	public partial class CtrlFormattedText : CtrlPropertyBase
	{
		/// <summary>
		/// 当前符号
		/// </summary>
		private IFormattedTextSymbol m_pSymbol = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
		public CtrlFormattedText()
		{
			InitializeComponent();
		}

		private void CtrlFormattedText_Load(object sender, EventArgs e)
		{
			if (null != this.m_pCtrlSymbol)
			{
				m_pSymbol = this.m_pCtrlSymbol as IFormattedTextSymbol;
				// 根据符号属性设置当前窗体控件值
				if (null != m_pSymbol)
				{
					this.numericUpDownCharSpacing.Value = (decimal)m_pSymbol.CharacterSpacing;
					this.numericUpDownCharWidth.Value = (decimal)m_pSymbol.CharacterWidth;
					this.numericUpDownFlipAngle.Value = (decimal)m_pSymbol.FlipAngle;
					this.numericUpDownLeading.Value = (decimal)m_pSymbol.Leading;
					this.numericUpDownWordSpacing.Value = (decimal)m_pSymbol.WordSpacing;
					// 文本位置
					this.radioButtonPNormal.Checked = m_pSymbol.Position == esriTextPosition.esriTPNormal;
					this.radioButtonSubscript.Checked = m_pSymbol.Position == esriTextPosition.esriTPSubscript;
					this.radioButtonSuperscript.Checked = m_pSymbol.Position == esriTextPosition.esriTPSuperscript;
					// 文本大小写
					this.radioButtonCNormal.Checked = m_pSymbol.Case == esriTextCase.esriTCNormal;
					this.radioButtonCAllCaps.Checked = m_pSymbol.Case == esriTextCase.esriTCAllCaps;
					this.radioButtonCSmallCaps.Checked = m_pSymbol.Case == esriTextCase.esriTCSmallCaps;

					this.checkBoxKerning.Checked = m_pSymbol.Kerning;

				}
			}
			m_bInitComplete = true;
		}
	
        #region 用户更改属性值
		/// <summary>
		/// 文本位置
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonPNormal_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if (this.radioButtonPNormal.Checked) m_pSymbol.Position = esriTextPosition.esriTPNormal;
			else if (this.radioButtonSubscript.Checked) m_pSymbol.Position = esriTextPosition.esriTPSubscript;
			else if (this.radioButtonSuperscript.Checked) m_pSymbol.Position = esriTextPosition.esriTPSuperscript;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// 文本大小写
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonCNormal_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if (this.radioButtonCNormal.Checked) m_pSymbol.Case = esriTextCase.esriTCNormal;
			else if (this.radioButtonCAllCaps.Checked) m_pSymbol.Case = esriTextCase.esriTCAllCaps;
			else if (this.radioButtonCSmallCaps.Checked) m_pSymbol.Case = esriTextCase.esriTCSmallCaps;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// 字符间隔
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownCharSpacing_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.CharacterSpacing = (double)this.numericUpDownCharSpacing.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// 行距
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownLeading_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.Leading = (double)this.numericUpDownLeading.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// 翻转角度
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownFlipAngle_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.FlipAngle = (double)this.numericUpDownFlipAngle.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// 字符宽度
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void numericUpDownCharWidth_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.CharacterWidth = (double)this.numericUpDownCharWidth.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// 单词间隔
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownWordSpacing_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.WordSpacing = (double)this.numericUpDownWordSpacing.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// 字距调整
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBoxKerning_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.Angle = (double)this.numericUpDownFlipAngle.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		#endregion
	}
}
