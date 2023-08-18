using System;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility
{
	/// <summary>
	/// 显示异常信息
	/// </summary>
	public class ExceptionMsg
	{
		/// <summary>
		/// 显示异常信息
		/// </summary>
		/// <param name="e">异常</param>
		public static void ShowExceptionMsg(Exception e)
		{
            MessageBox.Show(e.Message);
            FormException ex = new FormException(e);
            ex.ShowDialog();
        }

        /// <summary>
        /// 显示异常信息
        /// </summary>
        /// <param name="e">异常</param>
        /// <param name="caption">标题</param>
        /// <param name="content">内容</param>
        public static void ShowExceptionMsg(Exception e, string caption, string content)
        {
            MessageBox.Show(content,caption,MessageBoxButtons.OK,MessageBoxIcon.Error);
        }
	}
}
