using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace AG.Pipe.Analyst3DModel.Editor
{
    public class EnumTypeEditor : UITypeEditor
    {

        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;//下拉列表方式，即当在PropertyGrid中选中对象的属性后会显示一个下拉列表按钮
        }

        //控制编辑时数据的显示方式，并返回该一个值作为属性的值。
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService = null;
            if (provider != null)
            {
                editorService = provider.GetService(
                    typeof(IWindowsFormsEditorService))
                    as IWindowsFormsEditorService;
            }
            if (editorService != null)
            {
                EnumCombox mComBox = new EnumCombox((Enum)value, editorService); //创建显示枚举类型数据的中文描述的控件
                editorService.DropDownControl(mComBox);//展开下拉列表在下拉区域显示传入的控件
                value = mComBox.MyValue;//将列表控件中用于选定的值赋值给需要返回的Value
            }
            return value;
        }
    }

    public class EnumConverterEx : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return true;
        }
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {

            List<KeyValuePair<Enum, string>> mList = EnumCombox.ToListForBind(value.GetType());
            foreach (KeyValuePair<Enum, string> mItem in mList)
            {
                if (mItem.Key.Equals(value))
                {
                    return mItem.Value;
                }
            }
            return "";
        }
    }
}
