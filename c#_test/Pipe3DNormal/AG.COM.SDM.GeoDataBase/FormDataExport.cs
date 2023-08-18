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
    /// ����������
    /// </summary>
    public partial class FormDataExport : Form
    {
        private IFeatureClass m_InputFeatureClass;
        private object m_OutputLocation;
        
        /// <summary>
        /// ʵ�������ݵ�����ʵ��
        /// </summary>
        public FormDataExport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ���ð󶨵ĵ�ͼ����
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
                    //���������
                    this.listFields.Items.Clear();

                    //�������ط���Ҫ����ֶ�
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
                                //����ListViewItem��
                                ListViewItem tListViewItem = ListViewItemAdpter(tField);
                                //�������
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
            //ʵ���������������
            AG.COM.SDM.Catalog.IDataBrowser tDataBrowser = new FormDataBrowser();  

            //��ӹ����ռ��������
            tDataBrowser.AddFilter(new WorkspaceFilter());            
            tDataBrowser.AddFilter(new FolderFilter());
            //������ݼ���������
            tDataBrowser.AddFilter(new FeatureDatasetFilter());

            //���ܶ�ѡ
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
                    //������Ϊѡ��״̬
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
                    //������Ϊѡ��״̬
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
                //�������
                this.listFields.Items.Add(tListViewItem);
                //������Ϊѡ��״̬
                tListViewItem.Selected = true;
            }
        }

        private void btnDelField_Click(object sender, EventArgs e)
        {
            if (this.listFields.SelectedItems.Count > 0)
            {
                //��ȡѡ����
                ListViewItem tListViewItem = this.listFields.SelectedItems[0];
                IField tField = tListViewItem.Tag as IField;
                if (tField.Type != esriFieldType.esriFieldTypeGeometry)
                {
                    //�Ƴ�ѡ����
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
            //����������������Ҫ���򷵻�
            if (CheckedValid() == false) return; 

            //��ȡҪ������ֶμ�
            IFields tFields = GetOutputFields();
            if (tFields == null) return;

            if (this.m_OutputLocation != null)
            {

                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    //����Ҫ����
                    IFeatureClass tOutPutFeatureClass = GeoDBHelper.CreateFeatureClass(m_OutputLocation, txtOutFileName.Text, esriFeatureType.esriFTSimple, tFields, null, null, "");
                    if (tOutPutFeatureClass == null) return;

                    //��ȡ�ֶ�ƥ����� 
                    Dictionary<int, int> tDictFieldsRule = GetOutPutFieldsRule(tOutPutFeatureClass.Fields);

                    IQueryFilter tQueryFilter = null;
                    if (this.txtQueryFilter.Text.Trim().Length > 0)
                    {
                        tQueryFilter = new QueryFilterClass();
                        tQueryFilter.WhereClause = this.txtQueryFilter.Text;
                    }
                    //�������ݵ�ָ����Ҫ����
                    GeoDBHelper.ImportDataToFeatureClass(m_InputFeatureClass, tOutPutFeatureClass, tDictFieldsRule, tQueryFilter);

                    this.Cursor = Cursors.Default;

                    MessageBox.Show("�����ѳɹ�����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
        /// ������Ч�Լ��
        /// </summary>
        /// <returns>������ݷ���Ҫ���򷵻� true,���򷵻� false</returns>
        private bool CheckedValid()
        {
            bool bIsValid = true;

            //��ȡѡ���FeatureClass
            this.m_InputFeatureClass = this.SelectorInputFeatClass.FeatureClass;

            if (this.m_InputFeatureClass == null)
            {
                bIsValid = false;
                MessageBox.Show("��ѡ����Ҫ������Դ����!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);                
            }

            if (this.m_OutputLocation == null)
            {
                bIsValid = false;
                MessageBox.Show("��ѡ�����������Ŀ¼��ַ!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (this.txtOutFileName.Text.Length == 0)
            {
                bIsValid = false;
                MessageBox.Show("����������ļ�����!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            #region ���Ҫ�����Ƿ����
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
                    MessageBox.Show("�Ѵ�����ͬ���Ƶ�Ҫ����,�����������µ�����!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtOutFileName.SelectedText = this.txtOutFileName.Text;
                    this.txtOutFileName.Focus();
                }
            }
            #endregion

            return bIsValid;
        }

        /// <summary>
        /// ��ȡҪ�������ֶμ���
        /// </summary>
        /// <returns>�����ֶμ���</returns>
        private IFields GetOutputFields()
        {
            if (this.listFields.Items.Count == 0) return null;

            //ʵ�����ֶμ��϶���
            IFieldsEdit tFieldsEdit = new FieldsClass();

            //���Object�ֶ�
            IFieldEdit tFieldEdit = new FieldClass();
            tFieldEdit.AliasName_2 = "OBJECT";
            tFieldEdit.Name_2 = "OBJECT";
            tFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;

            //��ѯ���ýӿ�
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

            //��ѯ�ӿ�����
            IFields tFields = tFieldsEdit as IFields; 
            return tFields;
        }  

        /// <summary>
        /// ListViewItem������
        /// </summary>
        /// <param name="pField">IField�ֶζ���</param>
        /// <returns>����ListViewItem</returns>
        private ListViewItem ListViewItemAdpter(IField pField)
        {
            if (pField != null)
            {
                ListViewItem tListViewItem = new ListViewItem();
                //��ӱ���
                tListViewItem.Text = pField.AliasName;
                //����ֶ�����
                tListViewItem.SubItems.Add(pField.Name);
                //�ֶ����ͽ��к�������
                EnumFieldItems tEnumFieldItem = (EnumFieldItems)((int)pField.Type);
                //��Ӻ������ֶ�����˵��
                tListViewItem.SubItems.Add(tEnumFieldItem.ToString());

                tListViewItem.Tag = pField;

                return tListViewItem;
            }

            return null;
        }

        /// <summary>
        /// ��ȡ��������ֶεĶ�Ӧ����
        /// </summary>
        /// <param name="pOutPutFields">Ҫ����������ֶ�</param>
        /// <returns>�����ֶ�ƥ�����
        /// <key>��ʾĿ���ֶ�����</key>
        /// <value>��ʾԴ�ֶ�����</value>
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
            //�ж��Ƿ񵼳�ΪShape�ļ�
            bool IsShapeFolder = tWorkspace.IsDirectory();

            //ʵ����ƥ�����(keyΪĿ��Ҫ���ֶζ�������ֵ, valueΪԴҪ���ֶ�����ֵ)
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
                        //����ֶ�ƥ�����
                        tDictOutputRule.Add(toIndex, i);
                    }
                }
            }
            return tDictOutputRule;
        }

    
    }
}