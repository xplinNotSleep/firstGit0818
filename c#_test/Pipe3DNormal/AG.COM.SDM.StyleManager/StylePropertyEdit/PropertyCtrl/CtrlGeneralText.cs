using System;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// 常规文本属性
	/// </summary>
	public partial class CtrlGeneralText : CtrlPropertyBase
	{
		/// <summary>
		/// 当前符号
		/// </summary>
		private ISimpleTextSymbol m_pSymbol = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
		public CtrlGeneralText()
		{
			InitializeComponent();
		}

		private void CtrlGeneralText_Load(object sender, EventArgs e)
		{
			this.comboBoxFont.ComboBoxFont.SelectedIndexChanged += new EventHandler(ComboBoxFont_SelectedIndexChanged);
			if(null != this.m_pCtrlSymbol)
			{
				m_pSymbol = this.m_pCtrlSymbol as ISimpleTextSymbol;
				// 根据符号属性设置当前窗体控件值
				if (null != m_pSymbol)
				{
					this.colorPickerSymbol.Value = StyleCommon.GetColor(m_pSymbol.Color);
					this.numericUpDownAngle.Value = (decimal) m_pSymbol.Angle;
					this.numericUpDownXOffset.Value = (decimal)m_pSymbol.XOffset;
					this.numericUpDownYOffset.Value = (decimal)m_pSymbol.YOffset;
					// 垂直对齐
					this.radioButtonVTop.Checked = m_pSymbol.VerticalAlignment == esriTextVerticalAlignment.esriTVATop;
					this.radioButtonVCenter.Checked = m_pSymbol.VerticalAlignment == esriTextVerticalAlignment.esriTVACenter;
					this.radioButtonVBaseline.Checked = m_pSymbol.VerticalAlignment == esriTextVerticalAlignment.esriTVABaseline;
					this.radioButtonVBottom.Checked = m_pSymbol.VerticalAlignment == esriTextVerticalAlignment.esriTVABottom;
					// 水平对齐
					this.radioButtonHLeft.Checked = m_pSymbol.HorizontalAlignment == esriTextHorizontalAlignment.esriTHALeft;
					this.radioButtonHRight.Checked = m_pSymbol.HorizontalAlignment == esriTextHorizontalAlignment.esriTHARight;
					this.radioButtonHCenter.Checked = m_pSymbol.HorizontalAlignment == esriTextHorizontalAlignment.esriTHACenter;
					this.radioButtonHFull.Checked = m_pSymbol.HorizontalAlignment == esriTextHorizontalAlignment.esriTHAFull;
					// 字体
					this.comboBoxFont.SetCurrentFont(m_pSymbol.Font.Name);
					this.checkBoxBold.Checked = m_pSymbol.Font.Bold;
					this.checkBoxItalic.Checked = m_pSymbol.Font.Italic;
					this.checkBoxUnderline.Checked = m_pSymbol.Font.Underline;
					this.checkBoxStrikeout.Checked = m_pSymbol.Font.Strikethrough;
					this.comboBoxSize.Text = m_pSymbol.Font.Size.ToString();

					this.checkBoxRToL.Checked = m_pSymbol.RightToLeft;
				}
			}
			m_bInitComplete = true;
		}
		
        #region 用户更改属性值
		/// <summary>
		/// 更改当前字体
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ComboBoxFont_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			stdole.IFontDisp stdFont = new stdole.StdFontClass() as stdole.IFontDisp;
			stdFont.Name = comboBoxFont.ComboBoxFont.SelectedItem.ToString();
			stdFont.Size = m_pSymbol.Font.Size;
			stdFont.Bold =
			stdFont.Bold = this.checkBoxBold.Checked;
			stdFont.Italic = this.checkBoxItalic.Checked;
			stdFont.Underline = this.checkBoxUnderline.Checked;
			stdFont.Strikethrough = this.checkBoxStrikeout.Checked;
			m_pSymbol.Font = stdFont;

			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
	
        /// <summary>
		/// 更改字体样式
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FontStyle_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			stdole.IFontDisp stdFont = m_pSymbol.Font;
			stdFont.Bold = this.checkBoxBold.Checked;
			stdFont.Italic = this.checkBoxItalic.Checked;
			stdFont.Underline = this.checkBoxUnderline.Checked;
			stdFont.Strikethrough = this.checkBoxStrikeout.Checked;
			m_pSymbol.Font = stdFont;
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
			m_pSymbol.Color = StyleCommon.GetRgbColor(this.colorPickerSymbol.Value);
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
	
        /// <summary>
		/// 角度
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownAngle_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.Angle = (double)this.numericUpDownAngle.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// 大小
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBoxSize_TextChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			decimal dSize = m_pSymbol.Font.Size;
			stdole.IFontDisp stdFont = m_pSymbol.Font;
			if (decimal.TryParse(this.comboBoxSize.Text, out dSize) && dSize>0) stdFont.Size = dSize;
			else 
			{
				this.comboBoxSize.Text = m_pSymbol.Font.Size.ToString();
				return;
			}
			m_pSymbol.Font = stdFont;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
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
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
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
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
	
        /// <summary>
		/// 垂直对齐
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void VAlign_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			// 垂直对齐
			if (this.radioButtonVTop.Checked) m_pSymbol.VerticalAlignment = esriTextVerticalAlignment.esriTVATop;
			else if(this.radioButtonVCenter.Checked)  m_pSymbol.VerticalAlignment = esriTextVerticalAlignment.esriTVACenter;
			else if(this.radioButtonVBaseline.Checked) m_pSymbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABaseline;
			else if (this.radioButtonVBottom.Checked) m_pSymbol.VerticalAlignment = esriTextVerticalAlignment.esriTVABottom;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);

		}
		
        /// <summary>
		/// 水平对齐
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HAlign_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			// 水平对齐
			if(this.radioButtonHLeft.Checked) m_pSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
			else if(this.radioButtonHRight.Checked) m_pSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHARight;
			else if(this.radioButtonHCenter.Checked) m_pSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHACenter;
			else if (this.radioButtonHFull.Checked) m_pSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHAFull;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// 从右至左
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBoxRToL_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.RightToLeft = this.checkBoxRToL.Checked;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		#endregion

	}
}
