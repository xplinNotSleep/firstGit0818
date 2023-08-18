using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DMetaData
{
    /// <summary>
    /// �ռ����ϵͳ��Ϣ
    /// </summary>
    public class MetaSpatialRefInfo:MetaDataInfo
    {
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public MetaSpatialRefInfo()
        {
            this.m_metaTableType = EnumMetaTableType.�ռ�ο���Ϣ;
        }

        private EnumMetaCoordsRefName m_coordsRefName;
        /// <summary>
        /// ��ȡ�����ô���������ϵͳ����
        /// </summary>
        [Category("������Ϣ"), Description("����������ϵͳ����"), DefaultValue(EnumMetaCoordsRefName.����80����ϵ)]
        public EnumMetaCoordsRefName CoordsRefName
        {
            get
            {
                return this.m_coordsRefName;
            }
            set
            {
                this.m_coordsRefName = value;
            }
        }

        private EnumMetaCoordsType m_coordsType;
        /// <summary>
        /// ��ȡ����������ϵͳ����
        /// </summary>
        [Category("������Ϣ"), Description("����ϵͳ����"),DefaultValue(EnumMetaCoordsType.ͶӰ����ϵ)]
        public EnumMetaCoordsType CoordsType
        {
            get
            {
                return this.m_coordsType;
            }
            set
            {
                this.m_coordsType = value;
            }
        }

        private string m_coordsName;
        /// <summary>
        /// ��ȡ����������ϵͳ����
        /// </summary>
        [Category("������Ϣ"), Description("����ϵͳ����")]
        public string CoordsName
        {
            get
            {
                return this.m_coordsName;
            }
            set
            {
                this.m_coordsName = value;
            }
        } 

        private string m_projectParameters;
        /// <summary>
        /// ��ȡ������ͶӰ����ϵͳ����
        /// </summary>
        [Category("������Ϣ"), Description("ͶӰ����")]
        public string ProjectParameter
        {
            get
            {
                return this.m_projectParameters;
            }                                   
            set
            {
                this.m_projectParameters = value;
            }
        }
    }
}
