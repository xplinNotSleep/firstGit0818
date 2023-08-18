using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
namespace AG.COM.SDM.GeoDataBase.VersionManager.UI
{
    public partial class UCVersionProp : UserControl
    {
        private IVersion m_VersionCurrent = null;
        public IVersion VersionCurrent
        {
            get { return m_VersionCurrent; }
            set { m_VersionCurrent = value;
                if (m_VersionCurrent != null)
                {
                    Show();
                }
            }
        }
        public UCVersionProp()
        {
            InitializeComponent();
        }

        private void UCVersionProp_Load(object sender, EventArgs e)
        {
         
        }

        private void Show()
        {
            try
            {
                if (m_VersionCurrent == null) return;
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

            }
        }
    }
}
