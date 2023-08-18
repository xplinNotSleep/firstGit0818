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
        /// Ĭ�Ϲ��캯��
        /// </summary>
        /// <param name="pComponent">ItemProperty����</param>
        /// <param name="pPropertyDescriptor">PropertyDescriptor����</param>
        public MetaPropertyDescriptor(object pComponent, PropertyDescriptor pPropertyDescriptor)
            : base(pPropertyDescriptor)
        {
            this.m_Component = pComponent;
            this.m_PropertyDescriptor = pPropertyDescriptor;
            this.m_IsBrowsable = this.m_PropertyDescriptor.IsBrowsable;
            this.m_IsReadOnly = this.m_PropertyDescriptor.IsReadOnly;            
        }

        /// <summary>
        /// ��ȡ���������
        /// </summary>
        public override bool IsBrowsable
        {
            get
            {                
                return this.m_IsBrowsable;               
            }
        }

        /// <summary>
        /// ��ȡ��ʾ����(����)
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
        /// �������������
        /// </summary>
        /// <param name="visible">���������Ϊ true,����Ϊ false</param>
        public void SetBrowsable(bool visible)
        {        
            this.m_IsBrowsable = visible;
        }

        /// <summary>
        /// ����ֻ��������
        /// </summary>
        /// <param name="isReadonly">���ֻ����Ϊ true,����Ϊ false</param>
        public void SetReadOnly(bool isReadonly)
        {
            this.m_IsReadOnly = isReadonly;
        }

        #region ����ʵ�ֵĳ����ֶλ򷽷�
        /// <summary>
        /// �ܷ������õ�ǰֵ
        /// </summary>
        /// <param name="component">�������</param>
        /// <returns>������򷵻� true ���򷵻� false </returns>
        public override bool CanResetValue(object component)
        {
            return m_PropertyDescriptor.CanResetValue(component);
        }

        /// <summary>
        /// ��ȡ�������
        /// </summary>
        public override Type ComponentType
        {
            get
            {
                return m_PropertyDescriptor.ComponentType;
            }
        }

        /// <summary>
        /// ��ȡ��������Valueֵ
        /// </summary>
        /// <param name="component">���</param>
        /// <returns>ֵ</returns>
        public override object GetValue(object component)
        {   
             return m_PropertyDescriptor.GetValue(component);
        }

        /// <summary>
        /// ��ȡ�ö����Ƿ�Ϊֻ������
        /// </summary>
        public override bool IsReadOnly
        {
            get
            {
                return this.m_IsReadOnly;
            }
        }

        /// <summary>
        /// ��ȡ�ö������������
        /// </summary>
        public override Type PropertyType
        {
            get
            {
                return this.m_PropertyDescriptor.PropertyType;
            }
            
        }

        /// <summary>
        /// ���ö���ֵ
        /// </summary>
        /// <param name="component">���</param>
        public override void ResetValue(object component)
        {
            m_PropertyDescriptor.ResetValue(component);
        }

        /// <summary>
        /// �������ֵ����Ϊһ����ͬ��ֵ
        /// </summary>
        /// <param name="component">���</param>
        /// <param name="value">valueֵ</param>
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
        /// ȷ��һ��ֵ,��ֵ��ʾ�Ƿ����ñ��ִ����Ե�ֵ
        /// </summary>
        /// <param name="component">�������</param>
        /// <returns>������ñ����򷵻� true,���򷵻� false</returns>
        public override bool ShouldSerializeValue(object component)
        {
            return m_PropertyDescriptor.ShouldSerializeValue(component);
        }
        #endregion       
    }
}
