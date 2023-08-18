using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase.VersionManager
{
    public partial class FormVersionProp : Form
    {
        private IVersion m_VersionCurrent = null;

        public FormVersionProp(IVersion tVersionCurrent)
        {
            InitializeComponent();

            m_VersionCurrent = tVersionCurrent;
        }

        private void FormVersionProp_Load(object sender, EventArgs e)
        {
            try
            {
                IVersionInfo tVersionInfo = m_VersionCurrent.VersionInfo;

                string[] split = m_VersionCurrent.VersionName.Split('.');
                lblName.Text = split[1];
                lblOwner.Text = split[0];
                if (m_VersionCurrent.HasParent() == true)
                {
                    lblParent.Text = tVersionInfo.Parent.VersionName;
                }
                         
                if (tVersionInfo.Created is DateTime)
                {
                    DateTime dt = (DateTime)tVersionInfo.Created;
                    lblCreateDate.Text = dt.ToString();
                }
                if (tVersionInfo.Modified is DateTime)
                {
                    DateTime dt = (DateTime)tVersionInfo.Modified;
                    lblCreateDate.Text = dt.ToString();
                }

                txtDescption.Text = m_VersionCurrent.Description;
                if (m_VersionCurrent.Access == esriVersionAccess.esriVersionAccessPrivate)
                    rdoPrivate.Checked = true;
                else if (m_VersionCurrent.Access == esriVersionAccess.esriVersionAccessProtected)
                    rdoProtect.Checked = true;
                else
                    rdoPublic.Checked = true;
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
                Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                m_VersionCurrent.Description = txtDescption.Text;
                if (rdoPrivate.Checked == true)
                    m_VersionCurrent.Access = esriVersionAccess.esriVersionAccessPrivate;
                else if (rdoProtect.Checked == true)
                    m_VersionCurrent.Access = esriVersionAccess.esriVersionAccessProtected;
                else
                    m_VersionCurrent.Access = esriVersionAccess.esriVersionAccessPublic;

                this.DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }       
    }
}
