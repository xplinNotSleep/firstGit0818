using System;
using System.Windows.Forms;
using AG.COM.SDM.StyleManager;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
    /// <summary>
    /// ����ı�����
    /// </summary>
	public partial class CtrlMarkerTextBackground : CtrlPropertyBase
	{
		/// <summary>
		/// ��ǰ����
		/// </summary>
		private IMarkerTextBackground m_pSymbol = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
		public CtrlMarkerTextBackground()
		{
			InitializeComponent();
		}

		private void CtrlMarkerTextBackground_Load(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol != null)
			{
				m_pSymbol = this.m_pCtrlSymbol as IMarkerTextBackground;
				// ���ݷ����������õ�ǰ����ؼ�ֵ
				if (null != m_pSymbol)
				{
					this.colorPickerSymbol.Value = StyleCommon.GetColor(m_pSymbol.Symbol.Color);
					this.numericUpDownSize.Value = (decimal)m_pSymbol.Symbol.Size;
					this.checkBoxScaleMarker.Checked = m_pSymbol.ScaleToFit;
				}
			}
			m_bInitComplete = true;
		}

		#region �û���������ֵ
		/// <summary>
		/// ���ű����Ӧ�ı�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBoxScaleMarker_CheckedChanged(object sender, EventArgs e)
		{
			this.numericUpDownSize.Enabled = !this.checkBoxScaleMarker.Checked;
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.ScaleToFit = this.checkBoxScaleMarker.Checked;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSymbolSel_Click(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			frmSymbolSelector frm = new frmSymbolSelector();
			frm.SymbolType = SymbolType.stMarkerSymbol;
			frm.InitialSymbol = m_pSymbol.Symbol as ISymbol;
			if (frm.ShowDialog() != DialogResult.OK) return;
			m_pSymbol.Symbol = frm.SelectedSymbol as IMarkerSymbol;
			m_bInitComplete = false;
			this.numericUpDownSize.Value = (decimal)m_pSymbol.Symbol.Size;
			m_bInitComplete = true;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// ��ɫ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void colorPickerSymbol_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.Symbol.Color = StyleCommon.GetRgbColor(this.colorPickerSymbol.Value);
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// ��С
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownSize_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			IMarkerSymbol pMarkerSymbol = m_pSymbol.Symbol;
			pMarkerSymbol.Size = (double)this.numericUpDownSize.Value;
			m_pSymbol.Symbol = pMarkerSymbol;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		#endregion
	}
}
