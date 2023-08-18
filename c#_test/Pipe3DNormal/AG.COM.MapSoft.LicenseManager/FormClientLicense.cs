using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using AG.COM.MapSoft.Tool;

namespace AG.COM.MapSoft.LicenseManager
{
    /// <summary>
    /// 许可证界面
    /// </summary>
    public partial class FormClientLicense : Form
    {
        public bool RegistResult = false;   //判断软件是否注册成功的变量

        public FormClientLicense()
        {
            InitializeComponent();
            RegistResult = false;
        }

        private void FormClientLicense_Load(object sender, EventArgs e)
        {
            try
            {
                lblName.Text = LimitDateHelper.SoftwareName;
                //txtName.Text = LimitDateHelper.SoftwareName;
            }
            catch (Exception ex)
            {
                MapSoftLog.LogError(ex.Message.ToString());
                MessageHandler.ShowErrorMsg(ex);
            }
        }

        /// <summary>
        /// 加载许可信息
        /// </summary>
        private bool LoadLicenseInfo()
        {
            bool bResult = false;
            //RegistryKey lm = Registry.LocalMachine;
            RegistryKey lm = Registry.CurrentUser;
            RegistryKey s;
            s = lm.OpenSubKey("SOFTWARE", false);
            string[] subkeys = s.GetSubKeyNames();
            if (s != null)
            {
                //s = s.OpenSubKey(LimitDateHelper.SoftwareName, false);
                s = s.OpenSubKey(LimitDateHelper.SoftwareName, false);
                if (s != null)
                {
                    lblName.Text = SystemInfo.LicenseLevelName;
                    //txtName.Text = SystemInfo.LicenseLevelName;
                    dtpLimitDate.Value = SystemInfo.LimitDate;
                    bResult = true;
                }
                else
                {
                    lblName.Text = "未注册许可";
                    //txtName.Text = "未注册许可";
                }
            }

            if (string.IsNullOrEmpty(lblName.Text))
            {
                lblName.Text = LimitDateHelper.SoftwareName;
            }
            //if (string.IsNullOrEmpty(txtName.Text))
            //{
            //    txtName.Text = LimitDateHelper.DefaultSoftwareName;
            //}
            lm.Close();

            return bResult;
        }

        /// <summary>
        /// 注册许可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                //LimitDateHelper.SoftwareName = lblName.Text;

                FormLoadLic tFormLoadLic = new FormLoadLic();
                if (tFormLoadLic.ShowDialog() == DialogResult.OK)
                {
                    bool bResult = LoadLicenseInfo();//加载许可信息
                    if (bResult == false) return;

                    bResult = LimitDateHelper.IsSoftwareValid();

                    if (bResult)
                    {
                        RegistResult = true;
                        this.DialogResult = DialogResult.OK;
                        //this.Close();
                    }
                    else
                    {
                        RegistResult = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MapSoftLog.LogError(ex.Message.ToString());
                MessageHandler.ShowErrorMsg(ex);
            }
        }

        #region
        //private void btnGetMachineCode_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        FormGetMachineCode form = new FormGetMachineCode();
        //        form.ShowDialog();
        //    }
        //    catch (Exception ex)
        //    {
        //        MapSoftLog.LogError(ex.Message.ToString());
        //        MessageHandler.ShowErrorMsg(ex);
        //    }
        //}
        #endregion

        private void 获得机器码ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                FormGetMachineCode form = new FormGetMachineCode();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MapSoftLog.LogError(ex.Message.ToString());
                MessageHandler.ShowErrorMsg(ex);
            }
        }

        
    }
}
