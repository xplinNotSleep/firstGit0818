using System;
using System.Drawing;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Utility.Controls
{
    public partial class StyleComboBox : ComboBox,IMessageFilter
    {
        public StyleComboBox()
        {
            InitializeComponent();
            this.DrawMode = DrawMode.OwnerDrawVariable;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            Application.AddMessageFilter(this);
        }

        #region IMessageFilter 成员

        /// <summary>
        /// 处理掉鼠标滚轮事件
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool PreFilterMessage(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x020A)
            { return true; }
            return false;
        }

        #endregion

        private IStyleGalleryClass m_StyleGalleryClass;
        public IStyleGalleryClass StyleGalleryClass
        {
            get { return m_StyleGalleryClass; }
            set { m_StyleGalleryClass = value; }
        }

        //Bmp方法：成功
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (m_StyleGalleryClass == null)
                return;
            if (e.State == DrawItemState.Selected) return;
            e.DrawBackground();
            Rectangle r = e.Bounds;
            r.Inflate(-1, -1);
            if (e.Index == -1)
                return;
            if (e.Index >= 0)
            {
                if (this.Items[e.Index] is ESRI.ArcGIS.Display.IStyleGalleryItem)
                {
                    Bitmap bmp = new Bitmap(r.Width, r.Height);
                    IStyleGalleryItem item = this.Items[e.Index] as IStyleGalleryItem;
                    bmp = this.StyleGalleryItemToBmp(r.Width, r.Height, this.m_StyleGalleryClass, item);
                    e.Graphics.DrawImage(Image.FromHbitmap(bmp.GetHbitmap()), r);
                }

                if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
                {
                    e.Graphics.DrawRectangle(Pens.Blue, r);
                }
            }

        }

        //转化生成图片
        private System.Drawing.Bitmap StyleGalleryItemToBmp(
            int iWidth,
            int iHeight,
            ESRI.ArcGIS.Display.IStyleGalleryClass mStyleGlyCs,
            ESRI.ArcGIS.Display.IStyleGalleryItem mStyleGlyItem)
        {
            Bitmap bmp = new Bitmap(iWidth, iHeight);
            Graphics gImage = Graphics.FromImage(bmp);

            tagRECT rect = new tagRECT();
            rect.right = bmp.Width;
            rect.bottom = bmp.Height;
            //生成预览
            System.IntPtr hdc = new IntPtr();
            hdc = gImage.GetHdc();
            mStyleGlyCs.Preview(mStyleGlyItem.Item, hdc.ToInt32(), ref rect);
            gImage.ReleaseHdc(hdc);
            gImage.Dispose();

            return bmp;
        }

        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {          
            e.ItemHeight = 20;
        }

    }
}
