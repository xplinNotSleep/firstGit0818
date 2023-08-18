using System;
using System.Collections.Generic;

namespace AG.COM.MapSoft.LicenseManager
{
    /// <summary>
    /// ϵͳ��Ϣ��
    /// </summary>
    public class SystemInfo
    {
        /// <summary>
        /// �û���
        /// </summary>
        public static string UserName = string.Empty;

        /// <summary>
        /// �û����
        /// </summary>
        public static decimal UserID = -1;

        /// <summary>
        /// ��Ʒ����
        /// </summary>
        public static string ProductName = string.Empty;

        private static bool m_IsAdminUser = false;

        /// <summary>
        /// �Ƿ����Ա�˻�
        /// </summary>
        public static bool IsAdminUser
        {
            get { return m_IsAdminUser; }
        }

        private static string m_RoleName;

        /// <summary>
        /// ��ɫ����
        /// </summary>
        public static string RoleName
        {
            get { return m_RoleName; }
            set 
            {
                m_RoleName = value;
                if (m_RoleName.Equals("����Ա"))
                {
                    m_IsAdminUser = true;
                }
            }
        }

        /// <summary>
        /// ��λ���
        /// </summary>
        public static decimal PositionID = -1;

        /// <summary>
        /// ��λ����
        /// </summary>
        public static string PositionName = string.Empty;

        /// <summary>
        /// ��ͼ���̱��
        /// </summary>
        public static decimal ProjectID = -1;

        /// <summary>
        /// ����ID
        /// </summary>
        public static List<string> OrgIDLst = new List<string>();

        /// <summary>
        /// ��ǰ���صĵ�ͼ�ĵ�ID
        /// </summary>
        public static decimal MapDocID = -1;

        /// <summary>
        /// ��ʼ���ص�ͼ��ʽ
        /// </summary>
        public static LoadMapType InitLoadMapType = LoadMapType.MapProject;

        /// <summary>
        /// ��ɼ�������
        /// </summary>
        public static string LicenseLevelName = "";

        /// <summary>
        /// ���������
        /// </summary>
        public static bool LicenseUnlimited = false;

        /// <summary>
        /// ��Ȩ�޵Ĳ��������
        /// </summary>
        public static List<string> HasLicPlugins = new List<string>();

        /// <summary>
        /// ��Ȩ�޵Ĳ������Ϣ
        /// </summary>
        public static List<LicensePluginTag> HasLicPluginTags = new List<LicensePluginTag>();

        /// <summary>
        /// ���ʹ������
        /// </summary>
        public static DateTime LimitDate;

        /// <summary>
        /// �ϴ�ʹ������
        /// </summary>
        public static DateTime LastDate;
    }

    /// <summary>
    /// ��ɫ����
    /// </summary>
    public enum RoleType
    {
        ����Ա,
        ��ͨ�û�
    }

    /// <summary>
    /// ���ص�ͼ��ʽ
    /// </summary>
    public enum LoadMapType
    {
        /// <summary>
        /// ��ͼ����
        /// </summary>
        MapProject,

        /// <summary>
        /// ��ͼ�ĵ�
        /// </summary>
        MapDocument
    }
}
