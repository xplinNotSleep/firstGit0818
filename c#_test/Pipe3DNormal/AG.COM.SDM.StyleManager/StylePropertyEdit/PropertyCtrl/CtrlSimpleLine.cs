using System;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// ����
	/// </summary>
	public partial class CtrlSimpleLine : CtrlPropertyBase
	{
		/// <summary>
		/// ��ǰ����
		/// </summary>
		private ISimpleLineSymbol m_pSymbol = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
		public CtrlSimpleLine()
		{
			InitializeComponent();
		}

		private void CtrlSimpleLine_Load(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol != null)
			{
				m_pSymbol = m_pCtrlSymbol as ISimpleLineSymbol;
				// ���ݷ����������õ�ǰ����ؼ�ֵ
				if (null != m_pSymbol)
				{
					this.colorPickerSymbol.Value = StyleCommon.GetColor(m_pSymbol.Color);
					this.numericUpDownWidth.Value = (decimal)m_pSymbol.Width;
					this.comboBoxSMStyle.SelectedIndex = (int)m_pSymbol.Style;
					if (this.comboBoxSMStyle.SelectedIndex > 0)
					{
						this.numericUpDownWidth.Enabled = false;
						this.numericUpDownWidth.Value = 1.0M;
					}
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
		/// ��ʽ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBoxSMStyle_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if (this.comboBoxSMStyle.SelectedIndex != -1) m_pSymbol.Style = (esriSimpleLineStyle)this.comboBoxSMStyle.SelectedIndex;
			if (this.comboBoxSMStyle.SelectedIndex > 0)
			{
				this.numericUpDownWidth.Enabled = false;
				this.numericUpDownWidth.Value = 1.0M;
			}
			else this.numericUpDownWidth.Enabled = true;
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
		#endregion
	}
}
