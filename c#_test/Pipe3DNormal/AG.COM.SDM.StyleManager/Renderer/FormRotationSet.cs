using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AG.COM.SDM.Utility.Display;
using AG.COM.SDM.Utility.Wrapers;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.StyleManager.Renderer
{
    public partial class FormRotationSet : Form
    {
        private IGeoFeatureLayer m_GeoFeatureLayer = null;
        private IRotationRenderer m_RotationRenderer = null;

        public IRotationRenderer RotationRenderer
        {
            get { return m_RotationRenderer; }
            set { m_RotationRenderer = value; }
        }

        public FormRotationSet(IGeoFeatureLayer pGeoFeatureLayer)
        {
            InitializeComponent();
            m_GeoFeatureLayer = pGeoFeatureLayer;
        }

        private void FormRotationSet_Load(object sender, EventArgs e)
        {
            this.cboUVFieldFirst.Items.Clear();
            //初始化字段
            IList<IField> fields = CommonFunction.GetNumbleField(m_GeoFeatureLayer.FeatureClass.Fields);
            int i;
            for (i = 0; i <= fields.Count - 1; i++)
            {
                this.cboUVFieldFirst.Items.Add(new FieldWrapper(fields[i]));
            }            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                FieldWrapper tFieldWrapper = (cboUVFieldFirst.SelectedItem as FieldWrapper);
                if (tFieldWrapper == null) return;

                m_RotationRenderer.RotationField = tFieldWrapper.Field.Name;
                if (this.rbtnGeo.Checked == true)
                {
                    m_RotationRenderer.RotationType = esriSymbolRotationType.esriRotateSymbolGeographic;
                }
                else if (this.rbtnMath.Checked == true)
                {
                    m_RotationRenderer.RotationType = esriSymbolRotationType.esriRotateSymbolArithmetic;
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
