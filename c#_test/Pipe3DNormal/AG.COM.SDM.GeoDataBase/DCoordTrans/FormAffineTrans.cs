using AG.COM.SDM.Catalog;
using AG.COM.SDM.Catalog.DataItems;
using AG.COM.SDM.Catalog.Filters;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 坐标仿射变换 窗体类
    /// </summary>
    public partial class FormAffineTrans : Form
    {
        private IWorkspace m_OutWorkspace;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FormAffineTrans()
        {
            InitializeComponent();
        }

        private void FormAffineTrans_Load(object sender, EventArgs e)
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

        private void listSrcFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listSrcFiles.SelectedItems.Count > 0)
                this.btnDelete.Enabled = true;
            else
                this.btnDelete.Enabled = false;
        }

        private void chkOnSrcFile_CheckedChanged(object sender, EventArgs e)
        {
            bool IsChecked = this.chkOnSrcFile.Checked;
            this.txtOutWorkspace.Enabled = !IsChecked;
            this.btnOutWorkspace.Enabled = !IsChecked;
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
                        for(int j=0;j<this.dtgXY.Columns.Count;j++)
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.listSrcFiles.SelectedItems.Count > 0)
            {
                ListViewItem objSel = this.listSrcFiles.SelectedItems[0];
                if (objSel != null)
                {
                    this.listSrcFiles.Items.Remove(objSel);
                    if (this.listSrcFiles.Items.Count == 0) 
                        this.btnClear.Enabled = false;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.listSrcFiles.Items.Clear();
            this.btnClear.Enabled = false;
            this.btnDelete.Enabled = false;
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

        private void btnOpenSrcFile_Click(object sender, EventArgs e)
        {
            //设置过滤条件
            IDataItemFilter tDataItemFilter = new FeatureClassFilter();
            //实例化数据浏览窗体实例
            IDataBrowser tIDataBrowser = new FormDataBrowser();            
            //不可多选
            tIDataBrowser.MultiSelect = true;
            //添加过滤器
            tIDataBrowser.AddFilter(tDataItemFilter);

            if (tIDataBrowser.ShowDialog() == DialogResult.OK)
            {
                IList<DataItem> items = tIDataBrowser.SelectedItems;
                for (int i = 0; i < items.Count; i++)
                {
                    IDataset tDataset = items[i].GetGeoObject() as IDataset;
                    if (tDataset != null)
                    {
                        string strDatasetName = string.Format("{0}\\{1}", tDataset.Workspace.PathName, tDataset.Name);

                        //实例化ListViewItem对象
                        ListViewItem tListViewItem = new ListViewItem();
                        tListViewItem.Text = strDatasetName;
                        tListViewItem.Tag = tDataset;
                        //添加项
                        this.listSrcFiles.Items.Add(tListViewItem);
                    }
                }

                if (this.listSrcFiles.Items.Count > 0)
                {
                    this.btnDelete.Enabled = true;
                    this.btnClear.Enabled = true;
                }
            }
        }

        private void btnOutWorkspace_Click(object sender, EventArgs e)
        {
            //实例化数据浏览窗体实例
            IDataBrowser tIDataBrowser = new FormDataBrowser();
            tIDataBrowser.AddFilter(new WorkspaceFilter());
            tIDataBrowser.AddFilter(new FolderFilter());

            //不可多选
            tIDataBrowser.MultiSelect = false;

            if (tIDataBrowser.ShowDialog() == DialogResult.OK)
            {
                IList<DataItem> items = tIDataBrowser.SelectedItems;
                if (items.Count == 0) return;

                IWorkspace tWorkspace = items[0].GetGeoObject() as IWorkspace;
                if (tWorkspace != null)
                {
                    this.txtOutWorkspace.Text = tWorkspace.PathName;
                    this.m_OutWorkspace = tWorkspace;
                }
            } 
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //数据未有效，则返回
            if (CheckValid() == false) return;

            IPoint[] tFromPoints = new IPoint[this.dtgXY.Rows.Count-1];
            IPoint[] tToPoints = new IPoint[this.dtgXY.Rows.Count-1];

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
            ITransformation tTransformation = GetAffineTransformation(tFromPoints,tToPoints);

            //实例化进度条对话框
            ITrackProgress tProgressDialog = new TrackProgressDialog();
            tProgressDialog.DisplayTotal = true;
            tProgressDialog.TotalMax = this.listSrcFiles.Items.Count;
            (tProgressDialog as Form).TopMost = true;
            tProgressDialog.Show();

            try
            {
                //查询IWorkspaceEdit接口
                IWorkspaceEdit tWorkspaceEdit = this.m_OutWorkspace as IWorkspaceEdit;

                for (int j = 0; j < this.listSrcFiles.Items.Count; j++)
                {
                    if (tProgressDialog.IsContinue == false) return;

                    //开始编辑操作
                    tWorkspaceEdit.StartEditing(true);
                    tWorkspaceEdit.StartEditOperation();

                    ListViewItem tListItem = this.listSrcFiles.Items[j];
                    IFeatureClass tFromFeatureClass = tListItem.Tag as IFeatureClass;

                    tProgressDialog.TotalMessage = string.Format("正在处理[{0}]要素类……", tFromFeatureClass.AliasName);
                    tProgressDialog.TotalValue = j;

                    if (this.chkOnSrcFile.Checked == false)
                    {
                        //删除工作空间中指定名称的要素类
                        if (GeoDBHelper.HasDeleteFile(this.m_OutWorkspace, (tFromFeatureClass as IDataset).Name) == false) continue;    

                        //创建目标要素类
                        IFeatureClass tToFeatureClass = GeoDBHelper.CreateFeatureClass(this.m_OutWorkspace, (tFromFeatureClass as IDataset).Name, esriFeatureType.esriFTSimple, tFromFeatureClass.Fields, null, null, "");

                        //空间对象转换
                        Transform2D(true, tFromFeatureClass, tToFeatureClass, tTransformation, tProgressDialog);
                    }
                    else
                    {
                        //空间对象转换
                        Transform2D(false, tFromFeatureClass, null, tTransformation, tProgressDialog);
                    }

                    //停止编辑并保存修改
                    tWorkspaceEdit.StopEditOperation();
                    tWorkspaceEdit.StopEditing(true);
                }

                tProgressDialog.TotalValue = this.listSrcFiles.Items.Count;
                tProgressDialog.TotalMessage = "转换已成功完成……";         
            }
            catch (Exception ex)
            {
                (tProgressDialog as Form).TopMost = false;
                MessageBox.Show(ex.Message, "错误提示！", MessageBoxButtons.OK, MessageBoxIcon.Error);           
            }
            finally
            {
                tProgressDialog.AutoFinishClose = true;
                //标识完成状态
                tProgressDialog.SetFinish();
                //关闭窗体
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
        /// 数据有效性检查
        /// </summary>
        /// <returns>如果全部符合要求则返回 true,否则返回 false</returns>
        private bool CheckValid()
        {
            if (this.dtgXY.RowCount < 3)
            {
                MessageBox.Show("至少需要三对有效的坐标控制点！","提示");
                return false;
            }

            if (this.listSrcFiles.Items.Count == 0)
            {
                MessageBox.Show("请选择需要转换的源数据文件!", "提示");
                return false;
            }

            if (this.chkOnSrcFile.Checked == false && this.m_OutWorkspace == null)
            {
                MessageBox.Show("请选择数据输出工作空间!", "提示");
                return false;
            }

            return true;
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

        /// <summary>
        /// 空间二维对象转换
        /// </summary>
        /// <param name="IsInsert">是否插入,插入为true,否则为 false</param>
        /// <param name="pFromFeatClass">源要素类</param>
        /// <param name="pToFeatClass">目标要素类</param>
        /// <param name="pTransformation">转换定义</param>
        /// <param name="pTrackProgressDlg">进度对话框</param>
        private void Transform2D(bool IsInsert, IFeatureClass pFromFeatClass, IFeatureClass pToFeatClass, ITransformation pTransformation, ITrackProgress pTrackProgressDlg)
        {           
            if (IsInsert == true && pToFeatClass == null)
            {
                throw (new Exception("当变量IsInsert为true的情况下,pToFeatClass不允许为空."));               
            }

            //设置子进度条显示范围的最大值
            pTrackProgressDlg.SubMax = pFromFeatClass.FeatureCount(null);
            int curvalue = 0; 

            if (IsInsert == true)
            {                
                //获取源要素所有要素的游标
                IFeatureCursor tFromFeatCursor = pFromFeatClass.Search(null, false);

                //获取插入游标
                IFeatureCursor tToFeatCursor = pToFeatClass.Insert(true);
                //创建要素缓存
                IFeatureBuffer tToFeatBuffer = pToFeatClass.CreateFeatureBuffer();

                for (IFeature pFromFeat = tFromFeatCursor.NextFeature(); pFromFeat != null; pFromFeat = tFromFeatCursor.NextFeature())
                {
                    //判断运行状态
                    if (pTrackProgressDlg.IsContinue == false) return;

                    //得到克隆的空间对象
                    IGeometry tGeometry = pFromFeat.ShapeCopy;
                    //查询接口引用
                    ITransform2D tTransform2D = tGeometry as ITransform2D;
                    tTransform2D.Transform(esriTransformDirection.esriTransformForward, pTransformation);
                    tToFeatBuffer.Shape = tGeometry;

                    //属性赋值
                    for (int i = 0; i < pFromFeat.Fields.FieldCount; i++)
                    {
                        IField pField = pFromFeat.Fields.get_Field(i);
                        if (pField.Type != esriFieldType.esriFieldTypeOID && pField.Type != esriFieldType.esriFieldTypeGUID
                            && pField.Type != esriFieldType.esriFieldTypeGlobalID && pField.Type != esriFieldType.esriFieldTypeGeometry)
                        {
                            object objValue = pFromFeat.get_Value(i);
                            if (objValue.ToString().Length > 0)
                            {
                                int fieldIndex = tToFeatBuffer.Fields.FindField(pField.Name);

                                if (fieldIndex == -1)
                                {
                                    //输出为Shape文件的情况
                                    fieldIndex = tToFeatBuffer.Fields.FindField(pField.Name.Substring(0, 10));
                                }

                                tToFeatBuffer.set_Value(fieldIndex, objValue);
                            }                    
                        }
                    }

                    //新增要素
                    tToFeatCursor.InsertFeature(tToFeatBuffer);

                    curvalue++;
                    pTrackProgressDlg.SubValue = curvalue;
                    pTrackProgressDlg.SubMessage = "正在处理第" + curvalue.ToString() + "条记录";

                    Application.DoEvents();
                }
            }
            else
            {
                //获取源要素类所有要素的更新游标
                IFeatureCursor tFromUpdateCursor = pFromFeatClass.Update(null, false);

                for (IFeature tFromFeat = tFromUpdateCursor.NextFeature(); tFromFeat != null; tFromFeat = tFromUpdateCursor.NextFeature())
                {
                    if (pTrackProgressDlg.IsContinue == false) return;

                    //得到当前要素几何克隆对象
                    IGeometry tGeometry = tFromFeat.ShapeCopy;
                    ITransform2D tTransform2D = tGeometry as ITransform2D;
                    tTransform2D.Transform(esriTransformDirection.esriTransformForward, pTransformation);

                    tFromFeat.Shape = tGeometry;
                    //更新当前要素
                    tFromUpdateCursor.UpdateFeature(tFromFeat);

                    curvalue++;
                    pTrackProgressDlg.SubValue = curvalue;
                    pTrackProgressDlg.SubMessage = "正在处理第" + curvalue.ToString() + "条记录";

                    Application.DoEvents();
                }
            }
        }
    
    }
}