using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.ADF.Connection;
using ESRI.ArcGIS.ADF.Connection.IMS;

namespace AG.COM.SDM.Catalog.UI
{
    /// <summary>
    /// 添加ArcIMS地图服务窗体类 描述说明
    /// </summary>
    public partial class FormAddImsService : Form
    {
        /// <summary>
        /// 初始化添加ArcIMS地图服务窗体类实例
        /// </summary>
        public FormAddImsService()
        {
            InitializeComponent();
        }

        private void FormAddImsService_Load(object sender, EventArgs e)
        {
        }

        public IIMSServiceDescription ServiceDescription
        {
            get
            {
                IIMSServiceDescription service = new IMSServiceNameClass();
                service.URL = txtUrl.Text;
                //if (optFeatureService.Checked)
                //    service.ServiceType = acServiceType.acFeatureService;
                //else
                service.ServiceType = acServiceType.acMapService;
                service.Name = cboName.Text;
                //service.UserName = txtUserName.Text;

                //service.Password = txtPassword.Text;

                return service;
            }
            set
            {
                if (value == null)
                    return;
                txtUrl.Text = value.URL;
                cboName.Text = value.Name;

            }
        }

        private void txtTestConn_Click(object sender, EventArgs e)
        {
            if ((txtUrl.Text.Trim().Length == 0) || (txtUrl.Text == @"http://"))
            {
                System.Windows.Forms.MessageBox.Show("请输入服务的地址!", "连接测试", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                IMSServerConnectionFactory f = new IMSServerConnectionFactory();
                //ESRI.ArcGIS.ADF.Identity id = new ESRI.ArcGIS.ADF.Identity("", "", "");
                IServerConnection con = f.CreateServerConnection(txtUrl.Text, null);
                //(con as ESRI.ArcGIS.ADF.Connection.IMS.HTTPConnection).Timeout = 15000;
                (con as ESRI.ArcGIS.ADF.Connection.IMS.HTTPConnection).Timeout = 150;
                IMSServerConnection imscon = con as IMSServerConnection;
                ArrayList services = imscon.ClientServicesArray();
                this.cboName.Items.Clear();
                for (int i = 0; i <= services.Count - 1; i++)
                {
                    cboName.Items.Add(services[i]);
                }

                //bool flag = (ServiceDescription as ESRI.ArcGIS.esriSystem.ITestConnection).TestConnection();
                //if (flag)
                //    System.Windows.Forms.MessageBox.Show("连接成功!", "连接测试", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //else
                //    System.Windows.Forms.MessageBox.Show("连接失败!", "连接测试", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("连接失败!", "连接测试", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (txtUrl.Text.Trim().Length == 0)
            {
                System.Windows.Forms.MessageBox.Show("请输入服务的地址!", "连接测试", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboName.Text.Trim().Length == 0)
            {
                System.Windows.Forms.MessageBox.Show("请输入服务名称!", "连接测试", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            IIMSServiceDescription service = ServiceDescription;
            IMemoryBlobStream stream = new MemoryBlobStreamClass();
            (service as IPersistStream).Save(stream, 1);

            string conFolder = CommonConst.m_ConnectPropertyPath;

            string fn = txtUrl.Text + "_" + cboName.Text;
            fn = fn.ToLower();
            fn = fn.Replace(@"http://", "");
            fn = fn.Replace(':', '_');
            fn = fn.Replace('/', '_');
            fn = fn.Replace(@"\", "_");
            string fn2 = fn + ".myims";
            int index = 0;
            while (System.IO.File.Exists(conFolder + "\\" + fn2))
            {
                index++;
                fn2 = fn + "_" + index.ToString() + ".myims";
            }

            stream.SaveToFile(conFolder + "\\" + fn2);
            this.DialogResult = DialogResult.OK;
        }
    }
}