namespace AG.COM.SDM.Catalog.Filters
{
    /// <summary>
    /// Ĭ�Ϲ�����
    /// </summary>
    public class DefaultFilter:BaseItemFilter
    {
        /// <summary>
        /// ��ʼ��Ĭ�Ϲ�������ʵ������
        /// </summary>
        public DefaultFilter()
        {
            m_FilterName = "Ĭ��";
        }

        #region IDataItemFilter ��Ա

        public override bool CanPass(AG.COM.SDM.Catalog.DataItems.DataItem item)
        {
            return true;
        }
 

        #endregion
    }
}
