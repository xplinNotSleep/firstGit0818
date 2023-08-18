namespace AG.COM.SDM.Catalog.Filters
{
    /// <summary>
    /// 默认过滤器
    /// </summary>
    public class DefaultFilter:BaseItemFilter
    {
        /// <summary>
        /// 初始化默认过滤器的实例对象
        /// </summary>
        public DefaultFilter()
        {
            m_FilterName = "默认";
        }

        #region IDataItemFilter 成员

        public override bool CanPass(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            return true;
        }
 

        #endregion
    }
}
