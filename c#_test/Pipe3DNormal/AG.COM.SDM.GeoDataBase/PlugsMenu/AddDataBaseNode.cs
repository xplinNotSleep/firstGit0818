using System.Windows.Forms;
using AG.COM.SDM.GeoDataBase.DBuilder;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// ������ݿ�ڵ� �����Ĳ˵���
    /// </summary>
    internal sealed class AddDataBaseNode:BaseCommand
    {
        private TreeView m_DataView;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public AddDataBaseNode()
        {
            this.m_caption = "������ݿ�";
            this.m_toolTip = "������ݿ�ڵ�";
            this.m_name = "AddDataBaseNode";
        }
        /// <summary>
        /// ��ȡ����Ŀ���״̬
        /// </summary>
        public override bool Enabled
        {
            get
            {
                return (m_DataView == null) ? false : true;
            }
        }

        /// <summary>
        /// ���������¼�
        /// </summary>
        public override void OnClick()
        {
            //ʵ�����ڵ�������
            ItemProperty DBNodeItem = new ItemProperty();
            DBNodeItem.ItemName = "New DataBase";
            DBNodeItem.ItemAliasName = "�����ݿ�";
            DBNodeItem.DataNodeItem = EnumDataNodeItems.DataBaseItem;
            DBNodeItem.ItemPropertyValueChanged += new ItemPropertyValueChangedEventHandler(ItemPropertyValueChanged);

            //ʵ�������ڵ�
            TreeNode DBTreeNode = new TreeNode();       
            DBTreeNode.Text = string.Format("{0}:{1}", DBInfoHandler.GetDataNodeDescripble(DBNodeItem.DataNodeItem), DBNodeItem.ItemAliasName);
            DBTreeNode.Tag = DBNodeItem;

            this.m_DataView.Nodes.Add(DBTreeNode);            
        }

        /// <summary>
        /// ������ʼ��
        /// </summary>
        /// <param name="hook">��ǰ����������</param>
        public override void OnCreate(object hook)
        {
            if (hook == null) return;
            this.m_DataView = hook as TreeView;            
        }

        /// <summary>
        /// ItemProperty�����ô˷�����֪ͨDBTreeControl����������ֵ�ѷ����ı�
        /// </summary>
        /// <param name="sender">����</param>
        /// <param name="e">ItemPropertyEventArgs�¼�����</param>
        private void ItemPropertyValueChanged(object sender, ItemPropertyEventArgs e)
        {
            ItemProperty itemProperty = e.ItemProperty;            
            TreeNode selTreeNode = this.m_DataView.SelectedNode;           
            selTreeNode.ToolTipText = string.Format("{0}:{1}", DBInfoHandler.GetDataNodeDescripble(itemProperty.DataNodeItem), itemProperty.ItemAliasName);
        }  
    }
}
