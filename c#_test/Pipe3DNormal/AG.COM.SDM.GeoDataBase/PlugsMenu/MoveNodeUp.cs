using System.Windows.Forms; 
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// �ڵ����� �����Ĳ˵���
    /// </summary>
    internal sealed class MoveNodeUp :BaseCommand
    {
        private TreeNode m_SelTreeNode;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public MoveNodeUp()
        {
            this.m_caption = "����";
            this.m_toolTip = "����";
            this.m_name = "MoveNodeUp";
        }

        /// <summary>
        /// ��ȡ����Ŀ���״̬
        /// </summary>
        public override bool Enabled
        {
            get
            {
                return (m_SelTreeNode==null)?false:true;
            }
        }

        /// <summary>
        /// ���������¼�
        /// </summary>
        public override void OnClick()
        {
            //�ڵ�����
            TreeNodeUpDown(true);
        }

        /// <summary>
        /// ������ʼ��
        /// </summary>
        /// <param name="hook">��ǰѡ��ڵ�</param>
        public override void OnCreate(object hook)
        {
            if (hook == null) return;
            this.m_SelTreeNode = hook as TreeNode;
        }

        /// <summary>
        /// �ڵ�����������
        /// </summary>
        /// <param name="isup">���������Ϊtrue,����Ϊfalse</param>
        private void TreeNodeUpDown(bool isup)
        {
            //***************************************************
            //���˼�룺����˼���ǰ����Ʒ����������
            //��������Ƶ���������ǽ���ǰ�ڵ����һ�ڵ����ƣ�
            //***************************************************  
            TreeNode pTreeNode = this.m_SelTreeNode;                    //��ǰѡ��ڵ� 
            TreeNode pParentNode = pTreeNode.Parent;                    //���ڵ�

            if (isup == false) pTreeNode = pTreeNode.NextNode;          //��ǰ�ڵ����һ�ڵ�  
            if (pTreeNode == null) return;                              //ѡ��ڵ�Ϊ��ʱ����

            int index = 0;
            index = pTreeNode.Index - 1;                                //�����ƶ�ʱ,ѡ������ӦΪ��ǰѡ��ڵ�������һ

            if (index < 0) return;                                      //��ǰ�ڵ���Ϊ��һ���ӽڵ㣬�������ơ�

            TreeView tCurrentTreeView = pTreeNode.TreeView;
            if (pParentNode == null)
            {
                //���ڵ�Ϊ�յ����             
                tCurrentTreeView.BeginUpdate();
                tCurrentTreeView.Nodes.Insert(index, pTreeNode.Clone() as TreeNode);
                pTreeNode.Remove();
                tCurrentTreeView.EndUpdate();

                tCurrentTreeView.SelectedNode = pTreeNode.TreeView.Nodes[index];
            }
            else
            {
                //���ڵ㲻Ϊ�յ����          
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
