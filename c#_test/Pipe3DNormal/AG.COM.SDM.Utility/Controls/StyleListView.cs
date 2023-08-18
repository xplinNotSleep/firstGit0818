using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Utility.Controls
{
    public delegate void OnCellDblClickDelegate(ListViewItem item,ListViewItem.ListViewSubItem subItem);
    
    public struct POINTAPI
    {
        public int x;
        public int y;
    }
    
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
    
    public partial class StyleListView : ListView
    {
        [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
        public static extern int GetCursorPos(
            ref POINTAPI lpPoint
        );

        [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
        public static extern int GetWindowRect(
            int hwnd,
            ref RECT lpRect
        );

        public StyleListView()
        {
            this.OwnerDraw = true;
            this.FullRowSelect = true;
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
            base.OnDrawColumnHeader(e);
        }

        public void AddSymbolItem(ISymbol pSymbol, string[] subItems)
        {
            ListViewItem item = new ListViewItem();
            for (int i = 0; i <= subItems.Length - 1; i++)
            {
                item.SubItems.Add(subItems[i]);
            }
            item.Tag = pSymbol;
            Items.Add(item);
        }

        //方法Bmp: 
        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {

            Rectangle r = e.Bounds;
            if (this.Columns.Count > 0)
                r.Width = this.Columns[0].Width;
            r.Inflate(-3, -2);

            ISymbol pSymbol = e.Item.Tag as ISymbol;
            if (pSymbol == null)
                return;

            if (e.ItemIndex == -1)
                return;
            if (e.ItemIndex >= 0)
            {
                Bitmap bmp;
                if (r.Width <= 0||r.Height <=0)
                    return;
                bmp = CreateBmp(pSymbol, r.Width, r.Height);
                e.Graphics.DrawImage(Image.FromHbitmap(bmp.GetHbitmap()), r);
            }

            //用于选择状况的设置
            if (e.Item.Selected)
            {
                r.Width = e.Bounds.Width;
                r.Inflate(1, 1);
                e.Graphics.DrawRectangle(Pens.LightGray, r);
            }
        }

        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            e.DrawText();
        }

        private Bitmap CreateBmp(ISymbol pSymbol, int pWidth, int pHeight)
        {
            Bitmap Bmp = new Bitmap(pWidth, pHeight);
            Graphics GImage = Graphics.FromImage(Bmp);
            GImage.Clear(Color.White);

            Double Dpi = GImage.DpiX;

            IGeometry pGeo;
            IEnvelope pEnvelope = new EnvelopeClass();
            pEnvelope.PutCoords(0, 0, (double)Bmp.Width, (double)Bmp.Height);
            if (pSymbol is IMarkerSymbol)
            {
                IArea pArea;
                pArea = pEnvelope as IArea;
                pGeo = pArea.Centroid as IGeometry;
                
            }
            else if (pSymbol is ILineSymbol)
            {
                IPoint pPoint;
                pPoint = new ESRI.ArcGIS.Geometry.Point();
                IPolyline pPolyline = new PolylineClass();
                pPoint.X = pEnvelope.XMin;
                pPoint.Y = (pEnvelope.YMin + pEnvelope.YMax) / 2;
                pPolyline.FromPoint = pPoint;
                pPoint = new ESRI.ArcGIS.Geometry.Point();
                pPoint.X = pEnvelope.XMax;
                pPoint.Y = (pEnvelope.YMin + pEnvelope.YMax) / 2;
                pPolyline.ToPoint = pPoint;
                pGeo = pPolyline as IGeometry;
            }
            else
            {
                pGeo = pEnvelope as IGeometry;
            }
             

            tagRECT DeviceRect;
            DeviceRect.left = 0;
            DeviceRect.right = Bmp.Width;
            DeviceRect.top = 0;
            DeviceRect.bottom = Bmp.Height;

            //设置绘图设备转换
            IDisplayTransformation pDisplayTransformation = new DisplayTransformationClass();
            pDisplayTransformation.VisibleBounds = pEnvelope;
            pDisplayTransformation.Bounds = pEnvelope;
            pDisplayTransformation.set_DeviceFrame(ref DeviceRect);
            pDisplayTransformation.Resolution = Dpi;

            System.IntPtr hdc = new IntPtr();
            hdc = GImage.GetHdc();

            pSymbol.SetupDC((int)hdc, pDisplayTransformation);
            pSymbol.Draw(pGeo);
            pSymbol.ResetDC();
            GImage.ReleaseHdc(hdc);
            GImage.Dispose();
            return Bmp;
        }

        public event OnCellDblClickDelegate OnCellDblClick = null;
        protected override void OnDoubleClick(EventArgs e)
        {
            if (OnCellDblClick != null)
            {
                RECT r = new RECT();
                POINTAPI pt = new POINTAPI();
                GetWindowRect((int)this.Handle, ref r);
                GetCursorPos(ref pt);
                int x = pt.x - r.Left;
                int y = pt.y - r.Top;

                ListViewHitTestInfo info = this.HitTest(x, y);
                if (info.Item != null)
                {
                    OnCellDblClick(info.Item, info.SubItem);
                }
            }
        }
    }
}
