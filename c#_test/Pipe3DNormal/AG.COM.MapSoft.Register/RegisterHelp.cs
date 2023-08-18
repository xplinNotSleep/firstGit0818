using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AG.COM.MapSoft.Tool;
using System.Windows.Forms;

namespace AG.COM.MapSoft.Register
{
    public class RegisterHelp
    {
        /// <summary>
        /// 是否存在注册码文件
        /// </summary>
        /// <returns></returns>
        private bool IsExistMiFile(string sFileName)
        {
            if (Directory.Exists(CommonConstString.STR_ConfigPath))
            {
                string sSourceFile = CommonConstString.STR_ConfigPath + sFileName + ".txt";
                if (File.Exists(sSourceFile))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 核对密文是否正确
        /// </summary>
        /// <returns></returns>
        public bool CheckMiWen(string sRegisterNumber)
        {
            string sCode = MachineCode.getRNum();
            if (sRegisterNumber.Trim() == sCode.Trim())
                return true;
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
            string sSourceFile = CommonConstString.STR_ConfigPath + sMiFileName + ".txt";
            string sDesFile = CommonConstString.STR_ConfigPath + sMiFileName + "_tmp" + ".txt";
            DESCryDoc safe = new DESCryDoc();
            bool bIsRightLength = true;
            safe.DecryptFile(sSourceFile,sDesFile,ref bIsRightLength);
            if (!bIsRightLength) return null;
            FileStream fs = new System.IO.FileStream(sDesFile, FileMode.OpenOrCreate);
            StreamReader r=new System.IO.StreamReader(fs);               
            sRegistedCode=r.ReadLine();
            r.Close();
            File.Delete(sDesFile);
            return sRegistedCode;
        }

        /// <summary>
        /// 注册码写入到文件并加密
        /// </summary>
        public void WriteMiWen(string sDesFileName,string sRegisterNumber)
        {
            if (!Directory.Exists(CommonConstString.STR_ConfigPath))
            {
                Directory.CreateDirectory(CommonConstString.STR_ConfigPath);
            }
            string sDesFile = CommonConstString.STR_ConfigPath + sDesFileName + ".txt";
            //FileStream fs = new System.IO.FileStream(sDesFile, FileMode.OpenOrCreate);
            StreamWriter w = new System.IO.StreamWriter(sDesFile,false);
            w.Write(sRegisterNumber);
            w.Close();
            DESCryDoc safe = new DESCryDoc();
            safe.EncryptFile(sDesFile);           
        }

        /// <summary>
        /// 核对是否已经注册
        /// </summary>
        /// <param name="sRegisterNum">用户从注册</param>
        /// <returns></returns>
        public bool isRegisted()
        {
            string sFileName = "register";
            if (IsExistMiFile(sFileName))
            {
                string sRegNumber=ReadMiWen(sFileName);
                if (string.IsNullOrEmpty(sRegNumber))
                    return false;
                if (CheckMiWen(sRegNumber))
                {
                    return true;
                }
                else
                {
                    MessageHandler.ShowInfoMsg("注册码不正确，请重新注册！","提示");
                    return false;
                }
            }
            //else
            //{
            //    FormRegister frm = new FormRegister();
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        return true;
            //    }
            //}
            return false;
        }
    }
}
