using System;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.GeoDataBase
{
    /// <summary>
    /// ɾ��ͼ������
    /// ע��:���ݵ�hook����ΪIDataset����
    /// </summary>
    internal sealed class DeleteFeatClass : BaseCommand
    {
        private IDataset m_Dataset;

        /// <summary>
        /// ʵ�����ö���
        /// </summary>
        public DeleteFeatClass()
        {
            this.m_caption = "ɾ��ͼ��";
            this.m_toolTip = "ɾ��ͼ��";
            this.m_name = "DeleteFeatClass";
        }

        /// <summary>
        /// ��ȡ��ǰ����Ŀ���״̬
        /// </summary>
        public override bool Enabled
        {
            get
            {
                return (this.m_Dataset == null) ? false : true;
            }
        }

        /// <summary>
        /// OnClick������
        /// </summary>
        public override void OnClick()
        {
            if (MessageBox.Show("�ò������ɻָ�,���Ƿ�ȷʵ��Ҫɾ���˶���?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                IDataset tDataset = this.m_Dataset as IDataset;
                tDataset.Delete();
            }
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
                throw new Exception("���ݵ�hook������IDataset���Ͳ�һ��~.\r\n Source:At DeleteFeatClass OnCreate(object hook)");
            }
        }
    }
}
