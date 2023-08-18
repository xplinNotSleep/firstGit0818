using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AG.COM.SDM.Utility;
using ESRI.ArcGIS.Geodatabase;
using AG.COM.SDM.Framework;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;

namespace AG.COM.SDM.GeoDataBase.VersionManager.UI
{
    public partial class FormVersionManager1 : SkinForm
    {
        //主程序的Hook
        private IHookHelperEx m_HookHelperEx = null;
        public FormVersionManager1(IHookHelperEx hookHelper)
        {
            m_HookHelperEx = hookHelper;
            InitializeComponent();
        }

        private void FormVersionManager1_Load(object sender, EventArgs e)
        {
            try
            {
                RefreshList();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
                Close();
            }
        }
        private void RefreshList()
        {
            this.dgvTaskList.Rows.Clear();

            IVersionedWorkspace tVersionedWorkspace = CommonVariables.DatabaseConfig.Workspace as IVersionedWorkspace;
            IEnumVersionInfo tEnumVersionInfo = tVersionedWorkspace.Versions;
            IVersionInfo tVersionInfo = tEnumVersionInfo.Next();
            List<SdeVersion> versionInfos = new List<SdeVersion>();
            while (tVersionInfo != null)
            {
                SdeVersion version = new SdeVersion();
                string[] split = tVersionInfo.VersionName.Split('.');
                version.Name1 = split[1]== "DEFAULT"?"现状版本": split[1];
                version.Name2 = split[0];
                version.Name3 = VersionManagerHelper.GetVersionAccessCn(tVersionInfo.Access);
                if (tVersionInfo.Modified is DateTime)
                {
                    version.Modified = (DateTime)tVersionInfo.Modified;
                }
                version.IVersionInfo = tVersionInfo;
                versionInfos.Add(version);
                tVersionInfo = tEnumVersionInfo.Next();
            }
            List<SdeVersion> list = versionInfos.OrderByDescending(m => m.Modified).ToList();
            foreach (SdeVersion item in list)
            {
                DataGridViewRow tRow = this.dgvTaskList.Rows[this.dgvTaskList.Rows.Add()];
                tRow.Cells[0].Value = item.Name1;
                tRow.Cells[1].Value = item.Name2;
                tRow.Cells[2].Value = item.Modified.ToString();
                tRow.Tag = item;
            }
            
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTaskList.SelectedRows.Count <= 0) return;
                IVersionInfo tVersionInfo = (dgvTaskList.SelectedRows[0].Tag as SdeVersion).IVersionInfo;

                IVersionedWorkspace tVersionedWorkspace = CommonVariables.DatabaseConfig.Workspace as IVersionedWorkspace;
                IVersion tVersionCurrent = tVersionedWorkspace.FindVersion(tVersionInfo.VersionName);

                FormNewVersion tFormNewVersion = new FormNewVersion(tVersionCurrent);
                if (tFormNewVersion.ShowDialog() == DialogResult.OK)
                {
                    RefreshList();
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTaskList.SelectedRows.Count <= 0) return;

                if (MessageBox.Show("确定要删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    IVersionInfo tVersionInfo = (dgvTaskList.SelectedRows[0].Tag as SdeVersion).IVersionInfo;

                    IVersionedWorkspace tVersionedWorkspace = CommonVariables.DatabaseConfig.Workspace as IVersionedWorkspace;
                    IVersion tVersionCurrent = tVersionedWorkspace.FindVersion(tVersionInfo.VersionName);

                    tVersionCurrent.Delete();

                    RefreshList();
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTaskList.SelectedRows.Count <= 0) return;
                IVersionInfo tVersionInfo = (dgvTaskList.SelectedRows[0].Tag as SdeVersion).IVersionInfo;
                IVersionedWorkspace tVersionedWorkspace = CommonVariables.DatabaseConfig.Workspace as IVersionedWorkspace;
                IVersion tVersionCurrent = tVersionedWorkspace.FindVersion(tVersionInfo.VersionName);
                FormRenameVersion tFormRenameVersion = new FormRenameVersion(tVersionCurrent);
                if (tFormRenameVersion.ShowDialog() == DialogResult.OK)
                {
                    RefreshList();
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        private void btnChanged_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTaskList.SelectedRows.Count <= 0) return;
                IVersionInfo tVersionInfo = (dgvTaskList.SelectedRows[0].Tag as SdeVersion).IVersionInfo;
                IVersionedWorkspace tVersionedWorkspace = CommonVariables.DatabaseConfig.Workspace as IVersionedWorkspace;
                // if(CommonVariables.DatabaseConfig.CurrentVersion.VersionName.ToUpper()=="SDE.DEFAULT"|| CommonVariables.DatabaseConfig.CurrentVersion.VersionName.ToUpper() == "DEFAULT")
                IVersion versionTo = tVersionedWorkspace.FindVersion(tVersionInfo.VersionName);
                IVersion tDefaultVersion = CommonVariables.DatabaseConfig.CurrentVersion;// AG.COM.SDM.Utility.CommonVariables.DatabaseConfig.Workspace as IVersion;
                VersionManager.ChangeDatabaseVersion(tDefaultVersion, versionTo, (IBasicMap)this.m_HookHelperEx.FocusMap);
                AG.COM.SDM.Utility.CommonVariables.DatabaseConfig.CurrentVersion = versionTo;
                MessageBox.Show($"已经成功切换到{versionTo.VersionName}","提示");
                UpdateFeatureLayers(versionTo);
                m_HookHelperEx.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }
        /// <summary>
        /// 更新Map视图的FeatureLayer
        /// </summary>
        /// <param name="pWorkspace">历史工作空间</param>
        private void UpdateFeatureLayers(IVersion tVersionChild)
        {
            IFeatureWorkspace tFeatureWorkspace = (IFeatureWorkspace)tVersionChild;
            //取得Map视图的所有FeatureLayer
            UID uid = new UIDClass();
            uid.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}";    //FeatureLayer
            IEnumLayer pMapLayers = m_HookHelperEx.FocusMap.get_Layers(uid, true);
            ILayer pLayer = null;
            while ((pLayer = pMapLayers.Next()) != null)
            {
                IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                IDataset pDs = pFeatureLayer.FeatureClass as IDataset;
                if (pDs == null) continue;
                string strName = pDs.BrowseName;
                if (strName.Contains("."))
                {
                    string[] strNames = strName.Split('.');
                    strName = strNames[strNames.Length - 1];
                }
                IFeatureClass pClass =TryOpenClass(tFeatureWorkspace, strName);
                if (pClass != null)
                {
                    pFeatureLayer.FeatureClass = pClass;
                }
            }
            //刷新视图
            m_HookHelperEx.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }
        /// <summary>
        /// 打开要素类
        /// </summary>
        /// <param name="pWorkspace"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IFeatureClass TryOpenClass(IFeatureWorkspace pWorkspace, string name)
        {
            try
            {
                return pWorkspace.OpenFeatureClass(name);
            }
            catch (Exception err)
            {
                return null;
            }
        }
        private void btnPost_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTaskList.SelectedRows.Count <= 0) return;
                IVersionInfo tVersionInfo = (dgvTaskList.SelectedRows[0].Tag as SdeVersion).IVersionInfo;
                IVersionedWorkspace tVersionedWorkspace = CommonVariables.DatabaseConfig.Workspace as IVersionedWorkspace;
                IVersion versionfrom = tVersionedWorkspace.FindVersion(tVersionInfo.VersionName);
                FormPostVersion formPost = new FormPostVersion(versionfrom);
                if(formPost.ShowDialog()==DialogResult.OK)
                {
                    IVersion tDefaultVersion = tVersionedWorkspace.FindVersion(formPost.VersionName);// AG.COM.SDM.Utility.CommonVariables.DatabaseConfig.Workspace as IVersion;
                    VersionManager.PostVersion(versionfrom, tDefaultVersion, CommonVariables.DatabaseConfig.Workspace);
                }
               
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex?.Message, "错误");
            }
        }

        private void dgvTaskList_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void dgvTaskList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTaskList.SelectedRows.Count <= 0) return;
            if (dgvTaskList.SelectedRows[0].Tag == null) return;
            IVersionInfo tVersionInfo = (dgvTaskList.SelectedRows[0].Tag as SdeVersion).IVersionInfo;
            IVersionedWorkspace tVersionedWorkspace = CommonVariables.DatabaseConfig.Workspace as IVersionedWorkspace;
            IVersion tVersionCurrent = tVersionedWorkspace.FindVersion(tVersionInfo.VersionName);
            ucVersionProp1.VersionCurrent = tVersionCurrent;
        }
    }
}
