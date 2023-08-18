using System;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility
{
	public partial class FormException : Form
	{
		public FormException(Exception e)
		{
			InitializeComponent();

			// 异常信息
			textBox3.Text += e.Message;

			// 异常帮助链接
			label2.Text += e.HelpLink;

			// 导致异常的对象
			label3.Text += e.Source;

			// 异常堆栈
			textBox1.Text += e.StackTrace;

			// 导致异常的方法
			textBox2.Text += e.TargetSite.ToString();
		}
	}
}