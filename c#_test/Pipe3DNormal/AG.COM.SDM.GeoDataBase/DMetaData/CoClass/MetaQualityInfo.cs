using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DMetaData
{
    /// <summary>
    /// 数据质量信息
    /// </summary>
    public class MetaQualityInfo:MetaDataInfo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MetaQualityInfo()
        {
            this.m_metaTableType = EnumMetaTableType.质量信息; 
        }

        private string m_qualityComment;
        /// <summary>
        /// 获取或设置数据质量概述
        /// </summary>
        [Category("基本信息"), Description("数据质量概述")]
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
        /// 获取或设置数据志
        /// </summary>
        [Category("基本信息"),Description("数据志")]
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
