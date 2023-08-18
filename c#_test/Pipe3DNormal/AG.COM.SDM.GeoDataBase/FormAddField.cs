using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// �����ֶδ���
    /// </summary>
    public partial class FormAddField : Form
    {
        private IField m_Field=null;

        /// <summary>
        /// ʵ���������ֶζ���
        /// </summary>
        public FormAddField()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��ȡ�����ֶ�
        /// </summary>
        public IField Field
        {
            get
            {
                return this.m_Field;
            }
        }

        private void FormAddField_Load(object sender, EventArgs e)
        {
            //Ϊ�ؼ��������뽹��
            this.txtFieldName.Focus(); 
            this.btnOK.Enabled = false;
            this.chkIsNull.Checked = true;

            //����ֶ�����
            string[] strFieldItems=Enum.GetNames(typeof(EnumFieldItems));

            //��������ֶ���
            for (int i = 0; i < strFieldItems.Length; i++)
            {
                if (string.Compare(strFieldItems[i], EnumFieldItems.OID.ToString(), true) == 0 ||
                    string.Compare(strFieldItems[i], EnumFieldItems.���ζ���.ToString(), true) == 0)
                {
                    continue;
                }
                else
                {
                    this.combFieldType.Items.Add(strFieldItems[i]);
                }
            }

            if (this.combFieldType.Items.Count > 0) this.combFieldType.SelectedIndex = 0;
        }

        private void combFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtScale.Text = "0";
            this.txtScale.Enabled = false;

            string strFieldType = this.combFieldType.SelectedItem as string; 
            //�ֶ�����ת��
            EnumFieldItems tEnumFieldItem = (EnumFieldItems)Enum.Parse(typeof(EnumFieldItems), strFieldType);

            //�����ֶ����Ͳ�ͬ�����ı����ǩ״̬
            if (tEnumFieldItem == EnumFieldItems.������ ||
                tEnumFieldItem == EnumFieldItems.˫���� ||
                tEnumFieldItem == EnumFieldItems.������ ||
                tEnumFieldItem == EnumFieldItems.����)
            {
                this.lblLength.Text = "�ֶξ���";
                this.txtLength.Text = "10";

                if (tEnumFieldItem == EnumFieldItems.������ ||
                    tEnumFieldItem == EnumFieldItems.˫����)
                {
                    this.txtScale.Enabled = true;
                }
            }
            else
            {
                this.lblLength.Text = "�ֶγ���";  
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                //ʵ�����ֶζ���
                IFieldEdit tFieldEdit = new FieldClass();
                tFieldEdit.Name_2 = txtFieldName.Text;
                tFieldEdit.AliasName_2 = txtFieldAlias.Text;
                tFieldEdit.IsNullable_2 = chkIsNull.Checked;

                //�ֶ�����ת��
                esriFieldType tFieldType = (esriFieldType)((int)Enum.Parse(typeof(EnumFieldItems), combFieldType.Text));
                tFieldEdit.Type_2 = tFieldType;

                if (tFieldType == esriFieldType.esriFieldTypeDouble ||
                    tFieldType == esriFieldType.esriFieldTypeSingle ||
                    tFieldType == esriFieldType.esriFieldTypeSmallInteger ||
                    tFieldType == esriFieldType.esriFieldTypeInteger)
                {
                    if (txtLength.Text.Length == 0 || txtScale.Text.Length == 0)
                    {
                        MessageBox.Show("�ֶγ��Ȼ�С��λ������Ϊ�գ���������Ч������!");
                        return;
                    }
                    else
                    {
                        tFieldEdit.Precision_2 = Convert.ToInt32(txtLength.Text);
                        tFieldEdit.Scale_2 = Convert.ToInt32(txtScale.Text);
                    }
                }
                else
                {
                    if (txtLength.Text.Length == 0)
                    {
                        MessageBox.Show("�������ֶγ���");
                        return;
                    }
                    else
                    {
                        tFieldEdit.Length_2 = Convert.ToInt32(txtLength.Text);
                    }
                }

                this.m_Field = tFieldEdit as IField;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFieldName_Validated(object sender, EventArgs e)
        {
            if (txtFieldName.Text.Length == 0)
                this.btnOK.Enabled = false;
            else
                this.btnOK.Enabled = true;
        } 

    }
}