using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 移除当前节点插件类
    /// </summary>
    internal sealed class DeleteItemNode:BaseCommand
    {
        private TreeNode m_SelTreeNode;

        public DeleteItemNode()
        {
            this.m_caption = "删除";
            this.m_toolTip = "删除";
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
            //设置移除后的选择节点
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

            //移除当前选择节点    
            m_SelTreeNode.Remove();
        }

        public override void OnCreate(object hook)
        {
            if (hook == null) return;
            m_SelTreeNode = hook as TreeNode;      
        }
    }
}
