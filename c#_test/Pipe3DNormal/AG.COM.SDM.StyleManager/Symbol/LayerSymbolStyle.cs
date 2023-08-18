using AG.COM.SDM.Framework;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using System.Drawing;
using System.Windows.Forms;

namespace AG.COM.SDM.StyleManager
{
    /// <summary>
    /// 图层样式设置插件类
    /// </summary>
    public class LayerSymbolStyle : BaseCommand
    {
        private IHookHelperEx m_HookHelperEx = new HookHelperEx();
       
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public LayerSymbolStyle()
        {
            try
            {
                this.m_bitmap = new Bitmap(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "SymbolSelector.bmp"));
            }
            catch
            {
                this.m_bitmap = null;
            }
            finally
            {
                this.m_name = "LayerSymbolStyle";
                this.m_caption = "图层样式设置";
                this.m_message = "图层样式设置";
                this.m_toolTip = "图层样式设置";
            }
        }

        /// <summary>
        /// 单击事件处理方法
        /// </summary>
        public override void OnClick()
        {
            IFeatureLayer pFeatureLayer = m_HookHelperEx.MapService.CurrentLayer as IFeatureLayer;
            IGeoFeatureLayer pGeoFeatureLayer = pFeatureLayer as IGeoFeatureLayer;
            frmSymbolSelector frmSymbolSelectorNew = new frmSymbolSelector();
            IFeatureRenderer pFeatureRenderer =  pGeoFeatureLayer.Renderer;
            ISimpleRenderer pSimpleRenderer = new SimpleRendererClass();
            if (pFeatureRenderer is ISimpleRenderer)
            {
                pSimpleRenderer = pGeoFeatureLayer.Renderer as ISimpleRenderer;
                frmSymbolSelectorNew.InitialSymbol = pSimpleRenderer.Symbol;
            }
            else
            {
                frmSymbolSelectorNew.InitialSymbol = StyleHelper.GetInitialRandomSymbol(pGeoFeatureLayer);
            }
            frmSymbolSelectorNew.SymbolType = StyleHelper.GetSymbolType(pGeoFeatureLayer);
            frmSymbolSelectorNew.ShowInTaskbar = false;
            if (frmSymbolSelectorNew.ShowDialog() == DialogResult.OK)
            {
                pSimpleRenderer.Symbol = frmSymbolSelectorNew.SelectedSymbol as ISymbol;
                pGeoFeatureLayer.Renderer = pSimpleRenderer as IFeatureRenderer; ;
                m_HookHelperEx.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
        }

        /// <summary>
        /// 获取对象的可用状态
        /// </summary>
        public override bool Enabled
        {
            get
            {
                if (m_HookHelperEx.FocusMap.LayerCount == 0)
                    return false;
                if (m_HookHelperEx.MapService.CurrentLayer == null)
                    return false;
                if ((m_HookHelperEx.MapService.CurrentLayer is IFeatureLayer) == false)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// 创建对象处理方法
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            m_HookHelperEx.Hook = hook;
        }
    }
}
