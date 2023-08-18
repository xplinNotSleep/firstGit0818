using System;
using System.Net.NetworkInformation;
using System.Text;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// 网络相关的帮助类
    /// </summary>
    public class NetHelper
    {
        /// <summary>
        /// Ping IP是否连接
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public static bool Ping(string IP)
        {
            try
            {
                Ping tPint = new Ping();
                PingOptions tPingOptions = new PingOptions();

                // Use the default Ttl value which is 128,
                // but change the fragmentation behavior.
                tPingOptions.DontFragment = true;

                // Create a buffer of 32 bytes of data to be transmitted.
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;
                PingReply tPingReply = tPint.Send(IP, timeout, buffer, tPingOptions);
                if (tPingReply.Status == IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                return false;
            }
        }
    }
}
