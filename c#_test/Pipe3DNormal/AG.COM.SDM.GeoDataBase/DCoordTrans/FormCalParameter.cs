using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 坐标参数计算窗体类
    /// </summary>
    public partial class FormCalParameter : Form
    {
        public FormCalParameter()
        {
            InitializeComponent();
        }

        private void FormCalParameter_Load(object sender, EventArgs e)
        {
            //添加源X坐标列
            DataGridViewTextBoxColumn tDataGridViewColumn = new DataGridViewTextBoxColumn();
            tDataGridViewColumn.HeaderText = "源X坐标";
            tDataGridViewColumn.ValueType = typeof(Double);
            tDataGridViewColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            tDataGridViewColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dtgXY.Columns.Add(tDataGridViewColumn);

            //添加源Y坐标列
            tDataGridViewColumn = new DataGridViewTextBoxColumn();
            tDataGridViewColumn.HeaderText = "源Y坐标";
            tDataGridViewColumn.ValueType = typeof(Double);
            tDataGridViewColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            tDataGridViewColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dtgXY.Columns.Add(tDataGridViewColumn);

            //添加目标X坐标列
            tDataGridViewColumn = new DataGridViewTextBoxColumn();
            tDataGridViewColumn.HeaderText = "目标X坐标";
            tDataGridViewColumn.ValueType = typeof(Double);
            tDataGridViewColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            tDataGridViewColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dtgXY.Columns.Add(tDataGridViewColumn);

            //添加目标Y坐标列
            tDataGridViewColumn = new DataGridViewTextBoxColumn();
            tDataGridViewColumn.HeaderText = "目标X坐标";
            tDataGridViewColumn.ValueType = typeof(Double);
            tDataGridViewColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            tDataGridViewColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dtgXY.Columns.Add(tDataGridViewColumn);
        }

        private void dtgXY_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (this.dtgXY.RowCount > 0)
                this.btnSave.Enabled = true;
            else
                this.btnSave.Enabled = false;
        }

        private void dtgXY_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (btnSave.Enabled == false)
            {
                this.btnSave.Enabled = true;
            }
        }

        private void dtgXY_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //添加序列号
            using (SolidBrush brush = new SolidBrush(this.dtgXY.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1, CultureInfo.CurrentUICulture), e.InheritedRowStyle.Font, brush, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        private void dtgXY_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception.GetType() == typeof(System.FormatException))
            {
                MessageBox.Show(string.Format("{0},请输入数字型文本格式的字符串", e.Exception.Message));
                e.Cancel = false;
            }
        }      

        private void btnCalParameter_Click(object sender, EventArgs e)
        {
            if (this.dtgXY.RowCount < 3)
            {
                MessageBox.Show("至少需要三对有效的坐标控制点！", "提示");
                return ;
            }

            IPoint[] tFromPoints = new IPoint[this.dtgXY.Rows.Count - 1];
            IPoint[] tToPoints = new IPoint[this.dtgXY.Rows.Count - 1];

            //源/目标坐标点设置
            for (int i = 0; i < tFromPoints.Length; i++)
            {
                tFromPoints[i] = new PointClass();
                tFromPoints[i].X = Convert.ToDouble(dtgXY.Rows[i].Cells[0].Value);
                tFromPoints[i].Y = Convert.ToDouble(dtgXY.Rows[i].Cells[1].Value);

                tToPoints[i] = new PointClass();
                tToPoints[i].X = Convert.ToDouble(dtgXY.Rows[i].Cells[2].Value);
                tToPoints[i].Y = Convert.ToDouble(dtgXY.Rows[i].Cells[3].Value);
            }

            //从控制点定义仿射变换程式
            ITransformation tTransformation = GetAffineTransformation(tFromPoints, tToPoints);

            IAffineTransformation2D tAffineTransformation2D = tTransformation as IAffineTransformation2D;

            txtXTranslation.Text = Convert.ToString(tAffineTransformation2D.XTranslation);
            txtYTranslation.Text = Convert.ToString(tAffineTransformation2D.YTranslation);
            txtScale.Text = Convert.ToString(tAffineTransformation2D.XScale);
            txtRotation.Text = Convert.ToString(tAffineTransformation2D.Rotation); 
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog tSaveFileDialog = new SaveFileDialog();
            tSaveFileDialog.Title = "选择文件保存路径";
            tSaveFileDialog.Filter = "坐标文件(*.txt)|*.txt";

            if (tSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter tStreamWriter = new StreamWriter(tSaveFileDialog.FileName))
                {
                    StringBuilder tStrBuilder = new StringBuilder();
                    tStrBuilder.AppendFormat("X偏移量:{0}\r\n", txtXTranslation.Text);
                    tStrBuilder.AppendFormat("Y偏移量:{0}\r\n", txtYTranslation.Text);
                    tStrBuilder.AppendFormat("缩放因子:{0}\r\n", txtScale.Text);
                    tStrBuilder.AppendFormat("旋转角度:{0}\r\n", txtRotation.Text);
                    tStreamWriter.Write(tStrBuilder.ToString());                   
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXYFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog tFileDlg = new OpenFileDialog();
            tFileDlg.Filter = "坐标对映射文件(*.txt)|*.txt";
            tFileDlg.Title = "请选择坐标控制点文件";
            tFileDlg.Multiselect = false;

            if (tFileDlg.ShowDialog() == DialogResult.OK)
            {
                this.txtXYFile.Text = tFileDlg.FileName;

                //从指定文件中读取坐标控制点
                ReadXYControlPoints(tFileDlg.FileName);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog tSaveFileDialog = new SaveFileDialog();
            tSaveFileDialog.Title = "选择文件保存路径";
            tSaveFileDialog.Filter = "坐标文件(*.txt)|*.txt";

            if (tSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter tStreamWriter = new StreamWriter(tSaveFileDialog.FileName))
                {
                    for (int i = 0; i < this.dtgXY.Rows.Count; i++)
                    {
                        DataGridViewRow tDtRow = this.dtgXY.Rows[i];
                        if (tDtRow.IsNewRow == true) continue;
                        for (int j = 0; j < this.dtgXY.Columns.Count; j++)
                        {
                            tStreamWriter.Write(tDtRow.Cells[j].Value);
                            if (j == this.dtgXY.Columns.Count - 1)
                            {
                                tStreamWriter.Write("\r\n");
                            }
                            else
                            {
                                tStreamWriter.Write(',');
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 从指定文件路径中读取控制点
        /// </summary>
        /// <param name="pfilepath">指定的文件路径</param>
        private void ReadXYControlPoints(string pfilepath)
        {
            //如果文件不存在
            if (!File.Exists(pfilepath))
            {
                MessageBox.Show("坐标控制点文件路径不存在!", "提示");
                return;
            }

            //清除所有行
            this.dtgXY.Rows.Clear();

            bool IsValid = true;

            //使用using语句得到文件流
            using (StreamReader tStreamReader = File.OpenText(pfilepath))
            {
                string input;

                while ((input = tStreamReader.ReadLine()) != null)
                {
                    if (input.Trim().Length == 0) continue;

                    //文件格式以逗号作为分隔符
                    string[] strXY = input.Split(',');

                    if (strXY.Length != 4)
                    {
                        IsValid = false;
                        break;
                    }

                    this.dtgXY.Rows.Add(strXY);
                }
            }

            if (IsValid == false)
            {
                MessageBox.Show(string.Format("[{0}]为无效的坐标仿射文件", pfilepath), "提示");
            }
        }

        /// <summary>
        /// 从控制点定义仿射变换程式
        /// </summary>
        /// <param name="pFromPoints">源控制点</param>
        /// <param name="pToPoints">目标控制点</param>
        /// <returns>返回变换定义</returns>
        private ITransformation GetAffineTransformation(IPoint[] pFromPoints, IPoint[] pToPoints)
        {
            //实例化仿射变换对象
            IAffineTransformation2D3GEN tAffineTransformation = new AffineTransformation2DClass();
            //从源控制点定义参数
            tAffineTransformation.DefineFromControlPoints(ref pFromPoints, ref pToPoints);

            //查询引用接口
            ITransformation tTransformation = tAffineTransformation as ITransformation;

            return tTransformation;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            GJ2DFCoordTransformation tGJ2DF = new GJ2DFCoordTransformation();
            tGJ2DF.Scale = Convert.ToDouble(txtScale.Text);
            tGJ2DF.XTranslation = Convert.ToDouble(txtXTranslation.Text);
            tGJ2DF.YTranslation = Convert.ToDouble(txtYTranslation.Text);
            tGJ2DF.RotationAngle = Convert.ToDouble(txtRotation.Text);

            IPoint tPoint = new PointClass();
            tPoint.X = Convert.ToDouble(txtX0.Text);
            tPoint.Y = Convert.ToDouble(txtY0.Text);

            IPoint tPoint2 = tGJ2DF.GeometryTransform(tPoint as IGeometry) as IPoint;
            txtX1.Text = Convert.ToString(tPoint2.X);
            txtY1.Text = Convert.ToString(tPoint2.Y);
        } 
    }
}