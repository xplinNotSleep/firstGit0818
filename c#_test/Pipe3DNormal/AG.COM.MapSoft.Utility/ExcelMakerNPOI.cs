using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.Util;
using System.Data;
using System.Windows.Forms;
using AG.COM.MapSoft.Tool;
using NPOI.XSSF.UserModel;
using BorderStyle = NPOI.SS.UserModel.BorderStyle;
using HorizontalAlignment = NPOI.SS.UserModel.HorizontalAlignment;

namespace AG.COM.MapSoft.Tool
{
    /// <summary>
    /// NPOI 读取Excel
    /// </summary>
    public class ExcelMakerNPOI
    {
        #region 字段
        private IWorkbook workbook = null;//工作薄
        private ISheet currentSheet = null;//当前工作表
        //private int rowNumbers = 0;//工作表总行数
        //private int columnNumbers = 0;//工作表总列数
        private string fullpath = string.Empty;//Excel文件路径
        private ExcelFormat excelformat = ExcelFormat.XLS;//默认Excel文件格式
        private float rowDefaultHight = 14.5f;//默认行高
        private ICellStyle defaultCellStyle = null;
        #endregion

        public ICellStyle CellStyle1 = null;
        public ICellStyle CellStyle2 = null;

        #region 属性
        /// <summary>
        /// 工作表个数
        /// </summary>
        public int SheetNumber
        {
            get
            {
                if (workbook == null) return 0;
                else return workbook.NumberOfSheets;
            }
        }

        /// <summary>
        /// 当前工作表
        /// </summary>
        public ISheet CurrentSheet
        {
            get { return currentSheet; }
            set { currentSheet = value; }
        }

        /// <summary>
        /// 工作表表名数组
        /// </summary>
        public String[] SheetNames
        {
            get
            {
                if (workbook != null)
                {
                    return GetSheetNames();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 工作表总行数
        /// </summary>
        public int RowNumbers
        {
            get
            {
                //LastRowNum=PhysicalNumberOfCellsRow-1
                return currentSheet.PhysicalNumberOfRows;
            }
        }

        /// <summary>
        /// 工作表总列数
        /// </summary>
        public int ColumnNumbers
        {
            get
            {
                try
                {
                    IRow headRow = currentSheet.GetRow(0);
                    return headRow.LastCellNum;
                    //LastCellNum=PhysicalNumberOfCells
                }
                catch
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 行高
        /// </summary>
        public float RowHight
        {
            set { rowDefaultHight = value; }
        }

        public IWorkbook Workbook
        {
            get { return workbook; }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ExcelMakerNPOI()
        {

        }

        /// <summary>
        /// 构造函数，新建空Excel
        /// </summary>
        /// <param name="excelFormat">Excel文档格式</param>
        public ExcelMakerNPOI(ExcelFormat excelFormat)
        {
            //根据Excel文件格式，创建工作薄
            if (ExcelFormat.XLS == excelFormat)
            {
                workbook = new HSSFWorkbook();
            }
            if (ExcelFormat.XLSX == excelFormat)
            {
                workbook = new XSSFWorkbook();
            }

            excelformat = excelFormat;
            //初始化CellStyle对象
            defaultCellStyle = CommonCellType();
            CellStyle1 = SetCellStyle1();
            CellStyle2 = SetCellStyle2();
        }

        /// <summary>
        /// 打开Excel文件,默认获取第一张表
        /// </summary>
        /// <param name="filepath">Excel文件路径</param>
        /// <returns></returns>
        public void Open(string filepath)
        {
            if (!IsExcelFile(filepath)) return;
            using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.ReadWrite))
            {
                workbook = WorkbookFactory.Create(fs);
                //默认当前工作表为第一张表
                currentSheet = workbook.GetSheetAt(0);
                defaultCellStyle = CommonCellType();

                if (Path.GetExtension(filepath).ToUpper().Contains("XLSX"))
                    excelformat = ExcelFormat.XLSX;
            }
        }
        /// <summary>
        /// 打开Excel打开，如果被占用则提示
        /// </summary>
        /// <param name="maker"></param>
        /// <param name="excelFile"></param>
        public static bool OpenExcel(ref ExcelMakerNPOI maker, string excelFile)
        {
            try
            {
                maker.Open(excelFile);
                return true;
            }
            catch (Exception ex)
            {
                MapSoftLog.LogError(ex.Message, ex);
                if (ex.Message.Contains("The process cannot access the file"))
                    MessageBox.Show("Excel文件已被打开，请先关闭后再操作！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageHandler.ShowErrorMsg(ex);
                return false;
            }
        }

        /// <summary>
        /// 保存Excel文件
        /// </summary>
        /// <param name="savepath">Excel保存路径</param>
        /// <returns>true:保存成功；false：保存失败</returns>
        public bool Save(string savepath)
        {
            if (string.IsNullOrEmpty(savepath)) return false;

            try
            {
                using (FileStream fs = new FileStream(savepath, FileMode.OpenOrCreate))
                {
                    workbook.Write(fs);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 设置活动工作表
        /// </summary>
        /// <param name="index">工作表索引</param>
        public void ActiveSheet(int index)
        {
            currentSheet = workbook.GetSheetAt(index);
        }
        /// <summary>
        /// 设置活动工作表
        /// </summary>
        /// <param name="sheetName">表名</param>
        public void ActiveSheet(string sheetName)
        {
            currentSheet = workbook.GetSheet(sheetName);
        }

        /// <summary>
        ///  创建工作表
        /// </summary>
        /// <param name="sheetName">表名,若为空，表名设为Sheet+(N+1)</param>
        public void CreateSheet(string sheetName)
        {
            if (string.IsNullOrEmpty(sheetName))
                currentSheet = workbook.CreateSheet("Sheet" + (SheetNumber + 1));
            else
                currentSheet = workbook.CreateSheet(sheetName);
        }

        /// <summary>
        /// 单元格格式
        /// </summary>
        /// <returns></returns>
        public ICellStyle CommonCellType()
        {
            if (workbook != null)
            {
                ICellStyle cellStyle = workbook.CreateCellStyle();
                #region 对齐
                cellStyle.Alignment = HorizontalAlignment.Center;//水平对齐
                cellStyle.VerticalAlignment = VerticalAlignment.Top;//垂直对齐，靠上top实为居中
                /*NPOI 2.0.6.0垂直对齐方式有误：
                  Top=Center
                  Center=Bottom
                  Bottom=Distributed
                  Distributed=Bottom(设为此值，用Office打开无法正常显示)
                 */

                //缩进，需要与水平对齐以前使用，且水平对齐方式只能选择靠左Left、靠右Right、分散对齐Justify三者之一
                //cellStyle.Indention = 1;

                //自动换行
                cellStyle.WrapText = true;

                //文本旋转
                //cellStyle.Rotation = (short)45;//旋转角度-90 到 90.

                //文字竖排
                //未找到方法

                #endregion

                #region 字体
                //IFont font = (workbook).CreateFont();
                //font.FontHeight = 5 * 20;//字体大小，即字号
                ////font.FontHeightInPoints = 5;
                //font.Boldweight = (short)5;//加粗厚度
                //font.Charset = (short)5;
                //font.Color = IndexedColors.Red.Index;//颜色
                //font.FontName = "宋体";//字体样式
                //font.IsItalic = true;//倾斜
                //font.IsStrikeout = true;//删除线？
                //font.Underline = FontUnderlineType.Single;//下划线
                ////上标
                ////小标
                //font.TypeOffset = FontSuperScript.Super;
                //cellStyle.SetFont(font);
                #endregion

                #region 边框
                //样式
                cellStyle.BorderBottom = BorderStyle.Thin;
                cellStyle.BorderLeft = BorderStyle.Thin;
                cellStyle.BorderRight = BorderStyle.Thin;
                cellStyle.BorderTop = BorderStyle.Thin;
                //颜色,默认为黑色
                //cellStyle.BottomBorderColor = IndexedColors.LightBlue.Index;
                //cellStyle.LeftBorderColor = IndexedColors.LightBlue.Index;
                //cellStyle.RightBorderColor = IndexedColors.LightBlue.Index;
                //cellStyle.TopBorderColor = IndexedColors.LightBlue.Index; 
                //对角线
                //cellStyle.BorderDiagonal = BorderDiagonal.Both;
                //cellStyle.BorderDiagonalColor = IndexedColors.Red.Index;
                //cellStyle.BorderDiagonalLineStyle = BorderStyle.DashDot;
                #endregion

                #region 图案
                //图案样式
                //cellStyle.FillPattern = FillPattern.Diamonds;

                //图案颜色
                //cellStyle.FillForegroundColor = IndexedColors.Black.Index;

                //单元格背景
                //cellStyle.FillBackgroundColor = IndexedColors.Black.Index;
                #endregion

                #region 保护
                //cellStyle.IsLocked = true;//锁定
                //cellStyle.IsHidden = true;//隐藏
                #endregion

                //数据格式

                return cellStyle;
            }

            return null;
        }
        /// <summary>
        /// 设置单元格对齐
        /// </summary>
        /// <param name="rowIdx">行号</param>
        /// <param name="colIdx">列号</param>
        /// <param name="style">单元格格式</param>
        public void SetCellStyleAlignment(int rowIdx, int colIdx, HorizontalAlignment horizontal, VerticalAlignment vertical)
        {
            IRow row = currentSheet.GetRow(rowIdx);
            if (row == null) return;
            ICell cell = row.GetCell(colIdx);
            if (cell == null) return;
            ICellStyle style = workbook.CreateCellStyle();
            style.Alignment = horizontal;
            style.VerticalAlignment = vertical;
            cell.CellStyle = style;
        }
        /// <summary>
        /// 创建单元格格式1——水平居中，无边框
        /// </summary>
        /// <param name="rowIdx">行号</param>
        private ICellStyle SetCellStyle1()
        {
            ICellStyle cellStyle = workbook.CreateCellStyle();
            //对齐
            cellStyle.Alignment = HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = VerticalAlignment.Top;
            //边框
            cellStyle.BorderBottom = BorderStyle.None;
            cellStyle.BorderLeft = BorderStyle.None;
            cellStyle.BorderRight = BorderStyle.None;
            cellStyle.BorderTop = BorderStyle.None;

            return cellStyle;
        }
        /// <summary>
        /// 创建单元格格式2——字体20号
        /// </summary>
        /// <returns></returns>
        private ICellStyle SetCellStyle2()
        {
            ICellStyle cellStyle = workbook.CreateCellStyle();
            //对齐
            cellStyle.Alignment = HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = VerticalAlignment.Top;
            //边框
            cellStyle.BorderBottom = BorderStyle.None;
            cellStyle.BorderLeft = BorderStyle.None;
            cellStyle.BorderRight = BorderStyle.None;
            cellStyle.BorderTop = BorderStyle.None;
            //字体
            IFont font = workbook.CreateFont();
            font.FontHeight = 20 * 20;//20号字体
            font.Boldweight = (short)FontBoldWeight.Bold;
            cellStyle.SetFont(font);

            return cellStyle;
        }
        /// <summary>
        /// 设置列宽度
        /// </summary>
        /// <param name="nCol"></param>
        /// <param name="dWidth"></param>
        public void SetColunmWidth(int nCol, int dWidth)
        {
            if (currentSheet == null)
                return;
            if (nCol < 0 || dWidth <= 0)
                return;
            currentSheet.SetColumnWidth(nCol, dWidth * 256);
        }
        /// <summary>
        /// 设置行高
        /// </summary>
        /// <param name="nRow"></param>
        /// <param name="nHeight"></param>
        public void SetRowHeight(int nRow, int nHeight)
        {
            if (currentSheet == null)
                return;
            if (nRow < 0 || nHeight <= 0)
                return;
            currentSheet.SetColumnWidth(nRow, nHeight * 20);
        }
        /// <summary>
        /// 设置单元格值
        /// </summary>
        /// <param name="rowIdx">行号</param>
        /// <param name="colIdx">列号</param>
        /// <param name="value">值</param>
        public void SetCellValue(int rowIdx, int colIdx, object value)
        {
            //获取行，若不存在，创建行
            IRow row = currentSheet.GetRow(rowIdx);
            if (row == null)
                row = currentSheet.CreateRow(rowIdx);

            row.HeightInPoints = rowDefaultHight;
            //获取单元格，若不存在，创建单元格
            ICell cell = row.GetCell(colIdx);
            if (cell == null)
                cell = row.CreateCell(colIdx);

            //赋值
            switch (value.GetType().Name)
            {
                case "String":
                    cell.SetCellValue(Convert.ToString(value));
                    break;
                case "DateTime":
                    cell.SetCellValue(Convert.ToDateTime(value));
                    break;
                case "Bool":
                case "Boolean":
                    cell.SetCellValue(Convert.ToBoolean(value));
                    break;
                case "Double":
                    cell.SetCellValue(Convert.ToDouble(value));
                    break;
            }
            //单元格格式
            cell.CellStyle = defaultCellStyle;
        }
        /// <summary>
        /// 设置单元格值和格式
        /// </summary>
        /// <param name="rowIdx"></param>
        /// <param name="colIdx"></param>
        /// <param name="value"></param>
        /// <param name="cellStyle"></param>
        public void SetCellValue(int rowIdx, int colIdx, object value, ICellStyle cellStyle)
        {
            //获取行，若不存在，创建行
            IRow row = currentSheet.GetRow(rowIdx);
            if (row == null)
                row = currentSheet.CreateRow(rowIdx);

            row.HeightInPoints = rowDefaultHight;
            //获取单元格，若不存在，创建单元格
            ICell cell = row.GetCell(colIdx);
            if (cell == null)
                cell = row.CreateCell(colIdx);

            //赋值
            switch (value.GetType().Name)
            {
                case "String":
                    cell.SetCellValue(Convert.ToString(value));
                    break;
                case "DateTime":
                    cell.SetCellValue(Convert.ToDateTime(value));
                    break;
                case "Bool":
                case "Boolean":
                    cell.SetCellValue(Convert.ToBoolean(value));
                    break;
                case "Double":
                    cell.SetCellValue(Convert.ToDouble(value));
                    break;
            }
            if (cellStyle != null)
                cell.CellStyle = cellStyle;
        }
        /// <summary>
        /// 设置单元格格式
        /// </summary>
        /// <param name="rowIdx"></param>
        /// <param name="colIdx"></param>
        /// <param name="cellStyle"></param>
        public void SetCellSyle(int rowIdx, int colIdx, ICellStyle cellStyle)
        {
            //获取行，若不存在，创建行
            IRow row = currentSheet.GetRow(rowIdx);
            if (row == null)
                row = currentSheet.CreateRow(rowIdx);

            row.HeightInPoints = rowDefaultHight;
            //获取单元格，若不存在，创建单元格
            ICell cell = row.GetCell(colIdx);
            if (cell == null)
                cell = row.CreateCell(colIdx);
            //格式
            if (cellStyle != null)
                cell.CellStyle = cellStyle;
        }

        /// <summary>
        /// 设置行高
        /// </summary>
        /// <param name="rowStartIndex"></param>
        /// <param name="rowEndIndex"></param>
        /// <param name="hight"></param>
        public void SetRowHeight(int rowStartIndex, int rowEndIndex, short hight)
        {
            if (rowStartIndex > rowEndIndex) return;

            for (int i = rowStartIndex; i <= rowEndIndex; i++)
            {
                IRow row = currentSheet.GetRow(i);
                row.HeightInPoints = hight;
            }
        }

        /// <summary>
        /// 自动调整列宽
        /// </summary>
        /// <param name="colIndex"></param>
        public void AutoSizeColumn(int colIndex)
        {
            currentSheet.AutoSizeColumn(colIndex);
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="rowStartIdx">起始行号,起始行号为0</param>
        /// <param name="colStartIdx">起始列号，起始列号为0</param>
        /// <param name="rowEndIdx">终点行号</param>
        /// <param name="colEndIdx">终点列号</param>
        public void MergeCell(int rowStartIdx, int colStartIdx, int rowEndIdx, int colEndIdx)
        {
            //合并格子索引重新调整
            if (rowEndIdx < rowStartIdx)
                rowEndIdx = rowStartIdx;
            if (colEndIdx < colStartIdx)
                colEndIdx = colStartIdx;

            CellRangeAddress range = new CellRangeAddress(rowStartIdx, rowEndIdx, colStartIdx, colEndIdx);
            currentSheet.AddMergedRegion(range);
            //合并后单元格格式
            if (excelformat == ExcelFormat.XLS)
                ((HSSFSheet)currentSheet).SetEnclosedBorderOfRegion(range, NPOI.SS.UserModel.BorderStyle.Thin, HSSFColor.Black.Index);
        }

        /// <summary>
        /// 获取工作薄的所有工作表表名
        /// </summary>
        /// <returns></returns>
        public String[] GetSheetNames()
        {
            if (workbook == null) return null;

            List<String> list = new List<string>();
            int numofsheets = workbook.NumberOfSheets;
            for (int i = 0; i < numofsheets; i++)
            {
                list.Add(workbook.GetSheetAt(i).SheetName);
            }
            return list.ToArray();
        }

        /// <summary>
        /// 获取行
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<IRow> GetRows(int startIndex, int endIndex)
        {
            if (currentSheet == null) return null;

            List<IRow> listRows = new List<IRow>();
            for (int i = startIndex - 1; i < endIndex; i++)
            {
                IRow row = currentSheet.GetRow(i);
                if (row == null) continue;

                listRows.Add(row);
            }
            return listRows;
        }

        /// <summary>
        /// 将DataTable中的数据填充中Excel中
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="rowStartIdx">行号</param>
        /// <param name="colStartIdx">列号</param>
        public bool WriteDataToExcel(DataTable dt, int rowStartIdx, int colStartIdx)
        {
            try
            {
                //根据第一行数据调整单元格宽度
                DataRow firstRow = dt.Rows[0];
                for (int n = 0; n < dt.Columns.Count; n++)
                {
                    SetCellValue(rowStartIdx, n + colStartIdx, Convert.ToString(firstRow[n]).Trim());
                    currentSheet.AutoSizeColumn(n);
                }
                
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    for (int n = 0; n < dt.Columns.Count; n++)
                    {
                        SetCellValue(i + rowStartIdx, n + colStartIdx, Convert.ToString(dr[n]).Trim());
                    }
                }
                //for (int n = 0; n < dt.Columns.Count; n++)
                //{
                //    currentSheet.AutoSizeColumn(n);
                //}
                return true;
            }
            catch (Exception ex)
            {
                MapSoftLog.LogError(ex.Message, ex);
                return false;
            }
        }
        /// <summary>
        /// 将DataTable中的数据填充中Excel中(分页模式)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="rowStartIdx"></param>
        /// <param name="colStartIdx"></param>
        /// <param name="num">每页记录数量(默认为每页24条记录)</param>
        /// <returns></returns>
        public bool WriteDataToExcelPage(ref DataTable dt, int rowStartIdx, int colStartIdx, int num)
        {
            try
            {
                if (dt.Rows.Count > num)//表记录大于24条
                {
                    //先将前24条记录写入excel
                    for (int i = 0; i < num; i++)
                    {
                        DataRow dr = dt.Rows[i];

                        for (int n = 0; n < dt.Columns.Count; n++)
                        {
                            if (n == 6 || n == 7 || n == 8 || n == 9)
                                SetCellValue(i + rowStartIdx, n + colStartIdx, JudgeDouble(dr[n].ToString()));
                            else
                                SetCellValue(i + rowStartIdx, n + colStartIdx, dr[n].ToString());
                        }
                    }
                    //再将表前24条记录删除
                    for (int i = 0; i < num; i++)
                    {
                        dt.Rows.Remove(dt.Rows[0]);
                    }
                }
                else//表记录小于24条
                {
                    //向表添加空记录凑整24条
                    int count = dt.Rows.Count;
                    for (int i = 0; i < num - count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dt.Rows.Add(dr);
                    }
                    //再写入excel
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];

                        for (int n = 0; n < dt.Columns.Count; n++)
                        {
                            if (n == 6 || n == 7 || n == 8 || n == 9)
                                SetCellValue(i + rowStartIdx, n + colStartIdx, JudgeDouble(dr[n].ToString()));
                            else
                                SetCellValue(i + rowStartIdx, n + colStartIdx, dr[n].ToString());
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MapSoftLog.LogError(ex.Message, ex);
                return false;
            }
        }
        public bool WriteDataToExcel(DataTable dt, int rowStartIdx, int colStartIdx, bool IsAddTittle)
        {
            try
            {
                if (IsAddTittle == false)
                    WriteDataToExcel(dt, rowStartIdx, colStartIdx);
                else
                {
                    //DataTable标题作为第一行数据
                    int count = dt.Columns.Count;
                    for (int i = 0; i < count; i++)
                    {
                        SetCellValue(rowStartIdx, colStartIdx + i, dt.Columns[i].ColumnName);
                    }
                    //填充数据
                    WriteDataToExcel(dt, rowStartIdx + 1, colStartIdx);
                }
                return true;
            }
            catch (Exception ex)
            {
                MapSoftLog.LogError(ex.Message, ex);
                return false;
            }
        }
        public bool WriteDataToExcelWithStyle(DataTable dt, int rowStartIdx, int colStartIdx, bool IsAddTittle, ICellStyle cellStyle)
        {
            try
            {
                if (IsAddTittle == false)
                    WriteDataToExcelWithStyle(dt, rowStartIdx, colStartIdx, cellStyle);
                else
                {
                    //DataTable标题作为第一行数据
                    int count = dt.Columns.Count;
                    for (int i = 0; i < count; i++)
                    {
                        SetCellValue(rowStartIdx, colStartIdx + i, dt.Columns[i].ColumnName, cellStyle);
                    }
                    //填充数据
                    WriteDataToExcelWithStyle(dt, rowStartIdx + 1, colStartIdx, cellStyle);
                }
                return true;
            }
            catch (Exception ex)
            {
                MapSoftLog.LogError(ex.Message, ex);
                return false;
            }
        }
        public bool WriteDataToExcelWithStyle(DataTable dt, int rowStartIdx, int colStartIdx, ICellStyle cellStyle)
        {
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    for (int n = 0; n < dt.Columns.Count; n++)
                    {
                        SetCellValue(i + rowStartIdx, n + colStartIdx, Convert.ToString(dr[n]).Trim(), cellStyle);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MapSoftLog.LogError(ex.Message, ex);
                return false;
            }
        }


        public bool WriteDataToExcel(List<string> list, int rowStartIdx, int colStartIdx)
        {
            try
            {
                for (int n = 0; n < list.Count; n++)
                {
                    SetCellValue(rowStartIdx, n + colStartIdx, Convert.ToString(list[n]));
                }
                return true;
            }
            catch (Exception ex)
            {
                MapSoftLog.LogError(ex.Message, ex);
                return false;
            }
        }

        /// <summary>
        /// 从上到下将数据写入Excel
        /// </summary>
        /// <param name="list"></param>
        /// <param name="rowStartIdx"></param>
        /// <param name="colStartIdx"></param>
        /// <returns></returns>
        public bool WriteDataToExcelUpToDown(List<string> list, int rowStartIdx, int colStartIdx)
        {
            try
            {
                for (int n = 0; n < list.Count; n++)
                {
                    SetCellValue(n + rowStartIdx, colStartIdx, Convert.ToString(list[n]),CellStyle1);
                }
                return true;
            }
            catch (Exception ex)
            {
                MapSoftLog.LogError(ex.Message, ex);
                return false;
            }

        }
        /// <summary>
        /// 读取Excel数据到DataTable中
        /// </summary>
        /// <param name="sheet">工作表</param>
        /// <param name="rowStartIdx">起始行号</param>
        /// <param name="colStartIdx">起始列号</param>
        /// <param name="rowEndIdx">终点行号</param>
        /// <param name="colEndIdx">终点列号</param>
        /// <returns></returns>
        public DataTable ReadExcelToDataTable(int rowStartIdx, int colStartIdx, int rowEndIdx, int colEndIdx)
        {
            if (currentSheet == null) return null;

            DataTable dt = new DataTable(currentSheet.SheetName);
            //新增标题列
            for (int i = 0; i < colEndIdx; i++)
            {
                dt.Columns.Add("Columns" + i);
            }
            for (int i = rowStartIdx; i < rowEndIdx; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = colStartIdx; j < colEndIdx; j++)
                {
                    dr[j] = GetCellValue(i, j);
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }
        /// <summary>
        /// 读取Excel数据到DataTable中(第一行为表头，字段类型都为Object)
        /// </summary>
        /// <returns></returns>
        public DataTable ReadExcelToDataTable()
        {
            if (currentSheet == null) return null;
            DataTable dt = new DataTable(currentSheet.SheetName);
            //表头
            int colNum = ColumnNumbers;
            for (int i = 0; i < colNum; i++)
            {
                dt.Columns.Add(new System.Data.DataColumn(GetCellValue(0, i).ToString(), typeof(System.Object)));
            }

            //数据
            int rowNum = RowNumbers;
            for (int i = 1; i < rowNum; i++)
            {
                DataRow dr = dt.NewRow();
                if (currentSheet.GetRow(i) == null) continue;//跳过空行
                for (int j = 0; j < colNum; j++)
                {
                    dr[j] = GetCellValue(i, j).ToString();
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>
        /// 读取Excel数据到DataTable中(第headRow行为表头，字段类型都为Object)
        /// </summary>
        /// <returns></returns>
        public DataTable ReadExcelToDataTable(int headRow)
        {
            if (currentSheet == null) return null;
            DataTable dt = new DataTable(currentSheet.SheetName);
            //表头
            int colNum = ColumnNumbers;
            for (int i = 0; i < colNum; i++)
            {
                dt.Columns.Add(new System.Data.DataColumn(GetCellValue(headRow-1, i).ToString(), typeof(System.Object)));
            }

            //数据
            int rowNum = RowNumbers;
            for (int i = headRow; i < rowNum; i++)
            {
                DataRow dr = dt.NewRow();
                if (currentSheet.GetRow(i) == null) continue;//跳过空行
                for (int j = 0; j < colNum; j++)
                {
                    dr[j] = GetCellValue(i, j).ToString();
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>
        /// 读取Excel数据到DataTable中(第headRow行为表头，字段类型都为Object)
        /// </summary>
        /// <param name="headRow">表头行序号</param>
        /// <param name="startColumn">起始列序号</param>
        /// <param name="endColumn">结束列序号</param>
        /// <returns></returns>
        public DataTable ReadExcelToDataTable(int headRow,int startColumn,int endColumn)
        {
            if (currentSheet == null) return null;
            DataTable dt = new DataTable(currentSheet.SheetName);
            //表头
            //int colNum = ColumnNumbers;
            for (int i = startColumn; i <= endColumn; i++)
            {
                dt.Columns.Add(new System.Data.DataColumn(GetCellValue(headRow - 1, i-1).ToString(), typeof(System.Object)));
            }

            //数据
            int rowNum = RowNumbers;
            for (int i = headRow; i < rowNum; i++)
            {
                DataRow dr = dt.NewRow();
                if (currentSheet.GetRow(i) == null) continue;//跳过空行
                int column = startColumn;
                for (int j = 0; j < endColumn-startColumn+1; j++)
                {
                    dr[j] = GetCellValue(i, column - 1).ToString();
                    column++;
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>
        /// 获取某个单元格的行跨度与列跨度
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="rowNum"></param>
        /// <param name="colNum"></param>
        /// <param name="rowSpan"></param>
        /// <param name="colSpan"></param>
        /// <returns></returns>
        public bool IsMergeCell(ISheet sheet, int rowNum, int colNum, out int rowSpan, out int colSpan)
        {
            bool result = false;
            rowSpan = 0;
            colSpan = 0;
            if ((rowNum < 1) || (colNum < 1)) return result;
            int rowIndex = rowNum - 1;
            int colIndex = colNum - 1;
            int regionsCount = sheet.NumMergedRegions;
            rowSpan = 1;
            colSpan = 1;
            for (int i = 0; i < regionsCount; i++)
            {
                CellRangeAddress range = sheet.GetMergedRegion(i);
                sheet.IsMergedRegion(range);
                if (range.FirstRow == rowIndex && range.FirstColumn == colIndex)
                {
                    rowSpan = range.LastRow - range.FirstRow + 1;
                    colSpan = range.LastColumn - range.FirstColumn + 1;
                    break;
                }
            }
            try
            {
                result = sheet.GetRow(rowIndex).GetCell(colIndex).IsMergedCell;
            }
            catch
            {
            }
            return result;
        }
        /// <summary>
        /// 获取表格第一行数据
        /// </summary>
        /// <returns></returns>
        public List<string> GetTheFirstRowData()
        {
            if (currentSheet == null) return null;

            List<string> list = new List<string>();
            //表头
            int colNum = ColumnNumbers;
            for (int i = 0; i < colNum; i++)
            {
                list.Add(GetCellValue(0, i).ToString());
            }
            return list;
        }

        /// <summary>
        /// 获取表格某行数据
        /// </summary>
        /// <returns></returns>
        public List<string> GetRowData(int row)
        {
            if (currentSheet == null) return null;

            List<string> list = new List<string>();
            //表头
            int colNum = ColumnNumbers;
            for (int i = 0; i < colNum; i++)
            {
                list.Add(GetCellValue(row-1, i).ToString());
            }
            return list;
        }

        /// <summary>
        /// 获取单元格的值
        /// </summary>
        /// <param name="rowIdx">行号</param>
        /// <param name="colIdx">列号</param>
        /// <returns></returns>
        public object GetCellValue(int rowIdx, int colIdx)
        {
            object obj = null;
            try
            {
                IRow row = currentSheet.GetRow(rowIdx);
                ICell cell = currentSheet.GetRow(rowIdx).GetCell(colIdx);
                if (cell == null) return obj = "";

                switch (cell.CellType)
                {
                    case CellType.Boolean:
                        obj = cell.BooleanCellValue;
                        break;
                    case CellType.Error:
                        obj = cell.ErrorCellValue;
                        break;
                    case CellType.Numeric:
                        //NPOI中数字和日期都是NUMERIC类型的，这里对其进行判断是否是日期类型
                        if (HSSFDateUtil.IsCellDateFormatted(cell))//日期类型
                        {
                            DateTime dateTime = cell.DateCellValue;
                            obj = dateTime.ToShortDateString();//转换为短日期类型
                        }
                        else//其他数字类型
                        {
                            obj = cell.NumericCellValue;
                        }
                        //obj = cell.NumericCellValue;
                        break;
                    case CellType.String:
                        obj = cell.StringCellValue;
                        break;
                    case CellType.Formula:
                        obj = cell.CellFormula;
                        break;
                    default:
                        obj = "";
                        break;
                }
            }
            catch (Exception ex)
            {
                string message = string.Format("获取表格{0}行{1}列的出错，{2}", rowIdx, colIdx, ex.Message);
                MapSoftLog.LogError(message, ex);
            }
            return obj;
        }

        /// <summary>
        /// 判断文件是否存在及是否是Excle文件
        /// </summary>
        /// <param name="filepath">Excel文件路径</param>
        /// <returns>True：正确；false：错误</returns>
        private bool IsExcelFile(string filepath)
        {
            if (!File.Exists(filepath)) return false;

            string extension = System.IO.Path.GetExtension(filepath);
            if (extension.ToUpper() == ".XLS" || extension.ToUpper() == ".XLSX") return true;
            else return false;
        }

        #endregion
        /// <summary>
        /// 判断字符串是否为数字，若为数字则保留2位小数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string JudgeDouble(string value)
        {
            string rstValue = value;
            if (!string.IsNullOrEmpty(rstValue) && Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$"))
            {
                double dValue = Convert.ToDouble(value);
                if (dValue == 0)
                    rstValue = "0";
                else
                    rstValue = dValue.ToString("0.00");
            }
            return rstValue;
        }
        //public class Range
        //{
        //    private int rowStartIdx, colStartIdx, rowEndIdx, colEndIdx;

        //    public void Merge()
        //    { 

        //    }
        //}
        /// <summary>
        /// 将数字转为Excel列字母
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string ExcelNumToLetter(int index)
        {
            if (index < 0) { throw new Exception("invalid parameter"); }
            List<string> chars = new List<string>();
            do
            {
                if (chars.Count > 0) index--;
                chars.Insert(0, ((char)(index % 26 + (int)'A')).ToString());
                index = (int)((index - index % 26) / 26);
            } while (index > 0);
            return String.Join(string.Empty, chars.ToArray());
        }
    }

    #region 枚举

    /// <summary>
    /// Excel文件格式枚举
    /// </summary>
    public enum ExcelFormat
    {
        /// <summary>
        /// Excel 2003
        /// </summary>
        XLS = 0,
        /// <summary>
        /// Excel 2007
        /// </summary>
        XLSX = 1
    }

    ///// <summary>
    ///// 颜色
    ///// </summary>
    //public class NPOIColors : IndexedColors
    //{
    //    public NPOIColors()
    //    { 

    //    }
    //}

    #endregion

}
