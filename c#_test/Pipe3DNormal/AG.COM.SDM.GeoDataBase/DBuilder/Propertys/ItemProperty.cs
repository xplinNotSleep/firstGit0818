using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// 节点属性项
    /// </summary>
    public class ItemProperty
    {
        protected EnumDataNodeItems m_dataNodeItem;
        protected string m_itemName;
        protected string m_itemAliasName;
        protected object m_tag;

        public event ItemPropertyValueChangedEventHandler ItemPropertyValueChanged;

        /// <summary>
        /// 获取或设置节点类型
        /// </summary>
        [Browsable(false),ReadOnly(true),Description("节点类型")]
        public EnumDataNodeItems DataNodeItem
        {
            get
            {
                return this.m_dataNodeItem;
            }
            set
            {
                this.m_dataNodeItem = value;
            }
        }

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        [Category("常规"),Description("名称")]
        public virtual string ItemName
        {
            get
            {
                return this.m_itemName;
            }
            set
            {
                if (value.ToString().Length > 0)
                {
                    this.m_itemName = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置别名
        /// </summary>
        [Category("常规"),Description("别名")]
        public virtual string ItemAliasName
        {
            get
            {
                return this.m_itemAliasName;
            }
            set
            {
                if (value.ToString().Length > 0)
                {
                    this.m_itemAliasName = value;
                }
            }
        }

        /// <summary>
        /// 获取或设置节点所包含的数据对象
        /// </summary>
        [Browsable(false),Description("数据对象")]
        public virtual object Tag
        {
            get
            {
                return this.m_tag;
            }
            set
            {
                this.m_tag = value;
            }
        }

        /// <summary>
        /// 通知所有登记ItemPropertyValueChanged的事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">事件参数</param>
        public void OnItemPropertyValueChanged(object sender,ItemPropertyEventArgs e)
        {
            if (ItemPropertyValueChanged != null)
            {
                ItemPropertyValueChanged(sender, e);
            }
        }
    }
}
