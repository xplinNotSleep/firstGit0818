using System.ComponentModel;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// Object�ֶ�������
    /// </summary>
    public class ObjectFieldItemProperty:ItemProperty
    {
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public ObjectFieldItemProperty()
        {
            this.m_dataNodeItem = EnumDataNodeItems.ObjectFieldItem;
            this.m_itemName = "OBJECTID";
            this.m_itemAliasName = "OBJECTID";
        }

        /// <summary>
        /// ��ȡ����������
        /// </summary>
        [Category("����"), ReadOnly(true), Description("����"), DefaultValue("OBJECTID")]
        public override string ItemName
        {
            get
            {
                return base.ItemName;
            }
            set
            {
                base.ItemName = value;
            }
        }

        /// <summary>
        /// ��ȡ�����ñ���
        /// </summary>
        [Category("����"),ReadOnly(false),Description("����"),DefaultValue("OBJECTID")]
        public override string ItemAliasName
        {
            get
            {
                return base.ItemAliasName;
            }
            set
            {
                base.ItemAliasName = value;
            }
        }

        /// <summary>
        /// ��ȡ�ֶ�����
        /// </summary>
        [Category("ͨ��"), Description("�ֶ�����")]    
        public EnumFieldItems  FieldType
        {
            get
            {
                return EnumFieldItems.OID;
            }
        }
    }
}
