using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using AG.COM.SDM.Utility;
using AG.COM.SDM.Utility.Common;

namespace AG.Pipe.Analyst3DModel
{
    /// <summary>
    /// 系统配置信息类(单例对象)
    /// </summary>
    public class SysConfig
    {
        private static SysConfig m_SysConfig;
        private string m_DataSchemeFile;  //默认检查方案路径                  
        private ITrackProgress m_TrackProgress;
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 私有化对象实例
        /// </summary>
        private SysConfig()
        {
            this.m_DataSchemeFile = CommonConstString.STR_ConfigPath + "\\PipeConvert.xml";
            this.m_TrackProgress = new TrackProgressDialog();            
        }

        /// <summary>
        /// 获取SysConfig实例对象
        /// </summary>
        /// <returns>返回SysConfig单件实例</returns>
        public static SysConfig GetInstance()
        {
            if (m_SysConfig == null)
            {
                m_SysConfig = new SysConfig();
            }

            return m_SysConfig;
        }

        /// <summary>
        /// 获取或设置检查方案路径
        /// </summary>
        public string DataSchemeFile
        {
            get
            {
                return this.m_DataSchemeFile;
            }
            set
            {
                this.m_DataSchemeFile = value;
            }
        }

    }
}
