using System;
using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DMetaData
{
    public class MetaPropertyDescriptor : PropertyDescriptor
    {
        private PropertyDescriptor m_PropertyDescriptor;
        private object m_Component;
        private bool m_IsBrowsable;
        private bool m_IsReadOnly;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="pComponent">ItemProperty对象</param>
        /// <param name="pPropertyDescriptor">PropertyDescriptor对象</param>
        public MetaPropertyDescriptor(object pComponent, PropertyDescriptor pPropertyDescriptor)
            : base(pPropertyDescriptor)
        {
            this.m_Component = pComponent;
            this.m_PropertyDescriptor = pPropertyDescriptor;
            this.m_IsBrowsable = this.m_PropertyDescriptor.IsBrowsable;
            this.m_IsReadOnly = this.m_PropertyDescriptor.IsReadOnly;            
        }

        /// <summary>
        /// 获取浏览属性项
        /// </summary>
        public override bool IsBrowsable
        {
            get
            {                
                return this.m_IsBrowsable;               
            }
        }

        /// <summary>
        /// 获取显示名称(重载)
        /// </summary>
        public override string DisplayName
        {
            get
            {
                if (m_PropertyDescriptor.Description.Length > 0)
                    return m_PropertyDescriptor.Description;
                else
                    return m_PropertyDescriptor.DisplayName;
            }
        }

        /// <summary>
        /// 设置浏览属性项
        /// </summary>
        /// <param name="visible">如果可视则为 true,否则为 false</param>
        public void SetBrowsable(bool visible)
        {        
            this.m_IsBrowsable = visible;
        }

        /// <summary>
        /// 设置只读属性项
        /// </summary>
        /// <param name="isReadonly">如果只读则为 true,否则为 false</param>
        public void SetReadOnly(bool isReadonly)
        {
            this.m_IsReadOnly = isReadonly;
        }

        #region 必须实现的抽象字段或方法
        /// <summary>
        /// 能否重设置当前值
        /// </summary>
        /// <param name="component">组件对象</param>
        /// <returns>如果能则返回 true 否则返回 false </returns>
        public override bool CanResetValue(object component)
        {
            return m_PropertyDescriptor.CanResetValue(component);
        }

        /// <summary>
        /// 获取组件类型
        /// </summary>
        public override Type ComponentType
        {
            get
            {
                return m_PropertyDescriptor.ComponentType;
            }
        }

        /// <summary>
        /// 获取组件对象的Value值
        /// </summary>
        /// <param name="component">组件</param>
        /// <returns>值</returns>
        public override object GetValue(object component)
        {   
             return m_PropertyDescriptor.GetValue(component);
        }

        /// <summary>
        /// 获取该对象是否为只读属性
        /// </summary>
        public override bool IsReadOnly
        {
            get
            {
                return this.m_IsReadOnly;
            }
        }

        /// <summary>
        /// 获取该对象的属性类型
        /// </summary>
        public override Type PropertyType
        {
            get
            {
                return this.m_PropertyDescriptor.PropertyType;
            }
            
        }

        /// <summary>
        /// 重置对象值
        /// </summary>
        /// <param name="component">组件</param>
        public override void ResetValue(object component)
        {
            m_PropertyDescriptor.ResetValue(component);
        }

        /// <summary>
        /// 将组件的值设置为一个不同的值
        /// </summary>
        /// <param name="component">组件</param>
        /// <param name="value">value值</param>
        public override void SetValue(object component, object value)
        {
            m_PropertyDescriptor.SetValue(component, value);

            MetaDataInfo tMetaDataInfo = component as MetaDataInfo;
            if (tMetaDataInfo != null)
            {
                tMetaDataInfo.IsChanged = true;
            }
        }

        /// <summary>
        /// 确定一个值,该值表示是否永久保持此属性的值
        /// </summary>
        /// <param name="component">组件对象</param>
        /// <returns>如果永久保存则返回 true,否则返回 false</returns>
        public override bool ShouldSerializeValue(object component)
        {
            return m_PropertyDescriptor.ShouldSerializeValue(component);
        }
        #endregion       
    }
}
