using System;
using System.Collections.Generic;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	public partial class CtrlTemplate : CtrlPropertyBase
	{
		/// <summary>
		/// ��ǰ����
		/// </summary>
		private ILineProperties m_pSymbol = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
		public CtrlTemplate()
		{
			InitializeComponent();
		}

		private void CtrlTemplate_Load(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol != null)
			{
				m_pSymbol = m_pCtrlSymbol as ILineProperties;
				// ���ݷ����������õ�ǰ����ؼ�ֵ
				if (null != m_pSymbol)
				{
					ITemplate pTemplate = m_pSymbol.Template;
					if(null == pTemplate) 
					{
						pTemplate = new TemplateClass();
						m_pSymbol.Template = pTemplate;
					}
					if(null != pTemplate)
					{
						this.numericUpDownInterval.Value = (decimal)pTemplate.Interval;
						double dMark, dGap;
						for (int i = 0; i < pTemplate.PatternElementCount; i++)
						{
							pTemplate.GetPatternElement(i, out dMark, out dGap);
							this.pictureBoxTemplateLine1.AddPatternElement(dMark, dGap);
						}
					}
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
		private void btnClear_Click(object sender, EventArgs e)
		{
			if (this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			ITemplate pTemplate = m_pSymbol.Template;
			if (null != pTemplate)
			{
				pTemplate.ClearPatternElements();
				this.pictureBoxTemplateLine1.ClearPatternElements();
				this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
			}
		}
		
        /// <summary>
		/// ���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void numericUpDownInterval_ValueChanged(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			ITemplate pTemplate = m_pSymbol.Template;
			if (null != pTemplate)
			{
				if (this.numericUpDownInterval.Value>0) pTemplate.Interval = (double)this.numericUpDownInterval.Value;
				this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
			}
		}
	
        /// <summary>
		/// ģ����ı�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBoxTemplateLine1_ElementChange(object sender, EventArgs e)
		{
			if (!this.m_bInitComplete || this.m_pCtrlSymbol == null || this.m_pSymbolLayer == null || this.m_pSymbol == null) return;
			ITemplate pTemplate = m_pSymbol.Template;
			if (null != pTemplate)
			{
				IList<PatternElement> listPatternElement = this.pictureBoxTemplateLine1.GetPatternElement();
				pTemplate.ClearPatternElements();
				foreach (PatternElement pe in listPatternElement)
				{
					pTemplate.AddPatternElement(pe.Mark, pe.Gap);
				}
				this.m_pSymbolLayer.UpdateLayerView(m_pCtrlSymbol, this.m_iLayerIndex);
			}
		}
		#endregion
	}
}
