using System;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// ȡ��ע������
    /// ע��:���ݵ�hook����ӦΪIDataset����
    /// </summary>
    internal sealed class UnRegisterVersion : BaseCommand
    {
        private IDataset m_Dataset;

        /// <summary>
        /// ʵ�����ö���
        /// </summary>
        public UnRegisterVersion()
        {
            this.m_caption = "ȡ��ע��";
            this.m_toolTip = "ȡ��ע��";
            this.m_name = "UnRegisterVersion";
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
                    //��ѯ�ӿ�����
                    IVersionedObject3 tVersionedObject = this.m_Dataset as IVersionedObject3;
                    if (tVersionedObject != null && tVersionedObject.IsRegisteredAsVersioned)
                    {
                        //��ѯ�ӿ�����
                        IArchivableObject tArchivableObject = tVersionedObject as IArchivableObject;
                        if (tArchivableObject.IsArchiving == false)
                        {
                            return true;
                        }      
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
            IVersionedObject3 tVersionedObject = this.m_Dataset as IVersionedObject3;
            tVersionedObject.UnRegisterAsVersioned3(false);
        }

        /// <summary>
        /// ����ʱ��ʼ��
        /// </summary>
        /// <param name="hook">hook����</param>
        public override void OnCreate(object hook)
        {
            if (hook == null) return;

            if (hook is IDataset)
            {
                this.m_Dataset = hook as IDataset;
            }
            else
            {
                throw new Exception("���ݵ�hook������IDataset���Ͳ�һ��~.\r\n Source:At UnRegisterVersion OnCreate(object hook)");
            }
        }
    }
}
