using System;
using System.Windows.Forms;
using AG.COM.SDM.StyleManager;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// �������
	/// </summary>
	public partial class CtrlGradientFill : CtrlPropertyBase
	{
		/// <summary>
		/// ��ǰ����
		/// </summary>
		private IGradientFillSymbol m_pSymbol = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
		public CtrlGradientFill()
		{
			InitializeComponent();
		}

		private void CtrlGradientFill_Load(object sender, EventArgs e)
		{
			// ��ESRI.ServerStyle�ж�ȡ��ɫ��
			this.comboBoxCRStyle.Items.Clear();
			StyleCommon.InitialColorCombox(this.comboBoxCRStyle);

			if (this.m_pCtrlSymbol != null)
			{
				m_pSymbol = m_pCtrlSymbol as IGradientFillSymbol;
				// ���ݷ����������õ�ǰ����ؼ�ֵ
				if (null != m_pSymbol)
				{
					this.numericUpDownAngle.Value = (decimal)m_pSymbol.GradientAngle;
					this.numericUpDownPercentage.Value = (decimal)(m_pSymbol.GradientPercentage * 100.0);
					this.numericUpDownIntervals.Value = (decimal)m_pSymbol.IntervalCount;
					this.comboBoxStyle.SelectedIndex = (int)m_pSymbol.Style;
					// ��ӵ�ǰ��ɫ������ɫ��������
					IStyleGalleryItem pSGItem = new ServerStyleGalleryItemClass();
					pSGItem.Item = m_pSymbol.ColorRamp;
					this.comboBoxCRStyle.Items.Insert(0, pSGItem);
					this.comboBoxCRStyle.SelectedIndex = 0;
				}
			}
			m_bInitComplete = true;
		}

		#region �û���������ֵ
		/// <summary>
		/// ���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownIntervals_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.IntervalCount = (int)this.numericUpDownIntervals.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		
        /// <summary>
		/// �ٷֱ�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownPercentage_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.GradientPercentage = (double)this.numericUpDownPercentage.Value / 100.0;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		
        /// <summary>
		/// �Ƕ�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownAngle_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.GradientAngle = (double)this.numericUpDownAngle.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		
        /// <summary>
		/// ��ʽ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBoxStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.Style = (esriGradientFillStyle)this.comboBoxStyle.SelectedIndex;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol,this.m_iLayerIndex);
		}
		
        /// <summary>
		/// ��ɫ��ʽ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBoxCRStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			IStyleGalleryItem pStyleGalleryItem = this.comboBoxCRStyle.SelectedItem as IStyleGalleryItem;
			if(null != pStyleGalleryItem)
			{
				IColorRamp pColorRamp = pStyleGalleryItem.Item as ESRI.ArcGIS.Display.IColorRamp;
				if (null != pColorRamp)
				{
					m_pSymbol.ColorRamp = pColorRamp;
					this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
				}
			}
		}
		
        /// <summary>
		/// ������
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
