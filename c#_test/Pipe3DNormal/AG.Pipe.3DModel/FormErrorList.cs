using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AG.Pipe.Analyst3DModel
{
    public partial class FormErrorList : Form
    {
        private List<string> errorlist = new List<string>();
        public FormErrorList(List<string> list)
        {
            errorlist.Clear();
            errorlist.AddRange(list);
            InitializeComponent();

        }

        private void FormErrorList_Load(object sender, EventArgs e)
        {
            foreach (string item in errorlist)
            {
                listBox1.Items.Add(item);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
