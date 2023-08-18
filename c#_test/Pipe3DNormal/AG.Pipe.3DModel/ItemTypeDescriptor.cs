using AG.COM.SDM.GeoDataBase.DBuilder;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AG.Pipe.Analyst3DModel
{
    /// <summary>
    /// 类型描述类
    /// </summary>
    public class ItemTypeDescriptor : ICustomTypeDescriptor
    {
        private object m_selectObject;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="pSelectObject">组件对象</param>
        public ItemTypeDescriptor(object pSelectObject)
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
            List<ItemPropertyDescriptor> tPropDescList = new List<ItemPropertyDescriptor>();
            PropertyDescriptorCollection tPropDescriptionCol = TypeDescriptor.GetProperties(m_selectObject, attributes, true);
            ItemProperty tItemProperty = this.m_selectObject as ItemProperty;


            //if (tItemProperty.DataNodeItem == EnumDataNodeItems.CustomFieldItem)
            //{
            //    //针对字段类型的结点类型

            //    //设置自定义字段的可用状态
            //    SetCustomFieldAttribute(tPropDescList, tPropDescriptionCol);
            //}
            //else if (tItemProperty.DataNodeItem == EnumDataNodeItems.FeatureClassItem)
            //{
            //    //设置要素类项的可用状态
            //    SetFeatureClassAttribute(tPropDescList, tPropDescriptionCol);
            //}
            //else
            //{
            //针对其他类型的结点类型
            for (int i = 0; i < tPropDescriptionCol.Count; i++)
            {
                ItemPropertyDescriptor tPropDescription = new ItemPropertyDescriptor(m_selectObject, tPropDescriptionCol[i]);
                tPropDescList.Add(tPropDescription);
            }
            //}
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
