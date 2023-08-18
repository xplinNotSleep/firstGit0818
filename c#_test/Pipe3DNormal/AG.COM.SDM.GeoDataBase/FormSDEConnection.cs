using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// SDE连接UI类
    /// </summary>
    public partial class FormSDEConnection : Form
    {
        /// <summary>
        /// 实例化该对象
        /// </summary>
        public FormSDEConnection()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取工作空间
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
                MessageBox.Show("测试连接不成功");
            else
                MessageBox.Show("连接成功,运气还真不错(^_^)", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //获取连接状态
            bool IsConnect = this.ctrSDEConnection1.ConnectionState();
            if (IsConnect == false)
                MessageBox.Show("请检查属性设置是否有误,不能连接数据库!");           
        }        
    }
}