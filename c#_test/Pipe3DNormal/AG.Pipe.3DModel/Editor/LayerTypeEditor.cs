using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using ESRI.ArcGIS.Geodatabase;
using AG.Pipe.Analyst3DModel.Editor;

namespace AG.Pipe.Analyst3DModel
{
    /// <summary>
    /// 单选字段编辑器
    /// </summary>
    /// <remarks coder="3echo" date="2009-09-13"></remarks>
    public class LayerTypeEditor : UITypeEditor
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
                return UITypeEditorEditStyle.DropDown;
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
                        m_WindFormEditService.CloseDropDown();

                        //上下信息类型转换
                        //ItemTypeDescriptor tItemType = context.Instance as ItemTypeDescriptor;
                        SchemeTypeDescriptor tSchemePro = context.Instance as SchemeTypeDescriptor;
                        ConvertLayerSet tLayerSet = tSchemePro.GetPropertyOwner(null) as ConvertLayerSet;
                        if (tLayerSet == null) return null;
                        //if (tDataCheckRule.DataCheckRuleLayer.SourceFeatClass == null) return null;

                        ListBox tListField = new ListBox();
                        tListField.BorderStyle = BorderStyle.None;
                        tListField.Items.Add("点");
                        tListField.Items.Add("线");
                        tListField.Items.Add("面");
                        //tListField.Items.Add("注记");
                        tListField.Items.Add("属性表");
                        tListField.Items.Add("附属物");

                        //tListField.ItemCheck += new ItemCheckEventHandler(tListField_ItemCheck);
                        //添加事件处理
                        tListField.SelectedValueChanged += new EventHandler(tListField_SelectedValueChanged);

                        //为其下拉区域显式指定控件
                        m_WindFormEditService.DropDownControl(tListField);

                        if (tListField.SelectedIndex >= 0)
                        {
                            return tListField.SelectedItem;
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

        /// <summary>
        /// ListBox选择项发生改变的事情处理
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">事件参数</param>
        private void tListField_SelectedValueChanged(object sender, EventArgs e)
        {
            ListBox tListBox = sender as ListBox;
            if (tListBox.SelectedItem != null)
            {
                this.m_WindFormEditService.CloseDropDown();
            }
        }
    }
}
