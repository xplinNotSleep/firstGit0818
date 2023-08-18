using ESRI.ArcGIS.Carto;

namespace AG.COM.SDM.Utility.Wrapers
{
    /// <summary>
    /// 图层包装类
    /// </summary>
    public class LayerWrapper
    {
        private ILayer m_Layer = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="layer">图层</param>
        public LayerWrapper(ILayer layer)
        {
            m_Layer = layer;
        }

        /// <summary>
        /// 获取图层
        /// </summary>
        public ILayer Layer
        {
            get { return m_Layer; }
        }

        /// <summary>
        /// 重载ToString()方法
        /// </summary>
        /// <returns>返回图层名称</returns>
        public override string ToString()
        {
            return m_Layer.Name;
        }
    }
}
