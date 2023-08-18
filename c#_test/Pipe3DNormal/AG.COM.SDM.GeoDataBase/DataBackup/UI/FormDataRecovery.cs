using AG.COM.SDM.Catalog;
using AG.COM.SDM.Catalog.DataItems;
using AG.COM.SDM.Catalog.Filters;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AG.COM.SDM.GeoDataBase.DataBackup
{
    /// <summary>
    /// 数据恢复窗体类
    /// </summary>
    public partial class FormDataRecovery : SkinForm
    {
        #region 变量

        /// <summary>
        /// 源工作空间，选择备份数据库后暂存工作空间于此
        /// </summary>
        private IFeatureWorkspace m_FeatureWorkspaceSource = null;
        /// <summary>
        /// 进度条
        /// </summary>
        ITrackProgress m_TrackProgress = null;
        /// <summary>
        /// 恢复目标数据源
        /// </summary>
        AGSDM_DATASOURCE m_DataSourceTarget = null;

        #endregion

        #region  初始化

        public FormDataRecovery()
        {
            InitializeComponent();
        }

        private void FormDataRecovery_Load(object sender, EventArgs e)
        {
            try
            {
                //加载数据源到cmb
                ShowDataSource();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        /// <summary>
        /// 加载数据源到cmb
        /// </summary>
        private void ShowDataSource()
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);

            IList<AGSDM_DATASOURCE> tDataSources = tEntityHandler.GetEntities<AGSDM_DATASOURCE>("from AGSDM_DATASOURCE t");
            cmbDataSource.DisplayMember = "SOURCE_NAME";
            cmbDataSource.DataSource = tDataSources;
        }

        private void cmbDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //选择数据源后刷新数据源信息
                AGSDM_DATASOURCE tDataSource = cmbDataSource.SelectedItem as AGSDM_DATASOURCE;
                if (tDataSource != null)
                {
                    m_DataSourceTarget = tDataSource;

                    txtServerIP.Text = tDataSource.IP;
                    txtService.Text = tDataSource.SERVICE_NAME;
                    txtUser.Text = tDataSource.USER_NAME;
                }
                else
                {
                    m_DataSourceTarget = null;

                    txtServerIP.Text = "";
                    txtServerIP.Text = "";
                    txtUser.Text = "";
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        #endregion

        #region 数据恢复

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                //检查录入信息
                if (CheckInputInfo() == false) return;

                EnableInputControl(false);

                m_TrackProgress = new TrackProgressDialog();
                ///完成后不自动关闭
                m_TrackProgress.AutoFinishClose = true;
                m_TrackProgress.DisplayTotal = true;
                m_TrackProgress.TotalValue = 0;
                m_TrackProgress.TotalMessage = "";

                m_TrackProgress.SubMessage = "正在连接数据源......";
                m_TrackProgress.SubValue = 0;

                m_TrackProgress.Show();
                Application.DoEvents();

                //连接数据源，获取目标Workspace
                IWorkspace2 tWorkspaceTarget = DataBackupHelper.ConnectDataSource(m_DataSourceTarget);
                //源Workspace
                IFeatureWorkspace tFeatureWorkspaceSource = m_FeatureWorkspaceSource;
                //相同是否覆盖
                bool isCover = rdoCover.Checked;
                //所有需要恢复的数据集和要素类
                List<DBDataItem> tDBDataItems = new List<DBDataItem>();
                //创建数据集及数据集内部的要素类
                CreateDatasetAndFeatureClass(tWorkspaceTarget, tFeatureWorkspaceSource, ref tDBDataItems, isCover);
                //创建数据库根目录下的要素类
                CreateFeatureClass(tWorkspaceTarget, tFeatureWorkspaceSource, ref tDBDataItems, isCover);
                //复制数据
                WriteData(tFeatureWorkspaceSource as IWorkspace, tWorkspaceTarget as IFeatureWorkspace, tDBDataItems);
                                
                m_TrackProgress.SetFinish();

                EnableInputControl(true);

                AG.COM.SDM.Utility.Common.MessageHandler.ShowInfoMsg("恢复完成", "提示");
                Close();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");

                if (m_TrackProgress != null)
                {
                    m_TrackProgress.SetFinish();
                }

                EnableInputControl(true);
            }
        }

        /// <summary>
        /// 创建数据集和数据集内的要素类
        /// </summary>
        /// <param name="tWorkspaceTarget"></param>
        /// <param name="tFeatureWorkspaceSource"></param>
        /// <param name="tDBDataItems"></param>
        /// <param name="isCover"></param>
        private void CreateDatasetAndFeatureClass(IWorkspace2 tWorkspaceTarget, IFeatureWorkspace tFeatureWorkspaceSource,
           ref List<DBDataItem> tDBDataItems, bool isCover)
        {
            IWorkspace tWorkspaceSource = tFeatureWorkspaceSource as IWorkspace;
            IFeatureWorkspace tFeatureWorkspaceTarget = tWorkspaceTarget as IFeatureWorkspace;
            IFeatureWorkspaceAnno tFeatureWorkspaceAnnoTarget = tWorkspaceTarget as IFeatureWorkspaceAnno;
            IFieldChecker tFieldChecker = new FieldCheckerClass();
            tFieldChecker.ValidateWorkspace = tWorkspaceTarget as IWorkspace;

            //首先遍历源数据库的数据集
            IEnumDataset tEnumDatasetSource = tWorkspaceSource.get_Datasets(esriDatasetType.esriDTFeatureDataset);
            tEnumDatasetSource.Reset();
            IFeatureDataset tFeatureDatasetSource = tEnumDatasetSource.Next() as IFeatureDataset;

            string strSubMsgHead = "正在建立";

            while (tFeatureDatasetSource != null)
            {
                IFeatureDataset tFeatureDatasetTarget = null;
                //检查目标数据库是否存在同名数据集
                IDataset tDatasetSource = tFeatureDatasetSource as IDataset;
                bool isExist = tWorkspaceTarget.get_NameExists(esriDatasetType.esriDTFeatureDataset, tDatasetSource.Name);

                m_TrackProgress.SubMessage = strSubMsgHead + "数据集 " + tDatasetSource.Name;
                Application.DoEvents();               
               
                if (isExist == false || (isExist == true && isCover == true))
                {
                    //新建数据集                    
                    IGeoDataset tGeoDatasetSource = tFeatureDatasetSource as IGeoDataset;
                    if (tWorkspaceTarget.get_NameExists(esriDatasetType.esriDTFeatureDataset, tDatasetSource.Name))
                    {
                        tFeatureDatasetTarget = tFeatureWorkspaceTarget.OpenFeatureDataset(tDatasetSource.Name);
                    }
                    else
                    {
                        tFeatureDatasetTarget = tFeatureWorkspaceTarget.CreateFeatureDataset(tDatasetSource.Name, tGeoDatasetSource.SpatialReference);
                    }

                    DBDataItem tDBDataItem = new DBDataItem();
                    //这名称要主要，因为目标数据库是SDE，名称前面带数据库用户，与源数据库的本地数据库格式有异
                    tDBDataItem.Name = tFeatureDatasetTarget.Name;
                    tDBDataItem.Type = DataType.DataSet;
                    tDBDataItems.Add(tDBDataItem);

                    //获取目标数据集的所有要素类，用于判断是否存在同名要素类
                    List<DBDataItem> tDBDataItemTarget = DataRecoveryHelper.GetFcDBDataItemInDataset(tFeatureDatasetTarget);

                    //遍历源数据集中的要素类
                    IFeatureClassContainer tFeatureClassContainerSource = tGeoDatasetSource as IFeatureClassContainer;
                    IFeatureClassContainer tFeatureClassContainerTarget = tFeatureDatasetTarget as IFeatureClassContainer;
                    IEnumFeatureClass tEnumFeatureClass = tFeatureClassContainerSource.Classes;
                    tEnumFeatureClass.Reset();
                    IFeatureClass tFeatureClassSource = tEnumFeatureClass.Next();
                    while (tFeatureClassSource != null)
                    {
                        IDataset tDatasetFcSource = tFeatureClassSource as IDataset;
                        IFeatureClass tFeatureClassTarget = null;

                        m_TrackProgress.SubMessage = strSubMsgHead + "要素类 " + tDatasetFcSource.Name;
                        Application.DoEvents();
                        //检查目标数据集是否有同名要素类
                        DBDataItem tDBDataItemFcTarget = tDBDataItemTarget.FirstOrDefault(t => t.NameWithoutDBUser == tDatasetFcSource.Name);
                        bool isExistFc = tDBDataItemFcTarget != null ? true : false;
                        //如果存在且重复的要覆盖，先把目标数据集的同名要素类删除
                        if (isExistFc == true && isCover == true)
                        {
                            tFeatureClassTarget = tFeatureClassContainerTarget.get_ClassByName(tDBDataItemFcTarget.Name);
                     
                            DataRecoveryHelper.DeleteFeatureClass(tFeatureClassTarget);
                            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClassTarget);
                        }

                        if (isExistFc == false || (isExistFc == true && isCover == true))
                        {
                            List<DBDataItem> tDBDataItemsChild = tDBDataItem.Childs;

                            DataRecoveryHelper.CreateFeatureClasssReal(tFeatureClassSource, tFeatureClassTarget, tFeatureWorkspaceAnnoTarget,
                                tFeatureDatasetTarget, null, tFieldChecker, ref  tDBDataItemsChild);
                        }

                        ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClassTarget);
                        ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClassSource);
                        HandleStop();
                        tFeatureClassSource = tEnumFeatureClass.Next();
                    }
                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tEnumFeatureClass);
                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureDatasetTarget);
                }

                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureDatasetSource);
                HandleStop();
                tFeatureDatasetSource = tEnumDatasetSource.Next() as IFeatureDataset;
            }
            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tEnumDatasetSource);
        }

        /// <summary>
        /// 创建数据库根目录下的要素类
        /// </summary>
        /// <param name="tWorkspaceTarget"></param>
        /// <param name="tFeatureWorkspaceSource"></param>
        /// <param name="tDBDataItems"></param>
        /// <param name="isCover"></param>
        private void CreateFeatureClass(IWorkspace2 tWorkspaceTarget, IFeatureWorkspace tFeatureWorkspaceSource,
        ref List<DBDataItem> tDBDataItems, bool isCover)
        {
            IWorkspace tWorkspaceSource = tFeatureWorkspaceSource as IWorkspace;
            IFeatureWorkspace tFeatureWorkspaceTarget = tWorkspaceTarget as IFeatureWorkspace;
            IFeatureWorkspaceAnno tFeatureWorkspaceAnnoTarget = tWorkspaceTarget as IFeatureWorkspaceAnno;
            IFieldChecker tFieldChecker = new FieldCheckerClass();
            tFieldChecker.ValidateWorkspace = tWorkspaceTarget as IWorkspace;

            //首先遍历源数据库下的要素类
            IEnumDataset tEnumDatasetSource = tWorkspaceSource.get_Datasets(esriDatasetType.esriDTFeatureClass);
            tEnumDatasetSource.Reset();
            IFeatureClass tFeatureClassSource = tEnumDatasetSource.Next() as IFeatureClass;

            string strSubMsgHead = "正在建立";

            while (tFeatureClassSource != null)
            {
                IDataset tDatasetSource = tFeatureClassSource as IDataset;

                m_TrackProgress.SubMessage = strSubMsgHead + "要素类 " + tDatasetSource.Name;
                Application.DoEvents();

                bool isExist = tWorkspaceTarget.get_NameExists(esriDatasetType.esriDTFeatureClass, tDatasetSource.Name);
                if (isExist == true && isCover == true)
                {
                    //删除已有的要素类
                    IFeatureClass tFeatureClassTargetOld = tFeatureWorkspaceTarget.OpenFeatureClass(tDatasetSource.Name) as IFeatureClass;                  
                    DataRecoveryHelper.DeleteFeatureClass(tFeatureClassTargetOld);
                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClassTargetOld);
                }

                if (isExist == false || (isExist == true && isCover == true))
                {
                    IFeatureClass tFeatureClassTarget = null;
                    //创建要素类
                    DataRecoveryHelper.CreateFeatureClasssReal(tFeatureClassSource, tFeatureClassTarget, tFeatureWorkspaceAnnoTarget,
                           null, tFeatureWorkspaceTarget, tFieldChecker, ref  tDBDataItems);

                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClassTarget);
                }

                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClassSource);
                HandleStop();
                tFeatureClassSource = tEnumDatasetSource.Next() as IFeatureClass;
            }
            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tEnumDatasetSource);
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="tWorkspaceSource"></param>
        /// <param name="tFeatureWorkspaceTarget"></param>
        /// <param name="tDBDataItems"></param>
        private void WriteData(IWorkspace tWorkspaceSource, IFeatureWorkspace tFeatureWorkspaceTarget,
          List<DBDataItem> tDBDataItems)
        {
            int featureClassCount = DataBackupHelper.GetDataItemCount(tDBDataItems);

            m_TrackProgress.TotalMax = featureClassCount;
            m_TrackProgress.TotalValue = 0;
            m_TrackProgress.TotalMessage = "正在复制要素类，第1个（共" + m_TrackProgress.TotalMax.ToString() + "个）";
            m_TrackProgress.SubMessage = "";
            Application.DoEvents();
          
            IWorkspace tWorkspaceTarget = tFeatureWorkspaceTarget as IWorkspace;
            IFeatureWorkspace tFeatureWorkspaceSource = tWorkspaceSource as IFeatureWorkspace;
            IWorkspaceEdit tWorkspaceEditTarget = tFeatureWorkspaceTarget as IWorkspaceEdit;

            //首先遍历数据集
            IEnumDataset tEnumDatasetSource = tWorkspaceSource.get_Datasets(esriDatasetType.esriDTFeatureDataset);
            tEnumDatasetSource.Reset();
            IFeatureDataset tFeatureDatasetSource = tEnumDatasetSource.Next() as IFeatureDataset;
            while (tFeatureDatasetSource != null)
            {
                //数据集对应的对象，用于获取源数据的名称
                DBDataItem tDBDataItemDs = tDBDataItems.FirstOrDefault(t => t.Type == DataType.DataSet &&
                    t.NameWithoutDBUser == tFeatureDatasetSource.Name);

                if (tDBDataItemDs != null)
                {
                    IFeatureDataset tFeatureDatasetTarget = tFeatureWorkspaceTarget.OpenFeatureDataset(tDBDataItemDs.Name);
                    IFeatureClassContainer tFeatureClassContainerSource = tFeatureDatasetSource as IFeatureClassContainer;
                    IFeatureClassContainer tFeatureClassContainerTarget = tFeatureDatasetTarget as IFeatureClassContainer;
                    IEnumFeatureClass tEnumFeatureClassSource = tFeatureClassContainerSource.Classes;
                    tEnumFeatureClassSource.Reset();
                    IFeatureClass tFeatureClassSource = tEnumFeatureClassSource.Next();
                    //遍历数据集里的要素类
                    while (tFeatureClassSource != null)
                    {
                        IDataset tDatasetSource = tFeatureClassSource as IDataset;
                        //数据集对应的对象，用于获取源数据的名称
                        DBDataItem tDBDataItemDsFc = tDBDataItemDs.Childs.FirstOrDefault(t => t.Type == DataType.FeatureClass &&
                      t.NameWithoutDBUser == tDatasetSource.Name);

                        if (tDBDataItemDsFc != null)
                        {
                            m_TrackProgress.TotalMessage = "正在复制要素类" + tDatasetSource.Name + "，第" +
                                (m_TrackProgress.TotalValue + 1).ToString() + "个（共" + m_TrackProgress.TotalMax.ToString() + "个）";
                            Application.DoEvents();

                            IFeatureClass tFeatureClassTarget = tFeatureClassContainerTarget.get_ClassByName(tDBDataItemDsFc.Name);
                            //复制数据                         
                            DataRecoveryHelper.CopyFeatureClassData(tFeatureClassSource, tFeatureClassTarget, tWorkspaceEditTarget, m_TrackProgress);

                            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClassTarget);

                            m_TrackProgress.TotalValue++;
                        }
                        ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClassSource);
                        tFeatureClassSource = tEnumFeatureClassSource.Next();
                    
                        HandleStop();
                    }
                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tEnumFeatureClassSource);
                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureDatasetTarget);               
                }
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureDatasetSource);
                tFeatureDatasetSource = tEnumDatasetSource.Next() as IFeatureDataset;
            }
            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tEnumDatasetSource);

            //然后遍历在数据库根目录的要素类
            tEnumDatasetSource = tWorkspaceSource.get_Datasets(esriDatasetType.esriDTFeatureClass);
            tEnumDatasetSource.Reset();
            IFeatureClass tFeatureClassSource2 = tEnumDatasetSource.Next() as IFeatureClass;
            while (tFeatureClassSource2 != null)
            {
                IDataset tDatasetSource = tFeatureClassSource2 as IDataset;

                //数据集对应的对象，用于获取源数据的名称
                DBDataItem tDBDataItemFc = tDBDataItems.FirstOrDefault(t => t.Type == DataType.FeatureClass &&
                    t.NameWithoutDBUser == tDatasetSource.Name);

                if (tDBDataItemFc != null)
                {
                    m_TrackProgress.TotalMessage = "正在复制要素类" + tDatasetSource.Name + "，第" +
                          (m_TrackProgress.TotalValue + 1).ToString() + "个（共" + m_TrackProgress.TotalMax.ToString() + "个）";
                    Application.DoEvents();

                    IFeatureClass tFeatureClassTarget = tFeatureWorkspaceTarget.OpenFeatureClass(tDBDataItemFc.Name);
                    //复制数据
                    DataRecoveryHelper.CopyFeatureClassData(tFeatureClassSource2, tFeatureClassTarget, tWorkspaceEditTarget, m_TrackProgress);

                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClassTarget);

                    m_TrackProgress.TotalValue++;
                }
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClassSource2);
          
                tFeatureClassSource2 = tEnumDatasetSource.Next() as IFeatureClass;
              
                HandleStop();
            }
            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tEnumDatasetSource);
        }

        #endregion

        #region 其他

        /// <summary>
        /// 检查输入信息是否正确和完整
        /// </summary>
        /// <returns></returns>
        private bool CheckInputInfo()
        {
            if (m_DataSourceTarget == null)
            {
                MessageBox.Show("请选择恢复数据源");
                return false;
            }

            if (m_FeatureWorkspaceSource == null)
            {
                MessageBox.Show("请选择备份数据库");
                return false;
            }

            return true;
        }

        private void btnBackupPath_Click(object sender, EventArgs e)
        {
            try
            {
                AG.COM.SDM.Catalog.IDataBrowser tDataBrowser = new FormDataBrowser();
                tDataBrowser.AddFilter(new FileGeoDatabaseFilter());
                tDataBrowser.MultiSelect = false;
                if (tDataBrowser.ShowDialog() == DialogResult.OK)
                {
                    IList<DataItem> items = tDataBrowser.SelectedItems;
                    if (items.Count == 0) return;

                    DataItem tDataItem = items[0];
                    //暂时只支持文件数据库
                    if (tDataItem is FileGdbItem)
                    {
                        FileGdbItem tFileGdbItem = tDataItem as FileGdbItem;
                        txtBackupPath.Text = tFileGdbItem.FileName;
                        //暂存工作空间
                        m_FeatureWorkspaceSource = tFileGdbItem.GetGeoObject() as IFeatureWorkspace;
                    }
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        /// <summary>
        /// 进度条停止操作判断并处理
        /// </summary>
        private void HandleStop()
        {
            if (m_TrackProgress.IsContinue == false)
            {
                throw new Exception("停止操作");
            }
        }

        /// <summary>
        /// 设置输入类型控件的Enabled值
        /// </summary>
        /// <param name="tEnabled"></param>
        private void EnableInputControl(bool tEnabled)
        {
            btnBackupPath.Enabled = tEnabled;
            btnOK.Enabled = tEnabled;
            btnCancle.Enabled = tEnabled;
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
