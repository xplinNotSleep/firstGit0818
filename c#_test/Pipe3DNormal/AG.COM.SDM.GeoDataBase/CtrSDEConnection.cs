using AG.COM.SDM.Utility;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// SDE连接用户控件类
    /// </summary>
    public partial class CtrSDEConnection : UserControl
    {
        /// <summary>
        /// 实例化新对象
        /// </summary>
        public CtrSDEConnection()
        {
            //初始化界面组件
            InitializeComponent();

            this.txtInstance.Text = "5151";
            this.txtDataBase.Text = "SDE";
            this.txtUser.Text = "SDE";
            this.cboVersions.Text = "SDE.DEFAULT";
        }

        /// <summary>
        /// 获取连接状态
        /// </summary>
        /// <returns>如果成功连接则返回 true,否则返回 false</returns>
        public bool ConnectionState()
        {
            //获取工作空间
            IWorkspace tWorkspace = OpenWorkspace();
            if (tWorkspace == null) 
                return false;

            //查询IWorkspaceFactoryStatus接口
            IWorkspaceFactoryStatus tWorksapceFacStatus = tWorkspace.WorkspaceFactory as IWorkspaceFactoryStatus;
            //得到连接状态
            IWorkspaceStatus tWorkspaceStatus= tWorksapceFacStatus.PingWorkspaceStatus(tWorkspace);
            if (tWorkspaceStatus.ConnectionStatus != esriWorkspaceConnectionStatus.esriWCSDown)
                return true;
            else
                return false;         
        }

        /// <summary>
        /// 获取连接属性设置
        /// </summary>
        /// <returns>返回IPropertyset</returns>
        private IPropertySet GetPropertySet()
        {
            IPropertySet tPropertySet = new PropertySetClass();
            //SDE服务器名称
            tPropertySet.SetProperty("Server", txtServerName.Text);
            //SDE实例名
            tPropertySet.SetProperty("Instance", txtInstance.Text);
            //SDE数据库名称
            tPropertySet.SetProperty("DataBase", txtDataBase.Text);
            //SDE用户名
            tPropertySet.SetProperty("User", txtUser.Text);
            //SDE密码
            tPropertySet.SetProperty("Password", txtPwd.Text);
            //SDE版本号
            tPropertySet.SetProperty("VERSION", cboVersions.Text);
            //登录模式
            tPropertySet.SetProperty("AUTHENTICATION_MODE", "DBMS");
            return tPropertySet;
        }


        /// <summary>
        /// 获取或设置服务器
        /// </summary>
        public string Server
        {
            get { return txtServerName.Text; }
            set { txtServerName.Text = value; }
        }

        /// <summary>
        /// 获取或设置实例
        /// </summary>
        public string Instance
        {
            get { return txtInstance.Text; }
            set { txtInstance.Text = value; }
        }

        /// <summary>
        /// 获取或设置数据库
        /// </summary>
        public string DataBase
        {
            get { return txtDataBase.Text; }
            set { txtDataBase.Text = value; }
        }

        /// <summary>
        /// 获取或设置用户名
        /// </summary>
        public string User
        {
            get { return txtUser.Text; }
            set { txtUser.Text = value; }
        }


        /// <summary>
        /// 获取或设置密码
        /// </summary>
        public string Password
        {
            get { return txtPwd.Text; }
            set { txtPwd.Text = value; }
        }

        /// <summary>
        /// 获取或设置版本
        /// </summary>
        public string Version
        {
            get { return cboVersions.Text; }
            set { cboVersions.Text = value; }
        }


        /// <summary>
        /// 获取工作空间
        /// </summary>
        /// <returns>返回IWorkspace</returns>
        public IWorkspace OpenWorkspace()
        {
            //检测属性设置有效性
            bool IsValid=CheckInputValid();
            if(IsValid==false) 
                return null;

            //获取连接属性
            IPropertySet tPropertySet = GetPropertySet();

            //由于当IP不存在时连接会卡死，因此先ping一下SDE的IP
            string server = tPropertySet.GetProperty("Server").ToString();
            //if (NetHelper.Ping(server) == false) return null;    

            //实例化SdeWorkspaceFactoryClass类
            IWorkspaceFactory tWorkspaceFactory = new SdeWorkspaceFactoryClass();

            try
            {
                //根据连接属性打开工作空间
                IWorkspace tWorkspace = tWorkspaceFactory.Open(tPropertySet, 0);
                return tWorkspace;
            }
            catch(Exception ex) 
            {
                return null;
            }
        }

        /// <summary>
        /// 获取工作空间
        /// </summary>
        /// <param name="emptyVersion">是否允许将版本设为空</param>
        /// <returns>返回IWorkspace</returns>
        public IWorkspace OpenWorkspace(bool emptyVersion)
        {
            //检测属性设置有效性
            bool IsValid = CheckInputValid();
            if (IsValid == false)
                return null;

            //获取连接属性
            IPropertySet tPropertySet = GetPropertySet();
            if (emptyVersion)
                //tPropertySet.SetProperty("VERSION", "");
                tPropertySet.SetProperty("VERSION", "SDE.DEFAULT");

            //由于当IP不存在时连接会卡死，因此先ping一下SDE的IP
            string server = tPropertySet.GetProperty("Server").ToString();
            if (NetHelper.Ping(server) == false)
            {
                if (MessageBox.Show("服务器Ping不通，可能是服务器不存在，请问是否继续连接？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    throw new Exception("服务器 ping不通");
                }
            }

            //实例化SdeWorkspaceFactoryClass类
            IWorkspaceFactory tWorkspaceFactory = new SdeWorkspaceFactoryClass();

            //根据连接属性打开工作空间
            IWorkspace tWorkspace = tWorkspaceFactory.Open(tPropertySet, 0);
            return tWorkspace;
        }

        /// <summary>
        /// 检测属性设置有效性
        /// </summary>
        /// <returns>返回检测信息</returns>
        public bool CheckInputValid()
        {
            StringBuilder tStrBuilder = new StringBuilder();
            if (txtServerName.Text.Trim().Length == 0)
            {
                tStrBuilder.Append("'Server' ");
            }

            if (txtInstance.Text.Trim().Length == 0)
            {
                tStrBuilder.Append("'Service' ");
            }

            //if (txtDataBase.Text.Trim().Length == 0)
            //{
            //    tStrBuilder.Append("'DataBase' ");
            //}

            if (txtUser.Text.Trim().Length == 0)
            {
                tStrBuilder.Append("'User' ");
            }

            if (tStrBuilder.ToString().Length > 0)
            {
                tStrBuilder.Append("不能为空!");
                MessageBox.Show(tStrBuilder.ToString());
                return false;
            }
            else
                return true;
        }

        private void btnChanged_Click(object sender, EventArgs e)
        {
            try
            {
                IWorkspace ws = OpenWorkspace(true);
                if (ws == null)
                {
                    MessageBox.Show("连接失败！", "数据库连接");
                    return;
                }

                cboVersions.Items.Clear();

                IEnumVersionInfo pEnum = (ws as IVersionedWorkspace).Versions;
                IVersionInfo ver = pEnum.Next();
                while (ver != null)
                {
                    cboVersions.Items.Add(ver.VersionName);
                    ver = pEnum.Next();
                }
                if (cboVersions.SelectedIndex == -1)
                {
                    if (cboVersions.Items.Count > 0)
                        cboVersions.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(CommonVariables.ErroMsgHead + ex.Message);
            }
        }
    }
}