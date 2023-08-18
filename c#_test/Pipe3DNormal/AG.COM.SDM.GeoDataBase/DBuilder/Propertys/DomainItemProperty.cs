using System.ComponentModel;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// ��ڵ�������
    /// </summary>
    public class DomainItemProperty:ItemProperty
    {
        private IDomain m_Domain ;

        /// <summary>
        /// ʵ���������Զ���
        /// </summary>
        public DomainItemProperty()
        {
            this.m_itemName = "New Domain";
            this.m_itemAliasName = "�½���";
            this.m_dataNodeItem = EnumDataNodeItems.DomainItem;

            m_Domain = new RangeDomainClass();
            //��������������
            m_Domain.Name = this.m_itemName;
            //��������������
            m_Domain.Description = this.m_itemAliasName;
            //�����ֶ�����
            m_Domain.FieldType = esriFieldType.esriFieldTypeSmallInteger;
        }

        /// <summary>
        /// ��ȡ����������
        /// </summary>
        [Category("����"), Description("����")]
        public override string ItemName
        {
            get
            {
                return base.ItemName;
            }
            set
            {
                base.ItemName = value;
                this.m_Domain.Name = value;
            }
        }

        /// <summary>
        /// ��ȡ������������Ϣ
        /// </summary>
        [Category("����"), Description("������Ϣ")]
        public override string ItemAliasName
        {
            get
            {
                return base.ItemAliasName;
            }
            set
            {
                base.ItemAliasName = value;
                this.m_Domain.Description = value;
            }
        }

        /// <summary>
        /// ��ȡ�����������Զ���
        /// </summary>
        [Category("��������"), Description("������"), TypeConverter(typeof(DomainConverter)),
EditorAttribute(typeof(DomainEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public IDomain Domain
        {
            get
            {
                return this.m_Domain;
            }
            set
            {
                this.m_Domain = value;
            }
        }

        /// <summary>
        /// ����ToString����
        /// </summary>
        /// <returns>����������������</returns>
        [Browsable(false)]        
        public override string ToString()
        {
            return this.ItemAliasName;
        }
    }
}
