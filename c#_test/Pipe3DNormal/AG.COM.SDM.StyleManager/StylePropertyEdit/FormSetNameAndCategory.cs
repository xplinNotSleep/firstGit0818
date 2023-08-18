using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AG.COM.SDM.StylePropertyEdit
{
	/// <summary>
	/// �������ƺͷ���
	/// </summary>
	public partial class FormSetNameAndCategory : Form
	{
		/// <summary>
        /// ��ȡ����������
		/// </summary>
		public string StyleName
		{
			get { return this.textBoxName.Text; }
			set { this.textBoxName.Text = value; }
		}
		
        /// <summary>
        /// ��ȡ�����÷���
		/// </summary>
		public string StyleCategory
		{
			get { return this.comboBoxCategory.Text; }
			set { this.comboBoxCategory.Text = value; }
		}

		public FormSetNameAndCategory(IList<string> listCategory)
		{
			InitializeComponent();
			// ��ӷ���
			foreach (string strCategory in listCategory)
			{
				this.comboBoxCategory.Items.Add(strCategory);
			}
		}
		
        /// <summary>
		/// ȷ��
		/// </summary>
		private void btnOK_Click(object sender, EventArgs e)
		{
			string strName = this.textBoxName.Text.Trim();
			string strCategory = this.comboBoxCategory.Text.Trim();
			if(string.IsNullOrEmpty(strName)) 
			{
				MessageBox.Show("������������ƣ�", "��Ϣ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			this.textBoxName.Text = strName;
			this.comboBoxCategory.Text = strCategory;

			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}