using System;
using System.Windows.Forms;

namespace AG.COM.SDM.StylePropertyEdit
{
	/// <summary>
	/// 符号预览控件
	/// </summary>
	public partial class CtrlSymbolPreview : UserControl, ISymbolPreview
	{
		#region 字段/属性
		/// <summary>
		/// 当前编辑的符号类型
		/// </summary>
		EnumSymbolType m_SymbolType = EnumSymbolType.SymbolTypeUnknow;
		
        /// <summary>
		/// 获取或设置当前编辑的符号类型
		/// </summary>
		public EnumSymbolType SymbolType
		{
			set { m_SymbolType = value; }
			get { return m_SymbolType; }
		}
		#endregion

		#region 初始化
		public CtrlSymbolPreview()
		{
			InitializeComponent();
		}

		private void CtrlSymbolPreview_Load(object sender, EventArgs e)
		{
			switch (m_SymbolType)
			{
				case  EnumSymbolType.SymbolTypeMarker:
					{
						this.checkBoxReticle.Visible = true;
						checkBoxReticle_CheckedChanged(null,null);
					}
					break;
				case EnumSymbolType.SymbolTypeLine:
					{
						this.radioButtonBeeline.Visible = true;
						this.radioButtonFoldline.Visible = true;
					}
					break;
				default:
					break;
			}
			this.comboBoxScale.SelectedIndex = 2;
		}
		#endregion

		#region ISymbolPreview 成员
		/// <summary>
		/// 更新视图
		/// </summary>
		/// <param name="pSymbol"></param>
		public void UpdateView(object pSymbol)
		{
			this.pictureBoxPreview.Symbol = pSymbol;
		}
		#endregion

		/// <summary>
		/// 更新显示
		/// </summary>
		public void UpdateView()
		{
			pictureBoxPreview.Invalidate();
		}

		#region 响应按钮
		/// <summary>
		/// 放大
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnZoomIn_Click(object sender, EventArgs e)
		{
			if (comboBoxScale.SelectedIndex > 0) comboBoxScale.SelectedIndex = comboBoxScale.SelectedIndex - 1;
		}
	
        /// <summary>
		/// 缩小
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnZoomOut_Click(object sender, EventArgs e)
		{
			if (comboBoxScale.SelectedIndex < 4) comboBoxScale.SelectedIndex = comboBoxScale.SelectedIndex + 1;
		}
		
        /// <summary>
		/// 缩放到1:1
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnZoomTo11_Click(object sender, EventArgs e)
		{
			this.comboBoxScale.SelectedIndex = 2;
		}
	
        /// <summary>
		/// 更改缩放比例
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBoxScale_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (this.comboBoxScale.SelectedIndex)
			{
				case 0:
					this.pictureBoxPreview.Scale = 4.0;
					break;
				case 1:
					this.pictureBoxPreview.Scale = 2.0;
					break;
				case 2:
					this.pictureBoxPreview.Scale = 1.0;
					break;
				case 3:
					this.pictureBoxPreview.Scale = 0.75;
					break;
				case 4:
					this.pictureBoxPreview.Scale = 0.5;
					break;
				default:
					return;
			}
			this.pictureBoxPreview.Invalidate();
		}
	
        /// <summary>
		/// 线符号显示为直线或折线
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonBeeline_CheckedChanged(object sender, EventArgs e)
		{
			if (this.radioButtonBeeline.Checked) pictureBoxPreview.Beeline = true;
			else pictureBoxPreview.Beeline = false;
			this.pictureBoxPreview.Invalidate();
		}
	
        /// <summary>
		/// 显示十字线
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBoxReticle_CheckedChanged(object sender, EventArgs e)
		{
			this.pictureBoxPreview.ShowReticle = this.checkBoxReticle.Checked;
			this.pictureBoxPreview.Invalidate();
		}
		#endregion
	}
}
