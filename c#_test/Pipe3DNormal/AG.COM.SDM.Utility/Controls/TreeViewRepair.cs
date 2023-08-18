using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// 继承.Net自带TreeView的控件，用于解决一些bug，目前解决的bug有：
    /// 1.在Win7、vista下，双击只触发一次AfterCheck事件，但Checked已经改变两次。解决方法是屏蔽双击事件
    /// </summary>
    public class TreeViewRepair : TreeView
    {
        /// <summary>
        /// 屏蔽双击事件
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            // Suppress WM_LBUTTONDBLCLK
            if (m.Msg == 0x203) { m.Result = IntPtr.Zero; }
            else base.WndProc(ref m);
        }
    }
}