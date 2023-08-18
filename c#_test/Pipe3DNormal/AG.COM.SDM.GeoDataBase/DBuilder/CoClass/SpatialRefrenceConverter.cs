using System;
using System.ComponentModel;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// �ṩ�ռ�ο�����ת������
    /// </summary>
    public class SpatialRefrenceConverter:ExpandableObjectConverter 
    {
        /// <summary>
        /// Ĭ�Ϲ��캯��,��ʼ����ʵ��
        /// </summary>
        public SpatialRefrenceConverter()
        {
        }
        /// <summary>
        /// ʹ�ò�����������ֵ����ת��Ϊָ�������͡�
        /// </summary>
        /// <param name="context"> �ṩ��ʽ�����ġ�</param>
        /// <param name="culture">�������null������õ�ǰ�����ԡ�</param>
        /// <param name="value"> Ҫת���� System.Object��</param>
        /// <param name="destinationType">Ҫת������ System.Type</param>
        /// <returns>��ʾת���� value �� System.Object��</returns>
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (value is ISpatialReference)
            {
                ISpatialReference tempSR = (ISpatialReference)value;
                //����������ֻ������������
                return tempSR.Name;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// ���ش�ת�����Ƿ����ʹ��ָ���������Ľ��ö���ת��Ϊָ�������͡�
        /// </summary>
        /// <param name="context"> System.ComponentModel.ITypeDescriptorContext���ṩ��ʽ�����ġ�</param>
        /// <param name="destinationType">һ�� System.Type����ʾҪת���������͡�</param>
        /// <returns>�����ת�����ܹ�ִ��ת������Ϊ true������Ϊ false��</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(ISpatialReference))
                return true;
            else
                return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// ���ظ�ת�����Ƿ����ʹ��ָ���������Ľ��������͵Ķ���ת��Ϊ��ת���������͡�
        /// </summary>
        /// <param name="context">System.ComponentModel.ITypeDescriptorContext���ṩ��ʽ�����ġ�</param>
        /// <param name="sourceType">һ�� System.Type����ʾҪת�������͡�</param>
        /// <returns>�����ת�����ܹ�ִ��ת������Ϊ true������Ϊ false��</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(ISpatialReference))
                return true;
            else
                return base.CanConvertFrom(context, sourceType);
        }
        /// <summary>
        /// ������ֵת��Ϊ��ת���������͡�
        /// </summary>
        /// <param name="context">System.ComponentModel.ITypeDescriptorContext���ṩ��ʽ�����ġ�</param>
        /// <param name="culture"></param>
        /// <param name="value">Ҫת���� System.Object</param>
        /// <returns> ��ʾת���� value �� System.Object</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is ISpatialReference)
            {
                return value;
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}
