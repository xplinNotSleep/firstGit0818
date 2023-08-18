using System;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// �ṩ�����ڿռ�ο����ֵ�༭�����࣬���ṩ�û����� (UI)��������ʾ�ͱ༭��֧�ֵ��������͵Ķ���ֵ��
    /// </summary>
    public class GeoReferenceEditor:UITypeEditor
    {
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public GeoReferenceEditor()
        {
        }

        /// <summary>
        /// ��ȡ�� System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)����ʹ�õı༭����ʽ         
        /// </summary>
        /// <param name="context">�ṩ�й��������������Ϣ</param>
        /// <returns>System.Drawing.Design.UITypeEditorEditStyle ö��ֵ����ֵָʾ��ǰ System.Drawing.Design.UITypeEditor
        ///  ��ʹ�õı༭����ʽ����Ĭ������£��˷��������� System.Drawing.Design.UITypeEditorEditStyle.None��
        ///  </returns>
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            if (context != null && context.Instance != null)
            {
                return UITypeEditorEditStyle.Modal;
            }
            return base.GetEditStyle(context);
        }

        /// <summary>
        /// �༭ָ�������ֵ��
        /// </summary>
        /// <param name="context">�ṩ�й��������������Ϣ</param>
        /// <param name="provider">System.IServiceProvider���˱༭������������ȡ����</param>
        /// <param name="value">Ҫ�༭�Ķ���</param>
        /// <returns>�����µĶ���ֵ</returns>
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        { 
            //�ṩһ��������ʾ�Ľӿ�
            IWindowsFormsEditorService editorService = null;

            if (context != null && context.Instance != null && provider != null)
            {
                //�õ���ǰ������ʵ��
                editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (editorService != null)
                {
                    try
                    {
                        //��ѯ�ӿ�
                        ISpatialReference tSpatialReference = value as ISpatialReference;

                        if (tSpatialReference != null)
                        {
                            //ʵ�����ռ�ο��Ի�����
                            SpatialReferenceDialog tSpatialReferenceDlg = new SpatialReferenceDialog();
                            //��������������ʾ
                            tSpatialReferenceDlg.ShowInTaskbar = false;
                            //���õ�ǰ�༭�Ŀռ�ο���ϵ
                            tSpatialReferenceDlg.SpatialReference = tSpatialReference;

                            if (editorService.ShowDialog(tSpatialReferenceDlg) == DialogResult.OK)
                            {
                                return tSpatialReferenceDlg.SpatialReference;
                            }
                        }
                    }
                    catch
                    {
                        //�˴�ʲô�����ô���,������.
                        //(�޸Ŀռ�ο���δ���ü�����������,�ٴ��޸Ļᷢ���ڴ洦������)
                    }
                }
            }

            return value;          
        }

        /// <summary>
        /// ָʾ�ñ༭���Ƿ�֧�ֻ��ƶ���ֵ�ı�ʾ��ʽ��
        /// </summary>
        /// <param name="context">�ṩ�й��������������Ϣ</param>
        /// <returns>
        /// ���ʵ���� System.Drawing.Design.UITypeEditor.PaintValue(System.Object,System.Drawing.Graphics,System.Drawing.Rectangle)��
        /// ��Ϊtrue������Ϊ false��
        ///</returns>
        public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return false;
        }
    }
}
