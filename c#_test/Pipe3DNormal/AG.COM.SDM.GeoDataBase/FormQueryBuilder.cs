using System;
using System.Windows.Forms;
using AG.COM.SDM.Utility;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// ��ѯ���˱��ʽ ��
    /// </summary>
    public partial class FormQueryBuilder : Form
    {
        private IFeatureClass m_FeatureClass; 

        /// <summary>
        /// ��ȡ��ѯ���˱��ʽ
        /// </summary>
        public string QueryFilter
        {
            get
            {
                return this.txtExpress.Text;
            }
            set
            {
                this.txtExpress.Text = value;
            }
        }
        
        /// <summary>
        /// ʵ������ѯ���˱��ʽʵ��
        /// </summary>
        /// <param name="pFeatureClass">Ҫ���˵�Ҫ����</param>
        public FormQueryBuilder(IFeatureClass pFeatureClass)
        {
            //��ʼ����������
            InitializeComponent();

            this.m_FeatureClass = pFeatureClass;
            IDataset tDataset = this.m_FeatureClass as IDataset;
            if (tDataset.Workspace.IsDirectory() == false) this.btnPercentage.Text = "*";
        }

        private void FormQueryBuilder_Load(object sender, EventArgs e)
        {
            if (this.m_FeatureClass != null)
            {
                IFields tFields = this.m_FeatureClass.Fields;
                int indexNo = 1;
                //ѭ������ֶ�
                for (int i = 0; i < tFields.FieldCount; i++)
                {                   
                    IField tField = tFields.get_Field(i);
                    if (tField.Type != esriFieldType.esriFieldTypeBlob &&
                        tField.Type != esriFieldType.esriFieldTypeGeometry &&
                        tField.Type != esriFieldType.esriFieldTypeGlobalID &&
                        tField.Type != esriFieldType.esriFieldTypeGUID &&
                        tField.Type != esriFieldType.esriFieldTypeOID &&
                        tField.Type != esriFieldType.esriFieldTypeRaster)
                    {
                        //ʵ����ListViewItem��
                        ListViewItem tListViewItem = new ListViewItem();
                        tListViewItem.Text = Convert.ToString(indexNo++);
                        tListViewItem.SubItems.Add(tField.AliasName);
                        tListViewItem.SubItems.Add(tField.Name);

                        //�������
                        this.listFields.Items.Add(tListViewItem);
                    }                         
                }
            }
        }       

        private void listFields_DoubleClick(object sender, EventArgs e)
        {
            if (this.listFields.FocusedItem != null)
            {
                string strFieldName = this.listFields.FocusedItem.SubItems[2].Text;
                //����Sql���ʽ
                AddToSqlExpress(strFieldName);
            }
        }

        private void listValues_EnabledChanged(object sender, EventArgs e)
        {
            if (listValues.Enabled == true)
            {
                listValues.BackColor = System.Drawing.SystemColors.Window;
            }
            else
            {
                listValues.BackColor = System.Drawing.SystemColors.Control;
            }
        }

        private void listValues_DoubleClick(object sender, EventArgs e)
        {
            if (this.listValues.SelectedItem != null)
            {
                string strFieldValue = this.listValues.SelectedItem as string;
                if (this.listFields.SelectedItems.Count != 0)
                {
                    string strFieldName = this.listFields.SelectedItems[0].SubItems[2].Text;
                    if (m_FeatureClass.Fields.get_Field(m_FeatureClass.Fields.FindField(strFieldName)).Type == esriFieldType.esriFieldTypeString)
                    {
                        strFieldValue = string.Format("'{0}'", strFieldValue);
                    }

                }
                //����Sql���ʽ
                AddToSqlExpress(strFieldValue);
            }
        }

        private void btnGetUnique_Click(object sender, EventArgs e)
        {
            ListViewItem tSelListItem = this.listFields.FocusedItem;
            if (tSelListItem != null)
            {
                //��ȡ�ֶ�����
                string strField = tSelListItem.SubItems[2].Text;
                //����ǰҪ�����ֶε�Ψһֵ��ΪlistValues����Դ
                this.listValues.DataSource = GeoTableHandler.GetUniqueValueByDataStat(this.m_FeatureClass, strField);                
                this.listValues.Enabled = true;
            }
        }

        private void btnClearSql_Click(object sender, EventArgs e)
        {
            this.txtExpress.Text = "";
        } 

        private void btnOperationSymbol_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            //����Sql���ʽ
            AddToSqlExpress(btn.Text);
        } 

        /// <summary>
        /// ����SQL���ʽ����
        /// </summary>
        /// <param name="sqlexpress">׷�ӵ�����</param>
        private void AddToSqlExpress(string sqlexpress)
        {
            this.txtExpress.Focus();

            int startIndex = this.txtExpress.SelectionStart;
            string strPrev = this.txtExpress.Text.Substring(0, startIndex);
            string strNext = this.txtExpress.Text.Substring(startIndex);

            this.txtExpress.Text = string.Format("{0} {1} {2}", strPrev, sqlexpress, strNext).Trim();

            if (string.Equals(sqlexpress, this.btnBrackets.Text))
                this.txtExpress.SelectionStart = string.Format("{0} {1}", strPrev, sqlexpress).Length - 1;
            else
                this.txtExpress.SelectionStart = string.Format("{0} {1}", strPrev, sqlexpress).Length;
        }  
    }
}