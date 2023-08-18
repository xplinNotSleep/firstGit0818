using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;

namespace AG.COM.SDM.Catalog.UI
{
    /// <summary>
    /// 数据库连接自定义控件 描述说明
    /// </summary>
    public partial class DataConnectionControl : UserControl
    {
        /// <summary>
        /// 初始化数据库连接自定义控件实例对象
        /// </summary>
        public DataConnectionControl()
        {
            InitializeComponent();
        }

        private void optTrans_CheckedChanged(object sender, EventArgs e)
        {
            cboVersions.Enabled = optTrans.Checked;
            pnlHisInfo.Enabled = !optTrans.Checked;

        }

        /// <summary>
        /// 检查输入的属性是否符合要求
        /// </summary>
        /// <returns>如果输入的属性信息符合要求则返回true，否则返回false</returns>
        public bool CheckInput()
        {
            if (txtServer.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入服务器名！", "数据库连接", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (this.txtInstance.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入实例名！", "数据库连接", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (this.txtDatabase.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入数据库名！", "数据库连接", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (this.txtUser.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入用户名！", "数据库连接", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //if (this.txtPassword.Text.Trim().Length == 0)
            //{
            //    MessageBox.Show("请输入密码！", "数据库连接", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //} 

            return true;
        }

        /// <summary>
        /// 获取或设置连接属性
        /// </summary>
        [Browsable(false)]
        public IPropertySet ConnectionProperties
        {
            get
            {
                IPropertySet pset = new PropertySetClass();
                pset.SetProperty("SERVER", txtServer.Text);
                pset.SetProperty("INSTANCE", this.txtInstance.Text);
                pset.SetProperty("DATABASE", txtDatabase.Text);
                pset.SetProperty("USER", txtUser.Text);
                pset.SetProperty("PASSWORD", txtPassword.Text);
                if (optTrans.Checked)
                    pset.SetProperty("VERSION", cboVersions.Text);
                else
                {
                    if (optHisMarker.Checked)
                        pset.SetProperty("HISTORICAL_NAME", cboHisMarkers.Text);
                    else
                        pset.SetProperty("HISTORICAL_TIMESTAMP", dtHisTimestamp.Value);
                }

                return pset;
            }
            set
            {
                object a, b;
                object[] names;// = new string[value.Count];
                object[] values;// = new string[value.Count];
                value.GetAllProperties(out a, out b);
                names = a as object[];
                values = b as object[];
                System.Collections.Hashtable dict = new System.Collections.Hashtable();
                for (int i = 0; i <= names.Length - 1; i++)
                {
                    dict.Add(names[i].ToString().ToUpper(), values[i]);
                }
                if (dict.Contains("SERVER"))
                    txtServer.Text = dict["SERVER"].ToString();
                if (dict.Contains("INSTANCE"))
                    txtInstance.Text = dict["INSTANCE"].ToString();
                if (dict.Contains("DATABASE"))
                    txtDatabase.Text = dict["DATABASE"].ToString();
                if (dict.Contains("USER"))
                    txtUser.Text = dict["USER"].ToString();
                //密码是加密过的，读取不了
                //if (dict.Contains("PASSWORD"))
                //{
                //    byte[] bts = dict["PASSWORD"] as byte[];
                //    string s = System.Text.Encoding.ASCII.GetString(bts);
                //    txtPassword.Text = s;
                //}
                if (dict.Contains("VERSION"))
                {
                    cboVersions.Text = dict["VERSION"].ToString();
                    optTrans.Checked = true;
                    optHistory.Checked = false;
                    pnlHisInfo.Enabled = false;
                }
                else
                {
                    optTrans.Checked = false;
                    optHistory.Checked = true;
                    pnlHisInfo.Enabled = true;

                    if (dict.Contains("HISTORICAL_NAME"))
                    {
                        optHisMarker.Checked = true;
                        optHisTime.Checked = false;
                    }
                    else
                    {
                        optHisMarker.Checked = false;
                        optHisTime.Checked = true;
                    }
                }
            }
        }

        private void optHisMarker_CheckedChanged(object sender, EventArgs e)
        {
            this.cboHisMarkers.Enabled = optHisMarker.Checked;
            this.dtHisTimestamp.Enabled = !optHisMarker.Checked;
        }


        private object m_GeoObject = null;
        private IPropertySet m_PropertySet = null;
        private AutoResetEvent evt;

        /// <summary>
        /// 获取新的空间对象
        /// </summary>
        /// <returns>是否成功连接</returns>
        public bool GetGeoObject_NewThread()
        {
            try
            {
                IWorkspaceFactory wsf = new SdeWorkspaceFactoryClass();
                m_GeoObject = wsf.Open(m_PropertySet, 0);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }


        private void btConnect_Click(object sender, EventArgs e)
        {
            IWorkspace ws;
            bool tIsSucceed = false;

            IPropertySet pset = new PropertySetClass();
            pset.SetProperty("SERVER", txtServer.Text);
            pset.SetProperty("INSTANCE", this.txtInstance.Text);
            pset.SetProperty("DATABASE", txtDatabase.Text);
            pset.SetProperty("USER", txtUser.Text);
            pset.SetProperty("PASSWORD", txtPassword.Text);
            pset.SetProperty("VERSION", "SDE.DEFAULT");
            m_PropertySet = pset;

            tIsSucceed = GetGeoObject_NewThread();

            ws = m_GeoObject as IWorkspace;

            if (ws == null)
                return;

            string tSourceVersion = cboVersions.Text;
            IEnumVersionInfo pEnum = (ws as IVersionedWorkspace).Versions;
            IVersionInfo version = pEnum.Next();
            cboVersions.Items.Clear();
            while (version != null)
            {
                this.cboVersions.Items.Add(version.VersionName);
                version = pEnum.Next();
            }
            if (cboVersions.Items.Count > 0)
                cboVersions.SelectedIndex = 0;
            if (!string.IsNullOrEmpty(tSourceVersion))
                cboVersions.Text = tSourceVersion;

            IEnumHistoricalMarker pEnumHis = (ws as IHistoricalWorkspace).HistoricalMarkers;
            IHistoricalMarker marker = pEnumHis.Next();
            cboHisMarkers.Items.Clear();
            while (marker != null)
            {
                this.cboHisMarkers.Items.Add(marker.Name);
                marker = pEnumHis.Next();
            }

            if (tIsSucceed == true)
                MessageBox.Show("连接成功");
        }

        private void DataConnectionControl_Load(object sender, EventArgs e)
        {
            optTrans_CheckedChanged(sender, e);
            optHisMarker_CheckedChanged(sender, e);
            txtServer_TextChanged(sender, e);

        }

        private void txtServer_TextChanged(object sender, EventArgs e)
        {
            if (txtServer.Text.Trim().Length == 0)
                btConnect.Enabled = false;
            else
                btConnect.Enabled = true;

        }
    }
}
