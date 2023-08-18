using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// ������Ի���
    /// </summary>
    public partial class DomainPropertyDialog : Form
    {
        #region ˽�б���
        //������
        private IDomain m_Domain;
        //������Ի���״̬
        private EnumDomainDlgState m_DomainDlgState=EnumDomainDlgState.Normal;
        //�ֶ�����     
        private Dictionary<string, esriFieldType> m_FieldsType = new Dictionary<string, esriFieldType>();
        //�ϲ�����
        private Dictionary<string, esriMergePolicyType> m_MergePolicyType = new Dictionary<string, esriMergePolicyType>();
        //�ָ����
        private Dictionary<string, esriSplitPolicyType> m_SplitPolicyType = new Dictionary<string, esriSplitPolicyType>();
        //IDomain��Owner���ԣ����ڱ༭IDomainʱ��ֵ
        private string m_DomainOwner = "";
        #endregion

        /// <summary>
        /// ��ȡ������������
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
        /// ����������Ի���״̬
        /// </summary>
        public EnumDomainDlgState DomainDlgState
        {
            set
            {
                this.m_DomainDlgState = value;
            }
        }

        /// <summary>
        /// ʵ����������Ի���
        /// </summary>
        public DomainPropertyDialog()
        {
            //��ʼ���������
            InitializeComponent();

            //�����ֶ����������ֵ�
            this.m_FieldsType.Add("������", esriFieldType.esriFieldTypeSmallInteger);
            this.m_FieldsType.Add("������", esriFieldType.esriFieldTypeInteger);
            this.m_FieldsType.Add("������", esriFieldType.esriFieldTypeSingle);
            this.m_FieldsType.Add("˫������", esriFieldType.esriFieldTypeDouble);
            this.m_FieldsType.Add("�ַ���", esriFieldType.esriFieldTypeString);
            this.m_FieldsType.Add("������", esriFieldType.esriFieldTypeDate);  
          
            //���úϲ����������ֵ�
            this.m_MergePolicyType.Add("Ĭ��ֵ", esriMergePolicyType.esriMPTDefaultValue);
            this.m_MergePolicyType.Add("����Ȩ��", esriMergePolicyType.esriMPTAreaWeighted);
            this.m_MergePolicyType.Add("��ֵ", esriMergePolicyType.esriMPTSumValues);

            //���÷ָ���������ֵ�
            this.m_SplitPolicyType.Add("Ĭ��ֵ", esriSplitPolicyType.esriSPTDefaultValue);
            this.m_SplitPolicyType.Add("����", esriSplitPolicyType.esriSPTDuplicate);
            this.m_SplitPolicyType.Add("���α���", esriSplitPolicyType.esriSPTGeometryRatio);
        }   

        private void DomainPropertyDialog_Load(object sender, EventArgs e)
        {
            this.txtName.Text = this.m_Domain.Name;
            this.txtDescription.Text = this.m_Domain.Description;

            m_DomainOwner = this.m_Domain.Owner;

            //�ֶ���������ֵ����
            string[] strFieldType =new string[this.m_FieldsType.Count];
            this.m_FieldsType.Keys.CopyTo(strFieldType, 0);
            //�����ֶ���������Դ
            this.combFieldType.DataSource = strFieldType; 
 
            //��������������Դ
            this.combDomainType.DataSource = new string[] { "��Χ��", "����ֵ��" };

            //��ȡ�ֶ����͹ؼ���
            string strFieldKey=  GetFieldDictionaryKey(this.m_Domain.FieldType);
            this.combFieldType.SelectedItem =(strFieldKey == "")?null:strFieldKey;

            //��ȡ�ϲ����͹ؼ���
            string strMergePolicy = GetMergePolicyDictKey(this.m_Domain.MergePolicy);
            this.combMergePolicy.SelectedItem = (strMergePolicy == "") ? null : strMergePolicy;

            //��ȡ�ָ����͹ؼ���
            string strSplictPolicy=   GetSplitPolicyDictKey(this.m_Domain.SplitPolicy);
            this.combSplitPolicy.SelectedItem = (strSplictPolicy == "") ? null : strSplictPolicy;
          
            //��ѯ���ýӿ�            
            if (this.m_Domain.Type==esriDomainType.esriDTRange)
            {
                //���������͵�ѡ����
                this.combDomainType.SelectedItem = "��Χ��";
                //��ѯ���ýӿ�
                IRangeDomain tRangeDomain = this.m_Domain as IRangeDomain;
                this.txtMaxValue.Text = (tRangeDomain.MaxValue == null) ? "0" : Convert.ToString(tRangeDomain.MaxValue);
                this.txtMinValue.Text = (tRangeDomain.MinValue == null) ? "0" : Convert.ToString(tRangeDomain.MinValue);         
            }
            else
            {
                //���������͵�ѡ����
                this.combDomainType.SelectedItem = "����ֵ��";
                //��ѯ���ýӿ�
                ICodedValueDomain tCodeValueDomain = this.m_Domain as ICodedValueDomain;
                for (int i = 0; i < tCodeValueDomain.CodeCount; i++)
                {
                    this.codeDataGrid.Rows.Add(tCodeValueDomain.get_Value(i), tCodeValueDomain.get_Name(i));
                }
            }

            //�����������
            SettingComponent();
        }

        private void combDomainType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combDomainType.SelectedItem.ToString() == "��Χ��")
            {
                //��Χ��ʱ                  
                if (this.tabControl1.TabPages.Contains(this.tabPage2) == true)
                {
                    this.tabControl1.TabPages.Remove(this.tabPage2);    //�Ƴ�����ֵ�����
                }
                if (this.tabControl1.TabPages.Contains(this.tabPage3) == false)
                {
                    this.tabControl1.TabPages.Add(this.tabPage3);       //��ӷ�Χ������
                } 
            }
            else
            {                
                //����ֵ��ʱ
                if (this.tabControl1.TabPages.Contains(this.tabPage2) == false)
                {
                    //��Ӵ���ֵ�����
                    this.tabControl1.TabPages.Add(this.tabPage2);
                }
                if (this.tabControl1.TabPages.Contains(this.tabPage3) == true)
                {   
                    //�Ƴ���Χ�����
                    this.tabControl1.TabPages.Remove(this.tabPage3);   
                }
            }
        }

        private void combFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {            
            string[] strNumFieldType = new string[] { "������", "������", "������", "˫������" };
            IList<string> tStrList = strNumFieldType as IList<string>;
           
            string strFieldType = this.combFieldType.SelectedItem.ToString();
            if (tStrList.Contains(strFieldType))
            {
                //���úϲ������������Դ
                this.combMergePolicy.DataSource = new string[] { "Ĭ��ֵ", "��ֵ", "����Ȩ��" };
                //���÷ָ�����������Դ
                this.combSplitPolicy.DataSource = new string[] { "Ĭ��ֵ", "����", "���α���" };
            }
            else
            {
                //���úϲ������������Դ
                this.combMergePolicy.DataSource = new string[] { "Ĭ��ֵ" };
                //���÷ָ�����������Դ
                this.combSplitPolicy.DataSource = new string[] { "Ĭ��ֵ", "����"}; 
            }

            if (string.Compare(strFieldType, "�ַ���", true) == 0)
            {  
                //��������������Դ
                this.combDomainType.DataSource = new string[] { "����ֵ��" };
            }
            else
            {  
                //��������������Դ
                this.combDomainType.DataSource = new string[] {"��Χ��","����ֵ��" };              
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                IDomain tempDomain = null;

                if (combDomainType.SelectedItem.ToString() == "��Χ��")
                {
                    //ʵ������Χ�����
                    tempDomain = new RangeDomainClass();

                    //��ѯ�ӿ�
                    IRangeDomain tRangDomain = tempDomain as IRangeDomain;
                    tRangDomain.MinValue = this.txtMinValue.Text;
                    tRangDomain.MaxValue = this.txtMaxValue.Text;
                }
                else
                {
                    //ʵ����ֵ�����
                    tempDomain = new CodedValueDomainClass();

                    //��ѯ�ӿ�
                    ICodedValueDomain tCodedValueDomain = tempDomain as ICodedValueDomain;
                    for (int i = 0; i < this.codeDataGrid.Rows.Count-1; i++)
                    {
                        DataGridViewRow tDataRow = this.codeDataGrid.Rows[i];
                        //����ָ�����ֶ����͵õ�����ֵ
                        //object objValue = GetCodedValue(this.combFieldType.Text, tDataRow.Cells[0].Value);
                        object objValue = DbInfoReadWrite.GetCodedValue(this.m_FieldsType[this.combFieldType.Text], tDataRow.Cells[0].Value);
                        if (Convert.ToString(tDataRow.Cells[1].Value) == "") throw (new Exception(string.Format("{0} �����������ܿ�",objValue)));
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
                    this.codeDataGrid.Rows[e.RowIndex].ErrorText = string.Format("��������ȷ�� '{0}' ����", this.combFieldType.Text);
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

        #region ˽�з���

        /// <summary>
        /// ����ָ�����ֶ�����,�õ�����ֵ
        /// </summary>
        /// <param name="pFieldType">�ֶ�����������</param>
        /// <param name="pCellValue">����ֵ����</param>
        /// <returns>������ȷ�Ĵ���ֵ</returns>
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
                throw(new Exception(string.Format("{0} ֵ���ֶ����Ͳ���,���������룡",pCellValue)));
            }
        }  

        /// <summary>
        /// ͨ��ָ���ֶ����Ͳ��ҹؼ���
        /// </summary>
        /// <param name="pMergePolicy">�ֶ�����</param>
        /// <returns>���عؼ���</returns>
        private string GetFieldDictionaryKey(esriFieldType pFieldValue)
        {
            IDictionaryEnumerator tDictEnumerator = this.m_FieldsType.GetEnumerator();
            return GetDictKey(tDictEnumerator, pFieldValue);          
        }

        /// <summary>
        /// ͨ��ָ���ϲ����Ͳ��ҹؼ���
        /// </summary>
        /// <param name="pMergePolicy">�ϲ�����</param>
        /// <returns>���عؼ���</returns>
        private string GetMergePolicyDictKey(esriMergePolicyType pMergePolicy)
        {
            IDictionaryEnumerator tDictEnumerator = this.m_MergePolicyType.GetEnumerator();
            return GetDictKey(tDictEnumerator, pMergePolicy);
        }

        /// <summary>
        /// ͨ��ָ���ָ����Ͳ��ҹؼ���
        /// </summary>
        /// <param name="pMergePolicy">�ָ�����</param>
        /// <returns>���عؼ���</returns>
        private string GetSplitPolicyDictKey(esriSplitPolicyType pSplitPolicy)
        {
            IDictionaryEnumerator tDictEnumerator = this.m_SplitPolicyType.GetEnumerator();
            return GetDictKey(tDictEnumerator, pSplitPolicy);
        }

        /// <summary>
        /// ��ö�������в�ѯָ��ֵ�Ĺؼ���
        /// </summary>
        /// <param name="pDictEnumerator">Dictionary�ؼ���</param>
        /// <returns>ֵ����</returns>
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
        /// �����ֶ����ͷ���.NETƽ̨�µ���������
        /// </summary>
        /// <param name="pFieldType">�ֶ�����</param>
        /// <returns>.NET��������</returns>
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
        /// ����ָ������ֵ���ֶ�������������������
        /// </summary>
        /// <param name="pDomainType">��ֵ����</param>
        /// <param name="pFieldType">�ֶ�����</param>
        private void SetColumnDataType(string pDomainType,string pFieldType)
        {
            if (pDomainType == "����ֵ��")
            {
                this.codeDataGrid.Columns[0].ValueType = GetDataType(pFieldType);
            }
        }

        /// <summary>
        /// �����������
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
                //������״̬Ϊ����
                //��Ϊ�ı����ƺ����������棬����������ƣ������޸�
                this.txtName.Enabled = false;
                this.txtDescription.Enabled = true;

                this.txtName.ReadOnly = true;
                this.txtDescription.ReadOnly = false;

                //�༭ʱ�����Ͳ����޸�
                this.combDomainType.Enabled = false;
                //�༭ʱ���ֶ����Ͳ����޸�
                this.combFieldType.Enabled = false;

                //���ñ�����ɫ
                this.txtName.BackColor = System.Drawing.SystemColors.Window;
                this.txtDescription.BackColor = System.Drawing.SystemColors.Window;
                
                //�������뽹��
                this.txtName.Focus();

                if (this.panelBottom.Visible == false)
                {
                    this.panelBottom.Visible = true;
                }
            }
            else if (this.m_DomainDlgState == EnumDomainDlgState.New)
            {
                //������״̬Ϊ����                
                this.txtName.Enabled = true;
                this.txtDescription.Enabled = true;

                this.txtName.ReadOnly = false;
                this.txtDescription.ReadOnly = false;

                //���ñ�����ɫ
                this.txtName.BackColor = System.Drawing.SystemColors.Window;
                this.txtDescription.BackColor = System.Drawing.SystemColors.Window;

                //�������뽹��
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