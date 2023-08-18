
namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// 专题子库属性项
    /// </summary>
    public class SubjectItemProperty:ItemProperty
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SubjectItemProperty()
        {
            this.m_itemName = "NewSubjectDataBase";
            this.m_itemAliasName = "新建专题库";
            this.m_dataNodeItem = EnumDataNodeItems.SubjectChildItem;
        }
    }
}
