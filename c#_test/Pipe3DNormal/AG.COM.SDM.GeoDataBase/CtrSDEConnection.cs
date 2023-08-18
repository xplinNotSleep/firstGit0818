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
    /// SDE�����û��ؼ���
    /// </summary>
    public partial class CtrSDEConnection : UserControl
    {
        /// <summary>
        /// ʵ�����¶���
        /// </summary>
        public CtrSDEConnection()
        {
            //��ʼ���������
            InitializeComponent();

            this.txtInstance.Text = "5151";
            this.txtDataBase.Text = "SDE";
            this.txtUser.Text = "SDE";
            this.cboVersions.Text = "SDE.DEFAULT";
        }

        /// <summary>
        /// ��ȡ����״̬
        /// </summary>
        /// <returns>����ɹ������򷵻� true,���򷵻� false</returns>
        public bool ConnectionState()
        {
            //��ȡ�����ռ�
            IWorkspace tWorkspace = OpenWorkspace();
            if (tWorkspace == null) 
                return false;

            //��ѯIWorkspaceFactoryStatus�ӿ�
            IWorkspaceFactoryStatus tWorksapceFacStatus = tWorkspace.WorkspaceFactory as IWorkspaceFactoryStatus;
            //�õ�����״̬
            IWorkspaceStatus tWorkspaceStatus= tWorksapceFacStatus.PingWorkspaceStatus(tWorkspace);
            if (tWorkspaceStatus.ConnectionStatus != esriWorkspaceConnectionStatus.esriWCSDown)
                return true;
            else
                return false;         
        }

        /// <summary>
        /// ��ȡ������������
        /// </summary>
        /// <returns>����IPropertyset</returns>
        private IPropertySet GetPropertySet()
        {
            IPropertySet tPropertySet = new PropertySetClass();
            //SDE����������
            tPropertySet.SetProperty("Server", txtServerName.Text);
            //SDEʵ����
            tPropertySet.SetProperty("Instance", txtInstance.Text);
            //SDE���ݿ�����
            tPropertySet.SetProperty("DataBase", txtDataBase.Text);
            //SDE�û���
            tPropertySet.SetProperty("User", txtUser.Text);
            //SDE����
            tPropertySet.SetProperty("Password", txtPwd.Text);
            //SDE�汾��
            tPropertySet.SetProperty("VERSION", cboVersions.Text);
            //��¼ģʽ
            tPropertySet.SetProperty("AUTHENTICATION_MODE", "DBMS");
            return tPropertySet;
        }


        /// <summary>
        /// ��ȡ�����÷�����
        /// </summary>
        public string Server
        {
            get { return txtServerName.Text; }
            set { txtServerName.Text = value; }
        }

        /// <summary>
        /// ��ȡ������ʵ��
        /// </summary>
        public string Instance
        {
            get { return txtInstance.Text; }
            set { txtInstance.Text = value; }
        }

        /// <summary>
        /// ��ȡ���������ݿ�
        /// </summary>
        public string DataBase
        {
            get { return txtDataBase.Text; }
            set { txtDataBase.Text = value; }
        }

        /// <summary>
        /// ��ȡ�������û���
        /// </summary>
        public string User
        {
            get { return txtUser.Text; }
            set { txtUser.Text = value; }
        }


        /// <summary>
        /// ��ȡ����������
        /// </summary>
        public string Password
        {
            get { return txtPwd.Text; }
            set { txtPwd.Text = value; }
        }

        /// <summary>
        /// ��ȡ�����ð汾
        /// </summary>
        public string Version
        {
            get { return cboVersions.Text; }
            set { cboVersions.Text = value; }
        }


        /// <summary>
        /// ��ȡ�����ռ�
        /// </summary>
        /// <returns>����IWorkspace</returns>
        public IWorkspace OpenWorkspace()
        {
            //�������������Ч��
            bool IsValid=CheckInputValid();
            if(IsValid==false) 
                return null;

            //��ȡ��������
            IPropertySet tPropertySet = GetPropertySet();

            //���ڵ�IP������ʱ���ӻῨ���������pingһ��SDE��IP
            string server = tPropertySet.GetProperty("Server").ToString();
            //if (NetHelper.Ping(server) == false) return null;    

            //ʵ����SdeWorkspaceFactoryClass��
            IWorkspaceFactory tWorkspaceFactory = new SdeWorkspaceFactoryClass();

            try
            {
                //�����������Դ򿪹����ռ�
                IWorkspace tWorkspace = tWorkspaceFactory.Open(tPropertySet, 0);
                return tWorkspace;
            }
            catch(Exception ex) 
            {
                return null;
            }
        }

        /// <summary>
        /// ��ȡ�����ռ�
        /// </summary>
        /// <param name="emptyVersion">�Ƿ������汾��Ϊ��</param>
        /// <returns>����IWorkspace</returns>
        public IWorkspace OpenWorkspace(bool emptyVersion)
        {
            //�������������Ч��
            bool IsValid = CheckInputValid();
            if (IsValid == false)
                return null;

            //��ȡ��������
            IPropertySet tPropertySet = GetPropertySet();
            if (emptyVersion)
                //tPropertySet.SetProperty("VERSION", "");
                tPropertySet.SetProperty("VERSION", "SDE.DEFAULT");

            //���ڵ�IP������ʱ���ӻῨ���������pingһ��SDE��IP
            string server = tPropertySet.GetProperty("Server").ToString();
            if (NetHelper.Ping(server) == false)
            {
                if (MessageBox.Show("������Ping��ͨ�������Ƿ����������ڣ������Ƿ�������ӣ�", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    throw new Exception("������ ping��ͨ");
                }
            }

            //ʵ����SdeWorkspaceFactoryClass��
            IWorkspaceFactory tWorkspaceFactory = new SdeWorkspaceFactoryClass();

            //�����������Դ򿪹����ռ�
            IWorkspace tWorkspace = tWorkspaceFactory.Open(tPropertySet, 0);
            return tWorkspace;
        }

        /// <summary>
        /// �������������Ч��
        /// </summary>
        /// <returns>���ؼ����Ϣ</returns>
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
                tStrBuilder.Append("����Ϊ��!");
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
                    MessageBox.Show("����ʧ�ܣ�", "���ݿ�����");
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