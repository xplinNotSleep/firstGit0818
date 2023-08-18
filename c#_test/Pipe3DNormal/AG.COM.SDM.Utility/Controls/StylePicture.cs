using System.Windows.Forms;
using AG.COM.SDM.Utility.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Utility.Controls
{
    public partial class StylePicture : PictureBox
    {
        public StylePicture()
        {
            InitializeComponent();
        }

        private ISymbol m_Symbol = null; 
        /// <summary>
        /// 获取或设置样式
        /// </summary>
        public ISymbol Symbol
        {
            get
            {
                return m_Symbol;
            }
            set 
            { 
                m_Symbol = value; 
            }
        }

        private IStyleGalleryClass m_StyleGalleryClass;
        /// <summary>
        /// 设置样式类
        /// </summary>
        public IStyleGalleryClass StyleGalleryClass
        {
            set 
            { 
                m_StyleGalleryClass = value; 
            }
        }

        private object m_Item;
        public object Item
        {
            get { return m_Item; }
            set { m_Item = value; }
        }

        //重绘自定义控件
        private void StylePicture_Paint(object sender, PaintEventArgs e)
        {
            if (m_Item!=null && m_Item is IScaleBar)
            {
                if (m_StyleGalleryClass != null || m_Item != null)
                    this.PreviewStyleGalleryItem(m_StyleGalleryClass, m_Item, e);
            }
            else
            {
                if (m_Symbol == null)   return;
                DisplayHelper.DrawSymbol(e.Graphics, this.Bounds, m_Symbol);
            }
        }

        //预览符号
        private void PreviewStyleGalleryItem(IStyleGalleryClass pStyleGalleryClass, object pItem, PaintEventArgs e)
        {
            //声明坐标转换
            tagRECT DeviceRect;
            DeviceRect.left = 0;
            DeviceRect.right = 250;
            DeviceRect.top = 0;
            DeviceRect.bottom = 80;
            pStyleGalleryClass.Preview(pItem, (int)e.Graphics.GetHdc(), ref DeviceRect);
            e.Graphics.ReleaseHdc();
        }       
    }
}
