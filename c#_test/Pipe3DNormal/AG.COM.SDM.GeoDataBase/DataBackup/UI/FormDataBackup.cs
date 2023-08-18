using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace AG.COM.SDM.GeoDataBase.DataBackup
{
    /// <summary>
    /// 数据备份窗体类
    /// </summary>
    public partial class FormDataBackup : SkinForm
    {
        #region 变量

        /// <summary>
        /// 注销描述
        /// </summary>
        private static string STR_CANCEL_DESP = "(已注销)";

        /// <summary>
        /// 进度条
        /// </summary>
        ITrackProgress m_TrackProgress = null;

        #endregion

        #region  初始化

        public FormDataBackup()
        {
            InitializeComponent();
        }

        private void FormDataBackup_Load(object sender, EventArgs e)
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

        #endregion

        #region 加载树节点

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
                //选择数据源后刷新当前数据源的数据集与要素类
                tvwData.Nodes.Clear();

                if (cmbDataSource.SelectedIndex < 0) return;
                AGSDM_DATASOURCE tDataSource = cmbDataSource.SelectedItem as AGSDM_DATASOURCE;
                if (tDataSource == null) return;

                EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
                string strHQL = string.Format("from AGSDM_DATASET as t where t.DATASOURCE_ID={0}", tDataSource.ID);
                IList tDataSetList = tEntityHandler.GetEntities(strHQL);
                for (int j = 0; j < tDataSetList.Count; j++)
                {
                    AGSDM_DATASET tDataSet = tDataSetList[j] as AGSDM_DATASET;
                    TreeNode tSetTreeNode = CreateTreeNodeByDataSet(tDataSet);
                    #region 添加叶节点
                    strHQL = string.Format("from AGSDM_LAYER as t where t.DATASET_ID={0}", tDataSet.ID);
                    IList tDataLayerList = tEntityHandler.GetEntities(strHQL);
                    for (int k = 0; k < tDataLayerList.Count; k++)
                    {
                        AGSDM_LAYER tDataLayer = tDataLayerList[k] as AGSDM_LAYER;
                        TreeNode tLayerNode = CreateTreeNodeByDataLayer(tDataLayer);
                        tSetTreeNode.Nodes.Add(tLayerNode);
                    }
                    #endregion
                    tvwData.Nodes.Add(tSetTreeNode);
                }
                strHQL = string.Format("from AGSDM_LAYER as t where t.DATASET_ID={0}", tDataSource.ID);
                IList tDataLayerList1 = tEntityHandler.GetEntities(strHQL);
                for (int k = 0; k < tDataLayerList1.Count; k++)
                {
                    AGSDM_LAYER tDataLayer = tDataLayerList1[k] as AGSDM_LAYER;
                    TreeNode tLayerNode = CreateTreeNodeByDataLayer(tDataLayer);
                    tvwData.Nodes.Add(tLayerNode);
                }
                tvwData.ExpandAll();
                if (tvwData.Nodes.Count > 0)
                    tvwData.SelectedNode = tvwData.Nodes[0];
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        /// <summary>
        /// 通过指定的数据生成叶节点
        /// </summary>
        /// <param name="pDataItem">数据目录</param>
        /// <returns>返回叶节点</returns>      
        private TreeNode CreateTreeNodeByDataLayer(AGSDM_LAYER pDataItem)
        {
            TreeNode tNode = new TreeNode();
            tNode.Text = pDataItem.LAYER_NAME;
            tNode.ImageIndex = GetImageIndex(pDataItem.FEATURE_TYPE);
            tNode.SelectedImageIndex = GetImageIndex(pDataItem.FEATURE_TYPE);
            if (CanReCancel(pDataItem.STATE))
            {
                tNode.Text += STR_CANCEL_DESP;
                tNode.ForeColor = Color.DarkGray;
            }
            tNode.Checked = true;
            tNode.Tag = pDataItem;
            return tNode;
        }

        /// <summary>
        /// 通过指定的数据生成子节点
        /// </summary>
        /// <param name="pDataItem">数据目录</param>
        /// <returns>返回子节点</returns>      
        private TreeNode CreateTreeNodeByDataSet(AGSDM_DATASET pDataItem)
        {
            TreeNode tNode = new TreeNode();
            tNode.Text = pDataItem.DATASET_NAME_CN;
            tNode.ImageIndex = 1;
            tNode.SelectedImageIndex = 1;
            if (CanReCancel(pDataItem.STATE))
            {
                tNode.Text += STR_CANCEL_DESP;
                tNode.ForeColor = Color.DarkGray;
            }
            tNode.Checked = true;
            tNode.Tag = pDataItem;
            return tNode;
        }

        private int GetImageIndex(string pLayerType)
        {
            switch (pLayerType)
            {
                case "点":
                    return 4;
                case "线":
                    return 3;
                case "面":
                    return 2;
                default:
                    return 3;
            }
        }

        /// <summary>
        /// 判断是否可以反注销
        /// </summary>
        /// <param name="value">状态值</param>
        /// <returns></returns>
        private bool CanReCancel(string value)
        {
            return value == "0";
        }

        #endregion

        #region  导出到备份位置

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                //获取被选择的数据集和要素类
                List<DBDataItem> tDBDataItems = DataBackupHelper.GetSelectData(tvwData);
                //检查录入信息
                if (CheckInputInfo(tDBDataItems) == false) return;

                EnableInputControl(false);

                m_TrackProgress = new TrackProgressDialog();
                //完成后不自动关闭
                m_TrackProgress.AutoFinishClose = true;
                m_TrackProgress.DisplayTotal = true;
                m_TrackProgress.TotalValue = 0;
                m_TrackProgress.TotalMessage = "";

                m_TrackProgress.SubMessage = "正在连接数据源......";
                m_TrackProgress.SubValue = 0;

                m_TrackProgress.Show();
                Application.DoEvents();

                //连接数据源，获取数据源Workspace
                IWorkspace2 tWorkspaceSource = DataBackupHelper.ConnectDataSource(cmbDataSource.SelectedItem as AGSDM_DATASOURCE);
                //建立备份目标的文件数据库
                IFeatureWorkspace tFeatureWorkspaceTarget = DataBackupHelper.CreateFileGDB(txtOutputPath.Text, txtOutputName.Text);
                //建立数据集和要素类
                int featureClassCount = CreateDataItem(tWorkspaceSource, tFeatureWorkspaceTarget, tDBDataItems);
                //复制数据
                WriteData(tWorkspaceSource, tFeatureWorkspaceTarget, featureClassCount, tDBDataItems);

                m_TrackProgress.SetFinish();

                EnableInputControl(true);

                AG.COM.SDM.Utility.Common.MessageHandler.ShowInfoMsg("备份完成", "提示");
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
        /// 建立数据集和要素类
        /// </summary>
        /// <param name="tWorkspaceSource"></param>
        /// <param name="tFeatureWorkspaceTarget"></param>
        /// <param name="tDBDataItems"></param>
        private int CreateDataItem(IWorkspace2 tWorkspaceSource, IFeatureWorkspace tFeatureWorkspaceTarget, List<DBDataItem> tDBDataItems)
        {
            IFeatureWorkspace tFeatureWorkspaceSource = tWorkspaceSource as IFeatureWorkspace;
            IFeatureWorkspaceAnno tFeatureWorkspaceAnnoTarget = tFeatureWorkspaceTarget as IFeatureWorkspaceAnno;
            IFieldChecker tFieldChecker = new FieldCheckerClass();
            tFieldChecker.ValidateWorkspace = tFeatureWorkspaceTarget as IWorkspace;

            //获取数据项总数
            int count = DataBackupHelper.GetDataItemCount(tDBDataItems);
            //根据创建的要素类计算的总数
            int realCount = 0;

            m_TrackProgress.SubValue = 0;
            m_TrackProgress.SubMax = count;

            string strSubMsgHead = "正在建立";

            foreach (DBDataItem tDBDataItem1 in tDBDataItems)
            {
                HandleStop();

                m_TrackProgress.SubValue++;
                m_TrackProgress.SubMessage = strSubMsgHead + tDBDataItem1.NameWithoutDBUser + "，第" + m_TrackProgress.SubValue.ToString()
                    + "条（共" + m_TrackProgress.SubMax + "条）";
                Application.DoEvents();

                //建立数据集，通过名称在源数据库中查找数据集
                if (tDBDataItem1.Type == DataType.DataSet &&
                    tWorkspaceSource.get_NameExists(esriDatasetType.esriDTFeatureDataset, tDBDataItem1.Name) == true)
                {
                    IGeoDataset tGeoDatasetSource = tFeatureWorkspaceSource.OpenFeatureDataset(tDBDataItem1.Name) as IGeoDataset;
                    IFeatureDataset tFeatureDatasetTarget = tFeatureWorkspaceTarget.CreateFeatureDataset(tDBDataItem1.NameWithoutDBUser, tGeoDatasetSource.SpatialReference);
                    //遍历数据集中的要素类，如果名称匹配的就建立
                    IFeatureClassContainer tFeatureClassContainerSource = tGeoDatasetSource as IFeatureClassContainer;
                    IEnumFeatureClass tEnumFeatureClass = tFeatureClassContainerSource.Classes;
                    tEnumFeatureClass.Reset();
                    IFeatureClass tFeatureClassSource = tEnumFeatureClass.Next();
                    while (tFeatureClassSource != null)
                    {
                        IDataset tDatasetFcSource = tFeatureClassSource as IDataset;
                        //要素类与数据源管理匹配名称
                        DBDataItem tDBDataItem2 = tDBDataItem1.Childs.FirstOrDefault(r => r.Type == DataType.FeatureClass &&
                            r.Name == tDatasetFcSource.Name);
                        if (tDBDataItem2 != null)
                        {
                            m_TrackProgress.SubValue++;
                            m_TrackProgress.SubMessage = strSubMsgHead + tDBDataItem2.NameWithoutDBUser + "，第" + m_TrackProgress.SubValue.ToString()
                                + "条（共" + m_TrackProgress.SubMax + "条）";
                            Application.DoEvents();
                            //由于元数据是SDE，目标是FileGDB，字段名称存在不兼容的可能，因此要验证字段
                            IFields tFieldsValid = null;
                            IEnumFieldError tEnumFieldError = null;
                            tFieldChecker.Validate(tFeatureClassSource.Fields, out tEnumFieldError, out tFieldsValid);

                            IFeatureClass tFeatureClassTarget = null;
                            //标注要素类与一般要素类建立有不同
                            if (tFeatureClassSource.FeatureType == esriFeatureType.esriFTAnnotation)
                            {
                                IAnnoClass tAnnoClass = tFeatureClassSource.Extension as IAnnoClass;

                                IGraphicsLayerScale tGraphicsLayerScale = new GraphicsLayerScaleClass();
                                tGraphicsLayerScale.Units = tAnnoClass.ReferenceScaleUnits;
                                tGraphicsLayerScale.ReferenceScale = tAnnoClass.ReferenceScale;

                                tFeatureClassTarget = tFeatureWorkspaceAnnoTarget.CreateAnnotationClass(tDBDataItem2.NameWithoutDBUser, tFieldsValid,
                                    tFeatureClassSource.CLSID, tFeatureClassSource.EXTCLSID, tFeatureClassSource.ShapeFieldName,
                                    "", tFeatureDatasetTarget, null, tAnnoClass.AnnoProperties, tGraphicsLayerScale,
                                    tAnnoClass.SymbolCollection, false);
                            }
                            else
                            {
                                tFeatureClassTarget = tFeatureDatasetTarget.CreateFeatureClass(tDBDataItem2.NameWithoutDBUser, tFieldsValid, tFeatureClassSource.CLSID,
                                     tFeatureClassSource.EXTCLSID, tFeatureClassSource.FeatureType, tFeatureClassSource.ShapeFieldName, "");
                            }
                            realCount++;

                            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClassTarget);
                        }

                        ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClassSource);
                        HandleStop();
                        tFeatureClassSource = tEnumFeatureClass.Next();
                    }
                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tEnumFeatureClass);
                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tGeoDatasetSource);
                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureDatasetTarget);
                }
                //建立在数据库根目录的要素类
                else if (tDBDataItem1.Type == DataType.FeatureClass &&
                    tWorkspaceSource.get_NameExists(esriDatasetType.esriDTFeatureClass, tDBDataItem1.Name) == true)
                {
                    IFeatureClass tFeatureClassSource = tFeatureWorkspaceSource.OpenFeatureClass(tDBDataItem1.Name);

                    //由于元数据是SDE，目标是FileGDB，字段名称存在不兼容的可能，因此要验证字段
                    IFields tFieldsValid = null;
                    IEnumFieldError tEnumFieldError = null;
                    tFieldChecker.Validate(tFeatureClassSource.Fields, out tEnumFieldError, out tFieldsValid);

                    IFeatureClass tFeatureClassTarget = null;
                    //标注要素类与一般要素类建立有不同
                    if (tFeatureClassSource.FeatureType == esriFeatureType.esriFTAnnotation)
                    {
                        IAnnoClass tAnnoClass = tFeatureClassSource.Extension as IAnnoClass;

                        IGraphicsLayerScale tGraphicsLayerScale = new GraphicsLayerScaleClass();
                        tGraphicsLayerScale.Units = tAnnoClass.ReferenceScaleUnits;
                        tGraphicsLayerScale.ReferenceScale = tAnnoClass.ReferenceScale;

                        tFeatureClassTarget = tFeatureWorkspaceAnnoTarget.CreateAnnotationClass(tDBDataItem1.NameWithoutDBUser, tFieldsValid,
                               tFeatureClassSource.CLSID, tFeatureClassSource.EXTCLSID, tFeatureClassSource.ShapeFieldName,
                               "", null, null, tAnnoClass.AnnoProperties, tGraphicsLayerScale,
                               tAnnoClass.SymbolCollection, false);
                    }
                    else
                    {
                        tFeatureClassTarget = tFeatureWorkspaceTarget.CreateFeatureClass(tDBDataItem1.NameWithoutDBUser, tFieldsValid, tFeatureClassSource.CLSID,
                                tFeatureClassSource.EXTCLSID, tFeatureClassSource.FeatureType, tFeatureClassSource.ShapeFieldName, "");
                    }
                    realCount++;

                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClassSource);
                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClassTarget);
                }
            }

            return realCount;
        }

        /// <summary>
        /// 复制数据
        /// </summary>
        /// <param name="tWorkspaceSource"></param>
        /// <param name="tFeatureWorkspaceTarget"></param>
        /// <param name="featureClassCount">要素类总数，用于进度条</param>
        private void WriteData(IWorkspace2 tWorkspaceSource, IFeatureWorkspace tFeatureWorkspaceTarget, int featureClassCount,
            List<DBDataItem> tDBDataItems)
        {
            m_TrackProgress.TotalMax = featureClassCount;
            m_TrackProgress.TotalValue = 0;
            m_TrackProgress.TotalMessage = "正在复制要素类，第1个（共" + m_TrackProgress.TotalMax.ToString() + "个）";
            m_TrackProgress.SubMessage = "";
            Application.DoEvents();

            IWorkspace tWorkspaceTarget = tFeatureWorkspaceTarget as IWorkspace;
            IFeatureWorkspace tFeatureWorkspaceSource = tWorkspaceSource as IFeatureWorkspace;
            IWorkspaceEdit tWorkspaceEditTarget = tFeatureWorkspaceTarget as IWorkspaceEdit;

            //首先遍历数据集
            IEnumDataset tEnumDatasetTarget = tWorkspaceTarget.get_Datasets(esriDatasetType.esriDTFeatureDataset);
            tEnumDatasetTarget.Reset();
            IFeatureDataset tFeatureDatasetTarget = tEnumDatasetTarget.Next() as IFeatureDataset;
            while (tFeatureDatasetTarget != null)
            {
                //数据集对应的对象，用于获取源数据的名称
                DBDataItem tDBDataItemDs = tDBDataItems.FirstOrDefault(t => t.Type == DataType.DataSet &&
                    t.NameWithoutDBUser == tFeatureDatasetTarget.Name);

                IFeatureClassContainer tFeatureClassContainerTarget = tFeatureDatasetTarget as IFeatureClassContainer;
                IFeatureDataset tFeatureDatasetSource = tFeatureWorkspaceSource.OpenFeatureDataset(tDBDataItemDs.Name);
                IFeatureClassContainer tFeatureClassContainerSource = tFeatureDatasetSource as IFeatureClassContainer;
                IEnumFeatureClass tEnumFeatureClassTarget = tFeatureClassContainerTarget.Classes;
                tEnumFeatureClassTarget.Reset();
                IFeatureClass tFeatureClassTarget = tEnumFeatureClassTarget.Next();
                //遍历数据集里的要素类
                while (tFeatureClassTarget != null)
                {
                    IDataset tDatasetTarget = tFeatureClassTarget as IDataset;
                    //数据集对应的对象，用于获取源数据的名称
                    DBDataItem tDBDataItemDsFc = tDBDataItemDs.Childs.FirstOrDefault(t => t.Type == DataType.FeatureClass &&
                  t.NameWithoutDBUser == tDatasetTarget.Name);

                    m_TrackProgress.TotalMessage = "正在复制要素类" + tDatasetTarget.Name + "，第" +
                        (m_TrackProgress.TotalValue + 1).ToString() + "个（共" + m_TrackProgress.TotalMax.ToString() + "个）";
                    Application.DoEvents();

                    IFeatureClass tFeatureClassSource = tFeatureClassContainerSource.get_ClassByName(tDBDataItemDsFc.Name);
                    //复制数据
                    CopyFeatureClassData(tFeatureClassSource, tFeatureClassTarget, tWorkspaceEditTarget);

                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClassTarget);
                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClassSource);
                    tFeatureClassTarget = tEnumFeatureClassTarget.Next();

                    m_TrackProgress.TotalValue++;
                    HandleStop();
                }
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tEnumFeatureClassTarget);
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureDatasetTarget);
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureDatasetSource);
                tFeatureDatasetTarget = tEnumDatasetTarget.Next() as IFeatureDataset;
            }
            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tEnumDatasetTarget);
            //然后遍历在数据库根目录的要素类
            tEnumDatasetTarget = tWorkspaceTarget.get_Datasets(esriDatasetType.esriDTFeatureClass);
            tEnumDatasetTarget.Reset();
            IFeatureClass tFeatureClassTarget2 = tEnumDatasetTarget.Next() as IFeatureClass;
            while (tFeatureClassTarget2 != null)
            {
                IDataset tDatasetTarget = tFeatureClassTarget2 as IDataset;

                //数据集对应的对象，用于获取源数据的名称
                DBDataItem tDBDataItemFc = tDBDataItems.FirstOrDefault(t => t.Type == DataType.FeatureClass &&
                    t.NameWithoutDBUser == tDatasetTarget.Name);

                m_TrackProgress.TotalMessage = "正在复制要素类" + tDatasetTarget.Name + "，第" +
                      (m_TrackProgress.TotalValue + 1).ToString() + "个（共" + m_TrackProgress.TotalMax.ToString() + "个）";
                Application.DoEvents();

                IFeatureClass tFeatureClassSource = tFeatureWorkspaceSource.OpenFeatureClass(tDBDataItemFc.NameWithoutDBUser);
                //复制数据
                CopyFeatureClassData(tFeatureClassSource, tFeatureClassTarget2, tWorkspaceEditTarget);

                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClassTarget2);
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureClassSource);
                tFeatureClassTarget2 = tEnumDatasetTarget.Next() as IFeatureClass;

                m_TrackProgress.TotalValue++;
                HandleStop();
            }
            ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tEnumDatasetTarget);
        }

        /// <summary>
        /// 复制要素类
        /// </summary>
        /// <param name="tFeatureClassSource"></param>
        /// <param name="tFeatureClassTarget"></param>
        /// <param name="tWorkspaceEditTarget"></param>
        private void CopyFeatureClassData(IFeatureClass tFeatureClassSource, IFeatureClass tFeatureClassTarget, IWorkspaceEdit tWorkspaceEditTarget)
        {
            IFeatureCursor tFeatureCursorSource = null, tFeatureCursorTarget = null;
            IFeatureBuffer tFeatureBufferTarget = null;
            IFeature tFeatureSource = null;

            tWorkspaceEditTarget.StartEditing(false);
            tWorkspaceEditTarget.StartEditOperation();

            try
            {
                //源要素类的数据复制到目标要素类
                tFeatureCursorSource = tFeatureClassSource.Search(null, false);
                tFeatureCursorTarget = tFeatureClassTarget.Insert(true);

                m_TrackProgress.SubValue = 0;
                m_TrackProgress.SubMax = tFeatureClassSource.FeatureCount(null);

                tFeatureSource = tFeatureCursorSource.NextFeature();
                while (tFeatureSource != null)
                {
                    m_TrackProgress.SubValue++;
                    m_TrackProgress.SubMessage = "正在复制第" + m_TrackProgress.SubValue.ToString()
                        + "条记录（共" + m_TrackProgress.SubMax + "条）";
                    Application.DoEvents();

                    tFeatureBufferTarget = tFeatureClassTarget.CreateFeatureBuffer();

                    tFeatureBufferTarget.Shape = tFeatureSource.ShapeCopy;
                    IFields tFieldsTarget = tFeatureClassTarget.Fields;
                    for (int i = 0; i < tFieldsTarget.FieldCount; i++)
                    {
                        IField tFieldTarget = tFieldsTarget.get_Field(i);
                        //几何字段另外赋值，不可编辑字段不复制
                        if (tFieldTarget.Type != esriFieldType.esriFieldTypeGeometry && tFieldTarget.Editable == true)
                        {
                            tFeatureBufferTarget.set_Value(i, tFeatureSource.get_Value(i));
                        }
                    }
                    tFeatureCursorTarget.InsertFeature(tFeatureBufferTarget);
                    
                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureBufferTarget);
                    ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureSource);

                    HandleStop();

                    tFeatureSource = tFeatureCursorSource.NextFeature();
                }

                tWorkspaceEditTarget.StopEditOperation();
                tWorkspaceEditTarget.StopEditing(true);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);

                tWorkspaceEditTarget.StopEditOperation();
                tWorkspaceEditTarget.StopEditing(false);

                throw ex;
            }
            finally
            {
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureCursorSource);
                ESRI.ArcGIS.ADF.ComReleaser.ReleaseCOMObject(tFeatureCursorTarget);
            }
        }

        #endregion

        #region 其他

        /// <summary>
        /// 检查输入信息是否正确和完整
        /// </summary>
        /// <returns></returns>
        private bool CheckInputInfo(List<DBDataItem> tDBDataItems)
        {
            if (!(cmbDataSource.SelectedItem is AGSDM_DATASOURCE))
            {
                MessageBox.Show("请选择数据源");
                return false;
            }

            if (tDBDataItems.Count <= 0)
            {
                MessageBox.Show("请选择备份的数据");
                return false;
            }

            if (string.IsNullOrEmpty(txtOutputPath.Text))
            {
                MessageBox.Show("请选择备份到GDB的位置");
                return false;
            }

            if (string.IsNullOrEmpty(txtOutputName.Text))
            {
                MessageBox.Show("请输入备份到GDB的名称");
                return false;
            }

            if (Directory.Exists(System.IO.Path.Combine(txtOutputPath.Text, txtOutputName.Text) + ".gdb"))
            {
                MessageBox.Show("备份到位置已存在同名的GDB，请输入其他名称");
                return false;
            }

            return true;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            //全选
            SetChecked(tvwData.Nodes, true);
        }

        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            //全不选            
            SetChecked(tvwData.Nodes, false);
        }

        /// <summary>
        /// 设置treeview节点的checked
        /// </summary>
        /// <param name="tNodes"></param>
        /// <param name="check"></param>
        private void SetChecked(TreeNodeCollection tNodes, bool check)
        {
            foreach (TreeNode tNodeChild in tNodes)
            {
                tNodeChild.Checked = check;

                SetChecked(tNodeChild.Nodes, check);
            }
        }

        private void tvwData_AfterCheck(object sender, TreeViewEventArgs e)
        {
            ControlHelper.TreeViewRelateSelect(e, TreeViewRelateSelectDirection.All);
        }

        private void btnOutputPath_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog tSaveFileDialog = new SaveFileDialog();
                tSaveFileDialog.Title = "请选择备份到GDB文件位置";
                tSaveFileDialog.Filter = "File GDB|*.*";
                if (tSaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtOutputPath.Text = System.IO.Path.GetDirectoryName(tSaveFileDialog.FileName);
                    txtOutputName.Text = System.IO.Path.GetFileNameWithoutExtension(tSaveFileDialog.FileName);
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
            cmbDataSource.Enabled = tEnabled;
            tvwData.Enabled = tEnabled;
            btnOutputPath.Enabled = tEnabled;
            txtOutputName.Enabled = tEnabled;
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
