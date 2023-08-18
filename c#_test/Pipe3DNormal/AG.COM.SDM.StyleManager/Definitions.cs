using AG.COM.SDM.Utility;
using AG.COM.SDM.Utility.Controls;
using AG.COM.SDM.Utility.Display;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using stdole;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AG.COM.SDM.StyleManager
{
    /// <summary>
    /// 采用两种方式：stMarkerSymbol,stLineSymbol,stFillSymbol,stNorthArrows采用AE控件SymbologyControl
    /// stTextSymbol,stScaleBars因其符号较大，AE控件SymbologyControl没有提供如ARCMAP显示一列效果的接口
    /// 采用ListView 开发的符号控件,速度稍慢
    /// </summary>
    public enum SymbolType
    {
        stMarkerSymbol,
        stLineSymbol,
        stFillSymbol,
        stTextSymbol,
        stNorthArrows,
        stScaleBars,
        stLegendItems,
        stReferenceSystem
    }

    public interface IRendererControl
    {
        void SetGeoLayer(IGeoFeatureLayer layer);
        ESRI.ArcGIS.Carto.IFeatureRenderer Renderer { get;}
        string RendererName { get;}
        Control DisplayControl { get;}
        Image Icon { get;}
        Image DisplayImage { get;}
        bool CheckRenderer(IFeatureRenderer renderer);
    }

    public class StyleHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="combobox"></param>
        /// <param name="value"></param>
        public static void SetComboBoxValue(System.Windows.Forms.ComboBox combobox, string value)
        {
            int i;
            if (combobox.DropDownStyle == System.Windows.Forms.ComboBoxStyle.DropDownList)
            {
                for (i = 0; i <= combobox.Items.Count - 1; i++)
                {
                    if (combobox.Items[i].ToString() == value)
                    {
                        combobox.SelectedIndex = i;
                        return;
                    }
                }
                if (i >= combobox.Items.Count)
                    combobox.SelectedIndex = 0;
            }
            else
                combobox.Text = value;
        }

        /// <summary>
        /// 获取透明面状符号
        /// </summary>
        /// <returns></returns>
        public static ISymbol GetTransSymbol(SymbolType pSymbolType)
        {
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = 255;
            pRgbColor.Green = 255;
            pRgbColor.Blue = 255;
            pRgbColor.Transparency = 0;
            ISymbol pSymbol;
            ILineSymbol pLineSymbol;
            switch (pSymbolType)
            {
                case SymbolType.stFillSymbol:
                    pSymbol = new SimpleFillSymbolClass();
                    (pSymbol as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSSolid;
                    (pSymbol as ISimpleFillSymbol).Color = pRgbColor as IColor;
                    pLineSymbol = (pSymbol as ISimpleFillSymbol).Outline;
                    pLineSymbol.Color = pRgbColor as IColor;
                    pLineSymbol.Width = 0.4;
                    (pSymbol as ISimpleFillSymbol).Outline = pLineSymbol;
                    return pSymbol;
                case SymbolType.stLineSymbol:
                    pLineSymbol = new SimpleLineSymbolClass();
                    pLineSymbol.Color = pRgbColor as IColor;
                    pLineSymbol.Width = 0.4;
                    return pLineSymbol as ISymbol;
                default:
                    return null;
            }
            
        }
     
        /// <summary>
        /// 获取任意初始符号
        /// </summary>
        /// <param name="pGeoFeatureLayer"></param>
        /// <returns></returns>
        public static ISymbol GetInitialRandomSymbol(IGeoFeatureLayer pGeoFeatureLayer)
        {
            ISymbol pSymbol;
            switch (StyleHelper.GetSymbolType(pGeoFeatureLayer))
            {
                case SymbolType.stMarkerSymbol:
                    pSymbol = new SimpleMarkerSymbolClass();
                    (pSymbol as ISimpleMarkerSymbol).Size = 4;
                    (pSymbol as ISimpleMarkerSymbol).Angle = 0;
                    (pSymbol as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSCircle;
                    (pSymbol as ISimpleMarkerSymbol).Color = CommonFunction.GetRandomColor();
                    return pSymbol;
                case SymbolType.stLineSymbol:
                    pSymbol = new SimpleLineSymbolClass();
                    (pSymbol as ISimpleLineSymbol).Style = esriSimpleLineStyle.esriSLSSolid;
                    (pSymbol as ISimpleLineSymbol).Width = 1;
                    return pSymbol;
                case SymbolType.stFillSymbol:
                    pSymbol = new SimpleFillSymbolClass();
                    (pSymbol as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSSolid;
                    (pSymbol as ISimpleFillSymbol).Color = CommonFunction.GetRandomColor();
                    ILineSymbol pLineSymbol;
                    pLineSymbol = (pSymbol as ISimpleFillSymbol).Outline;
                    IRgbColor pRgbColor = new RgbColorClass();
                    pRgbColor.Red = 110;
                    pRgbColor.Green = 110;
                    pRgbColor.Blue = 110;
                    pLineSymbol.Color = pRgbColor as IColor;
                    pLineSymbol.Width = 0.4;
                    (pSymbol as ISimpleFillSymbol).Outline = pLineSymbol;
                    return pSymbol;
                case SymbolType.stTextSymbol:
                    ITextSymbol pTextSymbol = new TextSymbolClass();
                    pSymbol = pTextSymbol as ISymbol;
                    return pSymbol;
                default:
                    return null;
            }
        }
        
        /// <summary>
        /// 获取任意初始符号
        /// </summary>
        /// <param name="pSymbolType"></param>
        /// <returns></returns>
        public static ISymbol GetInitialRandomSymbol(SymbolType pSymbolType)
        {
            ISymbol pSymbol;
            IRgbColor pRgbColor;
            switch (pSymbolType)
            {
                case SymbolType.stMarkerSymbol:
                    pSymbol = new SimpleMarkerSymbolClass();
                    (pSymbol as ISimpleMarkerSymbol).Size = 4;
                    (pSymbol as ISimpleMarkerSymbol).Angle = 0;
                    (pSymbol as ISimpleMarkerSymbol).Style = esriSimpleMarkerStyle.esriSMSCircle;
                    (pSymbol as ISimpleMarkerSymbol).Color = CommonFunction.GetRandomColor();
                    return pSymbol;
                case SymbolType.stLineSymbol:
                    pSymbol = new SimpleLineSymbolClass();
                    (pSymbol as ISimpleLineSymbol).Style = esriSimpleLineStyle.esriSLSSolid;
                    (pSymbol as ISimpleLineSymbol).Width = 1;
                    return pSymbol;
                case SymbolType.stFillSymbol:
                    pSymbol = new SimpleFillSymbolClass();
                    (pSymbol as ISimpleFillSymbol).Style = esriSimpleFillStyle.esriSFSSolid;
                    (pSymbol as ISimpleFillSymbol).Color = CommonFunction.GetRandomColor();
                    ILineSymbol pLineSymbol;
                    pLineSymbol = (pSymbol as ISimpleFillSymbol).Outline;
                    pRgbColor = new RgbColorClass();
                    pRgbColor.Red = 110;
                    pRgbColor.Green = 110;
                    pRgbColor.Blue = 110;
                    pLineSymbol.Color = pRgbColor as IColor;
                    pLineSymbol.Width = 0.4;
                    (pSymbol as ISimpleFillSymbol).Outline = pLineSymbol;
                    return pSymbol;
                case SymbolType.stNorthArrows:
                    IMarkerNorthArrow pMarkerNorthArrow = new MarkerNorthArrowClass();
                    pSymbol = pMarkerNorthArrow.MarkerSymbol as ISymbol;
                    return pSymbol;
                case SymbolType.stTextSymbol:
                    ITextSymbol pTextSymbol = new TextSymbolClass();
                    IFontDisp pFontDisp =(IFontDisp)(new StdFontClass());
                    pFontDisp.Name = "宋体";
                    pFontDisp.Size = 8;
                    pFontDisp.Bold = false;
                    pFontDisp.Italic = false;
                    pFontDisp.Strikethrough = false;
                    pFontDisp.Underline = false;
                    pTextSymbol.Font = pFontDisp;
                    pRgbColor = new RgbColorClass();
                    pRgbColor.Red = 0;
                    pRgbColor.Green = 0;
                    pRgbColor.Blue = 0;
                    pTextSymbol.Color = pRgbColor as IColor;
                    pTextSymbol.Text = "AaBbYyZz";
                    pTextSymbol.HorizontalAlignment = esriTextHorizontalAlignment.esriTHALeft;
                    pSymbol = pTextSymbol as ISymbol;
                    return pSymbol;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 取制定Index的IColorRamp的颜色值
        /// </summary>
        /// <param name="pColorRamp"></param>
        /// <param name="Index"></param>
        /// <returns></returns>
        public static IColor GetColor(IColorRamp pColorRamp, int Index)
        {
            IColor pColor;
            pColor = null;
            IColor pPreColor;
            if (Index > pColorRamp.Size)
                return pColor;
            IEnumColors pEnumColor;
            pEnumColor = pColorRamp.Colors;
            pEnumColor.Reset();
            int i = 0;
            for (i = 0; i < pColorRamp.Size; i++)
            {
                pPreColor = pColor;
                pColor = pEnumColor.Next();
                if (pColor == null)
                {
                    pColor = pPreColor;
                    break;
                }
                if (i == Index)
                {
                    break;
                }
            }
            return pColor;
        }

        /// <summary>
        /// 初始化颜色Combox控件
        /// </summary>
        /// <param name="pStyleCombox"></param>
        public static void InitialColorCombox(StyleComboBox pStyleCombox)
        {
            pStyleCombox.Items.Clear();
            //颜色带文件
            String strStyleFile;
            strStyleFile = CommonConstString.STR_StylePath + @"\ESRI.ServerStyle";
            string strStyleGalleryClass;
            strStyleGalleryClass = "Color Ramps";
            IStyleGallery pStyleGallery = new ServerStyleGalleryClass();
            IStyleGalleryStorage pStyleGalleryStorage;
            pStyleGalleryStorage = pStyleGallery as IStyleGalleryStorage;
            pStyleGalleryStorage.AddFile(strStyleFile);

            //获取StyleGalleryClass
            IStyleGalleryClass pStyleGalleryClass;
            pStyleGalleryClass = null;
            for (int i = 0; i < pStyleGallery.ClassCount; i++)
            {
                pStyleGalleryClass = pStyleGallery.get_Class(i);
                if (pStyleGalleryClass.Name == strStyleGalleryClass)
                    break;
            }

            pStyleCombox.StyleGalleryClass = pStyleGalleryClass;

            //添加颜色带
            IEnumStyleGalleryItem pEnumStyleGalleryItem = new EnumServerStyleGalleryItemClass();
            pEnumStyleGalleryItem = pStyleGallery.get_Items(strStyleGalleryClass, strStyleFile, "");
            pEnumStyleGalleryItem.Reset();
            IStyleGalleryItem pEnumItem;
            pEnumItem = pEnumStyleGalleryItem.Next();
            while (pEnumItem != null)
            {
                pStyleCombox.Items.Add(pEnumItem);
                pEnumItem = pEnumStyleGalleryItem.Next();
            }
            
            //释放非托管变量
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pEnumStyleGalleryItem);
        }

        /// <summary>
        /// 获取图层渲染所使用的色带Index
        /// </summary>
        /// <param name="pStyleComboBox"></param>
        /// <param name="pUniqueValueRenderer"></param>
        /// <returns></returns>
        public static int GetColorItemIndex(StyleComboBox pStyleComboBox, IUniqueValueRenderer pUniqueValueRenderer)
        {
            string strItemName = pUniqueValueRenderer.ColorScheme;
            IStyleGalleryItem pStyleGalleryItem;
            int i;
            i = 0;
            for (i = 0; i < pStyleComboBox.Items.Count; i++)
            {
                pStyleGalleryItem = pStyleComboBox.Items[i] as IStyleGalleryItem;
                if (pStyleGalleryItem.Name == strItemName)
                { break; }
            }
            if (i >= pStyleComboBox.Items.Count)
                i = -1;
            return i;
        }

        /// <summary>
        /// 获取图层符号的类型
        /// </summary>
        /// <param name="pGeoFeatureLayer"></param>
        /// <returns></returns>
        public static SymbolType GetSymbolType(IGeoFeatureLayer pGeoFeatureLayer)
        {
            IFeatureClass pFeatureClass;
            pFeatureClass = pGeoFeatureLayer.FeatureClass;
            SymbolType pSymbolType = new SymbolType();
            //判断形状
            switch (pFeatureClass.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:                     //点类型的实体

                case esriGeometryType.esriGeometryMultipoint:                //多点类型的实体
                    pSymbolType = SymbolType.stMarkerSymbol;
                    break;
                case esriGeometryType.esriGeometryLine:                      //线段类型的实体

                case esriGeometryType.esriGeometryCircularArc:               //圆弧段类型的实体

                case esriGeometryType.esriGeometryEllipticArc:               //椭圆弧段类型的实体

                case esriGeometryType.esriGeometryBezier3Curve:              //贝赛尔曲线

                case esriGeometryType.esriGeometryPath:                      //路径

                case esriGeometryType.esriGeometryPolyline:                  //线类型的实体
                    pSymbolType = SymbolType.stLineSymbol;
                    break;
                case esriGeometryType.esriGeometryRing:                      //环类型的实体

                case esriGeometryType.esriGeometryEnvelope:                  //矩形类型的实体

                case esriGeometryType.esriGeometryMultiPatch:                //多路径类型的实体

                case esriGeometryType.esriGeometrySphere:                    //球类型的实体

                case esriGeometryType.esriGeometryTriangles:                 //三角形类型的实体

                case esriGeometryType.esriGeometryTriangleFan:

                case esriGeometryType.esriGeometryTriangleStrip:

                case esriGeometryType.esriGeometryPolygon:                   //面类型的实体
                    pSymbolType = SymbolType.stFillSymbol;
                    break;
                default:
                    break;
            }
            return pSymbolType;
        }

        /// <summary>
        /// 获取字段的最大值和最小值
        /// </summary>
        /// <param name="pStyleListView"></param>
        /// <param name="pGeoFeatureLayer"></param>
        /// <returns></returns>
        public static double[] GetMaxAndMinValue(StyleListView pStyleListView, IGeoFeatureLayer pGeoFeatureLayer)
        {
            double[] Value = new double[2];
            string strField = "";
            double MaxValue;
            double MinValue;

            ITable pTable = pGeoFeatureLayer as ITable;
            ICursor pCursor = pTable.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Cursor = pCursor;
            strField =CommonFunction.GetFieldName(pGeoFeatureLayer.FeatureClass,pStyleListView.Items[0].SubItems[1].Text);
            pDataStatistics.Field = strField;
            IStatisticsResults pStatisticsResults = pDataStatistics.Statistics;
            MaxValue = pStatisticsResults.Maximum;
            MinValue = pStatisticsResults.Minimum;

            for (int i = 1; i < pStyleListView.Items.Count; i++)
            {
                pCursor = pTable.Search(null, true);
                pDataStatistics = new DataStatisticsClass();
                pDataStatistics.Cursor = pCursor;
                strField = CommonFunction.GetFieldName(pGeoFeatureLayer.FeatureClass, pStyleListView.Items[i].SubItems[1].Text);
                pDataStatistics.Field = strField;
                pStatisticsResults = pDataStatistics.Statistics;
                if (MaxValue <= pStatisticsResults.Maximum)
                    MaxValue = pStatisticsResults.Maximum;
                if (MinValue >= pStatisticsResults.Minimum)
                    MinValue = pStatisticsResults.Minimum;
            }
            Value[0] = MaxValue;
            Value[1] = MinValue;
            return Value;
        }

        /// <summary>
        /// 判断字段值是否为空（条件：Sum和StandardDeviation都为空）
        /// </summary>
        /// <param name="pGeoLayer"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public static bool IsVoidField(IGeoFeatureLayer pGeoLayer,string FieldName)
        {
            bool bIsVoidField = false;
            ITable pTable = pGeoLayer as ITable;
            ICursor pCursor = pTable.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Cursor = pCursor;
            pDataStatistics.Field = FieldName;
            IStatisticsResults pStatisticsResults = pDataStatistics.Statistics;
            //所有字段值为空(设为1便于处理)
            if (pStatisticsResults.Sum == 0 && pStatisticsResults.StandardDeviation == 0)
            { bIsVoidField = true; }
            return bIsVoidField;
        }
    }
}
