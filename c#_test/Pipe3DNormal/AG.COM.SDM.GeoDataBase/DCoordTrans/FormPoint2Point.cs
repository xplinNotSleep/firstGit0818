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
            //实例化对象
            OpenFileDialog tOpenFileDialog = new OpenFileDialog();
            tOpenFileDialog.Title = "选择源坐标文件";
            tOpenFileDialog.Filter = "坐标文件(*.prj)|*.prj";
            tOpenFileDialog.InitialDirectory = AG.COM.SDM.Utility.CommonConstString.STR_PreAppPath;
            tOpenFileDialog.Multiselect = false;
            tOpenFileDialog.RestoreDirectory = true;

            if (tOpenFileDialog.ShowDialog() == DialogResult.OK)
            {              
                string strSrcPrj = tOpenFileDialog.FileName;               
                //空间投影(To)
                ISpatialReferenceFactory pSpatialFactory = new SpatialReferenceEnvironmentClass();
                ISpatialReference tSrcSpatial = pSpatialFactory.CreateESRISpatialReferenceFromPRJFile(strSrcPrj);
                this.txtSrcPrj.Tag = tSrcSpatial;
                this.txtSrcPrj.Text = tSrcSpatial.Name;
            }
        }

        private void btnToPrj_Click(object sender, EventArgs e)
        {
            //实例化对象
            OpenFileDialog tOpenFileDialog = new OpenFileDialog();
            tOpenFileDialog.Title = "选择输出坐标文件";
            tOpenFileDialog.Filter = "坐标文件(*.prj)|*.prj";
            tOpenFileDialog.InitialDirectory = AG.COM.SDM.Utility.CommonConstString.STR_PreAppPath;
            tOpenFileDialog.Multiselect = false;
            tOpenFileDialog.RestoreDirectory = true;

            if (tOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                string strToPrj = tOpenFileDialog.FileName;              
                //空间投影(To)
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
                MessageBox.Show("源坐标系或目标坐标系不能为空", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.txtSrcX.Text.Length == 0 || this.txtSrcY.Text.Length == 0)
            {
                MessageBox.Show("请输入目标坐标点", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                //设置转换方式
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