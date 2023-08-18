using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// 数据库项属性类
    /// </summary>
    public class DataBaseItemProperty:ItemProperty
    {     
        private string m_dbVersion;
        private string m_dbOwner;
        private bool isDefault = false;
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DataBaseItemProperty()
        {
            this.m_itemName = "NewDataBase";
            this.m_itemAliasName = "新建数据库";
            this.m_dataNodeItem = EnumDataNodeItems.DataBaseItem;
            this.m_dbVersion = "1.0.0";
            this.m_dbOwner = "Augruit_Echo";
        }

        /// <summary>
        /// 获取或设置数据库版本
        /// </summary>
        [Category("版本号"),Description("数据库版本"),DefaultValue("1.0.0")]
        public string DBVersion
        {
            get
            {
                return this.m_dbVersion;
            }
            set
            {
                this.m_dbVersion = value;
            }
        }

        /// <summary>
        /// 获取或设置数据库所有者
        /// </summary>
        [Description("数据库所有者"),DefaultValue("Echo")]
        public string DBOwner
        {
            get
            {
                return this.m_dbOwner;
            }
            set
            {
                this.m_dbOwner = value;
            }
        }
        /// <summary>
        /// 是否默认的数据库
        /// </summary>
        [Browsable(false)]
        public bool IsDefault
        {
            get => isDefault;
            set => isDefault = value;
        }
    }
}
