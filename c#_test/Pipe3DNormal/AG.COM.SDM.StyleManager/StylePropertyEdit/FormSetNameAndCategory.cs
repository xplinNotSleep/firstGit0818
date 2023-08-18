using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AG.COM.SDM.StylePropertyEdit
{
	/// <summary>
	/// 设置名称和分类
	/// </summary>
	public partial class FormSetNameAndCategory : Form
	{
		/// <summary>
        /// 获取或设置名称
		/// </summary>
		public string StyleName
		{
			get { return this.textBoxName.Text; }
			set { this.textBoxName.Text = value; }
		}
		
        /// <summary>
        /// 获取或设置分类
		/// </summary>
		public string StyleCategory
		{
			get { return this.comboBoxCategory.Text; }
			set { this.comboBoxCategory.Text = value; }
		}

		public FormSetNameAndCategory(IList<string> listCategory)
		{
			InitializeComponent();
			// 添加分类
			foreach (string strCategory in listCategory)
			{
				this.comboBoxCategory.Items.Add(strCategory);
			}
		}
		
        /// <summary>
		/// 确定
		/// </summary>
		private void btnOK_Click(object sender, EventArgs e)
		{
			string strName = this.textBoxName.Text.Trim();
			string strCategory = this.comboBoxCategory.Text.Trim();
			if(string.IsNullOrEmpty(strName)) 
			{
				MessageBox.Show("请输入符号名称！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			this.textBoxName.Text = strName;
			this.comboBoxCategory.Text = strCategory;

			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}