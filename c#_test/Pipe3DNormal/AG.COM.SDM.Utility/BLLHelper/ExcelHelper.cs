using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// Excel帮助类
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// 导出Csv
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="tFilePath"></param>
        public static void ExportCsv(DataGridView dgv, string tFilePath)
        {
            using (StreamWriter sw = new StreamWriter(tFilePath, false, Encoding.Default))
            {
                //写入表头
                string tHeader = "";
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    DataGridViewColumn col = dgv.Columns[i];
                    tHeader += col.HeaderText + ",";
                }
                if (tHeader.EndsWith(","))
                {
                    tHeader = tHeader.Substring(0, tHeader.Length - 1);
                }
                sw.WriteLine(tHeader);

                foreach (DataGridViewRow tDgvRow in dgv.Rows)
                {
                    if (tDgvRow.IsNewRow) continue;

                    string tText = "";

                    DataRow tRow = (tDgvRow.DataBoundItem as DataRowView).Row;

                    for (int n = 0; n < dgv.Columns.Count; n++)
                    {
                        string tValue = Convert.ToString(tRow[n]);
                        tText += tValue + ",";
                    }
                    if (tText.EndsWith(","))
                    {
                        tText = tText.Substring(0, tText.Length - 1);
                    }

                    sw.WriteLine(tText);
                }

                sw.Close();
            }
        }

        /// <summary>
        /// 获取Excel文件所有的sheet的名称
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<string> GetExcelAllSheetNames(string fileName)
        {
            Dictionary<string, DataTable> tReturn = new Dictionary<string, DataTable>();
            var fileType = System.IO.Path.GetExtension(fileName.ToLower()).Trim();
            var excelVersionNumber = fileType == ".xlsx" ? "12.0" : "8.0";
            var strConn = "";
            if (fileType == ".xls")
                strConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0} ;Extended Properties=\"Excel {1};HDR=Yes;IMEX=1;\"", fileName, excelVersionNumber);
            else
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source='" + fileName + "';" + "Extended Properties='Excel " + excelVersionNumber + ";HDR=Yes;IMEX=1';";

            //连接数据源
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            //获取所有sheet的名称
            DataTable sheetNames = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

            List<string> result = new List<string>();

            foreach (DataRow dr in sheetNames.Rows)
            {
                result.Add(Convert.ToString(dr[2]));
            }

            conn.Close();

            return result;
        }

        /// <summary>
        /// 从Excel文件读取DataTable
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static DataTable GetDataTableFromExcel(string fileName, string sheetName)
        {
            Dictionary<string, DataTable> tReturn = new Dictionary<string, DataTable>();
            var fileType = System.IO.Path.GetExtension(fileName.ToLower()).Trim();
            var excelVersionNumber = fileType == ".xlsx" ? "12.0" : "8.0";
            var strConn = "";
            if (fileType == ".xls")
                strConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0} ;Extended Properties=\"Excel {1};HDR=Yes;IMEX=1;\"", fileName, excelVersionNumber);
            else
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source='" + fileName + "';" + "Extended Properties='Excel " + excelVersionNumber + ";HDR=Yes;IMEX=1';";

            //连接数据源
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            //获取所有sheet的名称
            DataTable sheetNames = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

            DataSet ds = new DataSet();

            int idxSheet = 0;
            for (int i = 0; i < sheetNames.Rows.Count; i++)
            {
                DataRow dr = sheetNames.Rows[i];

                string name = Convert.ToString(dr[2]);
                if (name.ToLower()==sheetName.ToLower())
                {
                    idxSheet = i;
                    break;
                }
            }

            //sheetNames表保存sheet名的顺序跟excel软件的相反，也就是第一个sheet排在最后
            OleDbDataAdapter adapter = new OleDbDataAdapter("select * from [" + sheetNames.Rows[idxSheet][2] + "]", strConn);
            adapter.Fill(ds);

            conn.Close();

            DataTable dt = ds.Tables[0];

            return dt;
        }


        public static bool WriteDataTableToExcel(string fileName, DataTable dt)
        {
            var fileType = System.IO.Path.GetExtension(fileName.ToLower()).Trim();
            var excelVersionNumber = fileType == ".xlsx" ? "12.0" : "8.0";
            var strConn = "";
            if (fileType == ".xls")
                strConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0} ;Extended Properties=\"Excel {1};HDR=Yes;IMEX=0;\"", fileName, excelVersionNumber);
            else
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source='" + fileName + "';" + "Extended Properties='Excel " + excelVersionNumber + ";HDR=Yes;IMEX=0';";

            //连接数据源
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            //获取所有sheet的名称
            DataTable sheetNames = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });


            //excel型数据库不支持删除操作
            //try
            //{
            //    cmd.CommandText = string.Format("delete * from [{0}]", dt.TableName);
            //    cmd.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{
            //    AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
            //}

            DataSet ds = new DataSet();

            try
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter(string.Format("select * from [{0}]", dt.TableName), strConn);
                adapter.Fill(ds);
                if (ds == null || ds.Tables.Count == 0) return false;
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
            }

            DataTable dtTemp = ds.Tables[0];

            string strFields = "";
            //向语句中循环添加表中的字段名
            foreach (DataColumn col in dt.Columns)
            {
                if (dtTemp.Columns.Contains(col.ColumnName))
                {
                    strFields += col.ColumnName;
                    strFields += ",";
                }
            }
            strFields = strFields.TrimEnd(',');

            foreach (DataRow dr in dt.Rows)
            {
                string strValues = "";
                foreach (DataColumn col in dt.Columns)
                {
                    if (dtTemp.Columns.Contains(col.ColumnName))
                        strValues += string.Format("'{0}',", dr[col.ColumnName].ToString());
                }
                strValues = strValues.TrimEnd(',');

                try
                {
                    OleDbCommand cmd = conn.CreateCommand();
                    string strSQL = string.Format("insert into [{0}]({1})values({2})", dt.TableName, strFields, strValues);
                    cmd.CommandText = strSQL;
                    int nResult = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                }
            }

            conn.Close();

            return true;
        }
    }
}
