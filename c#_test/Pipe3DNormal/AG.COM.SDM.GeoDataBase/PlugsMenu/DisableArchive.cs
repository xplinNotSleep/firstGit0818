using System;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// ɾ����ʷ�浵 ����� 
    /// ע��:���ݵ�hook��������ӦΪIDataset
    /// </summary>
    internal sealed class DisableArchive : BaseCommand
    {
        private IDataset m_Dataset;

        /// <summary>
        /// ʵ�����ö���
        /// </summary>
        public DisableArchive()
        {
            this.m_caption = "ɾ����ʷ�浵";
            this.m_toolTip = "ɾ����ʷ�浵";
            this.m_name = "DisableArchive";
        }

        /// <summary>
        /// ��ȡ��ǰ����Ŀ���״̬
        /// </summary>
        public override bool Enabled
        {
            get
            {
                if (this.m_Dataset != null)
                {
                    IArchivableObject tArchivableObj = this.m_Dataset as IArchivableObject;
                    if (tArchivableObj != null && tArchivableObj.IsArchiving == true) return true; 
                }

                return false;
            }
        }

        /// <summary>
        /// �����¼�����
        /// </summary>
        public override void OnClick()
        {
            DialogResult tDlgResult = MessageBox.Show("�Ƿ�ɾ����˶����������������ʷ�浵?", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
            
            //��ѯ�ӿ����� 
            IArchivableObject tArchiveObj = this.m_Dataset as IArchivableObject;

            if (tDlgResult == DialogResult.Yes)
            {    
                //ɾ��������ص���ʷ�浵
                tArchiveObj.DisableArchiving(true, true);
            }
            else if (tDlgResult == DialogResult.No)
            {
                //��ɾ���䱾��
                tArchiveObj.DisableArchiving(true, false);
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// ����ʱ��ʼ��
        /// </summary>
        /// <param name="hook">hook���󣬴��ݵ�hook��������ӦΪIDataset</param>
        public override void OnCreate(object hook)
        {
            if (hook == null) return;

            if (hook is IDataset)
            {
                this.m_Dataset = hook as IDataset;
            }
            else
            {
                throw new Exception("���ݵ�hook������IDataset���Ͳ�һ��~.\r\n Source:At DisableArchive OnCreate(object hook)");
            }
        }
    }
}
