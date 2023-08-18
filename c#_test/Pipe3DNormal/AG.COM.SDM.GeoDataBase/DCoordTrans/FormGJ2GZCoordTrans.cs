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
    /// 国家到广州坐标转换
    /// </summary>
    public partial class FormGJ2GZCoordTrans : Form
    {
        //输出工作空间
        private IWorkspace m_OutWorkspace;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FormGJ2GZCoordTrans()
        {
            //初始化界面组件
            InitializeComponent();
        }

        private void FormGJ2GZCoordTrans_Load(object sender, EventArgs e)
        {
            //北京54到广州坐标转换类设置
            GJ2DFCoordTransformation tGJ2DFTransformation = new GJ2DFCoordTransformation();
            tGJ2DFTransformation.TransName = "北京54至广州坐标转换";
            tGJ2DFTransformation.XTranslation = 13731.829363131248;
            tGJ2DFTransformation.YTranslation = -29986.14979221805;
            tGJ2DFTransformation.RotationAngle = 0.004840873348379301;
            tGJ2DFTransformation.Scale = 0.999954188;           
            this.combTransType.Items.Add(tGJ2DFTransformation);

            //北京54至西安80坐标转换
            tGJ2DFTransformation = new GJ2DFCoordTransformation();
            tGJ2DFTransformation.TransName = "北京54至西安80坐标转换";
            tGJ2DFTransformation.XTranslation = -59.086066881827719;
            tGJ2DFTransformation.YTranslation = -59.241670671457541;
            tGJ2DFTransformation.RotationAngle = -0.0000013328012291310875;
            tGJ2DFTransformation.Scale = 1.000012482;
            this.combTransType.Items.Add(tGJ2DFTransformation);


            //西安80至广州坐标转换
            tGJ2DFTransformation = new GJ2DFCoordTransformation();
            tGJ2DFTransformation.TransName = "西安80至广州坐标转换 ";
            tGJ2DFTransformation.XTranslation = 13790.6244495477;
            tGJ2DFTransformation.YTranslation = -29926.6261804043; 
            tGJ2DFTransformation.RotationAngle = 0.00484220616136227;
            tGJ2DFTransformation.Scale = 0.999941706727689;
            this.combTransType.Items.Add(tGJ2DFTransformation);

            //西安80至北京54坐标转换
            tGJ2DFTransformation = new GJ2DFCoordTransformation();
            tGJ2DFTransformation.TransName = "西安80至北京54坐标转换";
            tGJ2DFTransformation.XTranslation = 59.085250363841624;
            tGJ2DFTransformation.YTranslation = 59.241009969919105;
            tGJ2DFTransformation.RotationAngle = 0.000001332801229313876;
            tGJ2DFTransformation.Scale = 0.99998752;
            this.combTransType.Items.Add(tGJ2DFTransformation);

            //西安80至2000国家坐标转换
            tGJ2DFTransformation = new GJ2DFCoordTransformation();
            tGJ2DFTransformation.TransName = "西安80至2000国家坐标转换";
            tGJ2DFTransformation.XTranslation = 59.085250363841624;
            tGJ2DFTransformation.YTranslation = 59.241009969919105;
            tGJ2DFTransformation.RotationAngle = 0.000001332801229313876;
            tGJ2DFTransformation.Scale = 0.99998752;
            this.combTransType.Items.Add(tGJ2DFTransformation);

            //广州至北京54坐标转换
            tGJ2DFTransformation = new GJ2DFCoordTransformation();
            tGJ2DFTransformation.TransName = "广州至北京54坐标转换";
            tGJ2DFTransformation.XTranslation = -13587.132341708118;
            tGJ2DFTransformation.YTranslation = 30053.649027313415;
            tGJ2DFTransformation.RotationAngle = -0.0048408733468931894;
            tGJ2DFTransformation.Scale = 1.000045813;
            this.combTransType.Items.Add(tGJ2DFTransformation);

            //广州至西安80坐标转换
            tGJ2DFTransformation = new GJ2DFCoordTransformation();
            tGJ2DFTransformation.TransName = "广州至西安80坐标转换";
            tGJ2DFTransformation.XTranslation = -13646.347937015507;
            tGJ2DFTransformation.YTranslation = 29994.800598071102;
            tGJ2DFTransformation.RotationAngle = -0.0048422061463885314;
            tGJ2DFTransformation.Scale = 1.000058296;
            this.combTransType.Items.Add(tGJ2DFTransformation);

            //广州至2000国家坐标转换
            tGJ2DFTransformation = new GJ2DFCoordTransformation();
            tGJ2DFTransformation.TransName = "广州至2000国家坐标转换";
            tGJ2DFTransformation.XTranslation = -13587.132341708118;
            tGJ2DFTransformation.YTranslation = 30053.649027313415;
            tGJ2DFTransformation.RotationAngle = -0.0048408733468931894;
            tGJ2DFTransformation.Scale = 1.000045813;
            this.combTransType.Items.Add(tGJ2DFTransformation);

            //2000国家坐标系至西安80坐标转换
            tGJ2DFTransformation = new GJ2DFCoordTransformation();
            tGJ2DFTransformation.TransName = "2000国家坐标系至西安80坐标转换";
            tGJ2DFTransformation.XTranslation = -59.086066881827719;
            tGJ2DFTransformation.YTranslation = -59.241670671457541;
            tGJ2DFTransformation.RotationAngle = -0.0000013328012291310875;
            tGJ2DFTransformation.Scale = 1.000012482;
            this.combTransType.Items.Add(tGJ2DFTransformation);

            //2000国家坐标系到广州坐标转换类设置
            tGJ2DFTransformation = new GJ2DFCoordTransformation();
            tGJ2DFTransformation.TransName = "2000国家坐标系至广州坐标转换";
            tGJ2DFTransformation.XTranslation = 13731.829363131248;
            tGJ2DFTransformation.YTranslation = -29986.14979221805;
            tGJ2DFTransformation.RotationAngle = 0.004840873348379301;
            tGJ2DFTransformation.Scale = 0.999954188;
            this.combTransType.Items.Add(tGJ2DFTransformation);

            this.combTransType.SelectedIndex = 0;
        }

        private void listSrcFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listSrcFiles.SelectedIndices.Count == 0)
            {
                this.btnDelete.Enabled = false;
            }
            else
            {
                this.btnDelete.Enabled = true;
            }
        }

        private void chkOnSrcFile_CheckedChanged(object sender, EventArgs e)
        {
            bool IsChecked = this.chkOnSrcFile.Checked;
            this.txtOutWorkspace.Enabled = !IsChecked;
            this.btnOutWorkspace.Enabled = !IsChecked;
        }

        private void btnOpenSrcFile_Click(object sender, EventArgs e)
        {
            //设置过滤条件
            IDataItemFilter tDataItemFilter = new FeatureClassFilter();
            //实例化数据浏览窗体实例
            IDataBrowser tIDataBrowser = new FormDataBrowser();            
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
                        string strDatasetName = string.Format("{0}\\{1}", tDataset.Workspace.PathName, tDataset.Name);

                        //实例化ListViewItem对象
                        ListViewItem tListViewItem = new ListViewItem();
                        tListViewItem.Text = strDatasetName;
                        tListViewItem.Tag = tDataset;
                        //添加项
                        this.listSrcFiles.Items.Add(tListViewItem);
                    }
                }

                if (this.listSrcFiles.Items.Count > 0)
                {
                    //this.btnDelete.Enabled = true;
                    this.btnClear.Enabled = true;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.listSrcFiles.SelectedItems.Count > 0)
            {
                ListViewItem objSel = this.listSrcFiles.SelectedItems[0];
                if (objSel != null)
                {
                    this.listSrcFiles.Items.Remove(objSel);
                    if (this.listSrcFiles.Items.Count == 0)
                    {
                        this.btnClear.Enabled = false;
                        this.btnDelete.Enabled = false;
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.listSrcFiles.Items.Clear();
            this.btnClear.Enabled = false;
            this.btnDelete.Enabled = false;
        }

        private void btnOutWorkspace_Click(object sender, EventArgs e)
        {
            //实例化数据浏览窗体实例
            IDataBrowser tIDataBrowser = new FormDataBrowser();
            tIDataBrowser.AddFilter(new WorkspaceFilter());
            tIDataBrowser.AddFilter(new FolderFilter());

            //不可多选
            tIDataBrowser.MultiSelect = false;

            if (tIDataBrowser.ShowDialog() == DialogResult.OK)
            {
                IList<DataItem> items = tIDataBrowser.SelectedItems;
                if (items.Count == 0) return;

                IWorkspace tWorkspace = items[0].GetGeoObject() as IWorkspace;
                if (tWorkspace != null)
                {
                    this.txtOutWorkspace.Text = tWorkspace.PathName ;
                    this.m_OutWorkspace = tWorkspace;
                }
            }
        } 

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (CheckValid() == false) return;

            //获取转换类型
            GJ2DFCoordTransformation tTransformation=this.combTransType.SelectedItem as GJ2DFCoordTransformation;

            //实例化进度条对话框
            ITrackProgress tProgressDialog = new TrackProgressDialog();
            tProgressDialog.DisplayTotal = true;
            tProgressDialog.TotalMax = this.listSrcFiles.Items.Count;
            (tProgressDialog as Form).TopMost = true;
            tProgressDialog.Show();

            try
            {
                for (int i = 0; i < this.listSrcFiles.Items.Count; i++)
                {
                    ListViewItem tListItem = this.listSrcFiles.Items[i];
                    IFeatureClass tFromFeatureClass = tListItem.Tag as IFeatureClass;

                    tProgressDialog.TotalMessage = string.Format("正在处理[{0}]要素类……", tFromFeatureClass.AliasName);
                    tProgressDialog.TotalValue = i;

                    Dictionary<int, int> tFieldMatch = null;

                    if (this.chkOnSrcFile.Checked == false)
                    {
                        string tPathTarget = txtOutWorkspace.Text;
                        string tFileNameWithoutExt = (tFromFeatureClass as IDataset).Name;
                        //创建目标要素类
                        IFeatureClass tToFeatureClass = GeoDBHelper.CreateFeatureClass(tFromFeatureClass, this.m_OutWorkspace, tPathTarget, tFileNameWithoutExt, esriFeatureType.esriFTSimple, null, null, "", ref tFieldMatch);

                        //空间对象转换
                        Transform2D(true, tFromFeatureClass, tToFeatureClass, tTransformation, tProgressDialog, tFieldMatch);
                    }
                    else
                    {
                        //空间对象转换
                        Transform2D(false, tFromFeatureClass, null, tTransformation, tProgressDialog, null);
                    }
                }

                tProgressDialog.TotalValue = this.listSrcFiles.Items.Count;
                tProgressDialog.TotalMessage = "已成功完成转换处理……";
                tProgressDialog.SetFinish();
                MessageBox.Show("处理完成", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch(Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                (tProgressDialog as Form).Hide();
                tProgressDialog.SetFinish();
                this.Close();
                MessageBox.Show(ex.Message, "错误提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //finally
            //{
            //    //标识已完成
            //    tProgressDialog.SetFinish();
            //    this.Close();
            //}        
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 数据有效性检查
        /// </summary>
        /// <returns>如果全部符合要求则返回 true,否则返回 false</returns>
        private bool CheckValid()
        {
            if (this.listSrcFiles.Items.Count == 0)
            {
                MessageBox.Show("请选择需要转换的源数据文件!", "提示");
                return false;
            }

            if (this.chkOnSrcFile.Checked == false && this.m_OutWorkspace == null)
            {
                MessageBox.Show("请选择数据输出工作空间!", "提示");
                return false;
            }

            return true;
        }

         /// <summary>
        /// 空间二维对象转换
        /// </summary>
        /// <param name="IsInsert">是否插入,插入为true,否则为 false</param>
        /// <param name="pFromFeatClass">源要素类</param>
        /// <param name="pToFeatClass">目标要素类</param>
        /// <param name="pTransformation">转换定义</param>
        /// <param name="pTrackProgressDlg">进度对话框</param>
        private void Transform2D(bool IsInsert, IFeatureClass tFeatureClassSource, IFeatureClass tFeatureClassTarget, GJ2DFCoordTransformation pTransformation, ITrackProgress pTrackProgressDlg, Dictionary<int, int> tFieldMatch)
        {           
            if (IsInsert == true && tFeatureClassTarget == null)
            {
                throw (new Exception("当变量IsInsert为true的情况下,pToFeatClass不允许为空."));               
            }

            //设置子进度条显示范围的最大值
            pTrackProgressDlg.SubMax = tFeatureClassSource.FeatureCount(null);

            int curvalue = 0;

            if (IsInsert == true)
            {         
                #region 向目标要素类中添加记录
                //获取源要素所有要素的游标
                IFeatureCursor tFromFeatCursor = tFeatureClassSource.Search(null, false);

                //获取插入游标
                IFeatureCursor tFeatureCursorInsert = tFeatureClassTarget.Insert(true);
                //创建要素缓存
                IFeatureBuffer tFeatureBuffer = tFeatureClassTarget.CreateFeatureBuffer();

                for (IFeature tFeatureSource = tFromFeatCursor.NextFeature(); tFeatureSource != null; tFeatureSource = tFromFeatCursor.NextFeature())
                {
                    //判断运行状态
                    if (pTrackProgressDlg.IsContinue == false) return;

                    //得到克隆的空间对象
                    IGeometry tGeometry = tFeatureSource.ShapeCopy;
                    //空间对象转换
                    tFeatureBuffer.Shape = pTransformation.GeometryTransform(tGeometry); ;

                    //属性赋值
                    if (tFieldMatch == null)
                    {
                        for (int i = 0; i < tFeatureSource.Fields.FieldCount; i++)
                        {
                            IField pField = tFeatureSource.Fields.get_Field(i);
                            if (pField.Type != esriFieldType.esriFieldTypeOID && pField.Type != esriFieldType.esriFieldTypeGUID
                                && pField.Type != esriFieldType.esriFieldTypeGlobalID && pField.Type != esriFieldType.esriFieldTypeGeometry)
                            {
                                object objValue = tFeatureSource.get_Value(i);

                                tFeatureBuffer.set_Value(i, objValue);
                            }
                        }
                    }
                    else
                    {
                        IFields tFieldsTarget = tFeatureClassTarget.Fields;
                        //使用创建FeatureClass时的字段匹配，key为新字段，value为对应的旧字段
                        foreach (KeyValuePair<int, int> kvp in tFieldMatch)
                        {
                            IField tFieldTarget = tFieldsTarget.get_Field(kvp.Key);
                            //几何字段另外赋值，不可编辑字段不复制
                            if (tFieldTarget.Type != esriFieldType.esriFieldTypeGeometry && tFieldTarget.Editable == true)
                            {
                                if (tFieldTarget.Type == esriFieldType.esriFieldTypeString)
                                {
                                    try
                                    {
                                        tFeatureBuffer.set_Value(kvp.Key, Convert.ToString(tFeatureSource.get_Value(kvp.Value)));
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }
                                }
                                else if (tFieldTarget.Type == esriFieldType.esriFieldTypeDouble || tFieldTarget.Type == esriFieldType.esriFieldTypeInteger
                                     || tFieldTarget.Type == esriFieldType.esriFieldTypeSingle || tFieldTarget.Type == esriFieldType.esriFieldTypeSmallInteger)
                                {
                                    //shapefile的数字类型字段不能为空，原值为空要赋值0
                                    double value = 0;
                                    if (double.TryParse(Convert.ToString(tFeatureSource.get_Value(kvp.Value)), out value) == true)
                                    {
                                        try
                                        {
                                            tFeatureBuffer.set_Value(kvp.Key, value);
                                        }
                                        catch (Exception ex)
                                        {
                                            throw ex;
                                        }
                                    }
                                    else
                                    {
                                        tFeatureBuffer.set_Value(kvp.Key, 0);
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        tFeatureBuffer.set_Value(kvp.Key, tFeatureSource.get_Value(kvp.Value));
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }
                                }
                            }
                        }
                    }

                    //新增要素
                    tFeatureCursorInsert.InsertFeature(tFeatureBuffer);

                    curvalue++;
                    pTrackProgressDlg.SubValue = curvalue;
                    pTrackProgressDlg.SubMessage = "正在处理第" + curvalue.ToString() + "条记录";

                    Application.DoEvents();
                }

                //释放对象
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFeatureCursorInsert); 
                #endregion
            }
            else
            {
                #region 更新源要素类几何对象
                //获取源要素类所有要素的更新游标
                IFeatureCursor tFromUpdateCursor = tFeatureClassSource.Update(null, false);

                for (IFeature tFromFeat = tFromUpdateCursor.NextFeature(); tFromFeat != null; tFromFeat = tFromUpdateCursor.NextFeature())
                {
                    if (pTrackProgressDlg.IsContinue == false) return;

                    //得到当前要素几何克隆对象
                    IGeometry tGeometry = tFromFeat.Shape;
                    tFromFeat.Shape = pTransformation.GeometryTransform(tGeometry);                   

                    //更新当前要素
                    tFromUpdateCursor.UpdateFeature(tFromFeat);
         
                    curvalue++;
                    pTrackProgressDlg.SubValue = curvalue;
                    pTrackProgressDlg.SubMessage = "正在处理第" + curvalue.ToString() + "条记录";

                    Application.DoEvents();
                }

                tFromUpdateCursor.Flush();

                //释放对象
                System.Runtime.InteropServices.Marshal.ReleaseComObject(tFromUpdateCursor); 
                #endregion
            }
        }      
    }
}