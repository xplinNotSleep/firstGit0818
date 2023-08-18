using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// 安全类（MD5加/解密）
    /// </summary>
    public class Security
    {
        private byte[] Keys ={ 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        private byte[] IVs = { 0x78, 0x10, 0x89, 0x23, 0x34, 0x45, 0xBC, 0xCD };

        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptStr">待加密的字符串</param>     
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns> 
        public string EncryptDES(string encryptStr)
        {
            if (encryptStr.Length == 0) return "";

            byte[] rgbKey = Keys;
            byte[] rgbIV = IVs;

            byte[] inputByteArray = Encoding.Default.GetBytes(encryptStr);

            DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();

            try
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(inputByteArray, 0, inputByteArray.Length);
                        cStream.FlushFinalBlock();

                        StringBuilder strEncrypt = new StringBuilder();
                        foreach (byte b in mStream.ToArray())
                        {
                            strEncrypt.AppendFormat("{0:X2}", b);
                        }

                        return strEncrypt.ToString();
                    }
                }
            }
            catch
            {
                return encryptStr;
            }

        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptStr">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥，要求为8位，和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返回源串</returns> 
        public string DecryptDES(string decryptStr)
        {
            if (decryptStr.Length == 0) return "";

            byte[] rgbKey = Keys;
            byte[] rgbIV = IVs;

            byte[] inputByteArray = new byte[decryptStr.Length / 2];

            for (int i = 0; i < decryptStr.Length / 2; i++)
            {
                inputByteArray[i] = (byte)(Convert.ToInt32(decryptStr.Substring(i * 2, 2), 16));
            }

            DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
            try
            {
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(inputByteArray, 0, inputByteArray.Length);
                        cStream.FlushFinalBlock();

                        return Encoding.Default.GetString(mStream.ToArray());
                    }
                }
            }
            catch
            {
                return decryptStr;
            }
        }

        /// <summary>
        /// DES加密文件
        /// </summary>
        /// <param name="inFilePath">待加密文件</param>
        /// <param name="outFilePath">加密后的文件</param> 
        /// <returns>如果加密成功则返回true,否则返回false</returns> 
        public bool EncryptDES(string inFilePath, string outFilePath)
        {
            bool isSuccess = false;

            byte[] rgbKey = Keys;
            byte[] rgbIV = IVs;

            //读入的流   
            using (FileStream inFs = new FileStream(inFilePath, FileMode.Open, FileAccess.Read))
            {
                //待写的流   
                using (FileStream outFs = new FileStream(outFilePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    outFs.SetLength(0);

                    //创建一个变量来帮助读写   
                    byte[] byteIn = new byte[100];   //临时存放读入的流   
                    long readLen = 0;                //读入流的长度   
                    long totalLen = inFs.Length;     //总共读入流的长度   
                    int everyLen;                    //每次读入流动长度 

                    //读入InFs，加密后写入OutFs   
                    DES des = new DESCryptoServiceProvider();

                    using (CryptoStream encStream = new CryptoStream(outFs, des.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write))
                    {

                        while (readLen < totalLen)
                        {
                            everyLen = inFs.Read(byteIn, 0, 100);
                            encStream.Write(byteIn, 0, everyLen);
                            readLen = readLen + everyLen;
                        }

                        isSuccess = true;
                    }
                }
            }

            return isSuccess;
        }

        ///   <summary>   
        ///   DES解密文件   
        ///   </summary>   
        ///   <param name="inFilePath">待解密文件</param>   
        ///   <param name="outFilePath">解密后的文件</param>
        ///   <returns>如果解密成功则返回true,否则返回false</returns> 
        public bool DecryptDES(string inFilePath, string outFilePath)
        {
            bool isSuccess = false;
            byte[] rgbKey = Keys;
            byte[] rgbIV = IVs;


            //读入的流   
            using (FileStream inFs = new FileStream(inFilePath, FileMode.Open, FileAccess.Read))
            {
                //待写的流   
                using (FileStream outFs = new FileStream(outFilePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    outFs.SetLength(0);

                    //创建一个变量来帮助读写   
                    byte[] byteIn = new byte[100];   //临时存放读入的流   
                    long readLen = 0;                //读入流的长度   
                    long totalLen = inFs.Length;     //总共读入流的长度   
                    int everyLen;                    //每次读入流动长度  

                    //读入InFs，解密后写入OutFs   
                    DES des = new DESCryptoServiceProvider();
                    using (CryptoStream encStream = new CryptoStream(outFs, des.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write))
                    {
                        while (readLen < totalLen)
                        {
                            everyLen = inFs.Read(byteIn, 0, 100);
                            encStream.Write(byteIn, 0, everyLen);
                            readLen = readLen + everyLen;
                        }

                        isSuccess = true;
                    }
                }
            }

            return isSuccess;
        }   


       public static string GetMd5(string ConvertString) //32位大写
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)));
            t2 = t2.Replace("-", "");
            return t2.ToLower();
        }
    }
}
