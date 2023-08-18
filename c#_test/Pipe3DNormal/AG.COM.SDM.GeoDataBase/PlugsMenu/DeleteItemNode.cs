using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// �Ƴ���ǰ�ڵ�����
    /// </summary>
    internal sealed class DeleteItemNode:BaseCommand
    {
        private TreeNode m_SelTreeNode;

        public DeleteItemNode()
        {
            this.m_caption = "ɾ��";
            this.m_toolTip = "ɾ��";
            this.m_name = "DeleteItemNode";
        }
        public override bool Enabled
        {
            get
            {
                return (m_SelTreeNode == null) ? false : true;
            }
        }

        public override void OnClick()
        {
            //�����Ƴ����ѡ��ڵ�
            TreeNode tAfterSelNode = null;
            if (m_SelTreeNode.PrevNode != null)
            {
                tAfterSelNode = m_SelTreeNode.PrevNode;
            }
            else if (m_SelTreeNode.NextNode != null)
            {
                tAfterSelNode = m_SelTreeNode.NextNode;
            }
            else if (m_SelTreeNode.Parent != null)
            {
                tAfterSelNode = m_SelTreeNode.Parent;
            }

            if (tAfterSelNode != null)
            {
                this.m_SelTreeNode.TreeView.SelectedNode = tAfterSelNode;     
            }

            //�Ƴ���ǰѡ��ڵ�    
            m_SelTreeNode.Remove();
        }

        public override void OnCreate(object hook)
        {
            if (hook == null) return;
            m_SelTreeNode = hook as TreeNode;      
        }
    }
}
