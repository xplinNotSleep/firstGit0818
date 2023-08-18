using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// 属性域对话框
    /// </summary>
    public partial class DomainPropertyDialog : Form
    {
        #region 私有变量
        //属性域
        private IDomain m_Domain;
        //属性域对话框状态
        private EnumDomainDlgState m_DomainDlgState=EnumDomainDlgState.Normal;
        //字段类型     
        private Dictionary<string, esriFieldType> m_FieldsType = new Dictionary<string, esriFieldType>();
        //合并策略
        private Dictionary<string, esriMergePolicyType> m_MergePolicyType = new Dictionary<string, esriMergePolicyType>();
        //分割策略
        private Dictionary<string, esriSplitPolicyType> m_SplitPolicyType = new Dictionary<string, esriSplitPolicyType>();
        //IDomain的Owner属性，用于编辑IDomain时赋值
        private string m_DomainOwner = "";
        #endregion

        /// <summary>
        /// 获取或设置属性域
        /// </summary>
        public IDomain Domain
        {
            get
            {
                return this.m_Domain;
            }
            set
            {
                this.m_Domain = value;
            }
        }

        /// <summary>
        /// 设置属性域对话框状态
        /// </summary>
        public EnumDomainDlgState DomainDlgState
        {
            set
            {
                this.m_DomainDlgState = value;
            }
        }

        /// <summary>
        /// 实例化属性域对话框
        /// </summary>
        public DomainPropertyDialog()
        {
            //初始化界面组件
            InitializeComponent();

            //设置字段类型数据字典
            this.m_FieldsType.Add("短整型", esriFieldType.esriFieldTypeSmallInteger);
            this.m_FieldsType.Add("长整型", esriFieldType.esriFieldTypeInteger);
            this.m_FieldsType.Add("浮点型", esriFieldType.esriFieldTypeSingle);
            this.m_FieldsType.Add("双精度型", esriFieldType.esriFieldTypeDouble);
            this.m_FieldsType.Add("字符型", esriFieldType.esriFieldTypeString);
            this.m_FieldsType.Add("日期型", esriFieldType.esriFieldTypeDate);  
          
            //设置合并策略数据字典
            this.m_MergePolicyType.Add("默认值", esriMergePolicyType.esriMPTDefaultValue);
            this.m_MergePolicyType.Add("几何权重", esriMergePolicyType.esriMPTAreaWeighted);
            this.m_MergePolicyType.Add("和值", esriMergePolicyType.esriMPTSumValues);

            //设置分割策略数据字典
            this.m_SplitPolicyType.Add("默认值", esriSplitPolicyType.esriSPTDefaultValue);
            this.m_SplitPolicyType.Add("复制", esriSplitPolicyType.esriSPTDuplicate);
            this.m_SplitPolicyType.Add("几何比例", esriSplitPolicyType.esriSPTGeometryRatio);
        }   

        private void DomainPropertyDialog_Load(object sender, EventArgs e)
        {
            this.txtName.Text = this.m_Domain.Name;
            this.txtDescription.Text = this.m_Domain.Description;

            m_DomainOwner = this.m_Domain.Owner;

            //字段类型中文值数组
            string[] strFieldType =new string[this.m_FieldsType.Count];
            this.m_FieldsType.Keys.CopyTo(strFieldType, 0);
            //设置字段类型数据源
            this.combFieldType.DataSource = strFieldType; 
 
            //设置域类型数据源
            this.combDomainType.DataSource = new string[] { "范围域", "代码值域" };

            //获取字段类型关键字
            string strFieldKey=  GetFieldDictionaryKey(this.m_Domain.FieldType);
            this.combFieldType.SelectedItem =(strFieldKey == "")?null:strFieldKey;

            //获取合并类型关键字
            string strMergePolicy = GetMergePolicyDictKey(this.m_Domain.MergePolicy);
            this.combMergePolicy.SelectedItem = (strMergePolicy == "") ? null : strMergePolicy;

            //获取分割类型关键字
            string strSplictPolicy=   GetSplitPolicyDictKey(this.m_Domain.SplitPolicy);
            this.combSplitPolicy.SelectedItem = (strSplictPolicy == "") ? null : strSplictPolicy;
          
            //查询引用接口            
            if (this.m_Domain.Type==esriDomainType.esriDTRange)
            {
                //设置域类型的选择项
                this.combDomainType.SelectedItem = "范围域";
                //查询引用接口
                IRangeDomain tRangeDomain = this.m_Domain as IRangeDomain;
                this.txtMaxValue.Text = (tRangeDomain.MaxValue == null) ? "0" : Convert.ToString(tRangeDomain.MaxValue);
                this.txtMinValue.Text = (tRangeDomain.MinValue == null) ? "0" : Convert.ToString(tRangeDomain.MinValue);         
            }
            else
            {
                //设置域类型的选择项
                this.combDomainType.SelectedItem = "代码值域";
                //查询引用接口
                ICodedValueDomain tCodeValueDomain = this.m_Domain as ICodedValueDomain;
                for (int i = 0; i < tCodeValueDomain.CodeCount; i++)
                {
                    this.codeDataGrid.Rows.Add(tCodeValueDomain.get_Value(i), tCodeValueDomain.get_Name(i));
                }
            }

            //界面组件设置
            SettingComponent();
        }

        private void combDomainType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combDomainType.SelectedItem.ToString() == "范围域")
            {
                //范围域时                  
                if (this.tabControl1.TabPages.Contains(this.tabPage2) == true)
                {
                    this.tabControl1.TabPages.Remove(this.tabPage2);    //移除代码值域面板
                }
                if (this.tabControl1.TabPages.Contains(this.tabPage3) == false)
                {
                    this.tabControl1.TabPages.Add(this.tabPage3);       //添加范围域面析
                } 
            }
            else
            {                
                //代码值域时
                if (this.tabControl1.TabPages.Contains(this.tabPage2) == false)
                {
                    //添加代码值域面板
                    this.tabControl1.TabPages.Add(this.tabPage2);
                }
                if (this.tabControl1.TabPages.Contains(this.tabPage3) == true)
                {   
                    //移除范围域面板
                    this.tabControl1.TabPages.Remove(this.tabPage3);   
                }
            }
        }

        private void combFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {            
            string[] strNumFieldType = new string[] { "短整型", "长整型", "浮点型", "双精度型" };
            IList<string> tStrList = strNumFieldType as IList<string>;
           
            string strFieldType = this.combFieldType.SelectedItem.ToString();
            if (tStrList.Contains(strFieldType))
            {
                //设置合并策略项的数据源
                this.combMergePolicy.DataSource = new string[] { "默认值", "和值", "几何权重" };
                //设置分割策略项的数据源
                this.combSplitPolicy.DataSource = new string[] { "默认值", "复制", "几何比例" };
            }
            else
            {
                //设置合并策略项的数据源
                this.combMergePolicy.DataSource = new string[] { "默认值" };
                //设置分割策略项的数据源
                this.combSplitPolicy.DataSource = new string[] { "默认值", "复制"}; 
            }

            if (string.Compare(strFieldType, "字符型", true) == 0)
            {  
                //设置域类型数据源
                this.combDomainType.DataSource = new string[] { "代码值域" };
            }
            else
            {  
                //设置域类型数据源
                this.combDomainType.DataSource = new string[] {"范围域","代码值域" };              
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                IDomain tempDomain = null;

                if (combDomainType.SelectedItem.ToString() == "范围域")
                {
                    //实例化范围域对象
                    tempDomain = new RangeDomainClass();

                    //查询接口
                    IRangeDomain tRangDomain = tempDomain as IRangeDomain;
                    tRangDomain.MinValue = this.txtMinValue.Text;
                    tRangDomain.MaxValue = this.txtMaxValue.Text;
                }
                else
                {
                    //实例化值域对象
                    tempDomain = new CodedValueDomainClass();

                    //查询接口
                    ICodedValueDomain tCodedValueDomain = tempDomain as ICodedValueDomain;
                    for (int i = 0; i < this.codeDataGrid.Rows.Count-1; i++)
                    {
                        DataGridViewRow tDataRow = this.codeDataGrid.Rows[i];
                        //根据指定的字段类型得到代码值
                        //object objValue = GetCodedValue(this.combFieldType.Text, tDataRow.Cells[0].Value);
                        object objValue = DbInfoReadWrite.GetCodedValue(this.m_FieldsType[this.combFieldType.Text], tDataRow.Cells[0].Value);
                        if (Convert.ToString(tDataRow.Cells[1].Value) == "") throw (new Exception(string.Format("{0} 代码描述不能空",objValue)));
                        tCodedValueDomain.AddCode(objValue, Convert.ToString(tDataRow.Cells[1].Value));
                    }
                }

                tempDomain.Name = txtName.Text;
                tempDomain.Description = txtDescription.Text;
                tempDomain.FieldType = this.m_FieldsType[this.combFieldType.Text];
                tempDomain.MergePolicy = this.m_MergePolicyType[this.combMergePolicy.Text];
                tempDomain.SplitPolicy = this.m_SplitPolicyType[this.combSplitPolicy.Text];

                tempDomain.Owner = this.m_DomainOwner;

                this.m_Domain = tempDomain;

                this.DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }  

        private void codeDataGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        { 
            if (this.codeDataGrid.Rows[e.RowIndex].IsNewRow) return;
            if (e.ColumnIndex == 0)
            {
                string strValue = e.FormattedValue.ToString();

                try
                {
                    esriFieldType tFieldType = this.m_FieldsType[this.combFieldType.Text];
                    switch (tFieldType)
                    {
                        case esriFieldType.esriFieldTypeSmallInteger:
                            Convert.ToInt16(strValue);
                            break;
                        case esriFieldType.esriFieldTypeInteger:
                            Convert.ToInt32(strValue);
                            break;
                        case esriFieldType.esriFieldTypeSingle:
                            Convert.ToSingle(strValue);
                            break;
                        case esriFieldType.esriFieldTypeDouble:
                            Convert.ToDouble(strValue);
                            break;
                        case esriFieldType.esriFieldTypeDate:
                            Convert.ToDateTime(strValue);
                            break;
                        default:
                            break;
                    }

                    this.codeDataGrid.Rows[e.RowIndex].ErrorText = string.Empty;
                }
                catch
                {
                    this.codeDataGrid.Rows[e.RowIndex].ErrorText = string.Format("请输入正确的 '{0}' 数据", this.combFieldType.Text);
                    e.Cancel = true;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelBottom_VisibleChanged(object sender, EventArgs e)
        {            
            if (panelBottom.Visible == true)
            {
                this.Height += this.panelBottom.Height-20;
            }
            else
            {
                this.Height = this.Height - 10;
            }
        }

        #region 私有方法

        /// <summary>
        /// 根据指定的字段类型,得到代码值
        /// </summary>
        /// <param name="pFieldType">字段类型中文名</param>
        /// <param name="pCellValue">代码值对象</param>
        /// <returns>返回正确的代码值</returns>
        private object GetCodedValue(string pFieldType,object pCellValue)
        {
            try
            {
                esriFieldType tempFieldType = this.m_FieldsType[pFieldType];
                switch (tempFieldType)
                {
                    case esriFieldType.esriFieldTypeDate:
                        return Convert.ToDateTime(pCellValue);
                    case esriFieldType.esriFieldTypeDouble:
                        return Convert.ToDouble(pCellValue);
                    case esriFieldType.esriFieldTypeSingle:
                        return Convert.ToSingle(pCellValue);
                    case esriFieldType.esriFieldTypeSmallInteger:
                        return Convert.ToByte(pCellValue);
                    case esriFieldType.esriFieldTypeInteger:
                        return Convert.ToInt32(pCellValue);
                    default:
                        return Convert.ToString(pCellValue);
                }
            }
            catch
            {
                throw(new Exception(string.Format("{0} 值与字段类型不符,请重新输入！",pCellValue)));
            }
        }  

        /// <summary>
        /// 通过指定字段类型查找关键字
        /// </summary>
        /// <param name="pMergePolicy">字段类型</param>
        /// <returns>返回关键字</returns>
        private string GetFieldDictionaryKey(esriFieldType pFieldValue)
        {
            IDictionaryEnumerator tDictEnumerator = this.m_FieldsType.GetEnumerator();
            return GetDictKey(tDictEnumerator, pFieldValue);          
        }

        /// <summary>
        /// 通过指定合并类型查找关键字
        /// </summary>
        /// <param name="pMergePolicy">合并类型</param>
        /// <returns>返回关键字</returns>
        private string GetMergePolicyDictKey(esriMergePolicyType pMergePolicy)
        {
            IDictionaryEnumerator tDictEnumerator = this.m_MergePolicyType.GetEnumerator();
            return GetDictKey(tDictEnumerator, pMergePolicy);
        }

        /// <summary>
        /// 通过指定分割类型查找关键字
        /// </summary>
        /// <param name="pMergePolicy">分割类型</param>
        /// <returns>返回关键字</returns>
        private string GetSplitPolicyDictKey(esriSplitPolicyType pSplitPolicy)
        {
            IDictionaryEnumerator tDictEnumerator = this.m_SplitPolicyType.GetEnumerator();
            return GetDictKey(tDictEnumerator, pSplitPolicy);
        }

        /// <summary>
        /// 在枚举类型中查询指定值的关键词
        /// </summary>
        /// <param name="pDictEnumerator">Dictionary关键词</param>
        /// <returns>值对象</returns>
        private string GetDictKey(IDictionaryEnumerator pDictEnumerator,object pValue)
        { 
            pDictEnumerator.Reset();
            while(pDictEnumerator.MoveNext() == true)
            {
                if ((int)pValue == (int)pDictEnumerator.Value)
                    return pDictEnumerator.Key.ToString();                
            }

            return "";
        }   

        /// <summary>
        /// 根据字段类型返回.NET平台下的数据类型
        /// </summary>
        /// <param name="pFieldType">字段类型</param>
        /// <returns>.NET数据类型</returns>
        private Type GetDataType(string pFieldType)
        {
            esriFieldType tFieldType = this.m_FieldsType[pFieldType];
            switch (tFieldType)
            {
                case esriFieldType.esriFieldTypeDate:
                    return typeof(DateTime);
                case esriFieldType.esriFieldTypeSmallInteger:
                    return typeof(Int16);
                case esriFieldType.esriFieldTypeInteger:
                    return typeof(Int32);
                case esriFieldType.esriFieldTypeSingle:
                    return typeof(Single);
                case esriFieldType.esriFieldTypeDouble:
                    return typeof(Double);
                default:
                    return typeof(String);
            }
        }

        /// <summary>
        /// 根据指定的域值和字段类型设置列数据类型
        /// </summary>
        /// <param name="pDomainType">域值类型</param>
        /// <param name="pFieldType">字段类型</param>
        private void SetColumnDataType(string pDomainType,string pFieldType)
        {
            if (pDomainType == "代码值域")
            {
                this.codeDataGrid.Columns[0].ValueType = GetDataType(pFieldType);
            }
        }

        /// <summary>
        /// 界面组件设置
        /// </summary>
        private void SettingComponent()
        {
            if (this.m_DomainDlgState == EnumDomainDlgState.Normal)
            {
                this.txtName.Enabled = false;                 
                this.txtDescription.Enabled = false;       
            }
            else if (this.m_DomainDlgState == EnumDomainDlgState.Editor)
            {
                //设置其状态为可用
                //因为改变名称后不能正常保存，因此锁定名称，不让修改
                this.txtName.Enabled = false;
                this.txtDescription.Enabled = true;

                this.txtName.ReadOnly = true;
                this.txtDescription.ReadOnly = false;

                //编辑时域类型不能修改
                this.combDomainType.Enabled = false;
                //编辑时域字段类型不能修改
                this.combFieldType.Enabled = false;

                //设置背景颜色
                this.txtName.BackColor = System.Drawing.SystemColors.Window;
                this.txtDescription.BackColor = System.Drawing.SystemColors.Window;
                
                //设置输入焦点
                this.txtName.Focus();

                if (this.panelBottom.Visible == false)
                {
                    this.panelBottom.Visible = true;
                }
            }
            else if (this.m_DomainDlgState == EnumDomainDlgState.New)
            {
                //设置其状态为可用                
                this.txtName.Enabled = true;
                this.txtDescription.Enabled = true;

                this.txtName.ReadOnly = false;
                this.txtDescription.ReadOnly = false;

                //设置背景颜色
                this.txtName.BackColor = System.Drawing.SystemColors.Window;
                this.txtDescription.BackColor = System.Drawing.SystemColors.Window;

                //设置输入焦点
                this.txtName.Focus();

                if (this.panelBottom.Visible == false)
                {
                    this.panelBottom.Visible = true;
                }
            }
            else
            {
                if (this.panelBottom.Visible == true)
                {
                    this.panelBottom.Visible = false;
                }
            }
        }
        #endregion        
    }
}