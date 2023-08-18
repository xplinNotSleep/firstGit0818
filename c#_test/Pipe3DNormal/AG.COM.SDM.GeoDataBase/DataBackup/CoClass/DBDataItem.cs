using System.Collections.Generic;

namespace AG.COM.SDM.GeoDataBase.DataBackup
{
    /// <summary>
    /// 数据备份，数据项对象
    /// </summary>
    public class DBDataItem
    {
        private string m_Name = "";

        public DBDataItem()
        {
            Name = "";
            Type = DataType.DataSet;
            Childs = new List<DBDataItem>();
        }

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
                //去掉名称中的用户名部分，并赋值到NameWithoutDBUser
                int dotIndex = value.LastIndexOf(".");
                if (!string.IsNullOrEmpty(value) && dotIndex >= 0)
                {
                    NameWithoutDBUser = value.Substring(dotIndex + 1, value.Length - dotIndex - 1);
                }
                else
                {
                    NameWithoutDBUser = value;
                }
            }
        }
        
        /// <summary>
        /// 去点数据库用户名的名称（如Name为AGSDM.TABLE，则NameWithoutDBUser为TABLE）
        /// </summary>
        public string NameWithoutDBUser { get; set; }
        
        /// <summary>
        /// 获取或设置数据类型
        /// </summary>
        public DataType Type { get; set; }
        /// <summary>
        /// 获取或设置子集合
        /// </summary>
        public List<DBDataItem> Childs { get; set; } 
    }

    /// <summary>
    /// 数据备份，数据项类型
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// 数据集
        /// </summary>
        DataSet,
        /// <summary>
        /// 要素类
        /// </summary>
        FeatureClass
    }
}
