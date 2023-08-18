using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// Ҫ����������
    /// </summary>
    public class FeatureClassItemProperty:ItemProperty
    {
        private EnumFeatureType m_FeatureType=EnumFeatureType.��Ҫ����;
        private int m_RefrenceScale = 2000;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public FeatureClassItemProperty()
        {
            this.m_dataNodeItem = EnumDataNodeItems.FeatureClassItem;
            this.m_itemName = "NewFeatureClass";
            this.m_itemAliasName = "�½�Ҫ����";
        }

        /// <summary>
        /// ��ȡ������Ҫ������
        /// </summary>
        [Category("����"),Description("Ҫ������"),DefaultValue(EnumFeatureType.��Ҫ����)]
        public EnumFeatureType FeatureType
        {
            get
            {
                return m_FeatureType;
            }
            set
            {
                m_FeatureType = value;
            }
        }

        /// <summary>
        /// ��ȡ�����òο�����
        /// </summary>
        [Category("����"),Description("�ο�����"), DefaultValue(2000)]
        public int RefrenceScale
        {
            get
            {
                return this.m_RefrenceScale;
            }
            set
            {
                this.m_RefrenceScale = value;
            }
        }
    }
}
