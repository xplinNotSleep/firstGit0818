using System;
using System.Windows.Forms;

namespace AG.COM.SDM.StyleManager.Renderer
{
    public partial class frmGraduatedBreak : Form
    {
        public double m_BreakValue;

        public double BreakValue
        {
            get { return m_BreakValue; }
            set { m_BreakValue = value; }
        }

        public frmGraduatedBreak()
        {
            InitializeComponent();
        }

        private void frmGraduatedBreak_Load(object sender, EventArgs e)
        {
            this.tbxBreakValue.Text = m_BreakValue.ToString();
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            if (this.tbxBreakValue.Text.Trim() == "")
                return;
            this.DialogResult = DialogResult.OK;
            m_BreakValue =System.Convert.ToDouble(this.tbxBreakValue.Text.Trim());
            this.Close();
        }
    }
}