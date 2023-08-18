using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// Object字段属性项
    /// </summary>
    public class ObjectFieldItemProperty:ItemProperty
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ObjectFieldItemProperty()
        {
            this.m_dataNodeItem = EnumDataNodeItems.ObjectFieldItem;
            this.m_itemName = "OBJECTID";
            this.m_itemAliasName = "OBJECTID";
        }

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        [Category("常规"), ReadOnly(true), Description("名称"), DefaultValue("OBJECTID")]
        public override string ItemName
        {
            get
            {
                return base.ItemName;
            }
            set
            {
                base.ItemName = value;
            }
        }

        /// <summary>
        /// 获取或设置别名
        /// </summary>
        [Category("常规"),ReadOnly(false),Description("别名"),DefaultValue("OBJECTID")]
        public override string ItemAliasName
        {
            get
            {
                return base.ItemAliasName;
            }
            set
            {
                base.ItemAliasName = value;
            }
        }

        /// <summary>
        /// 获取字段类型
        /// </summary>
        [Category("通用"), Description("字段类型")]    
        public EnumFieldItems  FieldType
        {
            get
            {
                return EnumFieldItems.OID;
            }
        }
    }
}
