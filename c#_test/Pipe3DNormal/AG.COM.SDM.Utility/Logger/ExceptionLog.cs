using System;
using System.Reflection;
using log4net;

namespace AG.COM.SDM.Utility.Logger
{
    /// <summary>
    /// 异常日志
    /// </summary>
    public class ExceptionLog
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
                string m_Config_File_Path = AppDomain.CurrentDomain.BaseDirectory + "log4net.config";
                if (System.IO.File.Exists(m_Config_File_Path))
                {
                    //加载配置信息
                    log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(m_Config_File_Path));
                    //生成日志对象
                    log4net.ILog log = LogManager.GetLogger("AppLog");
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
        /// 警告！要能正常输出日志，得保证系统安装目录下同时存在log4net.dll 和 log4net.config 两个文件！
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
    }

    /// <summary>
    /// Log4Net日志记录辅助类
    /// </summary>
    public class LogHelper
    {
        private static readonly log4net.ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public static void Debug(object ex)
        {
            Log.Debug(ex);
        }

        public static void Warn(object ex)
        {
            Log.Warn(ex);
        }

        public static void Error(object ex)
        {
            Log.Error(ex);
        }

        public static void Info(object ex)
        {
            Log.Info(ex);
        }

        public static void Debug(object message, Exception ex)
        {
            Log.Debug(message, ex);
        }

        public static void Warn(object message, Exception ex)
        {
            Log.Warn(message, ex);
        }

        public static void Error(object message, Exception ex)
        {
            Log.Error(message, ex);
        }

        public static void Info(object message, Exception ex)
        {
            Log.Info(message, ex);
        }
    }
}
