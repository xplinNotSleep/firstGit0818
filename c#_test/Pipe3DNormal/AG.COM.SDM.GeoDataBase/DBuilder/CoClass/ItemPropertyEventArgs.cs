using System;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// ί�� �ڵ���������ı�ʱ����
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ItemPropertyValueChangedEventHandler(object sender,ItemPropertyEventArgs e);

    /// <summary>
    /// �ڵ������¼�������
    /// </summary>
    public class ItemPropertyEventArgs : EventArgs
    {
        private ItemProperty m_ItemProperty;
        private object m_Value;

        /// <summary>
        /// ʵ�����µĽڵ����Բ�����
        /// </summary>
        /// <param name="pComponent">�ڵ�������</param>
        /// <param name="value">Objectֵ����</param>
        public ItemPropertyEventArgs(ItemProperty pComponent, object value)
        {
            this.m_ItemProperty = pComponent;
            this.m_Value = value;
        }

        /// <summary>
        /// ��ȡ����ֵ
        /// </summary>
        public object Value
        {
            get
            {
                return this.m_Value;
            }
        }

        /// <summary>
        /// ��ȡ������
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
