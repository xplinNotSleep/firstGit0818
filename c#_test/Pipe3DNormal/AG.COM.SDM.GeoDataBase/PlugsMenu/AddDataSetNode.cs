using System.Windows.Forms;
using AG.COM.SDM.GeoDataBase.DBuilder;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// ������ݼ����ڵ� �����Ĳ˵���
    /// </summary>
    internal sealed class AddDataSetNode:BaseCommand
    {
        private TreeNode m_SelTreeNode;         //��ǰѡ��ڵ�
        private TreeNode m_SelChildTreeNode;    //��ǰѡ��ڵ�Ҫ��ӵ��ӽڵ�

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public AddDataSetNode()
        {
            this.m_caption = "������ݼ�";
            this.m_toolTip = "������ݼ�";
            this.m_name = "AddDataSetNode";
        }

        /// <summary>
        /// ��ȡ����Ŀ���״̬
        /// </summary>
        public override bool Enabled
        {
            get
            {
                return (m_SelTreeNode == null) ? false : true;
            }
        }

        /// <summary>
        /// ���������¼�
        /// </summary>
        public override void OnClick()
        {
            //ʵ�����ڵ�������
            ItemProperty DBNodeItem = new ItemProperty();
            DBNodeItem.ItemName = "New Dataset";
            DBNodeItem.ItemAliasName = "�����ݼ�";
            DBNodeItem.DataNodeItem = EnumDataNodeItems.DataSetItem;
            DBNodeItem.ItemPropertyValueChanged += new ItemPropertyValueChangedEventHandler(ItemPropertyValueChanged);

            //ʵ�������ڵ�
            m_SelChildTreeNode = new TreeNode();
            
            m_SelChildTreeNode.Text = string.Format("{0}:{1}", DBInfoHandler.GetDataNodeDescripble(DBNodeItem.DataNodeItem), DBNodeItem.ItemAliasName);           
            m_SelChildTreeNode.Tag = DBNodeItem;
            m_SelChildTreeNode.ImageIndex = 2;
            m_SelChildTreeNode.SelectedImageIndex = 2;       
            
            //��ӵ�ǰ�ڵ�
            this.m_SelTreeNode.Nodes.Add(m_SelChildTreeNode);
            this.m_SelTreeNode.Expand();
            
        }
        /// <summary>
        /// ������ʼ��
        /// </summary>
        /// <param name="hook">��ǰѡ�����ڵ�</param>
        public override void OnCreate(object hook)
        {
            if (hook == null) return;
            this.m_SelTreeNode = hook as TreeNode;
        }

        /// <summary>
        /// ItemProperty�����ô˷�����֪ͨDBTreeControl����������ֵ�ѷ����ı�
        /// </summary>
        /// <param name="sender">����</param>
        /// <param name="e">ItemPropertyEventArgs�¼�����</param>
        private void ItemPropertyValueChanged(object sender, ItemPropertyEventArgs e)
        {          
            ItemProperty itemProperty = e.ItemProperty;      
            m_SelChildTreeNode.Text = string.Format("{0}:{1}", DBInfoHandler.GetDataNodeDescripble(itemProperty.DataNodeItem), itemProperty.ItemAliasName);
        }
    }
}
