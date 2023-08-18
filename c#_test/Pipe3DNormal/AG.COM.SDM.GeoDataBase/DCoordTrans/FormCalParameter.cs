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
    /// ����������㴰����
    /// </summary>
    public partial class FormCalParameter : Form
    {
        public FormCalParameter()
        {
            InitializeComponent();
        }

        private void FormCalParameter_Load(object sender, EventArgs e)
        {
            //���ԴX������
            DataGridViewTextBoxColumn tDataGridViewColumn = new DataGridViewTextBoxColumn();
            tDataGridViewColumn.HeaderText = "ԴX����";
            tDataGridViewColumn.ValueType = typeof(Double);
            tDataGridViewColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            tDataGridViewColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dtgXY.Columns.Add(tDataGridViewColumn);

            //���ԴY������
            tDataGridViewColumn = new DataGridViewTextBoxColumn();
            tDataGridViewColumn.HeaderText = "ԴY����";
            tDataGridViewColumn.ValueType = typeof(Double);
            tDataGridViewColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            tDataGridViewColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dtgXY.Columns.Add(tDataGridViewColumn);

            //���Ŀ��X������
            tDataGridViewColumn = new DataGridViewTextBoxColumn();
            tDataGridViewColumn.HeaderText = "Ŀ��X����";
            tDataGridViewColumn.ValueType = typeof(Double);
            tDataGridViewColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            tDataGridViewColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dtgXY.Columns.Add(tDataGridViewColumn);

            //���Ŀ��Y������
            tDataGridViewColumn = new DataGridViewTextBoxColumn();
            tDataGridViewColumn.HeaderText = "Ŀ��X����";
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
            //������к�
            using (SolidBrush brush = new SolidBrush(this.dtgXY.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1, CultureInfo.CurrentUICulture), e.InheritedRowStyle.Font, brush, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        private void dtgXY_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception.GetType() == typeof(System.FormatException))
            {
                MessageBox.Show(string.Format("{0},�������������ı���ʽ���ַ���", e.Exception.Message));
                e.Cancel = false;
            }
        }      

        private void btnCalParameter_Click(object sender, EventArgs e)
        {
            if (this.dtgXY.RowCount < 3)
            {
                MessageBox.Show("������Ҫ������Ч��������Ƶ㣡", "��ʾ");
                return ;
            }

            IPoint[] tFromPoints = new IPoint[this.dtgXY.Rows.Count - 1];
            IPoint[] tToPoints = new IPoint[this.dtgXY.Rows.Count - 1];

            //Դ/Ŀ�����������
            for (int i = 0; i < tFromPoints.Length; i++)
            {
                tFromPoints[i] = new PointClass();
                tFromPoints[i].X = Convert.ToDouble(dtgXY.Rows[i].Cells[0].Value);
                tFromPoints[i].Y = Convert.ToDouble(dtgXY.Rows[i].Cells[1].Value);

                tToPoints[i] = new PointClass();
                tToPoints[i].X = Convert.ToDouble(dtgXY.Rows[i].Cells[2].Value);
                tToPoints[i].Y = Convert.ToDouble(dtgXY.Rows[i].Cells[3].Value);
            }

            //�ӿ��Ƶ㶨�����任��ʽ
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
            tSaveFileDialog.Title = "ѡ���ļ�����·��";
            tSaveFileDialog.Filter = "�����ļ�(*.txt)|*.txt";

            if (tSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter tStreamWriter = new StreamWriter(tSaveFileDialog.FileName))
                {
                    StringBuilder tStrBuilder = new StringBuilder();
                    tStrBuilder.AppendFormat("Xƫ����:{0}\r\n", txtXTranslation.Text);
                    tStrBuilder.AppendFormat("Yƫ����:{0}\r\n", txtYTranslation.Text);
                    tStrBuilder.AppendFormat("��������:{0}\r\n", txtScale.Text);
                    tStrBuilder.AppendFormat("��ת�Ƕ�:{0}\r\n", txtRotation.Text);
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
            tFileDlg.Filter = "�����ӳ���ļ�(*.txt)|*.txt";
            tFileDlg.Title = "��ѡ��������Ƶ��ļ�";
            tFileDlg.Multiselect = false;

            if (tFileDlg.ShowDialog() == DialogResult.OK)
            {
                this.txtXYFile.Text = tFileDlg.FileName;

                //��ָ���ļ��ж�ȡ������Ƶ�
                ReadXYControlPoints(tFileDlg.FileName);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog tSaveFileDialog = new SaveFileDialog();
            tSaveFileDialog.Title = "ѡ���ļ�����·��";
            tSaveFileDialog.Filter = "�����ļ�(*.txt)|*.txt";

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
        /// ��ָ���ļ�·���ж�ȡ���Ƶ�
        /// </summary>
        /// <param name="pfilepath">ָ�����ļ�·��</param>
        private void ReadXYControlPoints(string pfilepath)
        {
            //����ļ�������
            if (!File.Exists(pfilepath))
            {
                MessageBox.Show("������Ƶ��ļ�·��������!", "��ʾ");
                return;
            }

            //���������
            this.dtgXY.Rows.Clear();

            bool IsValid = true;

            //ʹ��using���õ��ļ���
            using (StreamReader tStreamReader = File.OpenText(pfilepath))
            {
                string input;

                while ((input = tStreamReader.ReadLine()) != null)
                {
                    if (input.Trim().Length == 0) continue;

                    //�ļ���ʽ�Զ�����Ϊ�ָ���
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
                MessageBox.Show(string.Format("[{0}]Ϊ��Ч����������ļ�", pfilepath), "��ʾ");
            }
        }

        /// <summary>
        /// �ӿ��Ƶ㶨�����任��ʽ
        /// </summary>
        /// <param name="pFromPoints">Դ���Ƶ�</param>
        /// <param name="pToPoints">Ŀ����Ƶ�</param>
        /// <returns>���ر任����</returns>
        private ITransformation GetAffineTransformation(IPoint[] pFromPoints, IPoint[] pToPoints)
        {
            //ʵ��������任����
            IAffineTransformation2D3GEN tAffineTransformation = new AffineTransformation2DClass();
            //��Դ���Ƶ㶨�����
            tAffineTransformation.DefineFromControlPoints(ref pFromPoints, ref pToPoints);

            //��ѯ���ýӿ�
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