using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// �б���ListFieldItem��
    /// </summary>
    public class ListFieldItem
    {
        private IField m_Field;
        private bool m_IsNew = false;
        private bool m_Changed = false;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public ListFieldItem()
        {
            //ʵ�������ֶζ���
            IFieldEdit tFieldEdit = new FieldClass();
            tFieldEdit.Name_2 = "NewField";
            tFieldEdit.AliasName_2 = "���ֶ�";
            tFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            tFieldEdit.Length_2 = 50;
            tFieldEdit.IsNullable_2 = true;

            this.m_Field = tFieldEdit as IField;
            this.m_IsNew = true;
        }

        /// <summary>
        /// ʵ����ָ���ֶεĹ��캯��
        /// </summary>
        /// <param name="pField">IField����</param>
        public ListFieldItem(IField pField)
        {
            this.m_Field = pField;
        }

        /// <summary>
        /// ��ȡ�������ֶ�
        /// </summary>
        public IField Field
        {
            get
            {
                return this.m_Field;
            }
            set
            {
                this.m_Field = value;
                //��Ǹ��ֶ��ѷ����仯
                this.m_Changed = true;
            }
        }

        /// <summary>
        /// ��ȡ�������ֶ��Ƿ�Ϊ����״̬
        /// </summary>
        public bool IsNew
        {
            get
            {
                return this.m_IsNew;
            }
            set
            {
                this.m_IsNew = value;
            }
        }

        /// <summary>
        /// ��ȡ�������ֶνṹ�Ƿ����仯.����ѷ����仯��Ϊ true,����Ϊ false;
        /// </summary>
        public bool Changed
        {
            get
            {
                return this.m_Changed;
            }
            set
            {
                this.m_Changed = value;
            }
        }

        /// <summary>
        /// ����ToString()����
        /// </summary>
        /// <returns>�����ַ�������</returns>
        public override string ToString()
        {
            return m_Field.Name;
        }
    }  
}
