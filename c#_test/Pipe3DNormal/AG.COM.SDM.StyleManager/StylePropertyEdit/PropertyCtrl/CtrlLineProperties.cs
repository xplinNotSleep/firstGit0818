using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// ������
	/// </summary>
	public partial class CtrlLineProperties : CtrlPropertyBase
	{
		/// <summary>
		/// ��ǰ����
		/// </summary>
		private ILineProperties m_pSymbol = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
		public CtrlLineProperties()
		{
			InitializeComponent();
		}

		private void CtrlLineProperties_Load(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol != null)
			{
				m_pSymbol = m_pCtrlSymbol as ILineProperties;
				// ���ݷ����������õ�ǰ����ؼ�ֵ
				if (null != m_pSymbol)
				{
					this.numericUpDownOffset.Value = (decimal)m_pSymbol.Offset;
					ILineDecoration pLineDecoration = m_pSymbol.LineDecoration;
					if (null == m_pSymbol) this.radioButtonNone.Checked = true;
					else 
					{

					}
				}
			}
			m_bInitComplete = true;
		}

		#region �û���������ֵ
		/// <summary>
		/// ƫ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownOffset_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			m_pSymbol.Offset = (double)this.numericUpDownOffset.Value;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		
        /// <summary>
		/// ��װ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonNone_CheckedChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			if (this.radioButtonNone.Checked) m_pSymbol.LineDecoration = null;
			else if (this.radioButtonLeftArrow.Checked)
			{
				ILineDecoration lineDecoration = new LineDecoration();
				ISimpleLineDecorationElement decElement = new SimpleLineDecorationElement();
				decElement.MarkerSymbol = new ArrowMarkerSymbolClass();
				decElement.AddPosition(0);
				decElement.Rotate = false;
				lineDecoration.AddElement(decElement);
				m_pSymbol.LineDecoration = lineDecoration;
			}
			else if (this.radioButtonRightArrow.Checked)
			{
				ILineDecoration lineDecoration = new LineDecoration();
				ISimpleLineDecorationElement decElement = new SimpleLineDecorationElement();
				decElement.MarkerSymbol = new ArrowMarkerSymbolClass();
				decElement.AddPosition(1);
				decElement.Rotate = false;
				lineDecoration.AddElement(decElement);
				m_pSymbol.LineDecoration = lineDecoration;
			}
			else if (this.radioButtonBothArrow.Checked)
			{
				ILineDecoration lineDecoration = new LineDecoration();
				ISimpleLineDecorationElement decElement = new SimpleLineDecorationElement();
				decElement.MarkerSymbol = new ArrowMarkerSymbolClass();
				decElement.AddPosition(0);
				decElement.AddPosition(1);
				decElement.Rotate = false;
				lineDecoration.AddElement(decElement);
				m_pSymbol.LineDecoration = lineDecoration;
			}
			else return;
			this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
		}
		
        /// <summary>
		/// ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnProperties_Click(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			FormSymbolPropertyEdit frm = null;
			
			// ����һ��
			IClone pClone = null;
			if (m_pSymbol.LineDecoration != null) pClone = m_pSymbol.LineDecoration as IClone;
			else 
			{
				ILineDecoration lineDecoration = new LineDecoration();
				ISimpleLineDecorationElement decElement = new SimpleLineDecorationElement();
				decElement.MarkerSymbol = new ArrowMarkerSymbolClass();
				decElement.AddPosition(0);
				decElement.Rotate = false;
				lineDecoration.AddElement(decElement);
				pClone = lineDecoration as IClone;
			}
			if (null == pClone) return;
			frm = new FormSymbolPropertyEdit((ILineDecoration)pClone.Clone());
			frm.HasCheckBoxes = false;
			if (frm.ShowDialog(this) == DialogResult.OK)
			{
				m_pSymbol.LineDecoration = frm.Symbol as ILineDecoration;
				this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
				this.radioButtonNone.Checked = false;
				this.radioButtonLeftArrow.Checked = false;
				this.radioButtonRightArrow.Checked = false;
				this.radioButtonBothArrow.Checked = false;
			}
		}
		#endregion
	}
}
