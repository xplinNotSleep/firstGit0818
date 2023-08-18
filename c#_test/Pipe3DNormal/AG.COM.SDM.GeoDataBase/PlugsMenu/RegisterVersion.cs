using System;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// ע��汾����� 
    /// ע��:���ݵ�hook��������ӦΪIDataset
    /// </summary>
    internal sealed class RegisterVersion : BaseCommand
    {
        private IDataset m_Dataset;

        /// <summary>
        /// ʵ�����ö���
        /// </summary>
        public RegisterVersion()
        {
            this.m_caption = "ע��汾";
            this.m_toolTip = "ע��汾";
            this.m_name = "RegisterVersion";
        }

        /// <summary>
        /// ��ȡ��ǰ����Ŀ���״̬
        /// </summary>
        public override bool Enabled
        {
            get
            {
                if (this.m_Dataset == null)
                    return false;
                else 
                {
                    IVersionedObject tVersionedObj = this.m_Dataset as IVersionedObject;
                    if (tVersionedObj == null) return false;
                    if (tVersionedObj.IsRegisteredAsVersioned == true) return false;
                    return true;
                }
            }
        }

        public override void OnClick()
        {
            //��ѯ�ӿ�����
            IVersionedObject3 tVersionedObject = this.m_Dataset as IVersionedObject3;

            //��ȡ�Ի��򷵻ؽ��
            DialogResult tDlgResult = MessageBox.Show("�༭�ð汾ʱ,�Ƿ񽫱༭������µ��ϼ��汾?", "��ʾ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
            if (tDlgResult == DialogResult.Yes)
            {
                tVersionedObject.RegisterAsVersioned3(true);
            }
            else if (tDlgResult == DialogResult.No)
            {
                tVersionedObject.RegisterAsVersioned3(false);
            }
            else
            {
                //ʲô������
            }
        }

        /// <summary>
        /// ����ʱ��ʼ��
        /// </summary>
        /// <param name="hook">hook����</param>
        public override void OnCreate(object hook)
        {
            if (hook != null)
            {
                if (hook is IDataset)
                {
                    this.m_Dataset = hook as IDataset;
                }
                else
                {
                    throw new Exception("���ݵ�hook������IDataset���Ͳ�һ��~.\r\n Source:At RegisterVersion OnCreate(object hook)");
                }
            }
        }
    }
}
