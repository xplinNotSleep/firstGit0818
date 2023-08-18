using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// ר���������������
    /// </summary>
    public class SubDomainItemProperty:ItemProperty
    {
        /// <summary>
        /// ʵ�����¶���
        /// </summary>
        public SubDomainItemProperty()
        {
            this.m_itemName = "SubDomains";
            this.m_itemAliasName = "ר��������";
            this.m_dataNodeItem = EnumDataNodeItems.SubjectDomainItem;
        }

        /// <summary>
        /// ��ȡ����������
        /// </summary>
        [Category("����"),ReadOnly(true), Description("����")]
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
        /// ��ȡ�����ñ���
        /// </summary>
        [Category("����"), ReadOnly(true), Description("����")]
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
