using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 图层选择组合框控件
    /// </summary>
    public partial class LayerCombox : UserControl
    {
        private IMap m_Map;     
        private LayerFilterDelegate m_CustomFilter = null;
        private FeatureLayerFilterType m_Filter = FeatureLayerFilterType.lyFilterNone;
        
        public delegate bool LayerFilterDelegate(ILayer layer);

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public LayerCombox()
        {
            InitializeComponent();
        }

        private void LayerCombox_Load(object sender, EventArgs e)
        {
            this.comboBoxTreeView1.TreeView.ImageList = this.imageList1;
        }

        /// <summary>
        /// 设置绑定的地图对象，初始化控件的下拉图层列表
        /// </summary>
        public IMap Map
        {
            set
            {
                m_Map = value;
                ConstrucLayerTree(comboBoxTreeView1);
            }
        }

        /// <summary>
        /// 获取或设置图层过滤，即控件中只显示符合过滤条件的图层
        /// </summary>
        public FeatureLayerFilterType Filter
        {
            get { return m_Filter; }
            set { m_Filter = value; }
        }

        /// <summary>
        /// 获取或设置选择的Layer对象
        /// </summary>
        public ESRI.ArcGIS.Carto.ILayer Layer
        {
            get
            {
                return this.comboBoxTreeView1.Tag as ILayer;
            }
            set
            {
                this.comboBoxTreeView1.Tag = value;
                if (value != null)
                    this.comboBoxTreeView1.Text = (value as ILayer).Name;
                else
                    this.comboBoxTreeView1.Text = "";
            }
        }

        private void comboBoxTreeView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TreeNode node = this.comboBoxTreeView1.TreeView.SelectedNode;
            if (node == null)   return;

            if ((node.Tag is Utility.Wrapers.LayerWrapper) == false)
            {
                comboBoxTreeView1.Text = "";
                comboBoxTreeView1.TreeView.SelectedNode = null;
                return;
            }

            this.comboBoxTreeView1.Tag = (node.Tag as Utility.Wrapers.LayerWrapper).Layer; 
        }

        /// <summary>
        /// 构造树状图层
        /// </summary>
        /// <param name="combo">树形下拉框</param>
        private void ConstrucLayerTree(Utility.Controls.ComboBoxTreeView combo)
        {
            if (m_Map == null)  return;

            IMap map = m_Map;
            ILayer layer;
            TreeNode node;

            for (int i = 0; i <= map.LayerCount - 1; i++)
            {
                layer = map.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    node = new TreeNode(layer.Name);
                    AddGroupLayers(layer as IGroupLayer, node);
                    node.ImageIndex = 0;
                    if (node.Nodes.Count > 0)
                    {
                        combo.TreeView.Nodes.Add(node);
                    }
                }
                else if (layer is IFeatureLayer)
                {
                    if (LayerPass(layer as IFeatureLayer))
                    {
                        Utility.Wrapers.LayerWrapper wrap = new AG.COM.SDM.Utility.Wrapers.LayerWrapper(layer);
                        node = new TreeNode(wrap.ToString());
                        node.Tag = wrap;
                        node.ImageIndex = 1;
                        combo.TreeView.Nodes.Add(node);
                    } 
                }
            }

            //展开所有节点
            combo.TreeView.ExpandAll();
        }

        /// <summary>
        /// 添加组图层
        /// </summary>
        /// <param name="glayer">组图层</param>
        /// <param name="parentNode">树节点</param>
        private void AddGroupLayers(IGroupLayer glayer, TreeNode parentNode)
        {
            ICompositeLayer clayer = glayer as ICompositeLayer;
            ILayer layer;
            TreeNode node;

            for (int i = 0; i <= clayer.Count - 1; i++)
            {
                layer = clayer.get_Layer(i);
                if (layer is IGroupLayer)
                {
                    node = new TreeNode(layer.Name);
                    //递归调用添加组图层
                    AddGroupLayers(layer as IGroupLayer, node);
                    node.ImageIndex = 0;

                    if (node.Nodes.Count > 0)
                    {
                        parentNode.Nodes.Add(node);
                    }
                }
                if (layer is IFeatureLayer)
                {
                    if (LayerPass(layer as IFeatureLayer))
                    {
                        Utility.Wrapers.LayerWrapper wrap = new AG.COM.SDM.Utility.Wrapers.LayerWrapper(layer);
                        node = new TreeNode(wrap.ToString());
                        node.Tag = wrap;
                        node.ImageIndex = 1;
                        parentNode.Nodes.Add(node);
                    } 
                }
            }
        }

        /// <summary>
        /// 图层类型检查
        /// </summary>
        /// <param name="layer">图层</param>
        /// <returns>如果</returns>
        private bool LayerPass(IFeatureLayer layer)
        {
            if (m_CustomFilter != null)
            {
                return m_CustomFilter(layer);
            }
            if (m_Filter == FeatureLayerFilterType.lyFilterNone)   return true;

            if ((m_Filter & FeatureLayerFilterType.lyFilterAnno) == FeatureLayerFilterType.lyFilterAnno)
            {
                if (layer is IAnnotationLayer) return true;
            }

            esriGeometryType geoType = layer.FeatureClass.ShapeType;
            if ((geoType == esriGeometryType.esriGeometryMultipoint) || (geoType == esriGeometryType.esriGeometryPoint))
            {
                if ((m_Filter & FeatureLayerFilterType.lyFilterPoint) == FeatureLayerFilterType.lyFilterPoint) return true;
                  
            }
            else if (geoType == esriGeometryType.esriGeometryPolyline)
            {
                if ((m_Filter & FeatureLayerFilterType.lyFilterLine) == FeatureLayerFilterType.lyFilterLine) return true;                   
            }
            else if (geoType == esriGeometryType.esriGeometryPolygon)
            {
                if ((m_Filter & FeatureLayerFilterType.lyFilterArea) == FeatureLayerFilterType.lyFilterArea) return true;                  
            }

            return false;
        }
    }
}
