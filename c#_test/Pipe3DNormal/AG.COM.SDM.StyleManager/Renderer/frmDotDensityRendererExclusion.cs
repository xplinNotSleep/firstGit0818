using System;
using System.Windows.Forms;

namespace AG.COM.SDM.StyleManager.Renderer
{
    public partial class frmDotDensityRendererExclusion : Form
    {
        private string m_Clause;

        public string Clause
        {
            get { return m_Clause; }
            set { m_Clause = value; }
        }

        public frmDotDensityRendererExclusion()
        {
            InitializeComponent();
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            m_Clause = this.tbxClause.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmDotDensityRendererExclusion_Load(object sender, EventArgs e)
        {
            this.tbxClause.Text = m_Clause;
        }
    }
}