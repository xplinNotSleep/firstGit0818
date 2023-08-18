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
    public partial class FormRenameVersion : Form
    {
        private IVersion m_VersionCurrent = null;

        public FormRenameVersion(IVersion tVersionCurrent)
        {
            InitializeComponent();

            m_VersionCurrent = tVersionCurrent;
            string pVersion = m_VersionCurrent.VersionName;
            if (pVersion.Contains("."))
            {
                string[] pVersions = pVersion.Split('.');
                pVersion = pVersions[pVersions.Length - 1];
            }
            txtName.Text = pVersion;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ///数据验证
                if (Valid() == false) return;

                m_VersionCurrent.VersionName = txtName.Text;

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
