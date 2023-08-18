using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// 列表项ListFieldItem类
    /// </summary>
    public class ListFieldItem
    {
        private IField m_Field;
        private bool m_IsNew = false;
        private bool m_Changed = false;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ListFieldItem()
        {
            //实例化新字段对象
            IFieldEdit tFieldEdit = new FieldClass();
            tFieldEdit.Name_2 = "NewField";
            tFieldEdit.AliasName_2 = "新字段";
            tFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            tFieldEdit.Length_2 = 50;
            tFieldEdit.IsNullable_2 = true;

            this.m_Field = tFieldEdit as IField;
            this.m_IsNew = true;
        }

        /// <summary>
        /// 实例化指定字段的构造函数
        /// </summary>
        /// <param name="pField">IField对象</param>
        public ListFieldItem(IField pField)
        {
            this.m_Field = pField;
        }

        /// <summary>
        /// 获取或设置字段
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
                //标记该字段已发生变化
                this.m_Changed = true;
            }
        }

        /// <summary>
        /// 获取或设置字段是否为新增状态
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
        /// 获取或设置字段结构是否发生变化.如果已发生变化则为 true,否则为 false;
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
        /// 重载ToString()方法
        /// </summary>
        /// <returns>返回字符串类型</returns>
        public override string ToString()
        {
            return m_Field.Name;
        }
    }  
}
