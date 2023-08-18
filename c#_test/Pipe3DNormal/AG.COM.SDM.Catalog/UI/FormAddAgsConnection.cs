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
    /// ���ArcGIS Server���Ӵ��� ����˵��
    /// </summary>
    public partial class FormAddAgsConnection : Form
    {
        #region  ����

        /// <summary>
        /// �����ļ���ַ���޸�ʱ����ⲿ����
        /// </summary>
        public string FilePath
        {
            get;
            set;
        }

        #endregion 

        /// <summary>
        /// ��ʼ�����ArcGIS Server���Ӵ���ʵ������
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
            //        MessageBox.Show("����������ַ��", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }

            //}
            //else
            //{
            //    if (txtLanServer.Text.Trim().Length == 0)
            //    {
            //        MessageBox.Show("��������������ƣ�", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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




                //AE�ӿ��޷�����arcgis server�������ļ�����չ��ags�����˹��ܿ��������ʱ�޷����
                //IWorkspaceFactory tWorkspaceFactory = new IMSWorkspaceFactoryClass();
                IWorkspaceFactory tWorkspaceFactory = new IMSWorkspaceFactoryClass();
                tWorkspaceFactory.Create(Path.GetDirectoryName(FilePath), System.IO.Path.GetFileName(FilePath), tPropertySet, 0);

                this.DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("���Ӵ���" + Environment.NewLine + ex.Message + " " + ex.StackTrace);
            }
        }

        private bool IsValid()
        {
            if (rdoInternet.Checked == true)
            {
                if (string.IsNullOrEmpty(txtUrl.Text))
                {
                    MessageBox.Show("��������ַ����Ϊ��");
                    return false;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtHostName.Text))
                {
                    MessageBox.Show("����������Ϊ��");
                    return false;
                }
            }

            return true;
        }
    }
}