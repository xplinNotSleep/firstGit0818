using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility.Common
{
    /// <summary>
    /// �����Ĺ��ڴ�����ʾ��
    /// </summary>
    public class MessageHandler
    {
        /// <summary>
        /// ����������Ϣ����
        /// </summary>
        /// <param name="ex">�쳣��Ϣ</param>
        public static void ShowErrorMsg(Exception ex)
        {
            MessageBox.Show(ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// ����������Ϣ���ڣ��˺����ѹ�ʱ�������ô˺�����
        /// </summary>
        /// <param name="info">������Ϣ</param>
        /// <param name="ex">Exception����<see cref="Exception"/></param>       
        public static void ShowErrorMsg(string info, Exception ex)
        {
            MessageBox.Show(info, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// ����������Ϣ����
        /// </summary>
        /// <param name="info">��ʾ��Ϣ</param>
        /// <param name="title">������Ϣ</param>
        public static void ShowErrorMsg(string info, string title)
        {
            MessageBox.Show(info, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// ����������Ϣ���ڣ��˺����ѹ�ʱ�������ô˺�����
        /// </summary>
        /// <param name="info">��ʾ��Ϣ</param>
        /// <param name="title">������Ϣ</param>
        /// <param name="ex">Exception����<see cref="Exception"/></param>  
        public static void ShowErrorMsg(string info, string title, Exception ex)
        {
            MessageBox.Show(info, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// ������ʾ��Ϣ����
        /// </summary>
        /// <param name="msg">��ʾ��Ϣ</param>
        /// <param name="title">����</param>
        public static void ShowInfoMsg(string msg, string title)
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
