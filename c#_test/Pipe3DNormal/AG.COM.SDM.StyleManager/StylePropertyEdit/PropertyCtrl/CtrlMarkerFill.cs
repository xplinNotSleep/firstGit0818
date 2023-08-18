using System;
using System.Windows.Forms;
using AG.COM.SDM.StyleManager;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// 标记填充
	/// </summary>
	public partial class CtrlMarkerFill : CtrlPropertyBase
	{
		/// <summary>
		/// 当前符号
		/// </summary>
		private IMarkerFillSymbol m_pSymbol = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
		public CtrlMarkerFill()
		{
			InitializeComponent();
		}

		private void CtrlMarkerFill_Load(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol != null)
			{
				m_pSymbol = m_pCtrlSymbol as IMarkerFillSymbol;
				// 根据符号属性设置当前窗体控件值
				if (null != m_pSymbol)
				{
					this.radioButtonGrid.Checked = m_pSymbol.Style == esriMarkerFillStyle.esriMFSGrid;
					this.radioButtonRandom.Checked = m_pSymbol.Style == esriMarkerFillStyle.esriMFSRandom;
					this.colorPickerSymbol.Value = StyleCommon.GetColor(m_pSymbol.Color);
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
		/// 填充样式
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonGrid_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if (this.radioButtonGrid.Checked) m_pSymbol.Style = esriMarkerFillStyle.esriMFSGrid;
			else m_pSymbol.Style = esriMarkerFillStyle.esriMFSRandom;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		
        /// <summary>
		/// 选择标记符号
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
		
        /// <summary>
		/// 选择轮廓线
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
