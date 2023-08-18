using System;
using System.Windows.Forms;
using AG.COM.SDM.StyleManager;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
    /// <summary>
    /// ��װ��
    /// </summary>
	public partial class CtrlSimpleLineDecoration : CtrlPropertyBase
	{
		/// <summary>
		/// ��ǰ����
		/// </summary>
		private ISimpleLineDecorationElement m_pSymbol = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
		public CtrlSimpleLineDecoration()
		{
			InitializeComponent();
		}

		private void CtrlSimpleLineDecoration_Load(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol != null)
			{
				m_pSymbol = this.m_pCtrlSymbol as ISimpleLineDecorationElement ;
				// ���ݷ����������õ�ǰ����ؼ�ֵ
				if (null != m_pSymbol)
				{
					this.numericUpDownPositions.Value = (decimal)m_pSymbol.PositionCount;
					this.checkBoxFlipAll.Checked = m_pSymbol.FlipAll;
					this.checkBoxFlipFirst.Checked = m_pSymbol.FlipFirst;
					this.radioButtonRotate.Checked = m_pSymbol.Rotate;
					if (m_pSymbol.Rotate) m_pSymbol.PositionAsRatio = false;
					this.radioButtonPositionAsRatio.Checked = m_pSymbol.PositionAsRatio;
				}
			}
			m_bInitComplete = true;
		}
	
        /// <summary>
		/// λ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownPositions_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			int iCount = (int)this.numericUpDownPositions.Value;
			if (iCount == m_pSymbol.PositionCount) return;
			m_pSymbol.ClearPositions();
			if (iCount > 0) m_pSymbol.AddPosition(0);
			if(iCount>2)
			{
				for(int i=0;i<iCount-2;i++)
				{
					m_pSymbol.AddPosition((i+1) * 1.0 / (iCount - 1));
				}
			}
			if (iCount > 1) m_pSymbol.AddPosition(1);
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		
        /// <summary>
		/// ȫ������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBoxFlipAll_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.FlipAll = this.checkBoxFlipAll.Checked;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		
        /// <summary>
		/// ��һ������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBoxFlipFirst_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.FlipFirst = this.checkBoxFlipFirst.Checked;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		
        /// <summary>
		/// �����߽Ƕ���ת����/���ַ��ŵĽǶ������ҳ��̶�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonRotate_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.Rotate = this.radioButtonRotate.Checked;
			m_pSymbol.PositionAsRatio = this.radioButtonPositionAsRatio.Checked;
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
	}
}
