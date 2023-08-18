using System;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility
{
	public partial class FormException : Form
	{
		public FormException(Exception e)
		{
			InitializeComponent();

			// �쳣��Ϣ
			textBox3.Text += e.Message;

			// �쳣��������
			label2.Text += e.HelpLink;

			// �����쳣�Ķ���
			label3.Text += e.Source;

			// �쳣��ջ
			textBox1.Text += e.StackTrace;

			// �����쳣�ķ���
			textBox2.Text += e.TargetSite.ToString();
		}
	}
}