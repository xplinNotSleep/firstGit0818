using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AG.COM.SDM.Catalog;
using AG.COM.SDM.Catalog.DataItems;
using AG.COM.SDM.Catalog.Filters;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 导出数据类
    /// </summary>
    public partial class FormDataExport : Form
    {
        private IFeatureClass m_InputFeatureClass;
        private object m_OutputLocation;
        
        /// <summary>
        /// 实例化数据导出新实例
        /// </summary>
        public FormDataExport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置绑定的地图对象
        /// </summary>
        public IMap Map
        {
            set
            {
                this.SelectorInputFeatClass.Map = value;
            }
        }

        private void SelectorInputFeatClass_LayerChanged()
        {
            IFeatureClass tInputFeatClass = this.SelectorInputFeatClass.FeatureClass;

            if (tInputFeatClass != null)
            {
                this.btnQueryFilter.Enabled = true;

                this.m_InputFeatureClass = tInputFeatClass as IFeatureClass;

                if (this.m_InputFeatureClass != null)
                {
                    //清空所有项
                    this.listFields.Items.Clear();

                    //遍历加载符合要求的字段
                    for (int i = 0; i < this.m_InputFeatureClass.Fields.FieldCount; i++)
                    {
                        IField tField = this.m_InputFeatureClass.Fields.get_Field(i);
                        if (tField.Type != esriFieldType.esriFieldTypeOID &&
                            tField.Type != esriFieldType.esriFieldTypeGlobalID &&
                            tField.Type != esriFieldType.esriFieldTypeGUID &&
                            tField.Type != esriFieldType.esriFieldTypeRaster)
                        {
                            if (string.Compare(tField.Name, "Shape.Len", true) == 0 ||
                                string.Compare(tField.Name, "Shape.Area", true) == 0 ||
                                tField.Name.IndexOf('.') > 0)
                            {
                                continue;
                            }
                            else
                            {
                                //生成ListViewItem项
                                ListViewItem tListViewItem = ListViewItemAdpter(tField);
                                //添加子项
                                this.listFields.Items.Add(tListViewItem);
                            }
                        }
                    }
                }
            }
            else
            {
                this.btnQueryFilter.Enabled = false;
            }
        }

        private void btnOutFolder_Click(object sender, EventArgs e)
        {     
            //实例化数据浏览窗体
            AG.COM.SDM.Catalog.IDataBrowser tDataBrowser = new FormDataBrowser();  

            //添加工作空间过滤条件
            tDataBrowser.AddFilter(new WorkspaceFilter());            
            tDataBrowser.AddFilter(new FolderFilter());
            //添加数据集过滤条件
            tDataBrowser.AddFilter(new FeatureDatasetFilter());

            //不能多选
            tDataBrowser.MultiSelect = false;         

            if (tDataBrowser.ShowDialog() == DialogResult.OK)
            {
                IList<DataItem> items = tDataBrowser.SelectedItems;
                if (items.Count > 0)
                {                    
                    this.m_OutputLocation = items[0].GetGeoObject();
                     
                    if (this.m_OutputLocation is IWorkspace)
                    {
                        IWorkspace tWorkspace=this.m_OutputLocation as IWorkspace;
                        this.txtOutFolder.Text = tWorkspace.PathName;                        
                    }
                    else if (this.m_OutputLocation is IFeatureDataset)
                    {
                        IFeatureDataset tFeatureDataset = this.m_OutputLocation as IFeatureDataset;
                        this.txtOutFolder.Text = tFeatureDataset.BrowseName;
                    }
                }
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (this.listFields.SelectedItems.Count == 0) return;
            ListViewItem tSelListViewItem = this.listFields.SelectedItems[0];
            if (tSelListViewItem != null)
            {              
                int index = this.listFields.Items.IndexOf(tSelListViewItem);
                if (index > 0)
                {
                    this.listFields.SuspendLayout();                
                    this.listFields.Items.Remove(tSelListViewItem);
                    this.listFields.Items.Insert(index - 1, tSelListViewItem);
                    //设置其为选择状态
                    tSelListViewItem.Selected = true;
                    tSelListViewItem.Focused = true;
                    this.listFields.ResumeLayout();             
                }
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (this.listFields.SelectedItems.Count == 0) return;
            ListViewItem tSelListViewItem = this.listFields.SelectedItems[0];
            if (tSelListViewItem != null)
            {
                int index = this.listFields.Items.IndexOf(tSelListViewItem);
                if (index < this.listFields.Items.Count-1)
                {
                    this.listFields.SuspendLayout();
                    this.listFields.Items.Remove(tSelListViewItem);
                    this.listFields.Items.Insert(index+1, tSelListViewItem);
                    //设置其为选择状态
                    tSelListViewItem.Selected = true;
                    this.listFields.ResumeLayout(true);
                }
            }
        }

        private void btnAddField_Click(object sender, EventArgs e)
        {
            FormAddField frmAddField = new FormAddField();
            if (frmAddField.ShowDialog() == DialogResult.OK)
            {
                IField tField = frmAddField.Field;

                ListViewItem tListViewItem = ListViewItemAdpter(tField);
                //添加子项
                this.listFields.Items.Add(tListViewItem);
                //设置其为选择状态
                tListViewItem.Selected = true;
            }
        }

        private void btnDelField_Click(object sender, EventArgs e)
        {
            if (this.listFields.SelectedItems.Count > 0)
            {
                //获取选择项
                ListViewItem tListViewItem = this.listFields.SelectedItems[0];
                IField tField = tListViewItem.Tag as IField;
                if (tField.Type != esriFieldType.esriFieldTypeGeometry)
                {
                    //移除选择项
                    this.listFields.Items.Remove(tListViewItem);
                }
            }
        } 

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQueryFilter_Click(object sender, EventArgs e)
        {
            FormQueryBuilder tfrmQueryBuilder = new FormQueryBuilder(this.m_InputFeatureClass);
            if (tfrmQueryBuilder.ShowDialog() == DialogResult.OK)
            {
                this.txtQueryFilter.Text = tfrmQueryBuilder.QueryFilter;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //如果输入参数不符合要求则返回
            if (CheckedValid() == false) return; 

            //获取要输出的字段集
            IFields tFields = GetOutputFields();
            if (tFields == null) return;

            if (this.m_OutputLocation != null)
            {

                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    //创建要素类
                    IFeatureClass tOutPutFeatureClass = GeoDBHelper.CreateFeatureClass(m_OutputLocation, txtOutFileName.Text, esriFeatureType.esriFTSimple, tFields, null, null, "");
                    if (tOutPutFeatureClass == null) return;

                    //获取字段匹配规则 
                    Dictionary<int, int> tDictFieldsRule = GetOutPutFieldsRule(tOutPutFeatureClass.Fields);

                    IQueryFilter tQueryFilter = null;
                    if (this.txtQueryFilter.Text.Trim().Length > 0)
                    {
                        tQueryFilter = new QueryFilterClass();
                        tQueryFilter.WhereClause = this.txtQueryFilter.Text;
                    }
                    //导入数据到指定的要素类
                    GeoDBHelper.ImportDataToFeatureClass(m_InputFeatureClass, tOutPutFeatureClass, tDictFieldsRule, tQueryFilter);

                    this.Cursor = Cursors.Default;

                    MessageBox.Show("数据已成功导出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    this.Cursor = Cursors.Default;
                }
            }    
        }         

        private void txtOutFolder_TextChanged(object sender, EventArgs e)
        {
            if (this.txtOutFolder.Text.Length == 0)
            {
                this.m_OutputLocation = null;
            }
        }

        /// <summary>
        /// 数据有效性检查
        /// </summary>
        /// <returns>如果数据符合要求则返回 true,否则返回 false</returns>
        private bool CheckedValid()
        {
            bool bIsValid = true;

            //获取选择的FeatureClass
            this.m_InputFeatureClass = this.SelectorInputFeatClass.FeatureClass;

            if (this.m_InputFeatureClass == null)
            {
                bIsValid = false;
                MessageBox.Show("请选择需要导出的源数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);                
            }

            if (this.m_OutputLocation == null)
            {
                bIsValid = false;
                MessageBox.Show("请选择数据输出的目录地址!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (this.txtOutFileName.Text.Length == 0)
            {
                bIsValid = false;
                MessageBox.Show("请输入输出文件名称!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            #region 检查要素类是否存在
            IWorkspace2 tWorkspace2=null;

            if (this.m_OutputLocation is IWorkspace2)
            {
                tWorkspace2 = this.m_OutputLocation as IWorkspace2;
            }
            else if (this.m_OutputLocation is IFeatureDataset)
            {
                IFeatureDataset tFeatureDataset = this.m_OutputLocation as IFeatureDataset;
                tWorkspace2 = tFeatureDataset.Workspace as IWorkspace2;
            }

            if (tWorkspace2 != null)
            {
                bool bIsExist = tWorkspace2.get_NameExists(esriDatasetType.esriDTFeatureClass, txtOutFileName.Text);
                if (bIsExist == true)
                {
                    bIsValid = false;
                    MessageBox.Show("已存在相同名称的要素类,请重新输入新的名称!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtOutFileName.SelectedText = this.txtOutFileName.Text;
                    this.txtOutFileName.Focus();
                }
            }
            #endregion

            return bIsValid;
        }

        /// <summary>
        /// 获取要导出的字段集合
        /// </summary>
        /// <returns>返回字段集合</returns>
        private IFields GetOutputFields()
        {
            if (this.listFields.Items.Count == 0) return null;

            //实例化字段集合对象
            IFieldsEdit tFieldsEdit = new FieldsClass();

            //添加Object字段
            IFieldEdit tFieldEdit = new FieldClass();
            tFieldEdit.AliasName_2 = "OBJECT";
            tFieldEdit.Name_2 = "OBJECT";
            tFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;

            //查询引用接口
            IField tField = tFieldEdit as IField;
            tFieldsEdit.AddField(tField);

            for (int i = 0; i < this.listFields.Items.Count; i++)
            {
                tField = this.listFields.Items[i].Tag as IField;
                if (tFieldsEdit != null)
                {
                    tFieldsEdit.AddField(tField);
                }
            }

            //查询接口引用
            IFields tFields = tFieldsEdit as IFields; 
            return tFields;
        }  

        /// <summary>
        /// ListViewItem适配器
        /// </summary>
        /// <param name="pField">IField字段对象</param>
        /// <returns>返回ListViewItem</returns>
        private ListViewItem ListViewItemAdpter(IField pField)
        {
            if (pField != null)
            {
                ListViewItem tListViewItem = new ListViewItem();
                //添加别名
                tListViewItem.Text = pField.AliasName;
                //添加字段名称
                tListViewItem.SubItems.Add(pField.Name);
                //字段类型进行汉化处理
                EnumFieldItems tEnumFieldItem = (EnumFieldItems)((int)pField.Type);
                //添加汉化的字段类型说明
                tListViewItem.SubItems.Add(tEnumFieldItem.ToString());

                tListViewItem.Tag = pField;

                return tListViewItem;
            }

            return null;
        }

        /// <summary>
        /// 获取输出数据字段的对应规则
        /// </summary>
        /// <param name="pOutPutFields">要输出的数据字段</param>
        /// <returns>返回字段匹配规则
        /// <key>表示目标字段索引</key>
        /// <value>表示源字段索引</value>
        /// </returns>
        private Dictionary<int, int> GetOutPutFieldsRule(IFields pOutPutFields)
        {
            if (pOutPutFields == null) return null;
            
            IWorkspace tWorkspace = this.m_OutputLocation as IWorkspace;

            if (tWorkspace == null)
            {
                IDataset tDataset = m_OutputLocation as IDataset;
                tWorkspace = tDataset.Workspace;
            }
            if (tWorkspace == null) return null;
            //判断是否导出为Shape文件
            bool IsShapeFolder = tWorkspace.IsDirectory();

            //实例化匹配规则(key为目标要素字段对象索引值, value为源要素字段索引值)
            Dictionary<int, int> tDictOutputRule = new Dictionary<int, int>();

            IFields tSrcFields = this.m_InputFeatureClass.Fields; 
            for (int i = 0; i < tSrcFields.FieldCount; i++)
            {
                IField tField = tSrcFields.get_Field(i);

                if (tField.Type == esriFieldType.esriFieldTypeOID ||
                    tField.Type == esriFieldType.esriFieldTypeRaster ||
                    tField.Type == esriFieldType.esriFieldTypeGUID ||
                    tField.Type == esriFieldType.esriFieldTypeGlobalID ||
                    tField.Type == esriFieldType.esriFieldTypeGeometry )
                {
                    continue;
                }
                else
                {
                    int toIndex = -1;
                    if (IsShapeFolder == true && tField.Name.Length > 10)                   
                        toIndex=pOutPutFields.FindField(tField.Name.Substring(0,10));                   
                    else 
                        toIndex=pOutPutFields.FindField(tField.Name);

                    if (toIndex > -1)
                    {
                        //添加字段匹配规则
                        tDictOutputRule.Add(toIndex, i);
                    }
                }
            }
            return tDictOutputRule;
        }

    
    }
}