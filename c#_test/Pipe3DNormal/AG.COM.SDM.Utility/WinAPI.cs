using System.Runtime.InteropServices;

namespace AG.COM.SDM.Utility.Win32APIs
{      
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

     
    public struct POINTAPI {
	    public int x;
	    public int y;
    }

    public static class WinAPI
    {
        [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
        public static extern int GetCursorPos(
            ref POINTAPI lpPoint
        );

        [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
        public static extern int GetWindowRect(
            int hwnd,
            ref RECT lpRect
        );

        [DllImport("user32.dll", EntryPoint = "GetWindowDC")]
        public static extern int GetWindowDC(
            int hwnd
        );

        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        public static extern int ReleaseDC(
            int hwnd,
            int hdc
        );
    }
}
