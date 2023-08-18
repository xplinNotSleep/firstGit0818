using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// 字体组合框控件
	/// </summary>
	public partial class CtrlComboBoxFont : UserControl
	{
		/// <summary>
		/// 字体组合框
		/// </summary>
		public ComboBox ComboBoxFont
		{
			get { return this.comboBoxFont; }
		}
		
        /// <summary>
		/// 控制组合框外观和功能
		/// </summary>
		[Description("控制组合框外观和功能")]
		public ComboBoxStyle DropDownStyle
		{
			get { return this.comboBoxFont.DropDownStyle; }
			set { this.comboBoxFont.DropDownStyle = value; }
		}

        /// <summary>
        /// 默认构造函数
        /// </summary>
		public CtrlComboBoxFont()
		{
			InitializeComponent();
			GetInstalledFont();
		}
		
        /// <summary>
		/// 获取系统已经安装的字体
		/// </summary>
		private void GetInstalledFont()
		{
			InstalledFontCollection installedFont = new InstalledFontCollection();
			FontFamily[] fontFamilies = installedFont.Families;
			int iCount = fontFamilies.Length;
			for (int i = 0; i < iCount; i++)
			{
				string FontName = fontFamilies[i].Name;
				this.comboBoxFont.Items.Add(FontName);
			}
		}
	
        /// <summary>
		/// 设置当前选择的字体
		/// </summary>
		/// <param name="strFontName">字体名称</param>
		public void SetCurrentFont(string strFontName)
		{
			this.comboBoxFont.SelectedIndex = comboBoxFont.FindString(strFontName, 0);
		}
	}
}
