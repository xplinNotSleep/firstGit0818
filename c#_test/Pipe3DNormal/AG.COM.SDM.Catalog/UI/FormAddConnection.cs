using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.Catalog.UI
{
    /// <summary>
    /// ���ݿ����Ӵ����� ����˵��
    /// </summary>
    public partial class FormAddConnection : Form
    {
        /// <summary>
        /// ��ʼ�����ݿ����Ӵ�����
        /// </summary>
        public FormAddConnection()
        {
            InitializeComponent();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            IPropertySet pset = dataConnectionControl1.ConnectionProperties;
            string server = pset.GetProperty("SERVER").ToString();
            string fn = "Connection to " + server;
            try
            {
                if (pset.GetProperty("VERSION") == null)
                    fn = fn + "_His";
            }
            catch
            {
                fn = fn + "_His";
            }
            int index = 0;
            string fn2;
            string conFolder = CommonConst.m_ConnectPropertyPath;
            if (System.IO.File.Exists(m_FileName) == false)
            {
                fn2 = conFolder + "\\" + fn + ".sde";
                while (System.IO.File.Exists(fn2))
                {
                    index++;
                    fn2 = conFolder + "\\" + fn + "(" + index.ToString() + ")" + ".sde";
                }
            }
            else
            {
                fn2 = m_FileName;
                System.IO.File.Delete(fn2);
            }

            IWorkspaceFactory wsf = new SdeWorkspaceFactoryClass();
            wsf.Create(conFolder, System.IO.Path.GetFileName(fn2), pset, 0);

            this.DialogResult = DialogResult.OK;
        }

        private string m_FileName="";

        /// <summary>
        /// ��ȡ��ָ�����ݿ������ļ�����
        /// </summary>
        [Browsable(false)]
        public string FileName
        {
            get { return m_FileName; }
            set 
            { 
                m_FileName = value;
                if (System.IO.File.Exists(m_FileName) == false)
                {
                    this.Text = "�½��ռ�����";
                    return;
                }
                else
                {
                    this.Text = "�༭�ռ�����";
                }
                IWorkspaceFactory wsf = new SdeWorkspaceFactoryClass();
                IPropertySet pset = wsf.ReadConnectionPropertiesFromFile(m_FileName);
                this.dataConnectionControl1.ConnectionProperties = pset;                              
            }
        }         
    }
}