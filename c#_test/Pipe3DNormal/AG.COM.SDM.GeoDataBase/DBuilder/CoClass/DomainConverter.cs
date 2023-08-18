using System;
using System.ComponentModel;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// 提供属性域对象的转换
    /// </summary>
    public class DomainConverter:ExpandableObjectConverter
    {
        /// <summary>
        /// 默认构造函数,初始化新实例
        /// </summary>
        public DomainConverter()
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
            if (value is IDomain)
            {
                IDomain tempDomain = (IDomain)value;
                //在这里我们只返回它的名称
                return string.Format("{0}/{1}", tempDomain.Name, tempDomain.Description);
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
            if (destinationType == typeof(IDomain))
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
            if (sourceType == typeof(IDomain))
                return true;
            else
                return base.CanConvertFrom(context, sourceType);
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
            if (value is IDomain)
            {
                return value;
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
