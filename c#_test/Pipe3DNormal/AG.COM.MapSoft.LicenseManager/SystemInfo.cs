using System;
using System.Collections.Generic;

namespace AG.COM.MapSoft.LicenseManager
{
    /// <summary>
    /// 系统信息类
    /// </summary>
    public class SystemInfo
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public static string UserName = string.Empty;

        /// <summary>
        /// 用户编号
        /// </summary>
        public static decimal UserID = -1;

        /// <summary>
        /// 产品名称
        /// </summary>
        public static string ProductName = string.Empty;

        private static bool m_IsAdminUser = false;

        /// <summary>
        /// 是否管理员账户
        /// </summary>
        public static bool IsAdminUser
        {
            get { return m_IsAdminUser; }
        }

        private static string m_RoleName;

        /// <summary>
        /// 角色名称
        /// </summary>
        public static string RoleName
        {
            get { return m_RoleName; }
            set 
            {
                m_RoleName = value;
                if (m_RoleName.Equals("管理员"))
                {
                    m_IsAdminUser = true;
                }
            }
        }

        /// <summary>
        /// 岗位编号
        /// </summary>
        public static decimal PositionID = -1;

        /// <summary>
        /// 岗位名称
        /// </summary>
        public static string PositionName = string.Empty;

        /// <summary>
        /// 地图工程编号
        /// </summary>
        public static decimal ProjectID = -1;

        /// <summary>
        /// 部门ID
        /// </summary>
        public static List<string> OrgIDLst = new List<string>();

        /// <summary>
        /// 当前加载的地图文档ID
        /// </summary>
        public static decimal MapDocID = -1;

        /// <summary>
        /// 初始加载地图方式
        /// </summary>
        public static LoadMapType InitLoadMapType = LoadMapType.MapProject;

        /// <summary>
        /// 许可级别名称
        /// </summary>
        public static string LicenseLevelName = "";

        /// <summary>
        /// 无限制许可
        /// </summary>
        public static bool LicenseUnlimited = false;

        /// <summary>
        /// 有权限的插件类名称
        /// </summary>
        public static List<string> HasLicPlugins = new List<string>();

        /// <summary>
        /// 有权限的插件类信息
        /// </summary>
        public static List<LicensePluginTag> HasLicPluginTags = new List<LicensePluginTag>();

        /// <summary>
        /// 许可使用期限
        /// </summary>
        public static DateTime LimitDate;

        /// <summary>
        /// 上次使用日期
        /// </summary>
        public static DateTime LastDate;
    }

    /// <summary>
    /// 角色类型
    /// </summary>
    public enum RoleType
    {
        管理员,
        普通用户
    }

    /// <summary>
    /// 加载地图方式
    /// </summary>
    public enum LoadMapType
    {
        /// <summary>
        /// 地图工程
        /// </summary>
        MapProject,

        /// <summary>
        /// 地图文档
        /// </summary>
        MapDocument
    }
}
