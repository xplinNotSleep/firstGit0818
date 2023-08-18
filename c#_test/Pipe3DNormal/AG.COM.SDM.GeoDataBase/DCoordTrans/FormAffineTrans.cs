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
    /// �������任 ������
    /// </summary>
    public partial class FormAffineTrans : Form
    {
        private IWorkspace m_OutWorkspace;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public FormAffineTrans()
        {
            InitializeComponent();
        }

        private void FormAffineTrans_Load(object sender, EventArgs e)
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

        private void btnOpenSrcFile_Click(object sender, EventArgs e)
        {
            //���ù�������
            IDataItemFilter tDataItemFilter = new FeatureClassFilter();
            //ʵ���������������ʵ��
            IDataBrowser tIDataBrowser = new FormDataBrowser();            
            //���ɶ�ѡ
            tIDataBrowser.MultiSelect = true;
            //��ӹ�����
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

                        //ʵ����ListViewItem����
                        ListViewItem tListViewItem = new ListViewItem();
                        tListViewItem.Text = strDatasetName;
                        tListViewItem.Tag = tDataset;
                        //�����
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
            //ʵ���������������ʵ��
            IDataBrowser tIDataBrowser = new FormDataBrowser();
            tIDataBrowser.AddFilter(new WorkspaceFilter());
            tIDataBrowser.AddFilter(new FolderFilter());

            //���ɶ�ѡ
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
            //����δ��Ч���򷵻�
            if (CheckValid() == false) return;

            IPoint[] tFromPoints = new IPoint[this.dtgXY.Rows.Count-1];
            IPoint[] tToPoints = new IPoint[this.dtgXY.Rows.Count-1];

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
            ITransformation tTransformation = GetAffineTransformation(tFromPoints,tToPoints);

            //ʵ�����������Ի���
            ITrackProgress tProgressDialog = new TrackProgressDialog();
            tProgressDialog.DisplayTotal = true;
            tProgressDialog.TotalMax = this.listSrcFiles.Items.Count;
            (tProgressDialog as Form).TopMost = true;
            tProgressDialog.Show();

            try
            {
                //��ѯIWorkspaceEdit�ӿ�
                IWorkspaceEdit tWorkspaceEdit = this.m_OutWorkspace as IWorkspaceEdit;

                for (int j = 0; j < this.listSrcFiles.Items.Count; j++)
                {
                    if (tProgressDialog.IsContinue == false) return;

                    //��ʼ�༭����
                    tWorkspaceEdit.StartEditing(true);
                    tWorkspaceEdit.StartEditOperation();

                    ListViewItem tListItem = this.listSrcFiles.Items[j];
                    IFeatureClass tFromFeatureClass = tListItem.Tag as IFeatureClass;

                    tProgressDialog.TotalMessage = string.Format("���ڴ���[{0}]Ҫ���࡭��", tFromFeatureClass.AliasName);
                    tProgressDialog.TotalValue = j;

                    if (this.chkOnSrcFile.Checked == false)
                    {
                        //ɾ�������ռ���ָ�����Ƶ�Ҫ����
                        if (GeoDBHelper.HasDeleteFile(this.m_OutWorkspace, (tFromFeatureClass as IDataset).Name) == false) continue;    

                        //����Ŀ��Ҫ����
                        IFeatureClass tToFeatureClass = GeoDBHelper.CreateFeatureClass(this.m_OutWorkspace, (tFromFeatureClass as IDataset).Name, esriFeatureType.esriFTSimple, tFromFeatureClass.Fields, null, null, "");

                        //�ռ����ת��
                        Transform2D(true, tFromFeatureClass, tToFeatureClass, tTransformation, tProgressDialog);
                    }
                    else
                    {
                        //�ռ����ת��
                        Transform2D(false, tFromFeatureClass, null, tTransformation, tProgressDialog);
                    }

                    //ֹͣ�༭�������޸�
                    tWorkspaceEdit.StopEditOperation();
                    tWorkspaceEdit.StopEditing(true);
                }

                tProgressDialog.TotalValue = this.listSrcFiles.Items.Count;
                tProgressDialog.TotalMessage = "ת���ѳɹ���ɡ���";         
            }
            catch (Exception ex)
            {
                (tProgressDialog as Form).TopMost = false;
                MessageBox.Show(ex.Message, "������ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Error);           
            }
            finally
            {
                tProgressDialog.AutoFinishClose = true;
                //��ʶ���״̬
                tProgressDialog.SetFinish();
                //�رմ���
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
        /// ������Ч�Լ��
        /// </summary>
        /// <returns>���ȫ������Ҫ���򷵻� true,���򷵻� false</returns>
        private bool CheckValid()
        {
            if (this.dtgXY.RowCount < 3)
            {
                MessageBox.Show("������Ҫ������Ч��������Ƶ㣡","��ʾ");
                return false;
            }

            if (this.listSrcFiles.Items.Count == 0)
            {
                MessageBox.Show("��ѡ����Ҫת����Դ�����ļ�!", "��ʾ");
                return false;
            }

            if (this.chkOnSrcFile.Checked == false && this.m_OutWorkspace == null)
            {
                MessageBox.Show("��ѡ��������������ռ�!", "��ʾ");
                return false;
            }

            return true;
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

        /// <summary>
        /// �ռ��ά����ת��
        /// </summary>
        /// <param name="IsInsert">�Ƿ����,����Ϊtrue,����Ϊ false</param>
        /// <param name="pFromFeatClass">ԴҪ����</param>
        /// <param name="pToFeatClass">Ŀ��Ҫ����</param>
        /// <param name="pTransformation">ת������</param>
        /// <param name="pTrackProgressDlg">���ȶԻ���</param>
        private void Transform2D(bool IsInsert, IFeatureClass pFromFeatClass, IFeatureClass pToFeatClass, ITransformation pTransformation, ITrackProgress pTrackProgressDlg)
        {           
            if (IsInsert == true && pToFeatClass == null)
            {
                throw (new Exception("������IsInsertΪtrue�������,pToFeatClass������Ϊ��."));               
            }

            //�����ӽ�������ʾ��Χ�����ֵ
            pTrackProgressDlg.SubMax = pFromFeatClass.FeatureCount(null);
            int curvalue = 0; 

            if (IsInsert == true)
            {                
                //��ȡԴҪ������Ҫ�ص��α�
                IFeatureCursor tFromFeatCursor = pFromFeatClass.Search(null, false);

                //��ȡ�����α�
                IFeatureCursor tToFeatCursor = pToFeatClass.Insert(true);
                //����Ҫ�ػ���
                IFeatureBuffer tToFeatBuffer = pToFeatClass.CreateFeatureBuffer();

                for (IFeature pFromFeat = tFromFeatCursor.NextFeature(); pFromFeat != null; pFromFeat = tFromFeatCursor.NextFeature())
                {
                    //�ж�����״̬
                    if (pTrackProgressDlg.IsContinue == false) return;

                    //�õ���¡�Ŀռ����
                    IGeometry tGeometry = pFromFeat.ShapeCopy;
                    //��ѯ�ӿ�����
                    ITransform2D tTransform2D = tGeometry as ITransform2D;
                    tTransform2D.Transform(esriTransformDirection.esriTransformForward, pTransformation);
                    tToFeatBuffer.Shape = tGeometry;

                    //���Ը�ֵ
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
                                    //���ΪShape�ļ������
                                    fieldIndex = tToFeatBuffer.Fields.FindField(pField.Name.Substring(0, 10));
                                }

                                tToFeatBuffer.set_Value(fieldIndex, objValue);
                            }                    
                        }
                    }

                    //����Ҫ��
                    tToFeatCursor.InsertFeature(tToFeatBuffer);

                    curvalue++;
                    pTrackProgressDlg.SubValue = curvalue;
                    pTrackProgressDlg.SubMessage = "���ڴ����" + curvalue.ToString() + "����¼";

                    Application.DoEvents();
                }
            }
            else
            {
                //��ȡԴҪ��������Ҫ�صĸ����α�
                IFeatureCursor tFromUpdateCursor = pFromFeatClass.Update(null, false);

                for (IFeature tFromFeat = tFromUpdateCursor.NextFeature(); tFromFeat != null; tFromFeat = tFromUpdateCursor.NextFeature())
                {
                    if (pTrackProgressDlg.IsContinue == false) return;

                    //�õ���ǰҪ�ؼ��ο�¡����
                    IGeometry tGeometry = tFromFeat.ShapeCopy;
                    ITransform2D tTransform2D = tGeometry as ITransform2D;
                    tTransform2D.Transform(esriTransformDirection.esriTransformForward, pTransformation);

                    tFromFeat.Shape = tGeometry;
                    //���µ�ǰҪ��
                    tFromUpdateCursor.UpdateFeature(tFromFeat);

                    curvalue++;
                    pTrackProgressDlg.SubValue = curvalue;
                    pTrackProgressDlg.SubMessage = "���ڴ����" + curvalue.ToString() + "����¼";

                    Application.DoEvents();
                }
            }
        }
    
    }
}