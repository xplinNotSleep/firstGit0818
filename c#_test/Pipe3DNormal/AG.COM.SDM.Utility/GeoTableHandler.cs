using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// GeoTable��ز���������
    /// </summary>
    public class GeoTableHandler
    {
        /// <summary>
        /// �õ�Ҫ����ĳ�ֶε�Ψһֵ
        /// </summary>
        /// <param name="pFeatureClass">Ҫ����</param>
        /// <param name="strField">ָ��Ҫ�õ�Ψһֵ���ֶ�</param>
        /// <returns>Ψһֵ�ַ�����</returns>
        public static IList<string> GetUniqueValueByDataStat(IFeatureClass pFeatureClass, string strField)
        {
            //�õ�IFeatureCursor�α�
            ICursor pCursor = pFeatureClass.Search(null, false) as ICursor;

            //coClass����ʵ������
            IDataStatistics pData = new DataStatisticsClass();
            pData.Field = strField;
            pData.Cursor = pCursor;

            //ö��Ψһֵ
            IEnumerator pEnumVar = pData.UniqueValues;

            //��¼����
            int RecordCount = pData.UniqueValueCount;
            //�ַ�����
            IList<string> uvList = new List<string>();
            pEnumVar.Reset();
            int i = 0;//�޶���ӣ�����
            while (pEnumVar.MoveNext())
           {
                uvList.Add(pEnumVar.Current.ToString());
                if ((++i) == 50)  break;
            }

            //�ͷ��ڴ�
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pCursor);
            return uvList;
        }

        /// <summary>
        /// �õ�Ҫ����ĳ�ֶε�Ψһֵ����Ҫ�������������õ����ֵ����ͳ�����׳��쳣
        /// </summary>
        /// <param name="pFeatureClass">Ҫ����</param>
        /// <param name="strField">ָ��Ҫ�õ�Ψһֵ���ֶ�</param>       
        /// <param name="limitFeatureCount">Ҫ�����ֵ��������ͳ��</param>
        /// <param name="hasStat">�Ƿ�ͳ��</param>
        /// <returns>Ψһֵ�ַ�����</returns>
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
        /// �õ�Ҫ����ĳ�ֶε�Ψһֵ
        /// </summary>
        /// <param name="pFeatureClass">Ҫ����</param>
        /// <param name="strField">ָ��Ҫ�õ�Ψһֵ���ֶ�(����)</param>
        /// <returns>Ψһֵ�ַ�����</returns>
        public static IList<string> GetUniqueValueByDef(IFeatureClass pFeatureClass, string strField)
        {
            IList<string> uvList = new List<string>();
            IDataset tDataset = pFeatureClass as IDataset;           
            IFeatureWorkspace tFeatWorkspace = tDataset.Workspace as IFeatureWorkspace;

            int fieldIndex = pFeatureClass.Fields.FindField(strField);

            if (fieldIndex >= 0)
            {
                string strFieldx = pFeatureClass.Fields.get_Field(fieldIndex).Name;

                //������ѯ����
                IQueryDef tQueryDef = tFeatWorkspace.CreateQueryDef();
                tQueryDef.Tables = tDataset.BrowseName;
                tQueryDef.SubFields = "Distinct (" + strFieldx + ")";
                //�õ���ѯ�α�
                ICursor tCursor = tQueryDef.Evaluate();
                for (IRow tRow = tCursor.NextRow(); tRow != null; tRow = tCursor.NextRow())
                {
                    uvList.Add(tRow.get_Value(0).ToString());
                }

                //�ͷ��ڴ�
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tCursor);
            }

            return uvList;
        } 

        /// <summary>
        /// ��ָ�������ռ��д�ָ�����Ƶ����Ա�
        /// </summary>
        /// <param name="pTableName">������</param>
        /// <param name="pFeatWorkspace">�����ռ�</param>
        /// <returns>�������Ա�</returns>
        public static ITable OpenTable(string pTableName,IFeatureWorkspace pFeatWorkspace)
        {
            ITable tTable = null;
            //�ж�Ҫ�����Ƿ��Ѿ�����
            bool IsExist = (pFeatWorkspace as IWorkspace2).get_NameExists(esriDatasetType.esriDTTable, pTableName);
            if (IsExist == true)
            {
                //��ȡָ�����Ƶ����Ա�
                tTable = pFeatWorkspace.OpenTable(pTableName);             
            }

            return tTable;
        }

        /// <summary>
        /// ��ȡָ���ֶ�ֵ�ļ�¼��Ŀ
        /// </summary>
        /// <param name="pTable">���Ա�</param>
        /// <param name="strField">�ֶ���</param>
        /// <param name="strValue">�ֶ�ֵ</param>
        /// <returns>���ؼ�¼��Ŀ</returns>
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
        /// ����ֶ�ȡ��Ψһֵ������һ��hashtable,��ΪΨһֵ��value,ֵΪ��Ӧ�ĸ���
        /// </summary>
        /// <param name="pTable">���Ա�</param>
        /// <param name="fields">�ֶμ���</param>
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
                if (fields[1] != "�ٷֱ�" && fields[1] != "�����")
                {
                    indexNormField = cursor.Fields.FindField(fields[1]);
                }
                else if (fields[1] == "�ٷֱ�")
                {
                    indexNormField = -1;
                    IDataStatistics pDataStatistics = new DataStatisticsClass();
                    pDataStatistics.Cursor = cursor;
                    pDataStatistics.Field = fields[0];
                    valueTotal = pDataStatistics.Statistics.Sum;
                }
                else if (fields[1] == "�����")
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
                        tHashTable[fieldValue] = c + 1;  //������1
                    }
                    row = cursor.NextRow();
                }
            }
            else
            {
                if (fields.Count == 2)
                {
                    if (fields[1] == "�ٷֱ�")
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
                                tHashTable[fieldValue] = c + 1;  //������1
                            }
                            row = cursor.NextRow();
                        }
                    }
                    else if (fields[1] == "�����")
                    {
                        while (row != null)
                        {
                            fieldValue = Math.Log10(System.Convert.ToDouble(row.get_Value(indexValueField)));
                            if (tHashTable.Contains(fieldValue) == false)
                                tHashTable.Add(fieldValue, 1);
                            else
                            {
                                int c = (int)tHashTable[fieldValue];
                                tHashTable[fieldValue] = c + 1;  //������1
                            }
                            row = cursor.NextRow();
                        }
                    }
                }
                else
                {
                    while (row != null)
                    {
                        //Ϊ�յ�ʱ��ȡΪ0����Ϊ�յ�ʱ��ȡԭֵ
                        fieldValue = System.Convert.ToDouble((row.get_Value(indexValueField).ToString() == "" ? 0 : row.get_Value(indexValueField)));
                        if (tHashTable.Contains(fieldValue) == false)
                            tHashTable.Add(fieldValue, 1);
                        else
                        {
                            int c = (int)tHashTable[fieldValue];
                            tHashTable[fieldValue] = c + 1;  //������1
                        }
                        row = cursor.NextRow();
                    }
                }
            }

            //�ͷ��α���Դ
            System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor );

            return tHashTable;
        }

        /// <summary>
        /// �����ֶ��������ֶα���
        /// </summary>
        /// <param name="objClass">objClass ΪIFeatureClass</param>
        /// <param name="fieldName">�ֶ�����</param>
        /// <returns>���ر���������������򷵻ؿ�</returns>
        public static string GetFieldAliasName(IObjectClass objClass, string fieldName)
        {
            int index = objClass.FindField(fieldName);
            if (index < 0) return "";

            return objClass.Fields.get_Field(index).AliasName;
        }

        /// <summary>
        /// ͨ�����������ֶ�����
        /// </summary>
        /// <param name="objClass">objClass ΪIFeatureClass</param>
        /// <param name="aliasName">�ֶα���</param>
        /// <returns>�����ֶ�����,����������򷵻ؿ�</returns>
        public static string GetFieldName(IObjectClass objClass, string aliasName)
        {
            int index = objClass.Fields.FindFieldByAliasName(aliasName);
            if (index < 0) return "";

            return objClass.Fields.get_Field(index).Name;
        }

        /// <summary>
        /// ���ַ�������ʽ�����ֶ�ֵ
        /// </summary>
        /// <param name="fieldValue">�ֶ�ֵ</param>
        /// <returns>�����ֶ�ֵ���ַ�����ʽ</returns>
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
        /// ��ȡ��ֵ���ֶ�
        /// </summary>
        /// <param name="fields">�ֶμ�</param>
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
        /// ��ȡ�ı����ֶ�
        /// </summary>
        /// <param name="fields">�ֶμ�</param>
        /// <returns>�����ı����ֶμ�</returns>
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
        /// ��ȡ�������ֶ�
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
