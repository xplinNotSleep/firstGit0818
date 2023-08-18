using System;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// �򵥱��
	/// </summary>
	public partial class CtrlSimpleMarker : CtrlPropertyBase
	{
		/// <summary>
		/// ��ǰ����
		/// </summary>
		private ISimpleMarkerSymbol m_pSymbol = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
		public CtrlSimpleMarker()
		{
			InitializeComponent();
		}

		private void CtrlSimpleMarker_Load(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol != null)
			{
				m_pSymbol = m_pCtrlSymbol as ISimpleMarkerSymbol;
				// ���ݷ����������õ�ǰ����ؼ�ֵ
				if (null != m_pSymbol)
				{
					this.colorPickerSymbol.Value = StyleCommon.GetColor(m_pSymbol.Color);
					this.comboBoxSMStyle.SelectedIndex = (int)m_pSymbol.Style;
					this.numericUpDownSMSize.Value = (decimal)m_pSymbol.Size;
					this.numericUpDownXOffset.Value = (decimal)m_pSymbol.XOffset;
					this.numericUpDownYOffset.Value = (decimal)m_pSymbol.YOffset;
					this.checkBoxUseOutline.Checked = m_pSymbol.Outline;
					this.colorPickerOutline.Value = StyleCommon.GetColor(m_pSymbol.OutlineColor);
					this.numericUpDownOutlineSize.Value = (decimal)m_pSymbol.OutlineSize;
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
			if (-1 != this.comboBoxSMStyle.SelectedIndex) m_pSymbol.Style = (esriSimpleMarkerStyle)this.comboBoxSMStyle.SelectedIndex;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
	
        /// <summary>
		/// ��С
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownSMSize_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if (this.numericUpDownSMSize.Value > 0) m_pSymbol.Size = (double)this.numericUpDownSMSize.Value;
			else this.numericUpDownSMSize.Value = (decimal)m_pSymbol.Size;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
	
        /// <summary>
		/// Xƫ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownXOffset_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.XOffset = (double)this.numericUpDownXOffset.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
	
        /// <summary>
		/// Yƫ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownYOffset_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.YOffset = (double)this.numericUpDownYOffset.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
	
        /// <summary>
		/// ʹ��������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBoxUseOutline_CheckedChanged(object sender, EventArgs e)
		{
			this.colorPickerOutline.Enabled = this.checkBoxUseOutline.Checked;
			this.numericUpDownOutlineSize.Enabled = this.checkBoxUseOutline.Checked;
			// ���·���Ԥ��
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.Outline = this.checkBoxUseOutline.Checked;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
	
        /// <summary>
		/// ��������ɫ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void colorPickerOutline_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.OutlineColor = StyleCommon.GetRgbColor(this.colorPickerOutline.Value);
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
	
        /// <summary>
		/// �����ߴ�С
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownOutlineSize_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.OutlineSize = (double)this.numericUpDownOutlineSize.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		#endregion
	}
}
