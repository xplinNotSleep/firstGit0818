using System;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace AG.COM.SDM.GeoDataBase.DBuilder
{
    /// <summary>
    /// �ṩ�������ֶ�������ֵ�༭�����࣬���ṩ�û����� (UI)��������ʾ�ͱ༭��֧�ֵ��������͵Ķ���ֵ��
    /// </summary>
    public class ListDomainEditor:UITypeEditor
    {
        private IWindowsFormsEditorService m_WindFormEditService;

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
                return UITypeEditorEditStyle.DropDown;
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
            if (context != null && context.Instance != null && provider != null)
            {
                //�õ���ǰ������ʵ��
                m_WindFormEditService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (m_WindFormEditService != null)
                {
                    try
                    {
                        m_WindFormEditService.CloseDropDown();
                        ListBox tListDomain = new ListBox();              
                        tListDomain.BorderStyle = BorderStyle.None;
                        //��������ԴΪ���е�������
                        tListDomain.DataSource = DBInfoDialog.GetAllDomains();                         
                        //����¼�����
                        tListDomain.SelectedValueChanged += new EventHandler(tListDomain_SelectedValueChanged);                         
                        //Ϊ������������ʽָ���ؼ�
                        m_WindFormEditService.DropDownControl(tListDomain);                        

                        if (tListDomain.SelectedIndex == 0) 
                            return null;
                        else 
                            return tListDomain.SelectedItem;                                                          
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

        /// <summary>
        /// ListBoxѡ������ı�����鴦��
        /// </summary>
        /// <param name="sender">����</param>
        /// <param name="e">�¼�����</param>
        private void tListDomain_SelectedValueChanged(object sender, EventArgs e)
        {
            ListBox tListBox = sender as ListBox;
            if (tListBox.SelectedItem != null)
            {
                this.m_WindFormEditService.CloseDropDown();
            }
        }
    }
}
