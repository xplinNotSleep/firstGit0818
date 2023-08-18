using System;
using System.Windows.Forms;
using AG.COM.SDM.StyleManager;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
    /// <summary>
    /// ��״��ʾ��
    /// </summary>
	public partial class CtrlLineCallout : CtrlPropertyBase
	{
		/// <summary>
		/// ��ǰ����
		/// </summary>
		private ILineCallout m_pSymbol = null;
		private ITextMargins m_pSymbol2 = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
		public CtrlLineCallout()
		{
			InitializeComponent();
		}

		private void CtrlLineCallout_Load(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol != null)
			{
				m_pSymbol = this.m_pCtrlSymbol as ILineCallout;
				m_pSymbol2 = this.m_pCtrlSymbol as ITextMargins;
				// ���ݷ����������õ�ǰ����ؼ�ֵ
				if (null != m_pSymbol)
				{
					// ��϶
					this.numericUpDownGap.Value = (decimal)m_pSymbol.Gap;
					// ��������
					this.numericUpDownLeader.Value = (decimal)m_pSymbol.LeaderTolerance;
					// ������
					this.checkBoxLeader.Checked = m_pSymbol.LeaderLine != null;
					// ǿ����
					this.checkBoxAccentBar.Checked = m_pSymbol.AccentBar != null;
					// �߽���
					this.checkBoxBorder.Checked = m_pSymbol.Border != null;
					// ��ʽ
					this.radioButtonStyle1.Checked = m_pSymbol.Style == esriLineCalloutStyle.esriLCSBase;
					this.radioButtonStyle2.Checked = m_pSymbol.Style == esriLineCalloutStyle.esriLCSMidpoint;
					this.radioButtonStyle3.Checked = m_pSymbol.Style == esriLineCalloutStyle.esriLCSThreePoint;
					this.radioButtonStyle4.Checked = m_pSymbol.Style == esriLineCalloutStyle.esriLCSFourPoint;
					this.radioButtonStyle5.Checked = m_pSymbol.Style == esriLineCalloutStyle.esriLCSUnderline;
					this.radioButtonStyle6.Checked = m_pSymbol.Style == esriLineCalloutStyle.esriLCSCircularCW;
					this.radioButtonStyle7.Checked = m_pSymbol.Style == esriLineCalloutStyle.esriLCSCircularCCW;
				}
				if (null != m_pSymbol2)
				{
					// ҳ�߾�
					this.numericUpDownLeft.Value = (decimal)m_pSymbol2.LeftMargin;
					this.numericUpDownRight.Value = (decimal)m_pSymbol2.RightMargin;
					this.numericUpDownUp.Value = (decimal)m_pSymbol2.RightMargin;
					this.numericUpDownDown.Value = (decimal)m_pSymbol2.BottomMargin;
				}
			}
			m_bInitComplete = true;
		}

		#region �û���������ֵ
		/// <summary>
		/// ��϶
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownGap_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.Gap = (double)this.numericUpDownGap.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// ��������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownLeader_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.LeaderTolerance = (double)this.numericUpDownLeader.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// ������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBoxLeader_CheckedChanged(object sender, EventArgs e)
		{
			this.btnSymbolSel.Enabled = this.checkBoxLeader.Checked;
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if (this.checkBoxLeader.Checked) m_pSymbol.LeaderLine = new SimpleLineSymbolClass();
			else m_pSymbol.LeaderLine = null;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// ѡ�������߷���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSymbolSel_Click(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			frmSymbolSelector frm = new frmSymbolSelector();
			frm.SymbolType = SymbolType.stLineSymbol;
			frm.InitialSymbol = m_pSymbol.LeaderLine as ISymbol;
			if (frm.ShowDialog() != DialogResult.OK) return;
			m_pSymbol.LeaderLine = frm.SelectedSymbol as ILineSymbol;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// ǿ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBoxAccentBar_CheckedChanged(object sender, EventArgs e)
		{
			this.btnSymbolSel2.Enabled = this.checkBoxAccentBar.Checked;
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if (this.checkBoxAccentBar.Checked) m_pSymbol.AccentBar = new SimpleLineSymbolClass();
			else m_pSymbol.AccentBar = null;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// ѡ��ǿ���߷���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSymbolSel2_Click(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			frmSymbolSelector frm = new frmSymbolSelector();
			frm.SymbolType = SymbolType.stLineSymbol;
			frm.InitialSymbol = m_pSymbol.AccentBar as ISymbol;
			if (frm.ShowDialog() != DialogResult.OK) return;
			m_pSymbol.AccentBar = frm.SelectedSymbol as ILineSymbol;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// �߽���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBoxBorder_CheckedChanged(object sender, EventArgs e)
		{
			this.btnSymbolSel3.Enabled = this.checkBoxBorder.Checked;
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if (this.checkBoxBorder.Checked) m_pSymbol.Border = new SimpleFillSymbolClass();
			else m_pSymbol.Border = null;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// ѡ��߽��߷���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSymbolSel3_Click(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			frmSymbolSelector frm = new frmSymbolSelector();
			frm.SymbolType = SymbolType.stFillSymbol;
			frm.InitialSymbol = m_pSymbol.Border as ISymbol;
			if (frm.ShowDialog() != DialogResult.OK) return;
			m_pSymbol.Border = frm.SelectedSymbol as IFillSymbol;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// ��ʽ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonStyle1_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if (this.radioButtonStyle1.Checked) m_pSymbol.Style = esriLineCalloutStyle.esriLCSBase;
			else if (this.radioButtonStyle2.Checked) m_pSymbol.Style = esriLineCalloutStyle.esriLCSMidpoint;
			else if (this.radioButtonStyle3.Checked) m_pSymbol.Style = esriLineCalloutStyle.esriLCSThreePoint;
			else if (this.radioButtonStyle4.Checked) m_pSymbol.Style = esriLineCalloutStyle.esriLCSFourPoint;
			else if (this.radioButtonStyle5.Checked) m_pSymbol.Style = esriLineCalloutStyle.esriLCSUnderline;
			else if (this.radioButtonStyle6.Checked) m_pSymbol.Style = esriLineCalloutStyle.esriLCSCircularCW;
			else if (this.radioButtonStyle7.Checked) m_pSymbol.Style = esriLineCalloutStyle.esriLCSCircularCCW;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}

		#region ҳ�߾�
		/// <summary>
		/// ҳ�߾���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownLeft_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol2 == null) return;
			m_pSymbol2.LeftMargin = (double)this.numericUpDownLeft.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// ҳ�߾���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownUp_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol2 == null) return;
			m_pSymbol2.TopMargin = (double)this.numericUpDownUp.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// ҳ�߾���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownRight_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol2 == null) return;
			m_pSymbol2.RightMargin = (double)this.numericUpDownRight.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// ҳ�߾���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownDown_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol2 == null) return;
			m_pSymbol2.BottomMargin = (double)this.numericUpDownDown.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		#endregion

		#endregion
	}
}
