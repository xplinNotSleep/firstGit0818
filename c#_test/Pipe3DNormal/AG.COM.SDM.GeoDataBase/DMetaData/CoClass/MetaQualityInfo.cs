using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DMetaData
{
    /// <summary>
    /// ����������Ϣ
    /// </summary>
    public class MetaQualityInfo:MetaDataInfo
    {
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public MetaQualityInfo()
        {
            this.m_metaTableType = EnumMetaTableType.������Ϣ; 
        }

        private string m_qualityComment;
        /// <summary>
        /// ��ȡ������������������
        /// </summary>
        [Category("������Ϣ"), Description("������������")]
        public string QualityComment
        {
            get 
            { 
                return m_qualityComment; 
            }
            set 
            {
                this.m_qualityComment = value; 
            }
        }

        private string m_qualityLineage;
        /// <summary>
        /// ��ȡ����������־
        /// </summary>
        [Category("������Ϣ"),Description("����־")]
        public string QualityLineage
        {
            get
            {
                return this.m_qualityLineage;
            }
            set
            {
                this.m_qualityLineage = value;
            }
        }

    }
}
