using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.StylePropertyEdit.PropertyCtrl
{
	/// <summary>
	/// ������Ͽ�ؼ�
	/// </summary>
	public partial class CtrlComboBoxFont : UserControl
	{
		/// <summary>
		/// ������Ͽ�
		/// </summary>
		public ComboBox ComboBoxFont
		{
			get { return this.comboBoxFont; }
		}
		
        /// <summary>
		/// ������Ͽ���ۺ͹���
		/// </summary>
		[Description("������Ͽ���ۺ͹���")]
		public ComboBoxStyle DropDownStyle
		{
			get { return this.comboBoxFont.DropDownStyle; }
			set { this.comboBoxFont.DropDownStyle = value; }
		}

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
		public CtrlComboBoxFont()
		{
			InitializeComponent();
			GetInstalledFont();
		}
		
        /// <summary>
		/// ��ȡϵͳ�Ѿ���װ������
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
		/// ���õ�ǰѡ�������
		/// </summary>
		/// <param name="strFontName">��������</param>
		public void SetCurrentFont(string strFontName)
		{
			this.comboBoxFont.SelectedIndex = comboBoxFont.FindString(strFontName, 0);
		}
	}
}
