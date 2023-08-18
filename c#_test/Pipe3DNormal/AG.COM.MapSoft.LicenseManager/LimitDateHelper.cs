using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using AG.COM.MapSoft.Tool;
using System.Net;

namespace AG.COM.MapSoft.LicenseManager
{
    public class LimitDateHelper
    {
        /// <summary>
        /// 是否有时间限制的key
        /// </summary>
        public static readonly string IsLimitKey = "LimitDateIsLimit";

        /// <summary>
        /// 上次使用时间的key
        /// </summary>
        public static readonly string LastTimeKey = "LimitDateLastTime";

        /// <summary>
        /// 有效日期的key
        /// </summary>
        public static readonly string LimitTimeKey = "LimitDateLimitTime";

        /// <summary>
        /// 许可级别的key
        /// </summary>
        public static readonly string LicenseLevelNameKey = "LicenseLevelName";

        /// <summary>
        /// 系统许可信息Guid的key
        /// </summary>
        public static readonly string SystemLicGuidKey = "SystemLicGuid";

        /// <summary>
        /// 无限制许可的key
        /// </summary>
        public static readonly string UnlimitedKey = "Unlimited";

        /// <summary>
        /// 机器码的key
        /// </summary>
        public static readonly string MachineCodeKey = "MachineCode";

        /// <summary>
        /// 可用插件类的key
        /// </summary>
        public static readonly string PluginClassesKey = "PluginClasses";

        /// <summary>
        /// 可用插件类中文名的key
        /// </summary>
        public static readonly string PluginCnNameKey = "PluginCnName";

        /// <summary>
        /// 注册表中系统名称（不同系统应该不同，否则会冲突，导致其他系统许可无效）
        /// 不同软件的注册名称
        /// </summary>
        public static string SoftwareName = "AGS_PIPE3D";

        /// <summary>
        /// 注册表中系统名称（不同系统应该不同，否则会冲突，导致其他系统许可无效）
        /// 不同软件的注册名称
        /// </summary>
        public static string DefaultSoftwareName = "AGSDM";

        /// <summary>
        /// 注册表中使用期限项
        /// </summary>
        public static readonly string LimitDateRegItem = "SYSPATH";

        /// <summary>
        /// 注册表中上次使用日期
        /// </summary>
        public static readonly string LastDateRegItem = "SYSCONFIG";

        /// <summary>
        /// 注册表中许可GUID
        /// </summary>
        public static readonly string LicenseLevelRegItem = "SYSGROUP";

        /// <summary>
        /// 系统当前许可信息保存文件路径
        /// </summary>
        private static readonly string SystemUseConfigFilePath = CommonConstString.STR_ConfigPath + "SystemUseConfig.resx";

        /// <summary>
        /// 许可申请协议书模板
        /// </summary>
        public static readonly string ApplyTemplatePath = CommonConstString.STR_ConfigPath + "软件授权使用协议书模板.doc";

        /// <summary>
        /// 许可申请界面协议内容
        /// </summary>
        public static readonly string ApplyFormContentPath = CommonConstString.STR_ConfigPath + "软件授权使用协议书注册界面内容.rtf";

        /// <summary>
        /// 判断软件是否可用
        /// </summary>
        /// <returns>如果软件没到过期时间则返回true,否则返回false</returns>
        public static bool IsSoftwareValid()
        {
            //RegistryKey lm = Registry.LocalMachine;
            RegistryKey lm = Registry.CurrentUser;

            bool bResult = false;
            #region
            //RegistryKey s = lm.OpenSubKey("SOFTWARE", false);
            //string[] subkeys = s.GetSubKeyNames();
            ////s = lm.OpenSubKey("SOFTWARE", false);

            //if (s != null)
            //{
            //    s = s.OpenSubKey(SoftwareName, false);
            //    if (s != null)
            //    {
            //        //检查并读取许可信息
            //        bResult = CheckLicenseLevelFromObj(s);

            //    }
            //}
            #endregion
            RegistryKey s = lm.OpenSubKey($"SOFTWARE\\{SoftwareName}");
            if (s != null)
            {
                //检查并读取许可信息
                bResult = CheckLicenseLevelFromObj(s);
            }

            lm.Close();

            if (bResult == false && s != null)
                MessageBox.Show("未能成功读取许可信息，请重新注册许可！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (bResult == false && s == null)
                MessageBox.Show("注册表中未找到申请许可的名称，请重新申请许可!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return bResult;
        }

        /// <summary>
        /// 注册许可
        /// </summary>
        /// <param name="licPath"></param>
        public static bool RegisterLic(string licPath)
        {
            if (string.IsNullOrEmpty(licPath)) return false;
            if (File.Exists(licPath) == false) return false;

            ResourceHelper tResourceHelper = new ResourceHelper(licPath);//解析lic文件
            //Security security = new Security();

            //RegistryKey lm = Registry.LocalMachine;
            RegistryKey lm = Registry.CurrentUser;
            RegistryKey s;

            s = lm.OpenSubKey("SOFTWARE", true);
            string[] subkeys = s.GetSubKeyNames();
            ///获得注册的软件名称
            SoftwareName = tResourceHelper.GetString("LICENSELEVELNAME");
            ///首先判断有没有注册节点，如果有首先删除
            try
            {
                s.DeleteSubKey(SoftwareName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //再次创建注册节点
            s = s.CreateSubKey(SoftwareName);

            SystemInfo.LicenseLevelName = SoftwareName;

            ////用guid把保存在系统的许可信息和本机（注册表）绑定起来
            //string guid = Guid.NewGuid().ToString();

            //获取许可文件的机器码，为采用MD5加密后的密码
            string machineCode = tResourceHelper.GetString(MachineCodeKey);
            if (string.IsNullOrEmpty(machineCode))
            {
                MessageBox.Show("许可文件获取获取机器码失败！请申请正确的许可文件！");
                return false;
            }

            //采用MD5加密法，对本机的机器码进行加密，再判断和许可文件上的判断一不一样
            string machineCodeThis = HardwareHelper.GetDiskVolumeSerialNumber();
            string machineCodeThisEncry = MD5Encrypt64(machineCodeThis);
            if (machineCode != machineCodeThisEncry)
            {
                MessageBox.Show("许可文件机器码与本机机器码不匹配！");
                return false;
            }

            machineCodeThis = Encryption(machineCodeThis);

            s.SetValue(LicenseLevelRegItem, machineCodeThis, RegistryValueKind.String);

            //写入是否无限限制
            string strUnlimited = tResourceHelper.GetString(LimitDateHelper.UnlimitedKey);
            s.SetValue(UnlimitedKey, strUnlimited, RegistryValueKind.String);

            //写入过期日期
            string strTime = tResourceHelper.GetString(LimitDateHelper.LimitTimeKey);
            SystemInfo.LimitDate = Convert.ToDateTime(strTime);
            strTime = Encryption(strTime);
            s.SetValue(LimitTimeKey, strTime, RegistryValueKind.String);

            lm.Close();

            return true;
        }

        /// <summary>
        /// 获取网络日期时间
        /// </summary>
        /// <returns></returns>
        public static string GetNetDateTime()
        {
            WebRequest request = null;
            WebResponse response = null;
            WebHeaderCollection headerCollection = null;
            string datetime = string.Empty;
            try
            {
                request = WebRequest.Create("https://www.baidu.com");
                request.Timeout = 3000;
                request.Credentials = CredentialCache.DefaultCredentials;
                response = (WebResponse)request.GetResponse();
                headerCollection = response.Headers;
                foreach (var h in headerCollection.AllKeys)
                {
                    if (h == "Date")
                    {
                        datetime = headerCollection[h];
                    }
                }
                if (datetime != string.Empty)
                    datetime = Convert.ToDateTime(datetime).ToString("yyyy-MM-dd HH:mm:ss");
                return datetime;
            }
            catch (Exception) { return datetime; }
            finally
            {
                if (request != null)
                { request.Abort(); }
                if (response != null)
                { response.Close(); }
                if (headerCollection != null)
                { headerCollection.Clear(); }
            }
        }

        /// <summary>
        /// 检查并读取许可信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>false=未能成功读取许可</returns>
        public static bool CheckLicenseLevelFromObj(RegistryKey key)
        {
            if (key == null) return false;

            //注册表记录的MachineCode
            //获取当前机器的机器码
            string machineCodeThis = HardwareHelper.GetDiskVolumeSerialNumber();
            if (string.IsNullOrEmpty(machineCodeThis))
            {
                MessageBox.Show("无法获取机器码，请联系系统管理员");
                return false;
            }

            if (key.GetValue(LicenseLevelRegItem) == null) return false;
            string regeditMachineCode = key.GetValue(LicenseLevelRegItem).ToString();
            if (string.IsNullOrEmpty(regeditMachineCode)) return false;
            regeditMachineCode = Decrypt(regeditMachineCode);

            //判断机器码
            if (regeditMachineCode != machineCodeThis) return false;

            //判断日期
            if (key.GetValue(UnlimitedKey) == null) return false;
            string strUnlimited = key.GetValue(UnlimitedKey).ToString();

            if (string.Compare(strUnlimited, "true", true) != 0)
            {
                string strTimeNow = GetNetDateTime();
                DateTime timeNow;
                if (string.IsNullOrEmpty(strTimeNow))
                    timeNow = DateTime.Now;
                else
                    timeNow = Convert.ToDateTime(strTimeNow);

                string strLimitedTime = key.GetValue(LimitTimeKey).ToString();
                strLimitedTime = Decrypt(strLimitedTime);

                DateTime timeLimited = Convert.ToDateTime(strLimitedTime);
                TimeSpan span = (TimeSpan)(timeLimited - timeNow);

                if (span.Days <= 0)
                {
                    MessageBox.Show("许可已过期！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;

        }


        public static void GetLicenseTimeFromKey(string softwareName)
        {
            //先读取注册表查找已注册许可的软件名称
            RegistryKey lm = Registry.CurrentUser;
            RegistryKey s1 = lm.OpenSubKey("SOFTWARE", false);
            string[] subkeys = s1.GetSubKeyNames();
            if (s1 != null)
            {
                RegistryKey s2 = s1.OpenSubKey(softwareName, false);
                if (s2 != null)
                {
                    //判断日期
                    if (s2.GetValue(UnlimitedKey) == null) return ;
                    string strUnlimited = s2.GetValue(UnlimitedKey).ToString();

                    if (string.Compare(strUnlimited, "true", true) != 0)
                    {
                        string strTimeNow = GetNetDateTime();
                        DateTime timeNow;
                        if (string.IsNullOrEmpty(strTimeNow))
                            timeNow = DateTime.Now;
                        else
                            timeNow = Convert.ToDateTime(strTimeNow);

                        string strLimitedTime = s2.GetValue(LimitTimeKey).ToString();
                        strLimitedTime = Decrypt(strLimitedTime);

                        DateTime timeLimited = Convert.ToDateTime(strLimitedTime);
                        //DateTime dateLimited = timeLimited.Date;
                        TimeSpan span = (TimeSpan)(timeLimited - timeNow);

                        if (span.Days <= 0)
                        {
                            MessageBox.Show($"许可已过期,到期时间为{timeLimited}！");
                        }
                        else
                        {
                            MessageBox.Show($"许可还差{span.Days}天到期,到期时间为{timeLimited}！");
                        }
                    }
                }
            }

        }

        //加密
        public static string Encryption(string express)
        {
            CspParameters param = new CspParameters();
            param.KeyContainerName = "oa_erp_dowork";//密匙容器的名称，保持加密解密一致才能解密成功
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                try
                {
                    byte[] plaindata = Encoding.Default.GetBytes(express);//将要加密的字符串转换为字节数组
                    byte[] encryptdata = rsa.Encrypt(plaindata, false);//将加密后的字节数据转换为新的加密字节数组
                    return Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为字符串
                }
                catch (Exception ex)
                {
                    AG.COM.MapSoft.Tool.MapSoftLog.LogError("字符串加密出错！", ex);
                    return "";
                }

            }
        }

        //解密
        public static string Decrypt(string ciphertext)
        {
            CspParameters param = new CspParameters();
            param.KeyContainerName = "oa_erp_dowork";
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                try
                {
                    byte[] encryptdata = Convert.FromBase64String(ciphertext);
                    byte[] decryptdata = rsa.Decrypt(encryptdata, false);
                    return Encoding.Default.GetString(decryptdata);
                }
                catch (Exception ex)
                {
                    AG.COM.MapSoft.Tool.MapSoftLog.LogError("字符串解密出错！", ex);
                    return "";
                }

            }
        }

        /// <summary>
        ///MD5 64位加密算法
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt64(string password)
        {
            string cl = password;
            //string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            return Convert.ToBase64String(s);
        }

    }

    /// <summary>
    /// 许可插件
    /// </summary>
    [Serializable]
    public class LicensePluginTag
    {
        private string m_ClassName = "";
        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName
        {
            get { return m_ClassName; }
            set { m_ClassName = value; }
        }

        private string m_CnName = "";
        /// <summary>
        /// 插件中文名
        /// </summary>
        public string CnName
        {
            get { return m_CnName; }
            set { m_CnName = value; }
        }

        private string m_Group = "";
        /// <summary>
        /// 插件分组
        /// </summary>
        public string Group
        {
            get { return m_Group; }
            set { m_Group = value; }
        }
    }
}
