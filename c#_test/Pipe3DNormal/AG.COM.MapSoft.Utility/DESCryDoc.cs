using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace AG.COM.MapSoft.Tool
{
    /// <summary>
    /// DES文件加解密
    /// </summary>
    public class DESCryDoc
    {
        /// <summary>    
        /// myiv is  iv    
        /// </summary>    
        string myiv = "Hyey20140830";
        /// <summary>    
        /// mykey is key    
        /// </summary>    
        string mykey = "HyeyWl30";

        /// <summary>    
        /// DES加密偏移量    
        /// 必须是>=8位长的字符串    
        /// </summary>    
        public string IV
        {
            get { return myiv; }
            set { myiv = value; }
        }

        /// <summary>    
        /// DES加密的私钥    
        /// 必须是8位长的字符串    
        /// </summary>    
        public string Key
        {
            get { return mykey; }
            set { mykey = value; }
        }

        public DESCryDoc()
        {
            //    
            // TODO: 在此处添加构造函数逻辑    
            //    
        }
        /// <summary>    
        /// 对字符串进行DES加密    
        /// Encrypts the specified sourcestring.    
        /// </summary>    
        /// <param name="sourcestring">The sourcestring.待加密的字符串</param>    
        /// <returns>加密后的BASE64编码的字符串</returns>    
        public string Encrypt(string sourceString)
        {
            byte[] btKey = Encoding.Default.GetBytes(mykey);
            byte[] btIV = Encoding.Default.GetBytes(myiv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Encoding.Default.GetBytes(sourceString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>    
        /// Decrypts the specified encrypted string.    
        /// 对DES加密后的字符串进行解密    
        /// </summary>    
        /// <param name="encryptedString">The encrypted string.待解密的字符串</param>    
        /// <returns>解密后的字符串</returns>    
        public string Decrypt(string encryptedString)
        {
            byte[] btKey = Encoding.Default.GetBytes(mykey);
            byte[] btIV = Encoding.Default.GetBytes(myiv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Convert.FromBase64String(encryptedString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }
                    return Encoding.Default.GetString(ms.ToArray());
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>    
        /// Encrypts the file.    
        /// 对文件内容进行DES加密    
        /// </summary>    
        /// <param name="sourceFile">The source file.待加密的文件绝对路径</param>    
        /// <param name="destFile">The dest file.加密后的文件保存的绝对路径</param>    
        public void EncryptFile(string sourceFile, string destFile)
        {
            if (!File.Exists(sourceFile)) throw new FileNotFoundException("指定的文件路径不存在！", sourceFile);

            byte[] btKey = Encoding.Default.GetBytes(mykey);
            byte[] btIV = Encoding.Default.GetBytes(myiv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] btFile = File.ReadAllBytes(sourceFile);

            using (FileStream fs = new FileStream(destFile, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    using (CryptoStream cs = new CryptoStream(fs, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(btFile, 0, btFile.Length);
                        cs.FlushFinalBlock();
                    }
                }
                catch
                {
                    throw;
                }

                finally
                {
                    fs.Close();
                }
            }
        }

        /// <summary>    
        /// Encrypts the file.    
        /// 对文件内容进行DES加密，加密后覆盖掉原来的文件    
        /// </summary>    
        /// <param name="sourceFile">The source file.待加密的文件的绝对路径</param>    
        public void EncryptFile(string sourceFile)
        {
            EncryptFile(sourceFile, sourceFile);
        }

        /// <summary>    
        /// Decrypts the file.    
        /// 对文件内容进行DES解密    
        /// </summary>    
        /// <param name="sourceFile">The source file.待解密的文件绝对路径</param>    
        /// <param name="destFile">The dest file.解密后的文件保存的绝对路径</param>    
        public void DecryptFile(string sourceFile, string destFile, ref bool bIsRight)
        {
            if (!File.Exists(sourceFile)) throw new FileNotFoundException("指定的文件路径不存在！", sourceFile);
            byte[] btKey = Encoding.Default.GetBytes(mykey);
            byte[] btIV = Encoding.Default.GetBytes(myiv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] btFile = File.ReadAllBytes(sourceFile);
            using (FileStream fs = new FileStream(destFile, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    using (CryptoStream cs = new CryptoStream(fs, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(btFile, 0, btFile.Length);
                        cs.FlushFinalBlock();
                    }
                }
                catch
                {
                    MessageHandler.ShowInfoMsg("注册码不正确，请重新注册!!", "提示");
                    bIsRight = false;
                    return;
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
        }

        /// <summary>    
        /// Decrypts the file.    
        /// 对文件内容进行DES解密，加密后覆盖掉原来的文件.    
        /// </summary>    
        /// <param name="sourceFile">The source file.待解密的文件的绝对路径.</param>    
        public void DecryptFile(string sourceFile, ref bool bIsRight)
        {
            DecryptFile(sourceFile, sourceFile, ref bIsRight);
        }

    }
}
