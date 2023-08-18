using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AG.COM.MapSoft.Tool;
namespace AG.COM.MapSoft.LicenseManager
{
    /// <summary>
    /// 获取机器码
    /// </summary>
    public partial class FormGetMachineCode : Form
    {
        public FormGetMachineCode()
        {
            InitializeComponent();
        }

        private void FormGetMachineCode_Load(object sender, EventArgs e)
        {
            try
            {
                //获取当前机器的机器码
                string machineCodeThis = HardwareHelper.GetDiskVolumeSerialNumber();
                if (string.IsNullOrEmpty(machineCodeThis))
                {
                    throw new Exception("无法获取机器码，请联系系统管理员");
                }

                txtMechineCode.Text = machineCodeThis;
            }
            catch (Exception ex)
            {
                MapSoftLog.LogError(ex.Message, ex);
                MessageHandler.ShowErrorMsg(ex);
            }
        }
    }
}
