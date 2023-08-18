using System;
using System.Windows.Forms;
using AG.COM.SDM.StyleManager;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
    /// <summary>
    /// 线装饰
    /// </summary>
	public partial class CtrlSimpleLineDecoration : CtrlPropertyBase
	{
		/// <summary>
		/// 当前符号
		/// </summary>
		private ISimpleLineDecorationElement m_pSymbol = null;

        /// <summary>
        /// 默认构造函数
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
				// 根据符号属性设置当前窗体控件值
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
		/// 位置数
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
		/// 全部反向
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
		/// 第一个反向
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
		/// 沿着线角度旋转符号/保持符号的角度相对于页面固定
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
		/// 选择标记符号
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
