using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.IO;
namespace AG.COM.MapSoft.Tool
{
    /// <summary>
    /// 异常记录
    /// </summary>
    public class MapSoftLog
    {
        /// <summary>
        /// 日志对象
        /// </summary>
        private static log4net.ILog m_ExceptionLog;

        /// <summary>
        /// 日志对象
        /// </summary>
        public static log4net.ILog Log
        {
            get
            {
                if (m_ExceptionLog == null)
                {
                    m_ExceptionLog = GetLog();
                }

                return m_ExceptionLog;
            }
        }

        private static log4net.ILog GetLog()
        {
            try
            {
                //获取配置文件路径
                string m_Config_File_Path = CommonConstString.STR_PreAppPath + "\\log4net.config";
                if (System.IO.File.Exists(m_Config_File_Path))
                {
                    //加载配置信息
                    log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(m_Config_File_Path));
                    //生成日志对象
                    log4net.ILog log = LogManager.GetLogger("AppLog");

                    log4net.Core.LogImpl logImpl = log as log4net.Core.LogImpl;
                    if (logImpl != null)
                    {
                        log4net.Appender.AppenderCollection ac = ((log4net.Repository.Hierarchy.Logger)logImpl.Logger).Appenders;
                        for (int i = 0; i < ac.Count; i++)
                        {    //这里我只对RollingFileAppender类型做修改
                            log4net.Appender.RollingFileAppender rfa = ac[i] as log4net.Appender.RollingFileAppender;
                            if (rfa != null)
                            {
                                //修改路径
                                string path = System.IO.Path.GetDirectoryName(rfa.File);
                                if (path != null)
                                {
                                    int positon = path.LastIndexOf("\\", StringComparison.Ordinal);
                                    path = path.Substring(0, positon);
                                    rfa.File = rfa.File.Replace(path, CommonConstString.STR_PreAppPath);
                                }

                                if (File.Exists(rfa.File) == false)
                                {
                                    FileStream fr = new FileStream(rfa.File, FileMode.Create, FileAccess.Write);
                                    fr.Close();
                                }
                                //更新Writer属性
                                rfa.Writer = new System.IO.StreamWriter(rfa.File, rfa.AppendToFile, rfa.Encoding);
                            }
                        }
                    }
                    return log;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("加载日志对象出错" + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="Message">错误信息</param>
        /// <param name="ex">Exception对象</param>
        public static void LogError(string Message, Exception ex)
        {
            log4net.ILog tLog = Log;
            if (tLog != null)
            {
                tLog.Error(Message, ex);
            }
        }
        public static void LogError(string Message)
        {
            log4net.ILog tLog = Log;
            if (tLog != null)
            {
                tLog.Error(Message);
            }
        }
    }
}
