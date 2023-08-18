using AG.COM.SDM.Catalog;
using AG.COM.SDM.Catalog.DataItems;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace AG.Pipe.Analyst3DModel.Editor
{
    /// <summary>
    /// 空间图层编辑器
    /// </summary>
    public class LineLayerEditor : UITypeEditor
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
                        LineScheme lineScheme = null;
                        PointScheme pointScheme = null;
                        if (context.Instance is LineScheme)
                        {
                            lineScheme = context.Instance as LineScheme;
                        }
                        if (context.Instance is PointScheme)
                        {
                            pointScheme = context.Instance as PointScheme;
                        }

                        IDataBrowser tDataBrowser = new FormDataBrowser();
                        tDataBrowser.MultiSelect = false;
                        tDataBrowser.AddFilter(new AG.COM.SDM.Catalog.Filters.LineFeatureClassFilter());
                        if (tDataBrowser.ShowDialog() == DialogResult.OK)
                        {
                            IList<DataItem> items = tDataBrowser.SelectedItems;
                            DataItem item = items[0];
                            if (lineScheme != null)
                                lineScheme.WorkPath = items[0].Workspace.PathName;

                            IFeatureClass pLineFeatureClass = null;
                            if(item.Workspace==null)
                            {
                                IWorkspace workspace = item.Parent.Workspace;
                                if (workspace == null) return items[0].Name;
                                else
                                {
                                    pLineFeatureClass = (workspace as IFeatureWorkspace).OpenFeatureClass(item.Name);
                                }
                            }
                            else
                            {
                                pLineFeatureClass = (item.Workspace as IFeatureWorkspace).OpenFeatureClass(item.Name);
                            }
                            IFields pFields = (pLineFeatureClass.Fields as IClone).Clone() as IFields;
                            List<string> lstFields = new List<string>();
                            lstFields.Add("");
                            for (int i = 0; i < pFields.FieldCount; i++)
                            {
                                IField pField = pFields.Field[i];
                                if (!pField.Editable) continue;
                                if (pField.Type == esriFieldType.esriFieldTypeGeometry) continue;
                                lstFields.Add(pField.Name);

                            }
                            if (lineScheme != null)
                                lineScheme.LineFields = lstFields;
                            if (pointScheme != null)
                                pointScheme.LineFields = lstFields;
                            return items[0].Name;
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

    public class PointLayerEditor : UITypeEditor
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
                        PointScheme pointScheme = null;
                        if (context.Instance is PointScheme)
                        {
                            pointScheme = context.Instance as PointScheme;
                        }

                        IDataBrowser tDataBrowser = new FormDataBrowser();
                        tDataBrowser.MultiSelect = false;
                        tDataBrowser.AddFilter(new AG.COM.SDM.Catalog.Filters.PointFeatureClassFilter());
                        if (tDataBrowser.ShowDialog() == DialogResult.OK)
                        {
                            IList<DataItem> items = tDataBrowser.SelectedItems;
                            DataItem item = items[0];
                            if (pointScheme != null)
                                
                                pointScheme.WorkPath = items[0].Workspace.PathName;


                            IFeatureClass pLineFeatureClass = null;
                            if (item.Workspace==null)
                            {
                                IWorkspace workspace = item.Parent.Workspace;
                                if (workspace == null) return item.Name;
                                pLineFeatureClass = (workspace as IFeatureWorkspace).OpenFeatureClass(item.Name);
                            }
                            else
                            {
                                pLineFeatureClass = (item.Workspace as IFeatureWorkspace).OpenFeatureClass(item.Name);
                            }
                            
                            IFields pFields = (pLineFeatureClass.Fields as IClone).Clone() as IFields;
                            List<string> lstFields = new List<string>();
                            lstFields.Add("");
                            for (int i = 0; i < pFields.FieldCount; i++)
                            {
                                IField pField = pFields.Field[i];
                                if (!pField.Editable) continue;
                                if (pField.Type == esriFieldType.esriFieldTypeGeometry) continue;
                                lstFields.Add(pField.Name);

                            }
                            if (pointScheme != null)
                                pointScheme.PointFields = lstFields;
                            return items[0].Name;
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
