using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// SDE����UI��
    /// </summary>
    public partial class FormSDEConnection : Form
    {
        /// <summary>
        /// ʵ�����ö���
        /// </summary>
        public FormSDEConnection()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��ȡ�����ռ�
        /// </summary>
        public IWorkspace Workspace
        {
            get
            {
                return this.ctrSDEConnection1.OpenWorkspace(false);
            }
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            if (this.ctrSDEConnection1.ConnectionState() == false)
                MessageBox.Show("�������Ӳ��ɹ�");
            else
                MessageBox.Show("���ӳɹ�,�������治��(^_^)", "��ʾ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //��ȡ����״̬
            bool IsConnect = this.ctrSDEConnection1.ConnectionState();
            if (IsConnect == false)
                MessageBox.Show("�������������Ƿ�����,�����������ݿ�!");           
        }        
    }
}