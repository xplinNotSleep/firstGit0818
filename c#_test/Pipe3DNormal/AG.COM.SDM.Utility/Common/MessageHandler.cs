using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility.Common
{
    /// <summary>
    /// 基础的关于窗体显示类
    /// </summary>
    public class MessageHandler
    {
        /// <summary>
        /// 弹出错误信息窗口
        /// </summary>
        /// <param name="ex">异常信息</param>
        public static void ShowErrorMsg(Exception ex)
        {
            MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 弹出错误信息窗口（此函数已过时，请勿用此函数）
        /// </summary>
        /// <param name="info">错误信息</param>
        /// <param name="ex">Exception对象<see cref="Exception"/></param>       
        public static void ShowErrorMsg(string info, Exception ex)
        {
            MessageBox.Show(info, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 弹出错误信息窗口
        /// </summary>
        /// <param name="info">提示信息</param>
        /// <param name="title">标题信息</param>
        public static void ShowErrorMsg(string info, string title)
        {
            MessageBox.Show(info, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 弹出错误信息窗口（此函数已过时，请勿用此函数）
        /// </summary>
        /// <param name="info">提示信息</param>
        /// <param name="title">标题信息</param>
        /// <param name="ex">Exception对象<see cref="Exception"/></param>  
        public static void ShowErrorMsg(string info, string title, Exception ex)
        {
            MessageBox.Show(info, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 弹出提示信息窗口
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <param name="title">标题</param>
        public static void ShowInfoMsg(string msg, string title)
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
