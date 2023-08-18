using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DMetaData
{
    /// <summary>
    /// 元数据信息
    /// </summary>
    public abstract class MetaDataInfo
    {
        protected bool m_isChanged = false;
        protected string m_subjectName;
        protected EnumMetaTableType m_metaTableType;

        /// <summary>
        /// 获取或设置专题子库名称
        /// </summary>
        [Category("基本信息"),Browsable(false),Description("专题子库名称")]
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
        /// 获取或设置元数据是否修改
        /// </summary>
        [Category("基本信息"),Browsable(false),Description("")]
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
        /// 获取元数据表类型
        /// </summary>
        [Category("基本信息"),Browsable(false),Description("元数据表类型")]
        public EnumMetaTableType MetaTableType
        {
            get
            {
                return this.m_metaTableType;
            }
        }
    }
}
