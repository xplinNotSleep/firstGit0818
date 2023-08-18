using System;
using System.Collections.Generic;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	public partial class CtrlTemplate : CtrlPropertyBase
	{
		/// <summary>
		/// 当前符号
		/// </summary>
		private ILineProperties m_pSymbol = null;

        /// <summary>
        /// 默认构造函数
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
				// 根据符号属性设置当前窗体控件值
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

		#region 用户更改属性值
		/// <summary>
		/// 清除
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
		/// 间隔
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
		/// 模板项改变
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
