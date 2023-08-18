using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 新增字段窗体
    /// </summary>
    public partial class FormAddField : Form
    {
        private IField m_Field=null;

        /// <summary>
        /// 实例化新增字段对象
        /// </summary>
        public FormAddField()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取新增字段
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
            //为控件设置输入焦点
            this.txtFieldName.Focus(); 
            this.btnOK.Enabled = false;
            this.chkIsNull.Checked = true;

            //添加字段类型
            string[] strFieldItems=Enum.GetNames(typeof(EnumFieldItems));

            //遍历添加字段项
            for (int i = 0; i < strFieldItems.Length; i++)
            {
                if (string.Compare(strFieldItems[i], EnumFieldItems.OID.ToString(), true) == 0 ||
                    string.Compare(strFieldItems[i], EnumFieldItems.几何对象.ToString(), true) == 0)
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
            //字段类型转换
            EnumFieldItems tEnumFieldItem = (EnumFieldItems)Enum.Parse(typeof(EnumFieldItems), strFieldType);

            //根据字段类型不同设置文本或标签状态
            if (tEnumFieldItem == EnumFieldItems.单精度 ||
                tEnumFieldItem == EnumFieldItems.双精度 ||
                tEnumFieldItem == EnumFieldItems.短整型 ||
                tEnumFieldItem == EnumFieldItems.整型)
            {
                this.lblLength.Text = "字段精度";
                this.txtLength.Text = "10";

                if (tEnumFieldItem == EnumFieldItems.单精度 ||
                    tEnumFieldItem == EnumFieldItems.双精度)
                {
                    this.txtScale.Enabled = true;
                }
            }
            else
            {
                this.lblLength.Text = "字段长度";  
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                //实例化字段对象
                IFieldEdit tFieldEdit = new FieldClass();
                tFieldEdit.Name_2 = txtFieldName.Text;
                tFieldEdit.AliasName_2 = txtFieldAlias.Text;
                tFieldEdit.IsNullable_2 = chkIsNull.Checked;

                //字段类型转换
                esriFieldType tFieldType = (esriFieldType)((int)Enum.Parse(typeof(EnumFieldItems), combFieldType.Text));
                tFieldEdit.Type_2 = tFieldType;

                if (tFieldType == esriFieldType.esriFieldTypeDouble ||
                    tFieldType == esriFieldType.esriFieldTypeSingle ||
                    tFieldType == esriFieldType.esriFieldTypeSmallInteger ||
                    tFieldType == esriFieldType.esriFieldTypeInteger)
                {
                    if (txtLength.Text.Length == 0 || txtScale.Text.Length == 0)
                    {
                        MessageBox.Show("字段长度或小数位数不能为空，请输入有效的数字!");
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
                        MessageBox.Show("请输入字段长度");
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