/************************************************************************************
 * Copyright (c) 2016  All Rights Reserved.
 *命名空间：AG.COM.MapSoft.Register
 *文件名：  MachineCode
 *创建人：  胡仁勇
 *创建时间：2016-11-21 10:28:56
 *描述
 *=====================================================================
 *修改标记
 *修改时间：2016-11-21 10:28:56
 *修改人： 胡仁勇
 *描述：
************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
namespace AG.COM.MapSoft.Register
{
    public class MachineCode
    {
        //获得CPU的序列号
        public static string getCpu()
        {
            string strCpu = null;
            ManagementClass myCpu = new ManagementClass("win32_Processor");
            ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
            foreach (ManagementObject myObject in myCpuConnection)
            {
                strCpu = myObject.Properties["Processorid"].Value.ToString();
                break;
            }
            return strCpu;
        }
        // 取得设备硬盘的卷标号
        public static string GetDiskVolumeSerialNumber()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }

        public static int[] intCode = new int[127];//存储密钥
        //public static int[] intNumber = new int[25];//存机器码的Ascii值
        //public static char[] Charcode = new char[25];//存储机器码字
        public static int[] intNumber = new int[9];
        public static char[] Charcode = new char[9];
        public static void setIntCode()//给数组赋值小于10的数
        {
            for (int i = 1; i < intCode.Length; i++)
            {
                intCode[i] = i % 9;
            }
        }
        //生成机器码
        public static string getMNum()
        {
            //string strNum = getCpu() + GetDiskVolumeSerialNumber();//获得24位Cpu和硬盘序列号
            string strNum = GetDiskVolumeSerialNumber();//硬盘序列号
            string strMNum = strNum.Substring(0, 8);//从生成的字符串中取出前24个字符做为机器码
            return strMNum;
        }
        //生成注册码    
        public static string getRNum()
        {
            setIntCode();//初始化127位数组
            for (int i = 1; i < Charcode.Length; i++)//把机器码存入数组中
            {
                // Charcode[i] = Convert.ToChar(getMNum().Substring(i - 1, 1));
                Charcode[i] = Convert.ToChar(GetDiskVolumeSerialNumber().Substring(i - 1, 1));
            }
            for (int j = 1; j < intNumber.Length; j++)//把字符的ASCII值存入一个整数组中。
            {
                intNumber[j] = intCode[Convert.ToInt32(Charcode[j])] + Convert.ToInt32(Charcode[j]);
            }
            string strAsciiName = "";//用于存储注册码
            for (int j = 1; j < intNumber.Length; j++)
            {
                if (intNumber[j] >= 48 && intNumber[j] <= 57)//判断字符ASCII值是否0－9之间
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else if (intNumber[j] >= 65 && intNumber[j] <= 90)//判断字符ASCII值是否A－Z之间
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else if (intNumber[j] >= 97 && intNumber[j] <= 122)//判断字符ASCII值是否a－z之间
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else//判断字符ASCII值不在以上范围内
                {
                    if (intNumber[j] > 122)//判断字符ASCII值是否大于z
                    {
                        strAsciiName += Convert.ToChar(intNumber[j] - 10).ToString();
                    }
                    else
                    {
                        strAsciiName += Convert.ToChar(intNumber[j] - 9).ToString();
                    }
                }
            }
            return strAsciiName;
        }
    }
}
