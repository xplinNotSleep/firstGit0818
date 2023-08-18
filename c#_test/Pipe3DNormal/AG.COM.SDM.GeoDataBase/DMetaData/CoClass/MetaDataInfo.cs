using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DMetaData
{
    /// <summary>
    /// Ԫ������Ϣ
    /// </summary>
    public abstract class MetaDataInfo
    {
        protected bool m_isChanged = false;
        protected string m_subjectName;
        protected EnumMetaTableType m_metaTableType;

        /// <summary>
        /// ��ȡ������ר���ӿ�����
        /// </summary>
        [Category("������Ϣ"),Browsable(false),Description("ר���ӿ�����")]
        public string SubjectName
        {
            get
            {
                return this.m_subjectName;
            }
            set
            {
                this.m_subjectName = value;
            }
        }

        /// <summary>
        /// ��ȡ������Ԫ�����Ƿ��޸�
        /// </summary>
        [Category("������Ϣ"),Browsable(false),Description("")]
        public bool IsChanged
        {
            get
            {
                return this.m_isChanged;
            }
            set
            {
                this.m_isChanged = true;
            }
        }

        /// <summary>
        /// ��ȡԪ���ݱ�����
        /// </summary>
        [Category("������Ϣ"),Browsable(false),Description("Ԫ���ݱ�����")]
        public EnumMetaTableType MetaTableType
        {
            get
            {
                return this.m_metaTableType;
            }
        }
    }
}
