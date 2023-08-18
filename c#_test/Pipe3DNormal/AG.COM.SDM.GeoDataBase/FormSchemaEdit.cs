using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// ��ṹ�޸���
    /// </summary>
    public partial class FormSchemaEdit : Form
    {                                                                           
        private IObjectClass m_ObjectClass;                                     //Ҫ�����Ҫ�ر�
        private IList<ListFieldItem> m_DelFields=new List<ListFieldItem>();     //Ҫ�Ƴ����ֶμ���
        private IList<ListFieldItem> m_AddFields = new List<ListFieldItem>();   //Ҫ��ӵ��ֶμ���
        private IEnumDomain m_EnumDomain;                                       //�����ռ��е�������
        private ListFieldItem m_CurrentFieldItem;                               //��ǰѡ����ֶ��� 

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="pFeatureClass">IDataset����</param>
        public FormSchemaEdit(IObjectClass pObjectClass)
        {
            //��ʼ���������
            InitializeComponent();

            this.m_ObjectClass = pObjectClass;
        }

        private void FormSchemaEdit_Load(object sender, EventArgs e)
        {
            this.txtTableName.Text = (this.m_ObjectClass as IDataset).Name;
            this.txtTableAlias.Text = this.m_ObjectClass.AliasName;

            //�󶨱��ֶ�
            IFields tFields = this.m_ObjectClass.Fields;
            for (int i = 0; i < tFields.FieldCount; i++)
            {
                this.listFields.Items.Add(new ListFieldItem(tFields.get_Field(i)));
            }

            //��ȡ����������
            IDataset tDataset = this.m_ObjectClass as IDataset;
            IWorkspace tWorkspace = tDataset.Workspace;
            IWorkspaceDomains tWorkspaceDomains = tWorkspace as IWorkspaceDomains;

            if (tWorkspaceDomains != null)
            {
                this.m_EnumDomain = tWorkspaceDomains.Domains;
            }

            //���ֶ�����
            this.combFieldType.DataSource = Enum.GetNames(typeof(EnumFieldItems));
            //ѡ���һ��
            if(this.listFields.Items.Count>0)
                this.listFields.SelectedIndex = 0;
        }      

        private void listFields_SelectedValueChanged(object sender, EventArgs e)
        {           
            ListFieldItem listField = this.listFields.SelectedItem as ListFieldItem;
            if (listField == null) return;

            //�����ֶ�ֵ
            UpdateCurrentField(listField);
            //���ÿؼ�״̬�Ŀ�����
            SetControlEnable(listField);
            //�������ɼ���
            SetPanelVisible(listField.Field.Type);
            //���ÿؼ���ʾ�ı�
            SetControlText(listField);
        }   

        private void combFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //��ȡѡ����ֶ�������
            string strFieldType = this.combFieldType.SelectedItem as string;

            //����֮�����ת��
            EnumFieldItems tEnumFieldItems = (EnumFieldItems)Enum.Parse(typeof(EnumFieldItems), strFieldType);
            this.combFieldType.Tag = (esriFieldType)((int)tEnumFieldItems);       

            //��ȡѡ����ֶ���
            ListFieldItem tListFieldItem = this.listFields.SelectedItem as ListFieldItem;
            if (tListFieldItem == null) return;

            if (tListFieldItem.IsNew == true && (tEnumFieldItems == EnumFieldItems.OID || tEnumFieldItems == EnumFieldItems.���ζ���))
            {
                //�¼��ֶ����Ͳ���ΪOID�򼸺ζ�������,���ѡ�����Զ�����Ϊ�ַ���
                this.combFieldType.SelectedItem = EnumFieldItems.�ַ���.ToString();
            }

            if (tListFieldItem.IsNew == true)
            {
                if (tEnumFieldItems == EnumFieldItems.˫���� || tEnumFieldItems == EnumFieldItems.������)
                {
                    this.lblLength.Text = "�ֶξ���";

                    this.txtLength.Text = "10";
                    this.txtScale.Text = "3";
                    this.txtScale.Enabled = true;
                }
                else
                {
                    this.lblLength.Text = "�ֶγ���";
                    this.txtLength.Text = "50";
                    this.txtScale.Text = "";
                    this.txtScale.Enabled = false;
                }
            }

            #region ���������� 
            //���������
            this.combDomains.Items.Clear();

            if (this.m_EnumDomain != null)
            {
                //�����α�����ʼλ��
                this.m_EnumDomain.Reset();
                IField tField = tListFieldItem.Field;
                for (IDomain tDomin = m_EnumDomain.Next(); tDomin != null; tDomin = m_EnumDomain.Next())
                {
                    if (tDomin.FieldType == tField.Type)
                    {
                        ListDomainItem tlistDomainItem = new ListDomainItem(tDomin);
                        this.combDomains.Items.Add(tlistDomainItem);    
                    }
                }
            }

            #endregion
        }

        private void txtFieldName_Leave(object sender, EventArgs e)
        {
            if (this.m_CurrentFieldItem.IsNew == true)
            {
                if (string.Equals(this.m_CurrentFieldItem.Field.Name, this.txtFieldName.Text) == false)
                {
                    //��ѯIFieldEdit2�ӿ�
                    IFieldEdit2 tFieldEdit = this.m_CurrentFieldItem.Field as IFieldEdit2;
                    tFieldEdit.Name_2 = this.txtFieldName.Text;

                    //����ָ�����ڼ����е�����
                    int index = this.listFields.Items.IndexOf(this.m_CurrentFieldItem);
                    //�������ָ��������������ʾ
                    if (index > -1) this.listFields.Items[index] = this.m_CurrentFieldItem;
                }
            }
        }

        private void txtDefault_Leave(object sender, EventArgs e)
        {               
            //Ĭ��ֵ��Ϊ�յ����
            if (this.txtDefault.Text.Length > 0)
            {                
                try
                {
                    object tObjValue = null;
                    IField tField = this.m_CurrentFieldItem.Field;

                    if (tField.Type == esriFieldType.esriFieldTypeString)
                    {
                        tObjValue = Convert.ToString(this.txtDefault.Text);             //ת��Ϊ�ַ�������
                    }
                    else if (tField.Type == esriFieldType.esriFieldTypeDouble ||
                        this.m_CurrentFieldItem.Field.Type == esriFieldType.esriFieldTypeSingle)
                    {
                        tObjValue = Convert.ToDouble(this.txtDefault.Text);             //ת��Ϊ˫���ȶ���
                    }
                    else if (tField.Type == esriFieldType.esriFieldTypeDate)
                    {
                        tObjValue = Convert.ToDateTime(this.txtDefault.Text);           //ת��Ϊ�����Ͷ���
                    }
                    else
                    {
                        tObjValue = Convert.ToInt32(this.txtDefault.Text);              //ת��Ϊ���Ͷ���
                    }

                    this.txtDefault.Tag = tObjValue;
                }
                catch
                {
                    MessageBox.Show("Ĭ��ֵ����ת������;");
                }
            }        
        }      

        private void txtLength_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox.Text.Length > 0)
            {
                try
                {
                    int i = Convert.ToInt32(txtBox.Text);
                }
                catch
                {
                    MessageBox.Show("��������Ч�ֶγ���ֵ��С��λ��ֵ");
                }
            }
        }

        private void txtTableAlias_TextChanged(object sender, EventArgs e)
        {
            this.btnApply.Enabled = true;
        }

        private void btnAddField_Click(object sender, EventArgs e)
        {
            //ʵ����ListFieldItem
            ListFieldItem tListField = new ListFieldItem();
            this.listFields.Items.Add(tListField);
            this.listFields.SelectedItem = tListField;

            //��������ֶμ���
            this.m_AddFields.Add(tListField);

            //����Ӧ�ð�ťΪ����״̬
            this.btnApply.Enabled = true;
        }

        private void btnDelField_Click(object sender, EventArgs e)
        {
            //ɾ���ֶ�
            ListFieldItem tListFieldItem = this.listFields.SelectedItem as ListFieldItem;
            this.listFields.Items.Remove(tListFieldItem);

            if (tListFieldItem.IsNew == true)
            {
                //���Ϊ�����ֶ�������Ӽ������Ƴ�����
                this.m_AddFields.Remove(tListFieldItem);
            }
            else
            {
                //����ɾ���ֶμ���,��ʶΪҪ�Ƴ�������
                this.m_DelFields.Add(tListFieldItem);
            }

            if (this.listFields.Items.Count > 0)
            {
                this.m_CurrentFieldItem = this.listFields.Items[0] as ListFieldItem;
                this.listFields.SelectedItem = this.m_CurrentFieldItem;
            }

            //����Ӧ�ð�ťΪ����״̬
            this.btnApply.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //���ö�����Ϊ��
            this.m_ObjectClass = null;
            this.m_EnumDomain = null;
            this.m_AddFields = null;
            this.m_DelFields = null;
            this.m_CurrentFieldItem = null;
            //�رմ���
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {                   
            if (this.btnApply.Enabled = true)
            {
                //����[Ӧ��]����
                btnApply_Click(null, null);
            }
            //�رմ���
            btnClose_Click(null, null);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            //�����ǰ�����޸��򷵻�
            if (CanEdit() == false)
            {
                MessageBox.Show("��ǰ���ѱ�����,���ܶ�������޸Ĳ���!","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;    //����
            }

            try
            {
                //���µ�ǰѡ���ֶ�                 
                ListFieldItem tFieldItem = this.listFields.SelectedItem as ListFieldItem;

                if (tFieldItem != null)
                {
                    if (tFieldItem.IsNew == true)
                        UpdateNewField(tFieldItem);
                    else
                        UpdateOldField(tFieldItem);
                }    
      
          
                //����ֶ�
                for (int i = 0; i < this.m_AddFields.Count; i++)
                {
                    tFieldItem = this.m_AddFields[i];
                    if (tFieldItem.IsNew == true)
                    {
                        this.m_ObjectClass.AddField(tFieldItem.Field);
                        tFieldItem.IsNew = false;
                    }
                }

                //ɾ���ֶ�
                for (int i = 0; i < this.m_DelFields.Count; i++)
                {
                    tFieldItem = this.m_DelFields[i];
                    if (tFieldItem.IsNew == false)
                    {                   
                        this.m_ObjectClass.DeleteField(tFieldItem.Field);
                    }
                }

                //���������
                this.m_DelFields.Clear();              
                this.m_AddFields.Clear();

                //��ѯIClassSchemaEdit�ӿ�����
                IClassSchemaEdit tClassSchemaEdit = this.m_ObjectClass as IClassSchemaEdit;
                if (tClassSchemaEdit != null && string.Equals(this.m_ObjectClass.AliasName, this.txtTableAlias.Text) == false)
                {
                    tClassSchemaEdit.AlterAliasName(this.txtTableAlias.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.btnApply.Enabled = false;
            }
        }

        #region ˽�ж��巽��
        /// <summary>
        /// ����Ҫ��ʾ�Ŀؼ��ļ�����
        /// </summary>
        /// <param name="pListFieldItem">ListFieldItem��</param>
        private void SetControlText(ListFieldItem pListFieldItem)
        {
            IField tField = pListFieldItem.Field;

            this.txtFieldName.Text = tField.Name;
            this.txtFieldAlias.Text = tField.AliasName;
            this.combFieldType.SelectedItem = ((EnumFieldItems)((int)tField.Type)).ToString();
            this.txtDefault.Text =Convert.ToString( tField.DefaultValue);
            this.txtDefault.Tag = tField.DefaultValue;
            this.chkIsNull.Checked = tField.IsNullable;

            #region ����������
            if (tField.Domain != null)
            {
                //����������
                for (int index = 0; index < this.combDomains.Items.Count; index++)
                {
                    if (string.Compare(this.combDomains.Items[index].ToString(), tField.Domain.Description) == 0)
                    {
                        this.combDomains.SelectedIndex = index;
                        break;
                    }
                }
            }
            else
            {
                this.combDomains.SelectedItem = null;
            }
            #endregion


            if (tField.Type == esriFieldType.esriFieldTypeGeometry)
            {
                #region ����Grid��С
                //����ֶ�����Ϊ���ζ�������Ҫ������Grid��С
                for (int i = 0; i < 3; i++)
                {
                    //ʵ������ǩ����
                    Label tLabel = new Label();
                    tLabel.AutoSize = true;
                    tLabel.Text = string.Format("Grid {0}", i + 1);
                    tLabel.Left = this.lblGeoType.Left;
                    tLabel.Top = this.lblGeoType.Top + (i + 1) * 28;

                    //ʵ�����ı������
                    TextBox txtBox = new TextBox();
                    txtBox.Enabled = false;
                    txtBox.Size = this.txtGeometryType.Size;
                    txtBox.Left = this.txtGeometryType.Left;
                    txtBox.Top = this.txtGeometryType.Top + (i + 1) * 27;
                    if (i < tField.GeometryDef.GridCount)
                        txtBox.Text = Convert.ToString(tField.GeometryDef.get_GridSize(i));
                    else
                        txtBox.Text = "0";

                    //���Label��ǩ
                    this.pnlGeoAttr.Controls.Add(tLabel);
                    //���TextBox�ؼ�
                    this.pnlGeoAttr.Controls.Add(txtBox);
                }
                #endregion

                this.txtGeometryType.Text = GetGeoTypeString(tField.GeometryDef.GeometryType);
            }
            else
            {
                #region �����ֶγ��ȼ�С��λ
                if (tField.Type == esriFieldType.esriFieldTypeDouble || tField.Type == esriFieldType.esriFieldTypeSingle)
                {
                    this.lblLength.Text = "�ֶξ���:";
                    this.txtLength.Text = Convert.ToString(tField.Precision);
                    this.txtScale.Text = Convert.ToString(tField.Scale);
                }
                else if (tField.Type == esriFieldType.esriFieldTypeInteger || tField.Type == esriFieldType.esriFieldTypeSmallInteger)
                {
                    this.lblLength.Text = "�ֶξ���:";
                    this.txtLength.Text = Convert.ToString(tField.Length);
                    this.txtScale.Text = "";                    
                }
                else
                {
                    this.lblLength.Text = "�ֶγ���:";
                    this.txtLength.Text = Convert.ToString(tField.Length);
                    this.txtScale.Text = "";
                }
                #endregion
            }
        }

        /// <summary>
        /// ���ÿؼ�״̬�Ŀ�����
        /// </summary>
        /// <param name="bIsNew">�Ƿ�Ϊ�����ֶ�</param>
        private void SetControlEnable(ListFieldItem pListFieldItem)
        {
            if (pListFieldItem.Field.Type == esriFieldType.esriFieldTypeOID ||
                pListFieldItem.Field.Type == esriFieldType.esriFieldTypeRaster ||
                pListFieldItem.Field.Type == esriFieldType.esriFieldTypeGeometry ||
                pListFieldItem.Field.Type == esriFieldType.esriFieldTypeGUID ||
                pListFieldItem.Field.Type == esriFieldType.esriFieldTypeGlobalID)
            {
                //ɾ����ť������
                this.btnDelField.Enabled = false;
                this.txtDefault.Enabled = false;
                this.txtFieldAlias.Enabled = false;
            }
            else
            {
                this.btnDelField.Enabled = true;
                this.txtDefault.Enabled = true;
                this.txtFieldAlias.Enabled = true;
            }

            if (pListFieldItem.IsNew ==true && (pListFieldItem.Field.Type == esriFieldType.esriFieldTypeDouble 
                || pListFieldItem.Field.Type == esriFieldType.esriFieldTypeSingle))
            {
                this.txtScale.Enabled = true;
            }
            else
            {
                this.txtScale.Enabled = false;
            }

            //�Ƿ�Ϊ�����ֶ�
            bool bIsNew = pListFieldItem.IsNew;
            this.txtFieldName.Enabled = bIsNew;
            this.combFieldType.Enabled = bIsNew;
            this.chkIsNull.Enabled = bIsNew;
            this.txtLength.Enabled = bIsNew;
          
        }

        /// <summary>
        /// �������Ŀɼ���
        /// </summary>
        /// <param name="enumFieldType">�ֶ�����</param>
        private void SetPanelVisible(esriFieldType enumFieldType)
        {
            //��Ϊ�ײ�
            this.pnlCommon.SendToBack();

            if (enumFieldType == esriFieldType.esriFieldTypeGeometry)
            {
                this.pnlGeoAttr.Visible = true;
                this.pnlAtrr.Visible = false;
            }
            else if(enumFieldType==esriFieldType.esriFieldTypeOID)
            {
                this.pnlAtrr.Visible=false;
                this.pnlGeoAttr.Visible=false;
            }
            else 
            {
                this.pnlGeoAttr.Visible = false;
                this.pnlAtrr.Visible = true;
            } 
        }

        /// <summary>
        /// �����ֶ�ֵ
        /// </summary>
        /// <param name="pListFieldItem">�����б��ֶ���</param>
        private void UpdateCurrentField(ListFieldItem pListFieldItem)
        {
            if (pListFieldItem != this.m_CurrentFieldItem && this.m_CurrentFieldItem != null)
            {
                if (this.m_CurrentFieldItem.IsNew == true)
                {
                    //�����½����ֶζ���
                    UpdateNewField(this.m_CurrentFieldItem);
                }
                else
                {
                    //����ԭ���ֶζ���
                    UpdateOldField(this.m_CurrentFieldItem);
                }
            }

            //�ı䵱ǰ�ֶ���
            this.m_CurrentFieldItem = pListFieldItem;
        }

        /// <summary>
        /// ��������ӵ��ֶ�
        /// </summary>
        /// <param name="pListFieldItem">�����б��ֶ���</param>
        public void UpdateNewField(ListFieldItem pListFieldItem)
        {
            //��ѯ���ýӿ�
            IFieldEdit2 tFieldEdit = pListFieldItem.Field as IFieldEdit2;
            tFieldEdit.Name_2 = this.txtFieldName.Text;
            tFieldEdit.AliasName_2 = this.txtFieldAlias.Text;
            tFieldEdit.Editable_2 = true;
            tFieldEdit.IsNullable_2 = this.chkIsNull.Checked;
            tFieldEdit.Type_2 = (esriFieldType)this.combFieldType.Tag;
        
            if (this.txtDefault.Tag != null)
            {
                //����Ĭ��ֵ
                tFieldEdit.DefaultValue_2 = this.txtDefault.Tag;
            }
                        
            if (this.combDomains.SelectedItem != null)
            {
                //����������
                tFieldEdit.Domain_2 = (this.combDomains.SelectedItem as ListDomainItem).Domain;
            }

            try
            {
                if (tFieldEdit.Type == esriFieldType.esriFieldTypeString)
                {
                    //�����ֶγ���
                    tFieldEdit.Length_2 = Convert.ToInt32(this.txtLength.Text);
                }
                else if (tFieldEdit.Type == esriFieldType.esriFieldTypeSingle ||
                    tFieldEdit.Type == esriFieldType.esriFieldTypeDouble ||
                    tFieldEdit.Type == esriFieldType.esriFieldTypeInteger ||
                    tFieldEdit.Type == esriFieldType.esriFieldTypeSmallInteger)
                {
                    //���þ���ֵ��
                    tFieldEdit.Precision_2 = Convert.ToInt32(this.txtLength.Text);

                    if (tFieldEdit.Type == esriFieldType.esriFieldTypeDouble ||
                        tFieldEdit.Type == esriFieldType.esriFieldTypeSingle)
                    {             
                        //����С��λ��
                        tFieldEdit.Scale_2 = Convert.ToInt32(this.txtScale.Text);
                    }
                }
            }
            catch
            {
                MessageBox.Show("��������Ч���ֶγ��Ȼ�С��λ��ֵ");
            }  
        } 

        /// <summary>
        /// ���±��е�ԭ���ֶ�
        /// </summary>
        /// <param name="pListFieldItem">�����б��ֶ���</param>
        public void UpdateOldField(ListFieldItem pListFieldItem)
        {
            //�����ǰ�����޸��򷵻�
            if (CanEdit() == false) return;

            //�޸��ֶα���
            IClassSchemaEdit tClassSchemaEdit = this.m_ObjectClass as IClassSchemaEdit;
            if (tClassSchemaEdit != null)
            {
                IField tField = pListFieldItem.Field;        

                if (string.Compare(this.txtFieldAlias.Text, tField.AliasName) != 0)
                {        
                    //�޸��ֶα���
                    tClassSchemaEdit.AlterFieldAliasName(tField.Name, this.txtFieldAlias.Text);
                }

                if (tField.DefaultValue.ToString() != Convert.ToString(this.txtDefault.Tag))
                {      
                    //�޸�Ĭ��ֵ
                    tClassSchemaEdit.AlterDefaultValue(tField.Name, this.txtDefault.Tag);                   
                }

                if (tField.DomainFixed == false)
                {
                    //��ȡ��ǰѡ����������б���
                    ListDomainItem tListDomainItem = this.combDomains.SelectedItem as ListDomainItem;
                    if (tListDomainItem != null)
                    {
                        //��ȡ�ֶ�����������
                        string strDomainName = "";
                        if (tField.Domain != null)
                            strDomainName = tField.Domain.Name;

                        if (string.Compare(strDomainName, tListDomainItem.Domain.Name) != 0)
                        {
                            //�޸�������
                            tClassSchemaEdit.AlterDomain(tField.Name, tListDomainItem.Domain);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ��ȡ���ζ������͵�����
        /// </summary>
        /// <param name="pGeometryType">�ռ��������</param>
        /// <returns>���ض�Ӧ������</returns>
        private string GetGeoTypeString(esriGeometryType pGeometryType)
        {
            if (pGeometryType == esriGeometryType.esriGeometryMultipoint || pGeometryType == esriGeometryType.esriGeometryPoint)
            {
                return "��";
            }
            else if(pGeometryType==esriGeometryType.esriGeometryPolyline || pGeometryType==esriGeometryType.esriGeometryLine)
            {             
                return "��";
            }
            else if (pGeometryType == esriGeometryType.esriGeometryPolygon)
            {
                return "��";
            }
            else
            {
                return pGeometryType.ToString();
            }
        }

        /// <summary>
        /// ���ص�ǰ���Ƿ��ܹ����б༭����
        /// </summary>
        /// <returns>����ܹ��༭��Ϊ true,����Ϊ false</returns>
        private bool CanEdit()
        {
            //��ѯ���ýӿ�
            ISchemaLock tSchemaLock = this.m_ObjectClass as ISchemaLock;
            if (tSchemaLock != null)
            {
                IEnumSchemaLockInfo tEnumSchemaLockInfo;
                //�õ���ǰ���������Ϣ��ö�٣�
                tSchemaLock.GetCurrentSchemaLocks(out tEnumSchemaLockInfo);
                ISchemaLockInfo tSchemaLockInfo = tEnumSchemaLockInfo.Next();
                //���������ϢΪ��ռ���򷵻� false
                if (tSchemaLockInfo != null && tSchemaLockInfo.SchemaLockType == esriSchemaLock.esriExclusiveSchemaLock) return false;
                
            }
  
            //���m_ObjectClassԴ����Ϊ�ļ�����
            //���߱�����,�򷵻�true
            return true;
        }

        #endregion
    }
}