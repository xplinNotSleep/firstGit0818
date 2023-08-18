using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// �б���ListDomainItem��
    /// </summary>
    public class ListDomainItem
    {
        private IDomain m_Domain;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        /// <param name="pDomain">IDomain����</param>
        public ListDomainItem(IDomain pDomain)
        {
            this.m_Domain = pDomain;
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        public IDomain Domain
        {
            get
            {
                return this.m_Domain;
            }
        }

        /// <summary>
        /// ����ToString()����
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.m_Domain.Description;
        }
    }
}
