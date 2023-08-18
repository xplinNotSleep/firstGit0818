using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// �ڵ�������
    /// </summary>
    public class ItemProperty
    {
        protected EnumDataNodeItems m_dataNodeItem;
        protected string m_itemName;
        protected string m_itemAliasName;
        protected object m_tag;

        public event ItemPropertyValueChangedEventHandler ItemPropertyValueChanged;

        /// <summary>
        /// ��ȡ�����ýڵ�����
        /// </summary>
        [Browsable(false),ReadOnly(true),Description("�ڵ�����")]
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
        /// ��ȡ����������
        /// </summary>
        [Category("����"),Description("����")]
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
        /// ��ȡ�����ñ���
        /// </summary>
        [Category("����"),Description("����")]
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
        /// ��ȡ�����ýڵ������������ݶ���
        /// </summary>
        [Browsable(false),Description("���ݶ���")]
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
        /// ֪ͨ���еǼ�ItemPropertyValueChanged���¼�
        /// </summary>
        /// <param name="sender">����</param>
        /// <param name="e">�¼�����</param>
        public void OnItemPropertyValueChanged(object sender,ItemPropertyEventArgs e)
        {
            if (ItemPropertyValueChanged != null)
            {
                ItemPropertyValueChanged(sender, e);
            }
        }
    }
}
