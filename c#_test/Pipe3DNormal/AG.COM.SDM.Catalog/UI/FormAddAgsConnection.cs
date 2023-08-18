using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.GISClient;
using System.IO;

namespace AG.COM.SDM.Catalog.UI
{
    /// <summary>
    /// 添加ArcGIS Server连接窗体 描述说明
    /// </summary>
    public partial class FormAddAgsConnection : Form
    {
        #region  变量

        /// <summary>
        /// 连接文件地址，修改时需从外部传入
        /// </summary>
        public string FilePath
        {
            get;
            set;
        }

        #endregion 

        /// <summary>
        /// 初始化添加ArcGIS Server连接窗体实例对象
        /// </summary>
        public FormAddAgsConnection()
        {
            InitializeComponent();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            //if (optInternet.Checked)
            //{
            //    if ((txtInternetServer.Text.Trim().Length == 0) ||
            //        (txtInternetServer.Text.Trim().ToLower() == @"http://"))
            //    {
            //        MessageBox.Show("请输入服务地址！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }

            //}
            //else
            //{
            //    if (txtLanServer.Text.Trim().Length == 0)
            //    {
            //        MessageBox.Show("请输入服务器名称！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid() == false) return;

                IPropertySet tPropertySet = new PropertySetClass();
                if (rdoInternet.Checked == true)
                {
                    tPropertySet.SetProperty("url", txtUrl.Text);
                    tPropertySet.SetProperty("user", txtUserName.Text == null ? "" : txtUserName.Text);
                    tPropertySet.SetProperty("password", txtPassword.Text == null ? "" : txtPassword.Text);
                }
                else
                {
                    tPropertySet.SetProperty("machine", txtHostName.Text);
                }

                string tFolderPath = CommonConst.m_ConnectPropertyPath;
                if (string.IsNullOrEmpty(FilePath))
                {
                    IAGSServerConnectionFactory tFactory = new AGSServerConnectionFactory();
                    IAGSServerConnection tConnection = tFactory.Open(tPropertySet, 0);
                   
                    IAGSServerConnectionName2 tAGSName = tConnection.FullName as IAGSServerConnectionName2;

                    string tFileNameNoExt = tConnection.Name;
                    tFileNameNoExt = "ddd";
                    string tFileName = Path.Combine(tFolderPath, tFileNameNoExt + ".ags");
                    int i = 1;
                    while (File.Exists(tFileName))
                    {
                        tFileName = Path.Combine(tFolderPath, tFileNameNoExt + "(" + i + ").ags");
                        i++;
                    }

                    FilePath = tFileName;
                }




                //AE接口无法创建arcgis server的连接文件（扩展名ags），此功能卡在这里，暂时无法解决
                //IWorkspaceFactory tWorkspaceFactory = new IMSWorkspaceFactoryClass();
                IWorkspaceFactory tWorkspaceFactory = new IMSWorkspaceFactoryClass();
                tWorkspaceFactory.Create(Path.GetDirectoryName(FilePath), System.IO.Path.GetFileName(FilePath), tPropertySet, 0);

                this.DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接错误。" + Environment.NewLine + ex.Message + " " + ex.StackTrace);
            }
        }

        private bool IsValid()
        {
            if (rdoInternet.Checked == true)
            {
                if (string.IsNullOrEmpty(txtUrl.Text))
                {
                    MessageBox.Show("服务器地址不能为空");
                    return false;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtHostName.Text))
                {
                    MessageBox.Show("主机名不能为空");
                    return false;
                }
            }

            return true;
        }
    }
}