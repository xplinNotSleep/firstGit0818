using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using System.Windows.Forms;
namespace AG.COM.SDM.GeoDataBase.VersionManager
{
    public class VersionManagerHelper
    {
        public static string GetVersionAccessCn(esriVersionAccess tEsriVersionAccess)
        {
            if (tEsriVersionAccess == esriVersionAccess.esriVersionAccessPrivate)
                return "私有";
            else if (tEsriVersionAccess == esriVersionAccess.esriVersionAccessProtected)
                return "保护";
            else
                return "公共";
        }
        /// <summary>
        /// 创建版本
        /// </summary>
        /// <param name="pWorkspace"></param>
        /// <param name="pVersionName"></param>
        /// <returns></returns>
        public static IVersion CreateVersion(IWorkspace pWorkspace, string pVersionName)
        {
            IVersionedWorkspace pVersionedWS = pWorkspace as IVersionedWorkspace;
            IVersion pTempVersion = VersionManager.CreatVersion(pVersionedWS, pVersionName);
            return pTempVersion;
        }
    }

    /// <summary>
    /// 版本管理类，包含判断版本是否已存在，版本创建，版本提交等常用函数
    /// </summary>
    public static class VersionManager
    {
        /// <summary>
        /// 创建默认版本的子版本
        /// </summary>
        /// <param name="pVersionedWorkspace">已注册版本空间</param>
        /// <param name="strVersionName">版本名称</param>
        /// <returns>新建子版本</returns>
        public static IVersion CreatVersion(IVersionedWorkspace pVersionedWorkspace, string strVersionName)
        {
            IVersion NewVersion = null;
            List<string> VersionNameList = GetVersionNameList(pVersionedWorkspace);
            if (HasVersion(VersionNameList, strVersionName))
            {
                MessageBox.Show("已经建立过名称的版本，请使用其他名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return NewVersion;
            }

            if (pVersionedWorkspace == null)
            {
                return NewVersion;
            }
            else
            {
                IVersion tVersion = pVersionedWorkspace.DefaultVersion;
                NewVersion = tVersion.CreateVersion(strVersionName);
                NewVersion.Access = esriVersionAccess.esriVersionAccessPrivate;
            }
            return NewVersion;
        }

        /// <summary>
        /// 创建指定版本的子版本
        /// </summary>
        /// <param name="pVersionedWorkspace">已注册版本空间</param>
        /// <param name="strVersionName">子版本名称</param>
        /// <param name="strVersionName">父版本名称</param>
        /// <returns>新建子版本</returns>
        public static IVersion CreatVersion(IVersionedWorkspace pVersionedWorkspace, string strVersionName, string strParentVersionName)
        {
            IVersion NewVersion = null;
            List<string> VersionNameList = GetVersionNameList(pVersionedWorkspace);
            if (HasVersion(VersionNameList, strVersionName))
            {
                MessageBox.Show("已经建立过名称的版本，请使用其他名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return NewVersion;
            }
            if (pVersionedWorkspace == null)
                return NewVersion;
            else
            {
                IVersion tVersion = pVersionedWorkspace.FindVersion(strParentVersionName);

                if (tVersion != null)
                {
                    NewVersion = tVersion.CreateVersion(strVersionName);
                    NewVersion.Access = esriVersionAccess.esriVersionAccessPrivate;
                }
            }
            return NewVersion;
        }

        /// <summary>
        /// 获取版本的列表
        /// </summary>
        /// <param name="pVersionedWorkspace">已注册版本空间</param>
        /// <returns>返回版本名称列表</returns>
        public static List<string> GetVersionNameList(IVersionedWorkspace pVersionedWorkspace)
        {
            List<string> versionNames = new List<string>();
            if (pVersionedWorkspace == null)
                return versionNames;

            IEnumVersionInfo tEnumVersion = pVersionedWorkspace.Versions;
            IVersionInfo tVersionInfo = null;
            while ((tVersionInfo = tEnumVersion.Next()) != null)
            {
                versionNames.Add(tVersionInfo.VersionName);
            }
            return versionNames;
        }
        // <summary>
        /// 切换地图文档图层的数据源版本
        /// 
        /// <param name="fromVersion">From版本</param>
        /// <param name="toVersion">To版本</param>
        /// <param name="basicMap">IBasicMap对象</param>
        public static void ChangeDatabaseVersion(IVersion fromVersion, IVersion toVersion, IBasicMap basicMap)
        {
            IChangeDatabaseVersion pChangeDataBaseVersion = new ChangeDatabaseVersionClass();
           
            ISet pSet = pChangeDataBaseVersion.Execute(fromVersion, toVersion, basicMap);
        }
        /// <summary>
        /// 判断版本列表中是否存在指定名称版本
        /// </summary>
        /// <param name="m_Versions">版本列表</param>
        /// <param name="strVersionName">指定版本名称</param>
        /// <returns></returns>
        public static bool HasVersion(List<string> m_Versions, string strVersionName)
        {
            if (m_Versions == null)
                return false;

            return m_Versions.Contains(strVersionName);
        }

        /// <summary>
        /// 提交子版本至父版本
        /// </summary>
        /// <param name="fromVersionName">子版本名称</param>
        /// <param name="toVersionName">父版本名称</param>
        /// <param name="pWorkspace">工作空间</param>
        ///<returns>提交成功标识</returns>
        public static bool PostVersion(string fromVersionName, string toVersionName, IWorkspace pWorkspace)
        {
            bool isPostOrNot = false;

            try
            {
                //获取版本空间
                IVersionedWorkspace tVersionWorksp = pWorkspace as IVersionedWorkspace;
                //获取提交版本
                IVersion tPostVersion = tVersionWorksp.FindVersion(fromVersionName);
                //打开版本编辑
                IWorkspaceEdit tWorkspaceEdit = (IWorkspaceEdit)tPostVersion;
                //获取编辑版本
                IVersionEdit tVersionEdit = (IVersionEdit)tPostVersion;
                //打开编辑
                tWorkspaceEdit.StartEditing(true);
                tWorkspaceEdit.StartEditOperation();

                bool bConflictsDetect = tVersionEdit.Reconcile(toVersionName);
                if (bConflictsDetect != true)
                {
                    IVersion tPreReconcileVersion = tVersionEdit.PreReconcileVersion;
                    IVersion tCommonAncestorVersion = tVersionEdit.CommonAncestorVersion;
                    IVersion tReconcileVersion = tVersionEdit.ReconcileVersion;
                    IVersion tStartEditingVersion = tVersionEdit.StartEditingVersion;
                    bool bPost = tVersionEdit.CanPost();
                    if (tVersionEdit.CanPost())
                    {
                        if (DialogResult.Yes == MessageBox.Show("提交后将无法进行修改,请确认是否提交？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            //提交版本
                            tVersionEdit.Post(toVersionName);
                            isPostOrNot = true;
                        }
                    }
                }
                //结束编辑
                tWorkspaceEdit.StopEditOperation();
                tWorkspaceEdit.StopEditing(true);

                if (isPostOrNot)
                {
                    MessageBox.Show("提交成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("放弃提交！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("版本提交失败", "提示");
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message);
            }

            return isPostOrNot;
        }
        public static bool PostVersion(IVersion fromVersion, IVersion toVersion, IWorkspace pWorkspace)
        {
            bool isPostOrNot = false;

            try
            {
                //获取版本空间
                IVersionedWorkspace tVersionWorksp = pWorkspace as IVersionedWorkspace;
                //获取提交版本
                IVersion tPostVersion = fromVersion;
                //打开版本编辑
                IWorkspaceEdit tWorkspaceEdit = (IWorkspaceEdit)tPostVersion;
                //获取编辑版本
                IVersionEdit tVersionEdit = (IVersionEdit)tPostVersion;
                //打开编辑
                tWorkspaceEdit.StartEditing(true);
                tWorkspaceEdit.StartEditOperation();

                bool bConflictsDetect = tVersionEdit.Reconcile(toVersion.VersionName);
                if (bConflictsDetect != true)
                {
                    IVersion tPreReconcileVersion = tVersionEdit.PreReconcileVersion;
                    IVersion tCommonAncestorVersion = tVersionEdit.CommonAncestorVersion;
                    IVersion tReconcileVersion = tVersionEdit.ReconcileVersion;
                    IVersion tStartEditingVersion = tVersionEdit.StartEditingVersion;
                    bool bPost = tVersionEdit.CanPost();
                    if (tVersionEdit.CanPost())
                    {
                        if (DialogResult.Yes == MessageBox.Show("提交后将无法进行修改,请确认是否提交？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            //提交版本
                            tVersionEdit.Post(toVersion.VersionName);
                            isPostOrNot = true;
                        }
                    }
                }
                //结束编辑
                tWorkspaceEdit.StopEditOperation();
                tWorkspaceEdit.StopEditing(true);

                if (isPostOrNot)
                {
                    MessageBox.Show("提交成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("放弃提交！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("版本提交失败", "提示");
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message);
            }

            return isPostOrNot;
        }
        /// <summary>
        /// 提交合并版本（无提示信息）
        /// </summary>
        /// <param name="fromVersionName"></param>
        /// <param name="toVersionName"></param>
        /// <param name="workspace"></param>
        /// <returns></returns>
        public static bool PostVersionNoMsg(string fromVersionName, string toVersionName, IWorkspace workspace)
        {
            bool isPostOrNot = false;

            //获取版本空间
            IVersionedWorkspace versionWorkspace = workspace as IVersionedWorkspace;
            //获取提交版本
            IVersion postVersion = versionWorkspace.FindVersion(fromVersionName);
            //打开版本编辑
            IWorkspaceEdit workspaceEdit = (IWorkspaceEdit)postVersion;
            //获取编辑版本
            IVersionEdit versionEdit = (IVersionEdit)postVersion;
            //打开编辑
            workspaceEdit.StartEditing(true);
            workspaceEdit.StartEditOperation();

            bool bConflictsDetect = versionEdit.Reconcile(toVersionName);
            if (bConflictsDetect != true)
            {
                IVersion preReconcileVersion = versionEdit.PreReconcileVersion;
                IVersion commonAncestorVersion = versionEdit.CommonAncestorVersion;
                IVersion reconcileVersion = versionEdit.ReconcileVersion;
                IVersion startEditingVersion = versionEdit.StartEditingVersion;
                bool post = versionEdit.CanPost();
                if (versionEdit.CanPost())
                {
                    //提交版本
                    versionEdit.Post(toVersionName);
                    isPostOrNot = true;
                }
                else
                {
                    throw new Exception("版本不能Post");
                }
            }
            //结束编辑
            workspaceEdit.StopEditOperation();
            workspaceEdit.StopEditing(true);

            return isPostOrNot;
        }

        /// <summary>
        /// 获取版本中指定名称要素类
        /// </summary>
        /// <param name="pVersionedWorkspace">版本空间</param>
        /// <param name="VersionName">版本名称</param>
        /// <param name="strFeatureClass">要素类名称</param>
        /// <returns>结果要素类</returns>
        public static IFeatureClass FeatureClassinVersion(IVersion pVersion, string strFeatureClass)
        {
            IFeatureClass pFeatureClass = null;
            if (pVersion == null)
                MessageBox.Show("版本空间不能为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (strFeatureClass == null)
                MessageBox.Show("要素类名称不能为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

            IFeatureWorkspace pFeatureWorkspace = pVersion as IFeatureWorkspace;

            pFeatureClass = pFeatureWorkspace.OpenFeatureClass(strFeatureClass);
            if (pFeatureClass == null)
                MessageBox.Show("要素类不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return pFeatureClass;
        }

        /// <summary>
        /// 从版本空间获取指定名称版本
        /// </summary>
        /// <param name="pVersionedWorkspace">版本空间</param>
        /// <param name="VersionName">版本名称</param>
        /// <returns>结果版本</returns>
        public static IVersion GetVersion(IVersionedWorkspace pVersionedWorkspace, string VersionName)
        {
            IVersion pVersion = null;
            if (pVersionedWorkspace == null)
                MessageBox.Show("版本空间不能为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (VersionName == null)
                MessageBox.Show("版本名称不能为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

            pVersion = pVersionedWorkspace.FindVersion(VersionName);

            if (pVersion == null)
                MessageBox.Show("版本不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return pVersion;
        }
    }
}
