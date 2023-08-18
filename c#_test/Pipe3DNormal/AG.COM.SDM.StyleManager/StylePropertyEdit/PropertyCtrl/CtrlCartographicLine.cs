using System;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// ��ͼ��
	/// </summary>
	public partial class CtrlCartographicLine : CtrlPropertyBase
	{
		/// <summary>
		/// ��ǰ����
		/// </summary>
		private ICartographicLineSymbol m_pSymbol = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
		public CtrlCartographicLine()
		{
			InitializeComponent();
		}

		private void CtrlCartographicLine_Load(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol != null)
			{
				m_pSymbol = m_pCtrlSymbol as ICartographicLineSymbol;
				// ���ݷ����������õ�ǰ����ؼ�ֵ
				if (null != m_pSymbol)
				{
					this.colorPickerSymbol.Value = StyleCommon.GetColor(m_pSymbol.Color);
					this.numericUpDownWidth.Value = (decimal)m_pSymbol.Width;

					// ��ͷ
					this.radioButtonButt.Checked = m_pSymbol.Cap == esriLineCapStyle.esriLCSButt;
					this.radioButtonRound.Checked = m_pSymbol.Cap == esriLineCapStyle.esriLCSRound;
					this.radioButtonSquare.Checked = m_pSymbol.Cap == esriLineCapStyle.esriLCSSquare;
					// ������
					this.radioButtonBevel.Checked = m_pSymbol.Join == esriLineJoinStyle.esriLJSBevel;
					this.radioButtonJRound.Checked = m_pSymbol.Join == esriLineJoinStyle.esriLJSRound;
					this.radioButtonMiter.Checked = m_pSymbol.Join == esriLineJoinStyle.esriLJSMitre;
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
		/// ���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if (this.numericUpDownWidth.Value > 0) m_pSymbol.Width = (double)this.numericUpDownWidth.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		
        /// <summary>
		/// ��ͷ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonButt_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if (this.radioButtonButt.Checked) m_pSymbol.Cap = esriLineCapStyle.esriLCSButt;
			if (this.radioButtonRound.Checked) m_pSymbol.Cap = esriLineCapStyle.esriLCSRound;
			if (this.radioButtonSquare.Checked) m_pSymbol.Cap = esriLineCapStyle.esriLCSSquare;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		
        /// <summary>
		/// ������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonMiter_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if (this.radioButtonBevel.Checked) m_pSymbol.Join = esriLineJoinStyle.esriLJSBevel;
			if (this.radioButtonMiter.Checked) m_pSymbol.Join = esriLineJoinStyle.esriLJSMitre;
			if (this.radioButtonJRound.Checked) m_pSymbol.Join = esriLineJoinStyle.esriLJSRound;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		#endregion
	}
}
