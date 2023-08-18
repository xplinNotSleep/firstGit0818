using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.Utility;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase.VersionManager
{
    public partial class FormNewVersion : Form
    {
        private IVersion m_VersionCurrent = null;

        public FormNewVersion(IVersion tVersionCurrent)
        {
            InitializeComponent();

            m_VersionCurrent = tVersionCurrent;
        }

        private void FormNewVersion_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ///数据验证
                if (Valid() == false) return;
                IVersionedWorkspace tVersionedWorkspace = CommonVariables.DatabaseConfig.Workspace as IVersionedWorkspace;
                IVersion tVersionNew = VersionManager.CreatVersion(tVersionedWorkspace, txtName.Text, m_VersionCurrent.VersionName);// m_VersionCurrent.CreateVersion(txtName.Text);
                tVersionNew.Description = txtDescption.Text;
                if (rdoPrivate.Checked == true)
                    tVersionNew.Access = esriVersionAccess.esriVersionAccessPrivate;
                else if (rdoProtect.Checked == true)
                    tVersionNew.Access = esriVersionAccess.esriVersionAccessProtected;
                else
                    tVersionNew.Access = esriVersionAccess.esriVersionAccessPublic;

                this.DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");           
            }
        }

        /// <summary>
        /// 数据验证
        /// </summary>
        /// <returns></returns>
        private bool Valid()
        {
            return ValidateData.NotNull(txtName.Text, "名称");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }       
    }
}
