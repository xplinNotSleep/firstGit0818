using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace AG.Pipe.Analyst3DModel.Editor
{
    public partial class EnumCombox : UserControl
    {
        private IWindowsFormsEditorService mEditorService = null;
        private Enum curValue;

        //传入枚举值，以及windowsFormsEditorService控件
        public EnumCombox(Enum mValue, IWindowsFormsEditorService editorService)
        {
            InitializeComponent();
            mEditorService = editorService;
            curValue = mValue;
            if (mValue != null)
            {
                mCurEnumType = mValue.GetType();
                if (mCurEnumType != null)
                {
                    if (mCurEnumType.IsEnum)
                    {
                        listBox1.DataSource = ToListForBind(mCurEnumType);
                        listBox1.ValueMember = "Key";
                        listBox1.DisplayMember = "Value";
                        listBox1.SelectedValue = curValue;//根据当前对象的枚举值设置列表中应该选中的项
                        this.Height = listBox1.ItemHeight * listBox1.Items.Count + 5;//设置列表控件的高度为列表项能够全部显示出来并且再多5个像素                       
                    }
                }
            }
        }

        private Type mCurEnumType;
        private Enum mValue;
        public Enum MyValue
        {
            get
            {
                mValue = this.listBox1.SelectedValue as Enum;
                return mValue;
            }
        }

        //读取枚举类型的描述信息生成显示数据用的列表数据。   
        public static List<KeyValuePair<Enum, string>> ToListForBind(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (!type.IsEnum)
            {
                throw new ArgumentException("类型必须是枚举型的！", "type");
            }

            List<KeyValuePair<Enum, string>> list = new List<KeyValuePair<Enum, string>>();
            list.Clear();
            Array enumValues = Enum.GetValues(type);

            foreach (Enum value in enumValues)
            {
                list.Add(new KeyValuePair<Enum, string>(value, GetDescription(value)));
            }
            return list;
        }

        #region GetDescription
        /// <summary>
        /// 获取枚举类型值的描述信息.
        /// </summary>
        /// <param name="value">枚举类型<see cref="Enum"/>值</param>
        /// <returns>A string containing the text of the <see cref="DescriptionAttribute"/>.</returns>
        public static string GetDescription(Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            string description = value.ToString();
            System.Reflection.FieldInfo fieldInfo = value.GetType().GetField(description);
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                description = attributes[0].Description;
            }
            else
            {
                description = value.ToString();
            }
            return description;
        }
        #endregion

        //用户选择一项后就收起列表控件
        void ListBox1SelectedIndexChanged(object sender, EventArgs e)
        {
            mEditorService.CloseDropDown();
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            mEditorService.CloseDropDown();
        }
    }
}
