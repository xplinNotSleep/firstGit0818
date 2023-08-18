using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
namespace AG.COM.SDM.GeoDataBase.VersionManager
{
    public partial class FormPostVersion : Form
    {
        public string VersionName { get; set; }
        private IVersion m_VersionCurrent = null;
        public FormPostVersion(IVersion version)
        {
            m_VersionCurrent = version;
            InitializeComponent();
        }

        private void FormPostVersion_Load(object sender, EventArgs e)
        {
            IVersionInfo tVersionInfo = m_VersionCurrent.VersionInfo;
            if (m_VersionCurrent.HasParent() == true)
            {
                comboBox1.Items.Add(tVersionInfo.Parent.VersionName);
                comboBox1.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem==null)
            {
                MessageBox.Show("请选择目标版本","提示");
                return;
            }
            VersionName = comboBox1.SelectedItem.ToString();
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
