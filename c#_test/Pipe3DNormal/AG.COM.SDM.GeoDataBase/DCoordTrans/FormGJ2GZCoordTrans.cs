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
    /// ���ҵ���������ת��
    /// </summary>
    public partial class FormGJ2GZCoordTrans : Form
    {
        //��������ռ�
        private IWorkspace m_OutWorkspace;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public FormGJ2GZCoordTrans()
        {
            //��ʼ���������
            InitializeComponent();
        }

        private void FormGJ2GZCoordTrans_Load(object sender, EventArgs e)
        {
            //����54����������ת��������
            GJ2DFCoordTransformation tGJ2DFTransformation = new GJ2DFCoordTransformation();
            tGJ2DFTransformation.TransName = "����54����������ת��";
            tGJ2DFTransformation.XTranslation = 13731.829363131248;
            tGJ2DFTransformation.YTranslation = -29986.14979221805;
            tGJ2DFTransformation.RotationAngle = 0.004840873348379301;
            tGJ2DFTransformation.Scale = 0.999954188;           
            this.combTransType.Items.Add(tGJ2DFTransformation);

            //����54������80����ת��
            tGJ2DFTransformation = new GJ2DFCoordTransformation();
            tGJ2DFTransformation.TransName = "����54������80����ת��";
            tGJ2DFTransformation.XTranslation = -59.086066881827719;
            tGJ2DFTransformation.YTranslation = -59.241670671457541;
            tGJ2DFTransformation.RotationAngle = -0.0000013328012291310875;
            tGJ2DFTransformation.Scale = 1.000012482;
            this.combTransType.Items.Add(tGJ2DFTransformation);


            //����80����������ת��
            tGJ2DFTransformation = new GJ2DFCoordTransformation();
            tGJ2DFTransformation.TransName = "����80����������ת�� ";
            tGJ2DFTransformation.XTranslation = 13790.6244495477;
            tGJ2DFTransformation.YTranslation = -29926.6261804043; 
            tGJ2DFTransformation.RotationAngle = 0.00484220616136227;
            tGJ2DFTransformation.Scale = 0.999941706727689;
            this.combTransType.Items.Add(tGJ2DFTransformation);

            //����80������54����ת��
            tGJ2DFTransformation = new GJ2DFCoordTransformation();
            tGJ2DFTransformation.TransName = "����80������54����ת��";
            tGJ2DFTransformation.XTranslation = 59.085250363841624;
            tGJ2DFTransformation.YTranslation = 59.241009969919105;
            tGJ2DFTransformation.RotationAngle = 0.000001332801229313876;
            tGJ2DFTransformation.Scale = 0.99998752;
            this.combTransType.Items.Add(tGJ2DFTransformation);

            //����80��2000��������ת��
            tGJ2DFTransformation = new GJ2DFCoordTransformation();
            tGJ2DFTransformation.TransName = "����80��2000��������ת��";
            tGJ2DFTransformation.XTranslation = 59.085250363841624;
            tGJ2DFTransformation.YTranslation = 59.241009969919105;
            tGJ2DFTransformation.RotationAngle = 0.000001332801229313876;
            tGJ2DFTransformation.Scale = 0.99998752;
            this.combTransType.Items.Add(tGJ2DFTransformation);

            //����������54����ת��
            tGJ2DFTransformation = new GJ2DFCoordTransformation();
            tGJ2DFTransformation.TransName = "����������54����ת��";
            tGJ2DFTransformation.XTranslation = -13587.132341708118;
            tGJ2DFTransformation.YTranslation = 30053.649027313415;
            tGJ2DFTransformation.RotationAngle = -0.0048408733468931894;
            tGJ2DFTransformation.Scale = 1.000045813;
            this.combTransType.Items.Add(tGJ2DFTransformation);

            //����������80����ת��
            tGJ2DFTransformation = new GJ2DFCoordTransformation();
            tGJ2DFTransformation.TransName = "����������80����ת��";
            tGJ2DFTransformation.XTranslation = -13646.347937015507;
            tGJ2DFTransformation.YTranslation = 29994.800598071102;
            tGJ2DFTransformation.RotationAngle = -0.0048422061463885314;
            tGJ2DFTransformation.Scale = 1.000058296;
            this.combTransType.Items.Add(tGJ2DFTransformation);

            //������2000��������ת��
            tGJ2DFTransformation = new GJ2DFCoordTransformation();
            tGJ2DFTransformation.TransName = "������2000��������ת��";
            tGJ2DFTransformation.XTranslation = -13587.132341708118;
            tGJ2DFTransformation.YTranslation = 30053.649027313415;
            tGJ2DFTransformation.RotationAngle = -0.0048408733468931894;
            tGJ2DFTransformation.Scale = 1.000045813;
            this.combTransType.Items.Add(tGJ2DFTransformation);

            //2000��������ϵ������80����ת��
            tGJ2DFTransformation = new GJ2DFCoordTransformation();
            tGJ2DFTransformation.TransName = "2000��������ϵ������80����ת��";
            tGJ2DFTransformation.XTranslation = -59.086066881827719;
            tGJ2DFTransformation.YTranslation = -59.241670671457541;
            tGJ2DFTransformation.RotationAngle = -0.0000013328012291310875;
            tGJ2DFTransformation.Scale = 1.000012482;
            this.combTransType.Items.Add(tGJ2DFTransformation);

            //2000��������ϵ����������ת��������
            tGJ2DFTransformation = new GJ2DFCoordTransformation();
            tGJ2DFTransformation.TransName = "2000��������ϵ����������ת��";
            tGJ2DFTransformation.XTranslation = 13731.829363131248;
            tGJ2DFTransformation.YTranslation = -29986.14979221805;
            tGJ2DFTransformation.RotationAngle = 0.004840873348379301;
            tGJ2DFTransformation.Scale = 0.999954188;
            this.combTransType.Items.Add(tGJ2DFTransformation);

            this.combTransType.SelectedIndex = 0;
        }

        private void listSrcFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listSrcFiles.SelectedIndices.Count == 0)
            {
                this.btnDelete.Enabled = false;
            }
            else
            {
                this.btnDelete.Enabled = true;
            }
        }

        private void chkOnSrcFile_CheckedChanged(object sender, EventArgs e)
        {
            bool IsChecked = this.chkOnSrcFile.Checked;
            this.txtOutWorkspace.Enabled = !IsChecked;
            this.btnOutWorkspace.Enabled = !IsChecked;
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
                    //this.btnDelete.Enabled = true;
                    this.btnClear.Enabled = true;
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
                    {
                        this.btnClear.Enabled = false;
                        this.btnDelete.Enabled = false;
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.listSrcFiles.Items.Clear();
            this.btnClear.Enabled = false;
            this.btnDelete.Enabled = false;
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
                    this.txtOutWorkspace.Text = tWorkspace.PathName ;
                    this.m_OutWorkspace = tWorkspace;
                }
            }
        } 

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (CheckValid() == false) return;

            //��ȡת������
            GJ2DFCoordTransformation tTransformation=this.combTransType.SelectedItem as GJ2DFCoordTransformation;

            //ʵ�����������Ի���
            ITrackProgress tProgressDialog = new TrackProgressDialog();
            tProgressDialog.DisplayTotal = true;
            tProgressDialog.TotalMax = this.listSrcFiles.Items.Count;
            (tProgressDialog as Form).TopMost = true;
            tProgressDialog.Show();

            try
            {
                for (int i = 0; i < this.listSrcFiles.Items.Count; i++)
                {
                    ListViewItem tListItem = this.listSrcFiles.Items[i];
                    IFeatureClass tFromFeatureClass = tListItem.Tag as IFeatureClass;

                    tProgressDialog.TotalMessage = string.Format("���ڴ���[{0}]Ҫ���࡭��", tFromFeatureClass.AliasName);
                    tProgressDialog.TotalValue = i;

                    Dictionary<int, int> tFieldMatch = null;

                    if (this.chkOnSrcFile.Checked == false)
                    {
                        string tPathTarget = txtOutWorkspace.Text;
                        string tFileNameWithoutExt = (tFromFeatureClass as IDataset).Name;
                        //����Ŀ��Ҫ����
                        IFeatureClass tToFeatureClass = GeoDBHelper.CreateFeatureClass(tFromFeatureClass, this.m_OutWorkspace, tPathTarget, tFileNameWithoutExt, esriFeatureType.esriFTSimple, null, null, "", ref tFieldMatch);

                        //�ռ����ת��
                        Transform2D(true, tFromFeatureClass, tToFeatureClass, tTransformation, tProgressDialog, tFieldMatch);
                    }
                    else
                    {
                        //�ռ����ת��
                        Transform2D(false, tFromFeatureClass, null, tTransformation, tProgressDialog, null);
                    }
                }

                tProgressDialog.TotalValue = this.listSrcFiles.Items.Count;
                tProgressDialog.TotalMessage = "�ѳɹ����ת��������";
                tProgressDialog.SetFinish();
                MessageBox.Show("�������", "��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch(Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                (tProgressDialog as Form).Hide();
                tProgressDialog.SetFinish();
                this.Close();
                MessageBox.Show(ex.Message, "������ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //finally
            //{
            //    //��ʶ�����
            //    tProgressDialog.SetFinish();
            //    this.Close();
            //}        
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// ������Ч�Լ��
        /// </summary>
        /// <returns>���ȫ������Ҫ���򷵻� true,���򷵻� false</returns>
        private bool CheckValid()
        {
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
        /// �ռ��ά����ת��
        /// </summary>
        /// <param name="IsInsert">�Ƿ����,����Ϊtrue,����Ϊ false</param>
        /// <param name="pFromFeatClass">ԴҪ����</param>
        /// <param name="pToFeatClass">Ŀ��Ҫ����</param>
        /// <param name="pTransformation">ת������</param>
        /// <param name="pTrackProgressDlg">���ȶԻ���</param>
        private void Transform2D(bool IsInsert, IFeatureClass tFeatureClassSource, IFeatureClass tFeatureClassTarget, GJ2DFCoordTransformation pTransformation, ITrackProgress pTrackProgressDlg, Dictionary<int, int> tFieldMatch)
        {           
            if (IsInsert == true && tFeatureClassTarget == null)
            {
                throw (new Exception("������IsInsertΪtrue�������,pToFeatClass������Ϊ��."));               
            }

            //�����ӽ�������ʾ��Χ�����ֵ
            pTrackProgressDlg.SubMax = tFeatureClassSource.FeatureCount(null);

            int curvalue = 0;

            if (IsInsert == true)
            {         
                #region ��Ŀ��Ҫ��������Ӽ�¼
                //��ȡԴҪ������Ҫ�ص��α�
                IFeatureCursor tFromFeatCursor = tFeatureClassSource.Search(null, false);

                //��ȡ�����α�
                IFeatureCursor tFeatureCursorInsert = tFeatureClassTarget.Insert(true);
                //����Ҫ�ػ���
                IFeatureBuffer tFeatureBuffer = tFeatureClassTarget.CreateFeatureBuffer();

                for (IFeature tFeatureSource = tFromFeatCursor.NextFeature(); tFeatureSource != null; tFeatureSource = tFromFeatCursor.NextFeature())
                {
                    //�ж�����״̬
                    if (pTrackProgressDlg.IsContinue == false) return;

                    //�õ���¡�Ŀռ����
                    IGeometry tGeometry = tFeatureSource.ShapeCopy;
                    //�ռ����ת��
                    tFeatureBuffer.Shape = pTransformation.GeometryTransform(tGeometry); ;

                    //���Ը�ֵ
                    if (tFieldMatch == null)
                    {
                        for (int i = 0; i < tFeatureSource.Fields.FieldCount; i++)
                        {
                            IField pField = tFeatureSource.Fields.get_Field(i);
                            if (pField.Type != esriFieldType.esriFieldTypeOID && pField.Type != esriFieldType.esriFieldTypeGUID
                                && pField.Type != esriFieldType.esriFieldTypeGlobalID && pField.Type != esriFieldType.esriFieldTypeGeometry)
                            {
                                object objValue = tFeatureSource.get_Value(i);

                                tFeatureBuffer.set_Value(i, objValue);
                            }
                        }
                    }
                    else
                    {
                        IFields tFieldsTarget = tFeatureClassTarget.Fields;
                        //ʹ�ô���FeatureClassʱ���ֶ�ƥ�䣬keyΪ���ֶΣ�valueΪ��Ӧ�ľ��ֶ�
                        foreach (KeyValuePair<int, int> kvp in tFieldMatch)
                        {
                            IField tFieldTarget = tFieldsTarget.get_Field(kvp.Key);
                            //�����ֶ����⸳ֵ�����ɱ༭�ֶβ�����
                            if (tFieldTarget.Type != esriFieldType.esriFieldTypeGeometry && tFieldTarget.Editable == true)
                            {
                                if (tFieldTarget.Type == esriFieldType.esriFieldTypeString)
                                {
                                    try
                                    {
                                        tFeatureBuffer.set_Value(kvp.Key, Convert.ToString(tFeatureSource.get_Value(kvp.Value)));
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }
                                }
                                else if (tFieldTarget.Type == esriFieldType.esriFieldTypeDouble || tFieldTarget.Type == esriFieldType.esriFieldTypeInteger
                                     || tFieldTarget.Type == esriFieldType.esriFieldTypeSingle || tFieldTarget.Type == esriFieldType.esriFieldTypeSmallInteger)
                                {
                                    //shapefile�����������ֶβ���Ϊ�գ�ԭֵΪ��Ҫ��ֵ0
                                    double value = 0;
                                    if (double.TryParse(Convert.ToString(tFeatureSource.get_Value(kvp.Value)), out value) == true)
                                    {
                                        try
                                        {
                                            tFeatureBuffer.set_Value(kvp.Key, value);
                                        }
                                        catch (Exception ex)
                                        {
                                            throw ex;
                                        }
                                    }
                                    else
                                    {
                                        tFeatureBuffer.set_Value(kvp.Key, 0);
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        tFeatureBuffer.set_Value(kvp.Key, tFeatureSource.get_Value(kvp.Value));
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }
                                }
                            }
                        }
                    }

                    //����Ҫ��
                    tFeatureCursorInsert.InsertFeature(tFeatureBuffer);

                    curvalue++;
                    pTrackProgressDlg.SubValue = curvalue;
                    pTrackProgressDlg.SubMessage = "���ڴ����" + curvalue.ToString() + "����¼";

                    Application.DoEvents();
                }

                //�ͷŶ���
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursorInsert); 
                #endregion
            }
            else
            {
                #region ����ԴҪ���༸�ζ���
                //��ȡԴҪ��������Ҫ�صĸ����α�
                IFeatureCursor tFromUpdateCursor = tFeatureClassSource.Update(null, false);

                for (IFeature tFromFeat = tFromUpdateCursor.NextFeature(); tFromFeat != null; tFromFeat = tFromUpdateCursor.NextFeature())
                {
                    if (pTrackProgressDlg.IsContinue == false) return;

                    //�õ���ǰҪ�ؼ��ο�¡����
                    IGeometry tGeometry = tFromFeat.Shape;
                    tFromFeat.Shape = pTransformation.GeometryTransform(tGeometry);                   

                    //���µ�ǰҪ��
                    tFromUpdateCursor.UpdateFeature(tFromFeat);
         
                    curvalue++;
                    pTrackProgressDlg.SubValue = curvalue;
                    pTrackProgressDlg.SubMessage = "���ڴ����" + curvalue.ToString() + "����¼";

                    Application.DoEvents();
                }

                tFromUpdateCursor.Flush();

                //�ͷŶ���
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFromUpdateCursor); 
                #endregion
            }
        }      
    }
}