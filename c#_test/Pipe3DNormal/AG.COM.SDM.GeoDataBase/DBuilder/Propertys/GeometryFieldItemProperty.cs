using System.ComponentModel;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// 几何字段属性类
    /// </summary>
    public class GeometryFieldItemProperty:ItemProperty
    {
        private int m_AveragePoint;      
        private int m_Grid1 = 1000;
        private int m_Grid2 = 0;
        private int m_Grid3 = 0;
        private bool m_IsContainM = false;
        private bool m_IsContainZ = false;
        private bool m_IsNull=false;
        private ISpatialReference m_GeoReference = new UnknownCoordinateSystemClass();
        private EnumGeometryItems m_GeometryType = EnumGeometryItems.点;
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public GeometryFieldItemProperty()
        {
            this.m_dataNodeItem = EnumDataNodeItems.GeometryFieldItem;
            this.m_itemName = "Shape";
            this.m_itemAliasName = "shape";
        }

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        [Category("常规"),ReadOnly(true),Description("名称")]
        public override string ItemName
        {
            get
            {
                return base.ItemName;
            }
            set
            {
                base.ItemName = value;
            }
        }

        /// <summary>
        /// 获取字段类型
        /// </summary>
        [Category("通用"),ReadOnly(true),Description("字段类型")]
        public EnumFieldItems FieldType
        {
            get
            {
                return EnumFieldItems.几何对象;
            }           
        }

        /// <summary>
        /// 获取或设置是否允许空
        /// </summary>
        [Category("通用"), Description("允许为空"),DefaultValue(false)]
        public bool IsNull
        {
            get
            {
                return this.m_IsNull;
            }
            set
            {
                this.m_IsNull = value;
            }
        }

        /// <summary>
        /// 获取或设置平均点数
        /// </summary>
        [Category("几何类型"),Description("平均点数"),DefaultValue(0)]
        public int AveragePoint
        {
            get
            {
                return this.m_AveragePoint;
            }
            set
            {
                this.m_AveragePoint = value;
            }
        }

        /// <summary>
        /// 获取或设置几何类型
        /// </summary>
        [Category("几何类型"), Description("几何类型"),DefaultValue(1)]
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

        /// <summary>
        /// 获取或设置Grid1
        /// </summary>
        [Category("几何类型"), Description("Grid 1"), DefaultValue(1000)]
        public int Grid1
        {
            get
            {
                return this.m_Grid1;
            }
            set
            {
                this.m_Grid1 = value;
            }
        }

        /// <summary>
        /// 获取或设置Grid2
        /// </summary>
        [Category("几何类型"), Description("Grid 2"), DefaultValue(0)]
        public int Grid2
        {
            get
            {
                return this.m_Grid2;
            }
            set
            {
                this.m_Grid2 = value;
            }
        }

        /// <summary>
        /// 获取或设置Grid3
        /// </summary>
        [Category("几何类型"), Description("Grid 3"), DefaultValue(0)]
        public int Grid3
        {
            get
            {
                return this.m_Grid3;
            }
            set
            {
                this.m_Grid3 = value;
            }
        }

        /// <summary>
        /// 获取或设置是否包含M值
        /// </summary>
        [Category("几何类型"), Description("包含M值"), DefaultValue(false)]
        public bool IsContainM
        {
            get
            {
                return this.m_IsContainM;
            }
            set
            {
                this.m_IsContainM = value;
            }
        }

        /// <summary>
        /// 获取或设置是否包含Z值
        /// </summary>
        [Category("几何类型"), Description("包含Z值"), DefaultValue(false)]
        public bool IsContainZ
        {
            get
            {
                return this.m_IsContainZ;
            }
            set
            {
                this.m_IsContainZ = value;
            }
        }

        /// <summary>
        /// 获取或设置空间参考
        /// </summary>
        [Category("几何类型"),Browsable(true),Description("空间参考")]
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
