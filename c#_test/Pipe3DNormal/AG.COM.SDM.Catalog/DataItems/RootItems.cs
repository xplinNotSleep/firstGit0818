using System;
using System.Collections.Generic;
using System.Text;

namespace AG.COM.SDM.Catalog.DataItems
{
    /// <summary>
    /// �������������� ����˵��
    /// </summary>
    public class DiskConnectionRootItem : DataItem
    {
        /// <summary>
        /// ��ʼ����������������ʵ��
        /// </summary>
        public DiskConnectionRootItem()
        {
            Parent = null;
        }

        public override bool HasChildren
        {
            get
            {
                return true;
            }
        }

        public override IList<DataItem> GetChildren()
        {
            IList<DataItem> items = new List<DataItem>();
            string[] disks = System.IO.Directory.GetLogicalDrives();
            for (int i = 0; i <= disks.Length - 1; i++)
            {
                DataItem item = new DataItems.DiskItem(disks[i]);
                item.Parent = this;
                items.Add(item);
            }
            return items;
        }
    }

    /// <summary>
    /// ���ݿ��Ŀ¼������ ����˵��
    /// </summary>
    public class DatabaseRootItem : DataItem
    {
        private string m_ConnFolder = CommonConst.m_ConnectPropertyPath;

        /// <summary>
        /// ��ʼ�����ݿ��Ŀ¼������ʵ������
        /// </summary>
        public DatabaseRootItem()
        {
            if (!System.IO.Directory.Exists(m_ConnFolder))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(m_ConnFolder);
                }
                catch
                {
                }
            }

            Parent = null;
        }

        public override bool HasChildren
        {
            get
            {
                return true;
            }
        }

        public override IList<DataItem> GetChildren()
        {
            DataItem item;
            IList<DataItem> items = new List<DataItem>();
            string[] fns = System.IO.Directory.GetFiles(m_ConnFolder, "*.sde");
            foreach (string fn in fns)
            {
                item = new DatabaseConnectionItem(fn);
                item.Parent = this;
                items.Add(item);
            }
            item = new AddConnectionCommandItem();
            item.Parent = this;
            items.Add(item);

            return items;
        }
    }

    /// <summary>
    /// �����Ŀ¼������
    /// </summary>
    public class ServiceRootItem : DataItem
    {
        private string m_ConnFolder = CommonConst.m_ConnectPropertyPath;
        public ServiceRootItem()
        {
            if (!System.IO.Directory.Exists(m_ConnFolder))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(m_ConnFolder);
                }
                catch
                { }
            }

            Parent = null;
        }

        public override bool HasChildren
        {
            get
            {
                return true;
            }
        }

        public override IList<DataItem> GetChildren()
        {
            DataItem item;
            IList<DataItem> items = new List<DataItem>();
            string[] fns = System.IO.Directory.GetFiles(m_ConnFolder, "*.myims");
            foreach (string fn in fns)
            {
                item = new ImsConnectionItem(fn);
                item.Parent = this;
                items.Add(item);
            }

            fns = System.IO.Directory.GetFiles(m_ConnFolder, "*.ags");
            foreach (string fn in fns)
            {
                item = new AgsConnectionItem(fn);
                item.Parent = this;
                items.Add(item);
            }

            //�������IMS����
            //item = new AddImsCommandItem();
            //item.Parent = this;
            //items.Add(item);

            //���θô����ݲ�֧������µ�arcserver���Ӳ���
            //����arcgis server���ڼ������⣬δ���
            //item = new AddAgsCommandItem();
            //item.Parent = this;
            //items.Add(item);

            return items;
        }

    }

    /// <summary>
    /// ��ʷλ�ø�Ŀ¼������ ����˵��
    /// </summary>
    public class HistoryRootItem : DataItem
    {
        public override bool HasChildren
        {
            get
            {
                return true;
            }
        }

        public override IList<DataItem> GetChildren()
        {
            DataItem item;
            IList<DataItem> items = new List<DataItem>();
            //������ʷ�������ã���ʱ����
            string ln;
            for (int i = 0; i <= HistoryKeeperInstance.Instance.Histories.Count - 1; i++)
            {
                ln = HistoryKeeperInstance.Instance.Histories[i];
                item = new HisLocationItem(ln);
                items.Add(item);
            }

            return items;
        }
    }
}
