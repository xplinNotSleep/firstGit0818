using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace AG.COM.MapSoft.Tool
{
    public class ExcelTMLDriver
    {
        public class CellFormat
        {
            ICellStyle Style;
            IComment Comment;
            CellType Type;
            public CellFormat(ICellStyle style, IComment comment, CellType type)
            {
                Style = style;
                Comment = comment;
                Type = type;
            }
            public CellFormat(ICell cell)
            {
                Style = cell.CellStyle;
                Comment = cell.CellComment;
                Type = cell.CellType;
            }
            public void Format(ICell cell)
            {
                if (cell == null)
                    return;
                cell.CellStyle = Style;
                cell.CellComment = Comment;
                cell.SetCellType(Type);
            }
        }

        IWorkbook m_workbook;
        Dictionary<string, SheetTemplate> m_tml;
        bool m_loaded = false;

        public bool Loaded { get { return m_loaded; } set { m_loaded = value; } }

        public ExcelTMLDriver()
        { }

        public void LoadTemplate(string excelpath)
        {
            m_loaded = false;

            if (!excelpath.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) && !excelpath.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
                throw new Exception("文件后缀名错误,必须为xls或xlsx");
            if (!File.Exists(excelpath))
                throw new Exception("没有找到excel模板文件");
            string tmlpath = excelpath + ".tml";
            if (!File.Exists(tmlpath))
                throw new Exception("没有找到excel模板对应的tml文件");

            XmlDocument tmldoc = XmlDriver.Open(tmlpath);
            if (tmldoc == null)
                throw new Exception("打开tml文件失败");

            m_tml = ParseTML(tmldoc);
            if (m_tml == null)
                throw new Exception("读取tml文件失败");

            using (FileStream stream = new FileStream(excelpath, FileMode.Open, FileAccess.Read))
            {
                m_workbook = WorkbookFactory.Create(stream);
                if (m_workbook == null)
                    throw new Exception("读取excel模板文件失败");
                stream.Close();
            }

            m_loaded = true;
        }

        public static Dictionary<string, SheetTemplate> ParseTML(XmlDocument doc)
        {
            try
            {
                Dictionary<string, SheetTemplate> tml = new Dictionary<string, SheetTemplate>();
                XmlNodeList2 nodes = XmlDriver.Search(doc, "xmltemplate").Search("sheet");
                foreach (var node in nodes)
                {
                    XmlNodeList2 namenodes = XmlDriver.Search(node, "name");
                    if (!namenodes.Any())
                        throw new Exception();

                    XmlNodeList2 fieldnodes = XmlDriver.Search(node, "fieldcolumn").Search("rect");
                    if (!fieldnodes.Any())
                        throw new Exception();

                    XmlNodeList2 datanodes = XmlDriver.Search(node, "datarow").Search("rect");
                    if (!datanodes.Any())
                        throw new Exception();

                    SheetTemplate sheet = new SheetTemplate()
                    {
                        columns = SheetTemplate.FromString(fieldnodes[0].InnerText),
                        rows = SheetTemplate.FromString(datanodes[0].InnerText),
                        vars = new Dictionary<string, VariableCell>()
                    };

                    XmlNodeList2 varnodes = XmlDriver.Search(node, "variables").Search("var");
                    foreach (XmlNode varnode in varnodes)
                    {
                        sheet.vars = new Dictionary<string, VariableCell>();
                        XmlNodeList2 varnames = XmlDriver.Search(varnode, "name");
                        XmlNodeList2 varaddress = XmlDriver.Search(varnode, "loc");
                        XmlNodeList2 varcount = XmlDriver.Search(varnode, "varcount");
                        int varcounts;
                        if (varnames.Any() && varaddress.Any() && varcount.Any() && int.TryParse(varcount[0].InnerText, out varcounts))
                        {
                            sheet.vars.Add(varnames[0].InnerText, new VariableCell() { Address = SheetTemplate.FromString(varaddress[0].InnerText), VarCount = varcounts });
                        }
                    }

                    tml[namenodes[0].InnerText] = sheet;
                }
                return tml;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void WriteFile(string filepath, DataSet dataset)
        {
            if (!m_loaded)
                throw new Exception("没有加载模板");
                        
            foreach (ISheet sheet in m_workbook)
            {
                if (!m_tml.ContainsKey(sheet.SheetName) || !dataset.Tables.Contains(sheet.SheetName))
                    continue;

                SheetTemplate template = m_tml[sheet.SheetName];
                DataTable table = dataset.Tables[sheet.SheetName];

                // Get column mapping
                Dictionary<string, int> fieldDic = GetFieldDic(sheet, template);
                //Get column style
                Dictionary<int, CellFormat> formatDic = new Dictionary<int, CellFormat>();
                int[] colDic = new int[table.Columns.Count];
                for (int idx = 0; idx < colDic.Length; idx++)
                {
                    if (fieldDic.ContainsKey(table.Columns[idx].ColumnName))
                    {
                        colDic[idx] = fieldDic[table.Columns[idx].ColumnName];
                        ICell cell = sheet.GetRow(template.rows.FirstRow).GetCell(colDic[idx]);
                        formatDic[idx] = new CellFormat(cell);
                    }
                    else
                        colDic[idx] = -1;
                }

                // Add Rows
                sheet.ShiftRows(template.rows.FirstRow, sheet.LastRowNum, table.Rows.Count, true, false);
                for (int rowid = 0; rowid < table.Rows.Count; rowid++)
                {
                    int desRowid = template.rows.FirstRow + rowid;
                    IRow sheetrow = sheet.GetRow(desRowid);
                    if (sheetrow == null)
                        sheetrow = sheet.CreateRow(desRowid);
                    //FixMerged(sheet, sheet.LastRowNum, 1);
                    for (int colid = 0; colid < table.Columns.Count; colid++)
                    {
                        int sheetcol = colDic[colid];
                        if (sheetcol < 0)
                            continue;
                        ICell sheetcell = sheetrow.CreateCell(sheetcol);
                        formatDic[colid].Format(sheetcell);
                        object val = table.Rows[rowid][colid];
                        if (val is string)
                        {
                            sheetcell.SetCellValue((string)val);
                        }
                        else if (val is bool)
                        {
                            sheetcell.SetCellValue((bool)val);
                        }
                        else if (val is DateTime)
                        {
                            sheetcell.SetCellValue((DateTime)val);
                        }
                        else if (val is int || val is double || val is float || val is short || val is long || val is byte)
                        {
                            sheetcell.SetCellValue((double)val);
                        }
                    }
                }
            }

            using (FileStream stream = new FileStream(filepath, FileMode.Create, FileAccess.Write))
            {
                m_workbook.Write(stream);
                stream.Close();
            }
        }

        public bool ReadFile(string filepath, out DataSet dataset)
        {
            throw new NotImplementedException();

            dataset = null;
            if (!m_loaded)
                return false;

            return false;
        }

        private static Dictionary<string, int> GetFieldDic(ISheet sheet, SheetTemplate template)
        {
            var merges = GetMerges(sheet);
            Dictionary<string, int> dic = new Dictionary<string, int>();
            for (int col = template.columns.FirstColumn; col <= template.columns.LastColumn; col++)
            {
                List<string> colnames = new List<string>();
                for (int row = template.columns.FirstRow; row <= template.columns.LastRow; row++)
                {
                    ICell cell = sheet.GetRow(row).GetCell(col);
                    bool isMerged;
                    cell = SwitchToMergedParentCell(merges, cell, out isMerged);
                    string colname = cell.StringCellValue;
                    if (isMerged && colnames.Any() && colname == colnames.Last())
                        continue;
                    colnames.Add(colname);
                }
                string fieldname = "";
                foreach (string colname in colnames)
                {
                    fieldname += colname + '|';
                }
                fieldname = fieldname.TrimEnd('|');
                dic[fieldname] = col;
            }
            return dic;
        }

        private static List<CellRangeAddress> GetMerges(ISheet sheet)
        {
            List<CellRangeAddress> ranges = new List<CellRangeAddress>();
            for (int idx = 0; idx < sheet.NumMergedRegions; idx++)
                ranges.Add(sheet.GetMergedRegion(idx));
            return ranges;
        }

        private static ICell SwitchToMergedParentCell(List<CellRangeAddress> merges, ICell cell, out bool isMerged)
        {
            CellRangeAddress merge = merges.FirstOrDefault(p => p.IsInRange(cell.RowIndex, cell.ColumnIndex));
            isMerged = merge != null;
            if (isMerged)
                return cell.Sheet.GetRow(merge.FirstRow).GetCell(merge.FirstColumn);
            return cell;
        }

        #region unused
        /// <summary>
        /// 修正移动行后合并单元格错误的bug
        /// </summary>
        private void FixMerged(ISheet sheet, int startRow, int moveN)
        {
            var merges = GetMerges(sheet);
            //关键逻辑再这个for循环
            foreach (CellRangeAddress merge in merges)
            {
                //这里的8是插入行的index，表示这行之后才重新合并
                if (merge.FirstRow > 8)
                {
                    //你插入了几行就加几，我这里插入了一行，加1
                    int firstRow = merge.FirstRow + 1;
                    CellRangeAddress newMerge = new CellRangeAddress(firstRow, (firstRow + (merge
                            .LastRow - merge.FirstRow)), merge.FirstColumn,
                            merge.LastColumn);
                    sheet.AddMergedRegion(newMerge);
                }
            }
        }

        public static void CopyRegion(ISheet srcSheet, ISheet desSheet, CellRangeAddress region, bool copyValueFlag)
        {
            for (int rowid = region.FirstRow; rowid <= region.LastRow; rowid++)
            {
                IRow srcRow = srcSheet.GetRow(rowid);
                IRow desRow = desSheet.GetRow(rowid);
                for (int colid = region.FirstColumn; colid <= region.LastColumn; colid++)
                    CopyCell(srcRow.GetCell(colid), desRow.GetCell(colid), copyValueFlag);
            }
        }

        public static void CopyCell(ICell srcCell, ICell desCell, bool copyValueFlag)
        {
            desCell.CellStyle = srcCell.CellStyle;
            desCell.CellComment = srcCell.CellComment;
            desCell.SetCellType(srcCell.CellType);
            if (copyValueFlag)
            {
                switch (desCell.CellType)
                {
                    case CellType.Unknown:
                    case CellType.Blank:
                        break;
                    case CellType.Boolean:
                        desCell.SetCellValue(srcCell.BooleanCellValue);
                        break;
                    case CellType.Error:
                        desCell.SetCellValue(srcCell.ErrorCellValue);
                        break;
                    case CellType.Formula:
                        desCell.SetCellFormula(srcCell.CellFormula);
                        break;
                    case CellType.Numeric:
                        if (DateUtil.IsCellDateFormatted(srcCell))
                            desCell.SetCellValue(srcCell.DateCellValue);
                        else
                            desCell.SetCellValue(srcCell.NumericCellValue);
                        break;
                    case CellType.String:
                        desCell.SetCellValue(srcCell.StringCellValue);
                        break;
                }
            }
        }
        #endregion
    }

    public class SheetTemplate
    {
        public CellRangeAddress columns;
        public CellRangeAddress rows;
        public Dictionary<string, VariableCell> vars;

        public static CellRangeAddress FromString(string addressStr)
        {
            string[] twos = addressStr.ToUpper().Split(',');
            if (twos != null && twos.Length > 0)
            {
                int sx, sy;
                AddressToNum(twos[0], out sx, out sy);

                if (twos.Length == 1)
                {
                    return new CellRangeAddress(sy, sy, sx, sx);
                }
                else
                {
                    int ex, ey;
                    AddressToNum(twos[1], out ex, out ey);
                    return new CellRangeAddress(sy, ey, sx, ex);
                }
            }
            else
                return null;
        }

        private static char[] addressNums = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private static void AddressToNum(string address, out int x, out int y)
        {
            int pos = address.IndexOfAny(addressNums);
            string sx = address.Substring(0, pos).Trim();
            x = 0;
            for (int idx = sx.Length - 1; idx >= 0; idx--)
            {
                int wei = (int)Math.Pow(26, (sx.Length - 1 - idx));
                x += (sx[idx] - 'A' + 1) * wei;
            }
            x--;
            string sy = address.Substring(pos).Trim();
            y = int.Parse(sy) - 1;
        }
    }

    public class VariableCell
    {
        public CellRangeAddress Address;
        public int VarCount;
    }
}
