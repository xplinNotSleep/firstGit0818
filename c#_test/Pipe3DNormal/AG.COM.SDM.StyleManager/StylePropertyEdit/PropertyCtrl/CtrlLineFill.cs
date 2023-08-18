using System;
using System.Windows.Forms;
using AG.COM.SDM.StyleManager;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// 线填充
	/// </summary>
	public partial class CtrlLineFill : CtrlPropertyBase
	{
		/// <summary>
		/// 当前符号
		/// </summary>
		private ILineFillSymbol m_pSymbol = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
		public CtrlLineFill()
		{
			InitializeComponent();
		}

		private void CtrlLineFill_Load(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol != null)
			{
				m_pSymbol = m_pCtrlSymbol as ILineFillSymbol;
				// 根据符号属性设置当前窗体控件值
				if (null != m_pSymbol)
				{
					this.colorPickerSymbol.Value = StyleCommon.GetColor(m_pSymbol.Color);
					this.numericUpDownAngle.Value = (decimal)m_pSymbol.Angle;
					this.numericUpDownOffset.Value = (decimal)m_pSymbol.Offset;
					this.numericUpDownSeparation.Value = (decimal)m_pSymbol.Separation;
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
		/// 偏移
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownOffset_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.Offset = (double)this.numericUpDownOffset.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		
        /// <summary>
		/// 间隔
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownSeparation_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if (this.numericUpDownSeparation.Value > 0) m_pSymbol.Separation = (double)this.numericUpDownSeparation.Value;
			else this.numericUpDownSeparation.Value = (decimal)m_pSymbol.Separation;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
	
        /// <summary>
		/// 线符号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelLineSymbol_Click(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			frmSymbolSelector frm = new frmSymbolSelector();
			frm.SymbolType = SymbolType.stLineSymbol;
			frm.InitialSymbol = m_pSymbol.LineSymbol as ISymbol;
			if (frm.ShowDialog() != DialogResult.OK) return;
			m_pSymbol.LineSymbol = frm.SelectedSymbol as ILineSymbol;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
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
