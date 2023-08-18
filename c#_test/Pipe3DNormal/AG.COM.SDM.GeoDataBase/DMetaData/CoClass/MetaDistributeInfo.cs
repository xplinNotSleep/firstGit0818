using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DMetaData
{
    /// <summary>
    /// �ַ���Ϣ
    /// </summary>
    public class MetaDistributeInfo:MetaDataInfo 
    {
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public MetaDistributeInfo()
        {
            this.m_metaTableType = EnumMetaTableType.�ַ���Ϣ;
        }
        private string m_onLine;
        /// <summary>
        /// ��ȡ��������������
        /// </summary>
        [Category("������Ϣ"), Description("��������")]
        public string OnLine
        {
            get
            {
                return this.m_onLine;
            }
            set
            {
                this.m_onLine = value;
            }
        }

        private string m_distributeDepartName;
        /// <summary>
        /// ��ȡ�����÷ַ���λ����
        /// </summary>
        [Category("������Ϣ"), Description("�ַ���λ����")]
        public string DistributeDepartName
        {
            get
            {
                return this.m_distributeDepartName;
            }
            set
            {
                this.m_distributeDepartName = value;
            }
        }

        private string m_relPersonName;
        /// <summary>
        /// ��ȡ��������ϵ������
        /// </summary>
        [Category("��ϵ��Ϣ"), Description("��ϵ������")]
        public string RelationPersonName
        {
            get
            {
                return this.m_relPersonName;
            }
            set 
            {
                this.m_relPersonName=value;
            }
        }

        private string m_relPersonTel;

        /// <summary>
        /// ��ȡ��������ϵ�˵绰
        /// </summary>
        [Category("��ϵ��Ϣ"),Description("��ϵ�˵绰")]
        public string RelationPersonTel
        {
            get
            {
                return this.m_relPersonTel;
            }
            set
            {
                this.m_relPersonTel = value;
            }
        }

        private string m_relPersonFax;
        /// <summary>
        /// ��ȡ���������ݼ��ַ���λ�Ĵ���
        /// </summary>
        public string RelationPersonFax
        {
            get
            {
                return this.m_relPersonFax;
            }
            set
            {
                this.m_relPersonFax = value;
            }
        }

        private string m_relDepartAddress;
        /// <summary>
        /// ��ȡ���������ݼ��ַ���λ��ͨ�ŵ�ַ
        /// </summary>
        [Category("��ϵ��Ϣ"),Description("ͨ�ŵ�ַ")]
        public string RelationDepartAddress
        {
            get
            {
                return this.m_relDepartAddress;
            }
            set
            {
                this.m_relDepartAddress = value;
            }
        }

        private string m_relDepartZip;
        /// <summary>
        /// ��ȡ���������ݼ��ַ���λ��������
        /// </summary>
        [Category("��ϵ��Ϣ"), Description("��������")]
        public string RelationDepartZip
        {
            get
            {
                return this.m_relDepartZip;
            }
            set
            {
                this.m_relDepartZip = value;
            }
        }

        private string m_relDepartEmail;
        /// <summary>
        /// ��ȡ���������ݼ��ַ���λ���������ַ
        /// </summary>
        [Category("��ϵ��Ϣ"), Description("��������")]
        public string RelationDepartEmail
        {
            get
            {
                return this.m_relDepartZip;
            }
            set
            {
                this.m_relDepartZip = value;
            }
        }

        private string m_relDepartLinkWeb;

        /// <summary>
        /// ��ȡ���������ݼ��ַ���λ����ַ
        /// </summary>
        [Category("��ϵ��Ϣ"), Description("��ַ")]
        public string RelationDepartLinkWeb
        {
            get
            {
                return this.m_relDepartLinkWeb;
            }
            set
            {
                this.m_relDepartLinkWeb = value;
            }
        }  
    }
}
