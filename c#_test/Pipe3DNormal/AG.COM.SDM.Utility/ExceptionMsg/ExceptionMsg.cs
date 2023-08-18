using System;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility
{
	/// <summary>
	/// ��ʾ�쳣��Ϣ
	/// </summary>
	public class ExceptionMsg
	{
		/// <summary>
		/// ��ʾ�쳣��Ϣ
		/// </summary>
		/// <param name="e">�쳣</param>
		public static void ShowExceptionMsg(Exception e)
		{
            MessageBox.Show(e.Message);
            FormException ex = new FormException(e);
            ex.ShowDialog();
        }

        /// <summary>
        /// ��ʾ�쳣��Ϣ
        /// </summary>
        /// <param name="e">�쳣</param>
        /// <param name="caption">����</param>
        /// <param name="content">����</param>
        public static void ShowExceptionMsg(Exception e, string caption, string content)
        {
            MessageBox.Show(content,caption,MessageBoxButtons.OK,MessageBoxIcon.Error);
        }
	}
}
