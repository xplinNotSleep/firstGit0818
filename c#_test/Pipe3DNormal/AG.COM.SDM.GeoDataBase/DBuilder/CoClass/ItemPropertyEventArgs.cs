using System;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// 委托 节点属性项发生改变时处理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ItemPropertyValueChangedEventHandler(object sender,ItemPropertyEventArgs e);

    /// <summary>
    /// 节点属性事件参数类
    /// </summary>
    public class ItemPropertyEventArgs : EventArgs
    {
        private ItemProperty m_ItemProperty;
        private object m_Value;

        /// <summary>
        /// 实例化新的节点属性参数类
        /// </summary>
        /// <param name="pComponent">节点属性项</param>
        /// <param name="value">Object值对象</param>
        public ItemPropertyEventArgs(ItemProperty pComponent, object value)
        {
            this.m_ItemProperty = pComponent;
            this.m_Value = value;
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        public object Value
        {
            get
            {
                return this.m_Value;
            }
        }

        /// <summary>
        /// 获取属性项
        /// </summary>
        public ItemProperty ItemProperty
        {
            get
            {
                return this.m_ItemProperty;
            }
        }
    }
}
