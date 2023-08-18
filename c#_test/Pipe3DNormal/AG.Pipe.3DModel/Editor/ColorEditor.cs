using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace AG.Pipe.Analyst3DModel.Editor
{

    public class ColorEditor : UITypeEditor
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
                AG.COM.SDM.Utility.Controls.ColorPicker mColorPicker = new AG.COM.SDM.Utility.Controls.ColorPicker();
                mColorPicker.Value = (Color)value;
                //editorService.DropDownControl(mColorPicker);

                value = mColorPicker.Value;//将列表控件中用于选定的值赋值给需要返回的Value
            }
            return value;
        }
    }

    public class ColorConverterEx : ExpandableObjectConverter
    {
        /// <summary>
        /// 默认构造函数,初始化新实例
        /// </summary>
        public ColorConverterEx()
        {
        }
        /// <summary>
        /// 使用参数将给定的值对象转换为指定的类型。
        /// </summary>
        /// <param name="context"> 提供格式上下文。</param>
        /// <param name="culture">如果传递null，则采用当前区域性。</param>
        /// <param name="value"> 要转换的 System.Object。</param>
        /// <param name="destinationType">要转换到的 System.Type</param>
        /// <returns>表示转换的 value 的 System.Object。</returns>
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value is Color)
            {
                Color color = (Color)value;
                string strC = $"{color.R},{color.G},{color.B}";
                return strC;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// 返回此转换器是否可以使用指定的上下文将该对象转换为指定的类型。
        /// </summary>
        /// <param name="context"> System.ComponentModel.ITypeDescriptorContext，提供格式上下文。</param>
        /// <param name="destinationType">一个 System.Type，表示要转换到的类型。</param>
        /// <returns>如果该转换器能够执行转换，则为 true；否则为 false。</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(Color))
                return true;
            else
                return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// 返回该转换器是否可以使用指定的上下文将给定类型的对象转换为此转换器的类型。
        /// </summary>
        /// <param name="context">System.ComponentModel.ITypeDescriptorContext，提供格式上下文。</param>
        /// <param name="sourceType">一个 System.Type，表示要转换的类型。</param>
        /// <returns>如果该转换器能够执行转换，则为 true；否则为 false。</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            else
            {
                return base.CanConvertFrom(context, sourceType);
            }
        }
        /// <summary>
        /// 将给定值转换为此转换器的类型。
        /// </summary>
        /// <param name="context">System.ComponentModel.ITypeDescriptorContext，提供格式上下文。</param>
        /// <param name="culture"></param>
        /// <param name="value">要转换的 System.Object</param>
        /// <returns> 表示转换的 value 的 System.Object</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                try
                {
                    string[] strC = value.ToString().Split(',');
                    int r = int.Parse(strC[0]);
                    int g = int.Parse(strC[1]);
                    int b = int.Parse(strC[2]);
                    return Color.FromArgb(r, g, b);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    return base.ConvertFrom(context, culture, value);
                }

            }
            return base.ConvertFrom(context, culture, value);
        }
    }


    public class DialogColorEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            //if (context != null && context.Instance != null)
            //{
            //    return UITypeEditorEditStyle.DropDown;
            //}

            //return base.GetEditStyle(context);
            return UITypeEditorEditStyle.DropDown;
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
                System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog();
                colorDialog.Color = (Color)value;
                if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    return colorDialog.Color;
                }
            }
            return value;
        }
    }

    //public class DialogColorEditor : UITypeEditor
    //{
    //    private IWindowsFormsEditorService m_WindFormEditService;

    //    /// <summary>
    //    /// 获取由 System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)方法使用的编辑器样式         
    //    /// </summary>
    //    /// <param name="context">提供有关组件的上下文信息</param>
    //    /// <returns>System.Drawing.Design.UITypeEditorEditStyle 枚举值，该值指示当前 System.Drawing.Design.UITypeEditor
    //    ///  所使用的编辑器样式。在默认情况下，此方法将返回 System.Drawing.Design.UITypeEditorEditStyle.None。
    //    ///  </returns>
    //    public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
    //    {
    //        if (context != null && context.Instance != null)
    //        {
    //            return UITypeEditorEditStyle.Modal;
    //        }
    //        return UITypeEditorEditStyle.Modal;
    //        return base.GetEditStyle(context);
    //    }

    //    /// <summary>
    //    /// 编辑指定对象的值。
    //    /// </summary>
    //    /// <param name="context">提供有关组件的上下文信息</param>
    //    /// <param name="provider">System.IServiceProvider，此编辑器可用其来获取服务</param>
    //    /// <param name="value">要编辑的对象</param>
    //    /// <returns>返回新的对象值</returns>
    //    public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
    //    {
    //        if (context != null && context.Instance != null && provider != null)
    //        {
    //            //得到当前主窗体实例
    //            m_WindFormEditService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));


    //            if (m_WindFormEditService != null)
    //            {
    //                try
    //                {
    //                    System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog();
    //                    colorDialog.Color = (Color)value;
    //                    if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
    //                    {
    //                        return colorDialog.Color;
    //                    }

    //                }
    //                catch
    //                {
    //                    //此处什么都不用处理,但必需.
    //                }
    //            }
    //            return value;
    //        }

    //        return value;
    //    }
    //}
}
