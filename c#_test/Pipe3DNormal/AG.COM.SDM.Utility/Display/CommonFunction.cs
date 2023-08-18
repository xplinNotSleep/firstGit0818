using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.Utility.Display
{
    /// <summary>
    /// 公共函数类
    /// </summary>
    public class CommonFunction
    {
        /// <summary>
        /// 绘制符号
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r">绘制范围</param>
        /// <param name="symbol">符号样式</param>
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
        /// 获取数目
        /// </summary>
        /// <param name="pDicEnmu"></param>
        /// <returns></returns>
        public static int GetEnumNum(IDictionaryEnumerator pDicEnmu)
        {
            int i = 0;
            while (pDicEnmu.MoveNext())
                i++;
            return i;
        }

        /// <summary>
        /// 获取字段值的数目
        /// </summary>
        /// <param name="pCursor">查询游标</param>
        /// <returns>字段值数目</returns>
        public static int GetValueNum(ICursor pCursor)
        {
            IRow pNextRow;
            int i;
            i = 0;
            pNextRow = pCursor.NextRow();
            //历遍所有要素
            while (pNextRow != null)
            {
                i++;
                pNextRow = pCursor.NextRow();
            }
            return i;
        }

        /// <summary>
        /// 获取字段别名
        /// </summary>
        /// <param name="objClass"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static string GetFieldAliasName(IObjectClass objClass, string fieldName)
        {
            int index = objClass.FindField(fieldName);
            if (index < 0)
                return "";
            return objClass.Fields.get_Field(index).AliasName;
        }

        /// <summary>
        /// 获取字段名称
        /// </summary>
        /// <param name="objClass">objClass 为IFeatureClass</param>
        /// <param name="aliasName"></param>
        /// <returns></returns>
        public static string GetFieldName(IObjectClass objClass, string aliasName)
        {
            int index = objClass.Fields.FindFieldByAliasName(aliasName);
            if (index < 0)
                return "";
            return objClass.Fields.get_Field(index).Name;
        }

        /// <summary>
        /// 获取字段的字符串值
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public static string GetFieldValueString(object fieldValue)
        {
            if (fieldValue == null)
                return "";
            else if (fieldValue is System.DBNull)
                return "";
            else
                return fieldValue.ToString();
        }

        /// <summary>
        /// 获取数值型字段
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static IList<IField> GetNumbleField(IFields fields)
        {
            IList<IField> list = new List<IField>();
            IField field;
            for (int i = 0; i <= fields.FieldCount - 1; i++)
            {
                field = fields.get_Field(i);
                if ((field.Type != esriFieldType.esriFieldTypeBlob) &&
                    (field.Type != esriFieldType.esriFieldTypeGeometry) &&
                    (field.Type != esriFieldType.esriFieldTypeRaster) &&
                    (field.Type != esriFieldType.esriFieldTypeXML) &&
                    (field.Type != esriFieldType.esriFieldTypeGlobalID) &&
                    (field.Type != esriFieldType.esriFieldTypeGUID) &&
                    (field.Type != esriFieldType.esriFieldTypeOID) &&
                    (field.Type != esriFieldType.esriFieldTypeDate) &&
                    (field.Type != esriFieldType.esriFieldTypeString))
                {
                    list.Add(field);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取文本型字段
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static IList<IField> GetStringField(IFields fields)
        {
            IList<IField> list = new List<IField>();
            IField field;
            for (int i = 0; i <= fields.FieldCount - 1; i++)
            {
                field = fields.get_Field(i);
                if (field.Type == esriFieldType.esriFieldTypeString)
                {
                    list.Add(field);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取日期型字段
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static IList<IField> GetDateField(IFields fields)
        {
            IList<IField> list = new List<IField>();
            IField field;
            for (int i = 0; i <= fields.FieldCount - 1; i++)
            {
                field = fields.get_Field(i);
                if (field.Type == esriFieldType.esriFieldTypeDate)
                {
                    list.Add(field);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取一个随机的Icolor（ESRI）
        /// </summary>
        /// <returns></returns>
        public static IColor GetRandomColor()
        {
            int intRed;
            int intGreen;
            int intBlue;
            Random pRandom;
            pRandom = new Random();
            intRed = pRandom.Next(0, 255);
            intGreen = pRandom.Next(0, 255);
            intBlue = pRandom.Next(0, 255);
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = intRed;
            pRgbColor.Green = intGreen;
            pRgbColor.Blue = intBlue;
            return pRgbColor as IColor;
        }

        /// <summary>
        /// 获取合法的字段列表
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static IList<IField> GetRenderValidField(IFields fields)
        {
            IList<IField> list = new List<IField>();
            IField field;
            for (int i = 0; i <= fields.FieldCount - 1; i++)
            {
                field = fields.get_Field(i);
                if ((field.Type != esriFieldType.esriFieldTypeBlob) &&
                    (field.Type != esriFieldType.esriFieldTypeGeometry) &&
                    (field.Type != esriFieldType.esriFieldTypeRaster) &&
                    (field.Type != esriFieldType.esriFieldTypeXML) &&
                    (field.Type != esriFieldType.esriFieldTypeGlobalID) &&
                    (field.Type != esriFieldType.esriFieldTypeGUID) &&
                    (field.Type != esriFieldType.esriFieldTypeOID))
                {
                    list.Add(field);
                }
            }

            return list;
        }

        /// <summary>
        /// 多个字段取得唯一值。返回一个hashtable,键为唯一值的value,值为对应的个数
        /// </summary>
        /// <param name="cursor"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static System.Collections.Hashtable GetUniqueValues(ITable pTable, System.Collections.Specialized.StringCollection fields)
        {
            System.Collections.Hashtable table = new System.Collections.Hashtable();
            ICursor cursor = pTable.Search(null, true);
            int TotalCount = pTable.RowCount(null);
            int indexValueField = cursor.Fields.FindField(fields[0]);
            int indexNormField = -1;
            double valueTotal = 0;
            double fieldValue;
            if (fields.Count == 2)
            {
                if (fields[1] != "百分比" && fields[1] != "求对数")
                {
                    indexNormField = cursor.Fields.FindField(fields[1]);

                }
                else if (fields[1] == "百分比")
                {
                    indexNormField = -1;
                    IDataStatistics pDataStatistics = new DataStatisticsClass();
                    pDataStatistics.Cursor = cursor;
                    pDataStatistics.Field = fields[0];
                    valueTotal = pDataStatistics.Statistics.Sum;
                }
                else if (fields[1] == "求对数")
                    indexNormField = -1;
            }
            else
            {
                indexNormField = -1;
            }
            cursor = pTable.Search(null, true);
            IRow row = cursor.NextRow();
            if (indexNormField != -1)
            {
                while (row != null)
                {
                    fieldValue = DataConvert.ObjToDou(row.get_Value(indexValueField))
                                / DataConvert.ObjToDou(row.get_Value(indexNormField));
                    if (table.Contains(fieldValue) == false)
                        table.Add(fieldValue, 1);
                    else
                    {
                        int c = (int)table[fieldValue];
                        table[fieldValue] = c + 1;  //个数加1
                    }
                    row = cursor.NextRow();
                }
            }
            else
            {
                if (fields.Count == 2)
                {
                    if (fields[1] == "百分比")
                    {
                        while (row != null)
                        {
                            if (valueTotal != 0)
                                fieldValue = DataConvert.ObjToDou(row.get_Value(indexValueField)) / valueTotal;
                            else
                                fieldValue = DataConvert.ObjToDou(row.get_Value(indexValueField));
                            if (table.Contains(fieldValue) == false)
                                table.Add(fieldValue, 1);
                            else
                            {
                                int c = (int)table[fieldValue];
                                table[fieldValue] = c + 1;  //个数加1
                            }
                            row = cursor.NextRow();
                        }
                    }
                    else if (fields[1] == "求对数")
                    {
                        while (row != null)
                        {
                            object obj = row.get_Value(indexValueField);
                            if (obj == null)
                                continue;
                            fieldValue = Math.Log10(DataConvert.ObjToDou(row.get_Value(indexValueField)));
                            if (table.Contains(fieldValue) == false)
                                table.Add(fieldValue, 1);
                            else
                            {
                                int c = (int)table[fieldValue];
                                table[fieldValue] = c + 1;  //个数加1
                            }
                            row = cursor.NextRow();
                        }
                    }
                }
                else
                {
                    while (row != null)
                    {
                        //为空的时候取为0，不为空的时候取原值
                        fieldValue = DataConvert.ObjToDou((Convert.ToString(row.get_Value(indexValueField)) == "" ? 0 : row.get_Value(indexValueField)));
                        if (table.Contains(fieldValue) == false)
                            table.Add(fieldValue, 1);
                        else
                        {
                            int c = (int)table[fieldValue];
                            table[fieldValue] = c + 1;  //个数加1
                        }
                        row = cursor.NextRow();
                    }
                }
            }
            return table;
        }

        /// <summary>
        /// 获取某一字段值的数目
        /// </summary>
        /// <param name="pTable"></param>
        /// <param name="strField"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static int GetValueNumble(ITable pTable, string strField, string strValue)
        {
            ICursor pCursor;
            IQueryFilter pQueryFilter;
            pQueryFilter = new QueryFilterClass();
            int i = 0;
            int fieldNumble = pTable.FindField(strField);
            IField pField = pTable.Fields.get_Field(fieldNumble);
            Boolean bIsString;
            bIsString = false;
            switch (pField.Type)
            {
                case esriFieldType.esriFieldTypeDouble:
                    break;
                case esriFieldType.esriFieldTypeInteger:
                    break;
                case esriFieldType.esriFieldTypeSingle:
                    break;
                case esriFieldType.esriFieldTypeSmallInteger:
                    break;
                default:
                    bIsString = true;
                    break;
            }
            if (bIsString)
                pQueryFilter.WhereClause = string.Format("{0} ='{1}'", strField, strValue);
            else
                pQueryFilter.WhereClause = string.Format("{0} ={1}", strField, strValue);
            pCursor = pTable.Search(pQueryFilter, true);
            i = GetValueNum(pCursor);

            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(pCursor);
            return i;
        }

        /// <summary>
        /// 获取ESRI的地图单位
        /// </summary>
        /// <param name="pMapUnits">地图单位名称</param>
        /// <returns></returns>
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
        /// 获取地图单位名称
        /// </summary>
        /// <param name="pMapUnits"></param>
        /// <returns></returns>
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
        /// <param name="pESRIColor"></param>
        /// <returns></returns>
        public static Color TransvertColor(IColor pESRIColor)
        {
            int intColor;
            int intRed;
            int intGreen;
            int intBlue;
            Color pColor = new Color();
            intColor = pESRIColor.RGB;
            intRed = intColor % 0x100;
            intGreen = intColor / 0x100 % 0x100;
            intBlue = intColor / 0x10000 % 0x100;
            pColor = Color.FromArgb(intRed, intGreen, intBlue);
            return pColor;
        }
    }
}
