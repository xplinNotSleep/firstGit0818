using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.GeoDataBase.DCoordTrans
{
    public partial class FormPoint2Point : Form
    {
        public FormPoint2Point()
        {
            InitializeComponent();
        }

        private void btnFromPrj_Click(object sender, EventArgs e)
        {
            //ʵ��������
            OpenFileDialog tOpenFileDialog = new OpenFileDialog();
            tOpenFileDialog.Title = "ѡ��Դ�����ļ�";
            tOpenFileDialog.Filter = "�����ļ�(*.prj)|*.prj";
            tOpenFileDialog.InitialDirectory = AG.COM.SDM.Utility.CommonConstString.STR_PreAppPath;
            tOpenFileDialog.Multiselect = false;
            tOpenFileDialog.RestoreDirectory = true;

            if (tOpenFileDialog.ShowDialog() == DialogResult.OK)
            {              
                string strSrcPrj = tOpenFileDialog.FileName;               
                //�ռ�ͶӰ(To)
                ISpatialReferenceFactory pSpatialFactory = new SpatialReferenceEnvironmentClass();
                ISpatialReference tSrcSpatial = pSpatialFactory.CreateESRISpatialReferenceFromPRJFile(strSrcPrj);
                this.txtSrcPrj.Tag = tSrcSpatial;
                this.txtSrcPrj.Text = tSrcSpatial.Name;
            }
        }

        private void btnToPrj_Click(object sender, EventArgs e)
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
                string strToPrj = tOpenFileDialog.FileName;              
                //�ռ�ͶӰ(To)
                ISpatialReferenceFactory pSpatialFactory = new SpatialReferenceEnvironmentClass();
                ISpatialReference tToSpatial = pSpatialFactory.CreateESRISpatialReferenceFromPRJFile(strToPrj);
                this.txtToPrj.Tag = tToSpatial;
                this.txtToPrj.Text = tToSpatial.Name;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtSrcPrj.Text.Length == 0 || this.txtToPrj.Text.Length == 0)
            {
                MessageBox.Show("Դ����ϵ��Ŀ������ϵ����Ϊ��", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.txtSrcX.Text.Length == 0 || this.txtSrcY.Text.Length == 0)
            {
                MessageBox.Show("������Ŀ�������", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                double srcX = Convert.ToDouble(txtSrcX.Text);
                double srcY = Convert.ToDouble(txtSrcY.Text);

                ISpatialReference tFromSpatial = this.txtSrcPrj.Tag as ISpatialReference;
                ISpatialReference tToSpatial = this.txtToPrj.Tag as ISpatialReference;

                IPoint tPoint = new PointClass();
                tPoint.PutCoords(srcX, srcY);
                tPoint.SpatialReference = tFromSpatial;

                //����ת����ʽ
                IGeoTransformation tTrans = new GeocentricTranslationClass();
                tTrans.PutSpatialReferences(tFromSpatial, tToSpatial);

                IGeometry2 tGeometry2 = tPoint as IGeometry2;
                tGeometry2.ProjectEx(tToSpatial, esriTransformDirection.esriTransformForward, tTrans, false, 0, 0);

                IPoint tToPoint = tGeometry2 as IPoint;
                txtToX.Text = Convert.ToString(tToPoint.X);
                txtToY.Text = Convert.ToString(tToPoint.Y);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}