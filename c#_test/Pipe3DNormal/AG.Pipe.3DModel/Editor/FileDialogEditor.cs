using System;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace AG.Pipe.Analyst3DModel.Editor
{
    /// <summary>
    /// 模型符号文件选择器
    /// </summary>
    public class FileDialogEditor : UITypeEditor
    {
        private IWindowsFormsEditorService m_WindFormEditService;

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
            if (context != null && context.Instance != null && provider != null)
            {
                //得到当前主窗体实例
                m_WindFormEditService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));


                if (m_WindFormEditService != null)
                {
                    try
                    {
                        OpenFileDialog openFileDlg = new OpenFileDialog();
                        openFileDlg.Filter = "模型文件(*.dae)|*.dae";
                        openFileDlg.Title = "选择模型文件";
                        //openFileDlg.Filter = "样式符号文件(*.ServerStyle)|*.ServerStyle";
                        //openFileDlg.Title = "选择样式符号文件";

                        if (openFileDlg.ShowDialog() == DialogResult.OK)
                        {
                            return openFileDlg.FileName;
                        }

                    }
                    catch
                    {
                        //此处什么都不用处理,但必需.
                    }
                }
                return value;
            }

            return value;
        }
    }


    /// <summary>
    /// 3D符号样式配置文件选择器
    /// </summary>
    public class StyleFile3DEditor : UITypeEditor
    {
        private IWindowsFormsEditorService m_WindFormEditService;

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
            if (context != null && context.Instance != null && provider != null)
            {
                //得到当前主窗体实例
                m_WindFormEditService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));


                if (m_WindFormEditService != null)
                {
                    try
                    {
                        OpenFileDialog openFileDlg = new OpenFileDialog();
                        openFileDlg.Filter = "ESRI ServerStyle(*.ServerStyle)|*.ServerStyle";
                        openFileDlg.Title = "选择样式符号文件";

                        if (openFileDlg.ShowDialog() == DialogResult.OK)
                        {
                            return openFileDlg.FileName;
                        }

                    }
                    catch
                    {
                        //此处什么都不用处理,但必需.
                    }
                }
                return value;
            }

            return value;
        }
    }

    /// <summary>
    /// 投影转换文件文件选择器
    /// </summary>
    public class ProjectFileEditor : UITypeEditor
    {
        private IWindowsFormsEditorService m_WindFormEditService;

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
            if (context != null && context.Instance != null && provider != null)
            {
                //得到当前主窗体实例
                m_WindFormEditService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));


                if (m_WindFormEditService != null)
                {
                    try
                    {
                        OpenFileDialog openFileDlg = new OpenFileDialog();
                        openFileDlg.Filter = "(*.prj)|*.prj";
                        openFileDlg.Title = "选择投影转换文件";

                        if (openFileDlg.ShowDialog() == DialogResult.OK)
                        {
                            return openFileDlg.FileName;
                        }

                    }
                    catch
                    {
                        //此处什么都不用处理,但必需.
                    }
                }
                return value;
            }

            return value;
        }
    }
}
