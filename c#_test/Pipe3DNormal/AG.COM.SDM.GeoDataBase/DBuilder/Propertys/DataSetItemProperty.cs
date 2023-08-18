using System.ComponentModel;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// 数据集属性项
    /// </summary>
    public class DataSetItemProperty:ItemProperty
    {
        private ISpatialReference m_GeoReference =new UnknownCoordinateSystemClass();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DataSetItemProperty()
        {
            this.m_dataNodeItem = EnumDataNodeItems.DataSetItem;
            this.m_itemName = "NewDataSet";
            this.m_itemAliasName = "新建数据集";
        }

        /// <summary>
        /// 获取或设置空间参考
        /// </summary>
        [Category("空间关系"), Description("空间参考"), TypeConverter(typeof(SpatialRefrenceConverter)),
EditorAttribute(typeof(GeoReferenceEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public ISpatialReference GeoReference
        {
            get
            {
                return this.m_GeoReference;
            }
            set
            {
                this.m_GeoReference = value;
            }
        }
    }
}
