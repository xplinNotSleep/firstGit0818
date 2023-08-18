using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.MapSoft.Register
{
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
        }
        private void FormRegister_Load(object sender, EventArgs e)
        {
            txtSeriNum.Text = MachineCode.getMNum();
            txtRegNum.Focus();

        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            string sRegisterNumber=txtRegNum.Text.Trim();
            if(string.IsNullOrEmpty(sRegisterNumber))
            {
                MessageBox.Show("请输入注册码注册!");
                txtRegNum.Focus();
                return;
            }
            RegisterHelp reg = new RegisterHelp();
            if (!reg.CheckMiWen(sRegisterNumber))
            {
                MessageBox.Show("注册码不正确,请重新输入!");
                txtRegNum.Focus();
                return;
            }
            //注册码加密
            reg.WriteMiWen("register",sRegisterNumber);
            //写入软件用过次数
            //string sCount="00000000";
            //TimeOutCheck time = new TimeOutCheck();
            //time.WriteCount("reg32", sCount);
            MessageBox.Show("注册成功！");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
    }
}
