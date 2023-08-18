using System;
using System.Windows.Forms;
using AG.COM.SDM.StyleManager;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// 重复线
	/// </summary>
	public partial class CtrlHashLine : CtrlPropertyBase
	{
		/// <summary>
		/// 当前符号
		/// </summary>
		private IHashLineSymbol m_pSymbol = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
		public CtrlHashLine()
		{
			InitializeComponent();
		}

		private void CtrlHashLine_Load(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol != null)
			{
				m_pSymbol = m_pCtrlSymbol as IHashLineSymbol;
				// 根据符号属性设置当前窗体控件值
				if (null != m_pSymbol)
				{
					this.numericUpDownAngle.Value = (decimal)m_pSymbol.Angle;
				}
			}
			m_bInitComplete = true;
		}

		#region 用户更改属性值
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
		/// 线符号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelLineSymbol_Click(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			frmSymbolSelector frm = new frmSymbolSelector();
			frm.SymbolType = SymbolType.stLineSymbol;
			frm.InitialSymbol = m_pSymbol.HashSymbol as ISymbol;
			if (frm.ShowDialog() != DialogResult.OK) return;
			m_pSymbol.HashSymbol = frm.SelectedSymbol as ILineSymbol;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		#endregion
	}
}
