using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// ��ȫ�ࣨMD5��/���ܣ�
    /// </summary>
    public class Security
    {
        private byte[] Keys ={ 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        private byte[] IVs = { 0x78, 0x10, 0x89, 0x23, 0x34, 0x45, 0xBC, 0xCD };

        /// <summary>
        /// DES�����ַ���
        /// </summary>
        /// <param name="encryptStr">�����ܵ��ַ���</param>     
        /// <returns>���ܳɹ����ؼ��ܺ���ַ�����ʧ�ܷ���Դ��</returns> 
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
        /// DES�����ַ���
        /// </summary>
        /// <param name="decryptStr">�����ܵ��ַ���</param>
        /// <param name="decryptKey">������Կ��Ҫ��Ϊ8λ���ͼ�����Կ��ͬ</param>
        /// <returns>���ܳɹ����ؽ��ܺ���ַ�����ʧ�ܷ���Դ��</returns> 
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
        /// DES�����ļ�
        /// </summary>
        /// <param name="inFilePath">�������ļ�</param>
        /// <param name="outFilePath">���ܺ���ļ�</param> 
        /// <returns>������ܳɹ��򷵻�true,���򷵻�false</returns> 
        public bool EncryptDES(string inFilePath, string outFilePath)
        {
            bool isSuccess = false;

            byte[] rgbKey = Keys;
            byte[] rgbIV = IVs;

            //�������   
            using (FileStream inFs = new FileStream(inFilePath, FileMode.Open, FileAccess.Read))
            {
                //��д����   
                using (FileStream outFs = new FileStream(outFilePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    outFs.SetLength(0);

                    //����һ��������������д   
                    byte[] byteIn = new byte[100];   //��ʱ��Ŷ������   
                    long readLen = 0;                //�������ĳ���   
                    long totalLen = inFs.Length;     //�ܹ��������ĳ���   
                    int everyLen;                    //ÿ�ζ����������� 

                    //����InFs�����ܺ�д��OutFs   
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
        ///   DES�����ļ�   
        ///   </summary>   
        ///   <param name="inFilePath">�������ļ�</param>   
        ///   <param name="outFilePath">���ܺ���ļ�</param>
        ///   <returns>������ܳɹ��򷵻�true,���򷵻�false</returns> 
        public bool DecryptDES(string inFilePath, string outFilePath)
        {
            bool isSuccess = false;
            byte[] rgbKey = Keys;
            byte[] rgbIV = IVs;


            //�������   
            using (FileStream inFs = new FileStream(inFilePath, FileMode.Open, FileAccess.Read))
            {
                //��д����   
                using (FileStream outFs = new FileStream(outFilePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    outFs.SetLength(0);

                    //����һ��������������д   
                    byte[] byteIn = new byte[100];   //��ʱ��Ŷ������   
                    long readLen = 0;                //�������ĳ���   
                    long totalLen = inFs.Length;     //�ܹ��������ĳ���   
                    int everyLen;                    //ÿ�ζ�����������  

                    //����InFs�����ܺ�д��OutFs   
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


       public static string GetMd5(string ConvertString) //32λ��д
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)));
            t2 = t2.Replace("-", "");
            return t2.ToLower();
        }
    }
}
