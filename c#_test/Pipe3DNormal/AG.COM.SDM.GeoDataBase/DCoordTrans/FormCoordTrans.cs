using AG.COM.SDM.Catalog;
using AG.COM.SDM.Catalog.DataItems;
using AG.COM.SDM.Catalog.Filters;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// ͨ������ת�� ������
    /// </summary>
    public partial class FormCoordTrans : Form
    {
        private IWorkspace m_OutWorkspace;
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public FormCoordTrans()
        {
            //��ʼ���������
            InitializeComponent();
        }

        private void btnOpenSrcFile_Click(object sender, EventArgs e)
        {              
            //���ù�������
            IDataItemFilter tDataItemFilter = new FeatureClassFilter();
            //ʵ���������������ʵ��
            IDataBrowser tIDataBrowser = new FormDataBrowser();
            tIDataBrowser.AddFilter(new FeatureClassFilter());  

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
                        string strDatasetName=string.Format("{0}\\{1}",tDataset.Workspace.PathName,tDataset.Name);

                        //ʵ����ListViewItem����
                        ListViewItem tListViewItem=new ListViewItem();
                        tListViewItem.Text=strDatasetName;

                        //��ȡ��ռ�ο�����
                        string strSpatRefName=( tDataset as IGeoDataset).SpatialReference.Name;
                        if (string.Compare(strSpatRefName, "Unknown", true) == 0)
                        {
                            tListViewItem.SubItems.Add("��");
                        }
                        else
                        {
                            tListViewItem.SubItems.Add("��");
                        }

                        tListViewItem.Tag=tDataset;
                        //�����
                        this.listFiles.Items.Add(tListViewItem);
                    }                    
                }

                if (items.Count > 0)
                {
                    this.btnDelete.Enabled = true;
                    this.btnClear.Enabled = true;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ListViewItem ListSelItem = this.listFiles.FocusedItem;
            if (ListSelItem != null)
            {
                this.listFiles.Items.Remove(ListSelItem);
                if (listFiles.Items.Count == 0)
                {
                    this.btnDelete.Enabled = false;
                    this.btnClear.Enabled = false;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //���������
            this.listFiles.Items.Clear();
            this.btnDelete.Enabled = false;
            this.btnClear.Enabled = false;
        }

        private void listSrcFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            //����ɾ����ť�Ŀ�����
            if (this.listFiles.SelectedIndices.Count ==0)             
                this.btnDelete.Enabled = false;            
            else             
                this.btnDelete.Enabled = true;           
        }

        private void btnOutPutLocation_Click(object sender, EventArgs e)
        {
            //ʵ���������������ʵ��
            IDataBrowser tIDataBrowser = new FormDataBrowser();
            tIDataBrowser.AddFilter(new WorkspaceFilter());

            //���ɶ�ѡ
            tIDataBrowser.MultiSelect = false;

            if (tIDataBrowser.ShowDialog() == DialogResult.OK)
            {
                IList<DataItem> items = tIDataBrowser.SelectedItems;
                if (items.Count == 0) return;

                IWorkspace tWorkspace = items[0].GetGeoObject() as IWorkspace;
                if (tWorkspace != null)
                {
                    this.txtFolderPath.Text = items[0].Name;   
                    this.m_OutWorkspace = tWorkspace;
                }
            } 
        }

        private void btnCoordPrj_Click(object sender, EventArgs e)
        {
            //ʵ��������
            OpenFileDialog tOpenFileDialog = new OpenFileDialog();
            tOpenFileDialog.Title = "ѡ����������ļ�";
            tOpenFileDialog.Filter = "�����ļ�(*.prj)|*.prj";
            tOpenFileDialog.InitialDirectory = AG.COM.SDM.Utility.CommonConstString.STR_PreAppPath;
            tOpenFileDialog.Multiselect = false;         
            tOpenFileDialog.RestoreDirectory = true;             

            if (tOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtSpatialReference.Text = tOpenFileDialog.FileName;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //���������д�Ƿ�淶
            if (CheckValid() == false) return;

            //ʵ�����������Ի���
            TrackProgressDialog tTrackProgressDialog = new TrackProgressDialog();
            //tTrackProgressDialog.DisplayTotal = true;
            tTrackProgressDialog.Show();
          
            //����ͶӰ�任����
            ProjectTransHandler(tTrackProgressDialog);

            tTrackProgressDialog.Close();           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// ���������д�Ƿ�淶
        /// </summary>
        /// <returns>����淶�򷵻� true�����򷵻� false</returns>
        private bool CheckValid()
        { 
            if (this.listFiles.Items.Count == 0)
            {
                MessageBox.Show("��ѡ����Ҫת���������ļ���", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (this.m_OutWorkspace == null)
            {
                MessageBox.Show("��������ռ䲻��Ϊ�գ�", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //if (this.txtFolderPath.Text.Trim().Length == 0)
            //{
            //    MessageBox.Show("��ѡ������ļ�Ŀ¼·����", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}
            //else
            //{
            //    if (System.IO.Directory.Exists(this.txtFolderPath.Text) == false)
            //    {
            //        MessageBox.Show("��������ռ��ļ�Ŀ¼·�������ڣ�", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return false;
            //    }
            //}

            if (this.txtSpatialReference.Text.Trim().Length == 0)
            {
                MessageBox.Show("��ѡ������ռ�ο�����ϵ��", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
            {
                if (System.IO.File.Exists(this.txtSpatialReference.Text) == false)
                {
                    MessageBox.Show("��������ռ�ο�����ϵ�ļ�·�������ڣ�", "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            
            return true;
        }

        /// <summary>
        /// ����ͶӰ�任����
        /// </summary>
        /// <param name="pTrackProgressDlg">�������Ի���</param>
        private void ProjectTransHandler(TrackProgressDialog pTrackProgressDlg)
        {
            try
            {
                foreach (ListViewItem tListItem in listFiles.Items)
                {
                    if (tListItem.SubItems[1].Text == "��") continue;

                    if (pTrackProgressDlg.IsContinue == false) return;

                    //��������ת��
                    IFeatureClass tFromFeatClass = tListItem.Tag as IFeatureClass;  

                    //ɾ�������ռ���ָ�����Ƶ�Ҫ����
                    if (GeoDBHelper.HasDeleteFile(this.m_OutWorkspace, (tFromFeatClass as IDataset).Name) == false) continue;
                    
                    //��ѯIWorkspaceEdit�ӿ�
                    IWorkspaceEdit tWorkspaceEdit = m_OutWorkspace as IWorkspaceEdit;
                    //��ʼ�༭����
                    tWorkspaceEdit.StartEditing(true);
                    tWorkspaceEdit.StartEditOperation();

                    //�ռ�ͶӰ(From)
                    ISpatialReference tFromSpatial =(tFromFeatClass as IGeoDataset).SpatialReference;

                    //�ռ�ͶӰ(To)
                    ISpatialReferenceFactory pSpatialFactory = new SpatialReferenceEnvironmentClass();
                    ISpatialReference tToSpatial = pSpatialFactory.CreateESRISpatialReferenceFromPRJFile(txtSpatialReference.Text);

                    //�����ֶμ�
                    IFields destFields = GetFields(tFromFeatClass, tToSpatial);
                    //�������FeatureClass
                    IFeatureClass tToFeatClass = GeoDBHelper.CreateFeatureClass(m_OutWorkspace, (tFromFeatClass as IDataset).Name, esriFeatureType.esriFTSimple, destFields, null, null, "");

                    //����ת����ʽ
                    IGeoTransformation tTrans = new GeocentricTranslationClass();
                    tTrans.PutSpatialReferences(tFromSpatial, tToSpatial);

                    //ͶӰ�任
                    TranGeometry(tFromFeatClass, tToFeatClass, tTrans, tToSpatial, pTrackProgressDlg);

                    //��ѯ�ӿ�����
                    ISchemaLock tSchemaLock = tToFeatClass as ISchemaLock;
                    if (tSchemaLock != null)
                    {
                        //���ı������״̬
                        tSchemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);
                    }

                    //ֹͣ�༭����
                    tWorkspaceEdit.StopEditOperation();
                    tWorkspaceEdit.StopEditing(true); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "������ʾ!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ����ϵת��
        /// </summary>
        /// <param name="mFromFeatClass">ԴFeatureClass</param>
        /// <param name="mToFeatClass">Ŀ��FeatureClass</param>
        /// <param name="mTrans">ת����ʽ</param>
        /// <param name="mDestSpatial">Ŀ��ͶӰ����</param>
        /// <param name="pTrackProgressDlg">�������Ի���</param>
        private void TranGeometry(IFeatureClass mFromFeatClass, IFeatureClass mToFeatClass, IGeoTransformation mTrans, ISpatialReference mDestSpatial, TrackProgressDialog pTrackProgressDlg)
        {
            //�����ӽ�������ʾ��Χ�����ֵ
            pTrackProgressDlg.SubMax = mFromFeatClass.FeatureCount(null);
            int curvalue = 0;

            //����Ҫ�ػ���
            IFeatureBuffer pFeatBuffer = mToFeatClass.CreateFeatureBuffer();
            IFeatureCursor pToFeatCursor = mToFeatClass.Insert(true);

            //��ȡ��ѯ�α�
            IFeatureCursor pFromCursor = mFromFeatClass.Search(null, false);

            for (IFeature pFromFeat = pFromCursor.NextFeature(); pFromFeat != null;pFromFeat = pFromCursor.NextFeature())
            {
                //�ж�����״̬
                if (pTrackProgressDlg.IsContinue == false) return;

                IGeometry2 pGeometry = pFromFeat.ShapeCopy as IGeometry2;
                pGeometry.ProjectEx(mDestSpatial, esriTransformDirection.esriTransformForward, mTrans, true, 0, 0);

                pFeatBuffer.Shape = pGeometry;

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
                            int fieldIndex = pFeatBuffer.Fields.FindField(pField.Name);

                            if (fieldIndex == -1)
                            {
                                //���ΪShape�ļ������
                                fieldIndex = pFeatBuffer.Fields.FindField(pField.Name.Substring(0, 10));
                            }

                            pFeatBuffer.set_Value(fieldIndex, objValue);
                        }
                    }
                }

                //����Ҫ��
                pToFeatCursor.InsertFeature(pFeatBuffer);

                curvalue++;
                pTrackProgressDlg.SubValue = curvalue;
                pTrackProgressDlg.SubMessage="���ڴ����" + curvalue.ToString() + "����¼";

                Application.DoEvents();
            }
        }

        /// <summary>
        /// ��ȡҪ�������ֶμ�
        /// </summary>
        /// <param name="pSrcFeatureClass">ԴҪ����</param>
        /// <param name="pSpatialReference">�ռ��ϵ</param>
        /// <returns>�����ֶμ�</returns>
        private IFields GetFields(IFeatureClass pSrcFeatureClass, ISpatialReference pSpatialReference)
        {
            IFieldsEdit tFieldsEdit = new FieldsClass();

            IFields pSrcFields = pSrcFeatureClass.Fields;

            for (int i = 0; i < pSrcFields.FieldCount; i++)
            {
                IField tField = pSrcFields.get_Field(i);
                if (tField.Type == esriFieldType.esriFieldTypeGeometry)
                {
                    IGeometryDefEdit tGeometryDefEdit = tField.GeometryDef as IGeometryDefEdit;
                    tGeometryDefEdit.SpatialReference_2 = pSpatialReference;
                }

                tFieldsEdit.AddField(tField);
            }

            return (IFields)tFieldsEdit;
        }
    }
}