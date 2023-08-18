using System;
using System.Windows.Forms;
using AG.COM.SDM.StyleManager;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// ������
	/// </summary>
	public partial class CtrlMarkerFill : CtrlPropertyBase
	{
		/// <summary>
		/// ��ǰ����
		/// </summary>
		private IMarkerFillSymbol m_pSymbol = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
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
				// ���ݷ����������õ�ǰ����ؼ�ֵ
				if (null != m_pSymbol)
				{
					this.radioButtonGrid.Checked = m_pSymbol.Style == esriMarkerFillStyle.esriMFSGrid;
					this.radioButtonRandom.Checked = m_pSymbol.Style == esriMarkerFillStyle.esriMFSRandom;
					this.colorPickerSymbol.Value = StyleCommon.GetColor(m_pSymbol.Color);
				}
			}
			m_bInitComplete = true;
		}

		#region �û���������ֵ
		/// <summary>
		/// ��ɫ
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
		/// �����ʽ
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
		/// ѡ���Ƿ���
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
		/// ѡ��������
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
