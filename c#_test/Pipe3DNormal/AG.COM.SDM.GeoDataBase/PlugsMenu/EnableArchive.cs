using System;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// ������ʷ�浵 ����� 
    /// ע��:���ݵ�hook��������ӦΪIDataset
    /// </summary>
    internal sealed class EnableArchive : BaseCommand
    {
        private IDataset m_Dataset;

        /// <summary>
        /// ʵ�����ö���
        /// </summary>
        public EnableArchive()
        {
            this.m_caption = "������ʷ�浵";
            this.m_toolTip = "������ʷ�浵";
            this.m_name = "EnableArchive";
        }

        /// <summary>
        /// ��ȡ��ǰ����Ŀ���״̬
        /// </summary>
        public override bool Enabled
        {
            get
            {
                if (this.m_Dataset == null) return false;

                //��ѯ�ӿ�����
                IVersionedObject3 tVersionObject = this.m_Dataset as IVersionedObject3;
                if (tVersionObject != null)
                {
                    //��ȡ�汾ע����Ϣ
                    bool isRegistered,isMovingEditsToBase;                    
                    tVersionObject.GetVersionRegistrationInfo(out isRegistered,out isMovingEditsToBase);

                    if (isRegistered == true && isMovingEditsToBase == false)
                    {
                        IArchivableObject tArchiveObject = tVersionObject as IArchivableObject;
                        if (tArchiveObject.IsArchiving == false) return true;
                    }
                }
    
                return false;                
            }
        }

        /// <summary>
        /// �����¼�����
        /// </summary>
        public override void OnClick()
        {
            IArchivableObject tArchivableObject = this.m_Dataset as IArchivableObject;
            tArchivableObject.EnableArchiving(null, null, true);
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
                    throw new Exception("���ݵ�hook������IDataset���Ͳ�һ��~.\r\n Source:At EnableArchive OnCreate(object hook)");
                }
            }
        }
    }
}
