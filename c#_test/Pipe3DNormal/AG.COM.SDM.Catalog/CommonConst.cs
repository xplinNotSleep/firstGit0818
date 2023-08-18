//9,10不同
using System;

namespace AG.COM.SDM.Catalog
{
    /// <summary>
    /// 一般参数类
    /// </summary>
    public class CommonConst
    {
        /// <summary>
        /// arcgis运行环境版本
        /// </summary>
        private static string tVersion = ESRI.ArcGIS.RuntimeManager.ActiveRuntime.Version;
        /// <summary>
        /// 连接文件的保存地址
        /// </summary>
        public static string m_ConnectPropertyPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ESRI\Desktop"+tVersion+@"\ArcCatalog";
    }
}
