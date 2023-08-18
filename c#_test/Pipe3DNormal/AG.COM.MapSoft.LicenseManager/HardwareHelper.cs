/************************************************************************************
 * Copyright (c) 2016  All Rights Reserved.
 *命名空间：AG.COM.MapSoft.LicenseManager
 *文件名：  HardwareHelper
 *创建人：  胡仁勇
 *创建时间：2016-11-21 9:55:07
 *描述
 *=====================================================================
 *修改标记
 *修改时间：2016-11-21 9:55:07
 *修改人： 胡仁勇
 *描述：
************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
namespace AG.COM.MapSoft.LicenseManager
{
    public class HardwareHelper
    {
        /// <summary>
        /// 取得设备硬盘的卷标号
        /// </summary>
        /// <returns></returns>
        public static string GetDiskVolumeSerialNumber()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }
    }
}
