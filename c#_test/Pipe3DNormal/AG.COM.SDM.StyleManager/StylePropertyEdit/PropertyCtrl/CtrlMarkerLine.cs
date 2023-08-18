using System;
using System.Windows.Forms;
using AG.COM.SDM.StyleManager;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// 标记线
	/// </summary>
	public partial class CtrlMarkerLine : CtrlPropertyBase
	{
		/// <summary>
		/// 当前符号
		/// </summary>
		private IMarkerLineSymbol m_pSymbol = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
		public CtrlMarkerLine()
		{
			InitializeComponent();
		}

		private void CtrlMarkerLine_Load(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol != null)
			{
				// 根据符号属性设置当前窗体控件值
				m_pSymbol = this.m_pCtrlSymbol as IMarkerLineSymbol;

			}
			m_bInitComplete = true;
		}
		
        /// <summary>
		/// 选择符号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelMarkerSymbol_Click(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			frmSymbolSelector frm = new frmSymbolSelector();
			frm.SymbolType = SymbolType.stMarkerSymbol;
			frm.InitialSymbol = m_pSymbol.MarkerSymbol as ISymbol;
			if (frm.ShowDialog() != DialogResult.OK) return;
			m_pSymbol.MarkerSymbol = frm.SelectedSymbol as IMarkerSymbol;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
	}
}
