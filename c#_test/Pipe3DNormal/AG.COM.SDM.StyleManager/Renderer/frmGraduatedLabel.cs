using System;
using System.Windows.Forms;

namespace AG.COM.SDM.StyleManager.Renderer
{
    public partial class frmGraduatedLabel : Form
    {
        public string m_RangeLabel;

        public string RangeLabel
        {
            get { return m_RangeLabel; }
            set { m_RangeLabel = value; }
        }

        public frmGraduatedLabel()
        {
            InitializeComponent();
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            if (this.tbxLabel.Text.Trim() == "")
                return;
            this.DialogResult = DialogResult.OK;
            m_RangeLabel = this.tbxLabel.Text.Trim();
            this.Close();
        }

        private void frmGraduatedLabel_Load(object sender, EventArgs e)
        {
            this.tbxLabel.Text = m_RangeLabel;
        }
    }
}