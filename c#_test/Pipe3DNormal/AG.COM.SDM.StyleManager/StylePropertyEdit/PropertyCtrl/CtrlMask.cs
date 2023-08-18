using System;
using System.Windows.Forms;
using AG.COM.SDM.StyleManager;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// 掩膜
	/// </summary>
	public partial class CtrlMask : CtrlPropertyBase
	{
		/// <summary>
		/// 当前符号
		/// </summary>
		private IMask m_pSymbol = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
		public CtrlMask()
		{
			InitializeComponent();
		}

		private void CtrlMask_Load(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol != null)
			{
				m_pSymbol = this.m_pCtrlSymbol as IMask;
				// 根据符号属性设置当前窗体控件值
				if (null != m_pSymbol) 
				{
					if (m_pSymbol.MaskStyle == esriMaskStyle.esriMSNone) this.radioButtonNone.Checked = true;
					else this.radioButtonHalo.Checked = true;
					this.numericUpDownSize.Value = (decimal)m_pSymbol.MaskSize;
				}
			}
			m_bInitComplete = true;
		}

		#region 用户更改属性值
		/// <summary>
		/// 样式
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonNone_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if (this.radioButtonNone.Checked) m_pSymbol.MaskStyle = esriMaskStyle.esriMSNone;
			else m_pSymbol.MaskStyle = esriMaskStyle.esriMSHalo;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// 大小
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownSize_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.MaskSize = (double)this.numericUpDownSize.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// 选择符号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSymbolSel_Click(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			frmSymbolSelector frm = new frmSymbolSelector();
			frm.SymbolType = SymbolType.stFillSymbol;
			frm.InitialSymbol = m_pSymbol.MaskSymbol as ISymbol;
			if (frm.ShowDialog() != DialogResult.OK) return;
			m_pSymbol.MaskSymbol = frm.SelectedSymbol as IFillSymbol;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		#endregion
	}
}
