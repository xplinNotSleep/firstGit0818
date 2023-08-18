using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace AG.Pipe.Analyst3DModel
{
    /// <summary>
    /// 矩形
    /// </summary>
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    /// <summary>
    /// 点
    /// </summary>
    public struct POINTAPI
    {
        public int x;
        public int y;
    }


    public class PointInfo
    {
        public string x="";
        public string y = "";
        public string z = "";
    }
    /// <summary>
    /// WinAPI引用
    /// </summary>
    public static class WinAPI
    {
        /// <summary>
        /// 获取光标当前位置
        /// </summary>
        /// <param name="lpPoint"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
        public static extern int GetCursorPos(
            ref POINTAPI lpPoint
        );


        [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
        public static extern int GetWindowRect(
            int hwnd,
            ref RECT lpRect
        );

        /// <summary>
        /// 获取窗体句柄
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowDC")]
        public static extern int GetWindowDC(
            int hwnd
        );

        /// <summary>
        /// 释放设备资源
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="hdc"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        public static extern int ReleaseDC(
            int hwnd,
            int hdc
        );
    }
}
