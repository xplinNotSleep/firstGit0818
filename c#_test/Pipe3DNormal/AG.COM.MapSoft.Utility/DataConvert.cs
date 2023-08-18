using System;

namespace AG.COM.MapSoft.Tool
{
    /// <summary>
    /// 数据格式转换类
    /// </summary>
    public class DataConvert
    {
        #region 其他转字符串

        /// <summary>
        /// decimal?转string，值为null是返回空字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DecNullToString(decimal? value)
        {
            return DecNullToString(value, "");
        }

        /// <summary>
        /// decimal?转string，值为null是返回空字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string DecNullToString(decimal? value, string format)
        {
            if (value == null)
                return "";
            else
            {
                return value.Value.ToString(format);
            }
        }

        /// <summary>
        /// object转字符串，值为null是返回空字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ObjToString(object value)
        {
            return value != null ? value.ToString() : "";
        }

        /// <summary>
        /// Obj转Datetime，如果为null则返回当前时间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ObjToDate(object value)
        {
            if (value is DateTime)
                return (DateTime)value;

            if (value != null)
            {
                string str = value.ToString();
                DateTime dt = DateTime.Now;
                if (DateTime.TryParse(str, out dt) == true)
                {
                    return dt;
                }
            }

            return DateTime.Now;
        }

        #endregion

        #region 类型转换（由XXX?转为XXX）

        /// <summary>
        /// 返回一个非null的值（为null时返回默认值0）
        /// </summary>
        /// <param name="value"></param>       
        /// <returns></returns>
        public static decimal DecNotNull(decimal? value)
        {
            return value != null ? value.Value : 0;
        }

        /// <summary>
        /// 返回一个非null的值（为null时返回默认值0）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int IntNotNull(int? value)
        {
            return value != null ? value.Value : 0;
        }

        /// <summary>
        /// 返回一个非null的值（为null时返回默认值当前时间）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime DateNotNull(DateTime? value)
        {
            return value != null ? value.Value : DateTime.Now;
        }

        #endregion

        #region 类型转换（由XXX?转为字符串）

        /// <summary>
        /// int?转字符串（null将返回空字符串）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string IntNullToStr(int? value)
        {
            return value != null ? value.Value.ToString() : "";
        }

        /// <summary>
        /// int?转字符串（null将返回空字符串）
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string IntNullToStr(int? value, string format)
        {
            return value != null ? value.Value.ToString(format) : "";
        }

        /// <summary>
        /// double?转字符串（null将返回空字符串）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DouNullToStr(double? value)
        {
            return value != null ? value.Value.ToString() : "";
        }

        /// <summary>
        /// double?转字符串（null将返回空字符串）
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string DouNullToStr(double? value, string format)
        {
            return value != null ? value.Value.ToString() : "";
        }

        /// <summary>
        /// decimal?转字符串（null将返回空字符串）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DecNullToStr(decimal? value)
        {
            return value != null ? value.Value.ToString() : "";
        }

        /// <summary>
        /// decimal?转字符串（null将返回空字符串）
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string DecNullToStr(decimal? value, string format)
        {
            return value != null ? value.Value.ToString(format) : "";
        }

        /// <summary>
        /// DateTime?转字符串（null将返回空字符串）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DateNullToStr(DateTime? value)
        {
            return value != null ? value.Value.ToString() : "";
        }

        /// <summary>
        /// DateTime?转字符串（null将返回空字符串）
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string DateNullToStr(DateTime? value, string format)
        {
            return value != null ? value.Value.ToString(format) : "";
        }

        #endregion

        #region 由string转为XXX

        /// <summary>
        /// 由string转double（若不能转换返回0）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double StrToDou(string value)
        {
            double result = 0;
            if (double.TryParse(value, out result) == true)
                return result;
            else
                return 0;
        }

        /// <summary>
        /// 由string转decimal（若不能转换返回0）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal StrToDec(string value)
        {
            decimal result = 0;
            if (decimal.TryParse(value, out result) == true)
                return result;
            else
                return 0;
        }

        /// <summary>
        /// 由string转int（若不能转换返回0）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int StrToInt(string value)
        {
            int result = 0;
            if (int.TryParse(value, out result) == true)
                return result;
            else
                return 0;
        }

        #endregion

        #region 由object转XXX

        /// <summary>
        /// object转double，一般用于datatable的数值类型字段转double
        /// 若value为空则返回0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ObjToDou(object value)
        {
            double result = 0;

            if (value != null && value != DBNull.Value)
            {
                string tString = Convert.ToString(value);
                if (double.TryParse(tString, out result) == true)
                {

                }
            }

            return result;
        }

        /// <summary>
        /// object转decimal，一般用于datatable的数值类型字段转decimal
        /// 若value为空则返回0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ObjToDec(object value)
        {
            decimal result = 0;

            if (value != null && value != DBNull.Value)
            {
                string tString = Convert.ToString(value);
                if (decimal.TryParse(tString, out result) == true)
                {

                }
            }

            return result;
        }

        /// <summary>
        /// object转int，一般用于datatable的数值类型字段转int
        /// 若value为空则返回0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ObjToInt(object value)
        {
            int result = 0;

            if (value != null && value != DBNull.Value)
            {
                string tString = Convert.ToString(value);
                if (int.TryParse(tString, out result) == true)
                {

                }
            }

            return result;
        }

        #endregion

        #region 由object转XXX?

        /// <summary>
        /// object转double，一般用于datatable的数值类型字段转double
        /// 若value为空则返回0
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double? ObjToDouNull(object value)
        {
            double? result = null;
            double resultT = 0;

            if (value != null && value != DBNull.Value)
            {
                string tString = Convert.ToString(value);
                if (double.TryParse(tString, out resultT) == true)
                {
                    result = resultT;
                }
            }

            return result;
        }

        /// <summary>
        /// object转decimal，一般用于datatable的数值类型字段转decimal
        /// 若value为空则返回Null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal? ObjToDecNull(object value)
        {
            decimal? result = null;
            decimal resultT = 0;

            if (value != null && value != DBNull.Value)
            {
                string tString = Convert.ToString(value);
                if (decimal.TryParse(tString, out resultT) == true)
                {
                    result = resultT;
                }
            }

            return result;
        }

        /// <summary>
        /// object转int，一般用于datatable的数值类型字段转int
        /// 若value为空则返回null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int? ObjToIntNull(object value)
        {
            int? result = null;
            int resultT = 0;

            if (value != null && value != DBNull.Value)
            {
                string tString = Convert.ToString(value);
                if (int.TryParse(tString, out resultT) == true)
                {
                    result = resultT;
                }
            }

            return result;
        }

        /// <summary>
        /// object转DateTime，一般用于datatable的数值类型字段转int
        /// 若value为空则返回null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime? ObjToDateNull(object value)
        {
            DateTime? result = null;
            DateTime resultD;

            if (value != null && value != DBNull.Value)
            {
                string tString = Convert.ToString(value);
                if (DateTime.TryParse(tString, out resultD) == true)
                {
                    result = resultD;
                }
            }

            return result;
        }

        #endregion
    }
}
