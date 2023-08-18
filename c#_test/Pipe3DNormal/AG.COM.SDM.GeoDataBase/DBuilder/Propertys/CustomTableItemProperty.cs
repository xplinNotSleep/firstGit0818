namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// 属性表属性项类
    /// </summary>
    public class CustomTableItemProperty:ItemProperty
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CustomTableItemProperty()
        {
            this.m_dataNodeItem = EnumDataNodeItems.CustomTableItem;
            this.m_itemName = "New CustomTable";
            this.m_itemAliasName = "新建属性表";
        }
    }
}
