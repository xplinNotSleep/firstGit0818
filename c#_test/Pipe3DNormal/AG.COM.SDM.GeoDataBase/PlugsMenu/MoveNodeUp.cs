using System.Windows.Forms; 
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 节点上移 上下文菜单项
    /// </summary>
    internal sealed class MoveNodeUp :BaseCommand
    {
        private TreeNode m_SelTreeNode;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MoveNodeUp()
        {
            this.m_caption = "上移";
            this.m_toolTip = "上移";
            this.m_name = "MoveNodeUp";
        }

        /// <summary>
        /// 获取对象的可用状态
        /// </summary>
        public override bool Enabled
        {
            get
            {
                return (m_SelTreeNode==null)?false:true;
            }
        }

        /// <summary>
        /// 单击处理事件
        /// </summary>
        public override void OnClick()
        {
            //节点下移
            TreeNodeUpDown(true);
        }

        /// <summary>
        /// 创建初始化
        /// </summary>
        /// <param name="hook">当前选择节点</param>
        public override void OnCreate(object hook)
        {
            if (hook == null) return;
            this.m_SelTreeNode = hook as TreeNode;
        }

        /// <summary>
        /// 节点上移与下移
        /// </summary>
        /// <param name="isup">如果上移则为true,否则为false</param>
        private void TreeNodeUpDown(bool isup)
        {
            //***************************************************
            //设计思想：整个思想是按上移方法来解决。
            //如果是下移的情况，则考虑将当前节点的下一节点上移；
            //***************************************************  
            TreeNode pTreeNode = this.m_SelTreeNode;                    //当前选择节点 
            TreeNode pParentNode = pTreeNode.Parent;                    //父节点

            if (isup == false) pTreeNode = pTreeNode.NextNode;          //当前节点的下一节点  
            if (pTreeNode == null) return;                              //选择节点为空时返回

            int index = 0;
            index = pTreeNode.Index - 1;                                //向上移动时,选择索引应为当前选择节点索引减一

            if (index < 0) return;                                      //当前节点已为第一个子节点，不能上移。

            TreeView tCurrentTreeView = pTreeNode.TreeView;
            if (pParentNode == null)
            {
                //父节点为空的情况             
                tCurrentTreeView.BeginUpdate();
                tCurrentTreeView.Nodes.Insert(index, pTreeNode.Clone() as TreeNode);
                pTreeNode.Remove();
                tCurrentTreeView.EndUpdate();

                tCurrentTreeView.SelectedNode = pTreeNode.TreeView.Nodes[index];
            }
            else
            {
                //父节点不为空的情况          
                tCurrentTreeView.BeginUpdate();
                pParentNode.Nodes.Insert(index, pTreeNode.Clone() as TreeNode);
                pTreeNode.Remove();
                tCurrentTreeView.EndUpdate();
                if (isup == false)
                    tCurrentTreeView.SelectedNode = pParentNode.Nodes[index + 1];
                else
                    tCurrentTreeView.SelectedNode = pParentNode.Nodes[index];
            }
        }
    }
}
