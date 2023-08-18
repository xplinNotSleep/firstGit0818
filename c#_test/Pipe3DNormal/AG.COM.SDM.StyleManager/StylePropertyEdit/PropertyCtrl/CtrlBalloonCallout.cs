using System;
using System.Windows.Forms;
using AG.COM.SDM.StyleManager;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
    /// <summary>
    /// ��������ʾ
    /// </summary>
	public partial class CtrlBalloonCallout : CtrlPropertyBase
	{
        /// <summary>
        /// ��ǰ����
        /// </summary>
		private IBalloonCallout m_pSymbol = null;
		private ITextMargins m_pSymbol2 = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public CtrlBalloonCallout()
		{
			InitializeComponent();
		}

		private void CtrlBalloonCallout_Load(object sender, EventArgs e)
		{
            if (this.m_pCtrlSymbol != null)
            {
				m_pSymbol = this.m_pCtrlSymbol as IBalloonCallout;
				m_pSymbol2 = this.m_pCtrlSymbol as ITextMargins;
                // ���ݷ����������õ�ǰ����ؼ�ֵ
                if (null != m_pSymbol)
                {
                    this.radioButtonRect.Checked = m_pSymbol.Style == esriBalloonCalloutStyle.esriBCSRectangle;
					this.radioButtonRoundRect.Checked = m_pSymbol.Style == esriBalloonCalloutStyle.esriBCSRoundedRectangle;
					this.numericUpDownLeader.Value = (decimal)m_pSymbol.LeaderTolerance;
                }
				if (null != m_pSymbol2)
				{
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
        /// ѡ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSymbolSel_Click(object sender, EventArgs e)
        {
			if (this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			frmSymbolSelector frm = new frmSymbolSelector();
			frm.SymbolType = SymbolType.stFillSymbol;
			frm.InitialSymbol = m_pSymbol.Symbol as ISymbol;
			if (frm.ShowDialog() != DialogResult.OK) return;
			m_pSymbol.Symbol = frm.SelectedSymbol as IFillSymbol;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
       
        /// <summary>
        /// ���Ρ�Բ�Ǿ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonRect_CheckedChanged(object sender, EventArgs e)
        {
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if(this.radioButtonRect.Checked) m_pSymbol.Style = esriBalloonCalloutStyle.esriBCSRectangle;
			else if (this.radioButtonRoundRect.Checked) m_pSymbol.Style = esriBalloonCalloutStyle.esriBCSRoundedRectangle;
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
