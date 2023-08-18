using System;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// 提供可用于空间参考设计值编辑器的类，可提供用户界面 (UI)，用来表示和编辑所支持的数据类型的对象值。
    /// </summary>
    public class GeoReferenceEditor:UITypeEditor
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public GeoReferenceEditor()
        {
        }

        /// <summary>
        /// 获取由 System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)方法使用的编辑器样式         
        /// </summary>
        /// <param name="context">提供有关组件的上下文信息</param>
        /// <returns>System.Drawing.Design.UITypeEditorEditStyle 枚举值，该值指示当前 System.Drawing.Design.UITypeEditor
        ///  所使用的编辑器样式。在默认情况下，此方法将返回 System.Drawing.Design.UITypeEditorEditStyle.None。
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
        /// 编辑指定对象的值。
        /// </summary>
        /// <param name="context">提供有关组件的上下文信息</param>
        /// <param name="provider">System.IServiceProvider，此编辑器可用其来获取服务</param>
        /// <param name="value">要编辑的对象</param>
        /// <returns>返回新的对象值</returns>
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        { 
            //提供一个窗体显示的接口
            IWindowsFormsEditorService editorService = null;

            if (context != null && context.Instance != null && provider != null)
            {
                //得到当前主窗体实例
                editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (editorService != null)
                {
                    try
                    {
                        //查询接口
                        ISpatialReference tSpatialReference = value as ISpatialReference;

                        if (tSpatialReference != null)
                        {
                            //实例化空间参考对话框类
                            SpatialReferenceDialog tSpatialReferenceDlg = new SpatialReferenceDialog();
                            //不在任务栏上显示
                            tSpatialReferenceDlg.ShowInTaskbar = false;
                            //设置当前编辑的空间参考关系
                            tSpatialReferenceDlg.SpatialReference = tSpatialReference;

                            if (editorService.ShowDialog(tSpatialReferenceDlg) == DialogResult.OK)
                            {
                                return tSpatialReferenceDlg.SpatialReference;
                            }
                        }
                    }
                    catch
                    {
                        //此处什么都不用处理,但必需.
                        //(修改空间参考后未来得及保存的情况下,再次修改会发生内存处理问题)
                    }
                }
            }

            return value;          
        }

        /// <summary>
        /// 指示该编辑器是否支持绘制对象值的表示形式。
        /// </summary>
        /// <param name="context">提供有关组件的上下文信息</param>
        /// <returns>
        /// 如果实现了 System.Drawing.Design.UITypeEditor.PaintValue(System.Object,System.Drawing.Graphics,System.Drawing.Rectangle)，
        /// 则为true；否则为 false。
        ///</returns>
        public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return false;
        }
    }
}
