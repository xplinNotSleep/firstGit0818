using System;
using AG.COM.SDM.Utility;
using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// �汾������
    /// </summary>
    public partial class FormVersionManager :DockDocument
    {
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public FormVersionManager()
        {
            InitializeComponent();
        }

        private void FormVersionManager_Load(object sender, EventArgs e)
        {
            //������Ϊ��ѯ״̬
            this.gxTreeView1.OperateState = EnumOperateState.Query;
            //����SDE�����ռ�
            this.gxTreeView1.SetWorkspace(CommonVariables.DatabaseConfig.Workspace);
        }
    }
}