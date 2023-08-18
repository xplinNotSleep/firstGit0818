using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// 专题属性域库属性项
    /// </summary>
    public class SubDomainItemProperty:ItemProperty
    {
        /// <summary>
        /// 实例化新对象
        /// </summary>
        public SubDomainItemProperty()
        {
            this.m_itemName = "SubDomains";
            this.m_itemAliasName = "专题属性域";
            this.m_dataNodeItem = EnumDataNodeItems.SubjectDomainItem;
        }

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        [Category("常规"),ReadOnly(true), Description("名称")]
        public override string ItemName
        {
            get
            {
                return this.m_itemName;
            }
            set
            {
                if (value.ToString().Length > 0)
                {
                    this.m_itemName = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置别名
        /// </summary>
        [Category("常规"), ReadOnly(true), Description("别名")]
        public override  string ItemAliasName
        {
            get
            {
                return this.m_itemAliasName;
            }
            set
            {
                if (value.ToString().Length > 0)
                {
                    this.m_itemAliasName = value;
                }
            }
        }
    }
}
