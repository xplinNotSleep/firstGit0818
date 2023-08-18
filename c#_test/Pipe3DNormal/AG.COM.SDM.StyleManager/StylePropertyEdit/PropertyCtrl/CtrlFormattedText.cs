using System;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// ��ʽ���ı�����
	/// </summary>
	public partial class CtrlFormattedText : CtrlPropertyBase
	{
		/// <summary>
		/// ��ǰ����
		/// </summary>
		private IFormattedTextSymbol m_pSymbol = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
		public CtrlFormattedText()
		{
			InitializeComponent();
		}

		private void CtrlFormattedText_Load(object sender, EventArgs e)
		{
			if (null != this.m_pCtrlSymbol)
			{
				m_pSymbol = this.m_pCtrlSymbol as IFormattedTextSymbol;
				// ���ݷ����������õ�ǰ����ؼ�ֵ
				if (null != m_pSymbol)
				{
					this.numericUpDownCharSpacing.Value = (decimal)m_pSymbol.CharacterSpacing;
					this.numericUpDownCharWidth.Value = (decimal)m_pSymbol.CharacterWidth;
					this.numericUpDownFlipAngle.Value = (decimal)m_pSymbol.FlipAngle;
					this.numericUpDownLeading.Value = (decimal)m_pSymbol.Leading;
					this.numericUpDownWordSpacing.Value = (decimal)m_pSymbol.WordSpacing;
					// �ı�λ��
					this.radioButtonPNormal.Checked = m_pSymbol.Position == esriTextPosition.esriTPNormal;
					this.radioButtonSubscript.Checked = m_pSymbol.Position == esriTextPosition.esriTPSubscript;
					this.radioButtonSuperscript.Checked = m_pSymbol.Position == esriTextPosition.esriTPSuperscript;
					// �ı���Сд
					this.radioButtonCNormal.Checked = m_pSymbol.Case == esriTextCase.esriTCNormal;
					this.radioButtonCAllCaps.Checked = m_pSymbol.Case == esriTextCase.esriTCAllCaps;
					this.radioButtonCSmallCaps.Checked = m_pSymbol.Case == esriTextCase.esriTCSmallCaps;

					this.checkBoxKerning.Checked = m_pSymbol.Kerning;

				}
			}
			m_bInitComplete = true;
		}
	
        #region �û���������ֵ
		/// <summary>
		/// �ı�λ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonPNormal_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if (this.radioButtonPNormal.Checked) m_pSymbol.Position = esriTextPosition.esriTPNormal;
			else if (this.radioButtonSubscript.Checked) m_pSymbol.Position = esriTextPosition.esriTPSubscript;
			else if (this.radioButtonSuperscript.Checked) m_pSymbol.Position = esriTextPosition.esriTPSuperscript;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// �ı���Сд
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonCNormal_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if (this.radioButtonCNormal.Checked) m_pSymbol.Case = esriTextCase.esriTCNormal;
			else if (this.radioButtonCAllCaps.Checked) m_pSymbol.Case = esriTextCase.esriTCAllCaps;
			else if (this.radioButtonCSmallCaps.Checked) m_pSymbol.Case = esriTextCase.esriTCSmallCaps;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// �ַ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownCharSpacing_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.CharacterSpacing = (double)this.numericUpDownCharSpacing.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// �о�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownLeading_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.Leading = (double)this.numericUpDownLeading.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// ��ת�Ƕ�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownFlipAngle_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.FlipAngle = (double)this.numericUpDownFlipAngle.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// �ַ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void numericUpDownCharWidth_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.CharacterWidth = (double)this.numericUpDownCharWidth.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// ���ʼ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownWordSpacing_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.WordSpacing = (double)this.numericUpDownWordSpacing.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		
        /// <summary>
		/// �־����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBoxKerning_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.Angle = (double)this.numericUpDownFlipAngle.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol);
		}
		#endregion
	}
}
