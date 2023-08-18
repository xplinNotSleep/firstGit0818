using System.ComponentModel;
using AG.COM.SDM.GeoDataBase.DBuilder;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 数据管理 要素类型属性项
    /// </summary>
    public sealed class FeatClassNodeItem : ItemProperty
    {
        private EnumFeatureType m_FeatureType = EnumFeatureType.简要要素类;
        private EnumGeometryItems m_GeometryType = EnumGeometryItems.点;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FeatClassNodeItem()
        {
            this.m_dataNodeItem = EnumDataNodeItems.FeatureClassItem;
            this.m_itemName = "NewFeatureClass";
            this.m_itemAliasName = "新建要素类";
        }

        /// <summary>
        /// 要素类型
        /// </summary>
        [Category("其他"),Browsable(false), Description("要素类型"), DefaultValue(EnumFeatureType.简要要素类)]
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
        /// 几何类型
        /// </summary>
        [Category("其他"),Browsable(false),Description("几何类型"), DefaultValue(EnumGeometryItems.点)]
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
