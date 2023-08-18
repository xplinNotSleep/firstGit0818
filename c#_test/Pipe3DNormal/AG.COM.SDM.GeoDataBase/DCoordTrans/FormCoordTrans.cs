using AG.COM.SDM.Catalog;
using AG.COM.SDM.Catalog.DataItems;
using AG.COM.SDM.Catalog.Filters;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 通用坐标转换 窗体类
    /// </summary>
    public partial class FormCoordTrans : Form
    {
        private IWorkspace m_OutWorkspace;
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FormCoordTrans()
        {
            //初始化界面组件
            InitializeComponent();
        }

        private void btnOpenSrcFile_Click(object sender, EventArgs e)
        {              
            //设置过滤条件
            IDataItemFilter tDataItemFilter = new FeatureClassFilter();
            //实例化数据浏览窗体实例
            IDataBrowser tIDataBrowser = new FormDataBrowser();
            tIDataBrowser.AddFilter(new FeatureClassFilter());  

            //不可多选
            tIDataBrowser.MultiSelect = true;
            //添加过滤器
            tIDataBrowser.AddFilter(tDataItemFilter);

            if (tIDataBrowser.ShowDialog() == DialogResult.OK)
            {
                IList<DataItem> items = tIDataBrowser.SelectedItems;
                for (int i = 0; i < items.Count; i++)
                {
                    IDataset tDataset = items[i].GetGeoObject() as IDataset;
                    if (tDataset != null)
                    {
                        string strDatasetName=string.Format("{0}\\{1}",tDataset.Workspace.PathName,tDataset.Name);

                        //实例化ListViewItem对象
                        ListViewItem tListViewItem=new ListViewItem();
                        tListViewItem.Text=strDatasetName;

                        //获取其空间参考名称
                        string strSpatRefName=( tDataset as IGeoDataset).SpatialReference.Name;
                        if (string.Compare(strSpatRefName, "Unknown", true) == 0)
                        {
                            tListViewItem.SubItems.Add("否");
                        }
                        else
                        {
                            tListViewItem.SubItems.Add("是");
                        }

                        tListViewItem.Tag=tDataset;
                        //添加项
                        this.listFiles.Items.Add(tListViewItem);
                    }                    
                }

                if (items.Count > 0)
                {
                    this.btnDelete.Enabled = true;
                    this.btnClear.Enabled = true;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ListViewItem ListSelItem = this.listFiles.FocusedItem;
            if (ListSelItem != null)
            {
                this.listFiles.Items.Remove(ListSelItem);
                if (listFiles.Items.Count == 0)
                {
                    this.btnDelete.Enabled = false;
                    this.btnClear.Enabled = false;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //清空所有项
            this.listFiles.Items.Clear();
            this.btnDelete.Enabled = false;
            this.btnClear.Enabled = false;
        }

        private void listSrcFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            //设置删除按钮的可用性
            if (this.listFiles.SelectedIndices.Count ==0)             
                this.btnDelete.Enabled = false;            
            else             
                this.btnDelete.Enabled = true;           
        }

        private void btnOutPutLocation_Click(object sender, EventArgs e)
        {
            //实例化数据浏览窗体实例
            IDataBrowser tIDataBrowser = new FormDataBrowser();
            tIDataBrowser.AddFilter(new WorkspaceFilter());

            //不可多选
            tIDataBrowser.MultiSelect = false;

            if (tIDataBrowser.ShowDialog() == DialogResult.OK)
            {
                IList<DataItem> items = tIDataBrowser.SelectedItems;
                if (items.Count == 0) return;

                IWorkspace tWorkspace = items[0].GetGeoObject() as IWorkspace;
                if (tWorkspace != null)
                {
                    this.txtFolderPath.Text = items[0].Name;   
                    this.m_OutWorkspace = tWorkspace;
                }
            } 
        }

        private void btnCoordPrj_Click(object sender, EventArgs e)
        {
            //实例化对象
            OpenFileDialog tOpenFileDialog = new OpenFileDialog();
            tOpenFileDialog.Title = "选择输出坐标文件";
            tOpenFileDialog.Filter = "坐标文件(*.prj)|*.prj";
            tOpenFileDialog.InitialDirectory = AG.COM.SDM.Utility.CommonConstString.STR_PreAppPath;
            tOpenFileDialog.Multiselect = false;         
            tOpenFileDialog.RestoreDirectory = true;             

            if (tOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtSpatialReference.Text = tOpenFileDialog.FileName;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //检查数据填写是否规范
            if (CheckValid() == false) return;

            //实例化进度条对话框
            TrackProgressDialog tTrackProgressDialog = new TrackProgressDialog();
            //tTrackProgressDialog.DisplayTotal = true;
            tTrackProgressDialog.Show();
          
            //坐标投影变换处理
            ProjectTransHandler(tTrackProgressDialog);

            tTrackProgressDialog.Close();           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 检查数据填写是否规范
        /// </summary>
        /// <returns>如果规范则返回 true，否则返回 false</returns>
        private bool CheckValid()
        { 
            if (this.listFiles.Items.Count == 0)
            {
                MessageBox.Show("请选择需要转换的数据文件．", "提示！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (this.m_OutWorkspace == null)
            {
                MessageBox.Show("输出工作空间不能为空．", "提示！", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //if (this.txtFolderPath.Text.Trim().Length == 0)
            //{
            //    MessageBox.Show("请选择输出文件目录路径．", "提示！", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}
            //else
            //{
            //    if (System.IO.Directory.Exists(this.txtFolderPath.Text) == false)
            //    {
            //        MessageBox.Show("输出工作空间文件目录路径不存在．", "提示！", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return false;
            //    }
            //}

            if (this.txtSpatialReference.Text.Trim().Length == 0)
            {
                MessageBox.Show("请选择输出空间参考坐标系．", "提示！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
            {
                if (System.IO.File.Exists(this.txtSpatialReference.Text) == false)
                {
                    MessageBox.Show("输出工作空间参考坐标系文件路径不存在．", "提示！", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            
            return true;
        }

        /// <summary>
        /// 坐标投影变换处理
        /// </summary>
        /// <param name="pTrackProgressDlg">进度条对话框</param>
        private void ProjectTransHandler(TrackProgressDialog pTrackProgressDlg)
        {
            try
            {
                foreach (ListViewItem tListItem in listFiles.Items)
                {
                    if (tListItem.SubItems[1].Text == "否") continue;

                    if (pTrackProgressDlg.IsContinue == false) return;

                    //对象类型转换
                    IFeatureClass tFromFeatClass = tListItem.Tag as IFeatureClass;  

                    //删除工作空间中指定名称的要素类
                    if (GeoDBHelper.HasDeleteFile(this.m_OutWorkspace, (tFromFeatClass as IDataset).Name) == false) continue;
                    
                    //查询IWorkspaceEdit接口
                    IWorkspaceEdit tWorkspaceEdit = m_OutWorkspace as IWorkspaceEdit;
                    //开始编辑操作
                    tWorkspaceEdit.StartEditing(true);
                    tWorkspaceEdit.StartEditOperation();

                    //空间投影(From)
                    ISpatialReference tFromSpatial =(tFromFeatClass as IGeoDataset).SpatialReference;

                    //空间投影(To)
                    ISpatialReferenceFactory pSpatialFactory = new SpatialReferenceEnvironmentClass();
                    ISpatialReference tToSpatial = pSpatialFactory.CreateESRISpatialReferenceFromPRJFile(txtSpatialReference.Text);

                    //创建字段集
                    IFields destFields = GetFields(tFromFeatClass, tToSpatial);
                    //创建输出FeatureClass
                    IFeatureClass tToFeatClass = GeoDBHelper.CreateFeatureClass(m_OutWorkspace, (tFromFeatClass as IDataset).Name, esriFeatureType.esriFTSimple, destFields, null, null, "");

                    //设置转换方式
                    IGeoTransformation tTrans = new GeocentricTranslationClass();
                    tTrans.PutSpatialReferences(tFromSpatial, tToSpatial);

                    //投影变换
                    TranGeometry(tFromFeatClass, tToFeatClass, tTrans, tToSpatial, pTrackProgressDlg);

                    //查询接口引用
                    ISchemaLock tSchemaLock = tToFeatClass as ISchemaLock;
                    if (tSchemaLock != null)
                    {
                        //更改表的锁定状态
                        tSchemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock);
                    }

                    //停止编辑操作
                    tWorkspaceEdit.StopEditOperation();
                    tWorkspaceEdit.StopEditing(true); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误提示!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 坐标系转换
        /// </summary>
        /// <param name="mFromFeatClass">源FeatureClass</param>
        /// <param name="mToFeatClass">目标FeatureClass</param>
        /// <param name="mTrans">转换方式</param>
        /// <param name="mDestSpatial">目标投影坐标</param>
        /// <param name="pTrackProgressDlg">进度条对话框</param>
        private void TranGeometry(IFeatureClass mFromFeatClass, IFeatureClass mToFeatClass, IGeoTransformation mTrans, ISpatialReference mDestSpatial, TrackProgressDialog pTrackProgressDlg)
        {
            //设置子进度条显示范围的最大值
            pTrackProgressDlg.SubMax = mFromFeatClass.FeatureCount(null);
            int curvalue = 0;

            //分配要素缓存
            IFeatureBuffer pFeatBuffer = mToFeatClass.CreateFeatureBuffer();
            IFeatureCursor pToFeatCursor = mToFeatClass.Insert(true);

            //获取查询游标
            IFeatureCursor pFromCursor = mFromFeatClass.Search(null, false);

            for (IFeature pFromFeat = pFromCursor.NextFeature(); pFromFeat != null;pFromFeat = pFromCursor.NextFeature())
            {
                //判断运行状态
                if (pTrackProgressDlg.IsContinue == false) return;

                IGeometry2 pGeometry = pFromFeat.ShapeCopy as IGeometry2;
                pGeometry.ProjectEx(mDestSpatial, esriTransformDirection.esriTransformForward, mTrans, true, 0, 0);

                pFeatBuffer.Shape = pGeometry;

                //属性赋值
                for (int i = 0; i < pFromFeat.Fields.FieldCount; i++)
                {
                    IField pField = pFromFeat.Fields.get_Field(i);
                    if (pField.Type != esriFieldType.esriFieldTypeOID && pField.Type != esriFieldType.esriFieldTypeGUID
                        && pField.Type != esriFieldType.esriFieldTypeGlobalID && pField.Type != esriFieldType.esriFieldTypeGeometry)
                    {
                        object objValue = pFromFeat.get_Value(i);
                        if (objValue.ToString().Length > 0)
                        {
                            int fieldIndex = pFeatBuffer.Fields.FindField(pField.Name);

                            if (fieldIndex == -1)
                            {
                                //输出为Shape文件的情况
                                fieldIndex = pFeatBuffer.Fields.FindField(pField.Name.Substring(0, 10));
                            }

                            pFeatBuffer.set_Value(fieldIndex, objValue);
                        }
                    }
                }

                //新增要素
                pToFeatCursor.InsertFeature(pFeatBuffer);

                curvalue++;
                pTrackProgressDlg.SubValue = curvalue;
                pTrackProgressDlg.SubMessage="正在处理第" + curvalue.ToString() + "条记录";

                Application.DoEvents();
            }
        }

        /// <summary>
        /// 获取要创建的字段集
        /// </summary>
        /// <param name="pSrcFeatureClass">源要素类</param>
        /// <param name="pSpatialReference">空间关系</param>
        /// <returns>返回字段集</returns>
        private IFields GetFields(IFeatureClass pSrcFeatureClass, ISpatialReference pSpatialReference)
        {
            IFieldsEdit tFieldsEdit = new FieldsClass();

            IFields pSrcFields = pSrcFeatureClass.Fields;

            for (int i = 0; i < pSrcFields.FieldCount; i++)
            {
                IField tField = pSrcFields.get_Field(i);
                if (tField.Type == esriFieldType.esriFieldTypeGeometry)
                {
                    IGeometryDefEdit tGeometryDefEdit = tField.GeometryDef as IGeometryDefEdit;
                    tGeometryDefEdit.SpatialReference_2 = pSpatialReference;
                }

                tFieldsEdit.AddField(tField);
            }

            return (IFields)tFieldsEdit;
        }
    }
}