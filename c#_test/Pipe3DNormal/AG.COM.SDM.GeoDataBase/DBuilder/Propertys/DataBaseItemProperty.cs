using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// ���ݿ���������
    /// </summary>
    public class DataBaseItemProperty:ItemProperty
    {     
        private string m_dbVersion;
        private string m_dbOwner;
        private bool isDefault = false;
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public DataBaseItemProperty()
        {
            this.m_itemName = "NewDataBase";
            this.m_itemAliasName = "�½����ݿ�";
            this.m_dataNodeItem = EnumDataNodeItems.DataBaseItem;
            this.m_dbVersion = "1.0.0";
            this.m_dbOwner = "Augruit_Echo";
        }

        /// <summary>
        /// ��ȡ���������ݿ�汾
        /// </summary>
        [Category("�汾��"),Description("���ݿ�汾"),DefaultValue("1.0.0")]
        public string DBVersion
        {
            get
            {
                return this.m_dbVersion;
            }
            set
            {
                this.m_dbVersion = value;
            }
        }

        /// <summary>
        /// ��ȡ���������ݿ�������
        /// </summary>
        [Description("���ݿ�������"),DefaultValue("Echo")]
        public string DBOwner
        {
            get
            {
                return this.m_dbOwner;
            }
            set
            {
                this.m_dbOwner = value;
            }
        }
        /// <summary>
        /// �Ƿ�Ĭ�ϵ����ݿ�
        /// </summary>
        [Browsable(false)]
        public bool IsDefault
        {
            get => isDefault;
            set => isDefault = value;
        }
    }
}
