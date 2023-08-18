using System.Windows.Forms;
using ESRI.ArcGIS.Carto;

namespace AG.COM.SDM.Utility.Controls
{
    public partial class MapLayersTreeView2 : TreeView
    {
        public MapLayersTreeView2()
        {
            InitializeComponent();

            this.CheckBoxes = true;
            this.ImageList = imageList1;
        }

        //��дCheck�¼�
        private bool InWhiligigCheck = false;//�ж��Ƿ���Ƕ���ڲ�����check�¼��������ѭ���ڲ�������Ӧ������
        protected override void OnAfterCheck(TreeViewEventArgs e)
        {
            if (e.Action==TreeViewAction.Unknown && InWhiligigCheck==true)
                return;
            
            InWhiligigCheck = true;
            if (e.Node.Nodes.Count == 0)
            {
                if (e.Node.Checked == true)
                    SetParentNodeChecked(e.Node);
                return;
            }
            else
            {
                if (e.Node.Parent != null)
                    SetParentNodeChecked(e.Node);
                SetChildrenChecked(e.Node);
            }
            InWhiligigCheck = false;
        }

        //����ֱϵ��������
        private void SetParentNodeChecked(TreeNode node)
        {
            if (node.Parent == null)
            {
                return;
            }
            else
            {
                node.Parent.Checked = true;
                SetParentNodeChecked(node.Parent);
            }
        }

        //����ֱϵ�ӽ����ӻ򲻿���
        private void SetChildrenChecked(TreeNode node)
        {
            if (node.Nodes.Count == 0)
            {
                return;
            }
            else
            {
                TreeNode pNode;
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    node.Nodes[i].Checked = node.Checked;
                    SetChildrenChecked(node.Nodes[i]);
                }
            }
        }

        /// <summary>
        /// �������ѡ��
        /// </summary>
        public void ClearAll()
        {
            for(int i=0;i<this.Nodes.Count;i++)
            {
                this.Nodes[i].Checked = false;
                SetChildrenChecked(this.Nodes[i]);
            }

        }
      
        /// <summary>
        /// ѡ������
        /// </summary>
        public void SelectAll()
        {
            for (int i = 0; i < this.Nodes.Count; i++)
            {
                this.Nodes[i].Checked = true;
                SetChildrenChecked(this.Nodes[i]);
            }
        }

        public void Init(IBasicMap map)
        {
            this.Nodes.Clear();

            TreeNode node;
            ILayer layer;
            for (int i=0;i<=map.LayerCount-1;i++)
            {
                layer = map.get_Layer(i);
                node = new TreeNode();
                if (layer is IFeatureLayer)
                {
                    node.Text = layer.Name;
                    node.Tag = layer;
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 1;
                    node.Checked = (layer as IFeatureLayer).Selectable;
                    this.Nodes.Add(node);
                    if ((node.Parent != null) && (node.Checked ==true))
                        SetParentNodeChecked(node);
                }
                else if (layer is IGroupLayer)
                {
                    ConstructGroupLayerNode(node, layer as ICompositeLayer);
                    node.Tag = layer;
                    node.Text = layer.Name;
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 0;
                    if (node.Nodes.Count > 0)
                    {
                        this.Nodes.Add(node);
                    }
                }        
            }
        }

        public void ConstructGroupLayerNode(TreeNode parentNode, ICompositeLayer groupLayer)
        {
            TreeNode node;
            ILayer layer;
            parentNode.Checked = true;
            for (int i = 0; i <= groupLayer.Count - 1; i++)
            {
                layer = groupLayer.get_Layer(i);
                node = new TreeNode();
                
                if (layer is IFeatureLayer)
                {
                    node.Text = layer.Name;
                    node.Tag = layer;
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 1;
                    node.Checked = (layer as IFeatureLayer).Selectable;
                    parentNode.Nodes.Add(node);
                }
                else if (layer is IGroupLayer)
                {
                    ConstructGroupLayerNode(node, layer as ICompositeLayer);
                    node.Tag = layer;
                    node.Text = layer.Name;
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 0;
                    if (node.Nodes.Count > 0)
                    {
                        parentNode.Nodes.Add(node);
                    }
                }        
            }
        }
    }
}
