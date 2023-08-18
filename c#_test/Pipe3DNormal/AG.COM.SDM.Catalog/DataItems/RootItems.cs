using System;
using System.Collections.Generic;
using System.Text;

namespace AG.COM.SDM.Catalog.DataItems
{
    /// <summary>
    /// 磁盘连接数据项 描述说明
    /// </summary>
    public class DiskConnectionRootItem : DataItem
    {
        /// <summary>
        /// 初始化磁盘连接数据项实例
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
    /// 数据库根目录数据项 描述说明
    /// </summary>
    public class DatabaseRootItem : DataItem
    {
        private string m_ConnFolder = CommonConst.m_ConnectPropertyPath;

        /// <summary>
        /// 初始化数据库根目录数据项实例对象
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
    /// 服务根目录数据项
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

            //不用添加IMS服务，
            //item = new AddImsCommandItem();
            //item.Parent = this;
            //items.Add(item);

            //屏蔽该处，暂不支持添加新的arcserver连接操作
            //保存arcgis server存在技术问题，未解决
            //item = new AddAgsCommandItem();
            //item.Parent = this;
            //items.Add(item);

            return items;
        }

    }

    /// <summary>
    /// 历史位置根目录数据项 描述说明
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
            //由于历史功能无用，暂时屏蔽
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
