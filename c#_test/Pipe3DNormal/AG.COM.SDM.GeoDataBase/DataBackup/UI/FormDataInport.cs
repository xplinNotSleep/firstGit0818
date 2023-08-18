using AG.COM.SDM.Catalog;
using AG.COM.SDM.Catalog.DataItems;
using AG.COM.SDM.Catalog.Filters;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.GeoDataBase.DataBackup
{
    /// <summary>
    /// 数据录入类
    /// </summary>
    public partial class FormDataInport : Form
    {
        private IDataset m_DatasetTarget;
        private FieldsRuleConfig m_fieldsRuleConfig = new FieldsRuleConfig();//字段对照规则配置
        private const string C_NoFieldValue = "<空字段>";
        private List<InportError> m_inportError = new List<InportError>();

        public FormDataInport()
        {
            InitializeComponent();
        }

        #region 窗体事件

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {          
            m_inportError.Clear();
            ITrackProgress tTrackProgress = new TrackProgressDialog();
            tTrackProgress.SubMax = this.listLayers.Items.Count;
            tTrackProgress.Show();

            for (int i = 0; i < this.listLayers.Items.Count; i++)
            {
                ListViewItem tListViewItem = this.listLayers.Items[i];
                IDataset tDatasetInput = tListViewItem.Tag as IDataset;

                //进度条控件显示状态
                if (tTrackProgress.IsContinue == false) return;
                tTrackProgress.SubValue = i + 1;
                tTrackProgress.SubMessage = string.Format("正在处理 {0} 数据……", tDatasetInput.Name);
                Application.DoEvents();

                if (tDatasetInput is IFeatureClass)
                {
                    InportFeatureClass(tDatasetInput, tTrackProgress);
                }     
            }
            tTrackProgress.SetFinish();

            tabControl1.SelectedTab = tabPage2;
            AddErrorToDatagridView(m_inportError);
            if (m_inportError.Any(t => t.IsError == true))
            {
                MessageBox.Show("导入完成！然而在导入过程出现异常，详情请看以下列表", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);              
            }
            else
            {
                MessageBox.Show("导入完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            IDataBrowser tDataBrowser = new FormDataBrowser();
            tDataBrowser.AddFilter(new FeatureClassFilter());
            tDataBrowser.AddFilter(new TableFilter());
            tDataBrowser.AddFilter(new SDEWorkspaceFilter());
            tDataBrowser.AddFilter(new PersonalGeoDatabaseFilter());
            tDataBrowser.AddFilter(new FileGeoDatabaseFilter());
            tDataBrowser.AddFilter(new FeatureDatasetFilter());
            tDataBrowser.AddFilter(new AnnoFeatureClassFilter()); 
            if (tDataBrowser.ShowDialog() == DialogResult.OK)
            {
                IList<DataItem> tListDataItem = tDataBrowser.SelectedItems;
                for (int i = 0; i < tListDataItem.Count; i++)
                {
                    IDataset tDataset = tListDataItem[i].GetGeoObject() as IDataset;
                    if (tDataset == null) return;

                    
                        AddDatasetToListItem(tDataset);
                        tbLayers.Text = tDataset.Name;
                }
            }
            InitialSourceLayerList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.listLayers.SelectedItems.Count > 0)
            {
                for (int i = 0; i < this.listLayers.SelectedItems.Count; i++)
                {
                    ListViewItem tListViewItem = this.listLayers.SelectedItems[i];
                    this.listLayers.Items.Remove(tListViewItem);
                }
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            ListViewItem tSelListViewItem = this.listLayers.SelectedItems[0];
            if (tSelListViewItem != null)
            {
                int index = this.listLayers.Items.IndexOf(tSelListViewItem);
                if (index > 0)
                {
                    this.listLayers.SuspendLayout();
                    this.listLayers.Items.Remove(tSelListViewItem);
                    this.listLayers.Items.Insert(index - 1, tSelListViewItem);
                    //设置其为选择状态
                    tSelListViewItem.Selected = true;
                    this.listLayers.ResumeLayout();
                }
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            ListViewItem tSelListViewItem = this.listLayers.SelectedItems[0];
            if (tSelListViewItem != null)
            {
                int index = this.listLayers.Items.IndexOf(tSelListViewItem);
                if (index < this.listLayers.Items.Count - 1)
                {
                    this.listLayers.SuspendLayout();
                    this.listLayers.Items.Remove(tSelListViewItem);
                    this.listLayers.Items.Insert(index + 1, tSelListViewItem);
                    //设置其为选择状态
                    tSelListViewItem.Selected = true;
                    this.listLayers.ResumeLayout(true);
                }
            }
        }

        private void btnGetExportLocation_Click(object sender, EventArgs e)
        {
            IDataBrowser tDataBrowser = new FormDataBrowser();
            tDataBrowser.AddFilter(new WorkspaceFilter());
            tDataBrowser.AddFilter(new FolderFilter());
            tDataBrowser.AddFilter(new SDEWorkspaceFilter());
            tDataBrowser.AddFilter(new FeatureDatasetFilter());
            if (tDataBrowser.ShowDialog() == DialogResult.OK)
            {
                IList<DataItem> tListDataItem = tDataBrowser.SelectedItems;
                if (tListDataItem.Count > 0)
                {
                    IDataset tDataset = tListDataItem[0].GetGeoObject() as IDataset;
                    if (tDataset is IWorkspace)
                    {
                        m_DatasetTarget = tDataset;
                        this.txtLocationWorkspace.Text = tDataset.BrowseName;
                    }
                    else if (tDataset is IFeatureDataset)
                    {
                        m_DatasetTarget = tDataset;
                        this.txtLocationWorkspace.Text = m_DatasetTarget.Name;
                    }
                }
            }
        }

        private void listLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listLayers.SelectedItems.Count > 0)
            {
                this.btnDelete.Enabled = true;

                ListViewItem tListViewItem = this.listLayers.SelectedItems[0];
                int indexOf = this.listLayers.Items.IndexOf(tListViewItem);

                if (indexOf == this.listLayers.Items.Count - 1)
                {
                    this.btnMoveDown.Enabled = false;
                    this.btnMoveUp.Enabled = true;
                }
                else if (indexOf == 0)
                {
                    this.btnMoveUp.Enabled = false;
                    this.btnMoveDown.Enabled = true;
                }
                else
                {
                    this.btnMoveDown.Enabled = true;
                    this.btnMoveUp.Enabled = true;
                }
            }
            else
            {
                this.btnDelete.Enabled = false;
                this.btnMoveUp.Enabled = false;
                this.btnMoveDown.Enabled = false;
            }
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            if (btnDetail.Tag == null) return;
            string tTagString = btnDetail.Tag.ToString();
            if (tTagString == "0")
            {
                this.Height = 681;
                btnDetail.Text = "配置 ∧";
                btnDetail.Tag = "1";
            }
            else
            {
                this.Height = 375;
                btnDetail.Text = "配置 ∨";
                btnDetail.Tag = "0";
            }
        }

        private void cmbSourceLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            //更新字段规则配置
            IFeatureWorkspace tFeatureWorkspace = GetWorkspace(m_DatasetTarget);;
            if (tFeatureWorkspace == null) return;
            string tFeatClassName = cmbSourceLayers.Text;
            IDataset tDataset = GetListViewDatasetByName(cmbSourceLayers.Text);
            IDataset tTargetDataset = null;
            IFields tFields = GetDatasetFields(tDataset);
            try
            {
                if (tDataset is IFeatureClass)
                {
                    tTargetDataset = tFeatureWorkspace.OpenFeatureClass(tFeatClassName) as IDataset;
                }
                else if (tDataset is ITable)
                {
                    tTargetDataset = tFeatureWorkspace.OpenTable(tFeatClassName) as IDataset;
                }
            }
            catch
            {
                //
            }
            if (tTargetDataset != null)
            {
                cmbTargetLayers.Tag = tTargetDataset;
                cmbTargetLayers.Text = tTargetDataset.Name;
                RefreshDataGridView();
                UpdateDgvTargetFieldsList(tFields);
            }
        }

        private void cmbTargetLayers_TextChanged(object sender, EventArgs e)
        {
            InitialDataGridViewColumnStyle();
        }

        private void FormDataInport_Load(object sender, EventArgs e)
        {
            this.dgvDetailList.DataError += delegate(object sender2, DataGridViewDataErrorEventArgs e2) { };
        }

        private void dgvDetailList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

        }

        private void dgvDetailList_MouseLeave(object sender, EventArgs e)
        {
            if (!CheckContractValid())
            {
                btnOK.Enabled = false;
                dgvDetailList.Focus();
            }
            else
            {
                UpateFieldsRule(); 
                btnOK.Enabled = true;
            }
        }

        #endregion

        #region 自定义

        void AddErrorToDatagridView(List<InportError> pInportErrorList)
        {
            dataGridView1.Rows.Clear();
            if (pInportErrorList == null) return;
            int i = 1;
            foreach (InportError tError in pInportErrorList)
            {
                DataGridViewRow tRow =dataGridView1.Rows[dataGridView1.Rows.Add()];
                tRow.Cells[0].Value = i++;
                tRow.Cells[1].Value = tError.SourceFeatureDatasetName;
                tRow.Cells[2].Value = tError.TargetFeatureDatasetName;
                tRow.Cells[3].Value = tError.ErrorMessage; 
            }
        }

        IFeatureWorkspace GetWorkspace(IDataset pDataset)
        {
            if (pDataset is IFeatureClass)
                return null;
            else if (pDataset is IFeatureDataset)
            {
                return pDataset.Workspace as IFeatureWorkspace;
            }
            else if (pDataset is IWorkspace)
                return pDataset as IFeatureWorkspace;
            else
                return null;
        }

        //导入要素
        void InportFeatureClass(IDataset tDatasetInput, ITrackProgress pTrackProgress)
        {
            //源FeatureClass名称
            string sourceFeatureClassName = "";
            //目标FeatureClass名称
            string targetFeatureClassName = "";
            //导入总数
            int importCount = 0;
            //结果总数
            int resultCount = 0;
            //错误提示字段的值
            string infoFieldValue = "";
            //错误信息，空代表没错误
            string errorMsg = "";
            try
            {
                IFeatureClass tFeatClassInput = tDatasetInput as IFeatureClass;
                //获取源FeatureClass名称
                sourceFeatureClassName = tDatasetInput.BrowseName;             
                if (tFeatClassInput == null) return;
                pTrackProgress.SubMessage = string.Format("正在处理 {0} 数据……", tFeatClassInput.AliasName);
                //新字段集合
                IFields tFieldsInput = GetOutputFields(tFeatClassInput.Fields);
                //创建要素类
                IDataset tParentDatasetTarget = m_DatasetTarget;
                IFeatureWorkspace tFeatWorkspaceTarget = GetWorkspace(m_DatasetTarget);
                if (tFeatWorkspaceTarget == null) return;
                bool bNeedDeal = true;//是否需要导入该图层
                IFeatureClass tFeatClassTarget = GetFeatureClassInDataset(tFeatWorkspaceTarget, tFeatClassInput, ref bNeedDeal);
                //获取目标FeatureClass名称
                targetFeatureClassName = tFeatClassTarget != null ? tFeatClassTarget.AliasName : "";          
                if (bNeedDeal == false)
                {                 
                    throw new Exception("导入数据要素集不一致");
                }
                Dictionary<int, int> tDictFieldsRule;
                if (tFeatClassTarget == null)
                {
                    tFeatClassTarget = GeoDBHelper.CreateFeatureClass(m_DatasetTarget, tDatasetInput.Name, tFeatClassInput.FeatureType, tFieldsInput, null, null, "");
                    tDictFieldsRule = m_fieldsRuleConfig.GetConstIndexContrast(tFeatClassInput.Fields, tFeatClassTarget.Fields);
                    //获取目标FeatureClass名称（新建FeatureClass在此时才获取）
                    targetFeatureClassName = tFeatClassTarget != null ? tFeatClassTarget.AliasName : "";
                    //如果该图层没有编辑过字段对照就利用默认的对照
                    if (tDictFieldsRule.Count == 0 && tFeatClassTarget != null)
                    {
                        tDictFieldsRule = m_fieldsRuleConfig.GetIndexContrast(tDatasetInput.Name, tFeatClassInput.Fields, tFeatClassTarget.Fields);
                    }
                }
                else
                {
                    tDictFieldsRule = m_fieldsRuleConfig.GetIndexContrast(tDatasetInput.Name, tFeatClassInput.Fields, tFeatClassTarget.Fields);
                }               

                if (tFeatClassTarget == null) return;
                //导入数据到指定的要素类
                DataImportDataToFeatureClass(tFeatClassInput, tFeatClassTarget, tDictFieldsRule, null,
                    ref infoFieldValue, ref importCount);           
                //获取导入后总数
                resultCount = tFeatClassTarget.FeatureCount(null);
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                //先获取错误信息，finally部分再写入导出记录
                errorMsg = ex.Message;           
            }
            finally
            {
                string outputMessage = "导入要素 " + importCount + " 条，导入后要素共 " + resultCount + " 条。";
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    outputMessage += "导入过程中出错，错误要素的实体编号为：" + infoFieldValue + "，具体错误信息为：" + errorMsg;
                }
                m_inportError.Add(new InportError(sourceFeatureClassName, targetFeatureClassName, outputMessage, !string.IsNullOrEmpty(errorMsg)));
            }
        }

        private void DataImportDataToFeatureClass(IFeatureClass pFeatureClassInput, IFeatureClass pFeatureClassTarget, Dictionary<int, int> pDictFields,
            IQueryFilter pQueryFilter, ref  string infoFieldValue, ref int importCount)
        {
            //得到当前工作空间
            IWorkspace tWorkspaceTarget = (pFeatureClassTarget as IDataset).Workspace;
            //得到当前编辑空间
            IWorkspaceEdit tWorkspaceEditTarget = AG.COM.SDM.Utility.Editor.LibEditor.GetNewEditableWorkspace(tWorkspaceTarget);

            try
            {
                tWorkspaceEditTarget.StartEditing(false);
                tWorkspaceEditTarget.StartEditOperation();

                bool hasOID = pFeatureClassInput.HasOID;

                //设置游标状态为插入
                IFeatureCursor tFeatureCursorTarget = pFeatureClassTarget.Insert(true);
                //获取源文件要素类的的记录集游标
                IFeatureCursor tFeatureCursorInput = pFeatureClassInput.Search(pQueryFilter, false);
                //返回匹配规则枚举数
                IDictionaryEnumerator tDictEnumerator = pDictFields.GetEnumerator();
                IFeature tFeatureInput = tFeatureCursorInput.NextFeature();
                while (tFeatureInput != null)
                {              
                    //先获取OID，用于错误时提示
                    if (hasOID == true)
                    {
                        infoFieldValue = Convert.ToString(tFeatureInput.OID);
                    }

                    //设置要素缓存
                    IFeatureBuffer tFeatureBufferTarget = pFeatureClassTarget.CreateFeatureBuffer();
                    //设置初始位置
                    tDictEnumerator.Reset();
                    while (tDictEnumerator.MoveNext() == true)
                    {
                        //获取当前要素字段值
                        object objValue = tFeatureInput.get_Value((int)tDictEnumerator.Value);

                        if (objValue.GetType() == typeof(System.DBNull))
                        {
                            #region 防止出现null值
                            IField tField = tFeatureBufferTarget.Fields.get_Field((int)tDictEnumerator.Key);
                            if (tField.Type == esriFieldType.esriFieldTypeDouble ||
                                tField.Type == esriFieldType.esriFieldTypeInteger ||
                                tField.Type == esriFieldType.esriFieldTypeSingle ||
                                tField.Type == esriFieldType.esriFieldTypeSmallInteger)
                            {
                                //设置值
                                tFeatureBufferTarget.set_Value((int)tDictEnumerator.Key, 0);
                            }
                            else if (tField.Type == esriFieldType.esriFieldTypeString)
                            {
                                //设置值
                                tFeatureBufferTarget.set_Value((int)tDictEnumerator.Key, "");
                            }
                            else if (tField.Type == esriFieldType.esriFieldTypeDate)
                            {
                                //设置值
                                tFeatureBufferTarget.set_Value((int)tDictEnumerator.Key, DateTime.MinValue);
                            }
                            #endregion
                        }
                        else
                        {
                            IField tField = tFeatureBufferTarget.Fields.get_Field((int)tDictEnumerator.Key);
                            if (tField.Editable)
                            {
                                //设置值
                                tFeatureBufferTarget.set_Value((int)tDictEnumerator.Key, objValue);
                            }
                        }
                    }
                    //设置几何对象
                    //坐标变换
                    IGeometry tGeometry = tFeatureInput.ShapeCopy;

                    tGeometry.SnapToSpatialReference();
                    ITopologicalOperator2 tTopo = tGeometry as ITopologicalOperator2;
                    if (tTopo != null)
                    {
                        tTopo.IsKnownSimple_2 = false;
                        tTopo.Simplify();
                    }

                    tFeatureBufferTarget.Shape = tGeometry;
                    //tFeatureBuffer.Shape = tFeature.ShapeCopy;
                    tFeatureInput = tFeatureCursorInput.NextFeature();
                    //插入记录
                    tFeatureCursorTarget.InsertFeature(tFeatureBufferTarget);
                    tFeatureCursorTarget.Flush();

                    importCount++;
                }

                //释放非托管资源
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursorTarget);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursorInput);

                tWorkspaceEditTarget.StopEditOperation();
                tWorkspaceEditTarget.StopEditing(true);            
            }
            catch (Exception ex)
            {
                //忽略编辑操作
                //tWorkspaceEditTarget.AbortEditOperation();
                tWorkspaceEditTarget.StopEditOperation();
                tWorkspaceEditTarget.StopEditing(false);             
                throw new Exception(ex.Message, ex);           
            }
        }

        IFeatureClass GetFeatureClassInDataset(IFeatureWorkspace pWorkspaceTarget,IFeatureClass pFeatClassInput,ref bool bNeedDeal)
        {
            IFeatureClass tFeatClassTarget = null;
            try
            {
                tFeatClassTarget = pWorkspaceTarget.OpenFeatureClass((pFeatClassInput as IDataset).BrowseName);
            }
            catch //没有该图层
            {
                return null;
            }
            
            //有该图层
            if (m_DatasetTarget is IWorkspace)
            {
                if (tFeatClassTarget.FeatureDataset == null)//目标在根目录
                {
                    if (pFeatClassInput.FeatureDataset == null)
                    {
                        bNeedDeal = true;
                        return tFeatClassTarget;
                    }
                    else
                    {
                        bNeedDeal = false;
                        return null;
                    }
                }
                else
                {
                    if (pFeatClassInput.FeatureDataset == null)
                    {
                        return null;
                    }
                    else
                    {
                        bNeedDeal = pFeatClassInput.FeatureDataset.Name == tFeatClassTarget.FeatureDataset.Name;
                        return pFeatClassInput.FeatureDataset.Name == tFeatClassTarget.FeatureDataset.Name ? tFeatClassTarget : null;
                    }
                }
            }
            else if(m_DatasetTarget is IFeatureDataset)
            {
                if (tFeatClassTarget.FeatureDataset == null)
                {
                    bNeedDeal = false;
                    return null;
                }
                else if (pFeatClassInput.FeatureDataset == null)
                {
                    return tFeatClassTarget;
                }
                else 
                {
                    bNeedDeal = tFeatClassTarget.FeatureDataset.Name == pFeatClassInput.FeatureDataset.Name;
                    return tFeatClassTarget.FeatureDataset.Name == pFeatClassInput.FeatureDataset.Name ? tFeatClassTarget : null ;
                }
            }

            return null;
        }

        IFeatureDataset GetFeatureDataset(IFeatureWorkspace pFeatWorkspace, string pFeatDatasetName)
        {
            if (pFeatWorkspace == null) return null;
            IFeatureDataset tFeatDataset = null;
            try
            {
                tFeatDataset = pFeatWorkspace.OpenFeatureDataset(pFeatDatasetName);
                return tFeatDataset;
            }
            catch
            {
                return null;
            }
        }

        //导入工作空间
        void InprotWorkspace(IDataset pDataset, ITrackProgress pTrackProgress)
        {
            IEnumDataset tEnumDataset = pDataset.Subsets;
            IDataset tChildDataset = tEnumDataset.Next();
            while (tChildDataset != null)
            {
                if (tChildDataset is IFeatureClass)
                {
                    InportFeatureClass(tChildDataset, pTrackProgress);
                }
                else if (tChildDataset is IFeatureDataset)
                {
                    InportDataset(tChildDataset, pTrackProgress);
                }
                tChildDataset = tEnumDataset.Next();
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(tEnumDataset);
        }

        void InportTable(IDataset pDataset, ITrackProgress pTrackProgress)
        {
            IFeatureWorkspace tFeatWorkspace = GetWorkspace(m_DatasetTarget);;
            ITable tSourceTable = pDataset as ITable;
            if (tSourceTable == null) return;
            ITable tTargetTable = null;
            Dictionary<int, int> tDictFieldsRule;
            try
            {
                tTargetTable = tFeatWorkspace.OpenTable(pDataset.Name);
                tDictFieldsRule = m_fieldsRuleConfig.GetIndexContrast(pDataset.Name, tSourceTable.Fields, tTargetTable.Fields);
            }
            catch
            {
                //创建属性表
                tTargetTable = GeoDBHelper.CreateTable(tFeatWorkspace, pDataset.Name, tSourceTable.Fields, null, null, "");
                tDictFieldsRule = m_fieldsRuleConfig.GetConstIndexContrast(tSourceTable.Fields,tTargetTable.Fields);
            }
            if (tTargetTable == null) return;
            //获取字段匹配规则 
            //导入数据到指定的要素类
            GeoDBHelper.ImportDataToTable(tSourceTable, tTargetTable, tDictFieldsRule, null);
        }

        //导入要素集
        void InportDataset(IDataset pDataset, ITrackProgress pTrackProgress)
        {
            IEnumDataset tEnumDataset = pDataset.Subsets;
            IDataset tChildDataset = tEnumDataset.Next();
            while (tChildDataset != null)
            {
                if (tChildDataset is IFeatureClass)
                {
                    InportFeatureClass(tChildDataset, pTrackProgress);
                }
                tChildDataset = tEnumDataset.Next();
            }
        }

        //检查用户输入参数正确定
        bool CheckValid()
        {
            if (this.txtLocationWorkspace.Text.Length == 0)
            {
                MessageBox.Show("请选择数据导入的工作空间.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (this.listLayers.Items.Count == 0)
            {
                MessageBox.Show("请选择要导入的图层或要素表.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;

        }

        /// <summary>
        /// 获取要导出的字段集合
        /// </summary>
        /// <returns>返回字段集合</returns>
        IFields GetOutputFields(IFields pInputFields)
        {
            //实例化字段集合对象
            IFieldsEdit tFieldsEdit = new FieldsClass();

            for (int i = 0; i < pInputFields.FieldCount; i++)
            {
                IField tField = pInputFields.get_Field(i) as IField;

                if (tField.Type == esriFieldType.esriFieldTypeOID)
                {
                    //确保OID字段命名规范
                    IFieldEdit tFieldEdit = new FieldClass();
                    tFieldEdit.AliasName_2 = "OBJECTID";
                    tFieldEdit.Name_2 = "OBJECTID";
                    tFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;

                    tFieldsEdit.AddField(tFieldEdit as IField);
                }
                else if (tField.Type == esriFieldType.esriFieldTypeGeometry)
                {
                    tFieldsEdit.AddField(tField);   //确保Shape字段不会被过滤掉 
                }
                else if (tField.Type == esriFieldType.esriFieldTypeRaster ||
                   tField.Type == esriFieldType.esriFieldTypeGUID ||
                   tField.Type == esriFieldType.esriFieldTypeGlobalID ||
                   tField.Name.ToUpper().Contains("SHAPE") == true)
                {
                    continue;
                }
                else
                {
                    tFieldsEdit.AddField(tField);
                }
            }

            //查询接口引用
            IFields tFields = tFieldsEdit as IFields;
            return tFields;
        }

        //初始化源图层列表(cmbSourceLayer)
        void InitialSourceLayerList()
        {
            for (int i = 0; i < listLayers.Items.Count; i++)
            {
                ListViewItem tListViewItem = listLayers.Items[i];
                IDataset tDataset = tListViewItem.Tag as IDataset;
                IninitalUseDataset(tDataset);
            }
        }

        //根据给定Dataset初始化源图层列表
        void IninitalUseDataset(IDataset pDataset)
        {
            if (pDataset == null) return;
            if (pDataset is IFeatureClass || pDataset is ITable)
            {
                if (!cmbSourceLayers.Items.Contains(pDataset.Name))
                    cmbSourceLayers.Items.Add(pDataset.Name);
                return;
            }
            if (pDataset.Subsets == null) return;
            IEnumDataset tEnumDataset = pDataset.Subsets;
            IDataset tDataset = tEnumDataset.Next();
            while (tDataset != null)
            {
                if (tDataset is IFeatureClass)
                {
                    if(!cmbSourceLayers.Items.Contains(tDataset.Name))
                        cmbSourceLayers.Items.Add(tDataset.Name);
                }
                else if (tDataset is ITable)
                {
                    if (!cmbSourceLayers.Items.Contains(tDataset.Name))
                        cmbSourceLayers.Items.Add(tDataset.Name);
                }
                else if (tDataset is IWorkspace)
                {
                    IninitalUseDataset(tDataset);
                }
                else if (tDataset is IDataset)
                {
                    IninitalUseDataset(tDataset);
                }

                tDataset = tEnumDataset.Next();
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(tEnumDataset);
        }

        /// <summary>
        /// 增加Dataset到源图层列表
        /// </summary>
        /// <param name="pDatasetName">新增列表项名称</param>
        void AddDatasetToLayerList(string pDatasetName)
        {
            cmbSourceLayers.Items.Add(pDatasetName);
        }

        //刷新DataGridView列表
        void RefreshDataGridView()
        {
            string tLayerName = cmbSourceLayers.Text;
            IDataset tSourceDataset = GetListViewDatasetByName(tLayerName);
            IFields tFields = GetDatasetFields(tSourceDataset);
            UpdateDgvSourceField(tFields);
        }

        //更新DataGridView的目标Dataset字段列表
        void UpdateDgvTargetFieldsList(IFields pFields)
        {
            if (pFields == null) return;

            for (int i = 0; i < dgvDetailList.Rows.Count; i++)
            {
                DataGridViewRow tRow = dgvDetailList.Rows[i];
                int indexName = pFields.FindField(tRow.Cells[0].Value.ToString());
                int indexAliasName = pFields.FindField(tRow.Cells[0].ToolTipText);

                if (indexName >= 0)
                {
                    tRow.Cells[1].Value = tRow.Cells[0].Value.ToString().ToUpper();
                }
                else if (indexAliasName >= 0)
                {
                    tRow.Cells[1].Value = tRow.Cells[0].ToolTipText;
                }
                else
                {
                    tRow.Cells[1].Value = C_NoFieldValue;
                }
            }
        }

        /// <summary>
        /// 更新DataGridView的源Dataset字段列表
        /// </summary>
        /// <param name="pFields">源图层列表</param>
        void UpdateDgvSourceField(IFields pFields)
        {
            if (pFields == null) return;
            dgvDetailList.Rows.Clear();
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                IField tField = pFields.get_Field(i);
                if (tField.Type == esriFieldType.esriFieldTypeOID || tField.Type == esriFieldType.esriFieldTypeGeometry) continue;
                DataGridViewRow tRow = dgvDetailList.Rows[dgvDetailList.Rows.Add()];
                tRow.Cells[0].Value = tField.Name;
                tRow.Cells[0].ToolTipText = tField.AliasName;
            }
        }

        //根据名称得到listLayers中的Dataset
        IDataset GetListViewDatasetByName(string pName)
        {
            foreach (ListViewItem tListViewItem in listLayers.Items)
            {
                if (tListViewItem.Text == pName)
                    return tListViewItem.Tag as IDataset;
            }
            return null;
        }

        //增加Dataset到listLayers列表中
        void AddDatasetToListItem(IDataset pDataset)
        {
            if (pDataset == null) return;
            if (pDataset is IFeatureClass)
            {
                IFeatureClass tFeatClass = pDataset as IFeatureClass;
                string tFolder = pDataset.Workspace.PathName;
                if (tFeatClass.FeatureDataset != null)
                {
                    tFolder += "\\" + tFeatClass.FeatureDataset.BrowseName;
                }
                ListViewItem tListViewItem = new ListViewItem();
                System.Windows.Forms.ListViewItem.ListViewSubItem tSubItem2 = new System.Windows.Forms.ListViewItem.ListViewSubItem(tListViewItem, tFolder);
                tListViewItem.SubItems.Add(tSubItem2);
                tListViewItem.Text = pDataset.Name;
                tListViewItem.Tag = pDataset;
                if (!CheckDatasetListDuplied(pDataset.Name, tFolder))
                    this.listLayers.Items.Add(tListViewItem);
            }
            else if (pDataset is ITable)
            {
                ITable tTable = pDataset as ITable;
                ListViewItem tListViewItem = new ListViewItem();
                System.Windows.Forms.ListViewItem.ListViewSubItem tSubItem2 = new System.Windows.Forms.ListViewItem.ListViewSubItem(tListViewItem, pDataset.Workspace.PathName);
                tListViewItem.SubItems.Add(tSubItem2);
                tListViewItem.Text = pDataset.Name;
                tListViewItem.Tag = pDataset;
                if (!CheckDatasetListDuplied(pDataset.Name, pDataset.Workspace.PathName))
                    this.listLayers.Items.Add(tListViewItem);
            }
            else
            {
                IEnumDataset tEnumDataset = pDataset.Subsets;
                IDataset tDataset = tEnumDataset.Next();
                while (tDataset != null)
                {
                    AddDatasetToListItem(tDataset);
                    tDataset = tEnumDataset.Next();
                }
            }
        }

        //检查listLayers的列表是否有给定值
        bool CheckDatasetListDuplied(string pDataSetName, string pFolder)
        {
            foreach (ListViewItem tListViewItem in listLayers.Items)
            {
                if (tListViewItem.SubItems.Count < 2) continue;
                if (tListViewItem.Text == pDataSetName && tListViewItem.SubItems[1].Text == pFolder)
                    return true;
            }
            return false;
        }

        //初始化DataGridView列
        void InitialDataGridViewColumnStyle()
        {
            dgvDetailList.Rows.Clear();
            dgvDetailList.Columns.Clear();
            DataGridViewTextBoxColumn tTextBoxColumn = new DataGridViewTextBoxColumn();
            tTextBoxColumn.HeaderText = "源 字 段";
            tTextBoxColumn.Width = 265;
            tTextBoxColumn.ReadOnly = true;
            dgvDetailList.Columns.Add(tTextBoxColumn);

            DataGridViewComboBoxColumn tComboColumn = new DataGridViewComboBoxColumn();
            tComboColumn.HeaderText = "库 字 段";
            tComboColumn.Width = 270;
            tComboColumn.DisplayStyleForCurrentCellOnly = true;
            tComboColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            tComboColumn.ReadOnly = false;
            tComboColumn.Name = "sequ";

            IFields tFields = GetDatasetFields(cmbTargetLayers.Tag as IDataset);
            if (tFields != null)
            {
                tComboColumn.Items.Add("<空字段>");
                for (int j = 0; j < tFields.FieldCount; j++)
                {
                    IField tField = tFields.get_Field(j);
                    if (tField.Type == esriFieldType.esriFieldTypeGeometry || tField.Type == esriFieldType.esriFieldTypeOID) continue;
                    tComboColumn.Items.Add(tField.Name.ToUpper());
                }
            }
            dgvDetailList.Columns.Add(tComboColumn);
        }
        // 更新字段规则表
        void UpateFieldsRule()
        {
            m_fieldsRuleConfig.Add(cmbSourceLayers.Text, GetStringTableFromDataGridView(dgvDetailList));
        }

        //得到字段对照表
        Dictionary<string, string> GetStringTableFromDataGridView(DataGridView dgv)
        {
            Dictionary<string, string> tDircionary = new Dictionary<string, string>();
            if (dgv == null || dgv.Columns.Count < 2) return tDircionary;
            foreach (DataGridViewRow tRow in dgv.Rows)
            {
                if (tRow.Cells[0].Value != null && tRow.Cells[1].Value != null && tRow.Cells[1].Value.ToString() != C_NoFieldValue)
                    tDircionary.Add(tRow.Cells[0].Value.ToString(), tRow.Cells[1].Value.ToString());
            }
            return tDircionary;
        }

        //检查对照表的正确性
        bool CheckContractValid()
        {
            string tLayerName = cmbSourceLayers.Text;
            foreach (DataGridViewRow tRow in dgvDetailList.Rows)
            {
                if (tRow.Cells[1].Value == null || tRow.Cells[1].Value.ToString().Equals(C_NoFieldValue)) continue;
                bool bDuplied = FieldDuplied(tRow.Cells[1].Value.ToString(), tRow.Index);
                if (bDuplied)
                {
                    MessageBox.Show("字段" + tRow.Cells[1].Value.ToString() + "被使用两次，请重新设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            return true;
        }

        //目标对照字段是否重复使用
        bool FieldDuplied(string pFieldName, int pRowIndex)
        {
            foreach (DataGridViewRow tRow in dgvDetailList.Rows)
            {
                if (tRow.Index != pRowIndex && tRow.Cells[1].Value != null && tRow.Cells[1].Value.ToString() == pFieldName)
                {
                    return true;
                }
            }
            return false;
        }

        IFields GetDatasetFields(IDataset pDataset)
        {
            if (pDataset is IFeatureClass)
                return (pDataset as IFeatureClass).Fields;
            else if (pDataset is ITable)
                return (pDataset as ITable).Fields;
            else
                return null;
        }
        #endregion

        private void btnExportResult_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_inportError != null && m_inportError.Count > 0)
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string result = "";
                        foreach (InportError tError in m_inportError)
                        {
                            result += tError.SourceFeatureDatasetName + "," + tError.TargetFeatureDatasetName + "," + tError.ErrorMessage + Environment.NewLine;
                        }

                        using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, false, Encoding.Default))
                        {
                            sw.Write(result);
                            sw.Close();
                        }
                    }                 
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

    }

    //字段规则配置类
    internal class FieldsRuleConfig
    {
        //字段配置信息结构为： key：图层名称 value：对照表
        private Hashtable m_hashTable = new Hashtable();

        Dictionary<string, string> GetStringTable(string pLayerName)
        {
            IDictionaryEnumerator enumerator = m_hashTable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                string strKey = enumerator.Key.ToString();
                if (strKey == pLayerName)
                {
                    return enumerator.Value as Dictionary<string, string>;
                }
            }
            return new Dictionary<string, string>();
        }

        //得到给定字段名称对应的配置字段索引
        int GetConfigValue(IFields pTargetFields, string pFieldName, string pLayerName)
        {
            if (pTargetFields == null) return -1;
            string pTargetFieldName = string.Empty;
            Dictionary<string, string> tStrDict = GetStringTable(pLayerName);
            foreach (KeyValuePair<string, string> tPair in tStrDict)
            {
                string strKey = tPair.Key;
                if (strKey == pFieldName)
                {
                    pTargetFieldName = tPair.Value;
                    break;
                }
            }

            return pTargetFields.FindField(pTargetFieldName);
        }


        /// <summary>
        /// 增加一条字段配置
        /// </summary>
        /// <param name="pLayerName">图层名称</param>
        /// <param name="pTable">字段名称对照表</param>
        public void Add(string pLayerName, Dictionary<string, string> pTable)
        {
            IDictionaryEnumerator enumerator = m_hashTable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                string strKey = enumerator.Key.ToString();
                if (strKey == pLayerName)
                {
                    m_hashTable[strKey] = pTable;
                    return;
                }
            }

            if (pLayerName != string.Empty && pTable.Count != 0)
                m_hashTable.Add(pLayerName, pTable);
        }

        //得到字段索引对照表,根据源字段和目标字段构建
        public Dictionary<int, int> GetIndexContrast(string pLayerName, IFields pSourceFields, IFields pTargetFields)
        {
            Dictionary<int, int> tDictOutputRule = new Dictionary<int, int>();

            if (pSourceFields == null || pTargetFields == null) return tDictOutputRule;
            Dictionary<string, string> strTable = GetStringTable(pLayerName);

            for (int i = 0; i < pSourceFields.FieldCount; i++)
            {
                IField tField = pSourceFields.get_Field(i);

                if (tField.Type == esriFieldType.esriFieldTypeOID ||
                    tField.Type == esriFieldType.esriFieldTypeRaster ||
                    tField.Type == esriFieldType.esriFieldTypeGUID ||
                    tField.Type == esriFieldType.esriFieldTypeGlobalID ||
                    tField.Type == esriFieldType.esriFieldTypeGeometry ||
                    tField.Name.ToUpper().Contains("SHAPE") == true)
                {
                    continue;
                }
                else
                {
                    //得到对照表中的值
                    int toIndex = GetConfigValue(pTargetFields, tField.Name, pLayerName);

                    if (toIndex == -1)
                    {
                        if (tField.Name.Length > 10)
                            toIndex = pTargetFields.FindField(tField.Name.Substring(0, 10));
                        else
                            toIndex = pTargetFields.FindField(tField.Name);
                    }

                    if (toIndex > -1)
                    {
                        //添加字段匹配规则
                        if (tDictOutputRule.ContainsKey(toIndex))
                            tDictOutputRule[toIndex] = i;
                        else
                            tDictOutputRule.Add(toIndex, i);
                    }
                }
            }
            return tDictOutputRule;
        }

        //得到字段索引对照表,根据源字段构建
        public Dictionary<int, int> GetConstIndexContrast(IFields pSourceFields,IFields pTargetFields)
        {
            Dictionary<int, int> tDictOutputRule = new Dictionary<int, int>();
            if (pSourceFields == null) return tDictOutputRule;
            for (int i = 0; i < pSourceFields.FieldCount; i++)
            {
                IField tField = pSourceFields.get_Field(i);

                if (tField.Type == esriFieldType.esriFieldTypeOID ||
                    tField.Type == esriFieldType.esriFieldTypeRaster ||
                    tField.Type == esriFieldType.esriFieldTypeGUID ||
                    tField.Type == esriFieldType.esriFieldTypeGlobalID ||
                    tField.Type == esriFieldType.esriFieldTypeGeometry ||
                    tField.Name.ToUpper().Contains("SHAPE") == true)
                {
                    continue;
                }
                else
                {
                    int index = pTargetFields.FindField(tField.Name);
                    if (index >= 0)
                    {
                        tDictOutputRule.Add(index,i);
                    }
                }
            }
            return tDictOutputRule;
        }
    }

    /// <summary>
    /// 导入错误接口
    /// </summary>
    public class InportError
    {
        private string m_ErrorMessage = "";
        private bool m_IsError = false;

        public string SourceFeatureDatasetName { get; set; }
        public string TargetFeatureDatasetName { get; set; }

        public bool IsError
        {
            get { return m_IsError; }
            set { m_IsError = value; }
        }

        public InportError(string sourceFeatDatasetName, string targetFeatDatasetName)
        {
            SourceFeatureDatasetName = sourceFeatDatasetName;
            TargetFeatureDatasetName = targetFeatDatasetName;
        }

        public InportError(string sourceFeatDatasetName, string targetFeatDatasetName, string errorMsg, bool IsError)
            : this(sourceFeatDatasetName, targetFeatDatasetName)
        {
            m_ErrorMessage = errorMsg;
            m_IsError = IsError;
        }

        public virtual string ErrorMessage
        {
            get { return m_ErrorMessage; }
        }
    }

    /// <summary>
    /// 要素集不一致错误
    /// </summary>
    public class FeatureDatasetNotConsistentError : InportError
    {
        public FeatureDatasetNotConsistentError(string sourceFeatDatasetName, string targetFeatDatasetName) : base(sourceFeatDatasetName, targetFeatDatasetName) 
        { } 

        public override string ErrorMessage
        {
            get 
            {
                return "导入数据要素集不一致";
            }
        }

    }

    /// <summary>
    /// 数据本身有误错误（如线图层出现长度为0，面图层面积为0的情况）
    /// </summary>
    public class DataSourceError : InportError
    {
        private string m_errorMessage = string.Empty;
        public DataSourceError(string sourceFeatDatasetName, string targetFeatDataseName, string errMessage)
            : base(sourceFeatDatasetName, targetFeatDataseName)
        {
            m_errorMessage = errMessage;
        }

        public override string ErrorMessage
        {
            get
            {
                return m_errorMessage;
            }
        }
    }
}
