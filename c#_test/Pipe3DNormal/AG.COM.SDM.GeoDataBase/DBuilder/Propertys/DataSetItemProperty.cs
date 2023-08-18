using System.ComponentModel;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// ���ݼ�������
    /// </summary>
    public class DataSetItemProperty:ItemProperty
    {
        private ISpatialReference m_GeoReference =new UnknownCoordinateSystemClass();

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public DataSetItemProperty()
        {
            this.m_dataNodeItem = EnumDataNodeItems.DataSetItem;
            this.m_itemName = "NewDataSet";
            this.m_itemAliasName = "�½����ݼ�";
        }

        /// <summary>
        /// ��ȡ�����ÿռ�ο�
        /// </summary>
        [Category("�ռ��ϵ"), Description("�ռ�ο�"), TypeConverter(typeof(SpatialRefrenceConverter)),
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
