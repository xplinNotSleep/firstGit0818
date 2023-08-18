using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// 字段属性类
    /// </summary>
    public class FieldItemProperty:ItemProperty
    {
        private EnumFieldItems m_FieldType=EnumFieldItems.字符型;
        private DomainItemProperty m_Domain;
        private bool m_IsNull=true;
        private string m_DefaultValue="";
        private int m_Length=50;
        private int m_Precision=8;
        private int m_Scale=2;  
  
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FieldItemProperty()
        {
            this.m_dataNodeItem = EnumDataNodeItems.CustomFieldItem;
            this.m_itemName = "NewField";
            this.m_itemAliasName = "新建字段";
        }

        /// <summary>
        /// 获取或设置字段类型
        /// </summary>
        [Category("通用"),Description("字段类型"),DefaultValue(EnumFieldItems.字符型)]        
        public EnumFieldItems FieldType
        {
            get
            {
                return this.m_FieldType;
            }
            set
            {
                this.m_FieldType = value;               
            }
        }

        /// <summary>
        /// 获取或设置域值
        /// </summary>
        [Category("通用"), Description("域值"), DefaultValue(null), 
        EditorAttribute(typeof(ListDomainEditor), typeof(System.Drawing.Design.UITypeEditor))]   
        public DomainItemProperty DomainItemProperty
        {
            get
            {
                return this.m_Domain;
            }
            set
            {
                this.m_Domain = value;
            }
        }

        /// <summary>
        /// 是否允许为空
        /// </summary>
        [Category("通用"), Description("允许为空"),DefaultValue(true)]
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
        /// 获取或设置默认值
        /// </summary>
        [Category("通用"), Description("默认值"),DefaultValue("")]
        public string DefaultValue
        {
            get
            {
                return this.m_DefaultValue;
            }
            set
            {
                this.m_DefaultValue = value;
            }
        }

        /// <summary>
        /// 获取或设置字段长度
        /// </summary>
        [Category("大小"), Description("字段长度"),DefaultValue(50)]
        public int Length
        {
            get
            {
                return this.m_Length;
            }
            set
            {
                this.m_Length = value;
            }
        }

        /// <summary>
        /// 获取或设置精度
        /// </summary>
        [Category("大小"), Description("精度"),DefaultValue(8)]
        public int Precision
        {
            get
            {
                return this.m_Precision;
            }
            set
            {
                this.m_Precision = value;
            }
        }

        /// <summary>
        /// 获取或设置小数位数
        /// </summary>
        [Category("大小"), Description("小数位数"),DefaultValue(2)]
        public int Scale
        {
            get
            {
                return this.m_Scale;
            }
            set
            {
                this.m_Scale = value;
            }
        }
    }   
}
