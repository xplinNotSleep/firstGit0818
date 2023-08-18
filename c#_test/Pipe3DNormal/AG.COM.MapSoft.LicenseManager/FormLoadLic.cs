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
    public partial class FormLoadLic : Form
    {
        public FormLoadLic()
        {
            InitializeComponent();
        }

        private void btnOutputPath_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtOutputPath.Text = openFileDialog1.FileName;
            }
        }

        /// <summary>
        /// 根据许可文件路径注册许可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtOutputPath.Text))
                {
                    MessageBox.Show("请选择许可文件");
                    return;
                }

                //开始把许可文件的信息写入注册表的资源文件
                bool bResult = LimitDateHelper.RegisterLic(txtOutputPath.Text);
                if (bResult == false) return;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MapSoftLog.LogError(ex.Message.ToString());
                MessageHandler.ShowErrorMsg(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }       
    }
}
