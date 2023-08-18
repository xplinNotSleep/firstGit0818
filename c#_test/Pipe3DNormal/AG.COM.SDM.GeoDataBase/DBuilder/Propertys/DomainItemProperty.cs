using System.ComponentModel;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// 域节点属性项
    /// </summary>
    public class DomainItemProperty:ItemProperty
    {
        private IDomain m_Domain ;

        /// <summary>
        /// 实例化域属性对象
        /// </summary>
        public DomainItemProperty()
        {
            this.m_itemName = "New Domain";
            this.m_itemAliasName = "新建域";
            this.m_dataNodeItem = EnumDataNodeItems.DomainItem;

            m_Domain = new RangeDomainClass();
            //设置域属性名称
            m_Domain.Name = this.m_itemName;
            //设置域属性描述
            m_Domain.Description = this.m_itemAliasName;
            //设置字段类型
            m_Domain.FieldType = esriFieldType.esriFieldTypeSmallInteger;
        }

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        [Category("常规"), Description("名称")]
        public override string ItemName
        {
            get
            {
                return base.ItemName;
            }
            set
            {
                base.ItemName = value;
                this.m_Domain.Name = value;
            }
        }

        /// <summary>
        /// 获取或设置描述信息
        /// </summary>
        [Category("常规"), Description("描述信息")]
        public override string ItemAliasName
        {
            get
            {
                return base.ItemAliasName;
            }
            set
            {
                base.ItemAliasName = value;
                this.m_Domain.Description = value;
            }
        }

        /// <summary>
        /// 获取或设置域属性对象
        /// </summary>
        [Category("属性设置"), Description("属性域"), TypeConverter(typeof(DomainConverter)),
EditorAttribute(typeof(DomainEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public IDomain Domain
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
        /// 重载ToString方法
        /// </summary>
        /// <returns>返回属性域描述名</returns>
        [Browsable(false)]        
        public override string ToString()
        {
            return this.ItemAliasName;
        }
    }
}
