using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DMetaData
{
    /// <summary>
    /// Ԫ��������������
    /// </summary>
    public class MetaTypeDescriptor : ICustomTypeDescriptor
    {
        private object m_selectObject;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        /// <param name="pSelectObject">�������</param>
        public MetaTypeDescriptor(object pSelectObject)
        {
            this.m_selectObject = pSelectObject;
        }
        #region ICustomTypeDescriptor ��Ա
        /// <summary>
        /// ��ȡ���Լ���
        /// </summary>
        /// <returns>�������Լ���</returns>
        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(m_selectObject, true);
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <returns>����������</returns>
        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(m_selectObject, true);
        }

        /// <summary>
        /// ��ȡ�������
        /// </summary>
        /// <returns>�����������</returns>
        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(m_selectObject, true);
        }

        /// <summary>
        /// ��ȡ����ת����
        /// </summary>
        /// <returns>��������ת����</returns>
        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(m_selectObject, true);
        }

        /// <summary>
        /// ��ȡ�����Ĭ���¼�
        /// </summary>
        /// <returns>����Ĭ���¼�</returns>
        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(m_selectObject);
        }

        /// <summary>
        /// ��ȡ�����Ĭ������
        /// </summary>
        /// <returns>����Ĭ������</returns>
        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(m_selectObject);
        }

        /// <summary>
        /// ʹ��ָ���Ļ����ͷ���ָ�����͵ı༭��
        /// </summary>
        /// <param name="editorBaseType">ָ���༭����</param>
        /// <returns>����ָ�����͵ı༭��</returns>
        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(m_selectObject, editorBaseType, true);
        }

        /// <summary>
        /// ��ȡָ�����������ɸѡ�¼��ļ���
        /// </summary>
        /// <param name="attributes">���Լ�</param>
        /// <returns>����ָ��������¼��ļ���</returns>
        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(m_selectObject, attributes);
        }

        /// <summary>
        /// ��ȡָ��������¼��ļ���
        /// </summary>
        /// <returns>����ָ��������¼��ļ���</returns>
        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(m_selectObject);
        }

        /// <summary>
        /// ��ȡָ������ɸѡ�������Լ���
        /// </summary>
        /// <param name="attributes">����ɸѡ��</param>
        /// <returns>�������Լ���</returns>
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            List<MetaPropertyDescriptor> tPropDescList = new List<MetaPropertyDescriptor>();
            PropertyDescriptorCollection tPropDescriptionCol = TypeDescriptor.GetProperties(m_selectObject, attributes, true);
           
            //����������͵Ľ������
            for (int i = 0; i < tPropDescriptionCol.Count; i++)
            {
                MetaPropertyDescriptor tPropDescription = new MetaPropertyDescriptor(m_selectObject, tPropDescriptionCol[i]);
                tPropDescList.Add(tPropDescription);
            }
           
            return new PropertyDescriptorCollection(tPropDescList.ToArray());
        }

        /// <summary>
        /// ��ȡ��������Լ���
        /// </summary>
        /// <returns>������������Լ���</returns>
        public PropertyDescriptorCollection GetProperties()
        {
            return TypeDescriptor.GetProperties(m_selectObject);
        }

        /// <summary>
        /// ��ȡ���Ե�������
        /// </summary>
        /// <param name="pd">������������</param>
        /// <returns>�������Ե�������</returns>
        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return m_selectObject;
        }

        #endregion
    }
}
