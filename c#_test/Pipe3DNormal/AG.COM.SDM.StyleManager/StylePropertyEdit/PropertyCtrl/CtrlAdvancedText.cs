using System;
using System.Windows.Forms;
using AG.COM.SDM.StyleManager;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// 高级文本属性
	/// </summary>
	public partial class CtrlAdvancedText : CtrlPropertyBase
	{
		/// <summary>
		/// 当前符号
		/// </summary>
		private IFormattedTextSymbol m_pSymbol = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
		public CtrlAdvancedText()
		{
			InitializeComponent();
		}

		private void CtrlAdvancedText_Load(object sender, EventArgs e)
		{
			if(this.m_pCtrlSymbol!=null)
			{
				m_pSymbol = this.m_pCtrlSymbol as IFormattedTextSymbol;
				// 根据符号属性设置当前窗体控件值
				if (null != m_pSymbol)
				{
					IFillSymbol pFillSymbol = m_pSymbol.FillSymbol;
					if (null != pFillSymbol) checkBoxTextFillPattern.Checked = true;
					ITextBackground pTextBackground = m_pSymbol.Background;
					if (null != pTextBackground) checkBoxTextBackground.Checked = true;
					this.colorPickerSymbol.Value = StyleCommon.GetColor(m_pSymbol.Color);
					this.numericUpDownXOffset.Value = (decimal)m_pSymbol.ShadowXOffset;
					this.numericUpDownYOffset.Value = (decimal)m_pSymbol.ShadowYOffset;
				}
			}
			m_bInitComplete = true;
		}

		#region 用户更改属性值
		/// <summary>
		/// 文本填充样式
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBoxTextFillPattern_CheckedChanged(object sender, EventArgs e)
		{
			this.btnSelFillSymbol.Enabled = this.checkBoxTextFillPattern.Checked;
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if (this.checkBoxTextFillPattern.Checked) m_pSymbol.FillSymbol = new SimpleFillSymbolClass();
			else m_pSymbol.FillSymbol = null;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// 文本填充属性
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelFillSymbol_Click(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			frmSymbolSelector frm = new frmSymbolSelector();
			frm.SymbolType = SymbolType.stFillSymbol;
			frm.InitialSymbol = m_pSymbol.FillSymbol as ISymbol;
			if (frm.ShowDialog() != DialogResult.OK) return;
			m_pSymbol.FillSymbol = frm.SelectedSymbol as IFillSymbol;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);

		}
		
        /// <summary>
		/// 文本背景
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBoxTextBackground_CheckedChanged(object sender, EventArgs e)
		{
			this.btnBackground.Enabled = this.checkBoxTextBackground.Checked;
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if (this.checkBoxTextBackground.Checked) m_pSymbol.Background = new BalloonCalloutClass();
			else m_pSymbol.Background = null;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// 文本背景属性
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnBackground_Click(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			FormSymbolPropertyEdit frm = null;
			// 复制一份
			IClone pClone = m_pSymbol.Background as IClone;
			if (null == pClone) return;

			frm = new FormSymbolPropertyEdit((ITextBackground)pClone.Clone());
			if (frm.ShowDialog(this) == DialogResult.OK)
			{
				m_pSymbol.Background = frm.Symbol as ITextBackground;
			}

		}
		
        /// <summary>
		/// 颜色
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void colorPickerSymbol_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.ShadowColor = StyleCommon.GetRgbColor(this.colorPickerSymbol.Value);
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
			m_pSymbol.ShadowYOffset = (double)this.numericUpDownYOffset.Value;
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
			m_pSymbol.ShadowXOffset = (double)this.numericUpDownXOffset.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		#endregion
	}
}
