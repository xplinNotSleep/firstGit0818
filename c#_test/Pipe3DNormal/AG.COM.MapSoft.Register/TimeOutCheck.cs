using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AG.COM.MapSoft.Tool;
using System.Windows.Forms;

namespace AG.COM.MapSoft.Register
{
    public class TimeOutCheck
    {
        private string sDestPath = "C:\\Windows\\System32\\";
        private int iMaxCount = 100;
        /// <summary>
        /// 注册码写入到文件并加密
        /// </summary>
        public void WriteCount(string sDesFileName, string sRegisterNumber)
        {
            if (!Directory.Exists(sDestPath))
            {
                Directory.CreateDirectory(sDestPath);
            }
            string sDesFile = sDestPath + sDesFileName + ".txt";
            //FileStream fs = new System.IO.FileStream(sDesFile, FileMode.OpenOrCreate);
            StreamWriter w = new System.IO.StreamWriter(sDesFile, false);
            w.Write(sRegisterNumber);
            w.Close();
            DESCryDoc safe = new DESCryDoc();
            safe.EncryptFile(sDesFile);
        }


        /// <summary>
        /// 检查是否已过期
        /// </summary>
        /// <param name="sRegisterNum">用户从注册</param>
        /// <returns></returns>
        public bool isTimeOut()
        {
            string sFileName = "reg32";
            if (IsExistMiFile(sFileName))
            {
                string sUsedNumber = ReadMiWen(sFileName);
                if (string.IsNullOrEmpty(sUsedNumber))
                    return false;
                if (CheckMiWen(sUsedNumber))
                {
                    int iCount = Convert.ToInt32(sUsedNumber) + 1;
                    string sUsedNum=iCount.ToString("00000000");
                    WriteCount(sFileName, sUsedNum);
                    return true;
                }           
            }
            else
            {
                string sCount = "00000000";
                WriteCount(sFileName, sCount);
                return true;
            }
            return false;
        }

        public bool OnlyCheckTimeOut()
        {
            string sFileName = "reg32";
            if (!IsExistMiFile(sFileName)) return false;

            string sUsedNumber = ReadMiWen(sFileName);
            if (string.IsNullOrEmpty(sUsedNumber))
                return false;
            if (CheckMiWen(sUsedNumber))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否存在文件
        /// </summary>
        /// <returns></returns>
        private bool IsExistMiFile(string sFileName)
        {
            if (Directory.Exists(sDestPath))
            {
                string sSourceFile = sDestPath + sFileName + ".txt";
                if (File.Exists(sSourceFile))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 从密码文件里读取密文
        /// </summary>
        /// <param name="sMiFileName"></param>
        /// <returns></returns>
        public string ReadMiWen(string sMiFileName)
        {
            string sRegistedCode;
            string sSourceFile = sDestPath + sMiFileName + ".txt";
            string sDesFile = sDestPath + sMiFileName + "_tmp" + ".txt";
            DESCryDoc safe = new DESCryDoc();
            bool bIsRightLength = true;
            safe.DecryptFile(sSourceFile, sDesFile, ref bIsRightLength);
            if (!bIsRightLength) return null;
            FileStream fs = new System.IO.FileStream(sDesFile, FileMode.OpenOrCreate);
            StreamReader r = new System.IO.StreamReader(fs);
            sRegistedCode = r.ReadLine();
            r.Close();
            File.Delete(sDesFile);
            return sRegistedCode;
        }
        /// <summary>
        /// 密文值是否正确符合要求
        /// </summary>
        /// <returns></returns>
        public bool CheckMiWen(string sRegisterNumber)
        {
            int iUsedNum = Convert.ToInt32(sRegisterNumber);
            if (iUsedNum<=iMaxCount)
                return true;
            return false;
        }
    }
}
