using System;
using System.Drawing;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.Utility.Display
{
    /// <summary>
    /// 显示处理帮助类
    /// </summary>
    public class DisplayHelper
    {
        /// <summary>
        /// 画符号样式
        /// </summary>
        /// <param name="g">GDI+绘图对象</param>
        /// <param name="r">矩形位置</param>
        /// <param name="symbol">样式符号</param>
        public static void DrawSymbol(Graphics g, Rectangle r, ISymbol symbol)
        {
            //获取绘图范围
            int w = r.Width;
            int h = r.Height;
            IEnvelope pEnvelope = new EnvelopeClass();
            pEnvelope.PutCoords(0, 0, w, h);

            //声明坐标转换
            tagRECT DeviceRect;
            DeviceRect.left = 0;
            DeviceRect.right = w;
            DeviceRect.top = 0;
            DeviceRect.bottom = h;

            IDisplayTransformation pDisplayTransformation = new DisplayTransformationClass();
            pDisplayTransformation.VisibleBounds = pEnvelope;
            pDisplayTransformation.Bounds = pEnvelope;
            pDisplayTransformation.set_DeviceFrame(ref DeviceRect);
            pDisplayTransformation.Resolution = g.DpiX;

            int hdc = (int)g.GetHdc();             
            symbol.SetupDC(hdc, pDisplayTransformation);
            symbol.ROP2 = esriRasterOpCode.esriROPCopyPen;
            IGeometry pGeometry = null;

            if (symbol is IFillSymbol)        //面符号
            {
                IEnvelope env = pEnvelope;
                env.XMin = 6;
                env.XMax = r.Width - 6;
                env.YMin = 6;
                env.YMax = r.Height - 6;
                pGeometry = pEnvelope as IGeometry;
            }
            else if (symbol is ILineSymbol)       //线符号
            {
                IPoint pFromPoint = new PointClass();
                IPoint pToPoint = new PointClass();
                pFromPoint.X = pEnvelope.XMin + 6;
                pFromPoint.Y = pEnvelope.YMax / 2;
                pToPoint.X = pEnvelope.XMax - 6;
                pToPoint.Y = pEnvelope.YMax / 2;
                IPolyline pPolyline = new PolylineClass();
                pPolyline.FromPoint = pFromPoint;
                pPolyline.ToPoint = pToPoint;
                pGeometry = pPolyline as IGeometry;
            }
            else if (symbol is ITextSymbol)
            {
                double xSize;
                double ySize;
                ITextSymbol pTextSymbol = symbol as ITextSymbol;       
                //TextSymbol.Text有可能为空，这里设默认值
                if (string.IsNullOrEmpty(pTextSymbol.Text))
                {
                    pTextSymbol.Text = "AaBbYyZz";
                }
                pTextSymbol.GetTextSize(hdc, pDisplayTransformation as ITransformation, pTextSymbol.Text, out xSize, out ySize);
                IPoint pFromPoint = new PointClass();
                IPoint pToPoint = new PointClass();
                IPolyline pPolyline = new PolylineClass();
                pFromPoint.X = (w- xSize - 35) /2;
                pFromPoint.Y = pEnvelope.YMax / 2;
                pToPoint.X = pEnvelope.XMax - 6;
                pToPoint.Y = pEnvelope.YMax / 2;
                pPolyline.FromPoint = pFromPoint;
                pPolyline.ToPoint = pToPoint;
                pGeometry = pPolyline as IGeometry;
            }
            else                                                    //点
            {
                IArea pArea = pEnvelope as IArea;
                IPoint pt;
                pt = pArea.Centroid;
                pGeometry = pt as IGeometry;
            }
            symbol.Draw(pGeometry);
            symbol.ResetDC();
            g.ReleaseHdc();
        }

        /// <summary>
        /// 从GDI位图的句柄处创建位图对象
        /// </summary>
        /// <param name="gdi32">GDI位图句柄值</param>
        /// <returns>返回位图对象</returns>
        public static Image GetImageFromHbitmap(int gdi32)
        {
            if (gdi32 == 0) return null;

            IntPtr pIntPtr = new IntPtr(gdi32);
            //从句柄创建Bitmap
            Bitmap pBitmap = System.Drawing.Bitmap.FromHbitmap(pIntPtr);
            //使背景色透明
            pBitmap.MakeTransparent();

            return pBitmap;
        }
                
        /// <summary>
        /// 获取一个随机的Icolor（ESRI）
        /// </summary>
        /// <returns></returns>
        public static IColor GetRandomColor()
        {         
            Random pRandom = new Random();
            int intRed = pRandom.Next(0, 255);
            int intGreen = pRandom.Next(0, 255);
            int intBlue = pRandom.Next(0, 255);

            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = intRed;
            pRgbColor.Green = intGreen;
            pRgbColor.Blue = intBlue;

            return pRgbColor as IColor;
        }      

        /// <summary>
        /// 根据指定的地图单位返回枚举值
        /// </summary>
        /// <param name="pMapUnits">地图单位</param>
        /// <returns>返回esriUnits枚举值</returns>
        public static esriUnits MapUnits(string pMapUnits)
        {
            esriUnits MapUnits = esriUnits.esriUnknownUnits;
            switch (pMapUnits)
            {
                case "厘米":                //"Centimeters"
                    MapUnits = esriUnits.esriCentimeters;
                    break;
                case "度":                  //"Decimal Degrees"
                    MapUnits = esriUnits.esriDecimalDegrees;
                    break;
                case "分米":                //"Decimeters"
                    MapUnits = esriUnits.esriDecimeters;
                    break;
                case "英尺":                //"Feet"
                    MapUnits = esriUnits.esriFeet;
                    break;
                case "英寸":                //"Inches"
                    MapUnits = esriUnits.esriInches;
                    break;
                case "千米":                //"Kilometers"
                    MapUnits = esriUnits.esriKilometers;
                    break;
                case "米":                  //"Meters"
                    MapUnits = esriUnits.esriMeters;
                    break;
                case "英里":                //"Miles"
                    MapUnits = esriUnits.esriMiles;
                    break;
                case "毫米":                //"Millimeters"
                    MapUnits = esriUnits.esriMillimeters;
                    break;
                case "海里":                //"Nautical Miles"
                    MapUnits = esriUnits.esriNauticalMiles;
                    break;
                case "像素":                //"Points"
                    MapUnits = esriUnits.esriPoints;
                    break;
                case "未知单位":                //"Unknown Units"
                    MapUnits = esriUnits.esriUnknownUnits;
                    break;
                case "码":                  //"Yards"
                    MapUnits = esriUnits.esriYards;
                    break;
                case "间隔":
                    MapUnits = esriUnits.esriUnitsLast;
                    break;
                default:
                    break;
            }
            return MapUnits;
        }

        /// <summary>
        /// 根据地图单位枚举变量返回中文单位值
        /// </summary>
        /// <param name="pMapUnits">地图单位枚举变量</param>
        /// <returns>返回中文单位值</returns>
        public static string MapUnitsName(esriUnits pMapUnits)
        {
            string sMapUnits = "";
            switch (pMapUnits)
            {
                case esriUnits.esriCentimeters:
                    sMapUnits = "厘米";
                    break;
                case esriUnits.esriDecimalDegrees:
                    sMapUnits = "度";
                    break;
                case esriUnits.esriDecimeters:
                    sMapUnits = "分米";
                    break;
                case esriUnits.esriFeet:
                    sMapUnits = "英尺";
                    break;
                case esriUnits.esriInches:
                    sMapUnits = "英寸";
                    break;
                case esriUnits.esriKilometers:
                    sMapUnits = "千米";
                    break;
                case esriUnits.esriMeters:
                    sMapUnits = "米";
                    break;
                case esriUnits.esriMiles:
                    sMapUnits = "英里";
                    break;
                case esriUnits.esriMillimeters:
                    sMapUnits = "毫米";
                    break;
                case esriUnits.esriNauticalMiles:
                    sMapUnits = "海里";
                    break;
                case esriUnits.esriPoints:
                    sMapUnits = "像素";
                    break;
                case esriUnits.esriUnknownUnits:
                    sMapUnits = "未知单位";
                    break;
                case esriUnits.esriYards:
                    sMapUnits = "码";
                    break;
                case esriUnits.esriUnitsLast:
                    sMapUnits = "间隔";
                    break;
                default:
                    break;
            }
            return sMapUnits;
        }

        /// <summary>
        /// 将ESRI的Color转化为System的Color
        /// </summary>
        /// <param name="pESRIColor">ESRI的Color值</param>
        /// <returns>返回System的Color</returns>
        public static Color TransvertColor(IColor pESRIColor)
        {        
            int intColor = pESRIColor.RGB;
            int intRed = intColor % 0x100;
            int intGreen = intColor / 0x100 % 0x100;
            int intBlue = intColor / 0x10000 % 0x100;

            Color pColor = Color.FromArgb(intRed, intGreen, intBlue);

            return pColor;
        }

        /// <summary>
        /// 取得当前鼠标在控件上的位置
        /// </summary>
        /// <param name="hwnd">句柄值</param>
        /// <returns>返回鼠标在控件上的位置</returns>
        public static System.Drawing.Point GetMousePosInControl(int hwnd)
        {
            Win32APIs.POINTAPI pt = new AG.COM.SDM.Utility.Win32APIs.POINTAPI();
            Win32APIs.RECT r = new AG.COM.SDM.Utility.Win32APIs.RECT();
            Win32APIs.WinAPI.GetCursorPos(ref pt);
            Win32APIs.WinAPI.GetWindowRect(hwnd, ref r);

            System.Drawing.Point pt2 = new System.Drawing.Point();
            pt2.X = pt.x - r.Left;
            pt2.Y = pt.y - r.Top;

            return pt2;
        }

        /// <summary>
        /// 像素单位转换为地图单位值
        /// </summary>
        /// <param name="pActiveView">地图视图</param>
        /// <param name="pixelUnits">像素单位</param>
        /// <returns>真实的距离值</returns>
        public static double PixelsToMapUnits(IActiveView pActiveView, double pixelUnits)
        {
            tagRECT tTagRect = pActiveView.ScreenDisplay.DisplayTransformation.get_DeviceFrame();
            int width = tTagRect.right - tTagRect.left;

            if (width < 0.00001) return pixelUnits;    
            double scale = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.Width / width;

            return (pixelUnits * scale);
        }  
    }
}
