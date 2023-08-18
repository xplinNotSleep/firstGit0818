using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 列表项ListDomainItem类
    /// </summary>
    public class ListDomainItem
    {
        private IDomain m_Domain;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="pDomain">IDomain对象</param>
        public ListDomainItem(IDomain pDomain)
        {
            this.m_Domain = pDomain;
        }

        /// <summary>
        /// 获取属性域
        /// </summary>
        public IDomain Domain
        {
            get
            {
                return this.m_Domain;
            }
        }

        /// <summary>
        /// 重载ToString()方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.m_Domain.Description;
        }
    }
}
