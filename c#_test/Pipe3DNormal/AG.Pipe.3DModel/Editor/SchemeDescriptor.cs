using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AG.Pipe.Analyst3DModel.Editor
{

    /// <summary>
    /// 属性描述类
    /// </summary>
    public class SchemePropertyDescriptor : PropertyDescriptor
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
        public SchemePropertyDescriptor(object pComponent, PropertyDescriptor pPropertyDescriptor)
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

            ISchemeValueChanged itemProperty = component as ISchemeValueChanged;
            //实例化事件参数
            SchemePropertyEventArgs itemPropertyArgs = new SchemePropertyEventArgs(itemProperty, value);
            //通知所有通知此事件的对象
            itemProperty.OnSchemePropertyValueChanged(itemProperty, itemPropertyArgs);
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

    /// <summary>
    /// 类型描述类
    /// </summary>
    public class SchemeTypeDescriptor : ICustomTypeDescriptor
    {
        private object m_selectObject;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="pSelectObject">组件对象</param>
        public SchemeTypeDescriptor(object pSelectObject)
        {
            this.m_selectObject = pSelectObject;

        }

        #region ICustomTypeDescriptor 成员
        /// <summary>
        /// 获取属性集合
        /// </summary>
        /// <returns>返回属性集合</returns>
        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(m_selectObject, true);
        }

        /// <summary>
        /// 获取类名称
        /// </summary>
        /// <returns>返回类名称</returns>
        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(m_selectObject, true);
        }

        /// <summary>
        /// 获取组件名称
        /// </summary>
        /// <returns>返回组件名称</returns>
        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(m_selectObject, true);
        }

        /// <summary>
        /// 获取类型转换器
        /// </summary>
        /// <returns>返回类型转换器</returns>
        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(m_selectObject, true);
        }

        /// <summary>
        /// 获取组件的默认事件
        /// </summary>
        /// <returns>返回默认事件</returns>
        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(m_selectObject);
        }

        /// <summary>
        /// 获取组件的默认属性
        /// </summary>
        /// <returns>返回默认属性</returns>
        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(m_selectObject);
        }

        /// <summary>
        /// 使用指定的基类型返回指定类型的编辑器
        /// </summary>
        /// <param name="editorBaseType">指定编辑类型</param>
        /// <returns>返回指定类型的编辑器</returns>
        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(m_selectObject, editorBaseType, true);
        }

        /// <summary>
        /// 获取指定组件的属性筛选事件的集合
        /// </summary>
        /// <param name="attributes">属性集</param>
        /// <returns>返回指定组件的事件的集合</returns>
        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(m_selectObject, attributes);
        }

        /// <summary>
        /// 获取指定组件的事件的集合
        /// </summary>
        /// <returns>返回指定组件的事件的集合</returns>
        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(m_selectObject);
        }

        /// <summary>
        /// 获取指定属性筛选器的属性集合
        /// </summary>
        /// <param name="attributes">属性筛选器</param>
        /// <returns>返回属性集合</returns>
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            List<SchemePropertyDescriptor> tPropDescList = new List<SchemePropertyDescriptor>();
            PropertyDescriptorCollection tPropDescriptionCol = TypeDescriptor.GetProperties(m_selectObject, attributes, true);


            //针对其他类型的结点类型
            for (int i = 0; i < tPropDescriptionCol.Count; i++)
            {
                SchemePropertyDescriptor tPropDescription = new SchemePropertyDescriptor(m_selectObject, tPropDescriptionCol[i]);
                tPropDescList.Add(tPropDescription);
            }
            return new PropertyDescriptorCollection(tPropDescList.ToArray());
        }

        /// <summary>
        /// 获取组件的属性集合
        /// </summary>
        /// <returns>返回组件的属性集合</returns>
        public PropertyDescriptorCollection GetProperties()
        {
            return TypeDescriptor.GetProperties(m_selectObject);
        }

        /// <summary>
        /// 获取属性的所有者
        /// </summary>
        /// <param name="pd">属性描述对象</param>
        /// <returns>返回属性的所有者</returns>
        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return m_selectObject;
        }

        #endregion



    }
}
