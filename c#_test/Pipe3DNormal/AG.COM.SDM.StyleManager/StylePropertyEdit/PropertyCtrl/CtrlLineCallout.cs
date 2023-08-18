using System;
using System.Windows.Forms;
using AG.COM.SDM.StyleManager;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
    /// <summary>
    /// 线状提示框
    /// </summary>
	public partial class CtrlLineCallout : CtrlPropertyBase
	{
		/// <summary>
		/// 当前符号
		/// </summary>
		private ILineCallout m_pSymbol = null;
		private ITextMargins m_pSymbol2 = null;

        /// <summary>
        /// 默认构造函数
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
				// 根据符号属性设置当前窗体控件值
				if (null != m_pSymbol)
				{
					// 间隙
					this.numericUpDownGap.Value = (decimal)m_pSymbol.Gap;
					// 引导容限
					this.numericUpDownLeader.Value = (decimal)m_pSymbol.LeaderTolerance;
					// 引导线
					this.checkBoxLeader.Checked = m_pSymbol.LeaderLine != null;
					// 强调线
					this.checkBoxAccentBar.Checked = m_pSymbol.AccentBar != null;
					// 边界线
					this.checkBoxBorder.Checked = m_pSymbol.Border != null;
					// 样式
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
					// 页边距
					this.numericUpDownLeft.Value = (decimal)m_pSymbol2.LeftMargin;
					this.numericUpDownRight.Value = (decimal)m_pSymbol2.RightMargin;
					this.numericUpDownUp.Value = (decimal)m_pSymbol2.RightMargin;
					this.numericUpDownDown.Value = (decimal)m_pSymbol2.BottomMargin;
				}
			}
			m_bInitComplete = true;
		}

		#region 用户更改属性值
		/// <summary>
		/// 间隙
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
		/// 引导容限
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
		/// 引导线
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
		/// 选择引导线符号
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
		/// 强调线
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
		/// 选择强调线符号
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
		/// 边界线
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
		/// 选择边界线符号
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
		/// 样式
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

		#region 页边距
		/// <summary>
		/// 页边距左
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
		/// 页边距上
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
		/// 页边距右
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
		/// 页边距下
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
