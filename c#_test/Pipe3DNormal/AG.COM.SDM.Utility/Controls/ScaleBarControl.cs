using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Utility.Controls
{
    /// <summary>
    /// 比例尺控件
    /// </summary>
    public partial class ScaleBarControl : Control
    {
        public ScaleBarControl()
        {
            InitializeComponent();            
           
            this.BackColor = System.Drawing.Color.White;
        }

        private bool m_IsVisible = true;
        /// <summary>
        /// 获取或设置对象的可见性
        /// </summary>
        public bool IsVisible
        {
            get { return m_IsVisible; }
            set { m_IsVisible = value; }
        }

        /// <summary>
        /// 重绘自定义控件
        /// </summary>
        /// <param name="sender">源对象</param>
        /// <param name="e">事件参数</param>
        private void ScaleBarControl_Paint(object sender, PaintEventArgs e)
        {
            if (m_IsVisible == false)
                return;

            if (m_StyleGalleryClass != null || m_Item != null)
                this.PreviewStyleGalleryItem(m_StyleGalleryClass, m_Item, e);
        }

        private IStyleGalleryClass m_StyleGalleryClass;
        /// <summary>
        /// 设置样式类型
        /// </summary>
        public IStyleGalleryClass StyleGalleryClass
        {
            set { m_StyleGalleryClass = value; }
        }

        private object m_Item;
        /// <summary>
        /// 获取或设置显示比例对象
        /// </summary>
        public object Item
        {
            get { return m_Item; }
            set { m_Item = value; }
        }

        /// <summary>
        /// 预览符号
        /// </summary>
        /// <param name="pStyleGalleryClass"></param>
        /// <param name="pItem"></param>
        /// <param name="e"></param>
        private void PreviewStyleGalleryItem(IStyleGalleryClass pStyleGalleryClass, object pItem, PaintEventArgs e)
        {
            //声明坐标转换
            tagRECT DeviceRect;
            DeviceRect.left = 0;
            DeviceRect.right = 300;
            DeviceRect.top = 0;
            DeviceRect.bottom = 40;
            pStyleGalleryClass.Preview(pItem, (int)e.Graphics.GetHdc(), ref DeviceRect);

            //释放设置上下文句柄
            e.Graphics.ReleaseHdc();
        }  
    }
}
