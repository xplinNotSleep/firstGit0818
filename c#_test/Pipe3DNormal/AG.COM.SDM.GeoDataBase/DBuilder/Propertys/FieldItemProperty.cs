using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// �ֶ�������
    /// </summary>
    public class FieldItemProperty:ItemProperty
    {
        private EnumFieldItems m_FieldType=EnumFieldItems.�ַ���;
        private DomainItemProperty m_Domain;
        private bool m_IsNull=true;
        private string m_DefaultValue="";
        private int m_Length=50;
        private int m_Precision=8;
        private int m_Scale=2;  
  
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public FieldItemProperty()
        {
            this.m_dataNodeItem = EnumDataNodeItems.CustomFieldItem;
            this.m_itemName = "NewField";
            this.m_itemAliasName = "�½��ֶ�";
        }

        /// <summary>
        /// ��ȡ�������ֶ�����
        /// </summary>
        [Category("ͨ��"),Description("�ֶ�����"),DefaultValue(EnumFieldItems.�ַ���)]        
        public EnumFieldItems FieldType
        {
            get
            {
                return this.m_FieldType;
            }
            set
            {
                this.m_FieldType = value;               
            }
        }

        /// <summary>
        /// ��ȡ��������ֵ
        /// </summary>
        [Category("ͨ��"), Description("��ֵ"), DefaultValue(null), 
        EditorAttribute(typeof(ListDomainEditor), typeof(System.Drawing.Design.UITypeEditor))]   
        public DomainItemProperty DomainItemProperty
        {
            get
            {
                return this.m_Domain;
            }
            set
            {
                this.m_Domain = value;
            }
        }

        /// <summary>
        /// �Ƿ�����Ϊ��
        /// </summary>
        [Category("ͨ��"), Description("����Ϊ��"),DefaultValue(true)]
        public bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
            set
            {
                this.m_IsNull = value;
            }
        }

        /// <summary>
        /// ��ȡ������Ĭ��ֵ
        /// </summary>
        [Category("ͨ��"), Description("Ĭ��ֵ"),DefaultValue("")]
        public string DefaultValue
        {
            get
            {
                return this.m_DefaultValue;
            }
            set
            {
                this.m_DefaultValue = value;
            }
        }

        /// <summary>
        /// ��ȡ�������ֶγ���
        /// </summary>
        [Category("��С"), Description("�ֶγ���"),DefaultValue(50)]
        public int Length
        {
            get
            {
                return this.m_Length;
            }
            set
            {
                this.m_Length = value;
            }
        }

        /// <summary>
        /// ��ȡ�����þ���
        /// </summary>
        [Category("��С"), Description("����"),DefaultValue(8)]
        public int Precision
        {
            get
            {
                return this.m_Precision;
            }
            set
            {
                this.m_Precision = value;
            }
        }

        /// <summary>
        /// ��ȡ������С��λ��
        /// </summary>
        [Category("��С"), Description("С��λ��"),DefaultValue(2)]
        public int Scale
        {
            get
            {
                return this.m_Scale;
            }
            set
            {
                this.m_Scale = value;
            }
        }
    }   
}
