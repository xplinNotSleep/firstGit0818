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

        //重写Check事件
        private bool InWhiligigCheck = false;//判断是否在嵌套内部引发check事件，如果是循环内部引发的应该跳过
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

        //设置直系父结点可视
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

        //设置直系子结点可视或不可视
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
        /// 清除所有选择
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
        /// 选择所有
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
