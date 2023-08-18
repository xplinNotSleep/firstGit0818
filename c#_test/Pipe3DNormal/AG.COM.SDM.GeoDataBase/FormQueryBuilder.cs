using System;
using System.Windows.Forms;
using AG.COM.SDM.Utility;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 查询过滤表达式 类
    /// </summary>
    public partial class FormQueryBuilder : Form
    {
        private IFeatureClass m_FeatureClass; 

        /// <summary>
        /// 获取查询过滤表达式
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
        /// 实例化查询过滤表达式实例
        /// </summary>
        /// <param name="pFeatureClass">要过滤的要素类</param>
        public FormQueryBuilder(IFeatureClass pFeatureClass)
        {
            //初始化界面配置
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
                //循环添加字段
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
                        //实例化ListViewItem项
                        ListViewItem tListViewItem = new ListViewItem();
                        tListViewItem.Text = Convert.ToString(indexNo++);
                        tListViewItem.SubItems.Add(tField.AliasName);
                        tListViewItem.SubItems.Add(tField.Name);

                        //添加子项
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
                //设置Sql表达式
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
                //设置Sql表达式
                AddToSqlExpress(strFieldValue);
            }
        }

        private void btnGetUnique_Click(object sender, EventArgs e)
        {
            ListViewItem tSelListItem = this.listFields.FocusedItem;
            if (tSelListItem != null)
            {
                //获取字段名称
                string strField = tSelListItem.SubItems[2].Text;
                //将当前要素类字段的唯一值作为listValues数据源
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
            //设置Sql表达式
            AddToSqlExpress(btn.Text);
        } 

        /// <summary>
        /// 设置SQL表达式内容
        /// </summary>
        /// <param name="sqlexpress">追加的内容</param>
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