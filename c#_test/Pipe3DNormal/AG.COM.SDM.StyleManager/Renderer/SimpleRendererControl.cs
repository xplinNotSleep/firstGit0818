using System;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;

namespace AG.COM.SDM.StyleManager.Renderer
{
    public partial class SimpleRendererControl : UserControl, IRendererControl
    {
        /// <summary>
        /// 获取图例标注描述
        /// </summary>
        public string RendererDescription
        {
            get { return tbxDescription.Text.Trim(); }
        }

        /// <summary>
        /// 获取图例标注
        /// </summary>
        public string RendererLabel
        {
            get { return tbxLegendLabel.Text.Trim(); }
        }
        
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SimpleRendererControl()
        {
            InitializeComponent();
            btnAdvance.Enabled = false;
        }

        private void sbtSymbol_Click(object sender, EventArgs e)
        {
            //鼠标等待
            this.Cursor = Cursors.WaitCursor;
            frmSymbolSelector frmSymbolSelectorNew = new frmSymbolSelector();
            frmSymbolSelectorNew.InitialSymbol = (m_Renderer as ISimpleRenderer).Symbol;
            frmSymbolSelectorNew.SymbolType = StyleHelper.GetSymbolType(m_GeoLayer);
            if (frmSymbolSelectorNew.ShowDialog() == DialogResult.OK)
            {
                sbtSymbol.Symbol = frmSymbolSelectorNew.SelectedSymbol as ISymbol;
                m_Renderer.Symbol = sbtSymbol.Symbol;
                this.sbtSymbol.Refresh();
            }
            this.Cursor = Cursors.Default;
        }

        #region IRendererControl 成员

        public bool CheckRenderer(IFeatureRenderer renderer)
        {
            if (renderer is ISimpleRenderer)
                return true;
            else
                return false;
        }

        public Image Icon
        {
            get { return pictureBox2.Image; }
        }

        public Image DisplayImage
        {
            get { return pictureBox1.Image; }
        }

        public string RendererName
        {
            get
            {
                return "单一符号";
            }
        }

        public Control DisplayControl
        {
            get
            {
                return this;
            }
        }

        private ISimpleRenderer m_Renderer = null;
        public IFeatureRenderer Renderer
        {
            get
            {
                //返回设置的Renderer对象
                if (m_Renderer == null)
                    m_Renderer = new SimpleRendererClass();

                return m_Renderer as IFeatureRenderer;
            }
        }

        private IGeoFeatureLayer m_GeoLayer;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="layer"></param>
        public void SetGeoLayer(IGeoFeatureLayer layer)
        {
            m_GeoLayer = layer;
            if (StyleHelper.GetSymbolType(m_GeoLayer) == SymbolType.stMarkerSymbol)
            {
                btnAdvance.Enabled = true;
            }
            if (layer.Renderer is ISimpleRenderer)
            {
                m_Renderer = layer.Renderer as ISimpleRenderer;
                sbtSymbol.Symbol = m_Renderer.Symbol;
                tbxDescription.Text = m_Renderer.Description;
                tbxLegendLabel.Text = m_Renderer.Label;
            }
            else
            {
                if (m_Renderer == null)
                {
                    m_Renderer = new SimpleRendererClass();
                    //如果是第一次显示，并且图层渲染类型不是"Single symbol"，则赋予sbtSymbol一个默认符号
                    ISymbol pSymbol;
                    pSymbol = StyleHelper.GetInitialRandomSymbol(m_GeoLayer);
                    this.sbtSymbol.Symbol = pSymbol;
                    m_Renderer.Symbol = pSymbol;
                    this.sbtSymbol.Refresh();
                }
            }
        }

        #endregion

        private void btnAdvance_Click(object sender, EventArgs e)
        {
            if (StyleHelper.GetSymbolType(m_GeoLayer) == SymbolType.stMarkerSymbol)
            {
                FormRotationSet trs = new FormRotationSet(m_GeoLayer);
                trs.RotationRenderer = m_Renderer as IRotationRenderer;

                if (trs.ShowDialog() == DialogResult.OK)
                {
                    m_Renderer = trs.RotationRenderer as ISimpleRenderer;
                }
            }
        }
    }
}
