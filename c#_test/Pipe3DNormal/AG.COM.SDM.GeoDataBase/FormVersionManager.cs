using System;
using AG.COM.SDM.Utility;
using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 版本管理类
    /// </summary>
    public partial class FormVersionManager :DockDocument
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FormVersionManager()
        {
            InitializeComponent();
        }

        private void FormVersionManager_Load(object sender, EventArgs e)
        {
            //设置其为查询状态
            this.gxTreeView1.OperateState = EnumOperateState.Query;
            //设置SDE工作空间
            this.gxTreeView1.SetWorkspace(CommonVariables.DatabaseConfig.Workspace);
        }
    }
}