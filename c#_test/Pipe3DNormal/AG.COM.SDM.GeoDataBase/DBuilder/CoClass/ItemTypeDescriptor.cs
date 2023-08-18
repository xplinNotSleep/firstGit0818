using System;
using System.Collections.Generic;
using System.ComponentModel;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// ����������
    /// </summary>
    public class ItemTypeDescriptor : ICustomTypeDescriptor 
    {
        private object m_selectObject;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        /// <param name="pSelectObject">�������</param>
        public ItemTypeDescriptor(object pSelectObject)
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
            List<ItemPropertyDescriptor> tPropDescList = new List<ItemPropertyDescriptor>();
            PropertyDescriptorCollection tPropDescriptionCol = TypeDescriptor.GetProperties(m_selectObject, attributes, true);
            ItemProperty tItemProperty = this.m_selectObject as ItemProperty;

            
            if (tItemProperty.DataNodeItem == EnumDataNodeItems.CustomFieldItem)
            {
                //����ֶ����͵Ľ������

                //�����Զ����ֶεĿ���״̬
                SetCustomFieldAttribute(tPropDescList, tPropDescriptionCol);       
            }
            else if (tItemProperty.DataNodeItem == EnumDataNodeItems.FeatureClassItem)
            {
                //����Ҫ������Ŀ���״̬
                SetFeatureClassAttribute(tPropDescList, tPropDescriptionCol);
            }
            else
            {
                //����������͵Ľ������
                for (int i = 0; i < tPropDescriptionCol.Count; i++)
                {
                    ItemPropertyDescriptor tPropDescription = new ItemPropertyDescriptor(m_selectObject, tPropDescriptionCol[i]);
                    tPropDescList.Add(tPropDescription);
                }
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

        /// <summary>
        /// �����Զ����ֶ���Ŀ���״̬
        /// </summary>
        /// <param name="pPropDescList">��������List�б���</param>
        /// <param name="pPropDescriptionCol">������������</param>
        private void SetCustomFieldAttribute(List<ItemPropertyDescriptor> pPropDescList,PropertyDescriptorCollection pPropDescriptionCol)
        {
            #region �����Զ����ֶ���Ŀ���״̬
            //��ȡ���ֶ�����
            esriFieldType tFieldType = (esriFieldType)pPropDescriptionCol["FieldType"].GetValue(m_selectObject);

            for (int i = 0; i < pPropDescriptionCol.Count; i++)
            {
                ItemPropertyDescriptor tPropDescription = new ItemPropertyDescriptor(m_selectObject, pPropDescriptionCol[i]);
                if (tPropDescription.DisplayName == "����")
                {
                    if (tFieldType == esriFieldType.esriFieldTypeDouble ||
                        tFieldType == esriFieldType.esriFieldTypeSingle ||
                        tFieldType == esriFieldType.esriFieldTypeInteger ||
                        tFieldType == esriFieldType.esriFieldTypeSmallInteger)
                    {
                        tPropDescription.SetReadOnly(false);
                    }
                    else
                    {
                        tPropDescription.SetReadOnly(true);
                    }
                }
                else if (tPropDescription.DisplayName == "С��λ��")
                {
                    if (tFieldType == esriFieldType.esriFieldTypeDouble ||
                        tFieldType == esriFieldType.esriFieldTypeSingle)
                    {
                        tPropDescription.SetReadOnly(false);
                    }
                    else
                    {
                        tPropDescription.SetReadOnly(true);
                    }
                }
                else if (tPropDescription.DisplayName == "�ֶγ���")
                {
                    if (tFieldType == esriFieldType.esriFieldTypeDouble ||
                        tFieldType == esriFieldType.esriFieldTypeSingle ||
                        tFieldType == esriFieldType.esriFieldTypeInteger ||
                        tFieldType == esriFieldType.esriFieldTypeSmallInteger)
                    {
                        //����Ϊֻ��                        
                        tPropDescription.SetReadOnly(true);
                    }
                    else
                    {   //����Ϊ�ɶ�д
                        tPropDescription.SetReadOnly(false);
                    }
                }
                pPropDescList.Add(tPropDescription);
            }
            #endregion
        }

        /// <summary>
        /// ����Ҫ������Ŀ���״̬
        /// </summary>
        /// <param name="pPropDescList">��������List�б���</param>
        /// <param name="pPropDescriptionCol">������������</param>
        private void SetFeatureClassAttribute(List<ItemPropertyDescriptor> pPropDescList, PropertyDescriptorCollection pPropDescriptionCol)
        {
            #region �����Զ����ֶ���Ŀ���״̬
            if (pPropDescriptionCol["FeatureType"] == null)
            {
                for (int i = 0; i < pPropDescriptionCol.Count; i++)
                {
                    ItemPropertyDescriptor tPropDescription = new ItemPropertyDescriptor(m_selectObject, pPropDescriptionCol[i]);
                    pPropDescList.Add(tPropDescription);
                }
            }
            else
            {
                //��ȡ���ֶ�����
                esriFeatureType tFeatureType = (esriFeatureType)pPropDescriptionCol["FeatureType"].GetValue(m_selectObject);

                for (int i = 0; i < pPropDescriptionCol.Count; i++)
                {
                    ItemPropertyDescriptor tPropDescription = new ItemPropertyDescriptor(m_selectObject, pPropDescriptionCol[i]);
                    //Ҫ���ֶ�����Ϊ"RefrenceScale"
                    if (tPropDescription.DisplayName == "�ο�����")
                    {
                        if (tFeatureType == esriFeatureType.esriFTAnnotation)
                        {
                            tPropDescription.SetReadOnly(false);
                            //tPropDescription.SetBrowsable(true);
                        }
                        else
                        {
                            //tPropDescription.SetBrowsable(false);
                            tPropDescription.SetReadOnly(true);
                        }
                    }

                    pPropDescList.Add(tPropDescription);
                }
            }
            #endregion
        }
    }
}
