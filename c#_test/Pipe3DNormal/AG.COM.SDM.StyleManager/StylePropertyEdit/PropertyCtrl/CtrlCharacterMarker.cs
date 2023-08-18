using System;
using System.Drawing;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// 字符标记
	/// </summary>
	public partial class CtrlCharacterMarker : CtrlPropertyBase
	{
		/// <summary>
		/// 重设当前字体中的符号
		/// </summary>
		private bool m_bReset = false;
		
        /// <summary>
		/// 当前符号
		/// </summary>
		private ICharacterMarkerSymbol m_pSymbol = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
		public CtrlCharacterMarker()
		{
			InitializeComponent();
		}

		private void CtrlCharacterMarker_Load(object sender, EventArgs e)
		{
			this.comboBoxFont.ComboBoxFont.SelectedIndexChanged += new EventHandler(ComboBoxFont_SelectedIndexChanged);
			if (this.m_pCtrlSymbol != null)
			{
				m_pSymbol = m_pCtrlSymbol as ICharacterMarkerSymbol;
				// 根据符号属性设置当前窗体控件值
				if (null != m_pSymbol) 
				{
					// 字体
					this.comboBoxFont.SetCurrentFont(m_pSymbol.Font.Name);
					this.comboBoxSize.Text = m_pSymbol.Size.ToString();
					this.fontListView1.SelectUnicode(m_pSymbol.CharacterIndex);
					this.textBoxUnicode.Text = m_pSymbol.CharacterIndex.ToString();
					// 角度、偏移
					this.numericUpDownAngle.Value = (decimal)m_pSymbol.Angle;
					this.numericUpDownXOffset.Value = (decimal)m_pSymbol.XOffset;
					this.numericUpDownYOffset.Value = (decimal)m_pSymbol.YOffset;
					// 颜色
					this.colorPickerSymbol.Value = StyleCommon.GetColor(m_pSymbol.Color);
				}
			}
			m_bReset = true;
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
			this.fontListView1.DrawFont = new Font(comboBoxFont.ComboBoxFont.SelectedItem.ToString(), 16);
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			stdole.IFontDisp stdFont = new stdole.StdFontClass() as stdole.IFontDisp;
			stdFont.Name = comboBoxFont.ComboBoxFont.SelectedItem.ToString();
			stdFont.Size = m_pSymbol.Font.Size;
			m_pSymbol.Font = stdFont;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
			if (m_bReset) fontListView1.SelectFirstUnicode();
		}
		
        /// <summary>
		/// 选择字体中的符号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void fontListView1_SelectFontChanged(object sender, CommonLibrary.Control.SelectFontEventArgs args)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_bInitComplete = false;
			textBoxUnicode.Text = this.fontListView1.SelectedUnicode.ToString();
			m_pSymbol.CharacterIndex = this.fontListView1.SelectedUnicode;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
			m_bInitComplete = true;
		}
		
        /// <summary>
		/// 大小
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBoxSize_TextChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			double dSize = 5.0;
			if (double.TryParse(this.comboBoxSize.Text, out dSize) && dSize > 0.0) m_pSymbol.Size = dSize;
			else
			{
				m_pSymbol.Size = 5;
				this.comboBoxSize.SelectedIndex = 0;
			}
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
			m_pSymbol.Angle = (double)this.numericUpDownAngle.Value;
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
		#endregion
		
        /// <summary>
		/// 更改Unicode
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxUnicode_TextChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			int iUnicode = 0;
			if (int.TryParse(this.textBoxUnicode.Text, out iUnicode)) this.fontListView1.SelectUnicode(iUnicode);
		}

	}
}
