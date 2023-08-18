using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace AG.COM.MapSoft.Tool
{
    /// <summary>
    /// 软件用到的常量字符串
    /// </summary>
    public class CommonConstString
    {
        /// <summary>
        /// 应用程序上级目录路径
        /// </summary>
        //public static readonly string STR_PreAppPath = System.IO.Directory.GetCurrentDirectory();
        public static readonly string STR_PreAppPath = System.IO.Directory.GetParent(Application.StartupPath).FullName;

        /// <summary>
        /// 配置文件夹路径
        /// </summary>
        public static readonly string STR_ConfigPath = string.Format("{0}\\Config\\", STR_PreAppPath);

        /// <summary>
        /// 管点符号库路径
        /// </summary>
        public static readonly string STR_SymbolPath = string.Format("{0}\\Symbol\\", STR_PreAppPath);

        /// <summary>
        /// 错误日志文件保存路径
        /// </summary>
        public static readonly string STR_ErrorPath = string.Format("{0}\\ErrorLog\\", STR_PreAppPath);
        /// <summary>
        /// 数据文件夹路径
        /// </summary>
        public static readonly string STR_DataPath = string.Format("{0}\\Data\\", STR_PreAppPath);
        /// <summary>
        /// Report文件夹路径
        /// </summary>
        public static readonly string STR_ReportPath = string.Format("{0}\\Report\\", STR_PreAppPath);
        /// <summary>
        /// Report文件夹路径
        /// </summary>
        public static readonly string STR_ImagePath = string.Format("{0}\\Image\\", STR_PreAppPath);
    }
}
