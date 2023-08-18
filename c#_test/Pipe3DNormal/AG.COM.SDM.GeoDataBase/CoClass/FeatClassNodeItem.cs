using System.ComponentModel;
using AG.COM.SDM.GeoDataBase.DBuilder;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// ���ݹ��� Ҫ������������
    /// </summary>
    public sealed class FeatClassNodeItem : ItemProperty
    {
        private EnumFeatureType m_FeatureType = EnumFeatureType.��ҪҪ����;
        private EnumGeometryItems m_GeometryType = EnumGeometryItems.��;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public FeatClassNodeItem()
        {
            this.m_dataNodeItem = EnumDataNodeItems.FeatureClassItem;
            this.m_itemName = "NewFeatureClass";
            this.m_itemAliasName = "�½�Ҫ����";
        }

        /// <summary>
        /// Ҫ������
        /// </summary>
        [Category("����"),Browsable(false), Description("Ҫ������"), DefaultValue(EnumFeatureType.��ҪҪ����)]
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
        /// ��������
        /// </summary>
        [Category("����"),Browsable(false),Description("��������"), DefaultValue(EnumGeometryItems.��)]
        public EnumGeometryItems GeometryType
        {
            get
            {
                return this.m_GeometryType;
            }
            set
            {
                this.m_GeometryType = value;
            }
        }
    }
}
