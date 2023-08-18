using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using System;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// 公共变量类
    /// </summary>
    public class CommonVariables
    {
        #region 私有字段
        //系统变量是否已初始化
        private static bool m_HasInitialized = false;
        #endregion 

        #region 静态公有属性
        /// <summary>
        /// 保存系统选项的文件
        /// </summary>
        public static readonly string ConfigFile = CommonConstString.STR_ConfigPath + "\\appconfig.resx";

        /// <summary>
        /// 符号样式文件
        /// </summary>
        public static string StyleFiles;

        /// <summary>
        /// 通用的出错提示信息头部分
        /// </summary>
        public static readonly string ErroMsgHead = "操作失败。原因是：";

        public static bool IsClosed = false;

        #endregion 

        #region     私有方法
        /// <summary>
        /// 获取默认咬合点的样式
        /// </summary>
        /// <returns>返回点样式</returns>
        private static IMarkerSymbol GetDefaultSnapSymbol()
        {
            IMarkerSymbol sym = new SimpleMarkerSymbolClass();
            sym.Color.Transparency = 255;
            sym.Size = 4;

            (sym as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSSquare;
            (sym as ISimpleMarkerSymbol).OutlineColor = ESRI.ArcGIS.ADF.Converter.ToRGBColor(System.Drawing.Color.LightBlue) as IColor;
            (sym as ISimpleMarkerSymbol).OutlineSize = 1;
            return sym;
        }
        #endregion 
    }

    /// <summary>
    /// 文件路径参数类
    /// </summary>
    public class CommonConstString
    {            
        /// <summary>
        /// 应用程序上级目录路径
        /// </summary>
        public static readonly string STR_PreAppPath = System.IO.Directory.GetParent(Application.StartupPath).FullName;

        /// <summary>
        /// 模板方案默认文件夹路径
        /// </summary>
        public static readonly string STR_TemplatePath = string.Format("{0}\\Template", STR_PreAppPath);

        /// <summary>
        /// 样式符号文件夹路径
        /// </summary>
        public static readonly string STR_StylePath = string.Format("{0}\\Styles", STR_PreAppPath);

        /// <summary>
        /// 配置文件夹路径
        /// </summary>
        public static readonly string STR_ConfigPath = string.Format("{0}\\Config", STR_PreAppPath); 

        /// <summary>
        /// 临时文件夹路径
        /// </summary>
        public static readonly string STR_TempPath = string.Format("{0}\\Temp", STR_PreAppPath);

        /// <summary>
        /// 数据文件夹路径
        /// </summary>
        public static readonly string STR_DataPath = string.Format("{0}\\Data", STR_PreAppPath);
        /// <summary>
        /// 
        /// </summary>
        public static readonly string STR_ChartPath = string.Format("{0}\\Chart", STR_PreAppPath);

        /// <summary>
        /// NHiberate所需编译模块名称
        /// </summary>
        public static readonly string STR_ModelName = "AG.COM.SDM.Model";

        /// <summary>
        /// AGSDM OLE 系统表标识
        /// </summary>
        public static readonly string STR_AGSDMOleName = "AGSDM系统表连接";

        /// <summary>
        /// 帮助文件文件夹路径
        /// </summary>
        public static string STR_HelpPath = string.Format("{0}\\Help", STR_PreAppPath);

        /// <summary>
        /// 配置文件中当前皮肤的key
        /// </summary>
        public static readonly string SkinKeyInConfig = "DefaultQIOSSkinName";

        /// <summary>
        /// UIDesign本地Xml文件路径
        /// </summary>
        public static readonly string STR_UIDesignXml = CommonConstString.STR_ConfigPath + "\\MainMenu.xml";

        /// <summary>
        /// UIDesign功能绑定本地Xml文件路径
        /// </summary>
        public static readonly string STR_BindFunXml = CommonConstString.STR_ConfigPath + "\\MainMenu_BindFun.xml";

        /// <summary>
        /// UIDesign预览图片路径
        /// </summary>
        public static readonly string STR_UIDesignPreview = CommonConstString.STR_TempPath + "\\UIDesignPreview.png";

        /// <summary>
        /// 工具条布局
        /// </summary>
        public static readonly string STR_ToolBarLayout = CommonConstString.STR_ConfigPath + "\\QToolBarLayout.txt";

        /// <summary>
        /// 菜单配置文件路径
        /// </summary>
        public static readonly string MENUCONFIG_FILE = string.Format("{0}\\MenuConfig.xml", CommonConstString.STR_ConfigPath);
    }   
}
