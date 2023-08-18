using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.Catalog;
using AG.COM.SDM.Catalog.DataItems;
using AG.COM.SDM.Catalog.Filters;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// �ռ�ο� ������
    /// </summary>
    public partial class SpatialReferenceDialog:Form 
    {
        private ISpatialReference m_SpatialReference;
        private EnumDataSRType m_DataSRType = EnumDataSRType.DatasetSR;  

        /// <summary>
        /// ��ȡ�����õ�ǰ�������ݵ�����
        /// </summary>
        public EnumDataSRType DataSRType
        {
            get
            {
                return this.m_DataSRType;
            }
            set
            {
                this.m_DataSRType = value;
            }
        }

        /// <summary>
        /// ��ȡ�����ÿռ�ο���ϵ
        /// </summary>
        public ISpatialReference SpatialReference
        {
            get
            {
                return this.m_SpatialReference;
            }
            set
            {
                this.m_SpatialReference = value;
            }
        }

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public SpatialReferenceDialog()
        {
            //��ʼ���������
            InitializeComponent();
        }

        #region ҳ��ؼ��¼�
        private void SpatialReferenceDialog_Load(object sender, EventArgs e)
        {
            //��������ҳ����ʾ���
            if (this.m_DataSRType == EnumDataSRType.FeatureClassSR)
            {
                this.tabControl1.TabPages.Clear();
                this.tabControl1.TabPages.Add(this.tabPage1);
                this.tabControl1.TabPages.Add(this.tabPage2);
            }
            else if (this.m_DataSRType == EnumDataSRType.ShapeSR)
            {
                this.tabControl1.TabPages.Clear();
                this.tabControl1.TabPages.Add(this.tabPage1);
            }

            if (this.m_SpatialReference == null)
            {
                this.m_SpatialReference = new UnknownCoordinateSystemClass();
                //����Ĭ�Ϸֱ���
                SetDefaultResolution();
            }

            if (m_SpatialReference.Name == "Unknown")
            {
                this.btnClear.Enabled = false;
            }
            
            //��ʼ�����������Text
            InitialControlText();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.m_DataSRType != EnumDataSRType.ShapeSR)
                {
                    //��ѯISpatialReferenceResolution�ӿ�
                    ISpatialReferenceResolution tSRResolution = this.m_SpatialReference as ISpatialReferenceResolution;
                    //��ȡXY���������ֵ
                    double xMin, xMax, yMin, yMax,xyResolution;
                    xMin = Convert.ToDouble(this.txtXMin.Text);
                    xMax = Convert.ToDouble(this.txtXMax.Text);
                    yMin = Convert.ToDouble(this.txtYMin.Text);
                    yMax = Convert.ToDouble(this.txtYMax.Text);
                    xyResolution = Convert.ToDouble(this.txtXPrecision.Text);
                    //����XY��
                    this.m_SpatialReference.SetDomain(xMin, xMax, yMin, yMax);
                    //����XY��ֱ���
                    tSRResolution.set_XYResolution(false, xyResolution);

                    if (this.m_DataSRType == EnumDataSRType.DatasetSR)
                    {
                        //��ȡM���ֵ
                        double mMin, mMax, mResolution;
                        mMin = Convert.ToDouble(this.txtMMin.Text);
                        mMax = Convert.ToDouble(this.txtMMax.Text);
                        mResolution = Convert.ToDouble(this.txtMPrecision.Text);
                        //����M��
                        this.m_SpatialReference.SetMDomain(mMin, mMax);
                        //����M��ֱ���
                        tSRResolution.MResolution = mResolution;                 

                        //����Z��
                        double zMin, zMax, zResolution;
                        zMin = Convert.ToDouble(this.txtZMin.Text);
                        zMax = Convert.ToDouble(this.txtZMax.Text);
                        zResolution = Convert.ToDouble(this.txtZPrecision.Text);
                        //����Z��
                        this.m_SpatialReference.SetZDomain(zMin, zMax);
                        //����Z��ֱ���
                        tSRResolution.set_ZResolution(false,zResolution);
                    }
                }

                this.btnApply.Enabled = false;               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }      
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.btnApply_Click(sender, e);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.m_SpatialReference = new UnknownCoordinateSystemClass();
            this.btnClear.Enabled = false;
            this.btnSaveAs.Enabled = false;

            //����Ĭ�Ϸֱ���
            SetDefaultResolution();
            //��ʼ�����������Text
            InitialControlText();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog tOpenFileDlg = new OpenFileDialog();
            tOpenFileDlg.Title = "ѡ�������ļ�";
            tOpenFileDlg.Filter = "�����ļ�(*.prj)|*.prj";
            tOpenFileDlg.InitialDirectory =AG.COM.SDM.Utility.CommonConstString.STR_PreAppPath;
            tOpenFileDlg.RestoreDirectory = true;

            if (tOpenFileDlg.ShowDialog() == DialogResult.OK)
            {
                ISpatialReferenceFactory tSpatialFactory = new SpatialReferenceEnvironmentClass();
                m_SpatialReference = tSpatialFactory.CreateESRISpatialReferenceFromPRJFile(tOpenFileDlg.FileName);
                this.btnSaveAs.Enabled = true;
            }
            //����Ĭ�Ϸֱ���
            SetDefaultResolution();
            //��ʼ�����������Text
            InitialControlText();
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog tSaveFileDlg = new SaveFileDialog();
            tSaveFileDlg.Title = "����������Ϣ";
            tSaveFileDlg.Filter = "�����ļ�(*.prj)|*.prj";
            tSaveFileDlg.RestoreDirectory = true;

            if (tSaveFileDlg.ShowDialog() == DialogResult.OK)
            {
                ISpatialReferenceFactory tSpatialReferenceFactory = new SpatialReferenceEnvironmentClass();
                tSpatialReferenceFactory.ExportESRISpatialReferenceToPRJFile(tSaveFileDlg.FileName, this.m_SpatialReference);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                //ʵ���������������ʵ��
                AG.COM.SDM.Catalog.IDataBrowser tIDataBrowser = new FormDataBrowser();
                //���ɶ�ѡ
                tIDataBrowser.MultiSelect = false;
                //��ӹ�����
                tIDataBrowser.AddFilter(new FeatureClassFilter());   

                if (tIDataBrowser.ShowDialog() == DialogResult.OK)
                {
                    //�������Ϊæµ״̬
                    this.Cursor = Cursors.WaitCursor;

                    IList<DataItem> items = tIDataBrowser.SelectedItems;
                    if (items.Count > 0)
                    {
                        //��ѯ�ӿ�����
                        IGeoDataset tGeoDataset = items[0].GetGeoObject() as IGeoDataset;
                        if (tGeoDataset != null)
                        {
                            this.m_SpatialReference = tGeoDataset.SpatialReference;
                            //��ʼ���������
                            InitialControlText();
                        }
                    }

                    //���ָ�����״̬
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region �Զ���˽�з���

        /// <summary>
        /// ��ʼ�����������Text
        /// </summary>
        private void InitialControlText()
        {
            //��ѯISpatialReferenceResolution�ӿ�
            ISpatialReferenceResolution tSRResolution = this.m_SpatialReference as ISpatialReferenceResolution;

            #region ��ʼ��XY��
            if (this.m_SpatialReference.HasXYPrecision() == true)
            {
                double xMin, xMax, yMin, yMax;
                this.m_SpatialReference.GetDomain(out xMin, out xMax, out yMin, out yMax);
                this.txtXMin.Text = Convert.ToString(xMin);
                this.txtXMax.Text = Convert.ToString(xMax);
                this.txtYMin.Text = Convert.ToString(yMin);
                this.txtYMax.Text = Convert.ToString(yMax);
                this.txtXPrecision.Text = Convert.ToString(tSRResolution.get_XYResolution(false));
            }
            #endregion

            #region ��ʼM��
            if (this.m_SpatialReference.HasMPrecision() == true)
            {
                double MMin, MMax;
                this.m_SpatialReference.GetMDomain(out MMin, out MMax);
                this.txtMMin.Text = Convert.ToString(MMin);
                this.txtMMax.Text = Convert.ToString(MMax);
                this.txtMPrecision.Text = Convert.ToString(tSRResolution.MResolution);
            }
            #endregion

            #region ��ʼ��Z��
            if (this.m_SpatialReference.HasZPrecision() == true)
            {
                double zMin, zMax;
                this.m_SpatialReference.GetZDomain(out zMin, out zMax);
                this.txtZMin.Text = Convert.ToString(zMin);
                this.txtZMax.Text = Convert.ToString(zMax);
                this.txtZPrecision.Text = Convert.ToString(tSRResolution.get_ZResolution(false));
            }
            #endregion

            this.txtName.Text = m_SpatialReference.Name;
            this.txtDetailInfo.Text = GetSpatialRefrenceDetail();
        }

        /// <summary>
        /// ����Ĭ�Ϸֱ���
        /// </summary>
        private void SetDefaultResolution()
        {
            //��ѯIControlPrecision2�ӿ�
            IControlPrecision2 tControlPrecision = this.m_SpatialReference as IControlPrecision2;
            //�����侫��Ϊ�;���
            tControlPrecision.IsHighPrecision = true;// false;

            //��ѯISpatialReferenceResolution�ӿ�
            ISpatialReferenceResolution tSpatialReferenceResolution = this.m_SpatialReference as ISpatialReferenceResolution;
            tSpatialReferenceResolution.ConstructFromHorizon();
            //����XY�ֱ���
            tSpatialReferenceResolution.SetDefaultXYResolution();
            //����M�ֱ���
            tSpatialReferenceResolution.SetDefaultMResolution();
            //����Z�ֱ���
            tSpatialReferenceResolution.SetDefaultZResolution();
        }

        /// <summary>
        /// ��ȡ�ռ�ο�����ϸ��Ϣ
        /// </summary>
        /// <param name="pSpatialReference">�ռ�ο�</param>
        /// <returns>���ؿռ�ο���ϸ��Ϣ</returns>
        private string GetSpatialRefrenceDetail()
        {
            StringBuilder strSRDetail = new StringBuilder();
            IGeographicCoordinateSystem tGeoCoordSystem = m_SpatialReference as IGeographicCoordinateSystem;

            try
            {
                if (m_SpatialReference is IUnknownCoordinateSystem)
                {
                    //δ֪����ϵ�����
                    strSRDetail.Append("δ֪����ϵͳ:");
                }
                else if (m_SpatialReference is IProjectedCoordinateSystem)
                {
                    //��ѯIProjectedCoordinateSystem�ӿ�
                    IProjectedCoordinateSystem tProjectCoordSystem = this.m_SpatialReference as IProjectedCoordinateSystem;

                    //���������Ϣ
                    strSRDetail.Append("ͶӰ����ϵͳ:\r\n");
                    strSRDetail.Append(string.Format("����:{0}\r\n", tProjectCoordSystem.Name));
                    strSRDetail.Append(string.Format("����:{0}\r\n", tProjectCoordSystem.Alias));
                    strSRDetail.Append(string.Format("���:{0}\r\n", tProjectCoordSystem.Abbreviation));
                    strSRDetail.Append(string.Format("ͶӰ:{0}\r\n", tProjectCoordSystem.Projection.Name));
                    strSRDetail.Append(string.Format("ע��:{0}\r\n", tProjectCoordSystem.Remarks));
                    strSRDetail.Append("����\r\n");


                    //�õ�ͶӰ����ϵͳ�Ĳ�����Ϣ
                    //�������null�ᱨ������ʵ��������
                    //�����Сͨ��esriSRLimitsEnum.esriSR_MaxParameterCount��ȡ
                    IParameter[] tParameters = new ParameterClass[(int)esriSRLimitsEnum.esriSR_MaxParameterCount];
                    //IParameter[] tParameters = null;
                    //�õ����ķ��ͽӿ�
                    IProjectedCoordinateSystem4GEN tProjectSystemGen = tProjectCoordSystem as IProjectedCoordinateSystem4GEN;
                    tProjectSystemGen.GetParameters(ref tParameters);
                    
                    //׷��ÿһ������Ϣ
                    foreach (IParameter tParam in tParameters)
                    {
                        if (tParam == null) break;
                        strSRDetail.Append(string.Format("   {0}: {1}\r\n", tParam.Name, tParam.Value));
                    }

                    strSRDetail.Append(string.Format("���Ե�λ:{0}({1})\r\n\r\n", tProjectCoordSystem.CoordinateUnit.Name, tProjectCoordSystem.CoordinateUnit.MetersPerUnit));

                    //�õ�����������Ϣ
                    tGeoCoordSystem = tProjectCoordSystem.GeographicCoordinateSystem;
                }

                if (tGeoCoordSystem != null)
                {
                    //׷�ӵ�����Ϣ����
                    strSRDetail.Append("��������ϵͳ:\r\n");
                    strSRDetail.Append(string.Format("����:{0}\r\n", tGeoCoordSystem.Name));
                    strSRDetail.Append(string.Format("����:{0}\r\n", tGeoCoordSystem.Alias));
                    strSRDetail.Append(string.Format("���:{0}\r\n", tGeoCoordSystem.Abbreviation));
                    strSRDetail.Append(string.Format("ע��:{0}\r\n", tGeoCoordSystem.Remarks));
                    strSRDetail.Append(string.Format("�ǵ�λ:{0}({1})\r\n", tGeoCoordSystem.CoordinateUnit.Name, tGeoCoordSystem.CoordinateUnit.RadiansPerUnit));
                    strSRDetail.Append(string.Format("����������:{0}({1})\r\n", tGeoCoordSystem.PrimeMeridian.Name, tGeoCoordSystem.PrimeMeridian.Longitude));
                    strSRDetail.Append(string.Format("��׼��:{0}\r\n", tGeoCoordSystem.Datum.Name));
                    strSRDetail.Append(string.Format("  ������:{0}\r\n", tGeoCoordSystem.Datum.Spheroid.Name));
                    strSRDetail.Append(string.Format("\t������:{0}\r\n", tGeoCoordSystem.Datum.Spheroid.SemiMajorAxis));
                    strSRDetail.Append(string.Format("\t�̰���:{0}\r\n", tGeoCoordSystem.Datum.Spheroid.SemiMinorAxis));
                    strSRDetail.Append(string.Format("\tƫ����:{0}\r\n", tGeoCoordSystem.Datum.Spheroid.Flattening));
                }

                return strSRDetail.ToString();
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// ���������ַ��Ƿ����Ҫ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidateNumberChars(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            try
            {
                double AAA = Convert.ToDouble(textBox.Text);
                textBox.Tag = textBox.Text;
            }
            catch
            {
                MessageBox.Show("������һ����Ч������");
                textBox.Text = (string)textBox.Tag;
            }
        }

        private void BtnApplyChanged(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
            if (m_SpatialReference.Name != "Unknown")
            {
                this.btnClear.Enabled = true;
            }
        }       
        #endregion 

    }
}