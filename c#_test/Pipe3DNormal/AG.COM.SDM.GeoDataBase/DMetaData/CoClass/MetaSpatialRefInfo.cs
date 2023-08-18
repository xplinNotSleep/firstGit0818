using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DMetaData
{
    /// <summary>
    /// 空间参照系统信息
    /// </summary>
    public class MetaSpatialRefInfo:MetaDataInfo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MetaSpatialRefInfo()
        {
            this.m_metaTableType = EnumMetaTableType.空间参考信息;
        }

        private EnumMetaCoordsRefName m_coordsRefName;
        /// <summary>
        /// 获取或设置大地坐标参照系统名称
        /// </summary>
        [Category("基本信息"), Description("大地坐标参照系统名称"), DefaultValue(EnumMetaCoordsRefName.西安80坐标系)]
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
        /// 获取或设置坐标系统类型
        /// </summary>
        [Category("基本信息"), Description("坐标系统类型"),DefaultValue(EnumMetaCoordsType.投影坐标系)]
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
        /// 获取或设置坐标系统名称
        /// </summary>
        [Category("基本信息"), Description("坐标系统名称")]
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
        /// 获取或设置投影坐标系统参数
        /// </summary>
        [Category("基本信息"), Description("投影参数")]
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
