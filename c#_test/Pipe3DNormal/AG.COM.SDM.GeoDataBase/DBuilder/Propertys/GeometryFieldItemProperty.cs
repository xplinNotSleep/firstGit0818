using System.ComponentModel;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// �����ֶ�������
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
        private EnumGeometryItems m_GeometryType = EnumGeometryItems.��;
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public GeometryFieldItemProperty()
        {
            this.m_dataNodeItem = EnumDataNodeItems.GeometryFieldItem;
            this.m_itemName = "Shape";
            this.m_itemAliasName = "shape";
        }

        /// <summary>
        /// ��ȡ����������
        /// </summary>
        [Category("����"),ReadOnly(true),Description("����")]
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
        /// ��ȡ�ֶ�����
        /// </summary>
        [Category("ͨ��"),ReadOnly(true),Description("�ֶ�����")]
        public EnumFieldItems FieldType
        {
            get
            {
                return EnumFieldItems.���ζ���;
            }           
        }

        /// <summary>
        /// ��ȡ�������Ƿ������
        /// </summary>
        [Category("ͨ��"), Description("����Ϊ��"),DefaultValue(false)]
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
        /// ��ȡ������ƽ������
        /// </summary>
        [Category("��������"),Description("ƽ������"),DefaultValue(0)]
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
        /// ��ȡ�����ü�������
        /// </summary>
        [Category("��������"), Description("��������"),DefaultValue(1)]
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
        /// ��ȡ������Grid1
        /// </summary>
        [Category("��������"), Description("Grid 1"), DefaultValue(1000)]
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
        /// ��ȡ������Grid2
        /// </summary>
        [Category("��������"), Description("Grid 2"), DefaultValue(0)]
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
        /// ��ȡ������Grid3
        /// </summary>
        [Category("��������"), Description("Grid 3"), DefaultValue(0)]
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
        /// ��ȡ�������Ƿ����Mֵ
        /// </summary>
        [Category("��������"), Description("����Mֵ"), DefaultValue(false)]
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
        /// ��ȡ�������Ƿ����Zֵ
        /// </summary>
        [Category("��������"), Description("����Zֵ"), DefaultValue(false)]
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
        /// ��ȡ�����ÿռ�ο�
        /// </summary>
        [Category("��������"),Browsable(true),Description("�ռ�ο�")]
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
