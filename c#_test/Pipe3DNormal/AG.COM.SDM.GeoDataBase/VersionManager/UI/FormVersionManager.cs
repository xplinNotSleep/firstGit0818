using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.Utility;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase.VersionManager
{
    public partial class FormVersionManager : Form
    {
        #region 初始化

        public FormVersionManager()
        {
            InitializeComponent();
        }

        private void FormVersionManager_Load(object sender, EventArgs e)
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
            lvwManager.Items.Clear();

            IVersionedWorkspace tVersionedWorkspace = CommonVariables.DatabaseConfig.Workspace as IVersionedWorkspace;
            IEnumVersionInfo tEnumVersionInfo = tVersionedWorkspace.Versions;
            IVersionInfo tVersionInfo = tEnumVersionInfo.Next();
            List<SdeVersion> versionInfos = new List<SdeVersion>();
            while (tVersionInfo != null)
            {
                SdeVersion version = new SdeVersion();
                string[] split = tVersionInfo.VersionName.Split('.');
                version.Name1 = split[1];
                version.Name2=split[0];
                version.Name3=VersionManagerHelper.GetVersionAccessCn(tVersionInfo.Access);
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
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = item.Name1;
                lvItem.SubItems.Add(item.Name2);
                lvItem.SubItems.Add(item.Name3);
                lvItem.SubItems.Add(item.Modified.ToString());
                lvItem.Tag = item.IVersionInfo;
                lvwManager.Items.Add(lvItem);
            }
            //while (tVersionInfo != null)
            //{
            //    ListViewItem lvItem = new ListViewItem();
            //    string[] split = tVersionInfo.VersionName.Split('.');
            //    lvItem.Text = split[1];
            //    lvItem.SubItems.Add(split[0]);
            //    lvItem.SubItems.Add(VersionManagerHelper.GetVersionAccessCn(tVersionInfo.Access));
            //    string lastModify = "";
            //    if (tVersionInfo.Modified is DateTime)
            //    {
            //        DateTime dt = (DateTime)tVersionInfo.Modified;
            //        lastModify = dt.ToString();
            //    }
            //    lvItem.SubItems.Add(lastModify);
            //    lvItem.Tag = tVersionInfo;

            //    lvwManager.Items.Add(lvItem);

            //    tVersionInfo = tEnumVersionInfo.Next();
            //}
        }

        #endregion

        #region 增删改

        private void lvwManager_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (lvwManager.SelectedItems.Count > 0)
                {
                    cms1.Show(lvwManager, new Point(e.X, e.Y));                 
                }
            }
        }

        private void 新增ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwManager.SelectedItems.Count <= 0) return;
                IVersionInfo tVersionInfo = lvwManager.SelectedItems[0].Tag as IVersionInfo;

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

        private void 重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwManager.SelectedItems.Count <= 0) return;
                IVersionInfo tVersionInfo = lvwManager.SelectedItems[0].Tag as IVersionInfo;

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

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshList();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwManager.SelectedItems.Count <= 0) return;

                if (MessageBox.Show("确定要删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    IVersionInfo tVersionInfo = lvwManager.SelectedItems[0].Tag as IVersionInfo;

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

        private void 属性ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (lvwManager.SelectedItems.Count <= 0) return;
                    IVersionInfo tVersionInfo = lvwManager.SelectedItems[0].Tag as IVersionInfo;

                    IVersionedWorkspace tVersionedWorkspace = CommonVariables.DatabaseConfig.Workspace as IVersionedWorkspace;
                    IVersion tVersionCurrent = tVersionedWorkspace.FindVersion(tVersionInfo.VersionName);

                    FormVersionProp tFormVersionProp = new FormVersionProp(tVersionCurrent);
                    if (tFormVersionProp.ShowDialog() == DialogResult.OK)
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
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        #endregion
    }

    public class SdeVersion
    {
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public DateTime Modified { get; set; }
        public IVersionInfo IVersionInfo { get; set; }
    }
}
