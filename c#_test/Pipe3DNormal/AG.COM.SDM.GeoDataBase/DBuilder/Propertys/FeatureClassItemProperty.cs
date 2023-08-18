using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// 要素类属性项
    /// </summary>
    public class FeatureClassItemProperty:ItemProperty
    {
        private EnumFeatureType m_FeatureType=EnumFeatureType.简单要素类;
        private int m_RefrenceScale = 2000;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FeatureClassItemProperty()
        {
            this.m_dataNodeItem = EnumDataNodeItems.FeatureClassItem;
            this.m_itemName = "NewFeatureClass";
            this.m_itemAliasName = "新建要素类";
        }

        /// <summary>
        /// 获取或设置要素类型
        /// </summary>
        [Category("其他"),Description("要素类型"),DefaultValue(EnumFeatureType.简单要素类)]
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
        /// 获取或设置参考比例
        /// </summary>
        [Category("其他"),Description("参考比例"), DefaultValue(2000)]
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
