using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// GeoTable相关操作处理类
    /// </summary>
    public class GeoTableHandler
    {
        /// <summary>
        /// 得到要素类某字段的唯一值
        /// </summary>
        /// <param name="pFeatureClass">要素类</param>
        /// <param name="strField">指定要得到唯一值的字段</param>
        /// <returns>唯一值字符数据</returns>
        public static IList<string> GetUniqueValueByDataStat(IFeatureClass pFeatureClass, string strField)
        {
            //得到IFeatureCursor游标
            ICursor pCursor = pFeatureClass.Search(null, false) as ICursor;

            //coClass对象实例生成
            IDataStatistics pData = new DataStatisticsClass();
            pData.Field = strField;
            pData.Cursor = pCursor;

            //枚举唯一值
            IEnumerator pEnumVar = pData.UniqueValues;

            //记录总数
            int RecordCount = pData.UniqueValueCount;
            //字符数组
            IList<string> uvList = new List<string>();
            pEnumVar.Reset();
            int i = 0;//限定添加５０个
            while (pEnumVar.MoveNext())
           {
                uvList.Add(pEnumVar.Current.ToString());
                if ((++i) == 50)  break;
            }

            //释放内存
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pCursor);
            return uvList;
        }

        /// <summary>
        /// 得到要素类某字段的唯一值，当要素数量超出设置的最大值，不统计且抛出异常
        /// </summary>
        /// <param name="pFeatureClass">要素类</param>
        /// <param name="strField">指定要得到唯一值的字段</param>       
        /// <param name="limitFeatureCount">要素最大值，超出不统计</param>
        /// <param name="hasStat">是否统计</param>
        /// <returns>唯一值字符数据</returns>
        public static IList<string> GetUniqueValueByDataStatLimitFeatureCount(IFeatureClass pFeatureClass, string strField, int limitFeatureCount,out bool hasStat)
        {
            int count = pFeatureClass.FeatureCount(null);
            if (count > limitFeatureCount)
            {
                hasStat = false;
                return new List<string>();
            }
            else
            {
                hasStat = true;
                return GetUniqueValueByDataStat(pFeatureClass, strField);   
            }          
        }

        /// <summary>
        /// 得到要素类某字段的唯一值
        /// </summary>
        /// <param name="pFeatureClass">要素类</param>
        /// <param name="strField">指定要得到唯一值的字段(别名)</param>
        /// <returns>唯一值字符数据</returns>
        public static IList<string> GetUniqueValueByDef(IFeatureClass pFeatureClass, string strField)
        {
            IList<string> uvList = new List<string>();
            IDataset tDataset = pFeatureClass as IDataset;           
            IFeatureWorkspace tFeatWorkspace = tDataset.Workspace as IFeatureWorkspace;

            int fieldIndex = pFeatureClass.Fields.FindField(strField);

            if (fieldIndex >= 0)
            {
                string strFieldx = pFeatureClass.Fields.get_Field(fieldIndex).Name;

                //创建查询定义
                IQueryDef tQueryDef = tFeatWorkspace.CreateQueryDef();
                tQueryDef.Tables = tDataset.BrowseName;
                tQueryDef.SubFields = "Distinct (" + strFieldx + ")";
                //得到查询游标
                ICursor tCursor = tQueryDef.Evaluate();
                for (IRow tRow = tCursor.NextRow(); tRow != null; tRow = tCursor.NextRow())
                {
                    uvList.Add(tRow.get_Value(0).ToString());
                }

                //释放内存
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tCursor);
            }

            return uvList;
        } 

        /// <summary>
        /// 从指定工作空间中打开指定名称的属性表
        /// </summary>
        /// <param name="pTableName">表名称</param>
        /// <param name="pFeatWorkspace">工作空间</param>
        /// <returns>返回属性表</returns>
        public static ITable OpenTable(string pTableName,IFeatureWorkspace pFeatWorkspace)
        {
            ITable tTable = null;
            //判断要素类是否已经存在
            bool IsExist = (pFeatWorkspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTTable, pTableName);
            if (IsExist == true)
            {
                //获取指定名称的属性表
                tTable = pFeatWorkspace.OpenTable(pTableName);             
            }

            return tTable;
        }

        /// <summary>
        /// 获取指定字段值的记录数目
        /// </summary>
        /// <param name="pTable">属性表</param>
        /// <param name="strField">字段名</param>
        /// <param name="strValue">字段值</param>
        /// <returns>返回记录数目</returns>
        public static int GetRowCount(ITable pTable, string strField, string strValue)
        {
            int fieldNumble = pTable.FindField(strField);
            if (fieldNumble == -1) return -1;

            IField pField = pTable.Fields.get_Field(fieldNumble);
            Boolean bIsString = false;

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

            IQueryFilter pQueryFilter = new QueryFilterClass();
            if (bIsString)
                pQueryFilter.WhereClause = string.Format("{0} ='{1}'", strField, strValue);
            else
                pQueryFilter.WhereClause = string.Format("{0} ={1}", strField, strValue);

            int i = pTable.RowCount(pQueryFilter);
            return i;
        }

        /// <summary>
        /// 多个字段取得唯一值。返回一个hashtable,键为唯一值的value,值为对应的个数
        /// </summary>
        /// <param name="pTable">属性表</param>
        /// <param name="fields">字段集合</param>
        /// <returns></returns>
        public static Hashtable GetUniqueValues(ITable pTable, StringCollection fields)
        {
            Hashtable tHashTable = new Hashtable();

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

            cursor = pTable.Search(null, true);
            IRow row = cursor.NextRow();
            if (indexNormField != -1)
            {
                while (row != null)
                {
                    fieldValue = System.Convert.ToDouble(row.get_Value(indexValueField))
                                / System.Convert.ToDouble(row.get_Value(indexNormField));
                    if (tHashTable.Contains(fieldValue) == false)
                        tHashTable.Add(fieldValue, 1);
                    else
                    {
                        int c = (int)tHashTable[fieldValue];
                        tHashTable[fieldValue] = c + 1;  //个数加1
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
                                fieldValue = System.Convert.ToDouble(row.get_Value(indexValueField)) / valueTotal;
                            else
                                fieldValue = System.Convert.ToDouble(row.get_Value(indexValueField));
                            if (tHashTable.Contains(fieldValue) == false)
                                tHashTable.Add(fieldValue, 1);
                            else
                            {
                                int c = (int)tHashTable[fieldValue];
                                tHashTable[fieldValue] = c + 1;  //个数加1
                            }
                            row = cursor.NextRow();
                        }
                    }
                    else if (fields[1] == "求对数")
                    {
                        while (row != null)
                        {
                            fieldValue = Math.Log10(System.Convert.ToDouble(row.get_Value(indexValueField)));
                            if (tHashTable.Contains(fieldValue) == false)
                                tHashTable.Add(fieldValue, 1);
                            else
                            {
                                int c = (int)tHashTable[fieldValue];
                                tHashTable[fieldValue] = c + 1;  //个数加1
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
                        fieldValue = System.Convert.ToDouble((row.get_Value(indexValueField).ToString() == "" ? 0 : row.get_Value(indexValueField)));
                        if (tHashTable.Contains(fieldValue) == false)
                            tHashTable.Add(fieldValue, 1);
                        else
                        {
                            int c = (int)tHashTable[fieldValue];
                            tHashTable[fieldValue] = c + 1;  //个数加1
                        }
                        row = cursor.NextRow();
                    }
                }
            }

            //释放游标资源
            System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor );

            return tHashTable;
        }

        /// <summary>
        /// 根据字段名返回字段别名
        /// </summary>
        /// <param name="objClass">objClass 为IFeatureClass</param>
        /// <param name="fieldName">字段名称</param>
        /// <returns>返回别名，如果不存在则返回空</returns>
        public static string GetFieldAliasName(IObjectClass objClass, string fieldName)
        {
            int index = objClass.FindField(fieldName);
            if (index < 0) return "";

            return objClass.Fields.get_Field(index).AliasName;
        }

        /// <summary>
        /// 通过别名查找字段名称
        /// </summary>
        /// <param name="objClass">objClass 为IFeatureClass</param>
        /// <param name="aliasName">字段别名</param>
        /// <returns>返回字段名称,如果不存在则返回空</returns>
        public static string GetFieldName(IObjectClass objClass, string aliasName)
        {
            int index = objClass.Fields.FindFieldByAliasName(aliasName);
            if (index < 0) return "";

            return objClass.Fields.get_Field(index).Name;
        }

        /// <summary>
        /// 以字符串的形式返回字段值
        /// </summary>
        /// <param name="fieldValue">字段值</param>
        /// <returns>返回字段值的字符串形式</returns>
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
        /// <param name="fields">字段集</param>
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
        /// <param name="fields">字段集</param>
        /// <returns>返回文本型字段集</returns>
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

    }
}
