using ESRI.ArcGIS.Carto;

namespace AG.COM.SDM.Utility.Wrapers
{
    /// <summary>
    /// ͼ���װ��
    /// </summary>
    public class LayerWrapper
    {
        private ILayer m_Layer = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        /// <param name="layer">ͼ��</param>
        public LayerWrapper(ILayer layer)
        {
            m_Layer = layer;
        }

        /// <summary>
        /// ��ȡͼ��
        /// </summary>
        public ILayer Layer
        {
            get { return m_Layer; }
        }

        /// <summary>
        /// ����ToString()����
        /// </summary>
        /// <returns>����ͼ������</returns>
        public override string ToString()
        {
            return m_Layer.Name;
        }
    }
}
