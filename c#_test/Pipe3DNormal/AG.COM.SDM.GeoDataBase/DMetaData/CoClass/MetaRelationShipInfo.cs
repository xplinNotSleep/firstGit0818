using System;
using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DMetaData
{
    /// <summary>
    /// Ԫ����֮��ϵ��Ϣ
    /// </summary>
    public class MetaRelationShipInfo : MetaDataInfo
    {
        private DateTime m_publishTime;
        private string m_deptName;
        private string m_relationshipName;
        private string m_deptTel;
        private string m_deptFax;
        private string m_zipCode;
        private string m_deptAddress;
        private string m_deptEmail;
        private string m_deptWeb;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public MetaRelationShipInfo()
        {
            this.m_metaTableType = EnumMetaTableType.��ϵ��Ϣ; 
        }
        /// <summary>
        /// ��ȡ�����÷�������
        /// </summary>
        [Category("������Ϣ"), Description("��������")] 
        public DateTime PublishTime
        {
            get
            {
                return this.m_publishTime;
            }
            set
            {
                this.m_publishTime = value;
            }
        }

        /// <summary>
        /// ��ȡ�����÷�����λ����
        /// </summary>
        [Category("������Ϣ"), Description("������λ����")] 
        public string DepartmentName
        {
            get
            {
                return this.m_deptName;
            }
            set
            {
                this.m_deptName = value;
            }
        }

        /// <summary>
        /// ��ȡ��������ϵ�˵绰
        /// </summary>
        [Category("������Ϣ"), Description("��ϵ�˵绰")] 
        public string DepartmentTel
        {
            get
            {
                return this.m_deptTel;
            }
            set
            {
                this.m_deptTel = value;
            }
        }

        /// <summary>
        /// ��ȡ���������ݼ�������λͨ�ŵ�ַ
        /// </summary>
        [Category("������Ϣ"), Description("ͨ�ŵ�ַ")] 
        public string DepartmentAddress
        {
            get
            {
                return this.m_deptAddress;
            }
            set
            {
                this.m_deptAddress = value;
            }
        }

        /// <summary>
        /// ��ȡ��������������
        /// </summary>
        [Category("������Ϣ"), Description("��������")]
        public string ZipCode
        {
            get
            {
                return this.m_zipCode;
            }
            set
            {
                this.m_zipCode = value;
            }
        }

        /// <summary>
        /// ��ȡ��������ϵ��
        /// </summary>
        [Category("������Ϣ"), Description("��ϵ������")]
        public string RelationShipName
        {
            get
            {
                return this.m_relationshipName;
            }
            set
            {
                this.m_relationshipName = value;
            }
        }


        /// <summary>
        /// ��ȡ���������ݼ�������λ����
        /// </summary>
        [Category("������Ϣ"), Description("����")]
        public string DepartmentFax
        {
            get
            {
                return this.m_deptFax;
            }
            set
            {
                this.m_deptFax = value;
            }
        } 

        /// <summary>
        /// ��ȡ�����õ��������ַ
        /// </summary>
        [Category("������Ϣ"), Description("���������ַ")]
        public string DepartmentEmail
        {
            get
            {
                return this.m_deptEmail;
            }
            set
            {
                this.m_deptEmail = value;
            }
        }

        /// <summary>
        /// ��ȡ��������ַ
        /// </summary>
        [Category("������Ϣ"), Description("��ַ")]
        public string DepartmentWeb
        {
            get
            {
                return this.m_deptWeb;
            }
            set
            {
                this.m_deptWeb = value;
            }
        }
    }
}
